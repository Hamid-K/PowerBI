using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Explanations;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019E5 RID: 6629
	public interface ITranslationOptions
	{
		// Token: 0x1700242A RID: 9258
		// (get) Token: 0x0600D868 RID: 55400
		// (set) Token: 0x0600D869 RID: 55401
		IExplanationTemplateProvider ExplanationTemplateProvider { get; set; }

		// Token: 0x1700242B RID: 9259
		// (get) Token: 0x0600D86A RID: 55402
		bool SuppressConstantOutput { get; }

		// Token: 0x1700242C RID: 9260
		// (get) Token: 0x0600D86B RID: 55403
		bool SuppressInconsistentOutput { get; }

		// Token: 0x1700242D RID: 9261
		// (get) Token: 0x0600D86C RID: 55404
		SuppressionBehavior SuppressLowAcceptance { get; }

		// Token: 0x1700242E RID: 9262
		// (get) Token: 0x0600D86D RID: 55405
		SuppressionBehavior SuppressLowPrecision { get; }

		// Token: 0x1700242F RID: 9263
		// (get) Token: 0x0600D86E RID: 55406
		bool SuppressWholeColumnOutput { get; }
	}
}
