using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm.Provider;
using System.Data.Entity.Core.SchemaObjectModel;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000494 RID: 1172
	internal static class Converter
	{
		// Token: 0x060039D9 RID: 14809 RVA: 0x000BE738 File Offset: 0x000BC938
		static Converter()
		{
			EnumType enumType = new EnumType("ConcurrencyMode", "Edm", PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int32), false, DataSpace.CSpace);
			foreach (string text in Enum.GetNames(typeof(ConcurrencyMode)))
			{
				enumType.AddMember(new EnumMember(text, (int)Enum.Parse(typeof(ConcurrencyMode), text, false)));
			}
			EnumType enumType2 = new EnumType("StoreGeneratedPattern", "Edm", PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int32), false, DataSpace.CSpace);
			foreach (string text2 in Enum.GetNames(typeof(StoreGeneratedPattern)))
			{
				enumType2.AddMember(new EnumMember(text2, (int)Enum.Parse(typeof(StoreGeneratedPattern), text2, false)));
			}
			Converter.ConcurrencyModeFacet = new FacetDescription("ConcurrencyMode", enumType, null, null, ConcurrencyMode.None);
			Converter.StoreGeneratedPatternFacet = new FacetDescription("StoreGeneratedPattern", enumType2, null, null, StoreGeneratedPattern.None);
			Converter.CollationFacet = new FacetDescription("Collation", MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.String), null, null, string.Empty);
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000BE898 File Offset: 0x000BCA98
		internal static IEnumerable<GlobalItem> ConvertSchema(Schema somSchema, DbProviderManifest providerManifest, ItemCollection itemCollection)
		{
			Dictionary<SchemaElement, GlobalItem> dictionary = new Dictionary<SchemaElement, GlobalItem>();
			Converter.ConvertSchema(somSchema, providerManifest, new Converter.ConversionCache(itemCollection), dictionary);
			return dictionary.Values;
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000BE8C0 File Offset: 0x000BCAC0
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

		// Token: 0x060039DC RID: 14812 RVA: 0x000BE91C File Offset: 0x000BCB1C
		private static void ConvertSchema(Schema somSchema, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			List<Function> list = new List<Function>();
			foreach (SchemaType schemaType in somSchema.SchemaTypes)
			{
				if (Converter.LoadSchemaElement(schemaType, providerManifest, convertedItemCache, newGlobalItems) == null)
				{
					Function function = schemaType as Function;
					if (function != null)
					{
						list.Add(function);
					}
				}
			}
			foreach (SchemaEntityType schemaEntityType in somSchema.SchemaTypes.OfType<SchemaEntityType>())
			{
				Converter.LoadEntityTypePhase2(schemaEntityType, providerManifest, convertedItemCache, newGlobalItems);
			}
			foreach (Function function2 in list)
			{
				Converter.LoadSchemaElement(function2, providerManifest, convertedItemCache, newGlobalItems);
			}
			if (convertedItemCache.ItemCollection.DataSpace == DataSpace.CSpace)
			{
				((EdmItemCollection)convertedItemCache.ItemCollection).EdmVersion = somSchema.SchemaVersion;
				return;
			}
			StoreItemCollection storeItemCollection = convertedItemCache.ItemCollection as StoreItemCollection;
			if (storeItemCollection != null)
			{
				storeItemCollection.StoreSchemaVersion = somSchema.SchemaVersion;
			}
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000BEA58 File Offset: 0x000BCC58
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
			else if (element is Function)
			{
				globalItem = Converter.ConvertToFunction((Function)element, providerManifest, convertedItemCache, null, newGlobalItems);
			}
			else
			{
				if (!(element is SchemaEnumType))
				{
					return null;
				}
				globalItem = Converter.ConvertToEnumType((SchemaEnumType)element, newGlobalItems);
			}
			return globalItem;
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000BEB08 File Offset: 0x000BCD08
		private static EntityContainer ConvertToEntityContainer(EntityContainer element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntityContainer entityContainer = new EntityContainer(element.Name, Converter.GetDataSpace(providerManifest));
			newGlobalItems.Add(element, entityContainer);
			foreach (EntityContainerEntitySet entityContainerEntitySet in element.EntitySets)
			{
				entityContainer.AddEntitySetBase(Converter.ConvertToEntitySet(entityContainerEntitySet, providerManifest, convertedItemCache, newGlobalItems));
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

		// Token: 0x060039DF RID: 14815 RVA: 0x000BEC2C File Offset: 0x000BCE2C
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

		// Token: 0x060039E0 RID: 14816 RVA: 0x000BED5C File Offset: 0x000BCF5C
		private static void LoadEntityTypePhase2(SchemaEntityType element, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntityType entityType = (EntityType)newGlobalItems[element];
			foreach (NavigationProperty navigationProperty in element.NavigationProperties)
			{
				entityType.AddMember(Converter.ConvertToNavigationProperty(entityType, navigationProperty, providerManifest, convertedItemCache, newGlobalItems));
			}
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000BEDC0 File Offset: 0x000BCFC0
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

		// Token: 0x060039E2 RID: 14818 RVA: 0x000BEE84 File Offset: 0x000BD084
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
						OperationAction operationAction = OperationAction.None;
						global::System.Data.Entity.Core.SchemaObjectModel.Action action = onOperation.Action;
						if (action != global::System.Data.Entity.Core.SchemaObjectModel.Action.None)
						{
							if (action == global::System.Data.Entity.Core.SchemaObjectModel.Action.Cascade)
							{
								operationAction = OperationAction.Cascade;
							}
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

		// Token: 0x060039E3 RID: 14819 RVA: 0x000BF11C File Offset: 0x000BD31C
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

		// Token: 0x060039E4 RID: 14820 RVA: 0x000BF194 File Offset: 0x000BD394
		private static EdmProperty[] GetProperties(EntityTypeBase entityType, IList<PropertyRefElement> properties)
		{
			EdmProperty[] array = new EdmProperty[properties.Count];
			for (int i = 0; i < properties.Count; i++)
			{
				array[i] = (EdmProperty)entityType.Members[properties[i].Name];
			}
			return array;
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x000BF1DE File Offset: 0x000BD3DE
		private static void AddOtherContent(SchemaElement element, MetadataItem item)
		{
			if (element.OtherContent.Count > 0)
			{
				item.AddMetadataProperties(element.OtherContent);
			}
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x000BF1FC File Offset: 0x000BD3FC
		private static EntitySet ConvertToEntitySet(EntityContainerEntitySet set, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EntitySet entitySet = new EntitySet(set.Name, set.DbSchema, set.Table, set.DefiningQuery, (EntityType)Converter.LoadSchemaElement(set.EntityType, providerManifest, convertedItemCache, newGlobalItems));
			if (set.Documentation != null)
			{
				entitySet.Documentation = Converter.ConvertToDocumentation(set.Documentation);
			}
			Converter.AddOtherContent(set, entitySet);
			return entitySet;
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x000BF25B File Offset: 0x000BD45B
		private static EntitySet GetEntitySet(EntityContainerEntitySet set, EntityContainer container)
		{
			return container.GetEntitySetByName(set.Name, false);
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000BF26C File Offset: 0x000BD46C
		private static AssociationSet ConvertToAssociationSet(EntityContainerRelationshipSet relationshipSet, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, EntityContainer container, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			AssociationType associationType = (AssociationType)Converter.LoadSchemaElement((SchemaType)relationshipSet.Relationship, providerManifest, convertedItemCache, newGlobalItems);
			AssociationSet associationSet = new AssociationSet(relationshipSet.Name, associationType);
			foreach (EntityContainerRelationshipSetEnd entityContainerRelationshipSetEnd in relationshipSet.Ends)
			{
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

		// Token: 0x060039E9 RID: 14825 RVA: 0x000BF354 File Offset: 0x000BD554
		private static EdmProperty ConvertToProperty(StructuredProperty somProperty, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			TypeUsage typeUsage = null;
			ScalarType scalarType = somProperty.Type as ScalarType;
			if (scalarType != null && somProperty.Schema.DataModel != SchemaDataModelOption.EntityDataModel)
			{
				typeUsage = somProperty.TypeUsage;
				Converter.UpdateSentinelValuesInFacets(ref typeUsage);
			}
			else
			{
				EdmType edmType;
				if (scalarType != null)
				{
					edmType = convertedItemCache.ItemCollection.GetItem<PrimitiveType>(somProperty.TypeUsage.EdmType.FullName);
				}
				else
				{
					edmType = (EdmType)Converter.LoadSchemaElement(somProperty.Type, providerManifest, convertedItemCache, newGlobalItems);
				}
				if (somProperty.CollectionKind != CollectionKind.None)
				{
					typeUsage = TypeUsage.Create(new CollectionType(edmType));
				}
				else
				{
					bool flag = ((scalarType == null) ? (somProperty.Type as SchemaEnumType) : null) != null;
					typeUsage = TypeUsage.Create(edmType);
					if (flag)
					{
						somProperty.EnsureEnumTypeFacets(convertedItemCache, newGlobalItems);
					}
					if (somProperty.TypeUsage != null)
					{
						Converter.ApplyTypePropertyFacets(somProperty.TypeUsage, ref typeUsage);
					}
				}
			}
			Converter.PopulateGeneralFacets(somProperty, ref typeUsage);
			EdmProperty edmProperty = new EdmProperty(somProperty.Name, typeUsage);
			if (somProperty.Documentation != null)
			{
				edmProperty.Documentation = Converter.ConvertToDocumentation(somProperty.Documentation);
			}
			Converter.AddOtherContent(somProperty, edmProperty);
			return edmProperty;
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x000BF44C File Offset: 0x000BD64C
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

		// Token: 0x060039EB RID: 14827 RVA: 0x000BF5B4 File Offset: 0x000BD7B4
		private static EdmFunction ConvertToFunction(Function somFunction, DbProviderManifest providerManifest, Converter.ConversionCache convertedItemCache, EntityContainer functionImportEntityContainer, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			GlobalItem globalItem = null;
			if (!somFunction.IsFunctionImport && newGlobalItems.TryGetValue(somFunction, out globalItem))
			{
				return (EdmFunction)globalItem;
			}
			bool flag = somFunction.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel;
			List<FunctionParameter> list = new List<FunctionParameter>();
			if (somFunction.ReturnTypeList != null)
			{
				int num = 0;
				using (IEnumerator<ReturnType> enumerator = somFunction.ReturnTypeList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ReturnType returnType2 = enumerator.Current;
						TypeUsage functionTypeUsage = Converter.GetFunctionTypeUsage(somFunction is ModelFunction, somFunction, returnType2, providerManifest, flag, returnType2.Type, returnType2.CollectionKind, returnType2.IsRefType, convertedItemCache, newGlobalItems);
						if (functionTypeUsage == null)
						{
							return null;
						}
						string text = ((num == 0) ? string.Empty : num.ToString(CultureInfo.InvariantCulture));
						num++;
						FunctionParameter functionParameter = new FunctionParameter("ReturnType" + text, functionTypeUsage, ParameterMode.ReturnValue);
						Converter.AddOtherContent(returnType2, functionParameter);
						list.Add(functionParameter);
					}
					goto IL_014F;
				}
			}
			if (somFunction.Type != null)
			{
				TypeUsage functionTypeUsage2 = Converter.GetFunctionTypeUsage(somFunction is ModelFunction, somFunction, null, providerManifest, flag, somFunction.Type, somFunction.CollectionKind, somFunction.IsReturnAttributeReftype, convertedItemCache, newGlobalItems);
				if (functionTypeUsage2 == null)
				{
					return null;
				}
				list.Add(new FunctionParameter("ReturnType", functionTypeUsage2, ParameterMode.ReturnValue));
			}
			IL_014F:
			EntitySet[] array = null;
			string text2;
			if (somFunction.IsFunctionImport)
			{
				FunctionImportElement functionImportElement = (FunctionImportElement)somFunction;
				text2 = functionImportElement.Container.Name;
				if (functionImportElement.EntitySet != null)
				{
					EntityContainer functionImportEntityContainer2 = functionImportEntityContainer;
					array = new EntitySet[] { Converter.GetEntitySet(functionImportElement.EntitySet, functionImportEntityContainer2) };
				}
				else if (functionImportElement.ReturnTypeList != null)
				{
					array = functionImportElement.ReturnTypeList.Select(delegate(ReturnType returnType)
					{
						if (returnType.EntitySet == null)
						{
							return null;
						}
						return Converter.GetEntitySet(returnType.EntitySet, functionImportEntityContainer);
					}).ToArray<EntitySet>();
				}
			}
			else
			{
				text2 = somFunction.Namespace;
			}
			List<FunctionParameter> list2 = new List<FunctionParameter>();
			foreach (Parameter parameter in somFunction.Parameters)
			{
				TypeUsage functionTypeUsage3 = Converter.GetFunctionTypeUsage(somFunction is ModelFunction, somFunction, parameter, providerManifest, flag, parameter.Type, parameter.CollectionKind, parameter.IsRefType, convertedItemCache, newGlobalItems);
				if (functionTypeUsage3 == null)
				{
					return null;
				}
				FunctionParameter functionParameter2 = new FunctionParameter(parameter.Name, functionTypeUsage3, Converter.GetParameterMode(parameter.ParameterDirection));
				Converter.AddOtherContent(parameter, functionParameter2);
				if (parameter.Documentation != null)
				{
					functionParameter2.Documentation = Converter.ConvertToDocumentation(parameter.Documentation);
				}
				list2.Add(functionParameter2);
			}
			EdmFunction edmFunction = new EdmFunction(somFunction.Name, text2, Converter.GetDataSpace(providerManifest), new EdmFunctionPayload
			{
				Schema = somFunction.DbSchema,
				StoreFunctionName = somFunction.StoreFunctionName,
				CommandText = somFunction.CommandText,
				EntitySets = array,
				IsAggregate = new bool?(somFunction.IsAggregate),
				IsBuiltIn = new bool?(somFunction.IsBuiltIn),
				IsNiladic = new bool?(somFunction.IsNiladicFunction),
				IsComposable = new bool?(somFunction.IsComposable),
				IsFromProviderManifest = new bool?(flag),
				IsFunctionImport = new bool?(somFunction.IsFunctionImport),
				ReturnParameters = list.ToArray(),
				Parameters = list2.ToArray(),
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

		// Token: 0x060039EC RID: 14828 RVA: 0x000BF974 File Offset: 0x000BDB74
		private static EnumType ConvertToEnumType(SchemaEnumType somEnumType, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			ScalarType scalarType = (ScalarType)somEnumType.UnderlyingType;
			EnumType enumType = new EnumType(somEnumType.Name, somEnumType.Namespace, scalarType.Type, somEnumType.IsFlags, DataSpace.CSpace);
			Type clrEquivalentType = scalarType.Type.ClrEquivalentType;
			foreach (SchemaEnumMember schemaEnumMember in somEnumType.EnumMembers)
			{
				EnumMember enumMember = new EnumMember(schemaEnumMember.Name, Convert.ChangeType(schemaEnumMember.Value, clrEquivalentType, CultureInfo.InvariantCulture));
				if (schemaEnumMember.Documentation != null)
				{
					enumMember.Documentation = Converter.ConvertToDocumentation(schemaEnumMember.Documentation);
				}
				Converter.AddOtherContent(schemaEnumMember, enumMember);
				enumType.AddMember(enumMember);
			}
			if (somEnumType.Documentation != null)
			{
				enumType.Documentation = Converter.ConvertToDocumentation(somEnumType.Documentation);
			}
			Converter.AddOtherContent(somEnumType, enumType);
			newGlobalItems.Add(somEnumType, enumType);
			return enumType;
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x000BFA70 File Offset: 0x000BDC70
		private static Documentation ConvertToDocumentation(DocumentationElement element)
		{
			return element.MetadataDocumentation;
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x000BFA78 File Offset: 0x000BDC78
		private static TypeUsage GetFunctionTypeUsage(bool isModelFunction, Function somFunction, FacetEnabledSchemaElement somParameter, DbProviderManifest providerManifest, bool areConvertingForProviderManifest, SchemaType type, CollectionKind collectionKind, bool isRefType, Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
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
						edmType = Converter.GetPrimitiveType(scalarType, providerManifest);
					}
					else
					{
						edmType = (EdmType)Converter.LoadSchemaElement(type, providerManifest, convertedItemCache, newGlobalItems);
						if (isModelFunction && type is SchemaEnumType)
						{
							if (somParameter != null)
							{
								somParameter.ValidateAndSetTypeUsage(edmType);
								return somParameter.TypeUsage;
							}
							if (somFunction != null)
							{
								ModelFunction modelFunction2 = (ModelFunction)somFunction;
								modelFunction2.ValidateAndSetTypeUsage(edmType);
								return modelFunction2.TypeUsage;
							}
						}
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
				else
				{
					EntityType entityType = edmType as EntityType;
					if (entityType != null && isRefType)
					{
						typeUsage = TypeUsage.Create(new RefType(entityType));
					}
					else
					{
						typeUsage = convertedItemCache.GetTypeUsageWithNullFacets(edmType);
					}
				}
				return typeUsage;
			}
			if (isModelFunction && somParameter != null && somParameter is Parameter)
			{
				((Parameter)somParameter).ResolveNestedTypeNames(convertedItemCache, newGlobalItems);
				return somParameter.TypeUsage;
			}
			if (somParameter != null && somParameter is ReturnType)
			{
				((ReturnType)somParameter).ResolveNestedTypeNames(convertedItemCache, newGlobalItems);
				return somParameter.TypeUsage;
			}
			return null;
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x000BFC0D File Offset: 0x000BDE0D
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

		// Token: 0x060039F0 RID: 14832 RVA: 0x000BFC2C File Offset: 0x000BDE2C
		private static void ApplyTypePropertyFacets(TypeUsage sourceType, ref TypeUsage targetType)
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

		// Token: 0x060039F1 RID: 14833 RVA: 0x000BFD10 File Offset: 0x000BDF10
		private static void PopulateGeneralFacets(StructuredProperty somProperty, ref TypeUsage propertyTypeUsage)
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

		// Token: 0x060039F2 RID: 14834 RVA: 0x000BFDE7 File Offset: 0x000BDFE7
		private static DataSpace GetDataSpace(DbProviderManifest providerManifest)
		{
			if (providerManifest is EdmProviderManifest)
			{
				return DataSpace.CSpace;
			}
			return DataSpace.SSpace;
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x000BFDF4 File Offset: 0x000BDFF4
		private static PrimitiveType GetPrimitiveType(ScalarType scalarType, DbProviderManifest providerManifest)
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

		// Token: 0x060039F4 RID: 14836 RVA: 0x000BFE58 File Offset: 0x000BE058
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

		// Token: 0x04001354 RID: 4948
		internal static readonly FacetDescription ConcurrencyModeFacet;

		// Token: 0x04001355 RID: 4949
		internal static readonly FacetDescription StoreGeneratedPatternFacet;

		// Token: 0x04001356 RID: 4950
		internal static readonly FacetDescription CollationFacet;

		// Token: 0x02000AC2 RID: 2754
		internal class ConversionCache
		{
			// Token: 0x060062F1 RID: 25329 RVA: 0x00157594 File Offset: 0x00155794
			internal ConversionCache(ItemCollection itemCollection)
			{
				this.ItemCollection = itemCollection;
				this._nullFacetsTypeUsage = new Dictionary<EdmType, TypeUsage>();
				this._nullFacetsCollectionTypeUsage = new Dictionary<EdmType, TypeUsage>();
			}

			// Token: 0x060062F2 RID: 25330 RVA: 0x001575BC File Offset: 0x001557BC
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

			// Token: 0x060062F3 RID: 25331 RVA: 0x001575F8 File Offset: 0x001557F8
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

			// Token: 0x04002B93 RID: 11155
			internal readonly ItemCollection ItemCollection;

			// Token: 0x04002B94 RID: 11156
			private readonly Dictionary<EdmType, TypeUsage> _nullFacetsTypeUsage;

			// Token: 0x04002B95 RID: 11157
			private readonly Dictionary<EdmType, TypeUsage> _nullFacetsCollectionTypeUsage;
		}
	}
}
