using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200001D RID: 29
	internal class FixedNumberArgumentValidator : ArgumentValidator
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00002F77 File Offset: 0x00001177
		public FixedNumberArgumentValidator(string functionName, int numberOfArguments)
			: base(functionName)
		{
			this._numberOfArguments = numberOfArguments;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002F87 File Offset: 0x00001187
		public override void ValidateArguments(List<ExpressionSyntax> argumentExpressions)
		{
			if (argumentExpressions.Count != this._numberOfArguments)
			{
				base.RaiseError(string.Format("{0} does not support {1} arguments", base.FunctionName, argumentExpressions.Count));
			}
		}

		// Token: 0x04000024 RID: 36
		private readonly int _numberOfArguments;
	}
}
