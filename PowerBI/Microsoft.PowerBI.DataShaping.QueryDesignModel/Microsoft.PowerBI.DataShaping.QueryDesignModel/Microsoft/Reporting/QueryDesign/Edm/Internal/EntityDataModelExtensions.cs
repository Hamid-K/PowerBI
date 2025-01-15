using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Annotations;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Library;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001ED RID: 493
	internal static class EntityDataModelExtensions
	{
		// Token: 0x06001772 RID: 6002 RVA: 0x0004017B File Offset: 0x0003E37B
		internal static IFederatedConceptualSchema AsFederatedSchema(this EntityDataModel entityDataModel, IFeatureSwitchProvider featureSwitchProvider)
		{
			return entityDataModel.AsConceptualSchema(featureSwitchProvider, false).ToFederatedSchema();
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0004018C File Offset: 0x0003E38C
		internal static IFederatedConceptualSchema AsFederatedSchema(this EntityDataModel entityDataModel, IFeatureSwitchProvider featureSwitchProvider, QueryExtensionSchema extensionSchema, IErrorContext errorContext)
		{
			IConceptualSchema conceptualSchema = entityDataModel.AsConceptualSchema(featureSwitchProvider, false);
			QueryResolutionErrorContext queryResolutionErrorContext = new QueryResolutionErrorContext(errorContext);
			IConceptualSchema conceptualSchema2;
			if (!ExtensionConceptualSchemaBuilder.TryCreateConceptualSchema(conceptualSchema, extensionSchema, queryResolutionErrorContext, out conceptualSchema2))
			{
				Microsoft.DataShaping.Contract.RetailFail("Failed to create a ConceptualSchema that extends a base model by reading a QueryExtensionModel.");
			}
			return new FederatedConceptualSchema(new IConceptualSchema[] { conceptualSchema, conceptualSchema2 });
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000401D2 File Offset: 0x0003E3D2
		internal static bool CanGroupOnValue(this IConceptualProperty property)
		{
			return property.ConceptualDataCategory != ConceptualDataCategory.Image && property.ConceptualDataType != ConceptualPrimitiveType.Binary;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000401EC File Offset: 0x0003E3EC
		internal static IConceptualSchema AsConceptualSchema(this EntityDataModel entityDataModel, bool skipCaching = false)
		{
			return EntityDataModelExtensions.AsConceptualSchema(entityDataModel, EntityDataModelExtensions.BuildOptions(null), skipCaching);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x000401FB File Offset: 0x0003E3FB
		public static IConceptualSchema AsConceptualSchema(this EntityDataModel entityDataModel, IFeatureSwitchProvider featureSwitchProvider, bool skipCaching = false)
		{
			return EntityDataModelExtensions.AsConceptualSchema(entityDataModel, EntityDataModelExtensions.BuildOptions(featureSwitchProvider), skipCaching);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0004020C File Offset: 0x0003E40C
		private static IConceptualSchema AsConceptualSchema(EntityDataModel entityDataModel, ConceptualSchemaBuilderOptions options, bool skipCaching)
		{
			if (skipCaching)
			{
				return EntityDataModelExtensions.CreateConceptualSchema(entityDataModel, options);
			}
			if (entityDataModel.ConceptualSchemaCache == null)
			{
				IConceptualSchema conceptualSchema = EntityDataModelExtensions.CreateConceptualSchema(entityDataModel, options);
				entityDataModel.SetConceptualSchemaCache(conceptualSchema);
			}
			return (IConceptualSchema)entityDataModel.ConceptualSchemaCache;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00040248 File Offset: 0x0003E448
		private static IConceptualSchema CreateConceptualSchema(EntityDataModel entityDataModel, ConceptualSchemaBuilderOptions options)
		{
			ConceptualCapabilities conceptualCapabilities = EntityDataModelExtensions.CreateCapabilities(entityDataModel, options);
			EntitySetCollection entitySets = entityDataModel.EntitySets;
			List<IConceptualEntity> list = new List<IConceptualEntity>(entitySets.Count);
			Dictionary<string, IConceptualEntity> dictionary = new Dictionary<string, IConceptualEntity>(entitySets.Count, EdmItem.IdentityComparer);
			Dictionary<string, IConceptualEntity> dictionary2 = new Dictionary<string, IConceptualEntity>(entitySets.Count, EdmItem.ReferenceNameComparer);
			StatisticsAnnotationProviderBuilder statisticsAnnotationProviderBuilder = new StatisticsAnnotationProviderBuilder();
			for (int i = 0; i < entitySets.Count; i++)
			{
				IConceptualEntity conceptualEntity = EntityDataModelExtensions.CreateEntity(entityDataModel.EntitySets[i], statisticsAnnotationProviderBuilder);
				list.Add(conceptualEntity);
				dictionary.Add(conceptualEntity.GetFullName(), conceptualEntity);
				dictionary2.Add(conceptualEntity.Name, conceptualEntity);
			}
			Dictionary<string, AssociationSet> dictionary3 = new Dictionary<string, AssociationSet>(entityDataModel.AssociationSets.Count, EdmItem.IdentityComparer);
			foreach (AssociationSet associationSet in entityDataModel.AssociationSets)
			{
				dictionary3.Add(associationSet.ElementType.FullName, associationSet);
			}
			ConceptualCollation conceptualCollation = new ConceptualCollation(entityDataModel.Culture, entityDataModel.CompareOptions, entityDataModel.PreferOrdinalStringEquality);
			IConceptualMeasure defaultMeasure = EntityDataModelExtensions.GetDefaultMeasure(entityDataModel.EntityContainer.DefaultMeasureName, list);
			EdmConceptualSchema edmConceptualSchema = new EdmConceptualSchema(list, dictionary, dictionary2, conceptualCapabilities, entityDataModel.Culture, entityDataModel.Caption, conceptualCollation, defaultMeasure);
			for (int j = 0; j < entitySets.Count; j++)
			{
				EdmConceptualEntity edmConceptualEntity = list[j] as EdmConceptualEntity;
				EntitySet entitySet = entityDataModel.EntitySets[j];
				IReadOnlyCollection<IConceptualNavigationProperty> readOnlyCollection = EntityDataModelExtensions.CreateNavigationProperties(entitySet, dictionary, dictionary3);
				edmConceptualEntity.CompleteInitialization(edmConceptualSchema, readOnlyCollection);
				EdmConceptualPropertyInitContext edmConceptualPropertyInitContext = new EdmConceptualPropertyInitContext(edmConceptualEntity, entitySet, entityDataModel.EntityContainer.MappedMPrameters);
				foreach (IConceptualProperty conceptualProperty in edmConceptualEntity.Properties)
				{
					((EdmConceptualProperty)conceptualProperty).CompleteInitialization(edmConceptualPropertyInitContext);
				}
				foreach (IConceptualHierarchy conceptualHierarchy in edmConceptualEntity.Hierarchies)
				{
					foreach (IConceptualHierarchyLevel conceptualHierarchyLevel in conceptualHierarchy.Levels)
					{
						((EdmConceptualHierarchyLevel)conceptualHierarchyLevel).CompleteInitialization(conceptualHierarchy);
					}
				}
			}
			EntityDataModelExtensions.RegisterAnnotations(edmConceptualSchema, entityDataModel, statisticsAnnotationProviderBuilder);
			return edmConceptualSchema;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x000404DC File Offset: 0x0003E6DC
		private static IReadOnlyCollection<IConceptualNavigationProperty> CreateNavigationProperties(EntitySet edmEntitySet, IReadOnlyDictionary<string, IConceptualEntity> entitiesByName, IReadOnlyDictionary<string, AssociationSet> associationsByName)
		{
			List<IConceptualNavigationProperty> list = new List<IConceptualNavigationProperty>(edmEntitySet.ElementType.NavigationProperties.Count);
			foreach (EdmNavigationProperty edmNavigationProperty in edmEntitySet.ElementType.NavigationProperties)
			{
				AssociationSet associationSet;
				associationsByName.TryGetValue(edmNavigationProperty.AssociationName, out associationSet);
				AssociationSetEnd associationSetEnd = associationSet.AssociationSetEnds[edmNavigationProperty.AssociationTargetName];
				IConceptualEntity conceptualEntity;
				entitiesByName.TryGetValue(associationSetEnd.EntitySet.FullName, out conceptualEntity);
				EdmConceptualNavigationProperty edmConceptualNavigationProperty = new EdmConceptualNavigationProperty(edmNavigationProperty, associationSet.State == AssociationState.Active, EntityDataModelExtensions.ConvertCrossFilterDirection(associationSet.CrossFilterDirection), conceptualEntity, EntityDataModelExtensions.ConvertNavigationBehavior(associationSet.Behavior));
				list.Add(edmConceptualNavigationProperty);
			}
			return list.ToReadOnlyCollection<IConceptualNavigationProperty>();
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x000405B0 File Offset: 0x0003E7B0
		private static ConceptualCapabilities CreateCapabilities(EntityDataModel entityDataModel, ConceptualSchemaBuilderOptions options)
		{
			ModelCapabilities modelCapabilities = entityDataModel.ModelCapabilities;
			DAXFunctions daxFunctions = modelCapabilities.DaxFunctions;
			TransformCapabilities transformCapabilities = null;
			IReadOnlyList<string> readOnlyList;
			if (daxFunctions == null)
			{
				readOnlyList = null;
			}
			else
			{
				DaxExtensionFunctions daxExtensionFunctions = daxFunctions.DaxExtensionFunctions;
				readOnlyList = ((daxExtensionFunctions != null) ? daxExtensionFunctions.SupportedDaxExtensionFunctions : null);
			}
			IReadOnlyList<string> readOnlyList2 = readOnlyList;
			if (readOnlyList2 != null)
			{
				transformCapabilities = new TransformCapabilities(readOnlyList2);
			}
			bool flag = daxFunctions.BinaryMinMax == BinaryMinMaxType.DefaultSupport;
			bool flag2 = entityDataModel.ModelCapabilities.IsMultidimensional();
			bool flag3 = daxFunctions.SummarizeColumns == SummarizeColumnsType.DefaultSupport;
			bool flag4 = modelCapabilities.MultiColumnFiltering == MultiColumnFilteringType.LimitedToGroupByColumns;
			bool flag5 = daxFunctions.FormatByLocale == FormatByLocale.DefaultSupport;
			bool flag6 = flag3 && daxFunctions.TreatAs == TreatAsType.DefaultSupport && !flag4;
			bool flag7 = false;
			bool flag8 = false;
			if (flag3)
			{
				flag7 = daxFunctions.SampleAxisWithLocalMinMax == SampleAxisWithLocalMinMaxType.DefaultSupport;
				flag8 = daxFunctions.SampleCartesianPointsByCover == SampleCartesianPointsByCoverType.DefaultSupport;
			}
			bool flag9 = flag3;
			bool flag10 = flag3;
			bool flag11 = flag3;
			bool flag12 = flag3;
			bool flag13 = flag3;
			bool flag14 = flag5 && flag3 && options.SparklineDataEnabled && !flag2;
			bool flag15 = flag3 && !flag2;
			bool flag16 = modelCapabilities.DataSourceVariables == DataSourceVariablesType.DefaultSupport;
			bool flag17 = daxFunctions.TopNPerLevel == TopNPerLevelType.DefaultSupport;
			bool flag18 = entityDataModel.ModelCapabilities.VirtualColumns == VirtualColumnsType.DefaultSupport;
			bool flag19 = modelCapabilities.VisualCalculations == VisualCalculationType.DefaultSupport;
			return new ConceptualCapabilities(modelCapabilities.QueryAggregateUsage == QueryAggregateUsageType.Discourage, modelCapabilities.DiscourageCompositeModels, flag, flag, modelCapabilities.FiveStateKPIRange == FiveStateKPIRangeType.Normalized, flag15, daxFunctions.StringMinMax == StringMinMaxType.DefaultSupport, flag6, flag7, flag8, flag4, flag9, flag16, flag11, flag10, flag17, flag13, flag18, flag12, flag14, flag19, transformCapabilities);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00040712 File Offset: 0x0003E912
		public static bool SupportsNegatedMultiTableTuplesFiltering(this ModelCapabilities edmCapabilities)
		{
			return edmCapabilities.DaxFunctions.SummarizeColumns == SummarizeColumnsType.DefaultSupport;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00040722 File Offset: 0x0003E922
		public static bool SupportsKeepFiltersOverTableVariable(this ModelCapabilities edmCapabilities)
		{
			return edmCapabilities.DaxFunctions.TreatAs == TreatAsType.DefaultSupport;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00040732 File Offset: 0x0003E932
		public static bool SupportsKeepFiltersOverTableVariable(this DaxCapabilitiesAnnotation daxCapabilities)
		{
			return daxCapabilities.DaxFunctions.SupportsTreatAs;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0004073F File Offset: 0x0003E93F
		public static bool SupportsRollupContextTables(this ModelCapabilities edmCapabilities)
		{
			return edmCapabilities.DaxFunctions.NonVisual == NonVisualType.DefaultSupport;
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0004074F File Offset: 0x0003E94F
		public static bool SupportsRollupContextTables(this DaxCapabilitiesAnnotation daxCapabilities)
		{
			return daxCapabilities.DaxFunctions.SupportsNonVisual;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0004075C File Offset: 0x0003E95C
		public static bool IsMultidimensional(this ModelCapabilities edmCapabilities)
		{
			return edmCapabilities.FiveStateKPIRange > FiveStateKPIRangeType.Default;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x00040767 File Offset: 0x0003E967
		public static bool IsMultidimensional(this ConceptualCapabilities conceptualCapabilities)
		{
			return conceptualCapabilities.NormalizedFiveStateKpiRange;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0004076F File Offset: 0x0003E96F
		public static bool IsMultidimensional(this DaxCapabilitiesAnnotation daxCapabilities)
		{
			return daxCapabilities.SupportsNormalizedFiveStateKpiRange;
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00040777 File Offset: 0x0003E977
		public static bool DiscourageCountRowsOverTables(this IConceptualSchema schema)
		{
			return schema.GetDaxCapabilitiesAnnotation().IsMultidimensional();
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00040784 File Offset: 0x0003E984
		private static IConceptualEntity CreateEntity(EntitySet edmEntitySet, StatisticsAnnotationProviderBuilder statisticsProviderBuilder)
		{
			IList<EdmPropertyInstance> list = edmEntitySet.GetProperties().Evaluate<EdmPropertyInstance>();
			List<IConceptualProperty> list2 = new List<IConceptualProperty>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				list2.Add(EntityDataModelExtensions.CreateProperty(list[i], edmEntitySet, statisticsProviderBuilder));
			}
			IList<EdmHierarchyInstance> list3 = edmEntitySet.GetHierarchies().Evaluate<EdmHierarchyInstance>();
			List<IConceptualHierarchy> list4 = new List<IConceptualHierarchy>(list3.Count);
			for (int j = 0; j < list3.Count; j++)
			{
				list4.Add(EntityDataModelExtensions.CreateHierarchy(list3[j], list2));
			}
			return new EdmConceptualEntity(edmEntitySet, list2.AsReadOnly(), list4.AsReadOnly());
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00040828 File Offset: 0x0003EA28
		private static IConceptualMeasure GetDefaultMeasure(string edmDefaultMeasureName, List<IConceptualEntity> entities)
		{
			if (edmDefaultMeasureName == null)
			{
				return null;
			}
			IConceptualMeasure conceptualMeasure = null;
			Func<IConceptualProperty, bool> <>9__0;
			foreach (IConceptualEntity conceptualEntity in entities)
			{
				IEnumerable<IConceptualProperty> properties = conceptualEntity.Properties;
				Func<IConceptualProperty, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (IConceptualProperty p) => ConceptualNameComparer.Instance.Equals(p.EdmName, edmDefaultMeasureName));
				}
				IConceptualProperty conceptualProperty = properties.FirstOrDefault(func);
				if (conceptualProperty != null)
				{
					conceptualMeasure = conceptualProperty as IConceptualMeasure;
					if (conceptualMeasure == null)
					{
						Microsoft.DataShaping.Contract.RetailFail("Specified DefaultMeasure name does not refer to a measure");
						break;
					}
					break;
				}
			}
			if (conceptualMeasure == null)
			{
				Microsoft.DataShaping.Contract.RetailFail("Cannot find property reference for DefaultMeasure");
			}
			return conceptualMeasure;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x000408DC File Offset: 0x0003EADC
		private static IConceptualProperty CreateProperty(EdmPropertyInstance edmPropertyInstance, EntitySet edmEntitySet, StatisticsAnnotationProviderBuilder statisticsProviderBuilder)
		{
			if (edmPropertyInstance.Property is EdmMeasure)
			{
				return new EdmConceptualMeasure(edmPropertyInstance.Property);
			}
			EdmConceptualColumn edmConceptualColumn = new EdmConceptualColumn(edmPropertyInstance.Property);
			ConceptualColumnStatistics conceptualColumnStatistics = EntityDataModelExtensions.ConvertStatistics(edmPropertyInstance.Property.Statistics);
			if (conceptualColumnStatistics != null)
			{
				statisticsProviderBuilder.Register(edmEntitySet.ReferenceName, edmConceptualColumn.Name, conceptualColumnStatistics);
			}
			return edmConceptualColumn;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x0004093C File Offset: 0x0003EB3C
		private static IConceptualHierarchy CreateHierarchy(EdmHierarchyInstance edmHierarchyInstance, IEnumerable<IConceptualProperty> properties)
		{
			EdmHierarchy hierarchy = edmHierarchyInstance.Hierarchy;
			List<IConceptualHierarchyLevel> list = new List<IConceptualHierarchyLevel>(hierarchy.Levels.Count);
			for (int i = 0; i < hierarchy.Levels.Count; i++)
			{
				list.Add(EntityDataModelExtensions.CreateHierarchyLevel(hierarchy.Levels[i], properties));
			}
			return new EdmConceptualHierarchy(edmHierarchyInstance.Hierarchy, list);
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x000409A0 File Offset: 0x0003EBA0
		private static IConceptualHierarchyLevel CreateHierarchyLevel(EdmHierarchyLevel edmHierarchyLevel, IEnumerable<IConceptualProperty> properties)
		{
			IConceptualProperty conceptualProperty = properties.FirstOrDefault((IConceptualProperty p) => p.Name == edmHierarchyLevel.Source.ReferenceName);
			return new EdmConceptualHierarchyLevel(edmHierarchyLevel, conceptualProperty);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x000409D9 File Offset: 0x0003EBD9
		private static CrossFilterDirection ConvertCrossFilterDirection(CrossFilterDirection crossFilterDirection)
		{
			if (crossFilterDirection == CrossFilterDirection.Single)
			{
				return CrossFilterDirection.Single;
			}
			if (crossFilterDirection == CrossFilterDirection.Both)
			{
				return CrossFilterDirection.Both;
			}
			throw new InvalidDataContractException(Microsoft.Reporting.StringUtil.FormatInvariant("Unmapped CrossFilterDirection {0}", new object[] { crossFilterDirection }));
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00040A04 File Offset: 0x0003EC04
		private static ConceptualNavigationBehavior ConvertNavigationBehavior(AssociationBehavior behavior)
		{
			if (behavior == AssociationBehavior.Default)
			{
				return ConceptualNavigationBehavior.Default;
			}
			if (behavior == AssociationBehavior.Weak)
			{
				return ConceptualNavigationBehavior.Weak;
			}
			throw new InvalidDataContractException(Microsoft.Reporting.StringUtil.FormatInvariant("Unmapped AssociationBehavior {0}", new object[] { behavior }));
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00040A30 File Offset: 0x0003EC30
		private static void RegisterAnnotations(EdmConceptualSchema schema, EntityDataModel entityDataModel, StatisticsAnnotationProviderBuilder statisticsProviderBuilder)
		{
			schema.RegisterAnnotationProvider<DaxCapabilitiesAnnotation, IConceptualSchema>(EntityDataModelExtensions.CreateDaxCapabilitiesProvider(entityDataModel));
			schema.RegisterAnnotationProvider<NavigationPropertyGraphAnnotation, IConceptualSchema>(new NavigationPropertyGraphAnnotationProvider(schema));
			schema.RegisterAnnotationProvider<MParameterAnnotation, IConceptualSchema>(new MParameterAnnotationProvider(schema));
			schema.RegisterAnnotationProvider<FieldRelationshipAnnotations, IConceptualSchema>(EntityDataModelExtensions.CreateFieldRelationshipAnnotationsProvider(schema));
			schema.RegisterAnnotationProvider<ColumnGroupingAnnotations, IConceptualSchema>(new ColumnGroupingAnnotationsProvider(() => ColumnGroupingAnnotationsProviderBuilder.BuildColumnGroupingAnnotations(schema), schema));
			if (!entityDataModel.EntitySets.IsNullOrEmpty<EntitySet>())
			{
				schema.RegisterAnnotationProvider<CsdlSchemaNamespaceAnnotation, IConceptualSchema>(new CsdlSchemaNamespaceAnnotationProvider(new CsdlSchemaNamespaceAnnotation(entityDataModel.EntitySets.First<EntitySet>().ElementType.NamespaceName)));
			}
			statisticsProviderBuilder.BuildAndRegisterAnnotationProviders(schema);
			schema.RegisterAnnotationProvider<ComparerAnnotation, IConceptualSchema>(new ComparerAnnotationProvider(() => ComparerAnnotationProvider.BuildComparerAnnotation(schema), schema));
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00040B2C File Offset: 0x0003ED2C
		private static DaxCapabilitiesAnnotationProvider CreateDaxCapabilitiesProvider(EntityDataModel entityDataModel)
		{
			ModelCapabilities modelCapabilities = entityDataModel.ModelCapabilities;
			DAXFunctions daxFunctions = modelCapabilities.DaxFunctions;
			bool flag = modelCapabilities.GroupByValidation == GroupByValidationType.Enforced;
			bool flag2 = modelCapabilities.QueryAggregateUsage == QueryAggregateUsageType.Discourage;
			bool flag3 = modelCapabilities.CrossFilteringWithinTable == CrossFilteringWithinTableType.Always;
			bool flag4 = modelCapabilities.QueryBatching == QueryBatchingType.Supported;
			bool flag5 = modelCapabilities.Variables > VariablesType.NotSupported;
			bool flag6 = modelCapabilities.InOperator == InOperatorType.DefaultSupport;
			bool flag7 = modelCapabilities.VirtualColumns == VirtualColumnsType.DefaultSupport;
			bool flag8 = modelCapabilities.MultiColumnFiltering == MultiColumnFilteringType.LimitedToGroupByColumns;
			bool flag9 = modelCapabilities.DataSourceVariables == DataSourceVariablesType.DefaultSupport;
			bool flag10 = modelCapabilities.ExecutionMetrics == ExecutionMetricsType.DefaultSupport;
			bool flag11 = modelCapabilities.FiveStateKPIRange == FiveStateKPIRangeType.Normalized;
			return new DaxCapabilitiesAnnotationProvider(new DaxCapabilitiesAnnotation(flag, flag2, flag3, flag4, flag5, flag6, flag7, modelCapabilities.TableConstructor == TableConstructorType.DefaultSupport, flag8, flag9, flag10, flag11, modelCapabilities.IsMultidimensional(), modelCapabilities.VisualCalculations == VisualCalculationType.DefaultSupport, new DaxFunctionsAnnotation(!modelCapabilities.IsMultidimensional() && daxFunctions.TreatAs == TreatAsType.DefaultSupport, modelCapabilities.EncourageIsEmptyDAXFunctionUsage, entityDataModel.Version == null || entityDataModel.Version > EntityDataModel.VersionOnePointZero, daxFunctions.BinaryMinMax == BinaryMinMaxType.DefaultSupport, daxFunctions.SubstituteWithIndex == SubstituteWithIndexType.DefaultSupport, daxFunctions.SummarizeColumns == SummarizeColumnsType.DefaultSupport, daxFunctions.SummarizeColumns == SummarizeColumnsType.DefaultSupport, daxFunctions.SummarizeColumns == SummarizeColumnsType.DefaultSupport, daxFunctions.SummarizeColumns == SummarizeColumnsType.DefaultSupport, daxFunctions.TreatAs == TreatAsType.DefaultSupport, daxFunctions.StringMinMax == StringMinMaxType.DefaultSupport, daxFunctions.SampleAxisWithLocalMinMax == SampleAxisWithLocalMinMaxType.DefaultSupport, daxFunctions.OptimizedNotInOperator == OptimizedNotInOperatorType.DefaultSupport, daxFunctions.NonVisual == NonVisualType.DefaultSupport, daxFunctions.LeftOuterJoin == LeftOuterJoinType.DefaultSupport, daxFunctions.IsAfter == IsAfterType.DefaultSupport, daxFunctions.FormatByLocale == FormatByLocale.DefaultSupport)));
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00040C98 File Offset: 0x0003EE98
		private static FieldRelationshipAnnotationsProvider CreateFieldRelationshipAnnotationsProvider(EdmConceptualSchema schema)
		{
			Dictionary<IConceptualColumn, FieldRelationshipAnnotation> dictionary = new Dictionary<IConceptualColumn, FieldRelationshipAnnotation>();
			foreach (IConceptualEntity conceptualEntity in schema.Entities)
			{
				foreach (IConceptualProperty conceptualProperty in conceptualEntity.Properties)
				{
					EdmConceptualColumn edmConceptualColumn = conceptualProperty as EdmConceptualColumn;
					if (edmConceptualColumn != null)
					{
						EdmFieldRelationship relationship = edmConceptualColumn.Relationship;
						EdmField relatedToSource = relationship.RelatedToSource;
						ReadOnlyCollection<EdmField> relatedToFields = relationship.RelatedToFields;
						if (relatedToFields.Count != 0 || relatedToSource != null)
						{
							IConceptualColumn conceptualColumn = null;
							if (relatedToSource != null)
							{
								IConceptualProperty conceptualProperty2;
								conceptualEntity.TryGetPropertyByEdmName(relatedToSource.Name, out conceptualProperty2);
								conceptualColumn = ((conceptualProperty2 != null) ? conceptualProperty2.AsColumn() : null);
								Microsoft.DataShaping.Contract.RetailAssert(conceptualColumn != null, "Fail to get IConceptualColumn from EdmField");
							}
							List<IConceptualColumn> propertiesFromEdmFields = EntityDataModelExtensions.GetPropertiesFromEdmFields(conceptualEntity, relatedToFields);
							List<IConceptualColumn> propertiesFromEdmFields2 = EntityDataModelExtensions.GetPropertiesFromEdmFields(conceptualEntity, relationship.AllFieldsOnPath);
							FieldRelationshipAnnotation fieldRelationshipAnnotation = new FieldRelationshipAnnotation(conceptualColumn, propertiesFromEdmFields, propertiesFromEdmFields2);
							dictionary.Add(edmConceptualColumn, fieldRelationshipAnnotation);
						}
					}
				}
			}
			return new FieldRelationshipAnnotationsProvider(new FieldRelationshipAnnotations(dictionary), schema);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00040DCC File Offset: 0x0003EFCC
		private static List<IConceptualColumn> GetPropertiesFromEdmFields(IConceptualEntity entity, IReadOnlyList<EdmField> fields)
		{
			if (fields.IsNullOrEmpty<EdmField>())
			{
				return null;
			}
			List<IConceptualColumn> list = new List<IConceptualColumn>(fields.Count);
			foreach (EdmField edmField in fields)
			{
				IConceptualProperty conceptualProperty;
				entity.TryGetPropertyByEdmName(edmField.Name, out conceptualProperty);
				Microsoft.DataShaping.Contract.RetailAssert(conceptualProperty != null, "Fail to get IConceptualProperty from EdmField");
				list.Add(conceptualProperty.AsColumn());
			}
			return list;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00040E50 File Offset: 0x0003F050
		private static ConceptualColumnStatistics ConvertStatistics(EdmPropertyStatistics edmStats)
		{
			if (edmStats == null)
			{
				return null;
			}
			return new ConceptualColumnStatistics(edmStats.DistinctValueCount, null, null);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00040E64 File Offset: 0x0003F064
		internal static ConceptualSchemaBuilderOptions BuildOptions(IFeatureSwitchProvider featureSwitchProvider)
		{
			bool flag = featureSwitchProvider != null && featureSwitchProvider.IsEnabled(FeatureSwitchKind.SparklineData);
			return new ConceptualSchemaBuilderOptions(false, false, flag);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00040E87 File Offset: 0x0003F087
		internal static IDataComparer GetComparer(EntityDataModel model, IFederatedConceptualSchema federatedSchema, IFeatureSwitchProvider featureSwitchProvider)
		{
			return EntityDataModelExtensions.GetComparer(model, federatedSchema.GetDefaultSchema(), featureSwitchProvider);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00040E96 File Offset: 0x0003F096
		internal static IDataComparer GetComparer(EntityDataModel model, IConceptualSchema schema, IFeatureSwitchProvider featureSwitchProvider)
		{
			if (!featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema))
			{
				return model.Comparer;
			}
			return schema.GetComparer();
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00040EB0 File Offset: 0x0003F0B0
		internal static EdmMeasureInstance? GetCorrespondingEdmMeasure(this EntityDataModel model, IConceptualMeasure conceptualMeasure)
		{
			if (conceptualMeasure == null)
			{
				return null;
			}
			EntitySet entitySet = model.EntitySets.FindByEdmReferenceName(conceptualMeasure.Entity.Name);
			EdmMeasure edmMeasure = entitySet.ElementType.Members[conceptualMeasure.EdmName] as EdmMeasure;
			return new EdmMeasureInstance?(entitySet.MeasureInstance(edmMeasure));
		}
	}
}
