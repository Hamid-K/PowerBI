using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200027D RID: 637
	internal static class Strings
	{
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0002BADF File Offset: 0x00029CDF
		internal static string EdmPrimitive_UnexpectedKind
		{
			get
			{
				return EntityRes.GetString("EdmPrimitive_UnexpectedKind");
			}
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0002BAEC File Offset: 0x00029CEC
		internal static string Annotations_DocumentationPun(object p0)
		{
			return EntityRes.GetString("Annotations_DocumentationPun", new object[] { p0 });
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0002BB10 File Offset: 0x00029D10
		internal static string Annotations_TypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("Annotations_TypeMismatch", new object[] { p0, p1 });
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0002BB37 File Offset: 0x00029D37
		internal static string Constructable_VocabularyAnnotationMustHaveTarget
		{
			get
			{
				return EntityRes.GetString("Constructable_VocabularyAnnotationMustHaveTarget");
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0002BB43 File Offset: 0x00029D43
		internal static string Constructable_EntityTypeOrCollectionOfEntityTypeExpected
		{
			get
			{
				return EntityRes.GetString("Constructable_EntityTypeOrCollectionOfEntityTypeExpected");
			}
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0002BB50 File Offset: 0x00029D50
		internal static string Constructable_TargetMustBeStock(object p0)
		{
			return EntityRes.GetString("Constructable_TargetMustBeStock", new object[] { p0 });
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0002BB74 File Offset: 0x00029D74
		internal static string TypeSemantics_CouldNotConvertTypeReference(object p0, object p1)
		{
			return EntityRes.GetString("TypeSemantics_CouldNotConvertTypeReference", new object[] { p0, p1 });
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x0002BB9B File Offset: 0x00029D9B
		internal static string EdmModel_CannotUseElementWithTypeNone
		{
			get
			{
				return EntityRes.GetString("EdmModel_CannotUseElementWithTypeNone");
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0002BBA7 File Offset: 0x00029DA7
		internal static string EdmModel_CannotAddMoreThanOneEntityContainerToOneEdmModel
		{
			get
			{
				return EntityRes.GetString("EdmModel_CannotAddMoreThanOneEntityContainerToOneEdmModel");
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0002BBB3 File Offset: 0x00029DB3
		internal static string EdmEntityContainer_CannotUseElementWithTypeNone
		{
			get
			{
				return EntityRes.GetString("EdmEntityContainer_CannotUseElementWithTypeNone");
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0002BBC0 File Offset: 0x00029DC0
		internal static string ValueWriter_NonSerializableValue(object p0)
		{
			return EntityRes.GetString("ValueWriter_NonSerializableValue", new object[] { p0 });
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0002BBE3 File Offset: 0x00029DE3
		internal static string ValueHasAlreadyBeenSet
		{
			get
			{
				return EntityRes.GetString("ValueHasAlreadyBeenSet");
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0002BBEF File Offset: 0x00029DEF
		internal static string PathSegmentMustNotContainSlash
		{
			get
			{
				return EntityRes.GetString("PathSegmentMustNotContainSlash");
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0002BBFC File Offset: 0x00029DFC
		internal static string Constructable_DependentPropertyCountMustMatchNumberOfPropertiesOnPrincipalType(object p0, object p1)
		{
			return EntityRes.GetString("Constructable_DependentPropertyCountMustMatchNumberOfPropertiesOnPrincipalType", new object[] { p0, p1 });
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0002BC23 File Offset: 0x00029E23
		internal static string EdmType_UnexpectedEdmType
		{
			get
			{
				return EntityRes.GetString("EdmType_UnexpectedEdmType");
			}
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0002BC30 File Offset: 0x00029E30
		internal static string Edm_Evaluator_NoTermTypeAnnotationOnType(object p0, object p1)
		{
			return EntityRes.GetString("Edm_Evaluator_NoTermTypeAnnotationOnType", new object[] { p0, p1 });
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0002BC58 File Offset: 0x00029E58
		internal static string Edm_Evaluator_NoValueAnnotationOnType(object p0, object p1)
		{
			return EntityRes.GetString("Edm_Evaluator_NoValueAnnotationOnType", new object[] { p0, p1 });
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0002BC80 File Offset: 0x00029E80
		internal static string Edm_Evaluator_NoValueAnnotationOnElement(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_NoValueAnnotationOnElement", new object[] { p0 });
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0002BCA4 File Offset: 0x00029EA4
		internal static string Edm_Evaluator_UnrecognizedExpressionKind(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_UnrecognizedExpressionKind", new object[] { p0 });
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0002BCC8 File Offset: 0x00029EC8
		internal static string Edm_Evaluator_UnboundFunction(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_UnboundFunction", new object[] { p0 });
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0002BCEC File Offset: 0x00029EEC
		internal static string Edm_Evaluator_UnboundPath(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_UnboundPath", new object[] { p0 });
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x0002BD0F File Offset: 0x00029F0F
		internal static string Edm_Evaluator_NoContextPath
		{
			get
			{
				return EntityRes.GetString("Edm_Evaluator_NoContextPath");
			}
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0002BD1C File Offset: 0x00029F1C
		internal static string Edm_Evaluator_FailedTypeAssertion(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_FailedTypeAssertion", new object[] { p0 });
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0002BD3F File Offset: 0x00029F3F
		internal static string Edm_Evaluator_TypeCastNeedsEdmModel
		{
			get
			{
				return EntityRes.GetString("Edm_Evaluator_TypeCastNeedsEdmModel");
			}
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0002BD4C File Offset: 0x00029F4C
		internal static string EdmModel_Validator_Semantic_SystemNamespaceEncountered(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SystemNamespaceEncountered", new object[] { p0 });
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0002BD70 File Offset: 0x00029F70
		internal static string EdmModel_Validator_Semantic_NavigationSourceTypeHasNoKeys(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationSourceTypeHasNoKeys", new object[] { p0, p1 });
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0002BD98 File Offset: 0x00029F98
		internal static string EdmModel_Validator_Semantic_DuplicateEndName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEndName", new object[] { p0 });
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0002BDBC File Offset: 0x00029FBC
		internal static string EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey", new object[] { p0, p1 });
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0002BDE4 File Offset: 0x00029FE4
		internal static string EdmModel_Validator_Semantic_InvalidComplexTypeAbstract(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidComplexTypeAbstract", new object[] { p0 });
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0002BE08 File Offset: 0x0002A008
		internal static string EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic", new object[] { p0 });
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0002BE2C File Offset: 0x0002A02C
		internal static string EdmModel_Validator_Semantic_InvalidKeyNullablePart(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidKeyNullablePart", new object[] { p0, p1 });
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0002BE54 File Offset: 0x0002A054
		internal static string EdmModel_Validator_Semantic_EntityKeyMustBeScalar(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntityKeyMustBeScalar", new object[] { p0, p1 });
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0002BE7C File Offset: 0x0002A07C
		internal static string EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass", new object[] { p0, p1 });
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0002BEA4 File Offset: 0x0002A0A4
		internal static string EdmModel_Validator_Semantic_KeyMissingOnEntityType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_KeyMissingOnEntityType", new object[] { p0 });
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0002BEC8 File Offset: 0x0002A0C8
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0002BEF4 File Offset: 0x0002A0F4
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame", new object[] { p0 });
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0002BF18 File Offset: 0x0002A118
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType", new object[] { p0 });
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0002BF3B File Offset: 0x0002A13B
		internal static string EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation");
			}
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0002BF48 File Offset: 0x0002A148
		internal static string EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified", new object[] { p0 });
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0002BF6C File Offset: 0x0002A16C
		internal static string EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0002BF90 File Offset: 0x0002A190
		internal static string EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint", new object[] { p0 });
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0002BFB4 File Offset: 0x0002A1B4
		internal static string EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne", new object[] { p0 });
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0002BFD8 File Offset: 0x0002A1D8
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable", new object[] { p0 });
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0002BFFC File Offset: 0x0002A1FC
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable", new object[] { p0 });
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0002C020 File Offset: 0x0002A220
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne", new object[] { p0 });
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0002C044 File Offset: 0x0002A244
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany", new object[] { p0 });
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0002C067 File Offset: 0x0002A267
		internal static string EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint");
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0002C074 File Offset: 0x0002A274
		internal static string EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0002C0A8 File Offset: 0x0002A2A8
		internal static string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintDependentEnd(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintDependentEnd", new object[] { p0, p1 });
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0002C0D0 File Offset: 0x0002A2D0
		internal static string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintPrimaryEnd(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintPrimaryEnd", new object[] { p0, p1 });
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0002C0F8 File Offset: 0x0002A2F8
		internal static string EdmModel_Validator_Semantic_InvalidPropertyType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyType", new object[] { p0 });
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0002C11C File Offset: 0x0002A31C
		internal static string EdmModel_Validator_Semantic_BoundOperationMustHaveParameters(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BoundOperationMustHaveParameters", new object[] { p0 });
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0002C140 File Offset: 0x0002A340
		internal static string EdmModel_Validator_Semantic_OperationWithUnsupportedReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithUnsupportedReturnType", new object[] { p0 });
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0002C164 File Offset: 0x0002A364
		internal static string EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0002C190 File Offset: 0x0002A390
		internal static string EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet2(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet2", new object[] { p0, p1 });
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0002C1B8 File Offset: 0x0002A3B8
		internal static string EdmModel_Validator_Semantic_OperationImportEntitySetExpressionKindIsInvalid(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportEntitySetExpressionKindIsInvalid", new object[] { p0, p1 });
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0002C1E0 File Offset: 0x0002A3E0
		internal static string EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid", new object[] { p0 });
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0002C204 File Offset: 0x0002A404
		internal static string EdmModel_Validator_Semantic_OperationImportSpecifiesEntitySetButNotEntityType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportSpecifiesEntitySetButNotEntityType", new object[] { p0 });
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0002C228 File Offset: 0x0002A428
		internal static string EdmModel_Validator_Semantic_OperationImportCannotImportBoundOperation(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportCannotImportBoundOperation", new object[] { p0, p1 });
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0002C250 File Offset: 0x0002A450
		internal static string EdmModel_Validator_Semantic_FunctionImportWithParameterShouldNotBeIncludedInServiceDocument(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportWithParameterShouldNotBeIncludedInServiceDocument", new object[] { p0 });
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0002C274 File Offset: 0x0002A474
		internal static string EdmModel_Validator_Semantic_FunctionMustHaveReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionMustHaveReturnType", new object[] { p0 });
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0002C298 File Offset: 0x0002A498
		internal static string EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0002C2BC File Offset: 0x0002A4BC
		internal static string EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName", new object[] { p0 });
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0002C2E0 File Offset: 0x0002A4E0
		internal static string EdmModel_Validator_Semantic_UnboundFunctionOverloadHasIncorrectReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_UnboundFunctionOverloadHasIncorrectReturnType", new object[] { p0 });
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0002C304 File Offset: 0x0002A504
		internal static string EdmModel_Validator_Semantic_OperationCannotHaveEntitySetPathWithUnBoundOperation(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationCannotHaveEntitySetPathWithUnBoundOperation", new object[] { p0 });
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0002C328 File Offset: 0x0002A528
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathMissingBindingParameterName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathMissingBindingParameterName", new object[] { p0 });
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0002C34C File Offset: 0x0002A54C
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathWithFirstPathParameterNotMatchingFirstParameterName(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathWithFirstPathParameterNotMatchingFirstParameterName", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0002C37C File Offset: 0x0002A57C
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathTypeCastSegmentMustBeEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathTypeCastSegmentMustBeEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002C3A8 File Offset: 0x0002A5A8
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownNavigationProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownNavigationProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0002C3D4 File Offset: 0x0002A5D4
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathInvalidTypeCastSegment(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathInvalidTypeCastSegment", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0002C404 File Offset: 0x0002A604
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathWithNonEntityBindingParameter(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathWithNonEntityBindingParameter", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002C430 File Offset: 0x0002A630
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownTypeCastSegment(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownTypeCastSegment", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0002C45C File Offset: 0x0002A65C
		internal static string EdmModel_Validator_Semantic_OperationWithEntitySetPathReturnTypeInvalid(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithEntitySetPathReturnTypeInvalid", new object[] { p0 });
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0002C480 File Offset: 0x0002A680
		internal static string EdmModel_Validator_Semantic_OperationWithEntitySetPathAndReturnTypeTypeNotAssignable(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithEntitySetPathAndReturnTypeTypeNotAssignable", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0002C4AC File Offset: 0x0002A6AC
		internal static string EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToEntityTypeMismatchesCollectionEntityTypeReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToEntityTypeMismatchesCollectionEntityTypeReturnType", new object[] { p0 });
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0002C4D0 File Offset: 0x0002A6D0
		internal static string EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType", new object[] { p0 });
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002C4F4 File Offset: 0x0002A6F4
		internal static string EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0002C518 File Offset: 0x0002A718
		internal static string EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName", new object[] { p0 });
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0002C53C File Offset: 0x0002A73C
		internal static string EdmModel_Validator_Semantic_PropertyNameAlreadyDefined(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PropertyNameAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0002C55F File Offset: 0x0002A75F
		internal static string EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind");
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0002C56C File Offset: 0x0002A76C
		internal static string EdmModel_Validator_Semantic_BaseTypeOfOpenTypeMustBeOpen(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BaseTypeOfOpenTypeMustBeOpen", new object[] { p0 });
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0002C590 File Offset: 0x0002A790
		internal static string EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity", new object[] { p0, p1 });
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0002C5B8 File Offset: 0x0002A7B8
		internal static string EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity", new object[] { p0, p1 });
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0002C5E0 File Offset: 0x0002A7E0
		internal static string EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect", new object[] { p0 });
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002C604 File Offset: 0x0002A804
		internal static string EdmModel_Validator_Semantic_InaccessibleType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InaccessibleType", new object[] { p0 });
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0002C628 File Offset: 0x0002A828
		internal static string EdmModel_Validator_Semantic_AmbiguousType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_AmbiguousType", new object[] { p0 });
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0002C64C File Offset: 0x0002A84C
		internal static string EdmModel_Validator_Semantic_InvalidNavigationPropertyType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidNavigationPropertyType", new object[] { p0 });
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0002C670 File Offset: 0x0002A870
		internal static string EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional", new object[] { p0 });
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0002C694 File Offset: 0x0002A894
		internal static string EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne", new object[] { p0 });
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0002C6B8 File Offset: 0x0002A8B8
		internal static string EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne", new object[] { p0 });
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0002C6DC File Offset: 0x0002A8DC
		internal static string EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties", new object[] { p0 });
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0002C700 File Offset: 0x0002A900
		internal static string EdmModel_Validator_Semantic_DuplicateDependentProperty(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateDependentProperty", new object[] { p0, p1 });
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0002C727 File Offset: 0x0002A927
		internal static string EdmModel_Validator_Semantic_ScaleOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ScaleOutOfRange");
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0002C733 File Offset: 0x0002A933
		internal static string EdmModel_Validator_Semantic_PrecisionOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_PrecisionOutOfRange");
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0002C73F File Offset: 0x0002A93F
		internal static string EdmModel_Validator_Semantic_StringMaxLengthOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_StringMaxLengthOutOfRange");
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x0002C74B File Offset: 0x0002A94B
		internal static string EdmModel_Validator_Semantic_MaxLengthOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_MaxLengthOutOfRange");
			}
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0002C758 File Offset: 0x0002A958
		internal static string EdmModel_Validator_Semantic_InvalidPropertyTypeConcurrencyMode(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyTypeConcurrencyMode", new object[] { p0 });
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0002C77C File Offset: 0x0002A97C
		internal static string EdmModel_Validator_Semantic_EnumMemberTypeMustMatchEnumUnderlyingType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EnumMemberTypeMustMatchEnumUnderlyingType", new object[] { p0 });
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0002C7A0 File Offset: 0x0002A9A0
		internal static string EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000EC0 RID: 3776 RVA: 0x0002C7C3 File Offset: 0x0002A9C3
		internal static string EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly");
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0002C7CF File Offset: 0x0002A9CF
		internal static string EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull");
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x0002C7DB File Offset: 0x0002A9DB
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm");
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0002C7E7 File Offset: 0x0002A9E7
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml");
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0002C7F3 File Offset: 0x0002A9F3
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationNotIEdmStringValue
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationNotIEdmStringValue");
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0002C7FF File Offset: 0x0002A9FF
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationNullNamespaceOrName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationNullNamespaceOrName");
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0002C80C File Offset: 0x0002AA0C
		internal static string EdmModel_Validator_Semantic_CannotAssertNullableTypeAsNonNullableType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_CannotAssertNullableTypeAsNonNullableType", new object[] { p0 });
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0002C830 File Offset: 0x0002AA30
		internal static string EdmModel_Validator_Semantic_ExpressionPrimitiveKindCannotPromoteToAssertedType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ExpressionPrimitiveKindCannotPromoteToAssertedType", new object[] { p0, p1 });
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x0002C857 File Offset: 0x0002AA57
		internal static string EdmModel_Validator_Semantic_NullCannotBeAssertedToBeANonNullableType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_NullCannotBeAssertedToBeANonNullableType");
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0002C863 File Offset: 0x0002AA63
		internal static string EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType");
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0002C86F File Offset: 0x0002AA6F
		internal static string EdmModel_Validator_Semantic_CollectionExpressionNotValidForNonCollectionType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_CollectionExpressionNotValidForNonCollectionType");
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0002C87B File Offset: 0x0002AA7B
		internal static string EdmModel_Validator_Semantic_PrimitiveConstantExpressionNotValidForNonPrimitiveType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_PrimitiveConstantExpressionNotValidForNonPrimitiveType");
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0002C887 File Offset: 0x0002AA87
		internal static string EdmModel_Validator_Semantic_RecordExpressionNotValidForNonStructuredType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_RecordExpressionNotValidForNonStructuredType");
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0002C894 File Offset: 0x0002AA94
		internal static string EdmModel_Validator_Semantic_RecordExpressionMissingProperty(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_RecordExpressionMissingProperty", new object[] { p0 });
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0002C8B8 File Offset: 0x0002AAB8
		internal static string EdmModel_Validator_Semantic_RecordExpressionHasExtraProperties(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_RecordExpressionHasExtraProperties", new object[] { p0 });
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0002C8DC File Offset: 0x0002AADC
		internal static string EdmModel_Validator_Semantic_DuplicateAnnotation(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateAnnotation", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0002C908 File Offset: 0x0002AB08
		internal static string EdmModel_Validator_Semantic_IncorrectNumberOfArguments(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_IncorrectNumberOfArguments", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0002C934 File Offset: 0x0002AB34
		internal static string EdmModel_Validator_Semantic_DuplicateEntityContainerName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEntityContainerName", new object[] { p0 });
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0002C957 File Offset: 0x0002AB57
		internal static string EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType");
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0002C963 File Offset: 0x0002AB63
		internal static string EdmModel_Validator_Semantic_IntegerConstantValueOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_IntegerConstantValueOutOfRange");
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0002C970 File Offset: 0x0002AB70
		internal static string EdmModel_Validator_Semantic_StringConstantLengthOutOfRange(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_StringConstantLengthOutOfRange", new object[] { p0, p1 });
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0002C998 File Offset: 0x0002AB98
		internal static string EdmModel_Validator_Semantic_BinaryConstantLengthOutOfRange(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BinaryConstantLengthOutOfRange", new object[] { p0, p1 });
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x0002C9BF File Offset: 0x0002ABBF
		internal static string EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone");
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0002C9CC File Offset: 0x0002ABCC
		internal static string EdmModel_Validator_Semantic_TermMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TermMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0002C9F0 File Offset: 0x0002ABF0
		internal static string EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0002CA14 File Offset: 0x0002AC14
		internal static string EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0002CA38 File Offset: 0x0002AC38
		internal static string EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0002CA5C File Offset: 0x0002AC5C
		internal static string EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0002CA80 File Offset: 0x0002AC80
		internal static string EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping", new object[] { p0, p1 });
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0002CAA8 File Offset: 0x0002ACA8
		internal static string EdmModel_Validator_Semantic_NavigationMappingMustBeBidirectional(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationMappingMustBeBidirectional", new object[] { p0, p1 });
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0002CAD0 File Offset: 0x0002ACD0
		internal static string EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty", new object[] { p0 });
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0002CAF4 File Offset: 0x0002ACF4
		internal static string EdmModel_Validator_Semantic_TypeAnnotationMissingRequiredProperty(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeAnnotationMissingRequiredProperty", new object[] { p0 });
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0002CB18 File Offset: 0x0002AD18
		internal static string EdmModel_Validator_Semantic_TypeAnnotationHasExtraProperties(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeAnnotationHasExtraProperties", new object[] { p0 });
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0002CB3C File Offset: 0x0002AD3C
		internal static string EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType", new object[] { p0 });
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0002CB60 File Offset: 0x0002AD60
		internal static string EdmModel_Validator_Semantic_InaccessibleTerm(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InaccessibleTerm", new object[] { p0 });
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0002CB84 File Offset: 0x0002AD84
		internal static string EdmModel_Validator_Semantic_InaccessibleTarget(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InaccessibleTarget", new object[] { p0 });
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0002CBA8 File Offset: 0x0002ADA8
		internal static string EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique", new object[] { p0, p1 });
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0002CBD0 File Offset: 0x0002ADD0
		internal static string EdmModel_Validator_Semantic_NoEntitySetsFoundForType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NoEntitySetsFoundForType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0002CBFC File Offset: 0x0002ADFC
		internal static string EdmModel_Validator_Semantic_CannotInferEntitySetWithMultipleSetsPerType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_CannotInferEntitySetWithMultipleSetsPerType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0002CC28 File Offset: 0x0002AE28
		internal static string EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet", new object[] { p0, p1 });
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0002CC50 File Offset: 0x0002AE50
		internal static string EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself", new object[] { p0 });
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0002CC74 File Offset: 0x0002AE74
		internal static string EdmModel_Validator_Semantic_PathIsNotValidForTheGivenContext(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PathIsNotValidForTheGivenContext", new object[] { p0 });
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0002CC98 File Offset: 0x0002AE98
		internal static string EdmModel_Validator_Semantic_NavigationPropertyMappingMustPointToValidTargetForProperty(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyMappingMustPointToValidTargetForProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0002CCC0 File Offset: 0x0002AEC0
		internal static string EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterNames(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterNames", new object[] { p0 });
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0002CCE4 File Offset: 0x0002AEE4
		internal static string EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterTypes(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterTypes", new object[] { p0 });
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0002CD08 File Offset: 0x0002AF08
		internal static string EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterNames(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterNames", new object[] { p0 });
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0002CD2C File Offset: 0x0002AF2C
		internal static string EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterTypes(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterTypes", new object[] { p0 });
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0002CD50 File Offset: 0x0002AF50
		internal static string EdmModel_Validator_Semantic_ModelDuplicateBoundActions(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateBoundActions", new object[] { p0 });
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0002CD74 File Offset: 0x0002AF74
		internal static string EdmModel_Validator_Semantic_ModelDuplicateUnBoundActions(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateUnBoundActions", new object[] { p0 });
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0002CD98 File Offset: 0x0002AF98
		internal static string EdmModel_Validator_Semantic_BoundFunctionOverloadsMustHaveSameReturnType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BoundFunctionOverloadsMustHaveSameReturnType", new object[] { p0, p1 });
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0002CDC0 File Offset: 0x0002AFC0
		internal static string EdmModel_Validator_Semantic_EntitySetTypeMustBeCollectionOfEntityType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetTypeMustBeCollectionOfEntityType", new object[] { p0, p1 });
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0002CDE8 File Offset: 0x0002AFE8
		internal static string EdmModel_Validator_Semantic_SingletonTypeMustBeEntityType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SingletonTypeMustBeEntityType", new object[] { p0, p1 });
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0002CE10 File Offset: 0x0002B010
		internal static string EdmModel_Validator_Semantic_NavigationPropertyOfCollectionTypeMustNotTargetToSingleton(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyOfCollectionTypeMustNotTargetToSingleton", new object[] { p0, p1 });
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x0002CE37 File Offset: 0x0002B037
		internal static string EdmModel_Validator_Syntactic_MissingName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_MissingName");
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0002CE44 File Offset: 0x0002B044
		internal static string EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong", new object[] { p0 });
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0002CE68 File Offset: 0x0002B068
		internal static string EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed", new object[] { p0 });
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0002CE8B File Offset: 0x0002B08B
		internal static string EdmModel_Validator_Syntactic_MissingNamespaceName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_MissingNamespaceName");
			}
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0002CE98 File Offset: 0x0002B098
		internal static string EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong", new object[] { p0 });
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0002CEBC File Offset: 0x0002B0BC
		internal static string EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0002CEE0 File Offset: 0x0002B0E0
		internal static string EdmModel_Validator_Syntactic_PropertyMustNotBeNull(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_PropertyMustNotBeNull", new object[] { p0, p1 });
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0002CF08 File Offset: 0x0002B108
		internal static string EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0002CF38 File Offset: 0x0002B138
		internal static string EdmModel_Validator_Syntactic_InterfaceKindValueMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_InterfaceKindValueMismatch", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0002CF68 File Offset: 0x0002B168
		internal static string EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch", new object[] { p0, p1 });
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0002CF90 File Offset: 0x0002B190
		internal static string EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0002CFBC File Offset: 0x0002B1BC
		internal static string EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements", new object[] { p0, p1 });
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0002CFE4 File Offset: 0x0002B1E4
		internal static string EdmModel_Validator_Syntactic_NavigationPartnerInvalid(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_NavigationPartnerInvalid", new object[] { p0 });
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0002D008 File Offset: 0x0002B208
		internal static string EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy", new object[] { p0 });
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0002D02B File Offset: 0x0002B22B
		internal static string Serializer_SingleFileExpected
		{
			get
			{
				return EntityRes.GetString("Serializer_SingleFileExpected");
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0002D037 File Offset: 0x0002B237
		internal static string Serializer_UnknownEdmVersion
		{
			get
			{
				return EntityRes.GetString("Serializer_UnknownEdmVersion");
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0002D043 File Offset: 0x0002B243
		internal static string Serializer_UnknownEdmxVersion
		{
			get
			{
				return EntityRes.GetString("Serializer_UnknownEdmxVersion");
			}
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0002D050 File Offset: 0x0002B250
		internal static string Serializer_NonInlineOperationImportReturnType(object p0)
		{
			return EntityRes.GetString("Serializer_NonInlineOperationImportReturnType", new object[] { p0 });
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0002D074 File Offset: 0x0002B274
		internal static string Serializer_ReferencedTypeMustHaveValidName(object p0)
		{
			return EntityRes.GetString("Serializer_ReferencedTypeMustHaveValidName", new object[] { p0 });
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0002D098 File Offset: 0x0002B298
		internal static string Serializer_OutOfLineAnnotationTargetMustHaveValidName(object p0)
		{
			return EntityRes.GetString("Serializer_OutOfLineAnnotationTargetMustHaveValidName", new object[] { p0 });
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0002D0BB File Offset: 0x0002B2BB
		internal static string Serializer_NoSchemasProduced
		{
			get
			{
				return EntityRes.GetString("Serializer_NoSchemasProduced");
			}
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0002D0C8 File Offset: 0x0002B2C8
		internal static string XmlParser_EmptyFile(object p0)
		{
			return EntityRes.GetString("XmlParser_EmptyFile", new object[] { p0 });
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0002D0EB File Offset: 0x0002B2EB
		internal static string XmlParser_EmptySchemaTextReader
		{
			get
			{
				return EntityRes.GetString("XmlParser_EmptySchemaTextReader");
			}
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0002D0F8 File Offset: 0x0002B2F8
		internal static string XmlParser_MissingAttribute(object p0, object p1)
		{
			return EntityRes.GetString("XmlParser_MissingAttribute", new object[] { p0, p1 });
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0002D120 File Offset: 0x0002B320
		internal static string XmlParser_TextNotAllowed(object p0)
		{
			return EntityRes.GetString("XmlParser_TextNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0002D144 File Offset: 0x0002B344
		internal static string XmlParser_UnexpectedAttribute(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedAttribute", new object[] { p0 });
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0002D168 File Offset: 0x0002B368
		internal static string XmlParser_UnexpectedElement(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedElement", new object[] { p0 });
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0002D18C File Offset: 0x0002B38C
		internal static string XmlParser_UnusedElement(object p0)
		{
			return EntityRes.GetString("XmlParser_UnusedElement", new object[] { p0 });
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0002D1B0 File Offset: 0x0002B3B0
		internal static string XmlParser_UnexpectedNodeType(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedNodeType", new object[] { p0 });
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0002D1D4 File Offset: 0x0002B3D4
		internal static string XmlParser_UnexpectedRootElement(object p0, object p1)
		{
			return EntityRes.GetString("XmlParser_UnexpectedRootElement", new object[] { p0, p1 });
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0002D1FC File Offset: 0x0002B3FC
		internal static string XmlParser_UnexpectedRootElementWrongNamespace(object p0, object p1)
		{
			return EntityRes.GetString("XmlParser_UnexpectedRootElementWrongNamespace", new object[] { p0, p1 });
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0002D224 File Offset: 0x0002B424
		internal static string XmlParser_UnexpectedRootElementNoNamespace(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedRootElementNoNamespace", new object[] { p0 });
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0002D248 File Offset: 0x0002B448
		internal static string CsdlParser_InvalidEntitySetPathWithUnboundAction(object p0, object p1)
		{
			return EntityRes.GetString("CsdlParser_InvalidEntitySetPathWithUnboundAction", new object[] { p0, p1 });
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0002D270 File Offset: 0x0002B470
		internal static string CsdlParser_InvalidAlias(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidAlias", new object[] { p0 });
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0002D294 File Offset: 0x0002B494
		internal static string CsdlParser_InvalidDeleteAction(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidDeleteAction", new object[] { p0 });
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0002D2B7 File Offset: 0x0002B4B7
		internal static string CsdlParser_MissingTypeAttributeOrElement
		{
			get
			{
				return EntityRes.GetString("CsdlParser_MissingTypeAttributeOrElement");
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0002D2C4 File Offset: 0x0002B4C4
		internal static string CsdlParser_InvalidConcurrencyMode(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidConcurrencyMode", new object[] { p0 });
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0002D2E8 File Offset: 0x0002B4E8
		internal static string CsdlParser_InvalidEndRoleInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("CsdlParser_InvalidEndRoleInRelationshipConstraint", new object[] { p0, p1 });
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0002D310 File Offset: 0x0002B510
		internal static string CsdlParser_InvalidMultiplicity(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidMultiplicity", new object[] { p0 });
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0002D333 File Offset: 0x0002B533
		internal static string CsdlParser_ReferentialConstraintRequiresOneDependent
		{
			get
			{
				return EntityRes.GetString("CsdlParser_ReferentialConstraintRequiresOneDependent");
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0002D33F File Offset: 0x0002B53F
		internal static string CsdlParser_ReferentialConstraintRequiresOnePrincipal
		{
			get
			{
				return EntityRes.GetString("CsdlParser_ReferentialConstraintRequiresOnePrincipal");
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0002D34B File Offset: 0x0002B54B
		internal static string CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0002D357 File Offset: 0x0002B557
		internal static string CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0002D363 File Offset: 0x0002B563
		internal static string CsdlParser_InvalidCastExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidCastExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0002D36F File Offset: 0x0002B56F
		internal static string CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0002D37C File Offset: 0x0002B57C
		internal static string CsdlParser_InvalidTypeName(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidTypeName", new object[] { p0 });
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0002D3A0 File Offset: 0x0002B5A0
		internal static string CsdlParser_InvalidQualifiedName(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidQualifiedName", new object[] { p0 });
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0002D3C3 File Offset: 0x0002B5C3
		internal static string CsdlParser_NoReadersProvided
		{
			get
			{
				return EntityRes.GetString("CsdlParser_NoReadersProvided");
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0002D3CF File Offset: 0x0002B5CF
		internal static string CsdlParser_NullXmlReader
		{
			get
			{
				return EntityRes.GetString("CsdlParser_NullXmlReader");
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0002D3DC File Offset: 0x0002B5DC
		internal static string CsdlParser_InvalidEntitySetPath(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidEntitySetPath", new object[] { p0 });
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x0002D400 File Offset: 0x0002B600
		internal static string CsdlParser_InvalidEnumMemberPath(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidEnumMemberPath", new object[] { p0 });
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0002D423 File Offset: 0x0002B623
		internal static string CsdlParser_CannotSpecifyNullableAttributeForNavigationPropertyWithCollectionType
		{
			get
			{
				return EntityRes.GetString("CsdlParser_CannotSpecifyNullableAttributeForNavigationPropertyWithCollectionType");
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0002D42F File Offset: 0x0002B62F
		internal static string CsdlParser_MetadataDocumentCannotHaveMoreThanOneEntityContainer
		{
			get
			{
				return EntityRes.GetString("CsdlParser_MetadataDocumentCannotHaveMoreThanOneEntityContainer");
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x0002D43B File Offset: 0x0002B63B
		internal static string CsdlSemantics_ReferentialConstraintMismatch
		{
			get
			{
				return EntityRes.GetString("CsdlSemantics_ReferentialConstraintMismatch");
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0002D447 File Offset: 0x0002B647
		internal static string CsdlSemantics_EnumMemberValueOutOfRange
		{
			get
			{
				return EntityRes.GetString("CsdlSemantics_EnumMemberValueOutOfRange");
			}
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0002D454 File Offset: 0x0002B654
		internal static string CsdlSemantics_ImpossibleAnnotationsTarget(object p0)
		{
			return EntityRes.GetString("CsdlSemantics_ImpossibleAnnotationsTarget", new object[] { p0 });
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0002D478 File Offset: 0x0002B678
		internal static string CsdlSemantics_DuplicateAlias(object p0, object p1)
		{
			return EntityRes.GetString("CsdlSemantics_DuplicateAlias", new object[] { p0, p1 });
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0002D49F File Offset: 0x0002B69F
		internal static string EdmxParser_EdmxVersionMismatch
		{
			get
			{
				return EntityRes.GetString("EdmxParser_EdmxVersionMismatch");
			}
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0002D4AC File Offset: 0x0002B6AC
		internal static string EdmxParser_BodyElement(object p0)
		{
			return EntityRes.GetString("EdmxParser_BodyElement", new object[] { p0 });
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0002D4CF File Offset: 0x0002B6CF
		internal static string EdmxParser_InvalidReferenceIncorrectNumberOfIncludes
		{
			get
			{
				return EntityRes.GetString("EdmxParser_InvalidReferenceIncorrectNumberOfIncludes");
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0002D4DB File Offset: 0x0002B6DB
		internal static string EdmxParser_UnresolvedReferenceUriInEdmxReference
		{
			get
			{
				return EntityRes.GetString("EdmxParser_UnresolvedReferenceUriInEdmxReference");
			}
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0002D4E8 File Offset: 0x0002B6E8
		internal static string EdmParseException_ErrorsEncounteredInEdmx(object p0)
		{
			return EntityRes.GetString("EdmParseException_ErrorsEncounteredInEdmx", new object[] { p0 });
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0002D50C File Offset: 0x0002B70C
		internal static string ValueParser_InvalidBoolean(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidBoolean", new object[] { p0 });
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0002D530 File Offset: 0x0002B730
		internal static string ValueParser_InvalidInteger(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidInteger", new object[] { p0 });
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0002D554 File Offset: 0x0002B754
		internal static string ValueParser_InvalidLong(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidLong", new object[] { p0 });
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0002D578 File Offset: 0x0002B778
		internal static string ValueParser_InvalidFloatingPoint(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidFloatingPoint", new object[] { p0 });
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0002D59C File Offset: 0x0002B79C
		internal static string ValueParser_InvalidMaxLength(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidMaxLength", new object[] { p0 });
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0002D5C0 File Offset: 0x0002B7C0
		internal static string ValueParser_InvalidSrid(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidSrid", new object[] { p0 });
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0002D5E4 File Offset: 0x0002B7E4
		internal static string ValueParser_InvalidScale(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidScale", new object[] { p0 });
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0002D608 File Offset: 0x0002B808
		internal static string ValueParser_InvalidGuid(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidGuid", new object[] { p0 });
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0002D62C File Offset: 0x0002B82C
		internal static string ValueParser_InvalidDecimal(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDecimal", new object[] { p0 });
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0002D650 File Offset: 0x0002B850
		internal static string ValueParser_InvalidDateTimeOffset(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDateTimeOffset", new object[] { p0 });
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0002D674 File Offset: 0x0002B874
		internal static string ValueParser_InvalidDateTime(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDateTime", new object[] { p0 });
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0002D698 File Offset: 0x0002B898
		internal static string ValueParser_InvalidDate(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDate", new object[] { p0 });
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0002D6BC File Offset: 0x0002B8BC
		internal static string ValueParser_InvalidDuration(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDuration", new object[] { p0 });
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0002D6E0 File Offset: 0x0002B8E0
		internal static string ValueParser_InvalidBinary(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidBinary", new object[] { p0 });
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0002D704 File Offset: 0x0002B904
		internal static string ValueParser_InvalidTimeOfDay(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidTimeOfDay", new object[] { p0 });
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0002D728 File Offset: 0x0002B928
		internal static string UnknownEnumVal_Multiplicity(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_Multiplicity", new object[] { p0 });
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0002D74C File Offset: 0x0002B94C
		internal static string UnknownEnumVal_SchemaElementKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_SchemaElementKind", new object[] { p0 });
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0002D770 File Offset: 0x0002B970
		internal static string UnknownEnumVal_TypeKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_TypeKind", new object[] { p0 });
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0002D794 File Offset: 0x0002B994
		internal static string UnknownEnumVal_PrimitiveKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_PrimitiveKind", new object[] { p0 });
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0002D7B8 File Offset: 0x0002B9B8
		internal static string UnknownEnumVal_ContainerElementKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_ContainerElementKind", new object[] { p0 });
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0002D7DC File Offset: 0x0002B9DC
		internal static string UnknownEnumVal_EdmxTarget(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_EdmxTarget", new object[] { p0 });
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0002D800 File Offset: 0x0002BA00
		internal static string UnknownEnumVal_ConcurrencyMode(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_ConcurrencyMode", new object[] { p0 });
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0002D824 File Offset: 0x0002BA24
		internal static string UnknownEnumVal_PropertyKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_PropertyKind", new object[] { p0 });
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0002D848 File Offset: 0x0002BA48
		internal static string UnknownEnumVal_TermKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_TermKind", new object[] { p0 });
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0002D86C File Offset: 0x0002BA6C
		internal static string UnknownEnumVal_ExpressionKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_ExpressionKind", new object[] { p0 });
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0002D890 File Offset: 0x0002BA90
		internal static string Bad_AmbiguousElementBinding(object p0)
		{
			return EntityRes.GetString("Bad_AmbiguousElementBinding", new object[] { p0 });
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0002D8B4 File Offset: 0x0002BAB4
		internal static string Bad_UnresolvedType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedType", new object[] { p0 });
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0002D8D8 File Offset: 0x0002BAD8
		internal static string Bad_UnresolvedComplexType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedComplexType", new object[] { p0 });
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0002D8FC File Offset: 0x0002BAFC
		internal static string Bad_UnresolvedEntityType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEntityType", new object[] { p0 });
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0002D920 File Offset: 0x0002BB20
		internal static string Bad_UnresolvedPrimitiveType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0002D944 File Offset: 0x0002BB44
		internal static string Bad_UnresolvedOperation(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedOperation", new object[] { p0 });
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0002D968 File Offset: 0x0002BB68
		internal static string Bad_AmbiguousOperation(object p0)
		{
			return EntityRes.GetString("Bad_AmbiguousOperation", new object[] { p0 });
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0002D98C File Offset: 0x0002BB8C
		internal static string Bad_OperationParametersDontMatch(object p0)
		{
			return EntityRes.GetString("Bad_OperationParametersDontMatch", new object[] { p0 });
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0002D9B0 File Offset: 0x0002BBB0
		internal static string Bad_UnresolvedEntitySet(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEntitySet", new object[] { p0 });
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0002D9D4 File Offset: 0x0002BBD4
		internal static string Bad_UnresolvedEntityContainer(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEntityContainer", new object[] { p0 });
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0002D9F8 File Offset: 0x0002BBF8
		internal static string Bad_UnresolvedEnumType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEnumType", new object[] { p0 });
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0002DA1C File Offset: 0x0002BC1C
		internal static string Bad_UnresolvedEnumMember(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEnumMember", new object[] { p0 });
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002DA40 File Offset: 0x0002BC40
		internal static string Bad_UnresolvedProperty(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedProperty", new object[] { p0 });
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002DA64 File Offset: 0x0002BC64
		internal static string Bad_UnresolvedParameter(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedParameter", new object[] { p0 });
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0002DA88 File Offset: 0x0002BC88
		internal static string Bad_UnresolvedLabeledElement(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedLabeledElement", new object[] { p0 });
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002DAAC File Offset: 0x0002BCAC
		internal static string Bad_CyclicEntity(object p0)
		{
			return EntityRes.GetString("Bad_CyclicEntity", new object[] { p0 });
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002DAD0 File Offset: 0x0002BCD0
		internal static string Bad_CyclicComplex(object p0)
		{
			return EntityRes.GetString("Bad_CyclicComplex", new object[] { p0 });
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0002DAF4 File Offset: 0x0002BCF4
		internal static string Bad_CyclicEntityContainer(object p0)
		{
			return EntityRes.GetString("Bad_CyclicEntityContainer", new object[] { p0 });
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002DB18 File Offset: 0x0002BD18
		internal static string Bad_UnresolvedNavigationPropertyPath(object p0, object p1)
		{
			return EntityRes.GetString("Bad_UnresolvedNavigationPropertyPath", new object[] { p0, p1 });
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0002DB3F File Offset: 0x0002BD3F
		internal static string RuleSet_DuplicateRulesExistInRuleSet
		{
			get
			{
				return EntityRes.GetString("RuleSet_DuplicateRulesExistInRuleSet");
			}
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0002DB4C File Offset: 0x0002BD4C
		internal static string EdmToClr_UnsupportedTypeCode(object p0)
		{
			return EntityRes.GetString("EdmToClr_UnsupportedTypeCode", new object[] { p0 });
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0002DB6F File Offset: 0x0002BD6F
		internal static string EdmToClr_StructuredValueMappedToNonClass
		{
			get
			{
				return EntityRes.GetString("EdmToClr_StructuredValueMappedToNonClass");
			}
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0002DB7C File Offset: 0x0002BD7C
		internal static string EdmToClr_IEnumerableOfTPropertyAlreadyHasValue(object p0, object p1)
		{
			return EntityRes.GetString("EdmToClr_IEnumerableOfTPropertyAlreadyHasValue", new object[] { p0, p1 });
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0002DBA4 File Offset: 0x0002BDA4
		internal static string EdmToClr_StructuredPropertyDuplicateValue(object p0)
		{
			return EntityRes.GetString("EdmToClr_StructuredPropertyDuplicateValue", new object[] { p0 });
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
		internal static string EdmToClr_CannotConvertEdmValueToClrType(object p0, object p1)
		{
			return EntityRes.GetString("EdmToClr_CannotConvertEdmValueToClrType", new object[] { p0, p1 });
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0002DBF0 File Offset: 0x0002BDF0
		internal static string EdmToClr_CannotConvertEdmCollectionValueToClrType(object p0)
		{
			return EntityRes.GetString("EdmToClr_CannotConvertEdmCollectionValueToClrType", new object[] { p0 });
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0002DC14 File Offset: 0x0002BE14
		internal static string EdmToClr_TryCreateObjectInstanceReturnedWrongObject(object p0, object p1)
		{
			return EntityRes.GetString("EdmToClr_TryCreateObjectInstanceReturnedWrongObject", new object[] { p0, p1 });
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0002DC3B File Offset: 0x0002BE3B
		internal static string EdmUtil_NullValueForMimeTypeAnnotation
		{
			get
			{
				return EntityRes.GetString("EdmUtil_NullValueForMimeTypeAnnotation");
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002DC48 File Offset: 0x0002BE48
		internal static string EdmUtil_InvalidAnnotationValue(object p0, object p1)
		{
			return EntityRes.GetString("EdmUtil_InvalidAnnotationValue", new object[] { p0, p1 });
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0002DC70 File Offset: 0x0002BE70
		internal static string PlatformHelper_DateTimeOffsetMustContainTimeZone(object p0)
		{
			return EntityRes.GetString("PlatformHelper_DateTimeOffsetMustContainTimeZone", new object[] { p0 });
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0002DC93 File Offset: 0x0002BE93
		internal static string Date_InvalidAddedOrSubtractedResults
		{
			get
			{
				return EntityRes.GetString("Date_InvalidAddedOrSubtractedResults");
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0002DCA0 File Offset: 0x0002BEA0
		internal static string Date_InvalidDateParameters(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Date_InvalidDateParameters", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0002DCCC File Offset: 0x0002BECC
		internal static string Date_InvalidParsingString(object p0)
		{
			return EntityRes.GetString("Date_InvalidParsingString", new object[] { p0 });
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0002DCF0 File Offset: 0x0002BEF0
		internal static string Date_InvalidCompareToTarget(object p0)
		{
			return EntityRes.GetString("Date_InvalidCompareToTarget", new object[] { p0 });
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0002DD14 File Offset: 0x0002BF14
		internal static string TimeOfDay_InvalidTimeOfDayParameters(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("TimeOfDay_InvalidTimeOfDayParameters", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0002DD44 File Offset: 0x0002BF44
		internal static string TimeOfDay_TicksOutOfRange(object p0)
		{
			return EntityRes.GetString("TimeOfDay_TicksOutOfRange", new object[] { p0 });
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0002DD68 File Offset: 0x0002BF68
		internal static string TimeOfDay_ConvertErrorFromTimeSpan(object p0)
		{
			return EntityRes.GetString("TimeOfDay_ConvertErrorFromTimeSpan", new object[] { p0 });
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0002DD8C File Offset: 0x0002BF8C
		internal static string TimeOfDay_InvalidParsingString(object p0)
		{
			return EntityRes.GetString("TimeOfDay_InvalidParsingString", new object[] { p0 });
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0002DDB0 File Offset: 0x0002BFB0
		internal static string TimeOfDay_InvalidCompareToTarget(object p0)
		{
			return EntityRes.GetString("TimeOfDay_InvalidCompareToTarget", new object[] { p0 });
		}
	}
}
