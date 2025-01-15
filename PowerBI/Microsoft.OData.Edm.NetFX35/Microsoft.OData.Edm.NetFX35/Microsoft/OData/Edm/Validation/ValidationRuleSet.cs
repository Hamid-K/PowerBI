using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000276 RID: 630
	public sealed class ValidationRuleSet : IEnumerable<ValidationRule>, IEnumerable
	{
		// Token: 0x06000E20 RID: 3616 RVA: 0x0002AEFD File Offset: 0x000290FD
		public ValidationRuleSet(IEnumerable<ValidationRule> baseSet, IEnumerable<ValidationRule> newRules)
			: this(Enumerable.Concat<ValidationRule>(EdmUtil.CheckArgumentNull<IEnumerable<ValidationRule>>(baseSet, "baseSet"), EdmUtil.CheckArgumentNull<IEnumerable<ValidationRule>>(newRules, "newRules")))
		{
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0002AF20 File Offset: 0x00029120
		public ValidationRuleSet(IEnumerable<ValidationRule> rules)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<ValidationRule>>(rules, "rules");
			this.rules = new Dictionary<Type, List<ValidationRule>>();
			foreach (ValidationRule validationRule in rules)
			{
				this.AddRule(validationRule);
			}
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0002AF88 File Offset: 0x00029188
		public static ValidationRuleSet GetEdmModelRuleSet(Version version)
		{
			if (version == EdmConstants.EdmVersion4)
			{
				return ValidationRuleSet.V4RuleSet;
			}
			throw new InvalidOperationException(Strings.Serializer_UnknownEdmVersion);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0002B160 File Offset: 0x00029360
		public IEnumerator<ValidationRule> GetEnumerator()
		{
			foreach (List<ValidationRule> ruleList in this.rules.Values)
			{
				foreach (ValidationRule rule in ruleList)
				{
					yield return rule;
				}
			}
			yield break;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0002B17C File Offset: 0x0002937C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0002B184 File Offset: 0x00029384
		internal IEnumerable<ValidationRule> GetRules(Type t)
		{
			List<ValidationRule> list;
			if (!this.rules.TryGetValue(t, ref list))
			{
				return Enumerable.Empty<ValidationRule>();
			}
			return list;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0002B1A8 File Offset: 0x000293A8
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

		// Token: 0x0400069D RID: 1693
		private readonly Dictionary<Type, List<ValidationRule>> rules;

		// Token: 0x0400069E RID: 1694
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
			ValidationRules.StructuralPropertyInvalidPropertyTypeConcurrencyMode,
			ValidationRules.EnumMemberValueMustHaveSameTypeAsUnderlyingType,
			ValidationRules.EnumTypeEnumMemberNameAlreadyDefined,
			ValidationRules.BoundOperationMustHaveParameters,
			ValidationRules.OperationEntitySetPathMustBeValid,
			ValidationRules.OperationReturnTypeEntityTypeMustBeValid,
			ValidationRules.OperationImportEntitySetExpressionIsInvalid,
			ValidationRules.FunctionImportWithParameterShouldNotBeIncludedInServiceDocument,
			ValidationRules.BinaryTypeReferenceBinaryUnboundedNotValidForMaxLength,
			ValidationRules.StringTypeReferenceStringUnboundedNotValidForMaxLength,
			ValidationRules.ImmediateValueAnnotationElementAnnotationIsValid,
			ValidationRules.ValueAnnotationAssertCorrectExpressionType,
			ValidationRules.IfExpressionAssertCorrectTestType,
			ValidationRules.CollectionExpressionAllElementsCorrectType,
			ValidationRules.RecordExpressionPropertiesMatchType,
			ValidationRules.NavigationPropertyDependentPropertiesMustBelongToDependentEntity,
			ValidationRules.NavigationPropertyInvalidOperationMultipleEndsInAssociatedNavigationProperties,
			ValidationRules.NavigationPropertyEndWithManyMultiplicityCannotHaveOperationsSpecified,
			ValidationRules.NavigationPropertyTypeMismatchRelationshipConstraint,
			ValidationRules.NavigationPropertyDuplicateDependentProperty,
			ValidationRules.NavigationPropertyPrincipalEndMultiplicity,
			ValidationRules.NavigationPropertyDependentEndMultiplicity,
			ValidationRules.NavigationPropertyCorrectType,
			ValidationRules.ImmediateValueAnnotationElementAnnotationHasNameAndNamespace,
			ValidationRules.OpenComplexTypeCannotHaveClosedDerivedComplexType,
			ValidationRules.OperationApplicationExpressionParametersMatchAppliedOperation,
			ValidationRules.VocabularyAnnotatableNoDuplicateAnnotations,
			ValidationRules.TemporalTypeReferencePrecisionOutOfRange,
			ValidationRules.DecimalTypeReferencePrecisionOutOfRange,
			ValidationRules.ModelDuplicateEntityContainerName,
			ValidationRules.ModelBoundFunctionOverloadsMustHaveSameReturnType,
			ValidationRules.UnBoundFunctionOverloadsMustHaveIdenticalReturnTypes,
			ValidationRules.TypeMustNotHaveKindOfNone,
			ValidationRules.PrimitiveTypeMustNotHaveKindOfNone,
			ValidationRules.PropertyMustNotHaveKindOfNone,
			ValidationRules.TermMustNotHaveKindOfNone,
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

		// Token: 0x0400069F RID: 1695
		private static readonly ValidationRuleSet V4RuleSet = new ValidationRuleSet(ValidationRuleSet.BaseRuleSet, new ValidationRule[]
		{
			ValidationRules.OperationUnsupportedReturnType,
			ValidationRules.ModelDuplicateSchemaElementName
		});
	}
}
