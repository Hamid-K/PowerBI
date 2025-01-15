using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200133E RID: 4926
	internal interface IInvocationRewriter
	{
		// Token: 0x060081E2 RID: 33250
		bool TryRewriteInvocation(IInvocationExpression invocation, IExpressionEnvironment environment, out IExpression expression);
	}
}
