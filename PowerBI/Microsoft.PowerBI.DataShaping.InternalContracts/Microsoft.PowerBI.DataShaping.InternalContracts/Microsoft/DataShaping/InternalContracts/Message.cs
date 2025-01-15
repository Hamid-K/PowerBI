using System;
using System.Runtime.Serialization;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x0200001E RID: 30
	[DataContract]
	internal sealed class Message
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000032C1 File Offset: 0x000014C1
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000032C9 File Offset: 0x000014C9
		[DataMember(EmitDefaultValue = false, Order = 1)]
		internal string Code { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000032D2 File Offset: 0x000014D2
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000032DA File Offset: 0x000014DA
		[DataMember(EmitDefaultValue = false, Order = 2)]
		internal string Severity { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000032E3 File Offset: 0x000014E3
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000032EB File Offset: 0x000014EB
		[DataMember(EmitDefaultValue = false, Order = 3)]
		internal string Text { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000032F4 File Offset: 0x000014F4
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000032FC File Offset: 0x000014FC
		[DataMember(EmitDefaultValue = false, Order = 4)]
		internal string ObjectType { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003305 File Offset: 0x00001505
		// (set) Token: 0x0600008E RID: 142 RVA: 0x0000330D File Offset: 0x0000150D
		[DataMember(EmitDefaultValue = false, Order = 5)]
		internal string ObjectName { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003316 File Offset: 0x00001516
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000331E File Offset: 0x0000151E
		[DataMember(EmitDefaultValue = false, Order = 6)]
		internal string PropertyName { get; set; }
	}
}
