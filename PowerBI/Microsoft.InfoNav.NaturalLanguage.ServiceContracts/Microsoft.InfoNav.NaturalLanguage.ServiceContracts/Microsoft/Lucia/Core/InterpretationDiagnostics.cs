using System;
using System.Runtime.Serialization;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200009E RID: 158
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class InterpretationDiagnostics
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000065F6 File Offset: 0x000047F6
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x000065FE File Offset: 0x000047FE
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string Syntax { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00006607 File Offset: 0x00004807
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000660F File Offset: 0x0000480F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 20)]
		public string Semantics { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00006618 File Offset: 0x00004818
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00006620 File Offset: 0x00004820
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public string QuerySemantics { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00006629 File Offset: 0x00004829
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00006631 File Offset: 0x00004831
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public string QueryDefinition { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000663A File Offset: 0x0000483A
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00006642 File Offset: 0x00004842
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public string AnonymizedSyntax { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000664B File Offset: 0x0000484B
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00006653 File Offset: 0x00004853
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public string AnonymizedSemantics { get; set; }
	}
}
