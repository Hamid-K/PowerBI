using System;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003D2 RID: 978
	[Concept("StrConstFront")]
	[DataContract]
	internal sealed class StrConstFront : FrontierConceptRule
	{
		// Token: 0x060015D1 RID: 5585 RVA: 0x0003D517 File Offset: 0x0003B717
		private StrConstFront(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol StrSymbol
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x0003FC75 File Offset: 0x0003DE75
		protected internal override Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature
		{
			get
			{
				return Record.Create<ConceptRule.TypeParam[], ConceptRule.TypeParam>(new ConceptRule.TypeParam[] { ConceptRule.TP.Primitive(typeof(string)) }, ConceptRule.TP.Primitive(typeof(string[])));
			}
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x0003FCA4 File Offset: 0x0003DEA4
		protected override object Evaluate(object[] args)
		{
			string text = args[0] as string;
			return new string[] { text };
		}
	}
}
