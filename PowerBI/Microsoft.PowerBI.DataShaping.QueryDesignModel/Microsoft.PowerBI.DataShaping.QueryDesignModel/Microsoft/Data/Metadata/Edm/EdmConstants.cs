using System;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200007F RID: 127
	internal static class EdmConstants
	{
		// Token: 0x0400076D RID: 1901
		internal const string EdmNamespace = "Edm";

		// Token: 0x0400076E RID: 1902
		internal const string ClrPrimitiveTypeNamespace = "System";

		// Token: 0x0400076F RID: 1903
		internal const string TransientNamespace = "Transient";

		// Token: 0x04000770 RID: 1904
		internal const int NumPrimitiveTypes = 15;

		// Token: 0x04000771 RID: 1905
		internal const int NumBuiltInTypes = 40;

		// Token: 0x04000772 RID: 1906
		internal const int MaxLength = 256;

		// Token: 0x04000773 RID: 1907
		internal const string AssociationEnd = "AssociationEnd";

		// Token: 0x04000774 RID: 1908
		internal const string AssociationSetType = "AssocationSetType";

		// Token: 0x04000775 RID: 1909
		internal const string AssociationSetEndType = "AssociationSetEndType";

		// Token: 0x04000776 RID: 1910
		internal const string AssociationType = "AssociationType";

		// Token: 0x04000777 RID: 1911
		internal const string BaseEntitySetType = "BaseEntitySetType";

		// Token: 0x04000778 RID: 1912
		internal const string CollectionType = "CollectionType";

		// Token: 0x04000779 RID: 1913
		internal const string ComplexType = "ComplexType";

		// Token: 0x0400077A RID: 1914
		internal const string DeleteAction = "DeleteAction";

		// Token: 0x0400077B RID: 1915
		internal const string DeleteBehavior = "DeleteBehavior";

		// Token: 0x0400077C RID: 1916
		internal const string Documentation = "Documentation";

		// Token: 0x0400077D RID: 1917
		internal const string EdmType = "EdmType";

		// Token: 0x0400077E RID: 1918
		internal const string ElementType = "ElementType";

		// Token: 0x0400077F RID: 1919
		internal const string EntityContainerType = "EntityContainerType";

		// Token: 0x04000780 RID: 1920
		internal const string EntitySetType = "EntitySetType";

		// Token: 0x04000781 RID: 1921
		internal const string EntityType = "EntityType";

		// Token: 0x04000782 RID: 1922
		internal const string EnumerationMember = "EnumMember";

		// Token: 0x04000783 RID: 1923
		internal const string EnumerationType = "EnumType";

		// Token: 0x04000784 RID: 1924
		internal const string Facet = "Facet";

		// Token: 0x04000785 RID: 1925
		internal const string Function = "EdmFunction";

		// Token: 0x04000786 RID: 1926
		internal const string FunctionParameter = "FunctionParameter";

		// Token: 0x04000787 RID: 1927
		internal const string GlobalItem = "GlobalItem";

		// Token: 0x04000788 RID: 1928
		internal const string ItemAttribute = "MetadataProperty";

		// Token: 0x04000789 RID: 1929
		internal const string ItemType = "ItemType";

		// Token: 0x0400078A RID: 1930
		internal const string Member = "EdmMember";

		// Token: 0x0400078B RID: 1931
		internal const string NavigationProperty = "NavigationProperty";

		// Token: 0x0400078C RID: 1932
		internal const string OperationBehavior = "OperationBehavior";

		// Token: 0x0400078D RID: 1933
		internal const string OperationBehaviors = "OperationBehaviors";

		// Token: 0x0400078E RID: 1934
		internal const string ParameterMode = "ParameterMode";

		// Token: 0x0400078F RID: 1935
		internal const string PrimitiveType = "PrimitiveType";

		// Token: 0x04000790 RID: 1936
		internal const string PrimitiveTypeKind = "PrimitiveTypeKind";

		// Token: 0x04000791 RID: 1937
		internal const string Property = "EdmProperty";

		// Token: 0x04000792 RID: 1938
		internal const string ProviderManifest = "ProviderManifest";

		// Token: 0x04000793 RID: 1939
		internal const string ReferentialConstraint = "ReferentialConstraint";

		// Token: 0x04000794 RID: 1940
		internal const string RefType = "RefType";

		// Token: 0x04000795 RID: 1941
		internal const string RelationshipEnd = "RelationshipEnd";

		// Token: 0x04000796 RID: 1942
		internal const string RelationshipMultiplicity = "RelationshipMultiplicity";

		// Token: 0x04000797 RID: 1943
		internal const string RelationshipSet = "RelationshipSet";

		// Token: 0x04000798 RID: 1944
		internal const string RelationshipType = "RelationshipType";

		// Token: 0x04000799 RID: 1945
		internal const string ReturnParameter = "ReturnParameter";

		// Token: 0x0400079A RID: 1946
		internal const string Role = "Role";

		// Token: 0x0400079B RID: 1947
		internal const string RowType = "RowType";

		// Token: 0x0400079C RID: 1948
		internal const string SimpleType = "SimpleType";

		// Token: 0x0400079D RID: 1949
		internal const string StructuralType = "StructuralType";

		// Token: 0x0400079E RID: 1950
		internal const string TypeUsage = "TypeUsage";

		// Token: 0x0400079F RID: 1951
		internal const string Utc = "Utc";

		// Token: 0x040007A0 RID: 1952
		internal const string Unspecified = "Unspecified";

		// Token: 0x040007A1 RID: 1953
		internal const string Local = "Local";

		// Token: 0x040007A2 RID: 1954
		internal const string One = "One";

		// Token: 0x040007A3 RID: 1955
		internal const string ZeroToOne = "ZeroToOne";

		// Token: 0x040007A4 RID: 1956
		internal const string Many = "Many";

		// Token: 0x040007A5 RID: 1957
		internal const string In = "In";

		// Token: 0x040007A6 RID: 1958
		internal const string Out = "Out";

		// Token: 0x040007A7 RID: 1959
		internal const string InOut = "InOut";

		// Token: 0x040007A8 RID: 1960
		internal const string None = "None";

		// Token: 0x040007A9 RID: 1961
		internal const string Cascade = "Cascade";

		// Token: 0x040007AA RID: 1962
		internal const string Restrict = "Restrict";

		// Token: 0x040007AB RID: 1963
		internal const string NoneCollectionKind = "None";

		// Token: 0x040007AC RID: 1964
		internal const string ListCollectionKind = "List";

		// Token: 0x040007AD RID: 1965
		internal const string BagCollectionKind = "Bag";

		// Token: 0x040007AE RID: 1966
		internal const string MaxMaxLength = "Max";

		// Token: 0x040007AF RID: 1967
		internal const string AssociationSetEnds = "AssociationSetEnds";

		// Token: 0x040007B0 RID: 1968
		internal const string Child = "Child";

		// Token: 0x040007B1 RID: 1969
		internal const string DefaultValue = "DefaultValue";

		// Token: 0x040007B2 RID: 1970
		internal const string Ends = "Ends";

		// Token: 0x040007B3 RID: 1971
		internal const string EntitySet = "EntitySet";

		// Token: 0x040007B4 RID: 1972
		internal const string AssociationSet = "AssociationSet";

		// Token: 0x040007B5 RID: 1973
		internal const string EntitySets = "EntitySets";

		// Token: 0x040007B6 RID: 1974
		internal const string Facets = "Facets";

		// Token: 0x040007B7 RID: 1975
		internal const string FromProperties = "FromProperties";

		// Token: 0x040007B8 RID: 1976
		internal const string FromRole = "FromRole";

		// Token: 0x040007B9 RID: 1977
		internal const string IsParent = "IsParent";

		// Token: 0x040007BA RID: 1978
		internal const string KeyMembers = "KeyMembers";

		// Token: 0x040007BB RID: 1979
		internal const string Members = "Members";

		// Token: 0x040007BC RID: 1980
		internal const string Mode = "Mode";

		// Token: 0x040007BD RID: 1981
		internal const string Nullable = "Nullable";

		// Token: 0x040007BE RID: 1982
		internal const string Parameters = "Parameters";

		// Token: 0x040007BF RID: 1983
		internal const string Parent = "Parent";

		// Token: 0x040007C0 RID: 1984
		internal const string Properties = "Properties";

		// Token: 0x040007C1 RID: 1985
		internal const string ToProperties = "ToProperties";

		// Token: 0x040007C2 RID: 1986
		internal const string ToRole = "ToRole";

		// Token: 0x040007C3 RID: 1987
		internal const string ReferentialConstraints = "ReferentialConstraints";

		// Token: 0x040007C4 RID: 1988
		internal const string RelationshipTypeName = "RelationshipTypeName";

		// Token: 0x040007C5 RID: 1989
		internal const string ReturnType = "ReturnType";

		// Token: 0x040007C6 RID: 1990
		internal const string ToEndMemberName = "ToEndMemberName";

		// Token: 0x040007C7 RID: 1991
		internal const string CollectionKind = "CollectionKind";

		// Token: 0x040007C8 RID: 1992
		internal const string Binary = "Binary";

		// Token: 0x040007C9 RID: 1993
		internal const string Boolean = "Boolean";

		// Token: 0x040007CA RID: 1994
		internal const string Byte = "Byte";

		// Token: 0x040007CB RID: 1995
		internal const string DateTime = "DateTime";

		// Token: 0x040007CC RID: 1996
		internal const string Decimal = "Decimal";

		// Token: 0x040007CD RID: 1997
		internal const string Double = "Double";

		// Token: 0x040007CE RID: 1998
		internal const string Guid = "Guid";

		// Token: 0x040007CF RID: 1999
		internal const string Single = "Single";

		// Token: 0x040007D0 RID: 2000
		internal const string SByte = "SByte";

		// Token: 0x040007D1 RID: 2001
		internal const string Int16 = "Int16";

		// Token: 0x040007D2 RID: 2002
		internal const string Int32 = "Int32";

		// Token: 0x040007D3 RID: 2003
		internal const string Int64 = "Int64";

		// Token: 0x040007D4 RID: 2004
		internal const string Money = "Money";

		// Token: 0x040007D5 RID: 2005
		internal const string Null = "Null";

		// Token: 0x040007D6 RID: 2006
		internal const string String = "String";

		// Token: 0x040007D7 RID: 2007
		internal const string DateTimeOffset = "DateTimeOffset";

		// Token: 0x040007D8 RID: 2008
		internal const string Time = "Time";

		// Token: 0x040007D9 RID: 2009
		internal const string UInt16 = "UInt16";

		// Token: 0x040007DA RID: 2010
		internal const string UInt32 = "UInt32";

		// Token: 0x040007DB RID: 2011
		internal const string UInt64 = "UInt64";

		// Token: 0x040007DC RID: 2012
		internal const string Xml = "Xml";

		// Token: 0x040007DD RID: 2013
		internal const string Name = "Name";

		// Token: 0x040007DE RID: 2014
		internal const string Namespace = "Namespace";

		// Token: 0x040007DF RID: 2015
		internal const string Abstract = "Abstract";

		// Token: 0x040007E0 RID: 2016
		internal const string BaseType = "BaseType";

		// Token: 0x040007E1 RID: 2017
		internal const string Sealed = "Sealed";

		// Token: 0x040007E2 RID: 2018
		internal const string ItemAttributes = "MetadataProperties";

		// Token: 0x040007E3 RID: 2019
		internal const string Type = "Type";

		// Token: 0x040007E4 RID: 2020
		internal const string Schema = "Schema";

		// Token: 0x040007E5 RID: 2021
		internal const string Table = "Table";

		// Token: 0x040007E6 RID: 2022
		internal const string FacetType = "FacetType";

		// Token: 0x040007E7 RID: 2023
		internal const string Value = "Value";

		// Token: 0x040007E8 RID: 2024
		internal const string EnumMembers = "EnumMembers";

		// Token: 0x040007E9 RID: 2025
		internal const string BuiltInAttribute = "BuiltInAttribute";

		// Token: 0x040007EA RID: 2026
		internal const string StoreFunctionNamespace = "StoreFunctionNamespace";

		// Token: 0x040007EB RID: 2027
		internal const string ParameterTypeSemanticsAttribute = "ParameterTypeSemanticsAttribute";

		// Token: 0x040007EC RID: 2028
		internal const string ParameterTypeSemantics = "ParameterTypeSemantics";

		// Token: 0x040007ED RID: 2029
		internal const string NiladicFunctionAttribute = "NiladicFunctionAttribute";

		// Token: 0x040007EE RID: 2030
		internal const string IsComposableFunctionAttribute = "IsComposable";

		// Token: 0x040007EF RID: 2031
		internal const string CommandTextFunctionAttribyte = "CommandText";

		// Token: 0x040007F0 RID: 2032
		internal const string StoreFunctionNameAttribute = "StoreFunctionNameAttribute";

		// Token: 0x040007F1 RID: 2033
		internal const string WebHomeSymbol = "~";

		// Token: 0x040007F2 RID: 2034
		internal const string Summary = "Summary";

		// Token: 0x040007F3 RID: 2035
		internal const string LongDescription = "LongDescription";

		// Token: 0x040007F4 RID: 2036
		internal static readonly EdmConstants.Unbounded UnboundedValue = EdmConstants.Unbounded.Instance;

		// Token: 0x020002B7 RID: 695
		internal class Unbounded
		{
			// Token: 0x06001C5C RID: 7260 RVA: 0x0004EC94 File Offset: 0x0004CE94
			private Unbounded()
			{
			}

			// Token: 0x170007D2 RID: 2002
			// (get) Token: 0x06001C5D RID: 7261 RVA: 0x0004EC9C File Offset: 0x0004CE9C
			internal static EdmConstants.Unbounded Instance
			{
				get
				{
					return EdmConstants.Unbounded._instance;
				}
			}

			// Token: 0x06001C5E RID: 7262 RVA: 0x0004ECA3 File Offset: 0x0004CEA3
			public override string ToString()
			{
				return "Max";
			}

			// Token: 0x04000FAC RID: 4012
			private static readonly EdmConstants.Unbounded _instance = new EdmConstants.Unbounded();
		}
	}
}
