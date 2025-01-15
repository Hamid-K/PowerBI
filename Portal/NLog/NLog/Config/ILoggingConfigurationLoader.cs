using System;
using System.Collections.Generic;

namespace NLog.Config
{
	// Token: 0x02000188 RID: 392
	internal interface ILoggingConfigurationLoader : IDisposable
	{
		// Token: 0x060011C6 RID: 4550
		LoggingConfiguration Load(LogFactory logFactory);

		// Token: 0x060011C7 RID: 4551
		void Activated(LogFactory logFactory, LoggingConfiguration config);

		// Token: 0x060011C8 RID: 4552
		IEnumerable<string> GetDefaultCandidateConfigFilePaths();
	}
}
