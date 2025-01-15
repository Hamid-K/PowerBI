using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ServiceContract
{
	// Token: 0x02000009 RID: 9
	[DataContract]
	internal sealed class AdditionalMessage
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000022B8 File Offset: 0x000004B8
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000022C0 File Offset: 0x000004C0
		[DataMember(Name = "Code", IsRequired = true, EmitDefaultValue = false)]
		internal string Code { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000022C9 File Offset: 0x000004C9
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000022D1 File Offset: 0x000004D1
		[DataMember(Name = "Severity", IsRequired = true, EmitDefaultValue = false)]
		internal string Severity { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000022DA File Offset: 0x000004DA
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000022E2 File Offset: 0x000004E2
		[DataMember(Name = "Message", IsRequired = true, EmitDefaultValue = false)]
		internal string Message { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000022EB File Offset: 0x000004EB
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000022F3 File Offset: 0x000004F3
		[DataMember(Name = "ObjectType", IsRequired = true, EmitDefaultValue = false)]
		internal string ObjectType { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000022FC File Offset: 0x000004FC
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002304 File Offset: 0x00000504
		[DataMember(Name = "ObjectName", IsRequired = true, EmitDefaultValue = false)]
		internal string ObjectName { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000230D File Offset: 0x0000050D
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002315 File Offset: 0x00000515
		[DataMember(Name = "PropertyName", IsRequired = true, EmitDefaultValue = false)]
		internal string PropertyName { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000231E File Offset: 0x0000051E
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002326 File Offset: 0x00000526
		[DataMember(Name = "AffectedItems", IsRequired = false, EmitDefaultValue = false)]
		internal string[] AffectedItems { get; set; }
	}
}
