using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using Microsoft.IdentityModel.Json.Linq;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect
{
	// Token: 0x0200000C RID: 12
	public class OpenIdConnectMessage : AuthenticationProtocolMessage
	{
		// Token: 0x0600007D RID: 125 RVA: 0x0000288C File Offset: 0x00000A8C
		public OpenIdConnectMessage()
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000028AC File Offset: 0x00000AAC
		public OpenIdConnectMessage(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw LogHelper.LogArgumentNullException("json");
			}
			try
			{
				this.SetJsonParameters(JObject.Parse(json));
			}
			catch
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX21106: Error in deserializing to json: '{0}'", new object[] { json })));
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002928 File Offset: 0x00000B28
		protected OpenIdConnectMessage(OpenIdConnectMessage other)
		{
			if (other == null)
			{
				throw LogHelper.LogArgumentNullException("other");
			}
			foreach (KeyValuePair<string, string> keyValuePair in other.Parameters)
			{
				base.SetParameter(keyValuePair.Key, keyValuePair.Value);
			}
			this.AuthorizationEndpoint = other.AuthorizationEndpoint;
			base.IssuerAddress = other.IssuerAddress;
			this.RequestType = other.RequestType;
			this.TokenEndpoint = other.TokenEndpoint;
			this.EnableTelemetryParameters = other.EnableTelemetryParameters;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000029E8 File Offset: 0x00000BE8
		public OpenIdConnectMessage(NameValueCollection nameValueCollection)
		{
			if (nameValueCollection == null)
			{
				throw LogHelper.LogArgumentNullException("nameValueCollection");
			}
			foreach (string text in nameValueCollection.AllKeys)
			{
				if (text != null)
				{
					base.SetParameter(text, nameValueCollection[text]);
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002A4C File Offset: 0x00000C4C
		public OpenIdConnectMessage(IEnumerable<KeyValuePair<string, string[]>> parameters)
		{
			if (parameters == null)
			{
				throw LogHelper.LogArgumentNullException("parameters");
			}
			foreach (KeyValuePair<string, string[]> keyValuePair in parameters)
			{
				if (keyValuePair.Value != null && !string.IsNullOrWhiteSpace(keyValuePair.Key))
				{
					foreach (string text in keyValuePair.Value)
					{
						if (text != null)
						{
							base.SetParameter(keyValuePair.Key, text);
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002B00 File Offset: 0x00000D00
		[Obsolete("The 'OpenIdConnectMessage(object json)' constructor is obsolete. Please use 'OpenIdConnectMessage(string json)' instead.")]
		public OpenIdConnectMessage(object json)
		{
			if (json == null)
			{
				throw LogHelper.LogArgumentNullException("json");
			}
			JObject jobject = JObject.Parse(json.ToString());
			this.SetJsonParameters(jobject);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002B4C File Offset: 0x00000D4C
		private void SetJsonParameters(JObject json)
		{
			if (json == null)
			{
				throw LogHelper.LogArgumentNullException("json");
			}
			foreach (KeyValuePair<string, JToken> keyValuePair in json)
			{
				JToken jtoken;
				if (json.TryGetValue(keyValuePair.Key, out jtoken))
				{
					base.SetParameter(keyValuePair.Key, jtoken.ToString());
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public virtual OpenIdConnectMessage Clone()
		{
			return new OpenIdConnectMessage(this);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public virtual string CreateAuthenticationRequestUrl()
		{
			OpenIdConnectMessage openIdConnectMessage = this.Clone();
			openIdConnectMessage.RequestType = OpenIdConnectRequestType.Authentication;
			this.EnsureTelemetryValues(openIdConnectMessage);
			return openIdConnectMessage.BuildRedirectUrl();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public virtual string CreateLogoutRequestUrl()
		{
			OpenIdConnectMessage openIdConnectMessage = this.Clone();
			openIdConnectMessage.RequestType = OpenIdConnectRequestType.Logout;
			this.EnsureTelemetryValues(openIdConnectMessage);
			return openIdConnectMessage.BuildRedirectUrl();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002C18 File Offset: 0x00000E18
		private void EnsureTelemetryValues(OpenIdConnectMessage clonedMessage)
		{
			if (this.EnableTelemetryParameters)
			{
				clonedMessage.SetParameter("x-client-SKU", this.SkuTelemetryValue);
				clonedMessage.SetParameter("x-client-ver", typeof(OpenIdConnectMessage).GetTypeInfo().Assembly.GetName().Version.ToString());
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002C6C File Offset: 0x00000E6C
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002C74 File Offset: 0x00000E74
		public string AuthorizationEndpoint { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002C7D File Offset: 0x00000E7D
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002C8A File Offset: 0x00000E8A
		public string AccessToken
		{
			get
			{
				return this.GetParameter("access_token");
			}
			set
			{
				base.SetParameter("access_token", value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002C98 File Offset: 0x00000E98
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002CA5 File Offset: 0x00000EA5
		public string AcrValues
		{
			get
			{
				return this.GetParameter("acr_values");
			}
			set
			{
				base.SetParameter("acr_values", value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002CB3 File Offset: 0x00000EB3
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public string ClaimsLocales
		{
			get
			{
				return this.GetParameter("claims_locales");
			}
			set
			{
				base.SetParameter("claims_locales", value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002CCE File Offset: 0x00000ECE
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002CDB File Offset: 0x00000EDB
		public string ClientAssertion
		{
			get
			{
				return this.GetParameter("client_assertion");
			}
			set
			{
				base.SetParameter("client_assertion", value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002CE9 File Offset: 0x00000EE9
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00002CF6 File Offset: 0x00000EF6
		public string ClientAssertionType
		{
			get
			{
				return this.GetParameter("client_assertion_type");
			}
			set
			{
				base.SetParameter("client_assertion_type", value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002D04 File Offset: 0x00000F04
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00002D11 File Offset: 0x00000F11
		public string ClientId
		{
			get
			{
				return this.GetParameter("client_id");
			}
			set
			{
				base.SetParameter("client_id", value);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002D1F File Offset: 0x00000F1F
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00002D2C File Offset: 0x00000F2C
		public string ClientSecret
		{
			get
			{
				return this.GetParameter("client_secret");
			}
			set
			{
				base.SetParameter("client_secret", value);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002D3A File Offset: 0x00000F3A
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00002D47 File Offset: 0x00000F47
		public string Code
		{
			get
			{
				return this.GetParameter("code");
			}
			set
			{
				base.SetParameter("code", value);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002D55 File Offset: 0x00000F55
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00002D62 File Offset: 0x00000F62
		public string Display
		{
			get
			{
				return this.GetParameter("display");
			}
			set
			{
				base.SetParameter("display", value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002D70 File Offset: 0x00000F70
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00002D7D File Offset: 0x00000F7D
		public string DomainHint
		{
			get
			{
				return this.GetParameter("domain_hint");
			}
			set
			{
				base.SetParameter("domain_hint", value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002D8B File Offset: 0x00000F8B
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00002D93 File Offset: 0x00000F93
		public bool EnableTelemetryParameters { get; set; } = OpenIdConnectMessage.EnableTelemetryParametersByDefault;

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002D9C File Offset: 0x00000F9C
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00002DA3 File Offset: 0x00000FA3
		public static bool EnableTelemetryParametersByDefault { get; set; } = true;

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002DAB File Offset: 0x00000FAB
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public string Error
		{
			get
			{
				return this.GetParameter("error");
			}
			set
			{
				base.SetParameter("error", value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002DC6 File Offset: 0x00000FC6
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00002DD3 File Offset: 0x00000FD3
		public string ErrorDescription
		{
			get
			{
				return this.GetParameter("error_description");
			}
			set
			{
				base.SetParameter("error_description", value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002DE1 File Offset: 0x00000FE1
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00002DEE File Offset: 0x00000FEE
		public string ErrorUri
		{
			get
			{
				return this.GetParameter("error_uri");
			}
			set
			{
				base.SetParameter("error_uri", value);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00002DFC File Offset: 0x00000FFC
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00002E09 File Offset: 0x00001009
		public string ExpiresIn
		{
			get
			{
				return this.GetParameter("expires_in");
			}
			set
			{
				base.SetParameter("expires_in", value);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00002E17 File Offset: 0x00001017
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00002E24 File Offset: 0x00001024
		public string GrantType
		{
			get
			{
				return this.GetParameter("grant_type");
			}
			set
			{
				base.SetParameter("grant_type", value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002E32 File Offset: 0x00001032
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00002E3F File Offset: 0x0000103F
		public string IdToken
		{
			get
			{
				return this.GetParameter("id_token");
			}
			set
			{
				base.SetParameter("id_token", value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00002E4D File Offset: 0x0000104D
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00002E5A File Offset: 0x0000105A
		public string IdTokenHint
		{
			get
			{
				return this.GetParameter("id_token_hint");
			}
			set
			{
				base.SetParameter("id_token_hint", value);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00002E68 File Offset: 0x00001068
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00002E75 File Offset: 0x00001075
		public string IdentityProvider
		{
			get
			{
				return this.GetParameter("identity_provider");
			}
			set
			{
				base.SetParameter("identity_provider", value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002E83 File Offset: 0x00001083
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00002E90 File Offset: 0x00001090
		public string Iss
		{
			get
			{
				return this.GetParameter("iss");
			}
			set
			{
				base.SetParameter("iss", value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002E9E File Offset: 0x0000109E
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00002EAB File Offset: 0x000010AB
		public string LoginHint
		{
			get
			{
				return this.GetParameter("login_hint");
			}
			set
			{
				base.SetParameter("login_hint", value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002EB9 File Offset: 0x000010B9
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00002EC6 File Offset: 0x000010C6
		public string MaxAge
		{
			get
			{
				return this.GetParameter("max_age");
			}
			set
			{
				base.SetParameter("max_age", value);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002ED4 File Offset: 0x000010D4
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00002EE1 File Offset: 0x000010E1
		public string Nonce
		{
			get
			{
				return this.GetParameter("nonce");
			}
			set
			{
				base.SetParameter("nonce", value);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002EEF File Offset: 0x000010EF
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00002EFC File Offset: 0x000010FC
		public string Password
		{
			get
			{
				return this.GetParameter("password");
			}
			set
			{
				base.SetParameter("password", value);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00002F0A File Offset: 0x0000110A
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00002F17 File Offset: 0x00001117
		public string PostLogoutRedirectUri
		{
			get
			{
				return this.GetParameter("post_logout_redirect_uri");
			}
			set
			{
				base.SetParameter("post_logout_redirect_uri", value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002F25 File Offset: 0x00001125
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00002F32 File Offset: 0x00001132
		public string Prompt
		{
			get
			{
				return this.GetParameter("prompt");
			}
			set
			{
				base.SetParameter("prompt", value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002F40 File Offset: 0x00001140
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00002F4D File Offset: 0x0000114D
		public string RedirectUri
		{
			get
			{
				return this.GetParameter("redirect_uri");
			}
			set
			{
				base.SetParameter("redirect_uri", value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002F5B File Offset: 0x0000115B
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00002F68 File Offset: 0x00001168
		public string RefreshToken
		{
			get
			{
				return this.GetParameter("refresh_token");
			}
			set
			{
				base.SetParameter("refresh_token", value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002F76 File Offset: 0x00001176
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00002F7E File Offset: 0x0000117E
		public OpenIdConnectRequestType RequestType { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002F87 File Offset: 0x00001187
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00002F94 File Offset: 0x00001194
		public string RequestUri
		{
			get
			{
				return this.GetParameter("request_uri");
			}
			set
			{
				base.SetParameter("request_uri", value);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00002FA2 File Offset: 0x000011A2
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00002FAF File Offset: 0x000011AF
		public string ResponseMode
		{
			get
			{
				return this.GetParameter("response_mode");
			}
			set
			{
				base.SetParameter("response_mode", value);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00002FBD File Offset: 0x000011BD
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00002FCA File Offset: 0x000011CA
		public string ResponseType
		{
			get
			{
				return this.GetParameter("response_type");
			}
			set
			{
				base.SetParameter("response_type", value);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00002FD8 File Offset: 0x000011D8
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00002FE5 File Offset: 0x000011E5
		public string Resource
		{
			get
			{
				return this.GetParameter("resource");
			}
			set
			{
				base.SetParameter("resource", value);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00002FF3 File Offset: 0x000011F3
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003000 File Offset: 0x00001200
		public string Scope
		{
			get
			{
				return this.GetParameter("scope");
			}
			set
			{
				base.SetParameter("scope", value);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000300E File Offset: 0x0000120E
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000301B File Offset: 0x0000121B
		public string SessionState
		{
			get
			{
				return this.GetParameter("session_state");
			}
			set
			{
				base.SetParameter("session_state", value);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003029 File Offset: 0x00001229
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00003036 File Offset: 0x00001236
		public string Sid
		{
			get
			{
				return this.GetParameter("sid");
			}
			set
			{
				base.SetParameter("sid", value);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003044 File Offset: 0x00001244
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x0000304C File Offset: 0x0000124C
		public string SkuTelemetryValue { get; set; } = IdentityModelTelemetryUtil.ClientSku;

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003055 File Offset: 0x00001255
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003062 File Offset: 0x00001262
		public string State
		{
			get
			{
				return this.GetParameter("state");
			}
			set
			{
				base.SetParameter("state", value);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003070 File Offset: 0x00001270
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x0000307D File Offset: 0x0000127D
		public string TargetLinkUri
		{
			get
			{
				return this.GetParameter("target_link_uri");
			}
			set
			{
				base.SetParameter("target_link_uri", value);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000308B File Offset: 0x0000128B
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003093 File Offset: 0x00001293
		public string TokenEndpoint { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000309C File Offset: 0x0000129C
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000030A9 File Offset: 0x000012A9
		public string TokenType
		{
			get
			{
				return this.GetParameter("token_type");
			}
			set
			{
				base.SetParameter("token_type", value);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000030B7 File Offset: 0x000012B7
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000030C4 File Offset: 0x000012C4
		public string UiLocales
		{
			get
			{
				return this.GetParameter("ui_locales");
			}
			set
			{
				base.SetParameter("ui_locales", value);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000030D2 File Offset: 0x000012D2
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000030DF File Offset: 0x000012DF
		public string UserId
		{
			get
			{
				return this.GetParameter("user_id");
			}
			set
			{
				base.SetParameter("user_id", value);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000030ED File Offset: 0x000012ED
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x000030FA File Offset: 0x000012FA
		public string Username
		{
			get
			{
				return this.GetParameter("username");
			}
			set
			{
				base.SetParameter("username", value);
			}
		}
	}
}
