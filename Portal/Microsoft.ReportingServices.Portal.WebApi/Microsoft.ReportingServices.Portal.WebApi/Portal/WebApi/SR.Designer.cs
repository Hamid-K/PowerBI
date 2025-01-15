using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.Portal.WebApi
{
	// Token: 0x02000006 RID: 6
	[CompilerGenerated]
	internal class SR
	{
		// Token: 0x06000003 RID: 3 RVA: 0x0000211C File Offset: 0x0000031C
		protected SR()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002124 File Offset: 0x00000324
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000212B File Offset: 0x0000032B
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

		// Token: 0x0200000A RID: 10
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x0600000F RID: 15 RVA: 0x0000211C File Offset: 0x0000031C
			private Keys()
			{
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000010 RID: 16 RVA: 0x00002233 File Offset: 0x00000433
			// (set) Token: 0x06000011 RID: 17 RVA: 0x0000223A File Offset: 0x0000043A
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

			// Token: 0x06000012 RID: 18 RVA: 0x00002242 File Offset: 0x00000442
			public static string GetString(string key)
			{
				return SR.Keys.resourceManager.GetString(key, SR.Keys._culture);
			}

			// Token: 0x0400003A RID: 58
			private static ResourceManager resourceManager = new ResourceManager(typeof(SR).FullName, typeof(SR).Module.Assembly);

			// Token: 0x0400003B RID: 59
			private static CultureInfo _culture = null;
		}
	}
}
