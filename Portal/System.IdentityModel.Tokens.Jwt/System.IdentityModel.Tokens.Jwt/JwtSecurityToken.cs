using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace System.IdentityModel.Tokens.Jwt
{
	// Token: 0x0200000B RID: 11
	public class JwtSecurityToken : SecurityToken
	{
		// Token: 0x0600005A RID: 90 RVA: 0x000032AC File Offset: 0x000014AC
		public JwtSecurityToken(string jwtEncodedString)
		{
			if (string.IsNullOrWhiteSpace(jwtEncodedString))
			{
				throw LogHelper.LogArgumentNullException("jwtEncodedString");
			}
			string[] array = jwtEncodedString.Split(new char[] { '.' }, 6);
			if (array.Length == 3)
			{
				if (!JwtTokenUtilities.RegexJws.IsMatch(jwtEncodedString))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenMalformedException("IDX12739: JWT has three segments but is not in proper JWS format."));
				}
			}
			else
			{
				if (array.Length != 5)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenMalformedException("IDX12741: JWT must have three segments (JWS) or five segments (JWE)."));
				}
				if (!JwtTokenUtilities.RegexJwe.IsMatch(jwtEncodedString))
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenMalformedException("IDX12740: JWT has five segments but is not in proper JWE format."));
				}
			}
			this.Decode(array, jwtEncodedString);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003344 File Offset: 0x00001544
		public JwtSecurityToken(JwtHeader header, JwtPayload payload, string rawHeader, string rawPayload, string rawSignature)
		{
			if (header == null)
			{
				throw LogHelper.LogArgumentNullException("header");
			}
			if (payload == null)
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			if (string.IsNullOrWhiteSpace(rawHeader))
			{
				throw LogHelper.LogArgumentNullException("rawHeader");
			}
			if (string.IsNullOrWhiteSpace(rawPayload))
			{
				throw LogHelper.LogArgumentNullException("rawPayload");
			}
			if (rawSignature == null)
			{
				throw LogHelper.LogArgumentNullException("rawSignature");
			}
			this.Header = header;
			this.Payload = payload;
			this.RawData = string.Concat(new string[] { rawHeader, ".", rawPayload, ".", rawSignature });
			this.RawHeader = rawHeader;
			this.RawPayload = rawPayload;
			this.RawSignature = rawSignature;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003400 File Offset: 0x00001600
		public JwtSecurityToken(JwtHeader header, JwtSecurityToken innerToken, string rawHeader, string rawEncryptedKey, string rawInitializationVector, string rawCiphertext, string rawAuthenticationTag)
		{
			if (header == null)
			{
				throw LogHelper.LogArgumentNullException("header");
			}
			if (innerToken == null)
			{
				throw LogHelper.LogArgumentNullException("innerToken");
			}
			if (rawEncryptedKey == null)
			{
				throw LogHelper.LogArgumentNullException("rawEncryptedKey");
			}
			if (string.IsNullOrEmpty(rawInitializationVector))
			{
				throw LogHelper.LogArgumentNullException("rawInitializationVector");
			}
			if (string.IsNullOrEmpty(rawCiphertext))
			{
				throw LogHelper.LogArgumentNullException("rawCiphertext");
			}
			if (string.IsNullOrEmpty(rawAuthenticationTag))
			{
				throw LogHelper.LogArgumentNullException("rawAuthenticationTag");
			}
			this.Header = header;
			this.InnerToken = innerToken;
			this.RawData = string.Join(".", new string[] { rawHeader, rawEncryptedKey, rawInitializationVector, rawCiphertext, rawAuthenticationTag });
			this.RawHeader = rawHeader;
			this.RawEncryptedKey = rawEncryptedKey;
			this.RawInitializationVector = rawInitializationVector;
			this.RawCiphertext = rawCiphertext;
			this.RawAuthenticationTag = rawAuthenticationTag;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000034DD File Offset: 0x000016DD
		public JwtSecurityToken(JwtHeader header, JwtPayload payload)
		{
			if (header == null)
			{
				throw LogHelper.LogArgumentNullException("header");
			}
			if (payload == null)
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			this.Header = header;
			this.Payload = payload;
			this.RawSignature = string.Empty;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000351C File Offset: 0x0000171C
		public JwtSecurityToken(string issuer = null, string audience = null, IEnumerable<Claim> claims = null, DateTime? notBefore = null, DateTime? expires = null, SigningCredentials signingCredentials = null)
		{
			if (expires != null && notBefore != null && notBefore >= expires)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX12401: Expires: '{0}' must be after NotBefore: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII(expires.Value),
					LogHelper.MarkAsNonPII(notBefore.Value)
				})));
			}
			this.Payload = new JwtPayload(issuer, audience, claims, notBefore, expires);
			this.Header = new JwtHeader(signingCredentials);
			this.RawSignature = string.Empty;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000035DD File Offset: 0x000017DD
		public string Actor
		{
			get
			{
				if (this.Payload != null)
				{
					return this.Payload.Actort;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000035F8 File Offset: 0x000017F8
		public IEnumerable<string> Audiences
		{
			get
			{
				if (this.Payload != null)
				{
					return this.Payload.Aud;
				}
				return new List<string>();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003613 File Offset: 0x00001813
		public IEnumerable<Claim> Claims
		{
			get
			{
				if (this.Payload != null)
				{
					return this.Payload.Claims;
				}
				return new List<Claim>();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000362E File Offset: 0x0000182E
		public virtual string EncodedHeader
		{
			get
			{
				return this.Header.Base64UrlEncode();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000363B File Offset: 0x0000183B
		public virtual string EncodedPayload
		{
			get
			{
				if (this.Payload != null)
				{
					return this.Payload.Base64UrlEncode();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003656 File Offset: 0x00001856
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000365E File Offset: 0x0000185E
		public JwtHeader Header { get; internal set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003667 File Offset: 0x00001867
		public override string Id
		{
			get
			{
				if (this.Payload != null)
				{
					return this.Payload.Jti;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003682 File Offset: 0x00001882
		public override string Issuer
		{
			get
			{
				if (this.Payload != null)
				{
					return this.Payload.Iss;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000369D File Offset: 0x0000189D
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000036B9 File Offset: 0x000018B9
		public JwtPayload Payload
		{
			get
			{
				if (this.InnerToken != null)
				{
					return this.InnerToken.Payload;
				}
				return this._payload;
			}
			internal set
			{
				this._payload = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000036C2 File Offset: 0x000018C2
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000036CA File Offset: 0x000018CA
		public JwtSecurityToken InnerToken { get; internal set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000036D3 File Offset: 0x000018D3
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000036DB File Offset: 0x000018DB
		public string RawAuthenticationTag { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000036E4 File Offset: 0x000018E4
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000036EC File Offset: 0x000018EC
		public string RawCiphertext { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000036F5 File Offset: 0x000018F5
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000036FD File Offset: 0x000018FD
		public string RawData { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003706 File Offset: 0x00001906
		// (set) Token: 0x06000073 RID: 115 RVA: 0x0000370E File Offset: 0x0000190E
		public string RawEncryptedKey { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003717 File Offset: 0x00001917
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000371F File Offset: 0x0000191F
		public string RawInitializationVector { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003728 File Offset: 0x00001928
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003730 File Offset: 0x00001930
		public string RawHeader { get; internal set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003739 File Offset: 0x00001939
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003741 File Offset: 0x00001941
		public string RawPayload { get; internal set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000374A File Offset: 0x0000194A
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003752 File Offset: 0x00001952
		public string RawSignature { get; internal set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000375B File Offset: 0x0000195B
		public override SecurityKey SecurityKey
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000375E File Offset: 0x0000195E
		public string SignatureAlgorithm
		{
			get
			{
				return this.Header.Alg;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000376B File Offset: 0x0000196B
		public SigningCredentials SigningCredentials
		{
			get
			{
				return this.Header.SigningCredentials;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003778 File Offset: 0x00001978
		public EncryptingCredentials EncryptingCredentials
		{
			get
			{
				return this.Header.EncryptingCredentials;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003785 File Offset: 0x00001985
		// (set) Token: 0x06000081 RID: 129 RVA: 0x0000378D File Offset: 0x0000198D
		public override SecurityKey SigningKey { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003796 File Offset: 0x00001996
		public string Subject
		{
			get
			{
				if (this.Payload != null)
				{
					return this.Payload.Sub;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000037B1 File Offset: 0x000019B1
		public override DateTime ValidFrom
		{
			get
			{
				if (this.Payload != null)
				{
					return this.Payload.ValidFrom;
				}
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000037CC File Offset: 0x000019CC
		public override DateTime ValidTo
		{
			get
			{
				if (this.Payload != null)
				{
					return this.Payload.ValidTo;
				}
				return DateTime.MinValue;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000037E7 File Offset: 0x000019E7
		public virtual DateTime IssuedAt
		{
			get
			{
				if (this.Payload != null)
				{
					return this.Payload.IssuedAt;
				}
				return DateTime.MinValue;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003802 File Offset: 0x00001A02
		public override string ToString()
		{
			if (this.Payload != null)
			{
				return this.Header.SerializeToJson() + "." + this.Payload.SerializeToJson();
			}
			return this.Header.SerializeToJson() + ".";
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003842 File Offset: 0x00001A42
		public override string UnsafeToString()
		{
			return this.RawData;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000384C File Offset: 0x00001A4C
		internal void Decode(string[] tokenParts, string rawData)
		{
			try
			{
				this.Header = JwtHeader.Base64UrlDeserialize(tokenParts[0]);
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX12729: Unable to decode the header '{0}' as Base64Url encoded string.", new object[] { tokenParts[0] }), ex));
			}
			if (tokenParts.Length == 5)
			{
				this.DecodeJwe(tokenParts);
			}
			else
			{
				this.DecodeJws(tokenParts);
			}
			this.RawData = rawData;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000038BC File Offset: 0x00001ABC
		private void DecodeJws(string[] tokenParts)
		{
			if (this.Header.Cty != null)
			{
				LogHelper.LogVerbose(LogHelper.FormatInvariant("IDX12738: Header.Cty != null, assuming JWS. Cty: '{0}'.", new object[] { this.Header.Cty }), Array.Empty<object>());
			}
			try
			{
				this.Payload = JwtPayload.Base64UrlDeserialize(tokenParts[1]);
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX12723: Unable to decode the payload '{0}' as Base64Url encoded string.", new object[] { tokenParts[1] }), ex));
			}
			this.RawHeader = tokenParts[0];
			this.RawPayload = tokenParts[1];
			this.RawSignature = tokenParts[2];
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003960 File Offset: 0x00001B60
		private void DecodeJwe(string[] tokenParts)
		{
			this.RawHeader = tokenParts[0];
			this.RawEncryptedKey = tokenParts[1];
			this.RawInitializationVector = tokenParts[2];
			this.RawCiphertext = tokenParts[3];
			this.RawAuthenticationTag = tokenParts[4];
		}

		// Token: 0x0400003D RID: 61
		private JwtPayload _payload;
	}
}
