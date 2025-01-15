using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000092 RID: 146
	internal sealed class CacheEnumerator : CacheEnumeratorBase, IEnumerator<KeyValuePair<string, object>>, IDisposable, IEnumerator
	{
		// Token: 0x06000346 RID: 838 RVA: 0x000110D8 File Offset: 0x0000F2D8
		public CacheEnumerator(DataCache cache, string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener)
			: base(cache, region, tags, op, listener)
		{
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000347 RID: 839 RVA: 0x000110E7 File Offset: 0x0000F2E7
		public KeyValuePair<string, object> Current
		{
			get
			{
				if (this._myEnumerator == null)
				{
					return CacheEnumerator._myNull;
				}
				return this._myEnumerator.Current;
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00011102 File Offset: 0x0000F302
		public void Dispose()
		{
			this._isValid = false;
			this._myEnumerator = null;
			this._myState = null;
			this._listener = null;
			this._more = true;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00011127 File Offset: 0x0000F327
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

		// Token: 0x0600034A RID: 842 RVA: 0x00011144 File Offset: 0x0000F344
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
					EventLogWriter.WriteVerbose("DistributedCache.CacheAPI.CacheEnumerator", "Reading Data from prefetched batch");
				}
				return true;
			}
			if (!this._more)
			{
				return false;
			}
			IList<KeyValuePair<string, object>> nextBatch = this._myCache.GetNextBatch(this._myRegion, this._myTags, this._myOp, this._listener, ref this._myState, out this._more);
			if (nextBatch == null)
			{
				this._isValid = false;
				return false;
			}
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			foreach (KeyValuePair<string, object> keyValuePair in nextBatch)
			{
				list.Add(new KeyValuePair<string, object>(keyValuePair.Key, this._myCache.cacheObjectSerializationProvider.DeserializeUserObject((byte[][])keyValuePair.Value, ValueFlagsVersion.EitherType)));
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, object> keyValuePair2 in nextBatch)
				{
					stringBuilder.Append(keyValuePair2.Key);
					stringBuilder.Append(":");
				}
				EventLogWriter.WriteVerbose<int, int, string>("DistributedCache.CacheAPI.CacheEnumerator", "Enumerator HashCode:{0} New Batch Size : {1}    New batch content : {2}", this.GetHashCode(), nextBatch.Count, stringBuilder.ToString());
			}
			this._myEnumerator = list.GetEnumerator();
			return this._myEnumerator.MoveNext();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00003D71 File Offset: 0x00001F71
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x040002A2 RID: 674
		internal const string ComponentName = "DistributedCache.CacheAPI.CacheEnumerator";

		// Token: 0x040002A3 RID: 675
		private IEnumerator<KeyValuePair<string, object>> _myEnumerator;

		// Token: 0x040002A4 RID: 676
		private static KeyValuePair<string, object> _myNull = new KeyValuePair<string, object>(null, null);
	}
}
