using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005C4 RID: 1476
	public class ServiceCredential : ConfigurationElement
	{
		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06003350 RID: 13136 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[Description("Service User Name")]
		[Category("General")]
		[ConfigurationProperty("name", IsRequired = true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06003351 RID: 13137 RVA: 0x000ACFC4 File Offset: 0x000AB1C4
		// (set) Token: 0x06003352 RID: 13138 RVA: 0x000ACFD6 File Offset: 0x000AB1D6
		[Description("Service Password")]
		[Category("General")]
		[ConfigurationProperty("password", IsRequired = true)]
		public string Password
		{
			get
			{
				return (string)base["password"];
			}
			set
			{
				base["password"] = value;
			}
		}
	}
}
