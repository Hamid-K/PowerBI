using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000FD RID: 253
	public interface ICustomProvider : IDisposable
	{
		// Token: 0x060006E4 RID: 1764
		bool IsInitialized();

		// Token: 0x060006E5 RID: 1765
		void Initialize();

		// Token: 0x060006E6 RID: 1766
		void TestConnection();

		// Token: 0x060006E7 RID: 1767
		object AddUser(string machine, string user);

		// Token: 0x060006E8 RID: 1768
		void RemoveUser(string machine, string user, object state);

		// Token: 0x060006E9 RID: 1769
		void Cleanup();

		// Token: 0x060006EA RID: 1770
		Version RetrieveStoreVersion();

		// Token: 0x060006EB RID: 1771
		void Open(string connectionString);

		// Token: 0x060006EC RID: 1772
		object BeginTransaction();

		// Token: 0x060006ED RID: 1773
		void EndTransaction(object transactionContext, bool rollback);

		// Token: 0x060006EE RID: 1774
		byte[] GetValue(object transactionContext, string type, string key);

		// Token: 0x060006EF RID: 1775
		ConfigStoreEntry GetEntry(object transactionContext, string type, string key);

		// Token: 0x060006F0 RID: 1776
		ICollection<ConfigStoreEntry> GetEntries(object transactionContext, string type);

		// Token: 0x060006F1 RID: 1777
		bool Insert(object transactionContext, string type, string key, byte[] data, long version);

		// Token: 0x060006F2 RID: 1778
		bool Update(object transactionContext, string type, string key, byte[] data, long oldVersion);

		// Token: 0x060006F3 RID: 1779
		bool Delete(object transactionContext, string type, string key, long oldVersion);

		// Token: 0x060006F4 RID: 1780
		void Delete(object transactionContext, string type);

		// Token: 0x060006F5 RID: 1781
		DateTime GetStoreUtcTime(object transactionContext);
	}
}
