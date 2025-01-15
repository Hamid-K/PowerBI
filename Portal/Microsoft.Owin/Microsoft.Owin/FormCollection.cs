using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Owin
{
	// Token: 0x02000007 RID: 7
	public class FormCollection : ReadableStringCollection, IFormCollection, IReadableStringCollection, IEnumerable<KeyValuePair<string, string[]>>, IEnumerable
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002354 File Offset: 0x00000554
		public FormCollection(IDictionary<string, string[]> store)
			: base(store)
		{
		}
	}
}
