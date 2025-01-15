using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x02001996 RID: 6550
	public class IfTrueBranchModel : PipelineModel, ICompositePipeline
	{
		// Token: 0x1700237B RID: 9083
		// (get) Token: 0x0600D617 RID: 54807 RVA: 0x002D99C8 File Offset: 0x002D7BC8
		// (set) Token: 0x0600D618 RID: 54808 RVA: 0x002D99D0 File Offset: 0x002D7BD0
		public PipelineModel Body { get; set; }

		// Token: 0x1700237C RID: 9084
		// (get) Token: 0x0600D619 RID: 54809 RVA: 0x002D99D9 File Offset: 0x002D7BD9
		// (set) Token: 0x0600D61A RID: 54810 RVA: 0x002D99E1 File Offset: 0x002D7BE1
		public string Condition { get; set; }

		// Token: 0x0600D61B RID: 54811 RVA: 0x002D99EC File Offset: 0x002D7BEC
		public override string ToOperatorString()
		{
			return string.Concat(new string[]
			{
				"if ",
				this.Condition,
				":",
				Environment.NewLine,
				this.Body.ToString().Indent(4, false)
			});
		}
	}
}
