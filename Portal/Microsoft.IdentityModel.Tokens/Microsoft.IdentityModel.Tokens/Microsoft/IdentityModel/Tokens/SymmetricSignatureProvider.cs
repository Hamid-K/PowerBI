using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200017A RID: 378
	public class SymmetricSignatureProvider : SignatureProvider
	{
		// Token: 0x06001115 RID: 4373 RVA: 0x00041AD3 File Offset: 0x0003FCD3
		public SymmetricSignatureProvider(SecurityKey key, string algorithm)
			: this(key, algorithm, true)
		{
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00041AE0 File Offset: 0x0003FCE0
		public SymmetricSignatureProvider(SecurityKey key, string algorithm, bool willCreateSignatures)
			: base(key, algorithm)
		{
			if (!key.CryptoProviderFactory.IsSupportedAlgorithm(algorithm, key))
			{
				throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10634: Unable to create the SignatureProvider.\nAlgorithm: '{0}', SecurityKey: '{1}'\n is not supported. The list of supported algorithms is available here: https://aka.ms/IdentityModel/supported-algorithms", new object[]
				{
					LogHelper.MarkAsNonPII(algorithm),
					key
				})));
			}
			if (key.KeySize < this.MinimumSymmetricKeySizeInBits)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("key", LogHelper.FormatInvariant("IDX10653: The encryption algorithm '{0}' requires a key size of at least '{1}' bits. Key '{2}', is of size: '{3}'.", new object[]
				{
					LogHelper.MarkAsNonPII(algorithm),
					LogHelper.MarkAsNonPII(this.MinimumSymmetricKeySizeInBits),
					key,
					LogHelper.MarkAsNonPII(key.KeySize)
				})));
			}
			base.WillCreateSignatures = willCreateSignatures;
			this._keyedHashObjectPool = new DisposableObjectPool<KeyedHashAlgorithm>(new Func<KeyedHashAlgorithm>(this.CreateKeyedHashAlgorithm), key.CryptoProviderFactory.SignatureProviderObjectPoolCacheSize);
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00041BC1 File Offset: 0x0003FDC1
		// (set) Token: 0x06001118 RID: 4376 RVA: 0x00041BCC File Offset: 0x0003FDCC
		public int MinimumSymmetricKeySizeInBits
		{
			get
			{
				return this._minimumSymmetricKeySizeInBits;
			}
			set
			{
				if (value < SymmetricSignatureProvider.DefaultMinimumSymmetricKeySizeInBits)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", LogHelper.FormatInvariant("IDX10628: Cannot set the MinimumSymmetricKeySizeInBits to less than '{0}'.", new object[] { LogHelper.MarkAsNonPII(SymmetricSignatureProvider.DefaultMinimumSymmetricKeySizeInBits) })));
				}
				this._minimumSymmetricKeySizeInBits = value;
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00041C1C File Offset: 0x0003FE1C
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
			if (jsonWebKey != null && jsonWebKey.K != null && jsonWebKey.Kty == "oct")
			{
				return Base64UrlEncoder.DecodeBytes(jsonWebKey.K);
			}
			throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10667: Unable to obtain required byte array for KeyHashAlgorithm from SecurityKey: '{0}'.", new object[] { key })));
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00041C96 File Offset: 0x0003FE96
		protected virtual KeyedHashAlgorithm GetKeyedHashAlgorithm(byte[] keyBytes, string algorithm)
		{
			return this._keyedHashObjectPool.Allocate();
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00041CA3 File Offset: 0x0003FEA3
		private KeyedHashAlgorithm CreateKeyedHashAlgorithm()
		{
			return base.Key.CryptoProviderFactory.CreateKeyedHashAlgorithm(this.GetKeyBytes(base.Key), base.Algorithm);
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00041CC7 File Offset: 0x0003FEC7
		internal override int ObjectPoolSize
		{
			get
			{
				return this._keyedHashObjectPool.Size;
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00041CD4 File Offset: 0x0003FED4
		protected virtual void ReleaseKeyedHashAlgorithm(KeyedHashAlgorithm keyedHashAlgorithm)
		{
			if (keyedHashAlgorithm != null)
			{
				this._keyedHashObjectPool.Free(keyedHashAlgorithm);
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00041CE8 File Offset: 0x0003FEE8
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
			LogHelper.LogInformation("IDX10642: Creating signature using the input: '{0}'.", new object[] { input });
			KeyedHashAlgorithm keyedHashAlgorithm = this.GetKeyedHashAlgorithm(this.GetKeyBytes(base.Key), base.Algorithm);
			byte[] array;
			try
			{
				array = keyedHashAlgorithm.ComputeHash(input);
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
					this.ReleaseKeyedHashAlgorithm(keyedHashAlgorithm);
				}
			}
			return array;
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00041DBC File Offset: 0x0003FFBC
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
			LogHelper.LogInformation("IDX10643: Comparing the signature created over the input with the token signature: '{0}'.", new object[] { input });
			KeyedHashAlgorithm keyedHashAlgorithm = this.GetKeyedHashAlgorithm(this.GetKeyBytes(base.Key), base.Algorithm);
			bool flag;
			try
			{
				flag = Utility.AreEqual(signature, keyedHashAlgorithm.ComputeHash(input));
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
					this.ReleaseKeyedHashAlgorithm(keyedHashAlgorithm);
				}
			}
			return flag;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00041EA8 File Offset: 0x000400A8
		public bool Verify(byte[] input, byte[] signature, int length)
		{
			if (input == null)
			{
				throw LogHelper.LogArgumentNullException("input");
			}
			return this.Verify(input, 0, input.Length, signature, 0, length);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00041EC6 File Offset: 0x000400C6
		public override bool Verify(byte[] input, int inputOffset, int inputLength, byte[] signature, int signatureOffset, int signatureLength)
		{
			return this.Verify(input, inputOffset, inputLength, signature, signatureOffset, signatureLength, null);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00041ED8 File Offset: 0x000400D8
		internal bool Verify(byte[] input, int inputOffset, int inputLength, byte[] signature, int signatureOffset, int signatureLength, string algorithm)
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
			if (signatureLength + signatureOffset > signature.Length)
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
			string text = algorithm ?? base.Algorithm;
			int num;
			if (!SymmetricSignatureProvider._expectedSignatureSizeInBytes.TryGetValue(text, out num))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10718: AlgorithmToValidate is not supported: '{0}'. Algorithm '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII(text),
					LogHelper.MarkAsNonPII(base.Algorithm)
				})));
			}
			if (num != signatureLength)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10719: SignatureSize (in bytes) was expected to be '{0}', was '{1}'.", new object[]
				{
					LogHelper.MarkAsNonPII(signatureLength),
					LogHelper.MarkAsNonPII(num)
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
			LogHelper.LogInformation("IDX10643: Comparing the signature created over the input with the token signature: '{0}'.", new object[] { input });
			KeyedHashAlgorithm keyedHashAlgorithm = null;
			bool flag;
			try
			{
				keyedHashAlgorithm = this.GetKeyedHashAlgorithm(this.GetKeyBytes(base.Key), base.Algorithm);
				flag = Utility.AreEqual(signature, keyedHashAlgorithm.ComputeHash(input, inputOffset, inputLength), signatureLength);
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
					this.ReleaseKeyedHashAlgorithm(keyedHashAlgorithm);
				}
			}
			return flag;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00042220 File Offset: 0x00040420
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this._disposed = true;
				if (disposing)
				{
					DisposableObjectPool<KeyedHashAlgorithm>.Element[] items = this._keyedHashObjectPool.Items;
					for (int i = 0; i < items.Length; i++)
					{
						KeyedHashAlgorithm value = items[i].Value;
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

		// Token: 0x04000686 RID: 1670
		private bool _disposed;

		// Token: 0x04000687 RID: 1671
		private DisposableObjectPool<KeyedHashAlgorithm> _keyedHashObjectPool;

		// Token: 0x04000688 RID: 1672
		private static readonly Dictionary<string, int> _expectedSignatureSizeInBytes = new Dictionary<string, int>
		{
			{ "HS256", 32 },
			{ "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256", 32 },
			{ "HS384", 48 },
			{ "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384", 48 },
			{ "HS512", 64 },
			{ "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512", 64 },
			{ "A128CBC-HS256", 16 },
			{ "A192CBC-HS384", 24 },
			{ "A256CBC-HS512", 32 }
		};

		// Token: 0x04000689 RID: 1673
		public static readonly int DefaultMinimumSymmetricKeySizeInBits = 128;

		// Token: 0x0400068A RID: 1674
		private int _minimumSymmetricKeySizeInBits = SymmetricSignatureProvider.DefaultMinimumSymmetricKeySizeInBits;
	}
}
