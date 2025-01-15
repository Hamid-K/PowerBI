using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000998 RID: 2456
	public class RequesterFactory
	{
		// Token: 0x06004C05 RID: 19461 RVA: 0x00130508 File Offset: 0x0012E708
		private RequesterFactory()
		{
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06004C06 RID: 19462 RVA: 0x00130526 File Offset: 0x0012E726
		public static RequesterFactory Instance
		{
			get
			{
				return RequesterFactory._instance;
			}
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06004C07 RID: 19463 RVA: 0x0013052D File Offset: 0x0012E72D
		public static DrdaClientTraceContainer Container
		{
			get
			{
				return RequesterFactory._traceContainer;
			}
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x00130534 File Offset: 0x0012E734
		public IRequester GetRequester(string[] connectInfo, DrdaClientTraceContainer container, Func<string, string, int, int, Exception> exceptionMaker)
		{
			IRequester requester = null;
			if (!Requester.NeedConnectionPool(connectInfo))
			{
				return RequesterFactory.CreateRequester(connectInfo, container, exceptionMaker);
			}
			RequesterPool requesterPool = null;
			string text = Requester.EncodeConnectInfo(connectInfo);
			this._lock.EnterReadLock();
			try
			{
				this._poolMap.TryGetValue(text, out requesterPool);
			}
			finally
			{
				this._lock.ExitReadLock();
			}
			if (requesterPool != null)
			{
				requester = requesterPool.CheckOut(connectInfo, container, exceptionMaker);
				if (requester != null)
				{
					return requester;
				}
				requesterPool = null;
			}
			this._lock.EnterWriteLock();
			try
			{
				if (!this._poolMap.TryGetValue(text, out requesterPool))
				{
					requesterPool = new RequesterPool();
					this._poolMap.Add(text, requesterPool);
				}
				requester = requesterPool.CheckOut(connectInfo, container, exceptionMaker);
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
			return requester;
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x001305FC File Offset: 0x0012E7FC
		public void ClearPool(IRequester requester)
		{
			this.ClearPool(((Requester)requester).ConnectionInfo);
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x00130610 File Offset: 0x0012E810
		public void ClearPool(string[] connectInfo)
		{
			if (!Requester.NeedConnectionPool(connectInfo))
			{
				return;
			}
			string text = Requester.EncodeConnectInfo(connectInfo);
			RequesterPool requesterPool = null;
			this._lock.EnterWriteLock();
			try
			{
				if (this._poolMap.TryGetValue(text, out requesterPool))
				{
					this._poolMap.Remove(text);
					requesterPool.Clear();
				}
			}
			finally
			{
				this._lock.ExitWriteLock();
			}
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x00130680 File Offset: 0x0012E880
		public long GetCommandSourceId()
		{
			return Interlocked.Increment(ref this._sourceId);
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x0013068D File Offset: 0x0012E88D
		internal static IRequester CreateRequester(string[] connectInfo, DrdaClientTraceContainer container, Func<string, string, int, int, Exception> exceptionMaker)
		{
			Requester requester = new Requester(connectInfo);
			requester.Initialize(container, exceptionMaker);
			return requester;
		}

		// Token: 0x04003C11 RID: 15377
		private static readonly RequesterFactory _instance = new RequesterFactory();

		// Token: 0x04003C12 RID: 15378
		private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

		// Token: 0x04003C13 RID: 15379
		private Dictionary<string, RequesterPool> _poolMap = new Dictionary<string, RequesterPool>();

		// Token: 0x04003C14 RID: 15380
		private static DrdaClientTraceContainer _traceContainer = new DrdaClientTraceContainer();

		// Token: 0x04003C15 RID: 15381
		private long _sourceId;
	}
}
