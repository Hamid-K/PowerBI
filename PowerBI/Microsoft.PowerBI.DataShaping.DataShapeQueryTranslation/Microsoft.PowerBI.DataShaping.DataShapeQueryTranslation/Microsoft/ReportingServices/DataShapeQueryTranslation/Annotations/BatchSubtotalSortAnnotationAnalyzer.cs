using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Common;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x0200023F RID: 575
	internal sealed class BatchSubtotalSortAnnotationAnalyzer
	{
		// Token: 0x060013A7 RID: 5031 RVA: 0x0004C91C File Offset: 0x0004AB1C
		internal BatchSubtotalSortAnnotationAnalyzer(ScopeTree scopeTree, DataMemberAnnotations dataMemberAnnotations, BatchSubtotalAnnotations subtotalAnnotations)
		{
			this.m_scopeTree = scopeTree;
			this.m_dataMemberAnnotations = dataMemberAnnotations;
			this.m_dsContexts = new Stack<BatchSubtotalSortAnnotationAnalyzer.DataShapeSubtotalSortContext>();
			this.m_batchSubtotalAnnotations = new WritableBatchSubtotalAnnotations(subtotalAnnotations);
			this.m_batchSubtotalSortAnnotations = new BatchSubtotalSortAnnotations();
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0004C954 File Offset: 0x0004AB54
		internal BatchSubtotalSortAnnotations SubtotalSortAnnotations
		{
			get
			{
				return this.m_batchSubtotalSortAnnotations;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x0004C95C File Offset: 0x0004AB5C
		internal BatchSubtotalAnnotations SubtotalAnnotations
		{
			get
			{
				return this.m_batchSubtotalAnnotations;
			}
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0004C964 File Offset: 0x0004AB64
		internal void Analyze(DataShape dataShape)
		{
			this.m_dsContexts.Push(new BatchSubtotalSortAnnotationAnalyzer.DataShapeSubtotalSortContext(dataShape));
			this.AddSortByMeasureTotalAnnotations(dataShape);
			this.m_dsContexts.Pop();
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x0004C98A File Offset: 0x0004AB8A
		private BatchSubtotalSortAnnotationAnalyzer.DataShapeSubtotalSortContext Context
		{
			get
			{
				return this.m_dsContexts.Peek();
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x0004C997 File Offset: 0x0004AB97
		private DataShape DataShape
		{
			get
			{
				return this.Context.DataShape;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0004C9A4 File Offset: 0x0004ABA4
		private IList<DataMember> DynamicPrimaryMembers
		{
			get
			{
				return this.Context.DynamicPrimaryMembers;
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x0004C9B1 File Offset: 0x0004ABB1
		private IList<DataMember> DynamicSecondaryMembers
		{
			get
			{
				return this.Context.DynamicSecondaryMembers;
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0004C9BE File Offset: 0x0004ABBE
		private void AddSortByMeasureTotalAnnotations(DataShape dataShape)
		{
			this.AddSortByMeasureTotalAnnotations(dataShape, true);
			this.AddSortByMeasureTotalAnnotations(dataShape, false);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0004C9D0 File Offset: 0x0004ABD0
		private void AddSortByMeasureTotalAnnotations(DataShape dataShape, bool primary)
		{
			IScope scope = null;
			if (!BatchDataSetPlanningUtils.TryGetCoreTableRowScope(dataShape, this.m_scopeTree, out scope))
			{
				return;
			}
			IList<DataMember> list = (primary ? this.DynamicPrimaryMembers : this.DynamicSecondaryMembers);
			int count = list.Count;
			IList<DataMember> groupsForCoreTableRowScope = dataShape.GetGroupsForCoreTableRowScope(this.m_scopeTree, this.m_dataMemberAnnotations, primary);
			IList<DataMember> groupsForCoreTableRowScope2 = dataShape.GetGroupsForCoreTableRowScope(this.m_scopeTree, this.m_dataMemberAnnotations, !primary);
			int count2 = groupsForCoreTableRowScope.Count;
			int count3 = groupsForCoreTableRowScope2.Count;
			for (int i = 0; i < count; i++)
			{
				DataMember dataMember = list[i];
				bool flag = this.m_scopeTree.AreSameScope(dataMember, groupsForCoreTableRowScope[count2 - 1]);
				bool flag2 = count3 > 0;
				if (!BatchDataSetPlanningUtils.AreEquivalentScopes(dataMember, scope, this.m_scopeTree) && this.m_dataMemberAnnotations.HasSortByMeasureKeys(dataMember))
				{
					BatchSubtotalAnnotation batchSubtotalAnnotation = null;
					BatchSubtotalAnnotation batchSubtotalAnnotation2 = null;
					if (!flag)
					{
						DataMember dataMember2 = groupsForCoreTableRowScope[count2 - 1];
						DataMember rollupStopScope = this.GetRollupStopScope(groupsForCoreTableRowScope, i);
						if (rollupStopScope != null)
						{
							batchSubtotalAnnotation = this.AddSortByMeasureTotalAnnotation(dataMember2, rollupStopScope, BatchSubtotalAnnotations.GetSubtotalIndicatorColumnName(dataMember, primary, this.DataShape.Id.Value, this.m_dsContexts.Count));
						}
					}
					if (flag2)
					{
						batchSubtotalAnnotation2 = this.AddSortByMeasureTotalAnnotation(groupsForCoreTableRowScope2[count3 - 1], groupsForCoreTableRowScope2[0], BatchSubtotalAnnotations.GetSubtotalIndicatorColumnName(this.DataShape, !primary, this.DataShape.Id.Value, this.m_dsContexts.Count));
					}
					if (batchSubtotalAnnotation != null || batchSubtotalAnnotation2 != null)
					{
						BatchSortByMeasureSourceAnnotation batchSortByMeasureSourceAnnotation = new BatchSortByMeasureSourceAnnotation(batchSubtotalAnnotation, batchSubtotalAnnotation2);
						this.m_batchSubtotalSortAnnotations.AddSortByMeasureSourceAnnotation(dataMember, batchSortByMeasureSourceAnnotation);
					}
				}
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0004CB6C File Offset: 0x0004AD6C
		private DataMember GetRollupStopScope(IList<DataMember> membersInQuery, int currentMemberIndex)
		{
			int num = currentMemberIndex + 1;
			while (num != membersInQuery.Count)
			{
				DataMember dataMember = membersInQuery[num++];
				if (!dataMember.Group.SuppressSortByMeasureRollup)
				{
					return dataMember;
				}
			}
			return null;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0004CBA4 File Offset: 0x0004ADA4
		private BatchSubtotalAnnotation AddSortByMeasureTotalAnnotation(IScope rollupStartScope, IScope rollupStopScope, string subtotalIndicatorColumnName)
		{
			BatchSubtotalAnnotation batchSubtotalAnnotation;
			if (this.m_batchSubtotalAnnotations.TryGetSubtotalAnnotation(rollupStopScope, out batchSubtotalAnnotation))
			{
				return batchSubtotalAnnotation;
			}
			SortDirection sortDirection = SortDirection.Ascending;
			BatchSubtotalAnnotation batchSubtotalAnnotation2 = new BatchSubtotalAnnotation(rollupStartScope, rollupStopScope, subtotalIndicatorColumnName, sortDirection, SubtotalUsage.SortByMeasure);
			this.m_batchSubtotalAnnotations.AddSubtotalAnnotation(rollupStopScope, batchSubtotalAnnotation2);
			return batchSubtotalAnnotation2;
		}

		// Token: 0x040008AC RID: 2220
		private readonly ScopeTree m_scopeTree;

		// Token: 0x040008AD RID: 2221
		private readonly WritableBatchSubtotalAnnotations m_batchSubtotalAnnotations;

		// Token: 0x040008AE RID: 2222
		private readonly BatchSubtotalSortAnnotations m_batchSubtotalSortAnnotations;

		// Token: 0x040008AF RID: 2223
		private readonly DataMemberAnnotations m_dataMemberAnnotations;

		// Token: 0x040008B0 RID: 2224
		private readonly Stack<BatchSubtotalSortAnnotationAnalyzer.DataShapeSubtotalSortContext> m_dsContexts;

		// Token: 0x02000324 RID: 804
		private sealed class DataShapeSubtotalSortContext
		{
			// Token: 0x0600177A RID: 6010 RVA: 0x00052CD9 File Offset: 0x00050ED9
			internal DataShapeSubtotalSortContext(DataShape dataShape)
			{
				this.m_dataShape = dataShape;
				this.m_dynamicPrimaryMembers = dataShape.PrimaryHierarchy.GetAllDynamicMembers().Evaluate<DataMember>();
				this.m_dynamicSecondaryMembers = dataShape.SecondaryHierarchy.GetAllDynamicMembers().Evaluate<DataMember>();
			}

			// Token: 0x17000426 RID: 1062
			// (get) Token: 0x0600177B RID: 6011 RVA: 0x00052D14 File Offset: 0x00050F14
			internal DataShape DataShape
			{
				get
				{
					return this.m_dataShape;
				}
			}

			// Token: 0x17000427 RID: 1063
			// (get) Token: 0x0600177C RID: 6012 RVA: 0x00052D1C File Offset: 0x00050F1C
			internal IList<DataMember> DynamicPrimaryMembers
			{
				get
				{
					return this.m_dynamicPrimaryMembers;
				}
			}

			// Token: 0x17000428 RID: 1064
			// (get) Token: 0x0600177D RID: 6013 RVA: 0x00052D24 File Offset: 0x00050F24
			internal IList<DataMember> DynamicSecondaryMembers
			{
				get
				{
					return this.m_dynamicSecondaryMembers;
				}
			}

			// Token: 0x04000B7D RID: 2941
			private readonly DataShape m_dataShape;

			// Token: 0x04000B7E RID: 2942
			private readonly IList<DataMember> m_dynamicPrimaryMembers;

			// Token: 0x04000B7F RID: 2943
			private readonly IList<DataMember> m_dynamicSecondaryMembers;
		}
	}
}
