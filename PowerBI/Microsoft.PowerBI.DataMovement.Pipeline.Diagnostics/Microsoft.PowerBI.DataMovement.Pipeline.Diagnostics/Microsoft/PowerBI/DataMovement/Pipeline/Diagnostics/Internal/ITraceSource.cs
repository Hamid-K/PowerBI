using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000DB RID: 219
	[NullableContext(1)]
	internal interface ITraceSource
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060010CC RID: 4300
		TraceSourceIdentifier ID { get; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060010CD RID: 4301
		char Delimiter { get; }

		// Token: 0x060010CE RID: 4302
		void Trace(PipelineTraceVerbosity verbosity, string message);

		// Token: 0x060010CF RID: 4303
		void TraceWithHeader(PipelineTraceVerbosity verbosity, string header, string message);

		// Token: 0x060010D0 RID: 4304
		void Trace(PipelineTraceVerbosity verbosity, string format, params object[] args);

		// Token: 0x060010D1 RID: 4305
		void Trace(TraceEventType verbosity, string format, params object[] args);

		// Token: 0x060010D2 RID: 4306
		void TraceFatal(string message);

		// Token: 0x060010D3 RID: 4307
		void TraceFatal(string format, params object[] args);

		// Token: 0x060010D4 RID: 4308
		void TraceError(string message);

		// Token: 0x060010D5 RID: 4309
		void TraceError(string format, params object[] args);

		// Token: 0x060010D6 RID: 4310
		void TraceWarning(string message);

		// Token: 0x060010D7 RID: 4311
		void TraceWarning(string format, params object[] args);

		// Token: 0x060010D8 RID: 4312
		void TraceInformation(string message);

		// Token: 0x060010D9 RID: 4313
		void TraceInformation(string format, params object[] args);

		// Token: 0x060010DA RID: 4314
		void TraceVerbose(string message);

		// Token: 0x060010DB RID: 4315
		void TraceVerbose(string format, params object[] args);

		// Token: 0x060010DC RID: 4316
		bool ShouldTrace(PipelineTraceVerbosity level);
	}
}
