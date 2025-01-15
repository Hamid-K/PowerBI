using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.ReportingServices.RdlExpressions.SafeExpressions.Interfaces;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x02000012 RID: 18
	internal sealed class Function : IFunction
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002908 File Offset: 0x00000B08
		public Function(Type ReturnType, IArgumentValidator argumentValidator, IFunctionEvaluator evaluator)
		{
			this._returnType = ReturnType;
			this._argumentValidator = argumentValidator;
			this._evaluator = evaluator;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002925 File Offset: 0x00000B25
		public string Name
		{
			get
			{
				return this._argumentValidator.FunctionName;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002932 File Offset: 0x00000B32
		public Type ReturnType
		{
			get
			{
				return this._returnType;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000293A File Offset: 0x00000B3A
		public bool HasNoArguments
		{
			get
			{
				return this._argumentValidator is NoArgumentValidator;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000294A File Offset: 0x00000B4A
		public void ValidateArguments(List<ExpressionSyntax> argumentExpressions)
		{
			this._argumentValidator.ValidateArguments(argumentExpressions);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002958 File Offset: 0x00000B58
		public object Evaluate(List<object> arguments)
		{
			return this._evaluator.Evaluate(this._argumentValidator.FunctionName, arguments);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002971 File Offset: 0x00000B71
		public object Evaluate()
		{
			return this._evaluator.Evaluate(this._argumentValidator.FunctionName, new List<object>());
		}

		// Token: 0x0400001F RID: 31
		private readonly Type _returnType;

		// Token: 0x04000020 RID: 32
		private readonly IArgumentValidator _argumentValidator;

		// Token: 0x04000021 RID: 33
		private readonly IFunctionEvaluator _evaluator;
	}
}
