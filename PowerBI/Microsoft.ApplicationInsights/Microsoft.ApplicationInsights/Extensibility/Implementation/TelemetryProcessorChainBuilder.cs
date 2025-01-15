using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;
using Microsoft.ApplicationInsights.Shared.Extensibility.Implementation;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000085 RID: 133
	public sealed class TelemetryProcessorChainBuilder
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x00012F64 File Offset: 0x00011164
		public TelemetryProcessorChainBuilder(TelemetryConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.configuration = configuration;
			this.factories = new List<Func<ITelemetryProcessor, ITelemetryProcessor>>();
			this.telemetrySink = null;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00012F94 File Offset: 0x00011194
		public TelemetryProcessorChainBuilder(TelemetryConfiguration configuration, TelemetrySink telemetrySink)
			: this(configuration)
		{
			if (telemetrySink == null)
			{
				throw new ArgumentNullException("telemetrySink");
			}
			this.telemetrySink = telemetrySink;
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00012FB3 File Offset: 0x000111B3
		internal TelemetrySink TelemetrySink
		{
			get
			{
				return this.telemetrySink;
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00012FBB File Offset: 0x000111BB
		public TelemetryProcessorChainBuilder Use(Func<ITelemetryProcessor, ITelemetryProcessor> telemetryProcessorFactory)
		{
			this.factories.Add(telemetryProcessorFactory);
			return this;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00012FCC File Offset: 0x000111CC
		public void Build()
		{
			List<ITelemetryProcessor> list = new List<ITelemetryProcessor>();
			ITelemetryProcessor telemetryProcessor;
			if (this.telemetrySink == null)
			{
				if (this.configuration.TelemetrySinks.Count == 1)
				{
					telemetryProcessor = new PassThroughProcessor(this.configuration.DefaultTelemetrySink);
				}
				else
				{
					telemetryProcessor = new BroadcastProcessor(this.configuration.TelemetrySinks);
				}
			}
			else
			{
				telemetryProcessor = new TransmissionProcessor(this.telemetrySink);
			}
			list.Add(telemetryProcessor);
			foreach (Func<ITelemetryProcessor, ITelemetryProcessor> func in this.factories.AsEnumerable<Func<ITelemetryProcessor, ITelemetryProcessor>>().Reverse<Func<ITelemetryProcessor, ITelemetryProcessor>>())
			{
				ITelemetryProcessor telemetryProcessor2 = telemetryProcessor;
				telemetryProcessor = func(telemetryProcessor);
				if (telemetryProcessor == null)
				{
					telemetryProcessor = telemetryProcessor2;
				}
				else
				{
					list.Add(telemetryProcessor);
					ITelemetryModule telemetryModule;
					if ((telemetryModule = telemetryProcessor as ITelemetryModule) != null)
					{
						try
						{
							telemetryModule.Initialize(this.configuration);
						}
						catch (Exception ex)
						{
							CoreEventSource.Log.ComponentInitializationConfigurationError(telemetryModule.ToString(), ex.ToInvariantString(), "Incorrect");
						}
					}
				}
			}
			TelemetryProcessorChain telemetryProcessorChain = new TelemetryProcessorChain(list.AsEnumerable<ITelemetryProcessor>().Reverse<ITelemetryProcessor>());
			if (this.telemetrySink != null)
			{
				this.telemetrySink.TelemetryProcessorChain = telemetryProcessorChain;
				return;
			}
			this.configuration.TelemetryProcessorChain = telemetryProcessorChain;
		}

		// Token: 0x040001A7 RID: 423
		private readonly List<Func<ITelemetryProcessor, ITelemetryProcessor>> factories;

		// Token: 0x040001A8 RID: 424
		private readonly TelemetryConfiguration configuration;

		// Token: 0x040001A9 RID: 425
		private readonly TelemetrySink telemetrySink;
	}
}
