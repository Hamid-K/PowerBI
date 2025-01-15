using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005D1 RID: 1489
	public class ClientPrintServer : FeatureWithService
	{
		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060033CB RID: 13259 RVA: 0x000ADA3D File Offset: 0x000ABC3D
		// (set) Token: 0x060033CC RID: 13260 RVA: 0x000ADA4F File Offset: 0x000ABC4F
		[Description("Local COM.CFG Path")]
		[Category("General")]
		[ConfigurationProperty("localConfigurationFile", IsRequired = true)]
		public string LocalConfigurationFile
		{
			get
			{
				return (string)base["localConfigurationFile"];
			}
			set
			{
				base["localConfigurationFile"] = value;
			}
		}
	}
}
