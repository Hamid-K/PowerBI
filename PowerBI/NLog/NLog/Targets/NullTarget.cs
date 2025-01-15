using System;
using System.ComponentModel;

namespace NLog.Targets
{
	// Token: 0x0200004F RID: 79
	[Target("Null")]
	public sealed class NullTarget : TargetWithLayout
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00012CFE File Offset: 0x00010EFE
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x00012D06 File Offset: 0x00010F06
		[DefaultValue(false)]
		public bool FormatMessage { get; set; }

		// Token: 0x06000788 RID: 1928 RVA: 0x00012D0F File Offset: 0x00010F0F
		public NullTarget()
		{
			base.OptimizeBufferReuse = true;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x00012D1E File Offset: 0x00010F1E
		public NullTarget(string name)
			: this()
		{
			base.Name = name;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00012D2D File Offset: 0x00010F2D
		protected override void Write(LogEventInfo logEvent)
		{
			if (this.FormatMessage)
			{
				base.RenderLogEvent(this.Layout, logEvent);
			}
		}
	}
}
