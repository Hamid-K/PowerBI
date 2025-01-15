using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019A1 RID: 6561
	public class CaseTransformModel : PipelineModel
	{
		// Token: 0x170023AE RID: 9134
		// (get) Token: 0x0600D681 RID: 54913 RVA: 0x002DA099 File Offset: 0x002D8299
		// (set) Token: 0x0600D682 RID: 54914 RVA: 0x002DA0A1 File Offset: 0x002D82A1
		public bool LowerCase { get; set; }

		// Token: 0x170023AF RID: 9135
		// (get) Token: 0x0600D683 RID: 54915 RVA: 0x002DA0AA File Offset: 0x002D82AA
		// (set) Token: 0x0600D684 RID: 54916 RVA: 0x002DA0B2 File Offset: 0x002D82B2
		public bool ProperCase { get; set; }

		// Token: 0x170023B0 RID: 9136
		// (get) Token: 0x0600D685 RID: 54917 RVA: 0x002DA0BB File Offset: 0x002D82BB
		// (set) Token: 0x0600D686 RID: 54918 RVA: 0x002DA0C3 File Offset: 0x002D82C3
		public bool UpperCase { get; set; }

		// Token: 0x0600D687 RID: 54919 RVA: 0x002DA0CC File Offset: 0x002D82CC
		public override string ToOperatorString()
		{
			if (this.UpperCase)
			{
				return "Upper";
			}
			if (this.LowerCase)
			{
				return "Lower";
			}
			if (!this.ProperCase)
			{
				return string.Empty;
			}
			return "Proper";
		}
	}
}
