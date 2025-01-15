using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x0200199B RID: 6555
	public class ForwardFillLinearTransform : PipelineModel
	{
		// Token: 0x17002389 RID: 9097
		// (get) Token: 0x0600D63A RID: 54842 RVA: 0x002D9C06 File Offset: 0x002D7E06
		// (set) Token: 0x0600D63B RID: 54843 RVA: 0x002D9C0E File Offset: 0x002D7E0E
		public decimal Gradient { get; set; }

		// Token: 0x1700238A RID: 9098
		// (get) Token: 0x0600D63C RID: 54844 RVA: 0x002D9C17 File Offset: 0x002D7E17
		// (set) Token: 0x0600D63D RID: 54845 RVA: 0x002D9C1F File Offset: 0x002D7E1F
		public decimal Intercept { get; set; }

		// Token: 0x0600D63E RID: 54846 RVA: 0x002D9C28 File Offset: 0x002D7E28
		public override string ToOperatorString()
		{
			return string.Format("ForwardFillLinear({0}, {1})", this.Gradient, this.Intercept);
		}
	}
}
