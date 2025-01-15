using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.Channel
{
	// Token: 0x020000E2 RID: 226
	internal class InMemoryTransmitter : IDisposable
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x0001AD44 File Offset: 0x00018F44
		internal InMemoryTransmitter(TelemetryBuffer buffer)
		{
			this.buffer = buffer;
			this.buffer.OnFull = new Action(this.OnBufferFull);
			Task.Factory.StartNew(new Action(this.Runner), CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default).ContinueWith(delegate(Task task)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "InMemoryTransmitter: Unhandled exception in Runner: {0}", new object[] { task.Exception });
				CoreEventSource.Log.LogVerbose(text, "Incorrect");
			}, TaskContinuationOptions.OnlyOnFaulted);
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001ADF6 File Offset: 0x00018FF6
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x0001ADFE File Offset: 0x00018FFE
		internal Uri EndpointAddress
		{
			get
			{
				return this.endpointAddress;
			}
			set
			{
				Property.Set<Uri>(ref this.endpointAddress, value);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0001AE0C File Offset: 0x0001900C
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x0001AE14 File Offset: 0x00019014
		internal TimeSpan SendingInterval
		{
			get
			{
				return this.sendingInterval;
			}
			set
			{
				this.sendingInterval = value;
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001AE1D File Offset: 0x0001901D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001AE2C File Offset: 0x0001902C
		internal void Flush(TimeSpan timeout)
		{
			SdkInternalOperationsMonitor.Enter();
			try
			{
				this.DequeueAndSend(timeout);
			}
			finally
			{
				SdkInternalOperationsMonitor.Exit();
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001AE60 File Offset: 0x00019060
		private void Runner()
		{
			SdkInternalOperationsMonitor.Enter();
			try
			{
				using (this.startRunnerEvent = new AutoResetEvent(false))
				{
					while (this.enabled)
					{
						this.DequeueAndSend(default(TimeSpan));
						this.startRunnerEvent.WaitOne(this.sendingInterval);
					}
				}
			}
			finally
			{
				SdkInternalOperationsMonitor.Exit();
			}
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001AEDC File Offset: 0x000190DC
		private void OnBufferFull()
		{
			this.startRunnerEvent.Set();
			CoreEventSource.Log.LogVerbose("StartRunnerEvent set as Buffer is full.", "Incorrect");
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0001AF00 File Offset: 0x00019100
		private void DequeueAndSend(TimeSpan timeout)
		{
			object obj = this.sendingLockObj;
			lock (obj)
			{
				IEnumerable<ITelemetry> enumerable = this.buffer.Dequeue();
				try
				{
					this.Send(enumerable, timeout).Wait();
				}
				catch (Exception ex)
				{
					CoreEventSource.Log.FailedToSend(ex.Message, "Incorrect");
				}
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001AF78 File Offset: 0x00019178
		private Task Send(IEnumerable<ITelemetry> telemetryItems, TimeSpan timeout)
		{
			byte[] array = null;
			if (telemetryItems != null)
			{
				array = JsonSerializer.Serialize(telemetryItems, true);
			}
			if (array == null || array.Length == 0)
			{
				CoreEventSource.Log.LogVerbose("No Telemetry Items passed to Enqueue", "Incorrect");
				return Task.FromResult<object>(null);
			}
			return new Transmission(this.endpointAddress, array, JsonSerializer.ContentType, JsonSerializer.CompressionType, timeout).SendAsync();
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001AFD0 File Offset: 0x000191D0
		private void Dispose(bool disposing)
		{
			if (disposing && Interlocked.Increment(ref this.disposeCount) == 1)
			{
				this.enabled = false;
				if (this.startRunnerEvent != null)
				{
					try
					{
						this.startRunnerEvent.Set();
					}
					catch (ObjectDisposedException)
					{
					}
				}
				this.Flush(default(TimeSpan));
			}
		}

		// Token: 0x04000325 RID: 805
		private readonly TelemetryBuffer buffer;

		// Token: 0x04000326 RID: 806
		private object sendingLockObj = new object();

		// Token: 0x04000327 RID: 807
		private AutoResetEvent startRunnerEvent;

		// Token: 0x04000328 RID: 808
		private bool enabled = true;

		// Token: 0x04000329 RID: 809
		private int disposeCount;

		// Token: 0x0400032A RID: 810
		private TimeSpan sendingInterval = TimeSpan.FromSeconds(30.0);

		// Token: 0x0400032B RID: 811
		private Uri endpointAddress = new Uri("https://dc.services.visualstudio.com/v2/track");
	}
}
