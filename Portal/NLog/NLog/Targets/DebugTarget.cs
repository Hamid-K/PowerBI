using System;

namespace NLog.Targets
{
	// Token: 0x02000036 RID: 54
	[Target("Debug")]
	public sealed class DebugTarget : TargetWithLayout
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		public DebugTarget()
		{
			this.LastMessage = string.Empty;
			this.Counter = 0;
			base.OptimizeBufferReuse = true;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0000C9E9 File Offset: 0x0000ABE9
		public DebugTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x0000CA00 File Offset: 0x0000AC00
		public int Counter { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000CA09 File Offset: 0x0000AC09
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0000CA11 File Offset: 0x0000AC11
		public string LastMessage { get; private set; }

		// Token: 0x060005BD RID: 1469 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		protected override void Write(LogEventInfo logEvent)
		{
			int counter = this.Counter;
			this.Counter = counter + 1;
			this.LastMessage = base.RenderLogEvent(this.Layout, logEvent);
		}
	}
}
