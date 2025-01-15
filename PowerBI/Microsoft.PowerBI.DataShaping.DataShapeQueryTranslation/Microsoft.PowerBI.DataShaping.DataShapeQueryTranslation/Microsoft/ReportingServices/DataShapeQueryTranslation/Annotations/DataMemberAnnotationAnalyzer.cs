using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000242 RID: 578
	internal sealed class DataMemberAnnotationAnalyzer : DataShapeVisitor
	{
		// Token: 0x060013B9 RID: 5049 RVA: 0x0004CC38 File Offset: 0x0004AE38
		private DataMemberAnnotationAnalyzer(ExpressionTable expressionTable, ScopeTree scopeTree, DataShape outerDataShape, BatchSubtotalAnnotations subtotalAnnotations)
		{
			this.m_expressionTable = expressionTable;
			this.m_scopeTree = scopeTree;
			this.m_parents = new Stack<IIdentifiable>();
			this.m_dataMemberAnnotations = new Dictionary<DataMember, DataMemberAnnotation>();
			this.m_subtotalAnnotations = subtotalAnnotations;
			this.m_innermostScopesInDataShape = new Stack<IScope>();
			this.m_innermostScopesInDataShape.Push(this.ComputeInnermostScopeInDataShape(outerDataShape));
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0004CC94 File Offset: 0x0004AE94
		public static DataMemberAnnotations Analyze(DataShape dataShape, ScopeTree scopeTree, ExpressionTable expressionTable, BatchSubtotalAnnotations subtotalAnnotations)
		{
			DataMemberAnnotationAnalyzer dataMemberAnnotationAnalyzer = new DataMemberAnnotationAnalyzer(expressionTable, scopeTree, dataShape, subtotalAnnotations);
			dataMemberAnnotationAnalyzer.Visit(dataShape);
			return new DataMemberAnnotations(dataMemberAnnotationAnalyzer.m_dataMemberAnnotations);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0004CCB0 File Offset: 0x0004AEB0
		protected override void Enter(DataShape dataShape)
		{
			this.m_parents.Push(dataShape);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0004CCC0 File Offset: 0x0004AEC0
		protected override void Visit(DataShape dataShape)
		{
			int leafIndex = this.m_leafIndex;
			Dictionary<DataMember, List<global::System.ValueTuple<Limit, bool>>> limits = this.m_limits;
			this.m_limits = this.ComputeMemberLimits(dataShape.Limits);
			if (dataShape.IsIndependent)
			{
				this.m_innermostScopesInDataShape.Push(this.ComputeInnermostScopeInDataShape(dataShape));
			}
			base.Visit(dataShape);
			if (dataShape.IsIndependent)
			{
				this.m_innermostScopesInDataShape.Pop();
			}
			this.m_leafIndex = leafIndex;
			this.m_limits = limits;
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0004CD30 File Offset: 0x0004AF30
		protected override void Exit(DataShape dataShape)
		{
			this.m_parents.Pop();
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0004CD40 File Offset: 0x0004AF40
		protected override void TraverseDataShapeStructure(DataShape dataShape)
		{
			bool inPrimaryHierarchy = this.m_inPrimaryHierarchy;
			this.m_inPrimaryHierarchy = true;
			this.m_leafIndex = -1;
			base.Visit(dataShape.PrimaryHierarchy);
			this.m_inPrimaryHierarchy = false;
			this.m_leafIndex = -1;
			base.Visit(dataShape.SecondaryHierarchy);
			this.m_inPrimaryHierarchy = inPrimaryHierarchy;
			base.Visit<DataRow>(dataShape.DataRows, new Action<DataRow>(base.Visit));
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0004CDA8 File Offset: 0x0004AFA8
		protected override void Enter(DataMember dataMember)
		{
			Group group = dataMember.Group;
			SortByMeasureInfoCollection sortByMeasureInfoCollection = null;
			if (dataMember.IsDynamic)
			{
				IScope sortByMeasureInnermostScope = this.GetSortByMeasureInnermostScope(this.m_innermostScopesInDataShape.Peek());
				bool flag = BatchDataSetPlanningUtils.AreEquivalentScopes(dataMember, sortByMeasureInnermostScope, this.m_scopeTree);
				if (group.SortKeys != null)
				{
					List<ScopeValueDefinition> list = null;
					if (group.ScopeIdDefinition != null)
					{
						list = group.ScopeIdDefinition.Values;
					}
					for (int i = 0; i < group.SortKeys.Count; i++)
					{
						SortKey sortKey = group.SortKeys[i];
						if (MeasureAnalyzer.IsMeasure(this.m_expressionTable.GetNode(sortKey.Value)))
						{
							if (sortByMeasureInfoCollection == null)
							{
								sortByMeasureInfoCollection = new SortByMeasureInfoCollection(flag);
							}
							ExpressionId? expressionId = null;
							if (list != null)
							{
								expressionId = new ExpressionId?(list[i].Value.ExpressionId.Value);
							}
							SortByMeasureInfo sortByMeasureInfo = new SortByMeasureInfo(PlanNames.SortByMeasureColumn(sortKey, dataMember, i), sortKey.Value.ExpressionId.Value, expressionId);
							sortByMeasureInfoCollection.Add(sortKey, sortByMeasureInfo);
						}
					}
				}
			}
			int num;
			if (dataMember.DataMembers == null)
			{
				this.m_leafIndex++;
				num = this.m_leafIndex;
			}
			else
			{
				num = -1;
			}
			List<global::System.ValueTuple<Limit, bool>> list2 = null;
			if (this.m_limits != null)
			{
				this.m_limits.TryGetValue(dataMember, out list2);
			}
			DataMemberAnnotation dataMemberAnnotation = new DataMemberAnnotation(this.m_inPrimaryHierarchy, num, this.AreContentsIncludedInOutput(dataMember), sortByMeasureInfoCollection, list2);
			this.m_dataMemberAnnotations.Add(dataMember, dataMemberAnnotation);
			this.m_parents.Push(dataMember);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0004CF2E File Offset: 0x0004B12E
		protected override void Exit(DataMember dataMember)
		{
			this.m_parents.Pop();
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0004CF3C File Offset: 0x0004B13C
		private IScope GetSortByMeasureInnermostScope(IScope innermostScope)
		{
			DataMember dataMember = innermostScope as DataMember;
			if (dataMember == null || !dataMember.Group.SuppressSortByMeasureRollup)
			{
				return innermostScope;
			}
			do
			{
				innermostScope = this.m_scopeTree.GetParentScope(dataMember);
				dataMember = innermostScope as DataMember;
			}
			while (innermostScope != null && (dataMember == null || dataMember.Group.SuppressSortByMeasureRollup));
			return innermostScope;
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0004CF8B File Offset: 0x0004B18B
		protected override void Visit(Filter filter, Identifier dataShapeId)
		{
			FilterAnnotationAnalyzer.Analyze(filter, this.m_expressionTable, new VisitDataShapeDelegate(this.VisitFilterConditionDataShape));
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0004CFA6 File Offset: 0x0004B1A6
		private void VisitFilterConditionDataShape(DataShape dataShape, ObjectType filterConditionType)
		{
			this.Visit(dataShape);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0004CFB0 File Offset: 0x0004B1B0
		private Dictionary<DataMember, List<global::System.ValueTuple<Limit, bool>>> ComputeMemberLimits(List<Limit> limits)
		{
			if (limits == null || limits.Count == 0)
			{
				return null;
			}
			Dictionary<DataMember, List<global::System.ValueTuple<Limit, bool>>> dictionary = new Dictionary<DataMember, List<global::System.ValueTuple<Limit, bool>>>();
			for (int i = 0; i < limits.Count; i++)
			{
				Limit limit = limits[i];
				List<DataMember> groupScopesFromTargets = limit.GetGroupScopesFromTargets(this.m_expressionTable);
				IScope resolvedScope = limit.GetInnermostTarget().GetResolvedScope(this.m_expressionTable);
				if (!groupScopesFromTargets.IsNullOrEmpty<DataMember>())
				{
					foreach (DataMember dataMember in groupScopesFromTargets)
					{
						bool flag = this.m_scopeTree.AreSameScope(dataMember, resolvedScope);
						List<global::System.ValueTuple<Limit, bool>> list;
						if (!dictionary.TryGetValue(dataMember, out list))
						{
							list = new List<global::System.ValueTuple<Limit, bool>>();
							dictionary.Add(dataMember, list);
						}
						list.Add(new global::System.ValueTuple<Limit, bool>(limit, flag));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0004D094 File Offset: 0x0004B294
		private IScope ComputeInnermostScopeInDataShape(DataShape dataShape)
		{
			IScope innermostScopeInDataShape;
			if (!BatchDataSetPlanningUtils.TryGetCoreTableRowScope(dataShape, this.m_scopeTree, out innermostScopeInDataShape))
			{
				innermostScopeInDataShape = this.m_scopeTree.GetInnermostScopeInDataShape(dataShape);
			}
			return innermostScopeInDataShape;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0004D0C0 File Offset: 0x0004B2C0
		private bool AreContentsIncludedInOutput(DataMember dataMember)
		{
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			return !dataMember.ContextOnly || (!dataMember.IsDynamic && (!this.m_subtotalAnnotations.TryGetSubtotalSourceAnnotation(dataMember, out batchSubtotalAnnotation) || batchSubtotalAnnotation.Usage.IsIncludeInOutput()));
		}

		// Token: 0x040008B4 RID: 2228
		private readonly Dictionary<DataMember, DataMemberAnnotation> m_dataMemberAnnotations;

		// Token: 0x040008B5 RID: 2229
		private readonly BatchSubtotalAnnotations m_subtotalAnnotations;

		// Token: 0x040008B6 RID: 2230
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040008B7 RID: 2231
		private readonly ScopeTree m_scopeTree;

		// Token: 0x040008B8 RID: 2232
		private readonly Stack<IIdentifiable> m_parents;

		// Token: 0x040008B9 RID: 2233
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Limit", "IsInnermostTarget" })]
		private Dictionary<DataMember, List<global::System.ValueTuple<Limit, bool>>> m_limits;

		// Token: 0x040008BA RID: 2234
		private bool m_inPrimaryHierarchy;

		// Token: 0x040008BB RID: 2235
		private int m_leafIndex;

		// Token: 0x040008BC RID: 2236
		private Stack<IScope> m_innermostScopesInDataShape;
	}
}
