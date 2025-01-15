using System;
using System.Diagnostics;

namespace Microsoft.PowerBI.DataExtension.Contracts.Hosting
{
	// Token: 0x0200000C RID: 12
	public interface ITracer
	{
		// Token: 0x0600002F RID: 47
		void Trace(TraceLevel level, string message);

		// Token: 0x06000030 RID: 48
		void SanitizedTrace(TraceLevel level, string message);
	}
}
