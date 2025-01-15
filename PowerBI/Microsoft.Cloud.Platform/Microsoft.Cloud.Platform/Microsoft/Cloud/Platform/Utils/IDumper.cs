using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200022D RID: 557
	public interface IDumper
	{
		// Token: 0x06000E8B RID: 3723
		void InitializeDumper();

		// Token: 0x06000E8C RID: 3724
		void CreateMemoryDump(Exception e, bool fatal, string errorText);

		// Token: 0x06000E8D RID: 3725
		bool IsDumpHashInCache(string hash);

		// Token: 0x06000E8E RID: 3726
		void AddDumpHashToCache(bool fatal, string hash, DateTime expiryDate);

		// Token: 0x06000E8F RID: 3727
		void NotifyDumpOccured(string dumpName, Exception e, bool fatal, bool inCache, string dumpUid);

		// Token: 0x06000E90 RID: 3728
		void RemoveDumpHashFromCache(string hash);

		// Token: 0x06000E91 RID: 3729
		bool IsDumpCreated(Exception e);
	}
}
