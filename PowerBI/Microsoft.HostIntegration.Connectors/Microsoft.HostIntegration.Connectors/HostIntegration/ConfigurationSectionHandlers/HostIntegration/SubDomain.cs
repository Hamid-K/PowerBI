using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005C1 RID: 1473
	public class SubDomain : ConfigurationElement
	{
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x000AD2C6 File Offset: 0x000AB4C6
		// (set) Token: 0x06003333 RID: 13107 RVA: 0x000AD2D8 File Offset: 0x000AB4D8
		[ConfigurationProperty("role", IsRequired = true, DefaultValue = "Primary")]
		internal string Role
		{
			get
			{
				return (string)base["role"];
			}
			set
			{
				base["role"] = value;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x000AD2E8 File Offset: 0x000AB4E8
		// (set) Token: 0x06003335 RID: 13109 RVA: 0x000AD338 File Offset: 0x000AB538
		public SubDomainRole SubDomainRole
		{
			get
			{
				string role = this.Role;
				if (role != null)
				{
					if (role == "Primary")
					{
						return SubDomainRole.Primary;
					}
					if (role == "Secondary")
					{
						return SubDomainRole.Secondary;
					}
					if (role == "Remote")
					{
						return SubDomainRole.Remote;
					}
				}
				throw new Exception("BUGBUG: Invalid Role");
			}
			set
			{
				switch (value)
				{
				case SubDomainRole.Primary:
					this.Role = "Primary";
					return;
				case SubDomainRole.Secondary:
					this.Role = "Secondary";
					return;
				case SubDomainRole.Remote:
					this.Role = "Remote";
					return;
				default:
					throw new ArgumentOutOfRangeException("Role");
				}
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000AD387 File Offset: 0x000AB587
		// (set) Token: 0x06003337 RID: 13111 RVA: 0x000AD399 File Offset: 0x000AB599
		[Description("SubDomain Primary Server Name")]
		[Category("General")]
		[ConfigurationProperty("primaryServerName", IsRequired = false)]
		[StringValidator(MaxLength = 15)]
		public string PrimaryServerName
		{
			get
			{
				return (string)base["primaryServerName"];
			}
			set
			{
				base["primaryServerName"] = value;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06003339 RID: 13113 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[Description("SubDomain Name")]
		[Category("General")]
		[ConfigurationProperty("name", IsRequired = false)]
		[StringValidator(MaxLength = 19)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}
	}
}
