using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Tracing
{
	// Token: 0x02000013 RID: 19
	public sealed class TextFileListener : TextWriterTraceListener
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00003314 File Offset: 0x00001514
		public TextFileListener(string logsFolder)
			: base(Path.Combine(logsFolder, "{0}_{1}.log".FormatWithInvariantCulture(new object[]
			{
				Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().ProcessName),
				Process.GetCurrentProcess().Id
			})))
		{
			if (!Directory.Exists(logsFolder))
			{
				Directory.CreateDirectory(logsFolder);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003370 File Offset: 0x00001570
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (data.Length == 8)
			{
				string formatedMessage = Tracing.GetFormatedMessage(data);
				base.TraceData(eventCache, source, eventType, id, formatedMessage);
			}
			else
			{
				base.TraceData(eventCache, source, eventType, id, data);
			}
			this.Flush();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000033AD File Offset: 0x000015AD
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			base.TraceData(eventCache, source, eventType, id, data);
			this.Flush();
		}
	}
}
