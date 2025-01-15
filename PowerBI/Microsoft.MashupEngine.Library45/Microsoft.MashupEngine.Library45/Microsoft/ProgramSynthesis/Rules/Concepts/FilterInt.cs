using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003C0 RID: 960
	[Concept("FilterInt")]
	[DataContract]
	public sealed class FilterInt : ConceptRule
	{
		// Token: 0x06001579 RID: 5497 RVA: 0x0003EA31 File Offset: 0x0003CC31
		private FilterInt(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Slice
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol List
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x0003EBB0 File Offset: 0x0003CDB0
		protected internal override Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature
		{
			get
			{
				return Record.Create<ConceptRule.TypeParam[], ConceptRule.TypeParam>(new ConceptRule.TypeParam[]
				{
					ConceptRule.TP.Constructor(typeof(Record<, >), new ConceptRule.TypeParam[]
					{
						ConceptRule.TP.Primitive(typeof(int)),
						ConceptRule.TP.Primitive(typeof(int))
					}),
					ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("A") })
				}, ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("A") }));
			}
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0003EC4C File Offset: 0x0003CE4C
		protected override object Evaluate(object[] args)
		{
			Record<int, int> record = (Record<int, int>)args[0];
			return args[1].ToEnumerable<object>().Skip(record.Item1).TakeEvery(record.Item2);
		}
	}
}
