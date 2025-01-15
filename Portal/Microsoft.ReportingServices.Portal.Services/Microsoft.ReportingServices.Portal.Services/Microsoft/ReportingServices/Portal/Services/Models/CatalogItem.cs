using System;
using Microsoft.ReportingServices.Portal.Interfaces.Enums;
using Microsoft.ReportingServices.Portal.Interfaces.Models;

namespace Microsoft.ReportingServices.Portal.Services.Models
{
	// Token: 0x0200005A RID: 90
	internal abstract class CatalogItem : ICatalogItem
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x0001299A File Offset: 0x00010B9A
		protected CatalogItem(CatalogItemType type)
		{
			this.Type = type;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000129A9 File Offset: 0x00010BA9
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x000129B1 File Offset: 0x00010BB1
		public string Id { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x000129BA File Offset: 0x00010BBA
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x000129C2 File Offset: 0x00010BC2
		public string Name { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002DA RID: 730 RVA: 0x000129CB File Offset: 0x00010BCB
		// (set) Token: 0x060002DB RID: 731 RVA: 0x000129D3 File Offset: 0x00010BD3
		public string Path { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002DC RID: 732 RVA: 0x000129DC File Offset: 0x00010BDC
		// (set) Token: 0x060002DD RID: 733 RVA: 0x000129E4 File Offset: 0x00010BE4
		public CatalogItemType Type { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002DE RID: 734 RVA: 0x000129ED File Offset: 0x00010BED
		// (set) Token: 0x060002DF RID: 735 RVA: 0x000129F5 File Offset: 0x00010BF5
		public int Size { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x000129FE File Offset: 0x00010BFE
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00012A06 File Offset: 0x00010C06
		public string Description { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00012A0F File Offset: 0x00010C0F
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00012A17 File Offset: 0x00010C17
		public bool Hidden { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00012A20 File Offset: 0x00010C20
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00012A28 File Offset: 0x00010C28
		public DateTime CreatedDate { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00012A31 File Offset: 0x00010C31
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00012A39 File Offset: 0x00010C39
		public DateTime ModifiedDate { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00012A42 File Offset: 0x00010C42
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00012A4A File Offset: 0x00010C4A
		public string CreatedBy { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00012A53 File Offset: 0x00010C53
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00012A5B File Offset: 0x00010C5B
		public string ModifiedBy { get; set; }
	}
}
