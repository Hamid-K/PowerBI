using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200033B RID: 827
	internal class CTreeGenerator : BasicOpVisitorOfT<DbExpression>
	{
		// Token: 0x06002746 RID: 10054 RVA: 0x00072548 File Offset: 0x00070748
		internal static DbCommandTree Generate(Command itree, Node toConvert)
		{
			return new CTreeGenerator(itree, toConvert)._queryTree;
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x00072558 File Offset: 0x00070758
		private CTreeGenerator(Command itree, Node toConvert)
		{
			this._iqtCommand = itree;
			DbExpression dbExpression = base.VisitNode(toConvert);
			this._queryTree = DbQueryCommandTree.FromValidExpression(itree.MetadataWorkspace, DataSpace.SSpace, dbExpression, true, false);
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000726AB File Offset: 0x000708AB
		private void AssertRelOp(DbExpression expr)
		{
			PlanCompiler.Assert(this._relOpState.ContainsKey(expr), "not a relOp expression?");
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000726C4 File Offset: 0x000708C4
		private CTreeGenerator.RelOpInfo PublishRelOp(string name, DbExpression expr, CTreeGenerator.VarInfoList publishedVars)
		{
			CTreeGenerator.RelOpInfo relOpInfo = new CTreeGenerator.RelOpInfo(name, expr, publishedVars);
			this._relOpState.Add(expr, relOpInfo);
			return relOpInfo;
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000726E8 File Offset: 0x000708E8
		private CTreeGenerator.RelOpInfo ConsumeRelOp(DbExpression expr)
		{
			this.AssertRelOp(expr);
			CTreeGenerator.RelOpInfo relOpInfo = this._relOpState[expr];
			this._relOpState.Remove(expr);
			return relOpInfo;
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x0007270C File Offset: 0x0007090C
		private CTreeGenerator.RelOpInfo VisitAsRelOp(Node inputNode)
		{
			PlanCompiler.Assert(inputNode.Op is RelOp, "Non-RelOp used as DbExpressionBinding Input");
			DbExpression dbExpression = base.VisitNode(inputNode);
			return this.ConsumeRelOp(dbExpression);
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x00072740 File Offset: 0x00070940
		private void PushExpressionBindingScope(CTreeGenerator.RelOpInfo inputState)
		{
			PlanCompiler.Assert(inputState != null && inputState.PublisherName != null && inputState.PublishedVars != null, "Invalid RelOpInfo produced by DbExpressionBinding Input");
			this._bindingScopes.Push(inputState);
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x00072770 File Offset: 0x00070970
		private CTreeGenerator.RelOpInfo EnterExpressionBindingScope(Node inputNode, bool pushScope)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.VisitAsRelOp(inputNode);
			if (pushScope)
			{
				this.PushExpressionBindingScope(relOpInfo);
			}
			return relOpInfo;
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x00072790 File Offset: 0x00070990
		private CTreeGenerator.RelOpInfo EnterExpressionBindingScope(Node inputNode)
		{
			return this.EnterExpressionBindingScope(inputNode, true);
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x0007279A File Offset: 0x0007099A
		private void ExitExpressionBindingScope(CTreeGenerator.RelOpInfo scope, bool wasPushed)
		{
			if (wasPushed)
			{
				PlanCompiler.Assert(this._bindingScopes.Count > 0, "ExitExpressionBindingScope called on empty ExpressionBindingScope stack");
				PlanCompiler.Assert((CTreeGenerator.RelOpInfo)this._bindingScopes.Pop() == scope, "ExitExpressionBindingScope called on incorrect expression");
			}
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000727D4 File Offset: 0x000709D4
		private void ExitExpressionBindingScope(CTreeGenerator.RelOpInfo scope)
		{
			this.ExitExpressionBindingScope(scope, true);
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000727E0 File Offset: 0x000709E0
		private CTreeGenerator.GroupByScope EnterGroupByScope(Node inputNode)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.VisitAsRelOp(inputNode);
			string publisherName = relOpInfo.PublisherName;
			string text = string.Format(CultureInfo.InvariantCulture, "{0}Group", new object[] { publisherName });
			CTreeGenerator.GroupByScope groupByScope = new CTreeGenerator.GroupByScope(relOpInfo.CreateBinding().Expression.GroupBindAs(publisherName, text), relOpInfo.PublishedVars);
			this._bindingScopes.Push(groupByScope);
			return groupByScope;
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x00072841 File Offset: 0x00070A41
		private void ExitGroupByScope(CTreeGenerator.GroupByScope scope)
		{
			PlanCompiler.Assert(this._bindingScopes.Count > 0, "ExitGroupByScope called on empty ExpressionBindingScope stack");
			PlanCompiler.Assert((CTreeGenerator.GroupByScope)this._bindingScopes.Pop() == scope, "ExitGroupByScope called on incorrect expression");
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x00072878 File Offset: 0x00070A78
		private void EnterVarDefScope(List<Node> varDefNodes)
		{
			Dictionary<Var, DbExpression> dictionary = new Dictionary<Var, DbExpression>();
			foreach (Node node in varDefNodes)
			{
				VarDefOp varDefOp = node.Op as VarDefOp;
				PlanCompiler.Assert(varDefOp != null, "VarDefListOp contained non-VarDefOp child node");
				PlanCompiler.Assert(varDefOp.Var is ComputedVar, "VarDefOp defined non-Computed Var");
				dictionary.Add(varDefOp.Var, base.VisitNode(node.Child0));
			}
			this._varScopes.Push(new CTreeGenerator.VarDefScope(dictionary));
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x00072920 File Offset: 0x00070B20
		private void EnterVarDefListScope(Node varDefListNode)
		{
			PlanCompiler.Assert(varDefListNode.Op is VarDefListOp, "EnterVarDefListScope called with non-VarDefListOp");
			this.EnterVarDefScope(varDefListNode.Children);
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x00072946 File Offset: 0x00070B46
		private void ExitVarDefScope()
		{
			PlanCompiler.Assert(this._varScopes.Count > 0, "ExitVarDefScope called on empty VarDefScope stack");
			this._varScopes.Pop();
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x0007296C File Offset: 0x00070B6C
		private DbExpression ResolveVar(Var referencedVar)
		{
			DbExpression dbExpression = null;
			ParameterVar parameterVar = referencedVar as ParameterVar;
			if (parameterVar != null)
			{
				DbParameterReferenceExpression dbParameterReferenceExpression;
				if (!this._addedParams.TryGetValue(parameterVar, out dbParameterReferenceExpression))
				{
					dbParameterReferenceExpression = parameterVar.Type.Parameter(parameterVar.ParameterName);
					this._addedParams[parameterVar] = dbParameterReferenceExpression;
				}
				dbExpression = dbParameterReferenceExpression;
			}
			else
			{
				ComputedVar computedVar = referencedVar as ComputedVar;
				if (computedVar != null && this._varScopes.Count > 0 && !this._varScopes.Peek().TryResolveVar(computedVar, out dbExpression))
				{
					dbExpression = null;
				}
				if (dbExpression == null)
				{
					DbExpression dbExpression2 = null;
					using (Stack<CTreeGenerator.IqtVarScope>.Enumerator enumerator = this._bindingScopes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.TryResolveVar(referencedVar, out dbExpression2))
							{
								dbExpression = dbExpression2;
								break;
							}
						}
					}
				}
			}
			PlanCompiler.Assert(dbExpression != null, string.Format(CultureInfo.InvariantCulture, "Unresolvable Var used in Command: VarType={0}, Id={1}", new object[]
			{
				Enum.GetName(typeof(VarType), referencedVar.VarType),
				referencedVar.Id
			}));
			return dbExpression;
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x00072A88 File Offset: 0x00070C88
		private static void AssertBinary(Node n)
		{
			PlanCompiler.Assert(2 == n.Children.Count, string.Format(CultureInfo.InvariantCulture, "Non-Binary {0} encountered", new object[] { n.Op.GetType().Name }));
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x00072AC5 File Offset: 0x00070CC5
		private DbExpression VisitChild(Node n, int index)
		{
			PlanCompiler.Assert(n.Children.Count > index, "VisitChild called with invalid index");
			return base.VisitNode(n.Children[index]);
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x00072AF4 File Offset: 0x00070CF4
		private new List<DbExpression> VisitChildren(Node n)
		{
			List<DbExpression> list = new List<DbExpression>();
			foreach (Node node in n.Children)
			{
				list.Add(base.VisitNode(node));
			}
			return list;
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x00072B54 File Offset: 0x00070D54
		protected override DbExpression VisitConstantOp(ConstantBaseOp op, Node n)
		{
			return op.Type.Constant(op.Value);
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x00072B67 File Offset: 0x00070D67
		public override DbExpression Visit(ConstantOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x00072B71 File Offset: 0x00070D71
		public override DbExpression Visit(InternalConstantOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x00072B7B File Offset: 0x00070D7B
		public override DbExpression Visit(NullOp op, Node n)
		{
			return op.Type.Null();
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x00072B88 File Offset: 0x00070D88
		public override DbExpression Visit(NullSentinelOp op, Node n)
		{
			return this.VisitConstantOp(op, n);
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x00072B92 File Offset: 0x00070D92
		public override DbExpression Visit(ConstantPredicateOp op, Node n)
		{
			return DbExpressionBuilder.True.Equal(op.IsTrue ? DbExpressionBuilder.True : DbExpressionBuilder.False);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x00072BB2 File Offset: 0x00070DB2
		public override DbExpression Visit(FunctionOp op, Node n)
		{
			return op.Function.Invoke(this.VisitChildren(n));
		}

		// Token: 0x06002761 RID: 10081 RVA: 0x00072BC6 File Offset: 0x00070DC6
		public override DbExpression Visit(PropertyOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00072BCD File Offset: 0x00070DCD
		public override DbExpression Visit(RelPropertyOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x00072BD4 File Offset: 0x00070DD4
		public override DbExpression Visit(ArithmeticOp op, Node n)
		{
			DbExpression dbExpression;
			if (OpType.UnaryMinus == op.OpType)
			{
				dbExpression = this.VisitChild(n, 0).UnaryMinus();
			}
			else
			{
				DbExpression dbExpression2 = this.VisitChild(n, 0);
				DbExpression dbExpression3 = this.VisitChild(n, 1);
				switch (op.OpType)
				{
				case OpType.Plus:
					dbExpression = dbExpression2.Plus(dbExpression3);
					break;
				case OpType.Minus:
					dbExpression = dbExpression2.Minus(dbExpression3);
					break;
				case OpType.Multiply:
					dbExpression = dbExpression2.Multiply(dbExpression3);
					break;
				case OpType.Divide:
					dbExpression = dbExpression2.Divide(dbExpression3);
					break;
				case OpType.Modulo:
					dbExpression = dbExpression2.Modulo(dbExpression3);
					break;
				default:
					dbExpression = null;
					break;
				}
			}
			PlanCompiler.Assert(dbExpression != null, string.Format(CultureInfo.InvariantCulture, "ArithmeticOp OpType not recognized: {0}", new object[] { Enum.GetName(typeof(OpType), op.OpType) }));
			return dbExpression;
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x00072CA8 File Offset: 0x00070EA8
		public override DbExpression Visit(CaseOp op, Node n)
		{
			int num = n.Children.Count;
			PlanCompiler.Assert(num > 1, "Invalid CaseOp: At least 2 child Nodes (1 When/Then pair) must be present");
			List<DbExpression> list = new List<DbExpression>();
			List<DbExpression> list2 = new List<DbExpression>();
			DbExpression dbExpression;
			if (n.Children.Count % 2 == 0)
			{
				dbExpression = op.Type.Null();
			}
			else
			{
				num--;
				dbExpression = this.VisitChild(n, n.Children.Count - 1);
			}
			for (int i = 0; i < num; i += 2)
			{
				list.Add(this.VisitChild(n, i));
				list2.Add(this.VisitChild(n, i + 1));
			}
			return DbExpressionBuilder.Case(list, list2, dbExpression);
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x00072D4C File Offset: 0x00070F4C
		public override DbExpression Visit(ComparisonOp op, Node n)
		{
			CTreeGenerator.AssertBinary(n);
			DbExpression dbExpression = this.VisitChild(n, 0);
			DbExpression dbExpression2 = this.VisitChild(n, 1);
			DbExpression dbExpression3;
			switch (op.OpType)
			{
			case OpType.GT:
				dbExpression3 = dbExpression.GreaterThan(dbExpression2);
				break;
			case OpType.GE:
				dbExpression3 = dbExpression.GreaterThanOrEqual(dbExpression2);
				break;
			case OpType.LE:
				dbExpression3 = dbExpression.LessThanOrEqual(dbExpression2);
				break;
			case OpType.LT:
				dbExpression3 = dbExpression.LessThan(dbExpression2);
				break;
			case OpType.EQ:
				dbExpression3 = dbExpression.Equal(dbExpression2);
				break;
			case OpType.NE:
				dbExpression3 = dbExpression.NotEqual(dbExpression2);
				break;
			default:
				dbExpression3 = null;
				break;
			}
			PlanCompiler.Assert(dbExpression3 != null, string.Format(CultureInfo.InvariantCulture, "ComparisonOp OpType not recognized: {0}", new object[] { Enum.GetName(typeof(OpType), op.OpType) }));
			return dbExpression3;
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x00072E18 File Offset: 0x00071018
		public override DbExpression Visit(ConditionalOp op, Node n)
		{
			DbExpression dbExpression = this.VisitChild(n, 0);
			DbExpression dbExpression2;
			switch (op.OpType)
			{
			case OpType.And:
				dbExpression2 = dbExpression.And(this.VisitChild(n, 1));
				break;
			case OpType.Or:
				dbExpression2 = dbExpression.Or(this.VisitChild(n, 1));
				break;
			case OpType.In:
			{
				int count = n.Children.Count;
				List<DbExpression> list = new List<DbExpression>(count - 1);
				for (int i = 1; i < count; i++)
				{
					list.Add(this.VisitChild(n, i));
				}
				dbExpression2 = DbExpressionBuilder.CreateInExpression(dbExpression, list);
				break;
			}
			case OpType.Not:
			{
				DbNotExpression dbNotExpression = dbExpression as DbNotExpression;
				if (dbNotExpression != null)
				{
					dbExpression2 = dbNotExpression.Argument;
				}
				else
				{
					dbExpression2 = dbExpression.Not();
				}
				break;
			}
			case OpType.IsNull:
				dbExpression2 = dbExpression.IsNull();
				break;
			default:
				dbExpression2 = null;
				break;
			}
			PlanCompiler.Assert(dbExpression2 != null, string.Format(CultureInfo.InvariantCulture, "ConditionalOp OpType not recognized: {0}", new object[] { Enum.GetName(typeof(OpType), op.OpType) }));
			return dbExpression2;
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x00072F25 File Offset: 0x00071125
		public override DbExpression Visit(LikeOp op, Node n)
		{
			return this.VisitChild(n, 0).Like(this.VisitChild(n, 1), this.VisitChild(n, 2));
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x00072F44 File Offset: 0x00071144
		public override DbExpression Visit(AggregateOp op, Node n)
		{
			PlanCompiler.Assert(false, "AggregateOp encountered outside of GroupByOp");
			throw new NotSupportedException(Strings.Iqt_CTGen_UnexpectedAggregate);
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x00072F5B File Offset: 0x0007115B
		public override DbExpression Visit(NavigateOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x00072F62 File Offset: 0x00071162
		public override DbExpression Visit(NewEntityOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x00072F69 File Offset: 0x00071169
		public override DbExpression Visit(NewInstanceOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x00072F70 File Offset: 0x00071170
		public override DbExpression Visit(DiscriminatedNewEntityOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x00072F77 File Offset: 0x00071177
		public override DbExpression Visit(NewMultisetOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x00072F7E File Offset: 0x0007117E
		public override DbExpression Visit(NewRecordOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x00072F85 File Offset: 0x00071185
		public override DbExpression Visit(RefOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x00072F8C File Offset: 0x0007118C
		public override DbExpression Visit(VarRefOp op, Node n)
		{
			return this.ResolveVar(op.Var);
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x00072F9A File Offset: 0x0007119A
		public override DbExpression Visit(TreatOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x00072FA1 File Offset: 0x000711A1
		public override DbExpression Visit(CastOp op, Node n)
		{
			return this.VisitChild(n, 0).CastTo(op.Type);
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x00072FB6 File Offset: 0x000711B6
		public override DbExpression Visit(SoftCastOp op, Node n)
		{
			return this.VisitChild(n, 0);
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x00072FC0 File Offset: 0x000711C0
		public override DbExpression Visit(IsOfOp op, Node n)
		{
			if (op.IsOfOnly)
			{
				return this.VisitChild(n, 0).IsOfOnly(op.IsOfType);
			}
			return this.VisitChild(n, 0).IsOf(op.IsOfType);
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x00072FF4 File Offset: 0x000711F4
		public override DbExpression Visit(ExistsOp op, Node n)
		{
			DbExpression dbExpression = base.VisitNode(n.Child0);
			this.ConsumeRelOp(dbExpression);
			return dbExpression.IsEmpty().Not();
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x00073024 File Offset: 0x00071224
		public override DbExpression Visit(ElementOp op, Node n)
		{
			DbExpression dbExpression = base.VisitNode(n.Child0);
			this.AssertRelOp(dbExpression);
			this.ConsumeRelOp(dbExpression);
			return DbExpressionBuilder.CreateElementExpressionUnwrapSingleProperty(dbExpression);
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x00073053 File Offset: 0x00071253
		public override DbExpression Visit(GetRefKeyOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0007305A File Offset: 0x0007125A
		public override DbExpression Visit(GetEntityRefOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x00073061 File Offset: 0x00071261
		public override DbExpression Visit(CollectOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x00073068 File Offset: 0x00071268
		private static string GenerateNameForVar(Var projectedVar, Dictionary<string, AliasGenerator> aliasMap, AliasGenerator defaultAliasGenerator, Dictionary<string, string> alreadyUsedNames)
		{
			string text;
			AliasGenerator aliasGenerator;
			if (projectedVar.TryGetName(out text))
			{
				if (!aliasMap.TryGetValue(text, out aliasGenerator))
				{
					aliasGenerator = new AliasGenerator(text);
					aliasMap[text] = aliasGenerator;
				}
				else
				{
					text = aliasGenerator.Next();
				}
			}
			else
			{
				aliasGenerator = defaultAliasGenerator;
				text = aliasGenerator.Next();
			}
			while (alreadyUsedNames.ContainsKey(text))
			{
				text = aliasGenerator.Next();
			}
			alreadyUsedNames[text] = text;
			return text;
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000730C8 File Offset: 0x000712C8
		private DbExpression CreateProject(CTreeGenerator.RelOpInfo sourceInfo, IEnumerable<Var> outputVars)
		{
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
			AliasGenerator aliasGenerator = new AliasGenerator("C");
			Dictionary<string, AliasGenerator> dictionary = new Dictionary<string, AliasGenerator>(StringComparer.InvariantCultureIgnoreCase);
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
			foreach (Var var in outputVars)
			{
				string text = CTreeGenerator.GenerateNameForVar(var, dictionary, aliasGenerator, dictionary2);
				DbExpression dbExpression = this.ResolveVar(var);
				list.Add(new KeyValuePair<string, DbExpression>(text, dbExpression));
				CTreeGenerator.VarInfo varInfo = new CTreeGenerator.VarInfo(var);
				varInfo.PrependProperty(text);
				varInfoList.Add(varInfo);
			}
			DbExpression dbExpression2 = sourceInfo.CreateBinding().Project(DbExpressionBuilder.NewRow(list));
			this.PublishRelOp(this._projectAliases.Next(), dbExpression2, varInfoList);
			return dbExpression2;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000731A8 File Offset: 0x000713A8
		private static CTreeGenerator.VarInfoList GetTableVars(Table targetTable)
		{
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			if (targetTable.TableMetadata.Flattened)
			{
				for (int i = 0; i < targetTable.Columns.Count; i++)
				{
					CTreeGenerator.VarInfo varInfo = new CTreeGenerator.VarInfo(targetTable.Columns[i]);
					varInfo.PrependProperty(targetTable.TableMetadata.Columns[i].Name);
					varInfoList.Add(varInfo);
				}
			}
			else
			{
				varInfoList.Add(new CTreeGenerator.VarInfo(targetTable.Columns[0]));
			}
			return varInfoList;
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x00073230 File Offset: 0x00071430
		public override DbExpression Visit(ScanTableOp op, Node n)
		{
			PlanCompiler.Assert(op.Table.TableMetadata.Extent != null, "Invalid TableMetadata used in ScanTableOp - no Extent specified");
			PlanCompiler.Assert(!n.HasChild0, "views are not expected here");
			CTreeGenerator.VarInfoList tableVars = CTreeGenerator.GetTableVars(op.Table);
			DbExpression dbExpression = op.Table.TableMetadata.Extent.Scan();
			this.PublishRelOp(this._extentAliases.Next(), dbExpression, tableVars);
			return dbExpression;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x000732A4 File Offset: 0x000714A4
		public override DbExpression Visit(ScanViewOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000732AC File Offset: 0x000714AC
		public override DbExpression Visit(UnnestOp op, Node n)
		{
			PlanCompiler.Assert(n.Child0.Op.OpType == OpType.VarDef, "an un-nest's child must be a VarDef");
			Node child = n.Child0.Child0;
			DbExpression dbExpression = child.Op.Accept<DbExpression>(this, child);
			PlanCompiler.Assert(dbExpression.ResultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.CollectionType, "the input to un-nest must yield a collection after plan compilation");
			CTreeGenerator.VarInfoList tableVars = CTreeGenerator.GetTableVars(op.Table);
			this.PublishRelOp(this._extentAliases.Next(), dbExpression, tableVars);
			return dbExpression;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x00073330 File Offset: 0x00071530
		private CTreeGenerator.RelOpInfo BuildEmptyProjection(Node relOpNode)
		{
			if (relOpNode.Op.OpType == OpType.Project)
			{
				relOpNode = relOpNode.Child0;
			}
			CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(relOpNode);
			DbExpression dbExpression = DbExpressionBuilder.Constant(1);
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
			list.Add(new KeyValuePair<string, DbExpression>("C0", dbExpression));
			DbExpression dbExpression2 = relOpInfo.CreateBinding().Project(DbExpressionBuilder.NewRow(list));
			this.PublishRelOp(this._projectAliases.Next(), dbExpression2, new CTreeGenerator.VarInfoList());
			this.ExitExpressionBindingScope(relOpInfo);
			return this.ConsumeRelOp(dbExpression2);
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000733B8 File Offset: 0x000715B8
		private CTreeGenerator.RelOpInfo BuildProjection(Node relOpNode, IEnumerable<Var> projectionVars)
		{
			DbExpression dbExpression;
			if (relOpNode.Op is ProjectOp)
			{
				dbExpression = this.VisitProject(relOpNode, projectionVars);
			}
			else
			{
				CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(relOpNode);
				dbExpression = this.CreateProject(relOpInfo, projectionVars);
				this.ExitExpressionBindingScope(relOpInfo);
			}
			return this.ConsumeRelOp(dbExpression);
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x00073400 File Offset: 0x00071600
		private DbExpression VisitProject(Node n, IEnumerable<Var> varList)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(n.Child0);
			if (n.Children.Count > 1)
			{
				this.EnterVarDefListScope(n.Child1);
			}
			DbExpression dbExpression = this.CreateProject(relOpInfo, varList);
			if (n.Children.Count > 1)
			{
				this.ExitVarDefScope();
			}
			this.ExitExpressionBindingScope(relOpInfo);
			return dbExpression;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x00073457 File Offset: 0x00071657
		public override DbExpression Visit(ProjectOp op, Node n)
		{
			return this.VisitProject(n, op.Outputs);
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00073468 File Offset: 0x00071668
		public override DbExpression Visit(FilterOp op, Node n)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(n.Child0);
			DbExpression dbExpression = base.VisitNode(n.Child1);
			PlanCompiler.Assert(TypeSemantics.IsPrimitiveType(dbExpression.ResultType, PrimitiveTypeKind.Boolean), "Invalid FilterOp Predicate (non-ScalarOp or non-Boolean result)");
			DbExpression dbExpression2 = relOpInfo.CreateBinding().Filter(dbExpression);
			this.ExitExpressionBindingScope(relOpInfo);
			this.PublishRelOp(this._filterAliases.Next(), dbExpression2, relOpInfo.PublishedVars);
			return dbExpression2;
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000734D4 File Offset: 0x000716D4
		private List<DbSortClause> VisitSortKeys(IList<global::System.Data.Entity.Core.Query.InternalTrees.SortKey> sortKeys)
		{
			VarVec varVec = this._iqtCommand.CreateVarVec();
			List<DbSortClause> list = new List<DbSortClause>();
			foreach (global::System.Data.Entity.Core.Query.InternalTrees.SortKey sortKey in sortKeys)
			{
				if (!varVec.IsSet(sortKey.Var))
				{
					varVec.Set(sortKey.Var);
					DbExpression dbExpression = this.ResolveVar(sortKey.Var);
					DbSortClause dbSortClause;
					if (!string.IsNullOrEmpty(sortKey.Collation))
					{
						dbSortClause = (sortKey.AscendingSort ? dbExpression.ToSortClause(sortKey.Collation) : dbExpression.ToSortClauseDescending(sortKey.Collation));
					}
					else
					{
						dbSortClause = (sortKey.AscendingSort ? dbExpression.ToSortClause() : dbExpression.ToSortClauseDescending());
					}
					list.Add(dbSortClause);
				}
			}
			return list;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000735B0 File Offset: 0x000717B0
		public override DbExpression Visit(SortOp op, Node n)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(n.Child0);
			PlanCompiler.Assert(!n.HasChild1, "SortOp can have only one child");
			DbExpression dbExpression = relOpInfo.CreateBinding().Sort(this.VisitSortKeys(op.Keys));
			this.ExitExpressionBindingScope(relOpInfo);
			this.PublishRelOp(this._sortAliases.Next(), dbExpression, relOpInfo.PublishedVars);
			return dbExpression;
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x00073616 File Offset: 0x00071816
		private static DbExpression CreateLimitExpression(DbExpression argument, DbExpression limit, bool withTies)
		{
			PlanCompiler.Assert(!withTies, "Limit with Ties is not currently supported");
			return argument.Limit(limit);
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x00073630 File Offset: 0x00071830
		public override DbExpression Visit(ConstrainedSortOp op, Node n)
		{
			DbExpression dbExpression = null;
			string text = null;
			bool flag = OpType.Null == n.Child1.Op.OpType;
			bool flag2 = OpType.Null == n.Child2.Op.OpType;
			PlanCompiler.Assert(!flag || !flag2, "ConstrainedSortOp with no Skip Count and no Limit?");
			CTreeGenerator.RelOpInfo relOpInfo;
			if (op.Keys.Count == 0)
			{
				PlanCompiler.Assert(flag, "ConstrainedSortOp without SortKeys cannot have Skip Count");
				DbExpression dbExpression2 = base.VisitNode(n.Child0);
				relOpInfo = this.ConsumeRelOp(dbExpression2);
				dbExpression = CTreeGenerator.CreateLimitExpression(dbExpression2, base.VisitNode(n.Child2), op.WithTies);
				text = this._limitAliases.Next();
			}
			else
			{
				relOpInfo = this.EnterExpressionBindingScope(n.Child0);
				List<DbSortClause> list = this.VisitSortKeys(op.Keys);
				this.ExitExpressionBindingScope(relOpInfo);
				if (!flag && !flag2)
				{
					dbExpression = CTreeGenerator.CreateLimitExpression(relOpInfo.CreateBinding().Skip(list, this.VisitChild(n, 1)), this.VisitChild(n, 2), op.WithTies);
					text = this._limitAliases.Next();
				}
				else if (!flag && flag2)
				{
					dbExpression = relOpInfo.CreateBinding().Skip(list, this.VisitChild(n, 1));
					text = this._skipAliases.Next();
				}
				else if (flag && !flag2)
				{
					dbExpression = CTreeGenerator.CreateLimitExpression(relOpInfo.CreateBinding().Sort(list), this.VisitChild(n, 2), op.WithTies);
					text = this._limitAliases.Next();
				}
			}
			this.PublishRelOp(text, dbExpression, relOpInfo.PublishedVars);
			return dbExpression;
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000737AC File Offset: 0x000719AC
		public override DbExpression Visit(GroupByOp op, Node n)
		{
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			CTreeGenerator.GroupByScope groupByScope = this.EnterGroupByScope(n.Child0);
			this.EnterVarDefListScope(n.Child1);
			AliasGenerator aliasGenerator = new AliasGenerator("K");
			List<KeyValuePair<string, DbExpression>> list = new List<KeyValuePair<string, DbExpression>>();
			List<Var> list2 = new List<Var>(op.Outputs);
			foreach (Var var in op.Keys)
			{
				string text = aliasGenerator.Next();
				list.Add(new KeyValuePair<string, DbExpression>(text, this.ResolveVar(var)));
				CTreeGenerator.VarInfo varInfo = new CTreeGenerator.VarInfo(var);
				varInfo.PrependProperty(text);
				varInfoList.Add(varInfo);
				list2.Remove(var);
			}
			this.ExitVarDefScope();
			groupByScope.SwitchToGroupReference();
			Dictionary<Var, DbAggregate> dictionary = new Dictionary<Var, DbAggregate>();
			Node child = n.Child2;
			PlanCompiler.Assert(child.Op is VarDefListOp, "Invalid Aggregates VarDefListOp Node encountered in GroupByOp");
			foreach (Node node in child.Children)
			{
				VarDefOp varDefOp = node.Op as VarDefOp;
				PlanCompiler.Assert(varDefOp != null, "Non-VarDefOp Node encountered as child of Aggregates VarDefListOp Node");
				Var var2 = varDefOp.Var;
				PlanCompiler.Assert(var2 is ComputedVar, "Non-ComputedVar encountered in Aggregate VarDefOp");
				Node child2 = node.Child0;
				IEnumerable<DbExpression> enumerable = child2.Children.Select(new Func<Node, DbExpression>(base.VisitNode));
				AggregateOp aggregateOp = child2.Op as AggregateOp;
				PlanCompiler.Assert(aggregateOp != null, "Non-Aggregate Node encountered as child of Aggregate VarDefOp Node");
				DbFunctionAggregate dbFunctionAggregate;
				if (aggregateOp.IsDistinctAggregate)
				{
					dbFunctionAggregate = aggregateOp.AggFunc.AggregateDistinct(enumerable);
				}
				else
				{
					dbFunctionAggregate = aggregateOp.AggFunc.Aggregate(enumerable);
				}
				PlanCompiler.Assert(list2.Contains(var2), "Defined aggregate Var not in Output Aggregate Vars list?");
				dictionary.Add(var2, dbFunctionAggregate);
			}
			this.ExitGroupByScope(groupByScope);
			AliasGenerator aliasGenerator2 = new AliasGenerator("A");
			List<KeyValuePair<string, DbAggregate>> list3 = new List<KeyValuePair<string, DbAggregate>>();
			foreach (Var var3 in list2)
			{
				string text2 = aliasGenerator2.Next();
				list3.Add(new KeyValuePair<string, DbAggregate>(text2, dictionary[var3]));
				CTreeGenerator.VarInfo varInfo2 = new CTreeGenerator.VarInfo(var3);
				varInfo2.PrependProperty(text2);
				varInfoList.Add(varInfo2);
			}
			DbExpression dbExpression = groupByScope.Binding.GroupBy(list, list3);
			this.PublishRelOp(this._groupByAliases.Next(), dbExpression, varInfoList);
			return dbExpression;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x00073A54 File Offset: 0x00071C54
		public override DbExpression Visit(GroupByIntoOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x00073A5C File Offset: 0x00071C5C
		private CTreeGenerator.RelOpInfo VisitJoinInput(Node joinInputNode)
		{
			CTreeGenerator.RelOpInfo relOpInfo;
			if (joinInputNode.Op.OpType == OpType.Filter && joinInputNode.Child0.Op.OpType == OpType.ScanTable)
			{
				ScanTableOp scanTableOp = (ScanTableOp)joinInputNode.Child0.Op;
				if (scanTableOp.Table.ReferencedColumns.IsEmpty)
				{
					relOpInfo = this.BuildEmptyProjection(joinInputNode);
				}
				else
				{
					relOpInfo = this.BuildProjection(joinInputNode, scanTableOp.Table.ReferencedColumns);
				}
			}
			else
			{
				relOpInfo = this.EnterExpressionBindingScope(joinInputNode, false);
			}
			return relOpInfo;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x00073AD8 File Offset: 0x00071CD8
		private DbExpression VisitBinaryJoin(Node joinNode, DbExpressionKind joinKind)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.VisitJoinInput(joinNode.Child0);
			CTreeGenerator.RelOpInfo relOpInfo2 = this.VisitJoinInput(joinNode.Child1);
			bool flag = false;
			DbExpression dbExpression;
			if (joinNode.Children.Count > 2)
			{
				flag = true;
				this.PushExpressionBindingScope(relOpInfo);
				this.PushExpressionBindingScope(relOpInfo2);
				dbExpression = base.VisitNode(joinNode.Child2);
			}
			else
			{
				dbExpression = DbExpressionBuilder.True;
			}
			DbExpression dbExpression2 = DbExpressionBuilder.CreateJoinExpressionByKind(joinKind, dbExpression, relOpInfo.CreateBinding(), relOpInfo2.CreateBinding());
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			this.ExitExpressionBindingScope(relOpInfo2, flag);
			relOpInfo2.PublishedVars.PrependProperty(relOpInfo2.PublisherName);
			varInfoList.AddRange(relOpInfo2.PublishedVars);
			this.ExitExpressionBindingScope(relOpInfo, flag);
			relOpInfo.PublishedVars.PrependProperty(relOpInfo.PublisherName);
			varInfoList.AddRange(relOpInfo.PublishedVars);
			this.PublishRelOp(this._joinAliases.Next(), dbExpression2, varInfoList);
			return dbExpression2;
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x00073BB8 File Offset: 0x00071DB8
		public override DbExpression Visit(CrossJoinOp op, Node n)
		{
			List<DbExpressionBinding> list = new List<DbExpressionBinding>();
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			foreach (Node node in n.Children)
			{
				CTreeGenerator.RelOpInfo relOpInfo = this.VisitJoinInput(node);
				list.Add(relOpInfo.CreateBinding());
				this.ExitExpressionBindingScope(relOpInfo, false);
				relOpInfo.PublishedVars.PrependProperty(relOpInfo.PublisherName);
				varInfoList.AddRange(relOpInfo.PublishedVars);
			}
			DbExpression dbExpression = DbExpressionBuilder.CrossJoin(list);
			this.PublishRelOp(this._joinAliases.Next(), dbExpression, varInfoList);
			return dbExpression;
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x00073C70 File Offset: 0x00071E70
		public override DbExpression Visit(InnerJoinOp op, Node n)
		{
			return this.VisitBinaryJoin(n, DbExpressionKind.InnerJoin);
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x00073C7B File Offset: 0x00071E7B
		public override DbExpression Visit(LeftOuterJoinOp op, Node n)
		{
			return this.VisitBinaryJoin(n, DbExpressionKind.LeftOuterJoin);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x00073C86 File Offset: 0x00071E86
		public override DbExpression Visit(FullOuterJoinOp op, Node n)
		{
			return this.VisitBinaryJoin(n, DbExpressionKind.FullOuterJoin);
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x00073C94 File Offset: 0x00071E94
		private DbExpression VisitApply(Node applyNode, DbExpressionKind applyKind)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.EnterExpressionBindingScope(applyNode.Child0);
			CTreeGenerator.RelOpInfo relOpInfo2 = this.EnterExpressionBindingScope(applyNode.Child1, false);
			DbExpression dbExpression = DbExpressionBuilder.CreateApplyExpressionByKind(applyKind, relOpInfo.CreateBinding(), relOpInfo2.CreateBinding());
			this.ExitExpressionBindingScope(relOpInfo2, false);
			this.ExitExpressionBindingScope(relOpInfo);
			relOpInfo.PublishedVars.PrependProperty(relOpInfo.PublisherName);
			relOpInfo2.PublishedVars.PrependProperty(relOpInfo2.PublisherName);
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			varInfoList.AddRange(relOpInfo.PublishedVars);
			varInfoList.AddRange(relOpInfo2.PublishedVars);
			this.PublishRelOp(this._applyAliases.Next(), dbExpression, varInfoList);
			return dbExpression;
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x00073D33 File Offset: 0x00071F33
		public override DbExpression Visit(CrossApplyOp op, Node n)
		{
			return this.VisitApply(n, DbExpressionKind.CrossApply);
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x00073D3D File Offset: 0x00071F3D
		public override DbExpression Visit(OuterApplyOp op, Node n)
		{
			return this.VisitApply(n, DbExpressionKind.OuterApply);
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x00073D48 File Offset: 0x00071F48
		private DbExpression VisitSetOpArgument(Node argNode, VarVec outputVars, VarMap argVars)
		{
			List<Var> list = new List<Var>();
			CTreeGenerator.RelOpInfo relOpInfo;
			if (outputVars.IsEmpty)
			{
				relOpInfo = this.BuildEmptyProjection(argNode);
			}
			else
			{
				foreach (Var var in outputVars)
				{
					list.Add(argVars[var]);
				}
				relOpInfo = this.BuildProjection(argNode, list);
			}
			return relOpInfo.Publisher;
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002795 RID: 10133 RVA: 0x00073DC0 File Offset: 0x00071FC0
		private DbProviderManifest ProviderManifest
		{
			get
			{
				DbProviderManifest dbProviderManifest;
				if ((dbProviderManifest = this._providerManifest) == null)
				{
					dbProviderManifest = (this._providerManifest = ((StoreItemCollection)this._iqtCommand.MetadataWorkspace.GetItemCollection(DataSpace.SSpace)).ProviderManifest);
				}
				return dbProviderManifest;
			}
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x00073DFC File Offset: 0x00071FFC
		private DbExpression VisitSetOp(SetOp op, Node n, AliasGenerator alias, Func<DbExpression, DbExpression, DbExpression> setOpExpressionBuilder)
		{
			CTreeGenerator.AssertBinary(n);
			bool flag = (op.OpType == OpType.UnionAll || op.OpType == OpType.Intersect) && this.ProviderManifest.SupportsIntersectAndUnionAllFlattening();
			DbExpression dbExpression = ((flag && n.Child0.Op.OpType == op.OpType) ? this.VisitSetOp((SetOp)n.Child0.Op, n.Child0, alias, setOpExpressionBuilder) : this.VisitSetOpArgument(n.Child0, op.Outputs, op.VarMap[0]));
			DbExpression dbExpression2 = ((flag && n.Child1.Op.OpType == op.OpType) ? this.VisitSetOp((SetOp)n.Child1.Op, n.Child1, alias, setOpExpressionBuilder) : this.VisitSetOpArgument(n.Child1, op.Outputs, op.VarMap[1]));
			CollectionType edmType = TypeHelpers.GetEdmType<CollectionType>(TypeHelpers.GetCommonTypeUsage(dbExpression.ResultType, dbExpression2.ResultType));
			IEnumerator<EdmProperty> enumerator = null;
			RowType rowType = null;
			if (TypeHelpers.TryGetEdmType<RowType>(edmType.TypeUsage, out rowType))
			{
				enumerator = rowType.Properties.GetEnumerator();
			}
			CTreeGenerator.VarInfoList varInfoList = new CTreeGenerator.VarInfoList();
			foreach (Var var in op.Outputs)
			{
				CTreeGenerator.VarInfo varInfo = new CTreeGenerator.VarInfo(var);
				if (rowType != null)
				{
					if (!enumerator.MoveNext())
					{
						PlanCompiler.Assert(false, "Record columns don't match output vars");
					}
					varInfo.PrependProperty(enumerator.Current.Name);
				}
				varInfoList.Add(varInfo);
			}
			DbExpression dbExpression3 = setOpExpressionBuilder(dbExpression, dbExpression2);
			this.PublishRelOp(alias.Next(), dbExpression3, varInfoList);
			return dbExpression3;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x00073FB4 File Offset: 0x000721B4
		public override DbExpression Visit(UnionAllOp op, Node n)
		{
			return this.VisitSetOp(op, n, this._unionAllAliases, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.UnionAll));
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x00073FD0 File Offset: 0x000721D0
		public override DbExpression Visit(IntersectOp op, Node n)
		{
			return this.VisitSetOp(op, n, this._intersectAliases, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Intersect));
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x00073FEC File Offset: 0x000721EC
		public override DbExpression Visit(ExceptOp op, Node n)
		{
			return this.VisitSetOp(op, n, this._exceptAliases, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Except));
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x00074008 File Offset: 0x00072208
		public override DbExpression Visit(DerefOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x00074010 File Offset: 0x00072210
		public override DbExpression Visit(DistinctOp op, Node n)
		{
			CTreeGenerator.RelOpInfo relOpInfo = this.BuildProjection(n.Child0, op.Keys);
			DbExpression dbExpression = relOpInfo.Publisher.Distinct();
			this.PublishRelOp(this._distinctAliases.Next(), dbExpression, relOpInfo.PublishedVars);
			return dbExpression;
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x00074058 File Offset: 0x00072258
		public override DbExpression Visit(SingleRowOp op, Node n)
		{
			CTreeGenerator.RelOpInfo relOpInfo;
			DbExpression dbExpression;
			if (n.Child0.Op.OpType != OpType.Project)
			{
				ExtendedNodeInfo extendedNodeInfo = this._iqtCommand.GetExtendedNodeInfo(n.Child0);
				if (extendedNodeInfo.Definitions.IsEmpty)
				{
					relOpInfo = this.BuildEmptyProjection(n.Child0);
				}
				else
				{
					relOpInfo = this.BuildProjection(n.Child0, extendedNodeInfo.Definitions);
				}
				dbExpression = relOpInfo.Publisher;
			}
			else
			{
				dbExpression = base.VisitNode(n.Child0);
				this.AssertRelOp(dbExpression);
				relOpInfo = this.ConsumeRelOp(dbExpression);
			}
			DbElementExpression dbElementExpression = dbExpression.Element();
			DbNewInstanceExpression dbNewInstanceExpression = DbExpressionBuilder.NewCollection(new List<DbExpression> { dbElementExpression });
			this.PublishRelOp(this._elementAliases.Next(), dbNewInstanceExpression, relOpInfo.PublishedVars);
			return dbNewInstanceExpression;
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x00074118 File Offset: 0x00072318
		public override DbExpression Visit(SingleRowTableOp op, Node n)
		{
			DbExpression[] array = new DbConstantExpression[] { DbExpressionBuilder.Constant(1) };
			DbNewInstanceExpression dbNewInstanceExpression = DbExpressionBuilder.NewCollection(array);
			this.PublishRelOp(this._singleRowTableAliases.Next(), dbNewInstanceExpression, new CTreeGenerator.VarInfoList());
			return dbNewInstanceExpression;
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x0007415A File Offset: 0x0007235A
		public override DbExpression Visit(VarDefOp op, Node n)
		{
			PlanCompiler.Assert(false, "Unexpected VarDefOp");
			throw new NotSupportedException(Strings.Iqt_CTGen_UnexpectedVarDef);
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x00074171 File Offset: 0x00072371
		public override DbExpression Visit(VarDefListOp op, Node n)
		{
			PlanCompiler.Assert(false, "Unexpected VarDefListOp");
			throw new NotSupportedException(Strings.Iqt_CTGen_UnexpectedVarDefList);
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x00074188 File Offset: 0x00072388
		public override DbExpression Visit(PhysicalProjectOp op, Node n)
		{
			PlanCompiler.Assert(n.Children.Count == 1, "more than one input to physicalProjectOp?");
			VarList varList = new VarList();
			foreach (Var var in op.Outputs)
			{
				if (!varList.Contains(var))
				{
					varList.Add(var);
				}
			}
			op.Outputs.Clear();
			op.Outputs.AddRange(varList);
			return this.BuildProjection(n.Child0, op.Outputs).Publisher;
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x00074230 File Offset: 0x00072430
		public override DbExpression Visit(SingleStreamNestOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x00074237 File Offset: 0x00072437
		public override DbExpression Visit(MultiStreamNestOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000DB1 RID: 3505
		private readonly Command _iqtCommand;

		// Token: 0x04000DB2 RID: 3506
		private readonly DbQueryCommandTree _queryTree;

		// Token: 0x04000DB3 RID: 3507
		private readonly Dictionary<ParameterVar, DbParameterReferenceExpression> _addedParams = new Dictionary<ParameterVar, DbParameterReferenceExpression>();

		// Token: 0x04000DB4 RID: 3508
		private readonly Stack<CTreeGenerator.IqtVarScope> _bindingScopes = new Stack<CTreeGenerator.IqtVarScope>();

		// Token: 0x04000DB5 RID: 3509
		private readonly Stack<CTreeGenerator.VarDefScope> _varScopes = new Stack<CTreeGenerator.VarDefScope>();

		// Token: 0x04000DB6 RID: 3510
		private readonly Dictionary<DbExpression, CTreeGenerator.RelOpInfo> _relOpState = new Dictionary<DbExpression, CTreeGenerator.RelOpInfo>();

		// Token: 0x04000DB7 RID: 3511
		private readonly AliasGenerator _applyAliases = new AliasGenerator("Apply");

		// Token: 0x04000DB8 RID: 3512
		private readonly AliasGenerator _distinctAliases = new AliasGenerator("Distinct");

		// Token: 0x04000DB9 RID: 3513
		private readonly AliasGenerator _exceptAliases = new AliasGenerator("Except");

		// Token: 0x04000DBA RID: 3514
		private readonly AliasGenerator _extentAliases = new AliasGenerator("Extent");

		// Token: 0x04000DBB RID: 3515
		private readonly AliasGenerator _filterAliases = new AliasGenerator("Filter");

		// Token: 0x04000DBC RID: 3516
		private readonly AliasGenerator _groupByAliases = new AliasGenerator("GroupBy");

		// Token: 0x04000DBD RID: 3517
		private readonly AliasGenerator _intersectAliases = new AliasGenerator("Intersect");

		// Token: 0x04000DBE RID: 3518
		private readonly AliasGenerator _joinAliases = new AliasGenerator("Join");

		// Token: 0x04000DBF RID: 3519
		private readonly AliasGenerator _projectAliases = new AliasGenerator("Project");

		// Token: 0x04000DC0 RID: 3520
		private readonly AliasGenerator _sortAliases = new AliasGenerator("Sort");

		// Token: 0x04000DC1 RID: 3521
		private readonly AliasGenerator _unionAllAliases = new AliasGenerator("UnionAll");

		// Token: 0x04000DC2 RID: 3522
		private readonly AliasGenerator _elementAliases = new AliasGenerator("Element");

		// Token: 0x04000DC3 RID: 3523
		private readonly AliasGenerator _singleRowTableAliases = new AliasGenerator("SingleRowTable");

		// Token: 0x04000DC4 RID: 3524
		private readonly AliasGenerator _limitAliases = new AliasGenerator("Limit");

		// Token: 0x04000DC5 RID: 3525
		private readonly AliasGenerator _skipAliases = new AliasGenerator("Skip");

		// Token: 0x04000DC6 RID: 3526
		private DbProviderManifest _providerManifest;

		// Token: 0x020009D5 RID: 2517
		private class VarInfo
		{
			// Token: 0x17001078 RID: 4216
			// (get) Token: 0x06005F87 RID: 24455 RVA: 0x00148E61 File Offset: 0x00147061
			internal Var Var
			{
				get
				{
					return this._var;
				}
			}

			// Token: 0x17001079 RID: 4217
			// (get) Token: 0x06005F88 RID: 24456 RVA: 0x00148E69 File Offset: 0x00147069
			internal List<string> PropertyPath
			{
				get
				{
					return this._propertyChain;
				}
			}

			// Token: 0x06005F89 RID: 24457 RVA: 0x00148E71 File Offset: 0x00147071
			internal VarInfo(Var target)
			{
				this._var = target;
			}

			// Token: 0x06005F8A RID: 24458 RVA: 0x00148E8B File Offset: 0x0014708B
			internal void PrependProperty(string propName)
			{
				this._propertyChain.Insert(0, propName);
			}

			// Token: 0x0400285C RID: 10332
			private readonly Var _var;

			// Token: 0x0400285D RID: 10333
			private readonly List<string> _propertyChain = new List<string>();
		}

		// Token: 0x020009D6 RID: 2518
		private class VarInfoList : List<CTreeGenerator.VarInfo>
		{
			// Token: 0x06005F8B RID: 24459 RVA: 0x00148E9A File Offset: 0x0014709A
			internal VarInfoList()
			{
			}

			// Token: 0x06005F8C RID: 24460 RVA: 0x00148EA2 File Offset: 0x001470A2
			internal VarInfoList(IEnumerable<CTreeGenerator.VarInfo> elements)
				: base(elements)
			{
			}

			// Token: 0x06005F8D RID: 24461 RVA: 0x00148EAC File Offset: 0x001470AC
			internal void PrependProperty(string propName)
			{
				foreach (CTreeGenerator.VarInfo varInfo in this)
				{
					varInfo.PropertyPath.Insert(0, propName);
				}
			}

			// Token: 0x06005F8E RID: 24462 RVA: 0x00148F00 File Offset: 0x00147100
			internal bool TryGetInfo(Var targetVar, out CTreeGenerator.VarInfo varInfo)
			{
				varInfo = null;
				foreach (CTreeGenerator.VarInfo varInfo2 in this)
				{
					if (varInfo2.Var == targetVar)
					{
						varInfo = varInfo2;
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x020009D7 RID: 2519
		private abstract class IqtVarScope
		{
			// Token: 0x06005F8F RID: 24463
			internal abstract bool TryResolveVar(Var targetVar, out DbExpression resultExpr);
		}

		// Token: 0x020009D8 RID: 2520
		private abstract class BindingScope : CTreeGenerator.IqtVarScope
		{
			// Token: 0x06005F91 RID: 24465 RVA: 0x00148F68 File Offset: 0x00147168
			internal BindingScope(IEnumerable<CTreeGenerator.VarInfo> boundVars)
			{
				this._definedVars = new CTreeGenerator.VarInfoList(boundVars);
			}

			// Token: 0x1700107A RID: 4218
			// (get) Token: 0x06005F92 RID: 24466 RVA: 0x00148F7C File Offset: 0x0014717C
			internal CTreeGenerator.VarInfoList PublishedVars
			{
				get
				{
					return this._definedVars;
				}
			}

			// Token: 0x06005F93 RID: 24467 RVA: 0x00148F84 File Offset: 0x00147184
			internal override bool TryResolveVar(Var targetVar, out DbExpression resultExpr)
			{
				resultExpr = null;
				CTreeGenerator.VarInfo varInfo = null;
				if (this._definedVars.TryGetInfo(targetVar, out varInfo))
				{
					resultExpr = this.BindingReference;
					foreach (string text in varInfo.PropertyPath)
					{
						resultExpr = resultExpr.Property(text);
					}
					return true;
				}
				return false;
			}

			// Token: 0x1700107B RID: 4219
			// (get) Token: 0x06005F94 RID: 24468
			protected abstract DbVariableReferenceExpression BindingReference { get; }

			// Token: 0x0400285E RID: 10334
			private readonly CTreeGenerator.VarInfoList _definedVars;
		}

		// Token: 0x020009D9 RID: 2521
		private class RelOpInfo : CTreeGenerator.BindingScope
		{
			// Token: 0x06005F95 RID: 24469 RVA: 0x00148FFC File Offset: 0x001471FC
			internal RelOpInfo(string bindingName, DbExpression publisher, IEnumerable<CTreeGenerator.VarInfo> publishedVars)
				: base(publishedVars)
			{
				PlanCompiler.Assert(TypeSemantics.IsCollectionType(publisher.ResultType), "non-collection type used as RelOpInfo publisher");
				this._binding = publisher.BindAs(bindingName);
			}

			// Token: 0x1700107C RID: 4220
			// (get) Token: 0x06005F96 RID: 24470 RVA: 0x00149027 File Offset: 0x00147227
			internal string PublisherName
			{
				get
				{
					return this._binding.VariableName;
				}
			}

			// Token: 0x1700107D RID: 4221
			// (get) Token: 0x06005F97 RID: 24471 RVA: 0x00149034 File Offset: 0x00147234
			internal DbExpression Publisher
			{
				get
				{
					return this._binding.Expression;
				}
			}

			// Token: 0x06005F98 RID: 24472 RVA: 0x00149041 File Offset: 0x00147241
			internal DbExpressionBinding CreateBinding()
			{
				return this._binding;
			}

			// Token: 0x1700107E RID: 4222
			// (get) Token: 0x06005F99 RID: 24473 RVA: 0x00149049 File Offset: 0x00147249
			protected override DbVariableReferenceExpression BindingReference
			{
				get
				{
					return this._binding.Variable;
				}
			}

			// Token: 0x0400285F RID: 10335
			private readonly DbExpressionBinding _binding;
		}

		// Token: 0x020009DA RID: 2522
		private class GroupByScope : CTreeGenerator.BindingScope
		{
			// Token: 0x06005F9A RID: 24474 RVA: 0x00149056 File Offset: 0x00147256
			internal GroupByScope(DbGroupExpressionBinding binding, IEnumerable<CTreeGenerator.VarInfo> publishedVars)
				: base(publishedVars)
			{
				this._binding = binding;
			}

			// Token: 0x1700107F RID: 4223
			// (get) Token: 0x06005F9B RID: 24475 RVA: 0x00149066 File Offset: 0x00147266
			internal DbGroupExpressionBinding Binding
			{
				get
				{
					return this._binding;
				}
			}

			// Token: 0x06005F9C RID: 24476 RVA: 0x0014906E File Offset: 0x0014726E
			internal void SwitchToGroupReference()
			{
				PlanCompiler.Assert(!this._referenceGroup, "SwitchToGroupReference called more than once on the same GroupByScope?");
				this._referenceGroup = true;
			}

			// Token: 0x17001080 RID: 4224
			// (get) Token: 0x06005F9D RID: 24477 RVA: 0x0014908A File Offset: 0x0014728A
			protected override DbVariableReferenceExpression BindingReference
			{
				get
				{
					if (!this._referenceGroup)
					{
						return this._binding.Variable;
					}
					return this._binding.GroupVariable;
				}
			}

			// Token: 0x04002860 RID: 10336
			private readonly DbGroupExpressionBinding _binding;

			// Token: 0x04002861 RID: 10337
			private bool _referenceGroup;
		}

		// Token: 0x020009DB RID: 2523
		private class VarDefScope : CTreeGenerator.IqtVarScope
		{
			// Token: 0x06005F9E RID: 24478 RVA: 0x001490AB File Offset: 0x001472AB
			internal VarDefScope(Dictionary<Var, DbExpression> definedVars)
			{
				this._definedVars = definedVars;
			}

			// Token: 0x06005F9F RID: 24479 RVA: 0x001490BC File Offset: 0x001472BC
			internal override bool TryResolveVar(Var targetVar, out DbExpression resultExpr)
			{
				resultExpr = null;
				DbExpression dbExpression = null;
				if (this._definedVars.TryGetValue(targetVar, out dbExpression))
				{
					resultExpr = dbExpression;
					return true;
				}
				return false;
			}

			// Token: 0x04002862 RID: 10338
			private readonly Dictionary<Var, DbExpression> _definedVars;
		}
	}
}
