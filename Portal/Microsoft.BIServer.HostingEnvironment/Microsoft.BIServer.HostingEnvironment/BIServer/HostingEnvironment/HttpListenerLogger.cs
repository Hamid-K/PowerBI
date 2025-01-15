using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x02000012 RID: 18
	public class HttpListenerLogger : TraceListener
	{
		// Token: 0x0600007F RID: 127 RVA: 0x0000373C File Offset: 0x0000193C
		public HttpListenerLogger()
			: base("System.Net.HttpListener")
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003749 File Offset: 0x00001949
		public override void Write(string message)
		{
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003749 File Offset: 0x00001949
		public override void WriteLine(string message)
		{
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000374C File Offset: 0x0000194C
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (eventType == TraceEventType.Warning && source == "System.Net.HttpListener")
			{
				string text = "Received a request with an unmatched or no authentication scheme.";
				Match match = new Regex(string.Format("\\[(\\d+)\\] HttpListener#\\d+::HandleAuthentication\\(\\) - {0}", text)).Match(message);
				if (match.Success && match.Groups.Count == 2)
				{
					Logger.Warning(string.Format("{0} 401", text), Array.Empty<object>());
				}
			}
		}
	}
}
