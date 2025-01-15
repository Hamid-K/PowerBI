using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000037 RID: 55
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class GetSchemaDataSetResultTable
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002837 File Offset: 0x00000A37
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000283F File Offset: 0x00000A3F
		[DataMember(Name = "name", IsRequired = false, EmitDefaultValue = false)]
		public string Name { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00002848 File Offset: 0x00000A48
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00002850 File Offset: 0x00000A50
		[DataMember(Name = "cols", IsRequired = false, EmitDefaultValue = false)]
		public GetSchemaDataSetResultColumn[] Columns { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00002859 File Offset: 0x00000A59
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00002861 File Offset: 0x00000A61
		[DataMember(Name = "rows", IsRequired = false, EmitDefaultValue = false)]
		public GetSchemaDataSetResultRow[] Rows { get; set; }
	}
}
