using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005D0 RID: 1488
	public class ClientNetworkIntegration : Feature
	{
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x060033C4 RID: 13252 RVA: 0x000AD9DD File Offset: 0x000ABBDD
		// (set) Token: 0x060033C5 RID: 13253 RVA: 0x000AD9EF File Offset: 0x000ABBEF
		[Description("Sponsors")]
		[Category("General")]
		[ConfigurationProperty("sponsors", IsRequired = true)]
		public Sponsors Sponsors
		{
			get
			{
				return (Sponsors)base["sponsors"];
			}
			set
			{
				base["sponsors"] = value;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x000AD9FD File Offset: 0x000ABBFD
		// (set) Token: 0x060033C7 RID: 13255 RVA: 0x000ADA0F File Offset: 0x000ABC0F
		[Description("Client Transports")]
		[Category("General")]
		[ConfigurationProperty("transports", IsRequired = true)]
		public Transports Transports
		{
			get
			{
				return (Transports)base["transports"];
			}
			set
			{
				base["transports"] = value;
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x060033C8 RID: 13256 RVA: 0x000ADA1D File Offset: 0x000ABC1D
		// (set) Token: 0x060033C9 RID: 13257 RVA: 0x000ADA2F File Offset: 0x000ABC2F
		[Description("Authentication")]
		[Category("General")]
		[ConfigurationProperty("authentication", IsRequired = true)]
		public Authentication Authentication
		{
			get
			{
				return (Authentication)base["authentication"];
			}
			set
			{
				base["authentication"] = value;
			}
		}
	}
}
