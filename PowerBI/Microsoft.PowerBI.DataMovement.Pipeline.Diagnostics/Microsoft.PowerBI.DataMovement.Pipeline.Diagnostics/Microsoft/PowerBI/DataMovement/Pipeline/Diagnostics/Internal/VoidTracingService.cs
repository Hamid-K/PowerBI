using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000E5 RID: 229
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class VoidTracingService : MarshalByRefObject, ITracingService
	{
		// Token: 0x06001121 RID: 4385 RVA: 0x00046B17 File Offset: 0x00044D17
		public void Trace(PipelineTraceVerbosity verbosity, string message)
		{
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00046B19 File Offset: 0x00044D19
		public void Trace(PipelineTraceVerbosity verbosity, string format, object arg0)
		{
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00046B1B File Offset: 0x00044D1B
		public void Trace(PipelineTraceVerbosity verbosity, string format, object arg0, object arg1)
		{
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00046B1D File Offset: 0x00044D1D
		public void Trace(PipelineTraceVerbosity verbosity, string format, object arg0, object arg1, object arg2)
		{
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00046B1F File Offset: 0x00044D1F
		public void Trace(PipelineTraceVerbosity verbosity, string format, object arg0, object arg1, object arg2, object arg3)
		{
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00046B21 File Offset: 0x00044D21
		public void Trace(string sourceId, PipelineTraceVerbosity verbosity, string message)
		{
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00046B23 File Offset: 0x00044D23
		public void Trace(string sourceId, PipelineTraceVerbosity verbosity, string format, object arg0)
		{
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00046B25 File Offset: 0x00044D25
		public void Trace(string sourceId, PipelineTraceVerbosity verbosity, string format, object arg0, object arg1)
		{
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00046B27 File Offset: 0x00044D27
		public void Trace(string sourceId, PipelineTraceVerbosity verbosity, string format, object arg0, object arg1, object arg2)
		{
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00046B29 File Offset: 0x00044D29
		public void Trace(string sourceId, PipelineTraceVerbosity verbosity, string format, object arg0, object arg1, object arg2, object arg3)
		{
		}
	}
}
