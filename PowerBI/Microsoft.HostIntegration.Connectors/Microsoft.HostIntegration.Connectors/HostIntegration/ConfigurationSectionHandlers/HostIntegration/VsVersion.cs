using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005C7 RID: 1479
	public class VsVersion : ConfigurationElement
	{
		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x0600335A RID: 13146 RVA: 0x000AD539 File Offset: 0x000AB739
		// (set) Token: 0x0600335B RID: 13147 RVA: 0x000AD54B File Offset: 0x000AB74B
		[Description("Version Number")]
		[Category("General")]
		[ConfigurationProperty("version", IsRequired = true)]
		public int Version
		{
			get
			{
				return (int)base["version"];
			}
			set
			{
				base["version"] = value;
			}
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x000AD55E File Offset: 0x000AB75E
		public object GetElementKey()
		{
			return this.Version;
		}
	}
}
