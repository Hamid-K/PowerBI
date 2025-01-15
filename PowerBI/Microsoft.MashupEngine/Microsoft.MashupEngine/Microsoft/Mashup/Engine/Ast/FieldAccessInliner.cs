using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B72 RID: 7026
	public class FieldAccessInliner
	{
		// Token: 0x0600B039 RID: 45113 RVA: 0x00241EFE File Offset: 0x002400FE
		public static IExpression Inline(IEngine engine, IExpression expression)
		{
			return new FieldAccessInliner.Visitor(engine).Visit(expression).Expression;
		}

		// Token: 0x02001B73 RID: 7027
		private class Visitor : LogicalAstVisitor2<FieldAccessInliner.Node>
		{
			// Token: 0x0600B03B RID: 45115 RVA: 0x00241F11 File Offset: 0x00240111
			public Visitor(IEngine engine)
			{
				this.engine = engine;
			}

			// Token: 0x0600B03C RID: 45116 RVA: 0x00241F20 File Offset: 0x00240120
			public FieldAccessInliner.Node Visit(IExpression expression)
			{
				this.VisitExpression(expression);
				return this.result;
			}

			// Token: 0x0600B03D RID: 45117 RVA: 0x00241F30 File Offset: 0x00240130
			protected override IExpression VisitExpression(IExpression expression)
			{
				this.result = null;
				expression = base.VisitExpression(expression);
				if (this.result == null)
				{
					this.result = new FieldAccessInliner.ExpressionNode(() => expression);
				}
				return expression;
			}

			// Token: 0x0600B03E RID: 45118 RVA: 0x00241F88 File Offset: 0x00240188
			protected override IExpression VisitBinary(IBinaryExpression binary)
			{
				FieldAccessInliner.Node leftResult = this.GetResult(binary.Left);
				FieldAccessInliner.Node rightResult = this.GetResult(binary.Right);
				FieldAccessInliner.Node node = new FieldAccessInliner.ExpressionNode(() => this.CreateBinary(binary, leftResult.Expression, rightResult.Expression));
				return this.Result(binary, node);
			}

			// Token: 0x0600B03F RID: 45119 RVA: 0x00241FF8 File Offset: 0x002401F8
			protected override IExpression VisitElementAccess(IElementAccessExpression elementAccess)
			{
				FieldAccessInliner.Node collectionResult = this.GetResult(elementAccess.Collection);
				FieldAccessInliner.Node keyResult = this.GetResult(elementAccess.Key);
				FieldAccessInliner.Node node = new FieldAccessInliner.ExpressionNode(() => this.CreateElementAccess(elementAccess, collectionResult.Expression, keyResult.Expression));
				return this.Result(elementAccess, node);
			}

			// Token: 0x0600B040 RID: 45120 RVA: 0x00242068 File Offset: 0x00240268
			protected override IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
			{
				FieldAccessInliner.Node expressionResult = this.GetResult(fieldAccess.Expression);
				FieldAccessInliner.Node node;
				if (expressionResult.TryGetValue(fieldAccess.MemberName.Name, fieldAccess.IsOptional, out node))
				{
					return this.Result(fieldAccess, node ?? new FieldAccessInliner.NullNode(this.engine.ConstantExpression(this.engine.Null)));
				}
				return this.Result(fieldAccess, new FieldAccessInliner.ExpressionNode(() => this.CreateFieldAccess(fieldAccess, expressionResult.Expression)));
			}

			// Token: 0x0600B041 RID: 45121 RVA: 0x00242114 File Offset: 0x00240314
			protected override IExpression VisitFunction(IFunctionExpression function)
			{
				FieldAccessInliner.Node[] array = new FieldAccessInliner.Node[function.FunctionType.Parameters.Count];
				base.EnterScope(function, array);
				FieldAccessInliner.Node expressionResult = this.GetResult(function.Expression);
				base.ExitScope(function);
				FieldAccessInliner.Node node = new FieldAccessInliner.ExpressionNode(() => this.CreateFunction(function, function.FunctionType, expressionResult.Expression));
				return this.Result(function, node);
			}

			// Token: 0x0600B042 RID: 45122 RVA: 0x002421A0 File Offset: 0x002403A0
			protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
			{
				FieldAccessInliner.Node node;
				if (!base.Environment.TryGetValue(identifier.Name, identifier.IsInclusive, out node) && node != null)
				{
					node = new FieldAccessInliner.ExpressionNode(() => identifier);
				}
				return this.Result(identifier, node);
			}

			// Token: 0x0600B043 RID: 45123 RVA: 0x00242204 File Offset: 0x00240404
			protected override IExpression VisitIf(IIfExpression @if)
			{
				FieldAccessInliner.Node conditionResult = this.GetResult(@if.Condition);
				FieldAccessInliner.Node trueResult = this.GetResult(@if.TrueCase);
				FieldAccessInliner.Node falseResult = this.GetResult(@if.FalseCase);
				FieldAccessInliner.Node node = new FieldAccessInliner.ExpressionNode(() => this.CreateIf(@if, conditionResult.Expression, trueResult.Expression, falseResult.Expression));
				return this.Result(@if, node);
			}

			// Token: 0x0600B044 RID: 45124 RVA: 0x001AD352 File Offset: 0x001AB552
			protected override IExpression VisitImplicitIdentifier(IImplicitIdentifierExpression implicitIdentifier)
			{
				return this.VisitIdentifier(implicitIdentifier);
			}

			// Token: 0x0600B045 RID: 45125 RVA: 0x0024228C File Offset: 0x0024048C
			protected override IExpression VisitInvocation(IInvocationExpression invocation)
			{
				FieldAccessInliner.Node functionResult = this.GetResult(invocation.Function);
				FieldAccessInliner.Node[] argumentResults = new FieldAccessInliner.Node[invocation.Arguments.Count];
				for (int i = 0; i < argumentResults.Length; i++)
				{
					argumentResults[i] = this.GetResult(invocation.Arguments[i]);
				}
				FieldAccessInliner.Node node = new FieldAccessInliner.ExpressionNode(delegate
				{
					IExpression[] array = new IExpression[argumentResults.Length];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = argumentResults[j].Expression;
					}
					return this.CreateInvocation(invocation, functionResult.Expression, array);
				});
				return this.Result(invocation, node);
			}

			// Token: 0x0600B046 RID: 45126 RVA: 0x00242330 File Offset: 0x00240530
			protected override IExpression VisitLet(ILetExpression let)
			{
				IIdentifierExpression identifierExpression = let.Expression as IIdentifierExpression;
				IList<VariableInitializer> list;
				Identifier identifier;
				if (identifierExpression != null && FieldAccessInliner.Visitor.IsLetIdentifier(let.Variables, identifierExpression.Name))
				{
					list = let.Variables;
					identifier = identifierExpression.Name;
				}
				else
				{
					list = new VariableInitializer[let.Variables.Count + 1];
					for (int i = 0; i < let.Variables.Count; i++)
					{
						list[i] = new VariableInitializer(let.Variables[i].Name, let.Variables[i].Value);
					}
					identifier = Identifier.New();
					list[list.Count - 1] = new VariableInitializer(identifier, let.Expression);
				}
				return this.VisitFieldAccess(new RequiredFieldAccessExpressionSyntaxNode(new RecordExpressionSyntaxNode(list), identifier));
			}

			// Token: 0x0600B047 RID: 45127 RVA: 0x00242400 File Offset: 0x00240600
			private static bool IsLetIdentifier(IList<VariableInitializer> initializers, Identifier identifier)
			{
				for (int i = 0; i < initializers.Count; i++)
				{
					if (initializers[i].Name == identifier)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600B048 RID: 45128 RVA: 0x00242438 File Offset: 0x00240638
			protected override IExpression VisitList(IListExpression list)
			{
				FieldAccessInliner.Node[] elementResults = new FieldAccessInliner.Node[list.Members.Count];
				for (int i = 0; i < elementResults.Length; i++)
				{
					elementResults[i] = this.GetResult(list.Members[i]);
				}
				FieldAccessInliner.Node node = new FieldAccessInliner.ExpressionNode(delegate
				{
					IExpression[] array = new IExpression[elementResults.Length];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = elementResults[j].Expression;
					}
					return this.CreateList(list, array);
				});
				return this.Result(list, node);
			}

			// Token: 0x0600B049 RID: 45129 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override ISection VisitModule(ISection module)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600B04A RID: 45130 RVA: 0x002424C8 File Offset: 0x002406C8
			protected override IExpression VisitMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
			{
				FieldAccessInliner.Node expressionResult = this.GetResult(multiFieldRecordProjection.Expression);
				FieldAccessInliner.Node node = new FieldAccessInliner.ExpressionNode(() => this.CreateMultiFieldRecordProjection(multiFieldRecordProjection, expressionResult.Expression));
				return this.Result(multiFieldRecordProjection, node);
			}

			// Token: 0x0600B04B RID: 45131 RVA: 0x00242520 File Offset: 0x00240720
			protected override IExpression VisitRecord(IRecordExpression record)
			{
				string[] array = new string[record.Members.Count];
				FieldAccessInliner.Node[] array2 = new FieldAccessInliner.Node[record.Members.Count];
				for (int i = 0; i < record.Members.Count; i++)
				{
					array[i] = record.Members[i].Name.Name;
					array2[i] = new FieldAccessInliner.IndirectNode(this.engine);
				}
				FieldAccessInliner.Node node = new FieldAccessInliner.RecordNode(this.engine.Keys(array), array2);
				base.EnterScope(record.Members, array2);
				if (record.Identifier != null)
				{
					base.Environment.Add(record.Identifier, node);
				}
				for (int j = 0; j < record.Members.Count; j++)
				{
					((FieldAccessInliner.IndirectNode)array2[j]).Attach(this.GetResult(record.Members[j].Value));
				}
				if (record.Identifier != null)
				{
					base.Environment.Remove(record.Identifier);
				}
				base.ExitScope(record.Members);
				return this.Result(record, node);
			}

			// Token: 0x0600B04C RID: 45132 RVA: 0x00242648 File Offset: 0x00240848
			protected override IExpression VisitTryCatch(ITryCatchExpression tryCatch)
			{
				FieldAccessInliner.Node tryResult = this.GetResult(tryCatch.Try);
				FieldAccessInliner.Node caseExpressionResult = this.GetResult(tryCatch.ExceptionCase.Expression);
				FieldAccessInliner.Node node = new FieldAccessInliner.ExpressionNode(() => this.CreateTryCatch(tryCatch, tryResult.Expression, this.CreateTryCatchExceptionCase(tryCatch.ExceptionCase, caseExpressionResult.Expression)));
				return this.Result(tryCatch, node);
			}

			// Token: 0x0600B04D RID: 45133 RVA: 0x0000EE09 File Offset: 0x0000D009
			protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x0600B04E RID: 45134 RVA: 0x002426C0 File Offset: 0x002408C0
			protected override IExpression VisitUnary(IUnaryExpression unary)
			{
				FieldAccessInliner.Node expressionResult = this.GetResult(unary.Expression);
				FieldAccessInliner.Node node = new FieldAccessInliner.ExpressionNode(() => this.CreateUnary(unary, expressionResult.Expression));
				return this.Result(unary, node);
			}

			// Token: 0x0600B04F RID: 45135 RVA: 0x00242717 File Offset: 0x00240917
			private IExpression Result(IExpression expression, FieldAccessInliner.Node node)
			{
				this.result = node;
				return expression;
			}

			// Token: 0x0600B050 RID: 45136 RVA: 0x00241F20 File Offset: 0x00240120
			private FieldAccessInliner.Node GetResult(IExpression expression)
			{
				this.VisitExpression(expression);
				return this.result;
			}

			// Token: 0x04005A96 RID: 23190
			private readonly IEngine engine;

			// Token: 0x04005A97 RID: 23191
			private FieldAccessInliner.Node result;
		}

		// Token: 0x02001B80 RID: 7040
		private abstract class Node
		{
			// Token: 0x17002C0F RID: 11279
			// (get) Token: 0x0600B069 RID: 45161
			public abstract IExpression Expression { get; }

			// Token: 0x0600B06A RID: 45162 RVA: 0x000912D6 File Offset: 0x0008F4D6
			public virtual bool TryGetValue(string index, bool isOptional, out FieldAccessInliner.Node node)
			{
				node = null;
				return false;
			}
		}

		// Token: 0x02001B81 RID: 7041
		private class ExpressionNode : FieldAccessInliner.Node
		{
			// Token: 0x0600B06C RID: 45164 RVA: 0x0024291F File Offset: 0x00240B1F
			public ExpressionNode(Func<IExpression> expressionCtor)
			{
				this.expressionCtor = expressionCtor;
			}

			// Token: 0x17002C10 RID: 11280
			// (get) Token: 0x0600B06D RID: 45165 RVA: 0x0024292E File Offset: 0x00240B2E
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = this.expressionCtor();
					}
					return this.expression;
				}
			}

			// Token: 0x0600B06E RID: 45166 RVA: 0x000912D6 File Offset: 0x0008F4D6
			public override bool TryGetValue(string index, bool isOptional, out FieldAccessInliner.Node node)
			{
				node = null;
				return false;
			}

			// Token: 0x04005ABE RID: 23230
			private readonly Func<IExpression> expressionCtor;

			// Token: 0x04005ABF RID: 23231
			private IExpression expression;
		}

		// Token: 0x02001B82 RID: 7042
		private class NullNode : FieldAccessInliner.Node
		{
			// Token: 0x0600B06F RID: 45167 RVA: 0x0024294F File Offset: 0x00240B4F
			public NullNode(IExpression nullExpression)
			{
				this.nullExpression = nullExpression;
			}

			// Token: 0x17002C11 RID: 11281
			// (get) Token: 0x0600B070 RID: 45168 RVA: 0x0024295E File Offset: 0x00240B5E
			public override IExpression Expression
			{
				get
				{
					return this.nullExpression;
				}
			}

			// Token: 0x0600B071 RID: 45169 RVA: 0x00242966 File Offset: 0x00240B66
			public override bool TryGetValue(string index, bool isOptional, out FieldAccessInliner.Node node)
			{
				if (isOptional)
				{
					node = this;
					return true;
				}
				node = null;
				return false;
			}

			// Token: 0x04005AC0 RID: 23232
			private readonly IExpression nullExpression;
		}

		// Token: 0x02001B83 RID: 7043
		private class RecordNode : FieldAccessInliner.Node
		{
			// Token: 0x0600B072 RID: 45170 RVA: 0x00242974 File Offset: 0x00240B74
			public RecordNode(IKeys keys, FieldAccessInliner.Node[] fields)
			{
				this.keys = keys;
				this.fields = fields;
			}

			// Token: 0x17002C12 RID: 11282
			// (get) Token: 0x0600B073 RID: 45171 RVA: 0x0024298C File Offset: 0x00240B8C
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						VariableInitializer[] array = new VariableInitializer[this.keys.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = new VariableInitializer(Identifier.New(this.keys[i]), this.fields[i].Expression);
						}
						this.expression = new RecordExpressionSyntaxNode(array);
					}
					return this.expression;
				}
			}

			// Token: 0x0600B074 RID: 45172 RVA: 0x002429FC File Offset: 0x00240BFC
			public override bool TryGetValue(string index, bool isOptional, out FieldAccessInliner.Node node)
			{
				int num;
				if (this.keys.TryGetIndex(index, out num))
				{
					node = this.fields[num];
					return true;
				}
				if (isOptional)
				{
					node = null;
					return true;
				}
				node = null;
				return false;
			}

			// Token: 0x04005AC1 RID: 23233
			private readonly IKeys keys;

			// Token: 0x04005AC2 RID: 23234
			private readonly FieldAccessInliner.Node[] fields;

			// Token: 0x04005AC3 RID: 23235
			private IExpression expression;
		}

		// Token: 0x02001B84 RID: 7044
		private class IndirectNode : FieldAccessInliner.Node
		{
			// Token: 0x0600B075 RID: 45173 RVA: 0x00242A31 File Offset: 0x00240C31
			public IndirectNode(IEngine engine)
			{
				this.engine = engine;
			}

			// Token: 0x0600B076 RID: 45174 RVA: 0x00242A40 File Offset: 0x00240C40
			public void Attach(FieldAccessInliner.Node node)
			{
				if (this.node != null)
				{
					throw new InvalidOperationException();
				}
				this.node = node;
			}

			// Token: 0x17002C13 RID: 11283
			// (get) Token: 0x0600B077 RID: 45175 RVA: 0x00242A58 File Offset: 0x00240C58
			public override IExpression Expression
			{
				get
				{
					if (this.node == null)
					{
						return this.CreateCyclicReferenceExpression();
					}
					FieldAccessInliner.Node node = this.node;
					this.node = null;
					IExpression expression;
					try
					{
						expression = node.Expression;
					}
					finally
					{
						this.node = node;
					}
					return expression;
				}
			}

			// Token: 0x0600B078 RID: 45176 RVA: 0x00242AA4 File Offset: 0x00240CA4
			public override bool TryGetValue(string index, bool isOptional, out FieldAccessInliner.Node node)
			{
				if (this.node == null)
				{
					node = null;
					return false;
				}
				return this.node.TryGetValue(index, isOptional, out node);
			}

			// Token: 0x0600B079 RID: 45177 RVA: 0x00242AC4 File Offset: 0x00240CC4
			private IExpression CreateCyclicReferenceExpression()
			{
				return new ThrowExpressionSyntaxNode(this.engine.ConstantExpression(this.engine.ExceptionRecord(this.engine.Text("Expression.Error"), this.engine.Text(Strings.CyclicReference), this.engine.Null)));
			}

			// Token: 0x04005AC4 RID: 23236
			private readonly IEngine engine;

			// Token: 0x04005AC5 RID: 23237
			private FieldAccessInliner.Node node;
		}
	}
}
