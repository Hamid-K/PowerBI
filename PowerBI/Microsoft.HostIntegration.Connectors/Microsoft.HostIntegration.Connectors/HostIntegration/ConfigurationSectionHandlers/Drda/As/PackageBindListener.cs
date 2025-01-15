using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x0200054B RID: 1355
	public class PackageBindListener : ConfigurationElement
	{
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x0002038A File Offset: 0x0001E58A
		// (set) Token: 0x06002DBC RID: 11708 RVA: 0x0002039C File Offset: 0x0001E59C
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("type", IsRequired = true)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002DBD RID: 11709 RVA: 0x0009AC11 File Offset: 0x00098E11
		// (set) Token: 0x06002DBE RID: 11710 RVA: 0x0009AC23 File Offset: 0x00098E23
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("errorWhenNoCallback", IsRequired = false, DefaultValue = true)]
		public bool ErrorWhenNoCallback
		{
			get
			{
				return (bool)base["errorWhenNoCallback"];
			}
			set
			{
				base["errorWhenNoCallback"] = value;
			}
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x0009AC36 File Offset: 0x00098E36
		public object GetElementKey()
		{
			return this.Type;
		}
	}
}
