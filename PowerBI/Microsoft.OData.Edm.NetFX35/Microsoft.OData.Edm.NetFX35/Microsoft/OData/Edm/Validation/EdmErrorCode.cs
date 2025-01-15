using System;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000277 RID: 631
	public enum EdmErrorCode
	{
		// Token: 0x040006A1 RID: 1697
		InvalidErrorCodeValue,
		// Token: 0x040006A2 RID: 1698
		XmlError = 5,
		// Token: 0x040006A3 RID: 1699
		UnexpectedXmlNodeType = 8,
		// Token: 0x040006A4 RID: 1700
		UnexpectedXmlAttribute,
		// Token: 0x040006A5 RID: 1701
		UnexpectedXmlElement,
		// Token: 0x040006A6 RID: 1702
		TextNotAllowed,
		// Token: 0x040006A7 RID: 1703
		EmptyFile,
		// Token: 0x040006A8 RID: 1704
		MissingAttribute = 15,
		// Token: 0x040006A9 RID: 1705
		InvalidName = 17,
		// Token: 0x040006AA RID: 1706
		MissingType,
		// Token: 0x040006AB RID: 1707
		AlreadyDefined,
		// Token: 0x040006AC RID: 1708
		InvalidVersionNumber = 25,
		// Token: 0x040006AD RID: 1709
		InvalidBoolean = 27,
		// Token: 0x040006AE RID: 1710
		BadProperty = 42,
		// Token: 0x040006AF RID: 1711
		InvalidPropertyType = 44,
		// Token: 0x040006B0 RID: 1712
		PrecisionOutOfRange = 51,
		// Token: 0x040006B1 RID: 1713
		ScaleOutOfRange,
		// Token: 0x040006B2 RID: 1714
		NameTooLong = 60,
		// Token: 0x040006B3 RID: 1715
		InvalidAssociation = 62,
		// Token: 0x040006B4 RID: 1716
		BadNavigationProperty = 74,
		// Token: 0x040006B5 RID: 1717
		InvalidKey,
		// Token: 0x040006B6 RID: 1718
		InterfaceCriticalPropertyValueMustNotBeNull,
		// Token: 0x040006B7 RID: 1719
		InterfaceCriticalKindValueMismatch,
		// Token: 0x040006B8 RID: 1720
		InterfaceCriticalKindValueUnexpected,
		// Token: 0x040006B9 RID: 1721
		InterfaceCriticalEnumerableMustNotHaveNullElements,
		// Token: 0x040006BA RID: 1722
		InterfaceCriticalEnumPropertyValueOutOfRange,
		// Token: 0x040006BB RID: 1723
		InterfaceCriticalNavigationPartnerInvalid,
		// Token: 0x040006BC RID: 1724
		InterfaceCriticalCycleInTypeHierarchy,
		// Token: 0x040006BD RID: 1725
		InvalidMultiplicity = 92,
		// Token: 0x040006BE RID: 1726
		InvalidAction = 96,
		// Token: 0x040006BF RID: 1727
		InvalidOnDelete,
		// Token: 0x040006C0 RID: 1728
		BadUnresolvedComplexType,
		// Token: 0x040006C1 RID: 1729
		InvalidEndEntitySet = 100,
		// Token: 0x040006C2 RID: 1730
		OperationImportEntitySetExpressionIsInvalid = 103,
		// Token: 0x040006C3 RID: 1731
		NavigationPropertyMappingMustPointToValidTargetForProperty = 109,
		// Token: 0x040006C4 RID: 1732
		InvalidRoleInRelationshipConstraint,
		// Token: 0x040006C5 RID: 1733
		InvalidPropertyInRelationshipConstraint,
		// Token: 0x040006C6 RID: 1734
		TypeMismatchRelationshipConstraint,
		// Token: 0x040006C7 RID: 1735
		InvalidMultiplicityOfPrincipalEnd,
		// Token: 0x040006C8 RID: 1736
		MismatchNumberOfPropertiesInRelationshipConstraint,
		// Token: 0x040006C9 RID: 1737
		InvalidMultiplicityOfDependentEnd = 116,
		// Token: 0x040006CA RID: 1738
		OpenTypeNotSupported,
		// Token: 0x040006CB RID: 1739
		SameRoleReferredInReferentialConstraint = 119,
		// Token: 0x040006CC RID: 1740
		EntityKeyMustBeScalar = 128,
		// Token: 0x040006CD RID: 1741
		EntityKeyMustNotBeBinary,
		// Token: 0x040006CE RID: 1742
		EndWithManyMultiplicityCannotHaveOperationsSpecified = 132,
		// Token: 0x040006CF RID: 1743
		NavigationSourceTypeHasNoKeys,
		// Token: 0x040006D0 RID: 1744
		InvalidConcurrencyMode = 144,
		// Token: 0x040006D1 RID: 1745
		ConcurrencyRedefinedOnSubtypeOfEntitySetType,
		// Token: 0x040006D2 RID: 1746
		OperationImportUnsupportedReturnType,
		// Token: 0x040006D3 RID: 1747
		OperationImportReturnsEntitiesButDoesNotSpecifyEntitySet = 148,
		// Token: 0x040006D4 RID: 1748
		OperationImportEntityTypeDoesNotMatchEntitySet,
		// Token: 0x040006D5 RID: 1749
		OperationImportSpecifiesEntitySetButDoesNotReturnEntityType,
		// Token: 0x040006D6 RID: 1750
		OperationImportCannotImportBoundOperation,
		// Token: 0x040006D7 RID: 1751
		FunctionMustHaveReturnType,
		// Token: 0x040006D8 RID: 1752
		SimilarRelationshipEnd,
		// Token: 0x040006D9 RID: 1753
		DuplicatePropertySpecifiedInEntityKey,
		// Token: 0x040006DA RID: 1754
		NullableComplexTypeProperty = 157,
		// Token: 0x040006DB RID: 1755
		KeyMissingOnEntityType = 159,
		// Token: 0x040006DC RID: 1756
		SystemNamespaceEncountered = 161,
		// Token: 0x040006DD RID: 1757
		InvalidNamespaceName = 163,
		// Token: 0x040006DE RID: 1758
		EnumMemberValueOutOfRange = 206,
		// Token: 0x040006DF RID: 1759
		DuplicateEntityContainerMemberName = 218,
		// Token: 0x040006E0 RID: 1760
		UnboundFunctionOverloadHasIncorrectReturnType,
		// Token: 0x040006E1 RID: 1761
		InvalidAbstractComplexType,
		// Token: 0x040006E2 RID: 1762
		InvalidPolymorphicComplexType,
		// Token: 0x040006E3 RID: 1763
		NavigationPropertyEntityMustNotIndirectlyContainItself,
		// Token: 0x040006E4 RID: 1764
		EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet,
		// Token: 0x040006E5 RID: 1765
		BadAmbiguousElementBinding,
		// Token: 0x040006E6 RID: 1766
		BadUnresolvedType,
		// Token: 0x040006E7 RID: 1767
		BadUnresolvedPrimitiveType,
		// Token: 0x040006E8 RID: 1768
		BadCyclicComplex,
		// Token: 0x040006E9 RID: 1769
		BadCyclicEntityContainer,
		// Token: 0x040006EA RID: 1770
		BadCyclicEntity,
		// Token: 0x040006EB RID: 1771
		TypeSemanticsCouldNotConvertTypeReference,
		// Token: 0x040006EC RID: 1772
		ConstructibleEntitySetTypeInvalidFromEntityTypeRemoval,
		// Token: 0x040006ED RID: 1773
		BadUnresolvedEntityContainer,
		// Token: 0x040006EE RID: 1774
		BadUnresolvedEntitySet,
		// Token: 0x040006EF RID: 1775
		BadUnresolvedProperty,
		// Token: 0x040006F0 RID: 1776
		BadNonComputableAssociationEnd,
		// Token: 0x040006F1 RID: 1777
		NavigationPropertyTypeInvalidBecauseOfBadAssociation,
		// Token: 0x040006F2 RID: 1778
		EntityMustHaveEntityBaseType,
		// Token: 0x040006F3 RID: 1779
		ComplexTypeMustHaveComplexBaseType,
		// Token: 0x040006F4 RID: 1780
		BadUnresolvedOperation,
		// Token: 0x040006F5 RID: 1781
		KeyPropertyMustBelongToEntity = 242,
		// Token: 0x040006F6 RID: 1782
		ReferentialConstraintPrincipalEndMustBelongToAssociation,
		// Token: 0x040006F7 RID: 1783
		DependentPropertiesMustBelongToDependentEntity,
		// Token: 0x040006F8 RID: 1784
		DeclaringTypeMustBeCorrect,
		// Token: 0x040006F9 RID: 1785
		InvalidNavigationPropertyType = 258,
		// Token: 0x040006FA RID: 1786
		UnderlyingTypeIsBadBecauseEnumTypeIsBad = 261,
		// Token: 0x040006FB RID: 1787
		ComplexTypeMustHaveProperties = 264,
		// Token: 0x040006FC RID: 1788
		OperationImportParameterIncorrectType,
		// Token: 0x040006FD RID: 1789
		DuplicateDependentProperty = 267,
		// Token: 0x040006FE RID: 1790
		BoundOperationMustHaveParameters,
		// Token: 0x040006FF RID: 1791
		OperationCannotHaveEntitySetPathWithUnBoundOperation,
		// Token: 0x04000700 RID: 1792
		InvalidPathFirstPathParameterNotMatchingFirstParameterName = 271,
		// Token: 0x04000701 RID: 1793
		InvalidPathWithNonEntityBindingParameter = 246,
		// Token: 0x04000702 RID: 1794
		OperationWithInvalidEntitySetPathMissingCompletePath = 248,
		// Token: 0x04000703 RID: 1795
		InvalidPathUnknownTypeCastSegment,
		// Token: 0x04000704 RID: 1796
		InvalidPathInvalidTypeCastSegment,
		// Token: 0x04000705 RID: 1797
		InvalidPathTypeCastSegmentMustBeEntityType,
		// Token: 0x04000706 RID: 1798
		InvalidPathUnknownNavigationProperty,
		// Token: 0x04000707 RID: 1799
		OperationWithEntitySetPathAndReturnTypeTypeNotAssignable,
		// Token: 0x04000708 RID: 1800
		OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType,
		// Token: 0x04000709 RID: 1801
		OperationWithEntitySetPathResolvesToEntityTypeMismatchesCollectionEntityTypeReturnType,
		// Token: 0x0400070A RID: 1802
		OperationWithEntitySetPathReturnTypeInvalid,
		// Token: 0x0400070B RID: 1803
		MaxLengthOutOfRange = 272,
		// Token: 0x0400070C RID: 1804
		PathExpressionHasNoEntityContext = 274,
		// Token: 0x0400070D RID: 1805
		InvalidSrid,
		// Token: 0x0400070E RID: 1806
		InvalidMaxLength,
		// Token: 0x0400070F RID: 1807
		InvalidLong,
		// Token: 0x04000710 RID: 1808
		InvalidInteger,
		// Token: 0x04000711 RID: 1809
		InvalidAssociationSet,
		// Token: 0x04000712 RID: 1810
		InvalidParameterMode,
		// Token: 0x04000713 RID: 1811
		BadUnresolvedEntityType,
		// Token: 0x04000714 RID: 1812
		InvalidValue,
		// Token: 0x04000715 RID: 1813
		InvalidBinary,
		// Token: 0x04000716 RID: 1814
		InvalidFloatingPoint,
		// Token: 0x04000717 RID: 1815
		InvalidDateTime,
		// Token: 0x04000718 RID: 1816
		InvalidDateTimeOffset,
		// Token: 0x04000719 RID: 1817
		InvalidDecimal,
		// Token: 0x0400071A RID: 1818
		InvalidGuid,
		// Token: 0x0400071B RID: 1819
		InvalidTypeKindNone,
		// Token: 0x0400071C RID: 1820
		InvalidIfExpressionIncorrectNumberOfOperands,
		// Token: 0x0400071D RID: 1821
		EnumMemberTypeMustMatchEnumUnderlyingType = 292,
		// Token: 0x0400071E RID: 1822
		InvalidIsTypeExpressionIncorrectNumberOfOperands,
		// Token: 0x0400071F RID: 1823
		InvalidTypeName,
		// Token: 0x04000720 RID: 1824
		InvalidQualifiedName,
		// Token: 0x04000721 RID: 1825
		NoReadersProvided,
		// Token: 0x04000722 RID: 1826
		NullXmlReader,
		// Token: 0x04000723 RID: 1827
		IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull,
		// Token: 0x04000724 RID: 1828
		InvalidElementAnnotation,
		// Token: 0x04000725 RID: 1829
		InvalidLabeledElementExpressionIncorrectNumberOfOperands,
		// Token: 0x04000726 RID: 1830
		BadUnresolvedLabeledElement,
		// Token: 0x04000727 RID: 1831
		BadUnresolvedEnumMember,
		// Token: 0x04000728 RID: 1832
		InvalidCastExpressionIncorrectNumberOfOperands,
		// Token: 0x04000729 RID: 1833
		BadUnresolvedParameter,
		// Token: 0x0400072A RID: 1834
		NavigationPropertyWithRecursiveContainmentTargetMustBeOptional,
		// Token: 0x0400072B RID: 1835
		NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne,
		// Token: 0x0400072C RID: 1836
		NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne,
		// Token: 0x0400072D RID: 1837
		ImpossibleAnnotationsTarget = 309,
		// Token: 0x0400072E RID: 1838
		CannotAssertNullableTypeAsNonNullableType,
		// Token: 0x0400072F RID: 1839
		CannotAssertPrimitiveExpressionAsNonPrimitiveType,
		// Token: 0x04000730 RID: 1840
		ExpressionPrimitiveKindNotValidForAssertedType,
		// Token: 0x04000731 RID: 1841
		NullCannotBeAssertedToBeANonNullableType,
		// Token: 0x04000732 RID: 1842
		ExpressionNotValidForTheAssertedType,
		// Token: 0x04000733 RID: 1843
		CollectionExpressionNotValidForNonCollectionType,
		// Token: 0x04000734 RID: 1844
		RecordExpressionNotValidForNonStructuredType,
		// Token: 0x04000735 RID: 1845
		RecordExpressionMissingRequiredProperty,
		// Token: 0x04000736 RID: 1846
		RecordExpressionHasExtraProperties,
		// Token: 0x04000737 RID: 1847
		DuplicateAnnotation,
		// Token: 0x04000738 RID: 1848
		IncorrectNumberOfArguments,
		// Token: 0x04000739 RID: 1849
		DuplicateAlias,
		// Token: 0x0400073A RID: 1850
		ReferencedTypeMustHaveValidName,
		// Token: 0x0400073B RID: 1851
		SingleFileExpected,
		// Token: 0x0400073C RID: 1852
		UnknownEdmxVersion,
		// Token: 0x0400073D RID: 1853
		UnknownEdmVersion,
		// Token: 0x0400073E RID: 1854
		NoSchemasProduced,
		// Token: 0x0400073F RID: 1855
		DuplicateEntityContainerName,
		// Token: 0x04000740 RID: 1856
		ContainerElementContainerNameIncorrect,
		// Token: 0x04000741 RID: 1857
		PrimitiveConstantExpressionNotValidForNonPrimitiveType,
		// Token: 0x04000742 RID: 1858
		IntegerConstantValueOutOfRange,
		// Token: 0x04000743 RID: 1859
		StringConstantLengthOutOfRange,
		// Token: 0x04000744 RID: 1860
		BinaryConstantLengthOutOfRange,
		// Token: 0x04000745 RID: 1861
		InvalidOperationImportParameterMode,
		// Token: 0x04000746 RID: 1862
		TypeMustNotHaveKindOfNone,
		// Token: 0x04000747 RID: 1863
		PrimitiveTypeMustNotHaveKindOfNone,
		// Token: 0x04000748 RID: 1864
		PropertyMustNotHaveKindOfNone,
		// Token: 0x04000749 RID: 1865
		TermMustNotHaveKindOfNone,
		// Token: 0x0400074A RID: 1866
		SchemaElementMustNotHaveKindOfNone,
		// Token: 0x0400074B RID: 1867
		EntityContainerElementMustNotHaveKindOfNone,
		// Token: 0x0400074C RID: 1868
		BinaryValueCannotHaveEmptyValue,
		// Token: 0x0400074D RID: 1869
		EntitySetCanOnlyBeContainedByASingleNavigationProperty,
		// Token: 0x0400074E RID: 1870
		InconsistentNavigationPropertyPartner,
		// Token: 0x0400074F RID: 1871
		EntitySetCanOnlyHaveSingleNavigationPropertyWithContainment,
		// Token: 0x04000750 RID: 1872
		NavigationMappingMustBeBidirectional,
		// Token: 0x04000751 RID: 1873
		DuplicateNavigationPropertyMapping,
		// Token: 0x04000752 RID: 1874
		AllNavigationPropertiesMustBeMapped,
		// Token: 0x04000753 RID: 1875
		TypeAnnotationMissingRequiredProperty,
		// Token: 0x04000754 RID: 1876
		TypeAnnotationHasExtraProperties,
		// Token: 0x04000755 RID: 1877
		InvalidDuration,
		// Token: 0x04000756 RID: 1878
		InvalidPrimitiveValue,
		// Token: 0x04000757 RID: 1879
		EnumMustHaveIntegerUnderlyingType,
		// Token: 0x04000758 RID: 1880
		BadUnresolvedTerm,
		// Token: 0x04000759 RID: 1881
		BadPrincipalPropertiesInReferentialConstraint,
		// Token: 0x0400075A RID: 1882
		DuplicateDirectValueAnnotationFullName,
		// Token: 0x0400075B RID: 1883
		NoEntitySetsFoundForType,
		// Token: 0x0400075C RID: 1884
		CannotInferEntitySetWithMultipleSetsPerType,
		// Token: 0x0400075D RID: 1885
		InvalidEntitySetPath,
		// Token: 0x0400075E RID: 1886
		InvalidEnumMemberPath,
		// Token: 0x0400075F RID: 1887
		QualifierMustBeSimpleName,
		// Token: 0x04000760 RID: 1888
		BadUnresolvedEnumType,
		// Token: 0x04000761 RID: 1889
		BadUnresolvedTarget,
		// Token: 0x04000762 RID: 1890
		PathIsNotValidForTheGivenContext,
		// Token: 0x04000763 RID: 1891
		BadUnresolvedNavigationPropertyPath,
		// Token: 0x04000764 RID: 1892
		NavigationPropertyWithCollectionTypeCannotHaveNullableAttribute,
		// Token: 0x04000765 RID: 1893
		MetadataDocumentCannotHaveMoreThanOneEntityContainer,
		// Token: 0x04000766 RID: 1894
		DuplicateFunctions,
		// Token: 0x04000767 RID: 1895
		DuplicateActions,
		// Token: 0x04000768 RID: 1896
		BoundFunctionOverloadsMustHaveSameReturnType,
		// Token: 0x04000769 RID: 1897
		SingletonTypeMustBeEntityType,
		// Token: 0x0400076A RID: 1898
		EntitySetTypeMustBeCollectionOfEntityType,
		// Token: 0x0400076B RID: 1899
		NavigationPropertyOfCollectionTypeMustNotTargetToSingleton,
		// Token: 0x0400076C RID: 1900
		ReferenceElementMustContainAtLeastOneIncludeOrIncludeAnnotationsElement,
		// Token: 0x0400076D RID: 1901
		FunctionImportWithParameterShouldNotBeIncludedInServiceDocument,
		// Token: 0x0400076E RID: 1902
		UnresolvedReferenceUriInEdmxReference,
		// Token: 0x0400076F RID: 1903
		InvalidDate,
		// Token: 0x04000770 RID: 1904
		InvalidTimeOfDay
	}
}
