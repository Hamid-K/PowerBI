using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200111B RID: 4379
	internal sealed class ReturnBinaryLengthFunctionValue : NativeFunctionValue1
	{
		// Token: 0x06007296 RID: 29334 RVA: 0x00189D26 File Offset: 0x00187F26
		private ReturnBinaryLengthFunctionValue()
			: base("result")
		{
		}

		// Token: 0x17002014 RID: 8212
		// (get) Token: 0x06007297 RID: 29335 RVA: 0x00189D34 File Offset: 0x00187F34
		public override IExpression Expression
		{
			get
			{
				if (this.expression == null)
				{
					Identifier identifier = Identifier.New("result");
					this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(ActionModule.Action.Return), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(Library.Binary.Length), new RequiredFieldAccessExpressionSyntaxNode(new RequiredElementAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(identifier), new ConstantExpressionSyntaxNode(NumberValue.Zero)), Identifier.New("Value")))));
				}
				return this.expression;
			}
		}

		// Token: 0x06007298 RID: 29336 RVA: 0x00189DB6 File Offset: 0x00187FB6
		public override Value Invoke(Value result)
		{
			return ActionModule.Action.Return.Invoke(Library.Binary.Length.Invoke(result.AsTable.Item0["Value"]));
		}

		// Token: 0x04003F27 RID: 16167
		public static readonly FunctionValue Instance = new ReturnBinaryLengthFunctionValue();

		// Token: 0x04003F28 RID: 16168
		private IExpression expression;
	}
}
