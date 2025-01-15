using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019B0 RID: 6576
	public class NullModel : PipelineModel, IConstantValue
	{
		// Token: 0x0600D6C3 RID: 54979 RVA: 0x002BF808 File Offset: 0x002BDA08
		public override string ToOperatorString()
		{
			return "null";
		}
	}
}
