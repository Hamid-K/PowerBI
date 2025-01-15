using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding
{
	// Token: 0x0200052A RID: 1322
	public sealed class EncodingConfigurationSectionHandler : HisConfigurationSectionHandler
	{
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x00096DEB File Offset: 0x00094FEB
		// (set) Token: 0x06002CB9 RID: 11449 RVA: 0x00096DFD File Offset: 0x00094FFD
		[Description("XML Namespace of the schema associated with the Windows-Iniated Process configuration file.")]
		[Category("General")]
		[ConfigurationProperty("xmlns", IsRequired = true, DefaultValue = "http://schemas.microsoft.com/his/Encoding/2013")]
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

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x00097A8A File Offset: 0x00095C8A
		// (set) Token: 0x06002CBB RID: 11451 RVA: 0x00097A9C File Offset: 0x00095C9C
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("codePages", IsRequired = false)]
		public CodePageCollection CodePages
		{
			get
			{
				return (CodePageCollection)base["codePages"];
			}
			set
			{
				base["codePages"] = value;
			}
		}
	}
}
