using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200011D RID: 285
	public class AsymmetricSignatureProvider : SignatureProvider
	{
		// Token: 0x06000E2E RID: 3630 RVA: 0x00037C92 File Offset: 0x00035E92
		internal AsymmetricSignatureProvider(SecurityKey key, string algorithm, CryptoProviderFactory cryptoProviderFactory)
			: this(key, algorithm)
		{
			this._cryptoProviderFactory = cryptoProviderFactory;
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00037CA3 File Offset: 0x00035EA3
		internal AsymmetricSignatureProvider(SecurityKey key, string algorithm, bool willCreateSignatures, CryptoProviderFactory cryptoProviderFactory)
			: this(key, algorithm, willCreateSignatures)
		{
			this._cryptoProviderFactory = cryptoProviderFactory;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00037CB6 File Offset: 0x00035EB6
		public AsymmetricSignatureProvider(SecurityKey key, string algorithm)
			: this(key, algorithm, false)
		{
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00037CC4 File Offset: 0x00035EC4
		public AsymmetricSignatureProvider(SecurityKey key, string algorithm, bool willCreateSignatures)
			: base(key, algorithm)
		{
			this._cryptoProviderFactory = key.CryptoProviderFactory;
			this._minimumAsymmetricKeySizeInBitsForSigningMap = new Dictionary<string, int>(AsymmetricSignatureProvider.DefaultMinimumAsymmetricKeySizeInBitsForSigningMap);
			this._minimumAsymmetricKeySizeInBitsForVerifyingMap = new Dictionary<string, int>(AsymmetricSignatureProvider.DefaultMinimumAsymmetricKeySizeInBitsForVerifyingMap);
			JsonWebKey jsonWebKey = key as JsonWebKey;
			if (jsonWebKey != null)
			{
				SecurityKey securityKey;
				JsonWebKeyConverter.TryConvertToSecurityKey(jsonWebKey, out securityKey);
			}
			if (willCreateSignatures && AsymmetricSignatureProvider.FoundPrivateKey(key) == PrivateKeyStatus.DoesNotExist)
			{
				throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10638: Cannot create the SignatureProvider, 'key.HasPrivateKey' is false, cannot create signatures. Key: {0}.", new object[] { key })));
			}
			if (!this._cryptoProviderFactory.IsSupportedAlgorithm(algorithm, key))
			{
				throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10634: Unable to create the SignatureProvider.\nAlgorithm: '{0}', SecurityKey: '{1}'\n is not supported. The list of supported algorithms is available here: https://aka.ms/IdentityModel/supported-algorithms", new object[]
				{
					LogHelper.MarkAsNonPII(algorithm),
					key
				})));
			}
			base.WillCreateSignatures = willCreateSignatures;
			this._keySizeIsValid = new Lazy<bool>(new Func<bool>(this.ValidKeySize));
			this._asymmetricAdapterObjectPool = new DisposableObjectPool<AsymmetricAdapter>(new Func<AsymmetricAdapter>(this.CreateAsymmetricAdapter), this._cryptoProviderFactory.SignatureProviderObjectPoolCacheSize);
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x00037DBA File Offset: 0x00035FBA
		public IReadOnlyDictionary<string, int> MinimumAsymmetricKeySizeInBitsForSigningMap
		{
			get
			{
				return this._minimumAsymmetricKeySizeInBitsForSigningMap;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x00037DC2 File Offset: 0x00035FC2
		public IReadOnlyDictionary<string, int> MinimumAsymmetricKeySizeInBitsForVerifyingMap
		{
			get
			{
				return this._minimumAsymmetricKeySizeInBitsForVerifyingMap;
			}
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00037DCC File Offset: 0x00035FCC
		private static PrivateKeyStatus FoundPrivateKey(SecurityKey key)
		{
			AsymmetricSecurityKey asymmetricSecurityKey = key as AsymmetricSecurityKey;
			if (asymmetricSecurityKey != null)
			{
				return asymmetricSecurityKey.PrivateKeyStatus;
			}
			JsonWebKey jsonWebKey = key as JsonWebKey;
			if (jsonWebKey == null)
			{
				return PrivateKeyStatus.Unknown;
			}
			if (!jsonWebKey.HasPrivateKey)
			{
				return PrivateKeyStatus.DoesNotExist;
			}
			return PrivateKeyStatus.Exists;
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00037E01 File Offset: 0x00036001
		protected virtual HashAlgorithmName GetHashAlgorithmName(string algorithm)
		{
			if (string.IsNullOrWhiteSpace(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			return SupportedAlgorithms.GetHashAlgorithmName(algorithm);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00037E1C File Offset: 0x0003601C
		private AsymmetricAdapter CreateAsymmetricAdapter()
		{
			HashAlgorithmName hashAlgorithmName = this.GetHashAlgorithmName(base.Algorithm);
			return new AsymmetricAdapter(base.Key, base.Algorithm, this._cryptoProviderFactory.CreateHashAlgorithm(hashAlgorithmName), hashAlgorithmName, base.WillCreateSignatures);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00037E5A File Offset: 0x0003605A
		internal bool ValidKeySize()
		{
			this.ValidateAsymmetricSecurityKeySize(base.Key, base.Algorithm, base.WillCreateSignatures);
			return true;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x00037E75 File Offset: 0x00036075
		internal override int ObjectPoolSize
		{
			get
			{
				return this._asymmetricAdapterObjectPool.Size;
			}
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00037E84 File Offset: 0x00036084
		public override byte[] Sign(byte[] input)
		{
			if (input == null || input.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			if (this._disposed)
			{
				CryptoProviderCache cryptoProviderCache = base.CryptoProviderCache;
				if (cryptoProviderCache != null)
				{
					cryptoProviderCache.TryRemove(this);
				}
				throw LogHelper.LogExceptionMessage(new ObjectDisposedException(base.GetType().ToString()));
			}
			AsymmetricAdapter asymmetricAdapter = null;
			byte[] array;
			try
			{
				asymmetricAdapter = this._asymmetricAdapterObjectPool.Allocate();
				array = asymmetricAdapter.Sign(input);
			}
			catch
			{
				CryptoProviderCache cryptoProviderCache2 = base.CryptoProviderCache;
				if (cryptoProviderCache2 != null)
				{
					cryptoProviderCache2.TryRemove(this);
				}
				this.Dispose(true);
				throw;
			}
			finally
			{
				if (!this._disposed)
				{
					this._asymmetricAdapterObjectPool.Free(asymmetricAdapter);
				}
			}
			return array;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00037F3C File Offset: 0x0003613C
		public virtual void ValidateAsymmetricSecurityKeySize(SecurityKey key, string algorithm, bool willCreateSignatures)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			int num = key.KeySize;
			AsymmetricSecurityKey asymmetricSecurityKey = key as AsymmetricSecurityKey;
			if (asymmetricSecurityKey != null)
			{
				num = asymmetricSecurityKey.KeySize;
			}
			else
			{
				JsonWebKey jsonWebKey = key as JsonWebKey;
				if (jsonWebKey == null)
				{
					throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10704: Cannot verify the key size. The SecurityKey is not or cannot be converted to an AsymmetricSecuritKey. SecurityKey: '{0}'.", new object[] { key })));
				}
				SecurityKey securityKey;
				JsonWebKeyConverter.TryConvertToSecurityKey(jsonWebKey, out securityKey);
				AsymmetricSecurityKey asymmetricSecurityKey2 = securityKey as AsymmetricSecurityKey;
				if (asymmetricSecurityKey2 != null)
				{
					num = asymmetricSecurityKey2.KeySize;
				}
				else if (securityKey is SymmetricSecurityKey)
				{
					throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10704: Cannot verify the key size. The SecurityKey is not or cannot be converted to an AsymmetricSecuritKey. SecurityKey: '{0}'.", new object[] { key })));
				}
			}
			if (willCreateSignatures)
			{
				if (this.MinimumAsymmetricKeySizeInBitsForSigningMap.ContainsKey(algorithm) && num < this.MinimumAsymmetricKeySizeInBitsForSigningMap[algorithm])
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("key", LogHelper.FormatInvariant("IDX10630: The '{0}' for signing cannot be smaller than '{1}' bits. KeySize: '{2}'.", new object[]
					{
						key,
						LogHelper.MarkAsNonPII(this.MinimumAsymmetricKeySizeInBitsForSigningMap[algorithm]),
						LogHelper.MarkAsNonPII(num)
					})));
				}
			}
			else if (this.MinimumAsymmetricKeySizeInBitsForVerifyingMap.ContainsKey(algorithm) && num < this.MinimumAsymmetricKeySizeInBitsForVerifyingMap[algorithm])
			{
				throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("key", LogHelper.FormatInvariant("IDX10631: The '{0}' for verifying cannot be smaller than '{1}' bits. KeySize: '{2}'.", new object[]
				{
					key,
					LogHelper.MarkAsNonPII(this.MinimumAsymmetricKeySizeInBitsForVerifyingMap[algorithm]),
					LogHelper.MarkAsNonPII(num)
				})));
			}
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x000380CC File Offset: 0x000362CC
		public override bool Verify(byte[] input, byte[] signature)
		{
			if (input == null || input.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			if (signature == null || signature.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("signature");
			}
			if (this._disposed)
			{
				CryptoProviderCache cryptoProviderCache = base.CryptoProviderCache;
				if (cryptoProviderCache != null)
				{
					cryptoProviderCache.TryRemove(this);
				}
				throw LogHelper.LogExceptionMessage(new ObjectDisposedException(base.GetType().ToString()));
			}
			AsymmetricAdapter asymmetricAdapter = null;
			bool flag;
			try
			{
				asymmetricAdapter = this._asymmetricAdapterObjectPool.Allocate();
				flag = asymmetricAdapter.Verify(input, signature);
			}
			catch
			{
				CryptoProviderCache cryptoProviderCache2 = base.CryptoProviderCache;
				if (cryptoProviderCache2 != null)
				{
					cryptoProviderCache2.TryRemove(this);
				}
				this.Dispose(true);
				throw;
			}
			finally
			{
				if (!this._disposed)
				{
					this._asymmetricAdapterObjectPool.Free(asymmetricAdapter);
				}
			}
			return flag;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00038198 File Offset: 0x00036398
		public override bool Verify(byte[] input, int inputOffset, int inputLength, byte[] signature, int signatureOffset, int signatureLength)
		{
			if (input == null || input.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			if (signature == null || signature.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("signature");
			}
			if (inputOffset < 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10716: '{0}' must be greater than 0, was: '{1}'", new object[]
				{
					LogHelper.MarkAsNonPII("inputOffset"),
					LogHelper.MarkAsNonPII(inputOffset)
				})));
			}
			if (inputLength < 1)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10655: '{0}' must be greater than 1, was: '{1}'", new object[]
				{
					LogHelper.MarkAsNonPII("inputLength"),
					LogHelper.MarkAsNonPII(inputLength)
				})));
			}
			if (inputOffset + inputLength > input.Length)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10717: '{0} + {1}' must not be greater than {2}, '{3} + {4} > {5}'.", new object[]
				{
					LogHelper.MarkAsNonPII("inputOffset"),
					LogHelper.MarkAsNonPII("inputLength"),
					LogHelper.MarkAsNonPII("input"),
					LogHelper.MarkAsNonPII(inputOffset),
					LogHelper.MarkAsNonPII(inputLength),
					LogHelper.MarkAsNonPII(input.Length)
				})));
			}
			if (signatureOffset < 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10716: '{0}' must be greater than 0, was: '{1}'", new object[]
				{
					LogHelper.MarkAsNonPII("signatureOffset"),
					LogHelper.MarkAsNonPII(signatureOffset)
				})));
			}
			if (signatureLength < 1)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10655: '{0}' must be greater than 1, was: '{1}'", new object[]
				{
					LogHelper.MarkAsNonPII("signatureLength"),
					LogHelper.MarkAsNonPII(signatureLength)
				})));
			}
			if (signatureOffset + signatureLength > signature.Length)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10717: '{0} + {1}' must not be greater than {2}, '{3} + {4} > {5}'.", new object[]
				{
					LogHelper.MarkAsNonPII("signatureOffset"),
					LogHelper.MarkAsNonPII("signatureLength"),
					LogHelper.MarkAsNonPII("signature"),
					LogHelper.MarkAsNonPII(signatureOffset),
					LogHelper.MarkAsNonPII(signatureLength),
					LogHelper.MarkAsNonPII(signature.Length)
				})));
			}
			if (this._disposed)
			{
				CryptoProviderCache cryptoProviderCache = base.CryptoProviderCache;
				if (cryptoProviderCache != null)
				{
					cryptoProviderCache.TryRemove(this);
				}
				throw LogHelper.LogExceptionMessage(new ObjectDisposedException(base.GetType().ToString()));
			}
			AsymmetricAdapter asymmetricAdapter = null;
			bool flag;
			try
			{
				asymmetricAdapter = this._asymmetricAdapterObjectPool.Allocate();
				if (signature.Length == signatureLength)
				{
					flag = asymmetricAdapter.Verify(input, inputOffset, inputLength, signature);
				}
				else
				{
					byte[] array = new byte[signatureLength];
					Array.Copy(signature, 0, array, 0, signatureLength);
					flag = asymmetricAdapter.Verify(input, inputOffset, inputLength, array);
				}
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
					this._asymmetricAdapterObjectPool.Free(asymmetricAdapter);
				}
			}
			return flag;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0003845C File Offset: 0x0003665C
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this._disposed = true;
				if (disposing)
				{
					DisposableObjectPool<AsymmetricAdapter>.Element[] items = this._asymmetricAdapterObjectPool.Items;
					for (int i = 0; i < items.Length; i++)
					{
						AsymmetricAdapter value = items[i].Value;
						if (value != null)
						{
							value.Dispose();
						}
					}
					CryptoProviderCache cryptoProviderCache = base.CryptoProviderCache;
					if (cryptoProviderCache == null)
					{
						return;
					}
					cryptoProviderCache.TryRemove(this);
				}
			}
		}

		// Token: 0x04000477 RID: 1143
		private DisposableObjectPool<AsymmetricAdapter> _asymmetricAdapterObjectPool;

		// Token: 0x04000478 RID: 1144
		private CryptoProviderFactory _cryptoProviderFactory;

		// Token: 0x04000479 RID: 1145
		private bool _disposed;

		// Token: 0x0400047A RID: 1146
		private Lazy<bool> _keySizeIsValid;

		// Token: 0x0400047B RID: 1147
		private IReadOnlyDictionary<string, int> _minimumAsymmetricKeySizeInBitsForSigningMap;

		// Token: 0x0400047C RID: 1148
		private IReadOnlyDictionary<string, int> _minimumAsymmetricKeySizeInBitsForVerifyingMap;

		// Token: 0x0400047D RID: 1149
		public static readonly Dictionary<string, int> DefaultMinimumAsymmetricKeySizeInBitsForSigningMap = new Dictionary<string, int>
		{
			{ "ES256", 256 },
			{ "ES384", 256 },
			{ "ES512", 256 },
			{ "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha256", 256 },
			{ "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha384", 256 },
			{ "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha512", 256 },
			{ "RS256", 2048 },
			{ "RS384", 2048 },
			{ "RS512", 2048 },
			{ "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", 2048 },
			{ "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384", 2048 },
			{ "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512", 2048 },
			{ "PS256", 528 },
			{ "PS384", 784 },
			{ "PS512", 1040 },
			{ "http://www.w3.org/2007/05/xmldsig-more#sha256-rsa-MGF1", 528 },
			{ "http://www.w3.org/2007/05/xmldsig-more#sha384-rsa-MGF1", 784 },
			{ "http://www.w3.org/2007/05/xmldsig-more#sha512-rsa-MGF1", 1040 }
		};

		// Token: 0x0400047E RID: 1150
		public static readonly Dictionary<string, int> DefaultMinimumAsymmetricKeySizeInBitsForVerifyingMap = new Dictionary<string, int>
		{
			{ "ES256", 256 },
			{ "ES384", 256 },
			{ "ES512", 256 },
			{ "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha256", 256 },
			{ "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha384", 256 },
			{ "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha512", 256 },
			{ "RS256", 1024 },
			{ "RS384", 1024 },
			{ "RS512", 1024 },
			{ "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", 1024 },
			{ "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384", 1024 },
			{ "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512", 1024 },
			{ "PS256", 528 },
			{ "PS384", 784 },
			{ "PS512", 1040 },
			{ "http://www.w3.org/2007/05/xmldsig-more#sha256-rsa-MGF1", 528 },
			{ "http://www.w3.org/2007/05/xmldsig-more#sha384-rsa-MGF1", 784 },
			{ "http://www.w3.org/2007/05/xmldsig-more#sha512-rsa-MGF1", 1040 }
		};
	}
}
