using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004A1 RID: 1185
	internal static class EdmConstants
	{
		// Token: 0x0400136B RID: 4971
		internal const string EdmNamespace = "Edm";

		// Token: 0x0400136C RID: 4972
		internal const string ClrPrimitiveTypeNamespace = "System";

		// Token: 0x0400136D RID: 4973
		internal const string TransientNamespace = "Transient";

		// Token: 0x0400136E RID: 4974
		internal const int NumPrimitiveTypes = 32;

		// Token: 0x0400136F RID: 4975
		internal const int NumBuiltInTypes = 40;

		// Token: 0x04001370 RID: 4976
		internal const int MaxLength = 256;

		// Token: 0x04001371 RID: 4977
		internal const string AssociationEnd = "AssociationEnd";

		// Token: 0x04001372 RID: 4978
		internal const string AssociationSetType = "AssociationSetType";

		// Token: 0x04001373 RID: 4979
		internal const string AssociationSetEndType = "AssociationSetEndType";

		// Token: 0x04001374 RID: 4980
		internal const string AssociationType = "AssociationType";

		// Token: 0x04001375 RID: 4981
		internal const string BaseEntitySetType = "BaseEntitySetType";

		// Token: 0x04001376 RID: 4982
		internal const string CollectionType = "CollectionType";

		// Token: 0x04001377 RID: 4983
		internal const string ComplexType = "ComplexType";

		// Token: 0x04001378 RID: 4984
		internal const string DeleteAction = "DeleteAction";

		// Token: 0x04001379 RID: 4985
		internal const string DeleteBehavior = "DeleteBehavior";

		// Token: 0x0400137A RID: 4986
		internal const string Documentation = "Documentation";

		// Token: 0x0400137B RID: 4987
		internal const string EdmType = "EdmType";

		// Token: 0x0400137C RID: 4988
		internal const string ElementType = "ElementType";

		// Token: 0x0400137D RID: 4989
		internal const string EntityContainerType = "EntityContainerType";

		// Token: 0x0400137E RID: 4990
		internal const string EntitySetType = "EntitySetType";

		// Token: 0x0400137F RID: 4991
		internal const string EntityType = "EntityType";

		// Token: 0x04001380 RID: 4992
		internal const string EnumerationMember = "EnumMember";

		// Token: 0x04001381 RID: 4993
		internal const string EnumerationType = "EnumType";

		// Token: 0x04001382 RID: 4994
		internal const string Facet = "Facet";

		// Token: 0x04001383 RID: 4995
		internal const string Function = "EdmFunction";

		// Token: 0x04001384 RID: 4996
		internal const string FunctionParameter = "FunctionParameter";

		// Token: 0x04001385 RID: 4997
		internal const string GlobalItem = "GlobalItem";

		// Token: 0x04001386 RID: 4998
		internal const string ItemAttribute = "MetadataProperty";

		// Token: 0x04001387 RID: 4999
		internal const string ItemType = "ItemType";

		// Token: 0x04001388 RID: 5000
		internal const string Member = "EdmMember";

		// Token: 0x04001389 RID: 5001
		internal const string NavigationProperty = "NavigationProperty";

		// Token: 0x0400138A RID: 5002
		internal const string OperationBehavior = "OperationBehavior";

		// Token: 0x0400138B RID: 5003
		internal const string OperationBehaviors = "OperationBehaviors";

		// Token: 0x0400138C RID: 5004
		internal const string ParameterMode = "ParameterMode";

		// Token: 0x0400138D RID: 5005
		internal const string PrimitiveType = "PrimitiveType";

		// Token: 0x0400138E RID: 5006
		internal const string PrimitiveTypeKind = "PrimitiveTypeKind";

		// Token: 0x0400138F RID: 5007
		internal const string Property = "EdmProperty";

		// Token: 0x04001390 RID: 5008
		internal const string ProviderManifest = "ProviderManifest";

		// Token: 0x04001391 RID: 5009
		internal const string ReferentialConstraint = "ReferentialConstraint";

		// Token: 0x04001392 RID: 5010
		internal const string RefType = "RefType";

		// Token: 0x04001393 RID: 5011
		internal const string RelationshipEnd = "RelationshipEnd";

		// Token: 0x04001394 RID: 5012
		internal const string RelationshipMultiplicity = "RelationshipMultiplicity";

		// Token: 0x04001395 RID: 5013
		internal const string RelationshipSet = "RelationshipSet";

		// Token: 0x04001396 RID: 5014
		internal const string RelationshipType = "RelationshipType";

		// Token: 0x04001397 RID: 5015
		internal const string ReturnParameter = "ReturnParameter";

		// Token: 0x04001398 RID: 5016
		internal const string Role = "Role";

		// Token: 0x04001399 RID: 5017
		internal const string RowType = "RowType";

		// Token: 0x0400139A RID: 5018
		internal const string SimpleType = "SimpleType";

		// Token: 0x0400139B RID: 5019
		internal const string StructuralType = "StructuralType";

		// Token: 0x0400139C RID: 5020
		internal const string TypeUsage = "TypeUsage";

		// Token: 0x0400139D RID: 5021
		internal const string Utc = "Utc";

		// Token: 0x0400139E RID: 5022
		internal const string Unspecified = "Unspecified";

		// Token: 0x0400139F RID: 5023
		internal const string Local = "Local";

		// Token: 0x040013A0 RID: 5024
		internal const string One = "One";

		// Token: 0x040013A1 RID: 5025
		internal const string ZeroToOne = "ZeroToOne";

		// Token: 0x040013A2 RID: 5026
		internal const string Many = "Many";

		// Token: 0x040013A3 RID: 5027
		internal const string In = "In";

		// Token: 0x040013A4 RID: 5028
		internal const string Out = "Out";

		// Token: 0x040013A5 RID: 5029
		internal const string InOut = "InOut";

		// Token: 0x040013A6 RID: 5030
		internal const string None = "None";

		// Token: 0x040013A7 RID: 5031
		internal const string Cascade = "Cascade";

		// Token: 0x040013A8 RID: 5032
		internal const string NoneCollectionKind = "None";

		// Token: 0x040013A9 RID: 5033
		internal const string ListCollectionKind = "List";

		// Token: 0x040013AA RID: 5034
		internal const string BagCollectionKind = "Bag";

		// Token: 0x040013AB RID: 5035
		internal const string MaxMaxLength = "Max";

		// Token: 0x040013AC RID: 5036
		internal const string VariableSrid = "Variable";

		// Token: 0x040013AD RID: 5037
		internal const string AssociationSetEnds = "AssociationSetEnds";

		// Token: 0x040013AE RID: 5038
		internal const string Child = "Child";

		// Token: 0x040013AF RID: 5039
		internal const string DefaultValue = "DefaultValue";

		// Token: 0x040013B0 RID: 5040
		internal const string Ends = "Ends";

		// Token: 0x040013B1 RID: 5041
		internal const string EntitySet = "EntitySet";

		// Token: 0x040013B2 RID: 5042
		internal const string AssociationSet = "AssociationSet";

		// Token: 0x040013B3 RID: 5043
		internal const string EntitySets = "EntitySets";

		// Token: 0x040013B4 RID: 5044
		internal const string Facets = "Facets";

		// Token: 0x040013B5 RID: 5045
		internal const string FromProperties = "FromProperties";

		// Token: 0x040013B6 RID: 5046
		internal const string FromRole = "FromRole";

		// Token: 0x040013B7 RID: 5047
		internal const string IsParent = "IsParent";

		// Token: 0x040013B8 RID: 5048
		internal const string KeyMembers = "KeyMembers";

		// Token: 0x040013B9 RID: 5049
		internal const string Members = "Members";

		// Token: 0x040013BA RID: 5050
		internal const string Mode = "Mode";

		// Token: 0x040013BB RID: 5051
		internal const string Nullable = "Nullable";

		// Token: 0x040013BC RID: 5052
		internal const string Parameters = "Parameters";

		// Token: 0x040013BD RID: 5053
		internal const string Parent = "Parent";

		// Token: 0x040013BE RID: 5054
		internal const string Properties = "Properties";

		// Token: 0x040013BF RID: 5055
		internal const string ToProperties = "ToProperties";

		// Token: 0x040013C0 RID: 5056
		internal const string ToRole = "ToRole";

		// Token: 0x040013C1 RID: 5057
		internal const string ReferentialConstraints = "ReferentialConstraints";

		// Token: 0x040013C2 RID: 5058
		internal const string RelationshipTypeName = "RelationshipTypeName";

		// Token: 0x040013C3 RID: 5059
		internal const string ReturnType = "ReturnType";

		// Token: 0x040013C4 RID: 5060
		internal const string ToEndMemberName = "ToEndMemberName";

		// Token: 0x040013C5 RID: 5061
		internal const string CollectionKind = "CollectionKind";

		// Token: 0x040013C6 RID: 5062
		internal const string Binary = "Binary";

		// Token: 0x040013C7 RID: 5063
		internal const string Boolean = "Boolean";

		// Token: 0x040013C8 RID: 5064
		internal const string Byte = "Byte";

		// Token: 0x040013C9 RID: 5065
		internal const string DateTime = "DateTime";

		// Token: 0x040013CA RID: 5066
		internal const string Decimal = "Decimal";

		// Token: 0x040013CB RID: 5067
		internal const string Double = "Double";

		// Token: 0x040013CC RID: 5068
		internal const string Geometry = "Geometry";

		// Token: 0x040013CD RID: 5069
		internal const string GeometryPoint = "GeometryPoint";

		// Token: 0x040013CE RID: 5070
		internal const string GeometryLineString = "GeometryLineString";

		// Token: 0x040013CF RID: 5071
		internal const string GeometryPolygon = "GeometryPolygon";

		// Token: 0x040013D0 RID: 5072
		internal const string GeometryMultiPoint = "GeometryMultiPoint";

		// Token: 0x040013D1 RID: 5073
		internal const string GeometryMultiLineString = "GeometryMultiLineString";

		// Token: 0x040013D2 RID: 5074
		internal const string GeometryMultiPolygon = "GeometryMultiPolygon";

		// Token: 0x040013D3 RID: 5075
		internal const string GeometryCollection = "GeometryCollection";

		// Token: 0x040013D4 RID: 5076
		internal const string Geography = "Geography";

		// Token: 0x040013D5 RID: 5077
		internal const string GeographyPoint = "GeographyPoint";

		// Token: 0x040013D6 RID: 5078
		internal const string GeographyLineString = "GeographyLineString";

		// Token: 0x040013D7 RID: 5079
		internal const string GeographyPolygon = "GeographyPolygon";

		// Token: 0x040013D8 RID: 5080
		internal const string GeographyMultiPoint = "GeographyMultiPoint";

		// Token: 0x040013D9 RID: 5081
		internal const string GeographyMultiLineString = "GeographyMultiLineString";

		// Token: 0x040013DA RID: 5082
		internal const string GeographyMultiPolygon = "GeographyMultiPolygon";

		// Token: 0x040013DB RID: 5083
		internal const string GeographyCollection = "GeographyCollection";

		// Token: 0x040013DC RID: 5084
		internal const string Guid = "Guid";

		// Token: 0x040013DD RID: 5085
		internal const string HierarchyId = "HierarchyId";

		// Token: 0x040013DE RID: 5086
		internal const string Single = "Single";

		// Token: 0x040013DF RID: 5087
		internal const string SByte = "SByte";

		// Token: 0x040013E0 RID: 5088
		internal const string Int16 = "Int16";

		// Token: 0x040013E1 RID: 5089
		internal const string Int32 = "Int32";

		// Token: 0x040013E2 RID: 5090
		internal const string Int64 = "Int64";

		// Token: 0x040013E3 RID: 5091
		internal const string Money = "Money";

		// Token: 0x040013E4 RID: 5092
		internal const string Null = "Null";

		// Token: 0x040013E5 RID: 5093
		internal const string String = "String";

		// Token: 0x040013E6 RID: 5094
		internal const string DateTimeOffset = "DateTimeOffset";

		// Token: 0x040013E7 RID: 5095
		internal const string Time = "Time";

		// Token: 0x040013E8 RID: 5096
		internal const string UInt16 = "UInt16";

		// Token: 0x040013E9 RID: 5097
		internal const string UInt32 = "UInt32";

		// Token: 0x040013EA RID: 5098
		internal const string UInt64 = "UInt64";

		// Token: 0x040013EB RID: 5099
		internal const string Xml = "Xml";

		// Token: 0x040013EC RID: 5100
		internal const string Name = "Name";

		// Token: 0x040013ED RID: 5101
		internal const string Namespace = "Namespace";

		// Token: 0x040013EE RID: 5102
		internal const string Abstract = "Abstract";

		// Token: 0x040013EF RID: 5103
		internal const string BaseType = "BaseType";

		// Token: 0x040013F0 RID: 5104
		internal const string Sealed = "Sealed";

		// Token: 0x040013F1 RID: 5105
		internal const string ItemAttributes = "MetadataProperties";

		// Token: 0x040013F2 RID: 5106
		internal const string Type = "Type";

		// Token: 0x040013F3 RID: 5107
		internal const string Schema = "Schema";

		// Token: 0x040013F4 RID: 5108
		internal const string Table = "Table";

		// Token: 0x040013F5 RID: 5109
		internal const string FacetType = "FacetType";

		// Token: 0x040013F6 RID: 5110
		internal const string Value = "Value";

		// Token: 0x040013F7 RID: 5111
		internal const string EnumMembers = "EnumMembers";

		// Token: 0x040013F8 RID: 5112
		internal const string BuiltInAttribute = "BuiltInAttribute";

		// Token: 0x040013F9 RID: 5113
		internal const string StoreFunctionNamespace = "StoreFunctionNamespace";

		// Token: 0x040013FA RID: 5114
		internal const string ParameterTypeSemanticsAttribute = "ParameterTypeSemanticsAttribute";

		// Token: 0x040013FB RID: 5115
		internal const string ParameterTypeSemantics = "ParameterTypeSemantics";

		// Token: 0x040013FC RID: 5116
		internal const string NiladicFunctionAttribute = "NiladicFunctionAttribute";

		// Token: 0x040013FD RID: 5117
		internal const string IsComposableFunctionAttribute = "IsComposable";

		// Token: 0x040013FE RID: 5118
		internal const string CommandTextFunctionAttribute = "CommandText";

		// Token: 0x040013FF RID: 5119
		internal const string StoreFunctionNameAttribute = "StoreFunctionNameAttribute";

		// Token: 0x04001400 RID: 5120
		internal const string WebHomeSymbol = "~";

		// Token: 0x04001401 RID: 5121
		internal const string Summary = "Summary";

		// Token: 0x04001402 RID: 5122
		internal const string LongDescription = "LongDescription";

		// Token: 0x04001403 RID: 5123
		internal static readonly EdmConstants.Unbounded UnboundedValue = EdmConstants.Unbounded.Instance;

		// Token: 0x04001404 RID: 5124
		internal static readonly EdmConstants.Variable VariableValue = EdmConstants.Variable.Instance;

		// Token: 0x02000AC9 RID: 2761
		internal class Unbounded
		{
			// Token: 0x06006308 RID: 25352 RVA: 0x0015777D File Offset: 0x0015597D
			private Unbounded()
			{
			}

			// Token: 0x170010CC RID: 4300
			// (get) Token: 0x06006309 RID: 25353 RVA: 0x00157785 File Offset: 0x00155985
			internal static EdmConstants.Unbounded Instance
			{
				get
				{
					return EdmConstants.Unbounded._instance;
				}
			}

			// Token: 0x0600630A RID: 25354 RVA: 0x0015778C File Offset: 0x0015598C
			public override string ToString()
			{
				return "Max";
			}

			// Token: 0x04002BA2 RID: 11170
			private static readonly EdmConstants.Unbounded _instance = new EdmConstants.Unbounded();
		}

		// Token: 0x02000ACA RID: 2762
		internal class Variable
		{
			// Token: 0x0600630C RID: 25356 RVA: 0x0015779F File Offset: 0x0015599F
			private Variable()
			{
			}

			// Token: 0x170010CD RID: 4301
			// (get) Token: 0x0600630D RID: 25357 RVA: 0x001577A7 File Offset: 0x001559A7
			internal static EdmConstants.Variable Instance
			{
				get
				{
					return EdmConstants.Variable._instance;
				}
			}

			// Token: 0x0600630E RID: 25358 RVA: 0x001577AE File Offset: 0x001559AE
			public override string ToString()
			{
				return "Variable";
			}

			// Token: 0x04002BA3 RID: 11171
			private static readonly EdmConstants.Variable _instance = new EdmConstants.Variable();
		}
	}
}
