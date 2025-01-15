using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Metrics
{
	// Token: 0x0200008F RID: 143
	internal static class MetricTerms
	{
		// Token: 0x040001C9 RID: 457
		private const string MetricPropertiesNamePrefix = "_MS";

		// Token: 0x02000115 RID: 277
		public static class Aggregation
		{
			// Token: 0x0200013B RID: 315
			public static class Interval
			{
				// Token: 0x02000142 RID: 322
				public static class Moniker
				{
					// Token: 0x04000480 RID: 1152
					public const string Key = "_MS.AggregationIntervalMs";
				}
			}
		}

		// Token: 0x02000116 RID: 278
		public static class Extraction
		{
			// Token: 0x0200013C RID: 316
			public static class ProcessedByExtractors
			{
				// Token: 0x02000143 RID: 323
				public static class Moniker
				{
					// Token: 0x04000481 RID: 1153
					public const string Key = "_MS.ProcessedByMetricExtractors";

					// Token: 0x04000482 RID: 1154
					public const string ExtractorInfoTemplate = "(Name:'{0}', Ver:'{1}')";
				}
			}
		}

		// Token: 0x02000117 RID: 279
		public static class Autocollection
		{
			// Token: 0x0200013D RID: 317
			public static class Moniker
			{
				// Token: 0x0400047E RID: 1150
				public const string Key = "_MS.IsAutocollected";

				// Token: 0x0400047F RID: 1151
				public const string Value = "True";
			}

			// Token: 0x0200013E RID: 318
			public static class MetricId
			{
				// Token: 0x02000144 RID: 324
				public static class Moniker
				{
					// Token: 0x04000483 RID: 1155
					public const string Key = "_MS.MetricId";
				}
			}

			// Token: 0x0200013F RID: 319
			public static class Metric
			{
				// Token: 0x02000145 RID: 325
				public static class RequestDuration
				{
					// Token: 0x04000484 RID: 1156
					public const string Name = "Server response time";

					// Token: 0x04000485 RID: 1157
					public const string Id = "requests/duration";
				}

				// Token: 0x02000146 RID: 326
				public static class DependencyCallDuration
				{
					// Token: 0x04000486 RID: 1158
					public const string Name = "Dependency duration";

					// Token: 0x04000487 RID: 1159
					public const string Id = "dependencies/duration";
				}
			}

			// Token: 0x02000140 RID: 320
			public static class Request
			{
				// Token: 0x02000147 RID: 327
				public static class PropertyNames
				{
					// Token: 0x04000488 RID: 1160
					public const string Success = "Request.Success";
				}
			}

			// Token: 0x02000141 RID: 321
			public static class DependencyCall
			{
				// Token: 0x02000148 RID: 328
				public static class PropertyNames
				{
					// Token: 0x04000489 RID: 1161
					public const string Success = "Dependency.Success";

					// Token: 0x0400048A RID: 1162
					public const string TypeName = "Dependency.Type";
				}

				// Token: 0x02000149 RID: 329
				public static class TypeNames
				{
					// Token: 0x0400048B RID: 1163
					public const string Other = "Other";

					// Token: 0x0400048C RID: 1164
					public const string Unknown = "Unknown";
				}
			}
		}
	}
}
