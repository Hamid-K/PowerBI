using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000040 RID: 64
	[DataContract]
	public class DataSourceFunctionPublish
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001BB RID: 443 RVA: 0x000038D2 File Offset: 0x00001AD2
		// (set) Token: 0x060001BC RID: 444 RVA: 0x000038DA File Offset: 0x00001ADA
		[DataMember(Name = "Category", Order = 0)]
		public string Category { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001BD RID: 445 RVA: 0x000038E3 File Offset: 0x00001AE3
		// (set) Token: 0x060001BE RID: 446 RVA: 0x000038EB File Offset: 0x00001AEB
		[DataMember(Name = "SupportsDirectQuery", Order = 10)]
		public bool SupportsDirectQuery { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000038F4 File Offset: 0x00001AF4
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x000038FC File Offset: 0x00001AFC
		[DataMember(Name = "Beta", Order = 20)]
		public bool Beta { get; set; }
	}
}
