using System;
using System.ComponentModel;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000011 RID: 17
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public enum EvaluationBehavior
	{
		// Token: 0x04000046 RID: 70
		Automatic = 1,
		// Token: 0x04000047 RID: 71
		Static,
		// Token: 0x04000048 RID: 72
		Dynamic
	}
}
