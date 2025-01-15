using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x02000014 RID: 20
	[NullableContext(1)]
	public interface ITracingService
	{
		// Token: 0x060000A3 RID: 163
		void Trace(PipelineTraceVerbosity verbosity, string message);

		// Token: 0x060000A4 RID: 164
		void Trace(PipelineTraceVerbosity verbosity, string format, object arg0);

		// Token: 0x060000A5 RID: 165
		void Trace(PipelineTraceVerbosity verbosity, string format, object arg0, object arg1);

		// Token: 0x060000A6 RID: 166
		void Trace(PipelineTraceVerbosity verbosity, string format, object arg0, object arg1, object arg2);

		// Token: 0x060000A7 RID: 167
		void Trace(PipelineTraceVerbosity verbosity, string format, object arg0, object arg1, object arg2, object arg3);

		// Token: 0x060000A8 RID: 168
		void Trace(string sourceId, PipelineTraceVerbosity verbosity, string message);

		// Token: 0x060000A9 RID: 169
		void Trace(string sourceId, PipelineTraceVerbosity verbosity, string format, object arg0);

		// Token: 0x060000AA RID: 170
		void Trace(string sourceId, PipelineTraceVerbosity verbosity, string format, object arg0, object arg1);

		// Token: 0x060000AB RID: 171
		void Trace(string sourceId, PipelineTraceVerbosity verbosity, string format, object arg0, object arg1, object arg2);

		// Token: 0x060000AC RID: 172
		void Trace(string sourceId, PipelineTraceVerbosity verbosity, string format, object arg0, object arg1, object arg2, object arg3);
	}
}
