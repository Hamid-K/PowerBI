using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000085 RID: 133
	[DataContract]
	public class DmtsDataSourceConnectionDetails
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00004ED6 File Offset: 0x000030D6
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00004EDE File Offset: 0x000030DE
		[DataMember(Name = "provider", Order = 0)]
		public string Provider { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00004EE7 File Offset: 0x000030E7
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00004EEF File Offset: 0x000030EF
		[DataMember(Name = "dataSource", Order = 10)]
		public string Datasource { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00004EF8 File Offset: 0x000030F8
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x00004F00 File Offset: 0x00003100
		[DataMember(Name = "location", Order = 20)]
		public string Location { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00004F09 File Offset: 0x00003109
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x00004F11 File Offset: 0x00003111
		[DataMember(Name = "initialCatalog", Order = 30)]
		public string InitialCatalog { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00004F1A File Offset: 0x0000311A
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00004F22 File Offset: 0x00003122
		[DataMember(Name = "effectiveUserName", Order = 40)]
		public string EffectiveUserName { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00004F2B File Offset: 0x0000312B
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00004F33 File Offset: 0x00003133
		[DataMember(Name = "validFrom", Order = 50)]
		public DateTime ValidFrom { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00004F3C File Offset: 0x0000313C
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00004F44 File Offset: 0x00003144
		[DataMember(Name = "validTo", Order = 60)]
		public DateTime ValidTo { get; set; }
	}
}
