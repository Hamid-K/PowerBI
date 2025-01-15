using System;
using System.Collections.ObjectModel;
using System.Threading;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x0200005E RID: 94
	public sealed class TelemetrySink : IDisposable, ITelemetryModule
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x0000D640 File Offset: 0x0000B840
		public TelemetrySink(TelemetryConfiguration telemetryConfiguration, ITelemetryChannel telemetryChannel = null)
		{
			if (telemetryConfiguration == null)
			{
				throw new ArgumentNullException("telemetryConfiguration");
			}
			this.telemetryConfiguration = telemetryConfiguration;
			if (telemetryChannel != null)
			{
				this.telemetryChannel = telemetryChannel;
				this.shouldDisposeChannel = false;
				return;
			}
			this.telemetryChannel = new InMemoryChannel();
			this.shouldDisposeChannel = true;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000D68D File Offset: 0x0000B88D
		public TelemetrySink()
		{
			this.telemetryChannel = new InMemoryChannel();
			this.shouldDisposeChannel = true;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000D6A7 File Offset: 0x0000B8A7
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		public TelemetryProcessorChainBuilder TelemetryProcessorChainBuilder
		{
			get
			{
				return LazyInitializer.EnsureInitialized<TelemetryProcessorChainBuilder>(ref this.telemetryProcessorChainBuilder, () => new TelemetryProcessorChainBuilder(this.telemetryConfiguration, this));
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.EnsureNotDisposed();
				if (value.TelemetrySink != this)
				{
					throw new ArgumentException("The passed TelemetryProcessorChainBuilder has been configured to use a different TelemetrySink instance", "value");
				}
				if (this.telemetryProcessorChain != null && this.telemetryProcessorChainBuilder != null && this.telemetryProcessorChainBuilder != value)
				{
					this.telemetryProcessorChain.Dispose();
					this.telemetryProcessorChain = null;
				}
				this.telemetryProcessorChainBuilder = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000D72C File Offset: 0x0000B92C
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000D734 File Offset: 0x0000B934
		public ITelemetryChannel TelemetryChannel
		{
			get
			{
				return this.telemetryChannel;
			}
			set
			{
				this.EnsureNotDisposed();
				ITelemetryChannel telemetryChannel = this.telemetryChannel;
				this.telemetryChannel = value;
				if (telemetryChannel != null && telemetryChannel != value && this.shouldDisposeChannel)
				{
					telemetryChannel.Dispose();
					this.shouldDisposeChannel = false;
				}
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000D771 File Offset: 0x0000B971
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000D779 File Offset: 0x0000B979
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000D782 File Offset: 0x0000B982
		public ReadOnlyCollection<ITelemetryProcessor> TelemetryProcessors
		{
			get
			{
				return new ReadOnlyCollection<ITelemetryProcessor>(this.TelemetryProcessorChain.TelemetryProcessors);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000D794 File Offset: 0x0000B994
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000D7B7 File Offset: 0x0000B9B7
		internal TelemetryProcessorChain TelemetryProcessorChain
		{
			get
			{
				if (this.telemetryProcessorChain == null && !this.isDisposed)
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
				this.EnsureNotDisposed();
				this.telemetryProcessorChain = value;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
		public void Dispose()
		{
			this.isDisposed = true;
			if (this.shouldDisposeChannel)
			{
				ITelemetryChannel telemetryChannel = this.telemetryChannel;
				if (telemetryChannel != null)
				{
					telemetryChannel.Dispose();
				}
			}
			this.telemetryChannel = null;
			TelemetryProcessorChain telemetryProcessorChain = this.telemetryProcessorChain;
			if (telemetryProcessorChain != null)
			{
				telemetryProcessorChain.Dispose();
			}
			this.telemetryProcessorChain = null;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000D820 File Offset: 0x0000BA20
		public void Initialize(TelemetryConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.EnsureNotDisposed();
			this.telemetryConfiguration = configuration;
			ITelemetryModule telemetryModule = this.telemetryChannel as ITelemetryModule;
			if (telemetryModule != null)
			{
				telemetryModule.Initialize(configuration);
			}
			foreach (ITelemetryProcessor telemetryProcessor in this.TelemetryProcessorChain.TelemetryProcessors)
			{
				ITelemetryModule telemetryModule2 = telemetryProcessor as ITelemetryModule;
				if (telemetryModule2 != null)
				{
					telemetryModule2.Initialize(configuration);
				}
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		public void Process(ITelemetry item)
		{
			if (this.isDisposed)
			{
				CoreEventSource.Log.TelemetrySinkCalledAfterBeingDisposed("Incorrect");
				return;
			}
			this.TelemetryProcessorChain.Process(item);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000D8D6 File Offset: 0x0000BAD6
		private void EnsureNotDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(this.Name);
			}
		}

		// Token: 0x04000130 RID: 304
		public static readonly string DefaultSinkName = "default";

		// Token: 0x04000131 RID: 305
		private TelemetryConfiguration telemetryConfiguration;

		// Token: 0x04000132 RID: 306
		private ITelemetryChannel telemetryChannel;

		// Token: 0x04000133 RID: 307
		private bool shouldDisposeChannel;

		// Token: 0x04000134 RID: 308
		private TelemetryProcessorChain telemetryProcessorChain;

		// Token: 0x04000135 RID: 309
		private TelemetryProcessorChainBuilder telemetryProcessorChainBuilder;

		// Token: 0x04000136 RID: 310
		private string name;

		// Token: 0x04000137 RID: 311
		private bool isDisposed;
	}
}
