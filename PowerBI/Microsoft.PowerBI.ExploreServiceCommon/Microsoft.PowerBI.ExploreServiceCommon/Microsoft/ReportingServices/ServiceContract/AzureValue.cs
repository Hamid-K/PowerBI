using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ServiceContract
{
	// Token: 0x02000008 RID: 8
	[DataContract]
	internal sealed class AzureValue
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000225B File Offset: 0x0000045B
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002263 File Offset: 0x00000463
		[DataMember(Name = "details", IsRequired = false, EmitDefaultValue = false)]
		internal string Details { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000226C File Offset: 0x0000046C
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002274 File Offset: 0x00000474
		[DataMember(Name = "timestamp", IsRequired = false, EmitDefaultValue = false)]
		internal string TimeStamp { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000227D File Offset: 0x0000047D
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002285 File Offset: 0x00000485
		[DataMember(Name = "additionalMessages", IsRequired = false, EmitDefaultValue = false)]
		internal AdditionalMessage[] AdditionalMessages { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000228E File Offset: 0x0000048E
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002296 File Offset: 0x00000496
		[DataMember(Name = "moreInformation", IsRequired = false, EmitDefaultValue = false)]
		internal OdataErrorContainer MoreInformation { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000229F File Offset: 0x0000049F
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000022A7 File Offset: 0x000004A7
		[DataMember(Name = "powerBiErrorDetails", IsRequired = false, EmitDefaultValue = false)]
		internal string PowerBIErrorDetails { get; set; }
	}
}
