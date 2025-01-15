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
	// Token: 0x02000146 RID: 326
	public static class ValidationRules
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x00014D6C File Offset: 0x00012F6C
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

		// Token: 0x0600083F RID: 2111 RVA: 0x00014DCC File Offset: 0x00012FCC
		private static bool TryResolveNavigationPropertyBindingPath(IEdmModel model, IEdmNavigationSource navigationSource, IEdmNavigationPropertyBinding binding)
		{
			string[] array = binding.Path.PathSegments.ToArray<string>();
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
			IEdmNavigationProperty edmNavigationProperty2 = edmStructuredType.FindProperty(array.Last<string>()) as IEdmNavigationProperty;
			return edmNavigationProperty2 != null;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00014E94 File Offset: 0x00013094
		private static bool HasPathTypeProperty(IEdmStructuredType structuredType, IList<IEdmStructuredType> visited)
		{
			if (structuredType == null || visited == null || visited.Any((IEdmStructuredType c) => c == structuredType))
			{
				return false;
			}
			visited.Add(structuredType);
			IEdmStructuredType baseType = structuredType.BaseType;
			if (baseType != null && ValidationRules.HasPathTypeProperty(baseType, visited))
			{
				return true;
			}
			foreach (IEdmProperty edmProperty in structuredType.DeclaredProperties)
			{
				IEdmTypeReference edmTypeReference = edmProperty.Type;
				if (edmTypeReference.IsCollection())
				{
					edmTypeReference = edmTypeReference.AsCollection().ElementType();
				}
				if (edmTypeReference.IsStructured())
				{
					if (ValidationRules.HasPathTypeProperty(edmTypeReference.AsStructured().StructuredDefinition(), visited))
					{
						return true;
					}
				}
				else if (edmTypeReference.IsPath())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000391 RID: 913
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

		// Token: 0x04000392 RID: 914
		public static readonly ValidationRule<IEdmNamedElement> NamedElementNameMustNotBeEmptyOrWhiteSpace = new ValidationRule<IEdmNamedElement>(delegate(ValidationContext context, IEdmNamedElement item)
		{
			if (EdmUtil.IsNullOrWhiteSpaceInternal(item.Name) || item.Name.Length == 0)
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidName, Strings.EdmModel_Validator_Syntactic_MissingName);
			}
		});

		// Token: 0x04000393 RID: 915
		public static readonly ValidationRule<IEdmNamedElement> NamedElementNameIsTooLong = new ValidationRule<IEdmNamedElement>(delegate(ValidationContext context, IEdmNamedElement item)
		{
			if (!EdmUtil.IsNullOrWhiteSpaceInternal(item.Name) && item.Name.Length > 480)
			{
				context.AddError(item.Location(), EdmErrorCode.NameTooLong, Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong(item.Name));
			}
		});

		// Token: 0x04000394 RID: 916
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

		// Token: 0x04000395 RID: 917
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementNamespaceMustNotBeEmptyOrWhiteSpace = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement item)
		{
			if (EdmUtil.IsNullOrWhiteSpaceInternal(item.Namespace) || item.Namespace.Length == 0)
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidNamespaceName, Strings.EdmModel_Validator_Syntactic_MissingNamespaceName);
			}
		});

		// Token: 0x04000396 RID: 918
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementNamespaceIsTooLong = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement item)
		{
			if (item.Namespace.Length > 512)
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidNamespaceName, Strings.EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong(item.Namespace));
			}
		});

		// Token: 0x04000397 RID: 919
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementNamespaceIsNotAllowed = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement item)
		{
			if (item.Namespace.Length <= 512 && item.Namespace.Length > 0 && !EdmUtil.IsNullOrWhiteSpaceInternal(item.Namespace) && !EdmUtil.IsValidDottedName(item.Namespace))
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidNamespaceName, Strings.EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed(item.Namespace));
			}
		});

		// Token: 0x04000398 RID: 920
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementSystemNamespaceEncountered = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement element)
		{
			if (ValidationHelper.IsEdmSystemNamespace(element.Namespace))
			{
				context.AddError(element.Location(), EdmErrorCode.SystemNamespaceEncountered, Strings.EdmModel_Validator_Semantic_SystemNamespaceEncountered(element.Namespace));
			}
		});

		// Token: 0x04000399 RID: 921
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementMustNotHaveKindOfNone = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement element)
		{
			if (element.SchemaElementKind == EdmSchemaElementKind.None && !context.IsBad(element))
			{
				context.AddError(element.Location(), EdmErrorCode.SchemaElementMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone(element.FullName()));
			}
		});

		// Token: 0x0400039A RID: 922
		public static readonly ValidationRule<IEdmEntityContainerElement> EntityContainerElementMustNotHaveKindOfNone = new ValidationRule<IEdmEntityContainerElement>(delegate(ValidationContext context, IEdmEntityContainerElement element)
		{
			if (element.ContainerElementKind == EdmContainerElementKind.None && !context.IsBad(element))
			{
				context.AddError(element.Location(), EdmErrorCode.EntityContainerElementMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone(element.Container.FullName() + "/" + element.Name));
			}
		});

		// Token: 0x0400039B RID: 923
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

		// Token: 0x0400039C RID: 924
		public static readonly ValidationRule<IEdmNavigationSource> NavigationSourceTypeHasNoKeys = new ValidationRule<IEdmNavigationSource>(delegate(ValidationContext context, IEdmNavigationSource navigationSource)
		{
			if (navigationSource.NavigationSourceKind() == EdmNavigationSourceKind.Singleton || navigationSource.NavigationSourceKind() == EdmNavigationSourceKind.None)
			{
				return;
			}
			if (navigationSource.EntityType() == null)
			{
				return;
			}
			if ((navigationSource.EntityType().Key() == null || !navigationSource.EntityType().Key().Any<IEdmStructuralProperty>()) && !context.IsBad(navigationSource.EntityType()))
			{
				string text2 = Strings.EdmModel_Validator_Semantic_NavigationSourceTypeHasNoKeys(navigationSource.Name, navigationSource.EntityType().Name);
				context.AddError(navigationSource.Location(), EdmErrorCode.NavigationSourceTypeHasNoKeys, text2);
			}
		});

		// Token: 0x0400039D RID: 925
		public static readonly ValidationRule<IEdmNavigationSource> NavigationSourceDeclaringTypeCannotHavePathTypeProperty = new ValidationRule<IEdmNavigationSource>(delegate(ValidationContext context, IEdmNavigationSource navigationSource)
		{
			IEdmEntityType edmEntityType = navigationSource.EntityType();
			if (edmEntityType == null)
			{
				return;
			}
			IList<IEdmStructuredType> list = new List<IEdmStructuredType>();
			if (ValidationRules.HasPathTypeProperty(edmEntityType, list))
			{
				string text3 = ((navigationSource is IEdmSingleton) ? "singleton" : "entity set");
				string text4 = Strings.EdmModel_Validator_Semantic_DeclaringTypeOfNavigationSourceCannotHavePathProperty(edmEntityType.FullName(), text3, navigationSource.Name);
				context.AddError(navigationSource.Location(), EdmErrorCode.DeclaringTypeOfNavigationSourceCannotHavePathProperty, text4);
			}
		});

		// Token: 0x0400039E RID: 926
		public static readonly ValidationRule<IEdmNavigationSource> NavigationSourceInaccessibleEntityType = new ValidationRule<IEdmNavigationSource>(delegate(ValidationContext context, IEdmNavigationSource navigationSource)
		{
			IEdmEntityType edmEntityType2 = navigationSource.EntityType();
			if (edmEntityType2 != null && !context.IsBad(edmEntityType2))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, edmEntityType2, navigationSource.Location());
			}
		});

		// Token: 0x0400039F RID: 927
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

		// Token: 0x040003A0 RID: 928
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

		// Token: 0x040003A1 RID: 929
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

		// Token: 0x040003A2 RID: 930
		public static readonly ValidationRule<IEdmEntitySet> EntitySetCanOnlyBeContainedByASingleNavigationProperty = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet set)
		{
			bool flag2 = false;
			foreach (IEdmNavigationSource edmNavigationSource in set.Container.Elements.OfType<IEdmNavigationSource>())
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

		// Token: 0x040003A3 RID: 931
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

		// Token: 0x040003A4 RID: 932
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

		// Token: 0x040003A5 RID: 933
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
				string text5 = Strings.EdmModel_Validator_Semantic_EntitySetTypeMustBeCollectionOfEntityType(entitySet.Type.FullTypeName(), entitySet.Name);
				context.AddError(entitySet.Location(), EdmErrorCode.EntitySetTypeMustBeCollectionOfEntityType, text5);
			}
		});

		// Token: 0x040003A6 RID: 934
		public static readonly ValidationRule<IEdmEntitySet> EntitySetTypeCannotBeEdmEntityType = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet entitySet)
		{
			if (entitySet.Type.AsElementType() == EdmCoreModelEntityType.Instance)
			{
				context.AddError(entitySet.Location(), EdmErrorCode.EntityTypeOfEntitySetCannotBeEdmEntityType, Strings.EdmModel_Validator_Semantic_EdmEntityTypeCannotBeTypeOfEntitySet(entitySet.Name));
			}
		});

		// Token: 0x040003A7 RID: 935
		public static readonly ValidationRule<IEdmSingleton> SingletonTypeMustBeEntityType = new ValidationRule<IEdmSingleton>(delegate(ValidationContext context, IEdmSingleton singleton)
		{
			if (!(singleton.Type is IEdmEntityType))
			{
				string text6 = Strings.EdmModel_Validator_Semantic_SingletonTypeMustBeEntityType(singleton.Type.FullTypeName(), singleton.Name);
				context.AddError(singleton.Location(), EdmErrorCode.SingletonTypeMustBeEntityType, text6);
			}
		});

		// Token: 0x040003A8 RID: 936
		public static readonly ValidationRule<IEdmSingleton> SingletonTypeCannotBeEdmEntityType = new ValidationRule<IEdmSingleton>(delegate(ValidationContext context, IEdmSingleton singleton)
		{
			if (singleton.Type == EdmCoreModelEntityType.Instance)
			{
				context.AddError(singleton.Location(), EdmErrorCode.EntityTypeOfSingletonCannotBeEdmEntityType, Strings.EdmModel_Validator_Semantic_EdmEntityTypeCannotBeTypeOfSingleton(singleton.Name));
			}
		});

		// Token: 0x040003A9 RID: 937
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypeInvalidMemberNameMatchesTypeName = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			IEdmSchemaType edmSchemaType = structuredType as IEdmSchemaType;
			if (edmSchemaType != null)
			{
				List<IEdmProperty> list2 = structuredType.Properties().ToList<IEdmProperty>();
				if (list2.Count > 0)
				{
					foreach (IEdmProperty edmProperty in list2)
					{
						if (edmProperty != null && edmProperty.Name.EqualsOrdinal(edmSchemaType.Name))
						{
							context.AddError(edmProperty.Location(), EdmErrorCode.BadProperty, Strings.EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(edmProperty.Name));
						}
					}
				}
			}
		});

		// Token: 0x040003AA RID: 938
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypePropertyNameAlreadyDefined = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			HashSetInternal<string> hashSetInternal6 = new HashSetInternal<string>();
			foreach (IEdmProperty edmProperty2 in structuredType.Properties())
			{
				if (edmProperty2 != null)
				{
					ValidationHelper.AddMemberNameToHashSet(edmProperty2, hashSetInternal6, context, EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_PropertyNameAlreadyDefined(edmProperty2.Name), !structuredType.DeclaredProperties.Contains(edmProperty2));
				}
			}
		});

		// Token: 0x040003AB RID: 939
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypeBaseTypeMustBeSameKindAsDerivedKind = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			if (structuredType is IEdmSchemaType && structuredType.BaseType != null && structuredType.BaseType.TypeKind != structuredType.TypeKind)
			{
				context.AddError(structuredType.Location(), (structuredType.TypeKind == EdmTypeKind.Entity) ? EdmErrorCode.EntityMustHaveEntityBaseType : EdmErrorCode.ComplexTypeMustHaveComplexBaseType, Strings.EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind);
			}
		});

		// Token: 0x040003AC RID: 940
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypeBaseTypeCannotBeAbstractType = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			if (structuredType.BaseType != null && (structuredType.BaseType == EdmCoreModelComplexType.Instance || structuredType.BaseType == EdmCoreModelEntityType.Instance) && !context.IsBad(structuredType.BaseType))
			{
				string text7 = ((structuredType.TypeKind == EdmTypeKind.Entity) ? "entity" : "complex");
				context.AddError(structuredType.Location(), (structuredType.TypeKind == EdmTypeKind.Entity) ? EdmErrorCode.EntityTypeBaseTypeCannotBeEdmEntityType : EdmErrorCode.ComplexTypeBaseTypeCannotBeEdmComplexType, Strings.EdmModel_Validator_Semantic_StructuredTypeBaseTypeCannotBeAbstractType(structuredType.BaseType.FullTypeName(), text7, structuredType.FullTypeName()));
			}
		});

		// Token: 0x040003AD RID: 941
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypeInaccessibleBaseType = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			IEdmSchemaType edmSchemaType2 = structuredType.BaseType as IEdmSchemaType;
			if (edmSchemaType2 != null && !context.IsBad(edmSchemaType2))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, edmSchemaType2, structuredType.Location());
			}
		});

		// Token: 0x040003AE RID: 942
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

		// Token: 0x040003AF RID: 943
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

		// Token: 0x040003B0 RID: 944
		public static readonly ValidationRule<IEdmEnumType> EnumMustHaveIntegerUnderlyingType = new ValidationRule<IEdmEnumType>(delegate(ValidationContext context, IEdmEnumType enumType)
		{
			if (!enumType.UnderlyingType.PrimitiveKind.IsIntegral() && !context.IsBad(enumType.UnderlyingType))
			{
				context.AddError(enumType.Location(), EdmErrorCode.EnumMustHaveIntegerUnderlyingType, Strings.EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType(enumType.FullName()));
			}
		});

		// Token: 0x040003B1 RID: 945
		public static readonly ValidationRule<IEdmEnumType> EnumUnderlyingTypeCannotBeEdmPrimitiveType = new ValidationRule<IEdmEnumType>(delegate(ValidationContext context, IEdmEnumType enumType)
		{
			if (enumType.UnderlyingType.PrimitiveKind == EdmPrimitiveTypeKind.PrimitiveType && !context.IsBad(enumType.UnderlyingType))
			{
				context.AddError(enumType.Location(), EdmErrorCode.TypeDefinitionUnderlyingTypeCannotBeEdmPrimitiveType, Strings.EdmModel_Validator_Semantic_EdmPrimitiveTypeCannotBeUsedAsUnderlyingType("enumeration", enumType.FullName()));
			}
		});

		// Token: 0x040003B2 RID: 946
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

		// Token: 0x040003B3 RID: 947
		public static readonly ValidationRule<IEdmTypeDefinition> TypeDefinitionUnderlyingTypeCannotBeEdmPrimitiveType = new ValidationRule<IEdmTypeDefinition>(delegate(ValidationContext context, IEdmTypeDefinition typeDefinition)
		{
			if (typeDefinition.UnderlyingType == EdmCoreModel.Instance.GetPrimitiveType() && !context.IsBad(typeDefinition.UnderlyingType))
			{
				context.AddError(typeDefinition.Location(), EdmErrorCode.TypeDefinitionUnderlyingTypeCannotBeEdmPrimitiveType, Strings.EdmModel_Validator_Semantic_EdmPrimitiveTypeCannotBeUsedAsUnderlyingType("type definition", typeDefinition.FullName()));
			}
		});

		// Token: 0x040003B4 RID: 948
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

		// Token: 0x040003B5 RID: 949
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

		// Token: 0x040003B6 RID: 950
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

		// Token: 0x040003B7 RID: 951
		public static readonly ValidationRule<IEdmEntityType> EntityTypeInvalidKeyKeyDefinedInBaseClass = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.BaseType != null && entityType.DeclaredKey != null && entityType.BaseType.TypeKind == EdmTypeKind.Entity && entityType.BaseEntityType().DeclaredKey != null)
			{
				context.AddError(entityType.Location(), EdmErrorCode.InvalidKey, Strings.EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass(entityType.Name, entityType.BaseEntityType().Name));
			}
		});

		// Token: 0x040003B8 RID: 952
		public static readonly ValidationRule<IEdmEntityType> EntityTypeKeyMissingOnEntityType = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			IEnumerable<IEdmStructuralProperty> enumerable2 = entityType.Key();
			if ((enumerable2 == null || !enumerable2.Any<IEdmStructuralProperty>()) && entityType.BaseType == null && !entityType.IsAbstract)
			{
				context.AddError(entityType.Location(), EdmErrorCode.KeyMissingOnEntityType, Strings.EdmModel_Validator_Semantic_KeyMissingOnEntityType(entityType.Name));
			}
		});

		// Token: 0x040003B9 RID: 953
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

		// Token: 0x040003BA RID: 954
		public static readonly ValidationRule<IEdmEntityType> EntityTypeKeyTypeCannotBeEdmPrimitiveType = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.DeclaredKey != null)
			{
				foreach (IEdmStructuralProperty edmStructuralProperty5 in entityType.DeclaredKey)
				{
					if (edmStructuralProperty5.Type.Definition == EdmCoreModel.Instance.GetPrimitiveType())
					{
						context.AddError(entityType.Location(), EdmErrorCode.KeyPropertyTypeCannotBeEdmPrimitiveType, Strings.EdmModel_Validator_Semantic_EdmPrimitiveTypeCannotBeUsedAsTypeOfKey(edmStructuralProperty5.Name, entityType.FullName()));
					}
				}
			}
		});

		// Token: 0x040003BB RID: 955
		public static readonly ValidationRule<IEdmEntityType> EntityTypeBoundEscapeFunctionMustBeUnique = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			IList<IEdmFunction> list3 = new List<IEdmFunction>();
			IList<IEdmFunction> list4 = new List<IEdmFunction>();
			foreach (IEdmFunction edmFunction in (from o in context.Model.FindBoundOperations(entityType)
				where o.IsFunction()
				select o).OfType<IEdmFunction>())
			{
				if (context.Model.IsUrlEscapeFunction(edmFunction))
				{
					if (edmFunction.IsComposable)
					{
						list3.Add(edmFunction);
					}
					else
					{
						list4.Add(edmFunction);
					}
				}
			}
			if (list3.Count<IEdmFunction>() > 1)
			{
				string text8 = string.Join(",", list3.Select((IEdmFunction c) => c.Name).ToArray<string>());
				context.AddError(entityType.Location(), EdmErrorCode.EntityComposableBoundEscapeFunctionMustBeLessOne, Strings.EdmModel_Validator_Semantic_EntityComposableBoundEscapeFunctionMustBeLessOne(entityType.FullName(), text8));
			}
			if (list4.Count<IEdmFunction>() > 1)
			{
				string text9 = string.Join(",", list4.Select((IEdmFunction c) => c.Name).ToArray<string>());
				context.AddError(entityType.Location(), EdmErrorCode.EntityNoncomposableBoundEscapeFunctionMustBeLessOne, Strings.EdmModel_Validator_Semantic_EntityNoncomposableBoundEscapeFunctionMustBeLessOne(entityType.FullName(), text9));
			}
		});

		// Token: 0x040003BC RID: 956
		public static readonly ValidationRule<IEdmEntityReferenceType> EntityReferenceTypeInaccessibleEntityType = new ValidationRule<IEdmEntityReferenceType>(delegate(ValidationContext context, IEdmEntityReferenceType entityReferenceType)
		{
			if (!context.IsBad(entityReferenceType.EntityType))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, entityReferenceType.EntityType, entityReferenceType.Location());
			}
		});

		// Token: 0x040003BD RID: 957
		public static readonly ValidationRule<IEdmType> TypeMustNotHaveKindOfNone = new ValidationRule<IEdmType>(delegate(ValidationContext context, IEdmType type)
		{
			if (type.TypeKind == EdmTypeKind.None && !context.IsBad(type))
			{
				context.AddError(type.Location(), EdmErrorCode.TypeMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone);
			}
		});

		// Token: 0x040003BE RID: 958
		public static readonly ValidationRule<IEdmPrimitiveType> PrimitiveTypeMustNotHaveKindOfNone = new ValidationRule<IEdmPrimitiveType>(delegate(ValidationContext context, IEdmPrimitiveType type)
		{
			if (type.PrimitiveKind == EdmPrimitiveTypeKind.None && !context.IsBad(type))
			{
				context.AddError(type.Location(), EdmErrorCode.PrimitiveTypeMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone(type.FullName()));
			}
		});

		// Token: 0x040003BF RID: 959
		public static readonly ValidationRule<IEdmComplexType> OpenComplexTypeCannotHaveClosedDerivedComplexType = new ValidationRule<IEdmComplexType>(delegate(ValidationContext context, IEdmComplexType complexType)
		{
			if (complexType.BaseType != null && complexType.BaseType.IsOpen && !complexType.IsOpen)
			{
				context.AddError(complexType.Location(), EdmErrorCode.InvalidAbstractComplexType, Strings.EdmModel_Validator_Semantic_BaseTypeOfOpenTypeMustBeOpen(complexType.FullName()));
			}
		});

		// Token: 0x040003C0 RID: 960
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
			if (edmType.TypeKind != EdmTypeKind.Primitive && edmType.TypeKind != EdmTypeKind.Enum && edmType.TypeKind != EdmTypeKind.Untyped && edmType.TypeKind != EdmTypeKind.Complex && edmType.TypeKind != EdmTypeKind.Path && edmType.TypeKind != EdmTypeKind.TypeDefinition && !context.IsBad(edmType))
			{
				context.AddError(property.Location(), EdmErrorCode.InvalidPropertyType, Strings.EdmModel_Validator_Semantic_InvalidPropertyType(property.Type.TypeKind().ToString()));
			}
		});

		// Token: 0x040003C1 RID: 961
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyInvalidOperationMultipleEndsInAssociatedNavigationProperties = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			if (navigationProperty.OnDelete != EdmOnDeleteAction.None && navigationProperty.Partner != null && navigationProperty.Partner.OnDelete != EdmOnDeleteAction.None)
			{
				context.AddError(navigationProperty.Location(), EdmErrorCode.InvalidAction, Strings.EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation);
			}
		});

		// Token: 0x040003C2 RID: 962
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

		// Token: 0x040003C3 RID: 963
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyDuplicateDependentProperty = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmStructuralProperty> enumerable3 = navigationProperty.DependentProperties();
			if (enumerable3 != null)
			{
				HashSetInternal<string> hashSetInternal9 = new HashSetInternal<string>();
				foreach (IEdmStructuralProperty edmStructuralProperty6 in navigationProperty.DependentProperties())
				{
					if (edmStructuralProperty6 != null)
					{
						ValidationHelper.AddMemberNameToHashSet(edmStructuralProperty6, hashSetInternal9, context, EdmErrorCode.DuplicateDependentProperty, Strings.EdmModel_Validator_Semantic_DuplicateDependentProperty(edmStructuralProperty6.Name, navigationProperty.Name), false);
					}
				}
			}
		});

		// Token: 0x040003C4 RID: 964
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

		// Token: 0x040003C5 RID: 965
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

		// Token: 0x040003C6 RID: 966
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyDependentPropertiesMustBelongToDependentEntity = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmStructuralProperty> enumerable6 = navigationProperty.DependentProperties();
			if (enumerable6 != null)
			{
				IEdmEntityType edmEntityType3 = navigationProperty.DeclaringEntityType();
				foreach (IEdmStructuralProperty edmStructuralProperty7 in enumerable6)
				{
					if (!context.IsBad(edmStructuralProperty7) && !edmStructuralProperty7.IsBad())
					{
						IEdmProperty edmProperty4 = edmEntityType3.FindProperty(edmStructuralProperty7.Name);
						if (edmProperty4 != edmStructuralProperty7)
						{
							context.AddError(navigationProperty.Location(), EdmErrorCode.DependentPropertiesMustBelongToDependentEntity, Strings.EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity(edmStructuralProperty7.Name, edmEntityType3.Name));
						}
					}
				}
			}
		});

		// Token: 0x040003C7 RID: 967
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyEndWithManyMultiplicityCannotHaveOperationsSpecified = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty end)
		{
			if (end.Partner != null && end.Partner.Type.IsCollection() && end.OnDelete != EdmOnDeleteAction.None)
			{
				string text10 = Strings.EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(end.Name);
				context.AddError(end.Location(), EdmErrorCode.EndWithManyMultiplicityCannotHaveOperationsSpecified, text10);
			}
		});

		// Token: 0x040003C8 RID: 968
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyPartnerPathShouldBeResolvable = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			IEdmPathExpression partnerPath = property.GetPartnerPath();
			if (partnerPath != null && property.Type.Definition.AsElementType() is IEdmEntityType && CsdlSemanticsNavigationProperty.ResolvePartnerPath((IEdmEntityType)property.Type.Definition.AsElementType(), partnerPath, context.Model) == null)
			{
				context.AddError(property.Location(), EdmErrorCode.UnresolvedNavigationPropertyPartnerPath, string.Format(CultureInfo.CurrentCulture, "Cannot resolve partner path for navigation property '{0}'.", new object[] { property.Name }));
			}
		});

		// Token: 0x040003C9 RID: 969
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyWithRecursiveContainmentTargetMustBeOptional = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.ContainsTarget && property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && !property.Type.IsCollection() && !property.Type.IsNullable)
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyWithRecursiveContainmentTargetMustBeOptional, Strings.EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional(property.Name));
			}
		});

		// Token: 0x040003CA RID: 970
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.Partner != null && property.ContainsTarget && property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && (property.Partner.Type.IsCollection() || !property.Partner.Type.IsNullable))
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne, Strings.EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne(property.Name));
			}
		});

		// Token: 0x040003CB RID: 971
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.Partner != null && property.ContainsTarget && !property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && (property.Partner.Type.IsCollection() || property.Partner.Type.IsNullable))
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne, Strings.EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne(property.Name));
			}
		});

		// Token: 0x040003CC RID: 972
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyEntityMustNotIndirectlyContainItself = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.ContainsTarget && !property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && ValidationHelper.TypeIndirectlyContainsTarget(property.ToEntityType(), property.DeclaringEntityType(), new HashSetInternal<IEdmEntityType>(), context.Model))
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyEntityMustNotIndirectlyContainItself, Strings.EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself(property.Name));
			}
		});

		// Token: 0x040003CD RID: 973
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyTypeCannotHavePathTypeProperty = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			IEdmTypeReference edmTypeReference = property.Type;
			if (edmTypeReference.IsCollection())
			{
				edmTypeReference = edmTypeReference.AsCollection().ElementType();
			}
			IEdmStructuredType edmStructuredType = edmTypeReference.ToStructuredType();
			if (edmStructuredType == null)
			{
				return;
			}
			IList<IEdmStructuredType> list5 = new List<IEdmStructuredType>();
			if (ValidationRules.HasPathTypeProperty(edmStructuredType, list5))
			{
				string text11 = Strings.EdmModel_Validator_Semantic_TypeOfNavigationPropertyCannotHavePathProperty(property.Type.FullName(), property.Name, property.DeclaringType.FullTypeName());
				context.AddError(property.Location(), EdmErrorCode.TypeOfNavigationPropertyCannotHavePathProperty, text11);
			}
		});

		// Token: 0x040003CE RID: 974
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyTypeMismatchRelationshipConstraint = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmStructuralProperty> enumerable7 = navigationProperty.DependentProperties();
			if (enumerable7 != null)
			{
				int num = enumerable7.Count<IEdmStructuralProperty>();
				IEdmEntityType edmEntityType4 = navigationProperty.ToEntityType();
				IEnumerable<IEdmStructuralProperty> enumerable8 = navigationProperty.PrincipalProperties();
				if (num == enumerable8.Count<IEdmStructuralProperty>())
				{
					for (int i = 0; i < num; i++)
					{
						IEdmType definition = enumerable7.ElementAtOrDefault(i).Type.Definition;
						IEdmType definition2 = enumerable8.ElementAtOrDefault(i).Type.Definition;
						if (!(definition is BadType) && !(definition2 is BadType) && !definition.IsEquivalentTo(definition2))
						{
							string text12 = Strings.EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint(navigationProperty.DependentProperties().ToList<IEdmStructuralProperty>()[i].Name, navigationProperty.DeclaringEntityType().FullName(), enumerable8.ToList<IEdmStructuralProperty>()[i].Name, edmEntityType4.Name, "Fred");
							context.AddError(navigationProperty.Location(), EdmErrorCode.TypeMismatchRelationshipConstraint, text12);
						}
					}
				}
			}
		});

		// Token: 0x040003CF RID: 975
		public static readonly ValidationRule<IEdmProperty> PropertyMustNotHaveKindOfNone = new ValidationRule<IEdmProperty>(delegate(ValidationContext context, IEdmProperty property)
		{
			if (property.PropertyKind == EdmPropertyKind.None && !context.IsBad(property))
			{
				context.AddError(property.Location(), EdmErrorCode.PropertyMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone(property.Name));
			}
		});

		// Token: 0x040003D0 RID: 976
		public static readonly ValidationRule<IEdmProperty> PropertyTypeCannotBeCollectionOfAbstractType = new ValidationRule<IEdmProperty>(delegate(ValidationContext context, IEdmProperty property)
		{
			if (property.Type.IsCollection())
			{
				IEdmTypeReference edmTypeReference2 = property.Type.AsCollection().ElementType();
				if (edmTypeReference2.Definition == EdmCoreModelComplexType.Instance || edmTypeReference2.Definition == EdmCoreModel.Instance.GetPrimitiveType())
				{
					context.AddError(property.Location(), EdmErrorCode.PropertyTypeCannotBeCollectionOfAbstractType, Strings.EdmModel_Validator_Semantic_PropertyTypeCannotBeCollectionOfAbstractType(property.Type.FullName(), property.Name));
				}
			}
		});

		// Token: 0x040003D1 RID: 977
		public static readonly ValidationRule<IEdmOperationImport> OperationImportCannotImportBoundOperation = new ValidationRule<IEdmOperationImport>(delegate(ValidationContext context, IEdmOperationImport operationImport)
		{
			if (operationImport.Operation.IsBound)
			{
				context.AddError(operationImport.Location(), EdmErrorCode.OperationImportCannotImportBoundOperation, Strings.EdmModel_Validator_Semantic_OperationImportCannotImportBoundOperation(operationImport.Name, operationImport.Operation.Name));
			}
		});

		// Token: 0x040003D2 RID: 978
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

		// Token: 0x040003D3 RID: 979
		public static readonly ValidationRule<IEdmOperationImport> OperationImportEntityTypeDoesNotMatchEntitySet = new ValidationRule<IEdmOperationImport>(delegate(ValidationContext context, IEdmOperationImport operationImport)
		{
			if (operationImport.EntitySet != null && operationImport.Operation.ReturnType != null)
			{
				IEdmTypeReference edmTypeReference3 = (operationImport.Operation.ReturnType.IsCollection() ? operationImport.Operation.ReturnType.AsCollection().ElementType() : operationImport.Operation.ReturnType);
				if (edmTypeReference3.IsEntity())
				{
					IEdmEntityType edmEntityType5 = edmTypeReference3.AsEntity().EntityDefinition();
					IEdmEntitySetBase edmEntitySetBase2;
					IEdmOperationParameter edmOperationParameter;
					Dictionary<IEdmNavigationProperty, IEdmPathExpression> dictionary;
					IEnumerable<EdmError> enumerable9;
					if (operationImport.TryGetStaticEntitySet(context.Model, out edmEntitySetBase2))
					{
						IEdmEntityType edmEntityType6 = edmEntitySetBase2.EntityType();
						if (!edmEntityType5.IsOrInheritsFrom(edmEntityType6) && !context.IsBad(edmEntityType5) && !context.IsBad(edmEntitySetBase2) && !context.IsBad(edmEntityType6))
						{
							string text13 = Strings.EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet(operationImport.Name, edmEntityType5.FullName(), edmEntitySetBase2.Name);
							context.AddError(operationImport.Location(), EdmErrorCode.OperationImportEntityTypeDoesNotMatchEntitySet, text13);
							return;
						}
					}
					else if (operationImport.TryGetRelativeEntitySetPath(context.Model, out edmOperationParameter, out dictionary, out enumerable9))
					{
						List<IEdmNavigationProperty> list6 = dictionary.Select((KeyValuePair<IEdmNavigationProperty, IEdmPathExpression> s) => s.Key).ToList<IEdmNavigationProperty>();
						IEdmTypeReference edmTypeReference4 = ((list6.Count == 0) ? edmOperationParameter.Type : dictionary.Last<KeyValuePair<IEdmNavigationProperty, IEdmPathExpression>>().Key.Type);
						IEdmTypeReference edmTypeReference5 = (edmTypeReference4.IsCollection() ? edmTypeReference4.AsCollection().ElementType() : edmTypeReference4);
						if (!edmEntityType5.IsOrInheritsFrom(edmTypeReference5.Definition) && !context.IsBad(edmEntityType5) && !context.IsBad(edmTypeReference5.Definition))
						{
							context.AddError(operationImport.Location(), EdmErrorCode.OperationImportEntityTypeDoesNotMatchEntitySet, Strings.EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet2(operationImport.Name, edmTypeReference3.FullName()));
							return;
						}
					}
				}
				else if (!context.IsBad(edmTypeReference3.Definition))
				{
					context.AddError(operationImport.Location(), EdmErrorCode.OperationImportSpecifiesEntitySetButDoesNotReturnEntityType, Strings.EdmModel_Validator_Semantic_OperationImportSpecifiesEntitySetButNotEntityType(operationImport.Name));
				}
			}
		});

		// Token: 0x040003D4 RID: 980
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportWithParameterShouldNotBeIncludedInServiceDocument = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (functionImport.IncludeInServiceDocument && functionImport.Function.Parameters.Any<IEdmOperationParameter>())
			{
				context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportWithParameterShouldNotBeIncludedInServiceDocument, Strings.EdmModel_Validator_Semantic_FunctionImportWithParameterShouldNotBeIncludedInServiceDocument(functionImport.Name));
			}
		});

		// Token: 0x040003D5 RID: 981
		public static readonly ValidationRule<IEdmFunction> FunctionMustHaveReturnType = new ValidationRule<IEdmFunction>(delegate(ValidationContext context, IEdmFunction function)
		{
			if (function.ReturnType == null)
			{
				context.AddError(function.Location(), EdmErrorCode.FunctionMustHaveReturnType, Strings.EdmModel_Validator_Semantic_FunctionMustHaveReturnType(function.Name));
			}
		});

		// Token: 0x040003D6 RID: 982
		public static readonly ValidationRule<IEdmFunction> FunctionWithUrlEscapeFunctionMustBeBound = new ValidationRule<IEdmFunction>(delegate(ValidationContext context, IEdmFunction function)
		{
			if (!context.Model.IsUrlEscapeFunction(function))
			{
				return;
			}
			if (!function.IsBound)
			{
				context.AddError(function.Location(), EdmErrorCode.UrlEscapeFunctionMustBeBoundFunction, Strings.EdmModel_Validator_Semantic_UrlEscapeFunctionMustBoundFunction(function.Name));
			}
		});

		// Token: 0x040003D7 RID: 983
		public static readonly ValidationRule<IEdmFunction> FunctionWithUrlEscapeFunctionMustHaveOneStringParameter = new ValidationRule<IEdmFunction>(delegate(ValidationContext context, IEdmFunction function)
		{
			if (!context.Model.IsUrlEscapeFunction(function))
			{
				return;
			}
			if (function.Parameters == null || function.Parameters.Count<IEdmOperationParameter>() != 2 || !function.Parameters.ElementAt(1).Type.IsString())
			{
				context.AddError(function.Location(), EdmErrorCode.UrlEscapeFunctionMustHaveOnlyOneEdmStringParameter, Strings.EdmModel_Validator_Semantic_UrlEscapeFunctionMustHaveOneStringParameter(function.Name));
			}
		});

		// Token: 0x040003D8 RID: 984
		public static readonly ValidationRule<IEdmOperation> OperationUnsupportedReturnType = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			if (operation.ReturnType != null)
			{
				IEdmTypeReference edmTypeReference6 = (operation.ReturnType.IsCollection() ? operation.ReturnType.AsCollection().ElementType() : operation.ReturnType);
				if (!(edmTypeReference6.Definition is IUnresolvedElement) && context.IsBad(edmTypeReference6.Definition))
				{
					context.AddError(operation.Location(), EdmErrorCode.OperationImportUnsupportedReturnType, Strings.EdmModel_Validator_Semantic_OperationWithUnsupportedReturnType(operation.Name));
				}
			}
		});

		// Token: 0x040003D9 RID: 985
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

		// Token: 0x040003DA RID: 986
		public static readonly ValidationRule<IEdmOperation> BoundOperationMustHaveParameters = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			if (operation.IsBound)
			{
				if (!operation.Parameters.Any((IEdmOperationParameter p) => !(p is IEdmOptionalParameter)))
				{
					context.AddError(operation.Location(), EdmErrorCode.BoundOperationMustHaveParameters, Strings.EdmModel_Validator_Semantic_BoundOperationMustHaveParameters(operation.Name));
				}
			}
		});

		// Token: 0x040003DB RID: 987
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

		// Token: 0x040003DC RID: 988
		public static readonly ValidationRule<IEdmOperation> OperationEntitySetPathMustBeValid = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			IEdmOperationParameter edmOperationParameter4 = null;
			Dictionary<IEdmNavigationProperty, IEdmPathExpression> dictionary2 = null;
			IEdmEntityType edmEntityType7 = null;
			IEnumerable<EdmError> enumerable10 = null;
			operation.TryGetRelativeEntitySetPath(context.Model, out edmOperationParameter4, out dictionary2, out edmEntityType7, out enumerable10);
			foreach (EdmError edmError in enumerable10)
			{
				context.AddError(edmError);
			}
		});

		// Token: 0x040003DD RID: 989
		public static readonly ValidationRule<IEdmOperation> OperationReturnTypeEntityTypeMustBeValid = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			IEdmOperationParameter edmOperationParameter5 = null;
			Dictionary<IEdmNavigationProperty, IEdmPathExpression> dictionary3 = null;
			IEdmEntityType edmEntityType8 = null;
			IEnumerable<EdmError> enumerable11 = null;
			if (!operation.TryGetRelativeEntitySetPath(context.Model, out edmOperationParameter5, out dictionary3, out edmEntityType8, out enumerable11))
			{
				return;
			}
			if (operation.ReturnType != null)
			{
				IEdmEntityType edmEntityType9 = operation.ReturnType.Definition as IEdmEntityType;
				IEdmCollectionType edmCollectionType2 = operation.ReturnType.Definition as IEdmCollectionType;
				if (edmEntityType9 == null && edmCollectionType2 != null)
				{
					edmEntityType9 = edmCollectionType2.ElementType.Definition as IEdmEntityType;
				}
				bool flag5 = operation.ReturnType.IsEntity();
				if (edmCollectionType2 != null)
				{
					flag5 = edmCollectionType2.ElementType.IsEntity();
				}
				if (!flag5 || context.IsBad(edmEntityType9))
				{
					context.AddError(operation.Location(), EdmErrorCode.OperationWithEntitySetPathReturnTypeInvalid, Strings.EdmModel_Validator_Semantic_OperationWithEntitySetPathReturnTypeInvalid(operation.Name));
				}
				IEdmNavigationProperty edmNavigationProperty = null;
				if (dictionary3 != null)
				{
					edmNavigationProperty = dictionary3.LastOrDefault<KeyValuePair<IEdmNavigationProperty, IEdmPathExpression>>().Key;
				}
				if (edmNavigationProperty != null && edmNavigationProperty.TargetMultiplicity() != EdmMultiplicity.Many && edmCollectionType2 != null)
				{
					context.AddError(operation.Location(), EdmErrorCode.OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType, Strings.EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType(operation.Name));
				}
				if (edmEntityType8 != null && edmEntityType9 != null && !edmEntityType9.IsOrInheritsFrom(edmEntityType8))
				{
					context.AddError(operation.Location(), EdmErrorCode.OperationWithEntitySetPathAndReturnTypeTypeNotAssignable, Strings.EdmModel_Validator_Semantic_OperationWithEntitySetPathAndReturnTypeTypeNotAssignable(operation.Name, edmEntityType9.FullName(), edmEntityType8.FullName()));
				}
			}
		});

		// Token: 0x040003DE RID: 990
		public static readonly ValidationRule<IEdmOperation> OperationReturnTypeCannotBeCollectionOfAbstractType = new ValidationRule<IEdmOperation>(delegate(ValidationContext context, IEdmOperation operation)
		{
			if (operation.ReturnType != null && operation.ReturnType.IsCollection())
			{
				IEdmTypeReference edmTypeReference7 = operation.ReturnType.AsCollection().ElementType();
				if (edmTypeReference7.Definition == EdmCoreModelComplexType.Instance || edmTypeReference7.Definition == EdmCoreModel.Instance.GetPrimitiveType())
				{
					context.AddError(operation.Location(), EdmErrorCode.OperationWithCollectionOfAbstractReturnTypeInvalid, Strings.EdmModel_Validator_Semantic_OperationReturnTypeCannotBeCollectionOfAbstractType(operation.ReturnType.FullName(), operation.FullName()));
				}
			}
		});

		// Token: 0x040003DF RID: 991
		public static readonly ValidationRule<IEdmTypeReference> TypeReferenceInaccessibleSchemaType = new ValidationRule<IEdmTypeReference>(delegate(ValidationContext context, IEdmTypeReference typeReference)
		{
			IEdmSchemaType edmSchemaType3 = typeReference.Definition as IEdmSchemaType;
			if (edmSchemaType3 != null && !context.IsBad(edmSchemaType3))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, edmSchemaType3, typeReference.Location());
			}
		});

		// Token: 0x040003E0 RID: 992
		public static readonly ValidationRule<IEdmDecimalTypeReference> DecimalTypeReferenceScaleOutOfRange = new ValidationRule<IEdmDecimalTypeReference>(delegate(ValidationContext context, IEdmDecimalTypeReference type)
		{
			if (type.Scale > type.Precision || type.Scale < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.ScaleOutOfRange, Strings.EdmModel_Validator_Semantic_ScaleOutOfRange);
			}
		});

		// Token: 0x040003E1 RID: 993
		public static readonly ValidationRule<IEdmDecimalTypeReference> DecimalTypeReferencePrecisionOutOfRange = new ValidationRule<IEdmDecimalTypeReference>(delegate(ValidationContext context, IEdmDecimalTypeReference type)
		{
			if (type.Precision > 2147483647 || type.Precision < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.PrecisionOutOfRange, Strings.EdmModel_Validator_Semantic_PrecisionOutOfRange);
			}
		});

		// Token: 0x040003E2 RID: 994
		public static readonly ValidationRule<IEdmStringTypeReference> StringTypeReferenceStringMaxLengthNegative = new ValidationRule<IEdmStringTypeReference>(delegate(ValidationContext context, IEdmStringTypeReference type)
		{
			if (type.MaxLength < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.MaxLengthOutOfRange, Strings.EdmModel_Validator_Semantic_StringMaxLengthOutOfRange);
			}
		});

		// Token: 0x040003E3 RID: 995
		public static readonly ValidationRule<IEdmStringTypeReference> StringTypeReferenceStringUnboundedNotValidForMaxLength = new ValidationRule<IEdmStringTypeReference>(delegate(ValidationContext context, IEdmStringTypeReference type)
		{
			if (type.MaxLength != null && type.IsUnbounded)
			{
				context.AddError(type.Location(), EdmErrorCode.IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull, Strings.EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull);
			}
		});

		// Token: 0x040003E4 RID: 996
		public static readonly ValidationRule<IEdmBinaryTypeReference> BinaryTypeReferenceBinaryMaxLengthNegative = new ValidationRule<IEdmBinaryTypeReference>(delegate(ValidationContext context, IEdmBinaryTypeReference type)
		{
			if (type.MaxLength < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.MaxLengthOutOfRange, Strings.EdmModel_Validator_Semantic_MaxLengthOutOfRange);
			}
		});

		// Token: 0x040003E5 RID: 997
		public static readonly ValidationRule<IEdmBinaryTypeReference> BinaryTypeReferenceBinaryUnboundedNotValidForMaxLength = new ValidationRule<IEdmBinaryTypeReference>(delegate(ValidationContext context, IEdmBinaryTypeReference type)
		{
			if (type.MaxLength != null && type.IsUnbounded)
			{
				context.AddError(type.Location(), EdmErrorCode.IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull, Strings.EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull);
			}
		});

		// Token: 0x040003E6 RID: 998
		public static readonly ValidationRule<IEdmTemporalTypeReference> TemporalTypeReferencePrecisionOutOfRange = new ValidationRule<IEdmTemporalTypeReference>(delegate(ValidationContext context, IEdmTemporalTypeReference type)
		{
			if (type.Precision > 2147483647 || type.Precision < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.PrecisionOutOfRange, Strings.EdmModel_Validator_Semantic_PrecisionOutOfRange);
			}
		});

		// Token: 0x040003E7 RID: 999
		public static readonly ValidationRule<IEdmModel> ModelDuplicateSchemaElementName = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			HashSetInternal<string> hashSetInternal11 = new HashSetInternal<string>();
			DuplicateOperationValidator duplicateOperationValidator = new DuplicateOperationValidator(context);
			HashSetInternal<string> hashSetInternal12 = new HashSetInternal<string>();
			foreach (IEdmSchemaElement edmSchemaElement in model.SchemaElements)
			{
				bool flag6 = false;
				string text14 = edmSchemaElement.FullName();
				IEdmOperation edmOperation = edmSchemaElement as IEdmOperation;
				if (edmOperation != null)
				{
					if (!hashSetInternal12.Contains(edmOperation.FullName()))
					{
						hashSetInternal12.Add(edmOperation.FullName());
					}
					if (hashSetInternal11.Contains(text14))
					{
						flag6 = true;
					}
					duplicateOperationValidator.ValidateNotDuplicate(edmOperation, false);
					if (!flag6)
					{
						flag6 = model.OperationOrNameExistsInReferencedModel(edmOperation, text14);
					}
				}
				else
				{
					flag6 = !hashSetInternal11.Add(text14) || hashSetInternal12.Contains(text14) || model.ItemExistsInReferencedModel(text14, true);
				}
				if (flag6)
				{
					context.AddError(edmSchemaElement.Location(), EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined(text14));
				}
			}
		});

		// Token: 0x040003E8 RID: 1000
		public static readonly ValidationRule<IEdmModel> ModelDuplicateEntityContainerName = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			HashSetInternal<string> hashSetInternal13 = new HashSetInternal<string>();
			IEdmEntityContainer entityContainer = model.EntityContainer;
			if (entityContainer != null)
			{
				ValidationHelper.AddMemberNameToHashSet(entityContainer, hashSetInternal13, context, EdmErrorCode.DuplicateEntityContainerName, Strings.EdmModel_Validator_Semantic_DuplicateEntityContainerName(entityContainer.Name), false);
			}
		});

		// Token: 0x040003E9 RID: 1001
		public static readonly ValidationRule<IEdmModel> ModelBoundFunctionOverloadsMustHaveSameReturnType = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			foreach (IGrouping<string, IEdmFunction> grouping in from f in model.SchemaElements.OfType<IEdmFunction>()
				where f.IsBound
				select f into f2
				group f2 by f2.FullName())
			{
				Dictionary<IEdmTypeReference, IEdmTypeReference> dictionary4 = new Dictionary<IEdmTypeReference, IEdmTypeReference>(new ValidationRules.EdmTypeReferenceComparer());
				foreach (IEdmFunction edmFunction2 in grouping)
				{
					if (edmFunction2.Parameters.Any<IEdmOperationParameter>() && edmFunction2.ReturnType != null)
					{
						IEdmOperationParameter edmOperationParameter6 = edmFunction2.Parameters.First<IEdmOperationParameter>();
						if (!dictionary4.ContainsKey(edmOperationParameter6.Type))
						{
							dictionary4.Add(edmOperationParameter6.Type, edmFunction2.ReturnType);
						}
						else
						{
							IEdmTypeReference edmTypeReference8 = dictionary4[edmOperationParameter6.Type];
							if (!edmFunction2.ReturnType.IsEquivalentTo(edmTypeReference8))
							{
								context.AddError(edmFunction2.Location(), EdmErrorCode.BoundFunctionOverloadsMustHaveSameReturnType, Strings.EdmModel_Validator_Semantic_BoundFunctionOverloadsMustHaveSameReturnType(edmFunction2.Name, edmTypeReference8.FullName()));
							}
						}
					}
				}
			}
		});

		// Token: 0x040003EA RID: 1002
		public static readonly ValidationRule<IEdmModel> UnBoundFunctionOverloadsMustHaveIdenticalReturnTypes = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			Dictionary<string, IEdmTypeReference> dictionary5 = new Dictionary<string, IEdmTypeReference>();
			foreach (IEdmFunction edmFunction3 in from f in model.SchemaElements.OfType<IEdmFunction>()
				where !f.IsBound
				select f)
			{
				if (!dictionary5.ContainsKey(edmFunction3.Name))
				{
					dictionary5.Add(edmFunction3.Name, edmFunction3.ReturnType);
				}
				else if (!edmFunction3.ReturnType.IsEquivalentTo(dictionary5[edmFunction3.Name]))
				{
					context.AddError(edmFunction3.Location(), EdmErrorCode.UnboundFunctionOverloadHasIncorrectReturnType, Strings.EdmModel_Validator_Semantic_UnboundFunctionOverloadHasIncorrectReturnType(edmFunction3.Name));
				}
			}
		});

		// Token: 0x040003EB RID: 1003
		public static readonly ValidationRule<IEdmDirectValueAnnotation> ImmediateValueAnnotationElementAnnotationIsValid = new ValidationRule<IEdmDirectValueAnnotation>(delegate(ValidationContext context, IEdmDirectValueAnnotation annotation)
		{
			IEdmStringValue edmStringValue = annotation.Value as IEdmStringValue;
			if (edmStringValue != null && edmStringValue.IsSerializedAsElement(context.Model) && (EdmUtil.IsNullOrWhiteSpaceInternal(annotation.NamespaceUri) || EdmUtil.IsNullOrWhiteSpaceInternal(annotation.Name)))
			{
				context.AddError(annotation.Location(), EdmErrorCode.InvalidElementAnnotation, Strings.EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm);
			}
		});

		// Token: 0x040003EC RID: 1004
		public static readonly ValidationRule<IEdmDirectValueAnnotation> ImmediateValueAnnotationElementAnnotationHasNameAndNamespace = new ValidationRule<IEdmDirectValueAnnotation>(delegate(ValidationContext context, IEdmDirectValueAnnotation annotation)
		{
			IEdmStringValue edmStringValue2 = annotation.Value as IEdmStringValue;
			EdmError edmError2;
			if (edmStringValue2 != null && edmStringValue2.IsSerializedAsElement(context.Model) && !ValidationHelper.ValidateValueCanBeWrittenAsXmlElementAnnotation(edmStringValue2, annotation.NamespaceUri, annotation.Name, out edmError2))
			{
				context.AddError(edmError2);
			}
		});

		// Token: 0x040003ED RID: 1005
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

		// Token: 0x040003EE RID: 1006
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
								flag7 = context.Model.FindOperations(edmOperation2.FullName()).Any<IEdmOperation>();
							}
							else
							{
								IEdmOperationImport edmOperationImport2 = target as IEdmOperationImport;
								if (edmOperationImport2 != null)
								{
									flag7 = edmOperationImport2.Container.FindOperationImportsExtended(edmOperationImport2.Name).Any<IEdmOperationImport>();
								}
								else
								{
									IEdmProperty edmProperty5 = target as IEdmProperty;
									if (edmProperty5 != null)
									{
										string text15 = EdmUtil.FullyQualifiedName(edmProperty5.DeclaringType as IEdmSchemaType);
										IEdmStructuredType edmStructuredType2 = context.Model.FindType(text15) as IEdmStructuredType;
										if (edmStructuredType2 != null)
										{
											flag7 = edmStructuredType2.FindProperty(edmProperty5.Name) != null;
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
												using (IEnumerator<IEdmOperation> enumerator29 = context.Model.FindOperations(declaringOperation.FullName()).GetEnumerator())
												{
													while (enumerator29.MoveNext())
													{
														IEdmOperation edmOperation3 = enumerator29.Current;
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
											using (IEnumerator<IEdmOperationImport> enumerator30 = container2.FindOperationImportsExtended(edmOperationImport3.Name).GetEnumerator())
											{
												while (enumerator30.MoveNext())
												{
													IEdmOperationImport edmOperationImport4 = enumerator30.Current;
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

		// Token: 0x040003EF RID: 1007
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

		// Token: 0x040003F0 RID: 1008
		public static readonly ValidationRule<IEdmVocabularyAnnotation> AnnotationInaccessibleTerm = new ValidationRule<IEdmVocabularyAnnotation>(delegate(ValidationContext context, IEdmVocabularyAnnotation annotation)
		{
			IEdmTerm term = annotation.Term;
			if (!(term is IUnresolvedElement) && context.Model.FindTerm(term.FullName()) == null)
			{
				context.AddError(annotation.Location(), EdmErrorCode.BadUnresolvedTerm, Strings.EdmModel_Validator_Semantic_InaccessibleTerm(annotation.Term.FullName()));
			}
		});

		// Token: 0x040003F1 RID: 1009
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
		public static readonly ValidationRule<IEdmVocabularyAnnotation> VocabularyAnnotationTargetAllowedApplyToElement = new ValidationRule<IEdmVocabularyAnnotation>(delegate(ValidationContext context, IEdmVocabularyAnnotation annotation)
		{
			IEdmTerm term2 = annotation.Term;
			if (term2.AppliesTo == null)
			{
				return;
			}
			HashSet<string> hashSet = new HashSet<string>(from e in term2.AppliesTo.Split(new char[] { ' ' })
				select e.Trim());
			string symbolicString = annotation.Target.GetSymbolicString();
			if (hashSet.Contains(symbolicString))
			{
				return;
			}
			context.AddError(annotation.Location(), EdmErrorCode.AnnotationApplyToNotAllowedAnnotatable, Strings.EdmModel_Validator_Semantic_VocabularyAnnotationApplyToNotAllowedAnnotatable(EdmUtil.FullyQualifiedName(annotation.Target), term2.AppliesTo, term2.FullName()));
		});

		// Token: 0x040003F2 RID: 1010
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

		// Token: 0x040003F3 RID: 1011
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

		// Token: 0x040003F4 RID: 1012
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

		// Token: 0x040003F5 RID: 1013
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

		// Token: 0x040003F6 RID: 1014
		public static readonly ValidationRule<IEdmApplyExpression> FunctionApplicationExpressionParametersMatchAppliedFunction = new ValidationRule<IEdmApplyExpression>(delegate(ValidationContext context, IEdmApplyExpression expression)
		{
			IEdmFunction appliedFunction = expression.AppliedFunction;
			if (appliedFunction != null && !context.IsBad(appliedFunction))
			{
				if (appliedFunction.Parameters.Count<IEdmOperationParameter>() != expression.Arguments.Count<IEdmExpression>())
				{
					context.AddError(new EdmError(expression.Location(), EdmErrorCode.IncorrectNumberOfArguments, Strings.EdmModel_Validator_Semantic_IncorrectNumberOfArguments(expression.Arguments.Count<IEdmExpression>(), appliedFunction.FullName(), appliedFunction.Parameters.Count<IEdmOperationParameter>())));
				}
				IEnumerator<IEdmExpression> enumerator36 = expression.Arguments.GetEnumerator();
				foreach (IEdmOperationParameter edmOperationParameter8 in appliedFunction.Parameters)
				{
					enumerator36.MoveNext();
					IEnumerable<EdmError> enumerable17;
					if (!enumerator36.Current.TryCast(edmOperationParameter8.Type, out enumerable17))
					{
						foreach (EdmError edmError8 in enumerable17)
						{
							context.AddError(edmError8);
						}
					}
				}
			}
		});

		// Token: 0x040003F7 RID: 1015
		public static readonly ValidationRule<IEdmVocabularyAnnotatable> VocabularyAnnotatableNoDuplicateAnnotations = new ValidationRule<IEdmVocabularyAnnotatable>(delegate(ValidationContext context, IEdmVocabularyAnnotatable annotatable)
		{
			HashSetInternal<string> hashSetInternal14 = new HashSetInternal<string>();
			foreach (IEdmVocabularyAnnotation edmVocabularyAnnotation in context.Model.FindDeclaredVocabularyAnnotations(annotatable))
			{
				if (!hashSetInternal14.Add(edmVocabularyAnnotation.Term.FullName() + ":" + edmVocabularyAnnotation.Qualifier))
				{
					context.AddError(new EdmError(edmVocabularyAnnotation.Location(), EdmErrorCode.DuplicateAnnotation, Strings.EdmModel_Validator_Semantic_DuplicateAnnotation(EdmUtil.FullyQualifiedName(annotatable), edmVocabularyAnnotation.Term.FullName(), edmVocabularyAnnotation.Qualifier)));
				}
			}
		});

		// Token: 0x040003F8 RID: 1016
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

		// Token: 0x020002A3 RID: 675
		internal class EdmTypeReferenceComparer : IEqualityComparer<IEdmTypeReference>
		{
			// Token: 0x06000FCA RID: 4042 RVA: 0x00029E65 File Offset: 0x00028065
			public bool Equals(IEdmTypeReference x, IEdmTypeReference y)
			{
				return x.IsEquivalentTo(y);
			}

			// Token: 0x06000FCB RID: 4043 RVA: 0x00029E70 File Offset: 0x00028070
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
