using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Table;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001254 RID: 4692
	internal static class ActionHelpers
	{
		// Token: 0x170021BC RID: 8636
		// (get) Token: 0x06007BA6 RID: 31654 RVA: 0x001AA568 File Offset: 0x001A8768
		public static FunctionValue TableFromRecord
		{
			get
			{
				if (ActionHelpers.tableFromRecord == null)
				{
					Identifier identifier = Identifier.New();
					IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(TableModule.Table.FromRecords), new ListExpressionSyntaxNode(new IExpression[]
					{
						new InclusiveIdentifierExpressionSyntaxNode(identifier)
					})));
					ActionHelpers.tableFromRecord = new Compiler(CompileOptions.None).ToFunction(functionExpression);
				}
				return ActionHelpers.tableFromRecord;
			}
		}

		// Token: 0x06007BA7 RID: 31655 RVA: 0x001AA5D0 File Offset: 0x001A87D0
		public static FunctionValue ToBinding(this FunctionValue function)
		{
			Identifier identifier = Identifier.New();
			IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(ActionModule.Action.Return), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(function), new InclusiveIdentifierExpressionSyntaxNode(identifier))));
			return new Compiler(CompileOptions.None).ToFunction(functionExpression);
		}

		// Token: 0x06007BA8 RID: 31656 RVA: 0x001AA624 File Offset: 0x001A8824
		public static ActionValue NewSequenceResultsAction(params ActionValue[] actions)
		{
			List<IValueReference> list = new List<IValueReference>();
			list.Add(ActionModule.Action.Return.Invoke(ListValue.Empty));
			Identifier identifier = Identifier.New();
			Identifier identifier2 = Identifier.New();
			Compiler compiler = new Compiler(CompileOptions.None);
			for (int i = 0; i < actions.Length; i++)
			{
				IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier2 }), new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(ActionModule.Action.Bind), new ConstantExpressionSyntaxNode(actions[i]), new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(ActionModule.Action.Return), BinaryExpressionSyntaxNode.New(BinaryOperator2.Concatenate, new InclusiveIdentifierExpressionSyntaxNode(identifier2), new ListExpressionSyntaxNode(new IExpression[]
				{
					new InclusiveIdentifierExpressionSyntaxNode(identifier)
				}), TokenRange.Null)))));
				list.Add(compiler.ToFunction(functionExpression));
			}
			return ActionModule.Action.Sequence.Invoke(ListValue.New(list)).AsAction;
		}

		// Token: 0x06007BA9 RID: 31657 RVA: 0x001AA714 File Offset: 0x001A8914
		public static FunctionValue NewCombineParentChildResultsFunction(int parent, int child, string childColumn)
		{
			Identifier identifier = Identifier.New();
			IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), BinaryExpressionSyntaxNode.New(BinaryOperator2.Concatenate, new RequiredElementAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(identifier), new ConstantExpressionSyntaxNode(NumberValue.New(parent))), new RecordExpressionSyntaxNode(Identifier.New(), new VariableInitializer[]
			{
				new VariableInitializer(Identifier.New(childColumn), new RequiredElementAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(identifier), new ConstantExpressionSyntaxNode(NumberValue.New(child))))
			}, TokenRange.Null), TokenRange.Null));
			return new Compiler(CompileOptions.None).ToFunction(functionExpression);
		}

		// Token: 0x06007BAA RID: 31658 RVA: 0x001AA7A8 File Offset: 0x001A89A8
		public static FunctionValue NewCombineActionResultsFunction(ActionValue action)
		{
			Identifier identifier = Identifier.New();
			Identifier identifier2 = Identifier.New();
			IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier }), new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(ActionModule.Action.Bind), new ConstantExpressionSyntaxNode(action), new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.CreateDefaultFunctionType(new Identifier[] { identifier2 }), new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(ActionModule.Action.Return), BinaryExpressionSyntaxNode.New(BinaryOperator2.Concatenate, new InclusiveIdentifierExpressionSyntaxNode(identifier), new InclusiveIdentifierExpressionSyntaxNode(identifier2), TokenRange.Null)))));
			return new Compiler(CompileOptions.None).ToFunction(functionExpression);
		}

		// Token: 0x0400448B RID: 17547
		private static FunctionValue tableFromRecord;
	}
}
