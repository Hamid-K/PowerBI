using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019A4 RID: 6564
	public class SliceBetweenTransformModel : PipelineModel
	{
		// Token: 0x170023B8 RID: 9144
		// (get) Token: 0x0600D698 RID: 54936 RVA: 0x002DA231 File Offset: 0x002D8431
		// (set) Token: 0x0600D699 RID: 54937 RVA: 0x002DA239 File Offset: 0x002D8439
		public string StartText { get; set; }

		// Token: 0x170023B9 RID: 9145
		// (get) Token: 0x0600D69A RID: 54938 RVA: 0x002DA242 File Offset: 0x002D8442
		// (set) Token: 0x0600D69B RID: 54939 RVA: 0x002DA24A File Offset: 0x002D844A
		public string EndText { get; set; }

		// Token: 0x0600D69C RID: 54940 RVA: 0x002DA254 File Offset: 0x002D8454
		public override string ToOperatorString()
		{
			return string.Concat(new string[]
			{
				"SliceBetween(",
				this.StartText.ToCSharpPseudoLiteral(),
				", ",
				this.EndText.ToCSharpPseudoLiteral(),
				")"
			});
		}
	}
}
