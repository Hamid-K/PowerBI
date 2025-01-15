using System;
using System.ComponentModel;
using Microsoft.ApplicationInsights.DataContracts;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000082 RID: 130
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class TelemetryContextExtensions
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x00012D96 File Offset: 0x00010F96
		public static InternalContext GetInternalContext(this TelemetryContext context)
		{
			return context.Internal;
		}
	}
}
