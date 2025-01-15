using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005C5 RID: 1477
	public class VsIntegration : Feature
	{
		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x000AD4F4 File Offset: 0x000AB6F4
		// (set) Token: 0x06003355 RID: 13141 RVA: 0x000AD506 File Offset: 0x000AB706
		[Description("Visual Studio Versions to which Schemas and Designer Projects are registered")]
		[Category("General")]
		[ConfigurationProperty("vsVersions", IsRequired = false)]
		public VsVersionCollection VsVersions
		{
			get
			{
				return (VsVersionCollection)base["vsVersions"];
			}
			set
			{
				base["vsVersions"] = value;
			}
		}
	}
}
