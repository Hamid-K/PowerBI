using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000557 RID: 1367
	public class DrdaServiceTraceListener : ConfigurationElement
	{
		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x0002038A File Offset: 0x0001E58A
		// (set) Token: 0x06002E2F RID: 11823 RVA: 0x0002039C File Offset: 0x0001E59C
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

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002E30 RID: 11824 RVA: 0x0009B198 File Offset: 0x00099398
		// (set) Token: 0x06002E31 RID: 11825 RVA: 0x000203BC File Offset: 0x0001E5BC
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("settings", IsRequired = false)]
		public string Settings
		{
			get
			{
				return (string)base["settings"];
			}
			set
			{
				base["settings"] = value;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002E32 RID: 11826 RVA: 0x0009B1AA File Offset: 0x000993AA
		// (set) Token: 0x06002E33 RID: 11827 RVA: 0x0009B1BC File Offset: 0x000993BC
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("traceLevel", IsRequired = false, DefaultValue = -1)]
		public int TraceLevel
		{
			get
			{
				return (int)base["traceLevel"];
			}
			set
			{
				base["traceLevel"] = value;
			}
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x0009B1CF File Offset: 0x000993CF
		public object GetElementKey()
		{
			return this.Type;
		}
	}
}
