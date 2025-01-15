using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005B3 RID: 1459
	public class NetworkIntegration : FeatureWithService
	{
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600330C RID: 13068 RVA: 0x000AD105 File Offset: 0x000AB305
		// (set) Token: 0x0600330D RID: 13069 RVA: 0x000AD117 File Offset: 0x000AB317
		[Description("SubDomain Information and Role")]
		[Category("General")]
		[ConfigurationProperty("subDomain", IsRequired = true)]
		public SubDomain SubDomain
		{
			get
			{
				return (SubDomain)base["subDomain"];
			}
			set
			{
				base["subDomain"] = value;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600330E RID: 13070 RVA: 0x000AD125 File Offset: 0x000AB325
		// (set) Token: 0x0600330F RID: 13071 RVA: 0x000AD137 File Offset: 0x000AB337
		[Description("Client Communication Protocols supported")]
		[Category("General")]
		[ConfigurationProperty("clientProtocolSupport", IsRequired = true)]
		public ClientProtocolSupport ClientProtocolSupport
		{
			get
			{
				return (ClientProtocolSupport)base["clientProtocolSupport"];
			}
			set
			{
				base["clientProtocolSupport"] = value;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000AD145 File Offset: 0x000AB345
		// (set) Token: 0x06003311 RID: 13073 RVA: 0x000AD157 File Offset: 0x000AB357
		[Description("Optional Services")]
		[Category("General")]
		[ConfigurationProperty("optionalServices", IsRequired = true)]
		public OptionalServices OptionalServices
		{
			get
			{
				return (OptionalServices)base["optionalServices"];
			}
			set
			{
				base["optionalServices"] = value;
			}
		}
	}
}
