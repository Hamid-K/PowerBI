using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Owin
{
	// Token: 0x02000019 RID: 25
	public class RequestCookieCollection : IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x06000142 RID: 322 RVA: 0x0000373E File Offset: 0x0000193E
		public RequestCookieCollection(IDictionary<string, string> store)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			this.Store = store;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000375B File Offset: 0x0000195B
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00003763 File Offset: 0x00001963
		private IDictionary<string, string> Store { get; set; }

		// Token: 0x17000074 RID: 116
		public string this[string key]
		{
			get
			{
				string value;
				if (this.Store.TryGetValue(key, out value) || this.Store.TryGetValue(Uri.EscapeDataString(key), out value))
				{
					return value;
				}
				return null;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000037A1 File Offset: 0x000019A1
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return this.Store.GetEnumerator();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000037AE File Offset: 0x000019AE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
