using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000038 RID: 56
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class GetSchemaDataSetResultColumn : OperationDataContract
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00002872 File Offset: 0x00000A72
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x0000287A File Offset: 0x00000A7A
		[DataMember(Name = "name", IsRequired = true, EmitDefaultValue = false)]
		public string Name { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00002883 File Offset: 0x00000A83
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x0000288B File Offset: 0x00000A8B
		[DataMember(Name = "ns", IsRequired = false, EmitDefaultValue = false)]
		public string Namespace { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00002894 File Offset: 0x00000A94
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000289C File Offset: 0x00000A9C
		[DataMember(Name = "type", IsRequired = true, EmitDefaultValue = false)]
		public Type ColumnType { get; set; }
	}
}
