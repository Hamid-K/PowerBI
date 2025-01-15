using System;
using System.Diagnostics;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x02000019 RID: 25
	public interface ITracer
	{
		// Token: 0x0600003C RID: 60
		void Trace(TraceLevel level, string message);

		// Token: 0x0600003D RID: 61
		void Trace(TraceLevel level, string format, object arg0);

		// Token: 0x0600003E RID: 62
		void Trace(TraceLevel level, string format, object arg0, object arg1);

		// Token: 0x0600003F RID: 63
		void Trace(TraceLevel level, string format, object arg0, object arg1, object arg2);

		// Token: 0x06000040 RID: 64
		void Trace(TraceLevel level, string format, object arg0, object arg1, object arg2, object arg3);

		// Token: 0x06000041 RID: 65
		void Trace(TraceLevel level, string format, params string[] args);
	}
}
