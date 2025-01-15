using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000176 RID: 374
	public static class CsdlConstants
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x0001160C File Offset: 0x0000F80C
		// Note: this type is marked as 'beforefieldinit'.
		static CsdlConstants()
		{
			Dictionary<Version, string[]> dictionary = new Dictionary<Version, string[]>();
			dictionary.Add(EdmConstants.EdmVersion4, new string[] { "http://docs.oasis-open.org/odata/ns/edm" });
			CsdlConstants.SupportedVersions = dictionary;
			Dictionary<Version, string> dictionary2 = new Dictionary<Version, string>();
			dictionary2.Add(CsdlConstants.EdmxVersion4, "http://docs.oasis-open.org/odata/ns/edmx");
			CsdlConstants.SupportedEdmxVersions = dictionary2;
			CsdlConstants.SupportedEdmxNamespaces = Enumerable.ToDictionary<KeyValuePair<Version, string>, string, Version>(CsdlConstants.SupportedEdmxVersions, (KeyValuePair<Version, string> v) => v.Value, (KeyValuePair<Version, string> v) => v.Key);
			Dictionary<Version, Version> dictionary3 = new Dictionary<Version, Version>();
			dictionary3.Add(EdmConstants.EdmVersion4, CsdlConstants.EdmxVersion4);
			CsdlConstants.EdmToEdmxVersions = dictionary3;
		}

		// Token: 0x040002F9 RID: 761
		internal const string CsdlFileExtension = ".csdl";

		// Token: 0x040002FA RID: 762
		internal const string EdmOasisNamespace = "http://docs.oasis-open.org/odata/ns/edm";

		// Token: 0x040002FB RID: 763
		internal const string SchemaNamespaceAnnotation = "SchemaNamespace";

		// Token: 0x040002FC RID: 764
		internal const string AnnotationSerializationLocationAnnotation = "AnnotationSerializationLocation";

		// Token: 0x040002FD RID: 765
		internal const string NamespacePrefixAnnotation = "NamespacePrefix";

		// Token: 0x040002FE RID: 766
		internal const string IsEnumMemberValueExplicitAnnotation = "IsEnumMemberValueExplicit";

		// Token: 0x040002FF RID: 767
		internal const string IsSerializedAsElementAnnotation = "IsSerializedAsElement";

		// Token: 0x04000300 RID: 768
		internal const string NamespaceAliasAnnotation = "NamespaceAlias";

		// Token: 0x04000301 RID: 769
		internal const string UsedNamespacesAnnotation = "UsedNamespaces";

		// Token: 0x04000302 RID: 770
		internal const string ReferencesAnnotation = "References";

		// Token: 0x04000303 RID: 771
		internal const string PrimitiveValueConverterMapAnnotation = "PrimitiveValueConverterMap";

		// Token: 0x04000304 RID: 772
		internal const string Attribute_Abstract = "Abstract";

		// Token: 0x04000305 RID: 773
		internal const string Attribute_Action = "Action";

		// Token: 0x04000306 RID: 774
		internal const string Attribute_Alias = "Alias";

		// Token: 0x04000307 RID: 775
		internal const string Attribute_AppliesTo = "AppliesTo";

		// Token: 0x04000308 RID: 776
		internal const string Attribute_BaseType = "BaseType";

		// Token: 0x04000309 RID: 777
		internal const string Attribute_Binary = "Binary";

		// Token: 0x0400030A RID: 778
		internal const string Attribute_Bool = "Bool";

		// Token: 0x0400030B RID: 779
		internal const string Attribute_Collation = "Collation";

		// Token: 0x0400030C RID: 780
		internal const string Attribute_ConcurrencyMode = "ConcurrencyMode";

		// Token: 0x0400030D RID: 781
		internal const string Attribute_ContainsTarget = "ContainsTarget";

		// Token: 0x0400030E RID: 782
		internal const string Attribute_Date = "Date";

		// Token: 0x0400030F RID: 783
		internal const string Attribute_DateTimeOffset = "DateTimeOffset";

		// Token: 0x04000310 RID: 784
		internal const string Attribute_Decimal = "Decimal";

		// Token: 0x04000311 RID: 785
		internal const string Attribute_DefaultValue = "DefaultValue";

		// Token: 0x04000312 RID: 786
		internal const string Attribute_ElementType = "ElementType";

		// Token: 0x04000313 RID: 787
		internal const string Attribute_Extends = "Extends";

		// Token: 0x04000314 RID: 788
		internal const string Attribute_EntityType = "EntityType";

		// Token: 0x04000315 RID: 789
		internal const string Attribute_EntitySet = "EntitySet";

		// Token: 0x04000316 RID: 790
		internal const string Attribute_EntitySetPath = "EntitySetPath";

		// Token: 0x04000317 RID: 791
		internal const string Attribute_EnumMember = "EnumMember";

		// Token: 0x04000318 RID: 792
		internal const string Attribute_Float = "Float";

		// Token: 0x04000319 RID: 793
		internal const string Attribute_Function = "Function";

		// Token: 0x0400031A RID: 794
		internal const string Attribute_Guid = "Guid";

		// Token: 0x0400031B RID: 795
		internal const string Attribute_HasStream = "HasStream";

		// Token: 0x0400031C RID: 796
		internal const string Attribute_Int = "Int";

		// Token: 0x0400031D RID: 797
		internal const string Attribute_IncludeInServiceDocument = "IncludeInServiceDocument";

		// Token: 0x0400031E RID: 798
		internal const string Attribute_IsBound = "IsBound";

		// Token: 0x0400031F RID: 799
		internal const string Attribute_IsComposable = "IsComposable";

		// Token: 0x04000320 RID: 800
		internal const string Attribute_IsFlags = "IsFlags";

		// Token: 0x04000321 RID: 801
		internal const string Attribute_MaxLength = "MaxLength";

		// Token: 0x04000322 RID: 802
		internal const string Attribute_Name = "Name";

		// Token: 0x04000323 RID: 803
		internal const string Attribute_Namespace = "Namespace";

		// Token: 0x04000324 RID: 804
		internal const string Attribute_NavigationPropertyPath = "NavigationPropertyPath";

		// Token: 0x04000325 RID: 805
		internal const string Attribute_Nullable = "Nullable";

		// Token: 0x04000326 RID: 806
		internal const string Attribute_OpenType = "OpenType";

		// Token: 0x04000327 RID: 807
		internal const string Attribute_Partner = "Partner";

		// Token: 0x04000328 RID: 808
		internal const string Attribute_Path = "Path";

		// Token: 0x04000329 RID: 809
		internal const string Attribute_Precision = "Precision";

		// Token: 0x0400032A RID: 810
		internal const string Attribute_Property = "Property";

		// Token: 0x0400032B RID: 811
		internal const string Attribute_PropertyPath = "PropertyPath";

		// Token: 0x0400032C RID: 812
		internal const string Attribute_ReferencedProperty = "ReferencedProperty";

		// Token: 0x0400032D RID: 813
		internal const string Attribute_Qualifier = "Qualifier";

		// Token: 0x0400032E RID: 814
		internal const string Attribute_Scale = "Scale";

		// Token: 0x0400032F RID: 815
		internal const string Attribute_Srid = "SRID";

		// Token: 0x04000330 RID: 816
		internal const string Attribute_String = "String";

		// Token: 0x04000331 RID: 817
		internal const string Attribute_Target = "Target";

		// Token: 0x04000332 RID: 818
		internal const string Attribute_Term = "Term";

		// Token: 0x04000333 RID: 819
		internal const string Attribute_Duration = "Duration";

		// Token: 0x04000334 RID: 820
		internal const string Attribute_TimeOfDay = "TimeOfDay";

		// Token: 0x04000335 RID: 821
		internal const string Attribute_Type = "Type";

		// Token: 0x04000336 RID: 822
		internal const string Attribute_UnderlyingType = "UnderlyingType";

		// Token: 0x04000337 RID: 823
		internal const string Attribute_Unicode = "Unicode";

		// Token: 0x04000338 RID: 824
		internal const string Attribute_Value = "Value";

		// Token: 0x04000339 RID: 825
		internal const string Element_Action = "Action";

		// Token: 0x0400033A RID: 826
		internal const string Element_ActionImport = "ActionImport";

		// Token: 0x0400033B RID: 827
		internal const string Element_Annotation = "Annotation";

		// Token: 0x0400033C RID: 828
		internal const string Element_Annotations = "Annotations";

		// Token: 0x0400033D RID: 829
		internal const string Element_Apply = "Apply";

		// Token: 0x0400033E RID: 830
		internal const string Element_Binary = "Binary";

		// Token: 0x0400033F RID: 831
		internal const string Element_Bool = "Bool";

		// Token: 0x04000340 RID: 832
		internal const string Element_Cast = "Cast";

		// Token: 0x04000341 RID: 833
		internal const string Element_Collection = "Collection";

		// Token: 0x04000342 RID: 834
		internal const string Element_CollectionType = "CollectionType";

		// Token: 0x04000343 RID: 835
		internal const string Element_ComplexType = "ComplexType";

		// Token: 0x04000344 RID: 836
		internal const string Element_Date = "Date";

		// Token: 0x04000345 RID: 837
		internal const string Element_DateTimeOffset = "DateTimeOffset";

		// Token: 0x04000346 RID: 838
		internal const string Element_Decimal = "Decimal";

		// Token: 0x04000347 RID: 839
		internal const string Element_Documentation = "Documentation";

		// Token: 0x04000348 RID: 840
		internal const string Element_EntityContainer = "EntityContainer";

		// Token: 0x04000349 RID: 841
		internal const string Element_EntitySet = "EntitySet";

		// Token: 0x0400034A RID: 842
		internal const string Element_EntitySetReference = "EntitySetReference";

		// Token: 0x0400034B RID: 843
		internal const string Element_EntityType = "EntityType";

		// Token: 0x0400034C RID: 844
		internal const string Element_EnumMember = "EnumMember";

		// Token: 0x0400034D RID: 845
		internal const string Element_EnumType = "EnumType";

		// Token: 0x0400034E RID: 846
		internal const string Element_Float = "Float";

		// Token: 0x0400034F RID: 847
		internal const string Element_Guid = "Guid";

		// Token: 0x04000350 RID: 848
		internal const string Element_Function = "Function";

		// Token: 0x04000351 RID: 849
		internal const string Element_FunctionImport = "FunctionImport";

		// Token: 0x04000352 RID: 850
		internal const string Element_FunctionReference = "FunctionReference";

		// Token: 0x04000353 RID: 851
		internal const string Element_If = "If";

		// Token: 0x04000354 RID: 852
		internal const string Element_IsType = "IsType";

		// Token: 0x04000355 RID: 853
		internal const string Element_Int = "Int";

		// Token: 0x04000356 RID: 854
		internal const string Element_Key = "Key";

		// Token: 0x04000357 RID: 855
		internal const string Element_LabeledElement = "LabeledElement";

		// Token: 0x04000358 RID: 856
		internal const string Element_LabeledElementReference = "LabeledElementReference";

		// Token: 0x04000359 RID: 857
		internal const string Element_LongDescription = "LongDescription";

		// Token: 0x0400035A RID: 858
		internal const string Element_Member = "Member";

		// Token: 0x0400035B RID: 859
		internal const string Element_NavigationProperty = "NavigationProperty";

		// Token: 0x0400035C RID: 860
		internal const string Element_NavigationPropertyBinding = "NavigationPropertyBinding";

		// Token: 0x0400035D RID: 861
		internal const string Element_NavigationPropertyPath = "NavigationPropertyPath";

		// Token: 0x0400035E RID: 862
		internal const string Element_Null = "Null";

		// Token: 0x0400035F RID: 863
		internal const string Element_OnDelete = "OnDelete";

		// Token: 0x04000360 RID: 864
		internal const string Element_Parameter = "Parameter";

		// Token: 0x04000361 RID: 865
		internal const string Element_ParameterReference = "ParameterReference";

		// Token: 0x04000362 RID: 866
		internal const string Element_Path = "Path";

		// Token: 0x04000363 RID: 867
		internal const string Element_Property = "Property";

		// Token: 0x04000364 RID: 868
		internal const string Element_PropertyPath = "PropertyPath";

		// Token: 0x04000365 RID: 869
		internal const string Element_PropertyRef = "PropertyRef";

		// Token: 0x04000366 RID: 870
		internal const string Element_PropertyReference = "PropertyReference";

		// Token: 0x04000367 RID: 871
		internal const string Element_PropertyValue = "PropertyValue";

		// Token: 0x04000368 RID: 872
		internal const string Element_Record = "Record";

		// Token: 0x04000369 RID: 873
		internal const string Element_ReferenceType = "ReferenceType";

		// Token: 0x0400036A RID: 874
		internal const string Element_ReferentialConstraint = "ReferentialConstraint";

		// Token: 0x0400036B RID: 875
		internal const string Element_ReturnType = "ReturnType";

		// Token: 0x0400036C RID: 876
		internal const string Element_Singleton = "Singleton";

		// Token: 0x0400036D RID: 877
		internal const string Element_Schema = "Schema";

		// Token: 0x0400036E RID: 878
		internal const string Element_String = "String";

		// Token: 0x0400036F RID: 879
		internal const string Element_Summary = "Summary";

		// Token: 0x04000370 RID: 880
		internal const string Element_Duration = "Duration";

		// Token: 0x04000371 RID: 881
		internal const string Element_Term = "Term";

		// Token: 0x04000372 RID: 882
		internal const string Element_TimeOfDay = "TimeOfDay";

		// Token: 0x04000373 RID: 883
		internal const string Element_TypeDefinition = "TypeDefinition";

		// Token: 0x04000374 RID: 884
		internal const string Element_TypeRef = "TypeRef";

		// Token: 0x04000375 RID: 885
		internal const string Value_Cascade = "Cascade";

		// Token: 0x04000376 RID: 886
		internal const string Value_Collection = "Collection";

		// Token: 0x04000377 RID: 887
		internal const string Value_EndMany = "*";

		// Token: 0x04000378 RID: 888
		internal const string Value_EndOptional = "0..1";

		// Token: 0x04000379 RID: 889
		internal const string Value_EndRequired = "1";

		// Token: 0x0400037A RID: 890
		internal const string Value_Fixed = "Fixed";

		// Token: 0x0400037B RID: 891
		internal const string Value_Max = "max";

		// Token: 0x0400037C RID: 892
		internal const string Value_None = "None";

		// Token: 0x0400037D RID: 893
		internal const string Value_Ref = "Ref";

		// Token: 0x0400037E RID: 894
		internal const string Value_SridVariable = "Variable";

		// Token: 0x0400037F RID: 895
		internal const string Value_ScaleVariable = "Variable";

		// Token: 0x04000380 RID: 896
		internal const bool Default_Abstract = false;

		// Token: 0x04000381 RID: 897
		internal const EdmConcurrencyMode Default_ConcurrencyMode = EdmConcurrencyMode.None;

		// Token: 0x04000382 RID: 898
		internal const bool Default_ContainsTarget = false;

		// Token: 0x04000383 RID: 899
		internal const bool Default_HasStream = false;

		// Token: 0x04000384 RID: 900
		internal const bool Default_IncludeInServiceDocument = false;

		// Token: 0x04000385 RID: 901
		internal const bool Default_IsAtomic = false;

		// Token: 0x04000386 RID: 902
		internal const bool Default_IsBound = false;

		// Token: 0x04000387 RID: 903
		internal const bool Default_IsComposable = false;

		// Token: 0x04000388 RID: 904
		internal const bool Default_IsFlags = false;

		// Token: 0x04000389 RID: 905
		internal const bool Default_OpenType = false;

		// Token: 0x0400038A RID: 906
		internal const bool Default_Nullable = true;

		// Token: 0x0400038B RID: 907
		internal const bool Default_IsUnicode = true;

		// Token: 0x0400038C RID: 908
		internal const int Default_TemporalPrecision = 0;

		// Token: 0x0400038D RID: 909
		internal const int Default_SpatialGeographySrid = 4326;

		// Token: 0x0400038E RID: 910
		internal const int Default_SpatialGeometrySrid = 0;

		// Token: 0x0400038F RID: 911
		internal const int Default_Scale = 0;

		// Token: 0x04000390 RID: 912
		internal const int Max_NameLength = 480;

		// Token: 0x04000391 RID: 913
		internal const int Max_NamespaceLength = 512;

		// Token: 0x04000392 RID: 914
		internal const string Version3Xsd = "Edm.Csdl.CSDLSchema_3.xsd";

		// Token: 0x04000393 RID: 915
		internal const string EdmxFileExtension = ".edmx";

		// Token: 0x04000394 RID: 916
		internal const string EdmxOasisNamespace = "http://docs.oasis-open.org/odata/ns/edmx";

		// Token: 0x04000395 RID: 917
		internal const string ODataMetadataNamespace = "http://docs.oasis-open.org/odata/ns/metadata";

		// Token: 0x04000396 RID: 918
		internal const string EdmxVersionAnnotation = "EdmxVersion";

		// Token: 0x04000397 RID: 919
		internal const string Prefix_Edmx = "edmx";

		// Token: 0x04000398 RID: 920
		internal const string Prefix_ODataMetadata = "m";

		// Token: 0x04000399 RID: 921
		internal const string Attribute_TargetNamespace = "TargetNamespace";

		// Token: 0x0400039A RID: 922
		internal const string Attribute_TermNamespace = "TermNamespace";

		// Token: 0x0400039B RID: 923
		internal const string Attribute_Version = "Version";

		// Token: 0x0400039C RID: 924
		internal const string Attribute_Uri = "Uri";

		// Token: 0x0400039D RID: 925
		internal const string Element_ConceptualModels = "ConceptualModels";

		// Token: 0x0400039E RID: 926
		internal const string Element_Edmx = "Edmx";

		// Token: 0x0400039F RID: 927
		internal const string Element_Runtime = "Runtime";

		// Token: 0x040003A0 RID: 928
		internal const string Element_Reference = "Reference";

		// Token: 0x040003A1 RID: 929
		internal const string Element_Include = "Include";

		// Token: 0x040003A2 RID: 930
		internal const string Element_IncludeAnnotations = "IncludeAnnotations";

		// Token: 0x040003A3 RID: 931
		internal const string Element_DataServices = "DataServices";

		// Token: 0x040003A4 RID: 932
		public static readonly Version EdmxVersion4 = EdmConstants.EdmVersion4;

		// Token: 0x040003A5 RID: 933
		public static readonly Version EdmxVersionLatest = CsdlConstants.EdmxVersion4;

		// Token: 0x040003A6 RID: 934
		internal static Dictionary<Version, string[]> SupportedVersions;

		// Token: 0x040003A7 RID: 935
		internal static Dictionary<Version, string> SupportedEdmxVersions;

		// Token: 0x040003A8 RID: 936
		internal static Dictionary<string, Version> SupportedEdmxNamespaces;

		// Token: 0x040003A9 RID: 937
		internal static Dictionary<Version, Version> EdmToEdmxVersions;
	}
}
