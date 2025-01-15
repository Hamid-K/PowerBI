using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000DC RID: 220
	[CompilerGenerated]
	internal class ValidationSR
	{
		// Token: 0x06000CA6 RID: 3238 RVA: 0x0002D50D File Offset: 0x0002B70D
		protected ValidationSR()
		{
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0002D515 File Offset: 0x0002B715
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x0002D51C File Offset: 0x0002B71C
		public static CultureInfo Culture
		{
			get
			{
				return ValidationSR.Keys.Culture;
			}
			set
			{
				ValidationSR.Keys.Culture = value;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0002D524 File Offset: 0x0002B724
		public static string RuleCategory_Database
		{
			get
			{
				return ValidationSR.Keys.GetString("RuleCategory_Database");
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0002D530 File Offset: 0x0002B730
		public static string RuleCategory_DataSource
		{
			get
			{
				return ValidationSR.Keys.GetString("RuleCategory_DataSource");
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0002D53C File Offset: 0x0002B73C
		public static string RuleCategory_Dimension
		{
			get
			{
				return ValidationSR.Keys.GetString("RuleCategory_Dimension");
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0002D548 File Offset: 0x0002B748
		public static string RuleCategory_Cube
		{
			get
			{
				return ValidationSR.Keys.GetString("RuleCategory_Cube");
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0002D554 File Offset: 0x0002B754
		public static string RuleCategory_PartitionAndAggregation
		{
			get
			{
				return ValidationSR.Keys.GetString("RuleCategory_PartitionAndAggregation");
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0002D560 File Offset: 0x0002B760
		public static string DataSourceView_DataSourceNotSpecified
		{
			get
			{
				return ValidationSR.Keys.GetString("DataSourceView_DataSourceNotSpecified");
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0002D56C File Offset: 0x0002B76C
		public static string Action_TargetNotDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("Action_TargetNotDefined");
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0002D578 File Offset: 0x0002B778
		public static string Action_ExpressionNotDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("Action_ExpressionNotDefined");
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0002D584 File Offset: 0x0002B784
		public static string Cube_NoDimensionsDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("Cube_NoDimensionsDefined");
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0002D590 File Offset: 0x0002B790
		public static string Cube_NoDefaultMdxScriptDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("Cube_NoDefaultMdxScriptDefined");
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0002D59C File Offset: 0x0002B79C
		public static string Cube_PerspectiveNotAllow
		{
			get
			{
				return ValidationSR.Keys.GetString("Cube_PerspectiveNotAllow");
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0002D5A8 File Offset: 0x0002B7A8
		public static string DatabasePermission_WriteCannotBeAllowed
		{
			get
			{
				return ValidationSR.Keys.GetString("DatabasePermission_WriteCannotBeAllowed");
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0002D5B4 File Offset: 0x0002B7B4
		public static string Database_AssemblyNotAllowed
		{
			get
			{
				return ValidationSR.Keys.GetString("Database_AssemblyNotAllowed");
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0002D5C0 File Offset: 0x0002B7C0
		public static string Server_AssemblyNotAllowed
		{
			get
			{
				return ValidationSR.Keys.GetString("Server_AssemblyNotAllowed");
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0002D5CC File Offset: 0x0002B7CC
		public static string Server_BackupNotAllowed
		{
			get
			{
				return ValidationSR.Keys.GetString("Server_BackupNotAllowed");
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0002D5D8 File Offset: 0x0002B7D8
		public static string Server_RestoreNotAllowed
		{
			get
			{
				return ValidationSR.Keys.GetString("Server_RestoreNotAllowed");
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0002D5E4 File Offset: 0x0002B7E4
		public static string Dimension_NoHierarchiesDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_NoHierarchiesDefined");
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0002D5F0 File Offset: 0x0002B7F0
		public static string Dimension_NoAttributesDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_NoAttributesDefined");
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0002D5FC File Offset: 0x0002B7FC
		public static string Dimension_NoKeyAttributeDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_NoKeyAttributeDefined");
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0002D608 File Offset: 0x0002B808
		public static string Dimension_AttributesFormLoops
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_AttributesFormLoops");
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0002D614 File Offset: 0x0002B814
		public static string Dimension_WritebackRequiresDataSourceViewBinding
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_WritebackRequiresDataSourceViewBinding");
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0002D620 File Offset: 0x0002B820
		public static string Dimension_WritebackOnlyPossibleForStarSchemas
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_WritebackOnlyPossibleForStarSchemas");
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0002D62C File Offset: 0x0002B82C
		public static string Dimension_DM_ParentUniqueNameAttributeIsNotParent
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_DM_ParentUniqueNameAttributeIsNotParent");
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0002D638 File Offset: 0x0002B838
		public static string Dimension_DM_NodeUniqueNameAttributeIsNotKey
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_DM_NodeUniqueNameAttributeIsNotKey");
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0002D644 File Offset: 0x0002B844
		public static string Dimension_DM_AttributeIsNotRelatedWithTheKey
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_DM_AttributeIsNotRelatedWithTheKey");
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0002D650 File Offset: 0x0002B850
		public static string Dimension_LinkedMeasureNotAllow
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_LinkedMeasureNotAllow");
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0002D65C File Offset: 0x0002B85C
		public static string Dimension_WritebackNotAllow
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_WritebackNotAllow");
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0002D668 File Offset: 0x0002B868
		public static string Attribute_KeyColumns_Empty
		{
			get
			{
				return ValidationSR.Keys.GetString("Attribute_KeyColumns_Empty");
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0002D674 File Offset: 0x0002B874
		public static string Attribute_ParentAttributeMustBeRelatedToTheKeyAttribute
		{
			get
			{
				return ValidationSR.Keys.GetString("Attribute_ParentAttributeMustBeRelatedToTheKeyAttribute");
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0002D680 File Offset: 0x0002B880
		public static string Attribute_ParentAttributeCannotHaveAttributeRelationships
		{
			get
			{
				return ValidationSR.Keys.GetString("Attribute_ParentAttributeCannotHaveAttributeRelationships");
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0002D68C File Offset: 0x0002B88C
		public static string Attribute_KeyRequiresAttributeHierarchyEnabled
		{
			get
			{
				return ValidationSR.Keys.GetString("Attribute_KeyRequiresAttributeHierarchyEnabled");
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0002D698 File Offset: 0x0002B898
		public static string Attribute_AttributeRelationshipsRequireAttributeHierarchyEnabled
		{
			get
			{
				return ValidationSR.Keys.GetString("Attribute_AttributeRelationshipsRequireAttributeHierarchyEnabled");
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0002D6A4 File Offset: 0x0002B8A4
		public static string Attribute_OrderByAttribute_IsSelf
		{
			get
			{
				return ValidationSR.Keys.GetString("Attribute_OrderByAttribute_IsSelf");
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0002D6B0 File Offset: 0x0002B8B0
		public static string Attribute_Type_ShouldBeDaysForTheKeyOfATimeDimension
		{
			get
			{
				return ValidationSR.Keys.GetString("Attribute_Type_ShouldBeDaysForTheKeyOfATimeDimension");
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0002D6BC File Offset: 0x0002B8BC
		public static string Attribute_DiscretizationMethod_ShouldBeNoneForWriteEnabledDimensions
		{
			get
			{
				return ValidationSR.Keys.GetString("Attribute_DiscretizationMethod_ShouldBeNoneForWriteEnabledDimensions");
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0002D6C8 File Offset: 0x0002B8C8
		public static string Attribute_BrowsableRelatedAttributesRequireIsAggregatable
		{
			get
			{
				return ValidationSR.Keys.GetString("Attribute_BrowsableRelatedAttributesRequireIsAggregatable");
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0002D6D4 File Offset: 0x0002B8D4
		public static string AttributeRelationship_CreatesLoops
		{
			get
			{
				return ValidationSR.Keys.GetString("AttributeRelationship_CreatesLoops");
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0002D6E0 File Offset: 0x0002B8E0
		public static string Hierarchy_NoLevelsDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("Hierarchy_NoLevelsDefined");
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0002D6EC File Offset: 0x0002B8EC
		public static string Hierarchy_LevelBasedOnAttributeMarkedAsParentMustNotBeInHierarchy
		{
			get
			{
				return ValidationSR.Keys.GetString("Hierarchy_LevelBasedOnAttributeMarkedAsParentMustNotBeInHierarchy");
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0002D6F8 File Offset: 0x0002B8F8
		public static string Hierarchy_NoneAggregatableLevelCantHaveLevelsAboveIt
		{
			get
			{
				return ValidationSR.Keys.GetString("Hierarchy_NoneAggregatableLevelCantHaveLevelsAboveIt");
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0002D704 File Offset: 0x0002B904
		public static string Level_SourceAttributeIsUnknown
		{
			get
			{
				return ValidationSR.Keys.GetString("Level_SourceAttributeIsUnknown");
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0002D710 File Offset: 0x0002B910
		public static string Level_SourceAttributeOfParentChildLevelIsNotTheKeyAttribute
		{
			get
			{
				return ValidationSR.Keys.GetString("Level_SourceAttributeOfParentChildLevelIsNotTheKeyAttribute");
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0002D71C File Offset: 0x0002B91C
		public static string Level_SourceAttributeDoesntHaveAttributeHierarchyEnabled
		{
			get
			{
				return ValidationSR.Keys.GetString("Level_SourceAttributeDoesntHaveAttributeHierarchyEnabled");
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002D728 File Offset: 0x0002B928
		public static string Level_ParentAttributeDoesntHaveAttributeHierarchyEnabled
		{
			get
			{
				return ValidationSR.Keys.GetString("Level_ParentAttributeDoesntHaveAttributeHierarchyEnabled");
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0002D734 File Offset: 0x0002B934
		public static string DimensionPermission_WriteAccessRequiresRead
		{
			get
			{
				return ValidationSR.Keys.GetString("DimensionPermission_WriteAccessRequiresRead");
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0002D740 File Offset: 0x0002B940
		public static string DimensionPermission_WriteAccessRequiresNoAllowedOrDeniedSets
		{
			get
			{
				return ValidationSR.Keys.GetString("DimensionPermission_WriteAccessRequiresNoAllowedOrDeniedSets");
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0002D74C File Offset: 0x0002B94C
		public static string DimensionPermission_AccessNoneRequiresAllAttributesAggregatable
		{
			get
			{
				return ValidationSR.Keys.GetString("DimensionPermission_AccessNoneRequiresAllAttributesAggregatable");
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0002D758 File Offset: 0x0002B958
		public static string MiningStructure_KeyTimeNotDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("MiningStructure_KeyTimeNotDefined");
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0002D764 File Offset: 0x0002B964
		public static string MiningStructure_KeyTimeNotAllowed
		{
			get
			{
				return ValidationSR.Keys.GetString("MiningStructure_KeyTimeNotAllowed");
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0002D770 File Offset: 0x0002B970
		public static string MiningStructure_MultipleKeyTimeNotAllowed
		{
			get
			{
				return ValidationSR.Keys.GetString("MiningStructure_MultipleKeyTimeNotAllowed");
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0002D77C File Offset: 0x0002B97C
		public static string MiningModel_KeyColumnNotDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("MiningModel_KeyColumnNotDefined");
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0002D788 File Offset: 0x0002B988
		public static string MiningModel_PredictColumnNotDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("MiningModel_PredictColumnNotDefined");
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0002D794 File Offset: 0x0002B994
		public static string MiningModelColumn_NoNestedColumnShouldBeDefinedBecauseSourceIsScalar
		{
			get
			{
				return ValidationSR.Keys.GetString("MiningModelColumn_NoNestedColumnShouldBeDefinedBecauseSourceIsScalar");
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0002D7A0 File Offset: 0x0002B9A0
		public static string MiningModelColumn_Columns_KeyNotDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("MiningModelColumn_Columns_KeyNotDefined");
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0002D7AC File Offset: 0x0002B9AC
		public static string MiningModelColumn_NoFilterShouldBeDefinedBecauseSourceIsScalar
		{
			get
			{
				return ValidationSR.Keys.GetString("MiningModelColumn_NoFilterShouldBeDefinedBecauseSourceIsScalar");
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0002D7B8 File Offset: 0x0002B9B8
		public static string MiningStructureColumn_SourceCubeNeedsToBeTheSameAsForTheMiningStructure
		{
			get
			{
				return ValidationSR.Keys.GetString("MiningStructureColumn_SourceCubeNeedsToBeTheSameAsForTheMiningStructure");
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0002D7C4 File Offset: 0x0002B9C4
		public static string MiningStructureColumn_Columns_KeyNotTextType
		{
			get
			{
				return ValidationSR.Keys.GetString("MiningStructureColumn_Columns_KeyNotTextType");
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0002D7D0 File Offset: 0x0002B9D0
		public static string MiningStructureColumns_KeyColumnNotDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("MiningStructureColumns_KeyColumnNotDefined");
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0002D7DC File Offset: 0x0002B9DC
		public static string Kpi_Value_Missing
		{
			get
			{
				return ValidationSR.Keys.GetString("Kpi_Value_Missing");
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0002D7E8 File Offset: 0x0002B9E8
		public static string MeasureGroup_NoMeasuresDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureGroup_NoMeasuresDefined");
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0002D7F4 File Offset: 0x0002B9F4
		public static string MeasureGroup_SemiadditiveMeasureRequiresTimeDimension
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureGroup_SemiadditiveMeasureRequiresTimeDimension");
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0002D800 File Offset: 0x0002BA00
		public static string MeasureGroup_SourceMeasureGroupIsInTheSameCube
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureGroup_SourceMeasureGroupIsInTheSameCube");
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0002D80C File Offset: 0x0002BA0C
		public static string MeasureGroup_SourceIsSelf
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureGroup_SourceIsSelf");
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0002D818 File Offset: 0x0002BA18
		public static string MeasureGroup_DegenerateDimensionsAreBasedOnDifferentDimensions
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureGroup_DegenerateDimensionsAreBasedOnDifferentDimensions");
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0002D824 File Offset: 0x0002BA24
		public static string MeasureGroup_AtMostTwoNonWritebackPartition
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureGroup_AtMostTwoNonWritebackPartition");
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0002D830 File Offset: 0x0002BA30
		public static string MeasureGroup_LinkingToAnotherDatabaseNotAllowed
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureGroup_LinkingToAnotherDatabaseNotAllowed");
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0002D83C File Offset: 0x0002BA3C
		public static string Measure_DistinctCountRequiresNumericDataType
		{
			get
			{
				return ValidationSR.Keys.GetString("Measure_DistinctCountRequiresNumericDataType");
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0002D848 File Offset: 0x0002BA48
		public static string Measure_ByAccountRequiresAccountDimensionAndAttribute
		{
			get
			{
				return ValidationSR.Keys.GetString("Measure_ByAccountRequiresAccountDimensionAndAttribute");
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0002D854 File Offset: 0x0002BA54
		public static string RegularMeasureGroupDimension_GranularityNotDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("RegularMeasureGroupDimension_GranularityNotDefined");
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0002D860 File Offset: 0x0002BA60
		public static string RegularMeasureGroupDimension_MultipleGranularitiesDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("RegularMeasureGroupDimension_MultipleGranularitiesDefined");
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0002D86C File Offset: 0x0002BA6C
		public static string Partition_RemotePartitionNotAllowed
		{
			get
			{
				return ValidationSR.Keys.GetString("Partition_RemotePartitionNotAllowed");
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0002D878 File Offset: 0x0002BA78
		public static string ReferenceMeasureGroupDimension_MaterializedCannotHaveIntermediateTimeDimension
		{
			get
			{
				return ValidationSR.Keys.GetString("ReferenceMeasureGroupDimension_MaterializedCannotHaveIntermediateTimeDimension");
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0002D884 File Offset: 0x0002BA84
		public static string DataMiningMeasureGroupDimension_CaseCubeDimensionIsInvalid
		{
			get
			{
				return ValidationSR.Keys.GetString("DataMiningMeasureGroupDimension_CaseCubeDimensionIsInvalid");
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0002D890 File Offset: 0x0002BA90
		public static string DataMiningMeasureGroupDimension_CaseDimensionsShouldMatch
		{
			get
			{
				return ValidationSR.Keys.GetString("DataMiningMeasureGroupDimension_CaseDimensionsShouldMatch");
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0002D89C File Offset: 0x0002BA9C
		public static string MeasureGroupAttribute_AParentAttributeCannotBeGranularity
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureGroupAttribute_AParentAttributeCannotBeGranularity");
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0002D8A8 File Offset: 0x0002BAA8
		public static string MeasureGroupAttribute_GranularityRequiresAttributeHierarchyEnabled
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureGroupAttribute_GranularityRequiresAttributeHierarchyEnabled");
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0002D8B4 File Offset: 0x0002BAB4
		public static string Perspective_NoDimensionsDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("Perspective_NoDimensionsDefined");
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0002D8C0 File Offset: 0x0002BAC0
		public static string MdxScript_ThereAreOtherDefaultMdxScriptsInTheCube
		{
			get
			{
				return ValidationSR.Keys.GetString("MdxScript_ThereAreOtherDefaultMdxScriptsInTheCube");
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0002D8CC File Offset: 0x0002BACC
		public static string MdxScript_AnnotationsNotallowedOnCommand
		{
			get
			{
				return ValidationSR.Keys.GetString("MdxScript_AnnotationsNotallowedOnCommand");
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0002D8D8 File Offset: 0x0002BAD8
		public static string SourceIsMissing
		{
			get
			{
				return ValidationSR.Keys.GetString("SourceIsMissing");
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0002D8E4 File Offset: 0x0002BAE4
		public static string DataSourceIsUnknown
		{
			get
			{
				return ValidationSR.Keys.GetString("DataSourceIsUnknown");
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0002D8F0 File Offset: 0x0002BAF0
		public static string NameIsMissing
		{
			get
			{
				return ValidationSR.Keys.GetString("NameIsMissing");
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0002D8FC File Offset: 0x0002BAFC
		public static string IDIsMissing
		{
			get
			{
				return ValidationSR.Keys.GetString("IDIsMissing");
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0002D908 File Offset: 0x0002BB08
		public static string ParentIsMissing
		{
			get
			{
				return ValidationSR.Keys.GetString("ParentIsMissing");
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0002D914 File Offset: 0x0002BB14
		public static string NameColumnShouldBeDefinedIfMoreThanOneKeyColumnIsDefined
		{
			get
			{
				return ValidationSR.Keys.GetString("NameColumnShouldBeDefinedIfMoreThanOneKeyColumnIsDefined");
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0002D920 File Offset: 0x0002BB20
		public static string ParentServerIsMissing
		{
			get
			{
				return ValidationSR.Keys.GetString("ParentServerIsMissing");
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0002D92C File Offset: 0x0002BB2C
		public static string Translation_NotAllow
		{
			get
			{
				return ValidationSR.Keys.GetString("Translation_NotAllow");
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0002D938 File Offset: 0x0002BB38
		public static string ProactiveCaching_CanNotEnable
		{
			get
			{
				return ValidationSR.Keys.GetString("ProactiveCaching_CanNotEnable");
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0002D944 File Offset: 0x0002BB44
		public static string AggregationDesign_NotUsedByAnyPartition1
		{
			get
			{
				return ValidationSR.Keys.GetString("AggregationDesign_NotUsedByAnyPartition1");
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0002D950 File Offset: 0x0002BB50
		public static string AggregationDesign_NotUsedByAnyPartition2
		{
			get
			{
				return ValidationSR.Keys.GetString("AggregationDesign_NotUsedByAnyPartition2");
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0002D95C File Offset: 0x0002BB5C
		public static string MeasureGroup_HasTooManyAggregationDesigns
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureGroup_HasTooManyAggregationDesigns");
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0002D968 File Offset: 0x0002BB68
		public static string IntermediateGranularityNotAggregated1
		{
			get
			{
				return ValidationSR.Keys.GetString("IntermediateGranularityNotAggregated1");
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0002D974 File Offset: 0x0002BB74
		public static string CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup1
		{
			get
			{
				return ValidationSR.Keys.GetString("CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup1");
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0002D980 File Offset: 0x0002BB80
		public static string CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup2
		{
			get
			{
				return ValidationSR.Keys.GetString("CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup2");
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0002D98C File Offset: 0x0002BB8C
		public static string AggregationHasRelatedAttributes1
		{
			get
			{
				return ValidationSR.Keys.GetString("AggregationHasRelatedAttributes1");
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0002D998 File Offset: 0x0002BB98
		public static string Partition_RolapWithNoSlice
		{
			get
			{
				return ValidationSR.Keys.GetString("Partition_RolapWithNoSlice");
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0002D9A4 File Offset: 0x0002BBA4
		public static string Dimension_IsNotParentChildAndHasNoHierarchy
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_IsNotParentChildAndHasNoHierarchy");
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0002D9B0 File Offset: 0x0002BBB0
		public static string Hierarchy_IsUnNatural1
		{
			get
			{
				return ValidationSR.Keys.GetString("Hierarchy_IsUnNatural1");
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0002D9BC File Offset: 0x0002BBBC
		public static string Hierarchy_IsUnNatural2
		{
			get
			{
				return ValidationSR.Keys.GetString("Hierarchy_IsUnNatural2");
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0002D9C8 File Offset: 0x0002BBC8
		public static string DimensionIgnoresDuplicateKeys
		{
			get
			{
				return ValidationSR.Keys.GetString("DimensionIgnoresDuplicateKeys");
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0002D9D4 File Offset: 0x0002BBD4
		public static string DimensionHasAttributeUsedByLevelsAndWithVisibleHierarchy
		{
			get
			{
				return ValidationSR.Keys.GetString("DimensionHasAttributeUsedByLevelsAndWithVisibleHierarchy");
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0002D9E0 File Offset: 0x0002BBE0
		public static string Dimension_HasMultipleNonAggregatableAttributes1
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_HasMultipleNonAggregatableAttributes1");
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0002D9EC File Offset: 0x0002BBEC
		public static string AggregationBelowGranularity1
		{
			get
			{
				return ValidationSR.Keys.GetString("AggregationBelowGranularity1");
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0002D9F8 File Offset: 0x0002BBF8
		public static string DimensionAttribute_IsNonAggregatableInParentChild
		{
			get
			{
				return ValidationSR.Keys.GetString("DimensionAttribute_IsNonAggregatableInParentChild");
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0002DA04 File Offset: 0x0002BC04
		public static string NonAggregatableAttributeNeedsDefaultMember
		{
			get
			{
				return ValidationSR.Keys.GetString("NonAggregatableAttributeNeedsDefaultMember");
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0002DA10 File Offset: 0x0002BC10
		public static string Dimension_KeyAttributeOfParentChildShouldHaveHierarchyNotVisible
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_KeyAttributeOfParentChildShouldHaveHierarchyNotVisible");
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0002DA1C File Offset: 0x0002BC1C
		public static string Dimension_HasUnknownMemberSetToHidden
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_HasUnknownMemberSetToHidden");
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0002DA28 File Offset: 0x0002BC28
		public static string Dimension_RolapWithUnaryOperatorsOrCustomRollups
		{
			get
			{
				return ValidationSR.Keys.GetString("Dimension_RolapWithUnaryOperatorsOrCustomRollups");
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0002DA34 File Offset: 0x0002BC34
		public static string AttributeTypeAccountOrTimeNeedsMatchingDimension1
		{
			get
			{
				return ValidationSR.Keys.GetString("AttributeTypeAccountOrTimeNeedsMatchingDimension1");
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0002DA40 File Offset: 0x0002BC40
		public static string AttributeTypeNeedsMatchingDimension1
		{
			get
			{
				return ValidationSR.Keys.GetString("AttributeTypeNeedsMatchingDimension1");
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0002DA4C File Offset: 0x0002BC4C
		public static string DimensionTypeAccountOrTimeNeedsMatchingAttribute1
		{
			get
			{
				return ValidationSR.Keys.GetString("DimensionTypeAccountOrTimeNeedsMatchingAttribute1");
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0002DA58 File Offset: 0x0002BC58
		public static string DimensionTypeNeedsMatchingAttribute1
		{
			get
			{
				return ValidationSR.Keys.GetString("DimensionTypeNeedsMatchingAttribute1");
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0002DA64 File Offset: 0x0002BC64
		public static string AttributesTypesDontMatch1
		{
			get
			{
				return ValidationSR.Keys.GetString("AttributesTypesDontMatch1");
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0002DA70 File Offset: 0x0002BC70
		public static string AttributesTypesDontMatch2
		{
			get
			{
				return ValidationSR.Keys.GetString("AttributesTypesDontMatch2");
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x0002DA7C File Offset: 0x0002BC7C
		public static string LevelHasFewerMembersThanUpperLevel
		{
			get
			{
				return ValidationSR.Keys.GetString("LevelHasFewerMembersThanUpperLevel");
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0002DA88 File Offset: 0x0002BC88
		public static string DimensionAndRelationshipTypes
		{
			get
			{
				return ValidationSR.Keys.GetString("DimensionAndRelationshipTypes");
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0002DA94 File Offset: 0x0002BC94
		public static string RedundantRelationship1
		{
			get
			{
				return ValidationSR.Keys.GetString("RedundantRelationship1");
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0002DAA0 File Offset: 0x0002BCA0
		public static string RedundantRelationship2
		{
			get
			{
				return ValidationSR.Keys.GetString("RedundantRelationship2");
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0002DAAC File Offset: 0x0002BCAC
		public static string DiamondShapeRelationships1
		{
			get
			{
				return ValidationSR.Keys.GetString("DiamondShapeRelationships1");
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0002DAB8 File Offset: 0x0002BCB8
		public static string DiamondShapeRelationships2
		{
			get
			{
				return ValidationSR.Keys.GetString("DiamondShapeRelationships2");
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0002DAC4 File Offset: 0x0002BCC4
		public static string AttributeRelationshipName1
		{
			get
			{
				return ValidationSR.Keys.GetString("AttributeRelationshipName1");
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0002DAD0 File Offset: 0x0002BCD0
		public static string AttributeRelationshipName2
		{
			get
			{
				return ValidationSR.Keys.GetString("AttributeRelationshipName2");
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0002DADC File Offset: 0x0002BCDC
		public static string DimensionWithPollingQuery
		{
			get
			{
				return ValidationSR.Keys.GetString("DimensionWithPollingQuery");
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0002DAE8 File Offset: 0x0002BCE8
		public static string NoTimeDimension1
		{
			get
			{
				return ValidationSR.Keys.GetString("NoTimeDimension1");
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0002DAF4 File Offset: 0x0002BCF4
		public static string NoTimeDimension2
		{
			get
			{
				return ValidationSR.Keys.GetString("NoTimeDimension2");
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0002DB00 File Offset: 0x0002BD00
		public static string DimensionProcessByTable1
		{
			get
			{
				return ValidationSR.Keys.GetString("DimensionProcessByTable1");
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0002DB0C File Offset: 0x0002BD0C
		public static string DimensionProcessByTable2
		{
			get
			{
				return ValidationSR.Keys.GetString("DimensionProcessByTable2");
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0002DB18 File Offset: 0x0002BD18
		public static string Database_TooManyDimensionsWithSingleAttribute
		{
			get
			{
				return ValidationSR.Keys.GetString("Database_TooManyDimensionsWithSingleAttribute");
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0002DB24 File Offset: 0x0002BD24
		public static string DistinctCountMeasure
		{
			get
			{
				return ValidationSR.Keys.GetString("DistinctCountMeasure");
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0002DB30 File Offset: 0x0002BD30
		public static string ManyToManyHasLargeIntermediate
		{
			get
			{
				return ValidationSR.Keys.GetString("ManyToManyHasLargeIntermediate");
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0002DB3C File Offset: 0x0002BD3C
		public static string CubeWithSingleDimension
		{
			get
			{
				return ValidationSR.Keys.GetString("CubeWithSingleDimension");
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0002DB48 File Offset: 0x0002BD48
		public static string LinkedDimensionWithOutlineCalculations
		{
			get
			{
				return ValidationSR.Keys.GetString("LinkedDimensionWithOutlineCalculations");
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0002DB54 File Offset: 0x0002BD54
		public static string ReferencedMeasureGroupDimensionNotMaterialized
		{
			get
			{
				return ValidationSR.Keys.GetString("ReferencedMeasureGroupDimensionNotMaterialized");
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0002DB60 File Offset: 0x0002BD60
		public static string IndependentMeasureGroup1
		{
			get
			{
				return ValidationSR.Keys.GetString("IndependentMeasureGroup1");
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0002DB6C File Offset: 0x0002BD6C
		public static string IndependentMeasureGroup2
		{
			get
			{
				return ValidationSR.Keys.GetString("IndependentMeasureGroup2");
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0002DB78 File Offset: 0x0002BD78
		public static string PartitionWithPollingQuery
		{
			get
			{
				return ValidationSR.Keys.GetString("PartitionWithPollingQuery");
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0002DB84 File Offset: 0x0002BD84
		public static string MeasureGroupsWithTheSameDimensionalityAndGranularity1
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureGroupsWithTheSameDimensionalityAndGranularity1");
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0002DB90 File Offset: 0x0002BD90
		public static string CubeHasTooManyMeasureGroups1
		{
			get
			{
				return ValidationSR.Keys.GetString("CubeHasTooManyMeasureGroups1");
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0002DB9C File Offset: 0x0002BD9C
		public static string CubeHasTooManyMeasureGroups2
		{
			get
			{
				return ValidationSR.Keys.GetString("CubeHasTooManyMeasureGroups2");
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0002DBA8 File Offset: 0x0002BDA8
		public static string PerspectiveDefaultMeasureNotIncluded1
		{
			get
			{
				return ValidationSR.Keys.GetString("PerspectiveDefaultMeasureNotIncluded1");
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0002DBB4 File Offset: 0x0002BDB4
		public static string MeasureGroupWithSemiAdditiveMeasuresAndRolapDimensions1
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureGroupWithSemiAdditiveMeasuresAndRolapDimensions1");
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0002DBC0 File Offset: 0x0002BDC0
		public static string DotNetSqlClientProvider
		{
			get
			{
				return ValidationSR.Keys.GetString("DotNetSqlClientProvider");
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0002DBCC File Offset: 0x0002BDCC
		public static string UnsupportedOledbProvider
		{
			get
			{
				return ValidationSR.Keys.GetString("UnsupportedOledbProvider");
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0002DBD8 File Offset: 0x0002BDD8
		public static string MeasureGroupWithNoPartitions
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureGroupWithNoPartitions");
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0002DBE4 File Offset: 0x0002BDE4
		public static string PartitionIsRemoteRolap
		{
			get
			{
				return ValidationSR.Keys.GetString("PartitionIsRemoteRolap");
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0002DBF0 File Offset: 0x0002BDF0
		public static string AggregationDesignWithNoEstimatedRows1
		{
			get
			{
				return ValidationSR.Keys.GetString("AggregationDesignWithNoEstimatedRows1");
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0002DBFC File Offset: 0x0002BDFC
		public static string AggregationDesignWithNoEstimatedRows2
		{
			get
			{
				return ValidationSR.Keys.GetString("AggregationDesignWithNoEstimatedRows2");
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x0002DC08 File Offset: 0x0002BE08
		public static string AggregationsForTimeGranularityWithSemiAdditiveMeasures
		{
			get
			{
				return ValidationSR.Keys.GetString("AggregationsForTimeGranularityWithSemiAdditiveMeasures");
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x0002DC14 File Offset: 0x0002BE14
		public static string AggregationsForTimeGranularityWithOnlySemiAdditiveMeasures
		{
			get
			{
				return ValidationSR.Keys.GetString("AggregationsForTimeGranularityWithOnlySemiAdditiveMeasures");
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x0002DC20 File Offset: 0x0002BE20
		public static string AttributeRelationshipNamedDescription
		{
			get
			{
				return ValidationSR.Keys.GetString("AttributeRelationshipNamedDescription");
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0002DC2C File Offset: 0x0002BE2C
		public static string MeasureExpressionSKUError
		{
			get
			{
				return ValidationSR.Keys.GetString("MeasureExpressionSKUError");
			}
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0002DC38 File Offset: 0x0002BE38
		public static string ListStart(string firstElement)
		{
			return ValidationSR.Keys.GetString("ListStart", firstElement);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002DC45 File Offset: 0x0002BE45
		public static string List(string existingList, string nextElement)
		{
			return ValidationSR.Keys.GetString("List", existingList, nextElement);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0002DC53 File Offset: 0x0002BE53
		public static string Cube_MultipleDefaultMdxScriptsDefined(string list)
		{
			return ValidationSR.Keys.GetString("Cube_MultipleDefaultMdxScriptsDefined", list);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0002DC60 File Offset: 0x0002BE60
		public static string Cube_CubeDimensionsWithExcludeDimensionNameAndDuplicateHierarchies(string cubeDimensionName1, string cubeDimensionName2)
		{
			return ValidationSR.Keys.GetString("Cube_CubeDimensionsWithExcludeDimensionNameAndDuplicateHierarchies", cubeDimensionName1, cubeDimensionName2);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0002DC6E File Offset: 0x0002BE6E
		public static string Dimension_DependsOnDimensionIsUnknown(string dependsOnDimensionID)
		{
			return ValidationSR.Keys.GetString("Dimension_DependsOnDimensionIsUnknown", dependsOnDimensionID);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0002DC7B File Offset: 0x0002BE7B
		public static string Dimension_MultipleKeysDefined(string keyAttributesList)
		{
			return ValidationSR.Keys.GetString("Dimension_MultipleKeysDefined", keyAttributesList);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0002DC88 File Offset: 0x0002BE88
		public static string Dimension_MultipleParentsDefined(string parentAttributesList)
		{
			return ValidationSR.Keys.GetString("Dimension_MultipleParentsDefined", parentAttributesList);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0002DC95 File Offset: 0x0002BE95
		public static string Dimension_DM_DataMiningDimensionRequiresAnAttributeWithID(string id)
		{
			return ValidationSR.Keys.GetString("Dimension_DM_DataMiningDimensionRequiresAnAttributeWithID", id);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0002DCA2 File Offset: 0x0002BEA2
		public static string Attribute_KeyColumn_InvalidSourceType(int keyColumnIndex, string sourceType)
		{
			return ValidationSR.Keys.GetString("Attribute_KeyColumn_InvalidSourceType", keyColumnIndex, sourceType);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0002DCB5 File Offset: 0x0002BEB5
		public static string Attribute_KeyColumn_InvalidTable(int keyColumnIndex, string tableName, string dataSourceName)
		{
			return ValidationSR.Keys.GetString("Attribute_KeyColumn_InvalidTable", keyColumnIndex, tableName, dataSourceName);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0002DCC9 File Offset: 0x0002BEC9
		public static string Attribute_KeyColumn_InvalidColumn(int keyColumnIndex, string tableName, string columnName, string dataSourceName)
		{
			return ValidationSR.Keys.GetString("Attribute_KeyColumn_InvalidColumn", keyColumnIndex, tableName, columnName, dataSourceName);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0002DCDE File Offset: 0x0002BEDE
		public static string Attribute_NameColumn_InvalidSourceType(string sourceType)
		{
			return ValidationSR.Keys.GetString("Attribute_NameColumn_InvalidSourceType", sourceType);
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0002DCEB File Offset: 0x0002BEEB
		public static string Attribute_NameColumn_InvalidTable(string tableName, string dataSourceName)
		{
			return ValidationSR.Keys.GetString("Attribute_NameColumn_InvalidTable", tableName, dataSourceName);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0002DCF9 File Offset: 0x0002BEF9
		public static string Attribute_NameColumn_InvalidColumn(string tableName, string columnName, string dataSourceName)
		{
			return ValidationSR.Keys.GetString("Attribute_NameColumn_InvalidColumn", tableName, columnName, dataSourceName);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0002DD08 File Offset: 0x0002BF08
		public static string Attribute_NotRelatedToTheKeyAttribute(string keyAttributeName)
		{
			return ValidationSR.Keys.GetString("Attribute_NotRelatedToTheKeyAttribute", keyAttributeName);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0002DD15 File Offset: 0x0002BF15
		public static string Attribute_AttributeRelationshipIsUnknown(string attributeID)
		{
			return ValidationSR.Keys.GetString("Attribute_AttributeRelationshipIsUnknown", attributeID);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0002DD22 File Offset: 0x0002BF22
		public static string Attribute_KeyColumnsOfParentAttributeDoNotCorrespondToOnesOfKeyAttribute(string parentAttributeName, string keyAttributeName)
		{
			return ValidationSR.Keys.GetString("Attribute_KeyColumnsOfParentAttributeDoNotCorrespondToOnesOfKeyAttribute", parentAttributeName, keyAttributeName);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0002DD30 File Offset: 0x0002BF30
		public static string Attribute_OrderByAttribute_IsUnknown(string orderByAttributeID)
		{
			return ValidationSR.Keys.GetString("Attribute_OrderByAttribute_IsUnknown", orderByAttributeID);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0002DD3D File Offset: 0x0002BF3D
		public static string Attribute_OrderByAttribute_IsNotRelated(string orderByAttributeName)
		{
			return ValidationSR.Keys.GetString("Attribute_OrderByAttribute_IsNotRelated", orderByAttributeName);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0002DD4A File Offset: 0x0002BF4A
		public static string Attribute_OrderBy_IsSetWithoutOrderByAttributeID(string orderByValue)
		{
			return ValidationSR.Keys.GetString("Attribute_OrderBy_IsSetWithoutOrderByAttributeID", orderByValue);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0002DD57 File Offset: 0x0002BF57
		public static string AttributeRelationship_RelationshipTypeCannotBeNoneForTheKey(string relationshipName)
		{
			return ValidationSR.Keys.GetString("AttributeRelationship_RelationshipTypeCannotBeNoneForTheKey", relationshipName);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002DD64 File Offset: 0x0002BF64
		public static string Hierarchy_ManyLevelsReuseTheSameAttribute(string levelList, string attributeName)
		{
			return ValidationSR.Keys.GetString("Hierarchy_ManyLevelsReuseTheSameAttribute", levelList, attributeName);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002DD72 File Offset: 0x0002BF72
		public static string MiningStructure_KeyTimeInNestedTableNotDefined(string columnName)
		{
			return ValidationSR.Keys.GetString("MiningStructure_KeyTimeInNestedTableNotDefined", columnName);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0002DD7F File Offset: 0x0002BF7F
		public static string MiningStructure_MultipleKeyNotAllowedInNestedTable(string columnName)
		{
			return ValidationSR.Keys.GetString("MiningStructure_MultipleKeyNotAllowedInNestedTable", columnName);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0002DD8C File Offset: 0x0002BF8C
		public static string MiningStructure_ContentTypeNotSupportedMessage(string columnName, string contentType)
		{
			return ValidationSR.Keys.GetString("MiningStructure_ContentTypeNotSupportedMessage", columnName, contentType);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0002DD9A File Offset: 0x0002BF9A
		public static string MiningStructure_NestedTableForeignKeyNotCaseKey(string tableName, string columnName)
		{
			return ValidationSR.Keys.GetString("MiningStructure_NestedTableForeignKeyNotCaseKey", tableName, columnName);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0002DDA8 File Offset: 0x0002BFA8
		public static string MiningModel_ForeignKeyPresentMessage(string columnName, string tableName)
		{
			return ValidationSR.Keys.GetString("MiningModel_ForeignKeyPresentMessage", columnName, tableName);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0002DDB6 File Offset: 0x0002BFB6
		public static string MiningModel_PluginAlgorithmNotAllowed(string algorithm)
		{
			return ValidationSR.Keys.GetString("MiningModel_PluginAlgorithmNotAllowed", algorithm);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0002DDC3 File Offset: 0x0002BFC3
		public static string MiningModelColumn_UnexpectedType(string typeName, string expectedTypeName)
		{
			return ValidationSR.Keys.GetString("MiningModelColumn_UnexpectedType", typeName, expectedTypeName);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0002DDD1 File Offset: 0x0002BFD1
		public static string MiningStructureColumn_AttributeHierarchyNotEnabled(string attributeName)
		{
			return ValidationSR.Keys.GetString("MiningStructureColumn_AttributeHierarchyNotEnabled", attributeName);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0002DDDE File Offset: 0x0002BFDE
		public static string MiningStructureColumn_PropertyShouldNotBeDefinedInRelationalMiningStructure(string propertyName)
		{
			return ValidationSR.Keys.GetString("MiningStructureColumn_PropertyShouldNotBeDefinedInRelationalMiningStructure", propertyName);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0002DDEB File Offset: 0x0002BFEB
		public static string MiningStructureColumn_PropertyShouldNotBeDefinedInOlapMiningStructure(string propertyName)
		{
			return ValidationSR.Keys.GetString("MiningStructureColumn_PropertyShouldNotBeDefinedInOlapMiningStructure", propertyName);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0002DDF8 File Offset: 0x0002BFF8
		public static string MiningStructureColumn_NoPathFromKeyColumnsTableToNameColumnTable(string keyColumnsTable, string nameColumnTable)
		{
			return ValidationSR.Keys.GetString("MiningStructureColumn_NoPathFromKeyColumnsTableToNameColumnTable", keyColumnsTable, nameColumnTable);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0002DE06 File Offset: 0x0002C006
		public static string MiningStructureColumn_MultiPathsFromKeyColumnsTableToNameColumnTable(string keyColumnsTable, string nameColumnTable)
		{
			return ValidationSR.Keys.GetString("MiningStructureColumn_MultiPathsFromKeyColumnsTableToNameColumnTable", keyColumnsTable, nameColumnTable);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0002DE14 File Offset: 0x0002C014
		public static string MiningStructureColumn_ForeignKeyColumns_Count(string keyColumnName, int foreignKeysCount, int keysCount)
		{
			return ValidationSR.Keys.GetString("MiningStructureColumn_ForeignKeyColumns_Count", keyColumnName, foreignKeysCount, keysCount);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0002DE2D File Offset: 0x0002C02D
		public static string MiningStructureColumn_ForeignKeyColumns_DataType(string keyColumnName, int foreignKeyIndex, string foreignKeyType, string keyType)
		{
			return ValidationSR.Keys.GetString("MiningStructureColumn_ForeignKeyColumns_DataType", keyColumnName, foreignKeyIndex, foreignKeyType, keyType);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002DE42 File Offset: 0x0002C042
		public static string MiningStructureColumn_UnexpectedType(string typeName, string expectedTypeName)
		{
			return ValidationSR.Keys.GetString("MiningStructureColumn_UnexpectedType", typeName, expectedTypeName);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0002DE50 File Offset: 0x0002C050
		public static string MiningStructureColumn_IncompatibleContentType(string content)
		{
			return ValidationSR.Keys.GetString("MiningStructureColumn_IncompatibleContentType", content);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0002DE5D File Offset: 0x0002C05D
		public static string MiningStructureColumn_KeyColumnIsBasedOnGuidDataColumn(string table, string column)
		{
			return ValidationSR.Keys.GetString("MiningStructureColumn_KeyColumnIsBasedOnGuidDataColumn", table, column);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0002DE6B File Offset: 0x0002C06B
		public static string TableMiningStructureColumn_ForeignKeyColumns_NoPathToCaseTable(string foreignKeyColumnsTableName, string caseTableName)
		{
			return ValidationSR.Keys.GetString("TableMiningStructureColumn_ForeignKeyColumns_NoPathToCaseTable", foreignKeyColumnsTableName, caseTableName);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0002DE79 File Offset: 0x0002C079
		public static string TableMiningStructureColumn_ForeignKeyColumns_MultiPathsToCaseTable(string foreignKeyColumnsTableName, string caseTableName)
		{
			return ValidationSR.Keys.GetString("TableMiningStructureColumn_ForeignKeyColumns_MultiPathsToCaseTable", foreignKeyColumnsTableName, caseTableName);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0002DE87 File Offset: 0x0002C087
		public static string TableMiningStructureColumn_ForeignKeyColumns_NoPathFromTheNestedKey(string foreignKeyColumnsTableName, string keyTableName)
		{
			return ValidationSR.Keys.GetString("TableMiningStructureColumn_ForeignKeyColumns_NoPathFromTheNestedKey", foreignKeyColumnsTableName, keyTableName);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0002DE95 File Offset: 0x0002C095
		public static string TableMiningStructureColumn_ForeignKeyColumns_MultiPathsFromTheNestedKey(string foreignKeyColumnsTableName, string keyTableName)
		{
			return ValidationSR.Keys.GetString("TableMiningStructureColumn_ForeignKeyColumns_MultiPathsFromTheNestedKey", foreignKeyColumnsTableName, keyTableName);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0002DEA3 File Offset: 0x0002C0A3
		public static string MiningStructureColumns_MultipleKeyColumnsDefined(string localizedKeyColumnNameList)
		{
			return ValidationSR.Keys.GetString("MiningStructureColumns_MultipleKeyColumnsDefined", localizedKeyColumnNameList);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0002DEB0 File Offset: 0x0002C0B0
		public static string MiningStructureColumns_NoPathFromKeyToColumn(string caseTableName, string lookupTableName, string columnName)
		{
			return ValidationSR.Keys.GetString("MiningStructureColumns_NoPathFromKeyToColumn", caseTableName, lookupTableName, columnName);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0002DEBF File Offset: 0x0002C0BF
		public static string MiningStructureColumns_MultiPathsFromKeyToColumn(string caseTableName, string lookupTableName, string columnName)
		{
			return ValidationSR.Keys.GetString("MiningStructureColumns_MultiPathsFromKeyToColumn", caseTableName, lookupTableName, columnName);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0002DECE File Offset: 0x0002C0CE
		public static string Kpi_AssociatedMeasureGroupIsUnknown(string measureGroupID)
		{
			return ValidationSR.Keys.GetString("Kpi_AssociatedMeasureGroupIsUnknown", measureGroupID);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0002DEDB File Offset: 0x0002C0DB
		public static string MeasureGroup_NoDimensionsDefined(string measureGroupName)
		{
			return ValidationSR.Keys.GetString("MeasureGroup_NoDimensionsDefined", measureGroupName);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0002DEE8 File Offset: 0x0002C0E8
		public static string MeasureGroup_PartitionsWithSameSourceTableAndFilter(string list)
		{
			return ValidationSR.Keys.GetString("MeasureGroup_PartitionsWithSameSourceTableAndFilter", list);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0002DEF5 File Offset: 0x0002C0F5
		public static string MeasureGroup_CubeDimensionIsNotIncludedAsDegenerate(string cubeDimensionName, string measureGroupName)
		{
			return ValidationSR.Keys.GetString("MeasureGroup_CubeDimensionIsNotIncludedAsDegenerate", cubeDimensionName, measureGroupName);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0002DF03 File Offset: 0x0002C103
		public static string MeasureGroup_ContainsRelationshipsParticipatingInLoops(string measureGroupName)
		{
			return ValidationSR.Keys.GetString("MeasureGroup_ContainsRelationshipsParticipatingInLoops", measureGroupName);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0002DF10 File Offset: 0x0002C110
		public static string MeasureGroup_WriteEnabledWithNonSumOrUnsignedMeasures(string partitionsList, string measuresList)
		{
			return ValidationSR.Keys.GetString("MeasureGroup_WriteEnabledWithNonSumOrUnsignedMeasures", partitionsList, measuresList);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0002DF1E File Offset: 0x0002C11E
		public static string Measure_Source_ValueNotSupportedForNullProcessing(string enumValueName, string nullProcessingPropertyName)
		{
			return ValidationSR.Keys.GetString("Measure_Source_ValueNotSupportedForNullProcessing", enumValueName, nullProcessingPropertyName);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0002DF2C File Offset: 0x0002C12C
		public static string Measure_AggregateFunctionNotAllow(string function)
		{
			return ValidationSR.Keys.GetString("Measure_AggregateFunctionNotAllow", function);
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0002DF39 File Offset: 0x0002C139
		public static string ManyToManyMeasureGroupDimension_MeasureGroupIsSameAsParent(string measureGroupName)
		{
			return ValidationSR.Keys.GetString("ManyToManyMeasureGroupDimension_MeasureGroupIsSameAsParent", measureGroupName);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0002DF46 File Offset: 0x0002C146
		public static string ManyToManyMeasureGroupDimension_MeasureGroupShouldContainTheCubeDimension(string cubeDimensionName, string measureGroupName)
		{
			return ValidationSR.Keys.GetString("ManyToManyMeasureGroupDimension_MeasureGroupShouldContainTheCubeDimension", cubeDimensionName, measureGroupName);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0002DF54 File Offset: 0x0002C154
		public static string ReferenceMeasureGroupDimension_IntermediateCubeDimensionIsUnknown(string intermediateCubeDimensionID)
		{
			return ValidationSR.Keys.GetString("ReferenceMeasureGroupDimension_IntermediateCubeDimensionIsUnknown", intermediateCubeDimensionID);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0002DF61 File Offset: 0x0002C161
		public static string ReferenceMeasureGroupDimension_IntermediateCubeDimensionShouldBeIncludedInMeasureGroup(string cubeDimensionName, string measureGroupName)
		{
			return ValidationSR.Keys.GetString("ReferenceMeasureGroupDimension_IntermediateCubeDimensionShouldBeIncludedInMeasureGroup", cubeDimensionName, measureGroupName);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0002DF6F File Offset: 0x0002C16F
		public static string ReferenceMeasureGroupDimension_ChainingRequiresMaterialization(string intermediateCubeDimensionName)
		{
			return ValidationSR.Keys.GetString("ReferenceMeasureGroupDimension_ChainingRequiresMaterialization", intermediateCubeDimensionName);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0002DF7C File Offset: 0x0002C17C
		public static string ReferenceMeasureGroupDimension_IntermediateReferenceDimensionIsNotMaterialized(string intermediateCubeDimensionName, string measureGroupName)
		{
			return ValidationSR.Keys.GetString("ReferenceMeasureGroupDimension_IntermediateReferenceDimensionIsNotMaterialized", intermediateCubeDimensionName, measureGroupName);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0002DF8A File Offset: 0x0002C18A
		public static string ReferenceMeasureGroupDimension_IntermediateDimensionIsReferenced(string cubeDimensionName, string measureGroupName)
		{
			return ValidationSR.Keys.GetString("ReferenceMeasureGroupDimension_IntermediateDimensionIsReferenced", cubeDimensionName, measureGroupName);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0002DF98 File Offset: 0x0002C198
		public static string ReferenceMeasureGroupDimension_IntermediateGranularityAttributeIsUnknown(string intermediateGranularityAttributeID)
		{
			return ValidationSR.Keys.GetString("ReferenceMeasureGroupDimension_IntermediateGranularityAttributeIsUnknown", intermediateGranularityAttributeID);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0002DFA5 File Offset: 0x0002C1A5
		public static string ReferenceMeasureGroupDimension_IntermediateGranularityAttribute_AttributeHierarchyShouldBeEnabled(string attributeName)
		{
			return ValidationSR.Keys.GetString("ReferenceMeasureGroupDimension_IntermediateGranularityAttribute_AttributeHierarchyShouldBeEnabled", attributeName);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0002DFB2 File Offset: 0x0002C1B2
		public static string ReferenceMeasureGroupDimension_GranularityAttributeIsUnknown(string granularityAttributeID)
		{
			return ValidationSR.Keys.GetString("ReferenceMeasureGroupDimension_GranularityAttributeIsUnknown", granularityAttributeID);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0002DFBF File Offset: 0x0002C1BF
		public static string ReferenceMeasureGroupDimension_GranularityAttribute_AttributeHierarchyShouldBeEnabled(string attributeName)
		{
			return ValidationSR.Keys.GetString("ReferenceMeasureGroupDimension_GranularityAttribute_AttributeHierarchyShouldBeEnabled", attributeName);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0002DFCC File Offset: 0x0002C1CC
		public static string ReferenceMeasureGroupDimension_KeyColumnsCountDontMatch(int granularityAttributeKeysCount, int intermediateGranularityAttributeKeysCount)
		{
			return ValidationSR.Keys.GetString("ReferenceMeasureGroupDimension_KeyColumnsCountDontMatch", granularityAttributeKeysCount, intermediateGranularityAttributeKeysCount);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0002DFE4 File Offset: 0x0002C1E4
		public static string ReferenceMeasureGroupDimension_KeyColumnsDataTypesDontMatch(int keyColumnIndex, string granularityAttributeKeyColumnDataType, string intermediateGranularityAttributeKeyColumnDataType)
		{
			return ValidationSR.Keys.GetString("ReferenceMeasureGroupDimension_KeyColumnsDataTypesDontMatch", keyColumnIndex, granularityAttributeKeyColumnDataType, intermediateGranularityAttributeKeyColumnDataType);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0002DFF8 File Offset: 0x0002C1F8
		public static string ReferenceMeasureGroupDimension_LoopDetected(string loopDefinition, string measureGroupName)
		{
			return ValidationSR.Keys.GetString("ReferenceMeasureGroupDimension_LoopDetected", loopDefinition, measureGroupName);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0002E006 File Offset: 0x0002C206
		public static string DataMiningMeasureGroupDimension_CubeDimensionShouldBeDataMiningDimension(string cubeDimensionName)
		{
			return ValidationSR.Keys.GetString("DataMiningMeasureGroupDimension_CubeDimensionShouldBeDataMiningDimension", cubeDimensionName);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0002E013 File Offset: 0x0002C213
		public static string MeasureGroupAttribute_KeyColumnsCountDontMatch(int count, int dimensionAttributeKeysCount)
		{
			return ValidationSR.Keys.GetString("MeasureGroupAttribute_KeyColumnsCountDontMatch", count, dimensionAttributeKeysCount);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0002E02B File Offset: 0x0002C22B
		public static string MeasureGroupAttribute_KeyColumnDataTypeDoesnMatch(int keyColumnIndex, string keyColumnDataType, string attributeKeyColumnDataType)
		{
			return ValidationSR.Keys.GetString("MeasureGroupAttribute_KeyColumnDataTypeDoesnMatch", keyColumnIndex, keyColumnDataType, attributeKeyColumnDataType);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0002E03F File Offset: 0x0002C23F
		public static string MdxScript_CommandHasNoText(int commandIndex)
		{
			return ValidationSR.Keys.GetString("MdxScript_CommandHasNoText", commandIndex);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0002E051 File Offset: 0x0002C251
		public static string SourceTableIsUnknown(string table, string dsvName)
		{
			return ValidationSR.Keys.GetString("SourceTableIsUnknown", table, dsvName);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0002E05F File Offset: 0x0002C25F
		public static string SourceColumnIsUnknown(string table, string column, string dsvName)
		{
			return ValidationSR.Keys.GetString("SourceColumnIsUnknown", table, column, dsvName);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0002E06E File Offset: 0x0002C26E
		public static string BindingProperty_CannotCheckReference(string propertyName, string typeName, string missingParentTypeName)
		{
			return ValidationSR.Keys.GetString("BindingProperty_CannotCheckReference", propertyName, typeName, missingParentTypeName);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0002E07D File Offset: 0x0002C27D
		public static string BindingProperty_InvalidType(string propertyName, string typeName, string allowedTypesList)
		{
			return ValidationSR.Keys.GetString("BindingProperty_InvalidType", propertyName, typeName, allowedTypesList);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0002E08C File Offset: 0x0002C28C
		public static string BindingProperty_UnspecifiedReference(string propertyName, string referenceTypeName)
		{
			return ValidationSR.Keys.GetString("BindingProperty_UnspecifiedReference", propertyName, referenceTypeName);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0002E09A File Offset: 0x0002C29A
		public static string BindingProperty_UnknownReference(string propertyName, string referenceTypeName, string id)
		{
			return ValidationSR.Keys.GetString("BindingProperty_UnknownReference", propertyName, referenceTypeName, id);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0002E0A9 File Offset: 0x0002C2A9
		public static string DataItems_DataType_IsInvalid(string propertyName, int index, string oleDbTypeEnumName)
		{
			return ValidationSR.Keys.GetString("DataItems_DataType_IsInvalid", propertyName, index, oleDbTypeEnumName);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0002E0BD File Offset: 0x0002C2BD
		public static string DataItem_DataType_IsInvalid(string propertyName, string oleDbTypeEnumName)
		{
			return ValidationSR.Keys.GetString("DataItem_DataType_IsInvalid", propertyName, oleDbTypeEnumName);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0002E0CB File Offset: 0x0002C2CB
		public static string DataItem_DataType_IsUnexpected(string propertyName, string actualDataType, string expectedDataType)
		{
			return ValidationSR.Keys.GetString("DataItem_DataType_IsUnexpected", propertyName, actualDataType, expectedDataType);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0002E0DA File Offset: 0x0002C2DA
		public static string DataItems_Source_NotSpecified(string propertyName, int index)
		{
			return ValidationSR.Keys.GetString("DataItems_Source_NotSpecified", propertyName, index);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0002E0ED File Offset: 0x0002C2ED
		public static string DataItem_Source_NotSpecified(string propertyName)
		{
			return ValidationSR.Keys.GetString("DataItem_Source_NotSpecified", propertyName);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0002E0FA File Offset: 0x0002C2FA
		public static string DataItems_Source_InvalidType(string propertyName, int index, string actualType, string allowedTypesList)
		{
			return ValidationSR.Keys.GetString("DataItems_Source_InvalidType", propertyName, index, actualType, allowedTypesList);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0002E10F File Offset: 0x0002C30F
		public static string DataItem_Source_InvalidType(string propertyName, string actualType, string allowedTypesList)
		{
			return ValidationSR.Keys.GetString("DataItem_Source_InvalidType", propertyName, actualType, allowedTypesList);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0002E11E File Offset: 0x0002C31E
		public static string DataItems_Source_UndefinedTable(string propertyName, int index)
		{
			return ValidationSR.Keys.GetString("DataItems_Source_UndefinedTable", propertyName, index);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0002E131 File Offset: 0x0002C331
		public static string DataItem_Source_UndefinedTable(string propertyName)
		{
			return ValidationSR.Keys.GetString("DataItem_Source_UndefinedTable", propertyName);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0002E13E File Offset: 0x0002C33E
		public static string DataItems_Source_InvalidTable(string propertyName, int index, string table)
		{
			return ValidationSR.Keys.GetString("DataItems_Source_InvalidTable", propertyName, index, table);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0002E152 File Offset: 0x0002C352
		public static string DataItem_Source_InvalidTable(string propertyName, string table)
		{
			return ValidationSR.Keys.GetString("DataItem_Source_InvalidTable", propertyName, table);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0002E160 File Offset: 0x0002C360
		public static string DataItems_Source_UndefinedColumn(string propertyName, int index)
		{
			return ValidationSR.Keys.GetString("DataItems_Source_UndefinedColumn", propertyName, index);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0002E173 File Offset: 0x0002C373
		public static string DataItem_Source_UndefinedColumn(string propertyName)
		{
			return ValidationSR.Keys.GetString("DataItem_Source_UndefinedColumn", propertyName);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0002E180 File Offset: 0x0002C380
		public static string DataItems_Source_InvalidColumn(string propertyName, int index, string table, string column)
		{
			return ValidationSR.Keys.GetString("DataItems_Source_InvalidColumn", propertyName, index, table, column);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0002E195 File Offset: 0x0002C395
		public static string DataItem_Source_InvalidColumn(string propertyName, string table, string column)
		{
			return ValidationSR.Keys.GetString("DataItem_Source_InvalidColumn", propertyName, table, column);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0002E1A4 File Offset: 0x0002C3A4
		public static string DataItems_AllNeedToBeInTheSameTable(string propertyName)
		{
			return ValidationSR.Keys.GetString("DataItems_AllNeedToBeInTheSameTable", propertyName);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0002E1B1 File Offset: 0x0002C3B1
		public static string DataItem_NullProcessing_UnknownMember(string propertyName)
		{
			return ValidationSR.Keys.GetString("DataItem_NullProcessing_UnknownMember", propertyName);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0002E1BE File Offset: 0x0002C3BE
		public static string DataItem_NullProcessing_UnknownMemberForKey(string propertyName, int index)
		{
			return ValidationSR.Keys.GetString("DataItem_NullProcessing_UnknownMemberForKey", propertyName, index);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0002E1D1 File Offset: 0x0002C3D1
		public static string ParentOfParticularTypeIsMissing(string parentTypeNameLocalized)
		{
			return ValidationSR.Keys.GetString("ParentOfParticularTypeIsMissing", parentTypeNameLocalized);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0002E1DE File Offset: 0x0002C3DE
		public static string FullErrorText(string path, string errorText)
		{
			return ValidationSR.Keys.GetString("FullErrorText", path, errorText);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0002E1EC File Offset: 0x0002C3EC
		public static string PropertiesCannotBeSpecifiedInTheSameTime(string propertyName1, string propertyName2)
		{
			return ValidationSR.Keys.GetString("PropertiesCannotBeSpecifiedInTheSameTime", propertyName1, propertyName2);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0002E1FA File Offset: 0x0002C3FA
		public static string ReferenceIsInvalid(string referencedTypeNameLocalized, string id, string propertyName)
		{
			return ValidationSR.Keys.GetString("ReferenceIsInvalid", referencedTypeNameLocalized, id, propertyName);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0002E209 File Offset: 0x0002C409
		public static string Partition_IsLargeWithNoAggs1(long rowsThreshold)
		{
			return ValidationSR.Keys.GetString("Partition_IsLargeWithNoAggs1", rowsThreshold);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0002E21B File Offset: 0x0002C41B
		public static string Partition_IsLargeWithNoAggs2(long rowsThreshold)
		{
			return ValidationSR.Keys.GetString("Partition_IsLargeWithNoAggs2", rowsThreshold);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0002E22D File Offset: 0x0002C42D
		public static string MeasureGroup_HasLargePartitions(int rowsMillions, int sizeMB)
		{
			return ValidationSR.Keys.GetString("MeasureGroup_HasLargePartitions", rowsMillions, sizeMB);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0002E245 File Offset: 0x0002C445
		public static string MeasureGroup_HasPartitionsToConsolidate(int maxSmallPartitions, int rowsMillions, int sizeMB)
		{
			return ValidationSR.Keys.GetString("MeasureGroup_HasPartitionsToConsolidate", maxSmallPartitions, rowsMillions, sizeMB);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0002E263 File Offset: 0x0002C463
		public static string IntermediateGranularityNotAggregated2(string cubeDimension, string attribute)
		{
			return ValidationSR.Keys.GetString("IntermediateGranularityNotAggregated2", cubeDimension, attribute);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0002E271 File Offset: 0x0002C471
		public static string PartitionWithTooManyAggregations(int threshold)
		{
			return ValidationSR.Keys.GetString("PartitionWithTooManyAggregations", threshold);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0002E283 File Offset: 0x0002C483
		public static string AggregationHasRelatedAttributes2(string attribute, string relatedAttribute)
		{
			return ValidationSR.Keys.GetString("AggregationHasRelatedAttributes2", attribute, relatedAttribute);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0002E291 File Offset: 0x0002C491
		public static string Dimension_HasMultipleNonAggregatableAttributes2(string attributeList)
		{
			return ValidationSR.Keys.GetString("Dimension_HasMultipleNonAggregatableAttributes2", attributeList);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0002E29E File Offset: 0x0002C49E
		public static string AggregationBelowGranularity2(string attribute, string granularityAttribute)
		{
			return ValidationSR.Keys.GetString("AggregationBelowGranularity2", attribute, granularityAttribute);
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0002E2AC File Offset: 0x0002C4AC
		public static string Attribute_LargeAttributeWithNonNumericKey(long threshold)
		{
			return ValidationSR.Keys.GetString("Attribute_LargeAttributeWithNonNumericKey", threshold);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0002E2BE File Offset: 0x0002C4BE
		public static string NonKeyLargeAttributeWithVisibleHierarchy(int membersThreshold, int percentThreshold)
		{
			return ValidationSR.Keys.GetString("NonKeyLargeAttributeWithVisibleHierarchy", membersThreshold, percentThreshold);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0002E2D6 File Offset: 0x0002C4D6
		public static string AttributeTypeAccountOrTimeNeedsMatchingDimension2(string attributeType, string dimensionType)
		{
			return ValidationSR.Keys.GetString("AttributeTypeAccountOrTimeNeedsMatchingDimension2", attributeType, dimensionType);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0002E2E4 File Offset: 0x0002C4E4
		public static string AttributeTypeNeedsMatchingDimension2(string attributeType, string dimensionType)
		{
			return ValidationSR.Keys.GetString("AttributeTypeNeedsMatchingDimension2", attributeType, dimensionType);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002E2F2 File Offset: 0x0002C4F2
		public static string DimensionTypeAccountOrTimeNeedsMatchingAttribute2(string dimensionType)
		{
			return ValidationSR.Keys.GetString("DimensionTypeAccountOrTimeNeedsMatchingAttribute2", dimensionType);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0002E2FF File Offset: 0x0002C4FF
		public static string DimensionTypeNeedsMatchingAttribute2(string dimensionType)
		{
			return ValidationSR.Keys.GetString("DimensionTypeNeedsMatchingAttribute2", dimensionType);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0002E30C File Offset: 0x0002C50C
		public static string TooManyParentChildDimsWithOutlineCalcs(int threshold)
		{
			return ValidationSR.Keys.GetString("TooManyParentChildDimsWithOutlineCalcs", threshold);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0002E31E File Offset: 0x0002C51E
		public static string ParentChildDimensionWithLargeKey(long keyMembersThreshold)
		{
			return ValidationSR.Keys.GetString("ParentChildDimensionWithLargeKey", keyMembersThreshold);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0002E330 File Offset: 0x0002C530
		public static string MeasureGroupsWithTheSameDimensionalityAndGranularity2(string mgName1, string mgName2)
		{
			return ValidationSR.Keys.GetString("MeasureGroupsWithTheSameDimensionalityAndGranularity2", mgName1, mgName2);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0002E33E File Offset: 0x0002C53E
		public static string PerspectiveDefaultMeasureNotIncluded2(string measureName)
		{
			return ValidationSR.Keys.GetString("PerspectiveDefaultMeasureNotIncluded2", measureName);
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002E34B File Offset: 0x0002C54B
		public static string MeasureGroupWithSemiAdditiveMeasuresAndRolapDimensions2(string cubeDimensionName)
		{
			return ValidationSR.Keys.GetString("MeasureGroupWithSemiAdditiveMeasuresAndRolapDimensions2", cubeDimensionName);
		}

		// Token: 0x020001A1 RID: 417
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x0600132F RID: 4911 RVA: 0x000434BA File Offset: 0x000416BA
			private Keys()
			{
			}

			// Token: 0x17000631 RID: 1585
			// (get) Token: 0x06001330 RID: 4912 RVA: 0x000434C2 File Offset: 0x000416C2
			// (set) Token: 0x06001331 RID: 4913 RVA: 0x000434C9 File Offset: 0x000416C9
			public static CultureInfo Culture
			{
				get
				{
					return ValidationSR.Keys._culture;
				}
				set
				{
					ValidationSR.Keys._culture = value;
				}
			}

			// Token: 0x06001332 RID: 4914 RVA: 0x000434D1 File Offset: 0x000416D1
			public static string GetString(string key)
			{
				return ValidationSR.Keys.resourceManager.GetString(key, ValidationSR.Keys._culture);
			}

			// Token: 0x06001333 RID: 4915 RVA: 0x000434E3 File Offset: 0x000416E3
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, ValidationSR.Keys.resourceManager.GetString(key, ValidationSR.Keys._culture), arg0);
			}

			// Token: 0x06001334 RID: 4916 RVA: 0x00043500 File Offset: 0x00041700
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, ValidationSR.Keys.resourceManager.GetString(key, ValidationSR.Keys._culture), arg0, arg1);
			}

			// Token: 0x06001335 RID: 4917 RVA: 0x0004351E File Offset: 0x0004171E
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, ValidationSR.Keys.resourceManager.GetString(key, ValidationSR.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x06001336 RID: 4918 RVA: 0x0004353D File Offset: 0x0004173D
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3)
			{
				return string.Format(CultureInfo.CurrentCulture, ValidationSR.Keys.resourceManager.GetString(key, ValidationSR.Keys._culture), new object[] { arg0, arg1, arg2, arg3 });
			}

			// Token: 0x04000F40 RID: 3904
			private static ResourceManager resourceManager = new ResourceManager(typeof(ValidationSR).FullName, typeof(ValidationSR).Module.Assembly);

			// Token: 0x04000F41 RID: 3905
			private static CultureInfo _culture = null;

			// Token: 0x04000F42 RID: 3906
			public const string ListStart = "ListStart";

			// Token: 0x04000F43 RID: 3907
			public const string List = "List";

			// Token: 0x04000F44 RID: 3908
			public const string RuleCategory_Database = "RuleCategory_Database";

			// Token: 0x04000F45 RID: 3909
			public const string RuleCategory_DataSource = "RuleCategory_DataSource";

			// Token: 0x04000F46 RID: 3910
			public const string RuleCategory_Dimension = "RuleCategory_Dimension";

			// Token: 0x04000F47 RID: 3911
			public const string RuleCategory_Cube = "RuleCategory_Cube";

			// Token: 0x04000F48 RID: 3912
			public const string RuleCategory_PartitionAndAggregation = "RuleCategory_PartitionAndAggregation";

			// Token: 0x04000F49 RID: 3913
			public const string DataSourceView_DataSourceNotSpecified = "DataSourceView_DataSourceNotSpecified";

			// Token: 0x04000F4A RID: 3914
			public const string Action_TargetNotDefined = "Action_TargetNotDefined";

			// Token: 0x04000F4B RID: 3915
			public const string Action_ExpressionNotDefined = "Action_ExpressionNotDefined";

			// Token: 0x04000F4C RID: 3916
			public const string Cube_NoDimensionsDefined = "Cube_NoDimensionsDefined";

			// Token: 0x04000F4D RID: 3917
			public const string Cube_NoDefaultMdxScriptDefined = "Cube_NoDefaultMdxScriptDefined";

			// Token: 0x04000F4E RID: 3918
			public const string Cube_MultipleDefaultMdxScriptsDefined = "Cube_MultipleDefaultMdxScriptsDefined";

			// Token: 0x04000F4F RID: 3919
			public const string Cube_PerspectiveNotAllow = "Cube_PerspectiveNotAllow";

			// Token: 0x04000F50 RID: 3920
			public const string Cube_CubeDimensionsWithExcludeDimensionNameAndDuplicateHierarchies = "Cube_CubeDimensionsWithExcludeDimensionNameAndDuplicateHierarchies";

			// Token: 0x04000F51 RID: 3921
			public const string DatabasePermission_WriteCannotBeAllowed = "DatabasePermission_WriteCannotBeAllowed";

			// Token: 0x04000F52 RID: 3922
			public const string Database_AssemblyNotAllowed = "Database_AssemblyNotAllowed";

			// Token: 0x04000F53 RID: 3923
			public const string Server_AssemblyNotAllowed = "Server_AssemblyNotAllowed";

			// Token: 0x04000F54 RID: 3924
			public const string Server_BackupNotAllowed = "Server_BackupNotAllowed";

			// Token: 0x04000F55 RID: 3925
			public const string Server_RestoreNotAllowed = "Server_RestoreNotAllowed";

			// Token: 0x04000F56 RID: 3926
			public const string Dimension_NoHierarchiesDefined = "Dimension_NoHierarchiesDefined";

			// Token: 0x04000F57 RID: 3927
			public const string Dimension_NoAttributesDefined = "Dimension_NoAttributesDefined";

			// Token: 0x04000F58 RID: 3928
			public const string Dimension_NoKeyAttributeDefined = "Dimension_NoKeyAttributeDefined";

			// Token: 0x04000F59 RID: 3929
			public const string Dimension_DependsOnDimensionIsUnknown = "Dimension_DependsOnDimensionIsUnknown";

			// Token: 0x04000F5A RID: 3930
			public const string Dimension_AttributesFormLoops = "Dimension_AttributesFormLoops";

			// Token: 0x04000F5B RID: 3931
			public const string Dimension_MultipleKeysDefined = "Dimension_MultipleKeysDefined";

			// Token: 0x04000F5C RID: 3932
			public const string Dimension_MultipleParentsDefined = "Dimension_MultipleParentsDefined";

			// Token: 0x04000F5D RID: 3933
			public const string Dimension_WritebackRequiresDataSourceViewBinding = "Dimension_WritebackRequiresDataSourceViewBinding";

			// Token: 0x04000F5E RID: 3934
			public const string Dimension_WritebackOnlyPossibleForStarSchemas = "Dimension_WritebackOnlyPossibleForStarSchemas";

			// Token: 0x04000F5F RID: 3935
			public const string Dimension_DM_DataMiningDimensionRequiresAnAttributeWithID = "Dimension_DM_DataMiningDimensionRequiresAnAttributeWithID";

			// Token: 0x04000F60 RID: 3936
			public const string Dimension_DM_ParentUniqueNameAttributeIsNotParent = "Dimension_DM_ParentUniqueNameAttributeIsNotParent";

			// Token: 0x04000F61 RID: 3937
			public const string Dimension_DM_NodeUniqueNameAttributeIsNotKey = "Dimension_DM_NodeUniqueNameAttributeIsNotKey";

			// Token: 0x04000F62 RID: 3938
			public const string Dimension_DM_AttributeIsNotRelatedWithTheKey = "Dimension_DM_AttributeIsNotRelatedWithTheKey";

			// Token: 0x04000F63 RID: 3939
			public const string Dimension_LinkedMeasureNotAllow = "Dimension_LinkedMeasureNotAllow";

			// Token: 0x04000F64 RID: 3940
			public const string Dimension_WritebackNotAllow = "Dimension_WritebackNotAllow";

			// Token: 0x04000F65 RID: 3941
			public const string Attribute_KeyColumns_Empty = "Attribute_KeyColumns_Empty";

			// Token: 0x04000F66 RID: 3942
			public const string Attribute_KeyColumn_InvalidSourceType = "Attribute_KeyColumn_InvalidSourceType";

			// Token: 0x04000F67 RID: 3943
			public const string Attribute_KeyColumn_InvalidTable = "Attribute_KeyColumn_InvalidTable";

			// Token: 0x04000F68 RID: 3944
			public const string Attribute_KeyColumn_InvalidColumn = "Attribute_KeyColumn_InvalidColumn";

			// Token: 0x04000F69 RID: 3945
			public const string Attribute_NameColumn_InvalidSourceType = "Attribute_NameColumn_InvalidSourceType";

			// Token: 0x04000F6A RID: 3946
			public const string Attribute_NameColumn_InvalidTable = "Attribute_NameColumn_InvalidTable";

			// Token: 0x04000F6B RID: 3947
			public const string Attribute_NameColumn_InvalidColumn = "Attribute_NameColumn_InvalidColumn";

			// Token: 0x04000F6C RID: 3948
			public const string Attribute_ParentAttributeMustBeRelatedToTheKeyAttribute = "Attribute_ParentAttributeMustBeRelatedToTheKeyAttribute";

			// Token: 0x04000F6D RID: 3949
			public const string Attribute_NotRelatedToTheKeyAttribute = "Attribute_NotRelatedToTheKeyAttribute";

			// Token: 0x04000F6E RID: 3950
			public const string Attribute_AttributeRelationshipIsUnknown = "Attribute_AttributeRelationshipIsUnknown";

			// Token: 0x04000F6F RID: 3951
			public const string Attribute_KeyColumnsOfParentAttributeDoNotCorrespondToOnesOfKeyAttribute = "Attribute_KeyColumnsOfParentAttributeDoNotCorrespondToOnesOfKeyAttribute";

			// Token: 0x04000F70 RID: 3952
			public const string Attribute_ParentAttributeCannotHaveAttributeRelationships = "Attribute_ParentAttributeCannotHaveAttributeRelationships";

			// Token: 0x04000F71 RID: 3953
			public const string Attribute_KeyRequiresAttributeHierarchyEnabled = "Attribute_KeyRequiresAttributeHierarchyEnabled";

			// Token: 0x04000F72 RID: 3954
			public const string Attribute_AttributeRelationshipsRequireAttributeHierarchyEnabled = "Attribute_AttributeRelationshipsRequireAttributeHierarchyEnabled";

			// Token: 0x04000F73 RID: 3955
			public const string Attribute_OrderByAttribute_IsSelf = "Attribute_OrderByAttribute_IsSelf";

			// Token: 0x04000F74 RID: 3956
			public const string Attribute_OrderByAttribute_IsUnknown = "Attribute_OrderByAttribute_IsUnknown";

			// Token: 0x04000F75 RID: 3957
			public const string Attribute_OrderByAttribute_IsNotRelated = "Attribute_OrderByAttribute_IsNotRelated";

			// Token: 0x04000F76 RID: 3958
			public const string Attribute_OrderBy_IsSetWithoutOrderByAttributeID = "Attribute_OrderBy_IsSetWithoutOrderByAttributeID";

			// Token: 0x04000F77 RID: 3959
			public const string Attribute_Type_ShouldBeDaysForTheKeyOfATimeDimension = "Attribute_Type_ShouldBeDaysForTheKeyOfATimeDimension";

			// Token: 0x04000F78 RID: 3960
			public const string Attribute_DiscretizationMethod_ShouldBeNoneForWriteEnabledDimensions = "Attribute_DiscretizationMethod_ShouldBeNoneForWriteEnabledDimensions";

			// Token: 0x04000F79 RID: 3961
			public const string Attribute_BrowsableRelatedAttributesRequireIsAggregatable = "Attribute_BrowsableRelatedAttributesRequireIsAggregatable";

			// Token: 0x04000F7A RID: 3962
			public const string AttributeRelationship_CreatesLoops = "AttributeRelationship_CreatesLoops";

			// Token: 0x04000F7B RID: 3963
			public const string AttributeRelationship_RelationshipTypeCannotBeNoneForTheKey = "AttributeRelationship_RelationshipTypeCannotBeNoneForTheKey";

			// Token: 0x04000F7C RID: 3964
			public const string Hierarchy_NoLevelsDefined = "Hierarchy_NoLevelsDefined";

			// Token: 0x04000F7D RID: 3965
			public const string Hierarchy_LevelBasedOnAttributeMarkedAsParentMustNotBeInHierarchy = "Hierarchy_LevelBasedOnAttributeMarkedAsParentMustNotBeInHierarchy";

			// Token: 0x04000F7E RID: 3966
			public const string Hierarchy_NoneAggregatableLevelCantHaveLevelsAboveIt = "Hierarchy_NoneAggregatableLevelCantHaveLevelsAboveIt";

			// Token: 0x04000F7F RID: 3967
			public const string Hierarchy_ManyLevelsReuseTheSameAttribute = "Hierarchy_ManyLevelsReuseTheSameAttribute";

			// Token: 0x04000F80 RID: 3968
			public const string Level_SourceAttributeIsUnknown = "Level_SourceAttributeIsUnknown";

			// Token: 0x04000F81 RID: 3969
			public const string Level_SourceAttributeOfParentChildLevelIsNotTheKeyAttribute = "Level_SourceAttributeOfParentChildLevelIsNotTheKeyAttribute";

			// Token: 0x04000F82 RID: 3970
			public const string Level_SourceAttributeDoesntHaveAttributeHierarchyEnabled = "Level_SourceAttributeDoesntHaveAttributeHierarchyEnabled";

			// Token: 0x04000F83 RID: 3971
			public const string Level_ParentAttributeDoesntHaveAttributeHierarchyEnabled = "Level_ParentAttributeDoesntHaveAttributeHierarchyEnabled";

			// Token: 0x04000F84 RID: 3972
			public const string DimensionPermission_WriteAccessRequiresRead = "DimensionPermission_WriteAccessRequiresRead";

			// Token: 0x04000F85 RID: 3973
			public const string DimensionPermission_WriteAccessRequiresNoAllowedOrDeniedSets = "DimensionPermission_WriteAccessRequiresNoAllowedOrDeniedSets";

			// Token: 0x04000F86 RID: 3974
			public const string DimensionPermission_AccessNoneRequiresAllAttributesAggregatable = "DimensionPermission_AccessNoneRequiresAllAttributesAggregatable";

			// Token: 0x04000F87 RID: 3975
			public const string MiningStructure_KeyTimeNotDefined = "MiningStructure_KeyTimeNotDefined";

			// Token: 0x04000F88 RID: 3976
			public const string MiningStructure_KeyTimeInNestedTableNotDefined = "MiningStructure_KeyTimeInNestedTableNotDefined";

			// Token: 0x04000F89 RID: 3977
			public const string MiningStructure_KeyTimeNotAllowed = "MiningStructure_KeyTimeNotAllowed";

			// Token: 0x04000F8A RID: 3978
			public const string MiningStructure_MultipleKeyTimeNotAllowed = "MiningStructure_MultipleKeyTimeNotAllowed";

			// Token: 0x04000F8B RID: 3979
			public const string MiningStructure_MultipleKeyNotAllowedInNestedTable = "MiningStructure_MultipleKeyNotAllowedInNestedTable";

			// Token: 0x04000F8C RID: 3980
			public const string MiningStructure_ContentTypeNotSupportedMessage = "MiningStructure_ContentTypeNotSupportedMessage";

			// Token: 0x04000F8D RID: 3981
			public const string MiningStructure_NestedTableForeignKeyNotCaseKey = "MiningStructure_NestedTableForeignKeyNotCaseKey";

			// Token: 0x04000F8E RID: 3982
			public const string MiningModel_KeyColumnNotDefined = "MiningModel_KeyColumnNotDefined";

			// Token: 0x04000F8F RID: 3983
			public const string MiningModel_PredictColumnNotDefined = "MiningModel_PredictColumnNotDefined";

			// Token: 0x04000F90 RID: 3984
			public const string MiningModel_ForeignKeyPresentMessage = "MiningModel_ForeignKeyPresentMessage";

			// Token: 0x04000F91 RID: 3985
			public const string MiningModel_PluginAlgorithmNotAllowed = "MiningModel_PluginAlgorithmNotAllowed";

			// Token: 0x04000F92 RID: 3986
			public const string MiningModelColumn_NoNestedColumnShouldBeDefinedBecauseSourceIsScalar = "MiningModelColumn_NoNestedColumnShouldBeDefinedBecauseSourceIsScalar";

			// Token: 0x04000F93 RID: 3987
			public const string MiningModelColumn_UnexpectedType = "MiningModelColumn_UnexpectedType";

			// Token: 0x04000F94 RID: 3988
			public const string MiningModelColumn_Columns_KeyNotDefined = "MiningModelColumn_Columns_KeyNotDefined";

			// Token: 0x04000F95 RID: 3989
			public const string MiningModelColumn_NoFilterShouldBeDefinedBecauseSourceIsScalar = "MiningModelColumn_NoFilterShouldBeDefinedBecauseSourceIsScalar";

			// Token: 0x04000F96 RID: 3990
			public const string MiningStructureColumn_AttributeHierarchyNotEnabled = "MiningStructureColumn_AttributeHierarchyNotEnabled";

			// Token: 0x04000F97 RID: 3991
			public const string MiningStructureColumn_PropertyShouldNotBeDefinedInRelationalMiningStructure = "MiningStructureColumn_PropertyShouldNotBeDefinedInRelationalMiningStructure";

			// Token: 0x04000F98 RID: 3992
			public const string MiningStructureColumn_PropertyShouldNotBeDefinedInOlapMiningStructure = "MiningStructureColumn_PropertyShouldNotBeDefinedInOlapMiningStructure";

			// Token: 0x04000F99 RID: 3993
			public const string MiningStructureColumn_NoPathFromKeyColumnsTableToNameColumnTable = "MiningStructureColumn_NoPathFromKeyColumnsTableToNameColumnTable";

			// Token: 0x04000F9A RID: 3994
			public const string MiningStructureColumn_MultiPathsFromKeyColumnsTableToNameColumnTable = "MiningStructureColumn_MultiPathsFromKeyColumnsTableToNameColumnTable";

			// Token: 0x04000F9B RID: 3995
			public const string MiningStructureColumn_SourceCubeNeedsToBeTheSameAsForTheMiningStructure = "MiningStructureColumn_SourceCubeNeedsToBeTheSameAsForTheMiningStructure";

			// Token: 0x04000F9C RID: 3996
			public const string MiningStructureColumn_ForeignKeyColumns_Count = "MiningStructureColumn_ForeignKeyColumns_Count";

			// Token: 0x04000F9D RID: 3997
			public const string MiningStructureColumn_ForeignKeyColumns_DataType = "MiningStructureColumn_ForeignKeyColumns_DataType";

			// Token: 0x04000F9E RID: 3998
			public const string MiningStructureColumn_UnexpectedType = "MiningStructureColumn_UnexpectedType";

			// Token: 0x04000F9F RID: 3999
			public const string MiningStructureColumn_Columns_KeyNotTextType = "MiningStructureColumn_Columns_KeyNotTextType";

			// Token: 0x04000FA0 RID: 4000
			public const string MiningStructureColumn_IncompatibleContentType = "MiningStructureColumn_IncompatibleContentType";

			// Token: 0x04000FA1 RID: 4001
			public const string MiningStructureColumn_KeyColumnIsBasedOnGuidDataColumn = "MiningStructureColumn_KeyColumnIsBasedOnGuidDataColumn";

			// Token: 0x04000FA2 RID: 4002
			public const string TableMiningStructureColumn_ForeignKeyColumns_NoPathToCaseTable = "TableMiningStructureColumn_ForeignKeyColumns_NoPathToCaseTable";

			// Token: 0x04000FA3 RID: 4003
			public const string TableMiningStructureColumn_ForeignKeyColumns_MultiPathsToCaseTable = "TableMiningStructureColumn_ForeignKeyColumns_MultiPathsToCaseTable";

			// Token: 0x04000FA4 RID: 4004
			public const string TableMiningStructureColumn_ForeignKeyColumns_NoPathFromTheNestedKey = "TableMiningStructureColumn_ForeignKeyColumns_NoPathFromTheNestedKey";

			// Token: 0x04000FA5 RID: 4005
			public const string TableMiningStructureColumn_ForeignKeyColumns_MultiPathsFromTheNestedKey = "TableMiningStructureColumn_ForeignKeyColumns_MultiPathsFromTheNestedKey";

			// Token: 0x04000FA6 RID: 4006
			public const string MiningStructureColumns_KeyColumnNotDefined = "MiningStructureColumns_KeyColumnNotDefined";

			// Token: 0x04000FA7 RID: 4007
			public const string MiningStructureColumns_MultipleKeyColumnsDefined = "MiningStructureColumns_MultipleKeyColumnsDefined";

			// Token: 0x04000FA8 RID: 4008
			public const string MiningStructureColumns_NoPathFromKeyToColumn = "MiningStructureColumns_NoPathFromKeyToColumn";

			// Token: 0x04000FA9 RID: 4009
			public const string MiningStructureColumns_MultiPathsFromKeyToColumn = "MiningStructureColumns_MultiPathsFromKeyToColumn";

			// Token: 0x04000FAA RID: 4010
			public const string Kpi_Value_Missing = "Kpi_Value_Missing";

			// Token: 0x04000FAB RID: 4011
			public const string Kpi_AssociatedMeasureGroupIsUnknown = "Kpi_AssociatedMeasureGroupIsUnknown";

			// Token: 0x04000FAC RID: 4012
			public const string MeasureGroup_NoDimensionsDefined = "MeasureGroup_NoDimensionsDefined";

			// Token: 0x04000FAD RID: 4013
			public const string MeasureGroup_NoMeasuresDefined = "MeasureGroup_NoMeasuresDefined";

			// Token: 0x04000FAE RID: 4014
			public const string MeasureGroup_PartitionsWithSameSourceTableAndFilter = "MeasureGroup_PartitionsWithSameSourceTableAndFilter";

			// Token: 0x04000FAF RID: 4015
			public const string MeasureGroup_SemiadditiveMeasureRequiresTimeDimension = "MeasureGroup_SemiadditiveMeasureRequiresTimeDimension";

			// Token: 0x04000FB0 RID: 4016
			public const string MeasureGroup_SourceMeasureGroupIsInTheSameCube = "MeasureGroup_SourceMeasureGroupIsInTheSameCube";

			// Token: 0x04000FB1 RID: 4017
			public const string MeasureGroup_SourceIsSelf = "MeasureGroup_SourceIsSelf";

			// Token: 0x04000FB2 RID: 4018
			public const string MeasureGroup_DegenerateDimensionsAreBasedOnDifferentDimensions = "MeasureGroup_DegenerateDimensionsAreBasedOnDifferentDimensions";

			// Token: 0x04000FB3 RID: 4019
			public const string MeasureGroup_CubeDimensionIsNotIncludedAsDegenerate = "MeasureGroup_CubeDimensionIsNotIncludedAsDegenerate";

			// Token: 0x04000FB4 RID: 4020
			public const string MeasureGroup_ContainsRelationshipsParticipatingInLoops = "MeasureGroup_ContainsRelationshipsParticipatingInLoops";

			// Token: 0x04000FB5 RID: 4021
			public const string MeasureGroup_AtMostTwoNonWritebackPartition = "MeasureGroup_AtMostTwoNonWritebackPartition";

			// Token: 0x04000FB6 RID: 4022
			public const string MeasureGroup_LinkingToAnotherDatabaseNotAllowed = "MeasureGroup_LinkingToAnotherDatabaseNotAllowed";

			// Token: 0x04000FB7 RID: 4023
			public const string MeasureGroup_WriteEnabledWithNonSumOrUnsignedMeasures = "MeasureGroup_WriteEnabledWithNonSumOrUnsignedMeasures";

			// Token: 0x04000FB8 RID: 4024
			public const string Measure_Source_ValueNotSupportedForNullProcessing = "Measure_Source_ValueNotSupportedForNullProcessing";

			// Token: 0x04000FB9 RID: 4025
			public const string Measure_DistinctCountRequiresNumericDataType = "Measure_DistinctCountRequiresNumericDataType";

			// Token: 0x04000FBA RID: 4026
			public const string Measure_AggregateFunctionNotAllow = "Measure_AggregateFunctionNotAllow";

			// Token: 0x04000FBB RID: 4027
			public const string Measure_ByAccountRequiresAccountDimensionAndAttribute = "Measure_ByAccountRequiresAccountDimensionAndAttribute";

			// Token: 0x04000FBC RID: 4028
			public const string RegularMeasureGroupDimension_GranularityNotDefined = "RegularMeasureGroupDimension_GranularityNotDefined";

			// Token: 0x04000FBD RID: 4029
			public const string RegularMeasureGroupDimension_MultipleGranularitiesDefined = "RegularMeasureGroupDimension_MultipleGranularitiesDefined";

			// Token: 0x04000FBE RID: 4030
			public const string ManyToManyMeasureGroupDimension_MeasureGroupIsSameAsParent = "ManyToManyMeasureGroupDimension_MeasureGroupIsSameAsParent";

			// Token: 0x04000FBF RID: 4031
			public const string ManyToManyMeasureGroupDimension_MeasureGroupShouldContainTheCubeDimension = "ManyToManyMeasureGroupDimension_MeasureGroupShouldContainTheCubeDimension";

			// Token: 0x04000FC0 RID: 4032
			public const string Partition_RemotePartitionNotAllowed = "Partition_RemotePartitionNotAllowed";

			// Token: 0x04000FC1 RID: 4033
			public const string ReferenceMeasureGroupDimension_IntermediateCubeDimensionIsUnknown = "ReferenceMeasureGroupDimension_IntermediateCubeDimensionIsUnknown";

			// Token: 0x04000FC2 RID: 4034
			public const string ReferenceMeasureGroupDimension_IntermediateCubeDimensionShouldBeIncludedInMeasureGroup = "ReferenceMeasureGroupDimension_IntermediateCubeDimensionShouldBeIncludedInMeasureGroup";

			// Token: 0x04000FC3 RID: 4035
			public const string ReferenceMeasureGroupDimension_ChainingRequiresMaterialization = "ReferenceMeasureGroupDimension_ChainingRequiresMaterialization";

			// Token: 0x04000FC4 RID: 4036
			public const string ReferenceMeasureGroupDimension_IntermediateReferenceDimensionIsNotMaterialized = "ReferenceMeasureGroupDimension_IntermediateReferenceDimensionIsNotMaterialized";

			// Token: 0x04000FC5 RID: 4037
			public const string ReferenceMeasureGroupDimension_IntermediateDimensionIsReferenced = "ReferenceMeasureGroupDimension_IntermediateDimensionIsReferenced";

			// Token: 0x04000FC6 RID: 4038
			public const string ReferenceMeasureGroupDimension_IntermediateGranularityAttributeIsUnknown = "ReferenceMeasureGroupDimension_IntermediateGranularityAttributeIsUnknown";

			// Token: 0x04000FC7 RID: 4039
			public const string ReferenceMeasureGroupDimension_IntermediateGranularityAttribute_AttributeHierarchyShouldBeEnabled = "ReferenceMeasureGroupDimension_IntermediateGranularityAttribute_AttributeHierarchyShouldBeEnabled";

			// Token: 0x04000FC8 RID: 4040
			public const string ReferenceMeasureGroupDimension_GranularityAttributeIsUnknown = "ReferenceMeasureGroupDimension_GranularityAttributeIsUnknown";

			// Token: 0x04000FC9 RID: 4041
			public const string ReferenceMeasureGroupDimension_GranularityAttribute_AttributeHierarchyShouldBeEnabled = "ReferenceMeasureGroupDimension_GranularityAttribute_AttributeHierarchyShouldBeEnabled";

			// Token: 0x04000FCA RID: 4042
			public const string ReferenceMeasureGroupDimension_KeyColumnsCountDontMatch = "ReferenceMeasureGroupDimension_KeyColumnsCountDontMatch";

			// Token: 0x04000FCB RID: 4043
			public const string ReferenceMeasureGroupDimension_KeyColumnsDataTypesDontMatch = "ReferenceMeasureGroupDimension_KeyColumnsDataTypesDontMatch";

			// Token: 0x04000FCC RID: 4044
			public const string ReferenceMeasureGroupDimension_LoopDetected = "ReferenceMeasureGroupDimension_LoopDetected";

			// Token: 0x04000FCD RID: 4045
			public const string ReferenceMeasureGroupDimension_MaterializedCannotHaveIntermediateTimeDimension = "ReferenceMeasureGroupDimension_MaterializedCannotHaveIntermediateTimeDimension";

			// Token: 0x04000FCE RID: 4046
			public const string DataMiningMeasureGroupDimension_CaseCubeDimensionIsInvalid = "DataMiningMeasureGroupDimension_CaseCubeDimensionIsInvalid";

			// Token: 0x04000FCF RID: 4047
			public const string DataMiningMeasureGroupDimension_CubeDimensionShouldBeDataMiningDimension = "DataMiningMeasureGroupDimension_CubeDimensionShouldBeDataMiningDimension";

			// Token: 0x04000FD0 RID: 4048
			public const string DataMiningMeasureGroupDimension_CaseDimensionsShouldMatch = "DataMiningMeasureGroupDimension_CaseDimensionsShouldMatch";

			// Token: 0x04000FD1 RID: 4049
			public const string MeasureGroupAttribute_KeyColumnsCountDontMatch = "MeasureGroupAttribute_KeyColumnsCountDontMatch";

			// Token: 0x04000FD2 RID: 4050
			public const string MeasureGroupAttribute_KeyColumnDataTypeDoesnMatch = "MeasureGroupAttribute_KeyColumnDataTypeDoesnMatch";

			// Token: 0x04000FD3 RID: 4051
			public const string MeasureGroupAttribute_AParentAttributeCannotBeGranularity = "MeasureGroupAttribute_AParentAttributeCannotBeGranularity";

			// Token: 0x04000FD4 RID: 4052
			public const string MeasureGroupAttribute_GranularityRequiresAttributeHierarchyEnabled = "MeasureGroupAttribute_GranularityRequiresAttributeHierarchyEnabled";

			// Token: 0x04000FD5 RID: 4053
			public const string Perspective_NoDimensionsDefined = "Perspective_NoDimensionsDefined";

			// Token: 0x04000FD6 RID: 4054
			public const string MdxScript_CommandHasNoText = "MdxScript_CommandHasNoText";

			// Token: 0x04000FD7 RID: 4055
			public const string MdxScript_ThereAreOtherDefaultMdxScriptsInTheCube = "MdxScript_ThereAreOtherDefaultMdxScriptsInTheCube";

			// Token: 0x04000FD8 RID: 4056
			public const string MdxScript_AnnotationsNotallowedOnCommand = "MdxScript_AnnotationsNotallowedOnCommand";

			// Token: 0x04000FD9 RID: 4057
			public const string SourceIsMissing = "SourceIsMissing";

			// Token: 0x04000FDA RID: 4058
			public const string SourceTableIsUnknown = "SourceTableIsUnknown";

			// Token: 0x04000FDB RID: 4059
			public const string SourceColumnIsUnknown = "SourceColumnIsUnknown";

			// Token: 0x04000FDC RID: 4060
			public const string BindingProperty_CannotCheckReference = "BindingProperty_CannotCheckReference";

			// Token: 0x04000FDD RID: 4061
			public const string BindingProperty_InvalidType = "BindingProperty_InvalidType";

			// Token: 0x04000FDE RID: 4062
			public const string BindingProperty_UnspecifiedReference = "BindingProperty_UnspecifiedReference";

			// Token: 0x04000FDF RID: 4063
			public const string BindingProperty_UnknownReference = "BindingProperty_UnknownReference";

			// Token: 0x04000FE0 RID: 4064
			public const string DataItems_DataType_IsInvalid = "DataItems_DataType_IsInvalid";

			// Token: 0x04000FE1 RID: 4065
			public const string DataItem_DataType_IsInvalid = "DataItem_DataType_IsInvalid";

			// Token: 0x04000FE2 RID: 4066
			public const string DataItem_DataType_IsUnexpected = "DataItem_DataType_IsUnexpected";

			// Token: 0x04000FE3 RID: 4067
			public const string DataItems_Source_NotSpecified = "DataItems_Source_NotSpecified";

			// Token: 0x04000FE4 RID: 4068
			public const string DataItem_Source_NotSpecified = "DataItem_Source_NotSpecified";

			// Token: 0x04000FE5 RID: 4069
			public const string DataItems_Source_InvalidType = "DataItems_Source_InvalidType";

			// Token: 0x04000FE6 RID: 4070
			public const string DataItem_Source_InvalidType = "DataItem_Source_InvalidType";

			// Token: 0x04000FE7 RID: 4071
			public const string DataItems_Source_UndefinedTable = "DataItems_Source_UndefinedTable";

			// Token: 0x04000FE8 RID: 4072
			public const string DataItem_Source_UndefinedTable = "DataItem_Source_UndefinedTable";

			// Token: 0x04000FE9 RID: 4073
			public const string DataItems_Source_InvalidTable = "DataItems_Source_InvalidTable";

			// Token: 0x04000FEA RID: 4074
			public const string DataItem_Source_InvalidTable = "DataItem_Source_InvalidTable";

			// Token: 0x04000FEB RID: 4075
			public const string DataItems_Source_UndefinedColumn = "DataItems_Source_UndefinedColumn";

			// Token: 0x04000FEC RID: 4076
			public const string DataItem_Source_UndefinedColumn = "DataItem_Source_UndefinedColumn";

			// Token: 0x04000FED RID: 4077
			public const string DataItems_Source_InvalidColumn = "DataItems_Source_InvalidColumn";

			// Token: 0x04000FEE RID: 4078
			public const string DataItem_Source_InvalidColumn = "DataItem_Source_InvalidColumn";

			// Token: 0x04000FEF RID: 4079
			public const string DataItems_AllNeedToBeInTheSameTable = "DataItems_AllNeedToBeInTheSameTable";

			// Token: 0x04000FF0 RID: 4080
			public const string DataItem_NullProcessing_UnknownMember = "DataItem_NullProcessing_UnknownMember";

			// Token: 0x04000FF1 RID: 4081
			public const string DataItem_NullProcessing_UnknownMemberForKey = "DataItem_NullProcessing_UnknownMemberForKey";

			// Token: 0x04000FF2 RID: 4082
			public const string DataSourceIsUnknown = "DataSourceIsUnknown";

			// Token: 0x04000FF3 RID: 4083
			public const string NameIsMissing = "NameIsMissing";

			// Token: 0x04000FF4 RID: 4084
			public const string IDIsMissing = "IDIsMissing";

			// Token: 0x04000FF5 RID: 4085
			public const string ParentIsMissing = "ParentIsMissing";

			// Token: 0x04000FF6 RID: 4086
			public const string ParentOfParticularTypeIsMissing = "ParentOfParticularTypeIsMissing";

			// Token: 0x04000FF7 RID: 4087
			public const string FullErrorText = "FullErrorText";

			// Token: 0x04000FF8 RID: 4088
			public const string PropertiesCannotBeSpecifiedInTheSameTime = "PropertiesCannotBeSpecifiedInTheSameTime";

			// Token: 0x04000FF9 RID: 4089
			public const string ReferenceIsInvalid = "ReferenceIsInvalid";

			// Token: 0x04000FFA RID: 4090
			public const string NameColumnShouldBeDefinedIfMoreThanOneKeyColumnIsDefined = "NameColumnShouldBeDefinedIfMoreThanOneKeyColumnIsDefined";

			// Token: 0x04000FFB RID: 4091
			public const string ParentServerIsMissing = "ParentServerIsMissing";

			// Token: 0x04000FFC RID: 4092
			public const string Translation_NotAllow = "Translation_NotAllow";

			// Token: 0x04000FFD RID: 4093
			public const string ProactiveCaching_CanNotEnable = "ProactiveCaching_CanNotEnable";

			// Token: 0x04000FFE RID: 4094
			public const string Partition_IsLargeWithNoAggs1 = "Partition_IsLargeWithNoAggs1";

			// Token: 0x04000FFF RID: 4095
			public const string Partition_IsLargeWithNoAggs2 = "Partition_IsLargeWithNoAggs2";

			// Token: 0x04001000 RID: 4096
			public const string MeasureGroup_HasLargePartitions = "MeasureGroup_HasLargePartitions";

			// Token: 0x04001001 RID: 4097
			public const string MeasureGroup_HasPartitionsToConsolidate = "MeasureGroup_HasPartitionsToConsolidate";

			// Token: 0x04001002 RID: 4098
			public const string AggregationDesign_NotUsedByAnyPartition1 = "AggregationDesign_NotUsedByAnyPartition1";

			// Token: 0x04001003 RID: 4099
			public const string AggregationDesign_NotUsedByAnyPartition2 = "AggregationDesign_NotUsedByAnyPartition2";

			// Token: 0x04001004 RID: 4100
			public const string MeasureGroup_HasTooManyAggregationDesigns = "MeasureGroup_HasTooManyAggregationDesigns";

			// Token: 0x04001005 RID: 4101
			public const string IntermediateGranularityNotAggregated1 = "IntermediateGranularityNotAggregated1";

			// Token: 0x04001006 RID: 4102
			public const string IntermediateGranularityNotAggregated2 = "IntermediateGranularityNotAggregated2";

			// Token: 0x04001007 RID: 4103
			public const string CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup1 = "CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup1";

			// Token: 0x04001008 RID: 4104
			public const string CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup2 = "CubeAttributeAggregationUsageWithUnaryOperatorOrCustomRollup2";

			// Token: 0x04001009 RID: 4105
			public const string PartitionWithTooManyAggregations = "PartitionWithTooManyAggregations";

			// Token: 0x0400100A RID: 4106
			public const string AggregationHasRelatedAttributes1 = "AggregationHasRelatedAttributes1";

			// Token: 0x0400100B RID: 4107
			public const string AggregationHasRelatedAttributes2 = "AggregationHasRelatedAttributes2";

			// Token: 0x0400100C RID: 4108
			public const string Partition_RolapWithNoSlice = "Partition_RolapWithNoSlice";

			// Token: 0x0400100D RID: 4109
			public const string Dimension_IsNotParentChildAndHasNoHierarchy = "Dimension_IsNotParentChildAndHasNoHierarchy";

			// Token: 0x0400100E RID: 4110
			public const string Hierarchy_IsUnNatural1 = "Hierarchy_IsUnNatural1";

			// Token: 0x0400100F RID: 4111
			public const string Hierarchy_IsUnNatural2 = "Hierarchy_IsUnNatural2";

			// Token: 0x04001010 RID: 4112
			public const string DimensionIgnoresDuplicateKeys = "DimensionIgnoresDuplicateKeys";

			// Token: 0x04001011 RID: 4113
			public const string DimensionHasAttributeUsedByLevelsAndWithVisibleHierarchy = "DimensionHasAttributeUsedByLevelsAndWithVisibleHierarchy";

			// Token: 0x04001012 RID: 4114
			public const string Dimension_HasMultipleNonAggregatableAttributes1 = "Dimension_HasMultipleNonAggregatableAttributes1";

			// Token: 0x04001013 RID: 4115
			public const string Dimension_HasMultipleNonAggregatableAttributes2 = "Dimension_HasMultipleNonAggregatableAttributes2";

			// Token: 0x04001014 RID: 4116
			public const string AggregationBelowGranularity1 = "AggregationBelowGranularity1";

			// Token: 0x04001015 RID: 4117
			public const string AggregationBelowGranularity2 = "AggregationBelowGranularity2";

			// Token: 0x04001016 RID: 4118
			public const string DimensionAttribute_IsNonAggregatableInParentChild = "DimensionAttribute_IsNonAggregatableInParentChild";

			// Token: 0x04001017 RID: 4119
			public const string NonAggregatableAttributeNeedsDefaultMember = "NonAggregatableAttributeNeedsDefaultMember";

			// Token: 0x04001018 RID: 4120
			public const string Dimension_KeyAttributeOfParentChildShouldHaveHierarchyNotVisible = "Dimension_KeyAttributeOfParentChildShouldHaveHierarchyNotVisible";

			// Token: 0x04001019 RID: 4121
			public const string Dimension_HasUnknownMemberSetToHidden = "Dimension_HasUnknownMemberSetToHidden";

			// Token: 0x0400101A RID: 4122
			public const string Attribute_LargeAttributeWithNonNumericKey = "Attribute_LargeAttributeWithNonNumericKey";

			// Token: 0x0400101B RID: 4123
			public const string NonKeyLargeAttributeWithVisibleHierarchy = "NonKeyLargeAttributeWithVisibleHierarchy";

			// Token: 0x0400101C RID: 4124
			public const string Dimension_RolapWithUnaryOperatorsOrCustomRollups = "Dimension_RolapWithUnaryOperatorsOrCustomRollups";

			// Token: 0x0400101D RID: 4125
			public const string AttributeTypeAccountOrTimeNeedsMatchingDimension1 = "AttributeTypeAccountOrTimeNeedsMatchingDimension1";

			// Token: 0x0400101E RID: 4126
			public const string AttributeTypeAccountOrTimeNeedsMatchingDimension2 = "AttributeTypeAccountOrTimeNeedsMatchingDimension2";

			// Token: 0x0400101F RID: 4127
			public const string AttributeTypeNeedsMatchingDimension1 = "AttributeTypeNeedsMatchingDimension1";

			// Token: 0x04001020 RID: 4128
			public const string AttributeTypeNeedsMatchingDimension2 = "AttributeTypeNeedsMatchingDimension2";

			// Token: 0x04001021 RID: 4129
			public const string DimensionTypeAccountOrTimeNeedsMatchingAttribute1 = "DimensionTypeAccountOrTimeNeedsMatchingAttribute1";

			// Token: 0x04001022 RID: 4130
			public const string DimensionTypeAccountOrTimeNeedsMatchingAttribute2 = "DimensionTypeAccountOrTimeNeedsMatchingAttribute2";

			// Token: 0x04001023 RID: 4131
			public const string DimensionTypeNeedsMatchingAttribute1 = "DimensionTypeNeedsMatchingAttribute1";

			// Token: 0x04001024 RID: 4132
			public const string DimensionTypeNeedsMatchingAttribute2 = "DimensionTypeNeedsMatchingAttribute2";

			// Token: 0x04001025 RID: 4133
			public const string AttributesTypesDontMatch1 = "AttributesTypesDontMatch1";

			// Token: 0x04001026 RID: 4134
			public const string AttributesTypesDontMatch2 = "AttributesTypesDontMatch2";

			// Token: 0x04001027 RID: 4135
			public const string LevelHasFewerMembersThanUpperLevel = "LevelHasFewerMembersThanUpperLevel";

			// Token: 0x04001028 RID: 4136
			public const string DimensionAndRelationshipTypes = "DimensionAndRelationshipTypes";

			// Token: 0x04001029 RID: 4137
			public const string RedundantRelationship1 = "RedundantRelationship1";

			// Token: 0x0400102A RID: 4138
			public const string RedundantRelationship2 = "RedundantRelationship2";

			// Token: 0x0400102B RID: 4139
			public const string DiamondShapeRelationships1 = "DiamondShapeRelationships1";

			// Token: 0x0400102C RID: 4140
			public const string DiamondShapeRelationships2 = "DiamondShapeRelationships2";

			// Token: 0x0400102D RID: 4141
			public const string AttributeRelationshipName1 = "AttributeRelationshipName1";

			// Token: 0x0400102E RID: 4142
			public const string AttributeRelationshipName2 = "AttributeRelationshipName2";

			// Token: 0x0400102F RID: 4143
			public const string DimensionWithPollingQuery = "DimensionWithPollingQuery";

			// Token: 0x04001030 RID: 4144
			public const string NoTimeDimension1 = "NoTimeDimension1";

			// Token: 0x04001031 RID: 4145
			public const string NoTimeDimension2 = "NoTimeDimension2";

			// Token: 0x04001032 RID: 4146
			public const string TooManyParentChildDimsWithOutlineCalcs = "TooManyParentChildDimsWithOutlineCalcs";

			// Token: 0x04001033 RID: 4147
			public const string ParentChildDimensionWithLargeKey = "ParentChildDimensionWithLargeKey";

			// Token: 0x04001034 RID: 4148
			public const string DimensionProcessByTable1 = "DimensionProcessByTable1";

			// Token: 0x04001035 RID: 4149
			public const string DimensionProcessByTable2 = "DimensionProcessByTable2";

			// Token: 0x04001036 RID: 4150
			public const string Database_TooManyDimensionsWithSingleAttribute = "Database_TooManyDimensionsWithSingleAttribute";

			// Token: 0x04001037 RID: 4151
			public const string DistinctCountMeasure = "DistinctCountMeasure";

			// Token: 0x04001038 RID: 4152
			public const string ManyToManyHasLargeIntermediate = "ManyToManyHasLargeIntermediate";

			// Token: 0x04001039 RID: 4153
			public const string CubeWithSingleDimension = "CubeWithSingleDimension";

			// Token: 0x0400103A RID: 4154
			public const string LinkedDimensionWithOutlineCalculations = "LinkedDimensionWithOutlineCalculations";

			// Token: 0x0400103B RID: 4155
			public const string ReferencedMeasureGroupDimensionNotMaterialized = "ReferencedMeasureGroupDimensionNotMaterialized";

			// Token: 0x0400103C RID: 4156
			public const string IndependentMeasureGroup1 = "IndependentMeasureGroup1";

			// Token: 0x0400103D RID: 4157
			public const string IndependentMeasureGroup2 = "IndependentMeasureGroup2";

			// Token: 0x0400103E RID: 4158
			public const string PartitionWithPollingQuery = "PartitionWithPollingQuery";

			// Token: 0x0400103F RID: 4159
			public const string MeasureGroupsWithTheSameDimensionalityAndGranularity1 = "MeasureGroupsWithTheSameDimensionalityAndGranularity1";

			// Token: 0x04001040 RID: 4160
			public const string MeasureGroupsWithTheSameDimensionalityAndGranularity2 = "MeasureGroupsWithTheSameDimensionalityAndGranularity2";

			// Token: 0x04001041 RID: 4161
			public const string CubeHasTooManyMeasureGroups1 = "CubeHasTooManyMeasureGroups1";

			// Token: 0x04001042 RID: 4162
			public const string CubeHasTooManyMeasureGroups2 = "CubeHasTooManyMeasureGroups2";

			// Token: 0x04001043 RID: 4163
			public const string PerspectiveDefaultMeasureNotIncluded1 = "PerspectiveDefaultMeasureNotIncluded1";

			// Token: 0x04001044 RID: 4164
			public const string PerspectiveDefaultMeasureNotIncluded2 = "PerspectiveDefaultMeasureNotIncluded2";

			// Token: 0x04001045 RID: 4165
			public const string MeasureGroupWithSemiAdditiveMeasuresAndRolapDimensions1 = "MeasureGroupWithSemiAdditiveMeasuresAndRolapDimensions1";

			// Token: 0x04001046 RID: 4166
			public const string MeasureGroupWithSemiAdditiveMeasuresAndRolapDimensions2 = "MeasureGroupWithSemiAdditiveMeasuresAndRolapDimensions2";

			// Token: 0x04001047 RID: 4167
			public const string DotNetSqlClientProvider = "DotNetSqlClientProvider";

			// Token: 0x04001048 RID: 4168
			public const string UnsupportedOledbProvider = "UnsupportedOledbProvider";

			// Token: 0x04001049 RID: 4169
			public const string MeasureGroupWithNoPartitions = "MeasureGroupWithNoPartitions";

			// Token: 0x0400104A RID: 4170
			public const string PartitionIsRemoteRolap = "PartitionIsRemoteRolap";

			// Token: 0x0400104B RID: 4171
			public const string AggregationDesignWithNoEstimatedRows1 = "AggregationDesignWithNoEstimatedRows1";

			// Token: 0x0400104C RID: 4172
			public const string AggregationDesignWithNoEstimatedRows2 = "AggregationDesignWithNoEstimatedRows2";

			// Token: 0x0400104D RID: 4173
			public const string AggregationsForTimeGranularityWithSemiAdditiveMeasures = "AggregationsForTimeGranularityWithSemiAdditiveMeasures";

			// Token: 0x0400104E RID: 4174
			public const string AggregationsForTimeGranularityWithOnlySemiAdditiveMeasures = "AggregationsForTimeGranularityWithOnlySemiAdditiveMeasures";

			// Token: 0x0400104F RID: 4175
			public const string AttributeRelationshipNamedDescription = "AttributeRelationshipNamedDescription";

			// Token: 0x04001050 RID: 4176
			public const string MeasureExpressionSKUError = "MeasureExpressionSKUError";
		}
	}
}
