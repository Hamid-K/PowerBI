using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000032 RID: 50
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class GetSchemaDataSetRequest : DatabasesRequestBase
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00002754 File Offset: 0x00000954
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x0000275C File Offset: 0x0000095C
		[DataMember(Name = "schemaName", IsRequired = true, EmitDefaultValue = false)]
		public string SchemaName { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002765 File Offset: 0x00000965
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x0000276D File Offset: 0x0000096D
		[DataMember(Name = "schemaNamespace", IsRequired = false, EmitDefaultValue = false)]
		public string SchemaNamespace { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002776 File Offset: 0x00000976
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000277E File Offset: 0x0000097E
		[DataMember(Name = "throwOnInlineErrors", IsRequired = false, EmitDefaultValue = false)]
		public bool ThrowOnInlineErrors { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00002787 File Offset: 0x00000987
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x0000278F File Offset: 0x0000098F
		[DataMember(Name = "restrictions", IsRequired = false, EmitDefaultValue = false)]
		public GetSchemaDataSetRestriction[] Restrictions { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002798 File Offset: 0x00000998
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x000027A0 File Offset: 0x000009A0
		[DataMember(Name = "properties", IsRequired = false, EmitDefaultValue = false)]
		public GetSchemaDataSetRequestProperty[] RequestProperties { get; set; }
	}
}
