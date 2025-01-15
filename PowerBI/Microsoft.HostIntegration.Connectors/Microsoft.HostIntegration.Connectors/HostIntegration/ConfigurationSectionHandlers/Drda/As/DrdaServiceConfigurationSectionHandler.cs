using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000549 RID: 1353
	public sealed class DrdaServiceConfigurationSectionHandler : HisConfigurationSectionHandler
	{
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x00096DEB File Offset: 0x00094FEB
		// (set) Token: 0x06002DA8 RID: 11688 RVA: 0x00096DFD File Offset: 0x00094FFD
		[Description("XML Namespace of the schema associated with the Windows-Iniated Process configuration file.")]
		[Category("General")]
		[ConfigurationProperty("xmlns", IsRequired = true, DefaultValue = "http://schemas.microsoft.com/his/DrdaAs/DrdaService/2013")]
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

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x0009AB2C File Offset: 0x00098D2C
		// (set) Token: 0x06002DAA RID: 11690 RVA: 0x0009AB3E File Offset: 0x00098D3E
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("services", IsRequired = true)]
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
	}
}
