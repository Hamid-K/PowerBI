using System;
using System.Globalization;
using System.Net;

namespace AngleSharp.Services.Default
{
	// Token: 0x02000050 RID: 80
	public class MemoryCookieProvider : ICookieProvider
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000C12F File Offset: 0x0000A32F
		public MemoryCookieProvider()
		{
			this._container = new CookieContainer();
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000C142 File Offset: 0x0000A342
		public CookieContainer Container
		{
			get
			{
				return this._container;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000C14A File Offset: 0x0000A34A
		public string GetCookie(string origin)
		{
			return this._container.GetCookieHeader(new Uri(origin));
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000C160 File Offset: 0x0000A360
		public void SetCookie(string origin, string value)
		{
			string text = MemoryCookieProvider.Sanatize(value);
			this._container.SetCookies(new Uri(origin), text);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000C188 File Offset: 0x0000A388
		private static string Sanatize(string cookie)
		{
			string text = "expires=";
			int num3;
			for (int i = 0; i < cookie.Length; i = num3)
			{
				int num = cookie.IndexOf(text, i, StringComparison.OrdinalIgnoreCase);
				if (num == -1)
				{
					break;
				}
				int num2 = num + text.Length;
				num3 = cookie.IndexOfAny(new char[] { ';', ',' }, num2 + 4);
				if (num3 == -1)
				{
					num3 = cookie.Length;
				}
				string text2 = cookie.Substring(0, num2);
				string text3 = cookie.Substring(num2, num3 - num2);
				string text4 = cookie.Substring(num3);
				DateTime now = DateTime.Now;
				if (DateTime.TryParse(text3.Replace("UTC", "GMT"), out now))
				{
					string text5 = now.ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture);
					cookie = string.Format("{0}{1}{2}", text2, text5, text4);
				}
			}
			return cookie;
		}

		// Token: 0x040001CE RID: 462
		private readonly CookieContainer _container;
	}
}
