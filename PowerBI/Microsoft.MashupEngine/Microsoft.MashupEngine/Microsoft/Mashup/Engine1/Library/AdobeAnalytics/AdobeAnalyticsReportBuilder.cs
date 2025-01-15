using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F7F RID: 3967
	internal class AdobeAnalyticsReportBuilder
	{
		// Token: 0x17001E2A RID: 7722
		// (get) Token: 0x06006873 RID: 26739 RVA: 0x00167574 File Offset: 0x00165774
		// (set) Token: 0x06006874 RID: 26740 RVA: 0x0016757C File Offset: 0x0016577C
		public string ReportSuiteId { get; set; }

		// Token: 0x17001E2B RID: 7723
		// (get) Token: 0x06006875 RID: 26741 RVA: 0x00167585 File Offset: 0x00165785
		// (set) Token: 0x06006876 RID: 26742 RVA: 0x0016758D File Offset: 0x0016578D
		public IList<string> Measures { get; private set; }

		// Token: 0x17001E2C RID: 7724
		// (get) Token: 0x06006877 RID: 26743 RVA: 0x00167596 File Offset: 0x00165796
		// (set) Token: 0x06006878 RID: 26744 RVA: 0x0016759E File Offset: 0x0016579E
		public IList<string> Dimensions { get; private set; }

		// Token: 0x17001E2D RID: 7725
		// (get) Token: 0x06006879 RID: 26745 RVA: 0x001675A7 File Offset: 0x001657A7
		// (set) Token: 0x0600687A RID: 26746 RVA: 0x001675AF File Offset: 0x001657AF
		public IDictionary<string, int> DimensionToTop { get; private set; }

		// Token: 0x17001E2E RID: 7726
		// (get) Token: 0x0600687B RID: 26747 RVA: 0x001675B8 File Offset: 0x001657B8
		// (set) Token: 0x0600687C RID: 26748 RVA: 0x001675C0 File Offset: 0x001657C0
		public IList<string> Segments { get; private set; }

		// Token: 0x17001E2F RID: 7727
		// (get) Token: 0x0600687D RID: 26749 RVA: 0x001675C9 File Offset: 0x001657C9
		// (set) Token: 0x0600687E RID: 26750 RVA: 0x001675D1 File Offset: 0x001657D1
		public string DateStart { get; set; }

		// Token: 0x17001E30 RID: 7728
		// (get) Token: 0x0600687F RID: 26751 RVA: 0x001675DA File Offset: 0x001657DA
		// (set) Token: 0x06006880 RID: 26752 RVA: 0x001675E2 File Offset: 0x001657E2
		public string DateEnd { get; set; }

		// Token: 0x17001E31 RID: 7729
		// (get) Token: 0x06006881 RID: 26753 RVA: 0x001675EB File Offset: 0x001657EB
		// (set) Token: 0x06006882 RID: 26754 RVA: 0x001675F3 File Offset: 0x001657F3
		public string SortBy { get; set; }

		// Token: 0x17001E32 RID: 7730
		// (get) Token: 0x06006883 RID: 26755 RVA: 0x001675FC File Offset: 0x001657FC
		// (set) Token: 0x06006884 RID: 26756 RVA: 0x00167604 File Offset: 0x00165804
		public AdobeAnalyticsFilterExpression Filter { get; set; }

		// Token: 0x17001E33 RID: 7731
		// (get) Token: 0x06006885 RID: 26757 RVA: 0x0016760D File Offset: 0x0016580D
		// (set) Token: 0x06006886 RID: 26758 RVA: 0x00167615 File Offset: 0x00165815
		public long? Skip { get; set; }

		// Token: 0x06006887 RID: 26759 RVA: 0x0016761E File Offset: 0x0016581E
		public AdobeAnalyticsReportBuilder()
		{
			this.Measures = new List<string>();
			this.Dimensions = new List<string>();
			this.DimensionToTop = new Dictionary<string, int>();
			this.Segments = new List<string>();
		}
	}
}
