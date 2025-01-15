using System;
using System.Diagnostics;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x02000019 RID: 25
	public interface ITracer
	{
		// Token: 0x0600008B RID: 139
		void Trace(TraceLevel level, string message);

		// Token: 0x0600008C RID: 140
		void Trace(TraceLevel level, string format, object arg0);

		// Token: 0x0600008D RID: 141
		void Trace(TraceLevel level, string format, object arg0, object arg1);

		// Token: 0x0600008E RID: 142
		void Trace(TraceLevel level, string format, object arg0, object arg1, object arg2);

		// Token: 0x0600008F RID: 143
		void Trace(TraceLevel level, string format, object arg0, object arg1, object arg2, object arg3);

		// Token: 0x06000090 RID: 144
		void Trace(TraceLevel level, string format, params string[] args);

		// Token: 0x06000091 RID: 145
		void SanitizedTrace(TraceLevel level, string message);

		// Token: 0x06000092 RID: 146
		void SanitizedTrace(TraceLevel level, string format, object arg0);

		// Token: 0x06000093 RID: 147
		void SanitizedTrace(TraceLevel level, string format, object arg0, object arg1);

		// Token: 0x06000094 RID: 148
		void SanitizedTrace(TraceLevel level, string format, object arg0, object arg1, object arg2);

		// Token: 0x06000095 RID: 149
		void SanitizedTrace(TraceLevel level, string format, object arg0, object arg1, object arg2, object arg3);

		// Token: 0x06000096 RID: 150
		void SanitizedTrace(TraceLevel level, string format, params string[] args);
	}
}
