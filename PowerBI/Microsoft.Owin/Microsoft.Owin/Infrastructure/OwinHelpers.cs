using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x02000039 RID: 57
	internal static class OwinHelpers
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00005CAC File Offset: 0x00003EAC
		internal static IDictionary<string, string> GetCookies(IOwinRequest request)
		{
			IDictionary<string, string> cookies = request.Get<IDictionary<string, string>>("Microsoft.Owin.Cookies#dictionary");
			if (cookies == null)
			{
				cookies = new Dictionary<string, string>(StringComparer.Ordinal);
				request.Set<IDictionary<string, string>>("Microsoft.Owin.Cookies#dictionary", cookies);
			}
			string text = OwinHelpers.GetHeader(request.Headers, "Cookie");
			if (request.Get<string>("Microsoft.Owin.Cookies#text") != text)
			{
				cookies.Clear();
				OwinHelpers.ParseDelimited(text, OwinHelpers.SemicolonAndComma, OwinHelpers.AddCookieCallback, false, false, cookies);
				request.Set<string>("Microsoft.Owin.Cookies#text", text);
			}
			return cookies;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00005D2C File Offset: 0x00003F2C
		internal static void ParseDelimited(string text, char[] delimiters, Action<string, string, object> callback, bool decodePlus, bool decodeKey, object state)
		{
			int textLength = text.Length;
			int equalIndex = text.IndexOf('=');
			if (equalIndex == -1)
			{
				equalIndex = textLength;
			}
			int delimiterIndex;
			for (int scanIndex = 0; scanIndex < textLength; scanIndex = delimiterIndex + 1)
			{
				delimiterIndex = text.IndexOfAny(delimiters, scanIndex);
				if (delimiterIndex == -1)
				{
					delimiterIndex = textLength;
				}
				if (equalIndex < delimiterIndex)
				{
					while (scanIndex != equalIndex && char.IsWhiteSpace(text[scanIndex]))
					{
						scanIndex++;
					}
					string name = text.Substring(scanIndex, equalIndex - scanIndex);
					string value = text.Substring(equalIndex + 1, delimiterIndex - equalIndex - 1);
					if (decodePlus)
					{
						name = name.Replace('+', ' ');
						value = value.Replace('+', ' ');
					}
					if (decodeKey)
					{
						name = Uri.UnescapeDataString(name);
					}
					value = Uri.UnescapeDataString(value);
					callback(name, value, state);
					equalIndex = text.IndexOf('=', delimiterIndex);
					if (equalIndex == -1)
					{
						equalIndex = textLength;
					}
				}
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00005DFC File Offset: 0x00003FFC
		public static string GetHeader(IDictionary<string, string[]> headers, string key)
		{
			string[] values = OwinHelpers.GetHeaderUnmodified(headers, key);
			if (values != null)
			{
				return string.Join(",", values);
			}
			return null;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00005E24 File Offset: 0x00004024
		public static IEnumerable<string> GetHeaderSplit(IDictionary<string, string[]> headers, string key)
		{
			string[] values = OwinHelpers.GetHeaderUnmodified(headers, key);
			if (values != null)
			{
				return OwinHelpers.GetHeaderSplitImplementation(values);
			}
			return null;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00005E44 File Offset: 0x00004044
		private static IEnumerable<string> GetHeaderSplitImplementation(string[] values)
		{
			foreach (HeaderSegment segment in new HeaderSegmentCollection(values))
			{
				if (segment.Data.HasValue)
				{
					yield return OwinHelpers.DeQuote(segment.Data.Value);
				}
			}
			HeaderSegmentCollection.Enumerator enumerator = default(HeaderSegmentCollection.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00005E54 File Offset: 0x00004054
		public static string[] GetHeaderUnmodified(IDictionary<string, string[]> headers, string key)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			string[] values;
			if (!headers.TryGetValue(key, out values))
			{
				return null;
			}
			return values;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00005E80 File Offset: 0x00004080
		public static void SetHeader(IDictionary<string, string[]> headers, string key, string value)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException("key");
			}
			if (string.IsNullOrWhiteSpace(value))
			{
				headers.Remove(key);
				return;
			}
			headers[key] = new string[] { value };
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00005ED0 File Offset: 0x000040D0
		public static void SetHeaderJoined(IDictionary<string, string[]> headers, string key, params string[] values)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException("key");
			}
			if (values == null || values.Length == 0)
			{
				headers.Remove(key);
				return;
			}
			string[] array = new string[1];
			array[0] = string.Join(",", values.Select((string value) => OwinHelpers.QuoteIfNeeded(value)));
			headers[key] = array;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00005F50 File Offset: 0x00004150
		private static string QuoteIfNeeded(string value)
		{
			if (!string.IsNullOrWhiteSpace(value) && value.Contains(',') && (value[0] != '"' || value[value.Length - 1] != '"'))
			{
				value = "\"" + value + "\"";
			}
			return value;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00005FA0 File Offset: 0x000041A0
		private static string DeQuote(string value)
		{
			if (!string.IsNullOrWhiteSpace(value) && value.Length > 1 && value[0] == '"' && value[value.Length - 1] == '"')
			{
				value = value.Substring(1, value.Length - 2);
			}
			return value;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00005FED File Offset: 0x000041ED
		public static void SetHeaderUnmodified(IDictionary<string, string[]> headers, string key, params string[] values)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException("key");
			}
			if (values == null || values.Length == 0)
			{
				headers.Remove(key);
				return;
			}
			headers[key] = values;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006028 File Offset: 0x00004228
		public static void SetHeaderUnmodified(IDictionary<string, string[]> headers, string key, IEnumerable<string> values)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			headers[key] = values.ToArray<string>();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006048 File Offset: 0x00004248
		public static void AppendHeader(IDictionary<string, string[]> headers, string key, string values)
		{
			if (string.IsNullOrWhiteSpace(values))
			{
				return;
			}
			string existing = OwinHelpers.GetHeader(headers, key);
			if (existing == null)
			{
				OwinHelpers.SetHeader(headers, key, values);
				return;
			}
			headers[key] = new string[] { existing + "," + values };
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00006090 File Offset: 0x00004290
		public static void AppendHeaderJoined(IDictionary<string, string[]> headers, string key, params string[] values)
		{
			if (values == null || values.Length == 0)
			{
				return;
			}
			string existing = OwinHelpers.GetHeader(headers, key);
			if (existing == null)
			{
				OwinHelpers.SetHeaderJoined(headers, key, values);
				return;
			}
			string[] array = new string[1];
			array[0] = existing + "," + string.Join(",", values.Select((string value) => OwinHelpers.QuoteIfNeeded(value)));
			headers[key] = array;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006104 File Offset: 0x00004304
		public static void AppendHeaderUnmodified(IDictionary<string, string[]> headers, string key, params string[] values)
		{
			if (values == null || values.Length == 0)
			{
				return;
			}
			string[] existing = OwinHelpers.GetHeaderUnmodified(headers, key);
			if (existing == null)
			{
				OwinHelpers.SetHeaderUnmodified(headers, key, values);
				return;
			}
			OwinHelpers.SetHeaderUnmodified(headers, key, existing.Concat(values));
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000613C File Offset: 0x0000433C
		internal static IDictionary<string, string[]> GetQuery(IOwinRequest request)
		{
			IDictionary<string, string[]> query = request.Get<IDictionary<string, string[]>>("Microsoft.Owin.Query#dictionary");
			if (query == null)
			{
				query = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
				request.Set<IDictionary<string, string[]>>("Microsoft.Owin.Query#dictionary", query);
			}
			string text = request.QueryString.Value;
			if (request.Get<string>("Microsoft.Owin.Query#text") != text)
			{
				query.Clear();
				Dictionary<string, List<string>> accumulator = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
				OwinHelpers.ParseDelimited(text, OwinHelpers.AmpersandAndSemicolon, OwinHelpers.AppendItemCallback, true, true, accumulator);
				foreach (KeyValuePair<string, List<string>> kv in accumulator)
				{
					query.Add(kv.Key, kv.Value.ToArray());
				}
				request.Set<string>("Microsoft.Owin.Query#text", text);
			}
			return query;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000621C File Offset: 0x0000441C
		internal static IFormCollection GetForm(string text)
		{
			IDictionary<string, string[]> form = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
			Dictionary<string, List<string>> accumulator = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
			OwinHelpers.ParseDelimited(text, new char[] { '&' }, OwinHelpers.AppendItemCallback, true, true, accumulator);
			foreach (KeyValuePair<string, List<string>> kv in accumulator)
			{
				form.Add(kv.Key, kv.Value.ToArray());
			}
			return new FormCollection(form);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000062B4 File Offset: 0x000044B4
		internal static string GetJoinedValue(IDictionary<string, string[]> store, string key)
		{
			string[] values = OwinHelpers.GetUnmodifiedValues(store, key);
			if (values != null)
			{
				return string.Join(",", values);
			}
			return null;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000062DC File Offset: 0x000044DC
		internal static string[] GetUnmodifiedValues(IDictionary<string, string[]> store, string key)
		{
			if (store == null)
			{
				throw new ArgumentNullException("store");
			}
			string[] values;
			if (!store.TryGetValue(key, out values))
			{
				return null;
			}
			return values;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006308 File Offset: 0x00004508
		internal static string GetHost(IOwinRequest request)
		{
			IHeaderDictionary headers = request.Headers;
			string host = OwinHelpers.GetHeader(headers, "Host");
			if (!string.IsNullOrWhiteSpace(host))
			{
				return host;
			}
			string localIpAddress = request.LocalIpAddress ?? "localhost";
			string localPort = request.Get<string>("server.LocalPort");
			if (!string.IsNullOrWhiteSpace(localPort))
			{
				return localIpAddress + ":" + localPort;
			}
			return localIpAddress;
		}

		// Token: 0x0400006C RID: 108
		private static readonly Action<string, string, object> AddCookieCallback = delegate(string name, string value, object state)
		{
			IDictionary<string, string> dictionary = (IDictionary<string, string>)state;
			if (!dictionary.ContainsKey(name))
			{
				dictionary.Add(name, value);
			}
		};

		// Token: 0x0400006D RID: 109
		private static readonly char[] SemicolonAndComma = new char[] { ';', ',' };

		// Token: 0x0400006E RID: 110
		private static readonly Action<string, string, object> AppendItemCallback = delegate(string name, string value, object state)
		{
			IDictionary<string, List<string>> dictionary2 = (IDictionary<string, List<string>>)state;
			List<string> existing;
			if (!dictionary2.TryGetValue(name, out existing))
			{
				dictionary2.Add(name, new List<string>(1) { value });
				return;
			}
			existing.Add(value);
		};

		// Token: 0x0400006F RID: 111
		private static readonly char[] AmpersandAndSemicolon = new char[] { '&', ';' };
	}
}
