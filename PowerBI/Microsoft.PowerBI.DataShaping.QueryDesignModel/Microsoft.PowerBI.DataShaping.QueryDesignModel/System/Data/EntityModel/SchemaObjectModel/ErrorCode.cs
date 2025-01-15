using System;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000025 RID: 37
	internal enum ErrorCode
	{
		// Token: 0x040005BD RID: 1469
		InvalidErrorCodeValue,
		// Token: 0x040005BE RID: 1470
		SecurityError = 2,
		// Token: 0x040005BF RID: 1471
		IOException = 4,
		// Token: 0x040005C0 RID: 1472
		XmlError,
		// Token: 0x040005C1 RID: 1473
		TooManyErrors,
		// Token: 0x040005C2 RID: 1474
		MalformedXml,
		// Token: 0x040005C3 RID: 1475
		UnexpectedXmlNodeType,
		// Token: 0x040005C4 RID: 1476
		UnexpectedXmlAttribute,
		// Token: 0x040005C5 RID: 1477
		UnexpectedXmlElement,
		// Token: 0x040005C6 RID: 1478
		TextNotAllowed,
		// Token: 0x040005C7 RID: 1479
		EmptyFile,
		// Token: 0x040005C8 RID: 1480
		XsdError,
		// Token: 0x040005C9 RID: 1481
		InvalidAlias,
		// Token: 0x040005CA RID: 1482
		IntegerExpected = 16,
		// Token: 0x040005CB RID: 1483
		InvalidName,
		// Token: 0x040005CC RID: 1484
		AlreadyDefined = 19,
		// Token: 0x040005CD RID: 1485
		ElementNotInSchema,
		// Token: 0x040005CE RID: 1486
		InvalidBaseType = 22,
		// Token: 0x040005CF RID: 1487
		NoConcreteDescendants,
		// Token: 0x040005D0 RID: 1488
		CycleInTypeHierarchy,
		// Token: 0x040005D1 RID: 1489
		InvalidVersionNumber,
		// Token: 0x040005D2 RID: 1490
		InvalidSize,
		// Token: 0x040005D3 RID: 1491
		InvalidBoolean,
		// Token: 0x040005D4 RID: 1492
		BadType = 29,
		// Token: 0x040005D5 RID: 1493
		InvalidVersioningClass = 32,
		// Token: 0x040005D6 RID: 1494
		InvalidVersionIntroduced,
		// Token: 0x040005D7 RID: 1495
		BadNamespace,
		// Token: 0x040005D8 RID: 1496
		UnresolvedReferenceSchema = 38,
		// Token: 0x040005D9 RID: 1497
		NotInNamespace = 40,
		// Token: 0x040005DA RID: 1498
		NotUnnestedType,
		// Token: 0x040005DB RID: 1499
		BadProperty,
		// Token: 0x040005DC RID: 1500
		UndefinedProperty,
		// Token: 0x040005DD RID: 1501
		InvalidPropertyType,
		// Token: 0x040005DE RID: 1502
		InvalidAsNestedType,
		// Token: 0x040005DF RID: 1503
		InvalidChangeUnit,
		// Token: 0x040005E0 RID: 1504
		UnauthorizedAccessException,
		// Token: 0x040005E1 RID: 1505
		PrecisionOutOfRange = 51,
		// Token: 0x040005E2 RID: 1506
		ScaleOutOfRange,
		// Token: 0x040005E3 RID: 1507
		DefaultNotAllowed,
		// Token: 0x040005E4 RID: 1508
		InvalidDefault,
		// Token: 0x040005E5 RID: 1509
		RequiredFacetMissing,
		// Token: 0x040005E6 RID: 1510
		BadImageFormatException,
		// Token: 0x040005E7 RID: 1511
		MissingSchemaXml,
		// Token: 0x040005E8 RID: 1512
		BadPrecisionAndScale,
		// Token: 0x040005E9 RID: 1513
		InvalidChangeUnitUsage,
		// Token: 0x040005EA RID: 1514
		NameTooLong,
		// Token: 0x040005EB RID: 1515
		CircularlyDefinedType,
		// Token: 0x040005EC RID: 1516
		InvalidAssociation,
		// Token: 0x040005ED RID: 1517
		FacetNotAllowedByType,
		// Token: 0x040005EE RID: 1518
		ConstantFacetSpecifiedInSchema,
		// Token: 0x040005EF RID: 1519
		BadNavigationProperty = 74,
		// Token: 0x040005F0 RID: 1520
		InvalidKey,
		// Token: 0x040005F1 RID: 1521
		InvalidMultiplicity = 92,
		// Token: 0x040005F2 RID: 1522
		InvalidAction = 96,
		// Token: 0x040005F3 RID: 1523
		InvalidOperation,
		// Token: 0x040005F4 RID: 1524
		InvalidContainerTypeForEnd = 99,
		// Token: 0x040005F5 RID: 1525
		InvalidEndEntitySet,
		// Token: 0x040005F6 RID: 1526
		AmbiguousEntityContainerEnd,
		// Token: 0x040005F7 RID: 1527
		MissingExtentEntityContainerEnd,
		// Token: 0x040005F8 RID: 1528
		BadParameterDirection = 106,
		// Token: 0x040005F9 RID: 1529
		FailedInference,
		// Token: 0x040005FA RID: 1530
		InvalidFacetInProviderManifest = 109,
		// Token: 0x040005FB RID: 1531
		InvalidRoleInRelationshipConstraint,
		// Token: 0x040005FC RID: 1532
		InvalidPropertyInRelationshipConstraint,
		// Token: 0x040005FD RID: 1533
		TypeMismatchRelationshipConstaint,
		// Token: 0x040005FE RID: 1534
		InvalidMultiplicityInRoleInRelationshipConstraint,
		// Token: 0x040005FF RID: 1535
		MismatchNumberOfPropertiesInRelationshipConstraint,
		// Token: 0x04000600 RID: 1536
		MissingPropertyInRelationshipConstraint,
		// Token: 0x04000601 RID: 1537
		MissingConstraintOnRelationshipType,
		// Token: 0x04000602 RID: 1538
		SameRoleReferredInReferentialConstraint = 119,
		// Token: 0x04000603 RID: 1539
		InvalidValueForParameterTypeSemantics,
		// Token: 0x04000604 RID: 1540
		InvalidRelationshipEndType,
		// Token: 0x04000605 RID: 1541
		InvalidPrimitiveTypeKind,
		// Token: 0x04000606 RID: 1542
		InvalidTypeConversionDestinationType = 124,
		// Token: 0x04000607 RID: 1543
		ByteValueExpected,
		// Token: 0x04000608 RID: 1544
		FunctionWithNonScalarTypeNotSupported,
		// Token: 0x04000609 RID: 1545
		PrecisionMoreThanAllowedMax,
		// Token: 0x0400060A RID: 1546
		EntityKeyMustBeScalar,
		// Token: 0x0400060B RID: 1547
		BinaryEntityKeyCurrentlyNotSupported,
		// Token: 0x0400060C RID: 1548
		NoPreferredMappingForPrimitiveTypeKind,
		// Token: 0x0400060D RID: 1549
		TooManyPreferredMappingsForPrimitiveTypeKind,
		// Token: 0x0400060E RID: 1550
		EndWithManyMultiplicityCannotHaveOperationsSpecified,
		// Token: 0x0400060F RID: 1551
		EntitySetTypeHasNoKeys,
		// Token: 0x04000610 RID: 1552
		InvalidNumberOfParametersForAggregateFunction,
		// Token: 0x04000611 RID: 1553
		InvalidParameterTypeForAggregateFunction,
		// Token: 0x04000612 RID: 1554
		ComposableFunctionWithoutReturnType,
		// Token: 0x04000613 RID: 1555
		NonComposableFunctionWithReturnType,
		// Token: 0x04000614 RID: 1556
		NonComposableFunctionAttributesNotValid,
		// Token: 0x04000615 RID: 1557
		ComposableFunctionWithCommandText,
		// Token: 0x04000616 RID: 1558
		FunctionDeclaresCommandTextAndStoreFunctionName,
		// Token: 0x04000617 RID: 1559
		SystemNamespace,
		// Token: 0x04000618 RID: 1560
		EmptyDefiningQuery,
		// Token: 0x04000619 RID: 1561
		TableAndSchemaAreMutuallyExclusiveWithDefiningQuery,
		// Token: 0x0400061A RID: 1562
		ConcurrencyRedefinedOnSubTypeOfEntitySetType = 145,
		// Token: 0x0400061B RID: 1563
		FunctionImportUnsupportedReturnType,
		// Token: 0x0400061C RID: 1564
		FunctionImportUnknownEntitySet,
		// Token: 0x0400061D RID: 1565
		FunctionImportReturnsEntitiesButDoesNotSpecifyEntitySet,
		// Token: 0x0400061E RID: 1566
		FunctionImportEntityTypeDoesNotMatchEntitySet,
		// Token: 0x0400061F RID: 1567
		FunctionImportSpecifiesEntitySetButDoesNotReturnEntityType,
		// Token: 0x04000620 RID: 1568
		InternalError = 152,
		// Token: 0x04000621 RID: 1569
		SimilarRelationshipEnd,
		// Token: 0x04000622 RID: 1570
		DuplicatePropertySpecifiedInEntityKey,
		// Token: 0x04000623 RID: 1571
		AmbiguousFunctionReturnType = 156,
		// Token: 0x04000624 RID: 1572
		NullableComplexType,
		// Token: 0x04000625 RID: 1573
		NonComplexCollections,
		// Token: 0x04000626 RID: 1574
		KeyMissingOnEntityType,
		// Token: 0x04000627 RID: 1575
		InvalidNamespaceInUsing,
		// Token: 0x04000628 RID: 1576
		NeedNotUseSystemNamespaceInUsing,
		// Token: 0x04000629 RID: 1577
		CannotUseSystemNamespaceAsAlias,
		// Token: 0x0400062A RID: 1578
		InvalidNamespaceName,
		// Token: 0x0400062B RID: 1579
		InvalidEntityContainerNameInExtends,
		// Token: 0x0400062C RID: 1580
		InvalidNamespaceOrAliasSpecified = 166,
		// Token: 0x0400062D RID: 1581
		EntityContainerCannotExtendItself,
		// Token: 0x0400062E RID: 1582
		FailedToRetrieveProviderManifest,
		// Token: 0x0400062F RID: 1583
		ProviderManifestTokenMismatch,
		// Token: 0x04000630 RID: 1584
		ProviderManifestTokenNotFound,
		// Token: 0x04000631 RID: 1585
		EmptyCommandText,
		// Token: 0x04000632 RID: 1586
		InconsistentProvider,
		// Token: 0x04000633 RID: 1587
		InconsistentProviderManifestToken,
		// Token: 0x04000634 RID: 1588
		DuplicatedFunctionoverloads,
		// Token: 0x04000635 RID: 1589
		InvalidProvider,
		// Token: 0x04000636 RID: 1590
		FunctionWithNonEdmTypeNotSupported,
		// Token: 0x04000637 RID: 1591
		ComplexTypeAsReturnTypeAndDefinedEntitySet,
		// Token: 0x04000638 RID: 1592
		ComplexTypeAsReturnTypeAndNestedComplexProperty,
		// Token: 0x04000639 RID: 1593
		ModelFuncionFacetOnNonScalarType = 182,
		// Token: 0x0400063A RID: 1594
		ModelFunctionIncorrectlyPlacedFacet,
		// Token: 0x0400063B RID: 1595
		ModelFunctionReturnTypeNotDeclared,
		// Token: 0x0400063C RID: 1596
		ModelFunctionTypeNotDeclared,
		// Token: 0x0400063D RID: 1597
		RowTypeWithoutProperty,
		// Token: 0x0400063E RID: 1598
		ReturnTypeDeclaredAsAttributeAndElement,
		// Token: 0x0400063F RID: 1599
		TypeDeclaredAsAttributeAndElement,
		// Token: 0x04000640 RID: 1600
		ReferenceToNonEntityType,
		// Token: 0x04000641 RID: 1601
		InvalidValueInEnumOption,
		// Token: 0x04000642 RID: 1602
		IncompatibleSchemaVersion,
		// Token: 0x04000643 RID: 1603
		NoCodeGenNamespaceInStructuralAnnotation,
		// Token: 0x04000644 RID: 1604
		AmbiguousFunctionAndType,
		// Token: 0x04000645 RID: 1605
		CannotLoadDifferentVersionOfSchemaInTheSameItemCollection
	}
}
