using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.SqlTranslator
{
	// Token: 0x0200200F RID: 8207
	internal class ScalarSubqueryInliner
	{
		// Token: 0x0600C7F2 RID: 51186 RVA: 0x0027C9B3 File Offset: 0x0027ABB3
		public static IExpression Inline(IEngine engine, IExpression expression)
		{
			return new ScalarSubqueryInliner.Visitor(engine).Visit(expression).Expression;
		}

		// Token: 0x02002010 RID: 8208
		private class Visitor : LogicalAstVisitor2<ScalarSubqueryInliner.Node>
		{
			// Token: 0x0600C7F4 RID: 51188 RVA: 0x0027C9C6 File Offset: 0x0027ABC6
			public Visitor(IEngine engine)
			{
				this.engine = engine;
			}

			// Token: 0x0600C7F5 RID: 51189 RVA: 0x0027C9D5 File Offset: 0x0027ABD5
			public ScalarSubqueryInliner.Node Visit(IExpression expression)
			{
				this.VisitExpression(expression);
				return this.result;
			}

			// Token: 0x0600C7F6 RID: 51190 RVA: 0x0027C9E8 File Offset: 0x0027ABE8
			protected override IExpression VisitExpression(IExpression expression)
			{
				this.result = null;
				expression = base.VisitExpression(expression);
				if (this.result == null)
				{
					this.result = new ScalarSubqueryInliner.ExpressionNode(() => expression);
				}
				return expression;
			}

			// Token: 0x0600C7F7 RID: 51191 RVA: 0x0027CA40 File Offset: 0x0027AC40
			protected override IExpression VisitBinary(IBinaryExpression binary)
			{
				ScalarSubqueryInliner.Node leftResult = this.GetResult(binary.Left);
				ScalarSubqueryInliner.Node rightResult = this.GetResult(binary.Right);
				ScalarSubqueryInliner.Node node = new ScalarSubqueryInliner.ExpressionNode(() => this.CreateBinary(binary, leftResult.Expression, rightResult.Expression));
				return this.Result(binary, node);
			}

			// Token: 0x0600C7F8 RID: 51192 RVA: 0x0027CAB0 File Offset: 0x0027ACB0
			protected override IExpression VisitElementAccess(IElementAccessExpression elementAccess)
			{
				ScalarSubqueryInliner.Node collectionResult = this.GetResult(elementAccess.Collection);
				ScalarSubqueryInliner.Node keyResult = this.GetResult(elementAccess.Key);
				ScalarSubqueryInliner.Node node = new ScalarSubqueryInliner.ExpressionNode(() => this.CreateElementAccess(elementAccess, collectionResult.Expression, keyResult.Expression));
				return this.Result(elementAccess, node);
			}

			// Token: 0x0600C7F9 RID: 51193 RVA: 0x0027CB20 File Offset: 0x0027AD20
			protected override IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
			{
				ScalarSubqueryInliner.Node expressionResult = this.GetResult(fieldAccess.Expression);
				ScalarSubqueryInliner.Node node = new ScalarSubqueryInliner.ExpressionNode(delegate
				{
					ScalarSubqueryInliner.Node node2;
					if (expressionResult.TryGetValue(fieldAccess.MemberName.Name, out node2))
					{
						return node2.Expression;
					}
					if (fieldAccess.IsOptional)
					{
						return this.engine.ConstantExpression(this.engine.Null);
					}
					return this.CreateFieldAccess(fieldAccess, expressionResult.Expression);
				});
				return this.Result(fieldAccess, node);
			}

			// Token: 0x0600C7FA RID: 51194 RVA: 0x0027CB78 File Offset: 0x0027AD78
			protected override IExpression VisitFunction(IFunctionExpression function)
			{
				ScalarSubqueryInliner.Node[] array = new ScalarSubqueryInliner.Node[function.FunctionType.Parameters.Count];
				base.EnterScope(function, array);
				ScalarSubqueryInliner.Node expressionResult = this.GetResult(function.Expression);
				base.ExitScope(function);
				ScalarSubqueryInliner.FunctionNode functionNode = new ScalarSubqueryInliner.FunctionNode(() => this.CreateFunction(function, function.FunctionType, expressionResult.Expression));
				return this.Result(function, functionNode);
			}

			// Token: 0x0600C7FB RID: 51195 RVA: 0x0027CC04 File Offset: 0x0027AE04
			protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
			{
				ScalarSubqueryInliner.Node node;
				if (!base.Environment.TryGetValue(identifier.Name, identifier.IsInclusive, out node) && node != null)
				{
					node = new ScalarSubqueryInliner.ExpressionNode(() => identifier);
				}
				return this.Result(identifier, node);
			}

			// Token: 0x0600C7FC RID: 51196 RVA: 0x0027CC68 File Offset: 0x0027AE68
			protected override IExpression VisitIf(IIfExpression @if)
			{
				ScalarSubqueryInliner.Node conditionResult = this.GetResult(@if.Condition);
				ScalarSubqueryInliner.Node trueResult = this.GetResult(@if.TrueCase);
				ScalarSubqueryInliner.Node falseResult = this.GetResult(@if.FalseCase);
				ScalarSubqueryInliner.Node node = new ScalarSubqueryInliner.ExpressionNode(() => this.CreateIf(@if, conditionResult.Expression, trueResult.Expression, falseResult.Expression));
				return this.Result(@if, node);
			}

			// Token: 0x0600C7FD RID: 51197 RVA: 0x001AD352 File Offset: 0x001AB552
			protected override IExpression VisitImplicitIdentifier(IImplicitIdentifierExpression implicitIdentifier)
			{
				return this.VisitIdentifier(implicitIdentifier);
			}

			// Token: 0x0600C7FE RID: 51198 RVA: 0x0027CCF0 File Offset: 0x0027AEF0
			protected override IExpression VisitInvocation(IInvocationExpression invocation)
			{
				ScalarSubqueryInliner.Node functionResult = this.GetResult(invocation.Function);
				ScalarSubqueryInliner.Node[] argumentResults = new ScalarSubqueryInliner.Node[invocation.Arguments.Count];
				for (int i = 0; i < argumentResults.Length; i++)
				{
					argumentResults[i] = this.GetResult(invocation.Arguments[i]);
				}
				ScalarSubqueryInliner.Node node = null;
				Identifier identifier;
				if (ScalarSubqueryInliner.Visitor.TryGetIdentifier(functionResult, out identifier))
				{
					string name = identifier.Name;
					if (!(name == "Table.FirstValue"))
					{
						if (!(name == "Table.FromRows"))
						{
							if (!(name == "Table.AddColumn"))
							{
								if (!(name == "Table.RenameColumns"))
								{
									if (name == "Table.SelectColumns")
									{
										node = this.NewTableSelectColumns(argumentResults);
									}
								}
								else
								{
									node = this.NewTableRenameColumns(argumentResults);
								}
							}
							else
							{
								node = this.NewTableAddColumn(argumentResults);
							}
						}
						else
						{
							node = this.NewTableFromRows(argumentResults);
						}
					}
					else
					{
						node = this.NewTableFirstValue(argumentResults);
					}
				}
				if (node == null)
				{
					node = new ScalarSubqueryInliner.ExpressionNode(delegate
					{
						IExpression[] array = new IExpression[argumentResults.Length];
						for (int j = 0; j < array.Length; j++)
						{
							array[j] = argumentResults[j].Expression;
						}
						return this.CreateInvocation(invocation, functionResult.Expression, array);
					});
				}
				return this.Result(invocation, node);
			}

			// Token: 0x0600C7FF RID: 51199 RVA: 0x0027CE44 File Offset: 0x0027B044
			private ScalarSubqueryInliner.Node NewTableFirstValue(ScalarSubqueryInliner.Node[] arguments)
			{
				if (arguments.Length == 1 && arguments[0].IsTable)
				{
					ScalarSubqueryInliner.RecordNode recordNode = arguments[0].AsTable.Row(0);
					ScalarSubqueryInliner.Node node;
					if (recordNode != null && recordNode.TryGetValue(recordNode.Keys[0], out node))
					{
						return node;
					}
				}
				return null;
			}

			// Token: 0x0600C800 RID: 51200 RVA: 0x0027CE8C File Offset: 0x0027B08C
			private ScalarSubqueryInliner.Node NewTableFromRows(ScalarSubqueryInliner.Node[] arguments)
			{
				if (arguments.Length == 2)
				{
					IListExpression listExpression = arguments[0].Expression as IListExpression;
					IListExpression listExpression2 = arguments[1].Expression as IListExpression;
					if (listExpression2 != null && listExpression2.Members.Count == 0 && listExpression != null && listExpression.Members.Count == 1)
					{
						return new ScalarSubqueryInliner.SingleRowZeroColumnsTableNode(this.engine);
					}
				}
				return null;
			}

			// Token: 0x0600C801 RID: 51201 RVA: 0x0027CEE9 File Offset: 0x0027B0E9
			private ScalarSubqueryInliner.Node NewTableAddColumn(ScalarSubqueryInliner.Node[] arguments)
			{
				if (arguments.Length == 4 && arguments[0].IsTable)
				{
					return arguments[0].AsTable.AddColumn(arguments[1], arguments[2], arguments[3]);
				}
				return null;
			}

			// Token: 0x0600C802 RID: 51202 RVA: 0x0027CF13 File Offset: 0x0027B113
			private ScalarSubqueryInliner.Node NewTableSelectColumns(ScalarSubqueryInliner.Node[] arguments)
			{
				if (arguments.Length == 2 && arguments[0].IsTable)
				{
					return arguments[0].AsTable.SelectColumns(arguments[1]);
				}
				return null;
			}

			// Token: 0x0600C803 RID: 51203 RVA: 0x0027CF37 File Offset: 0x0027B137
			private ScalarSubqueryInliner.Node NewTableRenameColumns(ScalarSubqueryInliner.Node[] arguments)
			{
				if (arguments.Length == 2 && arguments[0].IsTable)
				{
					return arguments[0].AsTable.RenameColumns(arguments[1]);
				}
				return null;
			}

			// Token: 0x0600C804 RID: 51204 RVA: 0x0027CF5C File Offset: 0x0027B15C
			protected override IExpression VisitLet(ILetExpression let)
			{
				ScalarSubqueryInliner.Node[] array = new ScalarSubqueryInliner.Node[let.Variables.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new ScalarSubqueryInliner.IndirectNode(this.engine);
				}
				base.EnterScope(let.Variables, array);
				for (int j = 0; j < let.Variables.Count; j++)
				{
					((ScalarSubqueryInliner.IndirectNode)array[j]).Attach(this.GetResult(let.Variables[j].Value));
				}
				ScalarSubqueryInliner.Node node = this.GetResult(let.Expression);
				base.ExitScope(let.Variables);
				return this.Result(let, node);
			}

			// Token: 0x0600C805 RID: 51205 RVA: 0x0027D004 File Offset: 0x0027B204
			protected override IExpression VisitList(IListExpression list)
			{
				ScalarSubqueryInliner.Node[] elementResults = new ScalarSubqueryInliner.Node[list.Members.Count];
				for (int i = 0; i < elementResults.Length; i++)
				{
					elementResults[i] = this.GetResult(list.Members[i]);
				}
				ScalarSubqueryInliner.Node node = new ScalarSubqueryInliner.ExpressionNode(delegate
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

			// Token: 0x0600C806 RID: 51206 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override ISection VisitModule(ISection module)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600C807 RID: 51207 RVA: 0x0027D094 File Offset: 0x0027B294
			protected override IExpression VisitMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
			{
				ScalarSubqueryInliner.Node expressionResult = this.GetResult(multiFieldRecordProjection.Expression);
				ScalarSubqueryInliner.Node node = new ScalarSubqueryInliner.ExpressionNode(() => this.CreateMultiFieldRecordProjection(multiFieldRecordProjection, expressionResult.Expression));
				return this.Result(multiFieldRecordProjection, node);
			}

			// Token: 0x0600C808 RID: 51208 RVA: 0x0027D0EC File Offset: 0x0027B2EC
			protected override IExpression VisitRecord(IRecordExpression record)
			{
				string[] array = new string[record.Members.Count];
				ScalarSubqueryInliner.Node[] array2 = new ScalarSubqueryInliner.Node[record.Members.Count];
				for (int i = 0; i < record.Members.Count; i++)
				{
					array[i] = record.Members[i].Name.Name;
					array2[i] = new ScalarSubqueryInliner.IndirectNode(this.engine);
				}
				ScalarSubqueryInliner.Node node = new ScalarSubqueryInliner.RecordNode(this.engine.Keys(array), array2);
				base.EnterScope(record.Members, array2);
				if (record.Identifier != null)
				{
					base.Environment.Add(record.Identifier, node);
				}
				for (int j = 0; j < record.Members.Count; j++)
				{
					((ScalarSubqueryInliner.IndirectNode)array2[j]).Attach(this.GetResult(record.Members[j].Value));
				}
				if (record.Identifier != null)
				{
					base.Environment.Remove(record.Identifier);
				}
				base.ExitScope(record.Members);
				return this.Result(record, node);
			}

			// Token: 0x0600C809 RID: 51209 RVA: 0x0027D214 File Offset: 0x0027B414
			protected override IExpression VisitTryCatch(ITryCatchExpression tryCatch)
			{
				ScalarSubqueryInliner.Node tryResult = this.GetResult(tryCatch.Try);
				ScalarSubqueryInliner.Node caseExpressionResult = this.GetResult(tryCatch.ExceptionCase.Expression);
				ScalarSubqueryInliner.Node node = new ScalarSubqueryInliner.ExpressionNode(() => this.CreateTryCatch(tryCatch, tryResult.Expression, this.CreateTryCatchExceptionCase(tryCatch.ExceptionCase, caseExpressionResult.Expression)));
				return this.Result(tryCatch, node);
			}

			// Token: 0x0600C80A RID: 51210 RVA: 0x0000EE09 File Offset: 0x0000D009
			protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x0600C80B RID: 51211 RVA: 0x0027D28C File Offset: 0x0027B48C
			protected override IExpression VisitUnary(IUnaryExpression unary)
			{
				ScalarSubqueryInliner.Node expressionResult = this.GetResult(unary.Expression);
				ScalarSubqueryInliner.Node node = new ScalarSubqueryInliner.ExpressionNode(() => this.CreateUnary(unary, expressionResult.Expression));
				return this.Result(unary, node);
			}

			// Token: 0x0600C80C RID: 51212 RVA: 0x0027D2E3 File Offset: 0x0027B4E3
			private IExpression Result(IExpression expression, ScalarSubqueryInliner.Node node)
			{
				this.result = node;
				return expression;
			}

			// Token: 0x0600C80D RID: 51213 RVA: 0x0027C9D5 File Offset: 0x0027ABD5
			private ScalarSubqueryInliner.Node GetResult(IExpression expression)
			{
				this.VisitExpression(expression);
				return this.result;
			}

			// Token: 0x0600C80E RID: 51214 RVA: 0x0027D2F0 File Offset: 0x0027B4F0
			private static bool TryGetIdentifier(ScalarSubqueryInliner.Node node, out Identifier identifier)
			{
				IIdentifierExpression identifierExpression = node.Expression as IIdentifierExpression;
				if (identifierExpression != null)
				{
					identifier = identifierExpression.Name;
					return true;
				}
				identifier = null;
				return false;
			}

			// Token: 0x0400660A RID: 26122
			private readonly IEngine engine;

			// Token: 0x0400660B RID: 26123
			private ScalarSubqueryInliner.Node result;
		}

		// Token: 0x0200201D RID: 8221
		private abstract class Node
		{
			// Token: 0x1700306D RID: 12397
			// (get) Token: 0x0600C827 RID: 51239
			public abstract IExpression Expression { get; }

			// Token: 0x1700306E RID: 12398
			// (get) Token: 0x0600C828 RID: 51240 RVA: 0x00002105 File Offset: 0x00000305
			public virtual bool IsTable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700306F RID: 12399
			// (get) Token: 0x0600C829 RID: 51241 RVA: 0x00002105 File Offset: 0x00000305
			public virtual bool IsFunction
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17003070 RID: 12400
			// (get) Token: 0x0600C82A RID: 51242 RVA: 0x0000EE09 File Offset: 0x0000D009
			public virtual ScalarSubqueryInliner.TableNode AsTable
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17003071 RID: 12401
			// (get) Token: 0x0600C82B RID: 51243 RVA: 0x0000EE09 File Offset: 0x0000D009
			public virtual ScalarSubqueryInliner.FunctionNode AsFunction
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x0600C82C RID: 51244 RVA: 0x0007D355 File Offset: 0x0007B555
			public virtual bool TryGetValue(string index, out ScalarSubqueryInliner.Node node)
			{
				node = null;
				return false;
			}
		}

		// Token: 0x0200201E RID: 8222
		private class ExpressionNode : ScalarSubqueryInliner.Node
		{
			// Token: 0x0600C82E RID: 51246 RVA: 0x0027D576 File Offset: 0x0027B776
			public ExpressionNode(Func<IExpression> expressionCtor)
			{
				this.expressionCtor = expressionCtor;
			}

			// Token: 0x17003072 RID: 12402
			// (get) Token: 0x0600C82F RID: 51247 RVA: 0x0027D585 File Offset: 0x0027B785
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

			// Token: 0x0600C830 RID: 51248 RVA: 0x0007D355 File Offset: 0x0007B555
			public override bool TryGetValue(string index, out ScalarSubqueryInliner.Node node)
			{
				node = null;
				return false;
			}

			// Token: 0x04006632 RID: 26162
			private readonly Func<IExpression> expressionCtor;

			// Token: 0x04006633 RID: 26163
			private IExpression expression;
		}

		// Token: 0x0200201F RID: 8223
		private class RecordNode : ScalarSubqueryInliner.Node
		{
			// Token: 0x0600C831 RID: 51249 RVA: 0x0027D5A6 File Offset: 0x0027B7A6
			public RecordNode(IKeys keys, ScalarSubqueryInliner.Node[] fields)
			{
				this.keys = keys;
				this.fields = fields;
			}

			// Token: 0x17003073 RID: 12403
			// (get) Token: 0x0600C832 RID: 51250 RVA: 0x0027D5BC File Offset: 0x0027B7BC
			public IKeys Keys
			{
				get
				{
					return this.keys;
				}
			}

			// Token: 0x17003074 RID: 12404
			// (get) Token: 0x0600C833 RID: 51251 RVA: 0x0027D5C4 File Offset: 0x0027B7C4
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

			// Token: 0x0600C834 RID: 51252 RVA: 0x0027D634 File Offset: 0x0027B834
			public override bool TryGetValue(string index, out ScalarSubqueryInliner.Node node)
			{
				int num;
				if (this.keys.TryGetIndex(index, out num))
				{
					node = this.fields[num];
					return true;
				}
				node = null;
				return false;
			}

			// Token: 0x04006634 RID: 26164
			private readonly IKeys keys;

			// Token: 0x04006635 RID: 26165
			private readonly ScalarSubqueryInliner.Node[] fields;

			// Token: 0x04006636 RID: 26166
			private IExpression expression;
		}

		// Token: 0x02002020 RID: 8224
		private abstract class TableNode : ScalarSubqueryInliner.Node
		{
			// Token: 0x0600C835 RID: 51253 RVA: 0x0027D661 File Offset: 0x0027B861
			public TableNode(IEngine engine)
			{
				this.engine = engine;
			}

			// Token: 0x17003075 RID: 12405
			// (get) Token: 0x0600C836 RID: 51254 RVA: 0x00002139 File Offset: 0x00000339
			public sealed override bool IsTable
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17003076 RID: 12406
			// (get) Token: 0x0600C837 RID: 51255 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public sealed override ScalarSubqueryInliner.TableNode AsTable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17003077 RID: 12407
			// (get) Token: 0x0600C838 RID: 51256
			public abstract IKeys Columns { get; }

			// Token: 0x17003078 RID: 12408
			// (get) Token: 0x0600C839 RID: 51257
			public abstract ScalarSubqueryInliner.FunctionNode[] ColumnCtors { get; }

			// Token: 0x17003079 RID: 12409
			// (get) Token: 0x0600C83A RID: 51258
			public abstract ScalarSubqueryInliner.Node[] ColumnTypes { get; }

			// Token: 0x0600C83B RID: 51259 RVA: 0x0027D670 File Offset: 0x0027B870
			public ScalarSubqueryInliner.TableNode SelectColumns(ScalarSubqueryInliner.Node columnNames)
			{
				IListExpression listExpression = columnNames.Expression as IListExpression;
				if (listExpression != null)
				{
					string[] array = new string[listExpression.Members.Count];
					for (int i = 0; i < listExpression.Members.Count; i++)
					{
						IConstantExpression2 constantExpression = listExpression.Members[i] as IConstantExpression2;
						if (constantExpression == null || !constantExpression.Value.IsText)
						{
							return null;
						}
						array[i] = constantExpression.Value.AsString;
					}
					return this.SelectColumns(array);
				}
				return null;
			}

			// Token: 0x0600C83C RID: 51260 RVA: 0x0027D6F0 File Offset: 0x0027B8F0
			public ScalarSubqueryInliner.TableNode RenameColumns(ScalarSubqueryInliner.Node renames)
			{
				IListExpression listExpression = renames.Expression as IListExpression;
				if (listExpression != null)
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					for (int i = 0; i < listExpression.Members.Count; i++)
					{
						IListExpression listExpression2 = listExpression.Members[i] as IListExpression;
						if (listExpression2 == null || listExpression2.Members.Count != 2)
						{
							return null;
						}
						IConstantExpression2 constantExpression = listExpression2.Members[0] as IConstantExpression2;
						IConstantExpression2 constantExpression2 = listExpression2.Members[1] as IConstantExpression2;
						if (constantExpression == null || !constantExpression.Value.IsText || constantExpression2 == null || !constantExpression2.Value.IsText)
						{
							return null;
						}
						dictionary.Add(constantExpression.Value.AsString, constantExpression2.Value.AsString);
					}
					return this.RenameColumns(dictionary);
				}
				return null;
			}

			// Token: 0x0600C83D RID: 51261 RVA: 0x0027D7CC File Offset: 0x0027B9CC
			public ScalarSubqueryInliner.TableNode AddColumn(ScalarSubqueryInliner.Node columnName, ScalarSubqueryInliner.Node columnCtor, ScalarSubqueryInliner.Node columnType)
			{
				IConstantExpression2 constantExpression = columnName.Expression as IConstantExpression2;
				if (constantExpression != null && constantExpression.Value.IsText && columnCtor.IsFunction)
				{
					return this.AddColumn(constantExpression.Value.AsString, columnCtor.AsFunction, columnType);
				}
				return null;
			}

			// Token: 0x0600C83E RID: 51262 RVA: 0x000020FA File Offset: 0x000002FA
			public virtual ScalarSubqueryInliner.RecordNode Row(int index)
			{
				return null;
			}

			// Token: 0x0600C83F RID: 51263 RVA: 0x000020FA File Offset: 0x000002FA
			public virtual ScalarSubqueryInliner.TableNode SelectColumns(string[] columnNames)
			{
				return null;
			}

			// Token: 0x0600C840 RID: 51264 RVA: 0x000020FA File Offset: 0x000002FA
			public virtual ScalarSubqueryInliner.TableNode RenameColumns(Dictionary<string, string> renames)
			{
				return null;
			}

			// Token: 0x0600C841 RID: 51265 RVA: 0x000020FA File Offset: 0x000002FA
			public virtual ScalarSubqueryInliner.TableNode AddColumn(string columnName, ScalarSubqueryInliner.FunctionNode columnCtor, ScalarSubqueryInliner.Node columnType)
			{
				return null;
			}

			// Token: 0x04006637 RID: 26167
			protected readonly IEngine engine;
		}

		// Token: 0x02002021 RID: 8225
		private class SingleRowZeroColumnsTableNode : ScalarSubqueryInliner.TableNode
		{
			// Token: 0x0600C842 RID: 51266 RVA: 0x0027D817 File Offset: 0x0027BA17
			public SingleRowZeroColumnsTableNode(IEngine engine)
				: base(engine)
			{
			}

			// Token: 0x1700307A RID: 12410
			// (get) Token: 0x0600C843 RID: 51267 RVA: 0x0027D820 File Offset: 0x0027BA20
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = new InvocationExpressionSyntaxNode2(new ExclusiveIdentifierExpressionSyntaxNode("Table.FromRows"), new ListExpressionSyntaxNode(new IExpression[]
						{
							new ListExpressionSyntaxNode(EmptyArray<IExpression>.Instance)
						}), new ListExpressionSyntaxNode(EmptyArray<IExpression>.Instance));
					}
					return this.expression;
				}
			}

			// Token: 0x1700307B RID: 12411
			// (get) Token: 0x0600C844 RID: 51268 RVA: 0x0027D877 File Offset: 0x0027BA77
			public override IKeys Columns
			{
				get
				{
					return this.engine.Keys(EmptyArray<string>.Instance);
				}
			}

			// Token: 0x1700307C RID: 12412
			// (get) Token: 0x0600C845 RID: 51269 RVA: 0x0027D889 File Offset: 0x0027BA89
			public override ScalarSubqueryInliner.FunctionNode[] ColumnCtors
			{
				get
				{
					return EmptyArray<ScalarSubqueryInliner.FunctionNode>.Instance;
				}
			}

			// Token: 0x1700307D RID: 12413
			// (get) Token: 0x0600C846 RID: 51270 RVA: 0x0027D890 File Offset: 0x0027BA90
			public override ScalarSubqueryInliner.Node[] ColumnTypes
			{
				get
				{
					return EmptyArray<ScalarSubqueryInliner.Node>.Instance;
				}
			}

			// Token: 0x0600C847 RID: 51271 RVA: 0x0027D897 File Offset: 0x0027BA97
			public override ScalarSubqueryInliner.RecordNode Row(int index)
			{
				if (index == 0)
				{
					return new ScalarSubqueryInliner.RecordNode(this.engine.Keys(EmptyArray<string>.Instance), EmptyArray<ScalarSubqueryInliner.Node>.Instance);
				}
				return base.Row(index);
			}

			// Token: 0x0600C848 RID: 51272 RVA: 0x0027D8BE File Offset: 0x0027BABE
			public override ScalarSubqueryInliner.TableNode AddColumn(string columnName, ScalarSubqueryInliner.FunctionNode columnCtor, ScalarSubqueryInliner.Node columnType)
			{
				return new ScalarSubqueryInliner.AddColumnsTableNode(this.engine, this, new string[] { columnName }, new ScalarSubqueryInliner.FunctionNode[] { columnCtor }, new ScalarSubqueryInliner.Node[] { columnType });
			}

			// Token: 0x04006638 RID: 26168
			private IExpression expression;
		}

		// Token: 0x02002022 RID: 8226
		private class AddColumnsTableNode : ScalarSubqueryInliner.TableNode
		{
			// Token: 0x0600C849 RID: 51273 RVA: 0x0027D8EA File Offset: 0x0027BAEA
			public AddColumnsTableNode(IEngine engine, ScalarSubqueryInliner.TableNode inner, string[] columnNames, ScalarSubqueryInliner.FunctionNode[] columnCtors, ScalarSubqueryInliner.Node[] columnTypes)
				: base(engine)
			{
				this.inner = inner;
				this.columnNames = columnNames;
				this.columnCtors = columnCtors;
				this.columnTypes = columnTypes;
			}

			// Token: 0x1700307E RID: 12414
			// (get) Token: 0x0600C84A RID: 51274 RVA: 0x0027D911 File Offset: 0x0027BB11
			public override IKeys Columns
			{
				get
				{
					return this.engine.Keys(this.columnNames);
				}
			}

			// Token: 0x1700307F RID: 12415
			// (get) Token: 0x0600C84B RID: 51275 RVA: 0x0027D924 File Offset: 0x0027BB24
			public override ScalarSubqueryInliner.FunctionNode[] ColumnCtors
			{
				get
				{
					return this.columnCtors;
				}
			}

			// Token: 0x17003080 RID: 12416
			// (get) Token: 0x0600C84C RID: 51276 RVA: 0x0027D92C File Offset: 0x0027BB2C
			public override ScalarSubqueryInliner.Node[] ColumnTypes
			{
				get
				{
					return this.columnTypes;
				}
			}

			// Token: 0x17003081 RID: 12417
			// (get) Token: 0x0600C84D RID: 51277 RVA: 0x0027D934 File Offset: 0x0027BB34
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						IExpression[] array = new IExpression[this.columnNames.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = new ListExpressionSyntaxNode(new IExpression[]
							{
								this.engine.ConstantExpression(this.engine.Text(this.columnNames[i])),
								this.columnCtors[i].Expression,
								this.columnTypes[i].Expression
							});
						}
						this.expression = new InvocationExpressionSyntaxNodeN(new ExclusiveIdentifierExpressionSyntaxNode(Identifier.New("Table.AddColumns")), new IExpression[]
						{
							this.inner.Expression,
							new ListExpressionSyntaxNode(array)
						});
					}
					return this.expression;
				}
			}

			// Token: 0x0600C84E RID: 51278 RVA: 0x0027D9F8 File Offset: 0x0027BBF8
			public override ScalarSubqueryInliner.RecordNode Row(int index)
			{
				ScalarSubqueryInliner.Node node = this.inner.Row(index);
				if (node != null)
				{
					ScalarSubqueryInliner.Node[] array = new ScalarSubqueryInliner.Node[this.columnCtors.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this.columnCtors[i].Invoke(new ScalarSubqueryInliner.Node[] { node });
					}
					return new ScalarSubqueryInliner.RecordNode(this.Columns, array);
				}
				return base.Row(index);
			}

			// Token: 0x0600C84F RID: 51279 RVA: 0x0027DA60 File Offset: 0x0027BC60
			public override ScalarSubqueryInliner.TableNode SelectColumns(string[] columnNames)
			{
				HashSet<string> hashSet = new HashSet<string>(columnNames);
				string[] array = new string[columnNames.Length];
				ScalarSubqueryInliner.FunctionNode[] array2 = new ScalarSubqueryInliner.FunctionNode[columnNames.Length];
				ScalarSubqueryInliner.Node[] array3 = new ScalarSubqueryInliner.Node[columnNames.Length];
				int num = 0;
				for (int i = 0; i < this.columnNames.Length; i++)
				{
					if (hashSet.Contains(this.columnNames[i]))
					{
						array[num] = this.columnNames[i];
						array2[num] = this.columnCtors[i];
						array3[num] = this.columnTypes[i];
						num++;
					}
				}
				if (num != columnNames.Length)
				{
					return base.SelectColumns(columnNames);
				}
				return new ScalarSubqueryInliner.AddColumnsTableNode(this.engine, this.inner, array, array2, array3);
			}

			// Token: 0x0600C850 RID: 51280 RVA: 0x0027DB0C File Offset: 0x0027BD0C
			public override ScalarSubqueryInliner.TableNode RenameColumns(Dictionary<string, string> renames)
			{
				string[] array = new string[this.columnNames.Length];
				for (int i = 0; i < array.Length; i++)
				{
					string text;
					if (!renames.TryGetValue(this.columnNames[i], out text))
					{
						text = this.columnNames[i];
					}
					array[i] = text;
				}
				return new ScalarSubqueryInliner.AddColumnsTableNode(this.engine, this.inner, array, this.columnCtors, this.columnTypes);
			}

			// Token: 0x0600C851 RID: 51281 RVA: 0x0027DB74 File Offset: 0x0027BD74
			public override ScalarSubqueryInliner.TableNode AddColumn(string columnName, ScalarSubqueryInliner.FunctionNode columnCtor, ScalarSubqueryInliner.Node columnType)
			{
				ScalarSubqueryInliner.FunctionNode functionNode = new ScalarSubqueryInliner.FunctionNode(() => new ScalarSubqueryInliner.AddColumnsTableNode.ColumnReferenceInliner(this).Inline((IFunctionExpression)columnCtor.Expression));
				return new ScalarSubqueryInliner.AddColumnsTableNode(this.engine, this.inner, ScalarSubqueryInliner.AddColumnsTableNode.Concat<string>(this.columnNames, columnName), ScalarSubqueryInliner.AddColumnsTableNode.Concat<ScalarSubqueryInliner.FunctionNode>(this.columnCtors, functionNode), ScalarSubqueryInliner.AddColumnsTableNode.Concat<ScalarSubqueryInliner.Node>(this.columnTypes, columnType));
			}

			// Token: 0x0600C852 RID: 51282 RVA: 0x0027DBDC File Offset: 0x0027BDDC
			private static T[] Concat<T>(T[] array, T item)
			{
				T[] array2 = new T[array.Length + 1];
				Array.Copy(array, array2, array.Length);
				array2[array.Length] = item;
				return array2;
			}

			// Token: 0x04006639 RID: 26169
			private readonly ScalarSubqueryInliner.TableNode inner;

			// Token: 0x0400663A RID: 26170
			private readonly string[] columnNames;

			// Token: 0x0400663B RID: 26171
			private readonly ScalarSubqueryInliner.FunctionNode[] columnCtors;

			// Token: 0x0400663C RID: 26172
			private readonly ScalarSubqueryInliner.Node[] columnTypes;

			// Token: 0x0400663D RID: 26173
			private IExpression expression;

			// Token: 0x02002023 RID: 8227
			private class ColumnReferenceInliner : LogicalAstVisitor2<bool>
			{
				// Token: 0x0600C853 RID: 51283 RVA: 0x0027DC09 File Offset: 0x0027BE09
				public ColumnReferenceInliner(ScalarSubqueryInliner.TableNode table)
				{
					this.table = table;
				}

				// Token: 0x0600C854 RID: 51284 RVA: 0x0027DC18 File Offset: 0x0027BE18
				public IExpression Inline(IFunctionExpression rowFunction)
				{
					bool[] array = new bool[] { true };
					return base.VisitFunction(rowFunction, array);
				}

				// Token: 0x0600C855 RID: 51285 RVA: 0x0027DC38 File Offset: 0x0027BE38
				protected override IExpression VisitFunction(IFunctionExpression function)
				{
					bool[] array = new bool[function.FunctionType.Parameters.Count];
					return base.VisitFunction(function, array);
				}

				// Token: 0x0600C856 RID: 51286 RVA: 0x0027DC64 File Offset: 0x0027BE64
				protected override IExpression VisitLet(ILetExpression let)
				{
					bool[] array = new bool[let.Variables.Count];
					return base.VisitLet(let, array);
				}

				// Token: 0x0600C857 RID: 51287 RVA: 0x0027DC8C File Offset: 0x0027BE8C
				protected override IExpression VisitRecord(IRecordExpression record)
				{
					bool[] array = new bool[record.Members.Count];
					return base.VisitRecord(record, false, array);
				}

				// Token: 0x0600C858 RID: 51288 RVA: 0x00147058 File Offset: 0x00145258
				protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
				{
					return base.VisitTryCatchExceptionCase(tryCatchExceptionCase, false);
				}

				// Token: 0x0600C859 RID: 51289 RVA: 0x0027DCB4 File Offset: 0x0027BEB4
				protected override ISection VisitModule(ISection module)
				{
					bool[] array = new bool[module.Members.Count];
					return base.VisitModule(module, array);
				}

				// Token: 0x0600C85A RID: 51290 RVA: 0x0027DCDC File Offset: 0x0027BEDC
				protected override IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
				{
					fieldAccess = (IFieldAccessExpression)base.VisitFieldAccess(fieldAccess);
					IIdentifierExpression identifierExpression = fieldAccess.Expression as IIdentifierExpression;
					bool flag;
					int num;
					if (identifierExpression != null && base.Environment.TryGetValue(identifierExpression.Name, identifierExpression.IsInclusive, out flag) && flag && this.table.Columns.TryGetIndex(fieldAccess.MemberName.Name, out num))
					{
						return new InvocationExpressionSyntaxNode1(this.table.ColumnCtors[num].Expression, identifierExpression);
					}
					return fieldAccess;
				}

				// Token: 0x0400663E RID: 26174
				private readonly ScalarSubqueryInliner.TableNode table;
			}
		}

		// Token: 0x02002025 RID: 8229
		private class FunctionNode : ScalarSubqueryInliner.ExpressionNode
		{
			// Token: 0x0600C85D RID: 51293 RVA: 0x0027DD81 File Offset: 0x0027BF81
			public FunctionNode(Func<IExpression> expressionCtor)
				: base(expressionCtor)
			{
			}

			// Token: 0x17003082 RID: 12418
			// (get) Token: 0x0600C85E RID: 51294 RVA: 0x00002139 File Offset: 0x00000339
			public sealed override bool IsFunction
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17003083 RID: 12419
			// (get) Token: 0x0600C85F RID: 51295 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public sealed override ScalarSubqueryInliner.FunctionNode AsFunction
			{
				get
				{
					return this;
				}
			}

			// Token: 0x0600C860 RID: 51296 RVA: 0x0027DD8A File Offset: 0x0027BF8A
			public ScalarSubqueryInliner.Node Invoke(params ScalarSubqueryInliner.Node[] arguments)
			{
				return new ScalarSubqueryInliner.ExpressionNode(delegate
				{
					IExpression[] array = arguments.Select((ScalarSubqueryInliner.Node a) => a.Expression).ToArray<IExpression>();
					return new InvocationExpressionSyntaxNodeN(this.Expression, array);
				});
			}
		}

		// Token: 0x02002028 RID: 8232
		private class IndirectNode : ScalarSubqueryInliner.Node
		{
			// Token: 0x0600C866 RID: 51302 RVA: 0x0027DE12 File Offset: 0x0027C012
			public IndirectNode(IEngine engine)
			{
				this.engine = engine;
			}

			// Token: 0x0600C867 RID: 51303 RVA: 0x0027DE21 File Offset: 0x0027C021
			public void Attach(ScalarSubqueryInliner.Node node)
			{
				if (this.node != null)
				{
					throw new InvalidOperationException();
				}
				this.node = node;
			}

			// Token: 0x17003084 RID: 12420
			// (get) Token: 0x0600C868 RID: 51304 RVA: 0x0027DE38 File Offset: 0x0027C038
			public override IExpression Expression
			{
				get
				{
					if (this.node == null)
					{
						return this.CreateCyclicReferenceExpression();
					}
					ScalarSubqueryInliner.Node node = this.node;
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

			// Token: 0x17003085 RID: 12421
			// (get) Token: 0x0600C869 RID: 51305 RVA: 0x0027DE84 File Offset: 0x0027C084
			public override bool IsTable
			{
				get
				{
					if (this.node == null)
					{
						return base.IsTable;
					}
					ScalarSubqueryInliner.Node node = this.node;
					this.node = null;
					bool isTable;
					try
					{
						isTable = node.IsTable;
					}
					finally
					{
						this.node = node;
					}
					return isTable;
				}
			}

			// Token: 0x17003086 RID: 12422
			// (get) Token: 0x0600C86A RID: 51306 RVA: 0x0027DED0 File Offset: 0x0027C0D0
			public override ScalarSubqueryInliner.TableNode AsTable
			{
				get
				{
					if (this.node == null)
					{
						return base.AsTable;
					}
					ScalarSubqueryInliner.Node node = this.node;
					this.node = null;
					ScalarSubqueryInliner.TableNode asTable;
					try
					{
						asTable = node.AsTable;
					}
					finally
					{
						this.node = node;
					}
					return asTable;
				}
			}

			// Token: 0x0600C86B RID: 51307 RVA: 0x0027DF1C File Offset: 0x0027C11C
			public override bool TryGetValue(string index, out ScalarSubqueryInliner.Node node)
			{
				return this.node.TryGetValue(index, out node);
			}

			// Token: 0x0600C86C RID: 51308 RVA: 0x0027DF2B File Offset: 0x0027C12B
			private IExpression CreateCyclicReferenceExpression()
			{
				return this.engine.NewExpressionError(Strings.ValueException_CyclicReference, null);
			}

			// Token: 0x04006645 RID: 26181
			private readonly IEngine engine;

			// Token: 0x04006646 RID: 26182
			private ScalarSubqueryInliner.Node node;
		}
	}
}
