using System;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003D3 RID: 979
	[Concept("Switch", Lazy = true)]
	[DataContract]
	internal sealed class Switch : FrontierConceptRule
	{
		// Token: 0x060015D5 RID: 5589 RVA: 0x0003D517 File Offset: 0x0003B717
		private Switch(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Condition
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol TrueBranch
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0003EF3D File Offset: 0x0003D13D
		public Symbol FalseBranch
		{
			get
			{
				return base.Body[2];
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x0003FCC4 File Offset: 0x0003DEC4
		protected internal override Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature
		{
			get
			{
				return Record.Create<ConceptRule.TypeParam[], ConceptRule.TypeParam>(new ConceptRule.TypeParam[]
				{
					ConceptRule.TP.Primitive(typeof(bool)),
					ConceptRule.TP.Generic("A"),
					ConceptRule.TP.Generic("A")
				}, ConceptRule.TP.Generic("A"));
			}
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0003FD12 File Offset: 0x0003DF12
		protected override object Evaluate(object[] args)
		{
			if (!(args[0] is bool) || !(bool)args[0])
			{
				return args[2];
			}
			return args[1];
		}
	}
}
