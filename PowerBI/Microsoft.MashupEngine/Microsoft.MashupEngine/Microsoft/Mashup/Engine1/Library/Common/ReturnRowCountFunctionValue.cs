using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200111F RID: 4383
	internal sealed class ReturnRowCountFunctionValue : NativeFunctionValue1
	{
		// Token: 0x060072A5 RID: 29349 RVA: 0x00189D26 File Offset: 0x00187F26
		private ReturnRowCountFunctionValue()
			: base("result")
		{
		}

		// Token: 0x17002018 RID: 8216
		// (get) Token: 0x060072A6 RID: 29350 RVA: 0x00189F48 File Offset: 0x00188148
		public override IExpression Expression
		{
			get
			{
				if (this.expression == null)
				{
					Identifier identifier = Identifier.New("result");
					this.expression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(ActionModule.Action.Return), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(TableModule.Table.RowCount), new InclusiveIdentifierExpressionSyntaxNode(identifier))));
				}
				return this.expression;
			}
		}

		// Token: 0x060072A7 RID: 29351 RVA: 0x00189FAC File Offset: 0x001881AC
		public override Value Invoke(Value result)
		{
			return ActionModule.Action.Return.Invoke(TableModule.Table.RowCount.Invoke(result));
		}

		// Token: 0x04003F2F RID: 16175
		public static readonly FunctionValue Instance = new ReturnRowCountFunctionValue();

		// Token: 0x04003F30 RID: 16176
		private IExpression expression;
	}
}
