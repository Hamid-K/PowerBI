using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.EntityModel.SchemaObjectModel;
using System.Linq;
using Microsoft.Data.Common;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200007D RID: 125
	internal static class Converter
	{
		// Token: 0x0600095F RID: 2399 RVA: 0x00015858 File Offset: 0x00013A58
		static Converter()
		{
			EnumType enumType = new EnumType("ConcurrencyMode", "Edm", DataSpace.CSpace);
			foreach (string text in MetadataHelper.PrimitiveTypesNames)
			{
				enumType.AddMember(new EnumMember(text));
			}
			EnumType enumType2 = new EnumType("StoreGeneratedPattern", "Edm", DataSpace.CSpace);
			foreach (string text2 in MetadataHelper.StoreGeneratedPatternNames)
			{
				enumType2.AddMember(new EnumMember(text2));
			}
			Converter.ConcurrencyModeFacet = new FacetDescription("ConcurrencyMode", enumType, null, null, ConcurrencyMode.None);
			Converter.StoreGeneratedPatternFacet = new FacetDescription("StoreGeneratedPattern", enumType2, null, null, StoreGeneratedPattern.None);
			Converter.CollationFacet = new FacetDescription("Collation", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.String), null, null, string.Empty);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00015998 File Offset: 0x00013B98
		internal static IEnumerable<GlobalItem> ConvertSchema(Schema somSchema, DbProviderManifest providerManifest, ItemCollection itemCollection)
		{
			Dictionary<SchemaElement, GlobalItem> dictionary = new Dictionary<SchemaElement, GlobalItem>();
			Converter.ConvertSchema(somSchema, providerManifest, new Converter.ConversionCache(itemCollection), dictionary);
			return dictionary.Values;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x000159C0 File Offset: 0x00013BC0
		internal static IEnumerable<GlobalItem> ConvertSchema(IList<Schema> somSchemas, DbProviderManifest providerManifest, ItemCollection itemCollection)
		{
			Dictionary<SchemaElement, GlobalItem> dictionary = new Dictionary<SchemaElement, GlobalItem>();
			Converter.ConversionCache conversionCache = new Converter.ConversionCache(itemCollection);
			foreach (Schema schema in somSchemas)
			{
				Converter.ConvertSchema(schema, providerManifest, conversionCache, dictionary);
			}
			return dictionary.Values;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00015A1C File Offset: 0x00013C1C
		private static void ConvertSchema(Schema somSchema, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			List<Function> list = new List<Function>();
			foreach (SchemaType schemaType in somSchema.SchemaTypes)
			{
				MetadataItem metadataItem = Converter.LoadSchemaElement(schemaType, providerManifest, convertedItemCache, newGlobalItems);
				if (metadataItem == null)
				{
					if (schemaType is Function)
					{
						list.Add(schemaType as Function);
					}
				}
				else if (metadataItem is EntityContainer)
				{
					metadataItem.AddMetadataProperties(somSchema.OtherContent.OfType<MetadataProperty>().ToList<MetadataProperty>());
				}
			}
			foreach (SchemaEntityType schemaEntityType in somSchema.SchemaTypes.OfType<SchemaEntityType>())
			{
				Converter.LoadEntityTypePhase2(schemaEntityType, providerManifest, convertedItemCache, newGlobalItems);
			}
			foreach (Function function in list)
			{
				if (Converter.LoadSchemaElement(function, providerManifest, convertedItemCache, newGlobalItems) == null)
				{
					throw new Exception("Could not load model function definition");
				}
			}
			if (convertedItemCache.ItemCollection.DataSpace == DataSpace.CSpace)
			{
				EdmItemCollection edmItemCollection = (EdmItemCollection)convertedItemCache.ItemCollection;
				edmItemCollection.EdmVersion = Math.Max(somSchema.SchemaVersion, edmItemCollection.EdmVersion);
				return;
			}
			throw new Exception("Cannot handle store");
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00015B84 File Offset: 0x00013D84
		internal static MetadataItem LoadSchemaElement(SchemaType element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			GlobalItem globalItem;
			if (newGlobalItems.TryGetValue(element, out globalItem))
			{
				return globalItem;
			}
			EntityContainer entityContainer = element as EntityContainer;
			if (entityContainer != null)
			{
				globalItem = Converter.ConvertToEntityContainer(entityContainer, providerManifest, convertedItemCache, newGlobalItems);
			}
			else if (element is SchemaEntityType)
			{
				globalItem = Converter.ConvertToEntityType((SchemaEntityType)element, providerManifest, convertedItemCache, newGlobalItems);
			}
			else if (element is Relationship)
			{
				globalItem = Converter.ConvertToAssociationType((Relationship)element, providerManifest, convertedItemCache, newGlobalItems);
			}
			else if (element is SchemaComplexType)
			{
				globalItem = Converter.ConvertToComplexType((SchemaComplexType)element, providerManifest, convertedItemCache, newGlobalItems);
			}
			else
			{
				if (!(element is Function))
				{
					return null;
				}
				globalItem = Converter.ConvertToFunction((Function)element, providerManifest, convertedItemCache, null, newGlobalItems);
			}
			return globalItem;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00015C1C File Offset: 0x00013E1C
		private static EntityContainer ConvertToEntityContainer(EntityContainer element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntityContainer entityContainer = new EntityContainer(element.Name, Converter.GetDataSpace(providerManifest));
			newGlobalItems.Add(element, entityContainer);
			foreach (EntityContainerEntitySet entityContainerEntitySet in element.EntitySets)
			{
				entityContainer.AddEntitySetBase(Converter.ConvertToEntitySet(entityContainerEntitySet, entityContainer.Name, providerManifest, convertedItemCache, newGlobalItems));
			}
			foreach (EntityContainerRelationshipSet entityContainerRelationshipSet in element.RelationshipSets)
			{
				entityContainer.AddEntitySetBase(Converter.ConvertToAssociationSet(entityContainerRelationshipSet, providerManifest, convertedItemCache, entityContainer, newGlobalItems));
			}
			foreach (Function function in element.FunctionImports)
			{
				entityContainer.AddFunctionImport(Converter.ConvertToFunction(function, providerManifest, convertedItemCache, entityContainer, newGlobalItems));
			}
			if (element.Documentation != null)
			{
				entityContainer.Documentation = Converter.ConvertToDocumentation(element.Documentation);
			}
			Converter.AddOtherContent(element, entityContainer);
			return entityContainer;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00015D48 File Offset: 0x00013F48
		private static EntityType ConvertToEntityType(SchemaEntityType element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			string[] array = null;
			if (element.DeclaredKeyProperties.Count != 0)
			{
				array = new string[element.DeclaredKeyProperties.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = element.DeclaredKeyProperties[i].Property.Name;
				}
			}
			EdmProperty[] array2 = new EdmProperty[element.Properties.Count];
			int num = 0;
			foreach (StructuredProperty structuredProperty in element.Properties)
			{
				array2[num++] = Converter.ConvertToProperty(structuredProperty, providerManifest, convertedItemCache, newGlobalItems);
			}
			EntityType entityType = new EntityType(element.Name, element.Namespace, Converter.GetDataSpace(providerManifest), array, array2);
			if (element.BaseType != null)
			{
				entityType.BaseType = (EdmType)Converter.LoadSchemaElement(element.BaseType, providerManifest, convertedItemCache, newGlobalItems);
			}
			entityType.Abstract = element.IsAbstract;
			if (element.Documentation != null)
			{
				entityType.Documentation = Converter.ConvertToDocumentation(element.Documentation);
			}
			Converter.AddOtherContent(element, entityType);
			newGlobalItems.Add(element, entityType);
			return entityType;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00015E78 File Offset: 0x00014078
		private static void LoadEntityTypePhase2(SchemaEntityType element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntityType entityType = (EntityType)newGlobalItems[element];
			foreach (NavigationProperty navigationProperty in element.NavigationProperties)
			{
				entityType.AddMember(Converter.ConvertToNavigationProperty(entityType, navigationProperty, providerManifest, convertedItemCache, newGlobalItems));
			}
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00015EDC File Offset: 0x000140DC
		private static ComplexType ConvertToComplexType(SchemaComplexType element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			ComplexType complexType = new ComplexType(element.Name, element.Namespace, Converter.GetDataSpace(providerManifest));
			newGlobalItems.Add(element, complexType);
			foreach (StructuredProperty structuredProperty in element.Properties)
			{
				complexType.AddMember(Converter.ConvertToProperty(structuredProperty, providerManifest, convertedItemCache, newGlobalItems));
			}
			complexType.Abstract = element.IsAbstract;
			if (element.BaseType != null)
			{
				complexType.BaseType = (EdmType)Converter.LoadSchemaElement(element.BaseType, providerManifest, convertedItemCache, newGlobalItems);
			}
			if (element.Documentation != null)
			{
				complexType.Documentation = Converter.ConvertToDocumentation(element.Documentation);
			}
			Converter.AddOtherContent(element, complexType);
			return complexType;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00015FA0 File Offset: 0x000141A0
		private static AssociationType ConvertToAssociationType(Relationship element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			AssociationType associationType = new AssociationType(element.Name, element.Namespace, element.IsForeignKey, Converter.GetDataSpace(providerManifest));
			newGlobalItems.Add(element, associationType);
			foreach (IRelationshipEnd relationshipEnd in element.Ends)
			{
				RelationshipEnd relationshipEnd2 = (RelationshipEnd)relationshipEnd;
				EntityType entityType = (EntityType)Converter.LoadSchemaElement(relationshipEnd2.Type, providerManifest, convertedItemCache, newGlobalItems);
				AssociationEndMember associationEndMember = Converter.InitializeAssociationEndMember(associationType, relationshipEnd2, entityType);
				Converter.AddOtherContent(relationshipEnd2, associationEndMember);
				foreach (OnOperation onOperation in relationshipEnd2.Operations)
				{
					if (onOperation.Operation == Operation.Delete)
					{
						global::System.Data.EntityModel.SchemaObjectModel.Action action = onOperation.Action;
						OperationAction operationAction;
						if (action != global::System.Data.EntityModel.SchemaObjectModel.Action.None)
						{
							if (action != global::System.Data.EntityModel.SchemaObjectModel.Action.Cascade)
							{
								throw new Exception("Operation action not supported.");
							}
							operationAction = OperationAction.Cascade;
						}
						else
						{
							operationAction = OperationAction.None;
						}
						associationEndMember.DeleteBehavior = operationAction;
					}
				}
				if (relationshipEnd2.Documentation != null)
				{
					associationEndMember.Documentation = Converter.ConvertToDocumentation(relationshipEnd2.Documentation);
				}
			}
			for (int i = 0; i < element.Constraints.Count; i++)
			{
				ReferentialConstraint referentialConstraint = element.Constraints[i];
				AssociationEndMember associationEndMember2 = (AssociationEndMember)associationType.Members[referentialConstraint.PrincipalRole.Name];
				AssociationEndMember associationEndMember3 = (AssociationEndMember)associationType.Members[referentialConstraint.DependentRole.Name];
				EntityTypeBase elementType = ((RefType)associationEndMember2.TypeUsage.EdmType).ElementType;
				EntityTypeBase elementType2 = ((RefType)associationEndMember3.TypeUsage.EdmType).ElementType;
				ReferentialConstraint referentialConstraint2 = new ReferentialConstraint(associationEndMember2, associationEndMember3, Converter.GetProperties(elementType, referentialConstraint.PrincipalRole.RoleProperties), Converter.GetProperties(elementType2, referentialConstraint.DependentRole.RoleProperties));
				if (referentialConstraint.Documentation != null)
				{
					referentialConstraint2.Documentation = Converter.ConvertToDocumentation(referentialConstraint.Documentation);
				}
				if (referentialConstraint.PrincipalRole.Documentation != null)
				{
					referentialConstraint2.FromRole.Documentation = Converter.ConvertToDocumentation(referentialConstraint.PrincipalRole.Documentation);
				}
				if (referentialConstraint.DependentRole.Documentation != null)
				{
					referentialConstraint2.ToRole.Documentation = Converter.ConvertToDocumentation(referentialConstraint.DependentRole.Documentation);
				}
				associationType.AddReferentialConstraint(referentialConstraint2);
				Converter.AddOtherContent(element.Constraints[i], referentialConstraint2);
			}
			if (element.Documentation != null)
			{
				associationType.Documentation = Converter.ConvertToDocumentation(element.Documentation);
			}
			Converter.AddOtherContent(element, associationType);
			return associationType;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00016244 File Offset: 0x00014444
		private static AssociationEndMember InitializeAssociationEndMember(AssociationType associationType, IRelationshipEnd end, EntityType endMemberType)
		{
			EdmMember edmMember;
			AssociationEndMember associationEndMember;
			if (!associationType.Members.TryGetValue(end.Name, false, out edmMember))
			{
				associationEndMember = new AssociationEndMember(end.Name, endMemberType.GetReferenceType(), end.Multiplicity.Value);
				associationType.AddKeyMember(associationEndMember);
			}
			else
			{
				associationEndMember = (AssociationEndMember)edmMember;
			}
			RelationshipEnd relationshipEnd = end as RelationshipEnd;
			if (relationshipEnd != null && relationshipEnd.Documentation != null)
			{
				associationEndMember.Documentation = Converter.ConvertToDocumentation(relationshipEnd.Documentation);
			}
			return associationEndMember;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x000162BC File Offset: 0x000144BC
		private static EdmProperty[] GetProperties(EntityTypeBase entityType, IList<PropertyRefElement> properties)
		{
			EdmProperty[] array = new EdmProperty[properties.Count];
			for (int i = 0; i < properties.Count; i++)
			{
				array[i] = (EdmProperty)entityType.Members[properties[i].Name];
			}
			return array;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00016306 File Offset: 0x00014506
		private static void AddOtherContent(SchemaElement element, MetadataItem item)
		{
			if (element.OtherContent.Count > 0)
			{
				item.AddMetadataProperties(element.OtherContent);
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00016324 File Offset: 0x00014524
		private static EntitySet ConvertToEntitySet(EntityContainerEntitySet set, string containerName, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntitySet entitySet = new EntitySet(set.Name, set.DbSchema, set.Table, set.DefiningQuery, (EntityType)Converter.LoadSchemaElement(set.EntityType, providerManifest, convertedItemCache, newGlobalItems));
			if (set.Documentation != null)
			{
				entitySet.Documentation = Converter.ConvertToDocumentation(set.Documentation);
			}
			Converter.AddOtherContent(set, entitySet);
			return entitySet;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00016384 File Offset: 0x00014584
		private static EntitySet GetEntitySet(EntityContainerEntitySet set, EntityContainer container)
		{
			return container.GetEntitySetByName(set.Name, false);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00016394 File Offset: 0x00014594
		private static AssociationSet ConvertToAssociationSet(EntityContainerRelationshipSet relationshipSet, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, EntityContainer container, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			AssociationType associationType = (AssociationType)Converter.LoadSchemaElement((SchemaType)relationshipSet.Relationship, providerManifest, convertedItemCache, newGlobalItems);
			AssociationSet associationSet = new AssociationSet(relationshipSet.Name, associationType);
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in relationshipSet.Ends)
			{
				EntityType entityType = (EntityType)Converter.LoadSchemaElement(entityContainerRelationshipSetEnd.EntitySet.EntityType, providerManifest, convertedItemCache, newGlobalItems);
				AssociationEndMember associationEndMember = (AssociationEndMember)associationType.Members[entityContainerRelationshipSetEnd.Name];
				AssociationSetEnd associationSetEnd = new AssociationSetEnd(Converter.GetEntitySet(entityContainerRelationshipSetEnd.EntitySet, container), associationSet, associationEndMember);
				Converter.AddOtherContent(entityContainerRelationshipSetEnd, associationSetEnd);
				associationSet.AddAssociationSetEnd(associationSetEnd);
				if (entityContainerRelationshipSetEnd.Documentation != null)
				{
					associationSetEnd.Documentation = Converter.ConvertToDocumentation(entityContainerRelationshipSetEnd.Documentation);
				}
			}
			if (relationshipSet.Documentation != null)
			{
				associationSet.Documentation = Converter.ConvertToDocumentation(relationshipSet.Documentation);
			}
			Converter.AddOtherContent(relationshipSet, associationSet);
			return associationSet;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00016498 File Offset: 0x00014698
		private static EdmProperty ConvertToProperty(StructuredProperty somProperty, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			TypeUsage typeUsage = null;
			if (somProperty.Type is ScalarType)
			{
				if (somProperty.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
				{
					typeUsage = Converter.GetCsdlPrimitiveTypeUsageWithFacets(somProperty, convertedItemCache);
				}
				else
				{
					typeUsage = somProperty.TypeUsage;
					Converter.UpdateSentinelValuesInFacets(ref typeUsage);
				}
			}
			else
			{
				EdmType edmType = (EdmType)Converter.LoadSchemaElement(somProperty.Type, providerManifest, convertedItemCache, newGlobalItems);
				if (somProperty.CollectionKind != CollectionKind.None)
				{
					edmType = new CollectionType(edmType);
				}
				typeUsage = TypeUsage.Create(edmType);
			}
			Converter.PopulateGeneralFacets(somProperty, providerManifest, ref typeUsage);
			EdmProperty edmProperty = new EdmProperty(somProperty.Name, typeUsage);
			if (somProperty.Documentation != null)
			{
				edmProperty.Documentation = Converter.ConvertToDocumentation(somProperty.Documentation);
			}
			Converter.AddOtherContent(somProperty, edmProperty);
			return edmProperty;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0001653C File Offset: 0x0001473C
		private static NavigationProperty ConvertToNavigationProperty(EntityType declaringEntityType, NavigationProperty somNavigationProperty, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntityType entityType = (EntityType)Converter.LoadSchemaElement(somNavigationProperty.Type, providerManifest, convertedItemCache, newGlobalItems);
			AssociationType associationType = (AssociationType)Converter.LoadSchemaElement((Relationship)somNavigationProperty.Relationship, providerManifest, convertedItemCache, newGlobalItems);
			IRelationshipEnd relationshipEnd = null;
			somNavigationProperty.Relationship.TryGetEnd(somNavigationProperty.ToEnd.Name, out relationshipEnd);
			RelationshipMultiplicity? relationshipMultiplicity = relationshipEnd.Multiplicity;
			RelationshipMultiplicity relationshipMultiplicity2 = RelationshipMultiplicity.Many;
			EdmType edmType;
			if ((relationshipMultiplicity.GetValueOrDefault() == relationshipMultiplicity2) & (relationshipMultiplicity != null))
			{
				edmType = entityType.GetCollectionType();
			}
			else
			{
				edmType = entityType;
			}
			relationshipMultiplicity = relationshipEnd.Multiplicity;
			relationshipMultiplicity2 = RelationshipMultiplicity.One;
			TypeUsage typeUsage;
			if ((relationshipMultiplicity.GetValueOrDefault() == relationshipMultiplicity2) & (relationshipMultiplicity != null))
			{
				typeUsage = TypeUsage.Create(edmType, new FacetValues
				{
					Nullable = new bool?(false)
				});
			}
			else
			{
				typeUsage = TypeUsage.Create(edmType);
			}
			Converter.InitializeAssociationEndMember(associationType, somNavigationProperty.ToEnd, entityType);
			Converter.InitializeAssociationEndMember(associationType, somNavigationProperty.FromEnd, declaringEntityType);
			NavigationProperty navigationProperty = new NavigationProperty(somNavigationProperty.Name, typeUsage);
			navigationProperty.RelationshipType = associationType;
			navigationProperty.ToEndMember = (RelationshipEndMember)associationType.Members[somNavigationProperty.ToEnd.Name];
			navigationProperty.FromEndMember = (RelationshipEndMember)associationType.Members[somNavigationProperty.FromEnd.Name];
			if (somNavigationProperty.Documentation != null)
			{
				navigationProperty.Documentation = Converter.ConvertToDocumentation(somNavigationProperty.Documentation);
			}
			Converter.AddOtherContent(somNavigationProperty, navigationProperty);
			return navigationProperty;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x000166A4 File Offset: 0x000148A4
		private static EdmFunction ConvertToFunction(Function somFunction, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, EntityContainer entityContainer, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			GlobalItem globalItem = null;
			if (!somFunction.IsFunctionImport && newGlobalItems.TryGetValue(somFunction, out globalItem))
			{
				return (EdmFunction)globalItem;
			}
			FunctionParameter functionParameter = null;
			bool flag = somFunction.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel;
			TypeUsage functionTypeUsage = Converter.GetFunctionTypeUsage(somFunction is ModelFunction, somFunction, somFunction.ReturnType, providerManifest, flag, somFunction.Type, somFunction.CollectionKind, somFunction != null && somFunction.IsReturnAttributeReftype, somFunction, convertedItemCache, newGlobalItems);
			if (functionTypeUsage != null)
			{
				functionParameter = new FunctionParameter("ReturnType", functionTypeUsage, ParameterMode.ReturnValue);
			}
			else if (somFunction.Type != null)
			{
				return null;
			}
			if (somFunction.ReturnType != null)
			{
				Converter.AddOtherContent(somFunction.ReturnType, functionParameter);
			}
			EntitySet entitySet = null;
			if (somFunction.EntitySet != null)
			{
				entitySet = Converter.GetEntitySet(somFunction.EntitySet, entityContainer);
			}
			List<FunctionParameter> list = new List<FunctionParameter>();
			foreach (Parameter parameter in somFunction.Parameters)
			{
				TypeUsage functionTypeUsage2 = Converter.GetFunctionTypeUsage(somFunction is ModelFunction, somFunction, parameter, providerManifest, flag, parameter.Type, parameter.CollectionKind, parameter.IsRefType, parameter, convertedItemCache, newGlobalItems);
				if (functionTypeUsage2 == null)
				{
					return null;
				}
				FunctionParameter functionParameter2 = new FunctionParameter(parameter.Name, functionTypeUsage2, Converter.GetParameterMode(parameter.ParameterDirection));
				Converter.AddOtherContent(parameter, functionParameter2);
				if (parameter.Documentation != null)
				{
					functionParameter2.Documentation = Converter.ConvertToDocumentation(parameter.Documentation);
				}
				list.Add(functionParameter2);
			}
			EdmFunction edmFunction = new EdmFunction(somFunction.Name, somFunction.Schema.Namespace, Converter.GetDataSpace(providerManifest), new EdmFunctionPayload
			{
				Schema = somFunction.DbSchema,
				StoreFunctionName = somFunction.StoreFunctionName,
				CommandText = somFunction.CommandText,
				EntitySet = entitySet,
				IsAggregate = new bool?(somFunction.IsAggregate),
				IsBuiltIn = new bool?(somFunction.IsBuiltIn),
				IsNiladic = new bool?(somFunction.IsNiladicFunction),
				IsComposable = new bool?(somFunction.IsComposable),
				IsFromProviderManifest = new bool?(flag),
				ReturnParameter = functionParameter,
				Parameters = list.ToArray(),
				ParameterTypeSemantics = new ParameterTypeSemantics?(somFunction.ParameterTypeSemantics)
			});
			if (!somFunction.IsFunctionImport)
			{
				newGlobalItems.Add(somFunction, edmFunction);
			}
			if (somFunction.Documentation != null)
			{
				edmFunction.Documentation = Converter.ConvertToDocumentation(somFunction.Documentation);
			}
			Converter.AddOtherContent(somFunction, edmFunction);
			return edmFunction;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0001693C File Offset: 0x00014B3C
		private static Documentation ConvertToDocumentation(DocumentationElement element)
		{
			return element.MetadataDocumentation;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00016944 File Offset: 0x00014B44
		private static TypeUsage GetFunctionTypeUsage(bool isModelFunction, Function somFunction, FacetEnabledSchemaElement somParameter, DbProviderManifest providerManifest, bool areConvertingForProviderManifest, SchemaType type, CollectionKind collectionKind, bool isRefType, SchemaElement schemaElement, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			if (somParameter != null && areConvertingForProviderManifest && somParameter.HasUserDefinedFacets)
			{
				return somParameter.TypeUsage;
			}
			if (type != null)
			{
				EdmType edmType;
				if (!areConvertingForProviderManifest)
				{
					ScalarType scalarType = type as ScalarType;
					if (scalarType != null)
					{
						if (isModelFunction && somParameter != null)
						{
							if (somParameter.TypeUsage == null)
							{
								somParameter.ValidateAndSetTypeUsage(scalarType);
							}
							return somParameter.TypeUsage;
						}
						if (isModelFunction)
						{
							ModelFunction modelFunction = somFunction as ModelFunction;
							if (modelFunction.TypeUsage == null)
							{
								modelFunction.ValidateAndSetTypeUsage(scalarType);
							}
							return modelFunction.TypeUsage;
						}
						if (somParameter != null && somParameter.HasUserDefinedFacets && somFunction.Schema.DataModel == SchemaDataModelOption.ProviderDataModel)
						{
							somParameter.ValidateAndSetTypeUsage(scalarType);
							return somParameter.TypeUsage;
						}
						edmType = Converter.GetSsdlPrimitiveType(scalarType, providerManifest);
					}
					else
					{
						edmType = (EdmType)Converter.LoadSchemaElement(type, providerManifest, convertedItemCache, newGlobalItems);
					}
				}
				else if (type is TypeElement)
				{
					edmType = (type as TypeElement).PrimitiveType;
				}
				else
				{
					edmType = (type as ScalarType).Type;
				}
				TypeUsage typeUsage;
				if (collectionKind != CollectionKind.None)
				{
					typeUsage = convertedItemCache.GetCollectionTypeUsageWithNullFacets(edmType);
				}
				else if (edmType is EntityType && isRefType)
				{
					typeUsage = TypeUsage.Create(new RefType(edmType as EntityType));
				}
				else
				{
					typeUsage = convertedItemCache.GetTypeUsageWithNullFacets(edmType);
				}
				return typeUsage;
			}
			if (isModelFunction && somParameter != null && somParameter is Parameter)
			{
				(somParameter as Parameter).ResolveNestedTypeNames(convertedItemCache, newGlobalItems);
				return somParameter.TypeUsage;
			}
			if (isModelFunction && somFunction.ReturnType != null)
			{
				somFunction.ReturnType.ResolveNestedTypeNames(convertedItemCache, newGlobalItems);
				return somFunction.ReturnType.TypeUsage;
			}
			return null;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00016AAD File Offset: 0x00014CAD
		private static ParameterMode GetParameterMode(ParameterDirection parameterDirection)
		{
			switch (parameterDirection)
			{
			case ParameterDirection.Input:
				return ParameterMode.In;
			case ParameterDirection.Output:
				return ParameterMode.Out;
			}
			return ParameterMode.InOut;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00016ACC File Offset: 0x00014CCC
		private static void ApplyPrimitiveTypePropertyFacets(TypeUsage sourceType, ref TypeUsage targetType)
		{
			Dictionary<string, Facet> dictionary = targetType.Facets.ToDictionary((Facet f) => f.Name);
			bool flag = false;
			foreach (Facet facet in sourceType.Facets)
			{
				Facet facet2;
				if (dictionary.TryGetValue(facet.Name, out facet2))
				{
					if (!facet2.Description.IsConstant)
					{
						flag = true;
						dictionary[facet2.Name] = Facet.Create(facet2.Description, facet.Value);
					}
				}
				else
				{
					flag = true;
					dictionary.Add(facet.Name, facet);
				}
			}
			if (flag)
			{
				targetType = TypeUsage.Create(targetType.EdmType, dictionary.Values);
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00016BB0 File Offset: 0x00014DB0
		private static void PopulateGeneralFacets(StructuredProperty somProperty, DbProviderManifest providerManifest, ref TypeUsage propertyTypeUsage)
		{
			bool flag = false;
			Dictionary<string, Facet> dictionary = propertyTypeUsage.Facets.ToDictionary((Facet f) => f.Name);
			if (!somProperty.Nullable)
			{
				dictionary["Nullable"] = Facet.Create(MetadataItem.NullableFacetDescription, false);
				flag = true;
			}
			if (somProperty.Default != null)
			{
				dictionary["DefaultValue"] = Facet.Create(MetadataItem.DefaultValueFacetDescription, somProperty.DefaultAsObject);
				flag = true;
			}
			if (somProperty.Schema.SchemaVersion == 1.1)
			{
				Facet facet = Facet.Create(MetadataItem.CollectionKindFacetDescription, somProperty.CollectionKind);
				dictionary.Add(facet.Name, facet);
				flag = true;
			}
			if (flag)
			{
				propertyTypeUsage = TypeUsage.Create(propertyTypeUsage.EdmType, dictionary.Values);
			}
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00016C87 File Offset: 0x00014E87
		private static DataSpace GetDataSpace(DbProviderManifest providerManifest)
		{
			if (providerManifest is EdmProviderManifest)
			{
				return DataSpace.CSpace;
			}
			return DataSpace.SSpace;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00016C94 File Offset: 0x00014E94
		private static PrimitiveType GetSsdlPrimitiveType(ScalarType scalarType, DbProviderManifest providerManifest)
		{
			PrimitiveType primitiveType = null;
			string name = scalarType.Name;
			foreach (PrimitiveType primitiveType2 in providerManifest.GetStoreTypes())
			{
				if (primitiveType2.Name == name)
				{
					primitiveType = primitiveType2;
					break;
				}
			}
			return primitiveType;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00016CF8 File Offset: 0x00014EF8
		private static void UpdateSentinelValuesInFacets(ref TypeUsage typeUsage)
		{
			PrimitiveType primitiveType = (PrimitiveType)typeUsage.EdmType;
			if ((primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.String || primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Binary) && Helper.IsUnboundedFacetValue(typeUsage.Facets["MaxLength"]))
			{
				typeUsage = typeUsage.ShallowCopy(new FacetValues
				{
					MaxLength = Helper.GetFacet(primitiveType.FacetDescriptions, "MaxLength").MaxValue
				});
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00016D6C File Offset: 0x00014F6C
		private static TypeUsage GetCsdlPrimitiveTypeUsageWithFacets(StructuredProperty somProperty, Converter.ConversionCache convertedItemCache)
		{
			EdmType edmType = convertedItemCache.ItemCollection.GetItem<PrimitiveType>(somProperty.TypeUsage.EdmType.FullName);
			TypeUsage typeUsage = null;
			if (somProperty.CollectionKind != CollectionKind.None)
			{
				edmType = new CollectionType(edmType);
				typeUsage = TypeUsage.Create(edmType);
			}
			else
			{
				typeUsage = TypeUsage.Create(edmType);
				Converter.ApplyPrimitiveTypePropertyFacets(somProperty.TypeUsage, ref typeUsage);
			}
			return typeUsage;
		}

		// Token: 0x04000768 RID: 1896
		internal static readonly FacetDescription ConcurrencyModeFacet;

		// Token: 0x04000769 RID: 1897
		internal static readonly FacetDescription StoreGeneratedPatternFacet;

		// Token: 0x0400076A RID: 1898
		internal static readonly FacetDescription CollationFacet;

		// Token: 0x020002B5 RID: 693
		internal class ConversionCache
		{
			// Token: 0x06001C55 RID: 7253 RVA: 0x0004EBCA File Offset: 0x0004CDCA
			internal ConversionCache(ItemCollection itemCollection)
			{
				this.ItemCollection = itemCollection;
				this._nullFacetsTypeUsage = new Dictionary<EdmType, TypeUsage>();
				this._nullFacetsCollectionTypeUsage = new Dictionary<EdmType, TypeUsage>();
			}

			// Token: 0x06001C56 RID: 7254 RVA: 0x0004EBF0 File Offset: 0x0004CDF0
			internal TypeUsage GetTypeUsageWithNullFacets(EdmType edmType)
			{
				TypeUsage typeUsage;
				if (this._nullFacetsTypeUsage.TryGetValue(edmType, out typeUsage))
				{
					return typeUsage;
				}
				typeUsage = TypeUsage.Create(edmType, FacetValues.NullFacetValues);
				this._nullFacetsTypeUsage.Add(edmType, typeUsage);
				return typeUsage;
			}

			// Token: 0x06001C57 RID: 7255 RVA: 0x0004EC2C File Offset: 0x0004CE2C
			internal TypeUsage GetCollectionTypeUsageWithNullFacets(EdmType edmType)
			{
				TypeUsage typeUsage;
				if (this._nullFacetsCollectionTypeUsage.TryGetValue(edmType, out typeUsage))
				{
					return typeUsage;
				}
				typeUsage = TypeUsage.Create(new CollectionType(this.GetTypeUsageWithNullFacets(edmType)), FacetValues.NullFacetValues);
				this._nullFacetsCollectionTypeUsage.Add(edmType, typeUsage);
				return typeUsage;
			}

			// Token: 0x04000FA6 RID: 4006
			internal readonly ItemCollection ItemCollection;

			// Token: 0x04000FA7 RID: 4007
			private readonly Dictionary<EdmType, TypeUsage> _nullFacetsTypeUsage;

			// Token: 0x04000FA8 RID: 4008
			private readonly Dictionary<EdmType, TypeUsage> _nullFacetsCollectionTypeUsage;
		}
	}
}
