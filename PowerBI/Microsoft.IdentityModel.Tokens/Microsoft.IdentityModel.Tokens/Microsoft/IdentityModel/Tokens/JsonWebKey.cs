using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Json;
using Microsoft.IdentityModel.Json.Linq;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200015B RID: 347
	[JsonObject]
	public class JsonWebKey : SecurityKey
	{
		// Token: 0x06001004 RID: 4100 RVA: 0x0003E530 File Offset: 0x0003C730
		public static JsonWebKey Create(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw LogHelper.LogArgumentNullException("json");
			}
			return new JsonWebKey(json);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0003E54B File Offset: 0x0003C74B
		public JsonWebKey()
		{
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0003E574 File Offset: 0x0003C774
		public JsonWebKey(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				throw LogHelper.LogArgumentNullException("json");
			}
			try
			{
				LogHelper.LogVerbose("IDX10806: Deserializing json: '{0}' into '{1}'.", new object[]
				{
					json,
					LogHelper.MarkAsNonPII("Microsoft.IdentityModel.Tokens.JsonWebKey")
				});
				JsonConvert.PopulateObject(json, this);
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10805: Error deserializing json: '{0}' into '{1}'.", new object[]
				{
					json,
					LogHelper.MarkAsNonPII("Microsoft.IdentityModel.Tokens.JsonWebKey")
				}), ex));
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x0003E624 File Offset: 0x0003C824
		// (set) Token: 0x06001008 RID: 4104 RVA: 0x0003E62C File Offset: 0x0003C82C
		[JsonIgnore]
		internal SecurityKey ConvertedSecurityKey { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0003E635 File Offset: 0x0003C835
		// (set) Token: 0x0600100A RID: 4106 RVA: 0x0003E63D File Offset: 0x0003C83D
		[JsonIgnore]
		internal string ConvertKeyInfo { get; set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x0003E646 File Offset: 0x0003C846
		[JsonExtensionData]
		public virtual IDictionary<string, object> AdditionalData { get; } = new Dictionary<string, object>();

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0003E64E File Offset: 0x0003C84E
		// (set) Token: 0x0600100D RID: 4109 RVA: 0x0003E656 File Offset: 0x0003C856
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "alg", Required = Required.Default)]
		public string Alg { get; set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0003E65F File Offset: 0x0003C85F
		// (set) Token: 0x0600100F RID: 4111 RVA: 0x0003E667 File Offset: 0x0003C867
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "crv", Required = Required.Default)]
		public string Crv { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001010 RID: 4112 RVA: 0x0003E670 File Offset: 0x0003C870
		// (set) Token: 0x06001011 RID: 4113 RVA: 0x0003E678 File Offset: 0x0003C878
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "d", Required = Required.Default)]
		public string D { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001012 RID: 4114 RVA: 0x0003E681 File Offset: 0x0003C881
		// (set) Token: 0x06001013 RID: 4115 RVA: 0x0003E689 File Offset: 0x0003C889
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "dp", Required = Required.Default)]
		public string DP { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0003E692 File Offset: 0x0003C892
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x0003E69A File Offset: 0x0003C89A
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "dq", Required = Required.Default)]
		public string DQ { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0003E6A3 File Offset: 0x0003C8A3
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x0003E6AB File Offset: 0x0003C8AB
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "e", Required = Required.Default)]
		public string E { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0003E6B4 File Offset: 0x0003C8B4
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x0003E6BC File Offset: 0x0003C8BC
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "k", Required = Required.Default)]
		public string K { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0003E6C5 File Offset: 0x0003C8C5
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x0003E6CD File Offset: 0x0003C8CD
		[JsonIgnore]
		public override string KeyId
		{
			get
			{
				return this._kid;
			}
			set
			{
				this._kid = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x0003E6D6 File Offset: 0x0003C8D6
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x0003E6DE File Offset: 0x0003C8DE
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "key_ops", Required = Required.Default)]
		public IList<string> KeyOps { get; private set; } = new List<string>();

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x0003E6E7 File Offset: 0x0003C8E7
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x0003E6EF File Offset: 0x0003C8EF
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "kid", Required = Required.Default)]
		public string Kid
		{
			get
			{
				return this._kid;
			}
			set
			{
				this._kid = value;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0003E6F8 File Offset: 0x0003C8F8
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x0003E700 File Offset: 0x0003C900
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "kty", Required = Required.Default)]
		public string Kty { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0003E709 File Offset: 0x0003C909
		// (set) Token: 0x06001023 RID: 4131 RVA: 0x0003E711 File Offset: 0x0003C911
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "n", Required = Required.Default)]
		public string N { get; set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0003E71A File Offset: 0x0003C91A
		// (set) Token: 0x06001025 RID: 4133 RVA: 0x0003E722 File Offset: 0x0003C922
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "oth", Required = Required.Default)]
		public IList<string> Oth { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0003E72B File Offset: 0x0003C92B
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x0003E733 File Offset: 0x0003C933
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "p", Required = Required.Default)]
		public string P { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x0003E73C File Offset: 0x0003C93C
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0003E744 File Offset: 0x0003C944
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "q", Required = Required.Default)]
		public string Q { get; set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x0003E74D File Offset: 0x0003C94D
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x0003E755 File Offset: 0x0003C955
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "qi", Required = Required.Default)]
		public string QI { get; set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0003E75E File Offset: 0x0003C95E
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x0003E766 File Offset: 0x0003C966
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "use", Required = Required.Default)]
		public string Use { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0003E76F File Offset: 0x0003C96F
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x0003E777 File Offset: 0x0003C977
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "x", Required = Required.Default)]
		public string X { get; set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0003E780 File Offset: 0x0003C980
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x0003E788 File Offset: 0x0003C988
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "x5c", Required = Required.Default)]
		public IList<string> X5c { get; private set; } = new List<string>();

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0003E791 File Offset: 0x0003C991
		// (set) Token: 0x06001033 RID: 4147 RVA: 0x0003E799 File Offset: 0x0003C999
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "x5t", Required = Required.Default)]
		public string X5t { get; set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0003E7A2 File Offset: 0x0003C9A2
		// (set) Token: 0x06001035 RID: 4149 RVA: 0x0003E7AA File Offset: 0x0003C9AA
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "x5t#S256", Required = Required.Default)]
		public string X5tS256 { get; set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0003E7B3 File Offset: 0x0003C9B3
		// (set) Token: 0x06001037 RID: 4151 RVA: 0x0003E7BB File Offset: 0x0003C9BB
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "x5u", Required = Required.Default)]
		public string X5u { get; set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0003E7C4 File Offset: 0x0003C9C4
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x0003E7CC File Offset: 0x0003C9CC
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "y", Required = Required.Default)]
		public string Y { get; set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0003E7D8 File Offset: 0x0003C9D8
		[JsonIgnore]
		public override int KeySize
		{
			get
			{
				if (this.Kty == "RSA" && !string.IsNullOrEmpty(this.N))
				{
					return Base64UrlEncoder.DecodeBytes(this.N).Length * 8;
				}
				if (this.Kty == "EC" && !string.IsNullOrEmpty(this.X))
				{
					return Base64UrlEncoder.DecodeBytes(this.X).Length * 8;
				}
				if (this.Kty == "oct" && !string.IsNullOrEmpty(this.K))
				{
					return Base64UrlEncoder.DecodeBytes(this.K).Length * 8;
				}
				return 0;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0003E874 File Offset: 0x0003CA74
		[JsonIgnore]
		public bool HasPrivateKey
		{
			get
			{
				if (this.Kty == "RSA")
				{
					return this.D != null && this.DP != null && this.DQ != null && this.P != null && this.Q != null && this.QI != null;
				}
				return this.Kty == "EC" && this.D != null;
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0003E8E4 File Offset: 0x0003CAE4
		public bool ShouldSerializeKeyOps()
		{
			return this.KeyOps.Count > 0;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0003E8F4 File Offset: 0x0003CAF4
		public bool ShouldSerializeX5c()
		{
			return this.X5c.Count > 0;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0003E904 File Offset: 0x0003CB04
		internal RSAParameters CreateRsaParameters()
		{
			if (string.IsNullOrEmpty(this.N))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10700: {0} is unable to use 'rsaParameters'. {1} is null.", new object[]
				{
					LogHelper.MarkAsNonPII("Microsoft.IdentityModel.Tokens.JsonWebKey"),
					LogHelper.MarkAsNonPII("Modulus")
				})));
			}
			if (string.IsNullOrEmpty(this.E))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10700: {0} is unable to use 'rsaParameters'. {1} is null.", new object[]
				{
					LogHelper.MarkAsNonPII("Microsoft.IdentityModel.Tokens.JsonWebKey"),
					LogHelper.MarkAsNonPII("Exponent")
				})));
			}
			return new RSAParameters
			{
				Modulus = Base64UrlEncoder.DecodeBytes(this.N),
				Exponent = Base64UrlEncoder.DecodeBytes(this.E),
				D = (string.IsNullOrEmpty(this.D) ? null : Base64UrlEncoder.DecodeBytes(this.D)),
				P = (string.IsNullOrEmpty(this.P) ? null : Base64UrlEncoder.DecodeBytes(this.P)),
				Q = (string.IsNullOrEmpty(this.Q) ? null : Base64UrlEncoder.DecodeBytes(this.Q)),
				DP = (string.IsNullOrEmpty(this.DP) ? null : Base64UrlEncoder.DecodeBytes(this.DP)),
				DQ = (string.IsNullOrEmpty(this.DQ) ? null : Base64UrlEncoder.DecodeBytes(this.DQ)),
				InverseQ = (string.IsNullOrEmpty(this.QI) ? null : Base64UrlEncoder.DecodeBytes(this.QI))
			};
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0003EA90 File Offset: 0x0003CC90
		public override bool CanComputeJwkThumbprint()
		{
			if (string.IsNullOrEmpty(this.Kty))
			{
				return false;
			}
			if (string.Equals(this.Kty, "EC"))
			{
				return this.CanComputeECThumbprint();
			}
			if (string.Equals(this.Kty, "RSA"))
			{
				return this.CanComputeRsaThumbprint();
			}
			return string.Equals(this.Kty, "oct") && this.CanComputeOctThumbprint();
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0003EAF8 File Offset: 0x0003CCF8
		public override byte[] ComputeJwkThumbprint()
		{
			if (string.IsNullOrEmpty(this.Kty))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10705: Cannot create a JWK thumbprint, '{0}' is null or empty.", new object[] { LogHelper.MarkAsNonPII("Kty") })));
			}
			if (string.Equals(this.Kty, "EC"))
			{
				return this.ComputeECThumbprint();
			}
			if (string.Equals(this.Kty, "RSA"))
			{
				return this.ComputeRsaThumbprint();
			}
			if (string.Equals(this.Kty, "oct"))
			{
				return this.ComputeOctThumbprint();
			}
			throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10706: Cannot create a JWK thumbprint, '{0}' must be one of the following: '{1}'.", new object[]
			{
				LogHelper.MarkAsNonPII("Kty"),
				LogHelper.MarkAsNonPII(string.Join(", ", new string[] { "EC", "RSA", "oct" })),
				LogHelper.MarkAsNonPII("Kty")
			})));
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0003EBE9 File Offset: 0x0003CDE9
		private bool CanComputeOctThumbprint()
		{
			return !string.IsNullOrEmpty(this.K);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0003EBFC File Offset: 0x0003CDFC
		private byte[] ComputeOctThumbprint()
		{
			if (string.IsNullOrEmpty(this.K))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10705: Cannot create a JWK thumbprint, '{0}' is null or empty.", new object[] { LogHelper.MarkAsNonPII("K") })));
			}
			return Utility.GenerateSha256Hash(string.Concat(new string[] { "{\"k\":\"", this.K, "\",\"kty\":\"", this.Kty, "\"}" }));
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0003EC78 File Offset: 0x0003CE78
		private bool CanComputeRsaThumbprint()
		{
			return !string.IsNullOrEmpty(this.E) && !string.IsNullOrEmpty(this.N);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0003EC98 File Offset: 0x0003CE98
		private byte[] ComputeRsaThumbprint()
		{
			if (string.IsNullOrEmpty(this.E))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10705: Cannot create a JWK thumbprint, '{0}' is null or empty.", new object[] { LogHelper.MarkAsNonPII("E") })));
			}
			if (string.IsNullOrEmpty(this.N))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10705: Cannot create a JWK thumbprint, '{0}' is null or empty.", new object[] { LogHelper.MarkAsNonPII("N") })));
			}
			return Utility.GenerateSha256Hash(string.Concat(new string[] { "{\"e\":\"", this.E, "\",\"kty\":\"", this.Kty, "\",\"n\":\"", this.N, "\"}" }));
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0003ED5A File Offset: 0x0003CF5A
		private bool CanComputeECThumbprint()
		{
			return !string.IsNullOrEmpty(this.Crv) && !string.IsNullOrEmpty(this.X) && !string.IsNullOrEmpty(this.Y);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0003ED88 File Offset: 0x0003CF88
		private byte[] ComputeECThumbprint()
		{
			if (string.IsNullOrEmpty(this.Crv))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10705: Cannot create a JWK thumbprint, '{0}' is null or empty.", new object[] { LogHelper.MarkAsNonPII("Crv") })));
			}
			if (string.IsNullOrEmpty(this.X))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10705: Cannot create a JWK thumbprint, '{0}' is null or empty.", new object[] { LogHelper.MarkAsNonPII("X") })));
			}
			if (string.IsNullOrEmpty(this.Y))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10705: Cannot create a JWK thumbprint, '{0}' is null or empty.", new object[] { LogHelper.MarkAsNonPII("Y") })));
			}
			return Utility.GenerateSha256Hash(string.Concat(new string[] { "{\"crv\":\"", this.Crv, "\",\"kty\":\"", this.Kty, "\",\"x\":\"", this.X, "\",\"y\":\"", this.Y, "\"}" }));
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0003EE94 File Offset: 0x0003D094
		internal string RepresentAsAsymmetricPublicJwk()
		{
			JObject jobject = new JObject();
			if (!string.IsNullOrEmpty(this.Kid))
			{
				jobject.Add("kid", this.Kid);
			}
			if (string.Equals(this.Kty, "EC"))
			{
				this.PopulateWithPublicEcParams(jobject);
			}
			else
			{
				if (!string.Equals(this.Kty, "RSA"))
				{
					throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10707: Cannot create a JSON representation of an asymmetric public key, '{0}' must be one of the following: '{1}'.", new object[]
					{
						LogHelper.MarkAsNonPII("Kty"),
						LogHelper.MarkAsNonPII(string.Join(", ", new string[] { "EC", "RSA" })),
						LogHelper.MarkAsNonPII("Kty")
					})));
				}
				this.PopulateWithPublicRsaParams(jobject);
			}
			return jobject.ToString(Formatting.None, Array.Empty<JsonConverter>());
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0003EF6C File Offset: 0x0003D16C
		private void PopulateWithPublicEcParams(JObject jwk)
		{
			if (string.IsNullOrEmpty(this.Crv))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10708: Cannot create a JSON representation of an EC public key, '{0}' is null or empty.", new object[] { LogHelper.MarkAsNonPII("Crv") })));
			}
			if (string.IsNullOrEmpty(this.X))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10708: Cannot create a JSON representation of an EC public key, '{0}' is null or empty.", new object[] { LogHelper.MarkAsNonPII("X") })));
			}
			if (string.IsNullOrEmpty(this.Y))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10708: Cannot create a JSON representation of an EC public key, '{0}' is null or empty.", new object[] { LogHelper.MarkAsNonPII("Y") })));
			}
			jwk.Add("crv", this.Crv);
			jwk.Add("kty", this.Kty);
			jwk.Add("x", this.X);
			jwk.Add("y", this.Y);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0003F070 File Offset: 0x0003D270
		private void PopulateWithPublicRsaParams(JObject jwk)
		{
			if (string.IsNullOrEmpty(this.E))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10709: Cannot create a JSON representation of an RSA public key, '{0}' is null or empty.", new object[] { LogHelper.MarkAsNonPII("E") })));
			}
			if (string.IsNullOrEmpty(this.N))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10709: Cannot create a JSON representation of an RSA public key, '{0}' is null or empty.", new object[] { LogHelper.MarkAsNonPII("N") })));
			}
			jwk.Add("e", this.E);
			jwk.Add("kty", this.Kty);
			jwk.Add("n", this.N);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0003F129 File Offset: 0x0003D329
		public override string ToString()
		{
			return string.Format("{0}, Use: '{1}',  Kid: '{2}', Kty: '{3}', InternalId: '{4}'.", new object[]
			{
				base.GetType(),
				this.Use,
				this.Kid,
				this.Kty,
				this.InternalId
			});
		}

		// Token: 0x04000523 RID: 1315
		private string _kid;

		// Token: 0x04000524 RID: 1316
		private const string _className = "Microsoft.IdentityModel.Tokens.JsonWebKey";
	}
}
