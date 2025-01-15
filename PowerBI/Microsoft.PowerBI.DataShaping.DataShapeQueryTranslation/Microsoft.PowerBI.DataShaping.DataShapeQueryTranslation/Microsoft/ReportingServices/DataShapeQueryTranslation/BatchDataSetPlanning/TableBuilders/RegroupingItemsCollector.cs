using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001D9 RID: 473
	internal sealed class RegroupingItemsCollector : DataShapeVisitor
	{
		// Token: 0x0600106A RID: 4202 RVA: 0x00044258 File Offset: 0x00042458
		private RegroupingItemsCollector(DataShapeAnnotations annotations, DataTransformReferenceMap transformReferenceMap, DataShapeContext dataShapeContext)
		{
			this.m_annotations = annotations;
			this.m_transformReferenceMap = transformReferenceMap;
			this.m_dsContext = dataShapeContext;
			this.m_dataMembers = new List<DataMember>();
			this.m_detailsCalculations = new List<Calculation>();
			this.m_aggregateCalculations = new List<Calculation>();
			this.m_aggregateSortKeys = new List<global::System.ValueTuple<SortKey, string>>();
			this.m_dataTransformTableColumns = null;
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x000442B4 File Offset: 0x000424B4
		internal static GroupByRegroupingItems Collect(DataShapeAnnotations annotations, DataTransformReferenceMap transformReferenceMap, DataShapeContext dataShapeContext)
		{
			RegroupingItemsCollector regroupingItemsCollector = new RegroupingItemsCollector(annotations, transformReferenceMap, dataShapeContext);
			regroupingItemsCollector.Visit(dataShapeContext.DataShape);
			IScope innermostScope = dataShapeContext.InnermostScope;
			return new GroupByRegroupingItems(regroupingItemsCollector.m_dataMembers, regroupingItemsCollector.m_detailsCalculations, regroupingItemsCollector.m_aggregateCalculations, regroupingItemsCollector.m_aggregateSortKeys, innermostScope, regroupingItemsCollector.m_dataTransformTableColumns);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00044301 File Offset: 0x00042501
		protected override void Visit(DataShape dataShape)
		{
			if (this.m_dsContext.DataShape != dataShape)
			{
				return;
			}
			this.TraverseDataShapeStructure(dataShape);
			this.VisitFirstTransform(dataShape.Transforms);
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00044325 File Offset: 0x00042525
		protected override void Enter(DataMember dataMember)
		{
			if (dataMember.IsDynamic)
			{
				if (this.m_transformReferenceMap.HasDataTransformColumnReference(dataMember))
				{
					return;
				}
				this.m_dataMembers.Add(dataMember);
				this.AddAggregateSortKeys(dataMember);
			}
			this.AddCalculations(dataMember.Calculations);
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0004435D File Offset: 0x0004255D
		protected override void Enter(DataIntersection dataIntersection)
		{
			this.AddCalculations(dataIntersection.Calculations);
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0004436C File Offset: 0x0004256C
		private void VisitFirstTransform(List<DataTransform> transforms)
		{
			if (transforms.IsNullOrEmpty<DataTransform>())
			{
				return;
			}
			DataTransform dataTransform = transforms[0];
			this.m_dataTransformTableColumns = dataTransform.Input.Table.Columns;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x000443A0 File Offset: 0x000425A0
		private void AddCalculations(IList<Calculation> calculations)
		{
			if (calculations == null || calculations.Count == 0)
			{
				return;
			}
			foreach (Calculation calculation in calculations)
			{
				if (calculation.ShouldIncludeCalculationInBaseQuery(this.m_dsContext, this.m_annotations, this.m_transformReferenceMap))
				{
					if (this.m_annotations.IsMeasure(calculation))
					{
						this.m_aggregateCalculations.Add(calculation);
					}
					else
					{
						this.m_detailsCalculations.Add(calculation);
					}
				}
			}
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00044430 File Offset: 0x00042630
		private void AddAggregateSortKeys(DataMember member)
		{
			SortByMeasureInfoCollection sortByMeasureInfos = this.m_annotations.DataMemberAnnotations.GetSortByMeasureInfos(member);
			if (sortByMeasureInfos.IsNullOrEmpty<KeyValuePair<SortKey, SortByMeasureInfo>>())
			{
				return;
			}
			foreach (KeyValuePair<SortKey, SortByMeasureInfo> keyValuePair in sortByMeasureInfos)
			{
				this.m_aggregateSortKeys.Add(new global::System.ValueTuple<SortKey, string>(keyValuePair.Key, keyValuePair.Value.SuggestedName));
			}
		}

		// Token: 0x040007AC RID: 1964
		private readonly DataShapeContext m_dsContext;

		// Token: 0x040007AD RID: 1965
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x040007AE RID: 1966
		private readonly DataTransformReferenceMap m_transformReferenceMap;

		// Token: 0x040007AF RID: 1967
		private List<DataMember> m_dataMembers;

		// Token: 0x040007B0 RID: 1968
		private List<Calculation> m_detailsCalculations;

		// Token: 0x040007B1 RID: 1969
		private List<Calculation> m_aggregateCalculations;

		// Token: 0x040007B2 RID: 1970
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "SortKey", "SuggestedName" })]
		private List<global::System.ValueTuple<SortKey, string>> m_aggregateSortKeys;

		// Token: 0x040007B3 RID: 1971
		private IReadOnlyList<DataTransformTableColumn> m_dataTransformTableColumns;
	}
}
