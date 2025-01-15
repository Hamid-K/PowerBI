using System;
using System.Globalization;
using System.Linq;
using Microsoft.Owin;

namespace Microsoft.BIServer.Owin.Common
{
	// Token: 0x0200000A RID: 10
	public static class LanguageUtils
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002394 File Offset: 0x00000594
		public static string GetClientLocale(IOwinContext context)
		{
			string localeFromRequest = LanguageUtils.GetLocaleFromRequest(context);
			if (!string.IsNullOrEmpty(localeFromRequest))
			{
				if (localeFromRequest.Contains("en-"))
				{
					return localeFromRequest;
				}
				if (localeFromRequest.Equals("pt-PT", StringComparison.OrdinalIgnoreCase))
				{
					return localeFromRequest;
				}
				string[] array = localeFromRequest.Split(new char[] { '-' });
				if (array.Length > 2)
				{
					return array[0] + "-" + array[1];
				}
				if (array.Length != 0)
				{
					string text = array[0].ToLower();
					if ("zhsrnb".IndexOf(text) != -1)
					{
						return LanguageUtils.MapLocale(localeFromRequest.ToLower(), text);
					}
					return array[0];
				}
			}
			return "en-US";
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000242C File Offset: 0x0000062C
		public static string GetLocaleFromRequest(IOwinContext context)
		{
			string text = null;
			if (context != null)
			{
				QueryString queryString = context.Request.QueryString;
				if (!string.IsNullOrEmpty(context.Request.Query["language"]))
				{
					text = context.Request.Query["language"];
				}
				else if (context.Request.Headers["Accept-Language"] != null)
				{
					text = (from s in context.Request.Headers["Accept-Language"].Split(new char[] { ',' })
						select s.Split(new char[] { ';' })[0]).FirstOrDefault<string>();
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = null;
			}
			else
			{
				try
				{
					CultureInfo.GetCultureInfo(text);
				}
				catch (CultureNotFoundException)
				{
					text = null;
				}
			}
			return text;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002514 File Offset: 0x00000714
		public static bool IsCurrentContextRtl(IOwinContext context)
		{
			try
			{
				return CultureInfo.GetCultureInfo(LanguageUtils.GetClientLocale(context)).TextInfo.IsRightToLeft;
			}
			catch (CultureNotFoundException)
			{
			}
			return false;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002550 File Offset: 0x00000750
		public static CultureInfo GetClientCultureInfo(IOwinContext context)
		{
			CultureInfo cultureInfo;
			try
			{
				cultureInfo = CultureInfo.GetCultureInfo(LanguageUtils.GetClientLocale(context));
			}
			catch (CultureNotFoundException)
			{
				cultureInfo = CultureInfo.GetCultureInfo("en-US");
			}
			return cultureInfo;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000258C File Offset: 0x0000078C
		private static string MapLocale(string locale, string prefix)
		{
			if (prefix.Equals("zh"))
			{
				if (locale.IndexOf("cn") > 0 || locale.IndexOf("sg") > 0 || locale.IndexOf("hans") > 0 || locale.IndexOf("chs") > 0)
				{
					return "zh-Hans";
				}
				return "zh-Hant";
			}
			else if (prefix.Equals("sr"))
			{
				if (locale.IndexOf("rs") > 0 || locale.IndexOf("ba") > 0 || locale.IndexOf("me") > 0 || locale.IndexOf("cs") > 0 || locale.IndexOf("latn") > 0)
				{
					return "sr-Latn";
				}
				return "sr-Cyrl";
			}
			else
			{
				if (prefix.Equals("nb") && (locale.IndexOf("nb") != -1 || locale.IndexOf("no") != -1))
				{
					return "no";
				}
				return "en-US";
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000267D File Offset: 0x0000087D
		public static string GetTranslations(IOwinContext context)
		{
			return string.Empty;
		}
	}
}
