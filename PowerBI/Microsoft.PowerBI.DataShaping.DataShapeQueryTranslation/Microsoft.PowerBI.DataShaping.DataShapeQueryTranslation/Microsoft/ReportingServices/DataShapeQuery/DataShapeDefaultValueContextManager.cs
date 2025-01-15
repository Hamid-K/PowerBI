using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQuery
{
	// Token: 0x02000008 RID: 8
	internal sealed class DataShapeDefaultValueContextManager : IDataShapeDefaultValueContextManager
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000021C0 File Offset: 0x000003C0
		internal DataShapeDefaultValueContextManager(DataShapeAnnotations annotations, ScopeTree scopeTree, ExpressionTable expressionTable, FieldRelationshipAnnotations fieldRelationshipAnnotations, ColumnGroupingAnnotations columnGroupingAnnotations)
		{
			this.m_annotations = annotations;
			this.m_scopeTree = scopeTree;
			this.m_expressionTable = expressionTable;
			this.m_fieldRelationshipAnnotations = fieldRelationshipAnnotations;
			this.m_columnGroupingAnnotations = columnGroupingAnnotations;
			this.m_defaultContextByDataShape = new Dictionary<IContextItem, DefaultContextManager>();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021F8 File Offset: 0x000003F8
		internal static IDataShapeDefaultValueContextManager Create(IFederatedConceptualSchema schema, IFeatureSwitchProvider featureSwitches, DataShapeAnnotations annotations, ScopeTree scopeTree, ExpressionTable expressionTable)
		{
			if (schema.GetDefaultSchemaDaxCapabilitiesAnnotation().IsMultidimensional())
			{
				FieldRelationshipAnnotations defaultFieldRelationshipAnnotations = schema.GetDefaultFieldRelationshipAnnotations();
				ColumnGroupingAnnotations defaultColumnGroupingAnnotations = schema.GetDefaultColumnGroupingAnnotations();
				return new DataShapeDefaultValueContextManager(annotations, scopeTree, expressionTable, defaultFieldRelationshipAnnotations, defaultColumnGroupingAnnotations);
			}
			return NoOpDataShapeDefaultValueContextManager.Instance;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002234 File Offset: 0x00000434
		public PlanOperationClearDefaultContext ToPlanOperation(DataShape dataShape)
		{
			IReadOnlyDefaultContextManager forContextItem = this.GetForContextItem(dataShape);
			if (forContextItem == null || forContextItem.GetColumnsRequiringClearDefaultFilterContext().IsNullOrEmpty<IConceptualColumn>())
			{
				return null;
			}
			return new PlanOperationClearDefaultContext(forContextItem);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002264 File Offset: 0x00000464
		public void AddGrouping(DataMember dataMember)
		{
			IReadOnlyList<IConceptualColumn> readOnlyList = FilteredColumnsCollector.Collect(dataMember, this.m_annotations, this.m_expressionTable);
			this.GetOrAddManagerForContextItem(dataMember.GetParentDataShape(this.m_scopeTree, this.m_annotations)).AddColumnRequiringImplicitGroupingClearedContext(readOnlyList);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022A2 File Offset: 0x000004A2
		public void AddFilter(DataShape dataShape, Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition filterCondition)
		{
			this.AddDefaultContext(dataShape, filterCondition);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022AC File Offset: 0x000004AC
		public void AddDefaultValueFilters(DataShape dataShape)
		{
			DefaultValueFilterCondition defaultValueFilter = this.m_annotations.GetDefaultValueFilter(dataShape);
			if (defaultValueFilter != null)
			{
				this.AddDefaultContext(dataShape, defaultValueFilter);
			}
			IReadOnlyList<AnyValueFilterCondition> anyValueFilters = this.m_annotations.GetAnyValueFilters(dataShape);
			if (!anyValueFilters.IsNullOrEmpty<AnyValueFilterCondition>())
			{
				for (int i = 0; i < anyValueFilters.Count; i++)
				{
					this.AddDefaultContext(dataShape, anyValueFilters[i]);
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002308 File Offset: 0x00000508
		private void AddDefaultContext(DataShape dataShape, Microsoft.DataShaping.InternalContracts.DataShapeQuery.FilterCondition filter)
		{
			IReadOnlyList<IConceptualColumn> readOnlyList = FilteredColumnsCollector.Collect(filter, this.m_annotations, this.m_expressionTable);
			DefaultContextManager orAddManagerForContextItem = this.GetOrAddManagerForContextItem(dataShape);
			if (!(filter is DefaultValueFilterCondition))
			{
				AnyValueFilterCondition anyValueFilterCondition = filter as AnyValueFilterCondition;
				if (anyValueFilterCondition != null)
				{
					if (!anyValueFilterCondition.DefaultValueOverridesAncestors)
					{
						using (IEnumerator<IConceptualColumn> enumerator = readOnlyList.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								IConceptualColumn conceptualColumn = enumerator.Current;
								orAddManagerForContextItem.AddColumnRequiringClearedContext(conceptualColumn);
							}
							return;
						}
					}
					orAddManagerForContextItem.AddColumnRequiringImplicitGroupingClearedContext(readOnlyList);
					return;
				}
				orAddManagerForContextItem.AddColumnsRequiringClearedContext(readOnlyList);
				return;
			}
			orAddManagerForContextItem.AddColumnRequiringDefaultContext(readOnlyList);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023A8 File Offset: 0x000005A8
		private DefaultContextManager GetOrAddManagerForContextItem(DataShape contextItem)
		{
			DefaultContextManager defaultContextManager;
			if (!this.m_defaultContextByDataShape.TryGetValue(contextItem, out defaultContextManager))
			{
				DataShape canonicalContextItem = this.GetCanonicalContextItem(contextItem);
				if (contextItem != canonicalContextItem)
				{
					defaultContextManager = this.GetOrAddManagerForContextItem(canonicalContextItem);
				}
				else
				{
					defaultContextManager = new DefaultContextManager(this.m_fieldRelationshipAnnotations, this.m_columnGroupingAnnotations);
				}
				this.m_defaultContextByDataShape.Add(contextItem, defaultContextManager);
			}
			return defaultContextManager;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023FC File Offset: 0x000005FC
		private IReadOnlyDefaultContextManager GetForContextItem(DataShape contextItem)
		{
			DefaultContextManager defaultContextManager;
			if (!this.m_defaultContextByDataShape.TryGetValue(contextItem, out defaultContextManager))
			{
				DataShape canonicalContextItem = this.GetCanonicalContextItem(contextItem);
				if (contextItem != canonicalContextItem)
				{
					this.m_defaultContextByDataShape.TryGetValue(canonicalContextItem, out defaultContextManager);
				}
			}
			return defaultContextManager;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002438 File Offset: 0x00000638
		private DataShape GetCanonicalContextItem(DataShape dataShape)
		{
			if (!dataShape.IsIndependent)
			{
				DataShape parentDataShape;
				do
				{
					parentDataShape = dataShape.GetParentDataShape(this.m_scopeTree, this.m_annotations);
					if (parentDataShape != null)
					{
						dataShape = parentDataShape;
					}
				}
				while (parentDataShape != null && !parentDataShape.IsIndependent);
			}
			return dataShape;
		}

		// Token: 0x0400002B RID: 43
		private readonly Dictionary<IContextItem, DefaultContextManager> m_defaultContextByDataShape;

		// Token: 0x0400002C RID: 44
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x0400002D RID: 45
		private readonly ScopeTree m_scopeTree;

		// Token: 0x0400002E RID: 46
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x0400002F RID: 47
		private readonly FieldRelationshipAnnotations m_fieldRelationshipAnnotations;

		// Token: 0x04000030 RID: 48
		private readonly ColumnGroupingAnnotations m_columnGroupingAnnotations;
	}
}
