using System;
using NLog.Config;

namespace NLog.Internal
{
	// Token: 0x02000124 RID: 292
	internal interface ISupportsInitialize
	{
		// Token: 0x06000EE2 RID: 3810
		void Initialize(LoggingConfiguration configuration);

		// Token: 0x06000EE3 RID: 3811
		void Close();
	}
}
