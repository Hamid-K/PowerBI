using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001CA RID: 458
	internal class RDLUpgradeStringsWrapper
	{
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x000244F8 File Offset: 0x000226F8
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x000244FF File Offset: 0x000226FF
		public static CultureInfo Culture
		{
			get
			{
				return RDLUpgradeStringsWrapper.Keys.Culture;
			}
			set
			{
				RDLUpgradeStringsWrapper.Keys.Culture = value;
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00024507 File Offset: 0x00022707
		public static string rdlInvalidTargetNamespace(string @namespace)
		{
			return RDLUpgradeStringsWrapper.Keys.GetString("rdlInvalidTargetNamespace", @namespace);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00024514 File Offset: 0x00022714
		public static string rdlInvalidXmlContents(string innerMessage)
		{
			return RDLUpgradeStringsWrapper.Keys.GetString("rdlInvalidXmlContents", innerMessage);
		}

		// Token: 0x020003E2 RID: 994
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06001899 RID: 6297 RVA: 0x0003B9F1 File Offset: 0x00039BF1
			private Keys()
			{
			}

			// Token: 0x1700074B RID: 1867
			// (get) Token: 0x0600189A RID: 6298 RVA: 0x0003B9F9 File Offset: 0x00039BF9
			// (set) Token: 0x0600189B RID: 6299 RVA: 0x0003BA00 File Offset: 0x00039C00
			public static CultureInfo Culture
			{
				get
				{
					return RDLUpgradeStringsWrapper.Keys._culture;
				}
				set
				{
					RDLUpgradeStringsWrapper.Keys._culture = value;
				}
			}

			// Token: 0x0600189C RID: 6300 RVA: 0x0003BA08 File Offset: 0x00039C08
			public static string GetString(string key)
			{
				return RDLUpgradeStringsWrapper.Keys.resourceManager.GetString(key, RDLUpgradeStringsWrapper.Keys._culture);
			}

			// Token: 0x0600189D RID: 6301 RVA: 0x0003BA1A File Offset: 0x00039C1A
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, RDLUpgradeStringsWrapper.Keys.resourceManager.GetString(key, RDLUpgradeStringsWrapper.Keys._culture), arg0);
			}

			// Token: 0x0400079E RID: 1950
			private static ResourceManager resourceManager = RDLUpgradeStrings.ResourceManager;

			// Token: 0x0400079F RID: 1951
			private static CultureInfo _culture = null;

			// Token: 0x040007A0 RID: 1952
			public const string rdlInvalidTargetNamespace = "rdlInvalidTargetNamespace";

			// Token: 0x040007A1 RID: 1953
			public const string rdlInvalidXmlContents = "rdlInvalidXmlContents";
		}
	}
}
