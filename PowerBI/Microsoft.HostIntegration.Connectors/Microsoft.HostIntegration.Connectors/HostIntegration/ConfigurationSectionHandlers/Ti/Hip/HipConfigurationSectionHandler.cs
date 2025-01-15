using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000562 RID: 1378
	public sealed class HipConfigurationSectionHandler : HisConfigurationSectionHandler
	{
		// Token: 0x06002E71 RID: 11889 RVA: 0x0009B360 File Offset: 0x00099560
		public HipConfigurationSectionHandler()
		{
			this.GetHipObjectsDelegate = new GetHipObjectCollectionCallbackType(this.GetHipObjectCollection);
			this.GetReadOrderDelegate = new GetReadOrderCallbackType(this.GetReadOrder);
			this.GetTcpHostEnvironmentsDelegate = new GetTcpHostEnvironmentCollectionCallbackType(this.GetTcpHostEnvironmentCollection);
			this.GetSnaHostEnvironmentsDelegate = new GetSnaHostEnvironmentCollectionCallbackType(this.GetSnaHostEnvironmentCollection);
			this.GetEssoSecurityPoliciesDelegate = new GetEssoSecurityPolicyCollectionCallbackType(this.GetEssoSecurityPolicyCollection);
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x0009B3CD File Offset: 0x000995CD
		public string SectionXml
		{
			get
			{
				return "<section name=\"hostIntegration.ti.hip\" type=\"Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip.HipConfigurationSectionHandler, Microsoft.HostIntegration.ConfigurationSectionHandlers, Version=10.0.1000.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" />";
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x0009B3D4 File Offset: 0x000995D4
		public string HostIntegrationTiHipElementXml
		{
			get
			{
				return HipConfigurationUtilities.TiHipElementXml(this, HipGeneratedSchemaType.Full);
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002E74 RID: 11892 RVA: 0x0009B3DD File Offset: 0x000995DD
		public string HostIntegrationTiHipElementInnerXml
		{
			get
			{
				return HipConfigurationUtilities.TiHipElementInnerXml(this, HipGeneratedSchemaType.Full);
			}
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x0009B3E6 File Offset: 0x000995E6
		public static HipConfigurationSectionHandler LoadFromCache(string cacheName, string fileName, string region)
		{
			return HisConfigurationSectionHandler.LoadFromCache(cacheName, fileName, region, "hostIntegration.ti.hip") as HipConfigurationSectionHandler;
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x00096DEB File Offset: 0x00094FEB
		// (set) Token: 0x06002E77 RID: 11895 RVA: 0x00096DFD File Offset: 0x00094FFD
		[Description("XML Namespace of the schema associated with the Windows-Iniated Process configuration file.")]
		[Category("General")]
		[ConfigurationProperty("xmlns", IsRequired = true, DefaultValue = "http://schemas.microsoft.com/his/Config/TiHip/2013")]
		[DisplayName("XML NameSpace")]
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

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x0009B3FA File Offset: 0x000995FA
		// (set) Token: 0x06002E79 RID: 11897 RVA: 0x000972D9 File Offset: 0x000954D9
		[Description("The Configuration Read Order provides a means of defining the order that the TI Runtime obtains it’s HIP configuration information. The values First, Second, Third and Unused can be specified. At least one property must be set to First. Property values cannot be duplicated except for Unused. The TI Runtime attempts to resolve HIP configuration information in the order specified in the AppConfig and Cache properties.")]
		[Category("General")]
		[ConfigurationProperty("readOrder", IsRequired = true)]
		[DisplayName("Read Order")]
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

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x0009B40C File Offset: 0x0009960C
		// (set) Token: 0x06002E7B RID: 11899 RVA: 0x00096FEF File Offset: 0x000951EF
		[Description("The Cache can be used to store configuration information such that any process on any machine can gain access to a single instance of the TI Configuration information. HostName, Port and CacheName provide the details to allow the TI Runtime to obtain the configuration information. Before the TI Configuration Tool or the TI Runtime can use the Cache, an Administrator must define the Cache using the AppFabric utilities.")]
		[Category("General")]
		[ConfigurationProperty("cache", IsRequired = false)]
		[DisplayName("Cache")]
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

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x0009B41E File Offset: 0x0009961E
		// (set) Token: 0x06002E7D RID: 11901 RVA: 0x0009B430 File Offset: 0x00099630
		[Description("ESSO Security Policies represents a collection of security policies to be used with Enterprise Single Signon to translate incoming mainframe credentials to Windows credentials.")]
		[Category("General")]
		[ConfigurationProperty("essoSecurityPolicies", IsRequired = false)]
		[DisplayName("ESSO Security Policies")]
		public EssoSecurityPolicyCollection EssoSecurityPolicies
		{
			get
			{
				return (EssoSecurityPolicyCollection)base["essoSecurityPolicies"];
			}
			set
			{
				base["essoSecurityPolicies"] = value;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002E7E RID: 11902 RVA: 0x0009B43E File Offset: 0x0009963E
		// (set) Token: 0x06002E7F RID: 11903 RVA: 0x0009B450 File Offset: 0x00099650
		[Description("HipObjects represents a collection of .NET Assemblies that will be used by the HIP Runtime to process incoming requests. There must be one or .NET assembly definitions for the configuration to be valid.")]
		[Category("General")]
		[ConfigurationProperty("objects")]
		[DisplayName("Objects")]
		public HipObjectCollection HipObjects
		{
			get
			{
				return (HipObjectCollection)base["objects"];
			}
			set
			{
				base["objects"] = value;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x0009B45E File Offset: 0x0009965E
		// (set) Token: 0x06002E81 RID: 11905 RVA: 0x0009B470 File Offset: 0x00099670
		[Description("The TCP/IP Host Environment defines the unique set of characteristics for a mainframe that interacts with TI HIP using the TCP/IP protocol. There must be one or more TCP/IP host environments definition for the configuration to be valid.")]
		[Category("TCP")]
		[ConfigurationProperty("tcpHostEnvironments", IsRequired = false)]
		[DisplayName("TCP Host Environments")]
		public TcpHostEnvironmentCollection TcpHostEnvironments
		{
			get
			{
				return (TcpHostEnvironmentCollection)base["tcpHostEnvironments"];
			}
			set
			{
				base["tcpHostEnvironments"] = value;
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002E82 RID: 11906 RVA: 0x0009B47E File Offset: 0x0009967E
		// (set) Token: 0x06002E83 RID: 11907 RVA: 0x0009B490 File Offset: 0x00099690
		[Description("The SNA Host Environment defines the unique set of characteristics for a mainframe that interacts with TI HIP using the IBM SNA protocol. There must be one or more SNA host environments definition for the configuration to be valid.")]
		[Category("SNA")]
		[ConfigurationProperty("snaHostEnvironments", IsRequired = false)]
		[DisplayName("SNA Host Environments")]
		public SnaHostEnvironmentCollection SnaHostEnvironments
		{
			get
			{
				return (SnaHostEnvironmentCollection)base["snaHostEnvironments"];
			}
			set
			{
				base["snaHostEnvironments"] = value;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002E84 RID: 11908 RVA: 0x00096E0B File Offset: 0x0009500B
		// (set) Token: 0x06002E85 RID: 11909 RVA: 0x00096E1D File Offset: 0x0009501D
		[Description("Conversion Behavior represents information that the Primitive and Aggreagte converters use to change their execution behaviors.")]
		[Category("General")]
		[ConfigurationProperty("conversionBehavior")]
		[DisplayName("Conversion Behavior")]
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

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002E86 RID: 11910 RVA: 0x0009B49E File Offset: 0x0009969E
		// (set) Token: 0x06002E87 RID: 11911 RVA: 0x0009AB3E File Offset: 0x00098D3E
		[Description("Services represents a collection of services that can execute incoming mainframe requests. There must be one or more HIP Service definitions for the configuration to be valid. Each service is strongly typed for use with specific mainframe configurations.")]
		[Category("General")]
		[ConfigurationProperty("services")]
		[DisplayName("Services")]
		public ServiceCollection Services
		{
			get
			{
				return (ServiceCollection)base["services"];
			}
			set
			{
				base["services"] = value;
			}
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x0009B4B0 File Offset: 0x000996B0
		public HipObjectCollection GetHipObjectCollection()
		{
			return this.HipObjects;
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x0009B4B8 File Offset: 0x000996B8
		public ReadOrder GetReadOrder()
		{
			return this.ReadOrder;
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x0009B4C0 File Offset: 0x000996C0
		public TcpHostEnvironmentCollection GetTcpHostEnvironmentCollection()
		{
			return this.TcpHostEnvironments;
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x0009B4C8 File Offset: 0x000996C8
		public SnaHostEnvironmentCollection GetSnaHostEnvironmentCollection()
		{
			return this.SnaHostEnvironments;
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x0009B4D0 File Offset: 0x000996D0
		public EssoSecurityPolicyCollection GetEssoSecurityPolicyCollection()
		{
			return this.EssoSecurityPolicies;
		}

		// Token: 0x04001C16 RID: 7190
		public GetHipObjectCollectionCallbackType GetHipObjectsDelegate;

		// Token: 0x04001C17 RID: 7191
		public GetReadOrderCallbackType GetReadOrderDelegate;

		// Token: 0x04001C18 RID: 7192
		public GetTcpHostEnvironmentCollectionCallbackType GetTcpHostEnvironmentsDelegate;

		// Token: 0x04001C19 RID: 7193
		public GetSnaHostEnvironmentCollectionCallbackType GetSnaHostEnvironmentsDelegate;

		// Token: 0x04001C1A RID: 7194
		public GetEssoSecurityPolicyCollectionCallbackType GetEssoSecurityPoliciesDelegate;
	}
}
