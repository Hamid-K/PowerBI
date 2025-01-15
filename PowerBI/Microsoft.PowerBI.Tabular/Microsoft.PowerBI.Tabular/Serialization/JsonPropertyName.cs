using System;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Tabular.Extensions;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200017B RID: 379
	internal static class JsonPropertyName
	{
		// Token: 0x02000370 RID: 880
		public static class Database
		{
			// Token: 0x04000F04 RID: 3844
			public const string Name = "name";

			// Token: 0x04000F05 RID: 3845
			public const string Id = "id";

			// Token: 0x04000F06 RID: 3846
			public const string Description = "description";

			// Token: 0x04000F07 RID: 3847
			public const string CompatibilityLevel = "compatibilityLevel";

			// Token: 0x04000F08 RID: 3848
			public const string CompatibilityMode = "compatibilityMode";

			// Token: 0x04000F09 RID: 3849
			public const string StorageLocation = "storageLocation";

			// Token: 0x04000F0A RID: 3850
			public const string Language = "language";

			// Token: 0x04000F0B RID: 3851
			public const string ReadWriteMode = "readWriteMode";

			// Token: 0x04000F0C RID: 3852
			public const string CreatedTimestamp = "createdTimestamp";

			// Token: 0x04000F0D RID: 3853
			public const string LastUpdate = "lastUpdate";

			// Token: 0x04000F0E RID: 3854
			public const string LastSchemaUpdate = "lastSchemaUpdate";

			// Token: 0x04000F0F RID: 3855
			public const string LastProcessed = "lastProcessed";

			// Token: 0x04000F10 RID: 3856
			public const string Model = "model";
		}

		// Token: 0x02000371 RID: 881
		public static class Model
		{
			// Token: 0x04000F11 RID: 3857
			public const string Name = "name";

			// Token: 0x04000F12 RID: 3858
			public const string Description = "description";

			// Token: 0x04000F13 RID: 3859
			public const string StorageLocation = "storageLocation";

			// Token: 0x04000F14 RID: 3860
			public const string DefaultMode = "defaultMode";

			// Token: 0x04000F15 RID: 3861
			public const string DefaultDataView = "defaultDataView";

			// Token: 0x04000F16 RID: 3862
			public const string Culture = "culture";

			// Token: 0x04000F17 RID: 3863
			public const string Collation = "collation";

			// Token: 0x04000F18 RID: 3864
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000F19 RID: 3865
			public const string StructureModifiedTime = "structureModifiedTime";

			// Token: 0x04000F1A RID: 3866
			public const string DataAccessOptions = "dataAccessOptions";

			// Token: 0x04000F1B RID: 3867
			public const string DefaultPowerBIDataSourceVersion = "defaultPowerBIDataSourceVersion";

			// Token: 0x04000F1C RID: 3868
			public const string ForceUniqueNames = "forceUniqueNames";

			// Token: 0x04000F1D RID: 3869
			public const string DiscourageImplicitMeasures = "discourageImplicitMeasures";

			// Token: 0x04000F1E RID: 3870
			public const string DiscourageReportMeasures = "discourageReportMeasures";

			// Token: 0x04000F1F RID: 3871
			public const string DataSourceVariablesOverrideBehavior = "dataSourceVariablesOverrideBehavior";

			// Token: 0x04000F20 RID: 3872
			public const string DataSourceDefaultMaxConnections = "dataSourceDefaultMaxConnections";

			// Token: 0x04000F21 RID: 3873
			public const string SourceQueryCulture = "sourceQueryCulture";

			// Token: 0x04000F22 RID: 3874
			public const string MAttributes = "mAttributes";

			// Token: 0x04000F23 RID: 3875
			public const string DiscourageCompositeModels = "discourageCompositeModels";

			// Token: 0x04000F24 RID: 3876
			public const string AutomaticAggregationOptions = "automaticAggregationOptions";

			// Token: 0x04000F25 RID: 3877
			public const string DisableAutoExists = "disableAutoExists";

			// Token: 0x04000F26 RID: 3878
			public const string MaxParallelismPerRefresh = "maxParallelismPerRefresh";

			// Token: 0x04000F27 RID: 3879
			public const string MaxParallelismPerQuery = "maxParallelismPerQuery";

			// Token: 0x04000F28 RID: 3880
			public const string DirectLakeBehavior = "directLakeBehavior";

			// Token: 0x04000F29 RID: 3881
			public const string ValueFilterBehavior = "valueFilterBehavior";

			// Token: 0x04000F2A RID: 3882
			public const string DefaultMeasure = "defaultMeasure";

			// Token: 0x04000F2B RID: 3883
			public const string Tables = "tables";

			// Token: 0x04000F2C RID: 3884
			public const string Relationships = "relationships";

			// Token: 0x04000F2D RID: 3885
			public const string DataSources = "dataSources";

			// Token: 0x04000F2E RID: 3886
			public const string Perspectives = "perspectives";

			// Token: 0x04000F2F RID: 3887
			public const string Cultures = "cultures";

			// Token: 0x04000F30 RID: 3888
			public const string Roles = "roles";

			// Token: 0x04000F31 RID: 3889
			public const string Expressions = "expressions";

			// Token: 0x04000F32 RID: 3890
			public const string QueryGroups = "queryGroups";

			// Token: 0x04000F33 RID: 3891
			public const string AnalyticsAIMetadata = "analyticsAIMetadata";

			// Token: 0x04000F34 RID: 3892
			public const string Functions = "functions";

			// Token: 0x04000F35 RID: 3893
			public const string BindingInfoCollection = "bindingInfoCollection";

			// Token: 0x04000F36 RID: 3894
			public const string Annotations = "annotations";

			// Token: 0x04000F37 RID: 3895
			public const string ExtendedProperties = "extendedProperties";

			// Token: 0x04000F38 RID: 3896
			public const string ExcludedArtifacts = "excludedArtifacts";
		}

		// Token: 0x02000372 RID: 882
		public static class DataSource
		{
			// Token: 0x04000F39 RID: 3897
			public const string Name = "name";

			// Token: 0x04000F3A RID: 3898
			public const string Description = "description";

			// Token: 0x04000F3B RID: 3899
			public const string Type = "type";

			// Token: 0x04000F3C RID: 3900
			public const string MaxConnections = "maxConnections";

			// Token: 0x04000F3D RID: 3901
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000F3E RID: 3902
			public const string Annotations = "annotations";

			// Token: 0x04000F3F RID: 3903
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x02000373 RID: 883
		public static class Table
		{
			// Token: 0x04000F40 RID: 3904
			public const string Name = "name";

			// Token: 0x04000F41 RID: 3905
			public const string DataCategory = "dataCategory";

			// Token: 0x04000F42 RID: 3906
			public const string Description = "description";

			// Token: 0x04000F43 RID: 3907
			public const string IsHidden = "isHidden";

			// Token: 0x04000F44 RID: 3908
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000F45 RID: 3909
			public const string StructureModifiedTime = "structureModifiedTime";

			// Token: 0x04000F46 RID: 3910
			public const string ShowAsVariationsOnly = "showAsVariationsOnly";

			// Token: 0x04000F47 RID: 3911
			public const string IsPrivate = "isPrivate";

			// Token: 0x04000F48 RID: 3912
			public const string AlternateSourcePrecedence = "alternateSourcePrecedence";

			// Token: 0x04000F49 RID: 3913
			public const string ExcludeFromModelRefresh = "excludeFromModelRefresh";

			// Token: 0x04000F4A RID: 3914
			public const string LineageTag = "lineageTag";

			// Token: 0x04000F4B RID: 3915
			public const string SourceLineageTag = "sourceLineageTag";

			// Token: 0x04000F4C RID: 3916
			public const string SystemManaged = "systemManaged";

			// Token: 0x04000F4D RID: 3917
			public const string ExcludeFromAutomaticAggregations = "excludeFromAutomaticAggregations";

			// Token: 0x04000F4E RID: 3918
			public const string DefaultDetailRowsDefinition = "defaultDetailRowsDefinition";

			// Token: 0x04000F4F RID: 3919
			public const string RefreshPolicy = "refreshPolicy";

			// Token: 0x04000F50 RID: 3920
			public const string CalculationGroup = "calculationGroup";

			// Token: 0x04000F51 RID: 3921
			public const string Columns = "columns";

			// Token: 0x04000F52 RID: 3922
			public const string Measures = "measures";

			// Token: 0x04000F53 RID: 3923
			public const string Hierarchies = "hierarchies";

			// Token: 0x04000F54 RID: 3924
			public const string Sets = "sets";

			// Token: 0x04000F55 RID: 3925
			public const string Annotations = "annotations";

			// Token: 0x04000F56 RID: 3926
			public const string ExtendedProperties = "extendedProperties";

			// Token: 0x04000F57 RID: 3927
			public const string ExcludedArtifacts = "excludedArtifacts";

			// Token: 0x04000F58 RID: 3928
			public const string ChangedProperties = "changedProperties";

			// Token: 0x04000F59 RID: 3929
			public const string Calendars = "calendars";

			// Token: 0x04000F5A RID: 3930
			public const string Partitions = "partitions";
		}

		// Token: 0x02000374 RID: 884
		public static class Column
		{
			// Token: 0x04000F5B RID: 3931
			public const string Name = "name";

			// Token: 0x04000F5C RID: 3932
			public const string DataType = "dataType";

			// Token: 0x04000F5D RID: 3933
			public const string DataCategory = "dataCategory";

			// Token: 0x04000F5E RID: 3934
			public const string Description = "description";

			// Token: 0x04000F5F RID: 3935
			public const string IsHidden = "isHidden";

			// Token: 0x04000F60 RID: 3936
			public const string State = "state";

			// Token: 0x04000F61 RID: 3937
			public const string IsUnique = "isUnique";

			// Token: 0x04000F62 RID: 3938
			public const string IsKey = "isKey";

			// Token: 0x04000F63 RID: 3939
			public const string IsNullable = "isNullable";

			// Token: 0x04000F64 RID: 3940
			public const string Alignment = "alignment";

			// Token: 0x04000F65 RID: 3941
			public const string TableDetailPosition = "tableDetailPosition";

			// Token: 0x04000F66 RID: 3942
			public const string IsDefaultLabel = "isDefaultLabel";

			// Token: 0x04000F67 RID: 3943
			public const string IsDefaultImage = "isDefaultImage";

			// Token: 0x04000F68 RID: 3944
			public const string SummarizeBy = "summarizeBy";

			// Token: 0x04000F69 RID: 3945
			public const string Type = "type";

			// Token: 0x04000F6A RID: 3946
			public const string FormatString = "formatString";

			// Token: 0x04000F6B RID: 3947
			public const string IsAvailableInMDX = "isAvailableInMdx";

			// Token: 0x04000F6C RID: 3948
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000F6D RID: 3949
			public const string StructureModifiedTime = "structureModifiedTime";

			// Token: 0x04000F6E RID: 3950
			public const string RefreshedTime = "refreshedTime";

			// Token: 0x04000F6F RID: 3951
			public const string KeepUniqueRows = "keepUniqueRows";

			// Token: 0x04000F70 RID: 3952
			public const string DisplayOrdinal = "displayOrdinal";

			// Token: 0x04000F71 RID: 3953
			public const string ErrorMessage = "errorMessage";

			// Token: 0x04000F72 RID: 3954
			public const string SourceProviderType = "sourceProviderType";

			// Token: 0x04000F73 RID: 3955
			public const string DisplayFolder = "displayFolder";

			// Token: 0x04000F74 RID: 3956
			public const string EncodingHint = "encodingHint";

			// Token: 0x04000F75 RID: 3957
			public const string LineageTag = "lineageTag";

			// Token: 0x04000F76 RID: 3958
			public const string SourceLineageTag = "sourceLineageTag";

			// Token: 0x04000F77 RID: 3959
			public const string SortByColumn = "sortByColumn";

			// Token: 0x04000F78 RID: 3960
			public const string AttributeHierarchy = "attributeHierarchy";

			// Token: 0x04000F79 RID: 3961
			public const string RelatedColumnDetails = "relatedColumnDetails";

			// Token: 0x04000F7A RID: 3962
			public const string AlternateOf = "alternateOf";

			// Token: 0x04000F7B RID: 3963
			public const string Variations = "variations";

			// Token: 0x04000F7C RID: 3964
			public const string Annotations = "annotations";

			// Token: 0x04000F7D RID: 3965
			public const string ExtendedProperties = "extendedProperties";

			// Token: 0x04000F7E RID: 3966
			public const string ChangedProperties = "changedProperties";
		}

		// Token: 0x02000375 RID: 885
		public static class AttributeHierarchy
		{
			// Token: 0x04000F7F RID: 3967
			public const string State = "state";

			// Token: 0x04000F80 RID: 3968
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000F81 RID: 3969
			public const string RefreshedTime = "refreshedTime";

			// Token: 0x04000F82 RID: 3970
			public const string Annotations = "annotations";

			// Token: 0x04000F83 RID: 3971
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x02000376 RID: 886
		public static class Partition
		{
			// Token: 0x04000F84 RID: 3972
			public const string Name = "name";

			// Token: 0x04000F85 RID: 3973
			public const string Description = "description";

			// Token: 0x04000F86 RID: 3974
			public const string State = "state";

			// Token: 0x04000F87 RID: 3975
			public const string Mode = "mode";

			// Token: 0x04000F88 RID: 3976
			public const string DataView = "dataView";

			// Token: 0x04000F89 RID: 3977
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000F8A RID: 3978
			public const string RefreshedTime = "refreshedTime";

			// Token: 0x04000F8B RID: 3979
			public const string ErrorMessage = "errorMessage";

			// Token: 0x04000F8C RID: 3980
			public const string QueryGroup = "queryGroup";

			// Token: 0x04000F8D RID: 3981
			public const string Source = "source";

			// Token: 0x04000F8E RID: 3982
			public const string DataCoverageDefinition = "dataCoverageDefinition";

			// Token: 0x04000F8F RID: 3983
			public const string Annotations = "annotations";

			// Token: 0x04000F90 RID: 3984
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x02000377 RID: 887
		public static class Relationship
		{
			// Token: 0x04000F91 RID: 3985
			public const string Name = "name";

			// Token: 0x04000F92 RID: 3986
			public const string IsActive = "isActive";

			// Token: 0x04000F93 RID: 3987
			public const string Type = "type";

			// Token: 0x04000F94 RID: 3988
			public const string CrossFilteringBehavior = "crossFilteringBehavior";

			// Token: 0x04000F95 RID: 3989
			public const string JoinOnDateBehavior = "joinOnDateBehavior";

			// Token: 0x04000F96 RID: 3990
			public const string RelyOnReferentialIntegrity = "relyOnReferentialIntegrity";

			// Token: 0x04000F97 RID: 3991
			public const string State = "state";

			// Token: 0x04000F98 RID: 3992
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000F99 RID: 3993
			public const string RefreshedTime = "refreshedTime";

			// Token: 0x04000F9A RID: 3994
			public const string SecurityFilteringBehavior = "securityFilteringBehavior";

			// Token: 0x04000F9B RID: 3995
			public const string Annotations = "annotations";

			// Token: 0x04000F9C RID: 3996
			public const string ExtendedProperties = "extendedProperties";

			// Token: 0x04000F9D RID: 3997
			public const string ChangedProperties = "changedProperties";
		}

		// Token: 0x02000378 RID: 888
		public static class Measure
		{
			// Token: 0x04000F9E RID: 3998
			public const string Name = "name";

			// Token: 0x04000F9F RID: 3999
			public const string Description = "description";

			// Token: 0x04000FA0 RID: 4000
			public const string DataType = "dataType";

			// Token: 0x04000FA1 RID: 4001
			public const string Expression = "expression";

			// Token: 0x04000FA2 RID: 4002
			public const string FormatString = "formatString";

			// Token: 0x04000FA3 RID: 4003
			public const string IsHidden = "isHidden";

			// Token: 0x04000FA4 RID: 4004
			public const string State = "state";

			// Token: 0x04000FA5 RID: 4005
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000FA6 RID: 4006
			public const string StructureModifiedTime = "structureModifiedTime";

			// Token: 0x04000FA7 RID: 4007
			public const string IsSimpleMeasure = "isSimpleMeasure";

			// Token: 0x04000FA8 RID: 4008
			public const string ErrorMessage = "errorMessage";

			// Token: 0x04000FA9 RID: 4009
			public const string DisplayFolder = "displayFolder";

			// Token: 0x04000FAA RID: 4010
			public const string DataCategory = "dataCategory";

			// Token: 0x04000FAB RID: 4011
			public const string LineageTag = "lineageTag";

			// Token: 0x04000FAC RID: 4012
			public const string SourceLineageTag = "sourceLineageTag";

			// Token: 0x04000FAD RID: 4013
			public const string KPI = "kpi";

			// Token: 0x04000FAE RID: 4014
			public const string DetailRowsDefinition = "detailRowsDefinition";

			// Token: 0x04000FAF RID: 4015
			public const string FormatStringDefinition = "formatStringDefinition";

			// Token: 0x04000FB0 RID: 4016
			public const string Annotations = "annotations";

			// Token: 0x04000FB1 RID: 4017
			public const string ExtendedProperties = "extendedProperties";

			// Token: 0x04000FB2 RID: 4018
			public const string ChangedProperties = "changedProperties";
		}

		// Token: 0x02000379 RID: 889
		public static class Hierarchy
		{
			// Token: 0x04000FB3 RID: 4019
			public const string Name = "name";

			// Token: 0x04000FB4 RID: 4020
			public const string Description = "description";

			// Token: 0x04000FB5 RID: 4021
			public const string IsHidden = "isHidden";

			// Token: 0x04000FB6 RID: 4022
			public const string State = "state";

			// Token: 0x04000FB7 RID: 4023
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000FB8 RID: 4024
			public const string StructureModifiedTime = "structureModifiedTime";

			// Token: 0x04000FB9 RID: 4025
			public const string RefreshedTime = "refreshedTime";

			// Token: 0x04000FBA RID: 4026
			public const string DisplayFolder = "displayFolder";

			// Token: 0x04000FBB RID: 4027
			public const string HideMembers = "hideMembers";

			// Token: 0x04000FBC RID: 4028
			public const string LineageTag = "lineageTag";

			// Token: 0x04000FBD RID: 4029
			public const string SourceLineageTag = "sourceLineageTag";

			// Token: 0x04000FBE RID: 4030
			public const string Levels = "levels";

			// Token: 0x04000FBF RID: 4031
			public const string Annotations = "annotations";

			// Token: 0x04000FC0 RID: 4032
			public const string ExtendedProperties = "extendedProperties";

			// Token: 0x04000FC1 RID: 4033
			public const string ExcludedArtifacts = "excludedArtifacts";

			// Token: 0x04000FC2 RID: 4034
			public const string ChangedProperties = "changedProperties";
		}

		// Token: 0x0200037A RID: 890
		public static class Level
		{
			// Token: 0x04000FC3 RID: 4035
			public const string Ordinal = "ordinal";

			// Token: 0x04000FC4 RID: 4036
			public const string Name = "name";

			// Token: 0x04000FC5 RID: 4037
			public const string Description = "description";

			// Token: 0x04000FC6 RID: 4038
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000FC7 RID: 4039
			public const string LineageTag = "lineageTag";

			// Token: 0x04000FC8 RID: 4040
			public const string SourceLineageTag = "sourceLineageTag";

			// Token: 0x04000FC9 RID: 4041
			public const string Column = "column";

			// Token: 0x04000FCA RID: 4042
			public const string Annotations = "annotations";

			// Token: 0x04000FCB RID: 4043
			public const string ExtendedProperties = "extendedProperties";

			// Token: 0x04000FCC RID: 4044
			public const string ChangedProperties = "changedProperties";
		}

		// Token: 0x0200037B RID: 891
		public static class Annotation
		{
			// Token: 0x04000FCD RID: 4045
			public const string Name = "name";

			// Token: 0x04000FCE RID: 4046
			public const string Value = "value";

			// Token: 0x04000FCF RID: 4047
			public const string ModifiedTime = "modifiedTime";
		}

		// Token: 0x0200037C RID: 892
		public static class KPI
		{
			// Token: 0x04000FD0 RID: 4048
			public const string Description = "description";

			// Token: 0x04000FD1 RID: 4049
			public const string TargetDescription = "targetDescription";

			// Token: 0x04000FD2 RID: 4050
			public const string TargetExpression = "targetExpression";

			// Token: 0x04000FD3 RID: 4051
			public const string TargetFormatString = "targetFormatString";

			// Token: 0x04000FD4 RID: 4052
			public const string StatusGraphic = "statusGraphic";

			// Token: 0x04000FD5 RID: 4053
			public const string StatusDescription = "statusDescription";

			// Token: 0x04000FD6 RID: 4054
			public const string StatusExpression = "statusExpression";

			// Token: 0x04000FD7 RID: 4055
			public const string TrendGraphic = "trendGraphic";

			// Token: 0x04000FD8 RID: 4056
			public const string TrendDescription = "trendDescription";

			// Token: 0x04000FD9 RID: 4057
			public const string TrendExpression = "trendExpression";

			// Token: 0x04000FDA RID: 4058
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000FDB RID: 4059
			public const string Annotations = "annotations";

			// Token: 0x04000FDC RID: 4060
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x0200037D RID: 893
		public static class Culture
		{
			// Token: 0x04000FDD RID: 4061
			public const string Name = "name";

			// Token: 0x04000FDE RID: 4062
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000FDF RID: 4063
			public const string StructureModifiedTime = "structureModifiedTime";

			// Token: 0x04000FE0 RID: 4064
			public const string LinguisticMetadata = "linguisticMetadata";

			// Token: 0x04000FE1 RID: 4065
			public const string Annotations = "annotations";

			// Token: 0x04000FE2 RID: 4066
			public const string ExtendedProperties = "extendedProperties";

			// Token: 0x04000FE3 RID: 4067
			public const string Translations = "translations";
		}

		// Token: 0x0200037E RID: 894
		public static class LinguisticMetadata
		{
			// Token: 0x04000FE4 RID: 4068
			public const string Content = "content";

			// Token: 0x04000FE5 RID: 4069
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000FE6 RID: 4070
			public const string ContentType = "contentType";

			// Token: 0x04000FE7 RID: 4071
			public const string Annotations = "annotations";

			// Token: 0x04000FE8 RID: 4072
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x0200037F RID: 895
		public static class Perspective
		{
			// Token: 0x04000FE9 RID: 4073
			public const string Name = "name";

			// Token: 0x04000FEA RID: 4074
			public const string Description = "description";

			// Token: 0x04000FEB RID: 4075
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000FEC RID: 4076
			public const string PerspectiveTables = "tables";

			// Token: 0x04000FED RID: 4077
			public const string Annotations = "annotations";

			// Token: 0x04000FEE RID: 4078
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x02000380 RID: 896
		public static class PerspectiveTable
		{
			// Token: 0x04000FEF RID: 4079
			public const string Name = "name";

			// Token: 0x04000FF0 RID: 4080
			public const string IncludeAll = "includeAll";

			// Token: 0x04000FF1 RID: 4081
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000FF2 RID: 4082
			public const string PerspectiveColumns = "columns";

			// Token: 0x04000FF3 RID: 4083
			public const string PerspectiveMeasures = "measures";

			// Token: 0x04000FF4 RID: 4084
			public const string PerspectiveHierarchies = "hierarchies";

			// Token: 0x04000FF5 RID: 4085
			public const string PerspectiveSets = "sets";

			// Token: 0x04000FF6 RID: 4086
			public const string Annotations = "annotations";

			// Token: 0x04000FF7 RID: 4087
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x02000381 RID: 897
		public static class PerspectiveColumn
		{
			// Token: 0x04000FF8 RID: 4088
			public const string Name = "name";

			// Token: 0x04000FF9 RID: 4089
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000FFA RID: 4090
			public const string Annotations = "annotations";

			// Token: 0x04000FFB RID: 4091
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x02000382 RID: 898
		public static class PerspectiveHierarchy
		{
			// Token: 0x04000FFC RID: 4092
			public const string Name = "name";

			// Token: 0x04000FFD RID: 4093
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04000FFE RID: 4094
			public const string Annotations = "annotations";

			// Token: 0x04000FFF RID: 4095
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x02000383 RID: 899
		public static class PerspectiveMeasure
		{
			// Token: 0x04001000 RID: 4096
			public const string Name = "name";

			// Token: 0x04001001 RID: 4097
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04001002 RID: 4098
			public const string Annotations = "annotations";

			// Token: 0x04001003 RID: 4099
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x02000384 RID: 900
		public static class ModelRole
		{
			// Token: 0x04001004 RID: 4100
			public const string Name = "name";

			// Token: 0x04001005 RID: 4101
			public const string Description = "description";

			// Token: 0x04001006 RID: 4102
			public const string ModelPermission = "modelPermission";

			// Token: 0x04001007 RID: 4103
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04001008 RID: 4104
			public const string Members = "members";

			// Token: 0x04001009 RID: 4105
			public const string TablePermissions = "tablePermissions";

			// Token: 0x0400100A RID: 4106
			public const string Annotations = "annotations";

			// Token: 0x0400100B RID: 4107
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x02000385 RID: 901
		public static class ModelRoleMember
		{
			// Token: 0x0400100C RID: 4108
			public const string MemberName = "memberName";

			// Token: 0x0400100D RID: 4109
			public const string MemberID = "memberId";

			// Token: 0x0400100E RID: 4110
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x0400100F RID: 4111
			public const string Annotations = "annotations";

			// Token: 0x04001010 RID: 4112
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x02000386 RID: 902
		public static class TablePermission
		{
			// Token: 0x04001011 RID: 4113
			public const string Name = "name";

			// Token: 0x04001012 RID: 4114
			public const string FilterExpression = "filterExpression";

			// Token: 0x04001013 RID: 4115
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04001014 RID: 4116
			public const string State = "state";

			// Token: 0x04001015 RID: 4117
			public const string ErrorMessage = "errorMessage";

			// Token: 0x04001016 RID: 4118
			public const string MetadataPermission = "metadataPermission";

			// Token: 0x04001017 RID: 4119
			public const string Annotations = "annotations";

			// Token: 0x04001018 RID: 4120
			public const string ExtendedProperties = "extendedProperties";

			// Token: 0x04001019 RID: 4121
			public const string ColumnPermissions = "columnPermissions";
		}

		// Token: 0x02000387 RID: 903
		public static class Variation
		{
			// Token: 0x0400101A RID: 4122
			public const string Name = "name";

			// Token: 0x0400101B RID: 4123
			public const string Description = "description";

			// Token: 0x0400101C RID: 4124
			public const string IsDefault = "isDefault";

			// Token: 0x0400101D RID: 4125
			public const string Relationship = "relationship";

			// Token: 0x0400101E RID: 4126
			public const string DefaultHierarchy = "defaultHierarchy";

			// Token: 0x0400101F RID: 4127
			public const string DefaultHierarchy_Table = "defaultHierarchyTable";

			// Token: 0x04001020 RID: 4128
			public const string DefaultHierarchy_Hierarchy = "defaultHierarchyHierarchy";

			// Token: 0x04001021 RID: 4129
			public const string DefaultColumn = "defaultColumn";

			// Token: 0x04001022 RID: 4130
			public const string DefaultColumn_Table = "defaultColumnTable";

			// Token: 0x04001023 RID: 4131
			public const string DefaultColumn_Column = "defaultColumnColumn";

			// Token: 0x04001024 RID: 4132
			public const string Annotations = "annotations";

			// Token: 0x04001025 RID: 4133
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x02000388 RID: 904
		public static class Set
		{
			// Token: 0x04001026 RID: 4134
			public const string Name = "name";

			// Token: 0x04001027 RID: 4135
			public const string Description = "description";

			// Token: 0x04001028 RID: 4136
			public const string Expression = "expression";

			// Token: 0x04001029 RID: 4137
			public const string IsDynamic = "isDynamic";

			// Token: 0x0400102A RID: 4138
			public const string IsHidden = "isHidden";

			// Token: 0x0400102B RID: 4139
			public const string State = "state";

			// Token: 0x0400102C RID: 4140
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x0400102D RID: 4141
			public const string StructureModifiedTime = "structureModifiedTime";

			// Token: 0x0400102E RID: 4142
			public const string ErrorMessage = "errorMessage";

			// Token: 0x0400102F RID: 4143
			public const string DisplayFolder = "displayFolder";

			// Token: 0x04001030 RID: 4144
			public const string Annotations = "annotations";

			// Token: 0x04001031 RID: 4145
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x02000389 RID: 905
		public static class PerspectiveSet
		{
			// Token: 0x04001032 RID: 4146
			public const string Name = "name";

			// Token: 0x04001033 RID: 4147
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04001034 RID: 4148
			public const string Annotations = "annotations";

			// Token: 0x04001035 RID: 4149
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x0200038A RID: 906
		public static class ExtendedProperty
		{
			// Token: 0x04001036 RID: 4150
			public const string Name = "name";

			// Token: 0x04001037 RID: 4151
			public const string Type = "type";

			// Token: 0x04001038 RID: 4152
			public const string ModifiedTime = "modifiedTime";
		}

		// Token: 0x0200038B RID: 907
		public static class NamedExpression
		{
			// Token: 0x04001039 RID: 4153
			public const string Name = "name";

			// Token: 0x0400103A RID: 4154
			public const string Description = "description";

			// Token: 0x0400103B RID: 4155
			public const string Kind = "kind";

			// Token: 0x0400103C RID: 4156
			public const string Expression = "expression";

			// Token: 0x0400103D RID: 4157
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x0400103E RID: 4158
			public const string MAttributes = "mAttributes";

			// Token: 0x0400103F RID: 4159
			public const string LineageTag = "lineageTag";

			// Token: 0x04001040 RID: 4160
			public const string SourceLineageTag = "sourceLineageTag";

			// Token: 0x04001041 RID: 4161
			public const string RemoteParameterName = "remoteParameterName";

			// Token: 0x04001042 RID: 4162
			public const string QueryGroup = "queryGroup";

			// Token: 0x04001043 RID: 4163
			public const string ParameterValuesColumn = "parameterValuesColumn";

			// Token: 0x04001044 RID: 4164
			public const string ExpressionSource = "expressionSource";

			// Token: 0x04001045 RID: 4165
			public const string Annotations = "annotations";

			// Token: 0x04001046 RID: 4166
			public const string ExtendedProperties = "extendedProperties";

			// Token: 0x04001047 RID: 4167
			public const string ExcludedArtifacts = "excludedArtifacts";
		}

		// Token: 0x0200038C RID: 908
		public static class ColumnPermission
		{
			// Token: 0x04001048 RID: 4168
			public const string Name = "name";

			// Token: 0x04001049 RID: 4169
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x0400104A RID: 4170
			public const string MetadataPermission = "metadataPermission";

			// Token: 0x0400104B RID: 4171
			public const string Annotations = "annotations";

			// Token: 0x0400104C RID: 4172
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x0200038D RID: 909
		public static class DetailRowsDefinition
		{
			// Token: 0x0400104D RID: 4173
			public const string Expression = "expression";

			// Token: 0x0400104E RID: 4174
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x0400104F RID: 4175
			public const string State = "state";

			// Token: 0x04001050 RID: 4176
			public const string ErrorMessage = "errorMessage";
		}

		// Token: 0x0200038E RID: 910
		public static class RelatedColumnDetails
		{
			// Token: 0x04001051 RID: 4177
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04001052 RID: 4178
			public const string GroupByColumns = "groupByColumns";

			// Token: 0x04001053 RID: 4179
			public const string GroupByColumn = "groupByColumn";
		}

		// Token: 0x0200038F RID: 911
		public static class GroupByColumn
		{
			// Token: 0x04001054 RID: 4180
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04001055 RID: 4181
			public const string GroupingColumn = "groupingColumn";
		}

		// Token: 0x02000390 RID: 912
		public static class CalculationGroup
		{
			// Token: 0x04001056 RID: 4182
			public const string Description = "description";

			// Token: 0x04001057 RID: 4183
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04001058 RID: 4184
			public const string Precedence = "precedence";

			// Token: 0x04001059 RID: 4185
			public const string MultipleOrEmptySelectionExpression = "multipleOrEmptySelectionExpression";

			// Token: 0x0400105A RID: 4186
			public const string NoSelectionExpression = "noSelectionExpression";

			// Token: 0x0400105B RID: 4187
			public const string Annotations = "annotations";

			// Token: 0x0400105C RID: 4188
			public const string CalculationItems = "calculationItems";
		}

		// Token: 0x02000391 RID: 913
		public static class CalculationItem
		{
			// Token: 0x0400105D RID: 4189
			public const string Name = "name";

			// Token: 0x0400105E RID: 4190
			public const string Description = "description";

			// Token: 0x0400105F RID: 4191
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04001060 RID: 4192
			public const string State = "state";

			// Token: 0x04001061 RID: 4193
			public const string ErrorMessage = "errorMessage";

			// Token: 0x04001062 RID: 4194
			public const string Expression = "expression";

			// Token: 0x04001063 RID: 4195
			public const string Ordinal = "ordinal";

			// Token: 0x04001064 RID: 4196
			public const string FormatStringDefinition = "formatStringDefinition";
		}

		// Token: 0x02000392 RID: 914
		public static class AlternateOf
		{
			// Token: 0x04001065 RID: 4197
			public const string Summarization = "summarization";

			// Token: 0x04001066 RID: 4198
			public const string BaseColumn = "baseColumn";

			// Token: 0x04001067 RID: 4199
			public const string BaseTable = "baseTable";

			// Token: 0x04001068 RID: 4200
			public const string Annotations = "annotations";
		}

		// Token: 0x02000393 RID: 915
		public static class RefreshPolicy
		{
			// Token: 0x04001069 RID: 4201
			public const string PolicyType = "policyType";

			// Token: 0x0400106A RID: 4202
			public const string Mode = "mode";

			// Token: 0x0400106B RID: 4203
			public const string Annotations = "annotations";

			// Token: 0x0400106C RID: 4204
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x02000394 RID: 916
		public static class FormatStringDefinition
		{
			// Token: 0x0400106D RID: 4205
			public const string Expression = "expression";

			// Token: 0x0400106E RID: 4206
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x0400106F RID: 4207
			public const string State = "state";

			// Token: 0x04001070 RID: 4208
			public const string ErrorMessage = "errorMessage";
		}

		// Token: 0x02000395 RID: 917
		public static class QueryGroup
		{
			// Token: 0x04001071 RID: 4209
			public const string Folder = "folder";

			// Token: 0x04001072 RID: 4210
			public const string Description = "description";

			// Token: 0x04001073 RID: 4211
			public const string Annotations = "annotations";
		}

		// Token: 0x02000396 RID: 918
		public static class AnalyticsAIMetadata
		{
			// Token: 0x04001074 RID: 4212
			public const string Name = "name";

			// Token: 0x04001075 RID: 4213
			public const string MeasureAnalysisDefinition = "measureAnalysisDefinition";
		}

		// Token: 0x02000397 RID: 919
		public static class ChangedProperty
		{
			// Token: 0x04001076 RID: 4214
			public const string Property = "property";
		}

		// Token: 0x02000398 RID: 920
		public static class ExcludedArtifact
		{
			// Token: 0x04001077 RID: 4215
			public const string ArtifactType = "artifactType";

			// Token: 0x04001078 RID: 4216
			public const string Reference = "reference";
		}

		// Token: 0x02000399 RID: 921
		public static class DataCoverageDefinition
		{
			// Token: 0x04001079 RID: 4217
			public const string Description = "description";

			// Token: 0x0400107A RID: 4218
			public const string Expression = "expression";

			// Token: 0x0400107B RID: 4219
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x0400107C RID: 4220
			public const string State = "state";

			// Token: 0x0400107D RID: 4221
			public const string ErrorMessage = "errorMessage";

			// Token: 0x0400107E RID: 4222
			public const string Annotations = "annotations";
		}

		// Token: 0x0200039A RID: 922
		public static class CalculationGroupExpression
		{
			// Token: 0x0400107F RID: 4223
			public const string Description = "description";

			// Token: 0x04001080 RID: 4224
			public const string Expression = "expression";

			// Token: 0x04001081 RID: 4225
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04001082 RID: 4226
			public const string State = "state";

			// Token: 0x04001083 RID: 4227
			public const string ErrorMessage = "errorMessage";

			// Token: 0x04001084 RID: 4228
			public const string FormatStringDefinition = "formatStringDefinition";
		}

		// Token: 0x0200039B RID: 923
		public static class Calendar
		{
			// Token: 0x04001085 RID: 4229
			public const string Name = "name";

			// Token: 0x04001086 RID: 4230
			public const string Description = "description";

			// Token: 0x04001087 RID: 4231
			public const string LineageTag = "lineageTag";

			// Token: 0x04001088 RID: 4232
			public const string SourceLineageTag = "sourceLineageTag";

			// Token: 0x04001089 RID: 4233
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x0400108A RID: 4234
			public const string TimeUnitColumnAssociations = "timeUnitColumnAssociations";
		}

		// Token: 0x0200039C RID: 924
		public static class TimeUnitColumnAssociation
		{
			// Token: 0x0400108B RID: 4235
			public const string TimeUnit = "timeUnit";

			// Token: 0x0400108C RID: 4236
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x0400108D RID: 4237
			public const string PrimaryColumn = "primaryColumn";

			// Token: 0x0400108E RID: 4238
			public const string AssociatedColumns = "associatedColumns";

			// Token: 0x0400108F RID: 4239
			public const string AssociatedColumn = "associatedColumn";
		}

		// Token: 0x0200039D RID: 925
		public static class Function
		{
			// Token: 0x04001090 RID: 4240
			public const string Name = "name";

			// Token: 0x04001091 RID: 4241
			public const string Description = "description";

			// Token: 0x04001092 RID: 4242
			public const string Expression = "expression";

			// Token: 0x04001093 RID: 4243
			public const string IsHidden = "isHidden";

			// Token: 0x04001094 RID: 4244
			public const string State = "state";

			// Token: 0x04001095 RID: 4245
			public const string ErrorMessage = "errorMessage";

			// Token: 0x04001096 RID: 4246
			public const string ModifiedTime = "modifiedTime";

			// Token: 0x04001097 RID: 4247
			public const string StructureModifiedTime = "structureModifiedTime";

			// Token: 0x04001098 RID: 4248
			public const string LineageTag = "lineageTag";

			// Token: 0x04001099 RID: 4249
			public const string SourceLineageTag = "sourceLineageTag";

			// Token: 0x0400109A RID: 4250
			public const string Annotations = "annotations";

			// Token: 0x0400109B RID: 4251
			public const string ExtendedProperties = "extendedProperties";

			// Token: 0x0400109C RID: 4252
			public const string ChangedProperties = "changedProperties";
		}

		// Token: 0x0200039E RID: 926
		public static class BindingInfo
		{
			// Token: 0x0400109D RID: 4253
			public const string Name = "name";

			// Token: 0x0400109E RID: 4254
			public const string Description = "description";

			// Token: 0x0400109F RID: 4255
			public const string Type = "type";

			// Token: 0x040010A0 RID: 4256
			public const string Annotations = "annotations";

			// Token: 0x040010A1 RID: 4257
			public const string ExtendedProperties = "extendedProperties";
		}

		// Token: 0x0200039F RID: 927
		public static class ProviderDataSource
		{
			// Token: 0x040010A2 RID: 4258
			public const string ConnectionString = "connectionString";

			// Token: 0x040010A3 RID: 4259
			public const string ImpersonationMode = "impersonationMode";

			// Token: 0x040010A4 RID: 4260
			public const string Account = "account";

			// Token: 0x040010A5 RID: 4261
			public const string Password = "password";

			// Token: 0x040010A6 RID: 4262
			public const string Isolation = "isolation";

			// Token: 0x040010A7 RID: 4263
			public const string Timeout = "timeout";

			// Token: 0x040010A8 RID: 4264
			public const string Provider = "provider";
		}

		// Token: 0x020003A0 RID: 928
		public static class StructuredDataSource
		{
			// Token: 0x040010A9 RID: 4265
			public const string ConnectionDetails = "connectionDetails";

			// Token: 0x040010AA RID: 4266
			public const string Options = "options";

			// Token: 0x040010AB RID: 4267
			public const string Credential = "credential";

			// Token: 0x040010AC RID: 4268
			public const string ContextExpression = "contextExpression";
		}

		// Token: 0x020003A1 RID: 929
		public static class DataColumn
		{
			// Token: 0x040010AD RID: 4269
			public const string IsDataTypeInferred = "isDataTypeInferred";

			// Token: 0x040010AE RID: 4270
			public const string SourceColumn = "sourceColumn";
		}

		// Token: 0x020003A2 RID: 930
		public static class CalculatedTableColumn
		{
			// Token: 0x040010AF RID: 4271
			public const string IsNameInferred = "isNameInferred";

			// Token: 0x040010B0 RID: 4272
			public const string IsDataTypeInferred = "isDataTypeInferred";

			// Token: 0x040010B1 RID: 4273
			public const string SourceColumn = "sourceColumn";

			// Token: 0x040010B2 RID: 4274
			public const string ColumnOrigin = "columnOrigin";

			// Token: 0x040010B3 RID: 4275
			public const string ColumnOrigin_Table = "columnOriginTable";

			// Token: 0x040010B4 RID: 4276
			public const string ColumnOrigin_Column = "columnOriginColumn";
		}

		// Token: 0x020003A3 RID: 931
		public static class CalculatedColumn
		{
			// Token: 0x040010B5 RID: 4277
			public const string IsDataTypeInferred = "isDataTypeInferred";

			// Token: 0x040010B6 RID: 4278
			public const string Expression = "expression";

			// Token: 0x040010B7 RID: 4279
			public const string EvaluationBehavior = "evaluationBehavior";
		}

		// Token: 0x020003A4 RID: 932
		public static class SingleColumnRelationship
		{
			// Token: 0x040010B8 RID: 4280
			public const string FromCardinality = "fromCardinality";

			// Token: 0x040010B9 RID: 4281
			public const string ToCardinality = "toCardinality";

			// Token: 0x040010BA RID: 4282
			public const string FromTable = "fromTable";

			// Token: 0x040010BB RID: 4283
			public const string FromColumn = "fromColumn";

			// Token: 0x040010BC RID: 4284
			public const string FromColumnLinkPrefix = "from";

			// Token: 0x040010BD RID: 4285
			public const string ToTable = "toTable";

			// Token: 0x040010BE RID: 4286
			public const string ToColumn = "toColumn";

			// Token: 0x040010BF RID: 4287
			public const string ToColumnLinkPrefix = "to";
		}

		// Token: 0x020003A5 RID: 933
		public static class ExternalModelRoleMember
		{
			// Token: 0x040010C0 RID: 4288
			public const string IdentityProvider = "identityProvider";

			// Token: 0x040010C1 RID: 4289
			public const string MemberType = "memberType";
		}

		// Token: 0x020003A6 RID: 934
		public static class StringExtendedProperty
		{
			// Token: 0x040010C2 RID: 4290
			public const string Value = "value";
		}

		// Token: 0x020003A7 RID: 935
		public static class JsonExtendedProperty
		{
			// Token: 0x040010C3 RID: 4291
			public const string Value = "value";
		}

		// Token: 0x020003A8 RID: 936
		public static class BasicRefreshPolicy
		{
			// Token: 0x040010C4 RID: 4292
			public const string RollingWindowGranularity = "rollingWindowGranularity";

			// Token: 0x040010C5 RID: 4293
			public const string RollingWindowPeriods = "rollingWindowPeriods";

			// Token: 0x040010C6 RID: 4294
			public const string IncrementalGranularity = "incrementalGranularity";

			// Token: 0x040010C7 RID: 4295
			public const string IncrementalPeriods = "incrementalPeriods";

			// Token: 0x040010C8 RID: 4296
			public const string IncrementalPeriodsOffset = "incrementalPeriodsOffset";

			// Token: 0x040010C9 RID: 4297
			public const string PollingExpression = "pollingExpression";

			// Token: 0x040010CA RID: 4298
			public const string SourceExpression = "sourceExpression";
		}

		// Token: 0x020003A9 RID: 937
		public static class DataBindingHint
		{
			// Token: 0x040010CB RID: 4299
			public const string ConnectionId = "connectionId";
		}

		// Token: 0x020003AA RID: 938
		public static class PartitionSource
		{
			// Token: 0x040010CC RID: 4300
			public const string Type = "type";

			// Token: 0x040010CD RID: 4301
			public const string Query = "query";

			// Token: 0x040010CE RID: 4302
			public const string DataSource = "dataSource";

			// Token: 0x040010CF RID: 4303
			public const string Expression = "expression";

			// Token: 0x040010D0 RID: 4304
			public const string RetainDataTillForceCalculate = "retainDataTillForceCalculate";

			// Token: 0x040010D1 RID: 4305
			public const string Attributes = "attributes";

			// Token: 0x040010D2 RID: 4306
			public const string EntityName = "entityName";

			// Token: 0x040010D3 RID: 4307
			public const string SchemaName = "schemaName";

			// Token: 0x040010D4 RID: 4308
			public const string ExpressionSource = "expressionSource";

			// Token: 0x040010D5 RID: 4309
			public const string Start = "start";

			// Token: 0x040010D6 RID: 4310
			public const string End = "end";

			// Token: 0x040010D7 RID: 4311
			public const string Granularity = "granularity";

			// Token: 0x040010D8 RID: 4312
			public const string RefreshBookmark = "refreshBookmark";

			// Token: 0x040010D9 RID: 4313
			public const string Location = "location";
		}

		// Token: 0x020003AB RID: 939
		public static class DataRefreshOverride
		{
			// Token: 0x040010DA RID: 4314
			public const string Scope = "scope";

			// Token: 0x040010DB RID: 4315
			public const string OriginalObject = "originalObject";

			// Token: 0x040010DC RID: 4316
			public const string DataSources = "dataSources";

			// Token: 0x040010DD RID: 4317
			public const string Columns = "columns";

			// Token: 0x040010DE RID: 4318
			public const string Partitions = "partitions";

			// Token: 0x040010DF RID: 4319
			public const string Expressions = "expressions";
		}

		// Token: 0x020003AC RID: 940
		public static class JsonCommands
		{
			// Token: 0x040010E0 RID: 4320
			public const string CreateCommand = "create";

			// Token: 0x040010E1 RID: 4321
			public const string CreateOrReplaceCommand = "createOrReplace";

			// Token: 0x040010E2 RID: 4322
			public const string AlterCommand = "alter";

			// Token: 0x040010E3 RID: 4323
			public const string DeleteCommand = "delete";

			// Token: 0x040010E4 RID: 4324
			public const string ExportCommand = "export";

			// Token: 0x040010E5 RID: 4325
			public const string RefreshCommand = "refresh";

			// Token: 0x040010E6 RID: 4326
			public const string SequenceCommand = "sequence";

			// Token: 0x040010E7 RID: 4327
			public const string BackupCommand = "backup";

			// Token: 0x040010E8 RID: 4328
			public const string RestoreCommand = "restore";

			// Token: 0x040010E9 RID: 4329
			public const string AttachCommand = "attach";

			// Token: 0x040010EA RID: 4330
			public const string DetachCommand = "detach";

			// Token: 0x040010EB RID: 4331
			public const string SynchronizeCommand = "synchronize";

			// Token: 0x040010EC RID: 4332
			public const string MergePartitionsCommand = "mergePartitions";

			// Token: 0x040010ED RID: 4333
			public const string ApplyAutomaticAggregationsCommand = "applyAutomaticAggregations";

			// Token: 0x040010EE RID: 4334
			public const string CreateCommandName = "Create";

			// Token: 0x040010EF RID: 4335
			public const string CreateOrReplaceCommandName = "CreateOrReplace";

			// Token: 0x040010F0 RID: 4336
			public const string AlterCommandName = "Alter";

			// Token: 0x040010F1 RID: 4337
			public const string DeleteCommandName = "Delete";

			// Token: 0x040010F2 RID: 4338
			public const string ExportCommandName = "Export";

			// Token: 0x040010F3 RID: 4339
			public const string RefreshCommandName = "Refresh";

			// Token: 0x040010F4 RID: 4340
			public const string SequenceCommandName = "Sequence";

			// Token: 0x040010F5 RID: 4341
			public const string BackupCommandName = "Backup";

			// Token: 0x040010F6 RID: 4342
			public const string RestoreCommandName = "Restore";

			// Token: 0x040010F7 RID: 4343
			public const string AttachCommandName = "Attach";

			// Token: 0x040010F8 RID: 4344
			public const string DetachCommandName = "Detach";

			// Token: 0x040010F9 RID: 4345
			public const string SynchronizeCommandName = "Synchronize";

			// Token: 0x040010FA RID: 4346
			public const string MergePartitionsCommandName = "MergePartitions";

			// Token: 0x040010FB RID: 4347
			public const string ApplyAutomaticAggregationsCommandName = "ApplyAutomaticAggregations";

			// Token: 0x040010FC RID: 4348
			public const string Database = "database";

			// Token: 0x040010FD RID: 4349
			public const string Object = "object";

			// Token: 0x040010FE RID: 4350
			public const string Options = "options";

			// Token: 0x040010FF RID: 4351
			public const string Folder = "folder";

			// Token: 0x04001100 RID: 4352
			public const string Password = "password";

			// Token: 0x04001101 RID: 4353
			public const string ReadWriteMode = "readWriteMode";

			// Token: 0x04001102 RID: 4354
			public const string File = "file";

			// Token: 0x04001103 RID: 4355
			public const string ApplyCompression = "applyCompression";

			// Token: 0x04001104 RID: 4356
			public const string AllowOverwrite = "allowOverwrite";

			// Token: 0x04001105 RID: 4357
			public const string ParentObject = "parentObject";

			// Token: 0x04001106 RID: 4358
			public const string Layout = "layout";

			// Token: 0x04001107 RID: 4359
			public const string Target = "target";

			// Token: 0x04001108 RID: 4360
			public const string Sources = "sources";

			// Token: 0x04001109 RID: 4361
			public const string Type = "type";

			// Token: 0x0400110A RID: 4362
			public const string ApplyRefreshPolicy = "applyRefreshPolicy";

			// Token: 0x0400110B RID: 4363
			public const string EffectiveDate = "effectiveDate";

			// Token: 0x0400110C RID: 4364
			public const string Objects = "objects";

			// Token: 0x0400110D RID: 4365
			public const string Overrides = "overrides";

			// Token: 0x0400110E RID: 4366
			public const string DbStorageLocation = "dbStorageLocation";

			// Token: 0x0400110F RID: 4367
			public const string RestoreSecurity = "security";

			// Token: 0x04001110 RID: 4368
			public const string IgnoreIncompatibilities = "ignoreIncompatibilities";

			// Token: 0x04001111 RID: 4369
			public const string ForceRestore = "forceRestore";

			// Token: 0x04001112 RID: 4370
			public const string MaxParallelism = "maxParallelism";

			// Token: 0x04001113 RID: 4371
			public const string Operations = "operations";

			// Token: 0x04001114 RID: 4372
			public const string Source = "source";

			// Token: 0x04001115 RID: 4373
			public const string SynchronizeSecurity = "synchronizeSecurity";
		}

		// Token: 0x020003AD RID: 941
		public static class ObjectPath
		{
			// Token: 0x060026CE RID: 9934 RVA: 0x000EBCE6 File Offset: 0x000E9EE6
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static string GetObjectPathPropertyName(ObjectType objectType)
			{
				return objectType.ToString("G").ToJsonCase();
			}

			// Token: 0x060026CF RID: 9935 RVA: 0x000EBCFD File Offset: 0x000E9EFD
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static string Get1200SyleObjectPathPropertyName(string propertyName, ObjectType objectType)
			{
				return string.Format("{0}{1}", propertyName, objectType.ToString("G"));
			}

			// Token: 0x060026D0 RID: 9936 RVA: 0x000EBD1A File Offset: 0x000E9F1A
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static ObjectType GetObjectTypeFromPropertyName(string propertyName)
			{
				return (ObjectType)Enum.Parse(typeof(ObjectType), propertyName, true);
			}

			// Token: 0x04001116 RID: 4374
			public const string Database = "database";

			// Token: 0x04001117 RID: 4375
			public const string Model = "model";

			// Token: 0x04001118 RID: 4376
			public const string DataSource = "dataSource";

			// Token: 0x04001119 RID: 4377
			public const string Table = "table";

			// Token: 0x0400111A RID: 4378
			public const string Column = "column";

			// Token: 0x0400111B RID: 4379
			public const string AttributeHierarchy = "attributeHierarchy";

			// Token: 0x0400111C RID: 4380
			public const string Partition = "partition";

			// Token: 0x0400111D RID: 4381
			public const string Relationship = "relationship";

			// Token: 0x0400111E RID: 4382
			public const string Measure = "measure";

			// Token: 0x0400111F RID: 4383
			public const string Hierarchy = "hierarchy";

			// Token: 0x04001120 RID: 4384
			public const string Level = "level";

			// Token: 0x04001121 RID: 4385
			public const string Annotation = "annotation";

			// Token: 0x04001122 RID: 4386
			public const string KPI = "kpi";

			// Token: 0x04001123 RID: 4387
			public const string Culture = "culture";

			// Token: 0x04001124 RID: 4388
			public const string LinguisticMetadata = "linguisticMetadata";

			// Token: 0x04001125 RID: 4389
			public const string Perspective = "perspective";

			// Token: 0x04001126 RID: 4390
			public const string PerspectiveTable = "perspectiveTable";

			// Token: 0x04001127 RID: 4391
			public const string PerspectiveColumn = "perspectiveColumn";

			// Token: 0x04001128 RID: 4392
			public const string PerspectiveHierarchy = "perspectiveHierarchy";

			// Token: 0x04001129 RID: 4393
			public const string PerspectiveMeasure = "perspectiveMeasure";

			// Token: 0x0400112A RID: 4394
			public const string ModelRole = "role";

			// Token: 0x0400112B RID: 4395
			public const string ModelRoleMember = "roleMembership";

			// Token: 0x0400112C RID: 4396
			public const string TablePermission = "tablePermission";

			// Token: 0x0400112D RID: 4397
			public const string Variation = "variation";

			// Token: 0x0400112E RID: 4398
			public const string Set = "set";

			// Token: 0x0400112F RID: 4399
			public const string PerspectiveSet = "perspectiveSet";

			// Token: 0x04001130 RID: 4400
			public const string ExtendedProperty = "extendedProperty";

			// Token: 0x04001131 RID: 4401
			public const string NamedExpression = "expression";

			// Token: 0x04001132 RID: 4402
			public const string ColumnPermission = "columnPermission";

			// Token: 0x04001133 RID: 4403
			public const string DetailRowsDefinition = "detailRowsDefinition";

			// Token: 0x04001134 RID: 4404
			public const string RelatedColumnDetails = "relatedColumnDetails";

			// Token: 0x04001135 RID: 4405
			public const string GroupByColumn = "groupByColumn";

			// Token: 0x04001136 RID: 4406
			public const string CalculationGroup = "calculationGroup";

			// Token: 0x04001137 RID: 4407
			public const string CalculationItem = "calculationItem";

			// Token: 0x04001138 RID: 4408
			public const string AlternateOf = "alternateOf";

			// Token: 0x04001139 RID: 4409
			public const string RefreshPolicy = "refreshPolicy";

			// Token: 0x0400113A RID: 4410
			public const string FormatStringDefinition = "formatStringDefinition";

			// Token: 0x0400113B RID: 4411
			public const string QueryGroup = "queryGroup";

			// Token: 0x0400113C RID: 4412
			public const string AnalyticsAIMetadata = "analyticsAIMetadata";

			// Token: 0x0400113D RID: 4413
			public const string ChangedProperty = "changedProperty";

			// Token: 0x0400113E RID: 4414
			public const string ExcludedArtifact = "excludedArtifact";

			// Token: 0x0400113F RID: 4415
			public const string DataCoverageDefinition = "dataCoverageDefinition";

			// Token: 0x04001140 RID: 4416
			public const string CalculationGroupExpression = "calculationExpression";

			// Token: 0x04001141 RID: 4417
			public const string Calendar = "calendar";

			// Token: 0x04001142 RID: 4418
			public const string TimeUnitColumnAssociation = "timeUnitColumnAssociation";

			// Token: 0x04001143 RID: 4419
			public const string Function = "function";

			// Token: 0x04001144 RID: 4420
			public const string BindingInfo = "bindingInfo";
		}

		// Token: 0x020003AE RID: 942
		public static class Misc
		{
			// Token: 0x060026D1 RID: 9937 RVA: 0x000EBD32 File Offset: 0x000E9F32
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static string GetTranslatedPropertyName(TranslatedProperty property)
			{
				return string.Format("{0}{1}", "translated", property.ToString("G"));
			}

			// Token: 0x060026D2 RID: 9938 RVA: 0x000EBD53 File Offset: 0x000E9F53
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public static string GetTranslatedPropertyName(SerializationActivityContext context, TranslatedProperty property)
			{
				if (context.SerializationMode != MetadataSerializationMode.Json)
				{
					return property.ToString("G").ToJsonCase();
				}
				return string.Format("{0}{1}", "translated", property.ToString("G"));
			}

			// Token: 0x04001145 RID: 4421
			private const string JsonTranslatedPropertyPrefix = "translated";

			// Token: 0x04001146 RID: 4422
			public const string Model = "model";

			// Token: 0x04001147 RID: 4423
			public const string Name = "name";

			// Token: 0x04001148 RID: 4424
			public const string Description = "description";
		}

		// Token: 0x020003AF RID: 943
		public static class Schema
		{
			// Token: 0x04001149 RID: 4425
			public const string Description = "description";

			// Token: 0x0400114A RID: 4426
			public const string Type = "type";

			// Token: 0x0400114B RID: 4427
			public const string Properties = "properties";

			// Token: 0x0400114C RID: 4428
			public const string Items = "items";

			// Token: 0x0400114D RID: 4429
			public const string Format = "format";

			// Token: 0x0400114E RID: 4430
			public const string Enum = "enum";

			// Token: 0x0400114F RID: 4431
			public const string AnyOf = "anyOf";

			// Token: 0x04001150 RID: 4432
			public const string MinItems = "minItems";

			// Token: 0x04001151 RID: 4433
			public const string UniqueItems = "uniqueItems";

			// Token: 0x04001152 RID: 4434
			public const string AdditionalProperties = "additionalProperties";

			// Token: 0x04001153 RID: 4435
			public const string Required = "required";

			// Token: 0x04001154 RID: 4436
			public const string Ref = "$ref";

			// Token: 0x04001155 RID: 4437
			public const string Defs = "$defs";

			// Token: 0x04001156 RID: 4438
			public const string ElementType_Array = "array";

			// Token: 0x04001157 RID: 4439
			public const string ElementType_Object = "object";

			// Token: 0x04001158 RID: 4440
			public const string ElementType_String = "string";

			// Token: 0x04001159 RID: 4441
			public const string ElementType_Bool = "boolean";

			// Token: 0x0400115A RID: 4442
			public const string ElementType_Number = "number";

			// Token: 0x0400115B RID: 4443
			public const string ElementType_Integer = "integer";

			// Token: 0x0400115C RID: 4444
			public const string FormatType_DateTime = "date-time";
		}
	}
}
