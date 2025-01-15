using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003C7 RID: 967
	[Concept("Kth")]
	[DataContract]
	public class Kth : ConceptRule
	{
		// Token: 0x06001599 RID: 5529 RVA: 0x0003EA31 File Offset: 0x0003CC31
		private Kth(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Sequence
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol Index
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x0003EFB4 File Offset: 0x0003D1B4
		protected internal override Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature
		{
			get
			{
				return Record.Create<ConceptRule.TypeParam[], ConceptRule.TypeParam>(new ConceptRule.TypeParam[]
				{
					ConceptRule.TP.Constructor(typeof(IEnumerable<>), new ConceptRule.TypeParam[] { ConceptRule.TP.Generic("A") }),
					ConceptRule.TP.Primitive(typeof(int))
				}, ConceptRule.TP.Generic("A"));
			}
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0003F010 File Offset: 0x0003D210
		protected override object Evaluate(object[] args)
		{
			object[] array = args[0].ToEnumerable<object>().ToArray<object>();
			int num = Convert.ToInt32(args[1], CultureInfo.InvariantCulture);
			int num2 = ((num >= 0) ? num : (array.Length + num));
			return array.ElementAtOrDefault(num2);
		}
	}
}
