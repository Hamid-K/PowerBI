using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000011 RID: 17
	internal sealed class Global
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002E03 File Offset: 0x00001003
		internal static RSTrace Tracer
		{
			get
			{
				return Internal.GetTracer();
			}
		}
	}
}
