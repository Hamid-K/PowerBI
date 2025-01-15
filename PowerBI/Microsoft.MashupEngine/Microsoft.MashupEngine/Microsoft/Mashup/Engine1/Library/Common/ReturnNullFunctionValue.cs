using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200111D RID: 4381
	internal sealed class ReturnNullFunctionValue : NativeFunctionValue1
	{
		// Token: 0x0600729D RID: 29341 RVA: 0x00189D26 File Offset: 0x00187F26
		private ReturnNullFunctionValue()
			: base("result")
		{
		}

		// Token: 0x17002016 RID: 8214
		// (get) Token: 0x0600729E RID: 29342 RVA: 0x00189E78 File Offset: 0x00188078
		public override IExpression Expression
		{
			get
			{
				if (this.expression == null)
				{
					Identifier identifier = Identifier.New("result");
					this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), new ConstantExpressionSyntaxNode(ActionModule.Action.DoNothing));
				}
				return this.expression;
			}
		}

		// Token: 0x0600729F RID: 29343 RVA: 0x00189EC2 File Offset: 0x001880C2
		public override Value Invoke(Value result)
		{
			return ActionModule.Action.DoNothing;
		}

		// Token: 0x04003F2B RID: 16171
		public static readonly FunctionValue Instance = new ReturnNullFunctionValue();

		// Token: 0x04003F2C RID: 16172
		private IExpression expression;
	}
}
