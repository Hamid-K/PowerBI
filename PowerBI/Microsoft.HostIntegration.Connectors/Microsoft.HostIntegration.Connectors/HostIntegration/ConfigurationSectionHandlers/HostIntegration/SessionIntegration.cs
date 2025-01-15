using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005B5 RID: 1461
	public class SessionIntegration : FeatureWithService
	{
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x000AD16D File Offset: 0x000AB36D
		// (set) Token: 0x06003315 RID: 13077 RVA: 0x000AD17F File Offset: 0x000AB37F
		[Description("Managed TN3270 Only")]
		[Category("General")]
		[ConfigurationProperty("managedTn3270Only", IsRequired = false, DefaultValue = "false")]
		public bool ManagedTn3270Only
		{
			get
			{
				return (bool)base["managedTn3270Only"];
			}
			set
			{
				base["managedTn3270Only"] = value;
			}
		}
	}
}
