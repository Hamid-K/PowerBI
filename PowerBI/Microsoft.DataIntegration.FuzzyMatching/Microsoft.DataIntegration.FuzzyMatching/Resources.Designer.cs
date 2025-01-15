using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000059 RID: 89
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000374 RID: 884 RVA: 0x00011306 File Offset: 0x0000F506
		internal Resources()
		{
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0001130E File Offset: 0x0000F50E
		[EditorBrowsable(2)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Microsoft.DataIntegration.FuzzyMatching.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0001133A File Offset: 0x0000F53A
		// (set) Token: 0x06000377 RID: 887 RVA: 0x00011341 File Offset: 0x0000F541
		[EditorBrowsable(2)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00011349 File Offset: 0x0000F549
		internal static string Common_xsd
		{
			get
			{
				return Resources.ResourceManager.GetString("Common_xsd", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0001135F File Offset: 0x0000F55F
		internal static string ComparisonDefinition_xsd
		{
			get
			{
				return Resources.ResourceManager.GetString("ComparisonDefinition_xsd", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00011375 File Offset: 0x0000F575
		internal static string Configuration_xsd
		{
			get
			{
				return Resources.ResourceManager.GetString("Configuration_xsd", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0001138B File Offset: 0x0000F58B
		internal static string DomainManager_xsd
		{
			get
			{
				return Resources.ResourceManager.GetString("DomainManager_xsd", Resources.resourceCulture);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600037C RID: 892 RVA: 0x000113A1 File Offset: 0x0000F5A1
		internal static string IndexDefinition_xsd
		{
			get
			{
				return Resources.ResourceManager.GetString("IndexDefinition_xsd", Resources.resourceCulture);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600037D RID: 893 RVA: 0x000113B7 File Offset: 0x0000F5B7
		internal static string QueryDefinition_xsd
		{
			get
			{
				return Resources.ResourceManager.GetString("QueryDefinition_xsd", Resources.resourceCulture);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600037E RID: 894 RVA: 0x000113CD File Offset: 0x0000F5CD
		internal static string Results_xsd
		{
			get
			{
				return Resources.ResourceManager.GetString("Results_xsd", Resources.resourceCulture);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600037F RID: 895 RVA: 0x000113E3 File Offset: 0x0000F5E3
		internal static string RowsetManager_xsd
		{
			get
			{
				return Resources.ResourceManager.GetString("RowsetManager_xsd", Resources.resourceCulture);
			}
		}

		// Token: 0x0400012E RID: 302
		private static ResourceManager resourceMan;

		// Token: 0x0400012F RID: 303
		private static CultureInfo resourceCulture;
	}
}
