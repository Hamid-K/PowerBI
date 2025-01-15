using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200011C RID: 284
	internal sealed class RenderFromHistory : RenderFromSnapshot
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x0002AAE4 File Offset: 0x00028CE4
		public RenderFromHistory(ReportExecutionBase executionContext)
			: base(executionContext)
		{
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00010319 File Offset: 0x0000E519
		public override ExecutionLogExecType ExecutionType
		{
			get
			{
				return ExecutionLogExecType.History;
			}
		}
	}
}
