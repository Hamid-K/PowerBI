using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005BF RID: 1471
	public class FeatureWithService : Feature
	{
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x000AD2A6 File Offset: 0x000AB4A6
		// (set) Token: 0x06003330 RID: 13104 RVA: 0x000AD2B8 File Offset: 0x000AB4B8
		[Description("Service Credential")]
		[Category("General")]
		[ConfigurationProperty("serviceCredential", IsRequired = false)]
		public ServiceCredential ServiceCredential
		{
			get
			{
				return (ServiceCredential)base["serviceCredential"];
			}
			set
			{
				base["serviceCredential"] = value;
			}
		}
	}
}
