using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.JsonWebTokens
{
	// Token: 0x02000004 RID: 4
	public class JsonWebToken : SecurityToken
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002591 File Offset: 0x00000791
		public JsonWebToken(string jwtEncodedString)
		{
			if (string.IsNullOrEmpty(jwtEncodedString))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentNullException("jwtEncodedString"));
			}
			this.ReadToken(jwtEncodedString);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000025C4 File Offset: 0x000007C4
		public JsonWebToken(string header, string payload)
		{
			if (string.IsNullOrEmpty(header))
			{
				throw LogHelper.LogArgumentNullException("header");
			}
			if (string.IsNullOrEmpty(payload))
			{
				throw LogHelper.LogArgumentNullException("payload");
			}
			try
			{
				this.Header = new JsonClaimSet(header);
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX14301: Unable to parse the header into a JSON object. \nHeader: '{0}'.", new object[] { header }), ex));
			}
			try
			{
				this.Payload = new JsonClaimSet(payload);
			}
			catch (Exception ex2)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX14302: Unable to parse the payload into a JSON object. \nPayload: '{0}'.", new object[] { payload }), ex2));
			}
			this._encodedHeader = Base64UrlEncoder.Encode(header);
			this._encodedPayload = Base64UrlEncoder.Encode(payload);
			this.EncodedToken = this._encodedHeader + "." + this._encodedPayload + ".";
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000026C0 File Offset: 0x000008C0
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000026C8 File Offset: 0x000008C8
		internal string ActualIssuer { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000026D1 File Offset: 0x000008D1
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000026D9 File Offset: 0x000008D9
		internal ClaimsIdentity ActorClaimsIdentity { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000026E2 File Offset: 0x000008E2
		public string AuthenticationTag
		{
			get
			{
				if (this._authenticationTag == null)
				{
					this._authenticationTag = ((this.AuthenticationTagBytes == null) ? string.Empty : Encoding.UTF8.GetString(this.AuthenticationTagBytes));
				}
				return this._authenticationTag;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002717 File Offset: 0x00000917
		// (set) Token: 0x0600000D RID: 13 RVA: 0x0000271F File Offset: 0x0000091F
		internal byte[] AuthenticationTagBytes { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002728 File Offset: 0x00000928
		public string Ciphertext
		{
			get
			{
				if (this._ciphertext == null)
				{
					this._ciphertext = ((this.CipherTextBytes == null) ? string.Empty : Encoding.UTF8.GetString(this.CipherTextBytes));
				}
				return this._ciphertext;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000275D File Offset: 0x0000095D
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002765 File Offset: 0x00000965
		internal byte[] CipherTextBytes { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002770 File Offset: 0x00000970
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000293C File Offset: 0x00000B3C
		internal ClaimsIdentity ClaimsIdentity
		{
			get
			{
				if (!this._wasClaimsIdentitySet)
				{
					this._wasClaimsIdentitySet = true;
					string text = this.ActualIssuer ?? this.Issuer;
					foreach (Claim claim in this.Claims)
					{
						string type = claim.Type;
						if (type == "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor")
						{
							if (this._claimsIdentity.Actor != null)
							{
								throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX14112: Only a single 'Actor' is supported. Found second claim of type: '{0}'", new object[]
								{
									LogHelper.MarkAsNonPII("actort"),
									claim.Value
								})));
							}
							try
							{
								new JsonWebToken(claim.Value);
								this._claimsIdentity.Actor = this.ActorClaimsIdentity;
							}
							catch
							{
							}
						}
						if (claim.Properties.Count == 0)
						{
							this._claimsIdentity.AddClaim(new Claim(type, claim.Value, claim.ValueType, text, text, this._claimsIdentity));
						}
						else
						{
							Claim claim2 = new Claim(type, claim.Value, claim.ValueType, text, text, this._claimsIdentity);
							foreach (KeyValuePair<string, string> keyValuePair in claim.Properties)
							{
								claim2.Properties[keyValuePair.Key] = keyValuePair.Value;
							}
							this._claimsIdentity.AddClaim(claim2);
						}
					}
				}
				return this._claimsIdentity;
			}
			set
			{
				this._claimsIdentity = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002945 File Offset: 0x00000B45
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000294D File Offset: 0x00000B4D
		internal int Dot1 { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002956 File Offset: 0x00000B56
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000295E File Offset: 0x00000B5E
		internal int Dot2 { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002967 File Offset: 0x00000B67
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000296F File Offset: 0x00000B6F
		internal int Dot3 { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002978 File Offset: 0x00000B78
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002980 File Offset: 0x00000B80
		internal int Dot4 { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002989 File Offset: 0x00000B89
		public string EncodedHeader
		{
			get
			{
				if (this._encodedHeader == null)
				{
					if (this.EncodedToken != null)
					{
						this._encodedHeader = this.EncodedToken.Substring(0, this.Dot1);
					}
					else
					{
						this._encodedHeader = string.Empty;
					}
				}
				return this._encodedHeader;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000029C6 File Offset: 0x00000BC6
		public string EncryptedKey
		{
			get
			{
				if (this._encryptedKey == null)
				{
					this._encryptedKey = ((this.EncryptedKeyBytes == null) ? string.Empty : Encoding.UTF8.GetString(this.EncryptedKeyBytes));
				}
				return this._encryptedKey;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000029FB File Offset: 0x00000BFB
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002A03 File Offset: 0x00000C03
		internal byte[] EncryptedKeyBytes { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002A0C File Offset: 0x00000C0C
		public string EncodedPayload
		{
			get
			{
				if (this._encodedPayload == null)
				{
					if (this.EncodedToken != null)
					{
						this._encodedPayload = (this.IsEncrypted ? string.Empty : this.EncodedToken.Substring(this.Dot1 + 1, this.Dot2 - this.Dot1 - 1));
					}
					else
					{
						this._encodedPayload = string.Empty;
					}
				}
				return this._encodedPayload;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002A74 File Offset: 0x00000C74
		public string EncodedSignature
		{
			get
			{
				if (this._encodedSignature == null)
				{
					if (this.EncodedToken != null)
					{
						this._encodedSignature = (this.IsEncrypted ? string.Empty : this.EncodedToken.Substring(this.Dot2 + 1, this.EncodedToken.Length - this.Dot2 - 1));
					}
					else
					{
						this._encodedSignature = string.Empty;
					}
				}
				return this._encodedSignature;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002AE0 File Offset: 0x00000CE0
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public string EncodedToken { get; private set; }

		// Token: 0x06000023 RID: 35 RVA: 0x00002AF1 File Offset: 0x00000CF1
		internal bool HasPayloadClaim(string claimName)
		{
			return this.Payload.HasClaim(claimName);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002AFF File Offset: 0x00000CFF
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002B07 File Offset: 0x00000D07
		internal JsonClaimSet Header { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002B10 File Offset: 0x00000D10
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002B18 File Offset: 0x00000D18
		internal byte[] HeaderAsciiBytes { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002B21 File Offset: 0x00000D21
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002B29 File Offset: 0x00000D29
		internal byte[] InitializationVectorBytes { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002B32 File Offset: 0x00000D32
		public string InitializationVector
		{
			get
			{
				if (this.InitializationVectorBytes == null)
				{
					this._initializationVector = ((this.InitializationVectorBytes == null) ? string.Empty : Encoding.UTF8.GetString(this.InitializationVectorBytes));
				}
				return this._initializationVector;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002B67 File Offset: 0x00000D67
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002B6F File Offset: 0x00000D6F
		public JsonWebToken InnerToken { get; internal set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002B78 File Offset: 0x00000D78
		public bool IsEncrypted
		{
			get
			{
				return this.CipherTextBytes != null;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002B83 File Offset: 0x00000D83
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002B8B File Offset: 0x00000D8B
		public bool IsSigned { get; internal set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002B94 File Offset: 0x00000D94
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002B9C File Offset: 0x00000D9C
		internal JsonClaimSet Payload { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002BA5 File Offset: 0x00000DA5
		public override SecurityKey SecurityKey { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002BAD File Offset: 0x00000DAD
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002BB5 File Offset: 0x00000DB5
		public override SecurityKey SigningKey { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002BBE File Offset: 0x00000DBE
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002BC6 File Offset: 0x00000DC6
		internal byte[] MessageBytes { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002BCF File Offset: 0x00000DCF
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002BD7 File Offset: 0x00000DD7
		internal int NumberOfDots { get; set; }

		// Token: 0x06000039 RID: 57 RVA: 0x00002BE0 File Offset: 0x00000DE0
		private void ReadToken(string encodedJson)
		{
			this.Dot1 = encodedJson.IndexOf('.');
			if (this.Dot1 == -1 || this.Dot1 == encodedJson.Length - 1)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenMalformedException("IDX14100: JWT is not well formed, there are no dots (.).\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'."));
			}
			this.Dot2 = encodedJson.IndexOf('.', this.Dot1 + 1);
			if (this.Dot2 == -1)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenMalformedException("IDX14120: JWT is not well formed, there is only one dot (.).\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'."));
			}
			if (this.Dot2 == encodedJson.Length - 1)
			{
				this.Dot3 = -1;
			}
			else
			{
				this.Dot3 = encodedJson.IndexOf('.', this.Dot2 + 1);
			}
			if (this.Dot3 == -1)
			{
				this.IsSigned = this.Dot2 + 1 != encodedJson.Length;
				JsonDocument jsonDocument = null;
				try
				{
					jsonDocument = JwtTokenUtilities.GetJsonDocumentFromBase64UrlEncodedString(encodedJson, 0, this.Dot1);
					this.Header = new JsonClaimSet(jsonDocument);
				}
				catch (Exception ex)
				{
					string text = "IDX14102: Unable to decode the header '{0}' as Base64Url encoded string.";
					object[] array = new object[1];
					array[0] = LogHelper.MarkAsUnsafeSecurityArtifact(encodedJson.Substring(0, this.Dot1), (object t) => t.ToString());
					throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant(text, array), ex));
				}
				finally
				{
					if (jsonDocument != null)
					{
						jsonDocument.Dispose();
					}
				}
				JsonDocument jsonDocument2 = null;
				try
				{
					jsonDocument2 = JwtTokenUtilities.GetJsonDocumentFromBase64UrlEncodedString(encodedJson, this.Dot1 + 1, this.Dot2 - this.Dot1 - 1);
					this.Payload = new JsonClaimSet(jsonDocument2);
					goto IL_04AB;
				}
				catch (Exception ex2)
				{
					string text2 = "IDX14101: Unable to decode the payload '{0}' as Base64Url encoded string.";
					object[] array2 = new object[1];
					array2[0] = LogHelper.MarkAsUnsafeSecurityArtifact(encodedJson.Substring(this.Dot1 + 1, this.Dot2 - this.Dot1 - 1), (object t) => t.ToString());
					throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant(text2, array2), ex2));
				}
				finally
				{
					if (jsonDocument2 != null)
					{
						jsonDocument2.Dispose();
					}
				}
			}
			this.Payload = new JsonClaimSet(JsonDocument.Parse("{}", default(JsonDocumentOptions)));
			if (this.Dot3 == encodedJson.Length)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException("IDX14121: JWT is not a well formed JWE, there are there must be four dots (.).\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'."));
			}
			this.Dot4 = encodedJson.IndexOf('.', this.Dot3 + 1);
			if (this.Dot4 == -1)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenMalformedException("IDX14121: JWT is not a well formed JWE, there are there must be four dots (.).\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'."));
			}
			if (encodedJson.IndexOf('.', this.Dot4 + 1) != -1)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenMalformedException("IDX14122: JWT is not a well formed JWE, there are more than four dots (.) a JWE can have at most 4 dots.\nThe token needs to be in JWS or JWE Compact Serialization Format. (JWS): 'EncodedHeader.EndcodedPayload.EncodedSignature'. (JWE): 'EncodedProtectedHeader.EncodedEncryptedKey.EncodedInitializationVector.EncodedCiphertext.EncodedAuthenticationTag'."));
			}
			if (this.Dot4 == encodedJson.Length - 1)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenMalformedException("IDX14310: JWE authentication tag is missing."));
			}
			this._hChars = encodedJson.ToCharArray(0, this.Dot1);
			if (this._hChars.Length == 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException("IDX14307: JWE header is missing."));
			}
			this.HeaderAsciiBytes = Encoding.ASCII.GetBytes(this._hChars);
			try
			{
				this.Header = new JsonClaimSet(Base64UrlEncoder.UnsafeDecode(this._hChars));
			}
			catch (Exception ex3)
			{
				string text3 = "IDX14102: Unable to decode the header '{0}' as Base64Url encoded string.";
				object[] array3 = new object[1];
				array3[0] = LogHelper.MarkAsUnsafeSecurityArtifact(encodedJson.Substring(0, this.Dot1), (object t) => t.ToString());
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant(text3, array3), ex3));
			}
			char[] array4 = encodedJson.ToCharArray(this.Dot1 + 1, this.Dot2 - this.Dot1 - 1);
			if (array4.Length != 0)
			{
				this.EncryptedKeyBytes = Base64UrlEncoder.UnsafeDecode(array4);
				this._encryptedKey = encodedJson.Substring(this.Dot1 + 1, this.Dot2 - this.Dot1 - 1);
			}
			else
			{
				this._encryptedKey = string.Empty;
			}
			char[] array5 = encodedJson.ToCharArray(this.Dot2 + 1, this.Dot3 - this.Dot2 - 1);
			if (array5.Length == 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException("IDX14308: JWE initialization vector is missing."));
			}
			try
			{
				this.InitializationVectorBytes = Base64UrlEncoder.UnsafeDecode(array5);
			}
			catch (Exception ex4)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException("IDX14309: Unable to decode the initialization vector as Base64Url encoded string.", ex4));
			}
			char[] array6 = encodedJson.ToCharArray(this.Dot4 + 1, encodedJson.Length - this.Dot4 - 1);
			if (array6.Length == 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException("IDX14310: JWE authentication tag is missing."));
			}
			try
			{
				this.AuthenticationTagBytes = Base64UrlEncoder.UnsafeDecode(array6);
			}
			catch (Exception ex5)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException("IDX14311: Unable to decode the authentication tag as a Base64Url encoded string.", ex5));
			}
			if (encodedJson.ToCharArray(this.Dot3 + 1, this.Dot4 - this.Dot3 - 1).Length == 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException("IDX14306: JWE Ciphertext cannot be an empty string."));
			}
			try
			{
				this.CipherTextBytes = Base64UrlEncoder.UnsafeDecode(encodedJson.ToCharArray(this.Dot3 + 1, this.Dot4 - this.Dot3 - 1));
			}
			catch (Exception ex6)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException("IDX14312: Unable to decode the cipher text as a Base64Url encoded string.", ex6));
			}
			IL_04AB:
			this.EncodedToken = encodedJson;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003104 File Offset: 0x00001304
		public override string UnsafeToString()
		{
			return this.EncodedToken;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000310C File Offset: 0x0000130C
		public string Actor
		{
			get
			{
				if (this._act == null)
				{
					this._act = this.Payload.GetStringValue("actort");
				}
				return this._act;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003132 File Offset: 0x00001332
		public string Alg
		{
			get
			{
				if (this._alg == null)
				{
					this._alg = this.Header.GetStringValue("alg");
				}
				return this._alg;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003158 File Offset: 0x00001358
		public IEnumerable<string> Audiences
		{
			get
			{
				if (this._audiences == null)
				{
					object audiencesLock = this._audiencesLock;
					lock (audiencesLock)
					{
						if (this._audiences == null)
						{
							List<string> list = new List<string>();
							JsonElement jsonElement;
							if (this.Payload.TryGetValue<JsonElement>("aud", out jsonElement))
							{
								if (jsonElement.ValueKind == 3)
								{
									list = new List<string> { jsonElement.GetString() };
								}
								if (jsonElement.ValueKind == 2)
								{
									foreach (JsonElement jsonElement2 in jsonElement.EnumerateArray())
									{
										list.Add(jsonElement2.ToString());
									}
								}
							}
							this._audiences = list;
						}
					}
				}
				return this._audiences;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000324C File Offset: 0x0000144C
		internal override IEnumerable<Claim> CreateClaims(string issuer)
		{
			return this.Payload.CreateClaims(issuer);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000325A File Offset: 0x0000145A
		public virtual IEnumerable<Claim> Claims
		{
			get
			{
				return this.Payload.Claims(this.Issuer ?? "LOCAL AUTHORITY");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003276 File Offset: 0x00001476
		public string Cty
		{
			get
			{
				if (this._cty == null)
				{
					this._cty = this.Header.GetStringValue("cty");
				}
				return this._cty;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000329C File Offset: 0x0000149C
		public string Enc
		{
			get
			{
				if (this._enc == null)
				{
					this._enc = this.Header.GetStringValue("enc");
				}
				return this._enc;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000032C2 File Offset: 0x000014C2
		public Claim GetClaim(string key)
		{
			return this.Payload.GetClaim(key, this.Issuer ?? "LOCAL AUTHORITY");
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000032DF File Offset: 0x000014DF
		public T GetHeaderValue<T>(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			return this.Header.GetValue<T>(key);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003300 File Offset: 0x00001500
		public T GetPayloadValue<T>(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			if (typeof(T).Equals(typeof(Claim)))
			{
				return (T)((object)this.GetClaim(key));
			}
			return this.Payload.GetValue<T>(key);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003354 File Offset: 0x00001554
		public override string Id
		{
			get
			{
				if (this._id == null)
				{
					this._id = this.Payload.GetStringValue("jti");
				}
				return this._id;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000337A File Offset: 0x0000157A
		public DateTime IssuedAt
		{
			get
			{
				if (this._iat == null)
				{
					this._iat = new DateTime?(this.Payload.GetDateTime("iat"));
				}
				return this._iat.Value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000033AF File Offset: 0x000015AF
		public override string Issuer
		{
			get
			{
				if (this._iss == null)
				{
					this._iss = this.Payload.GetStringValue("iss");
				}
				return this._iss;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000033D5 File Offset: 0x000015D5
		public string Kid
		{
			get
			{
				if (this._kid == null)
				{
					this._kid = this.Header.GetStringValue("kid");
				}
				return this._kid;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000033FB File Offset: 0x000015FB
		public string Subject
		{
			get
			{
				if (this._sub == null)
				{
					this._sub = this.Payload.GetStringValue("sub");
				}
				return this._sub;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003424 File Offset: 0x00001624
		public override string ToString()
		{
			int num = this.EncodedToken.LastIndexOf('.');
			if (num >= 0)
			{
				return this.EncodedToken.Substring(0, num);
			}
			return this.EncodedToken;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003457 File Offset: 0x00001657
		public bool TryGetClaim(string key, out Claim value)
		{
			return this.Payload.TryGetClaim(key, this.Issuer ?? "LOCAL AUTHORITY", out value);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003475 File Offset: 0x00001675
		public bool TryGetValue<T>(string key, out T value)
		{
			if (string.IsNullOrEmpty(key))
			{
				value = default(T);
				return false;
			}
			return this.Payload.TryGetValue<T>(key, out value);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003495 File Offset: 0x00001695
		public bool TryGetHeaderValue<T>(string key, out T value)
		{
			if (string.IsNullOrEmpty(key))
			{
				value = default(T);
				return false;
			}
			return this.Header.TryGetValue<T>(key, out value);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000034B8 File Offset: 0x000016B8
		public bool TryGetPayloadValue<T>(string key, out T value)
		{
			if (string.IsNullOrEmpty(key))
			{
				value = default(T);
				return false;
			}
			if (typeof(T).Equals(typeof(Claim)))
			{
				Claim claim;
				bool flag = this.TryGetClaim(key, out claim);
				value = (T)((object)claim);
				return flag;
			}
			return this.Payload.TryGetValue<T>(key, out value);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003514 File Offset: 0x00001714
		public string Typ
		{
			get
			{
				if (this._typ == null)
				{
					this._typ = this.Header.GetStringValue("typ");
				}
				return this._typ;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000353A File Offset: 0x0000173A
		public string X5t
		{
			get
			{
				if (this._x5t == null)
				{
					this._x5t = this.Header.GetStringValue("x5t");
				}
				return this._x5t;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003560 File Offset: 0x00001760
		public override DateTime ValidFrom
		{
			get
			{
				if (this._validFrom == null)
				{
					this._validFrom = new DateTime?(this.Payload.GetDateTime("nbf"));
				}
				return this._validFrom.Value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003595 File Offset: 0x00001795
		public override DateTime ValidTo
		{
			get
			{
				if (this._validTo == null)
				{
					this._validTo = new DateTime?(this.Payload.GetDateTime("exp"));
				}
				return this._validTo.Value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000035CA File Offset: 0x000017CA
		public string Zip
		{
			get
			{
				if (this._zip == null)
				{
					this._zip = this.Header.GetStringValue("zip");
				}
				return this._zip;
			}
		}

		// Token: 0x04000007 RID: 7
		private char[] _hChars;

		// Token: 0x04000008 RID: 8
		private ClaimsIdentity _claimsIdentity;

		// Token: 0x04000009 RID: 9
		private bool _wasClaimsIdentitySet;

		// Token: 0x0400000A RID: 10
		private string _act;

		// Token: 0x0400000B RID: 11
		private string _alg;

		// Token: 0x0400000C RID: 12
		private IList<string> _audiences;

		// Token: 0x0400000D RID: 13
		private readonly object _audiencesLock = new object();

		// Token: 0x0400000E RID: 14
		private string _authenticationTag;

		// Token: 0x0400000F RID: 15
		private string _ciphertext;

		// Token: 0x04000010 RID: 16
		private string _cty;

		// Token: 0x04000011 RID: 17
		private string _enc;

		// Token: 0x04000012 RID: 18
		private string _encodedHeader;

		// Token: 0x04000013 RID: 19
		private string _encodedPayload;

		// Token: 0x04000014 RID: 20
		private string _encodedSignature;

		// Token: 0x04000015 RID: 21
		private string _encryptedKey;

		// Token: 0x04000016 RID: 22
		private DateTime? _iat;

		// Token: 0x04000017 RID: 23
		private string _id;

		// Token: 0x04000018 RID: 24
		private string _initializationVector;

		// Token: 0x04000019 RID: 25
		private string _iss;

		// Token: 0x0400001A RID: 26
		private string _kid;

		// Token: 0x0400001B RID: 27
		private string _sub;

		// Token: 0x0400001C RID: 28
		private string _typ;

		// Token: 0x0400001D RID: 29
		private DateTime? _validFrom;

		// Token: 0x0400001E RID: 30
		private DateTime? _validTo;

		// Token: 0x0400001F RID: 31
		private string _x5t;

		// Token: 0x04000020 RID: 32
		private string _zip;
	}
}
