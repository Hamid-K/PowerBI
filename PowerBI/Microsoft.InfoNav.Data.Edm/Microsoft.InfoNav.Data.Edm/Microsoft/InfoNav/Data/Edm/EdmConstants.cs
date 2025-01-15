using System;
using System.Linq;
using System.Xml.Linq;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200001B RID: 27
	internal static class EdmConstants
	{
		// Token: 0x040000B0 RID: 176
		private const string EdmNamespaceString = "http://schemas.microsoft.com/ado/2008/09/edm";

		// Token: 0x040000B1 RID: 177
		internal const string SqlBIExtensionsNamespace = "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions";

		// Token: 0x040000B2 RID: 178
		internal const string SqlBIEntityContainerExtension = "EntityContainer";

		// Token: 0x040000B3 RID: 179
		internal const string SqlBIEntitySetExtension = "EntitySet";

		// Token: 0x040000B4 RID: 180
		internal const string SqlBIEntityTypeExtension = "EntityType";

		// Token: 0x040000B5 RID: 181
		internal const string SqlBIPropertyExtension = "Property";

		// Token: 0x040000B6 RID: 182
		internal const string SqlBIMeasureExtension = "Measure";

		// Token: 0x040000B7 RID: 183
		internal const string SqlBICulturesExtension = "Cultures";

		// Token: 0x040000B8 RID: 184
		internal const string SqlBINavigationPropertyExtension = "NavigationProperty";

		// Token: 0x040000B9 RID: 185
		internal const string ActiveValueExtension = "Active";

		// Token: 0x040000BA RID: 186
		internal const string AssociationSetExtension = "AssociationSet";

		// Token: 0x040000BB RID: 187
		internal const string DateTableIdentifier = "Time";

		// Token: 0x040000BC RID: 188
		internal const string RowNumberIdentifier = "RowNumber";

		// Token: 0x040000BD RID: 189
		internal const string StableIdentifier = "Stable";

		// Token: 0x040000BE RID: 190
		internal const string GroupOnEntityKey = "GroupOnEntityKey";

		// Token: 0x040000BF RID: 191
		internal const string Discourage = "Discourage";

		// Token: 0x040000C0 RID: 192
		internal const string Encourage = "Encourage";

		// Token: 0x040000C1 RID: 193
		internal const string Enforced = "Enforced";

		// Token: 0x040000C2 RID: 194
		internal const string Relaxed = "Relaxed";

		// Token: 0x040000C3 RID: 195
		internal const string Always = "Always";

		// Token: 0x040000C4 RID: 196
		internal const string ScalarVariables = "ScalarVariables";

		// Token: 0x040000C5 RID: 197
		internal const string TableVariables = "TableVariables";

		// Token: 0x040000C6 RID: 198
		internal const string Unrestricted = "Unrestricted";

		// Token: 0x040000C7 RID: 199
		internal const string CrossFilterDirectionBothValue = "Both";

		// Token: 0x040000C8 RID: 200
		internal const string LimitedToGroupByColumns = "LimitedToGroupByColumns";

		// Token: 0x040000C9 RID: 201
		internal const string AssociationBehaviorWeakValue = "Weak";

		// Token: 0x040000CA RID: 202
		internal const string DefaultNoSupportValue = "0";

		// Token: 0x040000CB RID: 203
		internal const string DefaultSupportValue = "1";

		// Token: 0x040000CC RID: 204
		internal const string FiveStateKpiRangeDefaultValue = "0";

		// Token: 0x040000CD RID: 205
		internal const string FiveStateKpiRangeNormalizedValue = "1";

		// Token: 0x040000CE RID: 206
		private static readonly XNamespace EdmNamespace = "http://schemas.microsoft.com/ado/2008/09/edm";

		// Token: 0x040000CF RID: 207
		private static readonly XNamespace SqlBINamespace = "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions";

		// Token: 0x040000D0 RID: 208
		internal static readonly XName CaptionAttr = "Caption";

		// Token: 0x040000D1 RID: 209
		internal static readonly XName ReferenceNameAttr = "ReferenceName";

		// Token: 0x040000D2 RID: 210
		internal static readonly XName HiddenAttr = "Hidden";

		// Token: 0x040000D3 RID: 211
		internal static readonly XName FormatStringAttr = "FormatString";

		// Token: 0x040000D4 RID: 212
		internal static readonly XName ActualTypeAttr = "ActualType";

		// Token: 0x040000D5 RID: 213
		internal static readonly XName StateAttr = "State";

		// Token: 0x040000D6 RID: 214
		internal static readonly XName ContentsAttr = "Contents";

		// Token: 0x040000D7 RID: 215
		internal static readonly XName StabilityAttr = "Stability";

		// Token: 0x040000D8 RID: 216
		internal static readonly XName NameAttr = "Name";

		// Token: 0x040000D9 RID: 217
		internal static readonly XName RoleAttr = "Role";

		// Token: 0x040000DA RID: 218
		internal static readonly XName TypeAttr = "Type";

		// Token: 0x040000DB RID: 219
		internal static readonly XName AssociationAttr = "Association";

		// Token: 0x040000DC RID: 220
		internal static readonly XName EntitySetAttr = "EntitySet";

		// Token: 0x040000DD RID: 221
		internal static readonly XName ExtendedProperty = "ExtendedProperty";

		// Token: 0x040000DE RID: 222
		internal static readonly XName ValueTypeAttr = "ValueType";

		// Token: 0x040000DF RID: 223
		internal static readonly XName DistinctValueCountAttr = "DistinctValueCount";

		// Token: 0x040000E0 RID: 224
		internal static readonly XName RowCountAttr = "RowCount";

		// Token: 0x040000E1 RID: 225
		internal static readonly XName StatusGraphicAttr = "StatusGraphic";

		// Token: 0x040000E2 RID: 226
		internal static readonly XName TrendGraphicAttr = "TrendGraphic";

		// Token: 0x040000E3 RID: 227
		internal static readonly XName StringValueMaxLengthAttr = "StringValueMaxLength";

		// Token: 0x040000E4 RID: 228
		internal static readonly XName GroupingBehaviorAttr = "GroupingBehavior";

		// Token: 0x040000E5 RID: 229
		internal static readonly XName DefaultAggregateFunction = "DefaultAggregateFunction";

		// Token: 0x040000E6 RID: 230
		internal static readonly XName EntityContainer = EdmConstants.EdmNamespace + "EntityContainer";

		// Token: 0x040000E7 RID: 231
		internal static readonly XName Association = EdmConstants.EdmNamespace + "Association";

		// Token: 0x040000E8 RID: 232
		internal static readonly XName AssociationSet = EdmConstants.EdmNamespace + "AssociationSet";

		// Token: 0x040000E9 RID: 233
		internal static readonly XName End = EdmConstants.EdmNamespace + "End";

		// Token: 0x040000EA RID: 234
		internal static readonly XName Culture = "Culture";

		// Token: 0x040000EB RID: 235
		internal static readonly XName Description = "Description";

		// Token: 0x040000EC RID: 236
		internal static readonly XName Cultures = EdmConstants.SqlBINamespace + "Cultures";

		// Token: 0x040000ED RID: 237
		internal static readonly XName AggregateBehavior = "AggregateBehavior";

		// Token: 0x040000EE RID: 238
		internal static readonly XName CrossFilterDirection = "CrossFilterDirection";

		// Token: 0x040000EF RID: 239
		internal static readonly XName AggregationKindAttr = "AggregationKind";

		// Token: 0x040000F0 RID: 240
		internal static readonly XName BehaviorAttr = "Behavior";

		// Token: 0x040000F1 RID: 241
		internal static readonly XName ParameterMetadata = "ParameterMetadata";

		// Token: 0x040000F2 RID: 242
		internal static readonly XName StableNameAttr = "LineageTag";

		// Token: 0x040000F3 RID: 243
		internal static readonly XName IsErrorAttr = "IsError";

		// Token: 0x040000F4 RID: 244
		internal static readonly XName IgnoreCaseAttr = "IgnoreCase";

		// Token: 0x040000F5 RID: 245
		internal static readonly XName IgnoreNonSpaceAttr = "IgnoreNonSpace";

		// Token: 0x040000F6 RID: 246
		internal static readonly XName IgnoreKanaTypeAttr = "IgnoreKanaType";

		// Token: 0x040000F7 RID: 247
		internal static readonly XName IgnoreWidthAttr = "IgnoreWidth";

		// Token: 0x040000F8 RID: 248
		internal static readonly XName CompareOptionsElem = EdmConstants.SqlBINamespace + "CompareOptions";

		// Token: 0x040000F9 RID: 249
		internal static readonly XName DefaultMeasureElem = EdmConstants.SqlBINamespace + "DefaultMeasure";

		// Token: 0x040000FA RID: 250
		internal static readonly XName PreferOrdinalStringEqualityAtrr = "PreferOrdinalStringEquality";

		// Token: 0x040000FB RID: 251
		internal static readonly XName DefaultLabel = EdmConstants.SqlBINamespace + "DisplayKey";

		// Token: 0x040000FC RID: 252
		internal static readonly XName DefaultFieldSet = EdmConstants.SqlBINamespace + "DefaultDetails";

		// Token: 0x040000FD RID: 253
		internal static readonly XName DefaultImage = EdmConstants.SqlBINamespace + "DefaultImage";

		// Token: 0x040000FE RID: 254
		internal static readonly XName DefaultValue = EdmConstants.SqlBINamespace + "DefaultValue";

		// Token: 0x040000FF RID: 255
		internal static readonly XName MemberRef = EdmConstants.SqlBINamespace + "MemberRef";

		// Token: 0x04000100 RID: 256
		internal static readonly XName PropertyRef = EdmConstants.SqlBINamespace + "PropertyRef";

		// Token: 0x04000101 RID: 257
		internal static readonly XName HierarchyRef = EdmConstants.SqlBINamespace + "HierarchyRef";

		// Token: 0x04000102 RID: 258
		internal static readonly XName KpiRef = EdmConstants.SqlBINamespace + "KpiRef";

		// Token: 0x04000103 RID: 259
		internal static readonly XName BIAssociationSet = EdmConstants.SqlBINamespace + "AssociationSet";

		// Token: 0x04000104 RID: 260
		internal static readonly XName OrderBy = EdmConstants.SqlBINamespace + "OrderBy";

		// Token: 0x04000105 RID: 261
		internal static readonly XName GroupBy = EdmConstants.SqlBINamespace + "GroupBy";

		// Token: 0x04000106 RID: 262
		internal static readonly XName RelatedTo = EdmConstants.SqlBINamespace + "RelatedTo";

		// Token: 0x04000107 RID: 263
		internal static readonly XName FormatBy = EdmConstants.SqlBINamespace + "FormatBy";

		// Token: 0x04000108 RID: 264
		internal static readonly XName ApplyCulture = EdmConstants.SqlBINamespace + "ApplyCulture";

		// Token: 0x04000109 RID: 265
		internal static readonly XName Statistics = EdmConstants.SqlBINamespace + "Statistics";

		// Token: 0x0400010A RID: 266
		internal static readonly XName MinValue = EdmConstants.SqlBINamespace + "MinValue";

		// Token: 0x0400010B RID: 267
		internal static readonly XName MaxValue = EdmConstants.SqlBINamespace + "MaxValue";

		// Token: 0x0400010C RID: 268
		internal static readonly XName Version = EdmConstants.SqlBINamespace + "Version";

		// Token: 0x0400010D RID: 269
		internal static readonly XName Hierarchy = EdmConstants.SqlBINamespace + "Hierarchy";

		// Token: 0x0400010E RID: 270
		internal static readonly XName Level = EdmConstants.SqlBINamespace + "Level";

		// Token: 0x0400010F RID: 271
		internal static readonly XName DisplayFolders = EdmConstants.SqlBINamespace + "DisplayFolders";

		// Token: 0x04000110 RID: 272
		internal static readonly XName DisplayFolder = EdmConstants.SqlBINamespace + "DisplayFolder";

		// Token: 0x04000111 RID: 273
		internal static readonly XName Source = EdmConstants.SqlBINamespace + "Source";

		// Token: 0x04000112 RID: 274
		internal static readonly XName Kpi = EdmConstants.SqlBINamespace + "Kpi";

		// Token: 0x04000113 RID: 275
		internal static readonly XName KpiGoal = EdmConstants.SqlBINamespace + "KpiGoal";

		// Token: 0x04000114 RID: 276
		internal static readonly XName KpiStatus = EdmConstants.SqlBINamespace + "KpiStatus";

		// Token: 0x04000115 RID: 277
		internal static readonly XName KpiTrend = EdmConstants.SqlBINamespace + "KpiTrend";

		// Token: 0x04000116 RID: 278
		internal static readonly XName DistributiveBy = EdmConstants.SqlBINamespace + "DistributiveBy";

		// Token: 0x04000117 RID: 279
		internal static readonly XName EntityRef = EdmConstants.SqlBINamespace + "EntityRef";

		// Token: 0x04000118 RID: 280
		internal static readonly XName MParameters = EdmConstants.SqlBINamespace + "MParameters";

		// Token: 0x04000119 RID: 281
		internal static readonly XName MParameter = EdmConstants.SqlBINamespace + "MParameter";

		// Token: 0x0400011A RID: 282
		internal static readonly XName ParameterValuesColumn = EdmConstants.SqlBINamespace + "ParameterValuesColumn";

		// Token: 0x0400011B RID: 283
		internal static readonly XName Documentation = EdmConstants.SqlBINamespace + "Documentation";

		// Token: 0x0400011C RID: 284
		internal static readonly XName Summary = EdmConstants.SqlBINamespace + "Summary";

		// Token: 0x0400011D RID: 285
		internal static readonly XName Variations = EdmConstants.SqlBINamespace + "Variations";

		// Token: 0x0400011E RID: 286
		internal static readonly XName Variation = EdmConstants.SqlBINamespace + "Variation";

		// Token: 0x0400011F RID: 287
		internal static readonly XName DefaultAttr = "Default";

		// Token: 0x04000120 RID: 288
		internal static readonly XName NavigationPropertyRef = EdmConstants.SqlBINamespace + "NavigationPropertyRef";

		// Token: 0x04000121 RID: 289
		internal static readonly XName DefaultHierarchyRef = EdmConstants.SqlBINamespace + "DefaultHierarchyRef";

		// Token: 0x04000122 RID: 290
		internal static readonly XName DefaultPropertyRef = EdmConstants.SqlBINamespace + "DefaultPropertyRef";

		// Token: 0x04000123 RID: 291
		internal static readonly XName ShowAsVariationsOnlyAttr = "ShowAsVariationsOnly";

		// Token: 0x04000124 RID: 292
		internal static readonly XName PrivateAttr = "Private";

		// Token: 0x04000125 RID: 293
		internal static readonly string[] YearColumnApprovedList = new string[] { "year", "año" };

		// Token: 0x04000126 RID: 294
		internal static readonly string[] YearColumnExcludeList = new string[] { "yearly", "per year", "of year", "years", "por año", "del año", "al año", "años" }.SelectMany((string s) => new string[]
		{
			s,
			s.Replace(' ', '_'),
			s.Replace(" ", string.Empty)
		}).Distinct<string>().ToArray<string>();

		// Token: 0x04000127 RID: 295
		internal static readonly XName ModelCapabilities = EdmConstants.SqlBINamespace + "ModelCapabilities";

		// Token: 0x04000128 RID: 296
		internal static readonly XName CrossFilteringWithinTable = EdmConstants.SqlBINamespace + "CrossFilteringWithinTable";

		// Token: 0x04000129 RID: 297
		internal static readonly XName QueryAggregateUsage = EdmConstants.SqlBINamespace + "QueryAggregateUsage";

		// Token: 0x0400012A RID: 298
		internal static readonly XName DiscourageCompositeModels = EdmConstants.SqlBINamespace + "DiscourageCompositeModels";

		// Token: 0x0400012B RID: 299
		internal static readonly XName GroupByValidation = EdmConstants.SqlBINamespace + "GroupByValidation";

		// Token: 0x0400012C RID: 300
		internal static readonly XName QueryBatching = EdmConstants.SqlBINamespace + "QueryBatching";

		// Token: 0x0400012D RID: 301
		internal static readonly XName ExecutionMetrics = EdmConstants.SqlBINamespace + "ExecutionMetrics";

		// Token: 0x0400012E RID: 302
		internal static readonly XName DaxFunctions = EdmConstants.SqlBINamespace + "DAXFunctions";

		// Token: 0x0400012F RID: 303
		internal static readonly XName DaxExtensionFunctions = EdmConstants.SqlBINamespace + "DaxExtensionFunctions";

		// Token: 0x04000130 RID: 304
		internal static readonly XName DaxExtensionFunction = EdmConstants.SqlBINamespace + "DaxExtensionFunction";

		// Token: 0x04000131 RID: 305
		internal static readonly XName BinaryMinMax = EdmConstants.SqlBINamespace + "BinaryMinMax";

		// Token: 0x04000132 RID: 306
		internal static readonly XName SampleAxisWithLocalMinMax = EdmConstants.SqlBINamespace + "SampleAxisWithLocalMinMax";

		// Token: 0x04000133 RID: 307
		internal static readonly XName SampleCartesianPointsByCover = EdmConstants.SqlBINamespace + "SampleCartesianPointsByCover";

		// Token: 0x04000134 RID: 308
		internal static readonly XName StringMinMax = EdmConstants.SqlBINamespace + "StringMinMax";

		// Token: 0x04000135 RID: 309
		internal static readonly XName SubstituteWithIndex = EdmConstants.SqlBINamespace + "SubstituteWithIndex";

		// Token: 0x04000136 RID: 310
		internal static readonly XName SummarizeColumns = EdmConstants.SqlBINamespace + "SummarizeColumns";

		// Token: 0x04000137 RID: 311
		internal static readonly XName TopNPerLevel = EdmConstants.SqlBINamespace + "TopNPerLevel";

		// Token: 0x04000138 RID: 312
		internal static readonly XName TreatAs = EdmConstants.SqlBINamespace + "TreatAs";

		// Token: 0x04000139 RID: 313
		internal static readonly XName FiveStateKpiRange = EdmConstants.SqlBINamespace + "FiveStateKPIRange";

		// Token: 0x0400013A RID: 314
		internal static readonly XName MultiColumnFiltering = EdmConstants.SqlBINamespace + "MultiColumnFiltering";

		// Token: 0x0400013B RID: 315
		internal static readonly XName Variables = EdmConstants.SqlBINamespace + "Variables";

		// Token: 0x0400013C RID: 316
		internal static readonly XName InOperator = EdmConstants.SqlBINamespace + "InOperator";

		// Token: 0x0400013D RID: 317
		internal static readonly XName OptimizedNotInOperator = EdmConstants.SqlBINamespace + "OptimizedNotInOperator";

		// Token: 0x0400013E RID: 318
		internal static readonly XName TableConstructor = EdmConstants.SqlBINamespace + "TableConstructor";

		// Token: 0x0400013F RID: 319
		internal static readonly XName VirtualColumns = EdmConstants.SqlBINamespace + "VirtualColumns";

		// Token: 0x04000140 RID: 320
		internal static readonly XName DataSourceVariables = EdmConstants.SqlBINamespace + "DataSourceVariables";

		// Token: 0x04000141 RID: 321
		internal static readonly XName EncourageIsEmptyDAXFunctionalUsageElem = EdmConstants.SqlBINamespace + "EncourageIsEmptyDAXFunctionUsage";

		// Token: 0x04000142 RID: 322
		internal static readonly XName NonVisual = EdmConstants.SqlBINamespace + "NonVisual";

		// Token: 0x04000143 RID: 323
		internal static readonly XName LeftOuterJoin = EdmConstants.SqlBINamespace + "LeftOuterJoin";

		// Token: 0x04000144 RID: 324
		internal static readonly XName IsAfter = EdmConstants.SqlBINamespace + "IsAfter";

		// Token: 0x04000145 RID: 325
		internal static readonly XName FormatByLocale = EdmConstants.SqlBINamespace + "FormatByLocale";

		// Token: 0x04000146 RID: 326
		internal static readonly XName VisualCalculations = EdmConstants.SqlBINamespace + "VisualCalculations";

		// Token: 0x04000147 RID: 327
		internal static readonly string XsiNsString = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x04000148 RID: 328
		internal static readonly XNamespace XsiXNamespace = EdmConstants.XsiNsString;

		// Token: 0x04000149 RID: 329
		internal static readonly XName NilAttr = EdmConstants.XsiXNamespace + "nil";
	}
}
