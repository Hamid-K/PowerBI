using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000092 RID: 146
	internal sealed class QuerySubtotalGenerator
	{
		// Token: 0x060006EC RID: 1772 RVA: 0x0001A0AC File Offset: 0x000182AC
		internal QuerySubtotalGenerator(BatchSubtotalAnnotations subtotalAnnotations, ScopeTree scopeTree)
		{
			this.m_subtotalAnnotations = subtotalAnnotations;
			this.m_scopeTree = scopeTree;
			this.m_aggregateIndicatorFieldNames = new Dictionary<DataMember, string>();
			this.m_qdmGroupRefs = new Dictionary<DataMember, List<GroupReference>>();
			this.m_sortDirections = new Dictionary<DataMember, Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortDirection>();
			this.m_rollupGroups = new List<List<DataMember>>();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001A0FC File Offset: 0x000182FC
		public ReadOnlyDictionary<DataMember, string> Generate(QueryBuilder queryBuilder)
		{
			foreach (List<DataMember> list in this.m_rollupGroups)
			{
				this.AddRollupGroup(list, queryBuilder);
			}
			return this.m_aggregateIndicatorFieldNames.AsReadOnly<DataMember, string>();
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001A15C File Offset: 0x0001835C
		public void AddRollupInfo(DataMember dataMember, Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortDirection sortDirection, List<GroupReference> qdmGroupRefs)
		{
			this.m_qdmGroupRefs.Add(dataMember, qdmGroupRefs);
			this.m_sortDirections.Add(dataMember, sortDirection);
			this.AddOrMergeRollupGroup(dataMember);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001A17F File Offset: 0x0001837F
		public static ReadOnlyDictionary<DataMember, string> CreateEmptyAggregatorFieldNamesMap()
		{
			return new ReadOnlyDictionary<DataMember, string>(new Dictionary<DataMember, string>());
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001A18C File Offset: 0x0001838C
		private void AddOrMergeRollupGroup(DataMember dataMember)
		{
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			if (this.m_subtotalAnnotations.TryGetSubtotalAnnotation(dataMember, out batchSubtotalAnnotation))
			{
				Contract.RetailAssert(this.m_scopeTree.AreSameScope(batchSubtotalAnnotation.StopScope, dataMember), "Expected member to be stopScope");
				List<DataMember> list = new List<DataMember>();
				list.Add(dataMember);
				this.m_rollupGroups.Add(list);
				return;
			}
			if (this.m_rollupGroups.Count > 0)
			{
				List<DataMember> list2 = this.m_rollupGroups[this.m_rollupGroups.Count - 1];
				DataMember dataMember2 = list2[0];
				if (!this.m_subtotalAnnotations.TryGetSubtotalAnnotation(dataMember2, out batchSubtotalAnnotation))
				{
					Contract.RetailFail("Start member should have already had a subtotal annotation");
				}
				if (this.m_scopeTree.IsParentScope(batchSubtotalAnnotation.StopScope, dataMember) && this.m_scopeTree.IsSameOrParentScope(dataMember, batchSubtotalAnnotation.StartScope))
				{
					list2.Add(dataMember);
				}
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001A258 File Offset: 0x00018458
		private void AddRollupGroup(List<DataMember> rollupGroupMembers, QueryBuilder queryBuilder)
		{
			DataMember dataMember = rollupGroupMembers[0];
			IList<GroupReference> list = rollupGroupMembers.SelectMany((DataMember member) => this.m_qdmGroupRefs[member]).Evaluate<GroupReference>();
			string text = list[0].Group.Name + "IsAggregate";
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection sortDirection = this.m_sortDirections[dataMember].ToQdmSortDirection();
			queryBuilder.AddRollup(list, text, sortDirection);
			this.m_aggregateIndicatorFieldNames.Add(dataMember, text);
		}

		// Token: 0x04000357 RID: 855
		private const string IsAggregateSuffix = "IsAggregate";

		// Token: 0x04000358 RID: 856
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000359 RID: 857
		private readonly BatchSubtotalAnnotations m_subtotalAnnotations;

		// Token: 0x0400035A RID: 858
		private readonly Dictionary<DataMember, string> m_aggregateIndicatorFieldNames;

		// Token: 0x0400035B RID: 859
		private readonly Dictionary<DataMember, List<GroupReference>> m_qdmGroupRefs;

		// Token: 0x0400035C RID: 860
		private readonly Dictionary<DataMember, Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortDirection> m_sortDirections;

		// Token: 0x0400035D RID: 861
		private readonly List<List<DataMember>> m_rollupGroups;
	}
}
