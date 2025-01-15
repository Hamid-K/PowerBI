using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200111C RID: 4380
	internal sealed class ReturnConstantFunctionValue : NativeFunctionValue1
	{
		// Token: 0x0600729A RID: 29338 RVA: 0x00189DED File Offset: 0x00187FED
		public ReturnConstantFunctionValue(Value value)
			: base("result")
		{
			this.value = value;
		}

		// Token: 0x17002015 RID: 8213
		// (get) Token: 0x0600729B RID: 29339 RVA: 0x00189E04 File Offset: 0x00188004
		public override IExpression Expression
		{
			get
			{
				if (this.expression == null)
				{
					Identifier identifier = Identifier.New("result");
					this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(ActionModule.Action.Return), new ConstantExpressionSyntaxNode(this.value)));
				}
				return this.expression;
			}
		}

		// Token: 0x0600729C RID: 29340 RVA: 0x00189E5E File Offset: 0x0018805E
		public override Value Invoke(Value result)
		{
			return ActionModule.Action.Return.Invoke(this.value).AsAction;
		}

		// Token: 0x04003F29 RID: 16169
		private readonly Value value;

		// Token: 0x04003F2A RID: 16170
		private IExpression expression;
	}
}
