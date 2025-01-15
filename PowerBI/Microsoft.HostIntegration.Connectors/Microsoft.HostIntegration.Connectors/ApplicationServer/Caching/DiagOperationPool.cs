using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001AE RID: 430
	internal class DiagOperationPool
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0002F768 File Offset: 0x0002D968
		public int TotalPublished
		{
			get
			{
				return this._totalPublished;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0002F770 File Offset: 0x0002D970
		public int Count
		{
			get
			{
				return this._pooledOperationStates.Count;
			}
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0002F77D File Offset: 0x0002D97D
		public DiagOperationPool()
		{
			this._pooledOperationStates = new ConcurrentDictionary<string, DiagOperationState>(16, 10000);
			this._totalPublished = 0;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0002F7A0 File Offset: 0x0002D9A0
		internal DiagOperationState GetOperationState(string key)
		{
			DiagOperationState diagOperationState = null;
			if (!string.IsNullOrEmpty(key))
			{
				this._pooledOperationStates.TryGetValue(key, out diagOperationState);
				return diagOperationState;
			}
			return null;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0002F7CC File Offset: 0x0002D9CC
		internal DiagOperationState GetOrAddOperationState(string key)
		{
			DiagOperationState diagOperationState = null;
			if (!string.IsNullOrEmpty(key))
			{
				try
				{
					diagOperationState = this._pooledOperationStates.GetOrAdd(key, delegate(string k)
					{
						DiagOperationState diagOperationState2 = new DiagOperationState();
						diagOperationState2.UniqueIdentifier = key;
						diagOperationState2.IsRuntimeAware = true;
						Interlocked.Increment(ref this._totalPublished);
						return diagOperationState2;
					});
					return diagOperationState;
				}
				catch (OverflowException ex)
				{
					EventLogWriter.WriteInfo("DiagnosticStats", "{0}", new object[] { ex.ToString() });
				}
				return diagOperationState;
			}
			return diagOperationState;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0002F860 File Offset: 0x0002DA60
		internal DiagOperationState AddOrUpdateOperationState(string key, DiagOperationState opState)
		{
			DiagOperationState diagOperationState = null;
			if (!string.IsNullOrEmpty(key))
			{
				try
				{
					diagOperationState = this._pooledOperationStates.GetOrAdd(key, delegate(string k)
					{
						Interlocked.Increment(ref this._totalPublished);
						opState.IsRuntimeAware = true;
						return opState;
					});
					if (!opState.IsRuntimeAware)
					{
						diagOperationState.Merge(opState);
						opState.IsRuntimeAware = true;
					}
					return diagOperationState;
				}
				catch (OverflowException ex)
				{
					EventLogWriter.WriteInfo("DiagnosticStats", "{0}", new object[] { ex.ToString() });
				}
				return diagOperationState;
			}
			return diagOperationState;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0002F910 File Offset: 0x0002DB10
		internal void AddRequestStates(DiagOperationState states)
		{
			this.AddRequestStates(states.UniqueIdentifier, states);
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0002F920 File Offset: 0x0002DB20
		internal void AddRequestStates(string key, DiagEvent ev)
		{
			if (ev != null)
			{
				DiagOperationState orAddOperationState = this.GetOrAddOperationState(key);
				if (orAddOperationState != null)
				{
					orAddOperationState.AddEvent(ev);
					return;
				}
				EventLogWriter.WriteInfo("DiagnosticStats", "GetOperationState on key {0} resulted in null state", new object[] { key });
			}
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0002F960 File Offset: 0x0002DB60
		internal int AddRequestStates(string key, DiagOperationState states)
		{
			int num = 0;
			if (states != null && !states.IsRuntimeAware)
			{
				DiagOperationState diagOperationState = this.AddOrUpdateOperationState(key, states);
				if (diagOperationState != null)
				{
					num = states.Events.Count;
				}
				else
				{
					EventLogWriter.WriteInfo("DiagnosticStats", "GetOperationState on key {0} resulted in null state for Op {1}", new object[]
					{
						key,
						states.ToString()
					});
				}
			}
			return num;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0002F9BC File Offset: 0x0002DBBC
		internal bool RemoveRequestState(DiagOperationState states)
		{
			DiagOperationState diagOperationState;
			return this._pooledOperationStates.TryRemove(states.UniqueIdentifier, out diagOperationState);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0002F9DC File Offset: 0x0002DBDC
		internal IEnumerator<KeyValuePair<string, DiagOperationState>> GetStateEnumerator()
		{
			return this._pooledOperationStates.GetEnumerator();
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0002F9E9 File Offset: 0x0002DBE9
		internal void Clear()
		{
			this._pooledOperationStates.Clear();
		}

		// Token: 0x040009CF RID: 2511
		private ConcurrentDictionary<string, DiagOperationState> _pooledOperationStates;

		// Token: 0x040009D0 RID: 2512
		private int _totalPublished;
	}
}
