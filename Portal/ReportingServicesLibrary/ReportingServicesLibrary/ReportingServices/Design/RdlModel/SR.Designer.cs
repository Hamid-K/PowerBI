using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200042C RID: 1068
	[CompilerGenerated]
	internal class SR
	{
		// Token: 0x0600221A RID: 8730 RVA: 0x000025F4 File Offset: 0x000007F4
		protected SR()
		{
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x00081E0B File Offset: 0x0008000B
		// (set) Token: 0x0600221C RID: 8732 RVA: 0x00081E12 File Offset: 0x00080012
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

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x00081E1A File Offset: 0x0008001A
		public static string Language_bn
		{
			get
			{
				return SR.Keys.GetString("Language_bn");
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x00081E26 File Offset: 0x00080026
		public static string Language_or
		{
			get
			{
				return SR.Keys.GetString("Language_or");
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x00081E32 File Offset: 0x00080032
		public static string Language_lo
		{
			get
			{
				return SR.Keys.GetString("Language_lo");
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x00081E3E File Offset: 0x0008003E
		public static string Language_bo
		{
			get
			{
				return SR.Keys.GetString("Language_bo");
			}
		}

		// Token: 0x0200052C RID: 1324
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06002537 RID: 9527 RVA: 0x000025F4 File Offset: 0x000007F4
			private Keys()
			{
			}

			// Token: 0x17000ABB RID: 2747
			// (get) Token: 0x06002538 RID: 9528 RVA: 0x00087DCA File Offset: 0x00085FCA
			// (set) Token: 0x06002539 RID: 9529 RVA: 0x00087DD1 File Offset: 0x00085FD1
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

			// Token: 0x0600253A RID: 9530 RVA: 0x00087DD9 File Offset: 0x00085FD9
			public static string GetString(string key)
			{
				return SR.Keys.resourceManager.GetString(key, SR.Keys._culture);
			}

			// Token: 0x040012C2 RID: 4802
			private static ResourceManager resourceManager = new ResourceManager(typeof(SR).FullName, typeof(SR).Module.Assembly);

			// Token: 0x040012C3 RID: 4803
			private static CultureInfo _culture = null;

			// Token: 0x040012C4 RID: 4804
			public const string Language_bn = "Language_bn";

			// Token: 0x040012C5 RID: 4805
			public const string Language_or = "Language_or";

			// Token: 0x040012C6 RID: 4806
			public const string Language_lo = "Language_lo";

			// Token: 0x040012C7 RID: 4807
			public const string Language_bo = "Language_bo";
		}
	}
}
