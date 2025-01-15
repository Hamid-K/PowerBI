using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200011A RID: 282
	internal class AsymmetricAdapter : IDisposable
	{
		// Token: 0x06000DFC RID: 3580 RVA: 0x0003757A File Offset: 0x0003577A
		internal AsymmetricAdapter(SecurityKey key, string algorithm, bool requirePrivateKey)
			: this(key, algorithm, null, requirePrivateKey)
		{
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00037588 File Offset: 0x00035788
		internal AsymmetricAdapter(SecurityKey key, string algorithm, HashAlgorithm hashAlgorithm, bool requirePrivateKey)
		{
			DecryptDelegate decryptDelegate;
			if ((decryptDelegate = AsymmetricAdapter.<>O.<0>__DecryptFunctionNotFound) == null)
			{
				decryptDelegate = (AsymmetricAdapter.<>O.<0>__DecryptFunctionNotFound = new DecryptDelegate(AsymmetricAdapter.DecryptFunctionNotFound));
			}
			this.DecryptFunction = decryptDelegate;
			EncryptDelegate encryptDelegate;
			if ((encryptDelegate = AsymmetricAdapter.<>O.<1>__EncryptFunctionNotFound) == null)
			{
				encryptDelegate = (AsymmetricAdapter.<>O.<1>__EncryptFunctionNotFound = new EncryptDelegate(AsymmetricAdapter.EncryptFunctionNotFound));
			}
			this.EncryptFunction = encryptDelegate;
			SignDelegate signDelegate;
			if ((signDelegate = AsymmetricAdapter.<>O.<2>__SignatureFunctionNotFound) == null)
			{
				signDelegate = (AsymmetricAdapter.<>O.<2>__SignatureFunctionNotFound = new SignDelegate(AsymmetricAdapter.SignatureFunctionNotFound));
			}
			this.SignatureFunction = signDelegate;
			VerifyDelegate verifyDelegate;
			if ((verifyDelegate = AsymmetricAdapter.<>O.<3>__VerifyFunctionNotFound) == null)
			{
				verifyDelegate = (AsymmetricAdapter.<>O.<3>__VerifyFunctionNotFound = new VerifyDelegate(AsymmetricAdapter.VerifyFunctionNotFound));
			}
			this.VerifyFunction = verifyDelegate;
			VerifyDelegateWithLength verifyDelegateWithLength;
			if ((verifyDelegateWithLength = AsymmetricAdapter.<>O.<4>__VerifyFunctionWithLengthNotFound) == null)
			{
				verifyDelegateWithLength = (AsymmetricAdapter.<>O.<4>__VerifyFunctionWithLengthNotFound = new VerifyDelegateWithLength(AsymmetricAdapter.VerifyFunctionWithLengthNotFound));
			}
			this.VerifyFunctionWithLength = verifyDelegateWithLength;
			base..ctor();
			this.HashAlgorithm = hashAlgorithm;
			RsaSecurityKey rsaSecurityKey = key as RsaSecurityKey;
			if (rsaSecurityKey != null)
			{
				this.InitializeUsingRsaSecurityKey(rsaSecurityKey, algorithm);
				return;
			}
			X509SecurityKey x509SecurityKey = key as X509SecurityKey;
			if (x509SecurityKey != null)
			{
				this.InitializeUsingX509SecurityKey(x509SecurityKey, algorithm, requirePrivateKey);
				return;
			}
			JsonWebKey jsonWebKey = key as JsonWebKey;
			if (jsonWebKey != null)
			{
				SecurityKey securityKey;
				if (!JsonWebKeyConverter.TryConvertToSecurityKey(jsonWebKey, out securityKey))
				{
					return;
				}
				RsaSecurityKey rsaSecurityKey2 = securityKey as RsaSecurityKey;
				if (rsaSecurityKey2 != null)
				{
					this.InitializeUsingRsaSecurityKey(rsaSecurityKey2, algorithm);
					return;
				}
				X509SecurityKey x509SecurityKey2 = securityKey as X509SecurityKey;
				if (x509SecurityKey2 != null)
				{
					this.InitializeUsingX509SecurityKey(x509SecurityKey2, algorithm, requirePrivateKey);
					return;
				}
				ECDsaSecurityKey ecdsaSecurityKey = securityKey as ECDsaSecurityKey;
				if (ecdsaSecurityKey != null)
				{
					this.InitializeUsingEcdsaSecurityKey(ecdsaSecurityKey);
					return;
				}
				throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10684: Unable to convert the JsonWebKey to an AsymmetricSecurityKey. Algorithm: '{0}', Key: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII(algorithm),
					key
				})));
			}
			else
			{
				ECDsaSecurityKey ecdsaSecurityKey2 = key as ECDsaSecurityKey;
				if (ecdsaSecurityKey2 != null)
				{
					this.InitializeUsingEcdsaSecurityKey(ecdsaSecurityKey2);
					return;
				}
				throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10684: Unable to convert the JsonWebKey to an AsymmetricSecurityKey. Algorithm: '{0}', Key: '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII(algorithm),
					key
				})));
			}
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0003772E File Offset: 0x0003592E
		internal byte[] Decrypt(byte[] data)
		{
			return this.DecryptFunction(data);
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0003773C File Offset: 0x0003593C
		internal static byte[] DecryptFunctionNotFound(byte[] _)
		{
			throw LogHelper.LogExceptionMessage(new NotSupportedException("IDX10711: Unable to Decrypt, Internal DecryptionFunction is not available."));
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0003774D File Offset: 0x0003594D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0003775C File Offset: 0x0003595C
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this._disposed = true;
				if (disposing && this._disposeCryptoOperators)
				{
					if (this.ECDsa != null)
					{
						this.ECDsa.Dispose();
					}
					if (this.RsaCryptoServiceProviderProxy != null)
					{
						this.RsaCryptoServiceProviderProxy.Dispose();
					}
					if (this.RSA != null)
					{
						this.RSA.Dispose();
					}
				}
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x000377BC File Offset: 0x000359BC
		// (set) Token: 0x06000E03 RID: 3587 RVA: 0x000377C4 File Offset: 0x000359C4
		private ECDsa ECDsa { get; set; }

		// Token: 0x06000E04 RID: 3588 RVA: 0x000377CD File Offset: 0x000359CD
		internal byte[] Encrypt(byte[] data)
		{
			return this.EncryptFunction(data);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x000377DB File Offset: 0x000359DB
		internal static byte[] EncryptFunctionNotFound(byte[] _)
		{
			throw LogHelper.LogExceptionMessage(new NotSupportedException("IDX10712: Unable to Encrypt, Internal EncryptionFunction is not available."));
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x000377EC File Offset: 0x000359EC
		// (set) Token: 0x06000E07 RID: 3591 RVA: 0x000377F4 File Offset: 0x000359F4
		private HashAlgorithm HashAlgorithm { get; set; }

		// Token: 0x06000E08 RID: 3592 RVA: 0x00037800 File Offset: 0x00035A00
		private void InitializeUsingEcdsaSecurityKey(ECDsaSecurityKey ecdsaSecurityKey)
		{
			this.ECDsa = ecdsaSecurityKey.ECDsa;
			this.SignatureFunction = new SignDelegate(this.SignWithECDsa);
			this.VerifyFunction = new VerifyDelegate(this.VerifyWithECDsa);
			this.VerifyFunctionWithLength = new VerifyDelegateWithLength(this.VerifyWithECDsaWithLength);
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00037850 File Offset: 0x00035A50
		private void InitializeUsingRsa(RSA rsa, string algorithm)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = rsa as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null)
			{
				this._useRSAOeapPadding = algorithm.Equals("RSA-OAEP") || algorithm.Equals("http://www.w3.org/2001/04/xmlenc#rsa-oaep");
				this.RsaCryptoServiceProviderProxy = new RSACryptoServiceProviderProxy(rsacryptoServiceProvider);
				this.DecryptFunction = new DecryptDelegate(this.DecryptWithRsaCryptoServiceProviderProxy);
				this.EncryptFunction = new EncryptDelegate(this.EncryptWithRsaCryptoServiceProviderProxy);
				this.SignatureFunction = new SignDelegate(this.SignWithRsaCryptoServiceProviderProxy);
				this.VerifyFunction = new VerifyDelegate(this.VerifyWithRsaCryptoServiceProviderProxy);
				this.VerifyFunctionWithLength = new VerifyDelegateWithLength(this.VerifyWithRsaCryptoServiceProviderProxyWithLength);
				this._disposeCryptoOperators = true;
				return;
			}
			if (algorithm.Equals("PS256") || algorithm.Equals("http://www.w3.org/2007/05/xmldsig-more#sha256-rsa-MGF1") || algorithm.Equals("PS384") || algorithm.Equals("http://www.w3.org/2007/05/xmldsig-more#sha384-rsa-MGF1") || algorithm.Equals("PS512") || algorithm.Equals("http://www.w3.org/2007/05/xmldsig-more#sha512-rsa-MGF1"))
			{
				this.RSASignaturePadding = RSASignaturePadding.Pss;
			}
			else
			{
				this.RSASignaturePadding = RSASignaturePadding.Pkcs1;
			}
			this.RSAEncryptionPadding = ((algorithm.Equals("RSA-OAEP") || algorithm.Equals("http://www.w3.org/2001/04/xmlenc#rsa-oaep")) ? RSAEncryptionPadding.OaepSHA1 : RSAEncryptionPadding.Pkcs1);
			this.RSA = rsa;
			this.DecryptFunction = new DecryptDelegate(this.DecryptWithRsa);
			this.EncryptFunction = new EncryptDelegate(this.EncryptWithRsa);
			this.SignatureFunction = new SignDelegate(this.SignWithRsa);
			this.VerifyFunction = new VerifyDelegate(this.VerifyWithRsa);
			this.VerifyFunctionWithLength = new VerifyDelegateWithLength(this.VerifyWithRsaWithLength);
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x000379EC File Offset: 0x00035BEC
		private void InitializeUsingRsaSecurityKey(RsaSecurityKey rsaSecurityKey, string algorithm)
		{
			if (rsaSecurityKey.Rsa != null)
			{
				this.InitializeUsingRsa(rsaSecurityKey.Rsa, algorithm);
				return;
			}
			RSA rsa = RSA.Create();
			rsa.ImportParameters(rsaSecurityKey.Parameters);
			this.InitializeUsingRsa(rsa, algorithm);
			this._disposeCryptoOperators = true;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00037A30 File Offset: 0x00035C30
		private void InitializeUsingX509SecurityKey(X509SecurityKey x509SecurityKey, string algorithm, bool requirePrivateKey)
		{
			if (requirePrivateKey)
			{
				this.InitializeUsingRsa(x509SecurityKey.PrivateKey as RSA, algorithm);
				return;
			}
			this.InitializeUsingRsa(x509SecurityKey.PublicKey as RSA, algorithm);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00037A5A File Offset: 0x00035C5A
		// (set) Token: 0x06000E0D RID: 3597 RVA: 0x00037A62 File Offset: 0x00035C62
		private RSA RSA { get; set; }

		// Token: 0x06000E0E RID: 3598 RVA: 0x00037A6B File Offset: 0x00035C6B
		internal byte[] Sign(byte[] bytes)
		{
			return this.SignatureFunction(bytes);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00037A79 File Offset: 0x00035C79
		private static byte[] SignatureFunctionNotFound(byte[] _)
		{
			throw LogHelper.LogExceptionMessage(new CryptographicException("IDX10685: Unable to Sign, Internal SignFunction is not available."));
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00037A8A File Offset: 0x00035C8A
		private byte[] SignWithECDsa(byte[] bytes)
		{
			return this.ECDsa.SignHash(this.HashAlgorithm.ComputeHash(bytes));
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x00037AA3 File Offset: 0x00035CA3
		internal bool Verify(byte[] bytes, byte[] signature)
		{
			return this.VerifyFunction(bytes, signature);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00037AB2 File Offset: 0x00035CB2
		internal bool Verify(byte[] bytes, int start, int length, byte[] signature)
		{
			return this.VerifyFunctionWithLength(bytes, start, length, signature);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00037AC4 File Offset: 0x00035CC4
		private static bool VerifyFunctionNotFound(byte[] bytes, byte[] signature)
		{
			throw LogHelper.LogExceptionMessage(new NotSupportedException("IDX10686: Unable to Verify, Internal VerifyFunction is not available."));
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00037AD5 File Offset: 0x00035CD5
		private static bool VerifyFunctionWithLengthNotFound(byte[] bytes, int start, int length, byte[] signature)
		{
			throw LogHelper.LogExceptionMessage(new NotSupportedException("IDX10686: Unable to Verify, Internal VerifyFunction is not available."));
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00037AE6 File Offset: 0x00035CE6
		private bool VerifyWithECDsa(byte[] bytes, byte[] signature)
		{
			return this.ECDsa.VerifyHash(this.HashAlgorithm.ComputeHash(bytes), signature);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00037B00 File Offset: 0x00035D00
		private bool VerifyWithECDsaWithLength(byte[] bytes, int start, int length, byte[] signature)
		{
			return this.ECDsa.VerifyHash(this.HashAlgorithm.ComputeHash(bytes, start, length), signature);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00037B1D File Offset: 0x00035D1D
		internal AsymmetricAdapter(SecurityKey key, string algorithm, HashAlgorithm hashAlgorithm, HashAlgorithmName hashAlgorithmName, bool requirePrivateKey)
			: this(key, algorithm, hashAlgorithm, requirePrivateKey)
		{
			this.HashAlgorithmName = hashAlgorithmName;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00037B32 File Offset: 0x00035D32
		private byte[] DecryptWithRsa(byte[] bytes)
		{
			return this.RSA.Decrypt(bytes, this.RSAEncryptionPadding);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00037B46 File Offset: 0x00035D46
		private byte[] EncryptWithRsa(byte[] bytes)
		{
			return this.RSA.Encrypt(bytes, this.RSAEncryptionPadding);
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00037B5A File Offset: 0x00035D5A
		// (set) Token: 0x06000E1B RID: 3611 RVA: 0x00037B62 File Offset: 0x00035D62
		private HashAlgorithmName HashAlgorithmName { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x00037B6B File Offset: 0x00035D6B
		// (set) Token: 0x06000E1D RID: 3613 RVA: 0x00037B73 File Offset: 0x00035D73
		private RSAEncryptionPadding RSAEncryptionPadding { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00037B7C File Offset: 0x00035D7C
		// (set) Token: 0x06000E1F RID: 3615 RVA: 0x00037B84 File Offset: 0x00035D84
		private RSASignaturePadding RSASignaturePadding { get; set; }

		// Token: 0x06000E20 RID: 3616 RVA: 0x00037B8D File Offset: 0x00035D8D
		private byte[] SignWithRsa(byte[] bytes)
		{
			return this.RSA.SignHash(this.HashAlgorithm.ComputeHash(bytes), this.HashAlgorithmName, this.RSASignaturePadding);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00037BB2 File Offset: 0x00035DB2
		private bool VerifyWithRsa(byte[] bytes, byte[] signature)
		{
			return this.RSA.VerifyHash(this.HashAlgorithm.ComputeHash(bytes), signature, this.HashAlgorithmName, this.RSASignaturePadding);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00037BD8 File Offset: 0x00035DD8
		private bool VerifyWithRsaWithLength(byte[] bytes, int start, int length, byte[] signature)
		{
			return this.RSA.VerifyHash(this.HashAlgorithm.ComputeHash(bytes, start, length), signature, this.HashAlgorithmName, this.RSASignaturePadding);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00037C01 File Offset: 0x00035E01
		internal byte[] DecryptWithRsaCryptoServiceProviderProxy(byte[] bytes)
		{
			return this.RsaCryptoServiceProviderProxy.Decrypt(bytes, this._useRSAOeapPadding);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00037C15 File Offset: 0x00035E15
		internal byte[] EncryptWithRsaCryptoServiceProviderProxy(byte[] bytes)
		{
			return this.RsaCryptoServiceProviderProxy.Encrypt(bytes, this._useRSAOeapPadding);
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x00037C29 File Offset: 0x00035E29
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x00037C31 File Offset: 0x00035E31
		private RSACryptoServiceProviderProxy RsaCryptoServiceProviderProxy { get; set; }

		// Token: 0x06000E27 RID: 3623 RVA: 0x00037C3A File Offset: 0x00035E3A
		internal byte[] SignWithRsaCryptoServiceProviderProxy(byte[] bytes)
		{
			return this.RsaCryptoServiceProviderProxy.SignData(bytes, this.HashAlgorithm);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00037C4E File Offset: 0x00035E4E
		private bool VerifyWithRsaCryptoServiceProviderProxy(byte[] bytes, byte[] signature)
		{
			return this.RsaCryptoServiceProviderProxy.VerifyData(bytes, this.HashAlgorithm, signature);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00037C63 File Offset: 0x00035E63
		private bool VerifyWithRsaCryptoServiceProviderProxyWithLength(byte[] bytes, int offset, int length, byte[] signature)
		{
			return this.RsaCryptoServiceProviderProxy.VerifyDataWithLength(bytes, offset, length, this.HashAlgorithm, this.HashAlgorithmName, signature);
		}

		// Token: 0x04000464 RID: 1124
		private bool _useRSAOeapPadding;

		// Token: 0x04000465 RID: 1125
		private bool _disposeCryptoOperators;

		// Token: 0x04000466 RID: 1126
		private bool _disposed;

		// Token: 0x04000467 RID: 1127
		private DecryptDelegate DecryptFunction;

		// Token: 0x04000468 RID: 1128
		private EncryptDelegate EncryptFunction;

		// Token: 0x04000469 RID: 1129
		private SignDelegate SignatureFunction;

		// Token: 0x0400046A RID: 1130
		private VerifyDelegate VerifyFunction;

		// Token: 0x0400046B RID: 1131
		private VerifyDelegateWithLength VerifyFunctionWithLength;

		// Token: 0x0200026C RID: 620
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000B1F RID: 2847
			public static DecryptDelegate <0>__DecryptFunctionNotFound;

			// Token: 0x04000B20 RID: 2848
			public static EncryptDelegate <1>__EncryptFunctionNotFound;

			// Token: 0x04000B21 RID: 2849
			public static SignDelegate <2>__SignatureFunctionNotFound;

			// Token: 0x04000B22 RID: 2850
			public static VerifyDelegate <3>__VerifyFunctionNotFound;

			// Token: 0x04000B23 RID: 2851
			public static VerifyDelegateWithLength <4>__VerifyFunctionWithLengthNotFound;
		}
	}
}
