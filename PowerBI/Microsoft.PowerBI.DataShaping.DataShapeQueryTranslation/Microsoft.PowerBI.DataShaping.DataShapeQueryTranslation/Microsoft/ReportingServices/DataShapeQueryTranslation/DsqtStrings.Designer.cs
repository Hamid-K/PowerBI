using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000061 RID: 97
	[CompilerGenerated]
	internal class DsqtStrings
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0000FA57 File Offset: 0x0000DC57
		protected DsqtStrings()
		{
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000FA5F File Offset: 0x0000DC5F
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x0000FA66 File Offset: 0x0000DC66
		public static CultureInfo Culture
		{
			get
			{
				return DsqtStrings.Keys.Culture;
			}
			set
			{
				DsqtStrings.Keys.Culture = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0000FA6E File Offset: 0x0000DC6E
		public static string DaxExternalContent_Inline
		{
			get
			{
				return DsqtStrings.Keys.GetString("DaxExternalContent_Inline");
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000FA7A File Offset: 0x0000DC7A
		public static string UnexpectedError
		{
			get
			{
				return DsqtStrings.Keys.GetString("UnexpectedError");
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000FA86 File Offset: 0x0000DC86
		public static string DaxExternalContent_ExtensionMeasure(string itemName)
		{
			return DsqtStrings.Keys.GetString("DaxExternalContent_ExtensionMeasure", itemName);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000FA93 File Offset: 0x0000DC93
		public static string DaxExternalContent_ExtensionColumn(string itemName)
		{
			return DsqtStrings.Keys.GetString("DaxExternalContent_ExtensionColumn", itemName);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000FAA0 File Offset: 0x0000DCA0
		public static string InvalidDataShapeNoOutputData(string objectType, string objectId, string propertyName)
		{
			return DsqtStrings.Keys.GetString("InvalidDataShapeNoOutputData", objectType, objectId, propertyName);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000FAAF File Offset: 0x0000DCAF
		public static string InvalidDataShapeQuery(string details)
		{
			return DsqtStrings.Keys.GetString("InvalidDataShapeQuery", details);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000FABC File Offset: 0x0000DCBC
		public static string InvalidExtensionDax_MissingToken(string objectType, string objectId, string propertyName, string extensionItem, string token, string line, string position)
		{
			return DsqtStrings.Keys.GetString("InvalidExtensionDax_MissingToken", objectType, objectId, propertyName, extensionItem, token, line, position);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000FAD2 File Offset: 0x0000DCD2
		public static string InvalidExtensionDax_UnexpectedToken(string objectType, string objectId, string propertyName, string extensionItem, string token, string line, string position)
		{
			return DsqtStrings.Keys.GetString("InvalidExtensionDax_UnexpectedToken", objectType, objectId, propertyName, extensionItem, token, line, position);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
		public static string ModelUnavailable(string objectType, string objectId, string propertyName, string details)
		{
			return DsqtStrings.Keys.GetString("ModelUnavailable", objectType, objectId, propertyName, details);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
		public static string InvalidUnconstrainedJoin(string objectType, string objectId, string propertyName)
		{
			return DsqtStrings.Keys.GetString("InvalidUnconstrainedJoin", objectType, objectId, propertyName);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0000FB07 File Offset: 0x0000DD07
		public static string InternalDataShapeQueryError(string errorCode)
		{
			return DsqtStrings.Keys.GetString("InternalDataShapeQueryError", errorCode);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0000FB14 File Offset: 0x0000DD14
		public static string SuppressJoinPredicateOnNonMeasure(string objectType, string objectId, string propertyName)
		{
			return DsqtStrings.Keys.GetString("SuppressJoinPredicateOnNonMeasure", objectType, objectId, propertyName);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000FB23 File Offset: 0x0000DD23
		public static string ModelGroupingInstructionsIgnored(string objectType, string objectId, string propertyName, string groupKey)
		{
			return DsqtStrings.Keys.GetString("ModelGroupingInstructionsIgnored", objectType, objectId, propertyName, groupKey);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000FB33 File Offset: 0x0000DD33
		public static string OverlappingKeysOnOppositeHierarchies(string objectType, string objectId)
		{
			return DsqtStrings.Keys.GetString("OverlappingKeysOnOppositeHierarchies", objectType, objectId);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000FB41 File Offset: 0x0000DD41
		public static string NaNLiteralNotSupported(string objectType, string objectId, string propertyName)
		{
			return DsqtStrings.Keys.GetString("NaNLiteralNotSupported", objectType, objectId, propertyName);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000FB50 File Offset: 0x0000DD50
		public static string SubtotalAndNonSubtotalCalculations(string objectType, string objectId, string propertyName)
		{
			return DsqtStrings.Keys.GetString("SubtotalAndNonSubtotalCalculations", objectType, objectId, propertyName);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000FB5F File Offset: 0x0000DD5F
		public static string IsRelatedToManyNotSupportedForDetailTable(string objectType, string objectId, string propertyName)
		{
			return DsqtStrings.Keys.GetString("IsRelatedToManyNotSupportedForDetailTable", objectType, objectId, propertyName);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000FB6E File Offset: 0x0000DD6E
		public static string ModelMeasuresNotSupportedForDetailTable(string objectType, string objectId, string propertyName, string measureName)
		{
			return DsqtStrings.Keys.GetString("ModelMeasuresNotSupportedForDetailTable", objectType, objectId, propertyName, measureName);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000FB7E File Offset: 0x0000DD7E
		public static string NoUniqueKeyForDetailTable(string objectType, string objectId, string propertyName)
		{
			return DsqtStrings.Keys.GetString("NoUniqueKeyForDetailTable", objectType, objectId, propertyName);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000FB8D File Offset: 0x0000DD8D
		public static string UnsupportedDateTimeLiteral(string objectType, string objectId, string propertyName, string dateTimeValue, string minimumDatetime)
		{
			return DsqtStrings.Keys.GetString("UnsupportedDateTimeLiteral", objectType, objectId, propertyName, dateTimeValue, minimumDatetime);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000FB9F File Offset: 0x0000DD9F
		public static string ComplexSlicerNotAllowedWithMeasures(string objectType, string objectId, string propertyName)
		{
			return DsqtStrings.Keys.GetString("ComplexSlicerNotAllowedWithMeasures", objectType, objectId, propertyName);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000FBAE File Offset: 0x0000DDAE
		public static string ComplexHighlightsNotAllowed(string objectType, string objectId, string propertyName, string calculationId)
		{
			return DsqtStrings.Keys.GetString("ComplexHighlightsNotAllowed", objectType, objectId, propertyName, calculationId);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000FBBE File Offset: 0x0000DDBE
		public static string UnsupportedStringMinMaxColumn(string objectType, string objectId, string propertyName)
		{
			return DsqtStrings.Keys.GetString("UnsupportedStringMinMaxColumn", objectType, objectId, propertyName);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000FBCD File Offset: 0x0000DDCD
		public static string UnsupportedStringMinMaxExpression(string objectType, string objectId, string propertyName)
		{
			return DsqtStrings.Keys.GetString("UnsupportedStringMinMaxExpression", objectType, objectId, propertyName);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000FBDC File Offset: 0x0000DDDC
		public static string InvalidDeepComplexSlicer(string objectType, string objectId, string propertyName, int limit)
		{
			return DsqtStrings.Keys.GetString("InvalidDeepComplexSlicer", objectType, objectId, propertyName, limit);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000FBF1 File Offset: 0x0000DDF1
		public static string InvalidFilterConditionExceedsMaxNumberOfValuesForInFilter(string objectType, string objectId, string propertyName, int maxNumber)
		{
			return DsqtStrings.Keys.GetString("InvalidFilterConditionExceedsMaxNumberOfValuesForInFilter", objectType, objectId, propertyName, maxNumber);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000FC06 File Offset: 0x0000DE06
		public static string UnsupportedNegatedTuplesFilter(string objectType, string objectId, string propertyName)
		{
			return DsqtStrings.Keys.GetString("UnsupportedNegatedTuplesFilter", objectType, objectId, propertyName);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000FC15 File Offset: 0x0000DE15
		public static string InvalidFilterConditionExceedsMaxNumberOfValuesForInFilterTreeRewrite(string objectType, string objectId, string propertyName, int maxNumber)
		{
			return DsqtStrings.Keys.GetString("InvalidFilterConditionExceedsMaxNumberOfValuesForInFilterTreeRewrite", objectType, objectId, propertyName, maxNumber);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000FC2A File Offset: 0x0000DE2A
		public static string InvalidInFilterWithDuplicateColumns(string objectType, string objectId, string propertyName)
		{
			return DsqtStrings.Keys.GetString("InvalidInFilterWithDuplicateColumns", objectType, objectId, propertyName);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000FC39 File Offset: 0x0000DE39
		public static string InvalidFilterConditionExceedsMaxNumberOfDisjunctionsForSubqueryRewrite(string objectType, string objectId, string propertyName, int maxNumber)
		{
			return DsqtStrings.Keys.GetString("InvalidFilterConditionExceedsMaxNumberOfDisjunctionsForSubqueryRewrite", objectType, objectId, propertyName, maxNumber);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000FC4E File Offset: 0x0000DE4E
		public static string TranslationMaximumDurationExceeded(int durationInMs)
		{
			return DsqtStrings.Keys.GetString("TranslationMaximumDurationExceeded", durationInMs);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000FC60 File Offset: 0x0000DE60
		public static string InTableFilterNotSupportedForModel(string objectType, string objectId, string propertyName)
		{
			return DsqtStrings.Keys.GetString("InTableFilterNotSupportedForModel", objectType, objectId, propertyName);
		}

		// Token: 0x02000276 RID: 630
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06001526 RID: 5414 RVA: 0x0004F7A0 File Offset: 0x0004D9A0
			private Keys()
			{
			}

			// Token: 0x170003CE RID: 974
			// (get) Token: 0x06001527 RID: 5415 RVA: 0x0004F7A8 File Offset: 0x0004D9A8
			// (set) Token: 0x06001528 RID: 5416 RVA: 0x0004F7AF File Offset: 0x0004D9AF
			public static CultureInfo Culture
			{
				get
				{
					return DsqtStrings.Keys._culture;
				}
				set
				{
					DsqtStrings.Keys._culture = value;
				}
			}

			// Token: 0x06001529 RID: 5417 RVA: 0x0004F7B7 File Offset: 0x0004D9B7
			public static string GetString(string key)
			{
				return DsqtStrings.Keys.resourceManager.GetString(key, DsqtStrings.Keys._culture);
			}

			// Token: 0x0600152A RID: 5418 RVA: 0x0004F7C9 File Offset: 0x0004D9C9
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, DsqtStrings.Keys.resourceManager.GetString(key, DsqtStrings.Keys._culture), arg0);
			}

			// Token: 0x0600152B RID: 5419 RVA: 0x0004F7E6 File Offset: 0x0004D9E6
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, DsqtStrings.Keys.resourceManager.GetString(key, DsqtStrings.Keys._culture), arg0, arg1);
			}

			// Token: 0x0600152C RID: 5420 RVA: 0x0004F804 File Offset: 0x0004DA04
			public static string GetString(string key, object arg0, object arg1, object arg2)
			{
				return string.Format(CultureInfo.CurrentCulture, DsqtStrings.Keys.resourceManager.GetString(key, DsqtStrings.Keys._culture), arg0, arg1, arg2);
			}

			// Token: 0x0600152D RID: 5421 RVA: 0x0004F823 File Offset: 0x0004DA23
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3)
			{
				return string.Format(CultureInfo.CurrentCulture, DsqtStrings.Keys.resourceManager.GetString(key, DsqtStrings.Keys._culture), new object[] { arg0, arg1, arg2, arg3 });
			}

			// Token: 0x0600152E RID: 5422 RVA: 0x0004F856 File Offset: 0x0004DA56
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3, object arg4)
			{
				return string.Format(CultureInfo.CurrentCulture, DsqtStrings.Keys.resourceManager.GetString(key, DsqtStrings.Keys._culture), new object[] { arg0, arg1, arg2, arg3, arg4 });
			}

			// Token: 0x0600152F RID: 5423 RVA: 0x0004F890 File Offset: 0x0004DA90
			public static string GetString(string key, object arg0, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
			{
				return string.Format(CultureInfo.CurrentCulture, DsqtStrings.Keys.resourceManager.GetString(key, DsqtStrings.Keys._culture), new object[] { arg0, arg1, arg2, arg3, arg4, arg5, arg6 });
			}

			// Token: 0x04000991 RID: 2449
			private static ResourceManager resourceManager = new ResourceManager(typeof(DsqtStrings).FullName, typeof(DsqtStrings).Module.Assembly);

			// Token: 0x04000992 RID: 2450
			private static CultureInfo _culture = null;

			// Token: 0x04000993 RID: 2451
			public const string DaxExternalContent_ExtensionMeasure = "DaxExternalContent_ExtensionMeasure";

			// Token: 0x04000994 RID: 2452
			public const string DaxExternalContent_ExtensionColumn = "DaxExternalContent_ExtensionColumn";

			// Token: 0x04000995 RID: 2453
			public const string DaxExternalContent_Inline = "DaxExternalContent_Inline";

			// Token: 0x04000996 RID: 2454
			public const string InvalidDataShapeNoOutputData = "InvalidDataShapeNoOutputData";

			// Token: 0x04000997 RID: 2455
			public const string InvalidDataShapeQuery = "InvalidDataShapeQuery";

			// Token: 0x04000998 RID: 2456
			public const string InvalidExtensionDax_MissingToken = "InvalidExtensionDax_MissingToken";

			// Token: 0x04000999 RID: 2457
			public const string InvalidExtensionDax_UnexpectedToken = "InvalidExtensionDax_UnexpectedToken";

			// Token: 0x0400099A RID: 2458
			public const string ModelUnavailable = "ModelUnavailable";

			// Token: 0x0400099B RID: 2459
			public const string UnexpectedError = "UnexpectedError";

			// Token: 0x0400099C RID: 2460
			public const string InvalidUnconstrainedJoin = "InvalidUnconstrainedJoin";

			// Token: 0x0400099D RID: 2461
			public const string InternalDataShapeQueryError = "InternalDataShapeQueryError";

			// Token: 0x0400099E RID: 2462
			public const string SuppressJoinPredicateOnNonMeasure = "SuppressJoinPredicateOnNonMeasure";

			// Token: 0x0400099F RID: 2463
			public const string ModelGroupingInstructionsIgnored = "ModelGroupingInstructionsIgnored";

			// Token: 0x040009A0 RID: 2464
			public const string OverlappingKeysOnOppositeHierarchies = "OverlappingKeysOnOppositeHierarchies";

			// Token: 0x040009A1 RID: 2465
			public const string NaNLiteralNotSupported = "NaNLiteralNotSupported";

			// Token: 0x040009A2 RID: 2466
			public const string SubtotalAndNonSubtotalCalculations = "SubtotalAndNonSubtotalCalculations";

			// Token: 0x040009A3 RID: 2467
			public const string IsRelatedToManyNotSupportedForDetailTable = "IsRelatedToManyNotSupportedForDetailTable";

			// Token: 0x040009A4 RID: 2468
			public const string ModelMeasuresNotSupportedForDetailTable = "ModelMeasuresNotSupportedForDetailTable";

			// Token: 0x040009A5 RID: 2469
			public const string NoUniqueKeyForDetailTable = "NoUniqueKeyForDetailTable";

			// Token: 0x040009A6 RID: 2470
			public const string UnsupportedDateTimeLiteral = "UnsupportedDateTimeLiteral";

			// Token: 0x040009A7 RID: 2471
			public const string ComplexSlicerNotAllowedWithMeasures = "ComplexSlicerNotAllowedWithMeasures";

			// Token: 0x040009A8 RID: 2472
			public const string ComplexHighlightsNotAllowed = "ComplexHighlightsNotAllowed";

			// Token: 0x040009A9 RID: 2473
			public const string UnsupportedStringMinMaxColumn = "UnsupportedStringMinMaxColumn";

			// Token: 0x040009AA RID: 2474
			public const string UnsupportedStringMinMaxExpression = "UnsupportedStringMinMaxExpression";

			// Token: 0x040009AB RID: 2475
			public const string InvalidDeepComplexSlicer = "InvalidDeepComplexSlicer";

			// Token: 0x040009AC RID: 2476
			public const string InvalidFilterConditionExceedsMaxNumberOfValuesForInFilter = "InvalidFilterConditionExceedsMaxNumberOfValuesForInFilter";

			// Token: 0x040009AD RID: 2477
			public const string UnsupportedNegatedTuplesFilter = "UnsupportedNegatedTuplesFilter";

			// Token: 0x040009AE RID: 2478
			public const string InvalidFilterConditionExceedsMaxNumberOfValuesForInFilterTreeRewrite = "InvalidFilterConditionExceedsMaxNumberOfValuesForInFilterTreeRewrite";

			// Token: 0x040009AF RID: 2479
			public const string InvalidInFilterWithDuplicateColumns = "InvalidInFilterWithDuplicateColumns";

			// Token: 0x040009B0 RID: 2480
			public const string InvalidFilterConditionExceedsMaxNumberOfDisjunctionsForSubqueryRewrite = "InvalidFilterConditionExceedsMaxNumberOfDisjunctionsForSubqueryRewrite";

			// Token: 0x040009B1 RID: 2481
			public const string TranslationMaximumDurationExceeded = "TranslationMaximumDurationExceeded";

			// Token: 0x040009B2 RID: 2482
			public const string InTableFilterNotSupportedForModel = "InTableFilterNotSupportedForModel";
		}
	}
}
