using System;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C8F RID: 7311
	public class ParseNumber : TransformationDescription
	{
		// Token: 0x0600F775 RID: 63349 RVA: 0x0034BBE0 File Offset: 0x00349DE0
		internal ParseNumber(ParseNumber programNode, string columnName)
			: base(programNode.Node, columnName, TransformationCategory.Parse, TransformationKind.ParseNumber)
		{
			this.FormatDetails = programNode.numberFormatDetails.Value;
		}

		// Token: 0x1700294E RID: 10574
		// (get) Token: 0x0600F776 RID: 63350 RVA: 0x0034BC13 File Offset: 0x00349E13
		public NumberFormatDetails FormatDetails { get; }
	}
}
