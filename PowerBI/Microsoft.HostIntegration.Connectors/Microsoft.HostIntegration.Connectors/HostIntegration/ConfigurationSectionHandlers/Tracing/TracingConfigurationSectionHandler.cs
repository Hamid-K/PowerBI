using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Tracing
{
	// Token: 0x02000523 RID: 1315
	public sealed class TracingConfigurationSectionHandler : HisConfigurationSectionHandler
	{
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002C75 RID: 11381 RVA: 0x00096DEB File Offset: 0x00094FEB
		// (set) Token: 0x06002C76 RID: 11382 RVA: 0x00096DFD File Offset: 0x00094FFD
		[Description("XML Namespace of the schema associated with the Tracing configuration file.")]
		[Category("General")]
		[ConfigurationProperty("xmlns", IsRequired = true, DefaultValue = "http://schemas.microsoft.com/his/Tracing/2013")]
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

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x0009779C File Offset: 0x0009599C
		// (set) Token: 0x06002C78 RID: 11384 RVA: 0x000977AE File Offset: 0x000959AE
		[ConfigurationProperty("traceOptions", IsRequired = true)]
		public TraceOptions TraceOptions
		{
			get
			{
				return (TraceOptions)base["traceOptions"];
			}
			set
			{
				base["traceOptions"] = value;
			}
		}
	}
}
