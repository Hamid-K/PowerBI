using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000093 RID: 147
	internal abstract class LimitOperator
	{
		// Token: 0x06000929 RID: 2345 RVA: 0x00026974 File Offset: 0x00024B74
		internal LimitOperator(int count)
		{
			this.m_count = count;
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00026983 File Offset: 0x00024B83
		internal virtual int Count
		{
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600092B RID: 2347
		internal abstract bool IgnoreLimitCount { get; }

		// Token: 0x0400025B RID: 603
		private readonly int m_count;
	}
}
