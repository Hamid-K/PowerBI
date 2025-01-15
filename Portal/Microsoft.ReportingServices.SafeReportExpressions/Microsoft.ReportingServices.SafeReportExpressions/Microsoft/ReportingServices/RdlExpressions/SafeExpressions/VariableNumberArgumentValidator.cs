using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000020 RID: 32
	internal sealed class VariableNumberArgumentValidator : ArgumentValidator
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00002FCD File Offset: 0x000011CD
		public VariableNumberArgumentValidator(string functionName, int minNumberOfArguments, int maxNumberOfArguments)
			: base(functionName)
		{
			this._minNumberOfArguments = minNumberOfArguments;
			this._maxNumberOfArguments = maxNumberOfArguments;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002FE4 File Offset: 0x000011E4
		public override void ValidateArguments(List<ExpressionSyntax> argumentExpressions)
		{
			if (argumentExpressions.Count < this._minNumberOfArguments || argumentExpressions.Count > this._maxNumberOfArguments)
			{
				base.RaiseError(string.Format("{0} does not support {1} arguments", base.FunctionName, argumentExpressions.Count));
			}
		}

		// Token: 0x04000025 RID: 37
		private readonly int _minNumberOfArguments;

		// Token: 0x04000026 RID: 38
		private readonly int _maxNumberOfArguments;
	}
}
