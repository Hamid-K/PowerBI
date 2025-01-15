using System;
using NLog.Common;
using NLog.Config;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000078 RID: 120
	public abstract class WrapperTargetBase : Target
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00017B04 File Offset: 0x00015D04
		// (set) Token: 0x0600092F RID: 2351 RVA: 0x00017B0C File Offset: 0x00015D0C
		[RequiredParameter]
		public Target WrappedTarget { get; set; }

		// Token: 0x06000930 RID: 2352 RVA: 0x00017B15 File Offset: 0x00015D15
		public override string ToString()
		{
			return string.Format("{0}({1})", base.ToString(), this.WrappedTarget);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00017B2D File Offset: 0x00015D2D
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			this.WrappedTarget.Flush(asyncContinuation);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00017B3B File Offset: 0x00015D3B
		protected sealed override void Write(LogEventInfo logEvent)
		{
			throw new NotSupportedException("This target must not be invoked in a synchronous way.");
		}
	}
}
