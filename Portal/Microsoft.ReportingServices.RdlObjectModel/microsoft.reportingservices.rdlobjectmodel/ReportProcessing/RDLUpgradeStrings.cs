using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200007C RID: 124
	[CompilerGenerated]
	internal class RDLUpgradeStrings
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x000175CE File Offset: 0x000157CE
		protected RDLUpgradeStrings()
		{
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x000175D6 File Offset: 0x000157D6
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x000175DD File Offset: 0x000157DD
		public static CultureInfo Culture
		{
			get
			{
				return RDLUpgradeStrings.Keys.Culture;
			}
			set
			{
				RDLUpgradeStrings.Keys.Culture = value;
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000175E5 File Offset: 0x000157E5
		public static string rdlInvalidTargetNamespace(string @namespace)
		{
			return RDLUpgradeStrings.Keys.GetString("rdlInvalidTargetNamespace", @namespace);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000175F2 File Offset: 0x000157F2
		public static string rdlInvalidXmlContents(string innerMessage)
		{
			return RDLUpgradeStrings.Keys.GetString("rdlInvalidXmlContents", innerMessage);
		}

		// Token: 0x02000330 RID: 816
		[CompilerGenerated]
		public class Keys
		{
			// Token: 0x06001791 RID: 6033 RVA: 0x0003A5BC File Offset: 0x000387BC
			private Keys()
			{
			}

			// Token: 0x17000733 RID: 1843
			// (get) Token: 0x06001792 RID: 6034 RVA: 0x0003A5C4 File Offset: 0x000387C4
			// (set) Token: 0x06001793 RID: 6035 RVA: 0x0003A5CB File Offset: 0x000387CB
			public static CultureInfo Culture
			{
				get
				{
					return RDLUpgradeStrings.Keys._culture;
				}
				set
				{
					RDLUpgradeStrings.Keys._culture = value;
				}
			}

			// Token: 0x06001794 RID: 6036 RVA: 0x0003A5D3 File Offset: 0x000387D3
			public static string GetString(string key)
			{
				return RDLUpgradeStrings.Keys.resourceManager.GetString(key, RDLUpgradeStrings.Keys._culture);
			}

			// Token: 0x06001795 RID: 6037 RVA: 0x0003A5E5 File Offset: 0x000387E5
			public static string GetString(string key, object arg0)
			{
				return string.Format(CultureInfo.CurrentCulture, RDLUpgradeStrings.Keys.resourceManager.GetString(key, RDLUpgradeStrings.Keys._culture), arg0);
			}

			// Token: 0x0400075F RID: 1887
			private static ResourceManager resourceManager = new ResourceManager(typeof(RDLUpgradeStrings).FullName, typeof(RDLUpgradeStrings).Module.Assembly);

			// Token: 0x04000760 RID: 1888
			private static CultureInfo _culture = null;

			// Token: 0x04000761 RID: 1889
			public const string rdlInvalidTargetNamespace = "rdlInvalidTargetNamespace";

			// Token: 0x04000762 RID: 1890
			public const string rdlInvalidXmlContents = "rdlInvalidXmlContents";
		}
	}
}
