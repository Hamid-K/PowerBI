using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Throttling
{
	// Token: 0x02000106 RID: 262
	public sealed class ThrottlerResult
	{
		// Token: 0x06000742 RID: 1858 RVA: 0x00019D3D File Offset: 0x00017F3D
		public ThrottlerResult(bool isThrottled, TimeSpan retryAfterTimeSpan)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(retryAfterTimeSpan, "retryAfterTimeSpan");
			this.IsThrottled = isThrottled;
			this.RetryAfterTimeSpan = retryAfterTimeSpan;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00019D5E File Offset: 0x00017F5E
		public static ThrottlerResult NotThrottled
		{
			get
			{
				return ThrottlerResult.m_notThrottledResult;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00019D65 File Offset: 0x00017F65
		public static ThrottlerResult TimedOut
		{
			get
			{
				return ThrottlerResult.m_timedOutResult;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x00019D6C File Offset: 0x00017F6C
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x00019D74 File Offset: 0x00017F74
		public bool IsThrottled { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00019D7D File Offset: 0x00017F7D
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x00019D85 File Offset: 0x00017F85
		public TimeSpan RetryAfterTimeSpan { get; set; }

		// Token: 0x04000295 RID: 661
		private static readonly ThrottlerResult m_notThrottledResult = new ThrottlerResult(false, TimeSpan.Zero);

		// Token: 0x04000296 RID: 662
		private static readonly ThrottlerResult m_timedOutResult = new ThrottlerResult(false, TimeSpan.Zero);
	}
}
