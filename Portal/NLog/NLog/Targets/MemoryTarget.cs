using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NLog.Targets
{
	// Token: 0x02000046 RID: 70
	[Target("Memory")]
	public sealed class MemoryTarget : TargetWithLayout
	{
		// Token: 0x06000710 RID: 1808 RVA: 0x00011BE4 File Offset: 0x0000FDE4
		public MemoryTarget()
		{
			this.Logs = new List<string>();
			base.OptimizeBufferReuse = true;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00011BFE File Offset: 0x0000FDFE
		public MemoryTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00011C0D File Offset: 0x0000FE0D
		public IList<string> Logs { get; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x00011C15 File Offset: 0x0000FE15
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x00011C1D File Offset: 0x0000FE1D
		[DefaultValue(0)]
		public int MaxLogsCount { get; set; }

		// Token: 0x06000715 RID: 1813 RVA: 0x00011C28 File Offset: 0x0000FE28
		protected override void Write(LogEventInfo logEvent)
		{
			if (this.MaxLogsCount > 0 && this.Logs.Count >= this.MaxLogsCount)
			{
				this.Logs.RemoveAt(0);
			}
			this.Logs.Add(base.RenderLogEvent(this.Layout, logEvent));
		}
	}
}
