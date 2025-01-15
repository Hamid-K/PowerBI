using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000095 RID: 149
	internal sealed class CacheKeyEnumerator : CacheEnumeratorBase, IEnumerator<string>, IDisposable, IEnumerator
	{
		// Token: 0x06000351 RID: 849 RVA: 0x000110D8 File Offset: 0x0000F2D8
		public CacheKeyEnumerator(DataCache cache, string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener)
			: base(cache, region, tags, op, listener)
		{
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00011353 File Offset: 0x0000F553
		public string Current
		{
			get
			{
				if (this._myEnumerator == null)
				{
					return CacheKeyEnumerator._myNull;
				}
				return this._myEnumerator.Current;
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001136E File Offset: 0x0000F56E
		public void Dispose()
		{
			this._isValid = false;
			this._myEnumerator = null;
			this._myState = null;
			this._listener = null;
			this._more = true;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00011393 File Offset: 0x0000F593
		object IEnumerator.Current
		{
			get
			{
				if (this._myEnumerator == null)
				{
					return null;
				}
				return this._myEnumerator.Current;
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000113AC File Offset: 0x0000F5AC
		public bool MoveNext()
		{
			if (!this._isValid)
			{
				return false;
			}
			if (this._myEnumerator != null && this._myEnumerator.MoveNext())
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose("DistributedCache.CacheAPI.CacheKeyEnumerator", "Reading Data from prefetched batch");
				}
				return true;
			}
			if (!this._more)
			{
				return false;
			}
			IList<string> nextBatchOfKeys = this._myCache.GetNextBatchOfKeys(this._myRegion, this._myTags, this._myOp, this._listener, ref this._myState, out this._more);
			if (nextBatchOfKeys == null)
			{
				this._isValid = false;
				return false;
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in nextBatchOfKeys)
				{
					stringBuilder.Append(text);
					stringBuilder.Append(":");
				}
				EventLogWriter.WriteVerbose<int, int, string>("DistributedCache.CacheAPI.CacheKeyEnumerator", "Enumerator HashCode:{0} New Batch Size : {1}    New batch content : {2}", this.GetHashCode(), nextBatchOfKeys.Count, stringBuilder.ToString());
			}
			this._myEnumerator = nextBatchOfKeys.GetEnumerator();
			return this._myEnumerator.MoveNext();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00003D71 File Offset: 0x00001F71
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x040002AA RID: 682
		internal const string ComponentName = "DistributedCache.CacheAPI.CacheKeyEnumerator";

		// Token: 0x040002AB RID: 683
		private IEnumerator<string> _myEnumerator;

		// Token: 0x040002AC RID: 684
		private static string _myNull;
	}
}
