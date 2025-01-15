using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003A5 RID: 933
	[Concept("Concat")]
	[DataContract]
	internal sealed class Concat : FrontierConceptRule
	{
		// Token: 0x06001503 RID: 5379 RVA: 0x0003D517 File Offset: 0x0003B717
		private Concat(string dslName, Symbol head, params Symbol[] body)
			: base(dslName, head, body)
		{
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0003BB86 File Offset: 0x00039D86
		public Symbol Prefix
		{
			get
			{
				return base.Body[0];
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x0003D522 File Offset: 0x0003B722
		public Symbol Suffix
		{
			get
			{
				return base.Body[1];
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0003D530 File Offset: 0x0003B730
		protected internal override Record<ConceptRule.TypeParam[], ConceptRule.TypeParam> Signature
		{
			get
			{
				return Record.Create<ConceptRule.TypeParam[], ConceptRule.TypeParam>(new ConceptRule.TypeParam[]
				{
					ConceptRule.TP.Primitive(typeof(string[])),
					ConceptRule.TP.Primitive(typeof(string[]))
				}, ConceptRule.TP.Primitive(typeof(string[])));
			}
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0003D570 File Offset: 0x0003B770
		protected override object Evaluate(object[] args)
		{
			IEnumerable<string> enumerable = args[0] as string[];
			string[] array = args[1] as string[];
			return enumerable.Concat(array).ToArray<string>();
		}
	}
}
