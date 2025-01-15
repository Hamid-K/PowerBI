using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ffp
{
	// Token: 0x02000510 RID: 1296
	public sealed class FfpConfigurationSectionHandler : HisConfigurationSectionHandler
	{
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002BDD RID: 11229 RVA: 0x00096DEB File Offset: 0x00094FEB
		// (set) Token: 0x06002BDE RID: 11230 RVA: 0x00096DFD File Offset: 0x00094FFD
		[Description("XML Namespace of the schema associated with the Flat File Processor configuration file.")]
		[Category("General")]
		[ConfigurationProperty("xmlns", IsRequired = true, DefaultValue = "http://schemas.microsoft.com/his/Config/Ffp/2013")]
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

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x00096E0B File Offset: 0x0009500B
		// (set) Token: 0x06002BE0 RID: 11232 RVA: 0x00096E1D File Offset: 0x0009501D
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
	}
}
