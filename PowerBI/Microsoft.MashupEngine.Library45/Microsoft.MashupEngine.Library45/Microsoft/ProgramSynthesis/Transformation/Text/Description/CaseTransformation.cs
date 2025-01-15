using System;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Description
{
	// Token: 0x02001C7E RID: 7294
	public class CaseTransformation : TransformationDescription
	{
		// Token: 0x0600F724 RID: 63268 RVA: 0x0034A454 File Offset: 0x00348654
		internal CaseTransformation(GrammarBuilders build, conv conv, string columnName)
			: base(conv.Node, columnName, TransformationCategory.Mutation, TransformationKind.CaseTransformation)
		{
			if (conv.Is_ToLowercase(build))
			{
				this.CaseTransformationKind = CaseTransformationKind.Lower;
				return;
			}
			if (conv.Is_ToUppercase(build))
			{
				this.CaseTransformationKind = CaseTransformationKind.Upper;
				return;
			}
			if (conv.Is_ToSimpleTitleCase(build))
			{
				this.CaseTransformationKind = CaseTransformationKind.SimpleTitleCase;
				return;
			}
			throw new NotImplementedException("Unknown case transformation rule: " + conv.Node.GrammarRule.Id);
		}

		// Token: 0x17002934 RID: 10548
		// (get) Token: 0x0600F725 RID: 63269 RVA: 0x0034A4CB File Offset: 0x003486CB
		public CaseTransformationKind CaseTransformationKind { get; }
	}
}
