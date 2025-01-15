using System;
using System.ComponentModel;

namespace Model
{
	// Token: 0x02000066 RID: 102
	public class SystemResource
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000385A File Offset: 0x00001A5A
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00003862 File Offset: 0x00001A62
		[ReadOnly(true)]
		public Guid Id { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000386B File Offset: 0x00001A6B
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00003873 File Offset: 0x00001A73
		public Guid PackageId { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000387C File Offset: 0x00001A7C
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00003884 File Offset: 0x00001A84
		public CatalogItem PackageContent { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000388D File Offset: 0x00001A8D
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00003895 File Offset: 0x00001A95
		public SystemResourceType Type { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000389E File Offset: 0x00001A9E
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x000038A6 File Offset: 0x00001AA6
		public string TypeName { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x000038AF File Offset: 0x00001AAF
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x000038B7 File Offset: 0x00001AB7
		public string Name { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000038C0 File Offset: 0x00001AC0
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x000038C8 File Offset: 0x00001AC8
		public string Version { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x000038D1 File Offset: 0x00001AD1
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x000038D9 File Offset: 0x00001AD9
		public SystemResourceItem[] Items { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060002DA RID: 730 RVA: 0x000038E2 File Offset: 0x00001AE2
		// (set) Token: 0x060002DB RID: 731 RVA: 0x000038EA File Offset: 0x00001AEA
		public bool IsEmbedded { get; set; }
	}
}
