using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000121 RID: 289
	internal sealed class RenderRdceReport : RenderLive
	{
		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002B316 File Offset: 0x00029516
		public RenderRdceReport(ReportExecutionBase executionContext)
			: base(executionContext)
		{
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0002B40C File Offset: 0x0002960C
		public override ExecutionLogExecType ExecutionType
		{
			get
			{
				return ExecutionLogExecType.Rdce;
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002B40F File Offset: 0x0002960F
		protected override void PrepareExecutionSnapshot()
		{
			base.SnapshotManager.OriginalSnapshot = base.ExecutionContext.IntermediateSnapshot;
		}
	}
}
