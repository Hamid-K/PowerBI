using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200004F RID: 79
	internal static class Internal
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0000B0A4 File Offset: 0x000092A4
		internal static void TraceWarning(string format, params object[] arg)
		{
			RSTrace tracer = Internal.GetTracer();
			if (tracer.TraceWarning)
			{
				tracer.Trace(TraceLevel.Warning, format, arg);
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000B0C8 File Offset: 0x000092C8
		internal static RSTrace GetTracer()
		{
			return RSTrace.SQETracer;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000B0CF File Offset: 0x000092CF
		internal static Stream GetEmbeddedResource(string name)
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(Internal), name);
		}
	}
}
