using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.HostIntegration
{
	// Token: 0x020005D4 RID: 1492
	public class Sponsors : ConfigurationElement
	{
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060033D7 RID: 13271 RVA: 0x000ADADE File Offset: 0x000ABCDE
		// (set) Token: 0x060033D8 RID: 13272 RVA: 0x000ADAF0 File Offset: 0x000ABCF0
		[ConfigurationProperty("choice", IsRequired = true, DefaultValue = "Name")]
		internal string Choice
		{
			get
			{
				return (string)base["choice"];
			}
			set
			{
				base["choice"] = value;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060033D9 RID: 13273 RVA: 0x000ADB00 File Offset: 0x000ABD00
		// (set) Token: 0x060033DA RID: 13274 RVA: 0x000ADB41 File Offset: 0x000ABD41
		public SponsorChoice SponsorChoice
		{
			get
			{
				string choice = this.Choice;
				if (choice != null)
				{
					if (choice == "Name")
					{
						return SponsorChoice.Name;
					}
					if (choice == "ActiveDirectory")
					{
						return SponsorChoice.ActiveDirectory;
					}
				}
				throw new Exception("BUGBUG: Invalid SponsorChoice");
			}
			set
			{
				if (value == SponsorChoice.Name)
				{
					this.Choice = "Name";
					return;
				}
				if (value != SponsorChoice.ActiveDirectory)
				{
					throw new ArgumentOutOfRangeException("SponsorChoice");
				}
				this.Choice = "ActiveDirectory";
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x000ADB6E File Offset: 0x000ABD6E
		// (set) Token: 0x060033DC RID: 13276 RVA: 0x000AD117 File Offset: 0x000AB317
		[Description("SubDomain with sponsors retrieved by Active Directory")]
		[Category("General")]
		[ConfigurationProperty("subDomain", IsRequired = false)]
		public string SubDomain
		{
			get
			{
				return (string)base["subDomain"];
			}
			set
			{
				base["subDomain"] = value;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060033DD RID: 13277 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x060033DE RID: 13278 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[Description("Sponsor Name")]
		[Category("General")]
		[ConfigurationProperty("name", IsRequired = false)]
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

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060033DF RID: 13279 RVA: 0x000ADB80 File Offset: 0x000ABD80
		// (set) Token: 0x060033E0 RID: 13280 RVA: 0x000ADB92 File Offset: 0x000ABD92
		[Description("Sponsor Behavior")]
		[Category("General")]
		[ConfigurationProperty("behavior", IsRequired = true)]
		public SponsorBehavior Behavior
		{
			get
			{
				return (SponsorBehavior)base["behavior"];
			}
			set
			{
				base["behavior"] = value;
			}
		}
	}
}
