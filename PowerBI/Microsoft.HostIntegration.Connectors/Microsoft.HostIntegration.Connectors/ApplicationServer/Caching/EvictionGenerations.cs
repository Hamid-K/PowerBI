using System;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200031B RID: 795
	internal class EvictionGenerations
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x00057A74 File Offset: 0x00055C74
		internal int NumGenerations
		{
			get
			{
				return this._numGens;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x00057A7C File Offset: 0x00055C7C
		internal int GenLength
		{
			get
			{
				return this._genLength;
			}
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x00057A84 File Offset: 0x00055C84
		private EvictionGenerations(TimeSpan span)
		{
			this._scanCheckpoint = DateTime.UtcNow;
			EvictionGenerations.GetEvictionGenerationParams(span, out this._genLength, out this._numGens);
			this._evictableSize = new long[this._numGens];
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x00057ABA File Offset: 0x00055CBA
		internal static void GetEvictionGenerationParams(TimeSpan span, out int genLength, out int numGens)
		{
			span = EvictionGenerations.ValidateTimeSpan(span);
			genLength = (int)Math.Round(span.TotalSeconds / 32.0);
			numGens = (int)Math.Round(span.TotalSeconds / (double)genLength);
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x00057AF0 File Offset: 0x00055CF0
		internal void Init(TimeSpan span)
		{
			this._scanCheckpoint = DateTime.UtcNow;
			span = EvictionGenerations.ValidateTimeSpan(span);
			this._genLength = (int)Math.Round(span.TotalSeconds / 32.0);
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00057B24 File Offset: 0x00055D24
		internal void Clean()
		{
			this._scanCheckpoint = DateTime.MinValue;
			this._genLength = 0;
			for (int i = 0; i < this._numGens; i++)
			{
				this._evictableSize[i] = 0L;
			}
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x00057B60 File Offset: 0x00055D60
		private static TimeSpan ValidateTimeSpan(TimeSpan span)
		{
			long num = (long)ConfigManager.MIN_GENERATION_INTERVAL.TotalSeconds;
			long num2 = (long)ConfigManager.MAX_GENERATION_INTERVAL.TotalSeconds;
			int num3 = 32;
			if (span.TotalSeconds < (double)(num * (long)num3))
			{
				span = TimeSpan.FromSeconds((double)(num * (long)num3));
			}
			if (span.TotalSeconds > (double)(num2 * (long)num3))
			{
				span = TimeSpan.FromSeconds((double)(num2 * (long)num3));
			}
			return span;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00057BC4 File Offset: 0x00055DC4
		private int GetGeneration(DateTime lastAccessTime)
		{
			if (this._scanCheckpoint.CompareTo(lastAccessTime) < 0)
			{
				return 0;
			}
			long num = (long)this._scanCheckpoint.Subtract(lastAccessTime).TotalSeconds;
			long num2 = num / (long)this._genLength;
			if (num2 < 0L)
			{
				ReleaseAssert.Fail("generation became negative ScanCheckpoint {0}, lastAccessTime {1}", new object[]
				{
					this._scanCheckpoint.Ticks,
					lastAccessTime.Ticks
				});
			}
			if (num2 >= (long)this._numGens)
			{
				return this._numGens - 1;
			}
			return (int)num2;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x00057C50 File Offset: 0x00055E50
		internal bool GenerationNeedsEviction(int gen)
		{
			return this._evictableSize[gen] > 0L;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x00057C60 File Offset: 0x00055E60
		internal DateTime DistributeSizeToEvict(long sizeToEvict)
		{
			int num = -1;
			for (int i = this._numGens - 1; i >= 0; i--)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(EvictionGenerations._logSource, "Evictable Size in Gen {0} was {1}", new object[]
					{
						i,
						this._evictableSize[i]
					});
				}
				if (sizeToEvict < this._evictableSize[i])
				{
					this._evictableSize[i] = sizeToEvict;
					if (num == -1)
					{
						num = i;
					}
				}
				sizeToEvict -= this._evictableSize[i];
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(EvictionGenerations._logSource, "Size to be evicted from Gen {0} is {1}", new object[]
					{
						i,
						this._evictableSize[i]
					});
				}
			}
			return this._scanCheckpoint.Subtract(TimeSpan.FromSeconds((double)(this._genLength * (num + 1))));
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x00057D3C File Offset: 0x00055F3C
		internal static EvictionGenerations CreateTemplateForNewScan(DateTime leastAccessTime, GetEvictionGenerationDelegate callback)
		{
			DateTime utcNow = DateTime.UtcNow;
			return EvictionGenerations.GetEvictionGenerations(utcNow - leastAccessTime, callback);
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00057D5C File Offset: 0x00055F5C
		internal static EvictionGenerations GetEvictionGenerations(TimeSpan span, GetEvictionGenerationDelegate callback)
		{
			EvictionGenerations evictionGenerations = null;
			if (callback != null)
			{
				int num;
				int num2;
				EvictionGenerations.GetEvictionGenerationParams(span, out num, out num2);
				evictionGenerations = callback(num2);
			}
			if (evictionGenerations == null)
			{
				evictionGenerations = new EvictionGenerations(span);
			}
			else
			{
				evictionGenerations.Init(span);
			}
			return evictionGenerations;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00057D94 File Offset: 0x00055F94
		internal EvictionGenerations CreateTemplateForNewScan(GetEvictionGenerationDelegate callback)
		{
			DateTime dateTime = this._scanCheckpoint.Subtract(TimeSpan.FromSeconds((double)(this._genLength * this._numGens)));
			return EvictionGenerations.GetEvictionGenerations(DateTime.UtcNow.Subtract(dateTime), callback);
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x00057DD4 File Offset: 0x00055FD4
		internal void DecrEvictableSize(int gen, int candSize)
		{
			this._evictableSize[gen] -= (long)candSize;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00057DF0 File Offset: 0x00055FF0
		internal void IncrEvictableSize(int gen, int candSize)
		{
			this._evictableSize[gen] += (long)candSize;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x00057E0C File Offset: 0x0005600C
		internal int GetGeneration(AOMCacheItem cand)
		{
			DateTime dateTime = new DateTime(cand.LastAccess, DateTimeKind.Utc);
			return this.GetGeneration(dateTime);
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x00057E30 File Offset: 0x00056030
		internal void AddCandidate(AOMCacheItem cand)
		{
			DateTime dateTime = new DateTime(cand.LastAccess, DateTimeKind.Utc);
			int generation = this.GetGeneration(dateTime);
			this.IncrEvictableSize(generation, cand.Size);
		}

		// Token: 0x0400100A RID: 4106
		private DateTime _scanCheckpoint;

		// Token: 0x0400100B RID: 4107
		private int _genLength;

		// Token: 0x0400100C RID: 4108
		private readonly int _numGens;

		// Token: 0x0400100D RID: 4109
		private readonly long[] _evictableSize;

		// Token: 0x0400100E RID: 4110
		private static string _logSource = "DistributedCacheEvictionGenerations";
	}
}
