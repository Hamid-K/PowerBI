using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Csdl;
using Microsoft.Data.Edm.Validation;
using Microsoft.Data.Edm.Values;
using Microsoft.InfoNav.Common.Reflection;
using Microsoft.InfoNav.Data.Annotations;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.Utils;
using Microsoft.InfoNav.Defaults;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x02000021 RID: 33
	[ImmutableObject(true)]
	public sealed class ConceptualSchemaBuilder
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00004BC0 File Offset: 0x00002DC0
		private ConceptualSchemaBuilder(IEdmModel edmModel, ITracer tracer, Version csdlVersion, ConceptualSchemaBuilderOptions options)
		{
			this._edmModel = edmModel;
			this._tracer = tracer;
			this._csdlVersion = csdlVersion;
			this._options = options;
			this._rowNumberProviderBuilder = (options.BuildRowNumberAnnotations ? new RowNumberAnnotationProviderBuilder() : null);
			this._statisticsProviderBuilder = new StatisticsAnnotationProviderBuilder();
			this._translationsProviderBuilder = new TranslationsAnnotationProviderBuilder();
			this._errorStateProviderBuilder = new ErrorStateAnnotationProviderBuilder();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004C28 File Offset: 0x00002E28
		public static bool TryCreateConceptualSchema(IEnumerable<XmlReader> schemaReaders, ITracer tracer, ConceptualSchemaBuilderOptions options, out IConceptualSchema schema, out IList<ConceptualSchemaLoadError> deserializationErrors)
		{
			return ConceptualSchemaBuilder.TryCreateConceptualSchema(schemaReaders, tracer, null, "", options, out schema, out deserializationErrors);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004C3B File Offset: 0x00002E3B
		public static bool TryCreateConceptualSchema(IEnumerable<XmlReader> schemaReaders, ITracer tracer, Version csdlVersion, ConceptualSchemaBuilderOptions options, out IConceptualSchema schema, out IList<ConceptualSchemaLoadError> deserializationErrors)
		{
			return ConceptualSchemaBuilder.TryCreateConceptualSchema(schemaReaders, tracer, csdlVersion, string.Empty, options, out schema, out deserializationErrors);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004C50 File Offset: 0x00002E50
		internal static bool TryCreateConceptualSchema(IEnumerable<XmlReader> schemaReaders, ITracer tracer, Version csdlVersion, string schemaId, ConceptualSchemaBuilderOptions options, out IConceptualSchema schema, out IList<ConceptualSchemaLoadError> deserializationErrors)
		{
			ModelDaxCapabilities modelDaxCapabilities;
			return ConceptualSchemaBuilder.TryCreateConceptualSchema(schemaReaders, tracer, csdlVersion, schemaId, options, out schema, out modelDaxCapabilities, out deserializationErrors);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004C6E File Offset: 0x00002E6E
		internal static bool TryCreateConceptualSchema(IEnumerable<XmlReader> schemaReaders, ITracer tracer, Version csdlVersion, ConceptualSchemaBuilderOptions options, out IConceptualSchema schema, out ModelDaxCapabilities daxCapabilities, out IList<ConceptualSchemaLoadError> deserializationErrors)
		{
			return ConceptualSchemaBuilder.TryCreateConceptualSchema(schemaReaders, tracer, csdlVersion, string.Empty, options, out schema, out daxCapabilities, out deserializationErrors);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004C84 File Offset: 0x00002E84
		internal static bool TryCreateConceptualSchema(IEnumerable<XmlReader> schemaReaders, ITracer tracer, Version csdlVersion, string schemaId, ConceptualSchemaBuilderOptions options, out IConceptualSchema schema, out ModelDaxCapabilities daxCapabilities, out IList<ConceptualSchemaLoadError> deserializationErrors)
		{
			if (tracer == null)
			{
				tracer = DefaultTracer.Instance;
			}
			IEdmModel edmModel;
			IEnumerable<EdmError> enumerable;
			if (!CsdlReader.TryParse(schemaReaders, ref edmModel, ref enumerable))
			{
				daxCapabilities = null;
				schema = null;
				deserializationErrors = ConceptualSchemaBuilder.ConvertErrors(enumerable);
				return false;
			}
			schema = ConceptualSchemaBuilder.CreateConceptualSchema(edmModel, tracer, csdlVersion, schemaId, options, out daxCapabilities);
			deserializationErrors = null;
			return true;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004CD0 File Offset: 0x00002ED0
		private static ConceptualSchema CreateConceptualSchema(IEdmModel edmModel, ITracer tracer, Version csdlVersion, string schemaId, ConceptualSchemaBuilderOptions options, out ModelDaxCapabilities daxCapabilities)
		{
			ConceptualSchemaBuilder conceptualSchemaBuilder = new ConceptualSchemaBuilder(edmModel, tracer, csdlVersion, options);
			daxCapabilities = conceptualSchemaBuilder.GetDaxCapabilities();
			return conceptualSchemaBuilder.CreateConceptualSchema(schemaId, tracer);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004CFC File Offset: 0x00002EFC
		private ConceptualSchema CreateConceptualSchema(string schemaId, ITracer tracer)
		{
			LanguageIdentifier schemaLanguageOrDefault = this.GetSchemaLanguageOrDefault();
			List<IEdmEntitySet> list;
			IReadOnlyList<IConceptualEntity> readOnlyList = this.CreateConceptualEntities(out list);
			ConceptualCapabilities capabilities = this.GetCapabilities();
			XElement entityContainerElement = this.GetEntityContainerElement();
			string displayName = ConceptualSchemaBuilder.GetDisplayName(entityContainerElement);
			ConceptualCollation conceptualCollation = this.GetConceptualCollation(entityContainerElement);
			IConceptualMeasure defaultMeasure = this.GetDefaultMeasure(entityContainerElement, readOnlyList);
			ConceptualSchema conceptualSchema = new ConceptualSchema(schemaLanguageOrDefault, readOnlyList, schemaId, displayName, capabilities, conceptualCollation, defaultMeasure);
			this.CompleteInitialization(conceptualSchema, list, tracer);
			this.RegisterAnnotationProviders(conceptualSchema);
			return conceptualSchema;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004D65 File Offset: 0x00002F65
		private IEdmEntityContainer GetEntityContainer()
		{
			return ExtensionMethods.EntityContainers(this._edmModel).EmptyIfNull<IEdmEntityContainer>().FirstOrDefault<IEdmEntityContainer>();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004D7C File Offset: 0x00002F7C
		private ConceptualCapabilities GetCapabilities()
		{
			IEdmEntityContainer entityContainer = this.GetEntityContainer();
			if (entityContainer == null)
			{
				return new ConceptualCapabilities();
			}
			return entityContainer.GetCapabilities(this._edmModel, this._options);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004DAC File Offset: 0x00002FAC
		private ModelDaxCapabilities GetDaxCapabilities()
		{
			IEdmEntityContainer entityContainer = this.GetEntityContainer();
			if (entityContainer == null)
			{
				return new ModelDaxCapabilities(false, false, false, false);
			}
			return entityContainer.GetDaxCapabilities(this._edmModel);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004DDC File Offset: 0x00002FDC
		private LanguageIdentifier GetSchemaLanguageOrDefault()
		{
			IEdmEntityContainer entityContainer = this.GetEntityContainer();
			LanguageIdentifier languageIdentifier;
			if (entityContainer != null && entityContainer.TryGetLanguageIdentifier(this._edmModel, out languageIdentifier))
			{
				return languageIdentifier;
			}
			return LanguageIdentifier.en_US;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004E0C File Offset: 0x0000300C
		private XElement GetEntityContainerElement()
		{
			IEdmEntityContainer entityContainer = this.GetEntityContainer();
			if (entityContainer == null)
			{
				return null;
			}
			IEdmStringValue annotationValue = ExtensionMethods.GetAnnotationValue<IEdmStringValue>(this._edmModel, entityContainer, "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions", "EntityContainer");
			if (annotationValue == null)
			{
				return null;
			}
			return XElement.Parse(annotationValue.Value);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004E4C File Offset: 0x0000304C
		private ConceptualCollation GetConceptualCollation(XElement entityContainerElement)
		{
			string text = null;
			CompareOptions compareOptions = CompareOptions.None;
			bool flag = false;
			if (entityContainerElement != null)
			{
				text = entityContainerElement.GetStringAttributeValueOrDefault(EdmConstants.Culture, null);
				XElement xelement = entityContainerElement.Element(EdmConstants.CompareOptionsElem);
				if (xelement != null)
				{
					bool booleanAttributeValueOrDefault = xelement.GetBooleanAttributeValueOrDefault(EdmConstants.IgnoreCaseAttr, this._tracer, false);
					bool booleanAttributeValueOrDefault2 = xelement.GetBooleanAttributeValueOrDefault(EdmConstants.IgnoreKanaTypeAttr, this._tracer, false);
					bool booleanAttributeValueOrDefault3 = xelement.GetBooleanAttributeValueOrDefault(EdmConstants.IgnoreNonSpaceAttr, this._tracer, false);
					bool booleanAttributeValueOrDefault4 = xelement.GetBooleanAttributeValueOrDefault(EdmConstants.IgnoreWidthAttr, this._tracer, false);
					compareOptions |= (booleanAttributeValueOrDefault ? CompareOptions.IgnoreCase : CompareOptions.None);
					compareOptions |= (booleanAttributeValueOrDefault2 ? CompareOptions.IgnoreKanaType : CompareOptions.None);
					compareOptions |= (booleanAttributeValueOrDefault3 ? CompareOptions.IgnoreNonSpace : CompareOptions.None);
					compareOptions |= (booleanAttributeValueOrDefault4 ? CompareOptions.IgnoreWidth : CompareOptions.None);
				}
				flag = entityContainerElement.GetBooleanAttributeValueOrDefault(EdmConstants.PreferOrdinalStringEqualityAtrr, this._tracer, false);
			}
			return new ConceptualCollation(text, compareOptions, flag);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004F1C File Offset: 0x0000311C
		private IConceptualMeasure GetDefaultMeasure(XElement entityContainerElement, IReadOnlyList<IConceptualEntity> entities)
		{
			if (entityContainerElement == null)
			{
				return null;
			}
			XElement xelement = entityContainerElement.Element(EdmConstants.DefaultMeasureElem);
			if (xelement == null)
			{
				return null;
			}
			XElement xelement2 = xelement.Element(EdmConstants.PropertyRef);
			if (xelement2 == null)
			{
				return null;
			}
			string defaultMeasureName = xelement2.Attribute(EdmConstants.NameAttr).Value;
			IConceptualMeasure conceptualMeasure = null;
			if (defaultMeasureName != null)
			{
				Func<IConceptualProperty, bool> <>9__0;
				foreach (IConceptualEntity conceptualEntity in entities)
				{
					IEnumerable<IConceptualProperty> properties = conceptualEntity.Properties;
					Func<IConceptualProperty, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (IConceptualProperty p) => ConceptualNameComparer.Instance.Equals(p.EdmName, defaultMeasureName));
					}
					IConceptualProperty conceptualProperty = properties.FirstOrDefault(func);
					if (conceptualProperty != null)
					{
						conceptualMeasure = conceptualProperty as IConceptualMeasure;
						if (conceptualMeasure == null)
						{
							break;
						}
						break;
					}
				}
			}
			return conceptualMeasure;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004FF0 File Offset: 0x000031F0
		private static string GetDisplayName(XElement entityContainerElement)
		{
			XAttribute xattribute = ((entityContainerElement != null) ? entityContainerElement.Attribute("Caption") : null);
			if (xattribute == null)
			{
				return string.Empty;
			}
			return xattribute.Value;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005024 File Offset: 0x00003224
		private void CompleteInitialization(ConceptualSchema schema, IReadOnlyList<IEdmEntitySet> edmEntitySets, ITracer tracer)
		{
			IReadOnlyList<IConceptualEntity> entities = schema.Entities;
			Dictionary<IConceptualEntity, List<IConceptualNavigationProperty>> entityNavigationPropertiesMap = this.GetEntityNavigationPropertiesMap(schema, edmEntitySets, entities);
			Dictionary<string, Dictionary<string, List<ConceptualMParameter>>> mappedMParametersFromEntityContainer = this.GetMappedMParametersFromEntityContainer(tracer);
			foreach (IConceptualEntity conceptualEntity in entities)
			{
				ConceptualEntity conceptualEntity2 = conceptualEntity as ConceptualEntity;
				List<IConceptualNavigationProperty> list;
				IReadOnlyList<IConceptualNavigationProperty> readOnlyList;
				if (entityNavigationPropertiesMap.TryGetValue(conceptualEntity, out list))
				{
					readOnlyList = list;
				}
				else
				{
					readOnlyList = Util.EmptyReadOnlyCollection<IConceptualNavigationProperty>();
				}
				Dictionary<string, IConceptualNavigationProperty> dictionary = readOnlyList.ToDictionary((IConceptualNavigationProperty n) => n.EdmName, EdmNameComparer.Instance);
				int count = conceptualEntity.Properties.Count;
				ConceptualProperty[] array = new ConceptualProperty[count];
				IReadOnlyList<IConceptualColumn>[] array2 = new IReadOnlyList<IConceptualColumn>[count];
				int count2 = conceptualEntity.KeyColumns.Count;
				ConceptualColumn[] array3 = new ConceptualColumn[count2];
				bool flag = count2 > 0;
				for (int i = 0; i < count2; i++)
				{
					ConceptualColumn conceptualColumn = (ConceptualColumn)conceptualEntity.KeyColumns[i];
					array3[i] = conceptualColumn;
					flag &= conceptualColumn.IsStable;
				}
				for (int j = 0; j < count; j++)
				{
					ConceptualProperty conceptualProperty = conceptualEntity.Properties[j] as ConceptualProperty;
					array[j] = conceptualProperty;
					ConceptualColumn conceptualColumn2 = conceptualProperty as ConceptualColumn;
					if (conceptualColumn2 != null)
					{
						IList<string> list2 = conceptualColumn2.ParsedEdmStructuralProperty.OrderByProperties.Evaluate<string>();
						array2[j] = this.LookupConceptualPropertiesByEdmNames<ConceptualColumn>(new ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName(conceptualEntity2.TryGetPropertyByEdmName), list2);
						this.CompleteColumn(conceptualEntity2, dictionary, array2[j], array3, flag, conceptualColumn2, mappedMParametersFromEntityContainer);
					}
					else
					{
						ConceptualMeasure conceptualMeasure = conceptualProperty as ConceptualMeasure;
						this.CompleteMeasure(conceptualEntity2, conceptualMeasure, schema);
					}
				}
				foreach (IConceptualHierarchy conceptualHierarchy in conceptualEntity.Hierarchies)
				{
					foreach (IConceptualHierarchyLevel conceptualHierarchyLevel in conceptualHierarchy.Levels)
					{
						((ConceptualHierarchyLevel)conceptualHierarchyLevel).CompleteInitialization(conceptualHierarchy);
					}
				}
				ConceptualEntityStatisticsComputer conceptualEntityStatisticsComputer = new ConceptualEntityStatisticsComputer();
				int count3 = conceptualEntity.Hierarchies.Count;
				bool flag2 = conceptualEntity.Visibility.IsHidden();
				bool isDateTable = conceptualEntity.IsDateTable;
				IReadOnlyList<IConceptualProperty> defaultFieldProperties = conceptualEntity.DefaultFieldProperties;
				IReadOnlyList<IConceptualProperty> readOnlyList2 = array3;
				IConceptualProperty defaultImageColumn = conceptualEntity.DefaultImageColumn;
				IConceptualProperty defaultLabelColumn = conceptualEntity.DefaultLabelColumn;
				IReadOnlyList<ConceptualProperty> readOnlyList3 = array;
				IReadOnlyList<IConceptualProperty>[] array4 = array2;
				ConceptualEntityStatistics conceptualEntityStatistics = conceptualEntityStatisticsComputer.Compute(count3, flag2, isDateTable, defaultFieldProperties, readOnlyList2, defaultImageColumn, defaultLabelColumn, readOnlyList3, array4);
				conceptualEntity2.CompleteInitialization(schema, readOnlyList, conceptualEntityStatistics);
			}
			ConceptualSchemaStatisticsComputer conceptualSchemaStatisticsComputer = new ConceptualSchemaStatisticsComputer(entities);
			schema.CompleteInitialization(conceptualSchemaStatisticsComputer.Compute());
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000052FC File Offset: 0x000034FC
		private void RegisterAnnotationProviders(IConceptualSchema schema)
		{
			schema.RegisterAnnotationProvider<DaxCapabilitiesAnnotation, IConceptualSchema>(DaxCapabilitiesAnnotationProviderBuilder.BuildDaxCapabilitiesProvider(this.GetEntityContainer(), this._edmModel, this._csdlVersion));
			schema.RegisterAnnotationProvider<NavigationPropertyGraphAnnotation, IConceptualSchema>(new NavigationPropertyGraphAnnotationProvider(schema));
			schema.RegisterAnnotationProvider<MParameterAnnotation, IConceptualSchema>(new MParameterAnnotationProvider(schema));
			schema.RegisterAnnotationProvider<ColumnGroupingAnnotations, IConceptualSchema>(new ColumnGroupingAnnotationsProvider(() => ColumnGroupingAnnotationsProviderBuilder.BuildColumnGroupingAnnotations(schema), schema));
			schema.RegisterAnnotationProvider<FieldRelationshipAnnotations, IConceptualSchema>(new FieldRelationshipAnnotationsProvider(() => FieldRelationshipAnnotationsProviderBuilder.BuildFieldRelationshipAnnotations(schema), schema));
			if (this._rowNumberProviderBuilder != null)
			{
				IAnnotationProvider<EntityRowNumberAnnotation, IConceptualEntity> annotationProvider = this._rowNumberProviderBuilder.Build();
				schema.RegisterAnnotationProvider<EntityRowNumberAnnotation, IConceptualEntity>(annotationProvider);
			}
			this._statisticsProviderBuilder.BuildAndRegisterAnnotationProviders(schema);
			this._translationsProviderBuilder.RegisterTranslationsAnnotationProvider(schema);
			IEdmEntityContainer edmEntityContainer = ExtensionMethods.EntityContainers(this._edmModel).FirstOrDefault<IEdmEntityContainer>();
			if (edmEntityContainer != null)
			{
				schema.RegisterAnnotationProvider<CsdlSchemaNamespaceAnnotation, IConceptualSchema>(new CsdlSchemaNamespaceAnnotationProvider(new CsdlSchemaNamespaceAnnotation(edmEntityContainer.Namespace)));
			}
			this._errorStateProviderBuilder.BuildAndRegisterAnnotationProvider(schema);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005434 File Offset: 0x00003634
		private Dictionary<string, Dictionary<string, List<ConceptualMParameter>>> GetMappedMParametersFromEntityContainer(ITracer tracer)
		{
			IEdmEntityContainer entityContainer = this.GetEntityContainer();
			if (entityContainer == null)
			{
				return null;
			}
			return entityContainer.GetMappedMParameterList(this._edmModel, tracer);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000545C File Offset: 0x0000365C
		private Dictionary<IConceptualEntity, List<IConceptualNavigationProperty>> GetEntityNavigationPropertiesMap(ConceptualSchema schema, IReadOnlyList<IEdmEntitySet> edmEntitySets, IReadOnlyList<IConceptualEntity> entities)
		{
			Dictionary<IConceptualEntity, List<IConceptualNavigationProperty>> dictionary = new Dictionary<IConceptualEntity, List<IConceptualNavigationProperty>>();
			for (int i = 0; i < entities.Count; i++)
			{
				IConceptualEntity conceptualEntity = entities[i];
				IEdmEntitySet edmEntitySet = edmEntitySets[i];
				foreach (IEdmNavigationTargetMapping edmNavigationTargetMapping in edmEntitySet.NavigationTargets)
				{
					IConceptualEntity conceptualEntity2;
					if (schema.TryGetEntityByEdmName(edmNavigationTargetMapping.TargetEntitySet.GetFullName(), out conceptualEntity2))
					{
						ConceptualEntity conceptualEntity3 = conceptualEntity as ConceptualEntity;
						IEdmNavigationProperty navigationProperty = edmNavigationTargetMapping.NavigationProperty;
						IEnumerable<IEdmDirectValueAnnotation> enumerable;
						IEnumerable<IEdmDirectValueAnnotation> enumerable2;
						IEnumerable<IEdmDirectValueAnnotation> enumerable3;
						SerializationExtensionMethods.GetAssociationSetAnnotations(this._edmModel, edmEntitySet, navigationProperty, ref enumerable, ref enumerable2, ref enumerable3);
						List<XElement> list = new List<XElement>();
						if (enumerable != null)
						{
							foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in enumerable)
							{
								if (StringUtil.EqualsOrdinalIgnoreCase(edmDirectValueAnnotation.Name, "AssociationSet") && StringUtil.EqualsOrdinalIgnoreCase(edmDirectValueAnnotation.NamespaceUri, "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions"))
								{
									IEdmStringValue edmStringValue = edmDirectValueAnnotation.Value as IEdmStringValue;
									if (edmStringValue != null)
									{
										XElement xelement = XElement.Parse(edmStringValue.Value);
										if (xelement != null)
										{
											list.Add(xelement);
										}
									}
								}
							}
						}
						ConceptualNavigationProperty conceptualNavigationProperty = new ConceptualNavigationProperty(navigationProperty.GetReferenceName(this._edmModel), navigationProperty.Name, EdmExtensions.IsActive(list), EdmExtensions.GetCrossFilterDirection(list), ConceptualSchemaBuilder.GetSourceColumnForNavigationProperty(conceptualEntity3, navigationProperty), ConceptualSchemaBuilder.GetTargetColumnForNavigationProperty(conceptualEntity2, navigationProperty), conceptualEntity2, ConceptualSchemaBuilder.GetRelationshipMultiplicity(ExtensionMethods.Multiplicity(navigationProperty)), ConceptualSchemaBuilder.GetRelationshipMultiplicity(ExtensionMethods.Multiplicity(navigationProperty.Partner)), EdmExtensions.GetNavigationBehavior(list));
						dictionary.Add(conceptualEntity, conceptualNavigationProperty, 4);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005634 File Offset: 0x00003834
		private void CompleteColumn(ConceptualEntity conceptualEntity, Dictionary<string, IConceptualNavigationProperty> edmNamesToNavProps, IReadOnlyList<IConceptualColumn> orderByColumns, ConceptualColumn[] keyColumns, bool entityKeyIsStable, ConceptualColumn column, Dictionary<string, Dictionary<string, List<ConceptualMParameter>>> mappedMParametersInModel)
		{
			IList<string> list = column.ParsedEdmStructuralProperty.GroupByProperties.Evaluate<string>();
			IReadOnlyList<ConceptualColumn> readOnlyList = this.LookupConceptualPropertiesByEdmNames<ConceptualColumn>(new ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName(conceptualEntity.TryGetPropertyByEdmName), list);
			ConceptualColumnGrouping conceptualColumnGrouping = ConceptualColumnGroupingBuilder.BuildGrouping(column, readOnlyList, orderByColumns, column.Equals(conceptualEntity.DefaultLabelColumn), column.Equals(conceptualEntity.DefaultImageColumn), keyColumns, entityKeyIsStable);
			IReadOnlyList<ConceptualVariationSource> readOnlyList2 = this.CreateVariations(column, conceptualEntity, edmNamesToNavProps);
			ConceptualGroupingMetadata conceptualGroupingMetadata = null;
			ParameterMetadata parameterMetadata = null;
			IReadOnlyList<ExtendedProperty> edmExtendedProperties = column.ParsedEdmStructuralProperty.EdmExtendedProperties;
			if (edmExtendedProperties != null)
			{
				foreach (ExtendedProperty extendedProperty in edmExtendedProperties)
				{
					string name = extendedProperty.Name;
					if (!(name == "ParameterMetadata"))
					{
						if (name == "GroupingMetadata")
						{
							conceptualGroupingMetadata = ConceptualColumnGroupingMetadataBuilder.BuildConceptualGroupingMetadata(ConceptualSchemaBuilder.ParseGroupingMetadata(extendedProperty, this._tracer), conceptualEntity, this._tracer);
						}
					}
					else
					{
						parameterMetadata = CsdlParserUtil.ParseParameterMetadata(extendedProperty, this._tracer);
					}
				}
			}
			List<ConceptualMParameter> list2 = null;
			Dictionary<string, List<ConceptualMParameter>> dictionary;
			if (mappedMParametersInModel != null && mappedMParametersInModel.TryGetValue(conceptualEntity.EdmName, out dictionary))
			{
				dictionary.TryGetValue(column.EdmName, out list2);
			}
			ConceptualParameterMetadata conceptualParameterMetadata = ConceptualParameterMetadataBuilder.BuildConceptualParameter(list2, parameterMetadata);
			column.CompleteInitialization(conceptualEntity, orderByColumns, conceptualColumnGrouping, readOnlyList2, conceptualGroupingMetadata, conceptualParameterMetadata);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005788 File Offset: 0x00003988
		private static GroupingMetadata ParseGroupingMetadata(ExtendedProperty extendedProperty, ITracer tracer)
		{
			return CsdlParserUtil.ParseJsonProperty<GroupingMetadata>(extendedProperty, tracer);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005794 File Offset: 0x00003994
		private void CompleteMeasure(ConceptualEntity conceptualEntity, ConceptualMeasure measure, ConceptualSchema schema)
		{
			ConceptualKpi conceptualKpi = this.CreateKpi(measure, new ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName(conceptualEntity.TryGetPropertyByEdmName));
			IConceptualMeasure conceptualMeasure = this.TryGetDynamicFormattingMeasure(conceptualEntity, measure, measure.ParsedEdmStructuralProperty.DynamicFormatPropRef);
			IConceptualMeasure conceptualMeasure2 = this.TryGetDynamicFormattingMeasure(conceptualEntity, measure, measure.ParsedEdmStructuralProperty.DynamicCulturePropRef);
			ConceptualDistributiveAggregateKind? conceptualDistributiveAggregateKind = null;
			IReadOnlyList<IConceptualEntity> readOnlyList = null;
			EdmDistributiveBy edmDistributiveBy = measure.ParsedEdmStructuralProperty.EdmDistributiveBy;
			if (edmDistributiveBy != null)
			{
				if (!string.IsNullOrEmpty(edmDistributiveBy.AggregationKind))
				{
					ConceptualDistributiveAggregateKind conceptualDistributiveAggregateKind2;
					if (Enum.TryParse<ConceptualDistributiveAggregateKind>(edmDistributiveBy.AggregationKind, out conceptualDistributiveAggregateKind2))
					{
						conceptualDistributiveAggregateKind = new ConceptualDistributiveAggregateKind?(conceptualDistributiveAggregateKind2);
					}
					else
					{
						this._tracer.TraceWarning("Unexpected ConceptualDistributiveAggregate found");
					}
				}
				if (edmDistributiveBy.EntityRef != null)
				{
					readOnlyList = edmDistributiveBy.EntityRef.Select((string distributiveEntityName) => schema.Entities.FirstOrDefault((IConceptualEntity e) => e.EdmName.Equals(distributiveEntityName))).ToList<IConceptualEntity>();
				}
			}
			bool isVariant = measure.ParsedEdmStructuralProperty.IsVariant;
			measure.CompleteInitialization(conceptualEntity, conceptualKpi, conceptualMeasure, conceptualMeasure2, conceptualDistributiveAggregateKind, readOnlyList, isVariant);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005888 File Offset: 0x00003A88
		private ConceptualMeasureTemplate BuildMeasureTemplate(ParsedEdmStructuralProperty property)
		{
			ConceptualMeasureTemplate conceptualMeasureTemplate = null;
			IReadOnlyList<ExtendedProperty> edmExtendedProperties = property.EdmExtendedProperties;
			if (edmExtendedProperties != null)
			{
				foreach (ExtendedProperty extendedProperty in edmExtendedProperties)
				{
					if (extendedProperty.Name == "MeasureTemplate")
					{
						conceptualMeasureTemplate = ConceptualMeasureTemplateBuilder.BuildMeasureTemplate(ConceptualSchemaBuilder.ParseMeasureTemplate(extendedProperty, this._tracer));
					}
				}
			}
			return conceptualMeasureTemplate;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000058FC File Offset: 0x00003AFC
		private ConceptualDataChangeDetectionMetadata BuildDataChangeDetectionMetadata(ParsedEdmStructuralProperty property)
		{
			ConceptualDataChangeDetectionMetadata conceptualDataChangeDetectionMetadata = null;
			IReadOnlyList<ExtendedProperty> edmExtendedProperties = property.EdmExtendedProperties;
			if (edmExtendedProperties != null)
			{
				foreach (ExtendedProperty extendedProperty in edmExtendedProperties)
				{
					if (extendedProperty.Name == "ChangeDetectionMetadata")
					{
						ChangeDetectionMetadata changeDetectionMetadata = ConceptualSchemaBuilder.ParseChangeDetectionMetadata(extendedProperty, this._tracer);
						if (changeDetectionMetadata != null)
						{
							conceptualDataChangeDetectionMetadata = ConceptualDataChangeDetectionMetadataBuilder.BuildChangeDetectionMetadata(changeDetectionMetadata);
						}
						else
						{
							this._tracer.TraceError("Invalid ChangeDetectionMetadata ");
						}
					}
				}
			}
			return conceptualDataChangeDetectionMetadata;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005988 File Offset: 0x00003B88
		private static MeasureTemplate ParseMeasureTemplate(ExtendedProperty extendedProperty, ITracer tracer)
		{
			MeasureTemplate measureTemplate = null;
			try
			{
				measureTemplate = new DataContractJsonSerializer(typeof(MeasureTemplate)).FromJsonString(extendedProperty.Value);
			}
			catch (SerializationException ex)
			{
				tracer.TraceError(StringUtil.FormatInvariant("{0} occurred when parsing quick calc measure metadata", ex.Message.MarkAsCustomerContent()));
			}
			return measureTemplate;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000059E4 File Offset: 0x00003BE4
		private static ChangeDetectionMetadata ParseChangeDetectionMetadata(ExtendedProperty extendedProperty, ITracer tracer)
		{
			ChangeDetectionMetadata changeDetectionMetadata = null;
			try
			{
				changeDetectionMetadata = new DataContractJsonSerializer(typeof(ChangeDetectionMetadata)).FromJsonString(extendedProperty.Value);
				if (changeDetectionMetadata.Version != 0)
				{
					tracer.TraceError(StringUtil.FormatInvariant("Invalid ChangeDetectionMetadata Version {0} - supported {1}", changeDetectionMetadata.Version, 0));
					return null;
				}
			}
			catch (SerializationException ex)
			{
				tracer.TraceError(StringUtil.FormatInvariant("{0} occurred when parsing data change detection metadata", ex.Message.MarkAsCustomerContent()));
			}
			return changeDetectionMetadata;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005A70 File Offset: 0x00003C70
		private void CompleteHierarchyLevel(ConceptualHierarchyLevel hierarchyLevel, IConceptualHierarchy hierarchy)
		{
			hierarchyLevel.CompleteInitialization(hierarchy);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005A7C File Offset: 0x00003C7C
		private IConceptualMeasure TryGetDynamicFormattingMeasure(ConceptualEntity conceptualEntity, ConceptualMeasure dynamicallyFormattedMeasure, string propRef)
		{
			if (propRef == null)
			{
				return null;
			}
			IConceptualMeasure conceptualMeasure = this.LookupConceptualPropertyByEdmName<ConceptualMeasure>(new ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName(conceptualEntity.TryGetPropertyByEdmName), propRef);
			if (conceptualMeasure.DynamicFormatString != null || conceptualMeasure.DynamicFormatCulture != null || dynamicallyFormattedMeasure == conceptualMeasure)
			{
				Contract.TraceFail(this._tracer, "Malformed CSDL. Dynamic formatting measure {0} cannot refer to another measure for dynamic formatting.", new object[] { conceptualMeasure.DisplayName.MarkAsModelInfo() });
				conceptualMeasure = null;
			}
			return conceptualMeasure;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005AE0 File Offset: 0x00003CE0
		private static IConceptualColumn GetSourceColumnForNavigationProperty(IConceptualEntity entity, IEdmNavigationProperty navigationProperty)
		{
			IEdmStructuralProperty edmStructuralProperty = navigationProperty.DependentProperties.EmptyIfNull<IEdmStructuralProperty>().FirstOrDefault<IEdmStructuralProperty>();
			IConceptualProperty conceptualProperty = null;
			if (edmStructuralProperty != null)
			{
				entity.TryGetPropertyByEdmName(edmStructuralProperty.Name, out conceptualProperty);
			}
			return conceptualProperty as IConceptualColumn;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005B18 File Offset: 0x00003D18
		private static IConceptualColumn GetTargetColumnForNavigationProperty(IConceptualEntity entity, IEdmNavigationProperty navigationProperty)
		{
			string valueOrDefaultAs = RObject.Create(navigationProperty)["Association"]["ReferentialConstraint"]["Element"]["Principal"]["Properties"][0]["PropertyName"].GetValueOrDefaultAs<string>();
			IConceptualProperty conceptualProperty;
			if (string.IsNullOrEmpty(valueOrDefaultAs) || !entity.TryGetPropertyByEdmName(valueOrDefaultAs, out conceptualProperty))
			{
				return null;
			}
			return conceptualProperty as IConceptualColumn;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005B90 File Offset: 0x00003D90
		private IReadOnlyList<IConceptualEntity> CreateConceptualEntities(out List<IEdmEntitySet> edmEntitySets)
		{
			List<IConceptualEntity> list = new List<IConceptualEntity>();
			edmEntitySets = new List<IEdmEntitySet>();
			foreach (IEdmEntityContainer edmEntityContainer in ExtensionMethods.EntityContainers(this._edmModel))
			{
				foreach (IEdmEntitySet edmEntitySet in ExtensionMethods.EntitySets(edmEntityContainer))
				{
					IConceptualEntity conceptualEntity = this.CreateConceptualEntity(edmEntitySet);
					list.Add(conceptualEntity);
					edmEntitySets.Add(edmEntitySet);
				}
			}
			return list.AsReadOnlyList<IConceptualEntity>();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005C3C File Offset: 0x00003E3C
		private IConceptualEntity CreateConceptualEntity(IEdmEntitySet edmEntitySet)
		{
			string name = edmEntitySet.Name;
			string text = edmEntitySet.Name;
			string text2 = text;
			IEdmDocumentation documentation = ExtensionMethods.GetDocumentation(this._edmModel, edmEntitySet);
			string text3 = ((documentation != null) ? documentation.Summary : null);
			ConceptualEntityVisibilityType conceptualEntityVisibilityType = ConceptualEntityVisibilityType.AlwaysVisible;
			XElement xmlElement = edmEntitySet.GetXmlElement(this._edmModel);
			if (xmlElement != null)
			{
				XAttribute xattribute = xmlElement.Attribute(EdmConstants.ReferenceNameAttr);
				if (xattribute != null)
				{
					text = xattribute.Value;
				}
				xattribute = xmlElement.Attribute(EdmConstants.CaptionAttr);
				text2 = ((xattribute != null) ? xattribute.Value : text);
				if (xmlElement.GetBooleanAttributeValueOrDefault(EdmConstants.HiddenAttr, this._tracer, false))
				{
					conceptualEntityVisibilityType |= ConceptualEntityVisibilityType.Hidden;
				}
				if (xmlElement.GetBooleanAttributeValueOrDefault(EdmConstants.ShowAsVariationsOnlyAttr, this._tracer, false))
				{
					conceptualEntityVisibilityType |= ConceptualEntityVisibilityType.ShowAsVariationsOnly;
				}
				if (xmlElement.GetBooleanAttributeValueOrDefault(EdmConstants.PrivateAttr, this._tracer, false))
				{
					conceptualEntityVisibilityType |= ConceptualEntityVisibilityType.Private;
				}
			}
			IReadOnlyList<ConceptualProperty> readOnlyList = this.CreateConceptualProperties(edmEntitySet, text);
			ReadOnlyDictionary<string, IConceptualProperty> readOnlyDictionary = readOnlyList.ToDictionary(ConceptualSchemaBuilder._conceptualPropertyEdmNameSelector, Util.IdentityDelegate<IConceptualProperty>(), EdmNameComparer.Instance).AsReadOnlyDictionary<string, IConceptualProperty>();
			IList<IEdmStructuralProperty> list = ExtensionMethods.Key(edmEntitySet.ElementType).Evaluate<IEdmStructuralProperty>();
			IReadOnlyList<ConceptualColumn> readOnlyList2;
			if (list.Count == 1 && list[0].IsRowNumber(this._edmModel))
			{
				readOnlyList2 = Util.EmptyReadOnlyCollection<ConceptualColumn>();
			}
			else if (list.Any((IEdmStructuralProperty k) => k.Type.Definition.TypeKind == 0))
			{
				Contract.TraceFail(this._tracer, "Malformed EdmEntitySet. Key type is unknown or error. {0}", new object[] { edmEntitySet.Name.MarkAsModelInfo() });
				readOnlyList2 = Util.EmptyReadOnlyCollection<ConceptualColumn>();
			}
			else
			{
				readOnlyList2 = this.LookupConceptualPropertiesByEdmNames<ConceptualColumn>(new ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName(readOnlyDictionary.TryGetValue), list.Select((IEdmStructuralProperty k) => k.Name).Evaluate<string>());
			}
			XElement entityTypeXmlElement = edmEntitySet.ElementType.GetEntityTypeXmlElement(this._edmModel);
			EdmExtensions.EntityTypeExtensionObjects entityTypeExtensionObjects = entityTypeXmlElement.GetEntityTypeExtensionObjects(name, this._tracer);
			IReadOnlyList<ConceptualProperty> readOnlyList3 = this.LookupConceptualPropertiesByEdmNames<ConceptualProperty>(new ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName(readOnlyDictionary.TryGetValue), entityTypeExtensionObjects.DefaultFieldSet);
			ConceptualColumn conceptualColumn = null;
			if (entityTypeExtensionObjects.DefaultLabel != null)
			{
				conceptualColumn = this.LookupConceptualPropertyByEdmName<ConceptualColumn>(new ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName(readOnlyDictionary.TryGetValue), entityTypeExtensionObjects.DefaultLabel);
			}
			ConceptualColumn conceptualColumn2 = null;
			if (entityTypeExtensionObjects.DefaultImage != null)
			{
				conceptualColumn2 = this.LookupConceptualPropertyByEdmName<ConceptualColumn>(new ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName(readOnlyDictionary.TryGetValue), entityTypeExtensionObjects.DefaultImage);
			}
			IReadOnlyList<ConceptualHierarchy> readOnlyList4;
			if (this.CheckFeatureSupport(ConceptualSchemaBuilder.HierarchiesSupportedVersion))
			{
				readOnlyList4 = this.CreateConceptualHierarchies(entityTypeXmlElement, readOnlyList);
			}
			else
			{
				readOnlyList4 = Util.EmptyReadOnlyCollection<ConceptualHierarchy>();
			}
			IReadOnlyList<ConceptualDisplayFolder> readOnlyList5 = this.CreateConceptualDisplayFolders(entityTypeXmlElement, new ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName(readOnlyDictionary.TryGetValue), readOnlyList4);
			string stringAttributeValueOrDefault = xmlElement.GetStringAttributeValueOrDefault(EdmConstants.StableNameAttr, null);
			string text4 = text;
			string name2 = edmEntitySet.Name;
			string text5 = text2;
			string text6 = text3;
			string name3 = edmEntitySet.Container.Name;
			ConceptualEntityVisibilityType conceptualEntityVisibilityType2 = conceptualEntityVisibilityType;
			int? rowCount = entityTypeExtensionObjects.RowCount;
			ConceptualEntity conceptualEntity = new ConceptualEntity(text4, name2, text5, text6, name3, conceptualEntityVisibilityType2, entityTypeExtensionObjects.IsDateTable, rowCount, readOnlyList, readOnlyList4, readOnlyList5, readOnlyDictionary, readOnlyList2, readOnlyList3, conceptualColumn, conceptualColumn2, stringAttributeValueOrDefault);
			XElement entitySetCulturesElement = this.GetEntitySetCulturesElement(edmEntitySet);
			List<ConceptualTranslation> list2 = ((entitySetCulturesElement != null) ? entitySetCulturesElement.GetTranslations() : null);
			if (!list2.IsNullOrEmpty<ConceptualTranslation>())
			{
				this._translationsProviderBuilder.RegisterTranslations(conceptualEntity, list2);
			}
			if (conceptualEntity.RowCount != null)
			{
				this._statisticsProviderBuilder.Register(text, conceptualEntity.RowCount.Value);
			}
			return conceptualEntity;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005F88 File Offset: 0x00004188
		private XElement GetEntitySetCulturesElement(IEdmEntitySet entitySet)
		{
			IEdmStringValue annotationValue = ExtensionMethods.GetAnnotationValue<IEdmStringValue>(this._edmModel, entitySet, "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions", "Cultures");
			if (annotationValue == null)
			{
				return null;
			}
			return XElement.Parse(annotationValue.Value);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005FBC File Offset: 0x000041BC
		private IReadOnlyList<ConceptualVariationSource> CreateVariations(ConceptualProperty prop, ConceptualEntity entity, Dictionary<string, IConceptualNavigationProperty> edmNamesToNavProps)
		{
			IReadOnlyList<EdmVariation> edmVariations = prop.ParsedEdmStructuralProperty.EdmVariations;
			List<ConceptualVariationSource> list = null;
			if (edmVariations != null)
			{
				list = new List<ConceptualVariationSource>(edmVariations.Count);
				foreach (EdmVariation edmVariation in edmVariations)
				{
					IConceptualNavigationProperty conceptualNavigationProperty = null;
					IConceptualEntity conceptualEntity;
					if (!string.IsNullOrEmpty(edmVariation.NavigationPropertyRef))
					{
						if (!edmNamesToNavProps.TryGetValue(edmVariation.NavigationPropertyRef, out conceptualNavigationProperty))
						{
							Contract.TraceFail(this._tracer, "Unable to find navigation property {0} by reference on variation {1}", new object[]
							{
								edmVariation.NavigationPropertyRef.MarkAsModelInfo(),
								edmVariation.EdmName.MarkAsModelInfo()
							});
						}
						conceptualEntity = conceptualNavigationProperty.TargetEntity;
					}
					else
					{
						conceptualEntity = entity;
					}
					IConceptualHierarchy conceptualHierarchy = null;
					if (!string.IsNullOrEmpty(edmVariation.DefaultHierarchyRef) && !conceptualEntity.TryGetHierarchyByEdmName(edmVariation.DefaultHierarchyRef, out conceptualHierarchy))
					{
						Contract.TraceFail(this._tracer, "Unable to find hierarchy {0} by reference on entity {1}", new object[]
						{
							edmVariation.DefaultHierarchyRef.MarkAsModelInfo(),
							conceptualEntity.EdmName.MarkAsModelInfo()
						});
					}
					IConceptualProperty conceptualProperty = null;
					if (!string.IsNullOrEmpty(edmVariation.DefaultPropertyRef) && !conceptualEntity.TryGetPropertyByEdmName(edmVariation.DefaultPropertyRef, out conceptualProperty))
					{
						Contract.TraceFail(this._tracer, "Unable to find default property {0} by reference on entity {1}", new object[]
						{
							edmVariation.DefaultPropertyRef.MarkAsModelInfo(),
							conceptualEntity.EdmName.MarkAsModelInfo()
						});
					}
					list.Add(new ConceptualVariationSource(prop, edmVariation.ReferenceName ?? edmVariation.EdmName, edmVariation.IsDefault, conceptualNavigationProperty, conceptualHierarchy, conceptualProperty));
				}
			}
			return list.AsReadOnlyList<ConceptualVariationSource>();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006168 File Offset: 0x00004368
		private ConceptualKpi CreateKpi(ConceptualMeasure measure, ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName propNamesToProps)
		{
			EdmKpi edmKpi = measure.ParsedEdmStructuralProperty.EdmKpi;
			if (edmKpi == null)
			{
				return null;
			}
			ConceptualMeasure conceptualMeasure = ((edmKpi.KpiStatus != null) ? this.LookupConceptualPropertyByEdmName<ConceptualMeasure>(propNamesToProps, edmKpi.KpiStatus) : null);
			ConceptualMeasure conceptualMeasure2 = ((edmKpi.KpiGoal != null) ? this.LookupConceptualPropertyByEdmName<ConceptualMeasure>(propNamesToProps, edmKpi.KpiGoal) : null);
			ConceptualMeasure conceptualMeasure3 = ((edmKpi.KpiTrend != null) ? this.LookupConceptualPropertyByEdmName<ConceptualMeasure>(propNamesToProps, edmKpi.KpiTrend) : null);
			return new ConceptualKpi(edmKpi.StatusGraphic, edmKpi.TrendGraphic, conceptualMeasure, conceptualMeasure2, conceptualMeasure3, edmKpi.Description);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000061EC File Offset: 0x000043EC
		private IReadOnlyList<ConceptualProperty> CreateConceptualProperties(IEdmEntitySet entitySet, string entityReferenceName)
		{
			List<ConceptualProperty> list = new List<ConceptualProperty>();
			int num = 0;
			foreach (IEdmStructuralProperty edmStructuralProperty in ExtensionMethods.DeclaredStructuralProperties(entitySet.ElementType))
			{
				ParsedEdmStructuralProperty parsedEdmStructuralProperty = edmStructuralProperty.Parse(this._edmModel, this._tracer, this._options.SkipYearColumnDetectionHeuristics);
				if (parsedEdmStructuralProperty.IsRowNumber)
				{
					if (this._rowNumberProviderBuilder != null)
					{
						this._rowNumberProviderBuilder.Register(entityReferenceName, parsedEdmStructuralProperty.ReferenceName);
					}
				}
				else
				{
					DataType dataType = edmStructuralProperty.GetDataType(parsedEdmStructuralProperty.PropertyDataCategory);
					ConceptualPrimitiveType conceptualDataType = edmStructuralProperty.GetConceptualDataType();
					IEdmDocumentation documentation = ExtensionMethods.GetDocumentation(this._edmModel, edmStructuralProperty);
					string text = ((documentation != null) ? documentation.Summary : null);
					ConceptualProperty conceptualProperty;
					if (!parsedEdmStructuralProperty.IsMeasure)
					{
						conceptualProperty = new ConceptualColumn(parsedEdmStructuralProperty, dataType, conceptualDataType, num++, text);
						if (parsedEdmStructuralProperty.Statistics != null)
						{
							this._statisticsProviderBuilder.Register(entityReferenceName, conceptualProperty.Name, parsedEdmStructuralProperty.Statistics);
						}
					}
					else
					{
						ConceptualMeasureTemplate conceptualMeasureTemplate = this.BuildMeasureTemplate(parsedEdmStructuralProperty);
						ConceptualDataChangeDetectionMetadata conceptualDataChangeDetectionMetadata = this.BuildDataChangeDetectionMetadata(parsedEdmStructuralProperty);
						conceptualProperty = new ConceptualMeasure(parsedEdmStructuralProperty, dataType, conceptualDataType, num++, conceptualMeasureTemplate, conceptualDataChangeDetectionMetadata, text);
					}
					if (parsedEdmStructuralProperty.Translations != null && parsedEdmStructuralProperty.Translations.Any<ConceptualTranslation>())
					{
						this._translationsProviderBuilder.RegisterTranslations(conceptualProperty, parsedEdmStructuralProperty.Translations);
					}
					if (parsedEdmStructuralProperty.IsError)
					{
						this._errorStateProviderBuilder.RegisterErrorProperty(conceptualProperty);
					}
					list.Add(conceptualProperty);
				}
			}
			return list.AsReadOnlyList<ConceptualProperty>();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006390 File Offset: 0x00004590
		private IReadOnlyList<ConceptualHierarchy> CreateConceptualHierarchies(XElement entityTypeElement, IReadOnlyList<ConceptualProperty> properties)
		{
			List<ConceptualHierarchy> list = new List<ConceptualHierarchy>();
			foreach (EdmExtensions.EdmHierarchy edmHierarchy in entityTypeElement.GetHierarchies(this._tracer))
			{
				IReadOnlyList<ConceptualHierarchyLevel> readOnlyList = this.CreateConceptualHierarchyLevels(properties, edmHierarchy);
				ConceptualHierarchy conceptualHierarchy = new ConceptualHierarchy(edmHierarchy.ReferenceName, edmHierarchy.EdmName, edmHierarchy.DisplayName, edmHierarchy.Description, edmHierarchy.IsHidden, readOnlyList, edmHierarchy.StableName);
				List<ConceptualTranslation> translations = edmHierarchy.Translations;
				if (translations != null && translations.Any<ConceptualTranslation>())
				{
					this._translationsProviderBuilder.RegisterTranslations(conceptualHierarchy, edmHierarchy.Translations);
				}
				list.Add(conceptualHierarchy);
			}
			return list.AsReadOnlyList<ConceptualHierarchy>();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000644C File Offset: 0x0000464C
		private IReadOnlyList<ConceptualHierarchyLevel> CreateConceptualHierarchyLevels(IReadOnlyList<ConceptualProperty> properties, EdmExtensions.EdmHierarchy hierarchy)
		{
			List<ConceptualHierarchyLevel> list = new List<ConceptualHierarchyLevel>();
			using (List<EdmExtensions.EdmLevel>.Enumerator enumerator = hierarchy.Levels.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmExtensions.EdmLevel level = enumerator.Current;
					ConceptualProperty conceptualProperty = properties.FirstOrDefault((ConceptualProperty p) => p.EdmName == level.SourceRef);
					ConceptualHierarchyLevel conceptualHierarchyLevel = new ConceptualHierarchyLevel(level.ReferenceName, level.EdmName, level.DisplayName, level.Description, conceptualProperty, level.StableName);
					List<ConceptualTranslation> translations = level.Translations;
					if (translations != null && translations.Any<ConceptualTranslation>())
					{
						this._translationsProviderBuilder.RegisterTranslations(conceptualHierarchyLevel, level.Translations);
					}
					list.Add(conceptualHierarchyLevel);
				}
			}
			return list.AsReadOnlyList<ConceptualHierarchyLevel>();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006540 File Offset: 0x00004740
		private IReadOnlyList<ConceptualDisplayFolder> CreateConceptualDisplayFolders(XElement entityTypeElement, ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName propertyLookup, IReadOnlyList<ConceptualHierarchy> hierarchies)
		{
			IEnumerable<EdmExtensions.EdmDisplayFolder> displayFolders = entityTypeElement.GetDisplayFolders();
			List<ConceptualDisplayFolder> list = new List<ConceptualDisplayFolder>();
			if (displayFolders != null)
			{
				foreach (EdmExtensions.EdmDisplayFolder edmDisplayFolder in displayFolders)
				{
					list.Add(this.CreateConceptualDisplayFolder(edmDisplayFolder, propertyLookup, hierarchies));
				}
			}
			return list.AsReadOnlyList<ConceptualDisplayFolder>();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000065A8 File Offset: 0x000047A8
		private ConceptualDisplayFolder CreateConceptualDisplayFolder(EdmExtensions.EdmDisplayFolder folder, ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName propertyLookup, IReadOnlyList<ConceptualHierarchy> hierarchies)
		{
			List<IConceptualDisplayItem> list = new List<IConceptualDisplayItem>();
			foreach (EdmExtensions.EdmDisplayItem edmDisplayItem in folder.DisplayItems)
			{
				IConceptualDisplayItem conceptualDisplayItem = this.ResolveConceptualDisplayItem(edmDisplayItem, propertyLookup, hierarchies);
				if (conceptualDisplayItem != null)
				{
					list.Add(conceptualDisplayItem);
				}
			}
			ConceptualDisplayFolder conceptualDisplayFolder = new ConceptualDisplayFolder(folder.EdmName, folder.DisplayName, null, list.AsReadOnlyList<IConceptualDisplayItem>());
			this._translationsProviderBuilder.RegisterTranslations(conceptualDisplayFolder, folder.Translations);
			return conceptualDisplayFolder;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006640 File Offset: 0x00004840
		private IConceptualDisplayItem ResolveConceptualDisplayItem(EdmExtensions.EdmDisplayItem edmDisplayItem, ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName propertyLookup, IReadOnlyList<ConceptualHierarchy> hierarchies)
		{
			if (edmDisplayItem.PropertyRef != null)
			{
				return this.LookupConceptualPropertyByEdmName<ConceptualProperty>(propertyLookup, edmDisplayItem.PropertyRef);
			}
			if (edmDisplayItem.HierarchyRef != null)
			{
				return hierarchies.FirstOrDefault((ConceptualHierarchy p) => p.EdmName == edmDisplayItem.HierarchyRef);
			}
			if (edmDisplayItem.DisplayFolder != null)
			{
				return this.CreateConceptualDisplayFolder(edmDisplayItem.DisplayFolder, propertyLookup, hierarchies);
			}
			return null;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000066BC File Offset: 0x000048BC
		private T LookupConceptualPropertyByEdmName<T>(ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName lookup, string edmPropertyName) where T : ConceptualProperty
		{
			IConceptualProperty conceptualProperty;
			if (!lookup(edmPropertyName, out conceptualProperty))
			{
				this._tracer.TraceError("Unable to find PropertyRef {0}", edmPropertyName.MarkAsModelInfo());
			}
			T t = conceptualProperty as T;
			if (t == null)
			{
				this._tracer.TraceError("Unexpected type for PropertyRef {0}", edmPropertyName.MarkAsModelInfo());
			}
			return t;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006714 File Offset: 0x00004914
		private IReadOnlyList<T> LookupConceptualPropertiesByEdmNames<T>(ConceptualSchemaBuilder.TryGetConceptualPropertyByEdmName lookup, IList<string> edmPropertyNames) where T : ConceptualProperty
		{
			if (edmPropertyNames.Count == 0)
			{
				return Util.EmptyReadOnlyCollection<T>();
			}
			T[] array = new T[edmPropertyNames.Count];
			for (int i = 0; i < edmPropertyNames.Count; i++)
			{
				IConceptualProperty conceptualProperty;
				if (!lookup(edmPropertyNames[i], out conceptualProperty))
				{
					this._tracer.TraceError("Unable to map PropertyRef {0}", edmPropertyNames[i].MarkAsModelInfo());
				}
				array[i] = conceptualProperty as T;
			}
			return array;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000678C File Offset: 0x0000498C
		private bool CheckFeatureSupport(Version requiredVersion)
		{
			return this._csdlVersion == null || this._csdlVersion >= requiredVersion;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000067AC File Offset: 0x000049AC
		private static IList<ConceptualSchemaLoadError> ConvertErrors(IEnumerable<EdmError> edmErrors)
		{
			if (edmErrors == null)
			{
				return null;
			}
			List<ConceptualSchemaLoadError> list = new List<ConceptualSchemaLoadError>();
			foreach (EdmError edmError in edmErrors)
			{
				list.Add(new ConceptualSchemaLoadError(edmError.ErrorMessage, (edmError.ErrorLocation != null) ? edmError.ErrorLocation.ToString() : null, edmError.ErrorCode.ToString()));
			}
			return list;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006834 File Offset: 0x00004A34
		private static ConceptualMultiplicity GetRelationshipMultiplicity(EdmMultiplicity edmMultiplicity)
		{
			switch (edmMultiplicity)
			{
			case 1:
				return ConceptualMultiplicity.ZeroOrOne;
			case 2:
				return ConceptualMultiplicity.One;
			case 3:
				return ConceptualMultiplicity.Many;
			default:
				throw Contract.Except("Unexpected Multiplicity during Schema generation");
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000685C File Offset: 0x00004A5C
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		private static void VerifyStatistics(ConceptualColumnStatistics statistics, ConceptualPrimitiveType conceptualDataType)
		{
		}

		// Token: 0x0400014E RID: 334
		private static readonly Func<IConceptualProperty, string> _conceptualPropertyEdmNameSelector = (IConceptualProperty p) => p.EdmName;

		// Token: 0x0400014F RID: 335
		private static readonly Version HierarchiesSupportedVersion = new Version(1, 1);

		// Token: 0x04000150 RID: 336
		private readonly IEdmModel _edmModel;

		// Token: 0x04000151 RID: 337
		private readonly ITracer _tracer;

		// Token: 0x04000152 RID: 338
		private readonly Version _csdlVersion;

		// Token: 0x04000153 RID: 339
		private readonly ConceptualSchemaBuilderOptions _options;

		// Token: 0x04000154 RID: 340
		private readonly RowNumberAnnotationProviderBuilder _rowNumberProviderBuilder;

		// Token: 0x04000155 RID: 341
		private readonly StatisticsAnnotationProviderBuilder _statisticsProviderBuilder;

		// Token: 0x04000156 RID: 342
		private readonly TranslationsAnnotationProviderBuilder _translationsProviderBuilder;

		// Token: 0x04000157 RID: 343
		private readonly ErrorStateAnnotationProviderBuilder _errorStateProviderBuilder;

		// Token: 0x0200003B RID: 59
		// (Invoke) Token: 0x060001C8 RID: 456
		private delegate bool TryGetConceptualPropertyByEdmName(string edmName, out IConceptualProperty prop);
	}
}
