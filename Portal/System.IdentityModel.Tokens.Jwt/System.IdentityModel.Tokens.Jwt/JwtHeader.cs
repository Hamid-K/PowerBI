using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace System.IdentityModel.Tokens.Jwt
{
	// Token: 0x02000007 RID: 7
	public class JwtHeader : Dictionary<string, object>
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002120 File Offset: 0x00000320
		public JwtHeader()
			: base(StringComparer.Ordinal)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000212D File Offset: 0x0000032D
		public JwtHeader(SigningCredentials signingCredentials)
			: this(signingCredentials, null)
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002137 File Offset: 0x00000337
		public JwtHeader(EncryptingCredentials encryptingCredentials)
			: this(encryptingCredentials, null)
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002141 File Offset: 0x00000341
		public JwtHeader(SigningCredentials signingCredentials, IDictionary<string, string> outboundAlgorithmMap)
			: this(signingCredentials, outboundAlgorithmMap, null)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000214C File Offset: 0x0000034C
		public JwtHeader(SigningCredentials signingCredentials, IDictionary<string, string> outboundAlgorithmMap, string tokenType)
			: this(signingCredentials, outboundAlgorithmMap, tokenType, null)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002158 File Offset: 0x00000358
		public JwtHeader(SigningCredentials signingCredentials, IDictionary<string, string> outboundAlgorithmMap, string tokenType, IDictionary<string, object> additionalInnerHeaderClaims)
			: base(StringComparer.Ordinal)
		{
			if (signingCredentials == null)
			{
				base["alg"] = "none";
			}
			else
			{
				string text;
				if (outboundAlgorithmMap != null && outboundAlgorithmMap.TryGetValue(signingCredentials.Algorithm, out text))
				{
					this.Alg = text;
				}
				else
				{
					this.Alg = signingCredentials.Algorithm;
				}
				if (!string.IsNullOrEmpty(signingCredentials.Key.KeyId))
				{
					this.Kid = signingCredentials.Key.KeyId;
				}
				X509SigningCredentials x509SigningCredentials = signingCredentials as X509SigningCredentials;
				if (x509SigningCredentials != null)
				{
					base["x5t"] = Base64UrlEncoder.Encode(x509SigningCredentials.Certificate.GetCertHash());
				}
			}
			if (string.IsNullOrEmpty(tokenType))
			{
				this.Typ = "JWT";
			}
			else
			{
				this.Typ = tokenType;
			}
			this.AddAdditionalClaims(additionalInnerHeaderClaims, false);
			this.SigningCredentials = signingCredentials;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002221 File Offset: 0x00000421
		public JwtHeader(EncryptingCredentials encryptingCredentials, IDictionary<string, string> outboundAlgorithmMap)
			: this(encryptingCredentials, outboundAlgorithmMap, null)
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000222C File Offset: 0x0000042C
		public JwtHeader(EncryptingCredentials encryptingCredentials, IDictionary<string, string> outboundAlgorithmMap, string tokenType)
			: this(encryptingCredentials, outboundAlgorithmMap, tokenType, null)
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002238 File Offset: 0x00000438
		public JwtHeader(EncryptingCredentials encryptingCredentials, IDictionary<string, string> outboundAlgorithmMap, string tokenType, IDictionary<string, object> additionalHeaderClaims)
			: base(StringComparer.Ordinal)
		{
			if (encryptingCredentials == null)
			{
				throw LogHelper.LogArgumentNullException("encryptingCredentials");
			}
			string text;
			if (outboundAlgorithmMap != null && outboundAlgorithmMap.TryGetValue(encryptingCredentials.Alg, out text))
			{
				this.Alg = text;
			}
			else
			{
				this.Alg = encryptingCredentials.Alg;
			}
			if (outboundAlgorithmMap != null && outboundAlgorithmMap.TryGetValue(encryptingCredentials.Enc, out text))
			{
				this.Enc = text;
			}
			else
			{
				this.Enc = encryptingCredentials.Enc;
			}
			if (!string.IsNullOrEmpty(encryptingCredentials.Key.KeyId))
			{
				this.Kid = encryptingCredentials.Key.KeyId;
			}
			if (string.IsNullOrEmpty(tokenType))
			{
				this.Typ = "JWT";
			}
			else
			{
				this.Typ = tokenType;
			}
			this.AddAdditionalClaims(additionalHeaderClaims, encryptingCredentials.SetDefaultCtyClaim);
			this.EncryptingCredentials = encryptingCredentials;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002302 File Offset: 0x00000502
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000230F File Offset: 0x0000050F
		public string Alg
		{
			get
			{
				return this.GetStandardClaim("alg");
			}
			private set
			{
				base["alg"] = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000231D File Offset: 0x0000051D
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000232A File Offset: 0x0000052A
		public string Cty
		{
			get
			{
				return this.GetStandardClaim("cty");
			}
			private set
			{
				base["cty"] = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002338 File Offset: 0x00000538
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002345 File Offset: 0x00000545
		public string Enc
		{
			get
			{
				return this.GetStandardClaim("enc");
			}
			private set
			{
				base["enc"] = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002353 File Offset: 0x00000553
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000235B File Offset: 0x0000055B
		public EncryptingCredentials EncryptingCredentials { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002364 File Offset: 0x00000564
		public string IV
		{
			get
			{
				return this.GetStandardClaim("iv");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002371 File Offset: 0x00000571
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000237E File Offset: 0x0000057E
		public string Kid
		{
			get
			{
				return this.GetStandardClaim("kid");
			}
			private set
			{
				base["kid"] = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000238C File Offset: 0x0000058C
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002394 File Offset: 0x00000594
		public SigningCredentials SigningCredentials { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000239D File Offset: 0x0000059D
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000023AA File Offset: 0x000005AA
		public string Typ
		{
			get
			{
				return this.GetStandardClaim("typ");
			}
			private set
			{
				base["typ"] = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000023B8 File Offset: 0x000005B8
		public string X5t
		{
			get
			{
				return this.GetStandardClaim("x5t");
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000023C5 File Offset: 0x000005C5
		public string X5c
		{
			get
			{
				return this.GetStandardClaim("x5c");
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000023D2 File Offset: 0x000005D2
		public string Zip
		{
			get
			{
				return this.GetStandardClaim("zip");
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000023DF File Offset: 0x000005DF
		public static JwtHeader Base64UrlDeserialize(string base64UrlEncodedJsonString)
		{
			return JsonExtensions.DeserializeJwtHeader(Base64UrlEncoder.Decode(base64UrlEncodedJsonString));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000023EC File Offset: 0x000005EC
		public virtual string Base64UrlEncode()
		{
			return Base64UrlEncoder.Encode(this.SerializeToJson());
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000023F9 File Offset: 0x000005F9
		public static JwtHeader Deserialize(string jsonString)
		{
			return JsonExtensions.DeserializeJwtHeader(jsonString);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002404 File Offset: 0x00000604
		internal string GetStandardClaim(string claimType)
		{
			object obj;
			if (!base.TryGetValue(claimType, out obj))
			{
				return null;
			}
			if (obj == null)
			{
				return null;
			}
			string text = obj as string;
			if (text != null)
			{
				return text;
			}
			return JsonExtensions.SerializeToJson(obj);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002438 File Offset: 0x00000638
		internal void AddAdditionalClaims(IDictionary<string, object> additionalHeaderClaims, bool setDefaultCtyClaim)
		{
			if (additionalHeaderClaims != null && additionalHeaderClaims.Count > 0 && additionalHeaderClaims.Keys.Intersect(JwtHeader.DefaultHeaderParameters, StringComparer.OrdinalIgnoreCase).Any<string>())
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenException(LogHelper.FormatInvariant("IDX12742: ''{0}' cannot contain the following claims: '{1}'. These values are added by default (if necessary) during security token creation.", new object[]
				{
					"additionalHeaderClaims",
					string.Join(", ", JwtHeader.DefaultHeaderParameters)
				})));
			}
			if (additionalHeaderClaims != null)
			{
				object obj;
				if (!additionalHeaderClaims.TryGetValue("cty", out obj) && setDefaultCtyClaim)
				{
					this.Cty = "JWT";
				}
				using (IEnumerator<string> enumerator = additionalHeaderClaims.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						base[text] = additionalHeaderClaims[text];
					}
					return;
				}
			}
			if (setDefaultCtyClaim)
			{
				this.Cty = "JWT";
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000251C File Offset: 0x0000071C
		public virtual string SerializeToJson()
		{
			return JsonExtensions.SerializeToJson(this);
		}

		// Token: 0x04000012 RID: 18
		internal static IList<string> DefaultHeaderParameters = new List<string> { "alg", "kid", "x5t", "enc", "zip" };
	}
}
