using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x02001997 RID: 6551
	public class IfFalseBranchModel : PipelineModel, ICompositePipeline
	{
		// Token: 0x1700237D RID: 9085
		// (get) Token: 0x0600D61D RID: 54813 RVA: 0x002D9A3A File Offset: 0x002D7C3A
		// (set) Token: 0x0600D61E RID: 54814 RVA: 0x002D9A42 File Offset: 0x002D7C42
		public PipelineModel Body { get; set; }

		// Token: 0x0600D61F RID: 54815 RVA: 0x002D9A4B File Offset: 0x002D7C4B
		public override string ToOperatorString()
		{
			return "else:" + Environment.NewLine + this.Body.ToString().Indent(4, false);
		}
	}
}
