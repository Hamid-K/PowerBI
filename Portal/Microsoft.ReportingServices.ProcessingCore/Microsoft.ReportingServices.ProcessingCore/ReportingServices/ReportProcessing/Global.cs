using System;
using System.Reflection;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005F9 RID: 1529
	internal sealed class Global
	{
		// Token: 0x06005453 RID: 21587 RVA: 0x001622B4 File Offset: 0x001604B4
		private Global()
		{
		}

		// Token: 0x04002CE4 RID: 11492
		internal static readonly string ReportProcessingNamespace = "Microsoft.ReportingServices.ReportProcessing";

		// Token: 0x04002CE5 RID: 11493
		internal static RSTrace Tracer = RSTrace.ProcessingTracer;

		// Token: 0x04002CE6 RID: 11494
		internal static RSTrace RenderingTracer = RSTrace.RenderingTracer;

		// Token: 0x04002CE7 RID: 11495
		internal static string ReportProcessingLocation = Assembly.GetExecutingAssembly().Location;
	}
}
