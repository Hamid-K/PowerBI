using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000085 RID: 133
	public interface IKeyValueStoreReplica
	{
		// Token: 0x060004F2 RID: 1266
		IFabricTransaction CreateTransaction();

		// Token: 0x060004F3 RID: 1267
		bool Contains(IFabricTransaction transaction, string key);

		// Token: 0x060004F4 RID: 1268
		void Add(IFabricTransaction transaction, string key, byte[] value);

		// Token: 0x060004F5 RID: 1269
		void Update(IFabricTransaction transaction, string key, byte[] value);

		// Token: 0x060004F6 RID: 1270
		void Remove(IFabricTransaction transaction, string key);

		// Token: 0x060004F7 RID: 1271
		byte[] GetValue(IFabricTransaction transaction, string key);

		// Token: 0x060004F8 RID: 1272
		void Clear();

		// Token: 0x060004F9 RID: 1273
		IEnumerable<KeyValueItem> GetValues(IFabricTransaction transaction);

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004FA RID: 1274
		bool InMemoryKeyValueStore { get; }

		// Token: 0x060004FB RID: 1275
		bool IsPrimary();
	}
}
