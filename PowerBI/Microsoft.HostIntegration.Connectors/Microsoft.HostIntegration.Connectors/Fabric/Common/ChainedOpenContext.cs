using System;
using System.ServiceModel;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003F9 RID: 1017
	internal sealed class ChainedOpenContext : OperationContext
	{
		// Token: 0x0600239A RID: 9114 RVA: 0x0006D180 File Offset: 0x0006B380
		public ChainedOpenContext(ICommunicationObject object1, ChainedHandler handler, AsyncCallback callback, object state, TimeSpan timeout)
			: this(object1, null, handler, callback, state, timeout)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0006D19E File Offset: 0x0006B39E
		public ChainedOpenContext(ICommunicationObject object1, ICommunicationObject object2, AsyncCallback callback, object state, TimeSpan timeout)
			: this(object1, object2, null, callback, state, timeout)
		{
			if (object2 == null)
			{
				throw new ArgumentNullException("object2");
			}
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0006D1BC File Offset: 0x0006B3BC
		private ChainedOpenContext(ICommunicationObject object1, ICommunicationObject object2, ChainedHandler handler, AsyncCallback callback, object state, TimeSpan timeout)
			: base(callback, state, timeout)
		{
			if (object1 == null)
			{
				throw new ArgumentNullException("object1");
			}
			this.m_object1 = object1;
			this.m_object2 = object2;
			this.m_handler = handler;
			base.StartTimer();
			IAsyncResult asyncResult = this.m_object1.BeginOpen(timeout, ChainedOpenContext.Callback1, this);
			if (asyncResult.CompletedSynchronously)
			{
				this.End1(asyncResult);
			}
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x0006D224 File Offset: 0x0006B424
		private static void StaticCallback1(IAsyncResult ar)
		{
			if (ar.CompletedSynchronously)
			{
				return;
			}
			ChainedOpenContext chainedOpenContext = (ChainedOpenContext)ar.AsyncState;
			chainedOpenContext.End1(ar);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x0006D250 File Offset: 0x0006B450
		private void End1(IAsyncResult ar)
		{
			try
			{
				this.m_object1.EndOpen(ar);
				if (this.m_handler != null)
				{
					this.m_object2 = this.m_handler();
				}
				IAsyncResult asyncResult = this.m_object2.BeginOpen(base.ExpirationTime.SafeRemainingDuration, ChainedOpenContext.Callback2, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.End2(asyncResult, ar.CompletedSynchronously);
				}
			}
			catch (Exception ex)
			{
				base.CompleteOperation(ar.CompletedSynchronously, ex);
			}
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x0006D2DC File Offset: 0x0006B4DC
		private static void StaticCallback2(IAsyncResult ar)
		{
			if (ar.CompletedSynchronously)
			{
				return;
			}
			ChainedOpenContext chainedOpenContext = (ChainedOpenContext)ar.AsyncState;
			chainedOpenContext.End2(ar, false);
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x0006D308 File Offset: 0x0006B508
		private void End2(IAsyncResult ar, bool completedSynchronously)
		{
			Exception ex = null;
			try
			{
				this.m_object2.EndOpen(ar);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			base.CompleteOperation(completedSynchronously, ex);
		}

		// Token: 0x0400161D RID: 5661
		private ICommunicationObject m_object1;

		// Token: 0x0400161E RID: 5662
		private ICommunicationObject m_object2;

		// Token: 0x0400161F RID: 5663
		private ChainedHandler m_handler;

		// Token: 0x04001620 RID: 5664
		private static AsyncCallback Callback1 = new AsyncCallback(ChainedOpenContext.StaticCallback1);

		// Token: 0x04001621 RID: 5665
		private static AsyncCallback Callback2 = new AsyncCallback(ChainedOpenContext.StaticCallback2);
	}
}
