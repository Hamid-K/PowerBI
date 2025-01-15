using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;
using Microsoft.ApplicationInsights.Metrics;
using Microsoft.ApplicationInsights.Metrics.Extensibility;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x0200005D RID: 93
	public sealed class TelemetryConfiguration : IDisposable
	{
		// Token: 0x060002AD RID: 685 RVA: 0x0000D285 File Offset: 0x0000B485
		[EditorBrowsable(EditorBrowsableState.Never)]
		public TelemetryConfiguration()
			: this(string.Empty, null)
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000D293 File Offset: 0x0000B493
		public TelemetryConfiguration(string instrumentationKey)
			: this(instrumentationKey, null)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000D2A0 File Offset: 0x0000B4A0
		public TelemetryConfiguration(string instrumentationKey, ITelemetryChannel channel)
		{
			if (instrumentationKey == null)
			{
				throw new ArgumentNullException("instrumentationKey");
			}
			this.instrumentationKey = instrumentationKey;
			TelemetrySink telemetrySink = new TelemetrySink(this, channel);
			telemetrySink.Name = "default";
			this.telemetrySinks.Add(telemetrySink);
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000D308 File Offset: 0x0000B508
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000D374 File Offset: 0x0000B574
		public static TelemetryConfiguration Active
		{
			get
			{
				if (TelemetryConfiguration.active == null)
				{
					object obj = TelemetryConfiguration.syncRoot;
					lock (obj)
					{
						if (TelemetryConfiguration.active == null)
						{
							TelemetryConfiguration.active = new TelemetryConfiguration();
							TelemetryConfigurationFactory.Instance.Initialize(TelemetryConfiguration.active, TelemetryModules.Instance);
						}
					}
				}
				return TelemetryConfiguration.active;
			}
			internal set
			{
				object obj = TelemetryConfiguration.syncRoot;
				lock (obj)
				{
					TelemetryConfiguration.active = value;
				}
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		public string InstrumentationKey
		{
			get
			{
				return this.instrumentationKey;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.instrumentationKey = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000D3D3 File Offset: 0x0000B5D3
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000D3DB File Offset: 0x0000B5DB
		public bool DisableTelemetry
		{
			get
			{
				return this.disableTelemetry;
			}
			set
			{
				if (value)
				{
					CoreEventSource.Log.TrackingWasDisabled("Incorrect");
				}
				else
				{
					CoreEventSource.Log.TrackingWasEnabled("Incorrect");
				}
				this.disableTelemetry = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000D407 File Offset: 0x0000B607
		public IList<ITelemetryInitializer> TelemetryInitializers
		{
			get
			{
				return this.telemetryInitializers;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000D40F File Offset: 0x0000B60F
		public ReadOnlyCollection<ITelemetryProcessor> TelemetryProcessors
		{
			get
			{
				return new ReadOnlyCollection<ITelemetryProcessor>(this.TelemetryProcessorChain.TelemetryProcessors);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000D421 File Offset: 0x0000B621
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000D441 File Offset: 0x0000B641
		public TelemetryProcessorChainBuilder TelemetryProcessorChainBuilder
		{
			get
			{
				LazyInitializer.EnsureInitialized<TelemetryProcessorChainBuilder>(ref this.builder, () => new TelemetryProcessorChainBuilder(this));
				return this.builder;
			}
			internal set
			{
				this.builder = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000D44A File Offset: 0x0000B64A
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000D45C File Offset: 0x0000B65C
		public ITelemetryChannel TelemetryChannel
		{
			get
			{
				return this.telemetrySinks.DefaultSink.TelemetryChannel;
			}
			set
			{
				if (!this.isDisposed)
				{
					this.telemetrySinks.DefaultSink.TelemetryChannel = value;
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000D477 File Offset: 0x0000B677
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000D47F File Offset: 0x0000B67F
		public IApplicationIdProvider ApplicationIdProvider { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000D488 File Offset: 0x0000B688
		public IList<TelemetrySink> TelemetrySinks
		{
			get
			{
				return this.telemetrySinks;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000D490 File Offset: 0x0000B690
		public TelemetrySink DefaultTelemetrySink
		{
			get
			{
				return this.telemetrySinks.DefaultSink;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000D49D File Offset: 0x0000B69D
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		internal TelemetryProcessorChain TelemetryProcessorChain
		{
			get
			{
				if (this.telemetryProcessorChain == null)
				{
					this.TelemetryProcessorChainBuilder.Build();
				}
				return this.telemetryProcessorChain;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.telemetryProcessorChain = value;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000D4D0 File Offset: 0x0000B6D0
		public static TelemetryConfiguration CreateDefault()
		{
			TelemetryConfiguration telemetryConfiguration = new TelemetryConfiguration();
			TelemetryConfigurationFactory.Instance.Initialize(telemetryConfiguration, null);
			return telemetryConfiguration;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D4F0 File Offset: 0x0000B6F0
		public static TelemetryConfiguration CreateFromConfiguration(string config)
		{
			if (string.IsNullOrWhiteSpace(config))
			{
				throw new ArgumentNullException("config");
			}
			TelemetryConfiguration telemetryConfiguration = new TelemetryConfiguration();
			TelemetryConfigurationFactory.Instance.Initialize(telemetryConfiguration, null, config);
			return telemetryConfiguration;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D524 File Offset: 0x0000B724
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D534 File Offset: 0x0000B734
		internal MetricManager GetMetricManager(bool createIfNotExists)
		{
			MetricManager metricManager = this.metricManager;
			if (metricManager == null && createIfNotExists)
			{
				MetricManager metricManager2 = new MetricManager(new ApplicationInsightsTelemetryPipeline(this));
				MetricManager metricManager3 = Interlocked.CompareExchange<MetricManager>(ref this.metricManager, metricManager2, null);
				if (metricManager3 == null)
				{
					metricManager = metricManager2;
				}
				else
				{
					metricManager2.StopDefaultAggregationCycleAsync();
					metricManager = metricManager3;
				}
			}
			return metricManager;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D57C File Offset: 0x0000B77C
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed && disposing)
			{
				this.isDisposed = true;
				Interlocked.CompareExchange<TelemetryConfiguration>(ref TelemetryConfiguration.active, null, this);
				MetricManager metricManager = this.metricManager;
				if (metricManager != null)
				{
					metricManager.Flush();
				}
				if (this.telemetryProcessorChain != null)
				{
					this.telemetryProcessorChain.Dispose();
				}
				foreach (TelemetrySink telemetrySink in this.telemetrySinks)
				{
					telemetrySink.Dispose();
					if (telemetrySink != this.telemetrySinks.DefaultSink)
					{
						this.telemetrySinks.Remove(telemetrySink);
					}
				}
			}
		}

		// Token: 0x04000125 RID: 293
		private static object syncRoot = new object();

		// Token: 0x04000126 RID: 294
		private static TelemetryConfiguration active;

		// Token: 0x04000127 RID: 295
		private readonly SnapshottingList<ITelemetryInitializer> telemetryInitializers = new SnapshottingList<ITelemetryInitializer>();

		// Token: 0x04000128 RID: 296
		private readonly TelemetrySinkCollection telemetrySinks = new TelemetrySinkCollection();

		// Token: 0x04000129 RID: 297
		private TelemetryProcessorChain telemetryProcessorChain;

		// Token: 0x0400012A RID: 298
		private string instrumentationKey = string.Empty;

		// Token: 0x0400012B RID: 299
		private bool disableTelemetry;

		// Token: 0x0400012C RID: 300
		private TelemetryProcessorChainBuilder builder;

		// Token: 0x0400012D RID: 301
		private MetricManager metricManager;

		// Token: 0x0400012E RID: 302
		private bool isDisposed;
	}
}
