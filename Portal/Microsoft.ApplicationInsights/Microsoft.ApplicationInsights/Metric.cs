using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.ApplicationInsights.Metrics;
using Microsoft.ApplicationInsights.Metrics.ConcurrentDatastructures;

namespace Microsoft.ApplicationInsights
{
	// Token: 0x0200001D RID: 29
	public sealed class Metric
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00006354 File Offset: 0x00004554
		[SuppressMessage("Microsoft.Performance", "CA1825: Avoid unnecessary zero-length array allocations", Justification = "Array.Empty<string>() not supported in Net4.5")]
		internal Metric(MetricManager metricManager, MetricIdentifier metricIdentifier, MetricConfiguration configuration)
		{
			Util.ValidateNotNull(metricManager, "metricManager");
			Util.ValidateNotNull(metricIdentifier, "metricIdentifier");
			Metric.EnsureConfigurationValid(metricIdentifier.DimensionsCount, configuration);
			this.metricManager = metricManager;
			this.Identifier = metricIdentifier;
			this.configuration = configuration;
			this.zeroDimSeries = this.metricManager.CreateNewSeries(new MetricIdentifier(this.Identifier.MetricNamespace, this.Identifier.MetricId), null, this.configuration.SeriesConfig);
			if (metricIdentifier.DimensionsCount == 0)
			{
				this.metricSeries = null;
				this.zeroDimSeriesList = new KeyValuePair<string[], MetricSeries>[]
				{
					new KeyValuePair<string[], MetricSeries>(new string[0], this.zeroDimSeries)
				};
				return;
			}
			int[] array = new int[metricIdentifier.DimensionsCount];
			for (int i = 0; i < metricIdentifier.DimensionsCount; i++)
			{
				array[i] = configuration.GetValuesPerDimensionLimit(i + 1);
			}
			this.metricSeries = new MultidimensionalCube2<MetricSeries>(configuration.SeriesCountLimit - 1, new Func<string[], MetricSeries>(this.CreateNewMetricSeries), array);
			this.zeroDimSeriesList = null;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000645A File Offset: 0x0000465A
		public MetricIdentifier Identifier { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00006462 File Offset: 0x00004662
		public int SeriesCount
		{
			get
			{
				int num = 1;
				MultidimensionalCube2<MetricSeries> multidimensionalCube = this.metricSeries;
				return num + ((multidimensionalCube != null) ? multidimensionalCube.TotalPointsCount : 0);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006478 File Offset: 0x00004678
		[SuppressMessage("Microsoft.Usage", "CA2233", Justification = "dimensionNumber is validated.")]
		public IReadOnlyCollection<string> GetDimensionValues(int dimensionNumber)
		{
			this.Identifier.ValidateDimensionNumberForGetter(dimensionNumber);
			int num = dimensionNumber - 1;
			return this.metricSeries.GetDimensionValues(num);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000064A4 File Offset: 0x000046A4
		[SuppressMessage("Microsoft.Design", "CA1024: Use properties where appropriate", Justification = "Completes with non-trivial effort. Method is appropriate.")]
		[SuppressMessage("Microsoft.Performance", "CA1825: Avoid unnecessary zero-length array allocations", Justification = "Array.Empty<string>() not supported in Net4.5")]
		public IReadOnlyList<KeyValuePair<string[], MetricSeries>> GetAllSeries()
		{
			if (this.Identifier.DimensionsCount == 0)
			{
				return this.zeroDimSeriesList;
			}
			List<KeyValuePair<string[], MetricSeries>> list = new List<KeyValuePair<string[], MetricSeries>>(this.SeriesCount);
			list.Add(new KeyValuePair<string[], MetricSeries>(new string[0], this.zeroDimSeries));
			this.metricSeries.GetAllPoints(list);
			return list;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000064F6 File Offset: 0x000046F6
		public bool TryGetDataSeries(out MetricSeries series)
		{
			series = this.zeroDimSeries;
			return true;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006501 File Offset: 0x00004701
		public bool TryGetDataSeries(out MetricSeries series, string dimension1Value)
		{
			return this.TryGetDataSeries(out series, true, new string[] { dimension1Value });
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006515 File Offset: 0x00004715
		public bool TryGetDataSeries(out MetricSeries series, string dimension1Value, string dimension2Value)
		{
			return this.TryGetDataSeries(out series, true, new string[] { dimension1Value, dimension2Value });
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000652D File Offset: 0x0000472D
		public bool TryGetDataSeries(out MetricSeries series, string dimension1Value, string dimension2Value, string dimension3Value)
		{
			return this.TryGetDataSeries(out series, true, new string[] { dimension1Value, dimension2Value, dimension3Value });
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000654A File Offset: 0x0000474A
		public bool TryGetDataSeries(out MetricSeries series, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value)
		{
			return this.TryGetDataSeries(out series, true, new string[] { dimension1Value, dimension2Value, dimension3Value, dimension4Value });
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000656C File Offset: 0x0000476C
		public bool TryGetDataSeries(out MetricSeries series, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value)
		{
			return this.TryGetDataSeries(out series, true, new string[] { dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value });
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006593 File Offset: 0x00004793
		public bool TryGetDataSeries(out MetricSeries series, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value)
		{
			return this.TryGetDataSeries(out series, true, new string[] { dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value });
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000065BF File Offset: 0x000047BF
		public bool TryGetDataSeries(out MetricSeries series, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value, string dimension7Value)
		{
			return this.TryGetDataSeries(out series, true, new string[] { dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value, dimension7Value });
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000065F0 File Offset: 0x000047F0
		public bool TryGetDataSeries(out MetricSeries series, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value, string dimension7Value, string dimension8Value)
		{
			return this.TryGetDataSeries(out series, true, new string[] { dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value, dimension7Value, dimension8Value });
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006626 File Offset: 0x00004826
		public bool TryGetDataSeries(out MetricSeries series, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value, string dimension7Value, string dimension8Value, string dimension9Value)
		{
			return this.TryGetDataSeries(out series, true, new string[] { dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value, dimension7Value, dimension8Value, dimension9Value });
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006664 File Offset: 0x00004864
		public bool TryGetDataSeries(out MetricSeries series, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value, string dimension7Value, string dimension8Value, string dimension9Value, string dimension10Value)
		{
			return this.TryGetDataSeries(out series, true, new string[] { dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value, dimension7Value, dimension8Value, dimension9Value, dimension10Value });
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000066B4 File Offset: 0x000048B4
		public bool TryGetDataSeries(out MetricSeries series, bool createIfNotExists, params string[] dimensionValues)
		{
			if (dimensionValues == null || dimensionValues.Length == 0)
			{
				series = this.zeroDimSeries;
				return true;
			}
			if (this.Identifier.DimensionsCount != dimensionValues.Length)
			{
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Attempted to get a metric series by specifying {0} dimension(s),", new object[] { dimensionValues.Length })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" but this metric has {0} dimensions.", new object[] { this.Identifier.DimensionsCount })));
			}
			for (int i = 0; i < dimensionValues.Length; i++)
			{
				string text = dimensionValues[i];
				if (text == null)
				{
					throw new ArgumentNullException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0}[{1}]", new object[] { "dimensionValues", i })));
				}
				if (text.Length == 0)
				{
					throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0}[{1}]", new object[] { "dimensionValues", i })) + " may not be empty.");
				}
				if (string.IsNullOrWhiteSpace(text))
				{
					throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0}[{1}]", new object[] { "dimensionValues", i })) + " may not be whitespace only.");
				}
			}
			MultidimensionalPointResult<MetricSeries> multidimensionalPointResult = (createIfNotExists ? this.metricSeries.TryGetOrCreatePoint(dimensionValues) : this.metricSeries.TryGetPoint(dimensionValues));
			if (multidimensionalPointResult.IsSuccess)
			{
				series = multidimensionalPointResult.Point;
				return true;
			}
			series = null;
			return false;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000682E File Offset: 0x00004A2E
		public void TrackValue(double metricValue)
		{
			this.zeroDimSeries.TrackValue(metricValue);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000683C File Offset: 0x00004A3C
		public void TrackValue(object metricValue)
		{
			this.zeroDimSeries.TrackValue(metricValue);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000684C File Offset: 0x00004A4C
		public bool TrackValue(double metricValue, string dimension1Value)
		{
			MetricSeries metricSeries;
			bool flag = this.TryGetDataSeries(out metricSeries, dimension1Value);
			if (flag)
			{
				metricSeries.TrackValue(metricValue);
			}
			return flag;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000686C File Offset: 0x00004A6C
		public bool TrackValue(object metricValue, string dimension1Value)
		{
			MetricSeries metricSeries;
			if (this.TryGetDataSeries(out metricSeries, dimension1Value))
			{
				metricSeries.TrackValue(metricValue);
				return true;
			}
			return false;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006890 File Offset: 0x00004A90
		public bool TrackValue(double metricValue, string dimension1Value, string dimension2Value)
		{
			MetricSeries metricSeries;
			bool flag = this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value);
			if (flag)
			{
				metricSeries.TrackValue(metricValue);
			}
			return flag;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000068B4 File Offset: 0x00004AB4
		public bool TrackValue(object metricValue, string dimension1Value, string dimension2Value)
		{
			MetricSeries metricSeries;
			if (this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value))
			{
				metricSeries.TrackValue(metricValue);
				return true;
			}
			return false;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000068D8 File Offset: 0x00004AD8
		public bool TrackValue(double metricValue, string dimension1Value, string dimension2Value, string dimension3Value)
		{
			MetricSeries metricSeries;
			bool flag = this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value);
			if (flag)
			{
				metricSeries.TrackValue(metricValue);
			}
			return flag;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000068FC File Offset: 0x00004AFC
		public bool TrackValue(object metricValue, string dimension1Value, string dimension2Value, string dimension3Value)
		{
			MetricSeries metricSeries;
			if (this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value))
			{
				metricSeries.TrackValue(metricValue);
				return true;
			}
			return false;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006924 File Offset: 0x00004B24
		public bool TrackValue(double metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value)
		{
			MetricSeries metricSeries;
			bool flag = this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value);
			if (flag)
			{
				metricSeries.TrackValue(metricValue);
			}
			return flag;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000694C File Offset: 0x00004B4C
		public bool TrackValue(object metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value)
		{
			MetricSeries metricSeries;
			if (this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value))
			{
				metricSeries.TrackValue(metricValue);
				return true;
			}
			return false;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006974 File Offset: 0x00004B74
		public bool TrackValue(double metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value)
		{
			MetricSeries metricSeries;
			bool flag = this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value);
			if (flag)
			{
				metricSeries.TrackValue(metricValue);
			}
			return flag;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000699C File Offset: 0x00004B9C
		public bool TrackValue(object metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value)
		{
			MetricSeries metricSeries;
			if (this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value))
			{
				metricSeries.TrackValue(metricValue);
				return true;
			}
			return false;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000069C8 File Offset: 0x00004BC8
		public bool TrackValue(double metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value)
		{
			MetricSeries metricSeries;
			bool flag = this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value);
			if (flag)
			{
				metricSeries.TrackValue(metricValue);
			}
			return flag;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000069F4 File Offset: 0x00004BF4
		public bool TrackValue(object metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value)
		{
			MetricSeries metricSeries;
			if (this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value))
			{
				metricSeries.TrackValue(metricValue);
				return true;
			}
			return false;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006A20 File Offset: 0x00004C20
		public bool TrackValue(double metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value, string dimension7Value)
		{
			MetricSeries metricSeries;
			bool flag = this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value, dimension7Value);
			if (flag)
			{
				metricSeries.TrackValue(metricValue);
			}
			return flag;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006A4C File Offset: 0x00004C4C
		public bool TrackValue(object metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value, string dimension7Value)
		{
			MetricSeries metricSeries;
			if (this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value, dimension7Value))
			{
				metricSeries.TrackValue(metricValue);
				return true;
			}
			return false;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00006A7C File Offset: 0x00004C7C
		public bool TrackValue(double metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value, string dimension7Value, string dimension8Value)
		{
			MetricSeries metricSeries;
			bool flag = this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value, dimension7Value, dimension8Value);
			if (flag)
			{
				metricSeries.TrackValue(metricValue);
			}
			return flag;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006AAC File Offset: 0x00004CAC
		public bool TrackValue(object metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value, string dimension7Value, string dimension8Value)
		{
			MetricSeries metricSeries;
			if (this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value, dimension7Value, dimension8Value))
			{
				metricSeries.TrackValue(metricValue);
				return true;
			}
			return false;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00006ADC File Offset: 0x00004CDC
		public bool TrackValue(double metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value, string dimension7Value, string dimension8Value, string dimension9Value)
		{
			MetricSeries metricSeries;
			bool flag = this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value, dimension7Value, dimension8Value, dimension9Value);
			if (flag)
			{
				metricSeries.TrackValue(metricValue);
			}
			return flag;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00006B0C File Offset: 0x00004D0C
		public bool TrackValue(object metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value, string dimension7Value, string dimension8Value, string dimension9Value)
		{
			MetricSeries metricSeries;
			if (this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value, dimension7Value, dimension8Value, dimension9Value))
			{
				metricSeries.TrackValue(metricValue);
				return true;
			}
			return false;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006B40 File Offset: 0x00004D40
		public bool TrackValue(double metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value, string dimension7Value, string dimension8Value, string dimension9Value, string dimension10Value)
		{
			MetricSeries metricSeries;
			bool flag = this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value, dimension7Value, dimension8Value, dimension9Value, dimension10Value);
			if (flag)
			{
				metricSeries.TrackValue(metricValue);
			}
			return flag;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006B74 File Offset: 0x00004D74
		public bool TrackValue(object metricValue, string dimension1Value, string dimension2Value, string dimension3Value, string dimension4Value, string dimension5Value, string dimension6Value, string dimension7Value, string dimension8Value, string dimension9Value, string dimension10Value)
		{
			MetricSeries metricSeries;
			if (this.TryGetDataSeries(out metricSeries, dimension1Value, dimension2Value, dimension3Value, dimension4Value, dimension5Value, dimension6Value, dimension7Value, dimension8Value, dimension9Value, dimension10Value))
			{
				metricSeries.TrackValue(metricValue);
				return true;
			}
			return false;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006BA8 File Offset: 0x00004DA8
		private static void EnsureConfigurationValid(int dimensionsCount, MetricConfiguration configuration)
		{
			Util.ValidateNotNull(configuration, "configuration");
			Util.ValidateNotNull(configuration.SeriesConfig, "SeriesConfig");
			for (int i = 0; i < dimensionsCount; i++)
			{
				if (configuration.GetValuesPerDimensionLimit(i + 1) < 1)
				{
					throw new ArgumentException("Multidimensional metrics must allow at least one dimension-value per dimension" + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" (but {0}({1})", new object[]
					{
						"GetValuesPerDimensionLimit",
						i + 1
					})) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" = {0} was specified.", new object[] { configuration.GetValuesPerDimensionLimit(i + 1) })));
				}
			}
			if (configuration.SeriesCountLimit < 1)
			{
				throw new ArgumentException("Metrics must allow at least one data series" + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" (but {0} was specified)", new object[] { configuration.SeriesCountLimit })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" in {0}.{1}).", new object[] { "configuration", "SeriesCountLimit" })));
			}
			if (dimensionsCount > 0 && configuration.SeriesCountLimit < 2)
			{
				throw new ArgumentException("Multidimensional metrics must allow at least two data series: 1 for the basic (zero-dimensional) series and 1 additional series" + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" (but {0} was specified)", new object[] { configuration.SeriesCountLimit })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" in {0}.{1}).", new object[] { "configuration", "SeriesCountLimit" })));
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006D0C File Offset: 0x00004F0C
		private MetricSeries CreateNewMetricSeries(string[] dimensionValues)
		{
			KeyValuePair<string, string>[] array = null;
			if (dimensionValues != null)
			{
				array = new KeyValuePair<string, string>[dimensionValues.Length];
				for (int i = 0; i < dimensionValues.Length; i++)
				{
					string dimensionName = this.Identifier.GetDimensionName(i + 1);
					string text = dimensionValues[i];
					if (text == null)
					{
						throw new ArgumentNullException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("{0}[{1}]", new object[] { "dimensionValues", i })));
					}
					if (string.IsNullOrWhiteSpace(text))
					{
						throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("The value for dimension number {0} is empty or white-space.", new object[] { i })));
					}
					array[i] = new KeyValuePair<string, string>(dimensionName, text);
				}
			}
			return this.metricManager.CreateNewSeries(this.Identifier, array, this.configuration.SeriesConfig);
		}

		// Token: 0x04000086 RID: 134
		internal readonly MetricConfiguration configuration;

		// Token: 0x04000087 RID: 135
		private readonly MetricSeries zeroDimSeries;

		// Token: 0x04000088 RID: 136
		private readonly IReadOnlyList<KeyValuePair<string[], MetricSeries>> zeroDimSeriesList;

		// Token: 0x04000089 RID: 137
		private readonly MultidimensionalCube2<MetricSeries> metricSeries;

		// Token: 0x0400008A RID: 138
		private readonly MetricManager metricManager;
	}
}
