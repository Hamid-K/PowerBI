using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x0200000C RID: 12
	internal static class CookieHelper
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00003500 File Offset: 0x00001700
		internal static bool TryGetCookies(Uri url, HashSet<string> cookieNames, out CookieCollection cookies)
		{
			if (url == null)
			{
				cookies = null;
				return false;
			}
			string cookieString = CookieHelper.GetCookieString(url.AbsoluteUri);
			if (string.IsNullOrEmpty(cookieString))
			{
				cookies = null;
				return false;
			}
			cookies = new CookieCollection();
			CookieContainer cookieContainer = new CookieContainer();
			cookieContainer.SetCookies(url, cookieString);
			foreach (object obj in cookieContainer.GetCookies(url))
			{
				Cookie cookie = (Cookie)obj;
				if (cookieNames.Contains(cookie.Name))
				{
					cookies.Add(cookie);
				}
			}
			return true;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000035A4 File Offset: 0x000017A4
		internal static DateTime GetMinExpiry(CookieCollection cookies)
		{
			DateTime dateTime = DateTime.MaxValue;
			foreach (object obj in cookies)
			{
				Cookie cookie = (Cookie)obj;
				if (cookie.Expires < dateTime)
				{
					dateTime = cookie.Expires;
				}
			}
			return dateTime;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003610 File Offset: 0x00001810
		internal static string SerializeCookies(CookieCollection cookies)
		{
			SerializeableCookie[] array = new SerializeableCookie[cookies.Count];
			int num = 0;
			foreach (object obj in cookies)
			{
				Cookie cookie = (Cookie)obj;
				array[num] = new SerializeableCookie
				{
					Name = cookie.Name,
					Value = cookie.Value,
					Path = cookie.Path,
					Domain = cookie.Domain
				};
				num++;
			}
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(SerializeableCookie[]));
			TextWriter textWriter = new StringWriter(CultureInfo.InvariantCulture);
			xmlSerializer.Serialize(textWriter, array);
			return textWriter.ToString();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000036E4 File Offset: 0x000018E4
		private static string GetCookieString(string url)
		{
			int num = 512;
			StringBuilder stringBuilder = new StringBuilder(num);
			if (!CookieHelper.NativeMethods.InternetGetCookieEx(url, null, stringBuilder, ref num, 8192, IntPtr.Zero))
			{
				if (num <= 0)
				{
					return null;
				}
				stringBuilder = new StringBuilder(num);
				if (!CookieHelper.NativeMethods.InternetGetCookieEx(url, null, stringBuilder, ref num, 8192, IntPtr.Zero))
				{
					return null;
				}
			}
			return stringBuilder.ToString().Replace("; ", ",");
		}

		// Token: 0x0400007B RID: 123
		private const int InternetCookieHttpOnly = 8192;

		// Token: 0x0200002D RID: 45
		private static class NativeMethods
		{
			// Token: 0x06000169 RID: 361
			[DllImport("wininet.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			public static extern bool InternetGetCookieEx(string url, string cookieName, StringBuilder cookieData, ref int size, int flags, IntPtr pReserved);
		}
	}
}
