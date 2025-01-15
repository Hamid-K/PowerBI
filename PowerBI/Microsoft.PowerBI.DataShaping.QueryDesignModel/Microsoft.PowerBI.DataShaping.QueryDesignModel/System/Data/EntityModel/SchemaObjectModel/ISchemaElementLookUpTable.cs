using System;
using System.Collections.Generic;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200002F RID: 47
	internal interface ISchemaElementLookUpTable<T> where T : SchemaElement
	{
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060006CB RID: 1739
		int Count { get; }

		// Token: 0x060006CC RID: 1740
		bool ContainsKey(string key);

		// Token: 0x170002CC RID: 716
		T this[string key] { get; }

		// Token: 0x060006CE RID: 1742
		IEnumerator<T> GetEnumerator();

		// Token: 0x060006CF RID: 1743
		T LookUpEquivalentKey(string key);
	}
}
