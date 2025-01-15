using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.Diagnostics.Internal
{
	// Token: 0x020000C9 RID: 201
	internal sealed class ModelRetrieval
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x00012C56 File Offset: 0x00010E56
		internal ModelRetrieval(string caller)
			: this(caller, -1L)
		{
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00012C64 File Offset: 0x00010E64
		internal ModelRetrieval(string caller, long modelId)
		{
			this.m_caller = caller;
			this.m_modelId = modelId;
			this.m_watch = new Stopwatch();
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x00012CBF File Offset: 0x00010EBF
		internal string Caller
		{
			get
			{
				return this.m_caller;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00012CC7 File Offset: 0x00010EC7
		internal long ModelId
		{
			get
			{
				return this.m_modelId;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00012CCF File Offset: 0x00010ECF
		internal long ElapsedTimeMs
		{
			get
			{
				return this.m_watch.ElapsedMilliseconds;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00012CDC File Offset: 0x00010EDC
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x00012CE4 File Offset: 0x00010EE4
		internal int ModelByteCount
		{
			get
			{
				return this.m_modelByteCount;
			}
			set
			{
				this.m_modelByteCount = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00012CED File Offset: 0x00010EED
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x00012CF5 File Offset: 0x00010EF5
		internal NullableBool ModelCacheHit
		{
			get
			{
				return this.m_modelCacheHit;
			}
			set
			{
				this.m_modelCacheHit = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x00012CFE File Offset: 0x00010EFE
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x00012D06 File Offset: 0x00010F06
		internal NullableBool StringCacheHit
		{
			get
			{
				return this.m_stringCacheHit;
			}
			set
			{
				this.m_stringCacheHit = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00012D0F File Offset: 0x00010F0F
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x00012D17 File Offset: 0x00010F17
		internal NullableBool CloudCacheHit
		{
			get
			{
				return this.m_cloudCacheHit;
			}
			set
			{
				this.m_cloudCacheHit = value;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00012D20 File Offset: 0x00010F20
		internal bool HasError
		{
			get
			{
				return !string.IsNullOrEmpty(this.Error);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x00012D30 File Offset: 0x00010F30
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x00012D38 File Offset: 0x00010F38
		internal string Error
		{
			get
			{
				return this.m_error;
			}
			set
			{
				this.m_error = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00012D41 File Offset: 0x00010F41
		internal long TotalTime
		{
			get
			{
				return this.m_totalTimeMs;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00012D49 File Offset: 0x00010F49
		internal long ConnectionOpenTime
		{
			get
			{
				return this.m_connectionOpenTimeMs;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00012D51 File Offset: 0x00010F51
		internal long ModelRetrievalTime
		{
			get
			{
				return this.m_modelRetrievalTimeMs;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00012D59 File Offset: 0x00010F59
		internal long ModelParsingTime
		{
			get
			{
				return this.m_modelParsingTimeMs;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00012D61 File Offset: 0x00010F61
		internal long ModelResolutionTime
		{
			get
			{
				return this.m_modelResolutionTimeMs;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00012D69 File Offset: 0x00010F69
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x00012D71 File Offset: 0x00010F71
		internal NullableBool IsConnectionFromPool
		{
			get
			{
				return this.m_isConnectionFromPool;
			}
			set
			{
				this.m_isConnectionFromPool = value;
			}
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00012D7A File Offset: 0x00010F7A
		internal void SetTotalTime(long start, long end)
		{
			this.m_totalTimeMs = end - start;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00012D85 File Offset: 0x00010F85
		internal void SetConnectionOpenTime(long start, long end)
		{
			this.m_connectionOpenTimeMs = end - start;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00012D90 File Offset: 0x00010F90
		internal void SetModelRetrievalTime(long start, long end)
		{
			this.m_modelRetrievalTimeMs = end - start;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00012D9B File Offset: 0x00010F9B
		internal void SetModelParsingTime(long start, long end)
		{
			this.m_modelParsingTimeMs = end - start;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00012DA6 File Offset: 0x00010FA6
		internal void SetModelResolutionTime(long start, long end)
		{
			this.m_modelResolutionTimeMs = end - start;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00012DB1 File Offset: 0x00010FB1
		internal void StartWatch()
		{
			this.m_watch.Start();
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00012DBE File Offset: 0x00010FBE
		internal void StopWatch()
		{
			this.m_watch.Stop();
		}

		// Token: 0x04000394 RID: 916
		private readonly Stopwatch m_watch;

		// Token: 0x04000395 RID: 917
		private readonly string m_caller;

		// Token: 0x04000396 RID: 918
		private readonly long m_modelId;

		// Token: 0x04000397 RID: 919
		private long m_totalTimeMs = -1L;

		// Token: 0x04000398 RID: 920
		private long m_connectionOpenTimeMs = -1L;

		// Token: 0x04000399 RID: 921
		private long m_modelRetrievalTimeMs = -1L;

		// Token: 0x0400039A RID: 922
		private long m_modelResolutionTimeMs = -1L;

		// Token: 0x0400039B RID: 923
		private long m_modelParsingTimeMs = -1L;

		// Token: 0x0400039C RID: 924
		private int m_modelByteCount = -1;

		// Token: 0x0400039D RID: 925
		private NullableBool m_isConnectionFromPool;

		// Token: 0x0400039E RID: 926
		private NullableBool m_stringCacheHit;

		// Token: 0x0400039F RID: 927
		private NullableBool m_modelCacheHit;

		// Token: 0x040003A0 RID: 928
		private NullableBool m_cloudCacheHit;

		// Token: 0x040003A1 RID: 929
		private string m_error;
	}
}
