using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004AB RID: 1195
	internal static class EdmModelSemanticValidationRules
	{
		// Token: 0x06003AC0 RID: 15040 RVA: 0x000C1E13 File Offset: 0x000C0013
		private static string GetQualifiedName(INamedDataModelItem item, string qualifiedPrefix)
		{
			return qualifiedPrefix + "." + item.Name;
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x000C1E26 File Offset: 0x000C0026
		private static bool AreRelationshipEndsEqual(KeyValuePair<AssociationSet, EntitySet> left, KeyValuePair<AssociationSet, EntitySet> right)
		{
			return left.Value == right.Value && left.Key.ElementType == right.Key.ElementType;
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000C1E58 File Offset: 0x000C0058
		private static bool IsReferentialConstraintReadyForValidation(AssociationType association)
		{
			ReferentialConstraint constraint = association.Constraint;
			if (constraint == null)
			{
				return false;
			}
			if (constraint.FromRole == null || constraint.ToRole == null)
			{
				return false;
			}
			if (constraint.FromRole.GetEntityType() == null || constraint.ToRole.GetEntityType() == null)
			{
				return false;
			}
			if (constraint.ToProperties.Any<EdmProperty>())
			{
				using (ReadOnlyMetadataCollection<EdmProperty>.Enumerator enumerator = constraint.ToProperties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EdmProperty edmProperty = enumerator.Current;
						if (edmProperty == null)
						{
							return false;
						}
						if (edmProperty.TypeUsage == null || edmProperty.TypeUsage.EdmType == null)
						{
							return false;
						}
					}
					goto IL_009A;
				}
				return false;
				IL_009A:
				IEnumerable<EdmProperty> validKey = constraint.FromRole.GetEntityType().GetValidKey();
				if (validKey.Any<EdmProperty>())
				{
					return validKey.All((EdmProperty propRef) => propRef != null && propRef.TypeUsage != null && propRef.TypeUsage.EdmType != null);
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x000C1F54 File Offset: 0x000C0154
		private static void IsKeyProperty(List<EdmProperty> roleProperties, RelationshipEndMember roleElement, out bool isKeyProperty, out bool areAllPropertiesNullable, out bool isAnyPropertyNullable, out bool isSubsetOfKeyProperties)
		{
			isKeyProperty = true;
			areAllPropertiesNullable = true;
			isAnyPropertyNullable = false;
			isSubsetOfKeyProperties = true;
			if (roleElement.GetEntityType().GetValidKey().Count<EdmProperty>() != roleProperties.Count<EdmProperty>())
			{
				isKeyProperty = false;
			}
			for (int i = 0; i < roleProperties.Count<EdmProperty>(); i++)
			{
				if (isSubsetOfKeyProperties && !roleElement.GetEntityType().GetValidKey().ToList<EdmProperty>()
					.Contains(roleProperties[i]))
				{
					isKeyProperty = false;
					isSubsetOfKeyProperties = false;
				}
				bool nullable = roleProperties[i].Nullable;
				areAllPropertiesNullable = areAllPropertiesNullable && nullable;
				isAnyPropertyNullable = isAnyPropertyNullable || nullable;
			}
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x000C1FE0 File Offset: 0x000C01E0
		private static void AddMemberNameToHashSet(INamedDataModelItem item, HashSet<string> memberNameList, EdmModelValidationContext context, Func<string, string> getErrorString)
		{
			if (!string.IsNullOrWhiteSpace(item.Name) && !memberNameList.Add(item.Name))
			{
				context.AddError((MetadataItem)item, "Name", getErrorString(item.Name));
			}
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000C201C File Offset: 0x000C021C
		private static bool CheckForInheritanceCycle<T>(T type, Func<T, T> getBaseType) where T : class
		{
			T t = getBaseType(type);
			if (t != null)
			{
				T t2 = t;
				T t3 = t;
				for (;;)
				{
					t3 = getBaseType(t3);
					if (t2 == t3)
					{
						break;
					}
					if (t2 == null)
					{
						return false;
					}
					t2 = getBaseType(t2);
					if (t3 != null)
					{
						t3 = getBaseType(t3);
					}
					if (t3 == null)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000C2080 File Offset: 0x000C0280
		private static bool IsPrimitiveTypesEqual(EdmProperty primitiveType1, EdmProperty primitiveType2)
		{
			return primitiveType1.PrimitiveType.PrimitiveTypeKind == primitiveType2.PrimitiveType.PrimitiveTypeKind;
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000C209A File Offset: 0x000C029A
		private static bool IsEdmSystemNamespace(string namespaceName)
		{
			return namespaceName == "Transient" || namespaceName == "Edm" || namespaceName == "System";
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x000C20C4 File Offset: 0x000C02C4
		private static bool IsTypeDefinesNewConcurrencyProperties(EntityType entityType)
		{
			return entityType.DeclaredProperties.Where((EdmProperty property) => property.TypeUsage != null).Any((EdmProperty property) => property.PrimitiveType != null && property.ConcurrencyMode > ConcurrencyMode.None);
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x000C2120 File Offset: 0x000C0320
		private static bool TypeIsSubTypeOf(EntityType entityType, Dictionary<EntityType, EntitySet> baseEntitySetTypes, out EntitySet set)
		{
			if (entityType.IsTypeHierarchyRoot())
			{
				set = null;
				return false;
			}
			foreach (EntityType entityType2 in entityType.ToHierarchy())
			{
				if (baseEntitySetTypes.ContainsKey(entityType2))
				{
					set = baseEntitySetTypes[entityType2];
					return true;
				}
			}
			set = null;
			return false;
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x000C2190 File Offset: 0x000C0390
		private static bool IsTypeHierarchyRoot(this EntityType entityType)
		{
			return entityType.BaseType == null;
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x000C219B File Offset: 0x000C039B
		private static bool IsForeignKey(this AssociationType association, double version)
		{
			return version >= 2.0 && association.Constraint != null;
		}

		// Token: 0x0400142F RID: 5167
		internal static readonly EdmModelValidationRule<EdmFunction> EdmFunction_ComposableFunctionImportsNotAllowed_V1_V2 = new EdmModelValidationRule<EdmFunction>(delegate(EdmModelValidationContext context, EdmFunction function)
		{
			if (function.IsFunctionImport && function.IsComposableAttribute)
			{
				context.AddError(function, null, Strings.EdmModel_Validator_Semantic_ComposableFunctionImportsNotSupportedForSchemaVersion);
			}
		});

		// Token: 0x04001430 RID: 5168
		internal static readonly EdmModelValidationRule<EdmFunction> EdmFunction_DuplicateParameterName = new EdmModelValidationRule<EdmFunction>(delegate(EdmModelValidationContext context, EdmFunction function)
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (FunctionParameter functionParameter in function.Parameters)
			{
				if (functionParameter != null && !string.IsNullOrWhiteSpace(functionParameter.Name))
				{
					EdmModelSemanticValidationRules.AddMemberNameToHashSet(functionParameter, hashSet, context, new Func<string, string>(Strings.ParameterNameAlreadyDefinedDuplicate));
				}
			}
		});

		// Token: 0x04001431 RID: 5169
		internal static readonly EdmModelValidationRule<EdmType> EdmType_SystemNamespaceEncountered = new EdmModelValidationRule<EdmType>(delegate(EdmModelValidationContext context, EdmType edmType)
		{
			if (EdmModelSemanticValidationRules.IsEdmSystemNamespace(edmType.NamespaceName) && edmType.BuiltInTypeKind != BuiltInTypeKind.RowType && edmType.BuiltInTypeKind != BuiltInTypeKind.CollectionType && edmType.BuiltInTypeKind != BuiltInTypeKind.PrimitiveType)
			{
				context.AddError(edmType, null, Strings.EdmModel_Validator_Semantic_SystemNamespaceEncountered(edmType.Name));
			}
		});

		// Token: 0x04001432 RID: 5170
		internal static readonly EdmModelValidationRule<EntityContainer> EdmEntityContainer_SimilarRelationshipEnd = new EdmModelValidationRule<EntityContainer>(delegate(EdmModelValidationContext context, EntityContainer edmEntityContainer)
		{
			List<KeyValuePair<AssociationSet, EntitySet>> list = new List<KeyValuePair<AssociationSet, EntitySet>>();
			List<KeyValuePair<AssociationSet, EntitySet>> list2 = new List<KeyValuePair<AssociationSet, EntitySet>>();
			foreach (AssociationSet associationSet in edmEntityContainer.AssociationSets)
			{
				KeyValuePair<AssociationSet, EntitySet> sourceEnd = new KeyValuePair<AssociationSet, EntitySet>(associationSet, associationSet.SourceSet);
				KeyValuePair<AssociationSet, EntitySet> targetEnd = new KeyValuePair<AssociationSet, EntitySet>(associationSet, associationSet.TargetSet);
				KeyValuePair<AssociationSet, EntitySet> keyValuePair = list.FirstOrDefault((KeyValuePair<AssociationSet, EntitySet> e) => EdmModelSemanticValidationRules.AreRelationshipEndsEqual(e, sourceEnd));
				KeyValuePair<AssociationSet, EntitySet> keyValuePair2 = list2.FirstOrDefault((KeyValuePair<AssociationSet, EntitySet> e) => EdmModelSemanticValidationRules.AreRelationshipEndsEqual(e, targetEnd));
				if (!keyValuePair.Equals(default(KeyValuePair<AssociationSet, EntitySet>)))
				{
					context.AddError(edmEntityContainer, null, Strings.EdmModel_Validator_Semantic_SimilarRelationshipEnd(keyValuePair.Key.ElementType.SourceEnd.Name, keyValuePair.Key.Name, associationSet.Name, keyValuePair.Value.Name, edmEntityContainer.Name));
				}
				else
				{
					list.Add(sourceEnd);
				}
				if (!keyValuePair2.Equals(default(KeyValuePair<AssociationSet, EntitySet>)))
				{
					context.AddError(edmEntityContainer, null, Strings.EdmModel_Validator_Semantic_SimilarRelationshipEnd(keyValuePair2.Key.ElementType.TargetEnd.Name, keyValuePair2.Key.Name, associationSet.Name, keyValuePair2.Value.Name, edmEntityContainer.Name));
				}
				else
				{
					list2.Add(targetEnd);
				}
			}
		});

		// Token: 0x04001433 RID: 5171
		internal static readonly EdmModelValidationRule<EntityContainer> EdmEntityContainer_InvalidEntitySetNameReference = new EdmModelValidationRule<EntityContainer>(delegate(EdmModelValidationContext context, EntityContainer edmEntityContainer)
		{
			if (edmEntityContainer.AssociationSets != null)
			{
				foreach (AssociationSet associationSet2 in edmEntityContainer.AssociationSets)
				{
					if (associationSet2.SourceSet != null && associationSet2.ElementType != null && associationSet2.ElementType.SourceEnd != null && !edmEntityContainer.EntitySets.Contains(associationSet2.SourceSet))
					{
						context.AddError(associationSet2.SourceSet, null, Strings.EdmModel_Validator_Semantic_InvalidEntitySetNameReference(associationSet2.SourceSet.Name, associationSet2.ElementType.SourceEnd.Name));
					}
					if (associationSet2.TargetSet != null && associationSet2.ElementType != null && associationSet2.ElementType.TargetEnd != null && !edmEntityContainer.EntitySets.Contains(associationSet2.TargetSet))
					{
						context.AddError(associationSet2.TargetSet, null, Strings.EdmModel_Validator_Semantic_InvalidEntitySetNameReference(associationSet2.TargetSet.Name, associationSet2.ElementType.TargetEnd.Name));
					}
				}
			}
		});

		// Token: 0x04001434 RID: 5172
		internal static readonly EdmModelValidationRule<EntityContainer> EdmEntityContainer_ConcurrencyRedefinedOnSubTypeOfEntitySetType = new EdmModelValidationRule<EntityContainer>(delegate(EdmModelValidationContext context, EntityContainer edmEntityContainer)
		{
			Dictionary<EntityType, EntitySet> dictionary = new Dictionary<EntityType, EntitySet>();
			foreach (EntitySet entitySet in edmEntityContainer.EntitySets)
			{
				if (entitySet != null && entitySet.ElementType != null && !dictionary.ContainsKey(entitySet.ElementType))
				{
					dictionary.Add(entitySet.ElementType, entitySet);
				}
			}
			foreach (EntityType entityType in context.Model.EntityTypes)
			{
				EntitySet entitySet2;
				if (EdmModelSemanticValidationRules.TypeIsSubTypeOf(entityType, dictionary, out entitySet2) && EdmModelSemanticValidationRules.IsTypeDefinesNewConcurrencyProperties(entityType))
				{
					context.AddError(entityType, null, Strings.EdmModel_Validator_Semantic_ConcurrencyRedefinedOnSubTypeOfEntitySetType(EdmModelSemanticValidationRules.GetQualifiedName(entityType, entityType.NamespaceName), EdmModelSemanticValidationRules.GetQualifiedName(entitySet2.ElementType, entitySet2.ElementType.NamespaceName), EdmModelSemanticValidationRules.GetQualifiedName(entitySet2, entitySet2.EntityContainer.Name)));
				}
			}
		});

		// Token: 0x04001435 RID: 5173
		internal static readonly EdmModelValidationRule<EntityContainer> EdmEntityContainer_DuplicateEntityContainerMemberName = new EdmModelValidationRule<EntityContainer>(delegate(EdmModelValidationContext context, EntityContainer edmEntityContainer)
		{
			HashSet<string> hashSet2 = new HashSet<string>();
			foreach (EntitySetBase entitySetBase in edmEntityContainer.BaseEntitySets)
			{
				EdmModelSemanticValidationRules.AddMemberNameToHashSet(entitySetBase, hashSet2, context, new Func<string, string>(Strings.EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName));
			}
		});

		// Token: 0x04001436 RID: 5174
		internal static readonly EdmModelValidationRule<EntityContainer> EdmEntityContainer_DuplicateEntitySetTable = new EdmModelValidationRule<EntityContainer>(delegate(EdmModelValidationContext context, EntityContainer edmEntityContainer)
		{
			HashSet<string> hashSet3 = new HashSet<string>();
			foreach (EntitySetBase entitySetBase2 in edmEntityContainer.BaseEntitySets)
			{
				if (!string.IsNullOrWhiteSpace(entitySetBase2.Table) && !hashSet3.Add(string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { entitySetBase2.Schema, entitySetBase2.Table })))
				{
					context.AddError(entitySetBase2, "Name", Strings.DuplicateEntitySetTable(entitySetBase2.Name, entitySetBase2.Schema, entitySetBase2.Table));
				}
			}
		});

		// Token: 0x04001437 RID: 5175
		internal static readonly EdmModelValidationRule<EntitySet> EdmEntitySet_EntitySetTypeHasNoKeys = new EdmModelValidationRule<EntitySet>(delegate(EdmModelValidationContext context, EntitySet edmEntitySet)
		{
			if (edmEntitySet.ElementType != null && !edmEntitySet.ElementType.GetValidKey().Any<EdmProperty>())
			{
				context.AddError(edmEntitySet, "EntityType", Strings.EdmModel_Validator_Semantic_EntitySetTypeHasNoKeys(edmEntitySet.Name, edmEntitySet.ElementType.Name));
			}
		});

		// Token: 0x04001438 RID: 5176
		internal static readonly EdmModelValidationRule<AssociationSet> EdmAssociationSet_DuplicateEndName = new EdmModelValidationRule<AssociationSet>(delegate(EdmModelValidationContext context, AssociationSet edmAssociationSet)
		{
			if (edmAssociationSet.ElementType != null && edmAssociationSet.ElementType.SourceEnd != null && edmAssociationSet.ElementType.TargetEnd != null && edmAssociationSet.ElementType.SourceEnd.Name == edmAssociationSet.ElementType.TargetEnd.Name)
			{
				context.AddError(edmAssociationSet.SourceSet, "Name", Strings.EdmModel_Validator_Semantic_DuplicateEndName(edmAssociationSet.ElementType.SourceEnd.Name));
			}
		});

		// Token: 0x04001439 RID: 5177
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_DuplicatePropertyNameSpecifiedInEntityKey = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			List<EdmProperty> list3 = edmEntityType.GetKeyProperties().ToList<EdmProperty>();
			if (list3.Count > 0)
			{
				List<EdmProperty> list4 = new List<EdmProperty>();
				using (List<EdmProperty>.Enumerator enumerator8 = list3.GetEnumerator())
				{
					while (enumerator8.MoveNext())
					{
						EdmProperty key = enumerator8.Current;
						if (key != null && !list4.Contains(key))
						{
							if (list3.Count((EdmProperty p) => key.Equals(p)) > 1)
							{
								context.AddError(key, null, Strings.EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey(edmEntityType.Name, key.Name));
							}
							list4.Add(key);
						}
					}
				}
			}
		});

		// Token: 0x0400143A RID: 5178
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_InvalidKeyNullablePart = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			foreach (EdmProperty edmProperty11 in edmEntityType.GetValidKey())
			{
				if (edmProperty11.IsPrimitiveType && edmProperty11.Nullable)
				{
					context.AddError(edmProperty11, "Nullable", Strings.EdmModel_Validator_Semantic_InvalidKeyNullablePart(edmProperty11.Name, edmEntityType.Name));
				}
			}
		});

		// Token: 0x0400143B RID: 5179
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_EntityKeyMustBeScalar = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			foreach (EdmProperty edmProperty2 in edmEntityType.GetValidKey())
			{
				if (!edmProperty2.IsUnderlyingPrimitiveType)
				{
					context.AddError(edmProperty2, null, Strings.EdmModel_Validator_Semantic_EntityKeyMustBeScalar(edmEntityType.Name, edmProperty2.Name));
				}
			}
		});

		// Token: 0x0400143C RID: 5180
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_InvalidKeyKeyDefinedInBaseClass = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			if (edmEntityType.BaseType != null && edmEntityType.KeyProperties.Where((EdmProperty key) => edmEntityType.DeclaredMembers.Contains(key)).Any<EdmProperty>())
			{
				context.AddError(edmEntityType.BaseType, null, Strings.EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass(edmEntityType.Name, edmEntityType.BaseType.Name));
			}
		});

		// Token: 0x0400143D RID: 5181
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_KeyMissingOnEntityType = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			if (edmEntityType.BaseType == null && edmEntityType.KeyProperties.Count == 0)
			{
				context.AddError(edmEntityType, null, Strings.EdmModel_Validator_Semantic_KeyMissingOnEntityType(edmEntityType.Name));
			}
		});

		// Token: 0x0400143E RID: 5182
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_InvalidMemberNameMatchesTypeName = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			List<EdmProperty> list5 = edmEntityType.Properties.ToList<EdmProperty>();
			if (!string.IsNullOrWhiteSpace(edmEntityType.Name) && list5.Count > 0)
			{
				foreach (EdmProperty edmProperty3 in list5)
				{
					if (edmProperty3 != null && context.IsCSpace && edmProperty3.Name.EqualsOrdinal(edmEntityType.Name))
					{
						context.AddError(edmProperty3, "Name", Strings.EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(edmProperty3.Name, EdmModelSemanticValidationRules.GetQualifiedName(edmEntityType, edmEntityType.NamespaceName)));
					}
				}
				if (edmEntityType.DeclaredNavigationProperties.Any<NavigationProperty>())
				{
					foreach (NavigationProperty navigationProperty in edmEntityType.DeclaredNavigationProperties)
					{
						if (navigationProperty != null && navigationProperty.Name.EqualsOrdinal(edmEntityType.Name))
						{
							context.AddError(navigationProperty, "Name", Strings.EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(navigationProperty.Name, EdmModelSemanticValidationRules.GetQualifiedName(edmEntityType, edmEntityType.NamespaceName)));
						}
					}
				}
			}
		});

		// Token: 0x0400143F RID: 5183
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_PropertyNameAlreadyDefinedDuplicate = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			HashSet<string> hashSet4 = new HashSet<string>();
			foreach (EdmProperty edmProperty4 in edmEntityType.Properties)
			{
				if (edmProperty4 != null && !string.IsNullOrWhiteSpace(edmProperty4.Name))
				{
					EdmModelSemanticValidationRules.AddMemberNameToHashSet(edmProperty4, hashSet4, context, new Func<string, string>(Strings.EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate));
				}
			}
			if (edmEntityType.DeclaredNavigationProperties.Any<NavigationProperty>())
			{
				foreach (NavigationProperty navigationProperty2 in edmEntityType.DeclaredNavigationProperties)
				{
					if (navigationProperty2 != null && !string.IsNullOrWhiteSpace(navigationProperty2.Name))
					{
						EdmModelSemanticValidationRules.AddMemberNameToHashSet(navigationProperty2, hashSet4, context, new Func<string, string>(Strings.EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate));
					}
				}
			}
		});

		// Token: 0x04001440 RID: 5184
		internal static readonly EdmModelValidationRule<EntityType> EdmEntityType_CycleInTypeHierarchy = new EdmModelValidationRule<EntityType>(delegate(EdmModelValidationContext context, EntityType edmEntityType)
		{
			if (EdmModelSemanticValidationRules.CheckForInheritanceCycle<EntityType>(edmEntityType, (EntityType et) => (EntityType)et.BaseType))
			{
				context.AddError(edmEntityType, "BaseType", Strings.EdmModel_Validator_Semantic_CycleInTypeHierarchy(EdmModelSemanticValidationRules.GetQualifiedName(edmEntityType, edmEntityType.NamespaceName)));
			}
		});

		// Token: 0x04001441 RID: 5185
		internal static readonly EdmModelValidationRule<NavigationProperty> EdmNavigationProperty_BadNavigationPropertyUndefinedRole = new EdmModelValidationRule<NavigationProperty>(delegate(EdmModelValidationContext context, NavigationProperty edmNavigationProperty)
		{
			if (edmNavigationProperty.Association != null && edmNavigationProperty.Association.SourceEnd != null && edmNavigationProperty.Association.TargetEnd != null && edmNavigationProperty.Association.SourceEnd.Name != null && edmNavigationProperty.Association.TargetEnd.Name != null && edmNavigationProperty.ToEndMember != edmNavigationProperty.Association.SourceEnd && edmNavigationProperty.ToEndMember != edmNavigationProperty.Association.TargetEnd)
			{
				context.AddError(edmNavigationProperty, null, Strings.EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole(edmNavigationProperty.Association.SourceEnd.Name, edmNavigationProperty.Association.TargetEnd.Name, edmNavigationProperty.Association.Name));
			}
		});

		// Token: 0x04001442 RID: 5186
		internal static readonly EdmModelValidationRule<NavigationProperty> EdmNavigationProperty_BadNavigationPropertyRolesCannotBeTheSame = new EdmModelValidationRule<NavigationProperty>(delegate(EdmModelValidationContext context, NavigationProperty edmNavigationProperty)
		{
			if (edmNavigationProperty.Association != null && edmNavigationProperty.Association.SourceEnd != null && edmNavigationProperty.Association.TargetEnd != null && edmNavigationProperty.ToEndMember == edmNavigationProperty.GetFromEnd())
			{
				context.AddError(edmNavigationProperty, "ToRole", Strings.EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame);
			}
		});

		// Token: 0x04001443 RID: 5187
		internal static readonly EdmModelValidationRule<NavigationProperty> EdmNavigationProperty_BadNavigationPropertyBadFromRoleType = new EdmModelValidationRule<NavigationProperty>(delegate(EdmModelValidationContext context, NavigationProperty edmNavigationProperty)
		{
			AssociationEndMember fromEnd;
			if (edmNavigationProperty.Association != null && (fromEnd = edmNavigationProperty.GetFromEnd()) != null)
			{
				EntityType entityType2 = null;
				IList<EntityType> list6 = (context.Model.EntityTypes as IList<EntityType>) ?? context.Model.EntityTypes.ToList<EntityType>();
				for (int i = 0; i < list6.Count; i++)
				{
					EntityType entityType3 = list6[i];
					if (entityType3.DeclaredNavigationProperties.Contains(edmNavigationProperty))
					{
						entityType2 = entityType3;
						break;
					}
				}
				EntityType entityType4 = fromEnd.GetEntityType();
				if (entityType2 != entityType4)
				{
					context.AddError(edmNavigationProperty, "FromRole", Strings.BadNavigationPropertyBadFromRoleType(edmNavigationProperty.Name, entityType4.Name, fromEnd.Name, edmNavigationProperty.Association.Name, entityType2.Name));
				}
			}
		});

		// Token: 0x04001444 RID: 5188
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_InvalidOperationMultipleEndsInAssociation = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (edmAssociationType.SourceEnd != null && edmAssociationType.SourceEnd.DeleteBehavior != OperationAction.None && edmAssociationType.TargetEnd != null && edmAssociationType.TargetEnd.DeleteBehavior != OperationAction.None)
			{
				context.AddError(edmAssociationType, null, Strings.EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation);
			}
		});

		// Token: 0x04001445 RID: 5189
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_EndWithManyMultiplicityCannotHaveOperationsSpecified = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (edmAssociationType.SourceEnd != null && edmAssociationType.SourceEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many && edmAssociationType.SourceEnd.DeleteBehavior != OperationAction.None)
			{
				context.AddError(edmAssociationType.SourceEnd, "OnDelete", Strings.EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(edmAssociationType.SourceEnd.Name, edmAssociationType.Name));
			}
			if (edmAssociationType.TargetEnd != null && edmAssociationType.TargetEnd.RelationshipMultiplicity == RelationshipMultiplicity.Many && edmAssociationType.TargetEnd.DeleteBehavior != OperationAction.None)
			{
				context.AddError(edmAssociationType.TargetEnd, "OnDelete", Strings.EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(edmAssociationType.TargetEnd.Name, edmAssociationType.Name));
			}
		});

		// Token: 0x04001446 RID: 5190
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_EndNameAlreadyDefinedDuplicate = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (edmAssociationType.SourceEnd != null && edmAssociationType.TargetEnd != null && edmAssociationType.SourceEnd.Name == edmAssociationType.TargetEnd.Name)
			{
				context.AddError(edmAssociationType.SourceEnd, "Name", Strings.EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate(edmAssociationType.SourceEnd.Name));
			}
		});

		// Token: 0x04001447 RID: 5191
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_SameRoleReferredInReferentialConstraint = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (EdmModelSemanticValidationRules.IsReferentialConstraintReadyForValidation(edmAssociationType) && edmAssociationType.Constraint.FromRole.Name == edmAssociationType.Constraint.ToRole.Name)
			{
				context.AddError(edmAssociationType.Constraint.ToRole, null, Strings.EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint(edmAssociationType.Name));
			}
		});

		// Token: 0x04001448 RID: 5192
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_ValidateReferentialConstraint = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (EdmModelSemanticValidationRules.IsReferentialConstraintReadyForValidation(edmAssociationType))
			{
				ReferentialConstraint constraint = edmAssociationType.Constraint;
				RelationshipEndMember fromRole = constraint.FromRole;
				RelationshipEndMember toRole = constraint.ToRole;
				bool flag;
				bool flag2;
				bool flag3;
				bool flag4;
				EdmModelSemanticValidationRules.IsKeyProperty(constraint.ToProperties.ToList<EdmProperty>(), toRole, out flag, out flag2, out flag3, out flag4);
				bool flag5;
				bool flag6;
				bool flag7;
				bool flag8;
				EdmModelSemanticValidationRules.IsKeyProperty(constraint.FromRole.GetEntityType().GetValidKey().ToList<EdmProperty>(), fromRole, out flag5, out flag6, out flag7, out flag8);
				bool flag9 = context.Model.SchemaVersion <= 1.1;
				if (fromRole.RelationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					context.AddError(fromRole, null, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleUpperBoundMustBeOne(fromRole.Name, edmAssociationType.Name));
				}
				else if (flag2 && fromRole.RelationshipMultiplicity == RelationshipMultiplicity.One)
				{
					string text = Strings.EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNullableV1(fromRole.Name, edmAssociationType.Name);
					context.AddError(edmAssociationType, null, text);
				}
				else if (((flag9 && !flag2) || (!flag9 && !flag3)) && fromRole.RelationshipMultiplicity != RelationshipMultiplicity.One)
				{
					string text2;
					if (flag9)
					{
						text2 = Strings.EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV1(fromRole.Name, edmAssociationType.Name);
					}
					else
					{
						text2 = Strings.EdmModel_Validator_Semantic_InvalidMultiplicityFromRoleToPropertyNonNullableV2(fromRole.Name, edmAssociationType.Name);
					}
					context.AddError(edmAssociationType, null, text2);
				}
				if (!flag4 && !edmAssociationType.IsForeignKey(context.Model.SchemaVersion) && context.IsCSpace)
				{
					context.AddError(toRole, null, Strings.EdmModel_Validator_Semantic_InvalidToPropertyInRelationshipConstraint(toRole.Name, EdmModelSemanticValidationRules.GetQualifiedName(toRole.GetEntityType(), toRole.GetEntityType().NamespaceName), EdmModelSemanticValidationRules.GetQualifiedName(edmAssociationType, edmAssociationType.NamespaceName)));
				}
				if (flag)
				{
					if (toRole.RelationshipMultiplicity == RelationshipMultiplicity.Many)
					{
						context.AddError(toRole, null, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeOne(toRole.Name, edmAssociationType.Name));
					}
				}
				else if (toRole.RelationshipMultiplicity != RelationshipMultiplicity.Many)
				{
					context.AddError(toRole, null, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityToRoleUpperBoundMustBeMany(toRole.Name, edmAssociationType.Name));
				}
				List<EdmProperty> list7 = fromRole.GetEntityType().GetValidKey().ToList<EdmProperty>();
				List<EdmProperty> list8 = constraint.ToProperties.ToList<EdmProperty>();
				if (list8.Count != list7.Count)
				{
					context.AddError(constraint, null, Strings.EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint);
					return;
				}
				List<EdmProperty> principalProperties = constraint.FromProperties.ToList<EdmProperty>();
				int count = list8.Count;
				int i2;
				int i;
				Func<EdmProperty, bool> <>9__45;
				for (i = 0; i < count; i = i2 + 1)
				{
					EdmProperty edmProperty5 = list8[i];
					IEnumerable<EdmProperty> enumerable = list7;
					Func<EdmProperty, bool> func;
					if ((func = <>9__45) == null)
					{
						func = (<>9__45 = (EdmProperty p) => p.Name == principalProperties[i].Name);
					}
					EdmProperty edmProperty6 = enumerable.SingleOrDefault(func);
					if (edmProperty6 != null && edmProperty5 != null && edmProperty6.TypeUsage != null && edmProperty5.TypeUsage != null && edmProperty6.IsPrimitiveType && edmProperty5.IsPrimitiveType && !EdmModelSemanticValidationRules.IsPrimitiveTypesEqual(edmProperty5, edmProperty6))
					{
						context.AddError(constraint, null, Strings.EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint(constraint.ToProperties.ToList<EdmProperty>()[i].Name, toRole.GetEntityType().Name, edmProperty6.Name, fromRole.GetEntityType().Name, edmAssociationType.Name));
					}
					i2 = i;
				}
			}
		});

		// Token: 0x04001449 RID: 5193
		internal static readonly EdmModelValidationRule<AssociationType> EdmAssociationType_InvalidPropertyInRelationshipConstraint = new EdmModelValidationRule<AssociationType>(delegate(EdmModelValidationContext context, AssociationType edmAssociationType)
		{
			if (edmAssociationType.Constraint != null && edmAssociationType.Constraint.ToRole != null && edmAssociationType.Constraint.ToRole.GetEntityType() != null)
			{
				List<EdmProperty> list9 = edmAssociationType.Constraint.ToRole.GetEntityType().Properties.ToList<EdmProperty>();
				foreach (EdmProperty edmProperty7 in edmAssociationType.Constraint.ToProperties)
				{
					if (edmProperty7 != null && !list9.Contains(edmProperty7))
					{
						context.AddError(edmProperty7, null, Strings.EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraint(edmProperty7.Name, edmAssociationType.Constraint.ToRole.Name));
					}
				}
			}
		});

		// Token: 0x0400144A RID: 5194
		internal static readonly EdmModelValidationRule<ComplexType> EdmComplexType_InvalidIsAbstract = new EdmModelValidationRule<ComplexType>(delegate(EdmModelValidationContext context, ComplexType edmComplexType)
		{
			if (edmComplexType.Abstract)
			{
				context.AddError(edmComplexType, "Abstract", Strings.EdmModel_Validator_Semantic_InvalidComplexTypeAbstract(EdmModelSemanticValidationRules.GetQualifiedName(edmComplexType, edmComplexType.NamespaceName)));
			}
		});

		// Token: 0x0400144B RID: 5195
		internal static readonly EdmModelValidationRule<ComplexType> EdmComplexType_InvalidIsPolymorphic = new EdmModelValidationRule<ComplexType>(delegate(EdmModelValidationContext context, ComplexType edmComplexType)
		{
			if (edmComplexType.BaseType != null)
			{
				context.AddError(edmComplexType, "BaseType", Strings.EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic(EdmModelSemanticValidationRules.GetQualifiedName(edmComplexType, edmComplexType.NamespaceName)));
			}
		});

		// Token: 0x0400144C RID: 5196
		internal static readonly EdmModelValidationRule<ComplexType> EdmComplexType_InvalidMemberNameMatchesTypeName = new EdmModelValidationRule<ComplexType>(delegate(EdmModelValidationContext context, ComplexType edmComplexType)
		{
			if (!string.IsNullOrWhiteSpace(edmComplexType.Name) && edmComplexType.Properties.Any<EdmProperty>())
			{
				foreach (EdmProperty edmProperty8 in edmComplexType.Properties)
				{
					if (edmProperty8 != null && edmProperty8.Name.EqualsOrdinal(edmComplexType.Name))
					{
						context.AddError(edmProperty8, "Name", Strings.EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(edmProperty8.Name, EdmModelSemanticValidationRules.GetQualifiedName(edmComplexType, edmComplexType.NamespaceName)));
					}
				}
			}
		});

		// Token: 0x0400144D RID: 5197
		internal static readonly EdmModelValidationRule<ComplexType> EdmComplexType_PropertyNameAlreadyDefinedDuplicate = new EdmModelValidationRule<ComplexType>(delegate(EdmModelValidationContext context, ComplexType edmComplexType)
		{
			if (edmComplexType.Properties.Any<EdmProperty>())
			{
				HashSet<string> hashSet5 = new HashSet<string>();
				foreach (EdmProperty edmProperty9 in edmComplexType.Properties)
				{
					if (!string.IsNullOrWhiteSpace(edmProperty9.Name))
					{
						EdmModelSemanticValidationRules.AddMemberNameToHashSet(edmProperty9, hashSet5, context, new Func<string, string>(Strings.EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate));
					}
				}
			}
		});

		// Token: 0x0400144E RID: 5198
		internal static readonly EdmModelValidationRule<ComplexType> EdmComplexType_PropertyNameAlreadyDefinedDuplicate_V1_1 = new EdmModelValidationRule<ComplexType>(delegate(EdmModelValidationContext context, ComplexType edmComplexType)
		{
			if (edmComplexType.Properties.Any<EdmProperty>())
			{
				HashSet<string> hashSet6 = new HashSet<string>();
				foreach (EdmProperty edmProperty10 in edmComplexType.Properties)
				{
					if (edmProperty10 != null && !string.IsNullOrWhiteSpace(edmProperty10.Name))
					{
						EdmModelSemanticValidationRules.AddMemberNameToHashSet(edmProperty10, hashSet6, context, new Func<string, string>(Strings.EdmModel_Validator_Semantic_PropertyNameAlreadyDefinedDuplicate));
					}
				}
			}
		});

		// Token: 0x0400144F RID: 5199
		internal static readonly EdmModelValidationRule<ComplexType> EdmComplexType_CycleInTypeHierarchy_V1_1 = new EdmModelValidationRule<ComplexType>(delegate(EdmModelValidationContext context, ComplexType edmComplexType)
		{
			if (EdmModelSemanticValidationRules.CheckForInheritanceCycle<ComplexType>(edmComplexType, (ComplexType ct) => (ComplexType)ct.BaseType))
			{
				context.AddError(edmComplexType, "BaseType", Strings.EdmModel_Validator_Semantic_CycleInTypeHierarchy(EdmModelSemanticValidationRules.GetQualifiedName(edmComplexType, edmComplexType.NamespaceName)));
			}
		});

		// Token: 0x04001450 RID: 5200
		internal static readonly EdmModelValidationRule<EdmProperty> EdmProperty_InvalidCollectionKind = new EdmModelValidationRule<EdmProperty>(delegate(EdmModelValidationContext context, EdmProperty edmProperty)
		{
			if (edmProperty.CollectionKind != CollectionKind.None)
			{
				context.AddError(edmProperty, "CollectionKind", Strings.EdmModel_Validator_Semantic_InvalidCollectionKindNotV1_1(edmProperty.Name));
			}
		});

		// Token: 0x04001451 RID: 5201
		internal static readonly EdmModelValidationRule<EdmProperty> EdmProperty_InvalidCollectionKind_V1_1 = new EdmModelValidationRule<EdmProperty>(delegate(EdmModelValidationContext context, EdmProperty edmProperty)
		{
			if (edmProperty.CollectionKind != CollectionKind.None && edmProperty.TypeUsage != null && !edmProperty.IsCollectionType)
			{
				context.AddError(edmProperty, "CollectionKind", Strings.EdmModel_Validator_Semantic_InvalidCollectionKindNotCollection(edmProperty.Name));
			}
		});

		// Token: 0x04001452 RID: 5202
		internal static readonly EdmModelValidationRule<EdmProperty> EdmProperty_NullableComplexType = new EdmModelValidationRule<EdmProperty>(delegate(EdmModelValidationContext context, EdmProperty edmProperty)
		{
			if (edmProperty.TypeUsage != null && edmProperty.ComplexType != null && edmProperty.Nullable)
			{
				context.AddError(edmProperty, "Nullable", Strings.EdmModel_Validator_Semantic_NullableComplexType(edmProperty.Name));
			}
		});

		// Token: 0x04001453 RID: 5203
		internal static readonly EdmModelValidationRule<EdmProperty> EdmProperty_InvalidPropertyType = new EdmModelValidationRule<EdmProperty>(delegate(EdmModelValidationContext context, EdmProperty edmProperty)
		{
			if (edmProperty.TypeUsage.EdmType != null && !edmProperty.IsPrimitiveType && !edmProperty.IsComplexType)
			{
				context.AddError(edmProperty, "Type", Strings.EdmModel_Validator_Semantic_InvalidPropertyType(edmProperty.IsCollectionType ? "CollectionType" : edmProperty.TypeUsage.EdmType.BuiltInTypeKind.ToString()));
			}
		});

		// Token: 0x04001454 RID: 5204
		internal static readonly EdmModelValidationRule<EdmProperty> EdmProperty_InvalidPropertyType_V1_1 = new EdmModelValidationRule<EdmProperty>(delegate(EdmModelValidationContext context, EdmProperty edmProperty)
		{
			if (edmProperty.TypeUsage != null && edmProperty.TypeUsage.EdmType != null && !edmProperty.IsPrimitiveType && !edmProperty.IsComplexType && !edmProperty.IsCollectionType)
			{
				context.AddError(edmProperty, "Type", Strings.EdmModel_Validator_Semantic_InvalidPropertyType_V1_1(edmProperty.TypeUsage.EdmType.BuiltInTypeKind.ToString()));
			}
		});

		// Token: 0x04001455 RID: 5205
		internal static readonly EdmModelValidationRule<EdmProperty> EdmProperty_InvalidPropertyType_V3 = new EdmModelValidationRule<EdmProperty>(delegate(EdmModelValidationContext context, EdmProperty edmProperty)
		{
			if (edmProperty.TypeUsage != null && edmProperty.TypeUsage.EdmType != null && !edmProperty.IsPrimitiveType && !edmProperty.IsComplexType && !edmProperty.IsEnumType)
			{
				context.AddError(edmProperty, "Type", Strings.EdmModel_Validator_Semantic_InvalidPropertyType_V3(edmProperty.TypeUsage.EdmType.BuiltInTypeKind.ToString()));
			}
		});

		// Token: 0x04001456 RID: 5206
		internal static readonly EdmModelValidationRule<EdmModel> EdmNamespace_TypeNameAlreadyDefinedDuplicate = new EdmModelValidationRule<EdmModel>(delegate(EdmModelValidationContext context, EdmModel model)
		{
			HashSet<string> hashSet7 = new HashSet<string>();
			foreach (EdmType edmType in model.NamespaceItems)
			{
				EdmModelSemanticValidationRules.AddMemberNameToHashSet(edmType, hashSet7, context, new Func<string, string>(Strings.EdmModel_Validator_Semantic_TypeNameAlreadyDefinedDuplicate));
			}
		});
	}
}
