using System;
using System.IO;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000061 RID: 97
	public interface IStreamBasedStorage
	{
		// Token: 0x060002B5 RID: 693
		Stream CreateNewEntry(string key, bool overwriteOnCollision);

		// Token: 0x060002B6 RID: 694
		Stream GetExistingEntry(string key);

		// Token: 0x060002B7 RID: 695
		bool ContainsEntry(string key);

		// Token: 0x060002B8 RID: 696
		bool DeleteEntry(string key);
	}
}
