using System;

namespace Microsoft.Owin.Logging
{
	// Token: 0x0200002F RID: 47
	public static class LoggerFactory
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00004F78 File Offset: 0x00003178
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00004F7F File Offset: 0x0000317F
		public static ILoggerFactory Default { get; set; } = new DiagnosticsLoggerFactory();
	}
}
