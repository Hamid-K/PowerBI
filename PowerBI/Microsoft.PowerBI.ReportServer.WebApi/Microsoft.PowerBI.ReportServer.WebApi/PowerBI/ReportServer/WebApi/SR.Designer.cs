using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.ReportServer.WebApi
{
	// Token: 0x0200000F RID: 15
	[CompilerGenerated]
	internal class SR
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002C9D File Offset: 0x00000E9D
		protected SR()
		{
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002CA5 File Offset: 0x00000EA5
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002CAC File Offset: 0x00000EAC
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

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public static string Error_UnsupportedPowerBIReportVersion
		{
			get
			{
				return SR.Keys.GetString("Error_UnsupportedPowerBIReportVersion");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public static string Error_DataSourceConnectionErrorNotVisible
		{
			get
			{
				return SR.Keys.GetString("Error_DataSourceConnectionErrorNotVisible");
			}
		}

		// Token: 0x02000048 RID: 72
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06000149 RID: 329 RVA: 0x00002C9D File Offset: 0x00000E9D
			private Keys()
			{
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x0600014A RID: 330 RVA: 0x00008267 File Offset: 0x00006467
			// (set) Token: 0x0600014B RID: 331 RVA: 0x0000826E File Offset: 0x0000646E
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

			// Token: 0x0600014C RID: 332 RVA: 0x00008276 File Offset: 0x00006476
			public static string GetString(string key)
			{
				return SR.Keys.resourceManager.GetString(key, SR.Keys._culture);
			}

			// Token: 0x040000E2 RID: 226
			private static ResourceManager resourceManager = new ResourceManager(typeof(SR).FullName, typeof(SR).Module.Assembly);

			// Token: 0x040000E3 RID: 227
			private static CultureInfo _culture = null;

			// Token: 0x040000E4 RID: 228
			public const string Error_UnsupportedPowerBIReportVersion = "Error_UnsupportedPowerBIReportVersion";

			// Token: 0x040000E5 RID: 229
			public const string Error_DataSourceConnectionErrorNotVisible = "Error_DataSourceConnectionErrorNotVisible";
		}
	}
}
