using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005C2 RID: 1474
	public class OptionalServices : ConfigurationElement
	{
		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x000AD3A7 File Offset: 0x000AB5A7
		// (set) Token: 0x0600333C RID: 13116 RVA: 0x000AD3B9 File Offset: 0x000AB5B9
		[Description("Enable SNA Print")]
		[Category("General")]
		[ConfigurationProperty("snaPrintEnabled", IsRequired = false, DefaultValue = "true")]
		public bool SnaPrintEnabled
		{
			get
			{
				return (bool)base["snaPrintEnabled"];
			}
			set
			{
				base["snaPrintEnabled"] = value;
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x000AD3CC File Offset: 0x000AB5CC
		// (set) Token: 0x0600333E RID: 13118 RVA: 0x000AD3DE File Offset: 0x000AB5DE
		[Description("Enable TN3270")]
		[Category("General")]
		[ConfigurationProperty("tn3270Enabled", IsRequired = false, DefaultValue = "true")]
		public bool Tn3270Enabled
		{
			get
			{
				return (bool)base["tn3270Enabled"];
			}
			set
			{
				base["tn3270Enabled"] = value;
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000AD3F1 File Offset: 0x000AB5F1
		// (set) Token: 0x06003340 RID: 13120 RVA: 0x000AD403 File Offset: 0x000AB603
		[Description("Enable TN5250")]
		[Category("General")]
		[ConfigurationProperty("tn5250Enabled", IsRequired = false, DefaultValue = "false")]
		public bool Tn5250Enabled
		{
			get
			{
				return (bool)base["tn5250Enabled"];
			}
			set
			{
				base["tn5250Enabled"] = value;
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x000AD416 File Offset: 0x000AB616
		// (set) Token: 0x06003342 RID: 13122 RVA: 0x000AD428 File Offset: 0x000AB628
		[Description("Enable Netview")]
		[Category("General")]
		[ConfigurationProperty("netviewEnabled", IsRequired = false, DefaultValue = "false")]
		public bool NetviewEnabled
		{
			get
			{
				return (bool)base["netviewEnabled"];
			}
			set
			{
				base["netviewEnabled"] = value;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x000AD43B File Offset: 0x000AB63B
		// (set) Token: 0x06003344 RID: 13124 RVA: 0x000AD44D File Offset: 0x000AB64D
		[Description("Enable LU6.2 Resync")]
		[Category("General")]
		[ConfigurationProperty("resyncEnabled", IsRequired = false, DefaultValue = "true")]
		public bool ResyncEnabled
		{
			get
			{
				return (bool)base["resyncEnabled"];
			}
			set
			{
				base["resyncEnabled"] = value;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000AD460 File Offset: 0x000AB660
		// (set) Token: 0x06003346 RID: 13126 RVA: 0x000AD472 File Offset: 0x000AB672
		[Description("Enable Demos")]
		[Category("General")]
		[ConfigurationProperty("demosEnabled", IsRequired = false, DefaultValue = "false")]
		public bool DemosEnabled
		{
			get
			{
				return (bool)base["demosEnabled"];
			}
			set
			{
				base["demosEnabled"] = value;
			}
		}
	}
}
