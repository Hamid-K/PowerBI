using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004AA RID: 1194
	internal abstract class EdmModelRuleSet : DataModelValidationRuleSet
	{
		// Token: 0x06003ABE RID: 15038 RVA: 0x000C1B94 File Offset: 0x000BFD94
		public static EdmModelRuleSet CreateEdmModelRuleSet(double version, bool validateSyntax)
		{
			if (object.Equals(version, 1.0))
			{
				return new EdmModelRuleSet.V1RuleSet(validateSyntax);
			}
			if (object.Equals(version, 1.1))
			{
				return new EdmModelRuleSet.V1_1RuleSet(validateSyntax);
			}
			if (object.Equals(version, 2.0))
			{
				return new EdmModelRuleSet.V2RuleSet(validateSyntax);
			}
			if (object.Equals(version, 3.0))
			{
				return new EdmModelRuleSet.V3RuleSet(validateSyntax);
			}
			return null;
		}

		// Token: 0x06003ABF RID: 15039 RVA: 0x000C1C2C File Offset: 0x000BFE2C
		private EdmModelRuleSet(bool validateSyntax)
		{
			if (validateSyntax)
			{
				base.AddRule(EdmModelSyntacticValidationRules.EdmAssociationConstraint_DependentEndMustNotBeNull);
				base.AddRule(EdmModelSyntacticValidationRules.EdmAssociationConstraint_DependentPropertiesMustNotBeEmpty);
				base.AddRule(EdmModelSyntacticValidationRules.EdmAssociationEnd_EntityTypeMustNotBeNull);
				base.AddRule(EdmModelSyntacticValidationRules.EdmAssociationSet_ElementTypeMustNotBeNull);
				base.AddRule(EdmModelSyntacticValidationRules.EdmAssociationSet_SourceSetMustNotBeNull);
				base.AddRule(EdmModelSyntacticValidationRules.EdmAssociationSet_TargetSetMustNotBeNull);
				base.AddRule(EdmModelSyntacticValidationRules.EdmAssociationType_AssociationEndMustNotBeNull);
				base.AddRule(EdmModelSyntacticValidationRules.EdmEntitySet_ElementTypeMustNotBeNull);
				base.AddRule(EdmModelSyntacticValidationRules.EdmModel_NameMustNotBeEmptyOrWhiteSpace);
				base.AddRule(EdmModelSyntacticValidationRules.EdmModel_NameIsTooLong);
				base.AddRule(EdmModelSyntacticValidationRules.EdmModel_NameIsNotAllowed);
				base.AddRule(EdmModelSyntacticValidationRules.EdmNavigationProperty_AssociationMustNotBeNull);
				base.AddRule(EdmModelSyntacticValidationRules.EdmNavigationProperty_ResultEndMustNotBeNull);
				base.AddRule(EdmModelSyntacticValidationRules.EdmTypeReference_TypeNotValid);
			}
			base.AddRule(EdmModelSemanticValidationRules.EdmType_SystemNamespaceEncountered);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntityContainer_SimilarRelationshipEnd);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntityContainer_InvalidEntitySetNameReference);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntityContainer_ConcurrencyRedefinedOnSubTypeOfEntitySetType);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntityContainer_DuplicateEntityContainerMemberName);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntityContainer_DuplicateEntitySetTable);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntitySet_EntitySetTypeHasNoKeys);
			base.AddRule(EdmModelSemanticValidationRules.EdmAssociationSet_DuplicateEndName);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntityType_EntityKeyMustBeScalar);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntityType_DuplicatePropertyNameSpecifiedInEntityKey);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntityType_InvalidKeyNullablePart);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntityType_InvalidKeyKeyDefinedInBaseClass);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntityType_KeyMissingOnEntityType);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntityType_InvalidMemberNameMatchesTypeName);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntityType_PropertyNameAlreadyDefinedDuplicate);
			base.AddRule(EdmModelSemanticValidationRules.EdmEntityType_CycleInTypeHierarchy);
			base.AddRule(EdmModelSemanticValidationRules.EdmNavigationProperty_BadNavigationPropertyUndefinedRole);
			base.AddRule(EdmModelSemanticValidationRules.EdmNavigationProperty_BadNavigationPropertyRolesCannotBeTheSame);
			base.AddRule(EdmModelSemanticValidationRules.EdmNavigationProperty_BadNavigationPropertyBadFromRoleType);
			base.AddRule(EdmModelSemanticValidationRules.EdmAssociationType_InvalidOperationMultipleEndsInAssociation);
			base.AddRule(EdmModelSemanticValidationRules.EdmAssociationType_EndWithManyMultiplicityCannotHaveOperationsSpecified);
			base.AddRule(EdmModelSemanticValidationRules.EdmAssociationType_EndNameAlreadyDefinedDuplicate);
			base.AddRule(EdmModelSemanticValidationRules.EdmAssociationType_InvalidPropertyInRelationshipConstraint);
			base.AddRule(EdmModelSemanticValidationRules.EdmAssociationType_SameRoleReferredInReferentialConstraint);
			base.AddRule(EdmModelSemanticValidationRules.EdmAssociationType_ValidateReferentialConstraint);
			base.AddRule(EdmModelSemanticValidationRules.EdmComplexType_InvalidMemberNameMatchesTypeName);
			base.AddRule(EdmModelSemanticValidationRules.EdmNamespace_TypeNameAlreadyDefinedDuplicate);
			base.AddRule(EdmModelSemanticValidationRules.EdmFunction_DuplicateParameterName);
		}

		// Token: 0x02000AD2 RID: 2770
		private abstract class NonV1_1RuleSet : EdmModelRuleSet
		{
			// Token: 0x0600632D RID: 25389 RVA: 0x00157948 File Offset: 0x00155B48
			protected NonV1_1RuleSet(bool validateSyntax)
				: base(validateSyntax)
			{
				base.AddRule(EdmModelSemanticValidationRules.EdmProperty_NullableComplexType);
				base.AddRule(EdmModelSemanticValidationRules.EdmProperty_InvalidCollectionKind);
				base.AddRule(EdmModelSemanticValidationRules.EdmComplexType_PropertyNameAlreadyDefinedDuplicate);
				base.AddRule(EdmModelSemanticValidationRules.EdmComplexType_InvalidIsAbstract);
				base.AddRule(EdmModelSemanticValidationRules.EdmComplexType_InvalidIsPolymorphic);
				base.AddRule(EdmModelSemanticValidationRules.EdmFunction_ComposableFunctionImportsNotAllowed_V1_V2);
			}
		}

		// Token: 0x02000AD3 RID: 2771
		private sealed class V1RuleSet : EdmModelRuleSet.NonV1_1RuleSet
		{
			// Token: 0x0600632E RID: 25390 RVA: 0x0015799E File Offset: 0x00155B9E
			internal V1RuleSet(bool validateSyntax)
				: base(validateSyntax)
			{
				base.AddRule(EdmModelSemanticValidationRules.EdmProperty_InvalidPropertyType);
			}
		}

		// Token: 0x02000AD4 RID: 2772
		private sealed class V1_1RuleSet : EdmModelRuleSet
		{
			// Token: 0x0600632F RID: 25391 RVA: 0x001579B2 File Offset: 0x00155BB2
			internal V1_1RuleSet(bool validateSyntax)
				: base(validateSyntax)
			{
				base.AddRule(EdmModelSemanticValidationRules.EdmComplexType_PropertyNameAlreadyDefinedDuplicate_V1_1);
				base.AddRule(EdmModelSemanticValidationRules.EdmComplexType_CycleInTypeHierarchy_V1_1);
				base.AddRule(EdmModelSemanticValidationRules.EdmProperty_InvalidCollectionKind_V1_1);
				base.AddRule(EdmModelSemanticValidationRules.EdmProperty_InvalidPropertyType_V1_1);
			}
		}

		// Token: 0x02000AD5 RID: 2773
		private class V2RuleSet : EdmModelRuleSet.NonV1_1RuleSet
		{
			// Token: 0x06006330 RID: 25392 RVA: 0x001579E7 File Offset: 0x00155BE7
			internal V2RuleSet(bool validateSyntax)
				: base(validateSyntax)
			{
				base.AddRule(EdmModelSemanticValidationRules.EdmProperty_InvalidPropertyType);
			}
		}

		// Token: 0x02000AD6 RID: 2774
		private sealed class V3RuleSet : EdmModelRuleSet.V2RuleSet
		{
			// Token: 0x06006331 RID: 25393 RVA: 0x001579FB File Offset: 0x00155BFB
			internal V3RuleSet(bool validateSyntax)
				: base(validateSyntax)
			{
				base.RemoveRule(EdmModelSemanticValidationRules.EdmProperty_InvalidPropertyType);
				base.AddRule(EdmModelSemanticValidationRules.EdmProperty_InvalidPropertyType_V3);
				base.RemoveRule(EdmModelSemanticValidationRules.EdmFunction_ComposableFunctionImportsNotAllowed_V1_V2);
			}
		}
	}
}
