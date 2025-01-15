using System;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003C6 RID: 966
	[Concept("If")]
	[DataContract]
	internal sealed class IfRule : ConceptRule
	{
		// Token: 0x06001593 RID: 5523 RVA: 0x0003EA31 File Offset: 0x0003CC31
		private IfRule(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Predicate
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol Then
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0003EF3D File Offset: 0x0003D13D
		public Symbol Else
		{
			get
			{
				return base.Body[2];
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x0003EF4C File Offset: 0x0003D14C
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

		// Token: 0x06001598 RID: 5528 RVA: 0x0003EF9A File Offset: 0x0003D19A
		protected override object Evaluate(object[] args)
		{
			if (args[0] == null)
			{
				return null;
			}
			if (!(bool)args[0])
			{
				return args[2];
			}
			return args[1];
		}
	}
}
