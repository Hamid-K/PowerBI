using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ServiceContract
{
	// Token: 0x02000006 RID: 6
	[DataContract]
	internal sealed class AdditionalMessage
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000022A4 File Offset: 0x000004A4
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000022AC File Offset: 0x000004AC
		[DataMember(Name = "Code", IsRequired = true, EmitDefaultValue = false)]
		internal string Code { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000022B5 File Offset: 0x000004B5
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000022BD File Offset: 0x000004BD
		[DataMember(Name = "Severity", IsRequired = true, EmitDefaultValue = false)]
		internal string Severity { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000022C6 File Offset: 0x000004C6
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000022CE File Offset: 0x000004CE
		[DataMember(Name = "Message", IsRequired = true, EmitDefaultValue = false)]
		internal string Message { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000022D7 File Offset: 0x000004D7
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000022DF File Offset: 0x000004DF
		[DataMember(Name = "ObjectType", IsRequired = true, EmitDefaultValue = false)]
		internal string ObjectType { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000022E8 File Offset: 0x000004E8
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000022F0 File Offset: 0x000004F0
		[DataMember(Name = "ObjectName", IsRequired = true, EmitDefaultValue = false)]
		internal string ObjectName { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000022F9 File Offset: 0x000004F9
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002301 File Offset: 0x00000501
		[DataMember(Name = "PropertyName", IsRequired = true, EmitDefaultValue = false)]
		internal string PropertyName { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000230A File Offset: 0x0000050A
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002312 File Offset: 0x00000512
		[DataMember(Name = "AffectedItems", IsRequired = false, EmitDefaultValue = false)]
		internal string[] AffectedItems { get; set; }
	}
}
