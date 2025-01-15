using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Mapping.ViewGeneration.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000575 RID: 1397
	internal class RewritingValidator
	{
		// Token: 0x060043C5 RID: 17349 RVA: 0x000EC56C File Offset: 0x000EA76C
		internal RewritingValidator(ViewgenContext context, CellTreeNode basicView)
		{
			this._viewgenContext = context;
			this._basicView = basicView;
			this._domainMap = this._viewgenContext.MemberMaps.UpdateDomainMap;
			this._keyAttributes = MemberPath.GetKeyMembers(this._viewgenContext.Extent, this._domainMap);
			this._errorLog = new ErrorLog();
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x000EC5CC File Offset: 0x000EA7CC
		internal void Validate()
		{
			Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> dictionary = this.CreateMemberValueTrees(false);
			Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> dictionary2 = this.CreateMemberValueTrees(true);
			RewritingValidator.WhereClauseVisitor whereClauseVisitor = new RewritingValidator.WhereClauseVisitor(this._basicView, dictionary);
			RewritingValidator.WhereClauseVisitor whereClauseVisitor2 = new RewritingValidator.WhereClauseVisitor(this._basicView, dictionary2);
			foreach (LeftCellWrapper leftCellWrapper in this._viewgenContext.AllWrappersForExtent)
			{
				Cell onlyInputCell = leftCellWrapper.OnlyInputCell;
				CellTreeNode cellTreeNode = new LeafCellTreeNode(this._viewgenContext, leftCellWrapper);
				CellTreeNode cellTreeNode2 = whereClauseVisitor2.GetCellTreeNode(onlyInputCell.SQuery.WhereClause);
				if (cellTreeNode2 != null)
				{
					CellTreeNode cellTreeNode3;
					if (cellTreeNode2 != this._basicView)
					{
						cellTreeNode3 = new OpCellTreeNode(this._viewgenContext, CellTreeOpType.IJ, new CellTreeNode[] { cellTreeNode2, this._basicView });
					}
					else
					{
						cellTreeNode3 = this._basicView;
					}
					BoolExpression boolExpression = BoolExpression.CreateLiteral(leftCellWrapper.CreateRoleBoolean(), this._viewgenContext.MemberMaps.QueryDomainMap);
					BoolExpression boolExpression2;
					if (!this.CheckEquivalence(cellTreeNode.RightFragmentQuery, cellTreeNode3.RightFragmentQuery, boolExpression, out boolExpression2))
					{
						object obj = StringUtil.FormatInvariant("{0}", new object[] { this._viewgenContext.Extent });
						cellTreeNode.RightFragmentQuery.Condition.ExpensiveSimplify();
						cellTreeNode3.RightFragmentQuery.Condition.ExpensiveSimplify();
						string text = Strings.ViewGen_CQ_PartitionConstraint(obj);
						this.ReportConstraintViolation(text, boolExpression2, ViewGenErrorCode.PartitionConstraintViolation, cellTreeNode.GetLeaves().Concat(cellTreeNode3.GetLeaves()));
					}
					CellTreeNode cellTreeNode4 = whereClauseVisitor.GetCellTreeNode(onlyInputCell.SQuery.WhereClause);
					if (cellTreeNode4 != null)
					{
						RewritingValidator.DomainConstraintVisitor.CheckConstraints(cellTreeNode4, leftCellWrapper, this._viewgenContext, this._errorLog);
						if (this._errorLog.Count > 0)
						{
							continue;
						}
						this.CheckConstraintsOnProjectedConditionMembers(dictionary, leftCellWrapper, cellTreeNode3, boolExpression);
						if (this._errorLog.Count > 0)
						{
							continue;
						}
					}
					this.CheckConstraintsOnNonNullableMembers(leftCellWrapper);
				}
			}
			if (this._errorLog.Count > 0)
			{
				ExceptionHelpers.ThrowMappingException(this._errorLog, this._viewgenContext.Config);
			}
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x000EC7E8 File Offset: 0x000EA9E8
		private bool CheckEquivalence(FragmentQuery cQuery, FragmentQuery sQuery, BoolExpression inExtentCondition, out BoolExpression unsatisfiedConstraint)
		{
			FragmentQuery fragmentQuery = this._viewgenContext.RightFragmentQP.Difference(cQuery, sQuery);
			FragmentQuery fragmentQuery2 = this._viewgenContext.RightFragmentQP.Difference(sQuery, cQuery);
			FragmentQuery fragmentQuery3 = FragmentQuery.Create(BoolExpression.CreateAnd(new BoolExpression[] { fragmentQuery.Condition, inExtentCondition }));
			FragmentQuery fragmentQuery4 = FragmentQuery.Create(BoolExpression.CreateAnd(new BoolExpression[] { fragmentQuery2.Condition, inExtentCondition }));
			unsatisfiedConstraint = null;
			bool flag = true;
			bool flag2 = true;
			if (this._viewgenContext.RightFragmentQP.IsSatisfiable(fragmentQuery3))
			{
				unsatisfiedConstraint = fragmentQuery3.Condition;
				flag = false;
			}
			if (this._viewgenContext.RightFragmentQP.IsSatisfiable(fragmentQuery4))
			{
				unsatisfiedConstraint = fragmentQuery4.Condition;
				flag2 = false;
			}
			if (flag && flag2)
			{
				return true;
			}
			unsatisfiedConstraint.ExpensiveSimplify();
			return false;
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x000EC8B4 File Offset: 0x000EAAB4
		private void ReportConstraintViolation(string message, BoolExpression extraConstraint, ViewGenErrorCode errorCode, IEnumerable<LeftCellWrapper> relevantWrappers)
		{
			if (ErrorPatternMatcher.FindMappingErrors(this._viewgenContext, this._domainMap, this._errorLog))
			{
				return;
			}
			extraConstraint.ExpensiveSimplify();
			HashSet<LeftCellWrapper> hashSet = new HashSet<LeftCellWrapper>(relevantWrappers);
			new List<LeftCellWrapper>(hashSet).Sort(LeftCellWrapper.OriginalCellIdComparer);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(message);
			RewritingValidator.EntityConfigurationToUserString(extraConstraint, stringBuilder);
			this._errorLog.AddEntry(new ErrorLog.Record(errorCode, stringBuilder.ToString(), hashSet, ""));
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x000EC92C File Offset: 0x000EAB2C
		private Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> CreateMemberValueTrees(bool complementElse)
		{
			Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> dictionary = new Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode>();
			foreach (MemberPath memberPath in this._domainMap.ConditionMembers(this._viewgenContext.Extent))
			{
				List<Constant> list = new List<Constant>(this._domainMap.GetDomain(memberPath));
				OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this._viewgenContext, CellTreeOpType.Union);
				for (int i = 0; i < list.Count; i++)
				{
					Constant constant = list[i];
					RewritingValidator.MemberValueBinding memberValueBinding = new RewritingValidator.MemberValueBinding(memberPath, constant);
					FragmentQuery fragmentQuery = QueryRewriter.CreateMemberConditionQuery(memberPath, constant, this._keyAttributes, this._domainMap);
					Tile<FragmentQuery> tile;
					if (this._viewgenContext.TryGetCachedRewriting(fragmentQuery, out tile))
					{
						CellTreeNode cellTreeNode = QueryRewriter.TileToCellTree(tile, this._viewgenContext);
						dictionary[memberValueBinding] = cellTreeNode;
						if (i < list.Count - 1)
						{
							opCellTreeNode.Add(cellTreeNode);
						}
					}
				}
				if (complementElse && list.Count > 1)
				{
					Constant constant2 = list[list.Count - 1];
					RewritingValidator.MemberValueBinding memberValueBinding2 = new RewritingValidator.MemberValueBinding(memberPath, constant2);
					dictionary[memberValueBinding2] = new OpCellTreeNode(this._viewgenContext, CellTreeOpType.LASJ, new CellTreeNode[] { this._basicView, opCellTreeNode });
				}
			}
			return dictionary;
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x000ECA88 File Offset: 0x000EAC88
		private void CheckConstraintsOnProjectedConditionMembers(Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> memberValueTrees, LeftCellWrapper wrapper, CellTreeNode sQueryTree, BoolExpression inExtentCondition)
		{
			foreach (MemberPath memberPath in this._domainMap.ConditionMembers(this._viewgenContext.Extent))
			{
				int num = this._viewgenContext.MemberMaps.ProjectedSlotMap.IndexOf(memberPath);
				MemberProjectedSlot memberProjectedSlot = wrapper.RightCellQuery.ProjectedSlotAt(num) as MemberProjectedSlot;
				if (memberProjectedSlot != null)
				{
					foreach (Constant constant in this._domainMap.GetDomain(memberPath))
					{
						CellTreeNode cellTreeNode;
						if (memberValueTrees.TryGetValue(new RewritingValidator.MemberValueBinding(memberPath, constant), out cellTreeNode))
						{
							FragmentQuery fragmentQuery = FragmentQuery.Create(RewritingValidator.PropagateCellConstantsToWhereClause(wrapper, wrapper.RightCellQuery.WhereClause, constant, memberPath, this._viewgenContext.MemberMaps));
							CellTreeNode cellTreeNode2 = ((sQueryTree == this._basicView) ? cellTreeNode : new OpCellTreeNode(this._viewgenContext, CellTreeOpType.IJ, new CellTreeNode[] { cellTreeNode, sQueryTree }));
							BoolExpression boolExpression;
							if (!this.CheckEquivalence(fragmentQuery, cellTreeNode2.RightFragmentQuery, inExtentCondition, out boolExpression))
							{
								string text = Strings.ViewGen_CQ_DomainConstraint(memberProjectedSlot.ToUserString());
								this.ReportConstraintViolation(text, boolExpression, ViewGenErrorCode.DomainConstraintViolation, cellTreeNode2.GetLeaves().Concat(new LeftCellWrapper[] { wrapper }));
							}
						}
					}
				}
			}
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x000ECC20 File Offset: 0x000EAE20
		internal static BoolExpression PropagateCellConstantsToWhereClause(LeftCellWrapper wrapper, BoolExpression expression, Constant constant, MemberPath member, MemberMaps memberMaps)
		{
			MemberProjectedSlot csideMappedSlotForSMember = wrapper.GetCSideMappedSlotForSMember(member);
			if (csideMappedSlotForSMember == null)
			{
				return expression;
			}
			NegatedConstant negatedConstant = constant as NegatedConstant;
			IEnumerable<Constant> domain = memberMaps.QueryDomainMap.GetDomain(csideMappedSlotForSMember.MemberPath);
			Set<Constant> set = new Set<Constant>(Constant.EqualityComparer);
			if (negatedConstant != null)
			{
				set.Unite(domain);
				set.Difference(negatedConstant.Elements);
			}
			else
			{
				set.Add(constant);
			}
			MemberRestriction memberRestriction = new ScalarRestriction(csideMappedSlotForSMember.MemberPath, set, domain);
			return BoolExpression.CreateAnd(new BoolExpression[]
			{
				expression,
				BoolExpression.CreateLiteral(memberRestriction, memberMaps.QueryDomainMap)
			});
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x000ECCB0 File Offset: 0x000EAEB0
		private static FragmentQuery AddNullConditionOnCSideFragment(LeftCellWrapper wrapper, MemberPath member, MemberMaps memberMaps)
		{
			MemberProjectedSlot csideMappedSlotForSMember = wrapper.GetCSideMappedSlotForSMember(member);
			if (csideMappedSlotForSMember == null || !csideMappedSlotForSMember.MemberPath.IsNullable)
			{
				return null;
			}
			BoolExpression whereClause = wrapper.RightCellQuery.WhereClause;
			IEnumerable<Constant> domain = memberMaps.QueryDomainMap.GetDomain(csideMappedSlotForSMember.MemberPath);
			Set<Constant> set = new Set<Constant>(Constant.EqualityComparer);
			set.Add(Constant.Null);
			MemberRestriction memberRestriction = new ScalarRestriction(csideMappedSlotForSMember.MemberPath, set, domain);
			return FragmentQuery.Create(BoolExpression.CreateAnd(new BoolExpression[]
			{
				whereClause,
				BoolExpression.CreateLiteral(memberRestriction, memberMaps.QueryDomainMap)
			}));
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x000ECD40 File Offset: 0x000EAF40
		private void CheckConstraintsOnNonNullableMembers(LeftCellWrapper wrapper)
		{
			foreach (MemberPath memberPath in this._domainMap.NonConditionMembers(this._viewgenContext.Extent))
			{
				bool flag = memberPath.EdmType is SimpleType;
				if (!memberPath.IsNullable && flag)
				{
					FragmentQuery fragmentQuery = RewritingValidator.AddNullConditionOnCSideFragment(wrapper, memberPath, this._viewgenContext.MemberMaps);
					if (fragmentQuery != null && this._viewgenContext.RightFragmentQP.IsSatisfiable(fragmentQuery))
					{
						this._errorLog.AddEntry(new ErrorLog.Record(ViewGenErrorCode.NullableMappingForNonNullableColumn, Strings.Viewgen_NullableMappingForNonNullableColumn(wrapper.LeftExtent.ToString(), memberPath.ToFullString()), wrapper.Cells, ""));
					}
				}
			}
		}

		// Token: 0x060043CE RID: 17358 RVA: 0x000ECE18 File Offset: 0x000EB018
		internal static void EntityConfigurationToUserString(BoolExpression condition, StringBuilder builder)
		{
			RewritingValidator.EntityConfigurationToUserString(condition, builder, true);
		}

		// Token: 0x060043CF RID: 17359 RVA: 0x000ECE22 File Offset: 0x000EB022
		internal static void EntityConfigurationToUserString(BoolExpression condition, StringBuilder builder, bool writeRoundTrippingMessage)
		{
			condition.AsUserString(builder, "PK", writeRoundTrippingMessage);
		}

		// Token: 0x04001879 RID: 6265
		private readonly ViewgenContext _viewgenContext;

		// Token: 0x0400187A RID: 6266
		private readonly MemberDomainMap _domainMap;

		// Token: 0x0400187B RID: 6267
		private readonly CellTreeNode _basicView;

		// Token: 0x0400187C RID: 6268
		private readonly IEnumerable<MemberPath> _keyAttributes;

		// Token: 0x0400187D RID: 6269
		private readonly ErrorLog _errorLog;

		// Token: 0x02000B83 RID: 2947
		private class WhereClauseVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, CellTreeNode>
		{
			// Token: 0x0600666B RID: 26219 RVA: 0x0015F748 File Offset: 0x0015D948
			internal WhereClauseVisitor(CellTreeNode topLevelTree, Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> memberValueTrees)
			{
				this._topLevelTree = topLevelTree;
				this._memberValueTrees = memberValueTrees;
				this._viewgenContext = topLevelTree.ViewgenContext;
			}

			// Token: 0x0600666C RID: 26220 RVA: 0x0015F76A File Offset: 0x0015D96A
			internal CellTreeNode GetCellTreeNode(BoolExpression whereClause)
			{
				return whereClause.Tree.Accept<CellTreeNode>(this);
			}

			// Token: 0x0600666D RID: 26221 RVA: 0x0015F778 File Offset: 0x0015D978
			internal override CellTreeNode VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				IEnumerable<CellTreeNode> enumerable = this.AcceptChildren(expression.Children);
				OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this._viewgenContext, CellTreeOpType.IJ);
				foreach (CellTreeNode cellTreeNode in enumerable)
				{
					if (cellTreeNode == null)
					{
						return null;
					}
					if (cellTreeNode != this._topLevelTree)
					{
						opCellTreeNode.Add(cellTreeNode);
					}
				}
				if (opCellTreeNode.Children.Count != 0)
				{
					return opCellTreeNode;
				}
				return this._topLevelTree;
			}

			// Token: 0x0600666E RID: 26222 RVA: 0x0015F800 File Offset: 0x0015DA00
			internal override CellTreeNode VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this._topLevelTree;
			}

			// Token: 0x0600666F RID: 26223 RVA: 0x0015F808 File Offset: 0x0015DA08
			internal override CellTreeNode VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				MemberRestriction memberRestriction = (MemberRestriction)expression.Identifier.Variable.Identifier;
				Set<Constant> range = expression.Identifier.Range;
				OpCellTreeNode opCellTreeNode = new OpCellTreeNode(this._viewgenContext, CellTreeOpType.Union);
				CellTreeNode cellTreeNode = null;
				foreach (Constant constant in range)
				{
					if (this.TryGetCellTreeNode(memberRestriction.RestrictedMemberSlot.MemberPath, constant, out cellTreeNode))
					{
						opCellTreeNode.Add(cellTreeNode);
					}
				}
				int count = opCellTreeNode.Children.Count;
				if (count == 0)
				{
					return null;
				}
				if (count != 1)
				{
					return opCellTreeNode;
				}
				return cellTreeNode;
			}

			// Token: 0x06006670 RID: 26224 RVA: 0x0015F8BC File Offset: 0x0015DABC
			internal override CellTreeNode VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06006671 RID: 26225 RVA: 0x0015F8C3 File Offset: 0x0015DAC3
			internal override CellTreeNode VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06006672 RID: 26226 RVA: 0x0015F8CA File Offset: 0x0015DACA
			internal override CellTreeNode VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06006673 RID: 26227 RVA: 0x0015F8D1 File Offset: 0x0015DAD1
			private bool TryGetCellTreeNode(MemberPath memberPath, Constant value, out CellTreeNode singleNode)
			{
				return this._memberValueTrees.TryGetValue(new RewritingValidator.MemberValueBinding(memberPath, value), out singleNode);
			}

			// Token: 0x06006674 RID: 26228 RVA: 0x0015F8E6 File Offset: 0x0015DAE6
			private IEnumerable<CellTreeNode> AcceptChildren(IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> children)
			{
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in children)
				{
					yield return boolExpr.Accept<CellTreeNode>(this);
				}
				IEnumerator<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x04002DF7 RID: 11767
			private readonly ViewgenContext _viewgenContext;

			// Token: 0x04002DF8 RID: 11768
			private readonly CellTreeNode _topLevelTree;

			// Token: 0x04002DF9 RID: 11769
			private readonly Dictionary<RewritingValidator.MemberValueBinding, CellTreeNode> _memberValueTrees;
		}

		// Token: 0x02000B84 RID: 2948
		internal class DomainConstraintVisitor : CellTreeNode.SimpleCellTreeVisitor<bool, bool>
		{
			// Token: 0x06006675 RID: 26229 RVA: 0x0015F8FD File Offset: 0x0015DAFD
			private DomainConstraintVisitor(LeftCellWrapper wrapper, ViewgenContext context, ErrorLog errorLog)
			{
				this.m_wrapper = wrapper;
				this.m_viewgenContext = context;
				this.m_errorLog = errorLog;
			}

			// Token: 0x06006676 RID: 26230 RVA: 0x0015F91C File Offset: 0x0015DB1C
			internal static void CheckConstraints(CellTreeNode node, LeftCellWrapper wrapper, ViewgenContext context, ErrorLog errorLog)
			{
				RewritingValidator.DomainConstraintVisitor domainConstraintVisitor = new RewritingValidator.DomainConstraintVisitor(wrapper, context, errorLog);
				node.Accept<bool, bool>(domainConstraintVisitor, true);
			}

			// Token: 0x06006677 RID: 26231 RVA: 0x0015F93C File Offset: 0x0015DB3C
			internal override bool VisitLeaf(LeafCellTreeNode node, bool dummy)
			{
				CellQuery rightCellQuery = this.m_wrapper.RightCellQuery;
				CellQuery rightCellQuery2 = node.LeftCellWrapper.RightCellQuery;
				List<MemberPath> list = new List<MemberPath>();
				if (rightCellQuery != rightCellQuery2)
				{
					for (int i = 0; i < rightCellQuery.NumProjectedSlots; i++)
					{
						MemberProjectedSlot memberProjectedSlot = rightCellQuery.ProjectedSlotAt(i) as MemberProjectedSlot;
						if (memberProjectedSlot != null)
						{
							MemberProjectedSlot memberProjectedSlot2 = rightCellQuery2.ProjectedSlotAt(i) as MemberProjectedSlot;
							if (memberProjectedSlot2 != null)
							{
								MemberPath memberPath = this.m_viewgenContext.MemberMaps.ProjectedSlotMap[i];
								if (!memberPath.IsPartOfKey && !MemberPath.EqualityComparer.Equals(memberProjectedSlot.MemberPath, memberProjectedSlot2.MemberPath))
								{
									list.Add(memberPath);
								}
							}
						}
					}
				}
				if (list.Count > 0)
				{
					string text = Strings.ViewGen_NonKeyProjectedWithOverlappingPartitions(MemberPath.PropertiesToUserString(list, false));
					ErrorLog.Record record = new ErrorLog.Record(ViewGenErrorCode.NonKeyProjectedWithOverlappingPartitions, text, new LeftCellWrapper[] { this.m_wrapper, node.LeftCellWrapper }, string.Empty);
					this.m_errorLog.AddEntry(record);
				}
				return true;
			}

			// Token: 0x06006678 RID: 26232 RVA: 0x0015FA34 File Offset: 0x0015DC34
			internal override bool VisitOpNode(OpCellTreeNode node, bool dummy)
			{
				if (node.OpType == CellTreeOpType.LASJ)
				{
					node.Children[0].Accept<bool, bool>(this, dummy);
				}
				else
				{
					foreach (CellTreeNode cellTreeNode in node.Children)
					{
						cellTreeNode.Accept<bool, bool>(this, dummy);
					}
				}
				return true;
			}

			// Token: 0x04002DFA RID: 11770
			private readonly LeftCellWrapper m_wrapper;

			// Token: 0x04002DFB RID: 11771
			private readonly ViewgenContext m_viewgenContext;

			// Token: 0x04002DFC RID: 11772
			private readonly ErrorLog m_errorLog;
		}

		// Token: 0x02000B85 RID: 2949
		private struct MemberValueBinding : IEquatable<RewritingValidator.MemberValueBinding>
		{
			// Token: 0x06006679 RID: 26233 RVA: 0x0015FAA8 File Offset: 0x0015DCA8
			public MemberValueBinding(MemberPath member, Constant value)
			{
				this.Member = member;
				this.Value = value;
			}

			// Token: 0x0600667A RID: 26234 RVA: 0x0015FAB8 File Offset: 0x0015DCB8
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[] { this.Member, this.Value });
			}

			// Token: 0x0600667B RID: 26235 RVA: 0x0015FAE1 File Offset: 0x0015DCE1
			public bool Equals(RewritingValidator.MemberValueBinding other)
			{
				return MemberPath.EqualityComparer.Equals(this.Member, other.Member) && Constant.EqualityComparer.Equals(this.Value, other.Value);
			}

			// Token: 0x04002DFD RID: 11773
			internal readonly MemberPath Member;

			// Token: 0x04002DFE RID: 11774
			internal readonly Constant Value;
		}
	}
}
