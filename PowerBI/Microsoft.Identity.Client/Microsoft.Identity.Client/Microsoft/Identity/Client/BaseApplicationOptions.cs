using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000135 RID: 309
	public abstract class BaseApplicationOptions
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00039ED8 File Offset: 0x000380D8
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x00039EE0 File Offset: 0x000380E0
		public LogLevel LogLevel { get; set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00039EE9 File Offset: 0x000380E9
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x00039EF1 File Offset: 0x000380F1
		public bool EnablePiiLogging { get; set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00039EFA File Offset: 0x000380FA
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x00039F02 File Offset: 0x00038102
		public bool IsDefaultPlatformLoggingEnabled { get; set; }
	}
}
