using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.IdentityModel.Json;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect
{
	// Token: 0x02000003 RID: 3
	[JsonObject]
	public class OpenIdConnectConfiguration : BaseConfiguration
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static OpenIdConnectConfiguration Create(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw LogHelper.LogArgumentNullException("json");
			}
			LogHelper.LogVerbose("IDX21808: Deserializing json into OpenIdConnectConfiguration object: '{0}'.", new object[] { json });
			return new OpenIdConnectConfiguration(json);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000207F File Offset: 0x0000027F
		public static string Write(OpenIdConnectConfiguration configuration)
		{
			if (configuration == null)
			{
				throw LogHelper.LogArgumentNullException("configuration");
			}
			LogHelper.LogVerbose("IDX21809: Serializing OpenIdConfiguration object to json string.", Array.Empty<object>());
			return JsonConvert.SerializeObject(configuration);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A4 File Offset: 0x000002A4
		public OpenIdConnectConfiguration()
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021D8 File Offset: 0x000003D8
		public OpenIdConnectConfiguration(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw LogHelper.LogArgumentNullException("json");
			}
			try
			{
				LogHelper.LogVerbose("IDX21806: Deserializing json string into json web keys.", new object[]
				{
					json,
					LogHelper.MarkAsNonPII("Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration")
				});
				JsonConvert.PopulateObject(json, this);
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX21815: Error deserializing json: '{0}' into '{1}'.", new object[]
				{
					json,
					LogHelper.MarkAsNonPII("Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration")
				}), ex));
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002388 File Offset: 0x00000588
		[JsonExtensionData]
		public virtual IDictionary<string, object> AdditionalData { get; } = new Dictionary<string, object>();

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002390 File Offset: 0x00000590
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "acr_values_supported", Required = Required.Default)]
		public ICollection<string> AcrValuesSupported { get; } = new Collection<string>();

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002398 File Offset: 0x00000598
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000023A0 File Offset: 0x000005A0
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "authorization_endpoint", Required = Required.Default)]
		public string AuthorizationEndpoint { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000023A9 File Offset: 0x000005A9
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000023B1 File Offset: 0x000005B1
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "check_session_iframe", Required = Required.Default)]
		public string CheckSessionIframe { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000023BA File Offset: 0x000005BA
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "claims_supported", Required = Required.Default)]
		public ICollection<string> ClaimsSupported { get; } = new Collection<string>();

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000023C2 File Offset: 0x000005C2
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "claims_locales_supported", Required = Required.Default)]
		public ICollection<string> ClaimsLocalesSupported { get; } = new Collection<string>();

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000023CA File Offset: 0x000005CA
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000023D2 File Offset: 0x000005D2
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "claims_parameter_supported", Required = Required.Default)]
		public bool ClaimsParameterSupported { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000023DB File Offset: 0x000005DB
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "claim_types_supported", Required = Required.Default)]
		public ICollection<string> ClaimTypesSupported { get; } = new Collection<string>();

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000023E3 File Offset: 0x000005E3
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "display_values_supported", Required = Required.Default)]
		public ICollection<string> DisplayValuesSupported { get; } = new Collection<string>();

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000023EB File Offset: 0x000005EB
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000023F3 File Offset: 0x000005F3
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "end_session_endpoint", Required = Required.Default)]
		public string EndSessionEndpoint { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000023FC File Offset: 0x000005FC
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002404 File Offset: 0x00000604
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "frontchannel_logout_session_supported", Required = Required.Default)]
		public string FrontchannelLogoutSessionSupported { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000240D File Offset: 0x0000060D
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002415 File Offset: 0x00000615
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "frontchannel_logout_supported", Required = Required.Default)]
		public string FrontchannelLogoutSupported { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000241E File Offset: 0x0000061E
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "grant_types_supported", Required = Required.Default)]
		public ICollection<string> GrantTypesSupported { get; } = new Collection<string>();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002426 File Offset: 0x00000626
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000242E File Offset: 0x0000062E
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "http_logout_supported", Required = Required.Default)]
		public bool HttpLogoutSupported { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002437 File Offset: 0x00000637
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "id_token_encryption_alg_values_supported", Required = Required.Default)]
		public ICollection<string> IdTokenEncryptionAlgValuesSupported { get; } = new Collection<string>();

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000243F File Offset: 0x0000063F
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "id_token_encryption_enc_values_supported", Required = Required.Default)]
		public ICollection<string> IdTokenEncryptionEncValuesSupported { get; } = new Collection<string>();

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002447 File Offset: 0x00000647
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "id_token_signing_alg_values_supported", Required = Required.Default)]
		public ICollection<string> IdTokenSigningAlgValuesSupported { get; } = new Collection<string>();

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000244F File Offset: 0x0000064F
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002457 File Offset: 0x00000657
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "introspection_endpoint", Required = Required.Default)]
		public string IntrospectionEndpoint { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002460 File Offset: 0x00000660
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "introspection_endpoint_auth_methods_supported", Required = Required.Default)]
		public ICollection<string> IntrospectionEndpointAuthMethodsSupported { get; } = new Collection<string>();

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002468 File Offset: 0x00000668
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "introspection_endpoint_auth_signing_alg_values_supported", Required = Required.Default)]
		public ICollection<string> IntrospectionEndpointAuthSigningAlgValuesSupported { get; } = new Collection<string>();

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002470 File Offset: 0x00000670
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002478 File Offset: 0x00000678
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "issuer", Required = Required.Default)]
		public override string Issuer { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002481 File Offset: 0x00000681
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002489 File Offset: 0x00000689
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "jwks_uri", Required = Required.Default)]
		public string JwksUri { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002492 File Offset: 0x00000692
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000249A File Offset: 0x0000069A
		public JsonWebKeySet JsonWebKeySet { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000024A3 File Offset: 0x000006A3
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000024AB File Offset: 0x000006AB
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "logout_session_supported", Required = Required.Default)]
		public bool LogoutSessionSupported { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000024B4 File Offset: 0x000006B4
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000024BC File Offset: 0x000006BC
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "op_policy_uri", Required = Required.Default)]
		public string OpPolicyUri { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024C5 File Offset: 0x000006C5
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000024CD File Offset: 0x000006CD
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "op_tos_uri", Required = Required.Default)]
		public string OpTosUri { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000024D6 File Offset: 0x000006D6
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000024DE File Offset: 0x000006DE
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "registration_endpoint", Required = Required.Default)]
		public string RegistrationEndpoint { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000024E7 File Offset: 0x000006E7
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "request_object_encryption_alg_values_supported", Required = Required.Default)]
		public ICollection<string> RequestObjectEncryptionAlgValuesSupported { get; } = new Collection<string>();

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000024EF File Offset: 0x000006EF
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "request_object_encryption_enc_values_supported", Required = Required.Default)]
		public ICollection<string> RequestObjectEncryptionEncValuesSupported { get; } = new Collection<string>();

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000024F7 File Offset: 0x000006F7
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "request_object_signing_alg_values_supported", Required = Required.Default)]
		public ICollection<string> RequestObjectSigningAlgValuesSupported { get; } = new Collection<string>();

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000024FF File Offset: 0x000006FF
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002507 File Offset: 0x00000707
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "request_parameter_supported", Required = Required.Default)]
		public bool RequestParameterSupported { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002510 File Offset: 0x00000710
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002518 File Offset: 0x00000718
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "request_uri_parameter_supported", Required = Required.Default)]
		public bool RequestUriParameterSupported { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002521 File Offset: 0x00000721
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002529 File Offset: 0x00000729
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "require_request_uri_registration", Required = Required.Default)]
		public bool RequireRequestUriRegistration { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002532 File Offset: 0x00000732
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "response_modes_supported", Required = Required.Default)]
		public ICollection<string> ResponseModesSupported { get; } = new Collection<string>();

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000253A File Offset: 0x0000073A
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "response_types_supported", Required = Required.Default)]
		public ICollection<string> ResponseTypesSupported { get; } = new Collection<string>();

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002542 File Offset: 0x00000742
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000254A File Offset: 0x0000074A
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "service_documentation", Required = Required.Default)]
		public string ServiceDocumentation { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002553 File Offset: 0x00000753
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "scopes_supported", Required = Required.Default)]
		public ICollection<string> ScopesSupported { get; } = new Collection<string>();

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000255B File Offset: 0x0000075B
		[JsonIgnore]
		public override ICollection<SecurityKey> SigningKeys { get; } = new Collection<SecurityKey>();

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002563 File Offset: 0x00000763
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "subject_types_supported", Required = Required.Default)]
		public ICollection<string> SubjectTypesSupported { get; } = new Collection<string>();

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000256B File Offset: 0x0000076B
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002573 File Offset: 0x00000773
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "token_endpoint", Required = Required.Default)]
		public override string TokenEndpoint { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000257C File Offset: 0x0000077C
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002584 File Offset: 0x00000784
		[JsonIgnore]
		public override string ActiveTokenEndpoint { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000258D File Offset: 0x0000078D
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "token_endpoint_auth_methods_supported", Required = Required.Default)]
		public ICollection<string> TokenEndpointAuthMethodsSupported { get; } = new Collection<string>();

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002595 File Offset: 0x00000795
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "token_endpoint_auth_signing_alg_values_supported", Required = Required.Default)]
		public ICollection<string> TokenEndpointAuthSigningAlgValuesSupported { get; } = new Collection<string>();

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000259D File Offset: 0x0000079D
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "ui_locales_supported", Required = Required.Default)]
		public ICollection<string> UILocalesSupported { get; } = new Collection<string>();

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000025A5 File Offset: 0x000007A5
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000025AD File Offset: 0x000007AD
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "userinfo_endpoint", Required = Required.Default)]
		public string UserInfoEndpoint { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000025B6 File Offset: 0x000007B6
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "userinfo_encryption_alg_values_supported", Required = Required.Default)]
		public ICollection<string> UserInfoEndpointEncryptionAlgValuesSupported { get; } = new Collection<string>();

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000025BE File Offset: 0x000007BE
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "userinfo_encryption_enc_values_supported", Required = Required.Default)]
		public ICollection<string> UserInfoEndpointEncryptionEncValuesSupported { get; } = new Collection<string>();

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000025C6 File Offset: 0x000007C6
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "userinfo_signing_alg_values_supported", Required = Required.Default)]
		public ICollection<string> UserInfoEndpointSigningAlgValuesSupported { get; } = new Collection<string>();

		// Token: 0x0600004B RID: 75 RVA: 0x000025CE File Offset: 0x000007CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeAcrValuesSupported()
		{
			return this.AcrValuesSupported.Count > 0;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000025DE File Offset: 0x000007DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeClaimsSupported()
		{
			return this.ClaimsSupported.Count > 0;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000025EE File Offset: 0x000007EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeClaimsLocalesSupported()
		{
			return this.ClaimsLocalesSupported.Count > 0;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000025FE File Offset: 0x000007FE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeClaimTypesSupported()
		{
			return this.ClaimTypesSupported.Count > 0;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000260E File Offset: 0x0000080E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeDisplayValuesSupported()
		{
			return this.DisplayValuesSupported.Count > 0;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000261E File Offset: 0x0000081E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeGrantTypesSupported()
		{
			return this.GrantTypesSupported.Count > 0;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000262E File Offset: 0x0000082E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeIdTokenEncryptionAlgValuesSupported()
		{
			return this.IdTokenEncryptionAlgValuesSupported.Count > 0;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000263E File Offset: 0x0000083E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeIdTokenEncryptionEncValuesSupported()
		{
			return this.IdTokenEncryptionEncValuesSupported.Count > 0;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000264E File Offset: 0x0000084E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeIdTokenSigningAlgValuesSupported()
		{
			return this.IdTokenSigningAlgValuesSupported.Count > 0;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000265E File Offset: 0x0000085E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeIntrospectionEndpointAuthMethodsSupported()
		{
			return this.IntrospectionEndpointAuthMethodsSupported.Count > 0;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000266E File Offset: 0x0000086E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeIntrospectionEndpointAuthSigningAlgValuesSupported()
		{
			return this.IntrospectionEndpointAuthSigningAlgValuesSupported.Count > 0;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000267E File Offset: 0x0000087E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeRequestObjectEncryptionAlgValuesSupported()
		{
			return this.RequestObjectEncryptionAlgValuesSupported.Count > 0;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000268E File Offset: 0x0000088E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeRequestObjectEncryptionEncValuesSupported()
		{
			return this.RequestObjectEncryptionEncValuesSupported.Count > 0;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000269E File Offset: 0x0000089E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeRequestObjectSigningAlgValuesSupported()
		{
			return this.RequestObjectSigningAlgValuesSupported.Count > 0;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000026AE File Offset: 0x000008AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeResponseModesSupported()
		{
			return this.ResponseModesSupported.Count > 0;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000026BE File Offset: 0x000008BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeResponseTypesSupported()
		{
			return this.ResponseTypesSupported.Count > 0;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000026CE File Offset: 0x000008CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSigningKeys()
		{
			return false;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000026D1 File Offset: 0x000008D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeScopesSupported()
		{
			return this.ScopesSupported.Count > 0;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000026E1 File Offset: 0x000008E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSubjectTypesSupported()
		{
			return this.SubjectTypesSupported.Count > 0;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000026F1 File Offset: 0x000008F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTokenEndpointAuthMethodsSupported()
		{
			return this.TokenEndpointAuthMethodsSupported.Count > 0;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002701 File Offset: 0x00000901
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeTokenEndpointAuthSigningAlgValuesSupported()
		{
			return this.TokenEndpointAuthSigningAlgValuesSupported.Count > 0;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002711 File Offset: 0x00000911
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeUILocalesSupported()
		{
			return this.UILocalesSupported.Count > 0;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002721 File Offset: 0x00000921
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeUserInfoEndpointEncryptionAlgValuesSupported()
		{
			return this.UserInfoEndpointEncryptionAlgValuesSupported.Count > 0;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002731 File Offset: 0x00000931
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeUserInfoEndpointEncryptionEncValuesSupported()
		{
			return this.UserInfoEndpointEncryptionEncValuesSupported.Count > 0;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002741 File Offset: 0x00000941
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeUserInfoEndpointSigningAlgValuesSupported()
		{
			return this.UserInfoEndpointSigningAlgValuesSupported.Count > 0;
		}

		// Token: 0x04000004 RID: 4
		private const string _className = "Microsoft.IdentityModel.Protocols.OpenIdConnect.OpenIdConnectConfiguration";
	}
}
