using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000999 RID: 2457
	internal class RequesterPool
	{
		// Token: 0x06004C0E RID: 19470 RVA: 0x001306B4 File Offset: 0x0012E8B4
		public IRequester CheckOut(string[] connectInfo, DrdaClientTraceContainer container, Func<string, string, int, int, Exception> exceptionMaker)
		{
			IRequester requester = null;
			bool flag = false;
			if (this._maxPoolSize == -1)
			{
				int.TryParse(connectInfo[32], out this._maxPoolSize);
			}
			object obj = this._lock;
			lock (obj)
			{
				if (this._needCleared)
				{
					return null;
				}
				if (this._availableRequesters.Count > 0)
				{
					requester = this._availableRequesters.Dequeue();
					this._busyRequesters.Add(requester);
					((Requester)requester).ConnectionInfo = connectInfo;
				}
				else if (this._maxPoolSize > 0 && this._busyRequesters.Count >= this._maxPoolSize)
				{
					flag = true;
				}
			}
			if (requester == null)
			{
				if (flag)
				{
					throw new InvalidOperationException();
				}
				requester = RequesterFactory.CreateRequester(connectInfo, container, exceptionMaker);
				((Requester)requester).Pool = this;
				obj = this._lock;
				lock (obj)
				{
					if (this._needCleared)
					{
						return null;
					}
					this._busyRequesters.Add(requester);
					return requester;
				}
			}
			((Requester)requester).Reset(container, exceptionMaker);
			return requester;
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x001307E4 File Offset: 0x0012E9E4
		public void CheckIn(IRequester requester)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this._busyRequesters.Remove(requester);
				if (!this._needCleared)
				{
					this._availableRequesters.Enqueue(requester);
				}
				else
				{
					((Requester)requester).DropConnection();
				}
			}
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x0013084C File Offset: 0x0012EA4C
		public void Remove(IRequester requester)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				this._busyRequesters.Remove(requester);
			}
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x00130894 File Offset: 0x0012EA94
		public void Clear()
		{
			object @lock = this._lock;
			lock (@lock)
			{
				if (!this._needCleared)
				{
					this._availableRequesters.AsParallel<IRequester>().ForAll(delegate(IRequester requester)
					{
						((Requester)requester).DropConnection();
					});
					this._availableRequesters.Clear();
					this._needCleared = true;
					this._maxPoolSize = -1;
				}
			}
		}

		// Token: 0x04003C16 RID: 15382
		private object _lock = new object();

		// Token: 0x04003C17 RID: 15383
		private Queue<IRequester> _availableRequesters = new Queue<IRequester>();

		// Token: 0x04003C18 RID: 15384
		private SortedSet<IRequester> _busyRequesters = new SortedSet<IRequester>();

		// Token: 0x04003C19 RID: 15385
		private bool _needCleared;

		// Token: 0x04003C1A RID: 15386
		private int _maxPoolSize = -1;
	}
}
