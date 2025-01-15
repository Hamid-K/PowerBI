using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	internal sealed class PowerBIConfiguration : IPowerBIOAuthConfiguration
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003BDC File Offset: 0x00001DDC
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public string ResourceUrl { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003BED File Offset: 0x00001DED
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00003BF5 File Offset: 0x00001DF5
		public string AuthorizationUrl { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003BFE File Offset: 0x00001DFE
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00003C06 File Offset: 0x00001E06
		public string PowerBIEndpoint { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003C0F File Offset: 0x00001E0F
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00003C17 File Offset: 0x00001E17
		public List<string> RedirectUrls { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00003C20 File Offset: 0x00001E20
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00003C28 File Offset: 0x00001E28
		public string TokenUrl { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003C31 File Offset: 0x00001E31
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00003C39 File Offset: 0x00001E39
		public string LogoutUrl { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003C42 File Offset: 0x00001E42
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00003C4A File Offset: 0x00001E4A
		public string ClientId { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00003C53 File Offset: 0x00001E53
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00003C5B File Offset: 0x00001E5B
		public string ClientSecret { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003C64 File Offset: 0x00001E64
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00003C6C File Offset: 0x00001E6C
		public string AppObjectId { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00003C75 File Offset: 0x00001E75
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00003C7D File Offset: 0x00001E7D
		public string TenantName { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003C86 File Offset: 0x00001E86
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00003C8E File Offset: 0x00001E8E
		public string TenantId { get; private set; }

		// Token: 0x060000E7 RID: 231 RVA: 0x00003C98 File Offset: 0x00001E98
		internal void Load(OAuthConnectionConfigurationPropertyBag properties)
		{
			foreach (KeyValuePair<string, ConfigurationPropertyInfo> keyValuePair in properties)
			{
				string key = keyValuePair.Key;
				ConfigurationPropertyInfo value = keyValuePair.Value;
				string text = key.Split(PowerBIConfiguration.m_splitter)[0];
				uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1124663746U)
				{
					if (num <= 211218844U)
					{
						if (num != 160393366U)
						{
							if (num == 211218844U)
							{
								if (text == "LogoutUrl")
								{
									this.LogoutUrl = (string)value.Value;
								}
							}
						}
						else if (text == "AppObjectId")
						{
							this.AppObjectId = (string)value.Value;
						}
					}
					else if (num != 453348411U)
					{
						if (num != 710209030U)
						{
							if (num == 1124663746U)
							{
								if (text == "RedirectUrl")
								{
									if (this.RedirectUrls == null)
									{
										this.RedirectUrls = new List<string>();
									}
									this.RedirectUrls.Add((string)value.Value);
								}
							}
						}
						else if (text == "TenantName")
						{
							this.TenantName = (string)value.Value;
						}
					}
					else if (text == "TokenUrl")
					{
						this.TokenUrl = (string)value.Value;
					}
				}
				else if (num <= 2059514915U)
				{
					if (num != 1574139682U)
					{
						if (num != 1890720815U)
						{
							if (num == 2059514915U)
							{
								if (text == "ClientId")
								{
									this.ClientId = (string)value.Value;
								}
							}
						}
						else if (text == "AuthorizationUrl")
						{
							this.AuthorizationUrl = (string)value.Value;
						}
					}
					else if (text == "PowerBIEndpoint")
					{
						string text2 = (string)value.Value;
						this.PowerBIEndpoint = (string.IsNullOrEmpty(text2) ? "https://api.powerbi.com" : text2);
					}
				}
				else if (num != 2111347446U)
				{
					if (num != 2359032832U)
					{
						if (num == 4116816302U)
						{
							if (text == "ResourceUrl")
							{
								this.ResourceUrl = (string)value.Value;
							}
						}
					}
					else if (text == "TenantId")
					{
						this.TenantId = (string)value.Value;
					}
				}
				else if (text == "ClientSecret")
				{
					this.ClientSecret = (string)value.Value;
				}
			}
		}

		// Token: 0x04000111 RID: 273
		private static readonly char[] m_splitter = new char[] { '_' };
	}
}
