using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x0200054D RID: 1357
	public class SecurityManager : ConfigurationElement
	{
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x0002038A File Offset: 0x0001E58A
		// (set) Token: 0x06002DCE RID: 11726 RVA: 0x0002039C File Offset: 0x0001E59C
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("type", IsRequired = true)]
		public string Type
		{
			get
			{
				return (string)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x0009AC96 File Offset: 0x00098E96
		// (set) Token: 0x06002DD0 RID: 11728 RVA: 0x0009ACA8 File Offset: 0x00098EA8
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("mappedAuthenticationDomain", IsRequired = false)]
		public string MappedAuthenticationDomain
		{
			get
			{
				return (string)base["mappedAuthenticationDomain"];
			}
			set
			{
				base["mappedAuthenticationDomain"] = value;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002DD1 RID: 11729 RVA: 0x0009ACB6 File Offset: 0x00098EB6
		// (set) Token: 0x06002DD2 RID: 11730 RVA: 0x0009ACC8 File Offset: 0x00098EC8
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("securityTokenTimeout", IsRequired = false, DefaultValue = "P1D")]
		public string SecurityTokenTimeoutString
		{
			get
			{
				return (string)base["securityTokenTimeout"];
			}
			set
			{
				base["securityTokenTimeout"] = value;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06002DD3 RID: 11731 RVA: 0x0009ACD6 File Offset: 0x00098ED6
		// (set) Token: 0x06002DD4 RID: 11732 RVA: 0x0009ACE8 File Offset: 0x00098EE8
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("authenticationLookupTimeout", IsRequired = false, DefaultValue = "PT30S")]
		public string AuthenticationLookupTimeoutString
		{
			get
			{
				return (string)base["authenticationLookupTimeout"];
			}
			set
			{
				base["authenticationLookupTimeout"] = value;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002DD5 RID: 11733 RVA: 0x0009ACF6 File Offset: 0x00098EF6
		// (set) Token: 0x06002DD6 RID: 11734 RVA: 0x0009AD08 File Offset: 0x00098F08
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("authenticationLookupRetries", IsRequired = false, DefaultValue = 3)]
		public int AuthenticationLookupRetries
		{
			get
			{
				return (int)base["authenticationLookupRetries"];
			}
			set
			{
				base["authenticationLookupRetries"] = value;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002DD7 RID: 11735 RVA: 0x0009AD1B File Offset: 0x00098F1B
		// (set) Token: 0x06002DD8 RID: 11736 RVA: 0x0009AD28 File Offset: 0x00098F28
		public TimeSpan SecurityTokenTimeout
		{
			get
			{
				return SoapDuration.Parse(this.SecurityTokenTimeoutString);
			}
			set
			{
				this.SecurityTokenTimeoutString = SoapDuration.ToString(value);
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002DD9 RID: 11737 RVA: 0x0009AD36 File Offset: 0x00098F36
		// (set) Token: 0x06002DDA RID: 11738 RVA: 0x0009AD43 File Offset: 0x00098F43
		public TimeSpan AuthenticationLookupTimeout
		{
			get
			{
				return SoapDuration.Parse(this.AuthenticationLookupTimeoutString);
			}
			set
			{
				this.AuthenticationLookupTimeoutString = SoapDuration.ToString(value);
			}
		}
	}
}
