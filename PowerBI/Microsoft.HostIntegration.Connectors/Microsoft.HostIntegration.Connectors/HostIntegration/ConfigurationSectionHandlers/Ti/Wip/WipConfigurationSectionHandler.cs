using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x020005A9 RID: 1449
	public sealed class WipConfigurationSectionHandler : HisConfigurationSectionHandler
	{
		// Token: 0x060032BD RID: 12989 RVA: 0x000A84E4 File Offset: 0x000A66E4
		public WipConfigurationSectionHandler()
		{
			this.GetRemoteEnvironmentsDelegate = new GetRemoteEnvironmentCollectionCallbackType(this.GetRemoteEnvironmentCollection);
			this.GetReadOrderDelegate = new GetReadOrderCallbackType(this.GetReadOrder);
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060032BE RID: 12990 RVA: 0x000A8510 File Offset: 0x000A6710
		public string SectionXml
		{
			get
			{
				return "<section name=\"hostIntegration.ti.wip\" type=\"Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip.WipConfigurationSectionHandler, Microsoft.HostIntegration.ConfigurationSectionHandlers, Version=10.0.1000.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" />";
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060032BF RID: 12991 RVA: 0x000A8517 File Offset: 0x000A6717
		public string HostIntegrationTiWipElementXml
		{
			get
			{
				return WipConfigurationUtilities.TiWipElementXml(this, WipGeneratedSchemaType.Full);
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x000A8520 File Offset: 0x000A6720
		public string HostIntegrationTiWipElementInnerXml
		{
			get
			{
				return WipConfigurationUtilities.TiWipElementInnerXml(this, WipGeneratedSchemaType.Full);
			}
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x000A8529 File Offset: 0x000A6729
		public static WipConfigurationSectionHandler LoadFromCache(string cacheName, string fileName, string region)
		{
			return HisConfigurationSectionHandler.LoadFromCache(cacheName, fileName, region, "hostIntegration.ti.wip") as WipConfigurationSectionHandler;
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060032C2 RID: 12994 RVA: 0x00096DEB File Offset: 0x00094FEB
		// (set) Token: 0x060032C3 RID: 12995 RVA: 0x00096DFD File Offset: 0x00094FFD
		[Description("XML Namespace of the schema associated with the Windows-Iniated Process configuration file.")]
		[Category("General")]
		[ConfigurationProperty("xmlns", IsRequired = true, DefaultValue = "http://schemas.microsoft.com/his/Config/TiWip/2013")]
		public string XmlNs
		{
			get
			{
				return (string)base["xmlns"];
			}
			set
			{
				base["xmlns"] = value;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000A853D File Offset: 0x000A673D
		// (set) Token: 0x060032C5 RID: 12997 RVA: 0x000972D9 File Offset: 0x000954D9
		[Description("The Configuration Read Order provides a means of defining in what order and where the TI Runtime obtains its Remote Environment, Objects and Behavior configuration information. first, second, third and Unused can be specified. At least on property must be set to First. Property values cannot be duplicated except for Unused. The TI Runtime attempts to resolve Remote Environment configuration information in the order specified in the AppConfig, Cache and Registry properties.")]
		[Category("General")]
		[ConfigurationProperty("readOrder", IsRequired = true)]
		public ReadOrder ReadOrder
		{
			get
			{
				return (ReadOrder)base["readOrder"];
			}
			set
			{
				base["readOrder"] = value;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x0009B40C File Offset: 0x0009960C
		// (set) Token: 0x060032C7 RID: 12999 RVA: 0x00096FEF File Offset: 0x000951EF
		[Description("The AppFabric Cache can be used to store configuration information such that any process on any machine can gain access to a single instance of the TI Configuration information. HostName, Port and CacheName provide the details to allow the TI Runtime to obtain the configuration information. Before the TI Configuration Tool or the TI Runtime can use the Cache, an Administrator must define the Cache using the AppFabric utilities.")]
		[Category("General")]
		[ConfigurationProperty("cache", IsRequired = false)]
		public Cache Cache
		{
			get
			{
				return (Cache)base["cache"];
			}
			set
			{
				base["cache"] = value;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060032C8 RID: 13000 RVA: 0x00096E0B File Offset: 0x0009500B
		// (set) Token: 0x060032C9 RID: 13001 RVA: 0x00096E1D File Offset: 0x0009501D
		[Description("Conversion Behavior represents information that the Primitive and Aggreagte converters use to change their execution behaviors.")]
		[Category("General")]
		[ConfigurationProperty("conversionBehavior", IsRequired = false)]
		public ConversionBehavior ConversionBehavior
		{
			get
			{
				return (ConversionBehavior)base["conversionBehavior"];
			}
			set
			{
				base["conversionBehavior"] = value;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x000A854F File Offset: 0x000A674F
		// (set) Token: 0x060032CB RID: 13003 RVA: 0x000A8561 File Offset: 0x000A6761
		[Description("TI WIP Behavior represents information that TI Runtime uses to change its execution behaviors.")]
		[Category("General")]
		[ConfigurationProperty("tiWipBehavior", IsRequired = false)]
		public TiWipBehavior TiWipBehavior
		{
			get
			{
				return (TiWipBehavior)base["tiWipBehavior"];
			}
			set
			{
				base["tiWipBehavior"] = value;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x000A856F File Offset: 0x000A676F
		// (set) Token: 0x060032CD RID: 13005 RVA: 0x0009B450 File Offset: 0x00099650
		[Description("The WipObjects element represents a collection of TI Assemblies created with HIS Designer. There can be zero or more TI Assembly definitions for the configuration to be valid. Each TI Assembly will be associated with a specific Remote Environment of the same programming model type.")]
		[Category("General")]
		[ConfigurationProperty("objects", IsRequired = false)]
		public WipObjectCollection WipObjects
		{
			get
			{
				return (WipObjectCollection)base["objects"];
			}
			set
			{
				base["objects"] = value;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x000A8581 File Offset: 0x000A6781
		// (set) Token: 0x060032CF RID: 13007 RVA: 0x000A8593 File Offset: 0x000A6793
		[Description("Remote Environments represents a collection of Remote Environments. There must be one or more remote environment definitions for the configuration to be valid. Each remote environment is strongly typed for use with specific mainframe configurations.")]
		[Category("General")]
		[ConfigurationProperty("remoteEnvironments", IsRequired = false)]
		public RemoteEnvironmentCollection RemoteEnvironments
		{
			get
			{
				return (RemoteEnvironmentCollection)base["remoteEnvironments"];
			}
			set
			{
				base["remoteEnvironments"] = value;
			}
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000A85A1 File Offset: 0x000A67A1
		public RemoteEnvironmentCollection GetRemoteEnvironmentCollection()
		{
			return this.RemoteEnvironments;
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000A85A9 File Offset: 0x000A67A9
		public ReadOrder GetReadOrder()
		{
			return this.ReadOrder;
		}

		// Token: 0x04001C79 RID: 7289
		public GetRemoteEnvironmentCollectionCallbackType GetRemoteEnvironmentsDelegate;

		// Token: 0x04001C7A RID: 7290
		public GetReadOrderCallbackType GetReadOrderDelegate;
	}
}
