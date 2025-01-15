using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Internal
{
	// Token: 0x020000C6 RID: 198
	public sealed class ScaleTimeCategory
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x00012B3A File Offset: 0x00010D3A
		internal ScaleTimeCategory()
		{
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x00012B42 File Offset: 0x00010D42
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x00012B4A File Offset: 0x00010D4A
		public long? Pagination { get; set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x00012B54 File Offset: 0x00010D54
		[XmlIgnore]
		public bool PaginationSpecified
		{
			get
			{
				return this.Pagination != null;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00012B6F File Offset: 0x00010D6F
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00012B77 File Offset: 0x00010D77
		public long? Rendering { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00012B80 File Offset: 0x00010D80
		[XmlIgnore]
		public bool RenderingSpecified
		{
			get
			{
				return this.Rendering != null;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x00012B9B File Offset: 0x00010D9B
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x00012BA3 File Offset: 0x00010DA3
		public long? Processing { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00012BAC File Offset: 0x00010DAC
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
