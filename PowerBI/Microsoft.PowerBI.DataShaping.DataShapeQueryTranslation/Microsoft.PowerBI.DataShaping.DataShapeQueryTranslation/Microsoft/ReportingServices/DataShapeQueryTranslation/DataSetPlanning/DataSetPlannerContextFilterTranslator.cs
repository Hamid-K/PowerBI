using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000EE RID: 238
	internal sealed class DataSetPlannerContextFilterTranslator : DataShapeVisitor
	{
		// Token: 0x0600099F RID: 2463 RVA: 0x000249C0 File Offset: 0x00022BC0
		private DataSetPlannerContextFilterTranslator(ScopeTree scopeTree, ExpressionTable expressionTable, DataShapeAnnotations annotations)
		{
			this.m_scopeTree = scopeTree;
			this.m_annotations = annotations;
			this.m_expressionTable = expressionTable;
			this.m_outputExpressionTable = this.m_expressionTable.CopyTable();
			this.m_filtersByTarget = this.m_annotations.FiltersByTarget.ToDictionary((KeyValuePair<IIdentifiable, List<Filter>> k) => k.Key, (KeyValuePair<IIdentifiable, List<Filter>> v) => v.Value);
			this.m_primaryHierarchyMatches = new List<KeyValuePair<DataMember, DataMember>>();
			this.m_secondaryHierarchyMatches = new List<KeyValuePair<DataMember, DataMember>>();
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00024A63 File Offset: 0x00022C63
		private ReadOnlyExpressionTable OutputExpressionTable
		{
			get
			{
				return this.m_outputExpressionTable.AsReadOnly();
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00024A70 File Offset: 0x00022C70
		public static ContextFilterTranslationResult Translate(DataShape dataShape, ScopeTree scopeTree, ExpressionTable expressionTable, DataShapeAnnotations annotations)
		{
			DataSetPlannerContextFilterTranslator dataSetPlannerContextFilterTranslator = new DataSetPlannerContextFilterTranslator(scopeTree, expressionTable, annotations);
			dataSetPlannerContextFilterTranslator.Visit(dataShape);
			ScopeTree scopeTree2 = ScopeTreeBuilder.BuildScopeTree(dataShape);
			Dictionary<IIdentifiable, List<Filter>> dictionary = dataSetPlannerContextFilterTranslator.RecreateFilterTable(scopeTree2);
			dataSetPlannerContextFilterTranslator.RewriteFilterExpression(scopeTree2);
			return new ContextFilterTranslationResult(scopeTree2, dataShape, dictionary, dataSetPlannerContextFilterTranslator.OutputExpressionTable);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00024AB4 File Offset: 0x00022CB4
		protected override void Visit(DataShape dataShape)
		{
			if (dataShape.IsIndependent && this.m_currentDataShape != null)
			{
				return;
			}
			DataShape currentDataShape = this.m_currentDataShape;
			this.m_currentDataShape = dataShape;
			DataShape currentFilterContextDataShape = this.m_currentFilterContextDataShape;
			if (this.m_annotations.TryGetFilterContextDataShape(this.m_currentDataShape, out this.m_currentFilterContextDataShape))
			{
				IList<DataMember> list = this.m_currentFilterContextDataShape.PrimaryHierarchy.GetAllDynamicMembers().Evaluate<DataMember>();
				IList<DataMember> list2 = this.m_currentFilterContextDataShape.SecondaryHierarchy.GetAllDynamicMembers().Evaluate<DataMember>();
				IList<DataMember> list3 = this.m_currentDataShape.PrimaryHierarchy.GetAllDynamicMembers().Evaluate<DataMember>();
				IList<DataMember> list4 = this.m_currentDataShape.SecondaryHierarchy.GetAllDynamicMembers().Evaluate<DataMember>();
				this.MarkAsContextOnly(list);
				this.MarkAsContextOnly(list2);
				this.Merge(list3, list, this.m_primaryHierarchyMatches);
				this.Merge(list4, list2, this.m_secondaryHierarchyMatches);
				Filter filter = this.m_currentFilterContextDataShape.Filters.EmptyIfNull<Filter>().SingleOrDefault((Filter f) => f.Condition.ObjectType != ObjectType.FilterEmptyGroupsCondition);
				if (filter != null)
				{
					this.m_filterMergeInfo = new FilterMergeInfo(filter, this.m_currentDataShape);
				}
			}
			base.Visit(dataShape);
			this.m_currentDataShape = currentDataShape;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00024BE4 File Offset: 0x00022DE4
		private void MarkAsContextOnly(IList<DataMember> members)
		{
			foreach (DataMember dataMember in members)
			{
				dataMember.ContextOnly = true;
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00024C2C File Offset: 0x00022E2C
		private void Merge(IList<DataMember> dynamics, IList<DataMember> dynamicsToMerge, List<KeyValuePair<DataMember, DataMember>> matches)
		{
			int count = dynamics.Count;
			int count2 = dynamicsToMerge.Count;
			for (int i = 0; i < count; i++)
			{
				matches.Add(new KeyValuePair<DataMember, DataMember>(dynamicsToMerge[i], dynamics[i]));
			}
			if (count < count2)
			{
				KeyValuePair<DataMember, DataMember> keyValuePair = matches.Last<KeyValuePair<DataMember, DataMember>>();
				List<DataMember> dataMembers = keyValuePair.Value.DataMembers;
				keyValuePair.Value.DataMembers = ContextFilterTranslatorUtils.RemoveStaticMembers(keyValuePair.Key.DataMembers);
				if (dataMembers != null && dataMembers.Any<DataMember>())
				{
					keyValuePair.Key.GetAllMembersDepthFirst().Last<DataMember>().DataMembers = dataMembers;
				}
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00024CC7 File Offset: 0x00022EC7
		protected override void Exit(DataShape dataShape)
		{
			this.MergeFilters();
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00024CD0 File Offset: 0x00022ED0
		private void MergeFilters()
		{
			if (this.m_currentFilterContextDataShape == null)
			{
				return;
			}
			if (this.m_filterMergeInfo != null)
			{
				if (this.m_currentDataShape.Filters == null)
				{
					this.m_currentDataShape.Filters = new List<Filter>();
				}
				if (this.m_filterMergeInfo.TargetDataShape == this.m_currentDataShape)
				{
					this.m_currentDataShape.Filters.Add(this.m_filterMergeInfo.FilterToMerge);
					this.m_currentFilterContextDataShape.Filters.Remove(this.m_filterMergeInfo.FilterToMerge);
				}
			}
			Filter filter = this.m_currentDataShape.Filters.SingleOrDefault((Filter f) => f.Condition.ObjectType == ObjectType.ContextFilterCondition);
			if (filter != null)
			{
				this.m_currentDataShape.Filters.Remove(filter);
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00024D9C File Offset: 0x00022F9C
		private Dictionary<IIdentifiable, List<Filter>> RecreateFilterTable(ScopeTree scopeTreeAfterTranslation)
		{
			if (this.m_filterMergeInfo == null || this.m_currentFilterContextDataShape == null)
			{
				return null;
			}
			IScope targetScope = this.m_filterMergeInfo.FilterToMerge.Target.GetResolvedScope(this.m_expressionTable);
			IScope scope = null;
			bool flag = false;
			if (targetScope.ObjectType == ObjectType.DataIntersection)
			{
				flag = true;
				scope = this.GetIntersectionScopeAfterTranslation((DataIntersection)targetScope, scopeTreeAfterTranslation);
			}
			else if (targetScope.ObjectType == ObjectType.DataMember)
			{
				DataMember dataMember;
				if (this.m_annotations.IsPrimaryMember((DataMember)targetScope))
				{
					dataMember = (from v in this.m_primaryHierarchyMatches
						where v.Key == targetScope
						select v.Value).SingleOrDefault<DataMember>();
				}
				else
				{
					dataMember = (from v in this.m_secondaryHierarchyMatches
						where v.Key == targetScope
						select v.Value).SingleOrDefault<DataMember>();
				}
				if (dataMember != null)
				{
					flag = true;
					scope = dataMember;
				}
			}
			if (flag)
			{
				this.m_filtersByTarget.Remove(targetScope);
				this.m_filtersByTarget.Add(scope, new List<Filter> { this.m_filterMergeInfo.FilterToMerge });
				this.m_outputExpressionTable.SetNode(this.m_filterMergeInfo.FilterToMerge.Target, new ResolvedScopeReferenceExpressionNode(scope));
			}
			return this.m_filtersByTarget;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00024F24 File Offset: 0x00023124
		private IScope GetIntersectionScopeAfterTranslation(DataIntersection intersectionScopeBeforeTranslation, ScopeTree scopeTreeAfterTranslation)
		{
			IScope primaryParent = this.m_scopeTree.GetPrimaryParentScope(intersectionScopeBeforeTranslation);
			IScope secondaryParent = this.m_scopeTree.GetSecondaryParentScope(intersectionScopeBeforeTranslation);
			DataMember dataMember = (from v in this.m_primaryHierarchyMatches
				where v.Key == primaryParent
				select v.Value).SingleOrDefault<DataMember>();
			DataMember dataMember2 = (from v in this.m_secondaryHierarchyMatches
				where v.Key == secondaryParent
				select v.Value).SingleOrDefault<DataMember>();
			DataMember dataMember3 = ((dataMember == null) ? ((DataMember)primaryParent) : dataMember);
			DataMember dataMember4 = ((dataMember2 == null) ? ((DataMember)secondaryParent) : dataMember2);
			IScope scope;
			scopeTreeAfterTranslation.TryGetIntersectionScope(dataMember3, dataMember4, out scope);
			return scope;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00025010 File Offset: 0x00023210
		private void RewriteFilterExpression(ScopeTree scopeTreeAfterTranslation)
		{
			if (this.m_filterMergeInfo == null || this.m_currentFilterContextDataShape == null)
			{
				return;
			}
			ScopeFilterExpressionTranslator.RewriteFilterExpression(this.m_filterMergeInfo.FilterToMerge, new Func<DataIntersection, ScopeTree, IScope>(this.GetIntersectionScopeAfterTranslation), scopeTreeAfterTranslation, this.m_outputExpressionTable);
		}

		// Token: 0x0400048C RID: 1164
		private readonly ScopeTree m_scopeTree;

		// Token: 0x0400048D RID: 1165
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x0400048E RID: 1166
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x0400048F RID: 1167
		private readonly WritableExpressionTable m_outputExpressionTable;

		// Token: 0x04000490 RID: 1168
		private readonly Dictionary<IIdentifiable, List<Filter>> m_filtersByTarget;

		// Token: 0x04000491 RID: 1169
		private readonly List<KeyValuePair<DataMember, DataMember>> m_primaryHierarchyMatches;

		// Token: 0x04000492 RID: 1170
		private readonly List<KeyValuePair<DataMember, DataMember>> m_secondaryHierarchyMatches;

		// Token: 0x04000493 RID: 1171
		private DataShape m_currentDataShape;

		// Token: 0x04000494 RID: 1172
		private DataShape m_currentFilterContextDataShape;

		// Token: 0x04000495 RID: 1173
		private FilterMergeInfo m_filterMergeInfo;
	}
}
