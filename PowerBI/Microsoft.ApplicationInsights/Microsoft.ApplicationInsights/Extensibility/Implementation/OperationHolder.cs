using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000075 RID: 117
	internal class OperationHolder<T> : IOperationHolder<T>, IDisposable where T : OperationTelemetry
	{
		// Token: 0x06000396 RID: 918 RVA: 0x0001005E File Offset: 0x0000E25E
		public OperationHolder(TelemetryClient telemetryClient, T telemetry)
		{
			if (telemetry == null)
			{
				throw new ArgumentNullException("telemetry");
			}
			if (telemetryClient == null)
			{
				throw new ArgumentNullException("telemetryClient");
			}
			this.telemetryClient = telemetryClient;
			this.Telemetry = telemetry;
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00010095 File Offset: 0x0000E295
		public T Telemetry { get; }

		// Token: 0x06000398 RID: 920 RVA: 0x0001009D File Offset: 0x0000E29D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000100AC File Offset: 0x0000E2AC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.isDisposed)
			{
				lock (this)
				{
					if (!this.isDisposed)
					{
						T operationTelemetry = this.Telemetry;
						operationTelemetry.Stop();
						if (!ActivityExtensions.TryRun(delegate
						{
							Activity activity = Activity.Current;
							string text;
							if ((activity == null || operationTelemetry.Id != activity.Id) && (!operationTelemetry.Properties.TryGetValue("ai_legacyRequestId", out text) || text != ((activity != null) ? activity.Id : null)))
							{
								CoreEventSource.Log.InvalidOperationToStopError("Incorrect");
								CoreEventSource.Log.InvalidOperationToStopDetails(string.Format(CultureInfo.InvariantCulture, "Telemetry Id '{0}' does not match current Activity '{1}'", new object[]
								{
									operationTelemetry.Id,
									(activity != null) ? activity.Id : null
								}), "Incorrect");
								return;
							}
							this.telemetryClient.Track(operationTelemetry);
							activity.Stop();
						}))
						{
							OperationContextForCallContext currentOperationContext = CallContextHelpers.GetCurrentOperationContext();
							if (currentOperationContext == null || operationTelemetry.Id != currentOperationContext.ParentOperationId)
							{
								CoreEventSource.Log.InvalidOperationToStopError("Incorrect");
								CoreEventSource.Log.InvalidOperationToStopDetails(string.Format(CultureInfo.InvariantCulture, "Telemetry Id '{0}' does not match current Activity '{1}'", new object[]
								{
									operationTelemetry.Id,
									(currentOperationContext != null) ? currentOperationContext.ParentOperationId : null
								}), "Incorrect");
								return;
							}
							this.telemetryClient.Track(operationTelemetry);
							CallContextHelpers.RestoreOperationContext(this.ParentContext);
						}
					}
					this.isDisposed = true;
				}
			}
		}

		// Token: 0x04000178 RID: 376
		public OperationContextForCallContext ParentContext;

		// Token: 0x04000179 RID: 377
		private readonly TelemetryClient telemetryClient;

		// Token: 0x0400017A RID: 378
		private bool isDisposed;
	}
}
