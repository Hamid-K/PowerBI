using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000036 RID: 54
	internal sealed class ConnectionEntry : LinkedLruCache.ILruEntry
	{
		// Token: 0x06000199 RID: 409 RVA: 0x0000E35C File Offset: 0x0000C55C
		public ConnectionEntry(ConnectionKey connKey, IDbPoolableConnection connection, global::System.Action onConnectionCloseCallBack)
		{
			this.Key = connKey;
			this.Connection = connection;
			this.OnConnectionCloseCallBack = onConnectionCloseCallBack;
			this.ServiceInstanceContext = Microsoft.ReportingServices.Diagnostics.ProcessingContext.ServiceInstanceContext;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000E38F File Offset: 0x0000C58F
		// (set) Token: 0x0600019B RID: 411 RVA: 0x0000E397 File Offset: 0x0000C597
		public ConnectionKey Key { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000E3A0 File Offset: 0x0000C5A0
		// (set) Token: 0x0600019D RID: 413 RVA: 0x0000E3A8 File Offset: 0x0000C5A8
		public IDbPoolableConnection Connection { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600019E RID: 414 RVA: 0x0000E3B1 File Offset: 0x0000C5B1
		// (set) Token: 0x0600019F RID: 415 RVA: 0x0000E3B9 File Offset: 0x0000C5B9
		private global::System.Action OnConnectionCloseCallBack { get; set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001A0 RID: 416 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		// (remove) Token: 0x060001A1 RID: 417 RVA: 0x0000E3FC File Offset: 0x0000C5FC
		public event EventHandler<EventArgs> OnConnectionClosed;

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000E431 File Offset: 0x0000C631
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000E439 File Offset: 0x0000C639
		private IServiceInstanceContext ServiceInstanceContext { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000E442 File Offset: 0x0000C642
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000E44A File Offset: 0x0000C64A
		public LinkedLruCache.ILruEntry Next { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000E453 File Offset: 0x0000C653
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000E45B File Offset: 0x0000C65B
		public LinkedLruCache.ILruEntry Previous { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000E464 File Offset: 0x0000C664
		public bool OKToGetFromPool
		{
			get
			{
				object stateLock = this.m_stateLock;
				bool flag2;
				lock (stateLock)
				{
					flag2 = this.m_state < ConnectionEntry.StateValue.Closing;
				}
				return flag2;
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000E4AC File Offset: 0x0000C6AC
		public void RequestFromPool()
		{
			object stateLock = this.m_stateLock;
			lock (stateLock)
			{
				if (this.m_state < ConnectionEntry.StateValue.Closing)
				{
					this.m_state = ConnectionEntry.StateValue.GettingFromPool;
				}
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
		public void RequestClosing()
		{
			object stateLock = this.m_stateLock;
			lock (stateLock)
			{
				if (this.m_state == ConnectionEntry.StateValue.Initial)
				{
					this.m_state = ConnectionEntry.StateValue.Closing;
				}
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000E544 File Offset: 0x0000C744
		public bool IsClosed
		{
			get
			{
				object stateLock = this.m_stateLock;
				bool flag2;
				lock (stateLock)
				{
					flag2 = this.m_state == ConnectionEntry.StateValue.Closed;
				}
				return flag2;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000E58C File Offset: 0x0000C78C
		public void CloseConnectionAsync()
		{
			ThreadPool.QueueUserWorkItem(delegate(object o)
			{
				try
				{
					this.CloseConnection();
				}
				catch (Exception ex)
				{
					RSTrace.DataExtensionTracer.Trace(TraceLevel.Error, "Exception occurred during async CloseConnection: {0}", new object[] { ex.ToString() });
				}
			});
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000E5A0 File Offset: 0x0000C7A0
		public void CloseConnection()
		{
			object stateLock = this.m_stateLock;
			bool flag2;
			lock (stateLock)
			{
				if (flag2 = this.m_state != ConnectionEntry.StateValue.Closed)
				{
					this.m_state = ConnectionEntry.StateValue.Closed;
				}
			}
			if (flag2)
			{
				try
				{
					this.Connection.Close();
					this.Connection.Dispose();
				}
				catch (Exception ex)
				{
					RSTrace.DataExtensionTracer.Trace(TraceLevel.Error, "Exception occurred closing the connection: {0}", new object[] { ex.ToString() });
				}
				finally
				{
					if (this.OnConnectionCloseCallBack != null)
					{
						this.OnConnectionCloseCallBack();
					}
					if (this.OnConnectionClosed != null)
					{
						this.OnConnectionClosed(this, null);
					}
				}
			}
		}

		// Token: 0x0400012E RID: 302
		private object m_stateLock = new object();

		// Token: 0x0400012F RID: 303
		private ConnectionEntry.StateValue m_state;

		// Token: 0x0200043C RID: 1084
		private enum StateValue
		{
			// Token: 0x04000F34 RID: 3892
			Initial,
			// Token: 0x04000F35 RID: 3893
			GettingFromPool,
			// Token: 0x04000F36 RID: 3894
			Closing,
			// Token: 0x04000F37 RID: 3895
			Closed
		}
	}
}
