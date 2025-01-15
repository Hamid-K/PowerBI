using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Internal
{
	// Token: 0x020000C2 RID: 194
	public sealed class EstimatedMemoryUsageKBCategory
	{
		// Token: 0x060006AA RID: 1706 RVA: 0x00012984 File Offset: 0x00010B84
		internal EstimatedMemoryUsageKBCategory()
		{
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001298C File Offset: 0x00010B8C
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x00012994 File Offset: 0x00010B94
		public long? Pagination { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x000129A0 File Offset: 0x00010BA0
		[XmlIgnore]
		public bool PaginationSpecified
		{
			get
			{
				return this.Pagination != null;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x000129BB File Offset: 0x00010BBB
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x000129C3 File Offset: 0x00010BC3
		public long? Rendering { get; set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x000129CC File Offset: 0x00010BCC
		[XmlIgnore]
		public bool RenderingSpecified
		{
			get
			{
				return this.Rendering != null;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x000129E7 File Offset: 0x00010BE7
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x000129EF File Offset: 0x00010BEF
		public long? Processing { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x000129F8 File Offset: 0x00010BF8
		[XmlIgnore]
		public bool ProcessingSpecified
		{
			get
			{
				return this.Processing != null;
			}
		}
	}
}
