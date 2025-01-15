using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000D5 RID: 213
	internal static class Strings
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000DF9C File Offset: 0x0000C19C
		internal static string EdmPrimitive_UnexpectedKind
		{
			get
			{
				return EntityRes.GetString("EdmPrimitive_UnexpectedKind");
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		internal static string EdmPath_UnexpectedKind
		{
			get
			{
				return EntityRes.GetString("EdmPath_UnexpectedKind");
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000DFB4 File Offset: 0x0000C1B4
		internal static string Annotations_TypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("Annotations_TypeMismatch", new object[] { p0, p1 });
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000DFCE File Offset: 0x0000C1CE
		internal static string Constructable_VocabularyAnnotationMustHaveTarget
		{
			get
			{
				return EntityRes.GetString("Constructable_VocabularyAnnotationMustHaveTarget");
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0000DFDA File Offset: 0x0000C1DA
		internal static string Constructable_EntityTypeOrCollectionOfEntityTypeExpected
		{
			get
			{
				return EntityRes.GetString("Constructable_EntityTypeOrCollectionOfEntityTypeExpected");
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000DFE6 File Offset: 0x0000C1E6
		internal static string Constructable_TargetMustBeStock(object p0)
		{
			return EntityRes.GetString("Constructable_TargetMustBeStock", new object[] { p0 });
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000DFFC File Offset: 0x0000C1FC
		internal static string TypeSemantics_CouldNotConvertTypeReference(object p0, object p1)
		{
			return EntityRes.GetString("TypeSemantics_CouldNotConvertTypeReference", new object[] { p0, p1 });
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000E016 File Offset: 0x0000C216
		internal static string EdmModel_CannotUseElementWithTypeNone
		{
			get
			{
				return EntityRes.GetString("EdmModel_CannotUseElementWithTypeNone");
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000E022 File Offset: 0x0000C222
		internal static string EdmModel_CannotAddMoreThanOneEntityContainerToOneEdmModel
		{
			get
			{
				return EntityRes.GetString("EdmModel_CannotAddMoreThanOneEntityContainerToOneEdmModel");
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0000E02E File Offset: 0x0000C22E
		internal static string EdmEntityContainer_CannotUseElementWithTypeNone
		{
			get
			{
				return EntityRes.GetString("EdmEntityContainer_CannotUseElementWithTypeNone");
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000E03A File Offset: 0x0000C23A
		internal static string ValueWriter_NonSerializableValue(object p0)
		{
			return EntityRes.GetString("ValueWriter_NonSerializableValue", new object[] { p0 });
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0000E050 File Offset: 0x0000C250
		internal static string ValueHasAlreadyBeenSet
		{
			get
			{
				return EntityRes.GetString("ValueHasAlreadyBeenSet");
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000E05C File Offset: 0x0000C25C
		internal static string PathSegmentMustNotContainSlash
		{
			get
			{
				return EntityRes.GetString("PathSegmentMustNotContainSlash");
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000E068 File Offset: 0x0000C268
		internal static string Constructable_DependentPropertyCountMustMatchNumberOfPropertiesOnPrincipalType(object p0, object p1)
		{
			return EntityRes.GetString("Constructable_DependentPropertyCountMustMatchNumberOfPropertiesOnPrincipalType", new object[] { p0, p1 });
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000E082 File Offset: 0x0000C282
		internal static string EdmType_UnexpectedEdmType
		{
			get
			{
				return EntityRes.GetString("EdmType_UnexpectedEdmType");
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0000E08E File Offset: 0x0000C28E
		internal static string NavigationPropertyBinding_PathIsNotValid
		{
			get
			{
				return EntityRes.GetString("NavigationPropertyBinding_PathIsNotValid");
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000E09A File Offset: 0x0000C29A
		internal static string Edm_Evaluator_NoTermTypeAnnotationOnType(object p0, object p1)
		{
			return EntityRes.GetString("Edm_Evaluator_NoTermTypeAnnotationOnType", new object[] { p0, p1 });
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		internal static string Edm_Evaluator_NoValueAnnotationOnType(object p0, object p1)
		{
			return EntityRes.GetString("Edm_Evaluator_NoValueAnnotationOnType", new object[] { p0, p1 });
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000E0CE File Offset: 0x0000C2CE
		internal static string Edm_Evaluator_NoValueAnnotationOnElement(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_NoValueAnnotationOnElement", new object[] { p0 });
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		internal static string Edm_Evaluator_UnrecognizedExpressionKind(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_UnrecognizedExpressionKind", new object[] { p0 });
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000E0FA File Offset: 0x0000C2FA
		internal static string Edm_Evaluator_UnboundFunction(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_UnboundFunction", new object[] { p0 });
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000E110 File Offset: 0x0000C310
		internal static string Edm_Evaluator_UnboundPath(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_UnboundPath", new object[] { p0 });
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000E126 File Offset: 0x0000C326
		internal static string Edm_Evaluator_NoContextPath
		{
			get
			{
				return EntityRes.GetString("Edm_Evaluator_NoContextPath");
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000E132 File Offset: 0x0000C332
		internal static string Edm_Evaluator_FailedTypeAssertion(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_FailedTypeAssertion", new object[] { p0 });
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000E148 File Offset: 0x0000C348
		internal static string Edm_Evaluator_TypeCastNeedsEdmModel
		{
			get
			{
				return EntityRes.GetString("Edm_Evaluator_TypeCastNeedsEdmModel");
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000E154 File Offset: 0x0000C354
		internal static string EdmModel_Validator_Semantic_SystemNamespaceEncountered(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SystemNamespaceEncountered", new object[] { p0 });
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000E16A File Offset: 0x0000C36A
		internal static string EdmModel_Validator_Semantic_NavigationSourceTypeHasNoKeys(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationSourceTypeHasNoKeys", new object[] { p0, p1 });
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000E184 File Offset: 0x0000C384
		internal static string EdmModel_Validator_Semantic_DuplicateEndName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEndName", new object[] { p0 });
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000E19A File Offset: 0x0000C39A
		internal static string EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey", new object[] { p0, p1 });
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000E1B4 File Offset: 0x0000C3B4
		internal static string EdmModel_Validator_Semantic_InvalidComplexTypeAbstract(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidComplexTypeAbstract", new object[] { p0 });
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000E1CA File Offset: 0x0000C3CA
		internal static string EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic", new object[] { p0 });
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000E1E0 File Offset: 0x0000C3E0
		internal static string EdmModel_Validator_Semantic_InvalidKeyNullablePart(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidKeyNullablePart", new object[] { p0, p1 });
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000E1FA File Offset: 0x0000C3FA
		internal static string EdmModel_Validator_Semantic_EntityKeyMustBeScalar(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntityKeyMustBeScalar", new object[] { p0, p1 });
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000E214 File Offset: 0x0000C414
		internal static string EdmModel_Validator_Semantic_EntityComposableBoundEscapeFunctionMustBeLessOne(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntityComposableBoundEscapeFunctionMustBeLessOne", new object[] { p0, p1 });
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000E22E File Offset: 0x0000C42E
		internal static string EdmModel_Validator_Semantic_EntityNoncomposableBoundEscapeFunctionMustBeLessOne(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntityNoncomposableBoundEscapeFunctionMustBeLessOne", new object[] { p0, p1 });
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000E248 File Offset: 0x0000C448
		internal static string EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass", new object[] { p0, p1 });
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000E262 File Offset: 0x0000C462
		internal static string EdmModel_Validator_Semantic_KeyMissingOnEntityType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_KeyMissingOnEntityType", new object[] { p0 });
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0000E278 File Offset: 0x0000C478
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000E296 File Offset: 0x0000C496
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame", new object[] { p0 });
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType", new object[] { p0 });
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000E2C2 File Offset: 0x0000C4C2
		internal static string EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation");
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0000E2CE File Offset: 0x0000C4CE
		internal static string EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified", new object[] { p0 });
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		internal static string EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000E2FA File Offset: 0x0000C4FA
		internal static string EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint", new object[] { p0 });
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000E310 File Offset: 0x0000C510
		internal static string EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne", new object[] { p0 });
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000E326 File Offset: 0x0000C526
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable", new object[] { p0 });
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000E33C File Offset: 0x0000C53C
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable", new object[] { p0 });
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000E352 File Offset: 0x0000C552
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne", new object[] { p0 });
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000E368 File Offset: 0x0000C568
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany", new object[] { p0 });
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000E37E File Offset: 0x0000C57E
		internal static string EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint");
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000E38A File Offset: 0x0000C58A
		internal static string EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000E3B1 File Offset: 0x0000C5B1
		internal static string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintDependentEnd(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintDependentEnd", new object[] { p0, p1 });
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000E3CB File Offset: 0x0000C5CB
		internal static string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintPrimaryEnd(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintPrimaryEnd", new object[] { p0, p1 });
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000E3E5 File Offset: 0x0000C5E5
		internal static string EdmModel_Validator_Semantic_InvalidPropertyType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyType", new object[] { p0 });
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000E3FB File Offset: 0x0000C5FB
		internal static string EdmModel_Validator_Semantic_BoundOperationMustHaveParameters(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BoundOperationMustHaveParameters", new object[] { p0 });
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000E411 File Offset: 0x0000C611
		internal static string EdmModel_Validator_Semantic_RequiredParametersMustPrecedeOptional(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_RequiredParametersMustPrecedeOptional", new object[] { p0 });
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0000E427 File Offset: 0x0000C627
		internal static string EdmModel_Validator_Semantic_OperationWithUnsupportedReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithUnsupportedReturnType", new object[] { p0 });
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000E43D File Offset: 0x0000C63D
		internal static string EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000E45B File Offset: 0x0000C65B
		internal static string EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet2(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet2", new object[] { p0, p1 });
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0000E475 File Offset: 0x0000C675
		internal static string EdmModel_Validator_Semantic_OperationImportEntitySetExpressionKindIsInvalid(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportEntitySetExpressionKindIsInvalid", new object[] { p0, p1 });
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0000E48F File Offset: 0x0000C68F
		internal static string EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid", new object[] { p0 });
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0000E4A5 File Offset: 0x0000C6A5
		internal static string EdmModel_Validator_Semantic_OperationImportSpecifiesEntitySetButNotEntityType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportSpecifiesEntitySetButNotEntityType", new object[] { p0 });
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0000E4BB File Offset: 0x0000C6BB
		internal static string EdmModel_Validator_Semantic_OperationImportCannotImportBoundOperation(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportCannotImportBoundOperation", new object[] { p0, p1 });
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0000E4D5 File Offset: 0x0000C6D5
		internal static string EdmModel_Validator_Semantic_FunctionImportWithParameterShouldNotBeIncludedInServiceDocument(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportWithParameterShouldNotBeIncludedInServiceDocument", new object[] { p0 });
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0000E4EB File Offset: 0x0000C6EB
		internal static string EdmModel_Validator_Semantic_FunctionMustHaveReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionMustHaveReturnType", new object[] { p0 });
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000E501 File Offset: 0x0000C701
		internal static string EdmModel_Validator_Semantic_UrlEscapeFunctionMustBoundFunction(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_UrlEscapeFunctionMustBoundFunction", new object[] { p0 });
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000E517 File Offset: 0x0000C717
		internal static string EdmModel_Validator_Semantic_UrlEscapeFunctionMustHaveOneStringParameter(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_UrlEscapeFunctionMustHaveOneStringParameter", new object[] { p0 });
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0000E52D File Offset: 0x0000C72D
		internal static string EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000E543 File Offset: 0x0000C743
		internal static string EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName", new object[] { p0 });
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000E559 File Offset: 0x0000C759
		internal static string EdmModel_Validator_Semantic_UnboundFunctionOverloadHasIncorrectReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_UnboundFunctionOverloadHasIncorrectReturnType", new object[] { p0 });
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000E56F File Offset: 0x0000C76F
		internal static string EdmModel_Validator_Semantic_OperationCannotHaveEntitySetPathWithUnBoundOperation(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationCannotHaveEntitySetPathWithUnBoundOperation", new object[] { p0 });
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0000E585 File Offset: 0x0000C785
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathMissingBindingParameterName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathMissingBindingParameterName", new object[] { p0 });
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000E59B File Offset: 0x0000C79B
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathWithFirstPathParameterNotMatchingFirstParameterName(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathWithFirstPathParameterNotMatchingFirstParameterName", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0000E5BD File Offset: 0x0000C7BD
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathTypeCastSegmentMustBeEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathTypeCastSegmentMustBeEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0000E5DB File Offset: 0x0000C7DB
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownNavigationProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownNavigationProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000E5F9 File Offset: 0x0000C7F9
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathInvalidTypeCastSegment(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathInvalidTypeCastSegment", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000E61B File Offset: 0x0000C81B
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathWithNonEntityBindingParameter(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathWithNonEntityBindingParameter", new object[] { p0, p1, p2 });
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000E639 File Offset: 0x0000C839
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownTypeCastSegment(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownTypeCastSegment", new object[] { p0, p1, p2 });
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000E657 File Offset: 0x0000C857
		internal static string EdmModel_Validator_Semantic_OperationWithEntitySetPathReturnTypeInvalid(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithEntitySetPathReturnTypeInvalid", new object[] { p0 });
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0000E66D File Offset: 0x0000C86D
		internal static string EdmModel_Validator_Semantic_OperationWithEntitySetPathAndReturnTypeTypeNotAssignable(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithEntitySetPathAndReturnTypeTypeNotAssignable", new object[] { p0, p1, p2 });
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0000E68B File Offset: 0x0000C88B
		internal static string EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType", new object[] { p0 });
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000E6A1 File Offset: 0x0000C8A1
		internal static string EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000E6B7 File Offset: 0x0000C8B7
		internal static string EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName", new object[] { p0 });
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000E6CD File Offset: 0x0000C8CD
		internal static string EdmModel_Validator_Semantic_PropertyNameAlreadyDefined(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PropertyNameAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000E6E3 File Offset: 0x0000C8E3
		internal static string EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind");
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000E6EF File Offset: 0x0000C8EF
		internal static string EdmModel_Validator_Semantic_BaseTypeOfOpenTypeMustBeOpen(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BaseTypeOfOpenTypeMustBeOpen", new object[] { p0 });
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000E705 File Offset: 0x0000C905
		internal static string EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity", new object[] { p0, p1 });
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000E71F File Offset: 0x0000C91F
		internal static string EdmModel_Validator_Semantic_EdmPrimitiveTypeCannotBeUsedAsTypeOfKey(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EdmPrimitiveTypeCannotBeUsedAsTypeOfKey", new object[] { p0, p1 });
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000E739 File Offset: 0x0000C939
		internal static string EdmModel_Validator_Semantic_EdmPrimitiveTypeCannotBeUsedAsUnderlyingType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EdmPrimitiveTypeCannotBeUsedAsUnderlyingType", new object[] { p0, p1 });
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000E753 File Offset: 0x0000C953
		internal static string EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity", new object[] { p0, p1 });
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000E76D File Offset: 0x0000C96D
		internal static string EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect", new object[] { p0 });
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000E783 File Offset: 0x0000C983
		internal static string EdmModel_Validator_Semantic_InaccessibleType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InaccessibleType", new object[] { p0 });
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000E799 File Offset: 0x0000C999
		internal static string EdmModel_Validator_Semantic_AmbiguousType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_AmbiguousType", new object[] { p0 });
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000E7AF File Offset: 0x0000C9AF
		internal static string EdmModel_Validator_Semantic_InvalidNavigationPropertyType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidNavigationPropertyType", new object[] { p0 });
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000E7C5 File Offset: 0x0000C9C5
		internal static string EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional", new object[] { p0 });
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000E7DB File Offset: 0x0000C9DB
		internal static string EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne", new object[] { p0 });
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000E7F1 File Offset: 0x0000C9F1
		internal static string EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne", new object[] { p0 });
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000E807 File Offset: 0x0000CA07
		internal static string EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties", new object[] { p0 });
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000E81D File Offset: 0x0000CA1D
		internal static string EdmModel_Validator_Semantic_DuplicateDependentProperty(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateDependentProperty", new object[] { p0, p1 });
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0000E837 File Offset: 0x0000CA37
		internal static string EdmModel_Validator_Semantic_ScaleOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ScaleOutOfRange");
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0000E843 File Offset: 0x0000CA43
		internal static string EdmModel_Validator_Semantic_PrecisionOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_PrecisionOutOfRange");
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000E84F File Offset: 0x0000CA4F
		internal static string EdmModel_Validator_Semantic_StringMaxLengthOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_StringMaxLengthOutOfRange");
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000E85B File Offset: 0x0000CA5B
		internal static string EdmModel_Validator_Semantic_MaxLengthOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_MaxLengthOutOfRange");
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000E867 File Offset: 0x0000CA67
		internal static string EdmModel_Validator_Semantic_EnumMemberValueOutOfRange(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EnumMemberValueOutOfRange", new object[] { p0 });
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000E87D File Offset: 0x0000CA7D
		internal static string EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000E893 File Offset: 0x0000CA93
		internal static string EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly");
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000E89F File Offset: 0x0000CA9F
		internal static string EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull");
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000E8AB File Offset: 0x0000CAAB
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm");
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x0000E8B7 File Offset: 0x0000CAB7
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml");
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000E8C3 File Offset: 0x0000CAC3
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationNotIEdmStringValue
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationNotIEdmStringValue");
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x0000E8CF File Offset: 0x0000CACF
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationNullNamespaceOrName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationNullNamespaceOrName");
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000E8DB File Offset: 0x0000CADB
		internal static string EdmModel_Validator_Semantic_CannotAssertNullableTypeAsNonNullableType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_CannotAssertNullableTypeAsNonNullableType", new object[] { p0 });
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000E8F1 File Offset: 0x0000CAF1
		internal static string EdmModel_Validator_Semantic_ExpressionPrimitiveKindCannotPromoteToAssertedType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ExpressionPrimitiveKindCannotPromoteToAssertedType", new object[] { p0, p1 });
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000E90B File Offset: 0x0000CB0B
		internal static string EdmModel_Validator_Semantic_NullCannotBeAssertedToBeANonNullableType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_NullCannotBeAssertedToBeANonNullableType");
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0000E917 File Offset: 0x0000CB17
		internal static string EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType");
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000E923 File Offset: 0x0000CB23
		internal static string EdmModel_Validator_Semantic_CollectionExpressionNotValidForNonCollectionType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_CollectionExpressionNotValidForNonCollectionType");
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0000E92F File Offset: 0x0000CB2F
		internal static string EdmModel_Validator_Semantic_PrimitiveConstantExpressionNotValidForNonPrimitiveType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_PrimitiveConstantExpressionNotValidForNonPrimitiveType");
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000E93B File Offset: 0x0000CB3B
		internal static string EdmModel_Validator_Semantic_RecordExpressionNotValidForNonStructuredType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_RecordExpressionNotValidForNonStructuredType");
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000E947 File Offset: 0x0000CB47
		internal static string EdmModel_Validator_Semantic_RecordExpressionMissingProperty(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_RecordExpressionMissingProperty", new object[] { p0 });
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000E95D File Offset: 0x0000CB5D
		internal static string EdmModel_Validator_Semantic_RecordExpressionHasExtraProperties(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_RecordExpressionHasExtraProperties", new object[] { p0 });
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000E973 File Offset: 0x0000CB73
		internal static string EdmModel_Validator_Semantic_DuplicateAnnotation(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateAnnotation", new object[] { p0, p1, p2 });
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000E991 File Offset: 0x0000CB91
		internal static string EdmModel_Validator_Semantic_IncorrectNumberOfArguments(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_IncorrectNumberOfArguments", new object[] { p0, p1, p2 });
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000E9AF File Offset: 0x0000CBAF
		internal static string EdmModel_Validator_Semantic_DuplicateEntityContainerName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEntityContainerName", new object[] { p0 });
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000E9C5 File Offset: 0x0000CBC5
		internal static string EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType");
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000E9D1 File Offset: 0x0000CBD1
		internal static string EdmModel_Validator_Semantic_ExpressionEnumKindNotValidForAssertedType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ExpressionEnumKindNotValidForAssertedType");
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000E9DD File Offset: 0x0000CBDD
		internal static string EdmModel_Validator_Semantic_IntegerConstantValueOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_IntegerConstantValueOutOfRange");
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0000E9E9 File Offset: 0x0000CBE9
		internal static string EdmModel_Validator_Semantic_StringConstantLengthOutOfRange(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_StringConstantLengthOutOfRange", new object[] { p0, p1 });
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000EA03 File Offset: 0x0000CC03
		internal static string EdmModel_Validator_Semantic_BinaryConstantLengthOutOfRange(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BinaryConstantLengthOutOfRange", new object[] { p0, p1 });
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0000EA1D File Offset: 0x0000CC1D
		internal static string EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone");
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000EA29 File Offset: 0x0000CC29
		internal static string EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000EA3F File Offset: 0x0000CC3F
		internal static string EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000EA55 File Offset: 0x0000CC55
		internal static string EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000EA6B File Offset: 0x0000CC6B
		internal static string EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000EA81 File Offset: 0x0000CC81
		internal static string EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping", new object[] { p0, p1 });
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000EA9B File Offset: 0x0000CC9B
		internal static string EdmModel_Validator_Semantic_NavigationMappingMustBeBidirectional(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationMappingMustBeBidirectional", new object[] { p0, p1 });
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000EAB5 File Offset: 0x0000CCB5
		internal static string EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty", new object[] { p0 });
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000EACB File Offset: 0x0000CCCB
		internal static string EdmModel_Validator_Semantic_TypeAnnotationMissingRequiredProperty(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeAnnotationMissingRequiredProperty", new object[] { p0 });
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000EAE1 File Offset: 0x0000CCE1
		internal static string EdmModel_Validator_Semantic_TypeAnnotationHasExtraProperties(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeAnnotationHasExtraProperties", new object[] { p0 });
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000EAF7 File Offset: 0x0000CCF7
		internal static string EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType", new object[] { p0 });
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000EB0D File Offset: 0x0000CD0D
		internal static string EdmModel_Validator_Semantic_InaccessibleTerm(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InaccessibleTerm", new object[] { p0 });
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000EB23 File Offset: 0x0000CD23
		internal static string EdmModel_Validator_Semantic_InaccessibleTarget(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InaccessibleTarget", new object[] { p0 });
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000EB39 File Offset: 0x0000CD39
		internal static string EdmModel_Validator_Semantic_VocabularyAnnotationApplyToNotAllowedAnnotatable(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_VocabularyAnnotationApplyToNotAllowedAnnotatable", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000EB57 File Offset: 0x0000CD57
		internal static string EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique", new object[] { p0, p1 });
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000EB71 File Offset: 0x0000CD71
		internal static string EdmModel_Validator_Semantic_NoEntitySetsFoundForType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NoEntitySetsFoundForType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0000EB8F File Offset: 0x0000CD8F
		internal static string EdmModel_Validator_Semantic_CannotInferEntitySetWithMultipleSetsPerType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_CannotInferEntitySetWithMultipleSetsPerType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		internal static string EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet", new object[] { p0, p1 });
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000EBC7 File Offset: 0x0000CDC7
		internal static string EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself", new object[] { p0 });
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0000EBDD File Offset: 0x0000CDDD
		internal static string EdmModel_Validator_Semantic_PathIsNotValidForTheGivenContext(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PathIsNotValidForTheGivenContext", new object[] { p0 });
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0000EBF3 File Offset: 0x0000CDF3
		internal static string EdmModel_Validator_Semantic_NavigationPropertyMappingMustPointToValidTargetForProperty(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyMappingMustPointToValidTargetForProperty", new object[] { p0, p1 });
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0000EC0D File Offset: 0x0000CE0D
		internal static string EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterNames(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterNames", new object[] { p0 });
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0000EC23 File Offset: 0x0000CE23
		internal static string EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterTypes(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterTypes", new object[] { p0 });
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000EC39 File Offset: 0x0000CE39
		internal static string EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterNames(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterNames", new object[] { p0 });
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000EC4F File Offset: 0x0000CE4F
		internal static string EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterTypes(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterTypes", new object[] { p0 });
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0000EC65 File Offset: 0x0000CE65
		internal static string EdmModel_Validator_Semantic_ModelDuplicateBoundActions(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateBoundActions", new object[] { p0 });
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0000EC7B File Offset: 0x0000CE7B
		internal static string EdmModel_Validator_Semantic_ModelDuplicateUnBoundActions(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateUnBoundActions", new object[] { p0 });
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0000EC91 File Offset: 0x0000CE91
		internal static string EdmModel_Validator_Semantic_BoundFunctionOverloadsMustHaveSameReturnType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BoundFunctionOverloadsMustHaveSameReturnType", new object[] { p0, p1 });
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0000ECAB File Offset: 0x0000CEAB
		internal static string EdmModel_Validator_Semantic_EntitySetTypeMustBeCollectionOfEntityType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetTypeMustBeCollectionOfEntityType", new object[] { p0, p1 });
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0000ECC5 File Offset: 0x0000CEC5
		internal static string EdmModel_Validator_Semantic_SingletonTypeMustBeEntityType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SingletonTypeMustBeEntityType", new object[] { p0, p1 });
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0000ECDF File Offset: 0x0000CEDF
		internal static string EdmModel_Validator_Semantic_NavigationPropertyOfCollectionTypeMustNotTargetToSingleton(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyOfCollectionTypeMustNotTargetToSingleton", new object[] { p0, p1 });
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0000ECF9 File Offset: 0x0000CEF9
		internal static string EdmModel_Validator_Semantic_StructuredTypeBaseTypeCannotBeAbstractType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_StructuredTypeBaseTypeCannotBeAbstractType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0000ED17 File Offset: 0x0000CF17
		internal static string EdmModel_Validator_Semantic_PropertyTypeCannotBeCollectionOfAbstractType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PropertyTypeCannotBeCollectionOfAbstractType", new object[] { p0, p1 });
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0000ED31 File Offset: 0x0000CF31
		internal static string EdmModel_Validator_Semantic_OperationReturnTypeCannotBeCollectionOfAbstractType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationReturnTypeCannotBeCollectionOfAbstractType", new object[] { p0, p1 });
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0000ED4B File Offset: 0x0000CF4B
		internal static string EdmModel_Validator_Semantic_EdmEntityTypeCannotBeTypeOfSingleton(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EdmEntityTypeCannotBeTypeOfSingleton", new object[] { p0 });
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0000ED61 File Offset: 0x0000CF61
		internal static string EdmModel_Validator_Semantic_EdmEntityTypeCannotBeTypeOfEntitySet(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EdmEntityTypeCannotBeTypeOfEntitySet", new object[] { p0 });
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0000ED77 File Offset: 0x0000CF77
		internal static string EdmModel_Validator_Semantic_DeclaringTypeOfNavigationSourceCannotHavePathProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DeclaringTypeOfNavigationSourceCannotHavePathProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0000ED95 File Offset: 0x0000CF95
		internal static string EdmModel_Validator_Semantic_TypeOfNavigationPropertyCannotHavePathProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeOfNavigationPropertyCannotHavePathProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000EDB3 File Offset: 0x0000CFB3
		internal static string EdmModel_Validator_Syntactic_MissingName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_MissingName");
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000EDBF File Offset: 0x0000CFBF
		internal static string EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong", new object[] { p0 });
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000EDD5 File Offset: 0x0000CFD5
		internal static string EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed", new object[] { p0 });
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000EDEB File Offset: 0x0000CFEB
		internal static string EdmModel_Validator_Syntactic_MissingNamespaceName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_MissingNamespaceName");
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0000EDF7 File Offset: 0x0000CFF7
		internal static string EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong", new object[] { p0 });
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0000EE0D File Offset: 0x0000D00D
		internal static string EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0000EE23 File Offset: 0x0000D023
		internal static string EdmModel_Validator_Syntactic_PropertyMustNotBeNull(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_PropertyMustNotBeNull", new object[] { p0, p1 });
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0000EE3D File Offset: 0x0000D03D
		internal static string EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0000EE5F File Offset: 0x0000D05F
		internal static string EdmModel_Validator_Syntactic_InterfaceKindValueMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_InterfaceKindValueMismatch", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0000EE81 File Offset: 0x0000D081
		internal static string EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch", new object[] { p0, p1 });
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0000EE9B File Offset: 0x0000D09B
		internal static string EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000EEB9 File Offset: 0x0000D0B9
		internal static string EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements", new object[] { p0, p1 });
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000EED3 File Offset: 0x0000D0D3
		internal static string EdmModel_Validator_Syntactic_NavigationPartnerInvalid(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_NavigationPartnerInvalid", new object[] { p0 });
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0000EEE9 File Offset: 0x0000D0E9
		internal static string EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy", new object[] { p0 });
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0000EEFF File Offset: 0x0000D0FF
		internal static string Serializer_SingleFileExpected
		{
			get
			{
				return EntityRes.GetString("Serializer_SingleFileExpected");
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000EF0B File Offset: 0x0000D10B
		internal static string Serializer_UnknownEdmVersion
		{
			get
			{
				return EntityRes.GetString("Serializer_UnknownEdmVersion");
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000EF17 File Offset: 0x0000D117
		internal static string Serializer_UnknownEdmxVersion
		{
			get
			{
				return EntityRes.GetString("Serializer_UnknownEdmxVersion");
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000EF23 File Offset: 0x0000D123
		internal static string Serializer_NonInlineOperationImportReturnType(object p0)
		{
			return EntityRes.GetString("Serializer_NonInlineOperationImportReturnType", new object[] { p0 });
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000EF39 File Offset: 0x0000D139
		internal static string Serializer_ReferencedTypeMustHaveValidName(object p0)
		{
			return EntityRes.GetString("Serializer_ReferencedTypeMustHaveValidName", new object[] { p0 });
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0000EF4F File Offset: 0x0000D14F
		internal static string Serializer_OutOfLineAnnotationTargetMustHaveValidName(object p0)
		{
			return EntityRes.GetString("Serializer_OutOfLineAnnotationTargetMustHaveValidName", new object[] { p0 });
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000EF65 File Offset: 0x0000D165
		internal static string Serializer_NoSchemasProduced
		{
			get
			{
				return EntityRes.GetString("Serializer_NoSchemasProduced");
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0000EF71 File Offset: 0x0000D171
		internal static string XmlParser_EmptyFile(object p0)
		{
			return EntityRes.GetString("XmlParser_EmptyFile", new object[] { p0 });
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0000EF87 File Offset: 0x0000D187
		internal static string XmlParser_EmptySchemaTextReader
		{
			get
			{
				return EntityRes.GetString("XmlParser_EmptySchemaTextReader");
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0000EF93 File Offset: 0x0000D193
		internal static string XmlParser_MissingAttribute(object p0, object p1)
		{
			return EntityRes.GetString("XmlParser_MissingAttribute", new object[] { p0, p1 });
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0000EFAD File Offset: 0x0000D1AD
		internal static string XmlParser_TextNotAllowed(object p0)
		{
			return EntityRes.GetString("XmlParser_TextNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000EFC3 File Offset: 0x0000D1C3
		internal static string XmlParser_UnexpectedAttribute(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedAttribute", new object[] { p0 });
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000EFD9 File Offset: 0x0000D1D9
		internal static string XmlParser_UnexpectedElement(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedElement", new object[] { p0 });
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000EFEF File Offset: 0x0000D1EF
		internal static string XmlParser_UnusedElement(object p0)
		{
			return EntityRes.GetString("XmlParser_UnusedElement", new object[] { p0 });
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000F005 File Offset: 0x0000D205
		internal static string XmlParser_UnexpectedNodeType(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedNodeType", new object[] { p0 });
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000F01B File Offset: 0x0000D21B
		internal static string XmlParser_UnexpectedRootElement(object p0, object p1)
		{
			return EntityRes.GetString("XmlParser_UnexpectedRootElement", new object[] { p0, p1 });
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0000F035 File Offset: 0x0000D235
		internal static string XmlParser_UnexpectedRootElementWrongNamespace(object p0, object p1)
		{
			return EntityRes.GetString("XmlParser_UnexpectedRootElementWrongNamespace", new object[] { p0, p1 });
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0000F04F File Offset: 0x0000D24F
		internal static string XmlParser_UnexpectedRootElementNoNamespace(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedRootElementNoNamespace", new object[] { p0 });
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000F065 File Offset: 0x0000D265
		internal static string CsdlParser_InvalidEntitySetPathWithUnboundAction(object p0, object p1)
		{
			return EntityRes.GetString("CsdlParser_InvalidEntitySetPathWithUnboundAction", new object[] { p0, p1 });
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0000F07F File Offset: 0x0000D27F
		internal static string CsdlParser_InvalidAlias(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidAlias", new object[] { p0 });
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0000F095 File Offset: 0x0000D295
		internal static string CsdlParser_InvalidDeleteAction(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidDeleteAction", new object[] { p0 });
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0000F0AB File Offset: 0x0000D2AB
		internal static string CsdlParser_MissingTypeAttributeOrElement
		{
			get
			{
				return EntityRes.GetString("CsdlParser_MissingTypeAttributeOrElement");
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000F0B7 File Offset: 0x0000D2B7
		internal static string CsdlParser_InvalidEndRoleInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("CsdlParser_InvalidEndRoleInRelationshipConstraint", new object[] { p0, p1 });
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000F0D1 File Offset: 0x0000D2D1
		internal static string CsdlParser_InvalidMultiplicity(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidMultiplicity", new object[] { p0 });
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0000F0E7 File Offset: 0x0000D2E7
		internal static string CsdlParser_ReferentialConstraintRequiresOneDependent
		{
			get
			{
				return EntityRes.GetString("CsdlParser_ReferentialConstraintRequiresOneDependent");
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0000F0F3 File Offset: 0x0000D2F3
		internal static string CsdlParser_ReferentialConstraintRequiresOnePrincipal
		{
			get
			{
				return EntityRes.GetString("CsdlParser_ReferentialConstraintRequiresOnePrincipal");
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0000F0FF File Offset: 0x0000D2FF
		internal static string CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0000F10B File Offset: 0x0000D30B
		internal static string CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0000F117 File Offset: 0x0000D317
		internal static string CsdlParser_InvalidCastExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidCastExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0000F123 File Offset: 0x0000D323
		internal static string CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000F12F File Offset: 0x0000D32F
		internal static string CsdlParser_InvalidTypeName(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidTypeName", new object[] { p0 });
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0000F145 File Offset: 0x0000D345
		internal static string CsdlParser_InvalidQualifiedName(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidQualifiedName", new object[] { p0 });
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0000F15B File Offset: 0x0000D35B
		internal static string CsdlParser_NoReadersProvided
		{
			get
			{
				return EntityRes.GetString("CsdlParser_NoReadersProvided");
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0000F167 File Offset: 0x0000D367
		internal static string CsdlParser_NullXmlReader
		{
			get
			{
				return EntityRes.GetString("CsdlParser_NullXmlReader");
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0000F173 File Offset: 0x0000D373
		internal static string CsdlParser_InvalidEntitySetPath(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidEntitySetPath", new object[] { p0 });
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0000F189 File Offset: 0x0000D389
		internal static string CsdlParser_InvalidEnumMemberPath(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidEnumMemberPath", new object[] { p0 });
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0000F19F File Offset: 0x0000D39F
		internal static string CsdlParser_CannotSpecifyNullableAttributeForNavigationPropertyWithCollectionType
		{
			get
			{
				return EntityRes.GetString("CsdlParser_CannotSpecifyNullableAttributeForNavigationPropertyWithCollectionType");
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0000F1AB File Offset: 0x0000D3AB
		internal static string CsdlParser_MetadataDocumentCannotHaveMoreThanOneEntityContainer
		{
			get
			{
				return EntityRes.GetString("CsdlParser_MetadataDocumentCannotHaveMoreThanOneEntityContainer");
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x0000F1B7 File Offset: 0x0000D3B7
		internal static string CsdlSemantics_ReferentialConstraintMismatch
		{
			get
			{
				return EntityRes.GetString("CsdlSemantics_ReferentialConstraintMismatch");
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0000F1C3 File Offset: 0x0000D3C3
		internal static string CsdlSemantics_EnumMemberMustHaveValue
		{
			get
			{
				return EntityRes.GetString("CsdlSemantics_EnumMemberMustHaveValue");
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0000F1CF File Offset: 0x0000D3CF
		internal static string CsdlSemantics_ImpossibleAnnotationsTarget(object p0)
		{
			return EntityRes.GetString("CsdlSemantics_ImpossibleAnnotationsTarget", new object[] { p0 });
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0000F1E5 File Offset: 0x0000D3E5
		internal static string CsdlSemantics_DuplicateAlias(object p0, object p1)
		{
			return EntityRes.GetString("CsdlSemantics_DuplicateAlias", new object[] { p0, p1 });
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0000F1FF File Offset: 0x0000D3FF
		internal static string EdmxParser_EdmxVersionMismatch
		{
			get
			{
				return EntityRes.GetString("EdmxParser_EdmxVersionMismatch");
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0000F20B File Offset: 0x0000D40B
		internal static string EdmxParser_BodyElement(object p0)
		{
			return EntityRes.GetString("EdmxParser_BodyElement", new object[] { p0 });
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0000F221 File Offset: 0x0000D421
		internal static string EdmxParser_InvalidReferenceIncorrectNumberOfIncludes
		{
			get
			{
				return EntityRes.GetString("EdmxParser_InvalidReferenceIncorrectNumberOfIncludes");
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0000F22D File Offset: 0x0000D42D
		internal static string EdmxParser_UnresolvedReferenceUriInEdmxReference
		{
			get
			{
				return EntityRes.GetString("EdmxParser_UnresolvedReferenceUriInEdmxReference");
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0000F239 File Offset: 0x0000D439
		internal static string EdmParseException_ErrorsEncounteredInEdmx(object p0)
		{
			return EntityRes.GetString("EdmParseException_ErrorsEncounteredInEdmx", new object[] { p0 });
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0000F24F File Offset: 0x0000D44F
		internal static string ValueParser_InvalidBoolean(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidBoolean", new object[] { p0 });
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0000F265 File Offset: 0x0000D465
		internal static string ValueParser_InvalidInteger(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidInteger", new object[] { p0 });
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0000F27B File Offset: 0x0000D47B
		internal static string ValueParser_InvalidLong(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidLong", new object[] { p0 });
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0000F291 File Offset: 0x0000D491
		internal static string ValueParser_InvalidFloatingPoint(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidFloatingPoint", new object[] { p0 });
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0000F2A7 File Offset: 0x0000D4A7
		internal static string ValueParser_InvalidMaxLength(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidMaxLength", new object[] { p0 });
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0000F2BD File Offset: 0x0000D4BD
		internal static string ValueParser_InvalidSrid(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidSrid", new object[] { p0 });
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0000F2D3 File Offset: 0x0000D4D3
		internal static string ValueParser_InvalidScale(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidScale", new object[] { p0 });
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0000F2E9 File Offset: 0x0000D4E9
		internal static string ValueParser_InvalidGuid(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidGuid", new object[] { p0 });
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0000F2FF File Offset: 0x0000D4FF
		internal static string ValueParser_InvalidDecimal(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDecimal", new object[] { p0 });
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0000F315 File Offset: 0x0000D515
		internal static string ValueParser_InvalidDateTimeOffset(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDateTimeOffset", new object[] { p0 });
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0000F32B File Offset: 0x0000D52B
		internal static string ValueParser_InvalidDateTime(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDateTime", new object[] { p0 });
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0000F341 File Offset: 0x0000D541
		internal static string ValueParser_InvalidDate(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDate", new object[] { p0 });
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0000F357 File Offset: 0x0000D557
		internal static string ValueParser_InvalidDuration(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDuration", new object[] { p0 });
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0000F36D File Offset: 0x0000D56D
		internal static string ValueParser_InvalidBinary(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidBinary", new object[] { p0 });
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000F383 File Offset: 0x0000D583
		internal static string ValueParser_InvalidTimeOfDay(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidTimeOfDay", new object[] { p0 });
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0000F399 File Offset: 0x0000D599
		internal static string UnknownEnumVal_Multiplicity(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_Multiplicity", new object[] { p0 });
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000F3AF File Offset: 0x0000D5AF
		internal static string UnknownEnumVal_SchemaElementKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_SchemaElementKind", new object[] { p0 });
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000F3C5 File Offset: 0x0000D5C5
		internal static string UnknownEnumVal_TypeKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_TypeKind", new object[] { p0 });
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0000F3DB File Offset: 0x0000D5DB
		internal static string UnknownEnumVal_PrimitiveKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_PrimitiveKind", new object[] { p0 });
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0000F3F1 File Offset: 0x0000D5F1
		internal static string UnknownEnumVal_ContainerElementKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_ContainerElementKind", new object[] { p0 });
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0000F407 File Offset: 0x0000D607
		internal static string UnknownEnumVal_CsdlTarget(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_CsdlTarget", new object[] { p0 });
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0000F41D File Offset: 0x0000D61D
		internal static string UnknownEnumVal_PropertyKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_PropertyKind", new object[] { p0 });
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0000F433 File Offset: 0x0000D633
		internal static string UnknownEnumVal_ExpressionKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_ExpressionKind", new object[] { p0 });
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000F449 File Offset: 0x0000D649
		internal static string Bad_AmbiguousElementBinding(object p0)
		{
			return EntityRes.GetString("Bad_AmbiguousElementBinding", new object[] { p0 });
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0000F45F File Offset: 0x0000D65F
		internal static string Bad_UnresolvedType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedType", new object[] { p0 });
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0000F475 File Offset: 0x0000D675
		internal static string Bad_UnresolvedComplexType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedComplexType", new object[] { p0 });
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0000F48B File Offset: 0x0000D68B
		internal static string Bad_UnresolvedEntityType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEntityType", new object[] { p0 });
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0000F4A1 File Offset: 0x0000D6A1
		internal static string Bad_UnresolvedPrimitiveType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0000F4B7 File Offset: 0x0000D6B7
		internal static string Bad_UnresolvedOperation(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedOperation", new object[] { p0 });
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0000F4CD File Offset: 0x0000D6CD
		internal static string Bad_AmbiguousOperation(object p0)
		{
			return EntityRes.GetString("Bad_AmbiguousOperation", new object[] { p0 });
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000F4E3 File Offset: 0x0000D6E3
		internal static string Bad_OperationParametersDontMatch(object p0)
		{
			return EntityRes.GetString("Bad_OperationParametersDontMatch", new object[] { p0 });
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000F4F9 File Offset: 0x0000D6F9
		internal static string Bad_UnresolvedEntitySet(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEntitySet", new object[] { p0 });
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0000F50F File Offset: 0x0000D70F
		internal static string Bad_UnresolvedEntityContainer(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEntityContainer", new object[] { p0 });
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0000F525 File Offset: 0x0000D725
		internal static string Bad_UnresolvedEnumType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEnumType", new object[] { p0 });
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0000F53B File Offset: 0x0000D73B
		internal static string Bad_UnresolvedEnumMember(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEnumMember", new object[] { p0 });
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0000F551 File Offset: 0x0000D751
		internal static string Bad_UnresolvedProperty(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedProperty", new object[] { p0 });
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0000F567 File Offset: 0x0000D767
		internal static string Bad_UnresolvedParameter(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedParameter", new object[] { p0 });
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0000F57D File Offset: 0x0000D77D
		internal static string Bad_UnresolvedReturn(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedReturn", new object[] { p0 });
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000F593 File Offset: 0x0000D793
		internal static string Bad_UnresolvedLabeledElement(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedLabeledElement", new object[] { p0 });
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0000F5A9 File Offset: 0x0000D7A9
		internal static string Bad_CyclicEntity(object p0)
		{
			return EntityRes.GetString("Bad_CyclicEntity", new object[] { p0 });
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0000F5BF File Offset: 0x0000D7BF
		internal static string Bad_CyclicComplex(object p0)
		{
			return EntityRes.GetString("Bad_CyclicComplex", new object[] { p0 });
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0000F5D5 File Offset: 0x0000D7D5
		internal static string Bad_CyclicEntityContainer(object p0)
		{
			return EntityRes.GetString("Bad_CyclicEntityContainer", new object[] { p0 });
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0000F5EB File Offset: 0x0000D7EB
		internal static string Bad_UnresolvedNavigationPropertyPath(object p0, object p1)
		{
			return EntityRes.GetString("Bad_UnresolvedNavigationPropertyPath", new object[] { p0, p1 });
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0000F605 File Offset: 0x0000D805
		internal static string RuleSet_DuplicateRulesExistInRuleSet
		{
			get
			{
				return EntityRes.GetString("RuleSet_DuplicateRulesExistInRuleSet");
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0000F611 File Offset: 0x0000D811
		internal static string EdmToClr_UnsupportedType(object p0)
		{
			return EntityRes.GetString("EdmToClr_UnsupportedType", new object[] { p0 });
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0000F627 File Offset: 0x0000D827
		internal static string EdmToClr_StructuredValueMappedToNonClass
		{
			get
			{
				return EntityRes.GetString("EdmToClr_StructuredValueMappedToNonClass");
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0000F633 File Offset: 0x0000D833
		internal static string EdmToClr_IEnumerableOfTPropertyAlreadyHasValue(object p0, object p1)
		{
			return EntityRes.GetString("EdmToClr_IEnumerableOfTPropertyAlreadyHasValue", new object[] { p0, p1 });
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000F64D File Offset: 0x0000D84D
		internal static string EdmToClr_StructuredPropertyDuplicateValue(object p0)
		{
			return EntityRes.GetString("EdmToClr_StructuredPropertyDuplicateValue", new object[] { p0 });
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000F663 File Offset: 0x0000D863
		internal static string EdmToClr_CannotConvertEdmValueToClrType(object p0, object p1)
		{
			return EntityRes.GetString("EdmToClr_CannotConvertEdmValueToClrType", new object[] { p0, p1 });
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0000F67D File Offset: 0x0000D87D
		internal static string EdmToClr_CannotConvertEdmCollectionValueToClrType(object p0)
		{
			return EntityRes.GetString("EdmToClr_CannotConvertEdmCollectionValueToClrType", new object[] { p0 });
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000F693 File Offset: 0x0000D893
		internal static string EdmToClr_TryCreateObjectInstanceReturnedWrongObject(object p0, object p1)
		{
			return EntityRes.GetString("EdmToClr_TryCreateObjectInstanceReturnedWrongObject", new object[] { p0, p1 });
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0000F6AD File Offset: 0x0000D8AD
		internal static string EdmUtil_NullValueForMimeTypeAnnotation
		{
			get
			{
				return EntityRes.GetString("EdmUtil_NullValueForMimeTypeAnnotation");
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0000F6B9 File Offset: 0x0000D8B9
		internal static string EdmUtil_InvalidAnnotationValue(object p0, object p1)
		{
			return EntityRes.GetString("EdmUtil_InvalidAnnotationValue", new object[] { p0, p1 });
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0000F6D3 File Offset: 0x0000D8D3
		internal static string PlatformHelper_DateTimeOffsetMustContainTimeZone(object p0)
		{
			return EntityRes.GetString("PlatformHelper_DateTimeOffsetMustContainTimeZone", new object[] { p0 });
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0000F6E9 File Offset: 0x0000D8E9
		internal static string Date_InvalidAddedOrSubtractedResults
		{
			get
			{
				return EntityRes.GetString("Date_InvalidAddedOrSubtractedResults");
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0000F6F5 File Offset: 0x0000D8F5
		internal static string Date_InvalidDateParameters(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Date_InvalidDateParameters", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0000F713 File Offset: 0x0000D913
		internal static string Date_InvalidParsingString(object p0)
		{
			return EntityRes.GetString("Date_InvalidParsingString", new object[] { p0 });
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0000F729 File Offset: 0x0000D929
		internal static string Date_InvalidCompareToTarget(object p0)
		{
			return EntityRes.GetString("Date_InvalidCompareToTarget", new object[] { p0 });
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0000F73F File Offset: 0x0000D93F
		internal static string TimeOfDay_InvalidTimeOfDayParameters(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("TimeOfDay_InvalidTimeOfDayParameters", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0000F761 File Offset: 0x0000D961
		internal static string TimeOfDay_TicksOutOfRange(object p0)
		{
			return EntityRes.GetString("TimeOfDay_TicksOutOfRange", new object[] { p0 });
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0000F777 File Offset: 0x0000D977
		internal static string TimeOfDay_ConvertErrorFromTimeSpan(object p0)
		{
			return EntityRes.GetString("TimeOfDay_ConvertErrorFromTimeSpan", new object[] { p0 });
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0000F78D File Offset: 0x0000D98D
		internal static string TimeOfDay_InvalidParsingString(object p0)
		{
			return EntityRes.GetString("TimeOfDay_InvalidParsingString", new object[] { p0 });
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0000F7A3 File Offset: 0x0000D9A3
		internal static string TimeOfDay_InvalidCompareToTarget(object p0)
		{
			return EntityRes.GetString("TimeOfDay_InvalidCompareToTarget", new object[] { p0 });
		}
	}
}
