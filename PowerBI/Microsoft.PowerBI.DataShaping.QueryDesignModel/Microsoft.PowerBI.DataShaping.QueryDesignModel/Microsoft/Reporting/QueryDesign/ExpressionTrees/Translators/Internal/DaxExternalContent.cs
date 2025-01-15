using System;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200012A RID: 298
	internal static class DaxExternalContent
	{
		// Token: 0x06001072 RID: 4210 RVA: 0x0002CF64 File Offset: 0x0002B164
		internal static DaxExpression DaxText(string text)
		{
			return DaxExpression.Scalar(string.Concat(new string[]
			{
				"(",
				"/* USER DAX BEGIN */",
				DaxFormat.NewLine,
				text,
				DaxFormat.NewLine,
				"/* USER DAX END */",
				")"
			}));
		}

		// Token: 0x04000A85 RID: 2693
		internal const int ExternalDaxBeginWrapperLineCount = 1;

		// Token: 0x04000A86 RID: 2694
		internal const int ExternalDaxEndWrapperLineCount = 1;
	}
}
