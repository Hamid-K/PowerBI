using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200111E RID: 4382
	internal sealed class ReturnResultFunctionValue : NativeFunctionValue1
	{
		// Token: 0x060072A1 RID: 29345 RVA: 0x00189D26 File Offset: 0x00187F26
		private ReturnResultFunctionValue()
			: base("result")
		{
		}

		// Token: 0x17002017 RID: 8215
		// (get) Token: 0x060072A2 RID: 29346 RVA: 0x00189ED8 File Offset: 0x001880D8
		public override IExpression Expression
		{
			get
			{
				if (this.expression == null)
				{
					Identifier identifier = Identifier.New("result");
					this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(ActionModule.Action.Return), new InclusiveIdentifierExpressionSyntaxNode(identifier)));
				}
				return this.expression;
			}
		}

		// Token: 0x060072A3 RID: 29347 RVA: 0x00189F2D File Offset: 0x0018812D
		public override Value Invoke(Value result)
		{
			return ActionModule.Action.Return.Invoke(result);
		}

		// Token: 0x04003F2D RID: 16173
		public static readonly FunctionValue Instance = new ReturnResultFunctionValue();

		// Token: 0x04003F2E RID: 16174
		private IExpression expression;
	}
}
