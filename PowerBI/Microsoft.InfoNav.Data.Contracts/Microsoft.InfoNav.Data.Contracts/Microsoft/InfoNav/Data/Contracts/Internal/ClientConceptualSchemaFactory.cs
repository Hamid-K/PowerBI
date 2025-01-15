using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.PrimitiveValues;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000166 RID: 358
	public static class ClientConceptualSchemaFactory
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x0001263C File Offset: 0x0001083C
		public static ClientConceptualSchema Create(IConceptualSchema input, bool canEdit = false, IClientConceptualSchemaHelper helper = null, bool isExtendable = false)
		{
			if (helper != null)
			{
				helper.HasDirectQueryContent();
			}
			return new ClientConceptualSchema(ClientConceptualSchemaFactory.Create(input.Entities.Evaluate<IConceptualEntity>(), canEdit, helper), canEdit, ClientConceptualSchemaFactory.Create(input.Capabilities, isExtendable, helper));
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00012670 File Offset: 0x00010870
		private static ClientConceptualCapabilities Create(ConceptualCapabilities input, bool isExtendable, IClientConceptualSchemaHelper helper)
		{
			bool discourageQueryAggregateUsage = input.DiscourageQueryAggregateUsage;
			bool supportsMedian = input.SupportsMedian;
			bool supportsPercentile = input.SupportsPercentile;
			bool normalizedFiveStateKpiRange = input.NormalizedFiveStateKpiRange;
			bool supportsScopedEval = input.SupportsScopedEval;
			bool supportsStringMinMax = input.SupportsStringMinMax;
			bool supportsMultiTableTupleFilters = input.SupportsMultiTableTupleFilters;
			bool flag = helper == null || !helper.HasDirectQueryContent();
			bool supportsBinnedLineSample = input.SupportsBinnedLineSample;
			bool supportsOverlappingPointsSample = input.SupportsOverlappingPointsSample;
			bool limitMultiColumnFiltersToQueryGroupColumns = input.LimitMultiColumnFiltersToQueryGroupColumns;
			bool flag2 = helper == null || helper.SupportsCalculatedColumns();
			bool flag3 = helper == null || helper.SupportsGrouping();
			bool flag4 = helper != null && helper.SupportsInsights();
			bool flag5 = helper != null && helper.SupportsQnA();
			bool supportsInstanceFilters = input.SupportsInstanceFilters;
			bool supportsDataSourceVariables = input.SupportsDataSourceVariables;
			InsightsCapabilities insightsCapabilities = ((helper != null) ? helper.GetInsightsCapabilities() : null);
			bool supportsSubqueryRegrouping = input.SupportsSubqueryRegrouping;
			bool supportsTopNPerLevel = input.SupportsTopNPerLevel;
			bool flag6 = helper != null && helper.SupportsFastRefresh();
			bool supportsScopedDataReduction = input.SupportsScopedDataReduction;
			bool supportsScopedAggregates = input.SupportsScopedAggregates;
			bool flag7 = supportsScopedDataReduction;
			bool supportsExtensionColumns = input.SupportsExtensionColumns;
			bool flag8 = helper != null && helper.IsQnaEnabled();
			bool supportsGroupSynchronization = input.SupportsGroupSynchronization;
			bool supportsSparklineData = input.SupportsSparklineData;
			bool supportsVisualCalculations = input.SupportsVisualCalculations;
			return new ClientConceptualCapabilities(discourageQueryAggregateUsage, supportsMedian, supportsPercentile, normalizedFiveStateKpiRange, supportsScopedEval, supportsStringMinMax, supportsMultiTableTupleFilters, isExtendable, flag, supportsBinnedLineSample, supportsOverlappingPointsSample, limitMultiColumnFiltersToQueryGroupColumns, flag2, flag3, flag4, flag5, supportsInstanceFilters, supportsDataSourceVariables, supportsSubqueryRegrouping, supportsTopNPerLevel, flag6, supportsScopedAggregates, flag7, supportsExtensionColumns, helper != null && helper.CanEditChangeDetectionMeasure(), helper != null && helper.SupportChangeDetectionMeasureRefresh(), flag8, supportsGroupSynchronization, supportsSparklineData, supportsVisualCalculations, insightsCapabilities, ClientConceptualSchemaFactory.GetClientTransformCapabilities(input.TransformCapabilities));
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00012794 File Offset: 0x00010994
		private static int? GetLimitOnNumberOfGroups(bool isDirectQueryMode)
		{
			if (isDirectQueryMode)
			{
				return new int?(10);
			}
			return null;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000127B8 File Offset: 0x000109B8
		private static IList<ClientConceptualEntity> Create(IList<IConceptualEntity> input, bool canEdit, IClientConceptualSchemaHelper helper)
		{
			List<ClientConceptualEntity> list = new List<ClientConceptualEntity>(input.Count);
			for (int i = 0; i < input.Count; i++)
			{
				list.Add(ClientConceptualSchemaFactory.Create(input[i], canEdit, helper));
			}
			return list;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000127F8 File Offset: 0x000109F8
		private static ClientConceptualEntity Create(IConceptualEntity input, bool canEdit, IClientConceptualSchemaHelper helper)
		{
			bool flag = helper != null && helper.IsCalculatedTable(input.Name);
			ClientConceptualQueryableState clientConceptualQueryableState = ClientConceptualQueryableState.Queryable;
			string text = null;
			if (helper != null)
			{
				QueryableState entityQueryableState = helper.GetEntityQueryableState(input.Name);
				clientConceptualQueryableState = entityQueryableState.Queryable;
				text = entityQueryableState.ErrorMessage;
			}
			ClientConceptualEntityMode clientConceptualEntityMode = ClientConceptualEntityMode.Unknown;
			ClientConceptualEntitySource clientConceptualEntitySource = null;
			if (helper != null)
			{
				clientConceptualEntityMode = helper.GetMode(input.Name);
				DateTime? refreshedTime = helper.GetRefreshedTime(input.Name);
				string text2 = null;
				string text3 = null;
				helper.GetDirectQueryResourceInfo(input.Name, out text2, out text3);
				if (clientConceptualEntityMode != ClientConceptualEntityMode.Unknown || refreshedTime != null || !string.IsNullOrEmpty(text2) || !string.IsNullOrEmpty(text3))
				{
					clientConceptualEntitySource = new ClientConceptualEntitySource(clientConceptualEntityMode, refreshedTime, text2, text3);
				}
			}
			bool flag2 = clientConceptualEntityMode == ClientConceptualEntityMode.Import;
			bool flag3 = clientConceptualEntityMode == ClientConceptualEntityMode.DirectQuery || clientConceptualEntityMode == ClientConceptualEntityMode.Dual;
			IReadOnlyList<IConceptualProperty> defaultFieldProperties = input.DefaultFieldProperties;
			List<string> list = null;
			if (defaultFieldProperties != null)
			{
				list = new List<string>(defaultFieldProperties.Count);
				for (int i = 0; i < defaultFieldProperties.Count; i++)
				{
					list.Add(defaultFieldProperties[i].Name);
				}
			}
			return new ClientConceptualEntity(input.Name, input.GetFullName(), input.DisplayName, input.Description, input.Visibility.IsHidden(), input.Visibility.ShowAsVariationsOnly(), input.Visibility.IsPrivate(), input.IsDateTable, flag, clientConceptualQueryableState, text, ClientConceptualSchemaFactory.Create(input.Properties, helper), ClientConceptualSchemaFactory.Create(input.Hierarchies, helper, input.Name), ClientConceptualSchemaFactory.Create(input.DisplayFolders, helper), ClientConceptualSchemaFactory.Create(input.NavigationProperties), new ClientConceptualEntityCapabilities(helper == null || helper.CanRefreshTable(input.Name), helper == null || helper.CanEditTableSource(input.Name), helper == null || helper.CanRenameTable(input.Name), helper == null || helper.CanDeleteTable(input.Name), (helper == null || helper.CanEditStorageMode(input.Name)) && canEdit, flag2 && canEdit, flag2 && canEdit, ClientConceptualSchemaFactory.GetLimitOnNumberOfGroups(flag3), (helper != null) ? helper.GetDataViewCapabilities(input.Name) : null), (input.DefaultLabelColumn == null) ? null : input.DefaultLabelColumn.Name, list, clientConceptualEntitySource, input.StableName);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00012A18 File Offset: 0x00010C18
		private static IList<ClientConceptualProperty> Create(IReadOnlyList<IConceptualProperty> input, IClientConceptualSchemaHelper helper)
		{
			List<ClientConceptualProperty> list = new List<ClientConceptualProperty>(input.Count);
			for (int i = 0; i < input.Count; i++)
			{
				list.Add(ClientConceptualSchemaFactory.Create(input[i], helper));
			}
			return list;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00012A58 File Offset: 0x00010C58
		private static IList<ClientConceptualDisplayFolder> Create(IReadOnlyList<IConceptualDisplayFolder> input, IClientConceptualSchemaHelper helper)
		{
			List<ClientConceptualDisplayFolder> list = new List<ClientConceptualDisplayFolder>(input.Count);
			for (int i = 0; i < input.Count; i++)
			{
				list.Add(ClientConceptualSchemaFactory.Create(input[i], helper));
			}
			return list;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00012A98 File Offset: 0x00010C98
		private static ClientConceptualDisplayFolder Create(IConceptualDisplayFolder input, IClientConceptualSchemaHelper helper)
		{
			List<ClientConceptualDisplayItem> list = new List<ClientConceptualDisplayItem>(input.DisplayItems.Count);
			foreach (IConceptualDisplayItem conceptualDisplayItem in input.DisplayItems)
			{
				if (conceptualDisplayItem is IConceptualProperty)
				{
					list.Add(new ClientConceptualDisplayItem((IConceptualProperty)conceptualDisplayItem));
				}
				else if (conceptualDisplayItem is IConceptualHierarchy)
				{
					list.Add(new ClientConceptualDisplayItem((IConceptualHierarchy)conceptualDisplayItem));
				}
				else if (conceptualDisplayItem is IConceptualDisplayFolder)
				{
					list.Add(new ClientConceptualDisplayItem(ClientConceptualSchemaFactory.Create((IConceptualDisplayFolder)conceptualDisplayItem, helper)));
				}
			}
			return new ClientConceptualDisplayFolder(input.Name, input.DisplayName, input.Description, list);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00012B5C File Offset: 0x00010D5C
		private static IList<ClientConceptualHierarchy> Create(IReadOnlyList<IConceptualHierarchy> input, IClientConceptualSchemaHelper helper, string entityName)
		{
			List<ClientConceptualHierarchy> list = new List<ClientConceptualHierarchy>(input.Count);
			for (int i = 0; i < input.Count; i++)
			{
				list.Add(ClientConceptualSchemaFactory.Create(input[i], helper, entityName));
			}
			return list;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00012B9C File Offset: 0x00010D9C
		private static ClientConceptualHierarchy Create(IConceptualHierarchy input, IClientConceptualSchemaHelper helper, string entityName)
		{
			return new ClientConceptualHierarchy(input.Name, input.DisplayName, input.Description, input.IsHidden, ClientConceptualSchemaFactory.Create(input.Levels, helper, entityName), helper == null || helper.CanDeleteHierarchy(entityName, input.Name), input.StableName);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00012BEC File Offset: 0x00010DEC
		private static IList<ClientConceptualHierarchyLevel> Create(IReadOnlyList<IConceptualHierarchyLevel> input, IClientConceptualSchemaHelper helper, string entityName)
		{
			List<ClientConceptualHierarchyLevel> list = new List<ClientConceptualHierarchyLevel>(input.Count);
			for (int i = 0; i < input.Count; i++)
			{
				list.Add(ClientConceptualSchemaFactory.Create(input[i], helper, entityName));
			}
			return list;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00012C2C File Offset: 0x00010E2C
		private static ClientConceptualHierarchyLevel Create(IConceptualHierarchyLevel input, IClientConceptualSchemaHelper helper, string entityName)
		{
			return new ClientConceptualHierarchyLevel(input.Name, input.DisplayName, input.Source.Name, helper == null || helper.CanDeleteHierarchyLevel(entityName, input.Hierarchy.Name, input.Name), input.StableName);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00012C7C File Offset: 0x00010E7C
		private static ClientConceptualProperty Create(IConceptualProperty input, IClientConceptualSchemaHelper helper)
		{
			ClientConceptualColumn clientConceptualColumn = null;
			ClientConceptualMeasure clientConceptualMeasure = null;
			IConceptualColumn conceptualColumn = input as IConceptualColumn;
			if (conceptualColumn != null)
			{
				clientConceptualColumn = ClientConceptualSchemaFactory.CreateColumn(conceptualColumn, helper);
			}
			else
			{
				clientConceptualMeasure = ClientConceptualSchemaFactory.CreateMeasure(input as IConceptualMeasure, helper);
			}
			ClientConceptualQueryableState clientConceptualQueryableState = ClientConceptualQueryableState.Queryable;
			string text = null;
			if (helper != null)
			{
				QueryableState propertyQueryableState = helper.GetPropertyQueryableState(input.Entity.Name, input.Name);
				clientConceptualQueryableState = propertyQueryableState.Queryable;
				text = propertyQueryableState.ErrorMessage;
			}
			return new ClientConceptualProperty(input.Name, input.EdmName, input.DisplayName, input.Description, input.ConceptualDataType, input.ConceptualDataCategory, input.IsHidden, input.IsPrivate, input.FormatString, clientConceptualColumn, clientConceptualMeasure, clientConceptualQueryableState, text, helper == null || helper.CanDelete(input.Entity.Name, input.Name), input.StableName);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00012D40 File Offset: 0x00010F40
		private static ClientConceptualColumn CreateColumn(IConceptualColumn input, IClientConceptualSchemaHelper helper)
		{
			IReadOnlyList<IConceptualColumn> identityColumns = input.Grouping.IdentityColumns;
			IList<string> list;
			if (identityColumns.Count == 1 && identityColumns[0] == input)
			{
				list = Util.EmptyReadOnlyCollection<string>();
			}
			else
			{
				list = identityColumns.Select((IConceptualColumn p) => p.Name).ToList<string>();
			}
			string text = ((input.DefaultValue != null) ? PrimitiveValueEncoding.ToTypeEncodedString(input.DefaultValue) : null);
			IList<ClientConceptualVariationSource> list2 = ClientConceptualSchemaFactory.Create(input.VariationSources);
			GroupsMetadata groupsMetadata = null;
			BinsMetadata binsMetadata = null;
			bool flag = false;
			if (helper != null)
			{
				helper.GetColumnMetadata(input.Entity.Name, input.Name, out flag, out groupsMetadata, out binsMetadata);
			}
			ClientConceptualGroupingMetadata clientConceptualGroupingMetadata = null;
			if (groupsMetadata != null || binsMetadata != null)
			{
				clientConceptualGroupingMetadata = new ClientConceptualGroupingMetadata(null, (binsMetadata != null) ? binsMetadata.BinningDefinition : null);
			}
			else if (input.GroupingMetadata != null)
			{
				clientConceptualGroupingMetadata = ClientConceptualSchemaFactory.Create(input.GroupingMetadata, helper);
			}
			List<string> list3;
			if (!input.OrderByColumns.IsNullOrEmpty<IConceptualColumn>())
			{
				list3 = input.OrderByColumns.Select((IConceptualColumn c) => c.Name).ToList<string>();
			}
			else
			{
				list3 = null;
			}
			List<string> list4 = list3;
			ClientConceptualParameterMetadata clientConceptualParameterMetadata;
			if (input.ParameterMetadata != null)
			{
				ParameterKind parameterKind = input.ParameterMetadata.ParameterKind;
				IReadOnlyList<ConceptualMParameter> mappedMParameters = input.ParameterMetadata.MappedMParameters;
				IReadOnlyList<string> readOnlyList;
				if (mappedMParameters == null)
				{
					readOnlyList = null;
				}
				else
				{
					readOnlyList = mappedMParameters.Select((ConceptualMParameter p) => p.Name).ToList<string>();
				}
				clientConceptualParameterMetadata = new ClientConceptualParameterMetadata(parameterKind, readOnlyList, input.ParameterMetadata.SupportsMultipleValues, input.ParameterMetadata.SelectAllValue);
			}
			else
			{
				clientConceptualParameterMetadata = null;
			}
			ClientConceptualParameterMetadata clientConceptualParameterMetadata2 = clientConceptualParameterMetadata;
			return new ClientConceptualColumn(input.ConceptualDefaultAggregate, list, input.Grouping.IsIdentityOnEntityKey, flag, text, input.AggregateBehavior, list2, clientConceptualGroupingMetadata, clientConceptualParameterMetadata2, list4);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00012EFC File Offset: 0x000110FC
		private static IList<ClientConceptualNavigationProperty> Create(IReadOnlyList<IConceptualNavigationProperty> navigationProperties)
		{
			List<ClientConceptualNavigationProperty> list = new List<ClientConceptualNavigationProperty>(navigationProperties.Count);
			for (int i = 0; i < navigationProperties.Count; i++)
			{
				list.Add(ClientConceptualSchemaFactory.Create(navigationProperties[i]));
			}
			return list;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00012F3C File Offset: 0x0001113C
		private static ClientConceptualNavigationProperty Create(IConceptualNavigationProperty navigationProperty)
		{
			return new ClientConceptualNavigationProperty(navigationProperty.Name, navigationProperty.IsActive, (navigationProperty.SourceColumn != null) ? navigationProperty.SourceColumn.Name : null, (navigationProperty.TargetEntity != null) ? navigationProperty.TargetEntity.Name : null, navigationProperty.SourceMultiplicity, navigationProperty.TargetMultiplicity, navigationProperty.Behavior, navigationProperty.CrossFilterDirection);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00012FA0 File Offset: 0x000111A0
		private static IList<ClientConceptualVariationSource> Create(IReadOnlyList<IConceptualVariationSource> variationSources)
		{
			List<ClientConceptualVariationSource> list = new List<ClientConceptualVariationSource>(variationSources.Count);
			for (int i = 0; i < variationSources.Count; i++)
			{
				list.Add(ClientConceptualSchemaFactory.Create(variationSources[i]));
			}
			return list;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00012FE0 File Offset: 0x000111E0
		private static ClientConceptualVariationSource Create(IConceptualVariationSource variationSource)
		{
			return new ClientConceptualVariationSource(variationSource.Name, variationSource.IsDefault, (variationSource.NavigationProperty != null) ? variationSource.NavigationProperty.Name : null, (variationSource.DefaultProperty != null) ? variationSource.DefaultProperty.Name : null, (variationSource.DefaultHierarchy != null) ? variationSource.DefaultHierarchy.Name : null);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00013040 File Offset: 0x00011240
		private static ClientConceptualMeasure CreateMeasure(IConceptualMeasure input, IClientConceptualSchemaHelper helper)
		{
			ClientConceptualKpi clientConceptualKpi = null;
			if (input.Kpi != null)
			{
				clientConceptualKpi = ClientConceptualSchemaFactory.CreateKpi(input.Kpi);
			}
			ClientConceptualMeasureTemplate clientConceptualMeasureTemplate = null;
			if (input.Template != null)
			{
				clientConceptualMeasureTemplate = new ClientConceptualMeasureTemplate(input.Template.DaxTemplateName);
			}
			ClientConceptualChangeDetectionMetadata clientConceptualChangeDetectionMetadata = null;
			if (input.ChangeDetectionMetadata != null)
			{
				clientConceptualChangeDetectionMetadata = new ClientConceptualChangeDetectionMetadata(input.ChangeDetectionMetadata.RefreshInterval);
			}
			ClientConceptualKpi clientConceptualKpi2 = clientConceptualKpi;
			ClientConceptualMeasureTemplate clientConceptualMeasureTemplate2 = clientConceptualMeasureTemplate;
			ConceptualDistributiveAggregateKind? distributiveAggegate = input.DistributiveAggegate;
			IList<string> list;
			if (!input.DistributiveBy.IsNullOrEmpty<IConceptualEntity>())
			{
				list = input.DistributiveBy.Select((IConceptualEntity e) => e.Name).ToList<string>();
			}
			else
			{
				list = null;
			}
			IConceptualMeasure dynamicFormatString = input.DynamicFormatString;
			return new ClientConceptualMeasure(clientConceptualKpi2, clientConceptualMeasureTemplate2, distributiveAggegate, list, (dynamicFormatString != null) ? dynamicFormatString.Name : null, helper == null || helper.CanEditMeasure(input.Entity.Name, input.Name), clientConceptualChangeDetectionMetadata);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00013118 File Offset: 0x00011318
		private static ClientConceptualKpi CreateKpi(IConceptualKpi input)
		{
			string text = ((input.Goal != null) ? input.Goal.Name : null);
			string text2 = ((input.Status != null) ? input.Status.Name : null);
			string text3 = ((input.Trend != null) ? input.Trend.Name : null);
			return new ClientConceptualKpi(input.StatusGraphic, text2, text, text3, input.TrendGraphic, input.Description);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00013184 File Offset: 0x00011384
		private static ClientConceptualGroupingMetadata Create(ConceptualGroupingMetadata input, IClientConceptualSchemaHelper helper)
		{
			return new ClientConceptualGroupingMetadata(input.GroupedColumns.Select((ConceptualGroupedColumnContainer item) => ClientConceptualSchemaFactory.Create(item)).AsList<ClientConceptualGroupedColumnContainer>(), (input.BinningMetadata != null) ? ClientConceptualSchemaFactory.Create(input.BinningMetadata) : null);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000131DB File Offset: 0x000113DB
		private static ClientConceptualBinningMetadata Create(ConceptualBinningMetadata input)
		{
			return new ClientConceptualBinningMetadata(ClientConceptualSchemaFactory.Create(input.BinSize));
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000131ED File Offset: 0x000113ED
		private static ClientConceptualBinSize Create(ConceptualBinSize input)
		{
			return new ClientConceptualBinSize(input.Value, input.Unit);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00013200 File Offset: 0x00011400
		private static ClientConceptualGroupedColumnContainer Create(ConceptualGroupedColumnContainer input)
		{
			if (input.Column != null)
			{
				return new ClientConceptualGroupedColumnContainer(input.Column.Name);
			}
			if (input.HierarchyLevel != null)
			{
				return new ClientConceptualGroupedColumnContainer(ClientConceptualSchemaFactory.Create(input.HierarchyLevel));
			}
			throw new NotSupportedException("Unexpected ConceptualGroupedColumn value");
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0001323E File Offset: 0x0001143E
		private static ClientConceptualHierarchyLevelRef Create(IConceptualHierarchyLevel input)
		{
			return new ClientConceptualHierarchyLevelRef(input.Name, input.Hierarchy.Name);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00013258 File Offset: 0x00011458
		private static ClientTransformCapabilities GetClientTransformCapabilities(TransformCapabilities transformCapabilities)
		{
			if (((transformCapabilities != null) ? transformCapabilities.SupportedTransformCapabilities : null) == null || transformCapabilities.SupportedTransformCapabilities.Count < 1)
			{
				return null;
			}
			List<string> list = new List<string>(transformCapabilities.SupportedTransformCapabilities.Count);
			foreach (TransformCapability transformCapability in transformCapabilities.SupportedTransformCapabilities)
			{
				list.Add(transformCapability.AlgorithmName);
			}
			return new ClientTransformCapabilities(list);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x000132E8 File Offset: 0x000114E8
		public static bool? ConvertTrueToNull(bool desiredValue)
		{
			if (!desiredValue)
			{
				return new bool?(false);
			}
			return null;
		}
	}
}
