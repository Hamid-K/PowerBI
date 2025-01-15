using System;
using System.Net;
using Microsoft.ReportingServices.ProcessingRenderingCommon.Tracing;

namespace Microsoft.ReportingServices.ProcessingRenderingCommon.Common
{
	// Token: 0x020000D9 RID: 217
	public class TlsLoggedWebRequestUtils
	{
		// Token: 0x0600078A RID: 1930 RVA: 0x00014304 File Offset: 0x00012504
		public static void Log(WebResponse response)
		{
			EngineTracer.Info("Outbound TLS Protocol (WebRequest): {0}", new object[] { TlsInspector.GetTlsProtocol(response.GetResponseStream()) });
		}
	}
}
