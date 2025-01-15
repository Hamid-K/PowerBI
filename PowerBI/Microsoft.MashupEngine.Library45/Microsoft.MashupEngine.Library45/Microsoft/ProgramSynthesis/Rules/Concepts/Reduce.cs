using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003CC RID: 972
	[Concept("Reduce")]
	[DataContract]
	internal sealed class Reduce : ConceptRule
	{
		// Token: 0x060015B6 RID: 5558 RVA: 0x0003EA31 File Offset: 0x0003CC31
		private Reduce(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Lists
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x0003F7E8 File Offset: 0x0003D9E8
		protected internal override Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature
		{
			get
			{
				return Record.Create<ConceptRule.TypeParam[], ConceptRule.TypeParam>(new ConceptRule.TypeParam[] { ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("A") }) }) }, ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("A") }));
			}
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0003F85F File Offset: 0x0003DA5F
		protected override object Evaluate(object[] args)
		{
			return (args[0] as IEnumerable<object>).Cast<IEnumerable<object>>().SelectMany((IEnumerable<object> list) => list).ToArray<object>();
		}
	}
}
