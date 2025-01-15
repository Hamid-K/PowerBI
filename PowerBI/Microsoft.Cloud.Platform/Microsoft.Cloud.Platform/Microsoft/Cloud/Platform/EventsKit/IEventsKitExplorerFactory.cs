using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000344 RID: 836
	public interface IEventsKitExplorerFactory
	{
		// Token: 0x060018CB RID: 6347
		IEventsKitExplorer Create();

		// Token: 0x060018CC RID: 6348
		IEventsKitExplorer Create(IEnumerable<string> eventKitAssemblyNames);

		// Token: 0x060018CD RID: 6349
		IEventsKitExplorer Create(IEnumerable<Type> eventKitTypes);

		// Token: 0x060018CE RID: 6350
		IEventsKitExplorer Create(string path);

		// Token: 0x060018CF RID: 6351
		IEventsKitExplorer Create(string path, bool recursive);

		// Token: 0x060018D0 RID: 6352
		IEventsKitExplorer Create(string path, bool recursive, Predicate<string> assemblyLoadPredicate);
	}
}
