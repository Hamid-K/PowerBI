using System;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200030F RID: 783
	internal class DiagnosticsTraceSink : IEventSink
	{
		// Token: 0x06001CAF RID: 7343 RVA: 0x00002061 File Offset: 0x00000261
		public DiagnosticsTraceSink()
		{
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x000572CF File Offset: 0x000554CF
		public DiagnosticsTraceSink(string traceprovider)
		{
			this.SetTraceProvider(traceprovider);
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x000572DE File Offset: 0x000554DE
		public DiagnosticsTraceSink(ITraceProvider traceprovider)
		{
			this._traceProvider = traceprovider;
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x000572F0 File Offset: 0x000554F0
		private void SetTraceProvider(string traceprovider)
		{
			if (!string.IsNullOrEmpty(traceprovider))
			{
				try
				{
					Type type = Type.GetType(traceprovider, true);
					this._traceProvider = (ITraceProvider)Activator.CreateInstance(type);
				}
				catch (Exception ex)
				{
					EventLogWriter.WriteWarning("DataCache.ConfigManager", "Error while loading assembly = {0}, Exception= {1}", new object[] { traceprovider, ex });
					throw;
				}
			}
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00057354 File Offset: 0x00055554
		public void WriteEntry(string src, TraceEventType msgType, string msgText)
		{
			if (string.IsNullOrEmpty(msgText))
			{
				return;
			}
			if (this._traceProvider != null)
			{
				this._traceProvider.WriteEntry(src, msgType, msgText);
				return;
			}
			DiagnosticsTraceUtility.WriteEntry(src, msgType, msgText);
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00002B16 File Offset: 0x00000D16
		public bool Load(string id)
		{
			return true;
		}

		// Token: 0x04000FC6 RID: 4038
		public const string TraceProviderConfiguration = "TraceProviderConfiguration";

		// Token: 0x04000FC7 RID: 4039
		private ITraceProvider _traceProvider;
	}
}
