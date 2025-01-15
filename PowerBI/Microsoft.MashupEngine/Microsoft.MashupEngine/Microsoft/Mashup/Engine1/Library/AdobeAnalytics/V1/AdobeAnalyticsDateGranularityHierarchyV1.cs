using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V1
{
	// Token: 0x02000FA6 RID: 4006
	internal static class AdobeAnalyticsDateGranularityHierarchyV1
	{
		// Token: 0x06006967 RID: 26983 RVA: 0x0016A4D8 File Offset: 0x001686D8
		public static void AddAdobeAnalyticsDateGranularityDimension(this CubeObjectTableBuilder builder, AdobeAnalyticsCubeContextProviderV1 contextProvider)
		{
			List<IdentifierCubeExpression> list = new List<IdentifierCubeExpression>(AdobeAnalyticsDateGranularityHierarchyV1.Hierarchy.Select((AdobeAnalyticsDimension l) => new IdentifierCubeExpression(l.Id)));
			CubeValue cubeValue = CubeContextCubeValue.New(contextProvider, new AdobeAnalyticsCubeContextProvider<AdobeAnalyticsReportDescriptionV1>.AdobeAnalyticsCubeContext(contextProvider, new QueryCubeExpression(contextProvider.CubeId, list, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, EmptyArray<IdentifierCubeExpression>.Instance, null, EmptyArray<CubeSortOrder>.Instance, RowRange.All), EmptyArray<ParameterArguments>.Instance), AdobeAnalyticsDateGranularityHierarchyV1.levelKeys);
			builder.AddDimension(AdobeAnalyticsDateGranularityHierarchyV1.DateGranularityId, AdobeAnalyticsDateGranularityHierarchyV1.DateGranularityName, cubeValue);
		}

		// Token: 0x06006968 RID: 26984 RVA: 0x0016A564 File Offset: 0x00168764
		public static void AddAdobeAnalyticsDateGranularityDisplayFolder(this CubeObjectTableBuilder builder)
		{
			CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.NewWithoutLink();
			CubeObjectTableBuilder cubeObjectTableBuilder2 = CubeObjectTableBuilder.NewWithoutLink();
			foreach (AdobeAnalyticsDimension adobeAnalyticsDimension in AdobeAnalyticsDateGranularityHierarchyV1.Hierarchy)
			{
				cubeObjectTableBuilder2.AddDimensionAttribute(adobeAnalyticsDimension.Id, adobeAnalyticsDimension.Name);
			}
			CubeObjectTableBuilder cubeObjectTableBuilder3 = CubeObjectTableBuilder.NewWithoutLink();
			cubeObjectTableBuilder3.AddDimensionHierarchyFolder(AdobeAnalyticsDateGranularityHierarchyV1.DateGranularityId, AdobeAnalyticsDateGranularityHierarchyV1.DateGranularityName, cubeObjectTableBuilder2.ToTable());
			cubeObjectTableBuilder.AddDimensionFolder(AdobeAnalyticsDateGranularityHierarchyV1.DateGranularityId, AdobeAnalyticsDateGranularityHierarchyV1.DateGranularityName, cubeObjectTableBuilder3.ToTable());
			builder.AddFolder(AdobeAnalyticsDateGranularityHierarchyV1.DateGranularityName, cubeObjectTableBuilder.ToTable());
		}

		// Token: 0x06006969 RID: 26985 RVA: 0x0016A5F2 File Offset: 0x001687F2
		public static bool IsGranularity(string dimensionId)
		{
			return AdobeAnalyticsDateGranularityHierarchyV1.levelKeys.Contains(dimensionId);
		}

		// Token: 0x0600696A RID: 26986 RVA: 0x0016A600 File Offset: 0x00168800
		public static string GetFinestGranularity(IEnumerable<string> dimensions)
		{
			return AdobeAnalyticsDateGranularityHierarchyV1.levelKeys.LastOrDefault((string l) => dimensions.Contains(l));
		}

		// Token: 0x04003A37 RID: 14903
		private static string DateGranularityName = "Date Granularity";

		// Token: 0x04003A38 RID: 14904
		private static string DateGranularityId = "DateGranularity";

		// Token: 0x04003A39 RID: 14905
		public static AdobeAnalyticsDimension[] Hierarchy = new AdobeAnalyticsDimension[]
		{
			AdobeAnalyticsDimension.New("Level 1: Year", "year"),
			AdobeAnalyticsDimension.New("Level 2: Month", "month"),
			AdobeAnalyticsDimension.New("Level 3: Day", "day"),
			AdobeAnalyticsDimension.New("Level 4: Hour", "hour")
		};

		// Token: 0x04003A3A RID: 14906
		private static Keys levelKeys = Keys.New(AdobeAnalyticsDateGranularityHierarchyV1.Hierarchy.Select((AdobeAnalyticsDimension l) => l.Id).ToArray<string>());
	}
}
