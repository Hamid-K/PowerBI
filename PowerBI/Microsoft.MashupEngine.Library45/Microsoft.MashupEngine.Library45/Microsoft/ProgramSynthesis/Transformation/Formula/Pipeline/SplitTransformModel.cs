using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019A2 RID: 6562
	public class SplitTransformModel : PipelineModel
	{
		// Token: 0x170023B1 RID: 9137
		// (get) Token: 0x0600D689 RID: 54921 RVA: 0x002DA0FD File Offset: 0x002D82FD
		// (set) Token: 0x0600D68A RID: 54922 RVA: 0x002DA105 File Offset: 0x002D8305
		public string Delimiter { get; set; }

		// Token: 0x170023B2 RID: 9138
		// (get) Token: 0x0600D68B RID: 54923 RVA: 0x002DA10E File Offset: 0x002D830E
		// (set) Token: 0x0600D68C RID: 54924 RVA: 0x002DA116 File Offset: 0x002D8316
		public int Instance { get; set; }

		// Token: 0x0600D68D RID: 54925 RVA: 0x002DA11F File Offset: 0x002D831F
		public override string ToOperatorString()
		{
			return string.Format("Split({0})[{1}]", this.Delimiter.ToCSharpPseudoLiteral(), this.Instance);
		}
	}
}
