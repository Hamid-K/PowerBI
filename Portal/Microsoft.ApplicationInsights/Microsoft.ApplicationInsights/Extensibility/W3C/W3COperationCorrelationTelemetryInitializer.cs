using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.ApplicationInsights.Channel;

namespace Microsoft.ApplicationInsights.Extensibility.W3C
{
	// Token: 0x02000063 RID: 99
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class W3COperationCorrelationTelemetryInitializer : ITelemetryInitializer
	{
		// Token: 0x06000304 RID: 772 RVA: 0x0000E5DB File Offset: 0x0000C7DB
		public void Initialize(ITelemetry telemetry)
		{
			Activity.Current.UpdateTelemetry(telemetry, false);
		}
	}
}
