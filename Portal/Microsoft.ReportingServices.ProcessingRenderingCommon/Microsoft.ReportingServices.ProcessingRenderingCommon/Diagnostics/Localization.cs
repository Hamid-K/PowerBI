using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000020 RID: 32
	public sealed class Localization
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00005021 File Offset: 0x00003221
		public static string ClientPrimaryCultureKey
		{
			get
			{
				return "_Localization_ClientPrimaryCulture";
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005028 File Offset: 0x00003228
		public static string ClientBrowserCultureKey
		{
			get
			{
				return "_Localization_ClientBrowserCulture";
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000502F File Offset: 0x0000322F
		public static string ClientCurrentCultureKey
		{
			get
			{
				return "_Localization_ClientCurrentCultureKey";
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005036 File Offset: 0x00003236
		public static string ClientCurrentNativeUICultureKey
		{
			get
			{
				return "_Localization_ClientCurrentNativeUICultureKey";
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005040 File Offset: 0x00003240
		static Localization()
		{
			try
			{
				RevertImpersonationContext.Run(delegate
				{
					Localization.m_defaultReportServerCulture = CultureInfo.GetCultureInfo(Localization.GetUserDefaultUILanguage());
					Localization.GetInstalledCultures();
				});
			}
			catch
			{
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000050D4 File Offset: 0x000032D4
		public static void SetCultureFromPriorityList(string[] localeList)
		{
			Localization.SetCultureFromPriorityList(localeList, true);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000050DD File Offset: 0x000032DD
		private static void SetContextCultureData(AsyncLocal<string> key, string locale)
		{
			if (!string.IsNullOrEmpty(locale))
			{
				key.Value = Localization.GetNormalizedLocale(locale);
				return;
			}
			key.Value = null;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000050FB File Offset: 0x000032FB
		public static void SetBrowserCultureFromString(string locale)
		{
			Localization.SetContextCultureData(Localization._clientBrowserCulture, locale);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005108 File Offset: 0x00003308
		public static void SetClientCultureFromString(string locale)
		{
			Localization.SetContextCultureData(Localization._clientCurrentCulture, locale);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005118 File Offset: 0x00003318
		private static string GetCultureFromKey(AsyncLocal<string> key)
		{
			string text = key.Value;
			if (string.IsNullOrEmpty(text))
			{
				text = Localization.ClientPrimaryCulture.Name;
			}
			return text;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005140 File Offset: 0x00003340
		public static string ClientBrowserCultureName
		{
			get
			{
				return Localization.GetCultureFromKey(Localization._clientBrowserCulture);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000514C File Offset: 0x0000334C
		public static string ClientCurrentCultureName
		{
			get
			{
				return Localization.GetCultureFromKey(Localization._clientCurrentCulture);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005158 File Offset: 0x00003358
		public static string ClientCurrentNativeUICultureName
		{
			get
			{
				return Localization.GetCultureFromKey(Localization._clientCurrentNativeUICulture);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005164 File Offset: 0x00003364
		private static bool IsLocaleMatchCulture(string locale)
		{
			try
			{
				CultureInfo.CreateSpecificCulture(locale);
				return true;
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005194 File Offset: 0x00003394
		private static void AddLocaleToValidatedList(string locale, string normalizedLocale)
		{
			if (!Localization.m_validatedLocaleNames.ContainsKey(locale))
			{
				Hashtable validatedLocaleNames = Localization.m_validatedLocaleNames;
				lock (validatedLocaleNames)
				{
					if (!Localization.m_validatedLocaleNames.ContainsKey(locale))
					{
						Localization.m_validatedLocaleNames.Add(locale, normalizedLocale);
					}
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000051F4 File Offset: 0x000033F4
		private static string GetNormalizedLocale(string locale)
		{
			int num = locale.IndexOf(';');
			if (num != -1)
			{
				locale = locale.Substring(0, num);
			}
			locale = locale.Trim();
			if (Localization.m_validatedLocaleNames.ContainsKey(locale))
			{
				return (string)Localization.m_validatedLocaleNames[locale];
			}
			if (Localization.IsLocaleMatchCulture(locale))
			{
				Localization.AddLocaleToValidatedList(locale, locale);
				return locale;
			}
			string text = locale;
			string[] array = locale.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 3)
			{
				text = array[0] + "-" + array[2];
			}
			else if (array.Length == 4 && array[2] == "x")
			{
				text = array[0] + "-" + array[1];
			}
			else if (array.Length == 5 && array[3] == "x")
			{
				text = array[0] + "-" + array[2];
			}
			if (text != locale && Localization.IsLocaleMatchCulture(text))
			{
				Localization.AddLocaleToValidatedList(locale, text);
				return text;
			}
			Localization.AddLocaleToValidatedList(locale, locale);
			return locale;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000052F0 File Offset: 0x000034F0
		public static void SetCultureFromPriorityList(string[] localeList, bool setUICulture)
		{
			if (localeList != null)
			{
				Localization._clientCurrentNativeUICulture.Value = string.Join(",", localeList);
			}
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			bool flag = false;
			if (localeList != null)
			{
				for (int i = 0; i < localeList.Length; i++)
				{
					string normalizedLocale = Localization.GetNormalizedLocale(localeList[i]);
					if (!flag)
					{
						try
						{
							CultureInfo cultureInfoNoUserOverrides = Localization.GetCultureInfoNoUserOverrides(normalizedLocale);
							Localization._clientPrimaryCulture.Value = cultureInfoNoUserOverrides;
							flag = true;
						}
						catch (Exception)
						{
						}
					}
					if (setUICulture)
					{
						CultureInfo cultureInfo = Localization.InstalledCulture(normalizedLocale);
						if (cultureInfo != null)
						{
							Thread.CurrentThread.CurrentUICulture = cultureInfo;
							return;
						}
					}
				}
			}
			if (setUICulture)
			{
				Thread.CurrentThread.CurrentUICulture = Localization.FallbackUICulture;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000053A0 File Offset: 0x000035A0
		public static void SetCultureFromThread()
		{
			Localization._clientPrimaryCulture.Value = Thread.CurrentThread.CurrentCulture;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000053B6 File Offset: 0x000035B6
		public static void Reset()
		{
			Localization._clientPrimaryCulture.Value = null;
			Localization._clientBrowserCulture.Value = null;
			Localization._clientCurrentCulture.Value = null;
			Thread.CurrentThread.CurrentUICulture = Localization.FallbackUICulture;
			Localization._clientCurrentNativeUICulture.Value = null;
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000053F3 File Offset: 0x000035F3
		public static CultureInfo DefaultReportServerCulture
		{
			get
			{
				return Localization.m_defaultReportServerCulture;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000053FA File Offset: 0x000035FA
		internal static void SetDefaultReportServerCulture(CultureInfo culture)
		{
			Localization.m_defaultReportServerCulture = culture;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005404 File Offset: 0x00003604
		public static CultureInfo DefaultReportServerSpecificCulture
		{
			get
			{
				CultureInfo defaultReportServerCulture = Localization.DefaultReportServerCulture;
				if (defaultReportServerCulture.IsNeutralCulture)
				{
					return Localization.GetCultureInfoNoUserOverrides(defaultReportServerCulture.Name);
				}
				return defaultReportServerCulture;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000542C File Offset: 0x0000362C
		public static CultureInfo FallbackUICulture
		{
			get
			{
				CultureInfo cultureInfo = Localization.InstalledCulture(Localization.DefaultReportServerCulture.Name);
				if (cultureInfo == null)
				{
					return Localization.GetCultureInfoNoUserOverrides(9);
				}
				return cultureInfo;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005458 File Offset: 0x00003658
		public static CultureInfo ClientPrimaryCulture
		{
			get
			{
				object value = Localization._clientPrimaryCulture.Value;
				if (value == null)
				{
					return Localization.GetCultureInfoNoUserOverrides(Localization.DefaultReportServerCulture.Name);
				}
				return (CultureInfo)value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000548C File Offset: 0x0000368C
		public static CultureInfo ReportParameterCulture
		{
			get
			{
				CultureInfo value = Localization._localization_ReportParameterCulture.Value;
				if (value == null)
				{
					return Localization.ClientPrimaryCulture;
				}
				return value;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000054B0 File Offset: 0x000036B0
		public static void SetReportParameterCulture(string cultureName)
		{
			try
			{
				CultureInfo cultureInfoNoUserOverrides = Localization.GetCultureInfoNoUserOverrides(cultureName);
				Localization._localization_ReportParameterCulture.Value = cultureInfoNoUserOverrides;
			}
			catch (ArgumentException)
			{
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000054E4 File Offset: 0x000036E4
		public static CultureInfo GetCultureInfoNoUserOverrides(string cultureName)
		{
			return Localization.GetCultureInfoNoUserOverrides(CultureInfo.CreateSpecificCulture(cultureName).LCID);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000054F6 File Offset: 0x000036F6
		private static CultureInfo GetCultureInfoNoUserOverrides(int lcid)
		{
			return new CultureInfo(lcid, false);
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000054FF File Offset: 0x000036FF
		public static CultureInfo CatalogCulture
		{
			get
			{
				return CultureInfo.InvariantCulture;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005506 File Offset: 0x00003706
		public static int CatalogCultureCompare(string a, string b)
		{
			return string.Compare(a, b, Localization.CatalogStringComparison);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005514 File Offset: 0x00003714
		public static StringComparison CatalogStringComparison
		{
			get
			{
				return StringComparison.InvariantCultureIgnoreCase;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005517 File Offset: 0x00003717
		public static StringComparer CatalogStringComparer
		{
			get
			{
				return StringComparer.InvariantCultureIgnoreCase;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000551E File Offset: 0x0000371E
		public static ArrayList InstalledCultureNames
		{
			get
			{
				return Localization.m_installedCultureNames;
			}
		}

		// Token: 0x06000104 RID: 260
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern int GetUserDefaultUILanguage();

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005528 File Offset: 0x00003728
		private static string CurrentDir
		{
			get
			{
				if (Localization.m_currentDir == null)
				{
					string text;
					if (ProcessingContext.ReqContext != null)
					{
						text = ProcessingContext.ReqContext.MapPath(ProcessingContext.ReqContext.ApplicationPath);
						text += "\\bin";
					}
					else
					{
						text = AppDomain.CurrentDomain.BaseDirectory;
					}
					Localization.m_currentDir = text;
				}
				return Localization.m_currentDir;
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000557C File Offset: 0x0000377C
		private static void GetInstalledCultures()
		{
			HashSet<string> hashSet = new HashSet<string>(from cultureInfo in CultureInfo.GetCultures(CultureTypes.AllCultures)
				select cultureInfo.Name);
			string[] directories = Directory.GetDirectories(Localization.CurrentDir, "*");
			for (int i = 0; i < directories.Length; i++)
			{
				string fileName = Path.GetFileName(directories[i]);
				if (hashSet.Contains(fileName))
				{
					CultureInfo cultureInfo3 = new CultureInfo(fileName, false);
					Localization.m_installedLocales.Add(cultureInfo3.LCID);
					Localization.m_installedCultureNames.Add(cultureInfo3.Name);
				}
			}
			if (!Localization.m_installedLocales.Contains(9))
			{
				CultureInfo cultureInfo2 = new CultureInfo(9);
				Localization.m_installedLocales.Add(9);
				Localization.m_installedCultureNames.Add(cultureInfo2.Name);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000565C File Offset: 0x0000385C
		private static CultureInfo InstalledCulture(string locale)
		{
			CultureInfo cultureInfoNoUserOverrides;
			try
			{
				cultureInfoNoUserOverrides = Localization.GetCultureInfoNoUserOverrides(locale);
			}
			catch (Exception)
			{
				return null;
			}
			for (CultureInfo cultureInfo = cultureInfoNoUserOverrides; cultureInfo != CultureInfo.InvariantCulture; cultureInfo = cultureInfo.Parent)
			{
				if (Localization.m_installedLocales.Contains(cultureInfo.LCID))
				{
					return cultureInfo;
				}
			}
			return null;
		}

		// Token: 0x04000088 RID: 136
		private const int NeutralCultureLCID = 9;

		// Token: 0x04000089 RID: 137
		private static AsyncLocal<CultureInfo> _clientPrimaryCulture = new AsyncLocal<CultureInfo>();

		// Token: 0x0400008A RID: 138
		private static AsyncLocal<string> _clientBrowserCulture = new AsyncLocal<string>();

		// Token: 0x0400008B RID: 139
		private static AsyncLocal<string> _clientCurrentCulture = new AsyncLocal<string>();

		// Token: 0x0400008C RID: 140
		private static AsyncLocal<string> _clientCurrentNativeUICulture = new AsyncLocal<string>();

		// Token: 0x0400008D RID: 141
		private static AsyncLocal<CultureInfo> _localization_ReportParameterCulture = new AsyncLocal<CultureInfo>();

		// Token: 0x0400008E RID: 142
		private const string _ReportParameterCulture = "_Localization_ReportParameterCulture";

		// Token: 0x0400008F RID: 143
		private static ArrayList m_installedLocales = new ArrayList();

		// Token: 0x04000090 RID: 144
		private static ArrayList m_installedCultureNames = new ArrayList();

		// Token: 0x04000091 RID: 145
		private static Hashtable m_validatedLocaleNames = new Hashtable();

		// Token: 0x04000092 RID: 146
		private static CultureInfo m_defaultReportServerCulture = CultureInfo.InstalledUICulture;

		// Token: 0x04000093 RID: 147
		private static string m_currentDir;
	}
}
