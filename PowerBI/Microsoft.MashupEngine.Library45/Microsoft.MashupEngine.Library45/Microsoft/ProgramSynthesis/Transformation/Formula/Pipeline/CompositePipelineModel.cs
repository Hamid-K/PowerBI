using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x02001991 RID: 6545
	public abstract class CompositePipelineModel : PipelineModel
	{
		// Token: 0x1700237A RID: 9082
		// (get) Token: 0x0600D60A RID: 54794 RVA: 0x002D9905 File Offset: 0x002D7B05
		// (set) Token: 0x0600D60B RID: 54795 RVA: 0x002D990D File Offset: 0x002D7B0D
		public IReadOnlyList<PipelineModel> Children { get; set; }
	}
}
