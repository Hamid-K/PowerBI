using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000038 RID: 56
	public interface IDatabaseManager
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000152 RID: 338
		// (remove) Token: 0x06000153 RID: 339
		event DatabaseSpecificationEnabledStateChanged DatabaseSpecificationStateChanged;

		// Token: 0x1700004D RID: 77
		IDatabaseSpecificationProxy this[string key] { get; }

		// Token: 0x06000155 RID: 341
		IEnumerable<string> GetDatabaseIdentifiers(Func<string, bool> compareFunc);

		// Token: 0x06000156 RID: 342
		ReplicatedDatabaseSpecificationProxy GetReplicatedProxy(string key);
	}
}
