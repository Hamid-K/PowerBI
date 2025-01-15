using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V2
{
	// Token: 0x02000F8A RID: 3978
	internal static class AdobeAnalyticsDateGranularityHierarchyV2
	{
		// Token: 0x060068C6 RID: 26822 RVA: 0x00167EB4 File Offset: 0x001660B4
		public static void AddAdobeAnalyticsDateGranularityDimension(this CubeObjectTableBuilder builder, AdobeAnalyticsCubeContextProviderV2 contextProvider)
		{
			List<IdentifierCubeExpression> list = new List<IdentifierCubeExpression>(AdobeAnalyticsDateGranularityHierarchyV2.Hierarchy.Select((AdobeAnalyticsDateGranularityDimensionV2 l) => new IdentifierCubeExpression(l.Id)));
			CubeValue cubeValue = CubeContextCubeValue.New(contextProvider, new AdobeAnalyticsCubeContextProvider<AdobeAnalyticsReportDescriptionV2>.AdobeAnalyticsCubeContext(contextProvider, new QueryCubeExpression(contextProvider.CubeId, list, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, null, EmptyArray<CubeSortOrder>.Instance, RowRange.All), EmptyArray<ParameterArguments>.Instance), AdobeAnalyticsDateGranularityHierarchyV2.levelKeys);
			builder.AddDimension(AdobeAnalyticsDateGranularityHierarchyV2.DateGranularityId, AdobeAnalyticsDateGranularityHierarchyV2.DateGranularityName, cubeValue);
		}

		// Token: 0x060068C7 RID: 26823 RVA: 0x00167F40 File Offset: 0x00166140
		public static void AddAdobeAnalyticsDateGranularityDisplayFolder(this CubeObjectTableBuilder builder)
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
			CubeObjectTableBuilder cubeObjectTableBuilder2 = CubeObjectTableBuilder.NewWithoutLink();
			foreach (AdobeAnalyticsDateGranularityDimensionV2 adobeAnalyticsDimension in AdobeAnalyticsDateGranularityHierarchyV2.Hierarchy)
			{
				cubeObjectTableBuilder2.AddDimensionAttribute(adobeAnalyticsDimension.Id, adobeAnalyticsDimension.Name);
			}
			CubeObjectTableBuilder cubeObjectTableBuilder3 = CubeObjectTableBuilder.NewWithoutLink();
			cubeObjectTableBuilder3.AddDimensionHierarchyFolder(AdobeAnalyticsDateGranularityHierarchyV2.DateGranularityId, AdobeAnalyticsDateGranularityHierarchyV2.DateGranularityName, cubeObjectTableBuilder2.ToTable());
			cubeObjectTableBuilder.AddDimensionFolder(AdobeAnalyticsDateGranularityHierarchyV2.DateGranularityId, AdobeAnalyticsDateGranularityHierarchyV2.DateGranularityName, cubeObjectTableBuilder3.ToTable());
			builder.AddFolder(AdobeAnalyticsDateGranularityHierarchyV2.DateGranularityName, cubeObjectTableBuilder.ToTable());
		}

		// Token: 0x060068C8 RID: 26824 RVA: 0x00167FCE File Offset: 0x001661CE
		public static bool IsGranularity(string dimensionId)
		{
			return AdobeAnalyticsDateGranularityHierarchyV2.levelKeys.Contains(dimensionId);
		}

		// Token: 0x060068C9 RID: 26825 RVA: 0x00167FDB File Offset: 0x001661DB
		public static int GetNumberForDateString(string dimensionId, string dateString)
		{
			return AdobeAnalyticsDateGranularityHierarchyV2.Hierarchy[AdobeAnalyticsDateGranularityHierarchyV2.levelKeys.IndexOfKey(dimensionId)].GetNumber(dateString);
		}

		// Token: 0x060068CA RID: 26826 RVA: 0x00167FF4 File Offset: 0x001661F4
		public static string GetFinestGranularity(IEnumerable<string> dimensions)
		{
			return AdobeAnalyticsDateGranularityHierarchyV2.levelKeys.LastOrDefault((string l) => dimensions.Contains(l));
		}

		// Token: 0x040039B7 RID: 14775
		private static string DateGranularityName = "Date Granularity";

		// Token: 0x040039B8 RID: 14776
		private static string DateGranularityId = "DateGranularity";

		// Token: 0x040039B9 RID: 14777
		public static AdobeAnalyticsDateGranularityDimensionV2[] Hierarchy = new AdobeAnalyticsDateGranularityDimensionV2[]
		{
			new AdobeAnalyticsDateGranularityDimensionV2("Level 1: Year", "variables/daterangeyear", (string s) => DateTime.ParseExact(s, "yyyy", CultureInfo.InvariantCulture).Year),
			new AdobeAnalyticsDateGranularityDimensionV2("Level 2: Month", "variables/daterangemonth", (string s) => DateTime.ParseExact(s, "MMM yyyy", CultureInfo.InvariantCulture).Month),
			new AdobeAnalyticsDateGranularityDimensionV2("Level 3: Day", "variables/daterangeday", (string s) => DateTime.ParseExact(s, "MMM d, yyyy", CultureInfo.InvariantCulture).Day),
			new AdobeAnalyticsDateGranularityDimensionV2("Level 4: Hour", "variables/daterangehour", (string s) => DateTime.ParseExact(s, "HH:mm yyyy-MM-dd", CultureInfo.InvariantCulture).Hour),
			new AdobeAnalyticsDateGranularityDimensionV2("Level 5: Minute", "variables/daterangeminute", (string s) => DateTime.ParseExact(s, "HH:mm yyyy-MM-dd", CultureInfo.InvariantCulture).Minute)
		};

		// Token: 0x040039BA RID: 14778
		private static readonly Keys levelKeys = Keys.New(AdobeAnalyticsDateGranularityHierarchyV2.Hierarchy.Select((AdobeAnalyticsDateGranularityDimensionV2 l) => l.Id).ToArray<string>());
	}
}
