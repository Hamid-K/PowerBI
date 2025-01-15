using System;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DC5 RID: 7621
	public sealed class CacheStats
	{
		// Token: 0x0600BCDC RID: 48348 RVA: 0x002652D0 File Offset: 0x002634D0
		public CacheStats(string identity, IEvaluationConstants evaluationConstants, int traceFrequency, int resetFrequency)
		{
			if (resetFrequency % traceFrequency != 0)
			{
				throw new ArgumentOutOfRangeException("resetFrequency");
			}
			this.identity = identity;
			this.evaluationConstants = evaluationConstants;
			this.traceFrequency = traceFrequency;
			this.resetFrequency = resetFrequency;
			LruCache<string, CacheStats.Entry> lruCache = CacheStats.caches;
			lock (lruCache)
			{
				if (!CacheStats.caches.TryGetValue(this.identity, out this.entry))
				{
					this.entry = new CacheStats.Entry();
					CacheStats.caches.Add(this.identity, this.entry);
				}
			}
		}

		// Token: 0x0600BCDD RID: 48349 RVA: 0x00265378 File Offset: 0x00263578
		public void Access(bool hit)
		{
			object obj = this.entry.syncRoot;
			int num2;
			int num3;
			lock (obj)
			{
				int num;
				if (hit)
				{
					CacheStats.Entry entry = this.entry;
					num = entry.hits + 1;
					entry.hits = num;
					num2 = num;
				}
				else
				{
					num2 = this.entry.hits;
				}
				CacheStats.Entry entry2 = this.entry;
				num = entry2.requests + 1;
				entry2.requests = num;
				num3 = num;
			}
			if (num3 % this.traceFrequency == 0)
			{
				this.TraceHitRate(num2, num3);
				if (num3 % this.resetFrequency == 0)
				{
					obj = this.entry.syncRoot;
					lock (obj)
					{
						this.entry.hits -= num2;
						this.entry.requests -= num3;
					}
				}
			}
		}

		// Token: 0x0600BCDE RID: 48350 RVA: 0x0026546C File Offset: 0x0026366C
		public void Size(CacheSize cacheSize)
		{
			bool flag = false;
			object syncRoot = this.entry.syncRoot;
			int hits;
			int requests;
			lock (syncRoot)
			{
				hits = this.entry.hits;
				requests = this.entry.requests;
				CacheSize lastCacheSize = this.entry.lastCacheSize;
				double num = (double)Math.Abs(cacheSize.EntryCount - lastCacheSize.EntryCount);
				long num2 = Math.Abs(cacheSize.TotalSize - lastCacheSize.TotalSize);
				if (num > (double)lastCacheSize.EntryCount * 0.05 || (double)num2 > (double)lastCacheSize.TotalSize * 0.05)
				{
					this.entry.lastCacheSize = cacheSize;
					flag = true;
				}
			}
			if (flag)
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreatePerformanceTrace(this.identity + "/CacheStats/Size", this.evaluationConstants, TraceEventType.Information, null))
				{
					hostTrace.Add("entryCount", cacheSize.EntryCount, false);
					hostTrace.Add("totalSize", cacheSize.TotalSize, false);
				}
				this.TraceHitRate(hits, requests);
			}
		}

		// Token: 0x0600BCDF RID: 48351 RVA: 0x002655B0 File Offset: 0x002637B0
		private void TraceHitRate(int hits, int requests)
		{
			using (IHostTrace hostTrace = EvaluatorTracing.CreatePerformanceTrace(this.identity + "/CacheStats/Access", this.evaluationConstants, TraceEventType.Information, null))
			{
				hostTrace.Add("hits", hits, false);
				hostTrace.Add("requests", requests, false);
				hostTrace.Add("hitrate", (requests > 0) ? ((double)hits / (double)requests) : 0.0, false);
			}
		}

		// Token: 0x04006057 RID: 24663
		private const double cacheReportingThreshold = 0.05;

		// Token: 0x04006058 RID: 24664
		private static readonly LruCache<string, CacheStats.Entry> caches = new LruCache<string, CacheStats.Entry>(64, null);

		// Token: 0x04006059 RID: 24665
		private readonly string identity;

		// Token: 0x0400605A RID: 24666
		private readonly IEvaluationConstants evaluationConstants;

		// Token: 0x0400605B RID: 24667
		private readonly int traceFrequency;

		// Token: 0x0400605C RID: 24668
		private readonly int resetFrequency;

		// Token: 0x0400605D RID: 24669
		private readonly CacheStats.Entry entry;

		// Token: 0x02001DC6 RID: 7622
		private sealed class Entry
		{
			// Token: 0x0400605E RID: 24670
			public readonly object syncRoot = new object();

			// Token: 0x0400605F RID: 24671
			public int requests;

			// Token: 0x04006060 RID: 24672
			public int hits;

			// Token: 0x04006061 RID: 24673
			public CacheSize lastCacheSize;
		}
	}
}
