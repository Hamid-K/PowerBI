using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000219 RID: 537
	internal interface IMultiLevelHashTable
	{
		// Token: 0x060011D2 RID: 4562
		object Get(object[] LevelwiseKeys);

		// Token: 0x060011D3 RID: 4563
		void Add(object[] LevelwiseKeys, object obj);

		// Token: 0x060011D4 RID: 4564
		object Upsert(object[] LevelwiseKeys, object obj);

		// Token: 0x060011D5 RID: 4565
		object Delete(object[] LevelwiseKeys);

		// Token: 0x060011D6 RID: 4566
		IEnumerator Find(object[] LevelwiseKeys);

		// Token: 0x060011D7 RID: 4567
		IEnumerator FindUnion(List<object[]> listLevelwiseKeys);

		// Token: 0x060011D8 RID: 4568
		IEnumerator FindIntersection(List<object[]> listLevelwiseKeys);

		// Token: 0x060011D9 RID: 4569
		IIndexStoreSchema GetSchema();

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060011DA RID: 4570
		int Level { get; }

		// Token: 0x060011DB RID: 4571
		EnumeratorState UnionAllWithStatelessEnumerator(List<object[]> listlevelwiseKeys);

		// Token: 0x060011DC RID: 4572
		EnumeratorState FindWithStatelessEnumerator(object[] levelwiseKeys);

		// Token: 0x060011DD RID: 4573
		bool GetBatch(IScanner scanner, EnumeratorState enumeratorState);

		// Token: 0x060011DE RID: 4574
		int DoCompaction();
	}
}
