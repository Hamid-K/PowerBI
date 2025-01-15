using System;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.Channel
{
	// Token: 0x020000E1 RID: 225
	public class InMemoryChannel : ITelemetryChannel, IDisposable
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x0001AACB File Offset: 0x00018CCB
		public InMemoryChannel()
		{
			this.buffer = new TelemetryBuffer();
			this.transmitter = new InMemoryTransmitter(this.buffer);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001AAFB File Offset: 0x00018CFB
		internal InMemoryChannel(TelemetryBuffer telemetryBuffer, InMemoryTransmitter transmitter)
		{
			this.buffer = telemetryBuffer;
			this.transmitter = transmitter;
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x0001AB1D File Offset: 0x00018D1D
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x0001AB28 File Offset: 0x00018D28
		public bool? DeveloperMode
		{
			get
			{
				return this.developerMode;
			}
			set
			{
				bool? flag = value;
				bool? flag2 = this.developerMode;
				if (!((flag.GetValueOrDefault() == flag2.GetValueOrDefault()) & (flag != null == (flag2 != null))))
				{
					if (value != null && value.Value)
					{
						this.bufferSize = this.buffer.Capacity;
						this.buffer.Capacity = 1;
					}
					else
					{
						this.buffer.Capacity = this.bufferSize;
					}
					this.developerMode = value;
				}
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0001ABAA File Offset: 0x00018DAA
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x0001ABB7 File Offset: 0x00018DB7
		public TimeSpan SendingInterval
		{
			get
			{
				return this.transmitter.SendingInterval;
			}
			set
			{
				this.transmitter.SendingInterval = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0001ABC5 File Offset: 0x00018DC5
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x0001ABD7 File Offset: 0x00018DD7
		public string EndpointAddress
		{
			get
			{
				return this.transmitter.EndpointAddress.ToString();
			}
			set
			{
				this.transmitter.EndpointAddress = new Uri(value);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0001ABEA File Offset: 0x00018DEA
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x0001ABF7 File Offset: 0x00018DF7
		public int MaxTelemetryBufferCapacity
		{
			get
			{
				return this.buffer.Capacity;
			}
			set
			{
				this.buffer.Capacity = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0001AC05 File Offset: 0x00018E05
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x0001AC12 File Offset: 0x00018E12
		public int BacklogSize
		{
			get
			{
				return this.buffer.BacklogSize;
			}
			set
			{
				this.buffer.BacklogSize = value;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0001AC20 File Offset: 0x00018E20
		internal bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001AC28 File Offset: 0x00018E28
		public void Send(ITelemetry item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (this.isDisposed)
			{
				CoreEventSource.Log.InMemoryChannelSendCalledAfterBeingDisposed("Incorrect");
				return;
			}
			if (string.IsNullOrEmpty(item.Context.InstrumentationKey))
			{
				if (CoreEventSource.IsVerboseEnabled)
				{
					CoreEventSource.Log.ItemRejectedNoInstrumentationKey(item.ToString(), "Incorrect");
				}
				return;
			}
			try
			{
				this.buffer.Enqueue(item);
			}
			catch (Exception ex)
			{
				CoreEventSource.Log.LogVerbose("TelemetryBuffer.Enqueue failed: " + ex.ToString(), "Incorrect");
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001ACCC File Offset: 0x00018ECC
		public void Flush()
		{
			this.Flush(default(TimeSpan));
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001ACE8 File Offset: 0x00018EE8
		public void Flush(TimeSpan timeout)
		{
			this.transmitter.Flush(timeout);
			if (this.isDisposed)
			{
				CoreEventSource.Log.InMemoryChannelFlushedAfterBeingDisposed("Incorrect");
			}
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0001AD0D File Offset: 0x00018F0D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0001AD1C File Offset: 0x00018F1C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.isDisposed)
			{
				this.isDisposed = true;
				if (this.transmitter != null)
				{
					this.transmitter.Dispose();
				}
			}
		}

		// Token: 0x04000320 RID: 800
		private readonly TelemetryBuffer buffer;

		// Token: 0x04000321 RID: 801
		private readonly InMemoryTransmitter transmitter;

		// Token: 0x04000322 RID: 802
		private bool? developerMode = new bool?(false);

		// Token: 0x04000323 RID: 803
		private int bufferSize;

		// Token: 0x04000324 RID: 804
		private bool isDisposed;
	}
}
