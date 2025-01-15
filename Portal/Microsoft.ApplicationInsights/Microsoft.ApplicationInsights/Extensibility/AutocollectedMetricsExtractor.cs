using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Metrics;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x02000060 RID: 96
	public sealed class AutocollectedMetricsExtractor : ITelemetryProcessor, ITelemetryModule, IDisposable
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x0000D9AC File Offset: 0x0000BBAC
		public AutocollectedMetricsExtractor(ITelemetryProcessor nextProcessorInPipeline)
		{
			this.nextProcessorInPipeline = nextProcessorInPipeline;
			this.extractorForRequestMetrics = new RequestMetricsExtractor();
			this.extractorForDependencyMetrics = new DependencyMetricsExtractor();
			this.extractors = new AutocollectedMetricsExtractor.ExtractorWithInfo[]
			{
				new AutocollectedMetricsExtractor.ExtractorWithInfo(this.extractorForRequestMetrics, AutocollectedMetricsExtractor.GetExtractorInfo(this.extractorForRequestMetrics)),
				new AutocollectedMetricsExtractor.ExtractorWithInfo(this.extractorForDependencyMetrics, AutocollectedMetricsExtractor.GetExtractorInfo(this.extractorForDependencyMetrics))
			};
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000DA1A File Offset: 0x0000BC1A
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x0000DA27 File Offset: 0x0000BC27
		public int MaxDependencyTypesToDiscover
		{
			get
			{
				return this.extractorForDependencyMetrics.MaxDependencyTypesToDiscover;
			}
			set
			{
				this.extractorForDependencyMetrics.MaxDependencyTypesToDiscover = value;
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000DA38 File Offset: 0x0000BC38
		public void Initialize(TelemetryConfiguration configuration)
		{
			TelemetryClient telemetryClient = ((configuration == null) ? new TelemetryClient() : new TelemetryClient(configuration));
			if (!string.IsNullOrWhiteSpace("_MS.IsAutocollected"))
			{
				telemetryClient.Context.GlobalProperties["_MS.IsAutocollected"] = "True";
			}
			this.InitializeExtractors(telemetryClient);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000DA83 File Offset: 0x0000BC83
		public void Process(ITelemetry item)
		{
			if (item != null)
			{
				this.ExtractMetrics(item);
			}
			this.InvokeNextProcessor(item);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000DA98 File Offset: 0x0000BC98
		public void Dispose()
		{
			TelemetryClient telemetryClient = this.metricTelemetryClient;
			if (telemetryClient != null)
			{
				telemetryClient.Flush();
				this.InitializeExtractors(null);
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000DABC File Offset: 0x0000BCBC
		private static string GetExtractorInfo(ISpecificAutocollectedMetricsExtractor extractor)
		{
			string text;
			try
			{
				text = ((extractor != null) ? extractor.ExtractorName : null) ?? "null";
			}
			catch
			{
				text = extractor.GetType().FullName;
			}
			string text2;
			try
			{
				text2 = extractor.ExtractorVersion ?? "null";
			}
			catch
			{
				text2 = "unknown";
			}
			return string.Format(CultureInfo.InvariantCulture, "(Name:'{0}', Ver:'{1}')", new object[] { text, text2 });
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000DB48 File Offset: 0x0000BD48
		private static void AddExtractorInfo(ITelemetry item, string extractorInfo)
		{
			if (item is RequestTelemetry)
			{
				RequestTelemetry requestTelemetry = item as RequestTelemetry;
				requestTelemetry.MetricExtractorInfo = AutocollectedMetricsExtractor.ExtractionPipelineInfo(requestTelemetry.MetricExtractorInfo, extractorInfo);
				return;
			}
			if (item is DependencyTelemetry)
			{
				DependencyTelemetry dependencyTelemetry = item as DependencyTelemetry;
				dependencyTelemetry.MetricExtractorInfo = AutocollectedMetricsExtractor.ExtractionPipelineInfo(dependencyTelemetry.MetricExtractorInfo, extractorInfo);
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000DB94 File Offset: 0x0000BD94
		private static string ExtractionPipelineInfo(string extractionPipelineInfo, string extractorInfo)
		{
			if (extractionPipelineInfo != null && extractionPipelineInfo.Length > 0)
			{
				extractionPipelineInfo += "; ";
			}
			else
			{
				extractionPipelineInfo = string.Empty;
			}
			return extractionPipelineInfo + extractorInfo;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		private void InitializeExtractors(TelemetryClient metricsClient)
		{
			this.metricTelemetryClient = metricsClient;
			for (int i = 0; i < this.extractors.Length; i++)
			{
				try
				{
					this.extractors[i].Extractor.InitializeExtractor(metricsClient);
				}
				catch (Exception ex)
				{
					CoreEventSource.Log.LogError("Initialization error in " + this.extractors[i].Info + ": " + ex.ToString(), "Incorrect");
				}
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000DC44 File Offset: 0x0000BE44
		private void ExtractMetrics(ITelemetry fromItem)
		{
			ISupportSampling supportSampling = fromItem as ISupportSampling;
			if (supportSampling != null && !this.EnsureItemNotSampled(supportSampling))
			{
				return;
			}
			for (int i = 0; i < this.extractors.Length; i++)
			{
				AutocollectedMetricsExtractor.ExtractorWithInfo extractorWithInfo = this.extractors[i];
				try
				{
					bool flag;
					extractorWithInfo.Extractor.ExtractMetrics(fromItem, out flag);
					if (flag)
					{
						AutocollectedMetricsExtractor.AddExtractorInfo(fromItem, extractorWithInfo.Info);
					}
				}
				catch (Exception ex)
				{
					CoreEventSource.Log.LogError("Extraction error in " + extractorWithInfo.Extractor.GetType().Name + ": " + ex.ToString(), "Incorrect");
				}
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000DCEC File Offset: 0x0000BEEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool EnsureItemNotSampled(ISupportSampling item)
		{
			if (item.SamplingPercentage != null && item.SamplingPercentage.Value < 99.999999999999)
			{
				if (!this.isMetricExtractorAfterSamplingLogged)
				{
					this.isMetricExtractorAfterSamplingLogged = true;
					CoreEventSource.Log.MetricExtractorAfterSamplingError("Incorrect");
				}
				else
				{
					CoreEventSource.Log.MetricExtractorAfterSamplingVerbose("Incorrect");
				}
				return false;
			}
			return true;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000DD54 File Offset: 0x0000BF54
		private void InvokeNextProcessor(ITelemetry item)
		{
			ITelemetryProcessor telemetryProcessor = this.nextProcessorInPipeline;
			if (telemetryProcessor != null)
			{
				telemetryProcessor.Process(item);
			}
		}

		// Token: 0x0400013A RID: 314
		private readonly RequestMetricsExtractor extractorForRequestMetrics;

		// Token: 0x0400013B RID: 315
		private readonly DependencyMetricsExtractor extractorForDependencyMetrics;

		// Token: 0x0400013C RID: 316
		private readonly AutocollectedMetricsExtractor.ExtractorWithInfo[] extractors;

		// Token: 0x0400013D RID: 317
		private TelemetryClient metricTelemetryClient;

		// Token: 0x0400013E RID: 318
		private ITelemetryProcessor nextProcessorInPipeline;

		// Token: 0x0400013F RID: 319
		private bool isMetricExtractorAfterSamplingLogged;

		// Token: 0x020000FD RID: 253
		private class ExtractorWithInfo
		{
			// Token: 0x060008BE RID: 2238 RVA: 0x0001C494 File Offset: 0x0001A694
			public ExtractorWithInfo(ISpecificAutocollectedMetricsExtractor extractor, string info)
			{
				this.Extractor = extractor;
				this.Info = info;
			}

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x060008BF RID: 2239 RVA: 0x0001C4AA File Offset: 0x0001A6AA
			// (set) Token: 0x060008C0 RID: 2240 RVA: 0x0001C4B2 File Offset: 0x0001A6B2
			public ISpecificAutocollectedMetricsExtractor Extractor { get; private set; }

			// Token: 0x17000281 RID: 641
			// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001C4BB File Offset: 0x0001A6BB
			// (set) Token: 0x060008C2 RID: 2242 RVA: 0x0001C4C3 File Offset: 0x0001A6C3
			public string Info { get; private set; }
		}
	}
}
