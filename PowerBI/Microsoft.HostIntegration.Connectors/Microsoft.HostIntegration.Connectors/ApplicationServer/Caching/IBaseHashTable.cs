using System;
using System.Collections;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000202 RID: 514
	internal interface IBaseHashTable : IEnumerable, ICloneable
	{
		// Token: 0x060010B1 RID: 4273
		object Get(object searchKey);

		// Token: 0x060010B2 RID: 4274
		void Add(object searchKey, object data);

		// Token: 0x060010B3 RID: 4275
		object Delete(object searchKey);

		// Token: 0x060010B4 RID: 4276
		object Upsert(object searchKey, object data);

		// Token: 0x060010B5 RID: 4277
		bool ContainsKey(object searchKey);

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060010B6 RID: 4278
		ICollection Keys { get; }

		// Token: 0x060010B7 RID: 4279
		bool GetBatch(IScanner scanner, EnumeratorState state);

		// Token: 0x060010B8 RID: 4280
		EnumeratorState GetStatelessEnumeratorState();

		// Token: 0x060010B9 RID: 4281
		int DoCompaction();

		// Token: 0x060010BA RID: 4282
		IEnumerator GetKeyValueEnumerator();

		// Token: 0x060010BB RID: 4283
		void Clear();

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060010BC RID: 4284
		long LastCompactionEpoch { get; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060010BD RID: 4285
		int NumberOfSplits { get; }
	}
}
