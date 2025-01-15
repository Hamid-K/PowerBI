using System;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x02000050 RID: 80
	[Target("OutputDebugString")]
	public sealed class OutputDebugStringTarget : TargetWithLayout
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00012D45 File Offset: 0x00010F45
		public OutputDebugStringTarget()
		{
			base.OptimizeBufferReuse = true;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00012D54 File Offset: 0x00010F54
		public OutputDebugStringTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x00012D63 File Offset: 0x00010F63
		protected override void Write(LogEventInfo logEvent)
		{
			NativeMethods.OutputDebugString(base.RenderLogEvent(this.Layout, logEvent));
		}
	}
}
