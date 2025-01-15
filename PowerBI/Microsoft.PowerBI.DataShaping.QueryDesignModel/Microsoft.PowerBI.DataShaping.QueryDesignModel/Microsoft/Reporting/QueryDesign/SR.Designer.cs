using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Reporting.QueryDesign
{
	// Token: 0x020000CF RID: 207
	[CompilerGenerated]
	internal class SR
	{
		// Token: 0x06000D41 RID: 3393 RVA: 0x00021FEB File Offset: 0x000201EB
		protected SR()
		{
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00021FF3 File Offset: 0x000201F3
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x00021FFA File Offset: 0x000201FA
		public static CultureInfo Culture
		{
			get
			{
				return SR.Keys.Culture;
			}
			set
			{
				SR.Keys.Culture = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00022002 File Offset: 0x00020202
		public static string CannotInvokeWithGroupOnKey
		{
			get
			{
				return SR.Keys.GetString("CannotInvokeWithGroupOnKey");
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0002200E File Offset: 0x0002020E
		public static string DateTimeCannotBeConvertedToDax
		{
			get
			{
				return SR.Keys.GetString("DateTimeCannotBeConvertedToDax");
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0002201A File Offset: 0x0002021A
		public static string LoadEntityDataModelFailed
		{
			get
			{
				return SR.Keys.GetString("LoadEntityDataModelFailed");
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00022026 File Offset: 0x00020226
		public static string QueryDefinitionIsEmpty
		{
			get
			{
				return SR.Keys.GetString("QueryDefinitionIsEmpty");
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00022032 File Offset: 0x00020232
		public static string ProjectedExpressionWithEmptyAlias
		{
			get
			{
				return SR.Keys.GetString("ProjectedExpressionWithEmptyAlias");
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0002203E File Offset: 0x0002023E
		public static string ProjectedExpressionWithDuplicateAlias
		{
			get
			{
				return SR.Keys.GetString("ProjectedExpressionWithDuplicateAlias");
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0002204A File Offset: 0x0002024A
		public static string QueryDefinitionTranslationFailed
		{
			get
			{
				return SR.Keys.GetString("QueryDefinitionTranslationFailed");
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00022056 File Offset: 0x00020256
		public static string InvalidQueryDefinitionState
		{
			get
			{
				return SR.Keys.GetString("InvalidQueryDefinitionState");
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x00022062 File Offset: 0x00020262
		public static string InvalidQueryExpressionState
		{
			get
			{
				return SR.Keys.GetString("InvalidQueryExpressionState");
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0002206E File Offset: 0x0002026E
		public static string InvalidFilterDefinitionState
		{
			get
			{
				return SR.Keys.GetString("InvalidFilterDefinitionState");
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0002207A File Offset: 0x0002027A
		public static string UnrelatedGroups
		{
			get
			{
				return SR.Keys.GetString("UnrelatedGroups");
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00022086 File Offset: 0x00020286
		public static string UnrelatedMeasures
		{
			get
			{
				return SR.Keys.GetString("UnrelatedMeasures");
			}
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00022092 File Offset: 0x00020292
		public static string CannotInvokeFunction(string functionName)
		{
			return SR.Keys.GetString("CannotInvokeFunction", functionName);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0002209F File Offset: 0x0002029F
		public static string DefaultImageFieldIsNotAnImage(string fieldName)
		{
			return SR.Keys.GetString("DefaultImageFieldIsNotAnImage", fieldName);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x000220AC File Offset: 0x000202AC
		public static string InvalidFieldReference(string fieldName)
		{
			return SR.Keys.GetString("InvalidFieldReference", fieldName);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x000220B9 File Offset: 0x000202B9
		public static string InvalidHierarchyLevelReference(string hierarchyName, string hierarchyLevelName)
		{
			return SR.Keys.GetString("InvalidHierarchyLevelReference", hierarchyName, hierarchyLevelName);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x000220C7 File Offset: 0x000202C7
		public static string InvalidModelMeasure(string measureName)
		{
			return SR.Keys.GetString("InvalidModelMeasure", measureName);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x000220D4 File Offset: 0x000202D4
		public static string InvalidPropertyReference(string name)
		{
			return SR.Keys.GetString("InvalidPropertyReference", name);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000220E1 File Offset: 0x000202E1
		public static string EntitySetNotFound(string fullName)
		{
			return SR.Keys.GetString("EntitySetNotFound", fullName);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x000220EE File Offset: 0x000202EE
		public static string VariableNotFound(string fullName)
		{
			return SR.Keys.GetString("VariableNotFound", fullName);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x000220FB File Offset: 0x000202FB
		public static string FunctionSum(string argument)
		{
			return SR.Keys.GetString("FunctionSum", argument);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00022108 File Offset: 0x00020308
		public static string FunctionAverage(string argument)
		{
			return SR.Keys.GetString("FunctionAverage", argument);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00022115 File Offset: 0x00020315
		public static string FunctionCount(string argument)
		{
			return SR.Keys.GetString("FunctionCount", argument);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00022122 File Offset: 0x00020322
		public static string FunctionDistinctCount(string argument)
		{
			return SR.Keys.GetString("FunctionDistinctCount", argument);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0002212F File Offset: 0x0002032F
		public static string FunctionMin(string argument)
		{
			return SR.Keys.GetString("FunctionMin", argument);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0002213C File Offset: 0x0002033C
		public static string FunctionMax(string argument)
		{
			return SR.Keys.GetString("FunctionMax", argument);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00022149 File Offset: 0x00020349
		public static string HierarchyLevelParentCaption(string entityName, string hierarchyName)
		{
			return SR.Keys.GetString("HierarchyLevelParentCaption", entityName, hierarchyName);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00022157 File Offset: 0x00020357
		public static string KpiGoalCaption(string measureName)
		{
			return SR.Keys.GetString("KpiGoalCaption", measureName);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00022164 File Offset: 0x00020364
		public static string KpiStatusCaption(string measureName)
		{
			return SR.Keys.GetString("KpiStatusCaption", measureName);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00022171 File Offset: 0x00020371
		public static string KpiTrendCaption(string measureName)
		{
			return SR.Keys.GetString("KpiTrendCaption", measureName);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0002217E File Offset: 0x0002037E
		public static string DisplayFolderPath(string parentPath, string folderName)
		{
			return SR.Keys.GetString("DisplayFolderPath", parentPath, folderName);
		}

		// Token: 0x020002ED RID: 749
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06001CF8 RID: 7416 RVA: 0x0004FE9E File Offset: 0x0004E09E
			private Keys()
			{
			}

			// Token: 0x170007E1 RID: 2017
			// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x0004FEA6 File Offset: 0x0004E0A6
			// (set) Token: 0x06001CFA RID: 7418 RVA: 0x0004FEAD File Offset: 0x0004E0AD
			public static CultureInfo Culture
			{
				get
				{
					return SR.Keys._culture;
				}
				set
				{
					SR.Keys._culture = value;
				}
			}

			// Token: 0x06001CFB RID: 7419 RVA: 0x0004FEB5 File Offset: 0x0004E0B5
			public static string GetString(string key)
			{
				return SR.Keys.resourceManager.GetString(key, SR.Keys._culture);
			}

			// Token: 0x06001CFC RID: 7420 RVA: 0x0004FEC7 File Offset: 0x0004E0C7
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, SR.Keys.resourceManager.GetString(key, SR.Keys._culture), arg0);
			}

			// Token: 0x06001CFD RID: 7421 RVA: 0x0004FEE4 File Offset: 0x0004E0E4
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, SR.Keys.resourceManager.GetString(key, SR.Keys._culture), arg0, arg1);
			}

			// Token: 0x0400109A RID: 4250
			private static ResourceManager resourceManager = new ResourceManager(typeof(SR).FullName, typeof(SR).Module.Assembly);

			// Token: 0x0400109B RID: 4251
			private static CultureInfo _culture = null;

			// Token: 0x0400109C RID: 4252
			public const string CannotInvokeFunction = "CannotInvokeFunction";

			// Token: 0x0400109D RID: 4253
			public const string CannotInvokeWithGroupOnKey = "CannotInvokeWithGroupOnKey";

			// Token: 0x0400109E RID: 4254
			public const string DateTimeCannotBeConvertedToDax = "DateTimeCannotBeConvertedToDax";

			// Token: 0x0400109F RID: 4255
			public const string DefaultImageFieldIsNotAnImage = "DefaultImageFieldIsNotAnImage";

			// Token: 0x040010A0 RID: 4256
			public const string LoadEntityDataModelFailed = "LoadEntityDataModelFailed";

			// Token: 0x040010A1 RID: 4257
			public const string QueryDefinitionIsEmpty = "QueryDefinitionIsEmpty";

			// Token: 0x040010A2 RID: 4258
			public const string ProjectedExpressionWithEmptyAlias = "ProjectedExpressionWithEmptyAlias";

			// Token: 0x040010A3 RID: 4259
			public const string ProjectedExpressionWithDuplicateAlias = "ProjectedExpressionWithDuplicateAlias";

			// Token: 0x040010A4 RID: 4260
			public const string QueryDefinitionTranslationFailed = "QueryDefinitionTranslationFailed";

			// Token: 0x040010A5 RID: 4261
			public const string InvalidQueryDefinitionState = "InvalidQueryDefinitionState";

			// Token: 0x040010A6 RID: 4262
			public const string InvalidQueryExpressionState = "InvalidQueryExpressionState";

			// Token: 0x040010A7 RID: 4263
			public const string InvalidFilterDefinitionState = "InvalidFilterDefinitionState";

			// Token: 0x040010A8 RID: 4264
			public const string InvalidFieldReference = "InvalidFieldReference";

			// Token: 0x040010A9 RID: 4265
			public const string InvalidHierarchyLevelReference = "InvalidHierarchyLevelReference";

			// Token: 0x040010AA RID: 4266
			public const string InvalidModelMeasure = "InvalidModelMeasure";

			// Token: 0x040010AB RID: 4267
			public const string InvalidPropertyReference = "InvalidPropertyReference";

			// Token: 0x040010AC RID: 4268
			public const string EntitySetNotFound = "EntitySetNotFound";

			// Token: 0x040010AD RID: 4269
			public const string VariableNotFound = "VariableNotFound";

			// Token: 0x040010AE RID: 4270
			public const string UnrelatedGroups = "UnrelatedGroups";

			// Token: 0x040010AF RID: 4271
			public const string UnrelatedMeasures = "UnrelatedMeasures";

			// Token: 0x040010B0 RID: 4272
			public const string FunctionSum = "FunctionSum";

			// Token: 0x040010B1 RID: 4273
			public const string FunctionAverage = "FunctionAverage";

			// Token: 0x040010B2 RID: 4274
			public const string FunctionCount = "FunctionCount";

			// Token: 0x040010B3 RID: 4275
			public const string FunctionDistinctCount = "FunctionDistinctCount";

			// Token: 0x040010B4 RID: 4276
			public const string FunctionMin = "FunctionMin";

			// Token: 0x040010B5 RID: 4277
			public const string FunctionMax = "FunctionMax";

			// Token: 0x040010B6 RID: 4278
			public const string HierarchyLevelParentCaption = "HierarchyLevelParentCaption";

			// Token: 0x040010B7 RID: 4279
			public const string KpiGoalCaption = "KpiGoalCaption";

			// Token: 0x040010B8 RID: 4280
			public const string KpiStatusCaption = "KpiStatusCaption";

			// Token: 0x040010B9 RID: 4281
			public const string KpiTrendCaption = "KpiTrendCaption";

			// Token: 0x040010BA RID: 4282
			public const string DisplayFolderPath = "DisplayFolderPath";
		}
	}
}
