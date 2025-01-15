using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019AB RID: 6571
	public class LiteralModel<T> : PipelineModel, IConstantValue
	{
		// Token: 0x170023C2 RID: 9154
		// (get) Token: 0x0600D6B8 RID: 54968 RVA: 0x002DA495 File Offset: 0x002D8695
		// (set) Token: 0x0600D6B9 RID: 54969 RVA: 0x002DA49D File Offset: 0x002D869D
		public T Value { get; set; }

		// Token: 0x0600D6BA RID: 54970 RVA: 0x002DA4A6 File Offset: 0x002D86A6
		public override string ToOperatorString()
		{
			return this.Value.ToCSharpPseudoLiteral();
		}
	}
}
