using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016DA RID: 5850
	public interface IOutputSuggestion
	{
		// Token: 0x17002135 RID: 8501
		// (get) Token: 0x0600C326 RID: 49958
		double Score { get; }

		// Token: 0x17002136 RID: 8502
		// (get) Token: 0x0600C327 RID: 49959
		string Text { get; }
	}
}
