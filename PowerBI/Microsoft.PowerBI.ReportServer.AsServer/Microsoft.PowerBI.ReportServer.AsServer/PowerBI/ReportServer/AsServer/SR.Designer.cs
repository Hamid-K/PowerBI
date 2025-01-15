using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000023 RID: 35
	[CompilerGenerated]
	internal class SR
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00002050 File Offset: 0x00000250
		protected SR()
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004544 File Offset: 0x00002744
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x0000454B File Offset: 0x0000274B
		public static CultureInfo Culture
		{
			get
			{
				return SR.Keys.Culture;
			}
			set
			{
				SR.Keys.Culture = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004553 File Offset: 0x00002753
		public static string Error_LogonFailed
		{
			get
			{
				return SR.Keys.GetString("Error_LogonFailed");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000455F File Offset: 0x0000275F
		public static string Error_LanguageError
		{
			get
			{
				return SR.Keys.GetString("Error_LanguageError");
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000456B File Offset: 0x0000276B
		public static string Error_UnsupportedDataSourceLocation(string kind)
		{
			return SR.Keys.GetString("Error_UnsupportedDataSourceLocation", kind);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004578 File Offset: 0x00002778
		public static string Error_UnsupportedAuthenticationType(string authType)
		{
			return SR.Keys.GetString("Error_UnsupportedAuthenticationType", authType);
		}

		// Token: 0x02000047 RID: 71
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06000160 RID: 352 RVA: 0x00002050 File Offset: 0x00000250
			private Keys()
			{
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x06000161 RID: 353 RVA: 0x000063B2 File Offset: 0x000045B2
			// (set) Token: 0x06000162 RID: 354 RVA: 0x000063B9 File Offset: 0x000045B9
			public static CultureInfo Culture
			{
				get
				{
					return SR.Keys._culture;
				}
				set
				{
					SR.Keys._culture = value;
				}
			}

			// Token: 0x06000163 RID: 355 RVA: 0x000063C1 File Offset: 0x000045C1
			public static string GetString(string key)
			{
				return SR.Keys.resourceManager.GetString(key, SR.Keys._culture);
			}

			// Token: 0x06000164 RID: 356 RVA: 0x000063D3 File Offset: 0x000045D3
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, SR.Keys.resourceManager.GetString(key, SR.Keys._culture), arg0);
			}

			// Token: 0x040000E1 RID: 225
			private static ResourceManager resourceManager = new ResourceManager(typeof(SR).FullName, typeof(SR).Module.Assembly);

			// Token: 0x040000E2 RID: 226
			private static CultureInfo _culture = null;

			// Token: 0x040000E3 RID: 227
			public const string Error_UnsupportedDataSourceLocation = "Error_UnsupportedDataSourceLocation";

			// Token: 0x040000E4 RID: 228
			public const string Error_UnsupportedAuthenticationType = "Error_UnsupportedAuthenticationType";

			// Token: 0x040000E5 RID: 229
			public const string Error_LogonFailed = "Error_LogonFailed";

			// Token: 0x040000E6 RID: 230
			public const string Error_LanguageError = "Error_LanguageError";
		}
	}
}
