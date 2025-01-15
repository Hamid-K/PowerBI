using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	internal sealed class OAuthConnectionConfiguration : IOAuthConfiguration
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000038B7 File Offset: 0x00001AB7
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000038BF File Offset: 0x00001ABF
		public string AuthorizationUrl { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000038C8 File Offset: 0x00001AC8
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000038D0 File Offset: 0x00001AD0
		public string FederationMetadataUrl { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000038D9 File Offset: 0x00001AD9
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000038E1 File Offset: 0x00001AE1
		public string ClientId { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000038EA File Offset: 0x00001AEA
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000038F2 File Offset: 0x00001AF2
		public string ClientSecret { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000038FB File Offset: 0x00001AFB
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003903 File Offset: 0x00001B03
		public string TenantId { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000390C File Offset: 0x00001B0C
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003914 File Offset: 0x00001B14
		public string TokenUrl { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000391D File Offset: 0x00001B1D
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003925 File Offset: 0x00001B25
		public string ResourceUrl { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000392E File Offset: 0x00001B2E
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00003936 File Offset: 0x00001B36
		public string NativeClientId { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000393F File Offset: 0x00001B3F
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00003947 File Offset: 0x00001B47
		public string GraphUrl { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003950 File Offset: 0x00001B50
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003958 File Offset: 0x00001B58
		public string LogoutUrl { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003961 File Offset: 0x00001B61
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003969 File Offset: 0x00001B69
		public string SessionCookieName
		{
			get
			{
				return this.m_sessionCookieName;
			}
			private set
			{
				this.m_sessionCookieName = value;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003974 File Offset: 0x00001B74
		public IDictionary<string, string> GetProperties(IEnumerable<string> requestedProperties)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			IEnumerable<KeyValuePair<string, ConfigurationPropertyInfo>> properties = this.m_properties;
			Func<KeyValuePair<string, ConfigurationPropertyInfo>, bool> <>9__0;
			Func<KeyValuePair<string, ConfigurationPropertyInfo>, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (KeyValuePair<string, ConfigurationPropertyInfo> p) => requestedProperties.Contains(p.Key));
			}
			foreach (KeyValuePair<string, ConfigurationPropertyInfo> keyValuePair in properties.Where(func))
			{
				dictionary.Add(keyValuePair.Key, (string)keyValuePair.Value.Value);
			}
			return dictionary;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003A14 File Offset: 0x00001C14
		internal void Load(OAuthConnectionConfigurationPropertyBag properties)
		{
			this.m_properties = properties;
			ConfigurationPropertyInfo configurationPropertyInfo = new ConfigurationPropertyInfo();
			if (properties.TryGetValue("OAuthClientId", out configurationPropertyInfo))
			{
				this.ClientId = (string)configurationPropertyInfo.Value;
			}
			if (properties.TryGetValue("OAuthClientSecret", out configurationPropertyInfo))
			{
				this.ClientSecret = (string)configurationPropertyInfo.Value;
			}
			if (properties.TryGetValue("OAuthTenant", out configurationPropertyInfo))
			{
				this.TenantId = (string)configurationPropertyInfo.Value;
			}
			if (properties.TryGetValue("OAuthTokenUrl", out configurationPropertyInfo))
			{
				this.TokenUrl = (string)configurationPropertyInfo.Value;
			}
			if (properties.TryGetValue("OAuthAuthorizationUrl", out configurationPropertyInfo))
			{
				this.AuthorizationUrl = (string)configurationPropertyInfo.Value;
			}
			if (properties.TryGetValue("OAuthFederationMetadataUrl", out configurationPropertyInfo))
			{
				this.FederationMetadataUrl = (string)configurationPropertyInfo.Value;
			}
			if (properties.TryGetValue("OAuthResourceUrl", out configurationPropertyInfo))
			{
				this.ResourceUrl = (string)configurationPropertyInfo.Value;
			}
			if (properties.TryGetValue("OAuthNativeClientId", out configurationPropertyInfo))
			{
				this.NativeClientId = (string)configurationPropertyInfo.Value;
			}
			if (properties.TryGetValue("OAuthGraphUrl", out configurationPropertyInfo))
			{
				this.GraphUrl = (string)configurationPropertyInfo.Value;
			}
			if (properties.TryGetValue("OAuthSessionCookieName", out configurationPropertyInfo))
			{
				this.SessionCookieName = (string)configurationPropertyInfo.Value;
			}
			if (properties.TryGetValue("OAuthLogoutUrl", out configurationPropertyInfo))
			{
				this.LogoutUrl = (string)configurationPropertyInfo.Value;
			}
		}

		// Token: 0x040000FA RID: 250
		private OAuthConnectionConfigurationPropertyBag m_properties;

		// Token: 0x040000FB RID: 251
		private string m_sessionCookieName = "SSRSOAuthSession";
	}
}
