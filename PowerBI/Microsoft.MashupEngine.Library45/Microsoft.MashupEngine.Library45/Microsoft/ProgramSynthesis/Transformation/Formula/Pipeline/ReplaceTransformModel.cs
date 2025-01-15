using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019A6 RID: 6566
	public class ReplaceTransformModel : PipelineModel
	{
		// Token: 0x170023BD RID: 9149
		// (get) Token: 0x0600D6A6 RID: 54950 RVA: 0x002DA349 File Offset: 0x002D8549
		// (set) Token: 0x0600D6A7 RID: 54951 RVA: 0x002DA351 File Offset: 0x002D8551
		public string FindText { get; set; }

		// Token: 0x170023BE RID: 9150
		// (get) Token: 0x0600D6A8 RID: 54952 RVA: 0x002DA35A File Offset: 0x002D855A
		// (set) Token: 0x0600D6A9 RID: 54953 RVA: 0x002DA362 File Offset: 0x002D8562
		public string ReplaceText { get; set; }

		// Token: 0x0600D6AA RID: 54954 RVA: 0x002DA36C File Offset: 0x002D856C
		public override string ToOperatorString()
		{
			return string.Concat(new string[]
			{
				"Replace(",
				this.FindText.ToCSharpPseudoLiteral(),
				", ",
				this.ReplaceText.ToCSharpPseudoLiteral(),
				")"
			});
		}
	}
}
