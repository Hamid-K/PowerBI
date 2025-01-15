using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ServiceContract
{
	// Token: 0x02000005 RID: 5
	[DataContract]
	internal sealed class AzureValue
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002247 File Offset: 0x00000447
		// (set) Token: 0x06000015 RID: 21 RVA: 0x0000224F File Offset: 0x0000044F
		[DataMember(Name = "details", IsRequired = false, EmitDefaultValue = false)]
		internal string Details { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002258 File Offset: 0x00000458
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002260 File Offset: 0x00000460
		[DataMember(Name = "timestamp", IsRequired = false, EmitDefaultValue = false)]
		internal string TimeStamp { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002269 File Offset: 0x00000469
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002271 File Offset: 0x00000471
		[DataMember(Name = "additionalMessages", IsRequired = false, EmitDefaultValue = false)]
		internal AdditionalMessage[] AdditionalMessages { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000227A File Offset: 0x0000047A
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002282 File Offset: 0x00000482
		[DataMember(Name = "moreInformation", IsRequired = false, EmitDefaultValue = false)]
		internal OdataErrorContainer MoreInformation { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000228B File Offset: 0x0000048B
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002293 File Offset: 0x00000493
		[DataMember(Name = "powerBiErrorDetails", IsRequired = false, EmitDefaultValue = false)]
		internal string PowerBIErrorDetails { get; set; }
	}
}
