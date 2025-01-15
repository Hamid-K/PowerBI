using System;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C90 RID: 7312
	public class RoundDateTime : TransformationDescription
	{
		// Token: 0x0600F777 RID: 63351 RVA: 0x0034BC1C File Offset: 0x00349E1C
		internal RoundDateTime(RoundPartialDateTime programNode, string columnName)
			: base(programNode.Node, columnName, TransformationCategory.Mutation, TransformationKind.RoundDateTime)
		{
			this.RoundingSpec = programNode.dtRoundingSpec.Value;
		}

		// Token: 0x1700294F RID: 10575
		// (get) Token: 0x0600F778 RID: 63352 RVA: 0x0034BC52 File Offset: 0x00349E52
		public DateTimeRoundingSpec RoundingSpec { get; }
	}
}
