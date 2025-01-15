using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005B7 RID: 1463
	public sealed class HostIntegrationConfigurationSectionHandler : HisConfigurationSectionHandler
	{
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x00096DEB File Offset: 0x00094FEB
		// (set) Token: 0x0600331B RID: 13083 RVA: 0x00096DFD File Offset: 0x00094FFD
		[Description("XML Namespace of the schema associated with the Host Integration configuration file.")]
		[Category("General")]
		[ConfigurationProperty("xmlns", IsRequired = true, DefaultValue = "http://schemas.microsoft.com/his/2015")]
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

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x000AD1B7 File Offset: 0x000AB3B7
		// (set) Token: 0x0600331D RID: 13085 RVA: 0x000AD1C9 File Offset: 0x000AB3C9
		[Description("Configuration is for Server")]
		[Category("General")]
		[ConfigurationProperty("isForServer", IsRequired = true, DefaultValue = "true")]
		public bool IsForServer
		{
			get
			{
				return (bool)base["isForServer"];
			}
			set
			{
				base["isForServer"] = value;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600331E RID: 13086 RVA: 0x000AD1DC File Offset: 0x000AB3DC
		// (set) Token: 0x0600331F RID: 13087 RVA: 0x000AD1EE File Offset: 0x000AB3EE
		[Description("Configuration is Complete - it includes External Services and Registry Settings")]
		[Category("General")]
		[ConfigurationProperty("isComplete", IsRequired = false, DefaultValue = "false")]
		public bool IsComplete
		{
			get
			{
				return (bool)base["isComplete"];
			}
			set
			{
				base["isComplete"] = value;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x000AD201 File Offset: 0x000AB401
		// (set) Token: 0x06003321 RID: 13089 RVA: 0x000AD213 File Offset: 0x000AB413
		[Description("Server Configuration")]
		[Category("General")]
		[ConfigurationProperty("serverConfiguration", IsRequired = false)]
		public ServerConfiguration ServerConfiguration
		{
			get
			{
				return (ServerConfiguration)base["serverConfiguration"];
			}
			set
			{
				base["serverConfiguration"] = value;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x000AD221 File Offset: 0x000AB421
		// (set) Token: 0x06003323 RID: 13091 RVA: 0x000AD233 File Offset: 0x000AB433
		[Description("Client Configuration")]
		[Category("General")]
		[ConfigurationProperty("clientConfiguration", IsRequired = false)]
		public ClientConfiguration ClientConfiguration
		{
			get
			{
				return (ClientConfiguration)base["clientConfiguration"];
			}
			set
			{
				base["clientConfiguration"] = value;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06003324 RID: 13092 RVA: 0x000AD241 File Offset: 0x000AB441
		// (set) Token: 0x06003325 RID: 13093 RVA: 0x000AD253 File Offset: 0x000AB453
		[Description("Version of the Configuration")]
		[Category("General")]
		[ConfigurationProperty("savedConfigurationVersion", IsRequired = false, DefaultValue = "1")]
		public int SavedConfigurationVersion
		{
			get
			{
				return (int)base["savedConfigurationVersion"];
			}
			set
			{
				base["savedConfigurationVersion"] = value;
			}
		}
	}
}
