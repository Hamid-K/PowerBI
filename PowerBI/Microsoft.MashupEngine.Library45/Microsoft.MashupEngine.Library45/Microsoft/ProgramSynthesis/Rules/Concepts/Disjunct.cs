using System;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003BD RID: 957
	[Concept("Disjunct")]
	[DataContract]
	internal sealed class Disjunct : FrontierConceptRule
	{
		// Token: 0x0600156A RID: 5482 RVA: 0x0003D517 File Offset: 0x0003B717
		private Disjunct(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Predicate1
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol Predicate2
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x0003E9A3 File Offset: 0x0003CBA3
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

		// Token: 0x0600156E RID: 5486 RVA: 0x0003EA0C File Offset: 0x0003CC0C
		protected override object Evaluate(object[] args)
		{
			bool flag = (bool)args[0];
			bool flag2 = (bool)args[1];
			return flag || flag2;
		}
	}
}
