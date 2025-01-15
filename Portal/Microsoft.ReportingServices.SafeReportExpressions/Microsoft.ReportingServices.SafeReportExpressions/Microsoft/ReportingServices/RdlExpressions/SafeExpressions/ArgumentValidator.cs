using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200001C RID: 28
	internal abstract class ArgumentValidator : IArgumentValidator
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00002F58 File Offset: 0x00001158
		protected ArgumentValidator(string functionName)
		{
			this._functionName = functionName;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002F67 File Offset: 0x00001167
		public string FunctionName
		{
			get
			{
				return this._functionName;
			}
		}

		// Token: 0x06000078 RID: 120
		public abstract void ValidateArguments(List<ExpressionSyntax> argumentExpressions);

		// Token: 0x06000079 RID: 121 RVA: 0x00002F6F File Offset: 0x0000116F
		protected void RaiseError(string message)
		{
			throw new SyntaxErrorException(message);
		}

		// Token: 0x04000023 RID: 35
		private readonly string _functionName;
	}
}
