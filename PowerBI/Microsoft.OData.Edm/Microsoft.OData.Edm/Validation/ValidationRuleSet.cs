using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000149 RID: 329
	public sealed class ValidationRuleSet : IEnumerable<ValidationRule>, IEnumerable
	{
		// Token: 0x06000848 RID: 2120 RVA: 0x00015A61 File Offset: 0x00013C61
		public ValidationRuleSet(IEnumerable<ValidationRule> baseSet, IEnumerable<ValidationRule> newRules)
			: this(EdmUtil.CheckArgumentNull<IEnumerable<ValidationRule>>(baseSet, "baseSet").Concat(EdmUtil.CheckArgumentNull<IEnumerable<ValidationRule>>(newRules, "newRules")))
		{
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00015A84 File Offset: 0x00013C84
		public ValidationRuleSet(IEnumerable<ValidationRule> rules)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<ValidationRule>>(rules, "rules");
			this.rules = new Dictionary<Type, List<ValidationRule>>();
			foreach (ValidationRule validationRule in rules)
			{
				this.AddRule(validationRule);
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00015AEC File Offset: 0x00013CEC
		public static ValidationRuleSet GetEdmModelRuleSet(Version version)
		{
			if (version == EdmConstants.EdmVersion4 || version == EdmConstants.EdmVersion401)
			{
				return ValidationRuleSet.V4RuleSet;
			}
			throw new InvalidOperationException(Strings.Serializer_UnknownEdmVersion);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00015B18 File Offset: 0x00013D18
		public IEnumerator<ValidationRule> GetEnumerator()
		{
			foreach (List<ValidationRule> list in this.rules.Values)
			{
				foreach (ValidationRule validationRule in list)
				{
					yield return validationRule;
				}
				List<ValidationRule>.Enumerator enumerator2 = default(List<ValidationRule>.Enumerator);
			}
			Dictionary<Type, List<ValidationRule>>.ValueCollection.Enumerator enumerator = default(Dictionary<Type, List<ValidationRule>>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00015B27 File Offset: 0x00013D27
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00015B30 File Offset: 0x00013D30
		internal IEnumerable<ValidationRule> GetRules(Type t)
		{
			List<ValidationRule> list;
			if (!this.rules.TryGetValue(t, out list))
			{
				return Enumerable.Empty<ValidationRule>();
			}
			return list;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00015B58 File Offset: 0x00013D58
		private void AddRule(ValidationRule rule)
		{
			List<ValidationRule> list;
			if (!this.rules.TryGetValue(rule.ValidatedType, out list))
			{
				list = new List<ValidationRule>();
				this.rules[rule.ValidatedType] = list;
			}
			if (list.Contains(rule))
			{
				throw new InvalidOperationException(Strings.RuleSet_DuplicateRulesExistInRuleSet);
			}
			list.Add(rule);
		}

		// Token: 0x040003FA RID: 1018
		private readonly Dictionary<Type, List<ValidationRule>> rules;

		// Token: 0x040003FB RID: 1019
		private static readonly ValidationRuleSet BaseRuleSet = new ValidationRuleSet(new ValidationRule[]
		{
			ValidationRules.EntityTypeKeyPropertyMustBelongToEntity,
			ValidationRules.StructuredTypePropertiesDeclaringTypeMustBeCorrect,
			ValidationRules.NamedElementNameMustNotBeEmptyOrWhiteSpace,
			ValidationRules.NamedElementNameIsTooLong,
			ValidationRules.NamedElementNameIsNotAllowed,
			ValidationRules.SchemaElementNamespaceIsNotAllowed,
			ValidationRules.SchemaElementNamespaceIsTooLong,
			ValidationRules.SchemaElementNamespaceMustNotBeEmptyOrWhiteSpace,
			ValidationRules.SchemaElementSystemNamespaceEncountered,
			ValidationRules.EntityContainerDuplicateEntityContainerMemberName,
			ValidationRules.EntityTypeDuplicatePropertyNameSpecifiedInEntityKey,
			ValidationRules.EntityTypeInvalidKeyNullablePart,
			ValidationRules.EntityTypeEntityKeyMustBeScalar,
			ValidationRules.EntityTypeInvalidKeyKeyDefinedInBaseClass,
			ValidationRules.EntityTypeBoundEscapeFunctionMustBeUnique,
			ValidationRules.StructuredTypeInvalidMemberNameMatchesTypeName,
			ValidationRules.StructuredTypePropertyNameAlreadyDefined,
			ValidationRules.StructuralPropertyInvalidPropertyType,
			ValidationRules.OperationParameterNameAlreadyDefinedDuplicate,
			ValidationRules.OperationImportEntityTypeDoesNotMatchEntitySet,
			ValidationRules.OperationImportCannotImportBoundOperation,
			ValidationRules.StructuredTypeBaseTypeMustBeSameKindAsDerivedKind,
			ValidationRules.NavigationPropertyWithRecursiveContainmentTargetMustBeOptional,
			ValidationRules.NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne,
			ValidationRules.NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne,
			ValidationRules.EntitySetTypeMustBeCollectionOfEntityType,
			ValidationRules.NavigationSourceInaccessibleEntityType,
			ValidationRules.StructuredTypeInaccessibleBaseType,
			ValidationRules.EntityReferenceTypeInaccessibleEntityType,
			ValidationRules.TypeReferenceInaccessibleSchemaType,
			ValidationRules.NavigationSourceTypeHasNoKeys,
			ValidationRules.DecimalTypeReferenceScaleOutOfRange,
			ValidationRules.BinaryTypeReferenceBinaryMaxLengthNegative,
			ValidationRules.StringTypeReferenceStringMaxLengthNegative,
			ValidationRules.EnumMemberValueMustHaveSameTypeAsUnderlyingType,
			ValidationRules.EnumTypeEnumMemberNameAlreadyDefined,
			ValidationRules.BoundOperationMustHaveParameters,
			ValidationRules.OptionalParametersMustComeAfterRequiredParameters,
			ValidationRules.OperationEntitySetPathMustBeValid,
			ValidationRules.OperationReturnTypeEntityTypeMustBeValid,
			ValidationRules.OperationImportEntitySetExpressionIsInvalid,
			ValidationRules.FunctionImportWithParameterShouldNotBeIncludedInServiceDocument,
			ValidationRules.BinaryTypeReferenceBinaryUnboundedNotValidForMaxLength,
			ValidationRules.StringTypeReferenceStringUnboundedNotValidForMaxLength,
			ValidationRules.ImmediateValueAnnotationElementAnnotationIsValid,
			ValidationRules.VocabularyAnnotationAssertCorrectExpressionType,
			ValidationRules.IfExpressionAssertCorrectTestType,
			ValidationRules.CollectionExpressionAllElementsCorrectType,
			ValidationRules.RecordExpressionPropertiesMatchType,
			ValidationRules.NavigationPropertyDependentPropertiesMustBelongToDependentEntity,
			ValidationRules.NavigationPropertyInvalidOperationMultipleEndsInAssociatedNavigationProperties,
			ValidationRules.NavigationPropertyEndWithManyMultiplicityCannotHaveOperationsSpecified,
			ValidationRules.NavigationPropertyPartnerPathShouldBeResolvable,
			ValidationRules.NavigationPropertyTypeMismatchRelationshipConstraint,
			ValidationRules.NavigationPropertyDuplicateDependentProperty,
			ValidationRules.NavigationPropertyPrincipalEndMultiplicity,
			ValidationRules.NavigationPropertyDependentEndMultiplicity,
			ValidationRules.NavigationPropertyCorrectType,
			ValidationRules.NavigationPropertyBindingPathMustBeResolvable,
			ValidationRules.ImmediateValueAnnotationElementAnnotationHasNameAndNamespace,
			ValidationRules.OpenComplexTypeCannotHaveClosedDerivedComplexType,
			ValidationRules.FunctionApplicationExpressionParametersMatchAppliedFunction,
			ValidationRules.VocabularyAnnotatableNoDuplicateAnnotations,
			ValidationRules.TemporalTypeReferencePrecisionOutOfRange,
			ValidationRules.DecimalTypeReferencePrecisionOutOfRange,
			ValidationRules.ModelDuplicateEntityContainerName,
			ValidationRules.ModelBoundFunctionOverloadsMustHaveSameReturnType,
			ValidationRules.UnBoundFunctionOverloadsMustHaveIdenticalReturnTypes,
			ValidationRules.TypeMustNotHaveKindOfNone,
			ValidationRules.PrimitiveTypeMustNotHaveKindOfNone,
			ValidationRules.PropertyMustNotHaveKindOfNone,
			ValidationRules.SchemaElementMustNotHaveKindOfNone,
			ValidationRules.EntityContainerElementMustNotHaveKindOfNone,
			ValidationRules.PrimitiveValueValidForType,
			ValidationRules.EntitySetCanOnlyBeContainedByASingleNavigationProperty,
			ValidationRules.NavigationMappingMustBeBidirectional,
			ValidationRules.SingletonTypeMustBeEntityType,
			ValidationRules.NavigationPropertyMappingsMustBeUnique,
			ValidationRules.PropertyValueBindingValueIsCorrectType,
			ValidationRules.EnumMustHaveIntegerUnderlyingType,
			ValidationRules.AnnotationInaccessibleTerm,
			ValidationRules.ElementDirectValueAnnotationFullNameMustBeUnique,
			ValidationRules.VocabularyAnnotationInaccessibleTarget,
			ValidationRules.EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet,
			ValidationRules.NavigationPropertyMappingMustPointToValidTargetForProperty,
			ValidationRules.DirectValueAnnotationHasXmlSerializableName,
			ValidationRules.FunctionMustHaveReturnType,
			ValidationRules.FunctionWithUrlEscapeFunctionMustBeBound,
			ValidationRules.FunctionWithUrlEscapeFunctionMustHaveOneStringParameter,
			ValidationRules.EntitySetTypeCannotBeEdmEntityType,
			ValidationRules.SingletonTypeCannotBeEdmEntityType,
			ValidationRules.OperationReturnTypeCannotBeCollectionOfAbstractType,
			ValidationRules.PropertyTypeCannotBeCollectionOfAbstractType,
			ValidationRules.EntityTypeKeyTypeCannotBeEdmPrimitiveType,
			ValidationRules.TypeDefinitionUnderlyingTypeCannotBeEdmPrimitiveType,
			ValidationRules.EnumUnderlyingTypeCannotBeEdmPrimitiveType,
			ValidationRules.StructuredTypeBaseTypeCannotBeAbstractType,
			ValidationRules.NavigationSourceDeclaringTypeCannotHavePathTypeProperty,
			ValidationRules.NavigationPropertyTypeCannotHavePathTypeProperty
		});

		// Token: 0x040003FC RID: 1020
		private static readonly ValidationRuleSet V4RuleSet = new ValidationRuleSet(ValidationRuleSet.BaseRuleSet, new ValidationRule[]
		{
			ValidationRules.OperationUnsupportedReturnType,
			ValidationRules.ModelDuplicateSchemaElementName
		});
	}
}
