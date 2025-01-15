using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000036 RID: 54
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	internal sealed class GetSchemaDataSetResult : DatabaseResultBase
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000027FC File Offset: 0x000009FC
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00002804 File Offset: 0x00000A04
		[DataMember(Name = "name", IsRequired = true, EmitDefaultValue = false)]
		public string Name { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000280D File Offset: 0x00000A0D
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00002815 File Offset: 0x00000A15
		[DataMember(Name = "ns", IsRequired = false, EmitDefaultValue = false)]
		public string Namespace { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000281E File Offset: 0x00000A1E
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00002826 File Offset: 0x00000A26
		[DataMember(Name = "tables", IsRequired = false, EmitDefaultValue = false)]
		public GetSchemaDataSetResultTable[] Tables { get; set; }
	}
}
