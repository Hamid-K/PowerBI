using System;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200016F RID: 367
	public class RsaSecurityKey : AsymmetricSecurityKey
	{
		// Token: 0x06001091 RID: 4241 RVA: 0x000406C5 File Offset: 0x0003E8C5
		internal RsaSecurityKey(JsonWebKey webKey)
			: base(webKey)
		{
			this.IntializeWithRsaParameters(webKey.CreateRsaParameters());
			webKey.ConvertedSecurityKey = this;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x000406E1 File Offset: 0x0003E8E1
		public RsaSecurityKey(RSAParameters rsaParameters)
		{
			this.IntializeWithRsaParameters(rsaParameters);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000406F0 File Offset: 0x0003E8F0
		internal void IntializeWithRsaParameters(RSAParameters rsaParameters)
		{
			if (rsaParameters.Modulus == null)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10700: {0} is unable to use 'rsaParameters'. {1} is null.", new object[]
				{
					LogHelper.MarkAsNonPII("Microsoft.IdentityModel.Tokens.RsaSecurityKey"),
					LogHelper.MarkAsNonPII("Modulus")
				})));
			}
			if (rsaParameters.Exponent == null)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10700: {0} is unable to use 'rsaParameters'. {1} is null.", new object[]
				{
					LogHelper.MarkAsNonPII("Microsoft.IdentityModel.Tokens.RsaSecurityKey"),
					LogHelper.MarkAsNonPII("Exponent")
				})));
			}
			this._hasPrivateKey = new bool?(rsaParameters.D != null && rsaParameters.DP != null && rsaParameters.DQ != null && rsaParameters.P != null && rsaParameters.Q != null && rsaParameters.InverseQ != null);
			this._foundPrivateKey = (this._hasPrivateKey.Value ? PrivateKeyStatus.Exists : PrivateKeyStatus.DoesNotExist);
			this._foundPrivateKeyDetermined = true;
			this.Parameters = rsaParameters;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x000407DB File Offset: 0x0003E9DB
		public RsaSecurityKey(RSA rsa)
		{
			if (rsa == null)
			{
				throw LogHelper.LogArgumentNullException("rsa");
			}
			this.Rsa = rsa;
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x000407FC File Offset: 0x0003E9FC
		[Obsolete("HasPrivateKey method is deprecated, please use FoundPrivateKey instead.")]
		public override bool HasPrivateKey
		{
			get
			{
				if (this._hasPrivateKey == null)
				{
					try
					{
						byte[] array = new byte[20];
						this.Rsa.SignData(array, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
						this._hasPrivateKey = new bool?(true);
					}
					catch (CryptographicException)
					{
						this._hasPrivateKey = new bool?(false);
					}
				}
				return this._hasPrivateKey.Value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x00040870 File Offset: 0x0003EA70
		public override PrivateKeyStatus PrivateKeyStatus
		{
			get
			{
				if (this._foundPrivateKeyDetermined)
				{
					return this._foundPrivateKey;
				}
				this._foundPrivateKeyDetermined = true;
				if (this.Rsa != null)
				{
					try
					{
						RSAParameters rsaparameters = this.Rsa.ExportParameters(true);
						if (rsaparameters.D != null && rsaparameters.DP != null && rsaparameters.DQ != null && rsaparameters.P != null && rsaparameters.Q != null && rsaparameters.InverseQ != null)
						{
							this._foundPrivateKey = PrivateKeyStatus.Exists;
						}
						else
						{
							this._foundPrivateKey = PrivateKeyStatus.DoesNotExist;
						}
						goto IL_00DC;
					}
					catch (Exception)
					{
						this._foundPrivateKey = PrivateKeyStatus.Unknown;
						return this._foundPrivateKey;
					}
				}
				if (this.Parameters.D != null && this.Parameters.DP != null && this.Parameters.DQ != null && this.Parameters.P != null && this.Parameters.Q != null && this.Parameters.InverseQ != null)
				{
					this._foundPrivateKey = PrivateKeyStatus.Exists;
				}
				else
				{
					this._foundPrivateKey = PrivateKeyStatus.DoesNotExist;
				}
				IL_00DC:
				return this._foundPrivateKey;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x00040974 File Offset: 0x0003EB74
		public override int KeySize
		{
			get
			{
				if (this.Rsa != null)
				{
					return this.Rsa.KeySize;
				}
				if (this.Parameters.Modulus != null)
				{
					return this.Parameters.Modulus.Length * 8;
				}
				return 0;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x000409A8 File Offset: 0x0003EBA8
		// (set) Token: 0x06001099 RID: 4249 RVA: 0x000409B0 File Offset: 0x0003EBB0
		public RSAParameters Parameters { get; private set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x000409B9 File Offset: 0x0003EBB9
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x000409C1 File Offset: 0x0003EBC1
		public RSA Rsa { get; private set; }

		// Token: 0x0600109C RID: 4252 RVA: 0x000409CA File Offset: 0x0003EBCA
		public override bool CanComputeJwkThumbprint()
		{
			return this.Rsa != null || (this.Parameters.Exponent != null && this.Parameters.Modulus != null);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x000409F4 File Offset: 0x0003EBF4
		public override byte[] ComputeJwkThumbprint()
		{
			RSAParameters rsaparameters = this.Parameters;
			if (rsaparameters.Exponent == null || rsaparameters.Modulus == null)
			{
				rsaparameters = this.Rsa.ExportParameters(false);
			}
			return Utility.GenerateSha256Hash(string.Concat(new string[]
			{
				"{\"e\":\"",
				Base64UrlEncoder.Encode(rsaparameters.Exponent),
				"\",\"kty\":\"RSA\",\"n\":\"",
				Base64UrlEncoder.Encode(rsaparameters.Modulus),
				"\"}"
			}));
		}

		// Token: 0x0400061D RID: 1565
		private bool? _hasPrivateKey;

		// Token: 0x0400061E RID: 1566
		private bool _foundPrivateKeyDetermined;

		// Token: 0x0400061F RID: 1567
		private PrivateKeyStatus _foundPrivateKey;

		// Token: 0x04000620 RID: 1568
		private const string _className = "Microsoft.IdentityModel.Tokens.RsaSecurityKey";
	}
}
