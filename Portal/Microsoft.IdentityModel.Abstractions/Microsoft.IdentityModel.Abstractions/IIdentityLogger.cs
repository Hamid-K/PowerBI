using System;

namespace Microsoft.IdentityModel.Abstractions
{
	// Token: 0x02000003 RID: 3
	public interface IIdentityLogger
	{
		// Token: 0x06000001 RID: 1
		bool IsEnabled(EventLogLevel eventLogLevel);

		// Token: 0x06000002 RID: 2
		void Log(LogEntry entry);
	}
}
