using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005C9 RID: 1481
	public class ExternalService : ConfigurationElement
	{
		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06003368 RID: 13160 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[Description("Service's Name")]
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

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x000AD5A2 File Offset: 0x000AB7A2
		// (set) Token: 0x0600336A RID: 13162 RVA: 0x000AD5B4 File Offset: 0x000AB7B4
		[Description("Service's Display Name")]
		[Category("General")]
		[ConfigurationProperty("displayName", IsRequired = true)]
		public string DisplayName
		{
			get
			{
				return (string)base["displayName"];
			}
			set
			{
				base["displayName"] = value;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x0600336B RID: 13163 RVA: 0x000978D5 File Offset: 0x00095AD5
		// (set) Token: 0x0600336C RID: 13164 RVA: 0x000978E7 File Offset: 0x00095AE7
		[Description("Service's Description")]
		[Category("General")]
		[ConfigurationProperty("description", IsRequired = true)]
		public string Description
		{
			get
			{
				return (string)base["description"];
			}
			set
			{
				base["description"] = value;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x0600336D RID: 13165 RVA: 0x000AD5C2 File Offset: 0x000AB7C2
		// (set) Token: 0x0600336E RID: 13166 RVA: 0x000AD5D4 File Offset: 0x000AB7D4
		[Description("Service's Executable, short form")]
		[Category("General")]
		[ConfigurationProperty("executable", IsRequired = true)]
		public string Executable
		{
			get
			{
				return (string)base["executable"];
			}
			set
			{
				base["executable"] = value;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x0600336F RID: 13167 RVA: 0x000AD5E2 File Offset: 0x000AB7E2
		// (set) Token: 0x06003370 RID: 13168 RVA: 0x000AD5F4 File Offset: 0x000AB7F4
		[Description("Service Command Line Parameter")]
		[Category("General")]
		[ConfigurationProperty("commandLineParameter", IsRequired = false)]
		public string CommandLineParameter
		{
			get
			{
				return (string)base["commandLineParameter"];
			}
			set
			{
				base["commandLineParameter"] = value;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06003371 RID: 13169 RVA: 0x000AD602 File Offset: 0x000AB802
		// (set) Token: 0x06003372 RID: 13170 RVA: 0x000AD614 File Offset: 0x000AB814
		[Description("Service's Start Type")]
		[Category("General")]
		[ConfigurationProperty("startType", IsRequired = true)]
		public ServiceStartType StartType
		{
			get
			{
				return (ServiceStartType)base["startType"];
			}
			set
			{
				base["startType"] = value;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06003373 RID: 13171 RVA: 0x000AD627 File Offset: 0x000AB827
		// (set) Token: 0x06003374 RID: 13172 RVA: 0x000AD639 File Offset: 0x000AB839
		[Description("Service's Dependencies, separated by ';'")]
		[Category("General")]
		[ConfigurationProperty("dependencies", IsRequired = true)]
		public string Dependencies
		{
			get
			{
				return (string)base["dependencies"];
			}
			set
			{
				base["dependencies"] = value;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06003375 RID: 13173 RVA: 0x000AD647 File Offset: 0x000AB847
		// (set) Token: 0x06003376 RID: 13174 RVA: 0x000AD659 File Offset: 0x000AB859
		[Description("Service's Load Group")]
		[Category("General")]
		[ConfigurationProperty("loadGroup", IsRequired = true)]
		public string LoadGroup
		{
			get
			{
				return (string)base["loadGroup"];
			}
			set
			{
				base["loadGroup"] = value;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06003377 RID: 13175 RVA: 0x000AD667 File Offset: 0x000AB867
		// (set) Token: 0x06003378 RID: 13176 RVA: 0x000AD679 File Offset: 0x000AB879
		[Description("Should ignore startup errors")]
		[Category("General")]
		[ConfigurationProperty("ignoreStartupErrors", IsRequired = true)]
		public bool IgnoreStartupErrors
		{
			get
			{
				return (bool)base["ignoreStartupErrors"];
			}
			set
			{
				base["ignoreStartupErrors"] = value;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x000AD2A6 File Offset: 0x000AB4A6
		// (set) Token: 0x0600337A RID: 13178 RVA: 0x000AD2B8 File Offset: 0x000AB4B8
		[Description("Service Credential")]
		[Category("General")]
		[ConfigurationProperty("serviceCredential", IsRequired = true)]
		public ServiceCredential ServiceCredential
		{
			get
			{
				return (ServiceCredential)base["serviceCredential"];
			}
			set
			{
				base["serviceCredential"] = value;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x000AD286 File Offset: 0x000AB486
		// (set) Token: 0x0600337C RID: 13180 RVA: 0x000AD298 File Offset: 0x000AB498
		[Description("Registry Settings associated with the Service")]
		[Category("General")]
		[ConfigurationProperty("extraRegistrySettings", IsRequired = false)]
		public ExtraRegistrySettingCollection ExtraRegistrySettings
		{
			get
			{
				return (ExtraRegistrySettingCollection)base["extraRegistrySettings"];
			}
			set
			{
				base["extraRegistrySettings"] = value;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x0600337D RID: 13181 RVA: 0x000AD68C File Offset: 0x000AB88C
		// (set) Token: 0x0600337E RID: 13182 RVA: 0x000AD69E File Offset: 0x000AB89E
		[Description("Performance Counter Information")]
		[Category("General")]
		[ConfigurationProperty("externalPerformanceCounterInformation", IsRequired = false)]
		public ExternalPerformanceCounterInformation PerformanceCounterInformation
		{
			get
			{
				return (ExternalPerformanceCounterInformation)base["externalPerformanceCounterInformation"];
			}
			set
			{
				base["externalPerformanceCounterInformation"] = value;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x000AD6AC File Offset: 0x000AB8AC
		// (set) Token: 0x06003380 RID: 13184 RVA: 0x000AD6BE File Offset: 0x000AB8BE
		[Description("Firewall Rule")]
		[Category("General")]
		[ConfigurationProperty("externalFirewallRule", IsRequired = false)]
		public ExternalFirewallRule FirewallRule
		{
			get
			{
				return (ExternalFirewallRule)base["externalFirewallRule"];
			}
			set
			{
				base["externalFirewallRule"] = value;
			}
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000AD6CC File Offset: 0x000AB8CC
		public object GetElementKey()
		{
			return this.Name;
		}
	}
}
