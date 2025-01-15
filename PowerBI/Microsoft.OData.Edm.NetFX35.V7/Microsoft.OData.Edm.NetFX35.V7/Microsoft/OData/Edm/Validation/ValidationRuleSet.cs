using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x020000DA RID: 218
	public sealed class ValidationRuleSet : IEnumerable<ValidationRule>, IEnumerable
	{
		// Token: 0x06000649 RID: 1609 RVA: 0x00010465 File Offset: 0x0000E665
		public ValidationRuleSet(IEnumerable<ValidationRule> baseSet, IEnumerable<ValidationRule> newRules)
			: this(Enumerable.Concat<ValidationRule>(EdmUtil.CheckArgumentNull<IEnumerable<ValidationRule>>(baseSet, "baseSet"), EdmUtil.CheckArgumentNull<IEnumerable<ValidationRule>>(newRules, "newRules")))
		{
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00010488 File Offset: 0x0000E688
		public ValidationRuleSet(IEnumerable<ValidationRule> rules)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<ValidationRule>>(rules, "rules");
			this.rules = new Dictionary<Type, List<ValidationRule>>();
			foreach (ValidationRule validationRule in rules)
			{
				this.AddRule(validationRule);
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000104F0 File Offset: 0x0000E6F0
		public static ValidationRuleSet GetEdmModelRuleSet(Version version)
		{
			if (version == EdmConstants.EdmVersion4)
			{
				return ValidationRuleSet.V4RuleSet;
			}
			throw new InvalidOperationException(Strings.Serializer_UnknownEdmVersion);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001050F File Offset: 0x0000E70F
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

		// Token: 0x0600064D RID: 1613 RVA: 0x0001051E File Offset: 0x0000E71E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00010528 File Offset: 0x0000E728
		internal IEnumerable<ValidationRule> GetRules(Type t)
		{
			List<ValidationRule> list;
			if (!this.rules.TryGetValue(t, ref list))
			{
				return Enumerable.Empty<ValidationRule>();
			}
			return list;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00010550 File Offset: 0x0000E750
		private void AddRule(ValidationRule rule)
		{
			List<ValidationRule> list;
			if (!this.rules.TryGetValue(rule.ValidatedType, ref list))
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

		// Token: 0x0400030A RID: 778
		private readonly Dictionary<Type, List<ValidationRule>> rules;

		// Token: 0x0400030B RID: 779
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
			ValidationRules.EntityTypeKeyMissingOnEntityType,
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
			ValidationRules.NavigationPropertyEntityMustNotIndirectlyContainItself,
			ValidationRules.EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet,
			ValidationRules.NavigationPropertyMappingMustPointToValidTargetForProperty,
			ValidationRules.DirectValueAnnotationHasXmlSerializableName,
			ValidationRules.FunctionMustHaveReturnType
		});

		// Token: 0x0400030C RID: 780
		private static readonly ValidationRuleSet V4RuleSet = new ValidationRuleSet(ValidationRuleSet.BaseRuleSet, new ValidationRule[]
		{
			ValidationRules.OperationUnsupportedReturnType,
			ValidationRules.ModelDuplicateSchemaElementName
		});
	}
}
