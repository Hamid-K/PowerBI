using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.SqlServer.ReportingServices
{
	// Token: 0x02000072 RID: 114
	internal class SoapExceptionStrings
	{
		// Token: 0x06000379 RID: 889 RVA: 0x00002C7C File Offset: 0x00000E7C
		private SoapExceptionStrings()
		{
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00015B4B File Offset: 0x00013D4B
		// (set) Token: 0x0600037B RID: 891 RVA: 0x00015B52 File Offset: 0x00013D52
		public static CultureInfo Culture
		{
			get
			{
				return SoapExceptionStrings.Keys.Culture;
			}
			set
			{
				SoapExceptionStrings.Keys.Culture = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600037C RID: 892 RVA: 0x00015B5A File Offset: 0x00013D5A
		public static string MissingEndpoint
		{
			get
			{
				return SoapExceptionStrings.Keys.GetString("MissingEndpoint");
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00015B66 File Offset: 0x00013D66
		public static string VersionMismatch
		{
			get
			{
				return SoapExceptionStrings.Keys.GetString("VersionMismatch");
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00015B72 File Offset: 0x00013D72
		public static string RSSoapMessageFormat(string message, string errorCode)
		{
			return SoapExceptionStrings.Keys.GetString("RSSoapMessageFormat", message, errorCode);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00015B80 File Offset: 0x00013D80
		public static string SOAPProxySource
		{
			get
			{
				return SoapExceptionStrings.Keys.GetString("SOAPProxySource");
			}
		}

		// Token: 0x02000193 RID: 403
		public class Keys
		{
			// Token: 0x06000932 RID: 2354 RVA: 0x00002C7C File Offset: 0x00000E7C
			private Keys()
			{
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x06000933 RID: 2355 RVA: 0x00020B4E File Offset: 0x0001ED4E
			// (set) Token: 0x06000934 RID: 2356 RVA: 0x00020B55 File Offset: 0x0001ED55
			public static CultureInfo Culture
			{
				get
				{
					return SoapExceptionStrings.Keys._culture;
				}
				set
				{
					SoapExceptionStrings.Keys._culture = value;
				}
			}

			// Token: 0x06000935 RID: 2357 RVA: 0x00020B5D File Offset: 0x0001ED5D
			public static string GetString(string key)
			{
				return SoapExceptionStrings.Keys.resourceManager.GetString(key, SoapExceptionStrings.Keys._culture);
			}

			// Token: 0x06000936 RID: 2358 RVA: 0x00020B6F File Offset: 0x0001ED6F
			public static string GetString(string key, object arg0, object arg1)
			{
				return string.Format(CultureInfo.CurrentCulture, SoapExceptionStrings.Keys.resourceManager.GetString(key, SoapExceptionStrings.Keys._culture), arg0, arg1);
			}

			// Token: 0x040004A2 RID: 1186
			private static ResourceManager resourceManager = new ResourceManager(typeof(SoapExceptionStrings).FullName, typeof(SoapExceptionStrings).Module.Assembly);

			// Token: 0x040004A3 RID: 1187
			private static CultureInfo _culture = null;

			// Token: 0x040004A4 RID: 1188
			public const string SOAPProxySource = "SOAPProxySource";

			// Token: 0x040004A5 RID: 1189
			public const string MissingEndpoint = "MissingEndpoint";

			// Token: 0x040004A6 RID: 1190
			public const string VersionMismatch = "VersionMismatch";

			// Token: 0x040004A7 RID: 1191
			public const string RSSoapMessageFormat = "RSSoapMessageFormat";
		}
	}
}
