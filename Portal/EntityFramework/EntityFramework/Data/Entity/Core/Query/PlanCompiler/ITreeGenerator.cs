using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000348 RID: 840
	internal class ITreeGenerator : DbExpressionVisitor<Node>
	{
		// Token: 0x060027E7 RID: 10215 RVA: 0x0007665C File Offset: 0x0007485C
		private static Dictionary<DbExpressionKind, OpType> InitializeExpressionKindToOpTypeMap()
		{
			Dictionary<DbExpressionKind, OpType> dictionary = new Dictionary<DbExpressionKind, OpType>(12);
			dictionary[DbExpressionKind.Plus] = OpType.Plus;
			dictionary[DbExpressionKind.Minus] = OpType.Minus;
			dictionary[DbExpressionKind.Multiply] = OpType.Multiply;
			dictionary[DbExpressionKind.Divide] = OpType.Divide;
			dictionary[DbExpressionKind.Modulo] = OpType.Modulo;
			dictionary[DbExpressionKind.UnaryMinus] = OpType.UnaryMinus;
			dictionary[DbExpressionKind.Equals] = OpType.EQ;
			dictionary[DbExpressionKind.NotEquals] = OpType.NE;
			dictionary[DbExpressionKind.LessThan] = OpType.LT;
			dictionary[DbExpressionKind.GreaterThan] = OpType.GT;
			dictionary[DbExpressionKind.LessThanOrEquals] = OpType.LE;
			dictionary[DbExpressionKind.GreaterThanOrEquals] = OpType.GE;
			return dictionary;
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x000766E5 File Offset: 0x000748E5
		internal Dictionary<Node, Var> VarMap
		{
			get
			{
				return this._varMap;
			}
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x000766ED File Offset: 0x000748ED
		public static Command Generate(DbQueryCommandTree ctree)
		{
			return ITreeGenerator.Generate(ctree, null);
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000766F6 File Offset: 0x000748F6
		internal static Command Generate(DbQueryCommandTree ctree, DiscriminatorMap discriminatorMap)
		{
			return new ITreeGenerator(ctree, discriminatorMap)._iqtCommand;
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x00076704 File Offset: 0x00074904
		private ITreeGenerator(DbQueryCommandTree ctree, DiscriminatorMap discriminatorMap)
		{
			this._useDatabaseNullSemantics = ctree.UseDatabaseNullSemantics;
			this._iqtCommand = new Command(ctree.MetadataWorkspace);
			if (discriminatorMap != null)
			{
				this._discriminatorMap = discriminatorMap;
				PlanCompiler.Assert(ctree.Query.ExpressionKind == DbExpressionKind.Project, "top level QMV expression must be project to match discriminator pattern");
				this._discriminatedViewTopProject = (DbProjectExpression)ctree.Query;
			}
			foreach (KeyValuePair<string, TypeUsage> keyValuePair in ctree.Parameters)
			{
				if (!ITreeGenerator.ValidateParameterType(keyValuePair.Value))
				{
					throw new NotSupportedException(Strings.ParameterTypeNotSupported(keyValuePair.Key, keyValuePair.Value.ToString()));
				}
				this._iqtCommand.CreateParameterVar(keyValuePair.Key, keyValuePair.Value);
			}
			this._iqtCommand.Root = this.VisitExpr(ctree.Query);
			if (!this._iqtCommand.Root.Op.IsRelOp)
			{
				Node node = this.ConvertToScalarOpTree(this._iqtCommand.Root, ctree.Query);
				Node node2 = this._iqtCommand.CreateNode(this._iqtCommand.CreateSingleRowTableOp());
				Var var;
				Node node3 = this._iqtCommand.CreateVarDefListNode(node, out var);
				ProjectOp projectOp = this._iqtCommand.CreateProjectOp(var);
				Node node4 = this._iqtCommand.CreateNode(projectOp, node2, node3);
				if (TypeSemantics.IsCollectionType(this._iqtCommand.Root.Op.Type))
				{
					UnnestOp unnestOp = this._iqtCommand.CreateUnnestOp(var);
					node4 = this._iqtCommand.CreateNode(unnestOp, node3.Child0);
					var = unnestOp.Table.Columns[0];
				}
				this._iqtCommand.Root = node4;
				this._varMap[this._iqtCommand.Root] = var;
			}
			this._iqtCommand.Root = this.CapWithPhysicalProject(this._iqtCommand.Root);
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x00076950 File Offset: 0x00074B50
		private static bool ValidateParameterType(TypeUsage paramType)
		{
			return paramType != null && paramType.EdmType != null && (TypeSemantics.IsPrimitiveType(paramType) || paramType.EdmType is EnumType);
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x00076977 File Offset: 0x00074B77
		private static RowType ExtractElementRowType(TypeUsage typeUsage)
		{
			return TypeHelpers.GetEdmType<RowType>(TypeHelpers.GetEdmType<CollectionType>(typeUsage).TypeUsage);
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x0007698C File Offset: 0x00074B8C
		private bool IsPredicate(DbExpression expr)
		{
			if (TypeSemantics.IsPrimitiveType(expr.ResultType, PrimitiveTypeKind.Boolean))
			{
				DbExpressionKind expressionKind = expr.ExpressionKind;
				if (expressionKind <= DbExpressionKind.NotEquals)
				{
					if (expressionKind > DbExpressionKind.Any)
					{
						switch (expressionKind)
						{
						case DbExpressionKind.Equals:
						case DbExpressionKind.GreaterThan:
						case DbExpressionKind.GreaterThanOrEquals:
						case DbExpressionKind.IsEmpty:
						case DbExpressionKind.IsNull:
						case DbExpressionKind.IsOf:
						case DbExpressionKind.IsOfOnly:
						case DbExpressionKind.LessThan:
						case DbExpressionKind.LessThanOrEquals:
						case DbExpressionKind.Like:
						case DbExpressionKind.Not:
						case DbExpressionKind.NotEquals:
							break;
						case DbExpressionKind.Except:
						case DbExpressionKind.Filter:
						case DbExpressionKind.FullOuterJoin:
						case DbExpressionKind.GroupBy:
						case DbExpressionKind.InnerJoin:
						case DbExpressionKind.Intersect:
						case DbExpressionKind.LeftOuterJoin:
						case DbExpressionKind.Limit:
						case DbExpressionKind.Minus:
						case DbExpressionKind.Modulo:
						case DbExpressionKind.Multiply:
						case DbExpressionKind.NewInstance:
							return false;
						case DbExpressionKind.Function:
						{
							if (!((DbFunctionExpression)expr).Function.HasUserDefinedBody)
							{
								return false;
							}
							bool flag;
							if (this._functionsIsPredicateFlag.TryGetValue(expr, out flag))
							{
								return flag;
							}
							PlanCompiler.Assert(false, "IsPredicate must be called on a visited function expression");
							return false;
						}
						default:
							return false;
						}
					}
				}
				else if (expressionKind != DbExpressionKind.Or)
				{
					switch (expressionKind)
					{
					case DbExpressionKind.VariableReference:
					{
						DbVariableReferenceExpression dbVariableReferenceExpression = (DbVariableReferenceExpression)expr;
						return this.ResolveScope(dbVariableReferenceExpression).IsPredicate(dbVariableReferenceExpression.VariableName);
					}
					case DbExpressionKind.Lambda:
					{
						bool flag2;
						if (this._functionsIsPredicateFlag.TryGetValue(expr, out flag2))
						{
							return flag2;
						}
						PlanCompiler.Assert(false, "IsPredicate must be called on a visited lambda expression");
						return false;
					}
					case DbExpressionKind.In:
						break;
					default:
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x00076ABC File Offset: 0x00074CBC
		private Node VisitExpr(DbExpression e)
		{
			if (e == null)
			{
				return null;
			}
			return e.Accept<Node>(this);
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x00076ACC File Offset: 0x00074CCC
		private Node VisitExprAsScalar(DbExpression expr)
		{
			if (expr == null)
			{
				return null;
			}
			Node node = this.VisitExpr(expr);
			return this.ConvertToScalarOpTree(node, expr);
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x00076AF0 File Offset: 0x00074CF0
		private Node ConvertToScalarOpTree(Node node, DbExpression expr)
		{
			if (node.Op.IsRelOp)
			{
				node = this.ConvertRelOpToScalarOpTree(node, expr.ResultType);
			}
			else if (this.IsPredicate(expr))
			{
				node = this.ConvertPredicateToScalarOpTree(node, expr);
			}
			return node;
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x00076B24 File Offset: 0x00074D24
		private Node ConvertRelOpToScalarOpTree(Node node, TypeUsage resultType)
		{
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(resultType), "RelOp with non-Collection result type");
			CollectOp collectOp = this._iqtCommand.CreateCollectOp(resultType);
			Node node2 = this.CapWithPhysicalProject(node);
			node = this._iqtCommand.CreateNode(collectOp, node2);
			return node;
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x00076B68 File Offset: 0x00074D68
		private Node ConvertPredicateToScalarOpTree(Node node, DbExpression expr)
		{
			CaseOp caseOp = this._iqtCommand.CreateCaseOp(this._iqtCommand.BooleanType);
			bool flag = this.IsNullable(expr);
			List<Node> list = new List<Node>(flag ? 5 : 3);
			list.Add(node);
			list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateInternalConstantOp(this._iqtCommand.BooleanType, true)));
			if (flag)
			{
				Node node2 = this.VisitExpr(expr);
				list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Not), node2));
			}
			list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateInternalConstantOp(this._iqtCommand.BooleanType, false)));
			if (flag)
			{
				list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateNullOp(this._iqtCommand.BooleanType)));
			}
			node = this._iqtCommand.CreateNode(caseOp, list);
			return node;
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x00076C64 File Offset: 0x00074E64
		private bool IsNullable(DbExpression expression)
		{
			DbExpressionKind expressionKind = expression.ExpressionKind;
			if (expressionKind <= DbExpressionKind.IsNull)
			{
				switch (expressionKind)
				{
				case DbExpressionKind.All:
				case DbExpressionKind.Any:
					break;
				case DbExpressionKind.And:
					goto IL_0047;
				default:
					if (expressionKind - DbExpressionKind.IsEmpty > 1)
					{
						return true;
					}
					break;
				}
				return false;
			}
			if (expressionKind == DbExpressionKind.Not)
			{
				return this.IsNullable(((DbNotExpression)expression).Argument);
			}
			if (expressionKind != DbExpressionKind.Or)
			{
				return true;
			}
			IL_0047:
			DbBinaryExpression dbBinaryExpression = (DbBinaryExpression)expression;
			return this.IsNullable(dbBinaryExpression.Left) || this.IsNullable(dbBinaryExpression.Right);
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x00076CE0 File Offset: 0x00074EE0
		private Node VisitExprAsPredicate(DbExpression expr)
		{
			if (expr == null)
			{
				return null;
			}
			Node node = this.VisitExpr(expr);
			if (!this.IsPredicate(expr))
			{
				ComparisonOp comparisonOp = this._iqtCommand.CreateComparisonOp(OpType.EQ, false);
				Node node2 = this._iqtCommand.CreateNode(this._iqtCommand.CreateInternalConstantOp(this._iqtCommand.BooleanType, true));
				node = this._iqtCommand.CreateNode(comparisonOp, node, node2);
			}
			else
			{
				PlanCompiler.Assert(!node.Op.IsRelOp, "unexpected relOp as predicate?");
			}
			return node;
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x00076D64 File Offset: 0x00074F64
		private static IList<Node> VisitExpr(IList<DbExpression> exprs, ITreeGenerator.VisitExprDelegate exprDelegate)
		{
			List<Node> list = new List<Node>();
			for (int i = 0; i < exprs.Count; i++)
			{
				list.Add(exprDelegate(exprs[i]));
			}
			return list;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x00076D9C File Offset: 0x00074F9C
		private IList<Node> VisitExprAsScalar(IList<DbExpression> exprs)
		{
			return ITreeGenerator.VisitExpr(exprs, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x00076DB0 File Offset: 0x00074FB0
		private Node VisitUnary(DbUnaryExpression e, Op op, ITreeGenerator.VisitExprDelegate exprDelegate)
		{
			return this._iqtCommand.CreateNode(op, exprDelegate(e.Argument));
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x00076DCA File Offset: 0x00074FCA
		private Node VisitBinary(DbBinaryExpression e, Op op, ITreeGenerator.VisitExprDelegate exprDelegate)
		{
			return this._iqtCommand.CreateNode(op, exprDelegate(e.Left), exprDelegate(e.Right));
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x00076DF0 File Offset: 0x00074FF0
		private Node EnsureRelOp(Node inputNode)
		{
			Op op = inputNode.Op;
			if (op.IsRelOp)
			{
				return inputNode;
			}
			ScalarOp scalarOp = op as ScalarOp;
			PlanCompiler.Assert(scalarOp != null, "An expression in a CQT produced a non-ScalarOp and non-RelOp output Op");
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(scalarOp.Type), "An expression used as a RelOp argument was neither a RelOp or a collection");
			if (op is CollectOp)
			{
				PlanCompiler.Assert(inputNode.HasChild0, "CollectOp without argument");
				if (inputNode.Child0.Op is PhysicalProjectOp)
				{
					PlanCompiler.Assert(inputNode.Child0.HasChild0, "PhysicalProjectOp without argument");
					PlanCompiler.Assert(inputNode.Child0.Child0.Op.IsRelOp, "PhysicalProjectOp applied to non-RelOp input");
					return inputNode.Child0.Child0;
				}
			}
			Var var;
			Node node = this._iqtCommand.CreateVarDefNode(inputNode, out var);
			UnnestOp unnestOp = this._iqtCommand.CreateUnnestOp(var);
			PlanCompiler.Assert(unnestOp.Table.Columns.Count == 1, "Un-nest of collection ScalarOp produced unexpected number of columns (1 expected)");
			Node node2 = this._iqtCommand.CreateNode(unnestOp, node);
			this._varMap[node2] = unnestOp.Table.Columns[0];
			Node node3 = this._iqtCommand.CreateNode(this._iqtCommand.CreateVarRefOp(unnestOp.Table.Columns[0]));
			Var var2;
			Node node4 = this._iqtCommand.CreateVarDefListNode(node3, out var2);
			ProjectOp projectOp = this._iqtCommand.CreateProjectOp(var2);
			Node node5 = this._iqtCommand.CreateNode(projectOp, node2, node4);
			this._varMap[node5] = var2;
			return node5;
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x00076F74 File Offset: 0x00075174
		private Node CapWithProject(Node input)
		{
			PlanCompiler.Assert(input.Op.IsRelOp, "unexpected non-RelOp?");
			if (input.Op.OpType == OpType.Project)
			{
				return input;
			}
			Var var = this._varMap[input];
			ProjectOp projectOp = this._iqtCommand.CreateProjectOp(var);
			Node node = this._iqtCommand.CreateNode(projectOp, input, this._iqtCommand.CreateNode(this._iqtCommand.CreateVarDefListOp()));
			this._varMap[node] = var;
			return node;
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x00076FF4 File Offset: 0x000751F4
		private Node CapWithPhysicalProject(Node input)
		{
			PlanCompiler.Assert(input.Op.IsRelOp, "unexpected non-RelOp?");
			Var var = this._varMap[input];
			PhysicalProjectOp physicalProjectOp = this._iqtCommand.CreatePhysicalProjectOp(var);
			return this._iqtCommand.CreateNode(physicalProjectOp, input);
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x0007703D File Offset: 0x0007523D
		private Node EnterExpressionBinding(DbExpressionBinding binding)
		{
			return this.VisitBoundExpressionPushBindingScope(binding.Expression, binding.VariableName);
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x00077051 File Offset: 0x00075251
		private Node EnterGroupExpressionBinding(DbGroupExpressionBinding binding)
		{
			return this.VisitBoundExpressionPushBindingScope(binding.Expression, binding.VariableName);
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x00077068 File Offset: 0x00075268
		private Node VisitBoundExpressionPushBindingScope(DbExpression boundExpression, string bindingName)
		{
			Var var;
			Node node = this.VisitBoundExpression(boundExpression, out var);
			this.PushBindingScope(var, bindingName);
			return node;
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x00077088 File Offset: 0x00075288
		private Node VisitBoundExpression(DbExpression boundExpression, out Var boundVar)
		{
			Node node = this.VisitExpr(boundExpression);
			PlanCompiler.Assert(node != null, "DbExpressionBinding.Expression produced null conversion");
			node = this.EnsureRelOp(node);
			boundVar = this._varMap[node];
			PlanCompiler.Assert(boundVar != null, "No Var found for Input Op");
			return node;
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000770D1 File Offset: 0x000752D1
		private void PushBindingScope(Var boundVar, string bindingName)
		{
			this._varScopes.Push(new ITreeGenerator.ExpressionBindingScope(this._iqtCommand, bindingName, boundVar));
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000770EB File Offset: 0x000752EB
		private ITreeGenerator.ExpressionBindingScope ExitExpressionBinding()
		{
			ITreeGenerator.ExpressionBindingScope expressionBindingScope = this._varScopes.Pop() as ITreeGenerator.ExpressionBindingScope;
			PlanCompiler.Assert(expressionBindingScope != null, "ExitExpressionBinding called without ExpressionBindingScope on top of scope stack");
			return expressionBindingScope;
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x0007710B File Offset: 0x0007530B
		private void ExitGroupExpressionBinding()
		{
			PlanCompiler.Assert(this._varScopes.Pop() is ITreeGenerator.ExpressionBindingScope, "ExitGroupExpressionBinding called without ExpressionBindingScope on top of scope stack");
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x0007712C File Offset: 0x0007532C
		private void EnterLambdaFunction(DbLambda lambda, List<Tuple<Node, bool>> argumentValues, EdmFunction expandingEdmFunction)
		{
			IList<DbVariableReferenceExpression> variables = lambda.Variables;
			Dictionary<string, Tuple<Node, bool>> dictionary = new Dictionary<string, Tuple<Node, bool>>();
			int num = 0;
			foreach (Tuple<Node, bool> tuple in argumentValues)
			{
				dictionary.Add(variables[num].VariableName, tuple);
				num++;
			}
			if (expandingEdmFunction != null)
			{
				if (this._functionExpansions.Contains(expandingEdmFunction))
				{
					throw new EntityCommandCompilationException(Strings.Cqt_UDF_FunctionDefinitionWithCircularReference(expandingEdmFunction.FullName), null);
				}
				this._functionExpansions.Push(expandingEdmFunction);
			}
			this._varScopes.Push(new ITreeGenerator.LambdaScope(this, this._iqtCommand, dictionary));
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000771E4 File Offset: 0x000753E4
		private ITreeGenerator.LambdaScope ExitLambdaFunction(EdmFunction expandingEdmFunction)
		{
			ITreeGenerator.LambdaScope lambdaScope = this._varScopes.Pop() as ITreeGenerator.LambdaScope;
			PlanCompiler.Assert(lambdaScope != null, "ExitLambdaFunction called without LambdaScope on top of scope stack");
			if (expandingEdmFunction != null)
			{
				PlanCompiler.Assert(this._functionExpansions.Pop() == expandingEdmFunction, "Function expansion stack corruption: unexpected function at the top of the stack");
			}
			return lambdaScope;
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x00077220 File Offset: 0x00075420
		private Node ProjectNewRecord(Node inputNode, RowType recType, IEnumerable<Var> colVars)
		{
			List<Node> list = new List<Node>();
			foreach (Var var in colVars)
			{
				list.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateVarRefOp(var)));
			}
			Node node = this._iqtCommand.CreateNode(this._iqtCommand.CreateNewRecordOp(recType), list);
			Var var2;
			Node node2 = this._iqtCommand.CreateVarDefListNode(node, out var2);
			ProjectOp projectOp = this._iqtCommand.CreateProjectOp(var2);
			Node node3 = this._iqtCommand.CreateNode(projectOp, inputNode, node2);
			this._varMap[node3] = var2;
			return node3;
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000772E4 File Offset: 0x000754E4
		public override Node Visit(DbExpression e)
		{
			Check.NotNull<DbExpression>(e, "e");
			throw new NotSupportedException(Strings.Cqt_General_UnsupportedExpression(e.GetType().FullName));
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x00077308 File Offset: 0x00075508
		public override Node Visit(DbConstantExpression e)
		{
			Check.NotNull<DbConstantExpression>(e, "e");
			ConstantBaseOp constantBaseOp = this._iqtCommand.CreateConstantOp(e.ResultType, e.GetValue());
			return this._iqtCommand.CreateNode(constantBaseOp);
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x00077348 File Offset: 0x00075548
		public override Node Visit(DbNullExpression e)
		{
			Check.NotNull<DbNullExpression>(e, "e");
			NullOp nullOp = this._iqtCommand.CreateNullOp(e.ResultType);
			return this._iqtCommand.CreateNode(nullOp);
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x0007737F File Offset: 0x0007557F
		public override Node Visit(DbVariableReferenceExpression e)
		{
			Check.NotNull<DbVariableReferenceExpression>(e, "e");
			return this.ResolveScope(e)[e.VariableName];
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000773A0 File Offset: 0x000755A0
		private ITreeGenerator.CqtVariableScope ResolveScope(DbVariableReferenceExpression e)
		{
			foreach (ITreeGenerator.CqtVariableScope cqtVariableScope in this._varScopes)
			{
				if (cqtVariableScope.Contains(e.VariableName))
				{
					return cqtVariableScope;
				}
			}
			PlanCompiler.Assert(false, "CQT VarRef could not be resolved in the variable scope stack");
			return null;
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x0007740C File Offset: 0x0007560C
		public override Node Visit(DbParameterReferenceExpression e)
		{
			Check.NotNull<DbParameterReferenceExpression>(e, "e");
			Op op = this._iqtCommand.CreateVarRefOp(this._iqtCommand.GetParameter(e.ParameterName));
			return this._iqtCommand.CreateNode(op);
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x00077450 File Offset: 0x00075650
		public override Node Visit(DbFunctionExpression e)
		{
			Check.NotNull<DbFunctionExpression>(e, "e");
			Node node;
			if (e.Function.IsModelDefinedFunction)
			{
				DbLambda generatedFunctionDefinition;
				try
				{
					generatedFunctionDefinition = this._iqtCommand.MetadataWorkspace.GetGeneratedFunctionDefinition(e.Function);
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						throw new EntityCommandCompilationException(Strings.Cqt_UDF_FunctionDefinitionGenerationFailed(e.Function.FullName), ex);
					}
					throw;
				}
				node = this.VisitLambdaExpression(generatedFunctionDefinition, e.Arguments, e, e.Function);
			}
			else
			{
				List<Node> list = new List<Node>(e.Arguments.Count);
				for (int i = 0; i < e.Arguments.Count; i++)
				{
					list.Add(this.BuildSoftCast(this.VisitExprAsScalar(e.Arguments[i]), e.Function.Parameters[i].TypeUsage));
				}
				node = this._iqtCommand.CreateNode(this._iqtCommand.CreateFunctionOp(e.Function), list);
			}
			return node;
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x0007755C File Offset: 0x0007575C
		public override Node Visit(DbLambdaExpression e)
		{
			Check.NotNull<DbLambdaExpression>(e, "e");
			return this.VisitLambdaExpression(e.Lambda, e.Arguments, e, null);
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x00077580 File Offset: 0x00075780
		private Node VisitLambdaExpression(DbLambda lambda, IList<DbExpression> arguments, DbExpression applicationExpr, EdmFunction expandingEdmFunction)
		{
			List<Tuple<Node, bool>> list = new List<Tuple<Node, bool>>(arguments.Count);
			foreach (DbExpression dbExpression in arguments)
			{
				list.Add(Tuple.Create<Node, bool>(this.VisitExpr(dbExpression), this.IsPredicate(dbExpression)));
			}
			this.EnterLambdaFunction(lambda, list, expandingEdmFunction);
			Node node = this.VisitExpr(lambda.Body);
			this._functionsIsPredicateFlag[applicationExpr] = this.IsPredicate(lambda.Body);
			this.ExitLambdaFunction(expandingEdmFunction);
			return node;
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x00077620 File Offset: 0x00075820
		private Node BuildSoftCast(Node node, TypeUsage targetType)
		{
			if (node.Op.IsRelOp)
			{
				targetType = TypeHelpers.GetEdmType<CollectionType>(targetType).TypeUsage;
				Var var = this._varMap[node];
				if (Command.EqualTypes(targetType, var.Type))
				{
					return node;
				}
				Node node2 = this._iqtCommand.CreateNode(this._iqtCommand.CreateVarRefOp(var));
				Node node3 = this._iqtCommand.CreateNode(this._iqtCommand.CreateSoftCastOp(targetType), node2);
				Var var2;
				Node node4 = this._iqtCommand.CreateVarDefListNode(node3, out var2);
				ProjectOp projectOp = this._iqtCommand.CreateProjectOp(var2);
				Node node5 = this._iqtCommand.CreateNode(projectOp, node, node4);
				this._varMap[node5] = var2;
				return node5;
			}
			else
			{
				PlanCompiler.Assert(node.Op.IsScalarOp, "I want a scalar op");
				if (Command.EqualTypes(node.Op.Type, targetType))
				{
					return node;
				}
				SoftCastOp softCastOp = this._iqtCommand.CreateSoftCastOp(targetType);
				return this._iqtCommand.CreateNode(softCastOp, node);
			}
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x0007771F File Offset: 0x0007591F
		private Node BuildSoftCast(Node node, EdmType targetType)
		{
			return this.BuildSoftCast(node, TypeUsage.Create(targetType));
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x00077730 File Offset: 0x00075930
		private Node BuildEntityRef(Node arg, TypeUsage entityType)
		{
			TypeUsage typeUsage = TypeHelpers.CreateReferenceTypeUsage((EntityType)entityType.EdmType);
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateGetEntityRefOp(typeUsage), arg);
		}

		// Token: 0x06002813 RID: 10259 RVA: 0x00077768 File Offset: 0x00075968
		private static bool TryRewriteKeyPropertyAccess(DbPropertyExpression propertyExpression, out DbExpression rewritten)
		{
			if (propertyExpression.Instance.ExpressionKind == DbExpressionKind.Property && Helper.IsEntityType(propertyExpression.Instance.ResultType.EdmType))
			{
				EntityType entityType = (EntityType)propertyExpression.Instance.ResultType.EdmType;
				DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)propertyExpression.Instance;
				if (Helper.IsNavigationProperty(dbPropertyExpression.Property) && entityType.KeyMembers.Contains(propertyExpression.Property))
				{
					NavigationProperty navigationProperty = (NavigationProperty)dbPropertyExpression.Property;
					DbExpression dbExpression = dbPropertyExpression.Instance.GetEntityRef().Navigate(navigationProperty.FromEndMember, navigationProperty.ToEndMember);
					rewritten = dbExpression.GetRefKey();
					rewritten = rewritten.Property(propertyExpression.Property.Name);
					return true;
				}
			}
			rewritten = null;
			return false;
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x00077830 File Offset: 0x00075A30
		public override Node Visit(DbPropertyExpression e)
		{
			Check.NotNull<DbPropertyExpression>(e, "e");
			if (BuiltInTypeKind.EdmProperty != e.Property.BuiltInTypeKind && e.Property.BuiltInTypeKind != BuiltInTypeKind.AssociationEndMember && BuiltInTypeKind.NavigationProperty != e.Property.BuiltInTypeKind)
			{
				throw new NotSupportedException();
			}
			PlanCompiler.Assert(e.Instance != null, "Static properties are not supported");
			DbExpression dbExpression;
			Node node;
			if (ITreeGenerator.TryRewriteKeyPropertyAccess(e, out dbExpression))
			{
				node = this.VisitExpr(dbExpression);
			}
			else
			{
				Node node2 = this.VisitExpr(e.Instance);
				if (e.Instance.ExpressionKind == DbExpressionKind.NewInstance && Helper.IsStructuralType(e.Instance.ResultType.EdmType))
				{
					IList allStructuralMembers = Helper.GetAllStructuralMembers(e.Instance.ResultType.EdmType);
					int num = -1;
					for (int i = 0; i < allStructuralMembers.Count; i++)
					{
						if (string.Equals(e.Property.Name, ((EdmMember)allStructuralMembers[i]).Name, StringComparison.Ordinal))
						{
							num = i;
							break;
						}
					}
					PlanCompiler.Assert(num > -1, "The specified property was not found");
					node = node2.Children[num];
					node = this.BuildSoftCast(node, e.ResultType);
				}
				else
				{
					Op op = this._iqtCommand.CreatePropertyOp(e.Property);
					node2 = this.BuildSoftCast(node2, e.Property.DeclaringType);
					node = this._iqtCommand.CreateNode(op, node2);
				}
			}
			return node;
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x0007799C File Offset: 0x00075B9C
		public override Node Visit(DbComparisonExpression e)
		{
			Check.NotNull<DbComparisonExpression>(e, "e");
			Op op = this._iqtCommand.CreateComparisonOp(ITreeGenerator._opMap[e.ExpressionKind], false);
			Node node = this.VisitExprAsScalar(e.Left);
			Node node2 = this.VisitExprAsScalar(e.Right);
			TypeUsage commonTypeUsage = TypeHelpers.GetCommonTypeUsage(e.Left.ResultType, e.Right.ResultType);
			if (!Command.EqualTypes(e.Left.ResultType, e.Right.ResultType))
			{
				node = this.BuildSoftCast(node, commonTypeUsage);
				node2 = this.BuildSoftCast(node2, commonTypeUsage);
			}
			if (TypeSemantics.IsEntityType(commonTypeUsage) && (e.ExpressionKind == DbExpressionKind.Equals || e.ExpressionKind == DbExpressionKind.NotEquals))
			{
				node = this.BuildEntityRef(node, commonTypeUsage);
				node2 = this.BuildEntityRef(node2, commonTypeUsage);
			}
			return this._iqtCommand.CreateNode(op, node, node2);
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x00077A74 File Offset: 0x00075C74
		public override Node Visit(DbLikeExpression e)
		{
			Check.NotNull<DbLikeExpression>(e, "e");
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateLikeOp(), this.VisitExpr(e.Argument), this.VisitExpr(e.Pattern), this.VisitExpr(e.Escape));
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x00077AC8 File Offset: 0x00075CC8
		private Node CreateLimitNode(Node inputNode, Node limitNode, bool withTies)
		{
			Node node;
			if (OpType.ConstrainedSort == inputNode.Op.OpType && OpType.Null == inputNode.Child2.Op.OpType)
			{
				inputNode.Child2 = limitNode;
				if (withTies)
				{
					((ConstrainedSortOp)inputNode.Op).WithTies = true;
				}
				node = inputNode;
			}
			else if (OpType.Sort == inputNode.Op.OpType)
			{
				node = this._iqtCommand.CreateNode(this._iqtCommand.CreateConstrainedSortOp(((SortOp)inputNode.Op).Keys, withTies), inputNode.Child0, this._iqtCommand.CreateNode(this._iqtCommand.CreateNullOp(this._iqtCommand.IntegerType)), limitNode);
			}
			else
			{
				node = this._iqtCommand.CreateNode(this._iqtCommand.CreateConstrainedSortOp(new List<SortKey>(), withTies), inputNode, this._iqtCommand.CreateNode(this._iqtCommand.CreateNullOp(this._iqtCommand.IntegerType)), limitNode);
			}
			return node;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x00077BC0 File Offset: 0x00075DC0
		public override Node Visit(DbLimitExpression expression)
		{
			Check.NotNull<DbLimitExpression>(expression, "expression");
			Node node = this.EnsureRelOp(this.VisitExpr(expression.Argument));
			Var var = this._varMap[node];
			Node node2 = this.VisitExprAsScalar(expression.Limit);
			Node node3;
			if (OpType.Project == node.Op.OpType && (node.Child0.Op.OpType == OpType.Sort || node.Child0.Op.OpType == OpType.ConstrainedSort))
			{
				node.Child0 = this.CreateLimitNode(node.Child0, node2, expression.WithTies);
				node3 = node;
			}
			else
			{
				node3 = this.CreateLimitNode(node, node2, expression.WithTies);
			}
			if (node3 != node)
			{
				this._varMap[node3] = var;
			}
			return node3;
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x00077C7C File Offset: 0x00075E7C
		public override Node Visit(DbIsNullExpression e)
		{
			Check.NotNull<DbIsNullExpression>(e, "e");
			bool flag = false;
			if (e.Argument.ExpressionKind == DbExpressionKind.IsNull)
			{
				flag = true;
			}
			else if (e.Argument.ExpressionKind == DbExpressionKind.Not && ((DbNotExpression)e.Argument).Argument.ExpressionKind == DbExpressionKind.IsNull)
			{
				flag = true;
			}
			Op op = this._iqtCommand.CreateConditionalOp(OpType.IsNull);
			if (flag)
			{
				return this._iqtCommand.CreateNode(op, this._iqtCommand.CreateNode(this._iqtCommand.CreateInternalConstantOp(this._iqtCommand.BooleanType, true)));
			}
			Node node = this.VisitExprAsScalar(e.Argument);
			if (TypeSemantics.IsEntityType(e.Argument.ResultType))
			{
				node = this.BuildEntityRef(node, e.Argument.ResultType);
			}
			return this._iqtCommand.CreateNode(op, node);
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x00077D58 File Offset: 0x00075F58
		public override Node Visit(DbArithmeticExpression e)
		{
			Check.NotNull<DbArithmeticExpression>(e, "e");
			Op op = this._iqtCommand.CreateArithmeticOp(ITreeGenerator._opMap[e.ExpressionKind], e.ResultType);
			List<Node> list = new List<Node>();
			foreach (DbExpression dbExpression in e.Arguments)
			{
				Node node = this.VisitExprAsScalar(dbExpression);
				list.Add(this.BuildSoftCast(node, e.ResultType));
			}
			return this._iqtCommand.CreateNode(op, list);
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x00077DFC File Offset: 0x00075FFC
		public override Node Visit(DbAndExpression e)
		{
			Check.NotNull<DbAndExpression>(e, "e");
			Op op = this._iqtCommand.CreateConditionalOp(OpType.And);
			return this.VisitBinary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsPredicate));
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x00077E38 File Offset: 0x00076038
		public override Node Visit(DbOrExpression e)
		{
			Check.NotNull<DbOrExpression>(e, "e");
			Op op = this._iqtCommand.CreateConditionalOp(OpType.Or);
			return this.VisitBinary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsPredicate));
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x00077E74 File Offset: 0x00076074
		public override Node Visit(DbInExpression e)
		{
			Check.NotNull<DbInExpression>(e, "e");
			Op op = this._iqtCommand.CreateConditionalOp(OpType.In);
			List<Node> list = new List<Node>(1 + e.List.Count) { this.VisitExpr(e.Item) };
			list.AddRange(e.List.Select(new Func<DbExpression, Node>(this.VisitExpr)));
			return this._iqtCommand.CreateNode(op, list);
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x00077EEC File Offset: 0x000760EC
		public override Node Visit(DbNotExpression e)
		{
			Check.NotNull<DbNotExpression>(e, "e");
			Op op = this._iqtCommand.CreateConditionalOp(OpType.Not);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsPredicate));
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x00077F28 File Offset: 0x00076128
		public override Node Visit(DbDistinctExpression e)
		{
			Check.NotNull<DbDistinctExpression>(e, "e");
			Node node = this.EnsureRelOp(this.VisitExpr(e.Argument));
			Var var = this._varMap[node];
			Op op = this._iqtCommand.CreateDistinctOp(var);
			Node node2 = this._iqtCommand.CreateNode(op, node);
			this._varMap[node2] = var;
			return node2;
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x00077F8C File Offset: 0x0007618C
		public override Node Visit(DbElementExpression e)
		{
			Check.NotNull<DbElementExpression>(e, "e");
			Op op = this._iqtCommand.CreateElementOp(e.ResultType);
			Node node = this.EnsureRelOp(this.VisitExpr(e.Argument));
			node = this.BuildSoftCast(node, TypeHelpers.CreateCollectionTypeUsage(e.ResultType));
			Var var = this._varMap[node];
			node = this._iqtCommand.CreateNode(this._iqtCommand.CreateSingleRowOp(), node);
			this._varMap[node] = var;
			node = this.CapWithProject(node);
			return this._iqtCommand.CreateNode(op, node);
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x00078024 File Offset: 0x00076224
		public override Node Visit(DbIsEmptyExpression e)
		{
			Check.NotNull<DbIsEmptyExpression>(e, "e");
			Op op = this._iqtCommand.CreateExistsOp();
			Node node = this.EnsureRelOp(this.VisitExpr(e.Argument));
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Not), this._iqtCommand.CreateNode(op, node));
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x00078084 File Offset: 0x00076284
		private Node VisitSetOpExpression(DbBinaryExpression expression)
		{
			PlanCompiler.Assert(DbExpressionKind.Except == expression.ExpressionKind || DbExpressionKind.Intersect == expression.ExpressionKind || DbExpressionKind.UnionAll == expression.ExpressionKind, "Non-SetOp DbExpression used as argument to VisitSetOpExpression");
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(expression.ResultType), "SetOp DbExpression does not have collection result type?");
			Node node = this.EnsureRelOp(this.VisitExpr(expression.Left));
			Node node2 = this.EnsureRelOp(this.VisitExpr(expression.Right));
			node = this.BuildSoftCast(node, expression.ResultType);
			node2 = this.BuildSoftCast(node2, expression.ResultType);
			Var var = this._iqtCommand.CreateSetOpVar(TypeHelpers.GetEdmType<CollectionType>(expression.ResultType).TypeUsage);
			VarMap varMap = new VarMap();
			varMap.Add(var, this._varMap[node]);
			VarMap varMap2 = new VarMap();
			varMap2.Add(var, this._varMap[node2]);
			Op op = null;
			DbExpressionKind expressionKind = expression.ExpressionKind;
			if (expressionKind != DbExpressionKind.Except)
			{
				if (expressionKind != DbExpressionKind.Intersect)
				{
					if (expressionKind == DbExpressionKind.UnionAll)
					{
						op = this._iqtCommand.CreateUnionAllOp(varMap, varMap2);
					}
				}
				else
				{
					op = this._iqtCommand.CreateIntersectOp(varMap, varMap2);
				}
			}
			else
			{
				op = this._iqtCommand.CreateExceptOp(varMap, varMap2);
			}
			Node node3 = this._iqtCommand.CreateNode(op, node, node2);
			this._varMap[node3] = var;
			return node3;
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000781D7 File Offset: 0x000763D7
		public override Node Visit(DbUnionAllExpression e)
		{
			Check.NotNull<DbUnionAllExpression>(e, "e");
			return this.VisitSetOpExpression(e);
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000781EC File Offset: 0x000763EC
		public override Node Visit(DbIntersectExpression e)
		{
			Check.NotNull<DbIntersectExpression>(e, "e");
			return this.VisitSetOpExpression(e);
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x00078201 File Offset: 0x00076401
		public override Node Visit(DbExceptExpression e)
		{
			Check.NotNull<DbExceptExpression>(e, "e");
			return this.VisitSetOpExpression(e);
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x00078218 File Offset: 0x00076418
		public override Node Visit(DbTreatExpression e)
		{
			Check.NotNull<DbTreatExpression>(e, "e");
			Op op;
			if (this._fakeTreats.Contains(e))
			{
				op = this._iqtCommand.CreateFakeTreatOp(e.ResultType);
			}
			else
			{
				op = this._iqtCommand.CreateTreatOp(e.ResultType);
			}
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x0007827C File Offset: 0x0007647C
		public override Node Visit(DbIsOfExpression e)
		{
			Check.NotNull<DbIsOfExpression>(e, "e");
			Op op;
			if (DbExpressionKind.IsOfOnly == e.ExpressionKind)
			{
				op = this._iqtCommand.CreateIsOfOnlyOp(e.OfType);
			}
			else
			{
				op = this._iqtCommand.CreateIsOfOp(e.OfType);
			}
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x000782DC File Offset: 0x000764DC
		public override Node Visit(DbCastExpression e)
		{
			Check.NotNull<DbCastExpression>(e, "e");
			Op op = this._iqtCommand.CreateCastOp(e.ResultType);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x0007831C File Offset: 0x0007651C
		public override Node Visit(DbCaseExpression e)
		{
			Check.NotNull<DbCaseExpression>(e, "e");
			List<Node> list = new List<Node>();
			for (int i = 0; i < e.When.Count; i++)
			{
				list.Add(this.VisitExprAsPredicate(e.When[i]));
				list.Add(this.BuildSoftCast(this.VisitExprAsScalar(e.Then[i]), e.ResultType));
			}
			list.Add(this.BuildSoftCast(this.VisitExprAsScalar(e.Else), e.ResultType));
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateCaseOp(e.ResultType), list);
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000783C8 File Offset: 0x000765C8
		private DbFilterExpression CreateIsOfFilterExpression(DbExpression input, ITreeGenerator.IsOfFilter typeFilter)
		{
			DbExpressionBinding resultBinding = input.Bind();
			DbExpression dbExpression = Helpers.BuildBalancedTreeInPlace<DbExpression>(new List<DbExpression>(typeFilter.ToEnumerable().Select(delegate(KeyValuePair<TypeUsage, bool> tf)
			{
				if (!tf.Value)
				{
					return resultBinding.Variable.IsOf(tf.Key);
				}
				return resultBinding.Variable.IsOfOnly(tf.Key);
			}).ToList<DbIsOfExpression>()), (DbExpression left, DbExpression right) => left.And(right));
			DbFilterExpression dbFilterExpression = resultBinding.Filter(dbExpression);
			this._processedIsOfFilters.Add(dbFilterExpression);
			return dbFilterExpression;
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x00078448 File Offset: 0x00076648
		private static bool IsIsOfFilter(DbFilterExpression filter)
		{
			if (filter.Predicate.ExpressionKind != DbExpressionKind.IsOf && filter.Predicate.ExpressionKind != DbExpressionKind.IsOfOnly)
			{
				return false;
			}
			DbExpression argument = ((DbIsOfExpression)filter.Predicate).Argument;
			return argument.ExpressionKind == DbExpressionKind.VariableReference && ((DbVariableReferenceExpression)argument).VariableName == filter.Input.VariableName;
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x000784B0 File Offset: 0x000766B0
		private DbExpression ApplyIsOfFilter(DbExpression current, ITreeGenerator.IsOfFilter typeFilter)
		{
			DbExpressionKind expressionKind = current.ExpressionKind;
			if (expressionKind <= DbExpressionKind.Filter)
			{
				if (expressionKind == DbExpressionKind.Distinct)
				{
					return this.ApplyIsOfFilter(((DbDistinctExpression)current).Argument, typeFilter).Distinct();
				}
				if (expressionKind == DbExpressionKind.Filter)
				{
					DbFilterExpression dbFilterExpression = (DbFilterExpression)current;
					if (ITreeGenerator.IsIsOfFilter(dbFilterExpression))
					{
						DbIsOfExpression dbIsOfExpression = (DbIsOfExpression)dbFilterExpression.Predicate;
						typeFilter = typeFilter.Merge(dbIsOfExpression);
						return this.ApplyIsOfFilter(dbFilterExpression.Input.Expression, typeFilter);
					}
					return this.ApplyIsOfFilter(dbFilterExpression.Input.Expression, typeFilter).BindAs(dbFilterExpression.Input.VariableName).Filter(dbFilterExpression.Predicate);
				}
			}
			else
			{
				if (expressionKind - DbExpressionKind.OfType <= 1)
				{
					DbOfTypeExpression dbOfTypeExpression = (DbOfTypeExpression)current;
					typeFilter = typeFilter.Merge(dbOfTypeExpression);
					DbExpressionBinding dbExpressionBinding = this.ApplyIsOfFilter(dbOfTypeExpression.Argument, typeFilter).Bind();
					DbTreatExpression dbTreatExpression = dbExpressionBinding.Variable.TreatAs(dbOfTypeExpression.OfType);
					this._fakeTreats.Add(dbTreatExpression);
					return dbExpressionBinding.Project(dbTreatExpression);
				}
				if (expressionKind != DbExpressionKind.Project)
				{
					if (expressionKind == DbExpressionKind.Sort)
					{
						DbSortExpression dbSortExpression = (DbSortExpression)current;
						return this.ApplyIsOfFilter(dbSortExpression.Input.Expression, typeFilter).BindAs(dbSortExpression.Input.VariableName).Sort(dbSortExpression.SortOrder);
					}
				}
				else
				{
					DbProjectExpression dbProjectExpression = (DbProjectExpression)current;
					if (dbProjectExpression.Projection.ExpressionKind == DbExpressionKind.VariableReference && ((DbVariableReferenceExpression)dbProjectExpression.Projection).VariableName == dbProjectExpression.Input.VariableName)
					{
						return this.ApplyIsOfFilter(dbProjectExpression.Input.Expression, typeFilter);
					}
					return this.CreateIsOfFilterExpression(current, typeFilter);
				}
			}
			return this.CreateIsOfFilterExpression(current, typeFilter);
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x0007867C File Offset: 0x0007687C
		public override Node Visit(DbOfTypeExpression e)
		{
			Check.NotNull<DbOfTypeExpression>(e, "e");
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(e.Argument.ResultType), "Non-Collection Type Argument in DbOfTypeExpression");
			DbExpression dbExpression = this.ApplyIsOfFilter(e.Argument, new ITreeGenerator.IsOfFilter(e));
			Node node = this.EnsureRelOp(this.VisitExpr(dbExpression));
			Var var = this._varMap[node];
			Var var2;
			Node node2 = this._iqtCommand.BuildFakeTreatProject(node, var, e.OfType, out var2);
			this._varMap[node2] = var2;
			return node2;
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x00078704 File Offset: 0x00076904
		public override Node Visit(DbNewInstanceExpression e)
		{
			Check.NotNull<DbNewInstanceExpression>(e, "e");
			Op op = null;
			List<Node> list = null;
			if (TypeSemantics.IsCollectionType(e.ResultType))
			{
				op = this._iqtCommand.CreateNewMultisetOp(e.ResultType);
			}
			else if (TypeSemantics.IsRowType(e.ResultType))
			{
				op = this._iqtCommand.CreateNewRecordOp(e.ResultType);
			}
			else if (TypeSemantics.IsEntityType(e.ResultType))
			{
				List<RelProperty> list2 = new List<RelProperty>();
				list = new List<Node>();
				if (e.HasRelatedEntityReferences)
				{
					foreach (DbRelatedEntityRef dbRelatedEntityRef in e.RelatedEntityReferences)
					{
						RelProperty relProperty = new RelProperty((RelationshipType)dbRelatedEntityRef.TargetEnd.DeclaringType, dbRelatedEntityRef.SourceEnd, dbRelatedEntityRef.TargetEnd);
						list2.Add(relProperty);
						Node node = this.VisitExprAsScalar(dbRelatedEntityRef.TargetEntityReference);
						list.Add(node);
					}
				}
				op = this._iqtCommand.CreateNewEntityOp(e.ResultType, list2);
			}
			else
			{
				op = this._iqtCommand.CreateNewInstanceOp(e.ResultType);
			}
			List<Node> list3 = new List<Node>();
			if (TypeSemantics.IsStructuralType(e.ResultType))
			{
				EdmType edmType = TypeHelpers.GetEdmType<StructuralType>(e.ResultType);
				int num = 0;
				using (IEnumerator enumerator2 = TypeHelpers.GetAllStructuralMembers(edmType).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						object obj = enumerator2.Current;
						EdmMember edmMember = (EdmMember)obj;
						Node node2 = this.BuildSoftCast(this.VisitExprAsScalar(e.Arguments[num]), Helper.GetModelTypeUsage(edmMember));
						list3.Add(node2);
						num++;
					}
					goto IL_01FF;
				}
			}
			TypeUsage typeUsage = TypeHelpers.GetEdmType<CollectionType>(e.ResultType).TypeUsage;
			foreach (DbExpression dbExpression in e.Arguments)
			{
				Node node3 = this.BuildSoftCast(this.VisitExprAsScalar(dbExpression), typeUsage);
				list3.Add(node3);
			}
			IL_01FF:
			if (list != null)
			{
				list3.AddRange(list);
			}
			return this._iqtCommand.CreateNode(op, list3);
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x00078950 File Offset: 0x00076B50
		public override Node Visit(DbRefExpression e)
		{
			Check.NotNull<DbRefExpression>(e, "e");
			Op op = this._iqtCommand.CreateRefOp(e.EntitySet, e.ResultType);
			Node node = this.BuildSoftCast(this.VisitExprAsScalar(e.Argument), TypeHelpers.CreateKeyRowType(e.EntitySet.ElementType));
			return this._iqtCommand.CreateNode(op, node);
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x000789B4 File Offset: 0x00076BB4
		public override Node Visit(DbRelationshipNavigationExpression e)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(e, "e");
			RelProperty relProperty = new RelProperty(e.Relationship, e.NavigateFrom, e.NavigateTo);
			Op op = this._iqtCommand.CreateNavigateOp(e.ResultType, relProperty);
			Node node = this.VisitExprAsScalar(e.NavigationSource);
			return this._iqtCommand.CreateNode(op, node);
		}

		// Token: 0x06002831 RID: 10289 RVA: 0x00078A14 File Offset: 0x00076C14
		public override Node Visit(DbDerefExpression e)
		{
			Check.NotNull<DbDerefExpression>(e, "e");
			Op op = this._iqtCommand.CreateDerefOp(e.ResultType);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x00078A54 File Offset: 0x00076C54
		public override Node Visit(DbRefKeyExpression e)
		{
			Check.NotNull<DbRefKeyExpression>(e, "e");
			Op op = this._iqtCommand.CreateGetRefKeyOp(e.ResultType);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x00078A94 File Offset: 0x00076C94
		public override Node Visit(DbEntityRefExpression e)
		{
			Check.NotNull<DbEntityRefExpression>(e, "e");
			Op op = this._iqtCommand.CreateGetEntityRefOp(e.ResultType);
			return this.VisitUnary(e, op, new ITreeGenerator.VisitExprDelegate(this.VisitExprAsScalar));
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x00078AD4 File Offset: 0x00076CD4
		public override Node Visit(DbScanExpression e)
		{
			Check.NotNull<DbScanExpression>(e, "e");
			TableMD tableMD = Command.CreateTableDefinition(e.Target);
			ScanTableOp scanTableOp = this._iqtCommand.CreateScanTableOp(tableMD);
			Node node = this._iqtCommand.CreateNode(scanTableOp);
			Var var = scanTableOp.Table.Columns[0];
			this._varMap[node] = var;
			return node;
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x00078B34 File Offset: 0x00076D34
		public override Node Visit(DbFilterExpression e)
		{
			Check.NotNull<DbFilterExpression>(e, "e");
			if (!ITreeGenerator.IsIsOfFilter(e) || this._processedIsOfFilters.Contains(e))
			{
				Node node = this.EnterExpressionBinding(e.Input);
				Node node2 = this.VisitExprAsPredicate(e.Predicate);
				this.ExitExpressionBinding();
				Op op = this._iqtCommand.CreateFilterOp();
				Node node3 = this._iqtCommand.CreateNode(op, node, node2);
				this._varMap[node3] = this._varMap[node];
				return node3;
			}
			DbIsOfExpression dbIsOfExpression = (DbIsOfExpression)e.Predicate;
			DbExpression dbExpression = this.ApplyIsOfFilter(e.Input.Expression, new ITreeGenerator.IsOfFilter(dbIsOfExpression));
			return this.VisitExpr(dbExpression);
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x00078BE8 File Offset: 0x00076DE8
		public override Node Visit(DbProjectExpression e)
		{
			Check.NotNull<DbProjectExpression>(e, "e");
			if (e == this._discriminatedViewTopProject)
			{
				return this.GenerateDiscriminatedProject(e);
			}
			return this.GenerateStandardProject(e);
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x00078C10 File Offset: 0x00076E10
		private Node GenerateDiscriminatedProject(DbProjectExpression e)
		{
			PlanCompiler.Assert(this._discriminatedViewTopProject != null, "if a project matches the pattern, there must be a corresponding discriminator map");
			Node node = this.EnterExpressionBinding(e.Input);
			List<RelProperty> list = new List<RelProperty>();
			List<Node> list2 = new List<Node>();
			foreach (KeyValuePair<RelProperty, DbExpression> keyValuePair in this._discriminatorMap.RelPropertyMap)
			{
				list.Add(keyValuePair.Key);
				list2.Add(this.VisitExprAsScalar(keyValuePair.Value));
			}
			DiscriminatedNewEntityOp discriminatedNewEntityOp = this._iqtCommand.CreateDiscriminatedNewEntityOp(e.Projection.ResultType, new ExplicitDiscriminatorMap(this._discriminatorMap), this._discriminatorMap.EntitySet, list);
			List<Node> list3 = new List<Node>(this._discriminatorMap.PropertyMap.Count + 1);
			list3.Add(this.CreateNewInstanceArgument(this._discriminatorMap.Discriminator.Property, this._discriminatorMap.Discriminator));
			foreach (KeyValuePair<EdmProperty, DbExpression> keyValuePair2 in this._discriminatorMap.PropertyMap)
			{
				DbExpression value = keyValuePair2.Value;
				EdmProperty key = keyValuePair2.Key;
				Node node2 = this.CreateNewInstanceArgument(key, value);
				list3.Add(node2);
			}
			list3.AddRange(list2);
			Node node3 = this._iqtCommand.CreateNode(discriminatedNewEntityOp, list3);
			this.ExitExpressionBinding();
			Var var;
			Node node4 = this._iqtCommand.CreateVarDefListNode(node3, out var);
			ProjectOp projectOp = this._iqtCommand.CreateProjectOp(var);
			Node node5 = this._iqtCommand.CreateNode(projectOp, node, node4);
			this._varMap[node5] = var;
			return node5;
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x00078DE8 File Offset: 0x00076FE8
		private Node CreateNewInstanceArgument(EdmMember property, DbExpression value)
		{
			return this.BuildSoftCast(this.VisitExprAsScalar(value), Helper.GetModelTypeUsage(property));
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x00078E00 File Offset: 0x00077000
		private Node GenerateStandardProject(DbProjectExpression e)
		{
			Node node = this.EnterExpressionBinding(e.Input);
			Node node2 = this.VisitExprAsScalar(e.Projection);
			this.ExitExpressionBinding();
			Var var;
			Node node3 = this._iqtCommand.CreateVarDefListNode(node2, out var);
			ProjectOp projectOp = this._iqtCommand.CreateProjectOp(var);
			Node node4 = this._iqtCommand.CreateNode(projectOp, node, node3);
			this._varMap[node4] = var;
			return node4;
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x00078E6C File Offset: 0x0007706C
		public override Node Visit(DbCrossJoinExpression e)
		{
			Check.NotNull<DbCrossJoinExpression>(e, "e");
			return this.VisitJoin(e, e.Inputs, null);
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x00078E88 File Offset: 0x00077088
		public override Node Visit(DbJoinExpression e)
		{
			Check.NotNull<DbJoinExpression>(e, "e");
			return this.VisitJoin(e, new List<DbExpressionBinding> { e.Left, e.Right }, e.JoinCondition);
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x00078ED0 File Offset: 0x000770D0
		private Node VisitJoin(DbExpression e, IList<DbExpressionBinding> inputs, DbExpression joinCond)
		{
			PlanCompiler.Assert(DbExpressionKind.CrossJoin == e.ExpressionKind || DbExpressionKind.InnerJoin == e.ExpressionKind || DbExpressionKind.LeftOuterJoin == e.ExpressionKind || DbExpressionKind.FullOuterJoin == e.ExpressionKind, "Unrecognized JoinType specified in DbJoinExpression");
			List<Node> list = new List<Node>();
			List<Var> list2 = new List<Var>();
			for (int i = 0; i < inputs.Count; i++)
			{
				Var var;
				Node node = this.VisitBoundExpression(inputs[i].Expression, out var);
				list.Add(node);
				list2.Add(var);
			}
			for (int j = 0; j < list.Count; j++)
			{
				this.PushBindingScope(list2[j], inputs[j].VariableName);
			}
			Node node2 = this.VisitExprAsPredicate(joinCond);
			for (int k = 0; k < list.Count; k++)
			{
				this.ExitExpressionBinding();
			}
			JoinBaseOp joinBaseOp = null;
			DbExpressionKind expressionKind = e.ExpressionKind;
			if (expressionKind <= DbExpressionKind.FullOuterJoin)
			{
				if (expressionKind != DbExpressionKind.CrossJoin)
				{
					if (expressionKind == DbExpressionKind.FullOuterJoin)
					{
						joinBaseOp = this._iqtCommand.CreateFullOuterJoinOp();
					}
				}
				else
				{
					joinBaseOp = this._iqtCommand.CreateCrossJoinOp();
				}
			}
			else if (expressionKind != DbExpressionKind.InnerJoin)
			{
				if (expressionKind == DbExpressionKind.LeftOuterJoin)
				{
					joinBaseOp = this._iqtCommand.CreateLeftOuterJoinOp();
				}
			}
			else
			{
				joinBaseOp = this._iqtCommand.CreateInnerJoinOp();
			}
			PlanCompiler.Assert(joinBaseOp != null, "Unrecognized JoinOp specified in DbJoinExpression, no JoinOp was produced");
			if (e.ExpressionKind != DbExpressionKind.CrossJoin)
			{
				PlanCompiler.Assert(node2 != null, "Non CrossJoinOps must specify a join condition");
				list.Add(node2);
			}
			return this.ProjectNewRecord(this._iqtCommand.CreateNode(joinBaseOp, list), ITreeGenerator.ExtractElementRowType(e.ResultType), list2);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x0007905C File Offset: 0x0007725C
		public override Node Visit(DbApplyExpression e)
		{
			Check.NotNull<DbApplyExpression>(e, "e");
			Node node = this.EnterExpressionBinding(e.Input);
			Node node2 = this.EnterExpressionBinding(e.Apply);
			this.ExitExpressionBinding();
			this.ExitExpressionBinding();
			PlanCompiler.Assert(DbExpressionKind.CrossApply == e.ExpressionKind || DbExpressionKind.OuterApply == e.ExpressionKind, "Unrecognized DbExpressionKind specified in DbApplyExpression");
			ApplyBaseOp applyBaseOp;
			if (DbExpressionKind.CrossApply == e.ExpressionKind)
			{
				applyBaseOp = this._iqtCommand.CreateCrossApplyOp();
			}
			else
			{
				applyBaseOp = this._iqtCommand.CreateOuterApplyOp();
			}
			Node node3 = this._iqtCommand.CreateNode(applyBaseOp, node, node2);
			return this.ProjectNewRecord(node3, ITreeGenerator.ExtractElementRowType(e.ResultType), new Var[]
			{
				this._varMap[node],
				this._varMap[node2]
			});
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x00079128 File Offset: 0x00077328
		public override Node Visit(DbGroupByExpression e)
		{
			Check.NotNull<DbGroupByExpression>(e, "e");
			VarVec varVec = this._iqtCommand.CreateVarVec();
			VarVec varVec2 = this._iqtCommand.CreateVarVec();
			Node node;
			List<Node> list;
			ITreeGenerator.ExpressionBindingScope expressionBindingScope;
			this.ExtractKeys(e, varVec, varVec2, out node, out list, out expressionBindingScope);
			int num = -1;
			for (int i = 0; i < e.Aggregates.Count; i++)
			{
				if (e.Aggregates[i].GetType() == typeof(DbGroupAggregate))
				{
					num = i;
					break;
				}
			}
			Node node2 = null;
			List<Node> list2 = null;
			VarVec varVec3 = this._iqtCommand.CreateVarVec();
			VarVec varVec4 = this._iqtCommand.CreateVarVec();
			if (num >= 0)
			{
				ITreeGenerator.ExpressionBindingScope expressionBindingScope2;
				this.ExtractKeys(e, varVec4, varVec3, out node2, out list2, out expressionBindingScope2);
			}
			expressionBindingScope = new ITreeGenerator.ExpressionBindingScope(this._iqtCommand, e.Input.GroupVariableName, expressionBindingScope.ScopeVar);
			this._varScopes.Push(expressionBindingScope);
			List<Node> list3 = new List<Node>();
			Node node3 = null;
			for (int j = 0; j < e.Aggregates.Count; j++)
			{
				DbAggregate dbAggregate = e.Aggregates[j];
				IList<Node> list4 = this.VisitExprAsScalar(dbAggregate.Arguments);
				Var var;
				if (j != num)
				{
					DbFunctionAggregate dbFunctionAggregate = dbAggregate as DbFunctionAggregate;
					PlanCompiler.Assert(dbFunctionAggregate != null, "Unrecognized DbAggregate used in DbGroupByExpression");
					list3.Add(this.ProcessFunctionAggregate(dbFunctionAggregate, list4, out var));
				}
				else
				{
					node3 = this.ProcessGroupAggregate(list, node2, list2, varVec4, e.Input.Expression.ResultType, out var);
				}
				varVec2.Set(var);
			}
			this.ExitGroupExpressionBinding();
			List<Node> list5 = new List<Node>();
			list5.Add(node);
			list5.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateVarDefListOp(), list));
			list5.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateVarDefListOp(), list3));
			GroupByBaseOp groupByBaseOp;
			if (num >= 0)
			{
				list5.Add(this._iqtCommand.CreateNode(this._iqtCommand.CreateVarDefListOp(), node3));
				groupByBaseOp = this._iqtCommand.CreateGroupByIntoOp(varVec, this._iqtCommand.CreateVarVec(this._varMap[node]), varVec2);
			}
			else
			{
				groupByBaseOp = this._iqtCommand.CreateGroupByOp(varVec, varVec2);
			}
			Node node4 = this._iqtCommand.CreateNode(groupByBaseOp, list5);
			return this.ProjectNewRecord(node4, ITreeGenerator.ExtractElementRowType(e.ResultType), varVec2);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x00079388 File Offset: 0x00077588
		private void ExtractKeys(DbGroupByExpression e, VarVec keyVarSet, VarVec outputVarSet, out Node inputNode, out List<Node> keyVarDefNodes, out ITreeGenerator.ExpressionBindingScope scope)
		{
			inputNode = this.EnterGroupExpressionBinding(e.Input);
			keyVarDefNodes = new List<Node>();
			for (int i = 0; i < e.Keys.Count; i++)
			{
				DbExpression dbExpression = e.Keys[i];
				Node node = this.VisitExprAsScalar(dbExpression);
				PlanCompiler.Assert(node.Op is ScalarOp, "GroupBy Key is not a ScalarOp");
				Var var;
				keyVarDefNodes.Add(this._iqtCommand.CreateVarDefNode(node, out var));
				outputVarSet.Set(var);
				keyVarSet.Set(var);
			}
			scope = this.ExitExpressionBinding();
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x0007941C File Offset: 0x0007761C
		private Node ProcessFunctionAggregate(DbFunctionAggregate funcAgg, IList<Node> argNodes, out Var aggVar)
		{
			Node node = this._iqtCommand.CreateNode(this._iqtCommand.CreateAggregateOp(funcAgg.Function, funcAgg.Distinct), argNodes);
			return this._iqtCommand.CreateVarDefNode(node, out aggVar);
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0007945C File Offset: 0x0007765C
		private Node ProcessGroupAggregate(List<Node> keyVarDefNodes, Node copyOfInput, List<Node> copyOfkeyVarDefNodes, VarVec copyKeyVarSet, TypeUsage inputResultType, out Var groupAggVar)
		{
			Var var = this._varMap[copyOfInput];
			Node node = copyOfInput;
			if (keyVarDefNodes.Count > 0)
			{
				VarVec varVec = this._iqtCommand.CreateVarVec();
				varVec.Set(var);
				varVec.Or(copyKeyVarSet);
				Node node2 = this._iqtCommand.CreateNode(this._iqtCommand.CreateProjectOp(varVec), node, this._iqtCommand.CreateNode(this._iqtCommand.CreateVarDefListOp(), copyOfkeyVarDefNodes));
				List<Node> list = new List<Node>();
				List<Node> list2 = new List<Node>();
				for (int i = 0; i < keyVarDefNodes.Count; i++)
				{
					Node node3 = keyVarDefNodes[i];
					Node node4 = copyOfkeyVarDefNodes[i];
					Var var2 = ((VarDefOp)node3.Op).Var;
					Var var3 = ((VarDefOp)node4.Op).Var;
					this.FlattenProperties(this._iqtCommand.CreateNode(this._iqtCommand.CreateVarRefOp(var2)), list);
					this.FlattenProperties(this._iqtCommand.CreateNode(this._iqtCommand.CreateVarRefOp(var3)), list2);
				}
				PlanCompiler.Assert(list.Count == list2.Count, "The flattened keys lists should have the same number of elements");
				Node node5 = null;
				for (int j = 0; j < list.Count; j++)
				{
					Node node6 = list[j];
					Node node7 = list2[j];
					Node node8;
					if (this._useDatabaseNullSemantics)
					{
						node8 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Or), this._iqtCommand.CreateNode(this._iqtCommand.CreateComparisonOp(OpType.EQ, false), node6, node7), this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.And), this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.IsNull), OpCopier.Copy(this._iqtCommand, node6)), this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.IsNull), OpCopier.Copy(this._iqtCommand, node7))));
					}
					else
					{
						node8 = this._iqtCommand.CreateNode(this._iqtCommand.CreateComparisonOp(OpType.EQ, false), node6, node7);
					}
					if (node5 == null)
					{
						node5 = node8;
					}
					else
					{
						node5 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.And), node5, node8);
					}
				}
				node = this._iqtCommand.CreateNode(this._iqtCommand.CreateFilterOp(), node2, node5);
			}
			this._varMap[node] = var;
			node = this.ConvertRelOpToScalarOpTree(node, inputResultType);
			return this._iqtCommand.CreateVarDefNode(node, out groupAggVar);
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x000796E8 File Offset: 0x000778E8
		private void FlattenProperties(Node input, IList<Node> flattenedProperties)
		{
			if (input.Op.Type.EdmType.BuiltInTypeKind == BuiltInTypeKind.RowType)
			{
				IList<EdmProperty> properties = TypeHelpers.GetProperties(input.Op.Type);
				PlanCompiler.Assert(properties.Count != 0, "No nested properties for RowType");
				for (int i = 0; i < properties.Count; i++)
				{
					Node node = ((i == 0) ? input : OpCopier.Copy(this._iqtCommand, input));
					this.FlattenProperties(this._iqtCommand.CreateNode(this._iqtCommand.CreatePropertyOp(properties[i]), node), flattenedProperties);
				}
				return;
			}
			flattenedProperties.Add(input);
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x00079784 File Offset: 0x00077984
		private Node VisitSortArguments(DbExpressionBinding input, IList<DbSortClause> sortOrder, List<SortKey> sortKeys, out Var inputVar)
		{
			Node node = this.EnterExpressionBinding(input);
			inputVar = this._varMap[node];
			VarVec varVec = this._iqtCommand.CreateVarVec();
			varVec.Set(inputVar);
			List<Node> list = new List<Node>();
			PlanCompiler.Assert(sortKeys.Count == 0, "Non-empty SortKey list before adding converted SortClauses");
			for (int i = 0; i < sortOrder.Count; i++)
			{
				DbSortClause dbSortClause = sortOrder[i];
				Node node2 = this.VisitExprAsScalar(dbSortClause.Expression);
				PlanCompiler.Assert(node2.Op is ScalarOp, "DbSortClause Expression converted to non-ScalarOp");
				Var var;
				list.Add(this._iqtCommand.CreateVarDefNode(node2, out var));
				varVec.Set(var);
				SortKey sortKey;
				if (string.IsNullOrEmpty(dbSortClause.Collation))
				{
					sortKey = Command.CreateSortKey(var, dbSortClause.Ascending);
				}
				else
				{
					sortKey = Command.CreateSortKey(var, dbSortClause.Ascending, dbSortClause.Collation);
				}
				sortKeys.Add(sortKey);
			}
			this.ExitExpressionBinding();
			return this._iqtCommand.CreateNode(this._iqtCommand.CreateProjectOp(varVec), node, this._iqtCommand.CreateNode(this._iqtCommand.CreateVarDefListOp(), list));
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x000798B8 File Offset: 0x00077AB8
		public override Node Visit(DbSkipExpression expression)
		{
			Check.NotNull<DbSkipExpression>(expression, "expression");
			List<SortKey> list = new List<SortKey>();
			Var var;
			Node node = this.VisitSortArguments(expression.Input, expression.SortOrder, list, out var);
			Node node2 = this.VisitExprAsScalar(expression.Count);
			Node node3 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConstrainedSortOp(list), node, node2, this._iqtCommand.CreateNode(this._iqtCommand.CreateNullOp(this._iqtCommand.IntegerType)));
			this._varMap[node3] = var;
			return node3;
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x00079948 File Offset: 0x00077B48
		public override Node Visit(DbSortExpression e)
		{
			Check.NotNull<DbSortExpression>(e, "e");
			List<SortKey> list = new List<SortKey>();
			Var var;
			Node node = this.VisitSortArguments(e.Input, e.SortOrder, list, out var);
			SortOp sortOp = this._iqtCommand.CreateSortOp(list);
			Node node2 = this._iqtCommand.CreateNode(sortOp, node);
			this._varMap[node2] = var;
			return node2;
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x000799AC File Offset: 0x00077BAC
		public override Node Visit(DbQuantifierExpression e)
		{
			Check.NotNull<DbQuantifierExpression>(e, "e");
			PlanCompiler.Assert(DbExpressionKind.Any == e.ExpressionKind || e.ExpressionKind == DbExpressionKind.All, "Invalid DbExpressionKind in DbQuantifierExpression");
			Node node = this.EnterExpressionBinding(e.Input);
			Node node2 = this.VisitExprAsPredicate(e.Predicate);
			if (e.ExpressionKind == DbExpressionKind.All)
			{
				node2 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Not), node2);
				Node node3 = this.VisitExprAsScalar(e.Predicate);
				node3 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.IsNull), node3);
				node2 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Or), node2, node3);
			}
			this.ExitExpressionBinding();
			Var var = this._varMap[node];
			node = this._iqtCommand.CreateNode(this._iqtCommand.CreateFilterOp(), node, node2);
			this._varMap[node] = var;
			Node node4 = this._iqtCommand.CreateNode(this._iqtCommand.CreateExistsOp(), node);
			if (e.ExpressionKind == DbExpressionKind.All)
			{
				node4 = this._iqtCommand.CreateNode(this._iqtCommand.CreateConditionalOp(OpType.Not), node4);
			}
			return node4;
		}

		// Token: 0x04000DF6 RID: 3574
		private static readonly Dictionary<DbExpressionKind, OpType> _opMap = ITreeGenerator.InitializeExpressionKindToOpTypeMap();

		// Token: 0x04000DF7 RID: 3575
		private readonly bool _useDatabaseNullSemantics;

		// Token: 0x04000DF8 RID: 3576
		private readonly Command _iqtCommand;

		// Token: 0x04000DF9 RID: 3577
		private readonly Stack<ITreeGenerator.CqtVariableScope> _varScopes = new Stack<ITreeGenerator.CqtVariableScope>();

		// Token: 0x04000DFA RID: 3578
		private readonly Dictionary<Node, Var> _varMap = new Dictionary<Node, Var>();

		// Token: 0x04000DFB RID: 3579
		private readonly Stack<EdmFunction> _functionExpansions = new Stack<EdmFunction>();

		// Token: 0x04000DFC RID: 3580
		private readonly Dictionary<DbExpression, bool> _functionsIsPredicateFlag = new Dictionary<DbExpression, bool>();

		// Token: 0x04000DFD RID: 3581
		private readonly HashSet<DbFilterExpression> _processedIsOfFilters = new HashSet<DbFilterExpression>();

		// Token: 0x04000DFE RID: 3582
		private readonly HashSet<DbTreatExpression> _fakeTreats = new HashSet<DbTreatExpression>();

		// Token: 0x04000DFF RID: 3583
		private readonly DiscriminatorMap _discriminatorMap;

		// Token: 0x04000E00 RID: 3584
		private readonly DbProjectExpression _discriminatedViewTopProject;

		// Token: 0x020009DE RID: 2526
		private abstract class CqtVariableScope
		{
			// Token: 0x06005FA8 RID: 24488
			internal abstract bool Contains(string varName);

			// Token: 0x17001081 RID: 4225
			internal abstract Node this[string varName] { get; }

			// Token: 0x06005FAA RID: 24490
			internal abstract bool IsPredicate(string varName);
		}

		// Token: 0x020009DF RID: 2527
		private class ExpressionBindingScope : ITreeGenerator.CqtVariableScope
		{
			// Token: 0x06005FAC RID: 24492 RVA: 0x001491E6 File Offset: 0x001473E6
			internal ExpressionBindingScope(Command iqtTree, string name, Var iqtVar)
			{
				this._tree = iqtTree;
				this._varName = name;
				this._var = iqtVar;
			}

			// Token: 0x06005FAD RID: 24493 RVA: 0x00149203 File Offset: 0x00147403
			internal override bool Contains(string name)
			{
				return this._varName == name;
			}

			// Token: 0x17001082 RID: 4226
			internal override Node this[string name]
			{
				get
				{
					PlanCompiler.Assert(name == this._varName, "huh?");
					return this._tree.CreateNode(this._tree.CreateVarRefOp(this._var));
				}
			}

			// Token: 0x06005FAF RID: 24495 RVA: 0x00149245 File Offset: 0x00147445
			internal override bool IsPredicate(string varName)
			{
				return false;
			}

			// Token: 0x17001083 RID: 4227
			// (get) Token: 0x06005FB0 RID: 24496 RVA: 0x00149248 File Offset: 0x00147448
			internal Var ScopeVar
			{
				get
				{
					return this._var;
				}
			}

			// Token: 0x04002868 RID: 10344
			private readonly Command _tree;

			// Token: 0x04002869 RID: 10345
			private readonly string _varName;

			// Token: 0x0400286A RID: 10346
			private readonly Var _var;
		}

		// Token: 0x020009E0 RID: 2528
		private sealed class LambdaScope : ITreeGenerator.CqtVariableScope
		{
			// Token: 0x06005FB1 RID: 24497 RVA: 0x00149250 File Offset: 0x00147450
			internal LambdaScope(ITreeGenerator treeGen, Command command, Dictionary<string, Tuple<Node, bool>> args)
			{
				this._treeGen = treeGen;
				this._command = command;
				this._arguments = args;
				this._referencedArgs = new Dictionary<Node, bool>(this._arguments.Count);
			}

			// Token: 0x06005FB2 RID: 24498 RVA: 0x00149283 File Offset: 0x00147483
			internal override bool Contains(string name)
			{
				return this._arguments.ContainsKey(name);
			}

			// Token: 0x17001084 RID: 4228
			internal override Node this[string name]
			{
				get
				{
					PlanCompiler.Assert(this._arguments.ContainsKey(name), "LambdaScope indexer called for invalid Var");
					Node node = this._arguments[name].Item1;
					if (this._referencedArgs.ContainsKey(node))
					{
						VarMap varMap = null;
						Node node2 = OpCopier.Copy(this._command, node, out varMap);
						if (varMap.Count > 0)
						{
							this.MapCopiedNodeVars(new List<Node>(1) { node }, new List<Node>(1) { node2 }, varMap);
						}
						node = node2;
					}
					else
					{
						this._referencedArgs[node] = true;
					}
					return node;
				}
			}

			// Token: 0x06005FB4 RID: 24500 RVA: 0x0014932B File Offset: 0x0014752B
			internal override bool IsPredicate(string name)
			{
				PlanCompiler.Assert(this._arguments.ContainsKey(name), "LambdaScope indexer called for invalid Var");
				return this._arguments[name].Item2;
			}

			// Token: 0x06005FB5 RID: 24501 RVA: 0x00149354 File Offset: 0x00147554
			private void MapCopiedNodeVars(IList<Node> sources, IList<Node> copies, Dictionary<Var, Var> varMappings)
			{
				PlanCompiler.Assert(sources.Count == copies.Count, "Source/Copy Node count mismatch");
				for (int i = 0; i < sources.Count; i++)
				{
					Node node = sources[i];
					Node node2 = copies[i];
					if (node.Children.Count > 0)
					{
						this.MapCopiedNodeVars(node.Children, node2.Children, varMappings);
					}
					Var var = null;
					if (this._treeGen.VarMap.TryGetValue(node, out var))
					{
						PlanCompiler.Assert(varMappings.ContainsKey(var), "No mapping found for Var in Var to Var map from OpCopier");
						this._treeGen.VarMap[node2] = varMappings[var];
					}
				}
			}

			// Token: 0x0400286B RID: 10347
			private readonly ITreeGenerator _treeGen;

			// Token: 0x0400286C RID: 10348
			private readonly Command _command;

			// Token: 0x0400286D RID: 10349
			private readonly Dictionary<string, Tuple<Node, bool>> _arguments;

			// Token: 0x0400286E RID: 10350
			private readonly Dictionary<Node, bool> _referencedArgs;
		}

		// Token: 0x020009E1 RID: 2529
		// (Invoke) Token: 0x06005FB7 RID: 24503
		private delegate Node VisitExprDelegate(DbExpression e);

		// Token: 0x020009E2 RID: 2530
		private class IsOfFilter
		{
			// Token: 0x06005FBA RID: 24506 RVA: 0x001493FB File Offset: 0x001475FB
			internal IsOfFilter(DbIsOfExpression template)
			{
				this.requiredType = template.OfType;
				this.isExact = template.ExpressionKind == DbExpressionKind.IsOfOnly;
			}

			// Token: 0x06005FBB RID: 24507 RVA: 0x0014941F File Offset: 0x0014761F
			internal IsOfFilter(DbOfTypeExpression template)
			{
				this.requiredType = template.OfType;
				this.isExact = template.ExpressionKind == DbExpressionKind.OfTypeOnly;
			}

			// Token: 0x06005FBC RID: 24508 RVA: 0x00149443 File Offset: 0x00147643
			private IsOfFilter(TypeUsage required, bool exact)
			{
				this.requiredType = required;
				this.isExact = exact;
			}

			// Token: 0x06005FBD RID: 24509 RVA: 0x0014945C File Offset: 0x0014765C
			private ITreeGenerator.IsOfFilter Merge(TypeUsage otherRequiredType, bool otherIsExact)
			{
				bool flag = this.requiredType.EdmEquals(otherRequiredType);
				ITreeGenerator.IsOfFilter isOfFilter;
				if (flag && this.isExact == otherIsExact)
				{
					isOfFilter = this;
				}
				else if (this.isExact && otherIsExact)
				{
					isOfFilter = new ITreeGenerator.IsOfFilter(otherRequiredType, otherIsExact);
					isOfFilter.next = this;
				}
				else if (!this.isExact && !otherIsExact)
				{
					if (otherRequiredType.IsSubtypeOf(this.requiredType))
					{
						isOfFilter = new ITreeGenerator.IsOfFilter(otherRequiredType, false);
						isOfFilter.next = this.next;
					}
					else if (this.requiredType.IsSubtypeOf(otherRequiredType))
					{
						isOfFilter = this;
					}
					else
					{
						isOfFilter = new ITreeGenerator.IsOfFilter(otherRequiredType, otherIsExact);
						isOfFilter.next = this;
					}
				}
				else if (flag)
				{
					isOfFilter = new ITreeGenerator.IsOfFilter(otherRequiredType, true);
					isOfFilter.next = this.next;
				}
				else
				{
					TypeUsage typeUsage = (this.isExact ? this.requiredType : otherRequiredType);
					TypeUsage typeUsage2 = (this.isExact ? otherRequiredType : this.requiredType);
					if (typeUsage.IsSubtypeOf(typeUsage2))
					{
						if (typeUsage == this.requiredType && this.isExact)
						{
							isOfFilter = this;
						}
						else
						{
							isOfFilter = new ITreeGenerator.IsOfFilter(typeUsage, true);
							isOfFilter.next = this.next;
						}
					}
					else
					{
						isOfFilter = new ITreeGenerator.IsOfFilter(otherRequiredType, otherIsExact);
						isOfFilter.next = this;
					}
				}
				return isOfFilter;
			}

			// Token: 0x06005FBE RID: 24510 RVA: 0x00149583 File Offset: 0x00147783
			internal ITreeGenerator.IsOfFilter Merge(DbIsOfExpression other)
			{
				return this.Merge(other.OfType, other.ExpressionKind == DbExpressionKind.IsOfOnly);
			}

			// Token: 0x06005FBF RID: 24511 RVA: 0x0014959B File Offset: 0x0014779B
			internal ITreeGenerator.IsOfFilter Merge(DbOfTypeExpression other)
			{
				return this.Merge(other.OfType, other.ExpressionKind == DbExpressionKind.OfTypeOnly);
			}

			// Token: 0x06005FC0 RID: 24512 RVA: 0x001495B3 File Offset: 0x001477B3
			internal IEnumerable<KeyValuePair<TypeUsage, bool>> ToEnumerable()
			{
				for (ITreeGenerator.IsOfFilter currentFilter = this; currentFilter != null; currentFilter = currentFilter.next)
				{
					yield return new KeyValuePair<TypeUsage, bool>(currentFilter.requiredType, currentFilter.isExact);
				}
				yield break;
			}

			// Token: 0x0400286F RID: 10351
			private readonly TypeUsage requiredType;

			// Token: 0x04002870 RID: 10352
			private readonly bool isExact;

			// Token: 0x04002871 RID: 10353
			private ITreeGenerator.IsOfFilter next;
		}
	}
}
