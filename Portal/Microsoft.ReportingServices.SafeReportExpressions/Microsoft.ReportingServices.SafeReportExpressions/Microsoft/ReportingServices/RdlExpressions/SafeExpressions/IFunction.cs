using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace Microsoft.ReportingServices.RdlExpressions.SafeExpressions
{
	// Token: 0x0200001B RID: 27
	internal interface IFunction
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000070 RID: 112
		string Name { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000071 RID: 113
		Type ReturnType { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000072 RID: 114
		bool HasNoArguments { get; }

		// Token: 0x06000073 RID: 115
		void ValidateArguments(List<ExpressionSyntax> argumentExpressions);

		// Token: 0x06000074 RID: 116
		object Evaluate(List<object> arguments);

		// Token: 0x06000075 RID: 117
		object Evaluate();
	}
}
