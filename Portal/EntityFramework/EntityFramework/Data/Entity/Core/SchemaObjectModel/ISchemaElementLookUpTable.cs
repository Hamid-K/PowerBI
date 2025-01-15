using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002F9 RID: 761
	internal interface ISchemaElementLookUpTable<T> where T : SchemaElement
	{
		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06002439 RID: 9273
		int Count { get; }

		// Token: 0x0600243A RID: 9274
		bool ContainsKey(string key);

		// Token: 0x170007AB RID: 1963
		T this[string key] { get; }

		// Token: 0x0600243C RID: 9276
		IEnumerator<T> GetEnumerator();

		// Token: 0x0600243D RID: 9277
		T LookUpEquivalentKey(string key);
	}
}
