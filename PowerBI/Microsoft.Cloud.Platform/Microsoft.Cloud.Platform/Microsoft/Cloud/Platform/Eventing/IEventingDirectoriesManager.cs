using System;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000387 RID: 903
	public interface IEventingDirectoriesManager
	{
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001C05 RID: 7173
		string EventingFilesSourceDirectory { get; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001C06 RID: 7174
		string EventingFilesTargetDirectory { get; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001C07 RID: 7175
		string ProvidersManifestDirectory { get; }
	}
}
