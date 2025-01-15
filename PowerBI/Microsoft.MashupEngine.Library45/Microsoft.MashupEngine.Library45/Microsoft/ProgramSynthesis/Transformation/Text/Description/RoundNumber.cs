using System;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C91 RID: 7313
	public class RoundNumber : TransformationDescription
	{
		// Token: 0x0600F779 RID: 63353 RVA: 0x0034BC5C File Offset: 0x00349E5C
		internal RoundNumber(RoundNumber programNode, string columnName)
			: base(programNode.Node, columnName, TransformationCategory.Mutation, TransformationKind.RoundNumber)
		{
			this.RoundingSpec = programNode.roundingSpec.Value;
		}

		// Token: 0x17002950 RID: 10576
		// (get) Token: 0x0600F77A RID: 63354 RVA: 0x0034BC92 File Offset: 0x00349E92
		public RoundingSpec RoundingSpec { get; }
	}
}
