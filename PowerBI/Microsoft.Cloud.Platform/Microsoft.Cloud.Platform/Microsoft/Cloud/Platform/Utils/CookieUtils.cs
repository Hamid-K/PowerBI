using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.RequestProtection;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001A3 RID: 419
	public static class CookieUtils
	{
		// Token: 0x06000AB2 RID: 2738 RVA: 0x00025380 File Offset: 0x00023580
		public static bool TryGetCookieValue(string cookieHeader, string cookieKey, out string cookieValue)
		{
			IEnumerable<Cookie> enumerable = from kv in cookieHeader.Split(CookieUtils.s_cookieHeaderSplitChars, StringSplitOptions.RemoveEmptyEntries)
				where kv.Trim().StartsWith(cookieKey, StringComparison.OrdinalIgnoreCase)
				select CookieUtils.ParseCookie(kv);
			Cookie cookie = enumerable.FirstOrDefault((Cookie c) => c.Name.Equals(cookieKey, StringComparison.OrdinalIgnoreCase));
			if (cookie != null)
			{
				cookieValue = cookie.Value;
				return true;
			}
			return CookieUtils.TryGetCookieValueFromChunks(enumerable, cookieKey, out cookieValue);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002540C File Offset: 0x0002360C
		public static string CreateCookieString(string key, string value, string domain, string path, CookieFlags flags)
		{
			return CookieUtils.CreateCookieStringImpl(key, value, domain, path, null, flags);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002542D File Offset: 0x0002362D
		public static string CreateCookieString(string key, string value, string domain, string path, DateTime expires, CookieFlags flags)
		{
			return CookieUtils.CreateCookieStringImpl(key, value, domain, path, new DateTime?(expires), flags);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00025441 File Offset: 0x00023641
		public static string CreateExpiredCookieString(string key, string value, string domain, string path, CookieFlags flags)
		{
			return CookieUtils.CreateCookieStringImpl(key, value, domain, path, new DateTime?(DateTime.MinValue), flags);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00025458 File Offset: 0x00023658
		public static IList<string> CreateChunkedCookieStrings(string key, string value, string domain, string path, CookieFlags flags)
		{
			return CookieUtils.CreateChunkedCookieStrings(key, value, domain, path, null, flags);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00025479 File Offset: 0x00023679
		public static IList<string> CreateExpiredChunkedCookieStrings(string key, string value, string domain, string path, CookieFlags flags)
		{
			return CookieUtils.CreateChunkedCookieStrings(key, value, domain, path, new DateTime?(DateTime.MinValue), flags);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00025490 File Offset: 0x00023690
		public static IList<string> CreateChunkedCookieStrings(string key, string value, string domain, string path, DateTime? expires, CookieFlags flags)
		{
			List<string> list = new List<string>();
			string text = CookieUtils.CreateCookieString(key, value, domain, path, flags);
			if (text.Length <= CookieUtils.c_cookieMaxLength)
			{
				list.Add(text);
				return list;
			}
			int num = CookieUtils.c_cookieMaxLength - (text.Length + CookieUtils.c_cookieChunkKeySuffixLength - value.Length);
			IEnumerable<string> enumerable = from ch in value.Partition(num)
				select string.Concat<char>(ch);
			list.AddRange(enumerable.Select((string chunk, int i) => CookieUtils.CreateCookieStringImpl("{0}__{1:d2}".FormatWithInvariantCulture(new object[]
			{
				key,
				i + 1
			}), chunk, domain, path, expires, flags)));
			return list;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00025564 File Offset: 0x00023764
		public static void AddCookieToResponse(IRequestProtectionContext context, string cookieToSet)
		{
			context.AddResponseHeader("Set-Cookie", cookieToSet);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00025572 File Offset: 0x00023772
		public static IList<Cookie> ParseAllCookiesFromRequest(IRequestProtectionContext context)
		{
			return context.GetRequestHeader("Cookie").SelectMany((string cookieHeaderValue) => from cookieString in cookieHeaderValue.Split(CookieUtils.s_cookieHeaderSplitChars, StringSplitOptions.RemoveEmptyEntries)
				select CookieUtils.ParseCookie(cookieString)).ToList<Cookie>();
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000255A8 File Offset: 0x000237A8
		private static string CreateCookieStringImpl(string key, string value, string domain, string path, DateTime? expires, CookieFlags flags)
		{
			string text = string.Empty;
			if (expires != null)
			{
				text = "Expires={0}; ".FormatWithInvariantCulture(new object[] { (expires.Value == DateTime.MinValue) ? "Thu, 01 Jan 1970 00:00:01 GMT" : expires.Value.ToString("R", CultureInfo.InvariantCulture) });
			}
			return "{0}={1}; Domain={2}; Path={3}; {4}secure{5}".FormatWithInvariantCulture(new object[]
			{
				key,
				value,
				domain,
				path,
				text,
				flags.HasFlag(CookieFlags.HttpOnly) ? "; httponly" : string.Empty
			});
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00025654 File Offset: 0x00023854
		private static Cookie ParseCookie(string cookieString)
		{
			int num = cookieString.IndexOf('=');
			if (num > 0)
			{
				return new Cookie(cookieString.Substring(0, num).Trim(), cookieString.Substring(num + 1).Trim());
			}
			return new Cookie(cookieString.Trim(), string.Empty);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000256A0 File Offset: 0x000238A0
		private static bool TryGetCookieValueFromChunks(IEnumerable<Cookie> candidateCookies, string cookieKey, out string cookieValue)
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			foreach (Cookie cookie in candidateCookies)
			{
				string text = cookie.Name.Substring(cookieKey.Length);
				int num;
				if (text.Length == CookieUtils.c_cookieChunkKeySuffixLength && text.StartsWith("__", StringComparison.Ordinal) && int.TryParse(text.Substring("__".Length), out num))
				{
					dictionary[num] = cookie.Value;
				}
			}
			if (dictionary.Any<KeyValuePair<int, string>>())
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 1; i <= dictionary.Count; i++)
				{
					if (!dictionary.ContainsKey(i))
					{
						TraceSourceBase<CommonTrace>.Tracer.TraceError("Missing a cookie chunk (ID={0}) while reconstructing chunked cookie.", new object[] { i });
						cookieValue = string.Empty;
						return false;
					}
					stringBuilder.Append(dictionary[i]);
				}
				cookieValue = stringBuilder.ToString();
				return true;
			}
			cookieValue = string.Empty;
			return false;
		}

		// Token: 0x0400043B RID: 1083
		private const string c_cookieFormat = "{0}={1}; Domain={2}; Path={3}; {4}secure{5}";

		// Token: 0x0400043C RID: 1084
		private const string c_expiresFragment = "Expires={0}; ";

		// Token: 0x0400043D RID: 1085
		private const string c_httpOnlyFragment = "; httponly";

		// Token: 0x0400043E RID: 1086
		private const string c_expiredDateTime = "Thu, 01 Jan 1970 00:00:01 GMT";

		// Token: 0x0400043F RID: 1087
		private const char c_cookieSplitChar = '=';

		// Token: 0x04000440 RID: 1088
		private static readonly char[] s_cookieHeaderSplitChars = new char[] { ';' };

		// Token: 0x04000441 RID: 1089
		private const string c_cookieChunkSeperator = "__";

		// Token: 0x04000442 RID: 1090
		private const string c_cookieChunkKeySuffixFormat = "{0}__{1:d2}";

		// Token: 0x04000443 RID: 1091
		private const int c_chunkIdLength = 2;

		// Token: 0x04000444 RID: 1092
		private static readonly int c_cookieChunkKeySuffixLength = "__".Length + 2;

		// Token: 0x04000445 RID: 1093
		private const int c_cookieMaxLengthBuffer = 64;

		// Token: 0x04000446 RID: 1094
		private static readonly int c_cookieMaxLength = (int)ExtendedMath.KBtoBytes(4L) - 64;
	}
}
