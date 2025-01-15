using System;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003BC RID: 956
	[Concept("Conjunct")]
	[DataContract]
	public sealed class Conjunct : FrontierConceptRule
	{
		// Token: 0x06001565 RID: 5477 RVA: 0x0003D517 File Offset: 0x0003B717
		private Conjunct(string dslName, Symbol head, Symbol[] args)
			: base(dslName, head, args)
		{
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Predicate1
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol Predicate2
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x0003E9A3 File Offset: 0x0003CBA3
		protected internal override Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature
		{
			get
			{
				return Record.Create<ConceptRule.TypeParam[], ConceptRule.TypeParam>(new ConceptRule.TypeParam[]
				{
					ConceptRule.TP.Primitive(typeof(bool)),
					ConceptRule.TP.Primitive(typeof(bool))
				}, ConceptRule.TP.Primitive(typeof(bool)));
			}
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0003E9E4 File Offset: 0x0003CBE4
		protected override object Evaluate(object[] args)
		{
			bool flag = (bool)args[0];
			bool flag2 = (bool)args[1];
			return flag && flag2;
		}
	}
}
