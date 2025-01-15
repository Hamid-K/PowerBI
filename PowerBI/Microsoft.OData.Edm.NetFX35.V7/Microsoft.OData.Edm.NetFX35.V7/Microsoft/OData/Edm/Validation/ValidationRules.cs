using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.Edm.Csdl.CsdlSemantics;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x020000D7 RID: 215
	public static class ValidationRules
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
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

		// Token: 0x06000641 RID: 1601 RVA: 0x0000FA30 File Offset: 0x0000DC30
		private static bool TryResolveNavigationPropertyBindingPath(IEdmModel model, IEdmNavigationSource navigationSource, IEdmNavigationPropertyBinding binding)
		{
			string[] array = Enumerable.ToArray<string>(binding.Path.PathSegments);
			IEdmStructuredType edmStructuredType = navigationSource.EntityType();
			for (int i = 0; i < array.Length - 1; i++)
			{
				string text = array[i];
				if (text.IndexOf('.') < 0)
				{
					IEdmProperty edmProperty = edmStructuredType.FindProperty(text);
					if (edmProperty == null)
					{
						return false;
					}
					IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
					if (edmNavigationProperty != null && !edmNavigationProperty.ContainsTarget)
					{
						return false;
					}
					edmStructuredType = edmProperty.Type.Definition.AsElementType() as IEdmStructuredType;
					if (edmStructuredType == null)
					{
						return false;
					}
				}
				else
				{
					IEdmStructuredType edmStructuredType2 = model.FindType(text) as IEdmStructuredType;
					if (edmStructuredType2 == null || !edmStructuredType2.IsOrInheritsFrom(edmStructuredType))
					{
						return false;
					}
					edmStructuredType = edmStructuredType2;
				}
			}
			IEdmNavigationProperty edmNavigationProperty2 = edmStructuredType.FindProperty(Enumerable.Last<string>(array)) as IEdmNavigationProperty;
			return edmNavigationProperty2 != null;
		}

		// Token: 0x040002AF RID: 687
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

		// Token: 0x040002B0 RID: 688
		public static readonly ValidationRule<IEdmNamedElement> NamedElementNameMustNotBeEmptyOrWhiteSpace = new ValidationRule<IEdmNamedElement>(delegate(ValidationContext context, IEdmNamedElement item)
		{
			if (EdmUtil.IsNullOrWhiteSpaceInternal(item.Name) || item.Name.Length == 0)
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidName, Strings.EdmModel_Validator_Syntactic_MissingName);
			}
		});

		// Token: 0x040002B1 RID: 689
		public static readonly ValidationRule<IEdmNamedElement> NamedElementNameIsTooLong = new ValidationRule<IEdmNamedElement>(delegate(ValidationContext context, IEdmNamedElement item)
		{
			if (!EdmUtil.IsNullOrWhiteSpaceInternal(item.Name) && item.Name.Length > 480)
			{
				context.AddError(item.Location(), EdmErrorCode.NameTooLong, Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong(item.Name));
			}
		});

		// Token: 0x040002B2 RID: 690
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

		// Token: 0x040002B3 RID: 691
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementNamespaceMustNotBeEmptyOrWhiteSpace = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement item)
		{
			if (EdmUtil.IsNullOrWhiteSpaceInternal(item.Namespace) || item.Namespace.Length == 0)
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidNamespaceName, Strings.EdmModel_Validator_Syntactic_MissingNamespaceName);
			}
		});

		// Token: 0x040002B4 RID: 692
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementNamespaceIsTooLong = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement item)
		{
			if (item.Namespace.Length > 512)
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidNamespaceName, Strings.EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong(item.Namespace));
			}
		});

		// Token: 0x040002B5 RID: 693
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementNamespaceIsNotAllowed = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement item)
		{
			if (item.Namespace.Length <= 512 && item.Namespace.Length > 0 && !EdmUtil.IsNullOrWhiteSpaceInternal(item.Namespace) && !EdmUtil.IsValidDottedName(item.Namespace))
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidNamespaceName, Strings.EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed(item.Namespace));
			}
		});

		// Token: 0x040002B6 RID: 694
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementSystemNamespaceEncountered = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement element)
		{
			if (ValidationHelper.IsEdmSystemNamespace(element.Namespace))
			{
				context.AddError(element.Location(), EdmErrorCode.SystemNamespaceEncountered, Strings.EdmModel_Validator_Semantic_SystemNamespaceEncountered(element.Namespace));
			}
		});

		// Token: 0x040002B7 RID: 695
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementMustNotHaveKindOfNone = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement element)
		{
			if (element.SchemaElementKind == EdmSchemaElementKind.None && !context.IsBad(element))
			{
				context.AddError(element.Location(), EdmErrorCode.SchemaElementMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone(element.FullName()));
			}
		});

		// Token: 0x040002B8 RID: 696
		public static readonly ValidationRule<IEdmEntityContainerElement> EntityContainerElementMustNotHaveKindOfNone = new ValidationRule<IEdmEntityContainerElement>(delegate(ValidationContext context, IEdmEntityContainerElement element)
		{
			if (element.ContainerElementKind == EdmContainerElementKind.None && !context.IsBad(element))
			{
				context.AddError(element.Location(), EdmErrorCode.EntityContainerElementMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone(element.Container.FullName() + "/" + element.Name));
			}
		});

		// Token: 0x040002B9 RID: 697
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

		// Token: 0x040002BA RID: 698
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

		// Token: 0x040002BB RID: 699
		public static readonly ValidationRule<IEdmNavigationSource> NavigationSourceInaccessibleEntityType = new ValidationRule<IEdmNavigationSource>(delegate(ValidationContext context, IEdmNavigationSource navigationSource)
		{
			IEdmEntityType edmEntityType = navigationSource.EntityType();
			if (edmEntityType != null && !context.IsBad(edmEntityType))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, edmEntityType, navigationSource.Location());
			}
		});

		// Token: 0x040002BC RID: 700
		public static readonly ValidationRule<IEdmNavigationSource> NavigationPropertyMappingsMustBeUnique = new ValidationRule<IEdmNavigationSource>(delegate(ValidationContext context, IEdmNavigationSource navigationSource)
		{
			HashSetInternal<KeyValuePair<IEdmNavigationProperty, string>> hashSetInternal5 = new HashSetInternal<KeyValuePair<IEdmNavigationProperty, string>>();
			foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding in navigationSource.NavigationPropertyBindings)
			{
				if (!hashSetInternal5.Add(new KeyValuePair<IEdmNavigationProperty, string>(edmNavigationPropertyBinding.NavigationProperty, edmNavigationPropertyBinding.Path.Path)))
				{
					context.AddError(navigationSource.Location(), EdmErrorCode.DuplicateNavigationPropertyMapping, Strings.EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping(navigationSource.Name, edmNavigationPropertyBinding.NavigationProperty.Name));
				}
			}
		});

		// Token: 0x040002BD RID: 701
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

		// Token: 0x040002BE RID: 702
		public static readonly ValidationRule<IEdmNavigationSource> NavigationPropertyBindingPathMustBeResolvable = new ValidationRule<IEdmNavigationSource>(delegate(ValidationContext context, IEdmNavigationSource navigationSource)
		{
			foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding3 in navigationSource.NavigationPropertyBindings)
			{
				if (!edmNavigationPropertyBinding3.NavigationProperty.IsBad() && !edmNavigationPropertyBinding3.Target.IsBad() && !ValidationRules.TryResolveNavigationPropertyBindingPath(context.Model, navigationSource, edmNavigationPropertyBinding3))
				{
					context.AddError(navigationSource.Location(), EdmErrorCode.UnresolvedNavigationPropertyBindingPath, string.Format(CultureInfo.CurrentCulture, "The binding path {0} for navigation property {1} under navigation source {2} is not valid.", new object[]
					{
						edmNavigationPropertyBinding3.Path.Path,
						edmNavigationPropertyBinding3.NavigationProperty.Name,
						navigationSource.Name
					}));
				}
			}
		});

		// Token: 0x040002BF RID: 703
		public static readonly ValidationRule<IEdmEntitySet> EntitySetCanOnlyBeContainedByASingleNavigationProperty = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet set)
		{
			bool flag2 = false;
			foreach (IEdmNavigationSource edmNavigationSource in Enumerable.OfType<IEdmNavigationSource>(set.Container.Elements))
			{
				foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding4 in edmNavigationSource.NavigationPropertyBindings)
				{
					IEdmNavigationProperty navigationProperty3 = edmNavigationPropertyBinding4.NavigationProperty;
					if (edmNavigationPropertyBinding4.Target == set && navigationProperty3.ContainsTarget)
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

		// Token: 0x040002C0 RID: 704
		public static readonly ValidationRule<IEdmNavigationSource> NavigationMappingMustBeBidirectional = new ValidationRule<IEdmNavigationSource>(delegate(ValidationContext context, IEdmNavigationSource navigationSource)
		{
			foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding5 in navigationSource.NavigationPropertyBindings)
			{
				IEdmNavigationProperty navigationProperty2 = edmNavigationPropertyBinding5.NavigationProperty;
				if (navigationProperty2.Partner != null && !navigationProperty2.IsBad())
				{
					IEdmNavigationSource edmNavigationSource2 = edmNavigationPropertyBinding5.Target.FindNavigationTarget(navigationProperty2.Partner, new EdmPathExpression(navigationProperty2.Partner.Name));
					if (edmNavigationSource2 != null && !(edmNavigationSource2 is IEdmUnknownEntitySet) && !(edmNavigationSource2 is IEdmContainedEntitySet) && edmNavigationSource2 != navigationSource && navigationProperty2.Partner.DeclaringEntityType().FindProperty(navigationProperty2.Partner.Name) == navigationProperty2.Partner)
					{
						context.AddError(navigationSource.Location(), EdmErrorCode.NavigationMappingMustBeBidirectional, Strings.EdmModel_Validator_Semantic_NavigationMappingMustBeBidirectional(navigationSource.Name, navigationProperty2.Name));
					}
				}
			}
		});

		// Token: 0x040002C1 RID: 705
		public static readonly ValidationRule<IEdmEntitySet> EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet set)
		{
			foreach (IEdmNavigationPropertyBinding edmNavigationPropertyBinding6 in set.NavigationPropertyBindings)
			{
				if (edmNavigationPropertyBinding6.NavigationProperty.ContainsTarget && edmNavigationPropertyBinding6.NavigationProperty.DeclaringType.IsOrInheritsFrom(edmNavigationPropertyBinding6.NavigationProperty.ToEntityType()) && edmNavigationPropertyBinding6.Target != set)
				{
					context.AddError(set.Location(), EdmErrorCode.EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet, Strings.EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet(edmNavigationPropertyBinding6.NavigationProperty, set.Name));
				}
			}
		});

		// Token: 0x040002C2 RID: 706
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

		// Token: 0x040002C3 RID: 707
		public static readonly ValidationRule<IEdmSingleton> SingletonTypeMustBeEntityType = new ValidationRule<IEdmSingleton>(delegate(ValidationContext context, IEdmSingleton singleton)
		{
			if (!(singleton.Type is IEdmEntityType))
			{
				string text4 = Strings.EdmModel_Validator_Semantic_SingletonTypeMustBeEntityType(singleton.Type.FullTypeName(), singleton.Name);
				context.AddError(singleton.Location(), EdmErrorCode.SingletonTypeMustBeEntityType, text4);
			}
		});

		// Token: 0x040002C4 RID: 708
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

		// Token: 0x040002C5 RID: 709
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

		// Token: 0x040002C6 RID: 710
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypeBaseTypeMustBeSameKindAsDerivedKind = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			if (structuredType is IEdmSchemaType && structuredType.BaseType != null && structuredType.BaseType.TypeKind != structuredType.TypeKind)
			{
				context.AddError(structuredType.Location(), (structuredType.TypeKind == EdmTypeKind.Entity) ? EdmErrorCode.EntityMustHaveEntityBaseType : EdmErrorCode.ComplexTypeMustHaveComplexBaseType, Strings.EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind);
			}
		});

		// Token: 0x040002C7 RID: 711
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypeInaccessibleBaseType = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			IEdmSchemaType edmSchemaType2 = structuredType.BaseType as IEdmSchemaType;
			if (edmSchemaType2 != null && !context.IsBad(edmSchemaType2))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, edmSchemaType2, structuredType.Location());
			}
		});

		// Token: 0x040002C8 RID: 712
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

		// Token: 0x040002C9 RID: 713
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

		// Token: 0x040002CA RID: 714
		public static readonly ValidationRule<IEdmEnumType> EnumMustHaveIntegerUnderlyingType = new ValidationRule<IEdmEnumType>(delegate(ValidationContext context, IEdmEnumType enumType)
		{
			if (!enumType.UnderlyingType.PrimitiveKind.IsIntegral() && !context.IsBad(enumType.UnderlyingType))
			{
				context.AddError(enumType.Location(), EdmErrorCode.EnumMustHaveIntegerUnderlyingType, Strings.EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType(enumType.FullName()));
			}
		});

		// Token: 0x040002CB RID: 715
		public static readonly ValidationRule<IEdmEnumMember> EnumMemberValueMustHaveSameTypeAsUnderlyingType = new ValidationRule<IEdmEnumMember>(delegate(ValidationContext context, IEdmEnumMember enumMember)
		{
			if (!context.IsBad(enumMember.DeclaringType) && !context.IsBad(enumMember.DeclaringType.UnderlyingType))
			{
				IEdmPrimitiveValue edmPrimitiveValue = new EdmIntegerConstant(enumMember.Value.Value);
				IEnumerable<EdmError> enumerable;
				if (!edmPrimitiveValue.TryCastPrimitiveAsType(enumMember.DeclaringType.UnderlyingType.GetPrimitiveTypeReference(false), out enumerable))
				{
					context.AddError(enumMember.Location(), EdmErrorCode.EnumMemberValueOutOfRange, Strings.EdmModel_Validator_Semantic_EnumMemberValueOutOfRange(enumMember.Name));
				}
			}
		});

		// Token: 0x040002CC RID: 716
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

		// Token: 0x040002CD RID: 717
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

		// Token: 0x040002CE RID: 718
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

		// Token: 0x040002CF RID: 719
		public static readonly ValidationRule<IEdmEntityType> EntityTypeInvalidKeyKeyDefinedInBaseClass = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.BaseType != null && entityType.DeclaredKey != null && entityType.BaseType.TypeKind == EdmTypeKind.Entity && entityType.BaseEntityType().DeclaredKey != null)
			{
				context.AddError(entityType.Location(), EdmErrorCode.InvalidKey, Strings.EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass(entityType.Name, entityType.BaseEntityType().Name));
			}
		});

		// Token: 0x040002D0 RID: 720
		public static readonly ValidationRule<IEdmEntityType> EntityTypeKeyMissingOnEntityType = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			IEnumerable<IEdmStructuralProperty> enumerable2 = entityType.Key();
			if ((enumerable2 == null || !Enumerable.Any<IEdmStructuralProperty>(enumerable2)) && entityType.BaseType == null && !entityType.IsAbstract)
			{
				context.AddError(entityType.Location(), EdmErrorCode.KeyMissingOnEntityType, Strings.EdmModel_Validator_Semantic_KeyMissingOnEntityType(entityType.Name));
			}
		});

		// Token: 0x040002D1 RID: 721
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

		// Token: 0x040002D2 RID: 722
		public static readonly ValidationRule<IEdmEntityReferenceType> EntityReferenceTypeInaccessibleEntityType = new ValidationRule<IEdmEntityReferenceType>(delegate(ValidationContext context, IEdmEntityReferenceType entityReferenceType)
		{
			if (!context.IsBad(entityReferenceType.EntityType))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, entityReferenceType.EntityType, entityReferenceType.Location());
			}
		});

		// Token: 0x040002D3 RID: 723
		public static readonly ValidationRule<IEdmType> TypeMustNotHaveKindOfNone = new ValidationRule<IEdmType>(delegate(ValidationContext context, IEdmType type)
		{
			if (type.TypeKind == EdmTypeKind.None && !context.IsBad(type))
			{
				context.AddError(type.Location(), EdmErrorCode.TypeMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone);
			}
		});

		// Token: 0x040002D4 RID: 724
		public static readonly ValidationRule<IEdmPrimitiveType> PrimitiveTypeMustNotHaveKindOfNone = new ValidationRule<IEdmPrimitiveType>(delegate(ValidationContext context, IEdmPrimitiveType type)
		{
			if (type.PrimitiveKind == EdmPrimitiveTypeKind.None && !context.IsBad(type))
			{
				context.AddError(type.Location(), EdmErrorCode.PrimitiveTypeMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone(type.FullName()));
			}
		});

		// Token: 0x040002D5 RID: 725
		public static readonly ValidationRule<IEdmComplexType> OpenComplexTypeCannotHaveClosedDerivedComplexType = new ValidationRule<IEdmComplexType>(delegate(ValidationContext context, IEdmComplexType complexType)
		{
			if (complexType.BaseType != null && complexType.BaseType.IsOpen && !complexType.IsOpen)
			{
				context.AddError(complexType.Location(), EdmErrorCode.InvalidAbstractComplexType, Strings.EdmModel_Validator_Semantic_BaseTypeOfOpenTypeMustBeOpen(complexType.FullName()));
			}
		});

		// Token: 0x040002D6 RID: 726
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
			if (edmType.TypeKind != EdmTypeKind.Primitive && edmType.TypeKind != EdmTypeKind.Enum && edmType.TypeKind != EdmTypeKind.Untyped && edmType.TypeKind != EdmTypeKind.Complex && !context.IsBad(edmType))
			{
				context.AddError(property.Location(), EdmErrorCode.InvalidPropertyType, Strings.EdmModel_Validator_Semantic_InvalidPropertyType(property.Type.TypeKind().ToString()));
			}
		});

		// Token: 0x040002D7 RID: 727
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyInvalidOperationMultipleEndsInAssociatedNavigationProperties = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			if (navigationProperty.OnDelete != EdmOnDeleteAction.None && navigationProperty.Partner != null && navigationProperty.Partner.OnDelete != EdmOnDeleteAction.None)
			{
				context.AddError(navigationProperty.Location(), EdmErrorCode.InvalidAction, Strings.EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation);
			}
		});

		// Token: 0x040002D8 RID: 728
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyCorrectType = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.ToEntityType() == null)
			{
				context.AddError(property.Location(), EdmErrorCode.InvalidNavigationPropertyType, Strings.EdmModel_Validator_Semantic_InvalidNavigationPropertyType(property.Name));
				return;
			}
			if (property.Partner == null || property.Partner is BadNavigationProperty || property.Partner.DeclaringType is IEdmComplexType)
			{
				return;
			}
			if (property.ToEntityType() != property.Partner.DeclaringEntityType())
			{
				context.AddError(property.Location(), EdmErrorCode.InvalidNavigationPropertyType, Strings.EdmModel_Validator_Semantic_InvalidNavigationPropertyType(property.Name));
			}
		});

		// Token: 0x040002D9 RID: 729
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

		// Token: 0x040002DA RID: 730
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

		// Token: 0x040002DB RID: 731
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

		// Token: 0x040002DC RID: 732
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

		// Token: 0x040002DD RID: 733
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyEndWithManyMultiplicityCannotHaveOperationsSpecified = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty end)
		{
			if (end.Partner != null && end.Partner.Type.IsCollection() && end.OnDelete != EdmOnDeleteAction.None)
			{
				string text5 = Strings.EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(end.Name);
				context.AddError(end.Location(), EdmErrorCode.EndWithManyMultiplicityCannotHaveOperationsSpecified, text5);
			}
		});

		// Token: 0x040002DE RID: 734
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyPartnerPathShouldBeResolvable = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			IEdmPathExpression partnerPath = property.GetPartnerPath();
			if (partnerPath != null && property.Type.Definition.AsElementType() is IEdmEntityType && CsdlSemanticsNavigationProperty.ResolvePartnerPath((IEdmEntityType)property.Type.Definition.AsElementType(), partnerPath, context.Model) == null)
			{
				context.AddError(property.Location(), EdmErrorCode.UnresolvedNavigationPropertyPartnerPath, string.Format(CultureInfo.CurrentCulture, "Cannot resolve partner path for navigation property '{0}'.", new object[] { property.Name }));
			}
		});

		// Token: 0x040002DF RID: 735
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyWithRecursiveContainmentTargetMustBeOptional = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.ContainsTarget && property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && !property.Type.IsCollection() && !property.Type.IsNullable)
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyWithRecursiveContainmentTargetMustBeOptional, Strings.EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional(property.Name));
			}
		});

		// Token: 0x040002E0 RID: 736
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.Partner != null && property.ContainsTarget && property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && (property.Partner.Type.IsCollection() || !property.Partner.Type.IsNullable))
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne, Strings.EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne(property.Name));
			}
		});

		// Token: 0x040002E1 RID: 737
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.Partner != null && property.ContainsTarget && !property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && (property.Partner.Type.IsCollection() || property.Partner.Type.IsNullable))
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne, Strings.EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne(property.Name));
			}
		});

		// Token: 0x040002E2 RID: 738
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyEntityMustNotIndirectlyContainItself = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.ContainsTarget && !property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && ValidationHelper.TypeIndirectlyContainsTarget(property.ToEntityType(), property.DeclaringEntityType(), new HashSetInternal<IEdmEntityType>(), context.Model))
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyEntityMustNotIndirectlyContainItself, Strings.EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself(property.Name));
			}
		});

		// Token: 0x040002E3 RID: 739
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

		// Token: 0x040002E4 RID: 740
		public static readonly ValidationRule<IEdmProperty> PropertyMustNotHaveKindOfNone = new ValidationRule<IEdmProperty>(delegate(ValidationContext context, IEdmProperty property)
		{
			if (property.PropertyKind == EdmPropertyKind.None && !context.IsBad(property))
			{
				context.AddError(property.Location(), EdmErrorCode.PropertyMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone(property.Name));
			}
		});

		// Token: 0x040002E5 RID: 741
		public static readonly ValidationRule<IEdmOperationImport> OperationImportCannotImportBoundOperation = new ValidationRule<IEdmOperationImport>(delegate(ValidationContext context, IEdmOperationImport operationImport)
		{
			if (operationImport.Operation.IsBound)
			{
				context.AddError(operationImport.Location(), EdmErrorCode.OperationImportCannotImportBoundOperation, Strings.EdmModel_Validator_Semantic_OperationImportCannotImportBoundOperation(operationImport.Name, operationImport.Operation.Name));
			}
		});

		// Token: 0x040002E6 RID: 742
		public static readonly ValidationRule<IEdmOperationImport> OperationImportEntitySetExpressionIsInvalid = new ValidationRule<IEdmOperationImport>(delegate(ValidationContext context, IEdmOperationImport operationImport)
		{
			if (operationImport.EntitySet != null)
			{
				if (operationImport.EntitySet.ExpressionKind != EdmExpressionKind.Path)
				{
					context.AddError(operationImport.Location(), EdmErrorCode.OperationImportEntitySetExpressionIsInvalid, Strings.EdmModel_Validator_Semantic_OperationImportEntitySetExpressionKindIsInvalid(operationImport.Name, operationImport.EntitySet.ExpressionKind));
					return;
				}
				IEdmEntitySetBase edmEntitySetBase;
				if (!operationImport.TryGetStaticEntitySet(context.Model, out edmEntitySetBase))
				{
					context.AddError(operationImport.Location(), EdmErrorCode.OperationImportEntitySetExpressionIsInvalid, Strings.EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid(operationImport.Name));
					return;
				}
				if (!context.IsBad(edmEntitySetBase) && operationImport.Container.FindEntitySetExtended(edmEntitySetBase.Name) == null)
				{
					context.AddError(operationImport.Location(), EdmErrorCode.OperationImportEntitySetExpressionIsInvalid, Strings.EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid(operationImport.Name));
				}
			}
		});

		// Token: 0x040002E7 RID: 743
		public static readonly ValidationRule<IEdmOperationImport> OperationImportEntityTypeDoesNotMatchEntitySet = new ValidationRule<IEdmOperationImport>(delegate(ValidationContext context, IEdmOperationImport operationImport)
		{
			if (operationImport.EntitySet != null && operationImport.Operation.ReturnType != null)
			{
				IEdmTypeReference edmTypeReference = (operationImport.Operation.ReturnType.IsCollection() ? operationImport.Operation.ReturnType.AsCollection().ElementType() : operationImport.Operation.ReturnType);
				if (edmTypeReference.IsEntity())
				{
					IEdmEntityType edmEntityType4 = edmTypeReference.AsEntity().EntityDefinition();
					IEdmEntitySetBase edmEntitySetBase2;
					IEdmOperationParameter edmOperationParameter;
					Dictionary<IEdmNavigationProperty, IEdmPathExpression> dictionary;
					IEnumerable<EdmError> enumerable9;
					if (operationImport.TryGetStaticEntitySet(context.Model, out edmEntitySetBase2))
					{
						IEdmEntityType edmEntityType5 = edmEntitySetBase2.EntityType();
						if (!edmEntityType4.IsOrInheritsFrom(edmEntityType5) && !context.IsBad(edmEntityType4) && !context.IsBad(edmEntitySetBase2) && !context.IsBad(edmEntityType5))
						{
							string text7 = Strings.EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet(operationImport.Name, edmEntityType4.FullName(), edmEntitySetBase2.Name);
							context.AddError(operationImport.Location(), EdmErrorCode.OperationImportEntityTypeDoesNotMatchEntitySet, text7);
							return;
						}
					}
					else if (operationImport.TryGetRelativeEntitySetPath(context.Model, out edmOperationParameter, out dictionary, out enumerable9))
					{
						List<IEdmNavigationProperty> list2 = Enumerable.ToList<IEdmNavigationProperty>(Enumerable.Select<KeyValuePair<IEdmNavigationProperty, IEdmPathExpression>, IEdmNavigationProperty>(dictionary, (KeyValuePair<IEdmNavigationProperty, IEdmPathExpression> s) => s.Key));
						IEdmTypeReference edmTypeReference2 = ((list2.Count == 0) ? edmOperationParameter.Type : Enumerable.Last<KeyValuePair<IEdmNavigationProperty, IEdmPathExpression>>(dictionary).Key.Type);
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

		// Token: 0x040002E8 RID: 744
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportWithParameterShouldNotBeIncludedInServiceDocument = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (functionImport.IncludeInServiceDocument && Enumerable.Any<IEdmOperationParameter>(functionImport.Function.Parameters))
			{
				context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportWithParameterShouldNotBeIncludedInServiceDocument, Strings.EdmModel_Validator_Semantic_FunctionImportWithParameterShouldNotBeIncludedInServiceDocument(functionImport.Name));
			}
		});

		// Token: 0x040002E9 RID: 745
		public static readonly ValidationRule<IEdmFunction> FunctionMustHaveReturnType = new ValidationRule<IEdmFunction>(delegate(ValidationContext context, IEdmFunction function)
		{
			if (function.ReturnType == null)
			{
				context.AddError(function.Location(), EdmErrorCode.FunctionMustHaveReturnType, Strings.EdmModel_Validator_Semantic_FunctionMustHaveReturnType(function.Name));
			}
		});

		// Token: 0x040002EA RID: 746
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

		// Token: 0x040002EB RID: 747
		public static readonly ValidationRule<IEdmOperation> OperationParameterNameAlreadyDefinedDuplicate = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			HashSetInternal<string> hashSetInternal10 = new HashSetInternal<string>();
			if (operation.Parameters != null)
			{
				foreach (IEdmOperationParameter edmOperationParameter2 in operation.Parameters)
				{
					ValidationHelper.AddMemberNameToHashSet(edmOperationParameter2, hashSetInternal10, context, EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate(edmOperationParameter2.Name), false);
				}
			}
		});

		// Token: 0x040002EC RID: 748
		public static readonly ValidationRule<IEdmOperation> BoundOperationMustHaveParameters = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			if (operation.IsBound)
			{
				if (!Enumerable.Any<IEdmOperationParameter>(operation.Parameters, (IEdmOperationParameter p) => !(p is IEdmOptionalParameter)))
				{
					context.AddError(operation.Location(), EdmErrorCode.BoundOperationMustHaveParameters, Strings.EdmModel_Validator_Semantic_BoundOperationMustHaveParameters(operation.Name));
				}
			}
		});

		// Token: 0x040002ED RID: 749
		public static readonly ValidationRule<IEdmOperation> OptionalParametersMustComeAfterRequiredParameters = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			bool flag4 = false;
			foreach (IEdmOperationParameter edmOperationParameter3 in operation.Parameters)
			{
				if (edmOperationParameter3 is IEdmOptionalParameter)
				{
					flag4 = true;
				}
				else if (flag4)
				{
					context.AddError(operation.Location(), EdmErrorCode.RequiredParametersMustPrecedeOptional, Strings.EdmModel_Validator_Semantic_RequiredParametersMustPrecedeOptional(edmOperationParameter3.Name));
				}
			}
		});

		// Token: 0x040002EE RID: 750
		public static readonly ValidationRule<IEdmOperation> OperationEntitySetPathMustBeValid = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			IEdmOperationParameter edmOperationParameter4 = null;
			Dictionary<IEdmNavigationProperty, IEdmPathExpression> dictionary2 = null;
			IEdmEntityType edmEntityType6 = null;
			IEnumerable<EdmError> enumerable10 = null;
			operation.TryGetRelativeEntitySetPath(context.Model, out edmOperationParameter4, out dictionary2, out edmEntityType6, out enumerable10);
			foreach (EdmError edmError in enumerable10)
			{
				context.AddError(edmError);
			}
		});

		// Token: 0x040002EF RID: 751
		public static readonly ValidationRule<IEdmOperation> OperationReturnTypeEntityTypeMustBeValid = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			IEdmOperationParameter edmOperationParameter5 = null;
			Dictionary<IEdmNavigationProperty, IEdmPathExpression> dictionary3 = null;
			IEdmEntityType edmEntityType7 = null;
			IEnumerable<EdmError> enumerable11 = null;
			if (!operation.TryGetRelativeEntitySetPath(context.Model, out edmOperationParameter5, out dictionary3, out edmEntityType7, out enumerable11))
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
				bool flag5 = operation.ReturnType.IsEntity();
				if (edmCollectionType2 != null)
				{
					flag5 = edmCollectionType2.ElementType.IsEntity();
				}
				if (!flag5 || context.IsBad(edmEntityType8))
				{
					context.AddError(operation.Location(), EdmErrorCode.OperationWithEntitySetPathReturnTypeInvalid, Strings.EdmModel_Validator_Semantic_OperationWithEntitySetPathReturnTypeInvalid(operation.Name));
				}
				IEdmNavigationProperty edmNavigationProperty = null;
				if (dictionary3 != null)
				{
					edmNavigationProperty = Enumerable.LastOrDefault<KeyValuePair<IEdmNavigationProperty, IEdmPathExpression>>(dictionary3).Key;
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

		// Token: 0x040002F0 RID: 752
		public static readonly ValidationRule<IEdmTypeReference> TypeReferenceInaccessibleSchemaType = new ValidationRule<IEdmTypeReference>(delegate(ValidationContext context, IEdmTypeReference typeReference)
		{
			IEdmSchemaType edmSchemaType3 = typeReference.Definition as IEdmSchemaType;
			if (edmSchemaType3 != null && !context.IsBad(edmSchemaType3))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, edmSchemaType3, typeReference.Location());
			}
		});

		// Token: 0x040002F1 RID: 753
		public static readonly ValidationRule<IEdmDecimalTypeReference> DecimalTypeReferenceScaleOutOfRange = new ValidationRule<IEdmDecimalTypeReference>(delegate(ValidationContext context, IEdmDecimalTypeReference type)
		{
			if (type.Scale > type.Precision || type.Scale < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.ScaleOutOfRange, Strings.EdmModel_Validator_Semantic_ScaleOutOfRange);
			}
		});

		// Token: 0x040002F2 RID: 754
		public static readonly ValidationRule<IEdmDecimalTypeReference> DecimalTypeReferencePrecisionOutOfRange = new ValidationRule<IEdmDecimalTypeReference>(delegate(ValidationContext context, IEdmDecimalTypeReference type)
		{
			if (type.Precision > 2147483647 || type.Precision < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.PrecisionOutOfRange, Strings.EdmModel_Validator_Semantic_PrecisionOutOfRange);
			}
		});

		// Token: 0x040002F3 RID: 755
		public static readonly ValidationRule<IEdmStringTypeReference> StringTypeReferenceStringMaxLengthNegative = new ValidationRule<IEdmStringTypeReference>(delegate(ValidationContext context, IEdmStringTypeReference type)
		{
			if (type.MaxLength < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.MaxLengthOutOfRange, Strings.EdmModel_Validator_Semantic_StringMaxLengthOutOfRange);
			}
		});

		// Token: 0x040002F4 RID: 756
		public static readonly ValidationRule<IEdmStringTypeReference> StringTypeReferenceStringUnboundedNotValidForMaxLength = new ValidationRule<IEdmStringTypeReference>(delegate(ValidationContext context, IEdmStringTypeReference type)
		{
			if (type.MaxLength != null && type.IsUnbounded)
			{
				context.AddError(type.Location(), EdmErrorCode.IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull, Strings.EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull);
			}
		});

		// Token: 0x040002F5 RID: 757
		public static readonly ValidationRule<IEdmBinaryTypeReference> BinaryTypeReferenceBinaryMaxLengthNegative = new ValidationRule<IEdmBinaryTypeReference>(delegate(ValidationContext context, IEdmBinaryTypeReference type)
		{
			if (type.MaxLength < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.MaxLengthOutOfRange, Strings.EdmModel_Validator_Semantic_MaxLengthOutOfRange);
			}
		});

		// Token: 0x040002F6 RID: 758
		public static readonly ValidationRule<IEdmBinaryTypeReference> BinaryTypeReferenceBinaryUnboundedNotValidForMaxLength = new ValidationRule<IEdmBinaryTypeReference>(delegate(ValidationContext context, IEdmBinaryTypeReference type)
		{
			if (type.MaxLength != null && type.IsUnbounded)
			{
				context.AddError(type.Location(), EdmErrorCode.IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull, Strings.EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull);
			}
		});

		// Token: 0x040002F7 RID: 759
		public static readonly ValidationRule<IEdmTemporalTypeReference> TemporalTypeReferencePrecisionOutOfRange = new ValidationRule<IEdmTemporalTypeReference>(delegate(ValidationContext context, IEdmTemporalTypeReference type)
		{
			if (type.Precision > 2147483647 || type.Precision < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.PrecisionOutOfRange, Strings.EdmModel_Validator_Semantic_PrecisionOutOfRange);
			}
		});

		// Token: 0x040002F8 RID: 760
		public static readonly ValidationRule<IEdmModel> ModelDuplicateSchemaElementName = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			HashSetInternal<string> hashSetInternal11 = new HashSetInternal<string>();
			DuplicateOperationValidator duplicateOperationValidator = new DuplicateOperationValidator(context);
			HashSetInternal<string> hashSetInternal12 = new HashSetInternal<string>();
			foreach (IEdmSchemaElement edmSchemaElement in model.SchemaElements)
			{
				bool flag6 = false;
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
						flag6 = true;
					}
					duplicateOperationValidator.ValidateNotDuplicate(edmOperation, false);
					if (!flag6)
					{
						flag6 = model.OperationOrNameExistsInReferencedModel(edmOperation, text8);
					}
				}
				else
				{
					flag6 = !hashSetInternal11.Add(text8) || hashSetInternal12.Contains(text8) || model.ItemExistsInReferencedModel(text8, true);
				}
				if (flag6)
				{
					context.AddError(edmSchemaElement.Location(), EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined(text8));
				}
			}
		});

		// Token: 0x040002F9 RID: 761
		public static readonly ValidationRule<IEdmModel> ModelDuplicateEntityContainerName = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			HashSetInternal<string> hashSetInternal13 = new HashSetInternal<string>();
			IEdmEntityContainer entityContainer = model.EntityContainer;
			if (entityContainer != null)
			{
				ValidationHelper.AddMemberNameToHashSet(entityContainer, hashSetInternal13, context, EdmErrorCode.DuplicateEntityContainerName, Strings.EdmModel_Validator_Semantic_DuplicateEntityContainerName(entityContainer.Name), false);
			}
		});

		// Token: 0x040002FA RID: 762
		public static readonly ValidationRule<IEdmModel> ModelBoundFunctionOverloadsMustHaveSameReturnType = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			foreach (IGrouping<string, IEdmFunction> grouping in Enumerable.GroupBy<IEdmFunction, string>(Enumerable.Where<IEdmFunction>(Enumerable.OfType<IEdmFunction>(model.SchemaElements), (IEdmFunction f) => f.IsBound), (IEdmFunction f2) => f2.FullName()))
			{
				Dictionary<IEdmTypeReference, IEdmTypeReference> dictionary4 = new Dictionary<IEdmTypeReference, IEdmTypeReference>(new ValidationRules.EdmTypeReferenceComparer());
				foreach (IEdmFunction edmFunction in grouping)
				{
					if (Enumerable.Any<IEdmOperationParameter>(edmFunction.Parameters) && edmFunction.ReturnType != null)
					{
						IEdmOperationParameter edmOperationParameter6 = Enumerable.First<IEdmOperationParameter>(edmFunction.Parameters);
						if (!dictionary4.ContainsKey(edmOperationParameter6.Type))
						{
							dictionary4.Add(edmOperationParameter6.Type, edmFunction.ReturnType);
						}
						else
						{
							IEdmTypeReference edmTypeReference5 = dictionary4[edmOperationParameter6.Type];
							if (!edmFunction.ReturnType.IsEquivalentTo(edmTypeReference5))
							{
								context.AddError(edmFunction.Location(), EdmErrorCode.BoundFunctionOverloadsMustHaveSameReturnType, Strings.EdmModel_Validator_Semantic_BoundFunctionOverloadsMustHaveSameReturnType(edmFunction.Name, edmTypeReference5.FullName()));
							}
						}
					}
				}
			}
		});

		// Token: 0x040002FB RID: 763
		public static readonly ValidationRule<IEdmModel> UnBoundFunctionOverloadsMustHaveIdenticalReturnTypes = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			Dictionary<string, IEdmTypeReference> dictionary5 = new Dictionary<string, IEdmTypeReference>();
			foreach (IEdmFunction edmFunction2 in Enumerable.Where<IEdmFunction>(Enumerable.OfType<IEdmFunction>(model.SchemaElements), (IEdmFunction f) => !f.IsBound))
			{
				if (!dictionary5.ContainsKey(edmFunction2.Name))
				{
					dictionary5.Add(edmFunction2.Name, edmFunction2.ReturnType);
				}
				else if (!edmFunction2.ReturnType.IsEquivalentTo(dictionary5[edmFunction2.Name]))
				{
					context.AddError(edmFunction2.Location(), EdmErrorCode.UnboundFunctionOverloadHasIncorrectReturnType, Strings.EdmModel_Validator_Semantic_UnboundFunctionOverloadHasIncorrectReturnType(edmFunction2.Name));
				}
			}
		});

		// Token: 0x040002FC RID: 764
		public static readonly ValidationRule<IEdmDirectValueAnnotation> ImmediateValueAnnotationElementAnnotationIsValid = new ValidationRule<IEdmDirectValueAnnotation>(delegate(ValidationContext context, IEdmDirectValueAnnotation annotation)
		{
			IEdmStringValue edmStringValue = annotation.Value as IEdmStringValue;
			if (edmStringValue != null && edmStringValue.IsSerializedAsElement(context.Model) && (EdmUtil.IsNullOrWhiteSpaceInternal(annotation.NamespaceUri) || EdmUtil.IsNullOrWhiteSpaceInternal(annotation.Name)))
			{
				context.AddError(annotation.Location(), EdmErrorCode.InvalidElementAnnotation, Strings.EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm);
			}
		});

		// Token: 0x040002FD RID: 765
		public static readonly ValidationRule<IEdmDirectValueAnnotation> ImmediateValueAnnotationElementAnnotationHasNameAndNamespace = new ValidationRule<IEdmDirectValueAnnotation>(delegate(ValidationContext context, IEdmDirectValueAnnotation annotation)
		{
			IEdmStringValue edmStringValue2 = annotation.Value as IEdmStringValue;
			EdmError edmError2;
			if (edmStringValue2 != null && edmStringValue2.IsSerializedAsElement(context.Model) && !ValidationHelper.ValidateValueCanBeWrittenAsXmlElementAnnotation(edmStringValue2, annotation.NamespaceUri, annotation.Name, out edmError2))
			{
				context.AddError(edmError2);
			}
		});

		// Token: 0x040002FE RID: 766
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

		// Token: 0x040002FF RID: 767
		public static readonly ValidationRule<IEdmVocabularyAnnotation> VocabularyAnnotationInaccessibleTarget = new ValidationRule<IEdmVocabularyAnnotation>(delegate(ValidationContext context, IEdmVocabularyAnnotation annotation)
		{
			IEdmVocabularyAnnotatable target = annotation.Target;
			bool flag7 = false;
			IEdmEntityContainer edmEntityContainer = target as IEdmEntityContainer;
			if (edmEntityContainer != null)
			{
				flag7 = context.Model.FindEntityContainer(edmEntityContainer.FullName()) != null;
			}
			else
			{
				IEdmEntitySet edmEntitySet = target as IEdmEntitySet;
				if (edmEntitySet != null)
				{
					IEdmEntityContainer container = edmEntitySet.Container;
					if (container != null)
					{
						flag7 = container.FindEntitySetExtended(edmEntitySet.Name) != null;
					}
				}
				else
				{
					IEdmSchemaType edmSchemaType4 = target as IEdmSchemaType;
					if (edmSchemaType4 != null)
					{
						flag7 = context.Model.FindType(edmSchemaType4.FullName()) != null;
					}
					else
					{
						IEdmTerm edmTerm = target as IEdmTerm;
						if (edmTerm != null)
						{
							flag7 = context.Model.FindTerm(edmTerm.FullName()) != null;
						}
						else
						{
							IEdmOperation edmOperation2 = target as IEdmOperation;
							if (edmOperation2 != null)
							{
								flag7 = Enumerable.Any<IEdmOperation>(context.Model.FindOperations(edmOperation2.FullName()));
							}
							else
							{
								IEdmOperationImport edmOperationImport2 = target as IEdmOperationImport;
								if (edmOperationImport2 != null)
								{
									flag7 = Enumerable.Any<IEdmOperationImport>(edmOperationImport2.Container.FindOperationImportsExtended(edmOperationImport2.Name));
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
											flag7 = edmStructuredType.FindProperty(edmProperty5.Name) != null;
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
												using (IEnumerator<IEdmOperation> enumerator27 = context.Model.FindOperations(declaringOperation.FullName()).GetEnumerator())
												{
													while (enumerator27.MoveNext())
													{
														IEdmOperation edmOperation3 = enumerator27.Current;
														if (edmOperation3.FindParameter(edmOperationParameter7.Name) != null)
														{
															flag7 = true;
															break;
														}
													}
													goto IL_0229;
												}
											}
											IEdmOperationImport edmOperationImport3 = edmOperationParameter7.DeclaringOperation as IEdmOperationImport;
											if (edmOperationImport3 == null)
											{
												goto IL_0229;
											}
											IEdmEntityContainer container2 = edmOperationImport3.Container;
											using (IEnumerator<IEdmOperationImport> enumerator28 = container2.FindOperationImportsExtended(edmOperationImport3.Name).GetEnumerator())
											{
												while (enumerator28.MoveNext())
												{
													IEdmOperationImport edmOperationImport4 = enumerator28.Current;
													if (edmOperationImport4.Operation.FindParameter(edmOperationParameter7.Name) != null)
													{
														flag7 = true;
														break;
													}
												}
												goto IL_0229;
											}
										}
										flag7 = true;
									}
								}
							}
						}
					}
				}
			}
			IL_0229:
			if (!flag7)
			{
				context.AddError(annotation.Location(), EdmErrorCode.BadUnresolvedTarget, Strings.EdmModel_Validator_Semantic_InaccessibleTarget(EdmUtil.FullyQualifiedName(target)));
			}
		});

		// Token: 0x04000300 RID: 768
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
		public static readonly ValidationRule<IEdmVocabularyAnnotation> VocabularyAnnotationAssertCorrectExpressionType = new ValidationRule<IEdmVocabularyAnnotation>(delegate(ValidationContext context, IEdmVocabularyAnnotation annotation)
		{
			IEnumerable<EdmError> enumerable12;
			if (!annotation.Value.TryCast(annotation.Term.Type, out enumerable12))
			{
				foreach (EdmError edmError3 in enumerable12)
				{
					if (edmError3.ErrorCode != EdmErrorCode.RecordExpressionMissingRequiredProperty)
					{
						context.AddError(edmError3);
					}
				}
			}
		});

		// Token: 0x04000301 RID: 769
		public static readonly ValidationRule<IEdmVocabularyAnnotation> AnnotationInaccessibleTerm = new ValidationRule<IEdmVocabularyAnnotation>(delegate(ValidationContext context, IEdmVocabularyAnnotation annotation)
		{
			IEdmTerm term = annotation.Term;
			if (!(term is IUnresolvedElement) && context.Model.FindTerm(term.FullName()) == null)
			{
				context.AddError(annotation.Location(), EdmErrorCode.BadUnresolvedTerm, Strings.EdmModel_Validator_Semantic_InaccessibleTerm(annotation.Term.FullName()));
			}
		});

		// Token: 0x04000302 RID: 770
		public static readonly ValidationRule<IEdmPropertyValueBinding> PropertyValueBindingValueIsCorrectType = new ValidationRule<IEdmPropertyValueBinding>(delegate(ValidationContext context, IEdmPropertyValueBinding binding)
		{
			IEnumerable<EdmError> enumerable13;
			if (!binding.Value.TryCast(binding.BoundProperty.Type, out enumerable13) && !context.IsBad(binding) && !context.IsBad(binding.BoundProperty))
			{
				foreach (EdmError edmError4 in enumerable13)
				{
					context.AddError(edmError4);
				}
			}
		});

		// Token: 0x04000303 RID: 771
		public static readonly ValidationRule<IEdmIfExpression> IfExpressionAssertCorrectTestType = new ValidationRule<IEdmIfExpression>(delegate(ValidationContext context, IEdmIfExpression expression)
		{
			IEnumerable<EdmError> enumerable14;
			if (!expression.TestExpression.TryCast(EdmCoreModel.Instance.GetBoolean(false), out enumerable14))
			{
				foreach (EdmError edmError5 in enumerable14)
				{
					context.AddError(edmError5);
				}
			}
		});

		// Token: 0x04000304 RID: 772
		public static readonly ValidationRule<IEdmCollectionExpression> CollectionExpressionAllElementsCorrectType = new ValidationRule<IEdmCollectionExpression>(delegate(ValidationContext context, IEdmCollectionExpression expression)
		{
			if (expression.DeclaredType != null && !context.IsBad(expression) && !context.IsBad(expression.DeclaredType))
			{
				IEnumerable<EdmError> enumerable15;
				expression.TryCastCollectionAsType(expression.DeclaredType, null, false, out enumerable15);
				foreach (EdmError edmError6 in enumerable15)
				{
					context.AddError(edmError6);
				}
			}
		});

		// Token: 0x04000305 RID: 773
		public static readonly ValidationRule<IEdmRecordExpression> RecordExpressionPropertiesMatchType = new ValidationRule<IEdmRecordExpression>(delegate(ValidationContext context, IEdmRecordExpression expression)
		{
			if (expression.DeclaredType != null && !context.IsBad(expression) && !context.IsBad(expression.DeclaredType))
			{
				IEnumerable<EdmError> enumerable16;
				expression.TryCastRecordAsType(expression.DeclaredType, null, false, out enumerable16);
				foreach (EdmError edmError7 in enumerable16)
				{
					context.AddError(edmError7);
				}
			}
		});

		// Token: 0x04000306 RID: 774
		public static readonly ValidationRule<IEdmApplyExpression> FunctionApplicationExpressionParametersMatchAppliedFunction = new ValidationRule<IEdmApplyExpression>(delegate(ValidationContext context, IEdmApplyExpression expression)
		{
			IEdmFunction appliedFunction = expression.AppliedFunction;
			if (appliedFunction != null && !context.IsBad(appliedFunction))
			{
				if (Enumerable.Count<IEdmOperationParameter>(appliedFunction.Parameters) != Enumerable.Count<IEdmExpression>(expression.Arguments))
				{
					context.AddError(new EdmError(expression.Location(), EdmErrorCode.IncorrectNumberOfArguments, Strings.EdmModel_Validator_Semantic_IncorrectNumberOfArguments(Enumerable.Count<IEdmExpression>(expression.Arguments), appliedFunction.FullName(), Enumerable.Count<IEdmOperationParameter>(appliedFunction.Parameters))));
				}
				IEnumerator<IEdmExpression> enumerator34 = expression.Arguments.GetEnumerator();
				foreach (IEdmOperationParameter edmOperationParameter8 in appliedFunction.Parameters)
				{
					enumerator34.MoveNext();
					IEnumerable<EdmError> enumerable17;
					if (!enumerator34.Current.TryCast(edmOperationParameter8.Type, out enumerable17))
					{
						foreach (EdmError edmError8 in enumerable17)
						{
							context.AddError(edmError8);
						}
					}
				}
			}
		});

		// Token: 0x04000307 RID: 775
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

		// Token: 0x04000308 RID: 776
		public static readonly ValidationRule<IEdmPrimitiveValue> PrimitiveValueValidForType = new ValidationRule<IEdmPrimitiveValue>(delegate(ValidationContext context, IEdmPrimitiveValue value)
		{
			if (value.Type != null && !context.IsBad(value) && !context.IsBad(value.Type))
			{
				IEnumerable<EdmError> enumerable18;
				value.TryCastPrimitiveAsType(value.Type, out enumerable18);
				foreach (EdmError edmError9 in enumerable18)
				{
					context.AddError(edmError9);
				}
			}
		});

		// Token: 0x0200022A RID: 554
		internal class EdmTypeReferenceComparer : IEqualityComparer<IEdmTypeReference>
		{
			// Token: 0x06000E15 RID: 3605 RVA: 0x0002592F File Offset: 0x00023B2F
			public bool Equals(IEdmTypeReference x, IEdmTypeReference y)
			{
				return x.IsEquivalentTo(y);
			}

			// Token: 0x06000E16 RID: 3606 RVA: 0x00025938 File Offset: 0x00023B38
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
