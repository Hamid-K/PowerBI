using System;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x020005AC RID: 1452
	public sealed class IvpConfigurationSectionHandler : HisConfigurationSectionHandler
	{
		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060032EB RID: 13035 RVA: 0x00096DEB File Offset: 0x00094FEB
		// (set) Token: 0x060032EC RID: 13036 RVA: 0x00096DFD File Offset: 0x00094FFD
		[ConfigurationProperty("xmlns", IsRequired = true, DefaultValue = "http://schemas.microsoft.com/his/Config/TiWipIvp/2013")]
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

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060032ED RID: 13037 RVA: 0x000ACF3A File Offset: 0x000AB13A
		// (set) Token: 0x060032EE RID: 13038 RVA: 0x000ACF4C File Offset: 0x000AB14C
		[ConfigurationProperty("executionParameters", IsRequired = true)]
		public ExecutionParameters ExecutionParameters
		{
			get
			{
				return (ExecutionParameters)base["executionParameters"];
			}
			set
			{
				base["executionParameters"] = value;
			}
		}
	}
}
