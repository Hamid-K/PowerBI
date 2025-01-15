using System;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C8B RID: 7307
	public class FormatNumber : TransformationDescription
	{
		// Token: 0x0600F763 RID: 63331 RVA: 0x0034B998 File Offset: 0x00349B98
		internal FormatNumber(FormatNumber programNode)
			: base(programNode.Node, TransformationCategory.Format, TransformationKind.FormatNumber)
		{
			this.Format = (NumberFormat)programNode.numberFormat.Node.Invoke(null);
		}

		// Token: 0x17002941 RID: 10561
		// (get) Token: 0x0600F764 RID: 63332 RVA: 0x0034B9D8 File Offset: 0x00349BD8
		public NumberFormat Format { get; }
	}
}
