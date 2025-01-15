using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http.Formatting.Internal;
using System.Net.Http.Formatting.Parsers;
using System.Net.Http.Properties;
using System.Text;
using System.Threading;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000037 RID: 55
	public class FormDataCollection : IEnumerable<KeyValuePair<string, string>>, IEnumerable
	{
		// Token: 0x0600021C RID: 540 RVA: 0x0000738C File Offset: 0x0000558C
		public FormDataCollection(IEnumerable<KeyValuePair<string, string>> pairs)
		{
			if (pairs == null)
			{
				throw Error.ArgumentNull("pairs");
			}
			this._pairs = pairs;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000073AC File Offset: 0x000055AC
		public FormDataCollection(Uri uri)
		{
			if (uri == null)
			{
				throw Error.ArgumentNull("uri");
			}
			string text = uri.Query;
			if (text != null && text.Length > 0 && text[0] == '?')
			{
				text = text.Substring(1);
			}
			this._pairs = FormDataCollection.ParseQueryString(text);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007405 File Offset: 0x00005605
		public FormDataCollection(string query)
		{
			this._pairs = FormDataCollection.ParseQueryString(query);
		}

		// Token: 0x1700009D RID: 157
		public string this[string name]
		{
			get
			{
				return this.Get(name);
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007424 File Offset: 0x00005624
		private static IEnumerable<KeyValuePair<string, string>> ParseQueryString(string query)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			if (string.IsNullOrWhiteSpace(query))
			{
				return list;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(query);
			FormUrlEncodedParser formUrlEncodedParser = new FormUrlEncodedParser(list, long.MaxValue);
			int num = 0;
			if (formUrlEncodedParser.ParseBuffer(bytes, bytes.Length, ref num, true) != ParserState.Done)
			{
				throw Error.InvalidOperation(Resources.FormUrlEncodedParseError, new object[] { num });
			}
			return list;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007488 File Offset: 0x00005688
		public NameValueCollection ReadAsNameValueCollection()
		{
			if (this._nameValueCollection == null)
			{
				HttpValueCollection httpValueCollection = HttpValueCollection.Create(this);
				Interlocked.Exchange<NameValueCollection>(ref this._nameValueCollection, httpValueCollection);
			}
			return this._nameValueCollection;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000074B7 File Offset: 0x000056B7
		public string Get(string key)
		{
			return this.ReadAsNameValueCollection().Get(key);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000074C5 File Offset: 0x000056C5
		public string[] GetValues(string key)
		{
			return this.ReadAsNameValueCollection().GetValues(key);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000074D3 File Offset: 0x000056D3
		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			return this._pairs.GetEnumerator();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000074E0 File Offset: 0x000056E0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._pairs.GetEnumerator();
		}

		// Token: 0x0400009F RID: 159
		private readonly IEnumerable<KeyValuePair<string, string>> _pairs;

		// Token: 0x040000A0 RID: 160
		private NameValueCollection _nameValueCollection;
	}
}
