using System;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001C3 RID: 451
	internal class FileLogUtility : ITraceProvider, IDisposable
	{
		// Token: 0x06000EDD RID: 3805 RVA: 0x000325EC File Offset: 0x000307EC
		public FileLogUtility(string filePath)
		{
			this.fileSink = new FileEventSink();
			this.fileSink.Load(filePath);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003260C File Offset: 0x0003080C
		public void WriteEntry(string src, TraceEventType eventType, string message)
		{
			this.fileSink.WriteEntry(src, eventType, message);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00002B16 File Offset: 0x00000D16
		public bool Load(string id)
		{
			return true;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003261C File Offset: 0x0003081C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003262B File Offset: 0x0003082B
		private void Dispose(bool disposing)
		{
			if (disposing && this.fileSink != null)
			{
				this.fileSink.Dispose();
			}
		}

		// Token: 0x04000A2C RID: 2604
		private FileEventSink fileSink;
	}
}
