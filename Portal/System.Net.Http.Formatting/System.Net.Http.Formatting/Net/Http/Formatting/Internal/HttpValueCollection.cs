using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http.Properties;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting.Internal
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	internal class HttpValueCollection : NameValueCollection
	{
		// Token: 0x06000348 RID: 840 RVA: 0x0000C221 File Offset: 0x0000A421
		protected HttpValueCollection(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C22B File Offset: 0x0000A42B
		private HttpValueCollection()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000C238 File Offset: 0x0000A438
		internal static HttpValueCollection Create()
		{
			return new HttpValueCollection();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000C240 File Offset: 0x0000A440
		internal static HttpValueCollection Create(IEnumerable<KeyValuePair<string, string>> pairs)
		{
			HttpValueCollection httpValueCollection = new HttpValueCollection();
			foreach (KeyValuePair<string, string> keyValuePair in pairs)
			{
				httpValueCollection.Add(keyValuePair.Key, keyValuePair.Value);
			}
			httpValueCollection.IsReadOnly = false;
			return httpValueCollection;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
		public override void Add(string name, string value)
		{
			HttpValueCollection.ThrowIfMaxHttpCollectionKeysExceeded(this.Count);
			name = name ?? string.Empty;
			value = value ?? string.Empty;
			base.Add(name, value);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000C2D1 File Offset: 0x0000A4D1
		public override string ToString()
		{
			return this.ToString(true);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000C2DA File Offset: 0x0000A4DA
		private static void ThrowIfMaxHttpCollectionKeysExceeded(int count)
		{
			if (count >= MediaTypeFormatter.MaxHttpCollectionKeys)
			{
				throw Error.InvalidOperation(Resources.MaxHttpCollectionKeyLimitReached, new object[]
				{
					MediaTypeFormatter.MaxHttpCollectionKeys,
					typeof(MediaTypeFormatter)
				});
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000C310 File Offset: 0x0000A510
		private string ToString(bool urlEncode)
		{
			if (this.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (object obj in this)
			{
				string text = (string)obj;
				string[] values = this.GetValues(text);
				if (values == null || values.Length == 0)
				{
					flag = HttpValueCollection.AppendNameValuePair(stringBuilder, flag, urlEncode, text, string.Empty);
				}
				else
				{
					foreach (string text2 in values)
					{
						flag = HttpValueCollection.AppendNameValuePair(stringBuilder, flag, urlEncode, text, text2);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000C3CC File Offset: 0x0000A5CC
		private static bool AppendNameValuePair(StringBuilder builder, bool first, bool urlEncode, string name, string value)
		{
			string text = name ?? string.Empty;
			string text2 = (urlEncode ? UriQueryUtility.UrlEncode(text) : text);
			string text3 = value ?? string.Empty;
			string text4 = (urlEncode ? UriQueryUtility.UrlEncode(text3) : text3);
			if (first)
			{
				first = false;
			}
			else
			{
				builder.Append("&");
			}
			builder.Append(text2);
			if (!string.IsNullOrEmpty(text4))
			{
				builder.Append("=");
				builder.Append(text4);
			}
			return first;
		}
	}
}
