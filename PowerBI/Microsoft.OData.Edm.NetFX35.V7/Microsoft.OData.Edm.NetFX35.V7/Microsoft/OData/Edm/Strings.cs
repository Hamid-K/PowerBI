using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000CE RID: 206
	internal static class Strings
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000CD9D File Offset: 0x0000AF9D
		internal static string EdmPrimitive_UnexpectedKind
		{
			get
			{
				return EntityRes.GetString("EdmPrimitive_UnexpectedKind");
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000CDA9 File Offset: 0x0000AFA9
		internal static string Annotations_DocumentationPun(object p0)
		{
			return EntityRes.GetString("Annotations_DocumentationPun", new object[] { p0 });
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000CDBF File Offset: 0x0000AFBF
		internal static string Annotations_TypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("Annotations_TypeMismatch", new object[] { p0, p1 });
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000CDD9 File Offset: 0x0000AFD9
		internal static string Constructable_VocabularyAnnotationMustHaveTarget
		{
			get
			{
				return EntityRes.GetString("Constructable_VocabularyAnnotationMustHaveTarget");
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000CDE5 File Offset: 0x0000AFE5
		internal static string Constructable_EntityTypeOrCollectionOfEntityTypeExpected
		{
			get
			{
				return EntityRes.GetString("Constructable_EntityTypeOrCollectionOfEntityTypeExpected");
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000CDF1 File Offset: 0x0000AFF1
		internal static string Constructable_TargetMustBeStock(object p0)
		{
			return EntityRes.GetString("Constructable_TargetMustBeStock", new object[] { p0 });
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000CE07 File Offset: 0x0000B007
		internal static string TypeSemantics_CouldNotConvertTypeReference(object p0, object p1)
		{
			return EntityRes.GetString("TypeSemantics_CouldNotConvertTypeReference", new object[] { p0, p1 });
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000CE21 File Offset: 0x0000B021
		internal static string EdmModel_CannotUseElementWithTypeNone
		{
			get
			{
				return EntityRes.GetString("EdmModel_CannotUseElementWithTypeNone");
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000CE2D File Offset: 0x0000B02D
		internal static string EdmModel_CannotAddMoreThanOneEntityContainerToOneEdmModel
		{
			get
			{
				return EntityRes.GetString("EdmModel_CannotAddMoreThanOneEntityContainerToOneEdmModel");
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000CE39 File Offset: 0x0000B039
		internal static string EdmEntityContainer_CannotUseElementWithTypeNone
		{
			get
			{
				return EntityRes.GetString("EdmEntityContainer_CannotUseElementWithTypeNone");
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000CE45 File Offset: 0x0000B045
		internal static string ValueWriter_NonSerializableValue(object p0)
		{
			return EntityRes.GetString("ValueWriter_NonSerializableValue", new object[] { p0 });
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0000CE5B File Offset: 0x0000B05B
		internal static string ValueHasAlreadyBeenSet
		{
			get
			{
				return EntityRes.GetString("ValueHasAlreadyBeenSet");
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000CE67 File Offset: 0x0000B067
		internal static string PathSegmentMustNotContainSlash
		{
			get
			{
				return EntityRes.GetString("PathSegmentMustNotContainSlash");
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000CE73 File Offset: 0x0000B073
		internal static string Constructable_DependentPropertyCountMustMatchNumberOfPropertiesOnPrincipalType(object p0, object p1)
		{
			return EntityRes.GetString("Constructable_DependentPropertyCountMustMatchNumberOfPropertiesOnPrincipalType", new object[] { p0, p1 });
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000CE8D File Offset: 0x0000B08D
		internal static string EdmType_UnexpectedEdmType
		{
			get
			{
				return EntityRes.GetString("EdmType_UnexpectedEdmType");
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000CE99 File Offset: 0x0000B099
		internal static string NavigationPropertyBinding_PathIsNotValid
		{
			get
			{
				return EntityRes.GetString("NavigationPropertyBinding_PathIsNotValid");
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000CEA5 File Offset: 0x0000B0A5
		internal static string Edm_Evaluator_NoTermTypeAnnotationOnType(object p0, object p1)
		{
			return EntityRes.GetString("Edm_Evaluator_NoTermTypeAnnotationOnType", new object[] { p0, p1 });
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000CEBF File Offset: 0x0000B0BF
		internal static string Edm_Evaluator_NoValueAnnotationOnType(object p0, object p1)
		{
			return EntityRes.GetString("Edm_Evaluator_NoValueAnnotationOnType", new object[] { p0, p1 });
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000CED9 File Offset: 0x0000B0D9
		internal static string Edm_Evaluator_NoValueAnnotationOnElement(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_NoValueAnnotationOnElement", new object[] { p0 });
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000CEEF File Offset: 0x0000B0EF
		internal static string Edm_Evaluator_UnrecognizedExpressionKind(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_UnrecognizedExpressionKind", new object[] { p0 });
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000CF05 File Offset: 0x0000B105
		internal static string Edm_Evaluator_UnboundFunction(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_UnboundFunction", new object[] { p0 });
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000CF1B File Offset: 0x0000B11B
		internal static string Edm_Evaluator_UnboundPath(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_UnboundPath", new object[] { p0 });
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0000CF31 File Offset: 0x0000B131
		internal static string Edm_Evaluator_NoContextPath
		{
			get
			{
				return EntityRes.GetString("Edm_Evaluator_NoContextPath");
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000CF3D File Offset: 0x0000B13D
		internal static string Edm_Evaluator_FailedTypeAssertion(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_FailedTypeAssertion", new object[] { p0 });
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000CF53 File Offset: 0x0000B153
		internal static string Edm_Evaluator_TypeCastNeedsEdmModel
		{
			get
			{
				return EntityRes.GetString("Edm_Evaluator_TypeCastNeedsEdmModel");
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000CF5F File Offset: 0x0000B15F
		internal static string EdmModel_Validator_Semantic_SystemNamespaceEncountered(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SystemNamespaceEncountered", new object[] { p0 });
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000CF75 File Offset: 0x0000B175
		internal static string EdmModel_Validator_Semantic_NavigationSourceTypeHasNoKeys(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationSourceTypeHasNoKeys", new object[] { p0, p1 });
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000CF8F File Offset: 0x0000B18F
		internal static string EdmModel_Validator_Semantic_DuplicateEndName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEndName", new object[] { p0 });
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000CFA5 File Offset: 0x0000B1A5
		internal static string EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey", new object[] { p0, p1 });
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000CFBF File Offset: 0x0000B1BF
		internal static string EdmModel_Validator_Semantic_InvalidComplexTypeAbstract(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidComplexTypeAbstract", new object[] { p0 });
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000CFD5 File Offset: 0x0000B1D5
		internal static string EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic", new object[] { p0 });
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000CFEB File Offset: 0x0000B1EB
		internal static string EdmModel_Validator_Semantic_InvalidKeyNullablePart(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidKeyNullablePart", new object[] { p0, p1 });
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000D005 File Offset: 0x0000B205
		internal static string EdmModel_Validator_Semantic_EntityKeyMustBeScalar(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntityKeyMustBeScalar", new object[] { p0, p1 });
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000D01F File Offset: 0x0000B21F
		internal static string EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass", new object[] { p0, p1 });
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000D039 File Offset: 0x0000B239
		internal static string EdmModel_Validator_Semantic_KeyMissingOnEntityType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_KeyMissingOnEntityType", new object[] { p0 });
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000D04F File Offset: 0x0000B24F
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000D06D File Offset: 0x0000B26D
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame", new object[] { p0 });
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000D083 File Offset: 0x0000B283
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType", new object[] { p0 });
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000D099 File Offset: 0x0000B299
		internal static string EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation");
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000D0A5 File Offset: 0x0000B2A5
		internal static string EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified", new object[] { p0 });
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000D0BB File Offset: 0x0000B2BB
		internal static string EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000D0D1 File Offset: 0x0000B2D1
		internal static string EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint", new object[] { p0 });
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000D0E7 File Offset: 0x0000B2E7
		internal static string EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne", new object[] { p0 });
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000D0FD File Offset: 0x0000B2FD
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable", new object[] { p0 });
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000D113 File Offset: 0x0000B313
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable", new object[] { p0 });
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000D129 File Offset: 0x0000B329
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne", new object[] { p0 });
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000D13F File Offset: 0x0000B33F
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany", new object[] { p0 });
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000D155 File Offset: 0x0000B355
		internal static string EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint");
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000D161 File Offset: 0x0000B361
		internal static string EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000D188 File Offset: 0x0000B388
		internal static string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintDependentEnd(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintDependentEnd", new object[] { p0, p1 });
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000D1A2 File Offset: 0x0000B3A2
		internal static string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintPrimaryEnd(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintPrimaryEnd", new object[] { p0, p1 });
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000D1BC File Offset: 0x0000B3BC
		internal static string EdmModel_Validator_Semantic_InvalidPropertyType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyType", new object[] { p0 });
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000D1D2 File Offset: 0x0000B3D2
		internal static string EdmModel_Validator_Semantic_BoundOperationMustHaveParameters(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BoundOperationMustHaveParameters", new object[] { p0 });
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000D1E8 File Offset: 0x0000B3E8
		internal static string EdmModel_Validator_Semantic_RequiredParametersMustPrecedeOptional(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_RequiredParametersMustPrecedeOptional", new object[] { p0 });
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000D1FE File Offset: 0x0000B3FE
		internal static string EdmModel_Validator_Semantic_OperationWithUnsupportedReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithUnsupportedReturnType", new object[] { p0 });
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000D214 File Offset: 0x0000B414
		internal static string EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000D232 File Offset: 0x0000B432
		internal static string EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet2(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet2", new object[] { p0, p1 });
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000D24C File Offset: 0x0000B44C
		internal static string EdmModel_Validator_Semantic_OperationImportEntitySetExpressionKindIsInvalid(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportEntitySetExpressionKindIsInvalid", new object[] { p0, p1 });
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000D266 File Offset: 0x0000B466
		internal static string EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid", new object[] { p0 });
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000D27C File Offset: 0x0000B47C
		internal static string EdmModel_Validator_Semantic_OperationImportSpecifiesEntitySetButNotEntityType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportSpecifiesEntitySetButNotEntityType", new object[] { p0 });
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000D292 File Offset: 0x0000B492
		internal static string EdmModel_Validator_Semantic_OperationImportCannotImportBoundOperation(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationImportCannotImportBoundOperation", new object[] { p0, p1 });
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000D2AC File Offset: 0x0000B4AC
		internal static string EdmModel_Validator_Semantic_FunctionImportWithParameterShouldNotBeIncludedInServiceDocument(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportWithParameterShouldNotBeIncludedInServiceDocument", new object[] { p0 });
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000D2C2 File Offset: 0x0000B4C2
		internal static string EdmModel_Validator_Semantic_FunctionMustHaveReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionMustHaveReturnType", new object[] { p0 });
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		internal static string EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000D2EE File Offset: 0x0000B4EE
		internal static string EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName", new object[] { p0 });
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000D304 File Offset: 0x0000B504
		internal static string EdmModel_Validator_Semantic_UnboundFunctionOverloadHasIncorrectReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_UnboundFunctionOverloadHasIncorrectReturnType", new object[] { p0 });
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000D31A File Offset: 0x0000B51A
		internal static string EdmModel_Validator_Semantic_OperationCannotHaveEntitySetPathWithUnBoundOperation(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationCannotHaveEntitySetPathWithUnBoundOperation", new object[] { p0 });
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000D330 File Offset: 0x0000B530
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathMissingBindingParameterName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathMissingBindingParameterName", new object[] { p0 });
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000D346 File Offset: 0x0000B546
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathWithFirstPathParameterNotMatchingFirstParameterName(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathWithFirstPathParameterNotMatchingFirstParameterName", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000D368 File Offset: 0x0000B568
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathTypeCastSegmentMustBeEntityType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathTypeCastSegmentMustBeEntityType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000D386 File Offset: 0x0000B586
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownNavigationProperty(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownNavigationProperty", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathInvalidTypeCastSegment(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathInvalidTypeCastSegment", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000D3C6 File Offset: 0x0000B5C6
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathWithNonEntityBindingParameter(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathWithNonEntityBindingParameter", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000D3E4 File Offset: 0x0000B5E4
		internal static string EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownTypeCastSegment(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownTypeCastSegment", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000D402 File Offset: 0x0000B602
		internal static string EdmModel_Validator_Semantic_OperationWithEntitySetPathReturnTypeInvalid(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithEntitySetPathReturnTypeInvalid", new object[] { p0 });
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000D418 File Offset: 0x0000B618
		internal static string EdmModel_Validator_Semantic_OperationWithEntitySetPathAndReturnTypeTypeNotAssignable(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithEntitySetPathAndReturnTypeTypeNotAssignable", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000D436 File Offset: 0x0000B636
		internal static string EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToEntityTypeMismatchesCollectionEntityTypeReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToEntityTypeMismatchesCollectionEntityTypeReturnType", new object[] { p0 });
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000D44C File Offset: 0x0000B64C
		internal static string EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType", new object[] { p0 });
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000D462 File Offset: 0x0000B662
		internal static string EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000D478 File Offset: 0x0000B678
		internal static string EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName", new object[] { p0 });
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000D48E File Offset: 0x0000B68E
		internal static string EdmModel_Validator_Semantic_PropertyNameAlreadyDefined(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PropertyNameAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0000D4A4 File Offset: 0x0000B6A4
		internal static string EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind");
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		internal static string EdmModel_Validator_Semantic_BaseTypeOfOpenTypeMustBeOpen(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BaseTypeOfOpenTypeMustBeOpen", new object[] { p0 });
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0000D4C6 File Offset: 0x0000B6C6
		internal static string EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity", new object[] { p0, p1 });
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		internal static string EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity", new object[] { p0, p1 });
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000D4FA File Offset: 0x0000B6FA
		internal static string EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect", new object[] { p0 });
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0000D510 File Offset: 0x0000B710
		internal static string EdmModel_Validator_Semantic_InaccessibleType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InaccessibleType", new object[] { p0 });
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000D526 File Offset: 0x0000B726
		internal static string EdmModel_Validator_Semantic_AmbiguousType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_AmbiguousType", new object[] { p0 });
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000D53C File Offset: 0x0000B73C
		internal static string EdmModel_Validator_Semantic_InvalidNavigationPropertyType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidNavigationPropertyType", new object[] { p0 });
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000D552 File Offset: 0x0000B752
		internal static string EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional", new object[] { p0 });
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000D568 File Offset: 0x0000B768
		internal static string EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne", new object[] { p0 });
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000D57E File Offset: 0x0000B77E
		internal static string EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne", new object[] { p0 });
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000D594 File Offset: 0x0000B794
		internal static string EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties", new object[] { p0 });
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000D5AA File Offset: 0x0000B7AA
		internal static string EdmModel_Validator_Semantic_DuplicateDependentProperty(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateDependentProperty", new object[] { p0, p1 });
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		internal static string EdmModel_Validator_Semantic_ScaleOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ScaleOutOfRange");
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		internal static string EdmModel_Validator_Semantic_PrecisionOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_PrecisionOutOfRange");
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		internal static string EdmModel_Validator_Semantic_StringMaxLengthOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_StringMaxLengthOutOfRange");
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		internal static string EdmModel_Validator_Semantic_MaxLengthOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_MaxLengthOutOfRange");
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000D5F4 File Offset: 0x0000B7F4
		internal static string EdmModel_Validator_Semantic_EnumMemberValueOutOfRange(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EnumMemberValueOutOfRange", new object[] { p0 });
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000D60A File Offset: 0x0000B80A
		internal static string EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000D620 File Offset: 0x0000B820
		internal static string EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly");
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0000D62C File Offset: 0x0000B82C
		internal static string EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull");
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000D638 File Offset: 0x0000B838
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm");
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000D644 File Offset: 0x0000B844
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml");
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000D650 File Offset: 0x0000B850
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationNotIEdmStringValue
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationNotIEdmStringValue");
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000D65C File Offset: 0x0000B85C
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationNullNamespaceOrName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationNullNamespaceOrName");
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000D668 File Offset: 0x0000B868
		internal static string EdmModel_Validator_Semantic_CannotAssertNullableTypeAsNonNullableType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_CannotAssertNullableTypeAsNonNullableType", new object[] { p0 });
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000D67E File Offset: 0x0000B87E
		internal static string EdmModel_Validator_Semantic_ExpressionPrimitiveKindCannotPromoteToAssertedType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ExpressionPrimitiveKindCannotPromoteToAssertedType", new object[] { p0, p1 });
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000D698 File Offset: 0x0000B898
		internal static string EdmModel_Validator_Semantic_NullCannotBeAssertedToBeANonNullableType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_NullCannotBeAssertedToBeANonNullableType");
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0000D6A4 File Offset: 0x0000B8A4
		internal static string EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType");
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
		internal static string EdmModel_Validator_Semantic_CollectionExpressionNotValidForNonCollectionType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_CollectionExpressionNotValidForNonCollectionType");
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0000D6BC File Offset: 0x0000B8BC
		internal static string EdmModel_Validator_Semantic_PrimitiveConstantExpressionNotValidForNonPrimitiveType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_PrimitiveConstantExpressionNotValidForNonPrimitiveType");
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		internal static string EdmModel_Validator_Semantic_RecordExpressionNotValidForNonStructuredType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_RecordExpressionNotValidForNonStructuredType");
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		internal static string EdmModel_Validator_Semantic_RecordExpressionMissingProperty(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_RecordExpressionMissingProperty", new object[] { p0 });
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000D6EA File Offset: 0x0000B8EA
		internal static string EdmModel_Validator_Semantic_RecordExpressionHasExtraProperties(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_RecordExpressionHasExtraProperties", new object[] { p0 });
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000D700 File Offset: 0x0000B900
		internal static string EdmModel_Validator_Semantic_DuplicateAnnotation(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateAnnotation", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000D71E File Offset: 0x0000B91E
		internal static string EdmModel_Validator_Semantic_IncorrectNumberOfArguments(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_IncorrectNumberOfArguments", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000D73C File Offset: 0x0000B93C
		internal static string EdmModel_Validator_Semantic_DuplicateEntityContainerName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEntityContainerName", new object[] { p0 });
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000D752 File Offset: 0x0000B952
		internal static string EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType");
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0000D75E File Offset: 0x0000B95E
		internal static string EdmModel_Validator_Semantic_IntegerConstantValueOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_IntegerConstantValueOutOfRange");
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000D76A File Offset: 0x0000B96A
		internal static string EdmModel_Validator_Semantic_StringConstantLengthOutOfRange(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_StringConstantLengthOutOfRange", new object[] { p0, p1 });
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000D784 File Offset: 0x0000B984
		internal static string EdmModel_Validator_Semantic_BinaryConstantLengthOutOfRange(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BinaryConstantLengthOutOfRange", new object[] { p0, p1 });
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000D79E File Offset: 0x0000B99E
		internal static string EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone");
			}
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000D7AA File Offset: 0x0000B9AA
		internal static string EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		internal static string EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000D7D6 File Offset: 0x0000B9D6
		internal static string EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		internal static string EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000D802 File Offset: 0x0000BA02
		internal static string EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping", new object[] { p0, p1 });
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000D81C File Offset: 0x0000BA1C
		internal static string EdmModel_Validator_Semantic_NavigationMappingMustBeBidirectional(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationMappingMustBeBidirectional", new object[] { p0, p1 });
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000D836 File Offset: 0x0000BA36
		internal static string EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty", new object[] { p0 });
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000D84C File Offset: 0x0000BA4C
		internal static string EdmModel_Validator_Semantic_TypeAnnotationMissingRequiredProperty(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeAnnotationMissingRequiredProperty", new object[] { p0 });
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000D862 File Offset: 0x0000BA62
		internal static string EdmModel_Validator_Semantic_TypeAnnotationHasExtraProperties(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeAnnotationHasExtraProperties", new object[] { p0 });
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000D878 File Offset: 0x0000BA78
		internal static string EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType", new object[] { p0 });
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0000D88E File Offset: 0x0000BA8E
		internal static string EdmModel_Validator_Semantic_InaccessibleTerm(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InaccessibleTerm", new object[] { p0 });
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000D8A4 File Offset: 0x0000BAA4
		internal static string EdmModel_Validator_Semantic_InaccessibleTarget(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InaccessibleTarget", new object[] { p0 });
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000D8BA File Offset: 0x0000BABA
		internal static string EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique", new object[] { p0, p1 });
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000D8D4 File Offset: 0x0000BAD4
		internal static string EdmModel_Validator_Semantic_NoEntitySetsFoundForType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NoEntitySetsFoundForType", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000D8F2 File Offset: 0x0000BAF2
		internal static string EdmModel_Validator_Semantic_CannotInferEntitySetWithMultipleSetsPerType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_CannotInferEntitySetWithMultipleSetsPerType", new object[] { p0, p1, p2 });
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000D910 File Offset: 0x0000BB10
		internal static string EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet", new object[] { p0, p1 });
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0000D92A File Offset: 0x0000BB2A
		internal static string EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself", new object[] { p0 });
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000D940 File Offset: 0x0000BB40
		internal static string EdmModel_Validator_Semantic_PathIsNotValidForTheGivenContext(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PathIsNotValidForTheGivenContext", new object[] { p0 });
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000D956 File Offset: 0x0000BB56
		internal static string EdmModel_Validator_Semantic_NavigationPropertyMappingMustPointToValidTargetForProperty(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyMappingMustPointToValidTargetForProperty", new object[] { p0, p1 });
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000D970 File Offset: 0x0000BB70
		internal static string EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterNames(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterNames", new object[] { p0 });
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000D986 File Offset: 0x0000BB86
		internal static string EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterTypes(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterTypes", new object[] { p0 });
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000D99C File Offset: 0x0000BB9C
		internal static string EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterNames(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterNames", new object[] { p0 });
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000D9B2 File Offset: 0x0000BBB2
		internal static string EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterTypes(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterTypes", new object[] { p0 });
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000D9C8 File Offset: 0x0000BBC8
		internal static string EdmModel_Validator_Semantic_ModelDuplicateBoundActions(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateBoundActions", new object[] { p0 });
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000D9DE File Offset: 0x0000BBDE
		internal static string EdmModel_Validator_Semantic_ModelDuplicateUnBoundActions(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ModelDuplicateUnBoundActions", new object[] { p0 });
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		internal static string EdmModel_Validator_Semantic_BoundFunctionOverloadsMustHaveSameReturnType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BoundFunctionOverloadsMustHaveSameReturnType", new object[] { p0, p1 });
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000DA0E File Offset: 0x0000BC0E
		internal static string EdmModel_Validator_Semantic_EntitySetTypeMustBeCollectionOfEntityType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetTypeMustBeCollectionOfEntityType", new object[] { p0, p1 });
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000DA28 File Offset: 0x0000BC28
		internal static string EdmModel_Validator_Semantic_SingletonTypeMustBeEntityType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SingletonTypeMustBeEntityType", new object[] { p0, p1 });
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000DA42 File Offset: 0x0000BC42
		internal static string EdmModel_Validator_Semantic_NavigationPropertyOfCollectionTypeMustNotTargetToSingleton(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyOfCollectionTypeMustNotTargetToSingleton", new object[] { p0, p1 });
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000DA5C File Offset: 0x0000BC5C
		internal static string EdmModel_Validator_Syntactic_MissingName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_MissingName");
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000DA68 File Offset: 0x0000BC68
		internal static string EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong", new object[] { p0 });
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0000DA7E File Offset: 0x0000BC7E
		internal static string EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed", new object[] { p0 });
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000DA94 File Offset: 0x0000BC94
		internal static string EdmModel_Validator_Syntactic_MissingNamespaceName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_MissingNamespaceName");
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0000DAA0 File Offset: 0x0000BCA0
		internal static string EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong", new object[] { p0 });
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0000DAB6 File Offset: 0x0000BCB6
		internal static string EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed", new object[] { p0 });
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0000DACC File Offset: 0x0000BCCC
		internal static string EdmModel_Validator_Syntactic_PropertyMustNotBeNull(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_PropertyMustNotBeNull", new object[] { p0, p1 });
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0000DAE6 File Offset: 0x0000BCE6
		internal static string EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0000DB08 File Offset: 0x0000BD08
		internal static string EdmModel_Validator_Syntactic_InterfaceKindValueMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_InterfaceKindValueMismatch", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0000DB2A File Offset: 0x0000BD2A
		internal static string EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch", new object[] { p0, p1 });
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0000DB44 File Offset: 0x0000BD44
		internal static string EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0000DB62 File Offset: 0x0000BD62
		internal static string EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements", new object[] { p0, p1 });
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0000DB7C File Offset: 0x0000BD7C
		internal static string EdmModel_Validator_Syntactic_NavigationPartnerInvalid(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_NavigationPartnerInvalid", new object[] { p0 });
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000DB92 File Offset: 0x0000BD92
		internal static string EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy", new object[] { p0 });
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		internal static string Serializer_SingleFileExpected
		{
			get
			{
				return EntityRes.GetString("Serializer_SingleFileExpected");
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000DBB4 File Offset: 0x0000BDB4
		internal static string Serializer_UnknownEdmVersion
		{
			get
			{
				return EntityRes.GetString("Serializer_UnknownEdmVersion");
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		internal static string Serializer_UnknownEdmxVersion
		{
			get
			{
				return EntityRes.GetString("Serializer_UnknownEdmxVersion");
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		internal static string Serializer_NonInlineOperationImportReturnType(object p0)
		{
			return EntityRes.GetString("Serializer_NonInlineOperationImportReturnType", new object[] { p0 });
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000DBE2 File Offset: 0x0000BDE2
		internal static string Serializer_ReferencedTypeMustHaveValidName(object p0)
		{
			return EntityRes.GetString("Serializer_ReferencedTypeMustHaveValidName", new object[] { p0 });
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000DBF8 File Offset: 0x0000BDF8
		internal static string Serializer_OutOfLineAnnotationTargetMustHaveValidName(object p0)
		{
			return EntityRes.GetString("Serializer_OutOfLineAnnotationTargetMustHaveValidName", new object[] { p0 });
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000DC0E File Offset: 0x0000BE0E
		internal static string Serializer_NoSchemasProduced
		{
			get
			{
				return EntityRes.GetString("Serializer_NoSchemasProduced");
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000DC1A File Offset: 0x0000BE1A
		internal static string XmlParser_EmptyFile(object p0)
		{
			return EntityRes.GetString("XmlParser_EmptyFile", new object[] { p0 });
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000DC30 File Offset: 0x0000BE30
		internal static string XmlParser_EmptySchemaTextReader
		{
			get
			{
				return EntityRes.GetString("XmlParser_EmptySchemaTextReader");
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000DC3C File Offset: 0x0000BE3C
		internal static string XmlParser_MissingAttribute(object p0, object p1)
		{
			return EntityRes.GetString("XmlParser_MissingAttribute", new object[] { p0, p1 });
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000DC56 File Offset: 0x0000BE56
		internal static string XmlParser_TextNotAllowed(object p0)
		{
			return EntityRes.GetString("XmlParser_TextNotAllowed", new object[] { p0 });
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		internal static string XmlParser_UnexpectedAttribute(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedAttribute", new object[] { p0 });
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000DC82 File Offset: 0x0000BE82
		internal static string XmlParser_UnexpectedElement(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedElement", new object[] { p0 });
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000DC98 File Offset: 0x0000BE98
		internal static string XmlParser_UnusedElement(object p0)
		{
			return EntityRes.GetString("XmlParser_UnusedElement", new object[] { p0 });
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000DCAE File Offset: 0x0000BEAE
		internal static string XmlParser_UnexpectedNodeType(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedNodeType", new object[] { p0 });
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000DCC4 File Offset: 0x0000BEC4
		internal static string XmlParser_UnexpectedRootElement(object p0, object p1)
		{
			return EntityRes.GetString("XmlParser_UnexpectedRootElement", new object[] { p0, p1 });
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0000DCDE File Offset: 0x0000BEDE
		internal static string XmlParser_UnexpectedRootElementWrongNamespace(object p0, object p1)
		{
			return EntityRes.GetString("XmlParser_UnexpectedRootElementWrongNamespace", new object[] { p0, p1 });
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0000DCF8 File Offset: 0x0000BEF8
		internal static string XmlParser_UnexpectedRootElementNoNamespace(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedRootElementNoNamespace", new object[] { p0 });
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0000DD0E File Offset: 0x0000BF0E
		internal static string CsdlParser_InvalidEntitySetPathWithUnboundAction(object p0, object p1)
		{
			return EntityRes.GetString("CsdlParser_InvalidEntitySetPathWithUnboundAction", new object[] { p0, p1 });
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0000DD28 File Offset: 0x0000BF28
		internal static string CsdlParser_InvalidAlias(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidAlias", new object[] { p0 });
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0000DD3E File Offset: 0x0000BF3E
		internal static string CsdlParser_InvalidDeleteAction(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidDeleteAction", new object[] { p0 });
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000DD54 File Offset: 0x0000BF54
		internal static string CsdlParser_MissingTypeAttributeOrElement
		{
			get
			{
				return EntityRes.GetString("CsdlParser_MissingTypeAttributeOrElement");
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0000DD60 File Offset: 0x0000BF60
		internal static string CsdlParser_InvalidEndRoleInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("CsdlParser_InvalidEndRoleInRelationshipConstraint", new object[] { p0, p1 });
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0000DD7A File Offset: 0x0000BF7A
		internal static string CsdlParser_InvalidMultiplicity(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidMultiplicity", new object[] { p0 });
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000DD90 File Offset: 0x0000BF90
		internal static string CsdlParser_ReferentialConstraintRequiresOneDependent
		{
			get
			{
				return EntityRes.GetString("CsdlParser_ReferentialConstraintRequiresOneDependent");
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000DD9C File Offset: 0x0000BF9C
		internal static string CsdlParser_ReferentialConstraintRequiresOnePrincipal
		{
			get
			{
				return EntityRes.GetString("CsdlParser_ReferentialConstraintRequiresOnePrincipal");
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0000DDA8 File Offset: 0x0000BFA8
		internal static string CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000DDB4 File Offset: 0x0000BFB4
		internal static string CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		internal static string CsdlParser_InvalidCastExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidCastExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		internal static string CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		internal static string CsdlParser_InvalidTypeName(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidTypeName", new object[] { p0 });
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000DDEE File Offset: 0x0000BFEE
		internal static string CsdlParser_InvalidQualifiedName(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidQualifiedName", new object[] { p0 });
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000DE04 File Offset: 0x0000C004
		internal static string CsdlParser_NoReadersProvided
		{
			get
			{
				return EntityRes.GetString("CsdlParser_NoReadersProvided");
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000DE10 File Offset: 0x0000C010
		internal static string CsdlParser_NullXmlReader
		{
			get
			{
				return EntityRes.GetString("CsdlParser_NullXmlReader");
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0000DE1C File Offset: 0x0000C01C
		internal static string CsdlParser_InvalidEntitySetPath(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidEntitySetPath", new object[] { p0 });
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0000DE32 File Offset: 0x0000C032
		internal static string CsdlParser_InvalidEnumMemberPath(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidEnumMemberPath", new object[] { p0 });
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000DE48 File Offset: 0x0000C048
		internal static string CsdlParser_CannotSpecifyNullableAttributeForNavigationPropertyWithCollectionType
		{
			get
			{
				return EntityRes.GetString("CsdlParser_CannotSpecifyNullableAttributeForNavigationPropertyWithCollectionType");
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000DE54 File Offset: 0x0000C054
		internal static string CsdlParser_MetadataDocumentCannotHaveMoreThanOneEntityContainer
		{
			get
			{
				return EntityRes.GetString("CsdlParser_MetadataDocumentCannotHaveMoreThanOneEntityContainer");
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000DE60 File Offset: 0x0000C060
		internal static string CsdlSemantics_ReferentialConstraintMismatch
		{
			get
			{
				return EntityRes.GetString("CsdlSemantics_ReferentialConstraintMismatch");
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0000DE6C File Offset: 0x0000C06C
		internal static string CsdlSemantics_EnumMemberMustHaveValue
		{
			get
			{
				return EntityRes.GetString("CsdlSemantics_EnumMemberMustHaveValue");
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0000DE78 File Offset: 0x0000C078
		internal static string CsdlSemantics_ImpossibleAnnotationsTarget(object p0)
		{
			return EntityRes.GetString("CsdlSemantics_ImpossibleAnnotationsTarget", new object[] { p0 });
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000DE8E File Offset: 0x0000C08E
		internal static string CsdlSemantics_DuplicateAlias(object p0, object p1)
		{
			return EntityRes.GetString("CsdlSemantics_DuplicateAlias", new object[] { p0, p1 });
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		internal static string EdmxParser_EdmxVersionMismatch
		{
			get
			{
				return EntityRes.GetString("EdmxParser_EdmxVersionMismatch");
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
		internal static string EdmxParser_BodyElement(object p0)
		{
			return EntityRes.GetString("EdmxParser_BodyElement", new object[] { p0 });
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000DECA File Offset: 0x0000C0CA
		internal static string EdmxParser_InvalidReferenceIncorrectNumberOfIncludes
		{
			get
			{
				return EntityRes.GetString("EdmxParser_InvalidReferenceIncorrectNumberOfIncludes");
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000DED6 File Offset: 0x0000C0D6
		internal static string EdmxParser_UnresolvedReferenceUriInEdmxReference
		{
			get
			{
				return EntityRes.GetString("EdmxParser_UnresolvedReferenceUriInEdmxReference");
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0000DEE2 File Offset: 0x0000C0E2
		internal static string EdmParseException_ErrorsEncounteredInEdmx(object p0)
		{
			return EntityRes.GetString("EdmParseException_ErrorsEncounteredInEdmx", new object[] { p0 });
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
		internal static string ValueParser_InvalidBoolean(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidBoolean", new object[] { p0 });
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000DF0E File Offset: 0x0000C10E
		internal static string ValueParser_InvalidInteger(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidInteger", new object[] { p0 });
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000DF24 File Offset: 0x0000C124
		internal static string ValueParser_InvalidLong(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidLong", new object[] { p0 });
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000DF3A File Offset: 0x0000C13A
		internal static string ValueParser_InvalidFloatingPoint(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidFloatingPoint", new object[] { p0 });
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000DF50 File Offset: 0x0000C150
		internal static string ValueParser_InvalidMaxLength(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidMaxLength", new object[] { p0 });
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000DF66 File Offset: 0x0000C166
		internal static string ValueParser_InvalidSrid(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidSrid", new object[] { p0 });
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000DF7C File Offset: 0x0000C17C
		internal static string ValueParser_InvalidScale(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidScale", new object[] { p0 });
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000DF92 File Offset: 0x0000C192
		internal static string ValueParser_InvalidGuid(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidGuid", new object[] { p0 });
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		internal static string ValueParser_InvalidDecimal(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDecimal", new object[] { p0 });
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0000DFBE File Offset: 0x0000C1BE
		internal static string ValueParser_InvalidDateTimeOffset(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDateTimeOffset", new object[] { p0 });
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		internal static string ValueParser_InvalidDateTime(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDateTime", new object[] { p0 });
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0000DFEA File Offset: 0x0000C1EA
		internal static string ValueParser_InvalidDate(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDate", new object[] { p0 });
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000E000 File Offset: 0x0000C200
		internal static string ValueParser_InvalidDuration(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDuration", new object[] { p0 });
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000E016 File Offset: 0x0000C216
		internal static string ValueParser_InvalidBinary(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidBinary", new object[] { p0 });
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000E02C File Offset: 0x0000C22C
		internal static string ValueParser_InvalidTimeOfDay(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidTimeOfDay", new object[] { p0 });
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000E042 File Offset: 0x0000C242
		internal static string UnknownEnumVal_Multiplicity(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_Multiplicity", new object[] { p0 });
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000E058 File Offset: 0x0000C258
		internal static string UnknownEnumVal_SchemaElementKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_SchemaElementKind", new object[] { p0 });
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000E06E File Offset: 0x0000C26E
		internal static string UnknownEnumVal_TypeKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_TypeKind", new object[] { p0 });
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000E084 File Offset: 0x0000C284
		internal static string UnknownEnumVal_PrimitiveKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_PrimitiveKind", new object[] { p0 });
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000E09A File Offset: 0x0000C29A
		internal static string UnknownEnumVal_ContainerElementKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_ContainerElementKind", new object[] { p0 });
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000E0B0 File Offset: 0x0000C2B0
		internal static string UnknownEnumVal_CsdlTarget(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_CsdlTarget", new object[] { p0 });
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000E0C6 File Offset: 0x0000C2C6
		internal static string UnknownEnumVal_PropertyKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_PropertyKind", new object[] { p0 });
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0000E0DC File Offset: 0x0000C2DC
		internal static string UnknownEnumVal_ExpressionKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_ExpressionKind", new object[] { p0 });
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0000E0F2 File Offset: 0x0000C2F2
		internal static string Bad_AmbiguousElementBinding(object p0)
		{
			return EntityRes.GetString("Bad_AmbiguousElementBinding", new object[] { p0 });
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0000E108 File Offset: 0x0000C308
		internal static string Bad_UnresolvedType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedType", new object[] { p0 });
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0000E11E File Offset: 0x0000C31E
		internal static string Bad_UnresolvedComplexType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedComplexType", new object[] { p0 });
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000E134 File Offset: 0x0000C334
		internal static string Bad_UnresolvedEntityType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEntityType", new object[] { p0 });
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0000E14A File Offset: 0x0000C34A
		internal static string Bad_UnresolvedPrimitiveType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000E160 File Offset: 0x0000C360
		internal static string Bad_UnresolvedOperation(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedOperation", new object[] { p0 });
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0000E176 File Offset: 0x0000C376
		internal static string Bad_AmbiguousOperation(object p0)
		{
			return EntityRes.GetString("Bad_AmbiguousOperation", new object[] { p0 });
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000E18C File Offset: 0x0000C38C
		internal static string Bad_OperationParametersDontMatch(object p0)
		{
			return EntityRes.GetString("Bad_OperationParametersDontMatch", new object[] { p0 });
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000E1A2 File Offset: 0x0000C3A2
		internal static string Bad_UnresolvedEntitySet(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEntitySet", new object[] { p0 });
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0000E1B8 File Offset: 0x0000C3B8
		internal static string Bad_UnresolvedEntityContainer(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEntityContainer", new object[] { p0 });
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000E1CE File Offset: 0x0000C3CE
		internal static string Bad_UnresolvedEnumType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEnumType", new object[] { p0 });
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		internal static string Bad_UnresolvedEnumMember(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEnumMember", new object[] { p0 });
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000E1FA File Offset: 0x0000C3FA
		internal static string Bad_UnresolvedProperty(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedProperty", new object[] { p0 });
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000E210 File Offset: 0x0000C410
		internal static string Bad_UnresolvedParameter(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedParameter", new object[] { p0 });
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000E226 File Offset: 0x0000C426
		internal static string Bad_UnresolvedLabeledElement(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedLabeledElement", new object[] { p0 });
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000E23C File Offset: 0x0000C43C
		internal static string Bad_CyclicEntity(object p0)
		{
			return EntityRes.GetString("Bad_CyclicEntity", new object[] { p0 });
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000E252 File Offset: 0x0000C452
		internal static string Bad_CyclicComplex(object p0)
		{
			return EntityRes.GetString("Bad_CyclicComplex", new object[] { p0 });
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000E268 File Offset: 0x0000C468
		internal static string Bad_CyclicEntityContainer(object p0)
		{
			return EntityRes.GetString("Bad_CyclicEntityContainer", new object[] { p0 });
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0000E27E File Offset: 0x0000C47E
		internal static string Bad_UnresolvedNavigationPropertyPath(object p0, object p1)
		{
			return EntityRes.GetString("Bad_UnresolvedNavigationPropertyPath", new object[] { p0, p1 });
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0000E298 File Offset: 0x0000C498
		internal static string RuleSet_DuplicateRulesExistInRuleSet
		{
			get
			{
				return EntityRes.GetString("RuleSet_DuplicateRulesExistInRuleSet");
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0000E2A4 File Offset: 0x0000C4A4
		internal static string EdmToClr_UnsupportedType(object p0)
		{
			return EntityRes.GetString("EdmToClr_UnsupportedType", new object[] { p0 });
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0000E2BA File Offset: 0x0000C4BA
		internal static string EdmToClr_StructuredValueMappedToNonClass
		{
			get
			{
				return EntityRes.GetString("EdmToClr_StructuredValueMappedToNonClass");
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000E2C6 File Offset: 0x0000C4C6
		internal static string EdmToClr_IEnumerableOfTPropertyAlreadyHasValue(object p0, object p1)
		{
			return EntityRes.GetString("EdmToClr_IEnumerableOfTPropertyAlreadyHasValue", new object[] { p0, p1 });
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000E2E0 File Offset: 0x0000C4E0
		internal static string EdmToClr_StructuredPropertyDuplicateValue(object p0)
		{
			return EntityRes.GetString("EdmToClr_StructuredPropertyDuplicateValue", new object[] { p0 });
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000E2F6 File Offset: 0x0000C4F6
		internal static string EdmToClr_CannotConvertEdmValueToClrType(object p0, object p1)
		{
			return EntityRes.GetString("EdmToClr_CannotConvertEdmValueToClrType", new object[] { p0, p1 });
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000E310 File Offset: 0x0000C510
		internal static string EdmToClr_CannotConvertEdmCollectionValueToClrType(object p0)
		{
			return EntityRes.GetString("EdmToClr_CannotConvertEdmCollectionValueToClrType", new object[] { p0 });
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000E326 File Offset: 0x0000C526
		internal static string EdmToClr_TryCreateObjectInstanceReturnedWrongObject(object p0, object p1)
		{
			return EntityRes.GetString("EdmToClr_TryCreateObjectInstanceReturnedWrongObject", new object[] { p0, p1 });
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000E340 File Offset: 0x0000C540
		internal static string EdmUtil_NullValueForMimeTypeAnnotation
		{
			get
			{
				return EntityRes.GetString("EdmUtil_NullValueForMimeTypeAnnotation");
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000E34C File Offset: 0x0000C54C
		internal static string EdmUtil_InvalidAnnotationValue(object p0, object p1)
		{
			return EntityRes.GetString("EdmUtil_InvalidAnnotationValue", new object[] { p0, p1 });
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000E366 File Offset: 0x0000C566
		internal static string PlatformHelper_DateTimeOffsetMustContainTimeZone(object p0)
		{
			return EntityRes.GetString("PlatformHelper_DateTimeOffsetMustContainTimeZone", new object[] { p0 });
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000E37C File Offset: 0x0000C57C
		internal static string Date_InvalidAddedOrSubtractedResults
		{
			get
			{
				return EntityRes.GetString("Date_InvalidAddedOrSubtractedResults");
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000E388 File Offset: 0x0000C588
		internal static string Date_InvalidDateParameters(object p0, object p1, object p2)
		{
			return EntityRes.GetString("Date_InvalidDateParameters", new object[] { p0, p1, p2 });
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		internal static string Date_InvalidParsingString(object p0)
		{
			return EntityRes.GetString("Date_InvalidParsingString", new object[] { p0 });
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000E3BC File Offset: 0x0000C5BC
		internal static string Date_InvalidCompareToTarget(object p0)
		{
			return EntityRes.GetString("Date_InvalidCompareToTarget", new object[] { p0 });
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000E3D2 File Offset: 0x0000C5D2
		internal static string TimeOfDay_InvalidTimeOfDayParameters(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("TimeOfDay_InvalidTimeOfDayParameters", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		internal static string TimeOfDay_TicksOutOfRange(object p0)
		{
			return EntityRes.GetString("TimeOfDay_TicksOutOfRange", new object[] { p0 });
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000E40A File Offset: 0x0000C60A
		internal static string TimeOfDay_ConvertErrorFromTimeSpan(object p0)
		{
			return EntityRes.GetString("TimeOfDay_ConvertErrorFromTimeSpan", new object[] { p0 });
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000E420 File Offset: 0x0000C620
		internal static string TimeOfDay_InvalidParsingString(object p0)
		{
			return EntityRes.GetString("TimeOfDay_InvalidParsingString", new object[] { p0 });
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000E436 File Offset: 0x0000C636
		internal static string TimeOfDay_InvalidCompareToTarget(object p0)
		{
			return EntityRes.GetString("TimeOfDay_InvalidCompareToTarget", new object[] { p0 });
		}
	}
}
