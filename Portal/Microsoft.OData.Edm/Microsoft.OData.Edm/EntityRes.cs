using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000D4 RID: 212
	internal sealed class EntityRes
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x0000DE87 File Offset: 0x0000C087
		internal EntityRes()
		{
			this.resources = new ResourceManager("Microsoft.OData.Edm", base.GetType().GetTypeInfo().Assembly);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		private static EntityRes GetLoader()
		{
			if (EntityRes.loader == null)
			{
				EntityRes entityRes = new EntityRes();
				Interlocked.CompareExchange<EntityRes>(ref EntityRes.loader, entityRes, null);
			}
			return EntityRes.loader;
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x000026B0 File Offset: 0x000008B0
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000DEDC File Offset: 0x0000C0DC
		public static ResourceManager Resources
		{
			get
			{
				return EntityRes.GetLoader().resources;
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
		public static string GetString(string name, params object[] args)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			string @string = entityRes.resources.GetString(name, EntityRes.Culture);
			if (args != null && args.Length != 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					string text = args[i] as string;
					if (text != null && text.Length > 1024)
					{
						args[i] = text.Substring(0, 1021) + "...";
					}
				}
				return string.Format(CultureInfo.CurrentCulture, @string, args);
			}
			return @string;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000DF68 File Offset: 0x0000C168
		public static string GetString(string name)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			return entityRes.resources.GetString(name, EntityRes.Culture);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000DF91 File Offset: 0x0000C191
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return EntityRes.GetString(name);
		}

		// Token: 0x040001B3 RID: 435
		internal const string EdmPrimitive_UnexpectedKind = "EdmPrimitive_UnexpectedKind";

		// Token: 0x040001B4 RID: 436
		internal const string EdmPath_UnexpectedKind = "EdmPath_UnexpectedKind";

		// Token: 0x040001B5 RID: 437
		internal const string Annotations_TypeMismatch = "Annotations_TypeMismatch";

		// Token: 0x040001B6 RID: 438
		internal const string Constructable_VocabularyAnnotationMustHaveTarget = "Constructable_VocabularyAnnotationMustHaveTarget";

		// Token: 0x040001B7 RID: 439
		internal const string Constructable_EntityTypeOrCollectionOfEntityTypeExpected = "Constructable_EntityTypeOrCollectionOfEntityTypeExpected";

		// Token: 0x040001B8 RID: 440
		internal const string Constructable_TargetMustBeStock = "Constructable_TargetMustBeStock";

		// Token: 0x040001B9 RID: 441
		internal const string TypeSemantics_CouldNotConvertTypeReference = "TypeSemantics_CouldNotConvertTypeReference";

		// Token: 0x040001BA RID: 442
		internal const string EdmModel_CannotUseElementWithTypeNone = "EdmModel_CannotUseElementWithTypeNone";

		// Token: 0x040001BB RID: 443
		internal const string EdmModel_CannotAddMoreThanOneEntityContainerToOneEdmModel = "EdmModel_CannotAddMoreThanOneEntityContainerToOneEdmModel";

		// Token: 0x040001BC RID: 444
		internal const string EdmEntityContainer_CannotUseElementWithTypeNone = "EdmEntityContainer_CannotUseElementWithTypeNone";

		// Token: 0x040001BD RID: 445
		internal const string ValueWriter_NonSerializableValue = "ValueWriter_NonSerializableValue";

		// Token: 0x040001BE RID: 446
		internal const string ValueHasAlreadyBeenSet = "ValueHasAlreadyBeenSet";

		// Token: 0x040001BF RID: 447
		internal const string PathSegmentMustNotContainSlash = "PathSegmentMustNotContainSlash";

		// Token: 0x040001C0 RID: 448
		internal const string Constructable_DependentPropertyCountMustMatchNumberOfPropertiesOnPrincipalType = "Constructable_DependentPropertyCountMustMatchNumberOfPropertiesOnPrincipalType";

		// Token: 0x040001C1 RID: 449
		internal const string EdmType_UnexpectedEdmType = "EdmType_UnexpectedEdmType";

		// Token: 0x040001C2 RID: 450
		internal const string NavigationPropertyBinding_PathIsNotValid = "NavigationPropertyBinding_PathIsNotValid";

		// Token: 0x040001C3 RID: 451
		internal const string Edm_Evaluator_NoTermTypeAnnotationOnType = "Edm_Evaluator_NoTermTypeAnnotationOnType";

		// Token: 0x040001C4 RID: 452
		internal const string Edm_Evaluator_NoValueAnnotationOnType = "Edm_Evaluator_NoValueAnnotationOnType";

		// Token: 0x040001C5 RID: 453
		internal const string Edm_Evaluator_NoValueAnnotationOnElement = "Edm_Evaluator_NoValueAnnotationOnElement";

		// Token: 0x040001C6 RID: 454
		internal const string Edm_Evaluator_UnrecognizedExpressionKind = "Edm_Evaluator_UnrecognizedExpressionKind";

		// Token: 0x040001C7 RID: 455
		internal const string Edm_Evaluator_UnboundFunction = "Edm_Evaluator_UnboundFunction";

		// Token: 0x040001C8 RID: 456
		internal const string Edm_Evaluator_UnboundPath = "Edm_Evaluator_UnboundPath";

		// Token: 0x040001C9 RID: 457
		internal const string Edm_Evaluator_NoContextPath = "Edm_Evaluator_NoContextPath";

		// Token: 0x040001CA RID: 458
		internal const string Edm_Evaluator_FailedTypeAssertion = "Edm_Evaluator_FailedTypeAssertion";

		// Token: 0x040001CB RID: 459
		internal const string Edm_Evaluator_TypeCastNeedsEdmModel = "Edm_Evaluator_TypeCastNeedsEdmModel";

		// Token: 0x040001CC RID: 460
		internal const string EdmModel_Validator_Semantic_SystemNamespaceEncountered = "EdmModel_Validator_Semantic_SystemNamespaceEncountered";

		// Token: 0x040001CD RID: 461
		internal const string EdmModel_Validator_Semantic_NavigationSourceTypeHasNoKeys = "EdmModel_Validator_Semantic_NavigationSourceTypeHasNoKeys";

		// Token: 0x040001CE RID: 462
		internal const string EdmModel_Validator_Semantic_DuplicateEndName = "EdmModel_Validator_Semantic_DuplicateEndName";

		// Token: 0x040001CF RID: 463
		internal const string EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey = "EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey";

		// Token: 0x040001D0 RID: 464
		internal const string EdmModel_Validator_Semantic_InvalidComplexTypeAbstract = "EdmModel_Validator_Semantic_InvalidComplexTypeAbstract";

		// Token: 0x040001D1 RID: 465
		internal const string EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic = "EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic";

		// Token: 0x040001D2 RID: 466
		internal const string EdmModel_Validator_Semantic_InvalidKeyNullablePart = "EdmModel_Validator_Semantic_InvalidKeyNullablePart";

		// Token: 0x040001D3 RID: 467
		internal const string EdmModel_Validator_Semantic_EntityKeyMustBeScalar = "EdmModel_Validator_Semantic_EntityKeyMustBeScalar";

		// Token: 0x040001D4 RID: 468
		internal const string EdmModel_Validator_Semantic_EntityComposableBoundEscapeFunctionMustBeLessOne = "EdmModel_Validator_Semantic_EntityComposableBoundEscapeFunctionMustBeLessOne";

		// Token: 0x040001D5 RID: 469
		internal const string EdmModel_Validator_Semantic_EntityNoncomposableBoundEscapeFunctionMustBeLessOne = "EdmModel_Validator_Semantic_EntityNoncomposableBoundEscapeFunctionMustBeLessOne";

		// Token: 0x040001D6 RID: 470
		internal const string EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass = "EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass";

		// Token: 0x040001D7 RID: 471
		internal const string EdmModel_Validator_Semantic_KeyMissingOnEntityType = "EdmModel_Validator_Semantic_KeyMissingOnEntityType";

		// Token: 0x040001D8 RID: 472
		internal const string EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole = "EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole";

		// Token: 0x040001D9 RID: 473
		internal const string EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame = "EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame";

		// Token: 0x040001DA RID: 474
		internal const string EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType = "EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType";

		// Token: 0x040001DB RID: 475
		internal const string EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation = "EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation";

		// Token: 0x040001DC RID: 476
		internal const string EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified = "EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified";

		// Token: 0x040001DD RID: 477
		internal const string EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate = "EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate";

		// Token: 0x040001DE RID: 478
		internal const string EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint = "EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint";

		// Token: 0x040001DF RID: 479
		internal const string EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne = "EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne";

		// Token: 0x040001E0 RID: 480
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable = "EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable";

		// Token: 0x040001E1 RID: 481
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable = "EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable";

		// Token: 0x040001E2 RID: 482
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne = "EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne";

		// Token: 0x040001E3 RID: 483
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany = "EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany";

		// Token: 0x040001E4 RID: 484
		internal const string EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint = "EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint";

		// Token: 0x040001E5 RID: 485
		internal const string EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint = "EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint";

		// Token: 0x040001E6 RID: 486
		internal const string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintDependentEnd = "EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintDependentEnd";

		// Token: 0x040001E7 RID: 487
		internal const string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintPrimaryEnd = "EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintPrimaryEnd";

		// Token: 0x040001E8 RID: 488
		internal const string EdmModel_Validator_Semantic_InvalidPropertyType = "EdmModel_Validator_Semantic_InvalidPropertyType";

		// Token: 0x040001E9 RID: 489
		internal const string EdmModel_Validator_Semantic_BoundOperationMustHaveParameters = "EdmModel_Validator_Semantic_BoundOperationMustHaveParameters";

		// Token: 0x040001EA RID: 490
		internal const string EdmModel_Validator_Semantic_RequiredParametersMustPrecedeOptional = "EdmModel_Validator_Semantic_RequiredParametersMustPrecedeOptional";

		// Token: 0x040001EB RID: 491
		internal const string EdmModel_Validator_Semantic_OperationWithUnsupportedReturnType = "EdmModel_Validator_Semantic_OperationWithUnsupportedReturnType";

		// Token: 0x040001EC RID: 492
		internal const string EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet = "EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet";

		// Token: 0x040001ED RID: 493
		internal const string EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet2 = "EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet2";

		// Token: 0x040001EE RID: 494
		internal const string EdmModel_Validator_Semantic_OperationImportEntitySetExpressionKindIsInvalid = "EdmModel_Validator_Semantic_OperationImportEntitySetExpressionKindIsInvalid";

		// Token: 0x040001EF RID: 495
		internal const string EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid = "EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid";

		// Token: 0x040001F0 RID: 496
		internal const string EdmModel_Validator_Semantic_OperationImportSpecifiesEntitySetButNotEntityType = "EdmModel_Validator_Semantic_OperationImportSpecifiesEntitySetButNotEntityType";

		// Token: 0x040001F1 RID: 497
		internal const string EdmModel_Validator_Semantic_OperationImportCannotImportBoundOperation = "EdmModel_Validator_Semantic_OperationImportCannotImportBoundOperation";

		// Token: 0x040001F2 RID: 498
		internal const string EdmModel_Validator_Semantic_FunctionImportWithParameterShouldNotBeIncludedInServiceDocument = "EdmModel_Validator_Semantic_FunctionImportWithParameterShouldNotBeIncludedInServiceDocument";

		// Token: 0x040001F3 RID: 499
		internal const string EdmModel_Validator_Semantic_FunctionMustHaveReturnType = "EdmModel_Validator_Semantic_FunctionMustHaveReturnType";

		// Token: 0x040001F4 RID: 500
		internal const string EdmModel_Validator_Semantic_UrlEscapeFunctionMustBoundFunction = "EdmModel_Validator_Semantic_UrlEscapeFunctionMustBoundFunction";

		// Token: 0x040001F5 RID: 501
		internal const string EdmModel_Validator_Semantic_UrlEscapeFunctionMustHaveOneStringParameter = "EdmModel_Validator_Semantic_UrlEscapeFunctionMustHaveOneStringParameter";

		// Token: 0x040001F6 RID: 502
		internal const string EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate = "EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate";

		// Token: 0x040001F7 RID: 503
		internal const string EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName = "EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName";

		// Token: 0x040001F8 RID: 504
		internal const string EdmModel_Validator_Semantic_UnboundFunctionOverloadHasIncorrectReturnType = "EdmModel_Validator_Semantic_UnboundFunctionOverloadHasIncorrectReturnType";

		// Token: 0x040001F9 RID: 505
		internal const string EdmModel_Validator_Semantic_OperationCannotHaveEntitySetPathWithUnBoundOperation = "EdmModel_Validator_Semantic_OperationCannotHaveEntitySetPathWithUnBoundOperation";

		// Token: 0x040001FA RID: 506
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathMissingBindingParameterName = "EdmModel_Validator_Semantic_InvalidEntitySetPathMissingBindingParameterName";

		// Token: 0x040001FB RID: 507
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathWithFirstPathParameterNotMatchingFirstParameterName = "EdmModel_Validator_Semantic_InvalidEntitySetPathWithFirstPathParameterNotMatchingFirstParameterName";

		// Token: 0x040001FC RID: 508
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathTypeCastSegmentMustBeEntityType = "EdmModel_Validator_Semantic_InvalidEntitySetPathTypeCastSegmentMustBeEntityType";

		// Token: 0x040001FD RID: 509
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownNavigationProperty = "EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownNavigationProperty";

		// Token: 0x040001FE RID: 510
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathInvalidTypeCastSegment = "EdmModel_Validator_Semantic_InvalidEntitySetPathInvalidTypeCastSegment";

		// Token: 0x040001FF RID: 511
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathWithNonEntityBindingParameter = "EdmModel_Validator_Semantic_InvalidEntitySetPathWithNonEntityBindingParameter";

		// Token: 0x04000200 RID: 512
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownTypeCastSegment = "EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownTypeCastSegment";

		// Token: 0x04000201 RID: 513
		internal const string EdmModel_Validator_Semantic_OperationWithEntitySetPathReturnTypeInvalid = "EdmModel_Validator_Semantic_OperationWithEntitySetPathReturnTypeInvalid";

		// Token: 0x04000202 RID: 514
		internal const string EdmModel_Validator_Semantic_OperationWithEntitySetPathAndReturnTypeTypeNotAssignable = "EdmModel_Validator_Semantic_OperationWithEntitySetPathAndReturnTypeTypeNotAssignable";

		// Token: 0x04000203 RID: 515
		internal const string EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType = "EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType";

		// Token: 0x04000204 RID: 516
		internal const string EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined = "EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined";

		// Token: 0x04000205 RID: 517
		internal const string EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName = "EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName";

		// Token: 0x04000206 RID: 518
		internal const string EdmModel_Validator_Semantic_PropertyNameAlreadyDefined = "EdmModel_Validator_Semantic_PropertyNameAlreadyDefined";

		// Token: 0x04000207 RID: 519
		internal const string EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind = "EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind";

		// Token: 0x04000208 RID: 520
		internal const string EdmModel_Validator_Semantic_BaseTypeOfOpenTypeMustBeOpen = "EdmModel_Validator_Semantic_BaseTypeOfOpenTypeMustBeOpen";

		// Token: 0x04000209 RID: 521
		internal const string EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity = "EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity";

		// Token: 0x0400020A RID: 522
		internal const string EdmModel_Validator_Semantic_EdmPrimitiveTypeCannotBeUsedAsTypeOfKey = "EdmModel_Validator_Semantic_EdmPrimitiveTypeCannotBeUsedAsTypeOfKey";

		// Token: 0x0400020B RID: 523
		internal const string EdmModel_Validator_Semantic_EdmPrimitiveTypeCannotBeUsedAsUnderlyingType = "EdmModel_Validator_Semantic_EdmPrimitiveTypeCannotBeUsedAsUnderlyingType";

		// Token: 0x0400020C RID: 524
		internal const string EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity = "EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity";

		// Token: 0x0400020D RID: 525
		internal const string EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect = "EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect";

		// Token: 0x0400020E RID: 526
		internal const string EdmModel_Validator_Semantic_InaccessibleType = "EdmModel_Validator_Semantic_InaccessibleType";

		// Token: 0x0400020F RID: 527
		internal const string EdmModel_Validator_Semantic_AmbiguousType = "EdmModel_Validator_Semantic_AmbiguousType";

		// Token: 0x04000210 RID: 528
		internal const string EdmModel_Validator_Semantic_InvalidNavigationPropertyType = "EdmModel_Validator_Semantic_InvalidNavigationPropertyType";

		// Token: 0x04000211 RID: 529
		internal const string EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional = "EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional";

		// Token: 0x04000212 RID: 530
		internal const string EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne = "EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne";

		// Token: 0x04000213 RID: 531
		internal const string EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne = "EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne";

		// Token: 0x04000214 RID: 532
		internal const string EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties = "EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties";

		// Token: 0x04000215 RID: 533
		internal const string EdmModel_Validator_Semantic_DuplicateDependentProperty = "EdmModel_Validator_Semantic_DuplicateDependentProperty";

		// Token: 0x04000216 RID: 534
		internal const string EdmModel_Validator_Semantic_ScaleOutOfRange = "EdmModel_Validator_Semantic_ScaleOutOfRange";

		// Token: 0x04000217 RID: 535
		internal const string EdmModel_Validator_Semantic_PrecisionOutOfRange = "EdmModel_Validator_Semantic_PrecisionOutOfRange";

		// Token: 0x04000218 RID: 536
		internal const string EdmModel_Validator_Semantic_StringMaxLengthOutOfRange = "EdmModel_Validator_Semantic_StringMaxLengthOutOfRange";

		// Token: 0x04000219 RID: 537
		internal const string EdmModel_Validator_Semantic_MaxLengthOutOfRange = "EdmModel_Validator_Semantic_MaxLengthOutOfRange";

		// Token: 0x0400021A RID: 538
		internal const string EdmModel_Validator_Semantic_EnumMemberValueOutOfRange = "EdmModel_Validator_Semantic_EnumMemberValueOutOfRange";

		// Token: 0x0400021B RID: 539
		internal const string EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined = "EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined";

		// Token: 0x0400021C RID: 540
		internal const string EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly = "EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly";

		// Token: 0x0400021D RID: 541
		internal const string EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull = "EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull";

		// Token: 0x0400021E RID: 542
		internal const string EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm = "EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm";

		// Token: 0x0400021F RID: 543
		internal const string EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml = "EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml";

		// Token: 0x04000220 RID: 544
		internal const string EdmModel_Validator_Semantic_InvalidElementAnnotationNotIEdmStringValue = "EdmModel_Validator_Semantic_InvalidElementAnnotationNotIEdmStringValue";

		// Token: 0x04000221 RID: 545
		internal const string EdmModel_Validator_Semantic_InvalidElementAnnotationNullNamespaceOrName = "EdmModel_Validator_Semantic_InvalidElementAnnotationNullNamespaceOrName";

		// Token: 0x04000222 RID: 546
		internal const string EdmModel_Validator_Semantic_CannotAssertNullableTypeAsNonNullableType = "EdmModel_Validator_Semantic_CannotAssertNullableTypeAsNonNullableType";

		// Token: 0x04000223 RID: 547
		internal const string EdmModel_Validator_Semantic_ExpressionPrimitiveKindCannotPromoteToAssertedType = "EdmModel_Validator_Semantic_ExpressionPrimitiveKindCannotPromoteToAssertedType";

		// Token: 0x04000224 RID: 548
		internal const string EdmModel_Validator_Semantic_NullCannotBeAssertedToBeANonNullableType = "EdmModel_Validator_Semantic_NullCannotBeAssertedToBeANonNullableType";

		// Token: 0x04000225 RID: 549
		internal const string EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType = "EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType";

		// Token: 0x04000226 RID: 550
		internal const string EdmModel_Validator_Semantic_CollectionExpressionNotValidForNonCollectionType = "EdmModel_Validator_Semantic_CollectionExpressionNotValidForNonCollectionType";

		// Token: 0x04000227 RID: 551
		internal const string EdmModel_Validator_Semantic_PrimitiveConstantExpressionNotValidForNonPrimitiveType = "EdmModel_Validator_Semantic_PrimitiveConstantExpressionNotValidForNonPrimitiveType";

		// Token: 0x04000228 RID: 552
		internal const string EdmModel_Validator_Semantic_RecordExpressionNotValidForNonStructuredType = "EdmModel_Validator_Semantic_RecordExpressionNotValidForNonStructuredType";

		// Token: 0x04000229 RID: 553
		internal const string EdmModel_Validator_Semantic_RecordExpressionMissingProperty = "EdmModel_Validator_Semantic_RecordExpressionMissingProperty";

		// Token: 0x0400022A RID: 554
		internal const string EdmModel_Validator_Semantic_RecordExpressionHasExtraProperties = "EdmModel_Validator_Semantic_RecordExpressionHasExtraProperties";

		// Token: 0x0400022B RID: 555
		internal const string EdmModel_Validator_Semantic_DuplicateAnnotation = "EdmModel_Validator_Semantic_DuplicateAnnotation";

		// Token: 0x0400022C RID: 556
		internal const string EdmModel_Validator_Semantic_IncorrectNumberOfArguments = "EdmModel_Validator_Semantic_IncorrectNumberOfArguments";

		// Token: 0x0400022D RID: 557
		internal const string EdmModel_Validator_Semantic_DuplicateEntityContainerName = "EdmModel_Validator_Semantic_DuplicateEntityContainerName";

		// Token: 0x0400022E RID: 558
		internal const string EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType = "EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType";

		// Token: 0x0400022F RID: 559
		internal const string EdmModel_Validator_Semantic_ExpressionEnumKindNotValidForAssertedType = "EdmModel_Validator_Semantic_ExpressionEnumKindNotValidForAssertedType";

		// Token: 0x04000230 RID: 560
		internal const string EdmModel_Validator_Semantic_IntegerConstantValueOutOfRange = "EdmModel_Validator_Semantic_IntegerConstantValueOutOfRange";

		// Token: 0x04000231 RID: 561
		internal const string EdmModel_Validator_Semantic_StringConstantLengthOutOfRange = "EdmModel_Validator_Semantic_StringConstantLengthOutOfRange";

		// Token: 0x04000232 RID: 562
		internal const string EdmModel_Validator_Semantic_BinaryConstantLengthOutOfRange = "EdmModel_Validator_Semantic_BinaryConstantLengthOutOfRange";

		// Token: 0x04000233 RID: 563
		internal const string EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone = "EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone";

		// Token: 0x04000234 RID: 564
		internal const string EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone = "EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone";

		// Token: 0x04000235 RID: 565
		internal const string EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone = "EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone";

		// Token: 0x04000236 RID: 566
		internal const string EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone = "EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone";

		// Token: 0x04000237 RID: 567
		internal const string EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone = "EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone";

		// Token: 0x04000238 RID: 568
		internal const string EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping = "EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping";

		// Token: 0x04000239 RID: 569
		internal const string EdmModel_Validator_Semantic_NavigationMappingMustBeBidirectional = "EdmModel_Validator_Semantic_NavigationMappingMustBeBidirectional";

		// Token: 0x0400023A RID: 570
		internal const string EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty = "EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty";

		// Token: 0x0400023B RID: 571
		internal const string EdmModel_Validator_Semantic_TypeAnnotationMissingRequiredProperty = "EdmModel_Validator_Semantic_TypeAnnotationMissingRequiredProperty";

		// Token: 0x0400023C RID: 572
		internal const string EdmModel_Validator_Semantic_TypeAnnotationHasExtraProperties = "EdmModel_Validator_Semantic_TypeAnnotationHasExtraProperties";

		// Token: 0x0400023D RID: 573
		internal const string EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType = "EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType";

		// Token: 0x0400023E RID: 574
		internal const string EdmModel_Validator_Semantic_InaccessibleTerm = "EdmModel_Validator_Semantic_InaccessibleTerm";

		// Token: 0x0400023F RID: 575
		internal const string EdmModel_Validator_Semantic_InaccessibleTarget = "EdmModel_Validator_Semantic_InaccessibleTarget";

		// Token: 0x04000240 RID: 576
		internal const string EdmModel_Validator_Semantic_VocabularyAnnotationApplyToNotAllowedAnnotatable = "EdmModel_Validator_Semantic_VocabularyAnnotationApplyToNotAllowedAnnotatable";

		// Token: 0x04000241 RID: 577
		internal const string EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique = "EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique";

		// Token: 0x04000242 RID: 578
		internal const string EdmModel_Validator_Semantic_NoEntitySetsFoundForType = "EdmModel_Validator_Semantic_NoEntitySetsFoundForType";

		// Token: 0x04000243 RID: 579
		internal const string EdmModel_Validator_Semantic_CannotInferEntitySetWithMultipleSetsPerType = "EdmModel_Validator_Semantic_CannotInferEntitySetWithMultipleSetsPerType";

		// Token: 0x04000244 RID: 580
		internal const string EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet = "EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet";

		// Token: 0x04000245 RID: 581
		internal const string EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself = "EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself";

		// Token: 0x04000246 RID: 582
		internal const string EdmModel_Validator_Semantic_PathIsNotValidForTheGivenContext = "EdmModel_Validator_Semantic_PathIsNotValidForTheGivenContext";

		// Token: 0x04000247 RID: 583
		internal const string EdmModel_Validator_Semantic_NavigationPropertyMappingMustPointToValidTargetForProperty = "EdmModel_Validator_Semantic_NavigationPropertyMappingMustPointToValidTargetForProperty";

		// Token: 0x04000248 RID: 584
		internal const string EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterNames = "EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterNames";

		// Token: 0x04000249 RID: 585
		internal const string EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterTypes = "EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterTypes";

		// Token: 0x0400024A RID: 586
		internal const string EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterNames = "EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterNames";

		// Token: 0x0400024B RID: 587
		internal const string EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterTypes = "EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterTypes";

		// Token: 0x0400024C RID: 588
		internal const string EdmModel_Validator_Semantic_ModelDuplicateBoundActions = "EdmModel_Validator_Semantic_ModelDuplicateBoundActions";

		// Token: 0x0400024D RID: 589
		internal const string EdmModel_Validator_Semantic_ModelDuplicateUnBoundActions = "EdmModel_Validator_Semantic_ModelDuplicateUnBoundActions";

		// Token: 0x0400024E RID: 590
		internal const string EdmModel_Validator_Semantic_BoundFunctionOverloadsMustHaveSameReturnType = "EdmModel_Validator_Semantic_BoundFunctionOverloadsMustHaveSameReturnType";

		// Token: 0x0400024F RID: 591
		internal const string EdmModel_Validator_Semantic_EntitySetTypeMustBeCollectionOfEntityType = "EdmModel_Validator_Semantic_EntitySetTypeMustBeCollectionOfEntityType";

		// Token: 0x04000250 RID: 592
		internal const string EdmModel_Validator_Semantic_SingletonTypeMustBeEntityType = "EdmModel_Validator_Semantic_SingletonTypeMustBeEntityType";

		// Token: 0x04000251 RID: 593
		internal const string EdmModel_Validator_Semantic_NavigationPropertyOfCollectionTypeMustNotTargetToSingleton = "EdmModel_Validator_Semantic_NavigationPropertyOfCollectionTypeMustNotTargetToSingleton";

		// Token: 0x04000252 RID: 594
		internal const string EdmModel_Validator_Semantic_StructuredTypeBaseTypeCannotBeAbstractType = "EdmModel_Validator_Semantic_StructuredTypeBaseTypeCannotBeAbstractType";

		// Token: 0x04000253 RID: 595
		internal const string EdmModel_Validator_Semantic_PropertyTypeCannotBeCollectionOfAbstractType = "EdmModel_Validator_Semantic_PropertyTypeCannotBeCollectionOfAbstractType";

		// Token: 0x04000254 RID: 596
		internal const string EdmModel_Validator_Semantic_OperationReturnTypeCannotBeCollectionOfAbstractType = "EdmModel_Validator_Semantic_OperationReturnTypeCannotBeCollectionOfAbstractType";

		// Token: 0x04000255 RID: 597
		internal const string EdmModel_Validator_Semantic_EdmEntityTypeCannotBeTypeOfSingleton = "EdmModel_Validator_Semantic_EdmEntityTypeCannotBeTypeOfSingleton";

		// Token: 0x04000256 RID: 598
		internal const string EdmModel_Validator_Semantic_EdmEntityTypeCannotBeTypeOfEntitySet = "EdmModel_Validator_Semantic_EdmEntityTypeCannotBeTypeOfEntitySet";

		// Token: 0x04000257 RID: 599
		internal const string EdmModel_Validator_Semantic_DeclaringTypeOfNavigationSourceCannotHavePathProperty = "EdmModel_Validator_Semantic_DeclaringTypeOfNavigationSourceCannotHavePathProperty";

		// Token: 0x04000258 RID: 600
		internal const string EdmModel_Validator_Semantic_TypeOfNavigationPropertyCannotHavePathProperty = "EdmModel_Validator_Semantic_TypeOfNavigationPropertyCannotHavePathProperty";

		// Token: 0x04000259 RID: 601
		internal const string EdmModel_Validator_Syntactic_MissingName = "EdmModel_Validator_Syntactic_MissingName";

		// Token: 0x0400025A RID: 602
		internal const string EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong = "EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong";

		// Token: 0x0400025B RID: 603
		internal const string EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed = "EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed";

		// Token: 0x0400025C RID: 604
		internal const string EdmModel_Validator_Syntactic_MissingNamespaceName = "EdmModel_Validator_Syntactic_MissingNamespaceName";

		// Token: 0x0400025D RID: 605
		internal const string EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong = "EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong";

		// Token: 0x0400025E RID: 606
		internal const string EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed = "EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed";

		// Token: 0x0400025F RID: 607
		internal const string EdmModel_Validator_Syntactic_PropertyMustNotBeNull = "EdmModel_Validator_Syntactic_PropertyMustNotBeNull";

		// Token: 0x04000260 RID: 608
		internal const string EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange = "EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange";

		// Token: 0x04000261 RID: 609
		internal const string EdmModel_Validator_Syntactic_InterfaceKindValueMismatch = "EdmModel_Validator_Syntactic_InterfaceKindValueMismatch";

		// Token: 0x04000262 RID: 610
		internal const string EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch = "EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch";

		// Token: 0x04000263 RID: 611
		internal const string EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected = "EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected";

		// Token: 0x04000264 RID: 612
		internal const string EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements = "EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements";

		// Token: 0x04000265 RID: 613
		internal const string EdmModel_Validator_Syntactic_NavigationPartnerInvalid = "EdmModel_Validator_Syntactic_NavigationPartnerInvalid";

		// Token: 0x04000266 RID: 614
		internal const string EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy = "EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy";

		// Token: 0x04000267 RID: 615
		internal const string Serializer_SingleFileExpected = "Serializer_SingleFileExpected";

		// Token: 0x04000268 RID: 616
		internal const string Serializer_UnknownEdmVersion = "Serializer_UnknownEdmVersion";

		// Token: 0x04000269 RID: 617
		internal const string Serializer_UnknownEdmxVersion = "Serializer_UnknownEdmxVersion";

		// Token: 0x0400026A RID: 618
		internal const string Serializer_NonInlineOperationImportReturnType = "Serializer_NonInlineOperationImportReturnType";

		// Token: 0x0400026B RID: 619
		internal const string Serializer_ReferencedTypeMustHaveValidName = "Serializer_ReferencedTypeMustHaveValidName";

		// Token: 0x0400026C RID: 620
		internal const string Serializer_OutOfLineAnnotationTargetMustHaveValidName = "Serializer_OutOfLineAnnotationTargetMustHaveValidName";

		// Token: 0x0400026D RID: 621
		internal const string Serializer_NoSchemasProduced = "Serializer_NoSchemasProduced";

		// Token: 0x0400026E RID: 622
		internal const string XmlParser_EmptyFile = "XmlParser_EmptyFile";

		// Token: 0x0400026F RID: 623
		internal const string XmlParser_EmptySchemaTextReader = "XmlParser_EmptySchemaTextReader";

		// Token: 0x04000270 RID: 624
		internal const string XmlParser_MissingAttribute = "XmlParser_MissingAttribute";

		// Token: 0x04000271 RID: 625
		internal const string XmlParser_TextNotAllowed = "XmlParser_TextNotAllowed";

		// Token: 0x04000272 RID: 626
		internal const string XmlParser_UnexpectedAttribute = "XmlParser_UnexpectedAttribute";

		// Token: 0x04000273 RID: 627
		internal const string XmlParser_UnexpectedElement = "XmlParser_UnexpectedElement";

		// Token: 0x04000274 RID: 628
		internal const string XmlParser_UnusedElement = "XmlParser_UnusedElement";

		// Token: 0x04000275 RID: 629
		internal const string XmlParser_UnexpectedNodeType = "XmlParser_UnexpectedNodeType";

		// Token: 0x04000276 RID: 630
		internal const string XmlParser_UnexpectedRootElement = "XmlParser_UnexpectedRootElement";

		// Token: 0x04000277 RID: 631
		internal const string XmlParser_UnexpectedRootElementWrongNamespace = "XmlParser_UnexpectedRootElementWrongNamespace";

		// Token: 0x04000278 RID: 632
		internal const string XmlParser_UnexpectedRootElementNoNamespace = "XmlParser_UnexpectedRootElementNoNamespace";

		// Token: 0x04000279 RID: 633
		internal const string CsdlParser_InvalidEntitySetPathWithUnboundAction = "CsdlParser_InvalidEntitySetPathWithUnboundAction";

		// Token: 0x0400027A RID: 634
		internal const string CsdlParser_InvalidAlias = "CsdlParser_InvalidAlias";

		// Token: 0x0400027B RID: 635
		internal const string CsdlParser_InvalidDeleteAction = "CsdlParser_InvalidDeleteAction";

		// Token: 0x0400027C RID: 636
		internal const string CsdlParser_MissingTypeAttributeOrElement = "CsdlParser_MissingTypeAttributeOrElement";

		// Token: 0x0400027D RID: 637
		internal const string CsdlParser_InvalidEndRoleInRelationshipConstraint = "CsdlParser_InvalidEndRoleInRelationshipConstraint";

		// Token: 0x0400027E RID: 638
		internal const string CsdlParser_InvalidMultiplicity = "CsdlParser_InvalidMultiplicity";

		// Token: 0x0400027F RID: 639
		internal const string CsdlParser_ReferentialConstraintRequiresOneDependent = "CsdlParser_ReferentialConstraintRequiresOneDependent";

		// Token: 0x04000280 RID: 640
		internal const string CsdlParser_ReferentialConstraintRequiresOnePrincipal = "CsdlParser_ReferentialConstraintRequiresOnePrincipal";

		// Token: 0x04000281 RID: 641
		internal const string CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands = "CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands";

		// Token: 0x04000282 RID: 642
		internal const string CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands = "CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands";

		// Token: 0x04000283 RID: 643
		internal const string CsdlParser_InvalidCastExpressionIncorrectNumberOfOperands = "CsdlParser_InvalidCastExpressionIncorrectNumberOfOperands";

		// Token: 0x04000284 RID: 644
		internal const string CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands = "CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands";

		// Token: 0x04000285 RID: 645
		internal const string CsdlParser_InvalidTypeName = "CsdlParser_InvalidTypeName";

		// Token: 0x04000286 RID: 646
		internal const string CsdlParser_InvalidQualifiedName = "CsdlParser_InvalidQualifiedName";

		// Token: 0x04000287 RID: 647
		internal const string CsdlParser_NoReadersProvided = "CsdlParser_NoReadersProvided";

		// Token: 0x04000288 RID: 648
		internal const string CsdlParser_NullXmlReader = "CsdlParser_NullXmlReader";

		// Token: 0x04000289 RID: 649
		internal const string CsdlParser_InvalidEntitySetPath = "CsdlParser_InvalidEntitySetPath";

		// Token: 0x0400028A RID: 650
		internal const string CsdlParser_InvalidEnumMemberPath = "CsdlParser_InvalidEnumMemberPath";

		// Token: 0x0400028B RID: 651
		internal const string CsdlParser_CannotSpecifyNullableAttributeForNavigationPropertyWithCollectionType = "CsdlParser_CannotSpecifyNullableAttributeForNavigationPropertyWithCollectionType";

		// Token: 0x0400028C RID: 652
		internal const string CsdlParser_MetadataDocumentCannotHaveMoreThanOneEntityContainer = "CsdlParser_MetadataDocumentCannotHaveMoreThanOneEntityContainer";

		// Token: 0x0400028D RID: 653
		internal const string CsdlSemantics_ReferentialConstraintMismatch = "CsdlSemantics_ReferentialConstraintMismatch";

		// Token: 0x0400028E RID: 654
		internal const string CsdlSemantics_EnumMemberMustHaveValue = "CsdlSemantics_EnumMemberMustHaveValue";

		// Token: 0x0400028F RID: 655
		internal const string CsdlSemantics_ImpossibleAnnotationsTarget = "CsdlSemantics_ImpossibleAnnotationsTarget";

		// Token: 0x04000290 RID: 656
		internal const string CsdlSemantics_DuplicateAlias = "CsdlSemantics_DuplicateAlias";

		// Token: 0x04000291 RID: 657
		internal const string EdmxParser_EdmxVersionMismatch = "EdmxParser_EdmxVersionMismatch";

		// Token: 0x04000292 RID: 658
		internal const string EdmxParser_BodyElement = "EdmxParser_BodyElement";

		// Token: 0x04000293 RID: 659
		internal const string EdmxParser_InvalidReferenceIncorrectNumberOfIncludes = "EdmxParser_InvalidReferenceIncorrectNumberOfIncludes";

		// Token: 0x04000294 RID: 660
		internal const string EdmxParser_UnresolvedReferenceUriInEdmxReference = "EdmxParser_UnresolvedReferenceUriInEdmxReference";

		// Token: 0x04000295 RID: 661
		internal const string EdmParseException_ErrorsEncounteredInEdmx = "EdmParseException_ErrorsEncounteredInEdmx";

		// Token: 0x04000296 RID: 662
		internal const string ValueParser_InvalidBoolean = "ValueParser_InvalidBoolean";

		// Token: 0x04000297 RID: 663
		internal const string ValueParser_InvalidInteger = "ValueParser_InvalidInteger";

		// Token: 0x04000298 RID: 664
		internal const string ValueParser_InvalidLong = "ValueParser_InvalidLong";

		// Token: 0x04000299 RID: 665
		internal const string ValueParser_InvalidFloatingPoint = "ValueParser_InvalidFloatingPoint";

		// Token: 0x0400029A RID: 666
		internal const string ValueParser_InvalidMaxLength = "ValueParser_InvalidMaxLength";

		// Token: 0x0400029B RID: 667
		internal const string ValueParser_InvalidSrid = "ValueParser_InvalidSrid";

		// Token: 0x0400029C RID: 668
		internal const string ValueParser_InvalidScale = "ValueParser_InvalidScale";

		// Token: 0x0400029D RID: 669
		internal const string ValueParser_InvalidGuid = "ValueParser_InvalidGuid";

		// Token: 0x0400029E RID: 670
		internal const string ValueParser_InvalidDecimal = "ValueParser_InvalidDecimal";

		// Token: 0x0400029F RID: 671
		internal const string ValueParser_InvalidDateTimeOffset = "ValueParser_InvalidDateTimeOffset";

		// Token: 0x040002A0 RID: 672
		internal const string ValueParser_InvalidDateTime = "ValueParser_InvalidDateTime";

		// Token: 0x040002A1 RID: 673
		internal const string ValueParser_InvalidDate = "ValueParser_InvalidDate";

		// Token: 0x040002A2 RID: 674
		internal const string ValueParser_InvalidDuration = "ValueParser_InvalidDuration";

		// Token: 0x040002A3 RID: 675
		internal const string ValueParser_InvalidBinary = "ValueParser_InvalidBinary";

		// Token: 0x040002A4 RID: 676
		internal const string ValueParser_InvalidTimeOfDay = "ValueParser_InvalidTimeOfDay";

		// Token: 0x040002A5 RID: 677
		internal const string UnknownEnumVal_Multiplicity = "UnknownEnumVal_Multiplicity";

		// Token: 0x040002A6 RID: 678
		internal const string UnknownEnumVal_SchemaElementKind = "UnknownEnumVal_SchemaElementKind";

		// Token: 0x040002A7 RID: 679
		internal const string UnknownEnumVal_TypeKind = "UnknownEnumVal_TypeKind";

		// Token: 0x040002A8 RID: 680
		internal const string UnknownEnumVal_PrimitiveKind = "UnknownEnumVal_PrimitiveKind";

		// Token: 0x040002A9 RID: 681
		internal const string UnknownEnumVal_ContainerElementKind = "UnknownEnumVal_ContainerElementKind";

		// Token: 0x040002AA RID: 682
		internal const string UnknownEnumVal_CsdlTarget = "UnknownEnumVal_CsdlTarget";

		// Token: 0x040002AB RID: 683
		internal const string UnknownEnumVal_PropertyKind = "UnknownEnumVal_PropertyKind";

		// Token: 0x040002AC RID: 684
		internal const string UnknownEnumVal_ExpressionKind = "UnknownEnumVal_ExpressionKind";

		// Token: 0x040002AD RID: 685
		internal const string Bad_AmbiguousElementBinding = "Bad_AmbiguousElementBinding";

		// Token: 0x040002AE RID: 686
		internal const string Bad_UnresolvedType = "Bad_UnresolvedType";

		// Token: 0x040002AF RID: 687
		internal const string Bad_UnresolvedComplexType = "Bad_UnresolvedComplexType";

		// Token: 0x040002B0 RID: 688
		internal const string Bad_UnresolvedEntityType = "Bad_UnresolvedEntityType";

		// Token: 0x040002B1 RID: 689
		internal const string Bad_UnresolvedPrimitiveType = "Bad_UnresolvedPrimitiveType";

		// Token: 0x040002B2 RID: 690
		internal const string Bad_UnresolvedOperation = "Bad_UnresolvedOperation";

		// Token: 0x040002B3 RID: 691
		internal const string Bad_AmbiguousOperation = "Bad_AmbiguousOperation";

		// Token: 0x040002B4 RID: 692
		internal const string Bad_OperationParametersDontMatch = "Bad_OperationParametersDontMatch";

		// Token: 0x040002B5 RID: 693
		internal const string Bad_UnresolvedEntitySet = "Bad_UnresolvedEntitySet";

		// Token: 0x040002B6 RID: 694
		internal const string Bad_UnresolvedEntityContainer = "Bad_UnresolvedEntityContainer";

		// Token: 0x040002B7 RID: 695
		internal const string Bad_UnresolvedEnumType = "Bad_UnresolvedEnumType";

		// Token: 0x040002B8 RID: 696
		internal const string Bad_UnresolvedEnumMember = "Bad_UnresolvedEnumMember";

		// Token: 0x040002B9 RID: 697
		internal const string Bad_UnresolvedProperty = "Bad_UnresolvedProperty";

		// Token: 0x040002BA RID: 698
		internal const string Bad_UnresolvedParameter = "Bad_UnresolvedParameter";

		// Token: 0x040002BB RID: 699
		internal const string Bad_UnresolvedReturn = "Bad_UnresolvedReturn";

		// Token: 0x040002BC RID: 700
		internal const string Bad_UnresolvedLabeledElement = "Bad_UnresolvedLabeledElement";

		// Token: 0x040002BD RID: 701
		internal const string Bad_CyclicEntity = "Bad_CyclicEntity";

		// Token: 0x040002BE RID: 702
		internal const string Bad_CyclicComplex = "Bad_CyclicComplex";

		// Token: 0x040002BF RID: 703
		internal const string Bad_CyclicEntityContainer = "Bad_CyclicEntityContainer";

		// Token: 0x040002C0 RID: 704
		internal const string Bad_UnresolvedNavigationPropertyPath = "Bad_UnresolvedNavigationPropertyPath";

		// Token: 0x040002C1 RID: 705
		internal const string RuleSet_DuplicateRulesExistInRuleSet = "RuleSet_DuplicateRulesExistInRuleSet";

		// Token: 0x040002C2 RID: 706
		internal const string EdmToClr_UnsupportedType = "EdmToClr_UnsupportedType";

		// Token: 0x040002C3 RID: 707
		internal const string EdmToClr_StructuredValueMappedToNonClass = "EdmToClr_StructuredValueMappedToNonClass";

		// Token: 0x040002C4 RID: 708
		internal const string EdmToClr_IEnumerableOfTPropertyAlreadyHasValue = "EdmToClr_IEnumerableOfTPropertyAlreadyHasValue";

		// Token: 0x040002C5 RID: 709
		internal const string EdmToClr_StructuredPropertyDuplicateValue = "EdmToClr_StructuredPropertyDuplicateValue";

		// Token: 0x040002C6 RID: 710
		internal const string EdmToClr_CannotConvertEdmValueToClrType = "EdmToClr_CannotConvertEdmValueToClrType";

		// Token: 0x040002C7 RID: 711
		internal const string EdmToClr_CannotConvertEdmCollectionValueToClrType = "EdmToClr_CannotConvertEdmCollectionValueToClrType";

		// Token: 0x040002C8 RID: 712
		internal const string EdmToClr_TryCreateObjectInstanceReturnedWrongObject = "EdmToClr_TryCreateObjectInstanceReturnedWrongObject";

		// Token: 0x040002C9 RID: 713
		internal const string EdmUtil_NullValueForMimeTypeAnnotation = "EdmUtil_NullValueForMimeTypeAnnotation";

		// Token: 0x040002CA RID: 714
		internal const string EdmUtil_InvalidAnnotationValue = "EdmUtil_InvalidAnnotationValue";

		// Token: 0x040002CB RID: 715
		internal const string PlatformHelper_DateTimeOffsetMustContainTimeZone = "PlatformHelper_DateTimeOffsetMustContainTimeZone";

		// Token: 0x040002CC RID: 716
		internal const string Date_InvalidAddedOrSubtractedResults = "Date_InvalidAddedOrSubtractedResults";

		// Token: 0x040002CD RID: 717
		internal const string Date_InvalidDateParameters = "Date_InvalidDateParameters";

		// Token: 0x040002CE RID: 718
		internal const string Date_InvalidParsingString = "Date_InvalidParsingString";

		// Token: 0x040002CF RID: 719
		internal const string Date_InvalidCompareToTarget = "Date_InvalidCompareToTarget";

		// Token: 0x040002D0 RID: 720
		internal const string TimeOfDay_InvalidTimeOfDayParameters = "TimeOfDay_InvalidTimeOfDayParameters";

		// Token: 0x040002D1 RID: 721
		internal const string TimeOfDay_TicksOutOfRange = "TimeOfDay_TicksOutOfRange";

		// Token: 0x040002D2 RID: 722
		internal const string TimeOfDay_ConvertErrorFromTimeSpan = "TimeOfDay_ConvertErrorFromTimeSpan";

		// Token: 0x040002D3 RID: 723
		internal const string TimeOfDay_InvalidParsingString = "TimeOfDay_InvalidParsingString";

		// Token: 0x040002D4 RID: 724
		internal const string TimeOfDay_InvalidCompareToTarget = "TimeOfDay_InvalidCompareToTarget";

		// Token: 0x040002D5 RID: 725
		private static EntityRes loader;

		// Token: 0x040002D6 RID: 726
		private ResourceManager resources;
	}
}
