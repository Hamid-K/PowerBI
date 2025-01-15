using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001891 RID: 6289
	internal sealed class ConstantFoldingVisitor2 : ConstantFoldingVisitor
	{
		// Token: 0x06009F89 RID: 40841 RVA: 0x0020F1A0 File Offset: 0x0020D3A0
		public new static IExpression Fold(IFunctionExpression function, TypeValue[] parameterTypes)
		{
			return new ConstantFoldingVisitor2().VisitFunction(function, parameterTypes);
		}

		// Token: 0x06009F8A RID: 40842 RVA: 0x0020F1AE File Offset: 0x0020D3AE
		public new static IExpression Fold(IExpression node)
		{
			return new ConstantFoldingVisitor2().VisitExpression(node);
		}

		// Token: 0x06009F8B RID: 40843 RVA: 0x001893A3 File Offset: 0x001875A3
		private IExpression Constant(Value value)
		{
			return new ConstantExpressionSyntaxNode(value);
		}

		// Token: 0x06009F8C RID: 40844 RVA: 0x0020F1BC File Offset: 0x0020D3BC
		protected override IExpression VisitInvocation(IInvocationExpression invocation)
		{
			IExpression expression = base.VisitInvocation(invocation);
			invocation = expression as IInvocationExpression;
			if (invocation == null)
			{
				return expression;
			}
			Value value;
			IOpaqueFunctionValue opaqueFunctionValue;
			if (!invocation.Function.TryGetConstant(out value) || (value.IsFunction && value.AsFunction.TryGetAs<IOpaqueFunctionValue>(out opaqueFunctionValue)))
			{
				return invocation;
			}
			Value[] constants = base.GetConstants(invocation.Arguments);
			if (constants != null)
			{
				try
				{
					return this.Constant(value.AsFunction.Invoke(constants));
				}
				catch (RuntimeException)
				{
				}
			}
			return base.Reduce(invocation);
		}

		// Token: 0x06009F8D RID: 40845 RVA: 0x0020F24C File Offset: 0x0020D44C
		protected override IExpression VisitBinary(IBinaryExpression binary)
		{
			IExpression expression = base.VisitBinary(binary);
			if (expression.Kind == ExpressionKind.Binary)
			{
				return base.Reduce((IBinaryExpression)expression);
			}
			return expression;
		}
	}
}
