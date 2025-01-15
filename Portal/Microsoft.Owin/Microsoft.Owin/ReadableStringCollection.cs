using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Owin.Infrastructure;

namespace Microsoft.Owin
{
	// Token: 0x02000018 RID: 24
	public class ReadableStringCollection : IReadableStringCollection, IEnumerable<KeyValuePair<string, string[]>>, IEnumerable
	{
		// Token: 0x0600013A RID: 314 RVA: 0x000036C5 File Offset: 0x000018C5
		public ReadableStringCollection(IDictionary<string, string[]> store)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			this.Store = store;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600013B RID: 315 RVA: 0x000036E2 File Offset: 0x000018E2
		// (set) Token: 0x0600013C RID: 316 RVA: 0x000036EA File Offset: 0x000018EA
		private IDictionary<string, string[]> Store { get; set; }

		// Token: 0x17000072 RID: 114
		public string this[string key]
		{
			get
			{
				return this.Get(key);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000036FC File Offset: 0x000018FC
		public string Get(string key)
		{
			return OwinHelpers.GetJoinedValue(this.Store, key);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000370C File Offset: 0x0000190C
		public IList<string> GetValues(string key)
		{
			string[] values;
			this.Store.TryGetValue(key, out values);
			return values;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00003729 File Offset: 0x00001929
		public IEnumerator<KeyValuePair<string, string[]>> GetEnumerator()
		{
			return this.Store.GetEnumerator();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00003736 File Offset: 0x00001936
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
