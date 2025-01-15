using System;

namespace Microsoft.IdentityModel.Abstractions
{
	// Token: 0x02000006 RID: 6
	public sealed class NullIdentityModelLogger : IIdentityLogger
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000208B File Offset: 0x0000028B
		public static NullIdentityModelLogger Instance { get; } = new NullIdentityModelLogger();

		// Token: 0x06000012 RID: 18 RVA: 0x00002092 File Offset: 0x00000292
		private NullIdentityModelLogger()
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000209A File Offset: 0x0000029A
		public bool IsEnabled(EventLogLevel eventLogLevel)
		{
			return false;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000209D File Offset: 0x0000029D
		public void Log(LogEntry entry)
		{
		}
	}
}
