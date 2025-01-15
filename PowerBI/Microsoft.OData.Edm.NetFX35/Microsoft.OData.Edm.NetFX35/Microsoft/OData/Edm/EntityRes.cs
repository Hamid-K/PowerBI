using System;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200027C RID: 636
	internal sealed class EntityRes
	{
		// Token: 0x06000E54 RID: 3668 RVA: 0x0002B99D File Offset: 0x00029B9D
		internal EntityRes()
		{
			this.resources = new ResourceManager("Microsoft.OData.Edm", base.GetType().Assembly);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0002B9C0 File Offset: 0x00029BC0
		private static EntityRes GetLoader()
		{
			if (EntityRes.loader == null)
			{
				EntityRes entityRes = new EntityRes();
				Interlocked.CompareExchange<EntityRes>(ref EntityRes.loader, entityRes, null);
			}
			return EntityRes.loader;
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0002B9EC File Offset: 0x00029BEC
		private static CultureInfo Culture
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0002B9EF File Offset: 0x00029BEF
		public static ResourceManager Resources
		{
			get
			{
				return EntityRes.GetLoader().resources;
			}
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0002B9FC File Offset: 0x00029BFC
		public static string GetString(string name, params object[] args)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			string @string = entityRes.resources.GetString(name, EntityRes.Culture);
			if (args != null && args.Length > 0)
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

		// Token: 0x06000E59 RID: 3673 RVA: 0x0002BA80 File Offset: 0x00029C80
		public static string GetString(string name)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			return entityRes.resources.GetString(name, EntityRes.Culture);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0002BAA9 File Offset: 0x00029CA9
		public static string GetString(string name, out bool usedFallback)
		{
			usedFallback = false;
			return EntityRes.GetString(name);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0002BAB4 File Offset: 0x00029CB4
		public static object GetObject(string name)
		{
			EntityRes entityRes = EntityRes.GetLoader();
			if (entityRes == null)
			{
				return null;
			}
			return entityRes.resources.GetObject(name, EntityRes.Culture);
		}

		// Token: 0x0400077B RID: 1915
		internal const string EdmPrimitive_UnexpectedKind = "EdmPrimitive_UnexpectedKind";

		// Token: 0x0400077C RID: 1916
		internal const string Annotations_DocumentationPun = "Annotations_DocumentationPun";

		// Token: 0x0400077D RID: 1917
		internal const string Annotations_TypeMismatch = "Annotations_TypeMismatch";

		// Token: 0x0400077E RID: 1918
		internal const string Constructable_VocabularyAnnotationMustHaveTarget = "Constructable_VocabularyAnnotationMustHaveTarget";

		// Token: 0x0400077F RID: 1919
		internal const string Constructable_EntityTypeOrCollectionOfEntityTypeExpected = "Constructable_EntityTypeOrCollectionOfEntityTypeExpected";

		// Token: 0x04000780 RID: 1920
		internal const string Constructable_TargetMustBeStock = "Constructable_TargetMustBeStock";

		// Token: 0x04000781 RID: 1921
		internal const string TypeSemantics_CouldNotConvertTypeReference = "TypeSemantics_CouldNotConvertTypeReference";

		// Token: 0x04000782 RID: 1922
		internal const string EdmModel_CannotUseElementWithTypeNone = "EdmModel_CannotUseElementWithTypeNone";

		// Token: 0x04000783 RID: 1923
		internal const string EdmModel_CannotAddMoreThanOneEntityContainerToOneEdmModel = "EdmModel_CannotAddMoreThanOneEntityContainerToOneEdmModel";

		// Token: 0x04000784 RID: 1924
		internal const string EdmEntityContainer_CannotUseElementWithTypeNone = "EdmEntityContainer_CannotUseElementWithTypeNone";

		// Token: 0x04000785 RID: 1925
		internal const string ValueWriter_NonSerializableValue = "ValueWriter_NonSerializableValue";

		// Token: 0x04000786 RID: 1926
		internal const string ValueHasAlreadyBeenSet = "ValueHasAlreadyBeenSet";

		// Token: 0x04000787 RID: 1927
		internal const string PathSegmentMustNotContainSlash = "PathSegmentMustNotContainSlash";

		// Token: 0x04000788 RID: 1928
		internal const string Constructable_DependentPropertyCountMustMatchNumberOfPropertiesOnPrincipalType = "Constructable_DependentPropertyCountMustMatchNumberOfPropertiesOnPrincipalType";

		// Token: 0x04000789 RID: 1929
		internal const string EdmType_UnexpectedEdmType = "EdmType_UnexpectedEdmType";

		// Token: 0x0400078A RID: 1930
		internal const string Edm_Evaluator_NoTermTypeAnnotationOnType = "Edm_Evaluator_NoTermTypeAnnotationOnType";

		// Token: 0x0400078B RID: 1931
		internal const string Edm_Evaluator_NoValueAnnotationOnType = "Edm_Evaluator_NoValueAnnotationOnType";

		// Token: 0x0400078C RID: 1932
		internal const string Edm_Evaluator_NoValueAnnotationOnElement = "Edm_Evaluator_NoValueAnnotationOnElement";

		// Token: 0x0400078D RID: 1933
		internal const string Edm_Evaluator_UnrecognizedExpressionKind = "Edm_Evaluator_UnrecognizedExpressionKind";

		// Token: 0x0400078E RID: 1934
		internal const string Edm_Evaluator_UnboundFunction = "Edm_Evaluator_UnboundFunction";

		// Token: 0x0400078F RID: 1935
		internal const string Edm_Evaluator_UnboundPath = "Edm_Evaluator_UnboundPath";

		// Token: 0x04000790 RID: 1936
		internal const string Edm_Evaluator_NoContextPath = "Edm_Evaluator_NoContextPath";

		// Token: 0x04000791 RID: 1937
		internal const string Edm_Evaluator_FailedTypeAssertion = "Edm_Evaluator_FailedTypeAssertion";

		// Token: 0x04000792 RID: 1938
		internal const string Edm_Evaluator_TypeCastNeedsEdmModel = "Edm_Evaluator_TypeCastNeedsEdmModel";

		// Token: 0x04000793 RID: 1939
		internal const string EdmModel_Validator_Semantic_SystemNamespaceEncountered = "EdmModel_Validator_Semantic_SystemNamespaceEncountered";

		// Token: 0x04000794 RID: 1940
		internal const string EdmModel_Validator_Semantic_NavigationSourceTypeHasNoKeys = "EdmModel_Validator_Semantic_NavigationSourceTypeHasNoKeys";

		// Token: 0x04000795 RID: 1941
		internal const string EdmModel_Validator_Semantic_DuplicateEndName = "EdmModel_Validator_Semantic_DuplicateEndName";

		// Token: 0x04000796 RID: 1942
		internal const string EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey = "EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey";

		// Token: 0x04000797 RID: 1943
		internal const string EdmModel_Validator_Semantic_InvalidComplexTypeAbstract = "EdmModel_Validator_Semantic_InvalidComplexTypeAbstract";

		// Token: 0x04000798 RID: 1944
		internal const string EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic = "EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic";

		// Token: 0x04000799 RID: 1945
		internal const string EdmModel_Validator_Semantic_InvalidKeyNullablePart = "EdmModel_Validator_Semantic_InvalidKeyNullablePart";

		// Token: 0x0400079A RID: 1946
		internal const string EdmModel_Validator_Semantic_EntityKeyMustBeScalar = "EdmModel_Validator_Semantic_EntityKeyMustBeScalar";

		// Token: 0x0400079B RID: 1947
		internal const string EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass = "EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass";

		// Token: 0x0400079C RID: 1948
		internal const string EdmModel_Validator_Semantic_KeyMissingOnEntityType = "EdmModel_Validator_Semantic_KeyMissingOnEntityType";

		// Token: 0x0400079D RID: 1949
		internal const string EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole = "EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole";

		// Token: 0x0400079E RID: 1950
		internal const string EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame = "EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame";

		// Token: 0x0400079F RID: 1951
		internal const string EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType = "EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType";

		// Token: 0x040007A0 RID: 1952
		internal const string EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation = "EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation";

		// Token: 0x040007A1 RID: 1953
		internal const string EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified = "EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified";

		// Token: 0x040007A2 RID: 1954
		internal const string EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate = "EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate";

		// Token: 0x040007A3 RID: 1955
		internal const string EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint = "EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint";

		// Token: 0x040007A4 RID: 1956
		internal const string EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne = "EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne";

		// Token: 0x040007A5 RID: 1957
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable = "EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable";

		// Token: 0x040007A6 RID: 1958
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable = "EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable";

		// Token: 0x040007A7 RID: 1959
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne = "EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne";

		// Token: 0x040007A8 RID: 1960
		internal const string EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany = "EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany";

		// Token: 0x040007A9 RID: 1961
		internal const string EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint = "EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint";

		// Token: 0x040007AA RID: 1962
		internal const string EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint = "EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint";

		// Token: 0x040007AB RID: 1963
		internal const string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintDependentEnd = "EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintDependentEnd";

		// Token: 0x040007AC RID: 1964
		internal const string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintPrimaryEnd = "EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintPrimaryEnd";

		// Token: 0x040007AD RID: 1965
		internal const string EdmModel_Validator_Semantic_InvalidPropertyType = "EdmModel_Validator_Semantic_InvalidPropertyType";

		// Token: 0x040007AE RID: 1966
		internal const string EdmModel_Validator_Semantic_BoundOperationMustHaveParameters = "EdmModel_Validator_Semantic_BoundOperationMustHaveParameters";

		// Token: 0x040007AF RID: 1967
		internal const string EdmModel_Validator_Semantic_OperationWithUnsupportedReturnType = "EdmModel_Validator_Semantic_OperationWithUnsupportedReturnType";

		// Token: 0x040007B0 RID: 1968
		internal const string EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet = "EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet";

		// Token: 0x040007B1 RID: 1969
		internal const string EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet2 = "EdmModel_Validator_Semantic_OperationImportEntityTypeDoesNotMatchEntitySet2";

		// Token: 0x040007B2 RID: 1970
		internal const string EdmModel_Validator_Semantic_OperationImportEntitySetExpressionKindIsInvalid = "EdmModel_Validator_Semantic_OperationImportEntitySetExpressionKindIsInvalid";

		// Token: 0x040007B3 RID: 1971
		internal const string EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid = "EdmModel_Validator_Semantic_OperationImportEntitySetExpressionIsInvalid";

		// Token: 0x040007B4 RID: 1972
		internal const string EdmModel_Validator_Semantic_OperationImportSpecifiesEntitySetButNotEntityType = "EdmModel_Validator_Semantic_OperationImportSpecifiesEntitySetButNotEntityType";

		// Token: 0x040007B5 RID: 1973
		internal const string EdmModel_Validator_Semantic_OperationImportCannotImportBoundOperation = "EdmModel_Validator_Semantic_OperationImportCannotImportBoundOperation";

		// Token: 0x040007B6 RID: 1974
		internal const string EdmModel_Validator_Semantic_FunctionImportWithParameterShouldNotBeIncludedInServiceDocument = "EdmModel_Validator_Semantic_FunctionImportWithParameterShouldNotBeIncludedInServiceDocument";

		// Token: 0x040007B7 RID: 1975
		internal const string EdmModel_Validator_Semantic_FunctionMustHaveReturnType = "EdmModel_Validator_Semantic_FunctionMustHaveReturnType";

		// Token: 0x040007B8 RID: 1976
		internal const string EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate = "EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate";

		// Token: 0x040007B9 RID: 1977
		internal const string EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName = "EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName";

		// Token: 0x040007BA RID: 1978
		internal const string EdmModel_Validator_Semantic_UnboundFunctionOverloadHasIncorrectReturnType = "EdmModel_Validator_Semantic_UnboundFunctionOverloadHasIncorrectReturnType";

		// Token: 0x040007BB RID: 1979
		internal const string EdmModel_Validator_Semantic_OperationCannotHaveEntitySetPathWithUnBoundOperation = "EdmModel_Validator_Semantic_OperationCannotHaveEntitySetPathWithUnBoundOperation";

		// Token: 0x040007BC RID: 1980
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathMissingBindingParameterName = "EdmModel_Validator_Semantic_InvalidEntitySetPathMissingBindingParameterName";

		// Token: 0x040007BD RID: 1981
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathWithFirstPathParameterNotMatchingFirstParameterName = "EdmModel_Validator_Semantic_InvalidEntitySetPathWithFirstPathParameterNotMatchingFirstParameterName";

		// Token: 0x040007BE RID: 1982
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathTypeCastSegmentMustBeEntityType = "EdmModel_Validator_Semantic_InvalidEntitySetPathTypeCastSegmentMustBeEntityType";

		// Token: 0x040007BF RID: 1983
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownNavigationProperty = "EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownNavigationProperty";

		// Token: 0x040007C0 RID: 1984
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathInvalidTypeCastSegment = "EdmModel_Validator_Semantic_InvalidEntitySetPathInvalidTypeCastSegment";

		// Token: 0x040007C1 RID: 1985
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathWithNonEntityBindingParameter = "EdmModel_Validator_Semantic_InvalidEntitySetPathWithNonEntityBindingParameter";

		// Token: 0x040007C2 RID: 1986
		internal const string EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownTypeCastSegment = "EdmModel_Validator_Semantic_InvalidEntitySetPathUnknownTypeCastSegment";

		// Token: 0x040007C3 RID: 1987
		internal const string EdmModel_Validator_Semantic_OperationWithEntitySetPathReturnTypeInvalid = "EdmModel_Validator_Semantic_OperationWithEntitySetPathReturnTypeInvalid";

		// Token: 0x040007C4 RID: 1988
		internal const string EdmModel_Validator_Semantic_OperationWithEntitySetPathAndReturnTypeTypeNotAssignable = "EdmModel_Validator_Semantic_OperationWithEntitySetPathAndReturnTypeTypeNotAssignable";

		// Token: 0x040007C5 RID: 1989
		internal const string EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToEntityTypeMismatchesCollectionEntityTypeReturnType = "EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToEntityTypeMismatchesCollectionEntityTypeReturnType";

		// Token: 0x040007C6 RID: 1990
		internal const string EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType = "EdmModel_Validator_Semantic_OperationWithEntitySetPathResolvesToCollectionEntityTypeMismatchesEntityTypeReturnType";

		// Token: 0x040007C7 RID: 1991
		internal const string EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined = "EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined";

		// Token: 0x040007C8 RID: 1992
		internal const string EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName = "EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName";

		// Token: 0x040007C9 RID: 1993
		internal const string EdmModel_Validator_Semantic_PropertyNameAlreadyDefined = "EdmModel_Validator_Semantic_PropertyNameAlreadyDefined";

		// Token: 0x040007CA RID: 1994
		internal const string EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind = "EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind";

		// Token: 0x040007CB RID: 1995
		internal const string EdmModel_Validator_Semantic_BaseTypeOfOpenTypeMustBeOpen = "EdmModel_Validator_Semantic_BaseTypeOfOpenTypeMustBeOpen";

		// Token: 0x040007CC RID: 1996
		internal const string EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity = "EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity";

		// Token: 0x040007CD RID: 1997
		internal const string EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity = "EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity";

		// Token: 0x040007CE RID: 1998
		internal const string EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect = "EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect";

		// Token: 0x040007CF RID: 1999
		internal const string EdmModel_Validator_Semantic_InaccessibleType = "EdmModel_Validator_Semantic_InaccessibleType";

		// Token: 0x040007D0 RID: 2000
		internal const string EdmModel_Validator_Semantic_AmbiguousType = "EdmModel_Validator_Semantic_AmbiguousType";

		// Token: 0x040007D1 RID: 2001
		internal const string EdmModel_Validator_Semantic_InvalidNavigationPropertyType = "EdmModel_Validator_Semantic_InvalidNavigationPropertyType";

		// Token: 0x040007D2 RID: 2002
		internal const string EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional = "EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional";

		// Token: 0x040007D3 RID: 2003
		internal const string EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne = "EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne";

		// Token: 0x040007D4 RID: 2004
		internal const string EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne = "EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne";

		// Token: 0x040007D5 RID: 2005
		internal const string EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties = "EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties";

		// Token: 0x040007D6 RID: 2006
		internal const string EdmModel_Validator_Semantic_DuplicateDependentProperty = "EdmModel_Validator_Semantic_DuplicateDependentProperty";

		// Token: 0x040007D7 RID: 2007
		internal const string EdmModel_Validator_Semantic_ScaleOutOfRange = "EdmModel_Validator_Semantic_ScaleOutOfRange";

		// Token: 0x040007D8 RID: 2008
		internal const string EdmModel_Validator_Semantic_PrecisionOutOfRange = "EdmModel_Validator_Semantic_PrecisionOutOfRange";

		// Token: 0x040007D9 RID: 2009
		internal const string EdmModel_Validator_Semantic_StringMaxLengthOutOfRange = "EdmModel_Validator_Semantic_StringMaxLengthOutOfRange";

		// Token: 0x040007DA RID: 2010
		internal const string EdmModel_Validator_Semantic_MaxLengthOutOfRange = "EdmModel_Validator_Semantic_MaxLengthOutOfRange";

		// Token: 0x040007DB RID: 2011
		internal const string EdmModel_Validator_Semantic_InvalidPropertyTypeConcurrencyMode = "EdmModel_Validator_Semantic_InvalidPropertyTypeConcurrencyMode";

		// Token: 0x040007DC RID: 2012
		internal const string EdmModel_Validator_Semantic_EnumMemberTypeMustMatchEnumUnderlyingType = "EdmModel_Validator_Semantic_EnumMemberTypeMustMatchEnumUnderlyingType";

		// Token: 0x040007DD RID: 2013
		internal const string EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined = "EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined";

		// Token: 0x040007DE RID: 2014
		internal const string EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly = "EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly";

		// Token: 0x040007DF RID: 2015
		internal const string EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull = "EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull";

		// Token: 0x040007E0 RID: 2016
		internal const string EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm = "EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm";

		// Token: 0x040007E1 RID: 2017
		internal const string EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml = "EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml";

		// Token: 0x040007E2 RID: 2018
		internal const string EdmModel_Validator_Semantic_InvalidElementAnnotationNotIEdmStringValue = "EdmModel_Validator_Semantic_InvalidElementAnnotationNotIEdmStringValue";

		// Token: 0x040007E3 RID: 2019
		internal const string EdmModel_Validator_Semantic_InvalidElementAnnotationNullNamespaceOrName = "EdmModel_Validator_Semantic_InvalidElementAnnotationNullNamespaceOrName";

		// Token: 0x040007E4 RID: 2020
		internal const string EdmModel_Validator_Semantic_CannotAssertNullableTypeAsNonNullableType = "EdmModel_Validator_Semantic_CannotAssertNullableTypeAsNonNullableType";

		// Token: 0x040007E5 RID: 2021
		internal const string EdmModel_Validator_Semantic_ExpressionPrimitiveKindCannotPromoteToAssertedType = "EdmModel_Validator_Semantic_ExpressionPrimitiveKindCannotPromoteToAssertedType";

		// Token: 0x040007E6 RID: 2022
		internal const string EdmModel_Validator_Semantic_NullCannotBeAssertedToBeANonNullableType = "EdmModel_Validator_Semantic_NullCannotBeAssertedToBeANonNullableType";

		// Token: 0x040007E7 RID: 2023
		internal const string EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType = "EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType";

		// Token: 0x040007E8 RID: 2024
		internal const string EdmModel_Validator_Semantic_CollectionExpressionNotValidForNonCollectionType = "EdmModel_Validator_Semantic_CollectionExpressionNotValidForNonCollectionType";

		// Token: 0x040007E9 RID: 2025
		internal const string EdmModel_Validator_Semantic_PrimitiveConstantExpressionNotValidForNonPrimitiveType = "EdmModel_Validator_Semantic_PrimitiveConstantExpressionNotValidForNonPrimitiveType";

		// Token: 0x040007EA RID: 2026
		internal const string EdmModel_Validator_Semantic_RecordExpressionNotValidForNonStructuredType = "EdmModel_Validator_Semantic_RecordExpressionNotValidForNonStructuredType";

		// Token: 0x040007EB RID: 2027
		internal const string EdmModel_Validator_Semantic_RecordExpressionMissingProperty = "EdmModel_Validator_Semantic_RecordExpressionMissingProperty";

		// Token: 0x040007EC RID: 2028
		internal const string EdmModel_Validator_Semantic_RecordExpressionHasExtraProperties = "EdmModel_Validator_Semantic_RecordExpressionHasExtraProperties";

		// Token: 0x040007ED RID: 2029
		internal const string EdmModel_Validator_Semantic_DuplicateAnnotation = "EdmModel_Validator_Semantic_DuplicateAnnotation";

		// Token: 0x040007EE RID: 2030
		internal const string EdmModel_Validator_Semantic_IncorrectNumberOfArguments = "EdmModel_Validator_Semantic_IncorrectNumberOfArguments";

		// Token: 0x040007EF RID: 2031
		internal const string EdmModel_Validator_Semantic_DuplicateEntityContainerName = "EdmModel_Validator_Semantic_DuplicateEntityContainerName";

		// Token: 0x040007F0 RID: 2032
		internal const string EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType = "EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType";

		// Token: 0x040007F1 RID: 2033
		internal const string EdmModel_Validator_Semantic_IntegerConstantValueOutOfRange = "EdmModel_Validator_Semantic_IntegerConstantValueOutOfRange";

		// Token: 0x040007F2 RID: 2034
		internal const string EdmModel_Validator_Semantic_StringConstantLengthOutOfRange = "EdmModel_Validator_Semantic_StringConstantLengthOutOfRange";

		// Token: 0x040007F3 RID: 2035
		internal const string EdmModel_Validator_Semantic_BinaryConstantLengthOutOfRange = "EdmModel_Validator_Semantic_BinaryConstantLengthOutOfRange";

		// Token: 0x040007F4 RID: 2036
		internal const string EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone = "EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone";

		// Token: 0x040007F5 RID: 2037
		internal const string EdmModel_Validator_Semantic_TermMustNotHaveKindOfNone = "EdmModel_Validator_Semantic_TermMustNotHaveKindOfNone";

		// Token: 0x040007F6 RID: 2038
		internal const string EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone = "EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone";

		// Token: 0x040007F7 RID: 2039
		internal const string EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone = "EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone";

		// Token: 0x040007F8 RID: 2040
		internal const string EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone = "EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone";

		// Token: 0x040007F9 RID: 2041
		internal const string EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone = "EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone";

		// Token: 0x040007FA RID: 2042
		internal const string EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping = "EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping";

		// Token: 0x040007FB RID: 2043
		internal const string EdmModel_Validator_Semantic_NavigationMappingMustBeBidirectional = "EdmModel_Validator_Semantic_NavigationMappingMustBeBidirectional";

		// Token: 0x040007FC RID: 2044
		internal const string EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty = "EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty";

		// Token: 0x040007FD RID: 2045
		internal const string EdmModel_Validator_Semantic_TypeAnnotationMissingRequiredProperty = "EdmModel_Validator_Semantic_TypeAnnotationMissingRequiredProperty";

		// Token: 0x040007FE RID: 2046
		internal const string EdmModel_Validator_Semantic_TypeAnnotationHasExtraProperties = "EdmModel_Validator_Semantic_TypeAnnotationHasExtraProperties";

		// Token: 0x040007FF RID: 2047
		internal const string EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType = "EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType";

		// Token: 0x04000800 RID: 2048
		internal const string EdmModel_Validator_Semantic_InaccessibleTerm = "EdmModel_Validator_Semantic_InaccessibleTerm";

		// Token: 0x04000801 RID: 2049
		internal const string EdmModel_Validator_Semantic_InaccessibleTarget = "EdmModel_Validator_Semantic_InaccessibleTarget";

		// Token: 0x04000802 RID: 2050
		internal const string EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique = "EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique";

		// Token: 0x04000803 RID: 2051
		internal const string EdmModel_Validator_Semantic_NoEntitySetsFoundForType = "EdmModel_Validator_Semantic_NoEntitySetsFoundForType";

		// Token: 0x04000804 RID: 2052
		internal const string EdmModel_Validator_Semantic_CannotInferEntitySetWithMultipleSetsPerType = "EdmModel_Validator_Semantic_CannotInferEntitySetWithMultipleSetsPerType";

		// Token: 0x04000805 RID: 2053
		internal const string EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet = "EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet";

		// Token: 0x04000806 RID: 2054
		internal const string EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself = "EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself";

		// Token: 0x04000807 RID: 2055
		internal const string EdmModel_Validator_Semantic_PathIsNotValidForTheGivenContext = "EdmModel_Validator_Semantic_PathIsNotValidForTheGivenContext";

		// Token: 0x04000808 RID: 2056
		internal const string EdmModel_Validator_Semantic_NavigationPropertyMappingMustPointToValidTargetForProperty = "EdmModel_Validator_Semantic_NavigationPropertyMappingMustPointToValidTargetForProperty";

		// Token: 0x04000809 RID: 2057
		internal const string EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterNames = "EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterNames";

		// Token: 0x0400080A RID: 2058
		internal const string EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterTypes = "EdmModel_Validator_Semantic_ModelDuplicateBoundFunctionParameterTypes";

		// Token: 0x0400080B RID: 2059
		internal const string EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterNames = "EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterNames";

		// Token: 0x0400080C RID: 2060
		internal const string EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterTypes = "EdmModel_Validator_Semantic_ModelDuplicateUnBoundFunctionsParameterTypes";

		// Token: 0x0400080D RID: 2061
		internal const string EdmModel_Validator_Semantic_ModelDuplicateBoundActions = "EdmModel_Validator_Semantic_ModelDuplicateBoundActions";

		// Token: 0x0400080E RID: 2062
		internal const string EdmModel_Validator_Semantic_ModelDuplicateUnBoundActions = "EdmModel_Validator_Semantic_ModelDuplicateUnBoundActions";

		// Token: 0x0400080F RID: 2063
		internal const string EdmModel_Validator_Semantic_BoundFunctionOverloadsMustHaveSameReturnType = "EdmModel_Validator_Semantic_BoundFunctionOverloadsMustHaveSameReturnType";

		// Token: 0x04000810 RID: 2064
		internal const string EdmModel_Validator_Semantic_EntitySetTypeMustBeCollectionOfEntityType = "EdmModel_Validator_Semantic_EntitySetTypeMustBeCollectionOfEntityType";

		// Token: 0x04000811 RID: 2065
		internal const string EdmModel_Validator_Semantic_SingletonTypeMustBeEntityType = "EdmModel_Validator_Semantic_SingletonTypeMustBeEntityType";

		// Token: 0x04000812 RID: 2066
		internal const string EdmModel_Validator_Semantic_NavigationPropertyOfCollectionTypeMustNotTargetToSingleton = "EdmModel_Validator_Semantic_NavigationPropertyOfCollectionTypeMustNotTargetToSingleton";

		// Token: 0x04000813 RID: 2067
		internal const string EdmModel_Validator_Syntactic_MissingName = "EdmModel_Validator_Syntactic_MissingName";

		// Token: 0x04000814 RID: 2068
		internal const string EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong = "EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong";

		// Token: 0x04000815 RID: 2069
		internal const string EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed = "EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed";

		// Token: 0x04000816 RID: 2070
		internal const string EdmModel_Validator_Syntactic_MissingNamespaceName = "EdmModel_Validator_Syntactic_MissingNamespaceName";

		// Token: 0x04000817 RID: 2071
		internal const string EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong = "EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong";

		// Token: 0x04000818 RID: 2072
		internal const string EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed = "EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed";

		// Token: 0x04000819 RID: 2073
		internal const string EdmModel_Validator_Syntactic_PropertyMustNotBeNull = "EdmModel_Validator_Syntactic_PropertyMustNotBeNull";

		// Token: 0x0400081A RID: 2074
		internal const string EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange = "EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange";

		// Token: 0x0400081B RID: 2075
		internal const string EdmModel_Validator_Syntactic_InterfaceKindValueMismatch = "EdmModel_Validator_Syntactic_InterfaceKindValueMismatch";

		// Token: 0x0400081C RID: 2076
		internal const string EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch = "EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch";

		// Token: 0x0400081D RID: 2077
		internal const string EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected = "EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected";

		// Token: 0x0400081E RID: 2078
		internal const string EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements = "EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements";

		// Token: 0x0400081F RID: 2079
		internal const string EdmModel_Validator_Syntactic_NavigationPartnerInvalid = "EdmModel_Validator_Syntactic_NavigationPartnerInvalid";

		// Token: 0x04000820 RID: 2080
		internal const string EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy = "EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy";

		// Token: 0x04000821 RID: 2081
		internal const string Serializer_SingleFileExpected = "Serializer_SingleFileExpected";

		// Token: 0x04000822 RID: 2082
		internal const string Serializer_UnknownEdmVersion = "Serializer_UnknownEdmVersion";

		// Token: 0x04000823 RID: 2083
		internal const string Serializer_UnknownEdmxVersion = "Serializer_UnknownEdmxVersion";

		// Token: 0x04000824 RID: 2084
		internal const string Serializer_NonInlineOperationImportReturnType = "Serializer_NonInlineOperationImportReturnType";

		// Token: 0x04000825 RID: 2085
		internal const string Serializer_ReferencedTypeMustHaveValidName = "Serializer_ReferencedTypeMustHaveValidName";

		// Token: 0x04000826 RID: 2086
		internal const string Serializer_OutOfLineAnnotationTargetMustHaveValidName = "Serializer_OutOfLineAnnotationTargetMustHaveValidName";

		// Token: 0x04000827 RID: 2087
		internal const string Serializer_NoSchemasProduced = "Serializer_NoSchemasProduced";

		// Token: 0x04000828 RID: 2088
		internal const string XmlParser_EmptyFile = "XmlParser_EmptyFile";

		// Token: 0x04000829 RID: 2089
		internal const string XmlParser_EmptySchemaTextReader = "XmlParser_EmptySchemaTextReader";

		// Token: 0x0400082A RID: 2090
		internal const string XmlParser_MissingAttribute = "XmlParser_MissingAttribute";

		// Token: 0x0400082B RID: 2091
		internal const string XmlParser_TextNotAllowed = "XmlParser_TextNotAllowed";

		// Token: 0x0400082C RID: 2092
		internal const string XmlParser_UnexpectedAttribute = "XmlParser_UnexpectedAttribute";

		// Token: 0x0400082D RID: 2093
		internal const string XmlParser_UnexpectedElement = "XmlParser_UnexpectedElement";

		// Token: 0x0400082E RID: 2094
		internal const string XmlParser_UnusedElement = "XmlParser_UnusedElement";

		// Token: 0x0400082F RID: 2095
		internal const string XmlParser_UnexpectedNodeType = "XmlParser_UnexpectedNodeType";

		// Token: 0x04000830 RID: 2096
		internal const string XmlParser_UnexpectedRootElement = "XmlParser_UnexpectedRootElement";

		// Token: 0x04000831 RID: 2097
		internal const string XmlParser_UnexpectedRootElementWrongNamespace = "XmlParser_UnexpectedRootElementWrongNamespace";

		// Token: 0x04000832 RID: 2098
		internal const string XmlParser_UnexpectedRootElementNoNamespace = "XmlParser_UnexpectedRootElementNoNamespace";

		// Token: 0x04000833 RID: 2099
		internal const string CsdlParser_InvalidEntitySetPathWithUnboundAction = "CsdlParser_InvalidEntitySetPathWithUnboundAction";

		// Token: 0x04000834 RID: 2100
		internal const string CsdlParser_InvalidAlias = "CsdlParser_InvalidAlias";

		// Token: 0x04000835 RID: 2101
		internal const string CsdlParser_InvalidDeleteAction = "CsdlParser_InvalidDeleteAction";

		// Token: 0x04000836 RID: 2102
		internal const string CsdlParser_MissingTypeAttributeOrElement = "CsdlParser_MissingTypeAttributeOrElement";

		// Token: 0x04000837 RID: 2103
		internal const string CsdlParser_InvalidConcurrencyMode = "CsdlParser_InvalidConcurrencyMode";

		// Token: 0x04000838 RID: 2104
		internal const string CsdlParser_InvalidEndRoleInRelationshipConstraint = "CsdlParser_InvalidEndRoleInRelationshipConstraint";

		// Token: 0x04000839 RID: 2105
		internal const string CsdlParser_InvalidMultiplicity = "CsdlParser_InvalidMultiplicity";

		// Token: 0x0400083A RID: 2106
		internal const string CsdlParser_ReferentialConstraintRequiresOneDependent = "CsdlParser_ReferentialConstraintRequiresOneDependent";

		// Token: 0x0400083B RID: 2107
		internal const string CsdlParser_ReferentialConstraintRequiresOnePrincipal = "CsdlParser_ReferentialConstraintRequiresOnePrincipal";

		// Token: 0x0400083C RID: 2108
		internal const string CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands = "CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands";

		// Token: 0x0400083D RID: 2109
		internal const string CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands = "CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands";

		// Token: 0x0400083E RID: 2110
		internal const string CsdlParser_InvalidCastExpressionIncorrectNumberOfOperands = "CsdlParser_InvalidCastExpressionIncorrectNumberOfOperands";

		// Token: 0x0400083F RID: 2111
		internal const string CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands = "CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands";

		// Token: 0x04000840 RID: 2112
		internal const string CsdlParser_InvalidTypeName = "CsdlParser_InvalidTypeName";

		// Token: 0x04000841 RID: 2113
		internal const string CsdlParser_InvalidQualifiedName = "CsdlParser_InvalidQualifiedName";

		// Token: 0x04000842 RID: 2114
		internal const string CsdlParser_NoReadersProvided = "CsdlParser_NoReadersProvided";

		// Token: 0x04000843 RID: 2115
		internal const string CsdlParser_NullXmlReader = "CsdlParser_NullXmlReader";

		// Token: 0x04000844 RID: 2116
		internal const string CsdlParser_InvalidEntitySetPath = "CsdlParser_InvalidEntitySetPath";

		// Token: 0x04000845 RID: 2117
		internal const string CsdlParser_InvalidEnumMemberPath = "CsdlParser_InvalidEnumMemberPath";

		// Token: 0x04000846 RID: 2118
		internal const string CsdlParser_CannotSpecifyNullableAttributeForNavigationPropertyWithCollectionType = "CsdlParser_CannotSpecifyNullableAttributeForNavigationPropertyWithCollectionType";

		// Token: 0x04000847 RID: 2119
		internal const string CsdlParser_MetadataDocumentCannotHaveMoreThanOneEntityContainer = "CsdlParser_MetadataDocumentCannotHaveMoreThanOneEntityContainer";

		// Token: 0x04000848 RID: 2120
		internal const string CsdlSemantics_ReferentialConstraintMismatch = "CsdlSemantics_ReferentialConstraintMismatch";

		// Token: 0x04000849 RID: 2121
		internal const string CsdlSemantics_EnumMemberValueOutOfRange = "CsdlSemantics_EnumMemberValueOutOfRange";

		// Token: 0x0400084A RID: 2122
		internal const string CsdlSemantics_ImpossibleAnnotationsTarget = "CsdlSemantics_ImpossibleAnnotationsTarget";

		// Token: 0x0400084B RID: 2123
		internal const string CsdlSemantics_DuplicateAlias = "CsdlSemantics_DuplicateAlias";

		// Token: 0x0400084C RID: 2124
		internal const string EdmxParser_EdmxVersionMismatch = "EdmxParser_EdmxVersionMismatch";

		// Token: 0x0400084D RID: 2125
		internal const string EdmxParser_BodyElement = "EdmxParser_BodyElement";

		// Token: 0x0400084E RID: 2126
		internal const string EdmxParser_InvalidReferenceIncorrectNumberOfIncludes = "EdmxParser_InvalidReferenceIncorrectNumberOfIncludes";

		// Token: 0x0400084F RID: 2127
		internal const string EdmxParser_UnresolvedReferenceUriInEdmxReference = "EdmxParser_UnresolvedReferenceUriInEdmxReference";

		// Token: 0x04000850 RID: 2128
		internal const string EdmParseException_ErrorsEncounteredInEdmx = "EdmParseException_ErrorsEncounteredInEdmx";

		// Token: 0x04000851 RID: 2129
		internal const string ValueParser_InvalidBoolean = "ValueParser_InvalidBoolean";

		// Token: 0x04000852 RID: 2130
		internal const string ValueParser_InvalidInteger = "ValueParser_InvalidInteger";

		// Token: 0x04000853 RID: 2131
		internal const string ValueParser_InvalidLong = "ValueParser_InvalidLong";

		// Token: 0x04000854 RID: 2132
		internal const string ValueParser_InvalidFloatingPoint = "ValueParser_InvalidFloatingPoint";

		// Token: 0x04000855 RID: 2133
		internal const string ValueParser_InvalidMaxLength = "ValueParser_InvalidMaxLength";

		// Token: 0x04000856 RID: 2134
		internal const string ValueParser_InvalidSrid = "ValueParser_InvalidSrid";

		// Token: 0x04000857 RID: 2135
		internal const string ValueParser_InvalidScale = "ValueParser_InvalidScale";

		// Token: 0x04000858 RID: 2136
		internal const string ValueParser_InvalidGuid = "ValueParser_InvalidGuid";

		// Token: 0x04000859 RID: 2137
		internal const string ValueParser_InvalidDecimal = "ValueParser_InvalidDecimal";

		// Token: 0x0400085A RID: 2138
		internal const string ValueParser_InvalidDateTimeOffset = "ValueParser_InvalidDateTimeOffset";

		// Token: 0x0400085B RID: 2139
		internal const string ValueParser_InvalidDateTime = "ValueParser_InvalidDateTime";

		// Token: 0x0400085C RID: 2140
		internal const string ValueParser_InvalidDate = "ValueParser_InvalidDate";

		// Token: 0x0400085D RID: 2141
		internal const string ValueParser_InvalidDuration = "ValueParser_InvalidDuration";

		// Token: 0x0400085E RID: 2142
		internal const string ValueParser_InvalidBinary = "ValueParser_InvalidBinary";

		// Token: 0x0400085F RID: 2143
		internal const string ValueParser_InvalidTimeOfDay = "ValueParser_InvalidTimeOfDay";

		// Token: 0x04000860 RID: 2144
		internal const string UnknownEnumVal_Multiplicity = "UnknownEnumVal_Multiplicity";

		// Token: 0x04000861 RID: 2145
		internal const string UnknownEnumVal_SchemaElementKind = "UnknownEnumVal_SchemaElementKind";

		// Token: 0x04000862 RID: 2146
		internal const string UnknownEnumVal_TypeKind = "UnknownEnumVal_TypeKind";

		// Token: 0x04000863 RID: 2147
		internal const string UnknownEnumVal_PrimitiveKind = "UnknownEnumVal_PrimitiveKind";

		// Token: 0x04000864 RID: 2148
		internal const string UnknownEnumVal_ContainerElementKind = "UnknownEnumVal_ContainerElementKind";

		// Token: 0x04000865 RID: 2149
		internal const string UnknownEnumVal_EdmxTarget = "UnknownEnumVal_EdmxTarget";

		// Token: 0x04000866 RID: 2150
		internal const string UnknownEnumVal_ConcurrencyMode = "UnknownEnumVal_ConcurrencyMode";

		// Token: 0x04000867 RID: 2151
		internal const string UnknownEnumVal_PropertyKind = "UnknownEnumVal_PropertyKind";

		// Token: 0x04000868 RID: 2152
		internal const string UnknownEnumVal_TermKind = "UnknownEnumVal_TermKind";

		// Token: 0x04000869 RID: 2153
		internal const string UnknownEnumVal_ExpressionKind = "UnknownEnumVal_ExpressionKind";

		// Token: 0x0400086A RID: 2154
		internal const string Bad_AmbiguousElementBinding = "Bad_AmbiguousElementBinding";

		// Token: 0x0400086B RID: 2155
		internal const string Bad_UnresolvedType = "Bad_UnresolvedType";

		// Token: 0x0400086C RID: 2156
		internal const string Bad_UnresolvedComplexType = "Bad_UnresolvedComplexType";

		// Token: 0x0400086D RID: 2157
		internal const string Bad_UnresolvedEntityType = "Bad_UnresolvedEntityType";

		// Token: 0x0400086E RID: 2158
		internal const string Bad_UnresolvedPrimitiveType = "Bad_UnresolvedPrimitiveType";

		// Token: 0x0400086F RID: 2159
		internal const string Bad_UnresolvedOperation = "Bad_UnresolvedOperation";

		// Token: 0x04000870 RID: 2160
		internal const string Bad_AmbiguousOperation = "Bad_AmbiguousOperation";

		// Token: 0x04000871 RID: 2161
		internal const string Bad_OperationParametersDontMatch = "Bad_OperationParametersDontMatch";

		// Token: 0x04000872 RID: 2162
		internal const string Bad_UnresolvedEntitySet = "Bad_UnresolvedEntitySet";

		// Token: 0x04000873 RID: 2163
		internal const string Bad_UnresolvedEntityContainer = "Bad_UnresolvedEntityContainer";

		// Token: 0x04000874 RID: 2164
		internal const string Bad_UnresolvedEnumType = "Bad_UnresolvedEnumType";

		// Token: 0x04000875 RID: 2165
		internal const string Bad_UnresolvedEnumMember = "Bad_UnresolvedEnumMember";

		// Token: 0x04000876 RID: 2166
		internal const string Bad_UnresolvedProperty = "Bad_UnresolvedProperty";

		// Token: 0x04000877 RID: 2167
		internal const string Bad_UnresolvedParameter = "Bad_UnresolvedParameter";

		// Token: 0x04000878 RID: 2168
		internal const string Bad_UnresolvedLabeledElement = "Bad_UnresolvedLabeledElement";

		// Token: 0x04000879 RID: 2169
		internal const string Bad_CyclicEntity = "Bad_CyclicEntity";

		// Token: 0x0400087A RID: 2170
		internal const string Bad_CyclicComplex = "Bad_CyclicComplex";

		// Token: 0x0400087B RID: 2171
		internal const string Bad_CyclicEntityContainer = "Bad_CyclicEntityContainer";

		// Token: 0x0400087C RID: 2172
		internal const string Bad_UnresolvedNavigationPropertyPath = "Bad_UnresolvedNavigationPropertyPath";

		// Token: 0x0400087D RID: 2173
		internal const string RuleSet_DuplicateRulesExistInRuleSet = "RuleSet_DuplicateRulesExistInRuleSet";

		// Token: 0x0400087E RID: 2174
		internal const string EdmToClr_UnsupportedTypeCode = "EdmToClr_UnsupportedTypeCode";

		// Token: 0x0400087F RID: 2175
		internal const string EdmToClr_StructuredValueMappedToNonClass = "EdmToClr_StructuredValueMappedToNonClass";

		// Token: 0x04000880 RID: 2176
		internal const string EdmToClr_IEnumerableOfTPropertyAlreadyHasValue = "EdmToClr_IEnumerableOfTPropertyAlreadyHasValue";

		// Token: 0x04000881 RID: 2177
		internal const string EdmToClr_StructuredPropertyDuplicateValue = "EdmToClr_StructuredPropertyDuplicateValue";

		// Token: 0x04000882 RID: 2178
		internal const string EdmToClr_CannotConvertEdmValueToClrType = "EdmToClr_CannotConvertEdmValueToClrType";

		// Token: 0x04000883 RID: 2179
		internal const string EdmToClr_CannotConvertEdmCollectionValueToClrType = "EdmToClr_CannotConvertEdmCollectionValueToClrType";

		// Token: 0x04000884 RID: 2180
		internal const string EdmToClr_TryCreateObjectInstanceReturnedWrongObject = "EdmToClr_TryCreateObjectInstanceReturnedWrongObject";

		// Token: 0x04000885 RID: 2181
		internal const string EdmUtil_NullValueForMimeTypeAnnotation = "EdmUtil_NullValueForMimeTypeAnnotation";

		// Token: 0x04000886 RID: 2182
		internal const string EdmUtil_InvalidAnnotationValue = "EdmUtil_InvalidAnnotationValue";

		// Token: 0x04000887 RID: 2183
		internal const string PlatformHelper_DateTimeOffsetMustContainTimeZone = "PlatformHelper_DateTimeOffsetMustContainTimeZone";

		// Token: 0x04000888 RID: 2184
		internal const string Date_InvalidAddedOrSubtractedResults = "Date_InvalidAddedOrSubtractedResults";

		// Token: 0x04000889 RID: 2185
		internal const string Date_InvalidDateParameters = "Date_InvalidDateParameters";

		// Token: 0x0400088A RID: 2186
		internal const string Date_InvalidParsingString = "Date_InvalidParsingString";

		// Token: 0x0400088B RID: 2187
		internal const string Date_InvalidCompareToTarget = "Date_InvalidCompareToTarget";

		// Token: 0x0400088C RID: 2188
		internal const string TimeOfDay_InvalidTimeOfDayParameters = "TimeOfDay_InvalidTimeOfDayParameters";

		// Token: 0x0400088D RID: 2189
		internal const string TimeOfDay_TicksOutOfRange = "TimeOfDay_TicksOutOfRange";

		// Token: 0x0400088E RID: 2190
		internal const string TimeOfDay_ConvertErrorFromTimeSpan = "TimeOfDay_ConvertErrorFromTimeSpan";

		// Token: 0x0400088F RID: 2191
		internal const string TimeOfDay_InvalidParsingString = "TimeOfDay_InvalidParsingString";

		// Token: 0x04000890 RID: 2192
		internal const string TimeOfDay_InvalidCompareToTarget = "TimeOfDay_InvalidCompareToTarget";

		// Token: 0x04000891 RID: 2193
		private static EntityRes loader;

		// Token: 0x04000892 RID: 2194
		private ResourceManager resources;
	}
}
