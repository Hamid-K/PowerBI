using System;
using System.ComponentModel;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x02000057 RID: 87
	public abstract class TargetWithLayout : Target
	{
		// Token: 0x06000810 RID: 2064 RVA: 0x00014AD7 File Offset: 0x00012CD7
		protected TargetWithLayout()
		{
			this.Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}";
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00014AEF File Offset: 0x00012CEF
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x00014AF7 File Offset: 0x00012CF7
		[RequiredParameter]
		[DefaultValue("${longdate}|${level:uppercase=true}|${logger}|${message}")]
		public virtual Layout Layout { get; set; }
	}
}
