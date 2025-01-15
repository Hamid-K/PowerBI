using System;
using System.Collections.Generic;

namespace NLog.Internal.Fakeables
{
	// Token: 0x0200016B RID: 363
	internal interface IAppEnvironment : IFileSystem
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600110D RID: 4365
		string AppDomainBaseDirectory { get; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600110E RID: 4366
		string AppDomainConfigurationFile { get; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600110F RID: 4367
		string CurrentProcessFilePath { get; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001110 RID: 4368
		string EntryAssemblyLocation { get; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001111 RID: 4369
		string EntryAssemblyFileName { get; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001112 RID: 4370
		IEnumerable<string> PrivateBinPath { get; }
	}
}
