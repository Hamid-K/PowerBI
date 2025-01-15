using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200001F RID: 31
	internal sealed class NoValidationArgumentValidator : ArgumentValidator
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00002FC2 File Offset: 0x000011C2
		public NoValidationArgumentValidator(string functionName)
			: base(functionName)
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002FCB File Offset: 0x000011CB
		public override void ValidateArguments(List<ExpressionSyntax> argumentExpressions)
		{
		}
	}
}
