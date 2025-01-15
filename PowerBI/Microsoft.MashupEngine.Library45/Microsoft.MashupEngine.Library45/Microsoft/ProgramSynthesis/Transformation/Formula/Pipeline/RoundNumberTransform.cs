using System;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x0200199A RID: 6554
	public class RoundNumberTransform : PipelineModel
	{
		// Token: 0x17002387 RID: 9095
		// (get) Token: 0x0600D634 RID: 54836 RVA: 0x002D9BC2 File Offset: 0x002D7DC2
		// (set) Token: 0x0600D635 RID: 54837 RVA: 0x002D9BCA File Offset: 0x002D7DCA
		public double Delta { get; set; }

		// Token: 0x17002388 RID: 9096
		// (get) Token: 0x0600D636 RID: 54838 RVA: 0x002D9BD3 File Offset: 0x002D7DD3
		// (set) Token: 0x0600D637 RID: 54839 RVA: 0x002D9BDB File Offset: 0x002D7DDB
		public RoundingMode Mode { get; set; }

		// Token: 0x0600D638 RID: 54840 RVA: 0x002D9BE4 File Offset: 0x002D7DE4
		public override string ToOperatorString()
		{
			return string.Format("Round({0}, {1})", this.Mode, this.Delta);
		}
	}
}
