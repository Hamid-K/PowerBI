using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001898 RID: 6296
	internal struct QueryExpressionAssembler
	{
		// Token: 0x06009FBB RID: 40891 RVA: 0x0020FFAC File Offset: 0x0020E1AC
		public static FunctionValue Assemble(Keys columns, QueryExpression expression)
		{
			if (expression.Kind == QueryExpressionKind.Invocation)
			{
				InvocationQueryExpression invocationQueryExpression = (InvocationQueryExpression)expression;
				if (invocationQueryExpression.Function.Kind == QueryExpressionKind.Constant && invocationQueryExpression.Arguments.Count == 1 && invocationQueryExpression.Arguments[0].Kind == QueryExpressionKind.ArgumentAccess)
				{
					FunctionValue asFunction = ((ConstantQueryExpression)invocationQueryExpression.Function).Value.AsFunction;
					if (!(asFunction.Expression is IIdentifierExpression))
					{
						return asFunction;
					}
				}
			}
			IFunctionExpression functionExpression = FunctionExpressionBuilder.ToExpression(columns, Identifier.Underscore, expression);
			return new Compiler(CompileOptions.None).ToFunction(functionExpression);
		}
	}
}
