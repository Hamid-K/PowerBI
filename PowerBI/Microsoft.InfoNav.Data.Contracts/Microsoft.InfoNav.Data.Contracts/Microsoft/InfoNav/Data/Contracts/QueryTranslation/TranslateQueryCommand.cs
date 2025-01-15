using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000BD RID: 189
	[DataContract(Name = "TranslateQueryCommand")]
	public sealed class TranslateQueryCommand
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x0000BC00 File Offset: 0x00009E00
		[DataMember(Name = "Version", IsRequired = true, Order = 0)]
		public int Version { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000BC09 File Offset: 0x00009E09
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0000BC11 File Offset: 0x00009E11
		[DataMember(Name = "Query", IsRequired = true, Order = 10)]
		public QueryDefinition Query { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000BC1A File Offset: 0x00009E1A
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0000BC22 File Offset: 0x00009E22
		[DataMember(Name = "Binding", IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public DataShapeBinding Binding { get; set; }
	}
}
