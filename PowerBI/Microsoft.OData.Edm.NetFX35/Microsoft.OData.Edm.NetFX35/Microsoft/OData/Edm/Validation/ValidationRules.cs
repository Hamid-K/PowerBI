using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation.Internal;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000272 RID: 626
	public static class ValidationRules
	{
		// Token: 0x06000DB5 RID: 3509 RVA: 0x00027354 File Offset: 0x00025554
		private static void CheckForUnreacheableTypeError(ValidationContext context, IEdmSchemaType type, EdmLocation location)
		{
			IEdmType edmType = context.Model.FindType(type.FullName());
			if (edmType is AmbiguousTypeBinding)
			{
				context.AddError(location, EdmErrorCode.BadAmbiguousElementBinding, Strings.EdmModel_Validator_Semantic_AmbiguousType(type.FullName()));
				return;
			}
			if (!edmType.IsEquivalentTo(type))
			{
				context.AddError(location, EdmErrorCode.BadUnresolvedType, Strings.EdmModel_Validator_Semantic_InaccessibleType(type.FullName()));
			}
		}

		// Token: 0x040005DF RID: 1503
		public static readonly ValidationRule<IEdmElement> ElementDirectValueAnnotationFullNameMustBeUnique = new ValidationRule<IEdmElement>(delegate(ValidationContext context, IEdmElement item)
		{
			HashSetInternal<string> hashSetInternal = new HashSetInternal<string>();
			foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in context.Model.DirectValueAnnotationsManager.GetDirectValueAnnotations(item))
			{
				if (!hashSetInternal.Add(edmDirectValueAnnotation.NamespaceUri + ":" + edmDirectValueAnnotation.Name))
				{
					context.AddError(edmDirectValueAnnotation.Location(), EdmErrorCode.DuplicateDirectValueAnnotationFullName, Strings.EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique(edmDirectValueAnnotation.NamespaceUri, edmDirectValueAnnotation.Name));
				}
			}
		});

		// Token: 0x040005E0 RID: 1504
		public static readonly ValidationRule<IEdmNamedElement> NamedElementNameMustNotBeEmptyOrWhiteSpace = new ValidationRule<IEdmNamedElement>(delegate(ValidationContext context, IEdmNamedElement item)
		{
			if (EdmUtil.IsNullOrWhiteSpaceInternal(item.Name) || item.Name.Length == 0)
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidName, Strings.EdmModel_Validator_Syntactic_MissingName);
			}
		});

		// Token: 0x040005E1 RID: 1505
		public static readonly ValidationRule<IEdmNamedElement> NamedElementNameIsTooLong = new ValidationRule<IEdmNamedElement>(delegate(ValidationContext context, IEdmNamedElement item)
		{
			if (!EdmUtil.IsNullOrWhiteSpaceInternal(item.Name) && item.Name.Length > 480)
			{
				context.AddError(item.Location(), EdmErrorCode.NameTooLong, Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong(item.Name));
			}
		});

		// Token: 0x040005E2 RID: 1506
		public static readonly ValidationRule<IEdmNamedElement> NamedElementNameIsNotAllowed = new ValidationRule<IEdmNamedElement>(delegate(ValidationContext context, IEdmNamedElement item)
		{
			if (item is IEdmDirectValueAnnotation)
			{
				return;
			}
			if (!EdmUtil.IsNullOrWhiteSpaceInternal(item.Name) && item.Name.Length <= 480 && item.Name.Length > 0 && !EdmUtil.IsValidUndottedName(item.Name))
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidName, Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed(item.Name));
			}
		});

		// Token: 0x040005E3 RID: 1507
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementNamespaceMustNotBeEmptyOrWhiteSpace = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement item)
		{
			if (EdmUtil.IsNullOrWhiteSpaceInternal(item.Namespace) || item.Namespace.Length == 0)
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidNamespaceName, Strings.EdmModel_Validator_Syntactic_MissingNamespaceName);
			}
		});

		// Token: 0x040005E4 RID: 1508
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementNamespaceIsTooLong = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement item)
		{
			if (item.Namespace.Length > 512)
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidNamespaceName, Strings.EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong(item.Namespace));
			}
		});

		// Token: 0x040005E5 RID: 1509
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementNamespaceIsNotAllowed = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement item)
		{
			if (item.Namespace.Length <= 512 && item.Namespace.Length > 0 && !EdmUtil.IsNullOrWhiteSpaceInternal(item.Namespace) && !EdmUtil.IsValidDottedName(item.Namespace))
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidNamespaceName, Strings.EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed(item.Namespace));
			}
		});

		// Token: 0x040005E6 RID: 1510
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementSystemNamespaceEncountered = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement element)
		{
			if (ValidationHelper.IsEdmSystemNamespace(element.Namespace))
			{
				context.AddError(element.Location(), EdmErrorCode.SystemNamespaceEncountered, Strings.EdmModel_Validator_Semantic_SystemNamespaceEncountered(element.Namespace));
			}
		});

		// Token: 0x040005E7 RID: 1511
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementMustNotHaveKindOfNone = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement element)
		{
			if (element.SchemaElementKind == EdmSchemaElementKind.None && !context.IsBad(element))
			{
				context.AddError(element.Location(), EdmErrorCode.SchemaElementMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone(element.FullName()));
			}
		});

		// Token: 0x040005E8 RID: 1512
		public static readonly ValidationRule<IEdmEntityContainerElement> EntityContainerElementMustNotHaveKindOfNone = new ValidationRule<IEdmEntityContainerElement>(delegate(ValidationContext context, IEdmEntityContainerElement element)
		{
			if (element.ContainerElementKind == EdmContainerElementKind.None && !context.IsBad(element))
			{
				context.AddError(element.Location(), EdmErrorCode.EntityContainerElementMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone(element.Container.FullName() + '/' + element.Name));
			}
		});

		// Token: 0x040005E9 RID: 1513
		public static readonly ValidationRule<IEdmEntityContainer> EntityContainerDuplicateEntityContainerMemberName = new ValidationRule<IEdmEntityContainer>(delegate(ValidationContext context, IEdmEntityContainer entityContainer)
		{
			HashSetInternal<string> hashSetInternal2 = new HashSetInternal<string>();
			HashSetInternal<string> hashSetInternal3 = new HashSetInternal<string>();
			HashSetInternal<string> hashSetInternal4 = new HashSetInternal<string>();
			foreach (IEdmEntityContainerElement edmEntityContainerElement in entityContainer.Elements)
			{
				bool flag = false;
				IEdmOperationImport edmOperationImport = edmEntityContainerElement as IEdmOperationImport;
				if (edmOperationImport != null)
				{
					if (!hashSetInternal4.Contains(edmOperationImport.Name))
					{
						hashSetInternal4.Add(edmOperationImport.Name);
					}
					string text = edmOperationImport.Name + "_" + edmOperationImport.Operation.GetHashCode();
					if (hashSetInternal3.Contains(text))
					{
						flag = true;
					}
					else
					{
						hashSetInternal3.Add(text);
					}
					if (hashSetInternal2.Contains(edmOperationImport.Name))
					{
						flag = true;
					}
				}
				else
				{
					if (hashSetInternal2.Contains(edmEntityContainerElement.Name))
					{
						flag = true;
					}
					else
					{
						hashSetInternal2.Add(edmEntityContainerElement.Name);
					}
					if (hashSetInternal4.Contains(edmEntityContainerElement.Name))
					{
						flag = true;
					}
				}
				if (flag)
				{
					context.AddError(edmEntityContainerElement.Location(), EdmErrorCode.DuplicateEntityContainerMemberName, Strings.EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName(edmEntityContainerElement.Name));
				}
			}
		});

		// Token: 0x040005EA RID: 1514
		public static readonly ValidationRule<IEdmNavigationSource> NavigationSourceTypeHasNoKeys = new ValidationRule<IEdmNavigationSource>(delegate(ValidationContext context, IEdmNavigationSource navigationSource)
		{
			if (navigationSource.EntityType() == null)
			{
				return;
			}
			if ((navigationSource.EntityType().Key() == null || !Enumerable.Any<IEdmStructuralProperty>(navigationSource.EntityType().Key())) && !context.IsBad(navigationSource.EntityType()))
			{
				string text2 = Strings.EdmModel_Validator_Semantic_NavigationSourceTypeHasNoKeys(navigationSource.Name, navigationSource.EntityType().Name);
				context.AddError(navigationSource.Location(), EdmErrorCode.NavigationSourceTypeHasNoKeys, text2);
			}
		});

		// Token: 0x040005EB RID: 1515
		public static readonly ValidationRule<IEdmNavigationSource> NavigationSourceInaccessibleEntityType = new ValidationRule<IEdmNavigationSource>(delegate(ValidationContext context, IEdmNavigationSource navigationSource)
		{
			IEdmEntityType edmEntityType = navigationSource.EntityType();
			if (edmEntityType != null && !context.IsBad(edmEntityType))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, edmEntityType, navigationSource.Location());
			}
		});

		// Token: 0x040005EC RID: 1516
		public static readonly ValidationRule<IEdmNavigationSource> NavigationPropertyMappingsMustBeUnique = new ValidationRule<IEdmNavigationSource>(delegate(ValidationContext context, IEdmNavigationSource navigationSource)
		{
			HashSetInternal<IEdmNavigationProperty> hashSetInternal5 = new HashSetInternal<IEdmNavigationProperty>();
			foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding in navigationSource.NavigationPropertyBindings)
			{
				if (!hashSetInternal5.Add(edmNavigationPropertyBinding.NavigationProperty))
				{
					context.AddError(navigationSource.Location(), EdmErrorCode.DuplicateNavigationPropertyMapping, Strings.EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping(navigationSource.Name, edmNavigationPropertyBinding.NavigationProperty.Name));
				}
			}
		});

		// Token: 0x040005ED RID: 1517
		public static readonly ValidationRule<IEdmNavigationSource> NavigationPropertyMappingMustPointToValidTargetForProperty = new ValidationRule<IEdmNavigationSource>(delegate(ValidationContext context, IEdmNavigationSource navigationSource)
		{
			foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding2 in navigationSource.NavigationPropertyBindings)
			{
				if (!edmNavigationPropertyBinding2.NavigationProperty.IsBad() && !edmNavigationPropertyBinding2.Target.IsBad())
				{
					if (!edmNavigationPropertyBinding2.Target.EntityType().IsOrInheritsFrom(edmNavigationPropertyBinding2.NavigationProperty.ToEntityType()) && !edmNavigationPropertyBinding2.NavigationProperty.ToEntityType().IsOrInheritsFrom(edmNavigationPropertyBinding2.Target.EntityType()) && !context.IsBad(edmNavigationPropertyBinding2.Target))
					{
						context.AddError(navigationSource.Location(), EdmErrorCode.NavigationPropertyMappingMustPointToValidTargetForProperty, Strings.EdmModel_Validator_Semantic_NavigationPropertyMappingMustPointToValidTargetForProperty(edmNavigationPropertyBinding2.NavigationProperty.Name, edmNavigationPropertyBinding2.Target.Name));
					}
					if (edmNavigationPropertyBinding2.Target is IEdmSingleton && edmNavigationPropertyBinding2.NavigationProperty.Type.Definition.TypeKind == EdmTypeKind.Collection)
					{
						context.AddError(navigationSource.Location(), EdmErrorCode.NavigationPropertyOfCollectionTypeMustNotTargetToSingleton, Strings.EdmModel_Validator_Semantic_NavigationPropertyOfCollectionTypeMustNotTargetToSingleton(edmNavigationPropertyBinding2.NavigationProperty.Name, edmNavigationPropertyBinding2.Target.Name));
					}
				}
			}
		});

		// Token: 0x040005EE RID: 1518
		public static readonly ValidationRule<IEdmEntitySet> EntitySetCanOnlyBeContainedByASingleNavigationProperty = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet set)
		{
			bool flag2 = false;
			foreach (IEdmNavigationSource edmNavigationSource in Enumerable.OfType<IEdmNavigationSource>(set.Container.Elements))
			{
				foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding3 in edmNavigationSource.NavigationPropertyBindings)
				{
					IEdmNavigationProperty navigationProperty3 = edmNavigationPropertyBinding3.NavigationProperty;
					if (edmNavigationPropertyBinding3.Target == set && navigationProperty3.ContainsTarget)
					{
						if (flag2)
						{
							context.AddError(set.Location(), EdmErrorCode.EntitySetCanOnlyBeContainedByASingleNavigationProperty, Strings.EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty(set.Container.FullName() + "." + set.Name));
						}
						flag2 = true;
					}
				}
			}
		});

		// Token: 0x040005EF RID: 1519
		public static readonly ValidationRule<IEdmNavigationSource> NavigationMappingMustBeBidirectional = new ValidationRule<IEdmNavigationSource>(delegate(ValidationContext context, IEdmNavigationSource navigationSource)
		{
			foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding4 in navigationSource.NavigationPropertyBindings)
			{
				IEdmNavigationProperty navigationProperty2 = edmNavigationPropertyBinding4.NavigationProperty;
				if (navigationProperty2.Partner != null && !navigationProperty2.IsBad())
				{
					IEdmNavigationSource edmNavigationSource2 = edmNavigationPropertyBinding4.Target.FindNavigationTarget(navigationProperty2.Partner);
					if (edmNavigationSource2 != null && !(edmNavigationSource2 is IEdmUnknownEntitySet) && !(edmNavigationSource2 is IEdmContainedEntitySet) && edmNavigationSource2 != navigationSource && navigationProperty2.Partner.DeclaringEntityType().FindProperty(navigationProperty2.Partner.Name) == navigationProperty2.Partner)
					{
						context.AddError(navigationSource.Location(), EdmErrorCode.NavigationMappingMustBeBidirectional, Strings.EdmModel_Validator_Semantic_NavigationMappingMustBeBidirectional(navigationSource.Name, navigationProperty2.Name));
					}
				}
			}
		});

		// Token: 0x040005F0 RID: 1520
		public static readonly ValidationRule<IEdmEntitySet> EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet set)
		{
			foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding5 in set.NavigationPropertyBindings)
			{
				if (edmNavigationPropertyBinding5.NavigationProperty.ContainsTarget && edmNavigationPropertyBinding5.NavigationProperty.DeclaringType.IsOrInheritsFrom(edmNavigationPropertyBinding5.NavigationProperty.ToEntityType()) && edmNavigationPropertyBinding5.Target != set)
				{
					context.AddError(set.Location(), EdmErrorCode.EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet, Strings.EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet(edmNavigationPropertyBinding5.NavigationProperty, set.Name));
				}
			}
		});

		// Token: 0x040005F1 RID: 1521
		public static readonly ValidationRule<IEdmEntitySet> EntitySetTypeMustBeCollectionOfEntityType = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet entitySet)
		{
			bool flag3 = false;
			IEdmCollectionType edmCollectionType = entitySet.Type as IEdmCollectionType;
			if (edmCollectionType != null)
			{
				flag3 = edmCollectionType.ElementType != null && edmCollectionType.ElementType.Definition is IEdmEntityType;
			}
			if (!flag3)
			{
				string text3 = Strings.EdmModel_Validator_Semantic_EntitySetTypeMustBeCollectionOfEntityType(entitySet.Type.FullTypeName(), entitySet.Name);
				context.AddError(entitySet.Location(), EdmErrorCode.EntitySetTypeMustBeCollectionOfEntityType, text3);
			}
		});

		// Token: 0x040005F2 RID: 1522
		public static readonly ValidationRule<IEdmSingleton> SingletonTypeMustBeEntityType = new ValidationRule<IEdmSingleton>(delegate(ValidationContext context, IEdmSingleton singleton)
		{
			if (!(singleton.Type is IEdmEntityType))
			{
				string text4 = Strings.EdmModel_Validator_Semantic_SingletonTypeMustBeEntityType(singleton.Type.FullTypeName(), singleton.Name);
				context.AddError(singleton.Location(), EdmErrorCode.SingletonTypeMustBeEntityType, text4);
			}
		});

		// Token: 0x040005F3 RID: 1523
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypeInvalidMemberNameMatchesTypeName = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			IEdmSchemaType edmSchemaType = structuredType as IEdmSchemaType;
			if (edmSchemaType != null)
			{
				List<IEdmProperty> list = Enumerable.ToList<IEdmProperty>(structuredType.Properties());
				if (list.Count > 0)
				{
					foreach (IEdmProperty edmProperty in list)
					{
						if (edmProperty != null && edmProperty.Name.EqualsOrdinal(edmSchemaType.Name))
						{
							context.AddError(edmProperty.Location(), EdmErrorCode.BadProperty, Strings.EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(edmProperty.Name));
						}
					}
				}
			}
		});

		// Token: 0x040005F4 RID: 1524
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypePropertyNameAlreadyDefined = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			HashSetInternal<string> hashSetInternal6 = new HashSetInternal<string>();
			foreach (IEdmProperty edmProperty2 in structuredType.Properties())
			{
				if (edmProperty2 != null)
				{
					ValidationHelper.AddMemberNameToHashSet(edmProperty2, hashSetInternal6, context, EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_PropertyNameAlreadyDefined(edmProperty2.Name), !Enumerable.Contains<IEdmProperty>(structuredType.DeclaredProperties, edmProperty2));
				}
			}
		});

		// Token: 0x040005F5 RID: 1525
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypeBaseTypeMustBeSameKindAsDerivedKind = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			if (structuredType is IEdmSchemaType && structuredType.BaseType != null && structuredType.BaseType.TypeKind != structuredType.TypeKind)
			{
				context.AddError(structuredType.Location(), (structuredType.TypeKind == EdmTypeKind.Entity) ? EdmErrorCode.EntityMustHaveEntityBaseType : EdmErrorCode.ComplexTypeMustHaveComplexBaseType, Strings.EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind);
			}
		});

		// Token: 0x040005F6 RID: 1526
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypeInaccessibleBaseType = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			IEdmSchemaType edmSchemaType2 = structuredType.BaseType as IEdmSchemaType;
			if (edmSchemaType2 != null && !context.IsBad(edmSchemaType2))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, edmSchemaType2, structuredType.Location());
			}
		});

		// Token: 0x040005F7 RID: 1527
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypePropertiesDeclaringTypeMustBeCorrect = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			foreach (IEdmProperty edmProperty3 in structuredType.DeclaredProperties)
			{
				if (edmProperty3 != null && !edmProperty3.DeclaringType.Equals(structuredType))
				{
					context.AddError(edmProperty3.Location(), EdmErrorCode.DeclaringTypeMustBeCorrect, Strings.EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect(edmProperty3.Name));
				}
			}
		});

		// Token: 0x040005F8 RID: 1528
		public static readonly ValidationRule<IEdmStructuredType> OnlyEntityTypesCanBeOpen = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			if (structuredType.IsOpen && structuredType.TypeKind != EdmTypeKind.Entity)
			{
				context.AddError(structuredType.Location(), EdmErrorCode.OpenTypeNotSupported, Strings.EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly);
			}
		});

		// Token: 0x040005F9 RID: 1529
		public static readonly ValidationRule<IEdmEnumType> EnumTypeEnumMemberNameAlreadyDefined = new ValidationRule<IEdmEnumType>(delegate(ValidationContext context, IEdmEnumType enumType)
		{
			HashSetInternal<string> hashSetInternal7 = new HashSetInternal<string>();
			foreach (IEdmEnumMember edmEnumMember in enumType.Members)
			{
				if (edmEnumMember != null)
				{
					ValidationHelper.AddMemberNameToHashSet(edmEnumMember, hashSetInternal7, context, EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined(edmEnumMember.Name), false);
				}
			}
		});

		// Token: 0x040005FA RID: 1530
		public static readonly ValidationRule<IEdmEnumType> EnumMustHaveIntegerUnderlyingType = new ValidationRule<IEdmEnumType>(delegate(ValidationContext context, IEdmEnumType enumType)
		{
			if (!enumType.UnderlyingType.PrimitiveKind.IsIntegral() && !context.IsBad(enumType.UnderlyingType))
			{
				context.AddError(enumType.Location(), EdmErrorCode.EnumMustHaveIntegerUnderlyingType, Strings.EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType(enumType.FullName()));
			}
		});

		// Token: 0x040005FB RID: 1531
		public static readonly ValidationRule<IEdmEnumMember> EnumMemberValueMustHaveSameTypeAsUnderlyingType = new ValidationRule<IEdmEnumMember>(delegate(ValidationContext context, IEdmEnumMember enumMember)
		{
			IEnumerable<EdmError> enumerable;
			if (!context.IsBad(enumMember.DeclaringType) && !context.IsBad(enumMember.DeclaringType.UnderlyingType) && !enumMember.Value.TryCastPrimitiveAsType(enumMember.DeclaringType.UnderlyingType.GetPrimitiveTypeReference(false), out enumerable))
			{
				context.AddError(enumMember.Location(), EdmErrorCode.EnumMemberTypeMustMatchEnumUnderlyingType, Strings.EdmModel_Validator_Semantic_EnumMemberTypeMustMatchEnumUnderlyingType(enumMember.Name));
			}
		});

		// Token: 0x040005FC RID: 1532
		public static readonly ValidationRule<IEdmEntityType> EntityTypeDuplicatePropertyNameSpecifiedInEntityKey = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.DeclaredKey != null)
			{
				HashSetInternal<string> hashSetInternal8 = new HashSetInternal<string>();
				foreach (IEdmStructuralProperty edmStructuralProperty in entityType.DeclaredKey)
				{
					ValidationHelper.AddMemberNameToHashSet(edmStructuralProperty, hashSetInternal8, context, EdmErrorCode.DuplicatePropertySpecifiedInEntityKey, Strings.EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey(entityType.Name, edmStructuralProperty.Name), false);
				}
			}
		});

		// Token: 0x040005FD RID: 1533
		public static readonly ValidationRule<IEdmEntityType> EntityTypeInvalidKeyNullablePart = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.Key() != null)
			{
				foreach (IEdmStructuralProperty edmStructuralProperty2 in entityType.Key())
				{
					if (edmStructuralProperty2.Type.IsPrimitive() && edmStructuralProperty2.Type.IsNullable)
					{
						context.AddError(edmStructuralProperty2.Location(), EdmErrorCode.InvalidKey, Strings.EdmModel_Validator_Semantic_InvalidKeyNullablePart(edmStructuralProperty2.Name, entityType.Name));
					}
				}
			}
		});

		// Token: 0x040005FE RID: 1534
		public static readonly ValidationRule<IEdmEntityType> EntityTypeEntityKeyMustBeScalar = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.Key() != null)
			{
				foreach (IEdmStructuralProperty edmStructuralProperty3 in entityType.Key())
				{
					if (!edmStructuralProperty3.Type.IsPrimitive() && !edmStructuralProperty3.Type.IsEnum() && !context.IsBad(edmStructuralProperty3))
					{
						context.AddError(edmStructuralProperty3.Location(), EdmErrorCode.EntityKeyMustBeScalar, Strings.EdmModel_Validator_Semantic_EntityKeyMustBeScalar(edmStructuralProperty3.Name, entityType.Name));
					}
				}
			}
		});

		// Token: 0x040005FF RID: 1535
		public static readonly ValidationRule<IEdmEntityType> EntityTypeInvalidKeyKeyDefinedInBaseClass = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.BaseType != null && entityType.DeclaredKey != null && entityType.BaseType.TypeKind == EdmTypeKind.Entity && entityType.BaseEntityType().DeclaredKey != null)
			{
				context.AddError(entityType.Location(), EdmErrorCode.InvalidKey, Strings.EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass(entityType.Name, entityType.BaseEntityType().Name));
			}
		});

		// Token: 0x04000600 RID: 1536
		public static readonly ValidationRule<IEdmEntityType> EntityTypeKeyMissingOnEntityType = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			IEnumerable<IEdmStructuralProperty> enumerable2 = entityType.Key();
			if ((enumerable2 == null || !Enumerable.Any<IEdmStructuralProperty>(enumerable2)) && entityType.BaseType == null && !entityType.IsAbstract)
			{
				context.AddError(entityType.Location(), EdmErrorCode.KeyMissingOnEntityType, Strings.EdmModel_Validator_Semantic_KeyMissingOnEntityType(entityType.Name));
			}
		});

		// Token: 0x04000601 RID: 1537
		public static readonly ValidationRule<IEdmEntityType> EntityTypeKeyPropertyMustBelongToEntity = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.DeclaredKey != null)
			{
				foreach (IEdmStructuralProperty edmStructuralProperty4 in entityType.DeclaredKey)
				{
					if (edmStructuralProperty4.DeclaringType != entityType && !context.IsBad(edmStructuralProperty4))
					{
						context.AddError(entityType.Location(), EdmErrorCode.KeyPropertyMustBelongToEntity, Strings.EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity(edmStructuralProperty4.Name, entityType.Name));
					}
				}
			}
		});

		// Token: 0x04000602 RID: 1538
		public static readonly ValidationRule<IEdmEntityReferenceType> EntityReferenceTypeInaccessibleEntityType = new ValidationRule<IEdmEntityReferenceType>(delegate(ValidationContext context, IEdmEntityReferenceType entityReferenceType)
		{
			if (!context.IsBad(entityReferenceType.EntityType))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, entityReferenceType.EntityType, entityReferenceType.Location());
			}
		});

		// Token: 0x04000603 RID: 1539
		public static readonly ValidationRule<IEdmType> TypeMustNotHaveKindOfNone = new ValidationRule<IEdmType>(delegate(ValidationContext context, IEdmType type)
		{
			if (type.TypeKind == EdmTypeKind.None && !context.IsBad(type))
			{
				context.AddError(type.Location(), EdmErrorCode.TypeMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone);
			}
		});

		// Token: 0x04000604 RID: 1540
		public static readonly ValidationRule<IEdmPrimitiveType> PrimitiveTypeMustNotHaveKindOfNone = new ValidationRule<IEdmPrimitiveType>(delegate(ValidationContext context, IEdmPrimitiveType type)
		{
			if (type.PrimitiveKind == EdmPrimitiveTypeKind.None && !context.IsBad(type))
			{
				context.AddError(type.Location(), EdmErrorCode.PrimitiveTypeMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone(type.FullName()));
			}
		});

		// Token: 0x04000605 RID: 1541
		public static readonly ValidationRule<IEdmComplexType> ComplexTypeInvalidAbstractComplexType = new ValidationRule<IEdmComplexType>(delegate(ValidationContext context, IEdmComplexType complexType)
		{
			if (complexType.IsAbstract)
			{
				context.AddError(complexType.Location(), EdmErrorCode.InvalidAbstractComplexType, Strings.EdmModel_Validator_Semantic_InvalidComplexTypeAbstract(complexType.FullName()));
			}
		});

		// Token: 0x04000606 RID: 1542
		public static readonly ValidationRule<IEdmComplexType> OpenComplexTypeCannotHaveClosedDerivedComplexType = new ValidationRule<IEdmComplexType>(delegate(ValidationContext context, IEdmComplexType complexType)
		{
			if (complexType.BaseType != null && complexType.BaseType.IsOpen && !complexType.IsOpen)
			{
				context.AddError(complexType.Location(), EdmErrorCode.InvalidAbstractComplexType, Strings.EdmModel_Validator_Semantic_BaseTypeOfOpenTypeMustBeOpen(complexType.FullName()));
			}
		});

		// Token: 0x04000607 RID: 1543
		public static readonly ValidationRule<IEdmComplexType> ComplexTypeInvalidPolymorphicComplexType = new ValidationRule<IEdmComplexType>(delegate(ValidationContext context, IEdmComplexType edmComplexType)
		{
			if (edmComplexType.BaseType != null)
			{
				context.AddError(edmComplexType.Location(), EdmErrorCode.InvalidPolymorphicComplexType, Strings.EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic(edmComplexType.FullName()));
			}
		});

		// Token: 0x04000608 RID: 1544
		public static readonly ValidationRule<IEdmComplexType> ComplexTypeMustContainProperties = new ValidationRule<IEdmComplexType>(delegate(ValidationContext context, IEdmComplexType complexType)
		{
			if (!Enumerable.Any<IEdmProperty>(complexType.Properties()))
			{
				context.AddError(complexType.Location(), EdmErrorCode.ComplexTypeMustHaveProperties, Strings.EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties(complexType.FullName()));
			}
		});

		// Token: 0x04000609 RID: 1545
		public static readonly ValidationRule<IEdmStructuralProperty> StructuralPropertyInvalidPropertyType = new ValidationRule<IEdmStructuralProperty>(delegate(ValidationContext context, IEdmStructuralProperty property)
		{
			IEdmType edmType;
			if (property.Type.IsCollection())
			{
				edmType = property.Type.AsCollection().ElementType().Definition;
			}
			else
			{
				edmType = property.Type.Definition;
			}
			if (edmType.TypeKind != EdmTypeKind.Primitive && edmType.TypeKind != EdmTypeKind.Enum && edmType.TypeKind != EdmTypeKind.Complex && !context.IsBad(edmType))
			{
				context.AddError(property.Location(), EdmErrorCode.InvalidPropertyType, Strings.EdmModel_Validator_Semantic_InvalidPropertyType(property.Type.TypeKind().ToString()));
			}
		});

		// Token: 0x0400060A RID: 1546
		public static readonly ValidationRule<IEdmStructuralProperty> StructuralPropertyInvalidPropertyTypeConcurrencyMode = new ValidationRule<IEdmStructuralProperty>(delegate(ValidationContext context, IEdmStructuralProperty property)
		{
			if (property.ConcurrencyMode == EdmConcurrencyMode.Fixed && !property.Type.IsPrimitive() && !context.IsBad(property.Type.Definition))
			{
				context.AddError(property.Location(), EdmErrorCode.InvalidPropertyType, Strings.EdmModel_Validator_Semantic_InvalidPropertyTypeConcurrencyMode(property.Type.IsCollection() ? "Collection" : property.Type.TypeKind().ToString()));
			}
		});

		// Token: 0x0400060B RID: 1547
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyInvalidOperationMultipleEndsInAssociatedNavigationProperties = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			if (navigationProperty.OnDelete != EdmOnDeleteAction.None && navigationProperty.Partner != null && navigationProperty.Partner.OnDelete != EdmOnDeleteAction.None)
			{
				context.AddError(navigationProperty.Location(), EdmErrorCode.InvalidAction, Strings.EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation);
			}
		});

		// Token: 0x0400060C RID: 1548
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyCorrectType = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.ToEntityType() == null)
			{
				context.AddError(property.Location(), EdmErrorCode.InvalidNavigationPropertyType, Strings.EdmModel_Validator_Semantic_InvalidNavigationPropertyType(property.Name));
				return;
			}
			if (property.Partner == null || property.Partner is BadNavigationProperty)
			{
				return;
			}
			if (property.ToEntityType() != property.Partner.DeclaringEntityType())
			{
				context.AddError(property.Location(), EdmErrorCode.InvalidNavigationPropertyType, Strings.EdmModel_Validator_Semantic_InvalidNavigationPropertyType(property.Name));
			}
		});

		// Token: 0x0400060D RID: 1549
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyDuplicateDependentProperty = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmStructuralProperty> enumerable3 = navigationProperty.DependentProperties();
			if (enumerable3 != null)
			{
				HashSetInternal<string> hashSetInternal9 = new HashSetInternal<string>();
				foreach (IEdmStructuralProperty edmStructuralProperty5 in navigationProperty.DependentProperties())
				{
					if (edmStructuralProperty5 != null)
					{
						ValidationHelper.AddMemberNameToHashSet(edmStructuralProperty5, hashSetInternal9, context, EdmErrorCode.DuplicateDependentProperty, Strings.EdmModel_Validator_Semantic_DuplicateDependentProperty(edmStructuralProperty5.Name, navigationProperty.Name), false);
					}
				}
			}
		});

		// Token: 0x0400060E RID: 1550
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyPrincipalEndMultiplicity = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmStructuralProperty> enumerable4 = navigationProperty.DependentProperties();
			if (enumerable4 != null)
			{
				if (ValidationHelper.AllPropertiesAreNullable(enumerable4))
				{
					if (navigationProperty.TargetMultiplicity() != EdmMultiplicity.ZeroOrOne)
					{
						context.AddError(navigationProperty.Location(), EdmErrorCode.InvalidMultiplicityOfPrincipalEnd, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable(navigationProperty.Name));
						return;
					}
				}
				else if (!ValidationHelper.HasNullableProperty(enumerable4))
				{
					if (navigationProperty.TargetMultiplicity() != EdmMultiplicity.One)
					{
						context.AddError(navigationProperty.Location(), EdmErrorCode.InvalidMultiplicityOfPrincipalEnd, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable(navigationProperty.Name));
						return;
					}
				}
				else if (navigationProperty.TargetMultiplicity() != EdmMultiplicity.One && navigationProperty.TargetMultiplicity() != EdmMultiplicity.ZeroOrOne)
				{
					context.AddError(navigationProperty.Location(), EdmErrorCode.InvalidMultiplicityOfPrincipalEnd, Strings.EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne(navigationProperty.Name));
				}
			}
		});

		// Token: 0x0400060F RID: 1551
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyDependentEndMultiplicity = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			if (navigationProperty.Partner == null)
			{
				return;
			}
			IEnumerable<IEdmStructuralProperty> enumerable5 = navigationProperty.DependentProperties();
			if (enumerable5 != null)
			{
				if (ValidationHelper.PropertySetsAreEquivalent(navigationProperty.DeclaringEntityType().Key(), enumerable5))
				{
					if (navigationProperty.Type.IsCollection())
					{
						context.AddError(navigationProperty.Location(), EdmErrorCode.InvalidMultiplicityOfDependentEnd, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne(navigationProperty.Name));
						return;
					}
				}
				else if (!navigationProperty.Partner.Type.IsCollection())
				{
					context.AddError(navigationProperty.Location(), EdmErrorCode.InvalidMultiplicityOfDependentEnd, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany(navigationProperty.Name));
				}
			}
		});

		// Token: 0x04000610 RID: 1552
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyDependentPropertiesMustBelongToDependentEntity = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmStructuralProperty> enumerable6 = navigationProperty.DependentProperties();
			if (enumerable6 != null)
			{
				IEdmEntityType edmEntityType2 = navigationProperty.DeclaringEntityType();
				foreach (IEdmStructuralProperty edmStructuralProperty6 in enumerable6)
				{
					if (!context.IsBad(edmStructuralProperty6) && !edmStructuralProperty6.IsBad())
					{
						IEdmProperty edmProperty4 = edmEntityType2.FindProperty(edmStructuralProperty6.Name);
						if (edmProperty4 != edmStructuralProperty6)
						{
							context.AddError(navigationProperty.Location(), EdmErrorCode.DependentPropertiesMustBelongToDependentEntity, Strings.EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity(edmStructuralProperty6.Name, edmEntityType2.Name));
						}
					}
				}
			}
		});

		// Token: 0x04000611 RID: 1553
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyEndWithManyMultiplicityCannotHaveOperationsSpecified = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty end)
		{
			if (end.Partner != null && end.Partner.Type.IsCollection() && end.OnDelete != EdmOnDeleteAction.None)
			{
				string text5 = Strings.EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(end.Name);
				context.AddError(end.Location(), EdmErrorCode.EndWithManyMultiplicityCannotHaveOperationsSpecified, text5);
			}
		});

		// Token: 0x04000612 RID: 1554
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyWithRecursiveContainmentTargetMustBeOptional = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.ContainsTarget && property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && !property.Type.IsCollection() && !property.Type.IsNullable)
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyWithRecursiveContainmentTargetMustBeOptional, Strings.EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional(property.Name));
			}
		});

		// Token: 0x04000613 RID: 1555
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.Partner != null && property.ContainsTarget && property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && (property.Partner.Type.IsCollection() || !property.Partner.Type.IsNullable))
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne, Strings.EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne(property.Name));
			}
		});

		// Token: 0x04000614 RID: 1556
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.Partner != null && property.ContainsTarget && !property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && (property.Partner.Type.IsCollection() || property.Partner.Type.IsNullable))
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne, Strings.EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne(property.Name));
			}
		});

		// Token: 0x04000615 RID: 1557
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyEntityMustNotIndirectlyContainItself = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.ContainsTarget && !property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && ValidationHelper.TypeIndirectlyContainsTarget(property.ToEntityType(), property.DeclaringEntityType(), new HashSetInternal<IEdmEntityType>(), context.Model))
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyEntityMustNotIndirectlyContainItself, Strings.EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself(property.Name));
			}
		});

		// Token: 0x04000616 RID: 1558
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyTypeMismatchRelationshipConstraint = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmStructuralProperty> enumerable7 = navigationProperty.DependentProperties();
			if (enumerable7 != null)
			{
				int num = Enumerable.Count<IEdmStructuralProperty>(enumerable7);
				IEdmEntityType edmEntityType3 = navigationProperty.ToEntityType();
				IEnumerable<IEdmStructuralProperty> enumerable8 = navigationProperty.PrincipalProperties();
				if (num == Enumerable.Count<IEdmStructuralProperty>(enumerable8))
				{
					for (int i = 0; i < num; i++)
					{
						IEdmType definition = Enumerable.ElementAtOrDefault<IEdmStructuralProperty>(enumerable7, i).Type.Definition;
						IEdmType definition2 = Enumerable.ElementAtOrDefault<IEdmStructuralProperty>(enumerable8, i).Type.Definition;
						if (!(definition is BadType) && !(definition2 is BadType) && !definition.IsEquivalentTo(definition2))
						{
							string text6 = Strings.EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint(Enumerable.ToList<IEdmStructuralProperty>(navigationProperty.DependentProperties())[i].Name, navigationProperty.DeclaringEntityType().FullName(), Enumerable.ToList<IEdmStructuralProperty>(enumerable8)[i].Name, edmEntityType3.Name, "Fred");
							context.AddError(navigationProperty.Location(), EdmErrorCode.TypeMismatchRelationshipConstraint, text6);
						}
					}
				}
			}
		});

		// Token: 0x04000617 RID: 1559
		public static readonly ValidationRule<IEdmProperty> PropertyMustNotHaveKindOfNone = new ValidationRule<IEdmProperty>(delegate(ValidationContext context, IEdmProperty property)
		{
			if (property.PropertyKind == EdmPropertyKind.None && !context.IsBad(property))
			{
				context.AddError(property.Location(), EdmErrorCode.PropertyMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone(property.Name));
			}
		});

		// Token: 0x04000618 RID: 1560
		public static readonly ValidationRule<IEdmOperationImport> OperationImportCannotImportBoundOperation = new ValidationRule<IEdmOperationImport>(delegate(ValidationContext context, IEdmOperationImport operationImport)
		{
			if (operationImport.Operation.IsBound)
			{
				context.AddError(operationImport.Location(), EdmErrorCode.OperationImportCannotImportBoundOperation, Strings.EdmModel_Validator_Semantic_OperationImportCannotImportBoundOperation(operationImport.Name, operationImport.Operation.Name));
			}
		});

		// Token: 0x04000619 RID: 1561
		public static readonly ValidationRule<IEdmOperationImport> OperationImportEntitySetExpressionIsInvalid = new ValidationRule<IEdmOperationImport>(delegate(ValidationContext context, IEdmOperationImport operationImport)
		{
			if (operationImport.EntitySet != null)
			{
				if (operationImport.EntitySet.ExpressionKind != EdmExpressionKind.EntitySetReference && operationImport.EntitySet.ExpressionKind != EdmExpressionKind.Path)
				{
					context.AddError(operationImport.Location(), EdmErrorCode.OperationImportEntitySetExpressionIsInvalid, Strings.EdmModel_Validator_Semantic_OperationImportEntitySetExpressionKindIsInvalid(operationImport.Name, operationImport.EntitySet.ExpressionKind));
					return;
				}
				IEdmEntitySet edmEntitySet;
				if (!operationImport.TryGetStaticEntitySet(out edmEntitySet))
				{
					context.AddError(operationImport.Location(), EdmErrorCode.OperationImportEntitySetExpressionIsInvalid, Strings.EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid(operationImport.Name));
				}
				else if (!context.IsBad(edmEntitySet) && operationImport.Container.FindEntitySetExtended(edmEntitySet.Name) == null)
				{
					context.AddError(operationImport.Location(), EdmErrorCode.OperationImportEntitySetExpressionIsInvalid, Strings.EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid(operationImport.Name));
				}
				IEdmOperationParameter edmOperationParameter;
				IEnumerable<IEdmNavigationProperty> enumerable9;
				IEnumerable<EdmError> enumerable10;
				if (!operationImport.TryGetRelativeEntitySetPath(context.Model, out edmOperationParameter, out enumerable9, out enumerable10))
				{
					foreach (EdmError edmError in enumerable10)
					{
						context.AddError(edmError);
					}
				}
			}
		});

		// Token: 0x0400061A RID: 1562
		public static readonly ValidationRule<IEdmOperationImport> OperationImportEntityTypeDoesNotMatchEntitySet = new ValidationRule<IEdmOperationImport>(delegate(ValidationContext context, IEdmOperationImport operationImport)
		{
			if (operationImport.EntitySet != null && operationImport.Operation.ReturnType != null)
			{
				IEdmTypeReference edmTypeReference = (operationImport.Operation.ReturnType.IsCollection() ? operationImport.Operation.ReturnType.AsCollection().ElementType() : operationImport.Operation.ReturnType);
				if (edmTypeReference.IsEntity())
				{
					IEdmEntityType edmEntityType4 = edmTypeReference.AsEntity().EntityDefinition();
					IEdmEntitySet edmEntitySet2;
					IEdmOperationParameter edmOperationParameter2;
					IEnumerable<IEdmNavigationProperty> enumerable11;
					IEnumerable<EdmError> enumerable12;
					if (operationImport.TryGetStaticEntitySet(out edmEntitySet2))
					{
						IEdmEntityType edmEntityType5 = edmEntitySet2.EntityType();
						if (!edmEntityType4.IsOrInheritsFrom(edmEntityType5) && !context.IsBad(edmEntityType4) && !context.IsBad(edmEntitySet2) && !context.IsBad(edmEntityType5))
						{
							string text7 = Strings.EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet(operationImport.Name, edmEntityType4.FullName(), edmEntitySet2.Name);
							context.AddError(operationImport.Location(), EdmErrorCode.OperationImportEntityTypeDoesNotMatchEntitySet, text7);
							return;
						}
					}
					else if (operationImport.TryGetRelativeEntitySetPath(context.Model, out edmOperationParameter2, out enumerable11, out enumerable12))
					{
						List<IEdmNavigationProperty> list2 = Enumerable.ToList<IEdmNavigationProperty>(enumerable11);
						IEdmTypeReference edmTypeReference2 = ((list2.Count == 0) ? edmOperationParameter2.Type : Enumerable.Last<IEdmNavigationProperty>(enumerable11).Type);
						IEdmTypeReference edmTypeReference3 = (edmTypeReference2.IsCollection() ? edmTypeReference2.AsCollection().ElementType() : edmTypeReference2);
						if (!edmEntityType4.IsOrInheritsFrom(edmTypeReference3.Definition) && !context.IsBad(edmEntityType4) && !context.IsBad(edmTypeReference3.Definition))
						{
							context.AddError(operationImport.Location(), EdmErrorCode.OperationImportEntityTypeDoesNotMatchEntitySet, Strings.EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet2(operationImport.Name, edmTypeReference.FullName()));
							return;
						}
					}
				}
				else if (!context.IsBad(edmTypeReference.Definition))
				{
					context.AddError(operationImport.Location(), EdmErrorCode.OperationImportSpecifiesEntitySetButDoesNotReturnEntityType, Strings.EdmModel_Validator_Semantic_OperationImportSpecifiesEntitySetButNotEntityType(operationImport.Name));
				}
			}
		});

		// Token: 0x0400061B RID: 1563
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportWithParameterShouldNotBeIncludedInServiceDocument = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (functionImport.IncludeInServiceDocument && Enumerable.Any<IEdmOperationParameter>(functionImport.Function.Parameters))
			{
				context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportWithParameterShouldNotBeIncludedInServiceDocument, Strings.EdmModel_Validator_Semantic_FunctionImportWithParameterShouldNotBeIncludedInServiceDocument(functionImport.Name));
			}
		});

		// Token: 0x0400061C RID: 1564
		public static readonly ValidationRule<IEdmFunction> FunctionMustHaveReturnType = new ValidationRule<IEdmFunction>(delegate(ValidationContext context, IEdmFunction function)
		{
			if (function.ReturnType == null)
			{
				context.AddError(function.Location(), EdmErrorCode.FunctionMustHaveReturnType, Strings.EdmModel_Validator_Semantic_FunctionMustHaveReturnType(function.Name));
			}
		});

		// Token: 0x0400061D RID: 1565
		public static readonly ValidationRule<IEdmOperation> OperationUnsupportedReturnType = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			if (operation.ReturnType != null)
			{
				IEdmTypeReference edmTypeReference4 = (operation.ReturnType.IsCollection() ? operation.ReturnType.AsCollection().ElementType() : operation.ReturnType);
				if (!(edmTypeReference4.Definition is IUnresolvedElement) && context.IsBad(edmTypeReference4.Definition))
				{
					context.AddError(operation.Location(), EdmErrorCode.OperationImportUnsupportedReturnType, Strings.EdmModel_Validator_Semantic_OperationWithUnsupportedReturnType(operation.Name));
				}
			}
		});

		// Token: 0x0400061E RID: 1566
		public static readonly ValidationRule<IEdmOperation> OperationParameterNameAlreadyDefinedDuplicate = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			HashSetInternal<string> hashSetInternal10 = new HashSetInternal<string>();
			if (operation.Parameters != null)
			{
				foreach (IEdmOperationParameter edmOperationParameter3 in operation.Parameters)
				{
					ValidationHelper.AddMemberNameToHashSet(edmOperationParameter3, hashSetInternal10, context, EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate(edmOperationParameter3.Name), false);
				}
			}
		});

		// Token: 0x0400061F RID: 1567
		public static readonly ValidationRule<IEdmOperation> BoundOperationMustHaveParameters = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			if (operation.IsBound && Enumerable.Count<IEdmOperationParameter>(operation.Parameters) == 0)
			{
				context.AddError(operation.Location(), EdmErrorCode.BoundOperationMustHaveParameters, Strings.EdmModel_Validator_Semantic_BoundOperationMustHaveParameters(operation.Name));
			}
		});

		// Token: 0x04000620 RID: 1568
		public static readonly ValidationRule<IEdmOperation> OperationEntitySetPathMustBeValid = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			IEdmOperationParameter edmOperationParameter4 = null;
			IEnumerable<IEdmNavigationProperty> enumerable13 = null;
			IEdmEntityType edmEntityType6 = null;
			IEnumerable<EdmError> enumerable14 = null;
			operation.TryGetRelativeEntitySetPath(context.Model, out edmOperationParameter4, out enumerable13, out edmEntityType6, out enumerable14);
			foreach (EdmError edmError2 in enumerable14)
			{
				context.AddError(edmError2);
			}
		});

		// Token: 0x04000621 RID: 1569
		public static readonly ValidationRule<IEdmOperation> OperationReturnTypeEntityTypeMustBeValid = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			IEdmOperationParameter edmOperationParameter5 = null;
			IEnumerable<IEdmNavigationProperty> enumerable15 = null;
			IEdmEntityType edmEntityType7 = null;
			IEnumerable<EdmError> enumerable16 = null;
			if (!operation.TryGetRelativeEntitySetPath(context.Model, out edmOperationParameter5, out enumerable15, out edmEntityType7, out enumerable16))
			{
				return;
			}
			if (operation.ReturnType != null)
			{
				IEdmEntityType edmEntityType8 = operation.ReturnType.Definition as IEdmEntityType;
				IEdmCollectionType edmCollectionType2 = operation.ReturnType.Definition as IEdmCollectionType;
				if (edmEntityType8 == null && edmCollectionType2 != null)
				{
					edmEntityType8 = edmCollectionType2.ElementType.Definition as IEdmEntityType;
				}
				bool flag4 = operation.ReturnType.IsEntity();
				if (edmCollectionType2 != null)
				{
					flag4 = edmCollectionType2.ElementType.IsEntity();
				}
				if (!flag4 || context.IsBad(edmEntityType8))
				{
					context.AddError(operation.Location(), EdmErrorCode.OperationWithEntitySetPathReturnTypeInvalid, Strings.EdmModel_Validator_Semantic_OperationWithEntitySetPathReturnTypeInvalid(operation.Name));
				}
				IEdmNavigationProperty edmNavigationProperty = null;
				if (enumerable15 != null)
				{
					edmNavigationProperty = Enumerable.LastOrDefault<IEdmNavigationProperty>(enumerable15);
				}
				if (edmNavigationProperty != null && edmNavigationProperty.TargetMultiplicity() == EdmMultiplicity.Many && edmCollectionType2 == null)
				{
					context.AddError(operation.Location(), EdmErrorCode.OperationWithEntitySetPathResolvesToEntityTypeMismatchesCollectionEntityTypeReturnType, Strings.EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToEntityTypeMismatchesCollectionEntityTypeReturnType(operation.Name));
				}
				if (edmNavigationProperty != null && edmNavigationProperty.TargetMultiplicity() != EdmMultiplicity.Many && edmCollectionType2 != null)
				{
					context.AddError(operation.Location(), EdmErrorCode.OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType, Strings.EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType(operation.Name));
				}
				if (edmEntityType7 != null && edmEntityType8 != null && !edmEntityType8.IsOrInheritsFrom(edmEntityType7))
				{
					context.AddError(operation.Location(), EdmErrorCode.OperationWithEntitySetPathAndReturnTypeTypeNotAssignable, Strings.EdmModel_Validator_Semantic_OperationWithEntitySetPathAndReturnTypeTypeNotAssignable(operation.Name, edmEntityType8.FullName(), edmEntityType7.FullName()));
				}
			}
		});

		// Token: 0x04000622 RID: 1570
		public static readonly ValidationRule<IEdmTypeReference> TypeReferenceInaccessibleSchemaType = new ValidationRule<IEdmTypeReference>(delegate(ValidationContext context, IEdmTypeReference typeReference)
		{
			IEdmSchemaType edmSchemaType3 = typeReference.Definition as IEdmSchemaType;
			if (edmSchemaType3 != null && !context.IsBad(edmSchemaType3))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, edmSchemaType3, typeReference.Location());
			}
		});

		// Token: 0x04000623 RID: 1571
		public static readonly ValidationRule<IEdmDecimalTypeReference> DecimalTypeReferenceScaleOutOfRange = new ValidationRule<IEdmDecimalTypeReference>(delegate(ValidationContext context, IEdmDecimalTypeReference type)
		{
			if (type.Scale > type.Precision || type.Scale < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.ScaleOutOfRange, Strings.EdmModel_Validator_Semantic_ScaleOutOfRange);
			}
		});

		// Token: 0x04000624 RID: 1572
		public static readonly ValidationRule<IEdmDecimalTypeReference> DecimalTypeReferencePrecisionOutOfRange = new ValidationRule<IEdmDecimalTypeReference>(delegate(ValidationContext context, IEdmDecimalTypeReference type)
		{
			if (type.Precision > 2147483647 || type.Precision < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.PrecisionOutOfRange, Strings.EdmModel_Validator_Semantic_PrecisionOutOfRange);
			}
		});

		// Token: 0x04000625 RID: 1573
		public static readonly ValidationRule<IEdmStringTypeReference> StringTypeReferenceStringMaxLengthNegative = new ValidationRule<IEdmStringTypeReference>(delegate(ValidationContext context, IEdmStringTypeReference type)
		{
			if (type.MaxLength < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.MaxLengthOutOfRange, Strings.EdmModel_Validator_Semantic_StringMaxLengthOutOfRange);
			}
		});

		// Token: 0x04000626 RID: 1574
		public static readonly ValidationRule<IEdmStringTypeReference> StringTypeReferenceStringUnboundedNotValidForMaxLength = new ValidationRule<IEdmStringTypeReference>(delegate(ValidationContext context, IEdmStringTypeReference type)
		{
			if (type.MaxLength != null && type.IsUnbounded)
			{
				context.AddError(type.Location(), EdmErrorCode.IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull, Strings.EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull);
			}
		});

		// Token: 0x04000627 RID: 1575
		public static readonly ValidationRule<IEdmBinaryTypeReference> BinaryTypeReferenceBinaryMaxLengthNegative = new ValidationRule<IEdmBinaryTypeReference>(delegate(ValidationContext context, IEdmBinaryTypeReference type)
		{
			if (type.MaxLength < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.MaxLengthOutOfRange, Strings.EdmModel_Validator_Semantic_MaxLengthOutOfRange);
			}
		});

		// Token: 0x04000628 RID: 1576
		public static readonly ValidationRule<IEdmBinaryTypeReference> BinaryTypeReferenceBinaryUnboundedNotValidForMaxLength = new ValidationRule<IEdmBinaryTypeReference>(delegate(ValidationContext context, IEdmBinaryTypeReference type)
		{
			if (type.MaxLength != null && type.IsUnbounded)
			{
				context.AddError(type.Location(), EdmErrorCode.IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull, Strings.EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull);
			}
		});

		// Token: 0x04000629 RID: 1577
		public static readonly ValidationRule<IEdmTemporalTypeReference> TemporalTypeReferencePrecisionOutOfRange = new ValidationRule<IEdmTemporalTypeReference>(delegate(ValidationContext context, IEdmTemporalTypeReference type)
		{
			if (type.Precision > 2147483647 || type.Precision < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.PrecisionOutOfRange, Strings.EdmModel_Validator_Semantic_PrecisionOutOfRange);
			}
		});

		// Token: 0x0400062A RID: 1578
		public static readonly ValidationRule<IEdmModel> ModelDuplicateSchemaElementName = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			HashSetInternal<string> hashSetInternal11 = new HashSetInternal<string>();
			DuplicateOperationValidator duplicateOperationValidator = new DuplicateOperationValidator(context);
			HashSetInternal<string> hashSetInternal12 = new HashSetInternal<string>();
			foreach (IEdmSchemaElement edmSchemaElement in model.SchemaElements)
			{
				bool flag5 = false;
				string text8 = edmSchemaElement.FullName();
				IEdmOperation edmOperation = edmSchemaElement as IEdmOperation;
				if (edmOperation != null)
				{
					if (!hashSetInternal12.Contains(edmOperation.FullName()))
					{
						hashSetInternal12.Add(edmOperation.FullName());
					}
					if (hashSetInternal11.Contains(text8))
					{
						flag5 = true;
					}
					duplicateOperationValidator.ValidateNotDuplicate(edmOperation, false);
					if (!flag5)
					{
						flag5 = model.OperationOrNameExistsInReferencedModel(edmOperation, text8);
					}
				}
				else
				{
					flag5 = !hashSetInternal11.Add(text8) || hashSetInternal12.Contains(text8) || model.ItemExistsInReferencedModel(text8, true);
				}
				if (flag5)
				{
					context.AddError(edmSchemaElement.Location(), EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined(text8));
				}
			}
		});

		// Token: 0x0400062B RID: 1579
		public static readonly ValidationRule<IEdmModel> ModelDuplicateEntityContainerName = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			HashSetInternal<string> hashSetInternal13 = new HashSetInternal<string>();
			IEdmEntityContainer entityContainer = model.EntityContainer;
			if (entityContainer != null)
			{
				ValidationHelper.AddMemberNameToHashSet(entityContainer, hashSetInternal13, context, EdmErrorCode.DuplicateEntityContainerName, Strings.EdmModel_Validator_Semantic_DuplicateEntityContainerName(entityContainer.Name), false);
			}
		});

		// Token: 0x0400062C RID: 1580
		public static readonly ValidationRule<IEdmModel> ModelBoundFunctionOverloadsMustHaveSameReturnType = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			foreach (IGrouping<string, IEdmFunction> grouping in Enumerable.GroupBy<IEdmFunction, string>(Enumerable.Where<IEdmFunction>(Enumerable.OfType<IEdmFunction>(model.SchemaElements), (IEdmFunction f) => f.IsBound), (IEdmFunction f2) => f2.FullName()))
			{
				Dictionary<IEdmTypeReference, IEdmTypeReference> dictionary = new Dictionary<IEdmTypeReference, IEdmTypeReference>(new ValidationRules.EdmTypeReferenceComparer());
				foreach (IEdmFunction edmFunction in grouping)
				{
					if (Enumerable.Any<IEdmOperationParameter>(edmFunction.Parameters) && edmFunction.ReturnType != null)
					{
						IEdmOperationParameter edmOperationParameter6 = Enumerable.First<IEdmOperationParameter>(edmFunction.Parameters);
						if (!dictionary.ContainsKey(edmOperationParameter6.Type))
						{
							dictionary.Add(edmOperationParameter6.Type, edmFunction.ReturnType);
						}
						else
						{
							IEdmTypeReference edmTypeReference5 = dictionary[edmOperationParameter6.Type];
							if (!edmFunction.ReturnType.IsEquivalentTo(edmTypeReference5))
							{
								context.AddError(edmFunction.Location(), EdmErrorCode.BoundFunctionOverloadsMustHaveSameReturnType, Strings.EdmModel_Validator_Semantic_BoundFunctionOverloadsMustHaveSameReturnType(edmFunction.Name, edmTypeReference5.FullName()));
							}
						}
					}
				}
			}
		});

		// Token: 0x0400062D RID: 1581
		public static readonly ValidationRule<IEdmModel> UnBoundFunctionOverloadsMustHaveIdenticalReturnTypes = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			Dictionary<string, IEdmTypeReference> dictionary2 = new Dictionary<string, IEdmTypeReference>();
			foreach (IEdmFunction edmFunction2 in Enumerable.Where<IEdmFunction>(Enumerable.OfType<IEdmFunction>(model.SchemaElements), (IEdmFunction f) => !f.IsBound))
			{
				if (!dictionary2.ContainsKey(edmFunction2.Name))
				{
					dictionary2.Add(edmFunction2.Name, edmFunction2.ReturnType);
				}
				else if (!edmFunction2.ReturnType.IsEquivalentTo(dictionary2[edmFunction2.Name]))
				{
					context.AddError(edmFunction2.Location(), EdmErrorCode.UnboundFunctionOverloadHasIncorrectReturnType, Strings.EdmModel_Validator_Semantic_UnboundFunctionOverloadHasIncorrectReturnType(edmFunction2.Name));
				}
			}
		});

		// Token: 0x0400062E RID: 1582
		public static readonly ValidationRule<IEdmDirectValueAnnotation> ImmediateValueAnnotationElementAnnotationIsValid = new ValidationRule<IEdmDirectValueAnnotation>(delegate(ValidationContext context, IEdmDirectValueAnnotation annotation)
		{
			IEdmStringValue edmStringValue = annotation.Value as IEdmStringValue;
			if (edmStringValue != null && edmStringValue.IsSerializedAsElement(context.Model) && (EdmUtil.IsNullOrWhiteSpaceInternal(annotation.NamespaceUri) || EdmUtil.IsNullOrWhiteSpaceInternal(annotation.Name)))
			{
				context.AddError(annotation.Location(), EdmErrorCode.InvalidElementAnnotation, Strings.EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm);
			}
		});

		// Token: 0x0400062F RID: 1583
		public static readonly ValidationRule<IEdmDirectValueAnnotation> ImmediateValueAnnotationElementAnnotationHasNameAndNamespace = new ValidationRule<IEdmDirectValueAnnotation>(delegate(ValidationContext context, IEdmDirectValueAnnotation annotation)
		{
			IEdmStringValue edmStringValue2 = annotation.Value as IEdmStringValue;
			EdmError edmError3;
			if (edmStringValue2 != null && edmStringValue2.IsSerializedAsElement(context.Model) && !ValidationHelper.ValidateValueCanBeWrittenAsXmlElementAnnotation(edmStringValue2, annotation.NamespaceUri, annotation.Name, out edmError3))
			{
				context.AddError(edmError3);
			}
		});

		// Token: 0x04000630 RID: 1584
		public static readonly ValidationRule<IEdmDirectValueAnnotation> DirectValueAnnotationHasXmlSerializableName = new ValidationRule<IEdmDirectValueAnnotation>(delegate(ValidationContext context, IEdmDirectValueAnnotation annotation)
		{
			string name = annotation.Name;
			if (!EdmUtil.IsNullOrWhiteSpaceInternal(name) && name.Length <= 480 && name.Length > 0)
			{
				try
				{
					XmlConvert.VerifyNCName(annotation.Name);
				}
				catch (XmlException)
				{
					IEdmValue edmValue = annotation.Value as IEdmValue;
					EdmLocation edmLocation = ((edmValue == null) ? null : edmValue.Location());
					context.AddError(new EdmError(edmLocation, EdmErrorCode.InvalidName, Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed(annotation.Name)));
				}
			}
		});

		// Token: 0x04000631 RID: 1585
		public static readonly ValidationRule<IEdmVocabularyAnnotation> VocabularyAnnotationInaccessibleTarget = new ValidationRule<IEdmVocabularyAnnotation>(delegate(ValidationContext context, IEdmVocabularyAnnotation annotation)
		{
			IEdmVocabularyAnnotatable target = annotation.Target;
			bool flag6 = false;
			IEdmEntityContainer edmEntityContainer = target as IEdmEntityContainer;
			if (edmEntityContainer != null)
			{
				flag6 = context.Model.FindEntityContainer(edmEntityContainer.FullName()) != null;
			}
			else
			{
				IEdmEntitySet edmEntitySet3 = target as IEdmEntitySet;
				if (edmEntitySet3 != null)
				{
					IEdmEntityContainer container = edmEntitySet3.Container;
					if (container != null)
					{
						flag6 = container.FindEntitySetExtended(edmEntitySet3.Name) != null;
					}
				}
				else
				{
					IEdmSchemaType edmSchemaType4 = target as IEdmSchemaType;
					if (edmSchemaType4 != null)
					{
						flag6 = context.Model.FindType(edmSchemaType4.FullName()) != null;
					}
					else
					{
						IEdmTerm edmTerm = target as IEdmTerm;
						if (edmTerm != null)
						{
							flag6 = context.Model.FindValueTerm(edmTerm.FullName()) != null;
						}
						else
						{
							IEdmOperation edmOperation2 = target as IEdmOperation;
							if (edmOperation2 != null)
							{
								flag6 = Enumerable.Any<IEdmOperation>(context.Model.FindOperations(edmOperation2.FullName()));
							}
							else
							{
								IEdmOperationImport edmOperationImport2 = target as IEdmOperationImport;
								if (edmOperationImport2 != null)
								{
									flag6 = Enumerable.Any<IEdmOperationImport>(edmOperationImport2.Container.FindOperationImportsExtended(edmOperationImport2.Name));
								}
								else
								{
									IEdmProperty edmProperty5 = target as IEdmProperty;
									if (edmProperty5 != null)
									{
										string text9 = EdmUtil.FullyQualifiedName(edmProperty5.DeclaringType as IEdmSchemaType);
										IEdmStructuredType edmStructuredType = context.Model.FindType(text9) as IEdmStructuredType;
										if (edmStructuredType != null)
										{
											flag6 = edmStructuredType.FindProperty(edmProperty5.Name) != null;
										}
									}
									else
									{
										IEdmOperationParameter edmOperationParameter7 = target as IEdmOperationParameter;
										if (edmOperationParameter7 != null)
										{
											IEdmOperation declaringOperation = edmOperationParameter7.DeclaringOperation;
											if (declaringOperation != null)
											{
												using (IEnumerator<IEdmOperation> enumerator26 = context.Model.FindOperations(declaringOperation.FullName()).GetEnumerator())
												{
													while (enumerator26.MoveNext())
													{
														IEdmOperation edmOperation3 = enumerator26.Current;
														if (edmOperation3.FindParameter(edmOperationParameter7.Name) != null)
														{
															flag6 = true;
															break;
														}
													}
													goto IL_0235;
												}
											}
											IEdmOperationImport edmOperationImport3 = edmOperationParameter7.DeclaringOperation as IEdmOperationImport;
											if (edmOperationImport3 == null)
											{
												goto IL_0235;
											}
											IEdmEntityContainer container2 = edmOperationImport3.Container;
											using (IEnumerator<IEdmOperationImport> enumerator27 = container2.FindOperationImportsExtended(edmOperationImport3.Name).GetEnumerator())
											{
												while (enumerator27.MoveNext())
												{
													IEdmOperationImport edmOperationImport4 = enumerator27.Current;
													if (edmOperationImport4.Operation.FindParameter(edmOperationParameter7.Name) != null)
													{
														flag6 = true;
														break;
													}
												}
												goto IL_0235;
											}
										}
										flag6 = true;
									}
								}
							}
						}
					}
				}
			}
			IL_0235:
			if (!flag6)
			{
				context.AddError(annotation.Location(), EdmErrorCode.BadUnresolvedTarget, Strings.EdmModel_Validator_Semantic_InaccessibleTarget(EdmUtil.FullyQualifiedName(target)));
			}
		});

		// Token: 0x04000632 RID: 1586
		public static readonly ValidationRule<IEdmValueAnnotation> ValueAnnotationAssertCorrectExpressionType = new ValidationRule<IEdmValueAnnotation>(delegate(ValidationContext context, IEdmValueAnnotation annotation)
		{
			IEnumerable<EdmError> enumerable17;
			if (!annotation.Value.TryCast(((IEdmValueTerm)annotation.Term).Type, out enumerable17))
			{
				foreach (EdmError edmError4 in enumerable17)
				{
					if (edmError4.ErrorCode != EdmErrorCode.RecordExpressionMissingRequiredProperty)
					{
						context.AddError(edmError4);
					}
				}
			}
		});

		// Token: 0x04000633 RID: 1587
		public static readonly ValidationRule<IEdmValueAnnotation> AnnotationInaccessibleTerm = new ValidationRule<IEdmValueAnnotation>(delegate(ValidationContext context, IEdmValueAnnotation annotation)
		{
			IEdmTerm term2 = annotation.Term;
			if (!(term2 is IUnresolvedElement) && context.Model.FindValueTerm(term2.FullName()) == null)
			{
				context.AddError(annotation.Location(), EdmErrorCode.BadUnresolvedTerm, Strings.EdmModel_Validator_Semantic_InaccessibleTerm(annotation.Term.FullName()));
			}
		});

		// Token: 0x04000634 RID: 1588
		public static readonly ValidationRule<IEdmPropertyValueBinding> PropertyValueBindingValueIsCorrectType = new ValidationRule<IEdmPropertyValueBinding>(delegate(ValidationContext context, IEdmPropertyValueBinding binding)
		{
			IEnumerable<EdmError> enumerable18;
			if (!binding.Value.TryCast(binding.BoundProperty.Type, out enumerable18) && !context.IsBad(binding) && !context.IsBad(binding.BoundProperty))
			{
				foreach (EdmError edmError5 in enumerable18)
				{
					context.AddError(edmError5);
				}
			}
		});

		// Token: 0x04000635 RID: 1589
		public static readonly ValidationRule<IEdmTerm> TermMustNotHaveKindOfNone = new ValidationRule<IEdmTerm>(delegate(ValidationContext context, IEdmTerm term)
		{
			if (term.TermKind == EdmTermKind.None && !context.IsBad(term))
			{
				context.AddError(term.Location(), EdmErrorCode.TermMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_TermMustNotHaveKindOfNone(term.FullName()));
			}
		});

		// Token: 0x04000636 RID: 1590
		public static readonly ValidationRule<IEdmIfExpression> IfExpressionAssertCorrectTestType = new ValidationRule<IEdmIfExpression>(delegate(ValidationContext context, IEdmIfExpression expression)
		{
			IEnumerable<EdmError> enumerable19;
			if (!expression.TestExpression.TryCast(EdmCoreModel.Instance.GetBoolean(false), out enumerable19))
			{
				foreach (EdmError edmError6 in enumerable19)
				{
					context.AddError(edmError6);
				}
			}
		});

		// Token: 0x04000637 RID: 1591
		public static readonly ValidationRule<IEdmCollectionExpression> CollectionExpressionAllElementsCorrectType = new ValidationRule<IEdmCollectionExpression>(delegate(ValidationContext context, IEdmCollectionExpression expression)
		{
			if (expression.DeclaredType != null && !context.IsBad(expression) && !context.IsBad(expression.DeclaredType))
			{
				IEnumerable<EdmError> enumerable20;
				expression.TryCastCollectionAsType(expression.DeclaredType, null, false, out enumerable20);
				foreach (EdmError edmError7 in enumerable20)
				{
					context.AddError(edmError7);
				}
			}
		});

		// Token: 0x04000638 RID: 1592
		public static readonly ValidationRule<IEdmRecordExpression> RecordExpressionPropertiesMatchType = new ValidationRule<IEdmRecordExpression>(delegate(ValidationContext context, IEdmRecordExpression expression)
		{
			if (expression.DeclaredType != null && !context.IsBad(expression) && !context.IsBad(expression.DeclaredType))
			{
				IEnumerable<EdmError> enumerable21;
				expression.TryCastRecordAsType(expression.DeclaredType, null, false, out enumerable21);
				foreach (EdmError edmError8 in enumerable21)
				{
					context.AddError(edmError8);
				}
			}
		});

		// Token: 0x04000639 RID: 1593
		public static readonly ValidationRule<IEdmApplyExpression> OperationApplicationExpressionParametersMatchAppliedOperation = new ValidationRule<IEdmApplyExpression>(delegate(ValidationContext context, IEdmApplyExpression expression)
		{
			IEdmOperationReferenceExpression edmOperationReferenceExpression = expression.AppliedOperation as IEdmOperationReferenceExpression;
			if (edmOperationReferenceExpression.ReferencedOperation != null && !context.IsBad(edmOperationReferenceExpression.ReferencedOperation))
			{
				if (Enumerable.Count<IEdmOperationParameter>(edmOperationReferenceExpression.ReferencedOperation.Parameters) != Enumerable.Count<IEdmExpression>(expression.Arguments))
				{
					context.AddError(new EdmError(expression.Location(), EdmErrorCode.IncorrectNumberOfArguments, Strings.EdmModel_Validator_Semantic_IncorrectNumberOfArguments(Enumerable.Count<IEdmExpression>(expression.Arguments), edmOperationReferenceExpression.ReferencedOperation.FullName(), Enumerable.Count<IEdmOperationParameter>(edmOperationReferenceExpression.ReferencedOperation.Parameters))));
				}
				IEnumerator<IEdmExpression> enumerator33 = expression.Arguments.GetEnumerator();
				foreach (IEdmOperationParameter edmOperationParameter8 in edmOperationReferenceExpression.ReferencedOperation.Parameters)
				{
					enumerator33.MoveNext();
					IEnumerable<EdmError> enumerable22;
					if (!enumerator33.Current.TryCast(edmOperationParameter8.Type, out enumerable22))
					{
						foreach (EdmError edmError9 in enumerable22)
						{
							context.AddError(edmError9);
						}
					}
				}
			}
		});

		// Token: 0x0400063A RID: 1594
		public static readonly ValidationRule<IEdmVocabularyAnnotatable> VocabularyAnnotatableNoDuplicateAnnotations = new ValidationRule<IEdmVocabularyAnnotatable>(delegate(ValidationContext context, IEdmVocabularyAnnotatable annotatable)
		{
			HashSetInternal<string> hashSetInternal14 = new HashSetInternal<string>();
			foreach (IEdmVocabularyAnnotation edmVocabularyAnnotation in annotatable.VocabularyAnnotations(context.Model))
			{
				if (!hashSetInternal14.Add(edmVocabularyAnnotation.Term.FullName() + ":" + edmVocabularyAnnotation.Qualifier))
				{
					context.AddError(new EdmError(edmVocabularyAnnotation.Location(), EdmErrorCode.DuplicateAnnotation, Strings.EdmModel_Validator_Semantic_DuplicateAnnotation(EdmUtil.FullyQualifiedName(annotatable), edmVocabularyAnnotation.Term.FullName(), edmVocabularyAnnotation.Qualifier)));
				}
			}
		});

		// Token: 0x0400063B RID: 1595
		public static readonly ValidationRule<IEdmPrimitiveValue> PrimitiveValueValidForType = new ValidationRule<IEdmPrimitiveValue>(delegate(ValidationContext context, IEdmPrimitiveValue value)
		{
			if (value.Type != null && !context.IsBad(value) && !context.IsBad(value.Type))
			{
				IEnumerable<EdmError> enumerable23;
				value.TryCastPrimitiveAsType(value.Type, out enumerable23);
				foreach (EdmError edmError10 in enumerable23)
				{
					context.AddError(edmError10);
				}
			}
		});

		// Token: 0x02000273 RID: 627
		internal class EdmTypeReferenceComparer : IEqualityComparer<IEdmTypeReference>
		{
			// Token: 0x06000E17 RID: 3607 RVA: 0x0002AE84 File Offset: 0x00029084
			public bool Equals(IEdmTypeReference x, IEdmTypeReference y)
			{
				return x.IsEquivalentTo(y);
			}

			// Token: 0x06000E18 RID: 3608 RVA: 0x0002AE90 File Offset: 0x00029090
			public int GetHashCode(IEdmTypeReference obj)
			{
				string text = obj.FullName();
				if (text == null)
				{
					return 0;
				}
				return text.GetHashCode();
			}
		}
	}
}
