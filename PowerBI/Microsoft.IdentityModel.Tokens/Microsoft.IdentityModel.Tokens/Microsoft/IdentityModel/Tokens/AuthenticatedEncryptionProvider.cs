using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000133 RID: 307
	public class AuthenticatedEncryptionProvider : IDisposable
	{
		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003B5AC File Offset: 0x000397AC
		public AuthenticatedEncryptionProvider(SecurityKey key, string algorithm)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			if (string.IsNullOrWhiteSpace(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			this.Key = key;
			this.Algorithm = algorithm;
			this._cryptoProviderFactory = key.CryptoProviderFactory;
			if (!SupportedAlgorithms.IsSupportedEncryptionAlgorithm(algorithm, key))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10668: Unable to create '{0}', algorithm '{1}'; key: '{2}' is not supported.", new object[]
				{
					LogHelper.MarkAsNonPII("Microsoft.IdentityModel.Tokens.AuthenticatedEncryptionProvider"),
					LogHelper.MarkAsNonPII(algorithm),
					key
				})));
			}
			if (SupportedAlgorithms.IsAesGcm(algorithm))
			{
				this.InitializeUsingAesGcm();
				return;
			}
			this.InitializeUsingAesCbc();
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003B650 File Offset: 0x00039850
		private void InitializeUsingAesGcm()
		{
			this._keySizeIsValid = new Lazy<bool>(new Func<bool>(this.ValidKeySize));
			this._aesGcmObjectPool = new DisposableObjectPool<AesGcm>(new Func<AesGcm>(this.CreateAesGcmInstance));
			this.EncryptFunction = new EncryptionDelegate(this.EncryptWithAesGcm);
			this.DecryptFunction = new DecryptionDelegate(this.DecryptWithAesGcm);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003B6B0 File Offset: 0x000398B0
		private void InitializeUsingAesCbc()
		{
			this._authenticatedkeys = new Lazy<AuthenticatedEncryptionProvider.AuthenticatedKeys>(new Func<AuthenticatedEncryptionProvider.AuthenticatedKeys>(this.CreateAuthenticatedKeys));
			this._symmetricSignatureProvider = new Lazy<SymmetricSignatureProvider>(new Func<SymmetricSignatureProvider>(this.CreateSymmetricSignatureProvider));
			this.EncryptFunction = new EncryptionDelegate(this.EncryptWithAesCbc);
			this.DecryptFunction = new DecryptionDelegate(this.DecryptWithAesCbc);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0003B70F File Offset: 0x0003990F
		internal bool ValidKeySize()
		{
			this.ValidateKeySize(this.Key, this.Algorithm);
			return true;
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0003B724 File Offset: 0x00039924
		private AuthenticatedEncryptionResult EncryptWithAesGcm(byte[] plaintext, byte[] authenticatedData, byte[] iv)
		{
			throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10715: Encryption using algorithm: '{0}' is not supported.", new object[] { LogHelper.MarkAsNonPII(this.Algorithm) })));
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003B74E File Offset: 0x0003994E
		private AesGcm CreateAesGcmInstance()
		{
			return new AesGcm(this.GetKeyBytes(this.Key));
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003B764 File Offset: 0x00039964
		private byte[] DecryptWithAesGcm(byte[] ciphertext, byte[] authenticatedData, byte[] iv, byte[] authenticationTag)
		{
			bool value = this._keySizeIsValid.Value;
			byte[] array = new byte[ciphertext.Length];
			AesGcm aesGcm = null;
			try
			{
				aesGcm = this._aesGcmObjectPool.Allocate();
				aesGcm.Decrypt(iv, ciphertext, authenticationTag, array, authenticatedData);
			}
			catch
			{
				this.Dispose(true);
				throw;
			}
			finally
			{
				if (!this._disposed)
				{
					this._aesGcmObjectPool.Free(aesGcm);
				}
			}
			return array;
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003B7E0 File Offset: 0x000399E0
		private AuthenticatedEncryptionResult EncryptWithAesCbc(byte[] plaintext, byte[] authenticatedData, byte[] iv)
		{
			AuthenticatedEncryptionResult authenticatedEncryptionResult;
			using (Aes aes = Aes.Create())
			{
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;
				aes.Key = this._authenticatedkeys.Value.AesKey.Key;
				if (iv != null)
				{
					aes.IV = iv;
				}
				byte[] array;
				try
				{
					array = AuthenticatedEncryptionProvider.Transform(aes.CreateEncryptor(), plaintext, 0, plaintext.Length);
				}
				catch (Exception ex)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenEncryptionFailedException(LogHelper.FormatInvariant("IDX10654: Decryption failed. Cryptographic operation exception: '{0}'.", new object[] { ex })));
				}
				byte[] array2 = Utility.ConvertToBigEndian((long)(authenticatedData.Length * 8));
				byte[] array3 = new byte[authenticatedData.Length + aes.IV.Length + array.Length + array2.Length];
				Array.Copy(authenticatedData, 0, array3, 0, authenticatedData.Length);
				Array.Copy(aes.IV, 0, array3, authenticatedData.Length, aes.IV.Length);
				Array.Copy(array, 0, array3, authenticatedData.Length + aes.IV.Length, array.Length);
				Array.Copy(array2, 0, array3, authenticatedData.Length + aes.IV.Length + array.Length, array2.Length);
				Array array4 = this._symmetricSignatureProvider.Value.Sign(array3);
				byte[] array5 = new byte[this._authenticatedkeys.Value.HmacKey.Key.Length];
				Array.Copy(array4, array5, array5.Length);
				authenticatedEncryptionResult = new AuthenticatedEncryptionResult(this.Key, array, aes.IV, array5);
			}
			return authenticatedEncryptionResult;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003B96C File Offset: 0x00039B6C
		private byte[] DecryptWithAesCbc(byte[] ciphertext, byte[] authenticatedData, byte[] iv, byte[] authenticationTag)
		{
			byte[] array = Utility.ConvertToBigEndian((long)(authenticatedData.Length * 8));
			byte[] array2 = new byte[authenticatedData.Length + iv.Length + ciphertext.Length + array.Length];
			Array.Copy(authenticatedData, 0, array2, 0, authenticatedData.Length);
			Array.Copy(iv, 0, array2, authenticatedData.Length, iv.Length);
			Array.Copy(ciphertext, 0, array2, authenticatedData.Length + iv.Length, ciphertext.Length);
			Array.Copy(array, 0, array2, authenticatedData.Length + iv.Length + ciphertext.Length, array.Length);
			if (!this._symmetricSignatureProvider.Value.Verify(array2, 0, array2.Length, authenticationTag, 0, this._authenticatedkeys.Value.HmacKey.Key.Length, this.Algorithm))
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenDecryptionFailedException(LogHelper.FormatInvariant("IDX10650: Failed to verify ciphertext with aad '{0}'; iv '{1}'; and authenticationTag '{2}'.", new object[]
				{
					Base64UrlEncoder.Encode(authenticatedData),
					Base64UrlEncoder.Encode(iv),
					Base64UrlEncoder.Encode(authenticationTag)
				})));
			}
			byte[] array3;
			using (Aes aes = Aes.Create())
			{
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;
				aes.Key = this._authenticatedkeys.Value.AesKey.Key;
				aes.IV = iv;
				try
				{
					array3 = AuthenticatedEncryptionProvider.Transform(aes.CreateDecryptor(), ciphertext, 0, ciphertext.Length);
				}
				catch (Exception ex)
				{
					throw LogHelper.LogExceptionMessage(new SecurityTokenDecryptionFailedException(LogHelper.FormatInvariant("IDX10654: Decryption failed. Cryptographic operation exception: '{0}'.", new object[] { ex })));
				}
			}
			return array3;
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0003BADC File Offset: 0x00039CDC
		private AuthenticatedEncryptionProvider.AuthenticatedKeys CreateAuthenticatedKeys()
		{
			this.ValidateKeySize(this.Key, this.Algorithm);
			return this.GetAlgorithmParameters(this.Key, this.Algorithm);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0003BB04 File Offset: 0x00039D04
		internal SymmetricSignatureProvider CreateSymmetricSignatureProvider()
		{
			if (!this.IsSupportedAlgorithm(this.Key, this.Algorithm))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10668: Unable to create '{0}', algorithm '{1}'; key: '{2}' is not supported.", new object[]
				{
					LogHelper.MarkAsNonPII("Microsoft.IdentityModel.Tokens.AuthenticatedEncryptionProvider"),
					LogHelper.MarkAsNonPII(this.Algorithm),
					this.Key
				})));
			}
			this.ValidateKeySize(this.Key, this.Algorithm);
			SymmetricSignatureProvider symmetricSignatureProvider;
			if (this.Key.CryptoProviderFactory.GetType() == typeof(CryptoProviderFactory))
			{
				symmetricSignatureProvider = this.Key.CryptoProviderFactory.CreateForSigning(this._authenticatedkeys.Value.HmacKey, this.Algorithm, false) as SymmetricSignatureProvider;
			}
			else
			{
				symmetricSignatureProvider = this.Key.CryptoProviderFactory.CreateForSigning(this._authenticatedkeys.Value.HmacKey, this.Algorithm) as SymmetricSignatureProvider;
			}
			if (symmetricSignatureProvider == null)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10649: Failed to create a SymmetricSignatureProvider for the algorithm '{0}'.", new object[] { LogHelper.MarkAsNonPII(this.Algorithm) })));
			}
			return symmetricSignatureProvider;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0003BC1F File Offset: 0x00039E1F
		public string Algorithm { get; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x0003BC27 File Offset: 0x00039E27
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x0003BC2F File Offset: 0x00039E2F
		public string Context { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x0003BC38 File Offset: 0x00039E38
		public SecurityKey Key { get; }

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003BC40 File Offset: 0x00039E40
		public virtual AuthenticatedEncryptionResult Encrypt(byte[] plaintext, byte[] authenticatedData)
		{
			return this.Encrypt(plaintext, authenticatedData, null);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0003BC4C File Offset: 0x00039E4C
		public virtual AuthenticatedEncryptionResult Encrypt(byte[] plaintext, byte[] authenticatedData, byte[] iv)
		{
			if (plaintext == null || plaintext.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("plaintext");
			}
			if (authenticatedData == null || authenticatedData.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("authenticatedData");
			}
			if (this._disposed)
			{
				throw LogHelper.LogExceptionMessage(new ObjectDisposedException(base.GetType().ToString()));
			}
			return this.EncryptFunction(plaintext, authenticatedData, iv);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0003BCAC File Offset: 0x00039EAC
		public virtual byte[] Decrypt(byte[] ciphertext, byte[] authenticatedData, byte[] iv, byte[] authenticationTag)
		{
			if (ciphertext == null || ciphertext.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("ciphertext");
			}
			if (authenticatedData == null || authenticatedData.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("authenticatedData");
			}
			if (iv == null || iv.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("iv");
			}
			if (authenticationTag == null || authenticationTag.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("authenticationTag");
			}
			if (this._disposed)
			{
				throw LogHelper.LogExceptionMessage(new ObjectDisposedException(base.GetType().ToString()));
			}
			return this.DecryptFunction(ciphertext, authenticatedData, iv, authenticationTag);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0003BD31 File Offset: 0x00039F31
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0003BD40 File Offset: 0x00039F40
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this._disposed = true;
				if (disposing)
				{
					if (this._symmetricSignatureProvider != null)
					{
						this._cryptoProviderFactory.ReleaseSignatureProvider(this._symmetricSignatureProvider.Value);
					}
					if (this._aesGcmObjectPool != null)
					{
						DisposableObjectPool<AesGcm>.Element[] items = this._aesGcmObjectPool.Items;
						for (int i = 0; i < items.Length; i++)
						{
							AesGcm value = items[i].Value;
							if (value != null)
							{
								value.Dispose();
							}
						}
					}
				}
			}
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0003BDB6 File Offset: 0x00039FB6
		protected virtual bool IsSupportedAlgorithm(SecurityKey key, string algorithm)
		{
			return SupportedAlgorithms.IsSupportedEncryptionAlgorithm(algorithm, key);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0003BDC0 File Offset: 0x00039FC0
		private AuthenticatedEncryptionProvider.AuthenticatedKeys GetAlgorithmParameters(SecurityKey key, string algorithm)
		{
			int num;
			if (algorithm.Equals("A256CBC-HS512"))
			{
				num = 32;
			}
			else if (algorithm.Equals("A192CBC-HS384"))
			{
				num = 24;
			}
			else
			{
				if (!algorithm.Equals("A128CBC-HS256"))
				{
					throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10668: Unable to create '{0}', algorithm '{1}'; key: '{2}' is not supported.", new object[]
					{
						LogHelper.MarkAsNonPII("Microsoft.IdentityModel.Tokens.AuthenticatedEncryptionProvider"),
						LogHelper.MarkAsNonPII(algorithm),
						key
					})));
				}
				num = 16;
			}
			byte[] keyBytes = this.GetKeyBytes(key);
			byte[] array = new byte[num];
			byte[] array2 = new byte[num];
			Array.Copy(keyBytes, num, array, 0, num);
			Array.Copy(keyBytes, array2, num);
			return new AuthenticatedEncryptionProvider.AuthenticatedKeys
			{
				AesKey = new SymmetricSecurityKey(array),
				HmacKey = new SymmetricSecurityKey(array2)
			};
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0003BE84 File Offset: 0x0003A084
		private static string GetHmacAlgorithm(string algorithm)
		{
			if ("A128CBC-HS256".Equals(algorithm))
			{
				return "HS256";
			}
			if ("A192CBC-HS384".Equals(algorithm))
			{
				return "HS384";
			}
			if ("A256CBC-HS512".Equals(algorithm))
			{
				return "HS512";
			}
			throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10652: The algorithm '{0}' is not supported.", new object[] { LogHelper.MarkAsNonPII(algorithm) }), "algorithm"));
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0003BEF4 File Offset: 0x0003A0F4
		protected virtual byte[] GetKeyBytes(SecurityKey key)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			SymmetricSecurityKey symmetricSecurityKey = key as SymmetricSecurityKey;
			if (symmetricSecurityKey != null)
			{
				return symmetricSecurityKey.Key;
			}
			JsonWebKey jsonWebKey = key as JsonWebKey;
			SecurityKey securityKey;
			if (jsonWebKey != null && JsonWebKeyConverter.TryConvertToSymmetricSecurityKey(jsonWebKey, out securityKey))
			{
				return this.GetKeyBytes(securityKey);
			}
			throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10667: Unable to obtain required byte array for KeyHashAlgorithm from SecurityKey: '{0}'.", new object[] { key })));
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0003BF5C File Offset: 0x0003A15C
		internal static byte[] Transform(ICryptoTransform transform, byte[] input, int inputOffset, int inputLength)
		{
			if (transform.CanTransformMultipleBlocks)
			{
				return transform.TransformFinalBlock(input, inputOffset, inputLength);
			}
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
				{
					cryptoStream.Write(input, inputOffset, inputLength);
					cryptoStream.FlushFinalBlock();
					array = memoryStream.ToArray();
				}
			}
			return array;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0003BFD4 File Offset: 0x0003A1D4
		protected virtual void ValidateKeySize(SecurityKey key, string algorithm)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			if ("A128CBC-HS256".Equals(algorithm))
			{
				if (key.KeySize < 256)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("key", LogHelper.FormatInvariant("IDX10653: The encryption algorithm '{0}' requires a key size of at least '{1}' bits. Key '{2}', is of size: '{3}'.", new object[]
					{
						LogHelper.MarkAsNonPII("A128CBC-HS256"),
						LogHelper.MarkAsNonPII(256),
						key.KeyId,
						LogHelper.MarkAsNonPII(key.KeySize)
					})));
				}
				return;
			}
			else if ("A192CBC-HS384".Equals(algorithm))
			{
				if (key.KeySize < 384)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("key", LogHelper.FormatInvariant("IDX10653: The encryption algorithm '{0}' requires a key size of at least '{1}' bits. Key '{2}', is of size: '{3}'.", new object[]
					{
						LogHelper.MarkAsNonPII("A192CBC-HS384"),
						LogHelper.MarkAsNonPII(384),
						key.KeyId,
						LogHelper.MarkAsNonPII(key.KeySize)
					})));
				}
				return;
			}
			else if ("A256CBC-HS512".Equals(algorithm))
			{
				if (key.KeySize < 512)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("key", LogHelper.FormatInvariant("IDX10653: The encryption algorithm '{0}' requires a key size of at least '{1}' bits. Key '{2}', is of size: '{3}'.", new object[]
					{
						LogHelper.MarkAsNonPII("A256CBC-HS512"),
						LogHelper.MarkAsNonPII(512),
						key.KeyId,
						LogHelper.MarkAsNonPII(key.KeySize)
					})));
				}
				return;
			}
			else if ("A128GCM".Equals(algorithm))
			{
				if (key.KeySize < 128)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("key", LogHelper.FormatInvariant("IDX10653: The encryption algorithm '{0}' requires a key size of at least '{1}' bits. Key '{2}', is of size: '{3}'.", new object[]
					{
						LogHelper.MarkAsNonPII("A128GCM"),
						LogHelper.MarkAsNonPII(128),
						key.KeyId,
						LogHelper.MarkAsNonPII(key.KeySize)
					})));
				}
				return;
			}
			else if ("A192GCM".Equals(algorithm))
			{
				if (key.KeySize < 192)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("key", LogHelper.FormatInvariant("IDX10653: The encryption algorithm '{0}' requires a key size of at least '{1}' bits. Key '{2}', is of size: '{3}'.", new object[]
					{
						LogHelper.MarkAsNonPII("A192GCM"),
						LogHelper.MarkAsNonPII(192),
						key.KeyId,
						LogHelper.MarkAsNonPII(key.KeySize)
					})));
				}
				return;
			}
			else
			{
				if (!"A256GCM".Equals(algorithm))
				{
					throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10652: The algorithm '{0}' is not supported.", new object[] { LogHelper.MarkAsNonPII(algorithm) })));
				}
				if (key.KeySize < 256)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("key", LogHelper.FormatInvariant("IDX10653: The encryption algorithm '{0}' requires a key size of at least '{1}' bits. Key '{2}', is of size: '{3}'.", new object[]
					{
						LogHelper.MarkAsNonPII("A256GCM"),
						LogHelper.MarkAsNonPII(256),
						key.KeyId,
						LogHelper.MarkAsNonPII(key.KeySize)
					})));
				}
				return;
			}
		}

		// Token: 0x040004BE RID: 1214
		private Lazy<AuthenticatedEncryptionProvider.AuthenticatedKeys> _authenticatedkeys;

		// Token: 0x040004BF RID: 1215
		private DisposableObjectPool<AesGcm> _aesGcmObjectPool;

		// Token: 0x040004C0 RID: 1216
		private CryptoProviderFactory _cryptoProviderFactory;

		// Token: 0x040004C1 RID: 1217
		private bool _disposed;

		// Token: 0x040004C2 RID: 1218
		private Lazy<bool> _keySizeIsValid;

		// Token: 0x040004C3 RID: 1219
		private Lazy<SymmetricSignatureProvider> _symmetricSignatureProvider;

		// Token: 0x040004C4 RID: 1220
		private DecryptionDelegate DecryptFunction;

		// Token: 0x040004C5 RID: 1221
		private EncryptionDelegate EncryptFunction;

		// Token: 0x040004C6 RID: 1222
		private const string _className = "Microsoft.IdentityModel.Tokens.AuthenticatedEncryptionProvider";

		// Token: 0x02000271 RID: 625
		private struct AuthenticatedKeys
		{
			// Token: 0x04000B33 RID: 2867
			public SymmetricSecurityKey AesKey;

			// Token: 0x04000B34 RID: 2868
			public SymmetricSecurityKey HmacKey;
		}
	}
}
