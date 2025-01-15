using System;
using System.Collections.Generic;
using System.Linq;

namespace Azure.Core
{
	// Token: 0x02000070 RID: 112
	internal class DictionaryHeaders
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x0000AE30 File Offset: 0x00009030
		public void AddHeader(string name, string value)
		{
			object obj;
			if (!this._headers.TryGetValue(name, out obj))
			{
				this._headers[name] = value;
				return;
			}
			List<string> list = obj as List<string>;
			if (list != null)
			{
				list.Add(value);
				return;
			}
			this._headers[name] = new List<string>
			{
				obj as string,
				value
			};
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000AE94 File Offset: 0x00009094
		public bool TryGetHeader(string name, out string value)
		{
			object obj;
			if (this._headers.TryGetValue(name, out obj))
			{
				List<string> list = obj as List<string>;
				if (list != null)
				{
					value = DictionaryHeaders.JoinHeaderValue(list);
				}
				else
				{
					value = obj as string;
				}
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000AED4 File Offset: 0x000090D4
		public bool TryGetHeaderValues(string name, out IEnumerable<string> values)
		{
			object obj;
			if (this._headers.TryGetValue(name, out obj))
			{
				List<string> list = obj as List<string>;
				if (list != null)
				{
					values = list;
				}
				else
				{
					values = new List<string> { obj as string };
				}
				return true;
			}
			values = null;
			return false;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000AF19 File Offset: 0x00009119
		public bool ContainsHeader(string name)
		{
			return this._headers.ContainsKey(name);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000AF27 File Offset: 0x00009127
		public void SetHeader(string name, string value)
		{
			this._headers[name] = value;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000AF36 File Offset: 0x00009136
		public bool RemoveHeader(string name)
		{
			return this._headers.Remove(name);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000AF44 File Offset: 0x00009144
		public IEnumerable<HttpHeader> EnumerateHeaders()
		{
			return this._headers.Select(delegate(KeyValuePair<string, object> h)
			{
				string key = h.Key;
				List<string> list = h.Value as List<string>;
				return new HttpHeader(key, (list != null) ? DictionaryHeaders.JoinHeaderValue(list) : (h.Value as string));
			});
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000AF70 File Offset: 0x00009170
		private static string JoinHeaderValue(IEnumerable<string> values)
		{
			return string.Join(",", values);
		}

		// Token: 0x04000184 RID: 388
		private readonly Dictionary<string, object> _headers = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
	}
}
