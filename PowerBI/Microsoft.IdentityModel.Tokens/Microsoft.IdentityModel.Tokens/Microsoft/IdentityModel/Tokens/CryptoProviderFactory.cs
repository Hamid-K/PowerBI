using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000129 RID: 297
	public class CryptoProviderFactory
	{
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00039B25 File Offset: 0x00037D25
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x00039B2C File Offset: 0x00037D2C
		public static CryptoProviderFactory Default
		{
			get
			{
				return CryptoProviderFactory._default;
			}
			set
			{
				if (value == null)
				{
					throw LogHelper.LogArgumentNullException("value");
				}
				CryptoProviderFactory._default = value;
			}
		} = new CryptoProviderFactory();

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x00039B43 File Offset: 0x00037D43
		// (set) Token: 0x06000E8E RID: 3726 RVA: 0x00039B4A File Offset: 0x00037D4A
		[DefaultValue(true)]
		public static bool DefaultCacheSignatureProviders { get; set; } = true;

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00039B52 File Offset: 0x00037D52
		// (set) Token: 0x06000E90 RID: 3728 RVA: 0x00039B59 File Offset: 0x00037D59
		public static int DefaultSignatureProviderObjectPoolCacheSize
		{
			get
			{
				return CryptoProviderFactory._defaultSignatureProviderObjectPoolCacheSize;
			}
			set
			{
				if (value <= 0)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", LogHelper.FormatInvariant("IDX10698: The SignatureProviderObjectPoolCacheSize must be greater than 0. Value: '{0}'.", new object[] { LogHelper.MarkAsNonPII(value) })));
				}
				CryptoProviderFactory._defaultSignatureProviderObjectPoolCacheSize = value;
			}
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00039BC5 File Offset: 0x00037DC5
		public CryptoProviderFactory()
		{
			this.CryptoProviderCache = new InMemoryCryptoProviderCache
			{
				CryptoProviderFactory = this
			};
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00039BF5 File Offset: 0x00037DF5
		public CryptoProviderFactory(CryptoProviderCache cache)
		{
			if (cache == null)
			{
				throw LogHelper.LogArgumentNullException("cache");
			}
			this.CryptoProviderCache = cache;
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00039C2C File Offset: 0x00037E2C
		public CryptoProviderFactory(CryptoProviderFactory other)
		{
			if (other == null)
			{
				throw LogHelper.LogArgumentNullException("other");
			}
			this.CryptoProviderCache = new InMemoryCryptoProviderCache
			{
				CryptoProviderFactory = this
			};
			this.CustomCryptoProvider = other.CustomCryptoProvider;
			this.CacheSignatureProviders = other.CacheSignatureProviders;
			this.SignatureProviderObjectPoolCacheSize = other.SignatureProviderObjectPoolCacheSize;
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00039C99 File Offset: 0x00037E99
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x00039CA1 File Offset: 0x00037EA1
		public CryptoProviderCache CryptoProviderCache { get; internal set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x00039CAA File Offset: 0x00037EAA
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x00039CB2 File Offset: 0x00037EB2
		public ICryptoProvider CustomCryptoProvider { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00039CBB File Offset: 0x00037EBB
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x00039CC3 File Offset: 0x00037EC3
		[DefaultValue(true)]
		public bool CacheSignatureProviders { get; set; } = CryptoProviderFactory.DefaultCacheSignatureProviders;

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00039CCC File Offset: 0x00037ECC
		// (set) Token: 0x06000E9C RID: 3740 RVA: 0x00039CD4 File Offset: 0x00037ED4
		public int SignatureProviderObjectPoolCacheSize
		{
			get
			{
				return this._signatureProviderObjectPoolCacheSize;
			}
			set
			{
				if (value <= 0)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("value", LogHelper.FormatInvariant("IDX10698: The SignatureProviderObjectPoolCacheSize must be greater than 0. Value: '{0}'.", new object[] { LogHelper.MarkAsNonPII(value) })));
				}
				this._signatureProviderObjectPoolCacheSize = value;
			}
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x00039D10 File Offset: 0x00037F10
		public virtual AuthenticatedEncryptionProvider CreateAuthenticatedEncryptionProvider(SecurityKey key, string algorithm)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			if (this.CustomCryptoProvider != null && this.CustomCryptoProvider.IsSupportedAlgorithm(algorithm, new object[] { key }))
			{
				AuthenticatedEncryptionProvider authenticatedEncryptionProvider = this.CustomCryptoProvider.Create(algorithm, new object[] { key }) as AuthenticatedEncryptionProvider;
				if (authenticatedEncryptionProvider == null)
				{
					throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10646: A CustomCryptoProvider was set and returned 'true' for IsSupportedAlgorithm(Algorithm: '{0}', Key: '{1}'), but Create.(algorithm, args) as '{2}' == NULL.", new object[]
					{
						LogHelper.MarkAsNonPII(algorithm),
						key,
						LogHelper.MarkAsNonPII(typeof(AuthenticatedEncryptionProvider))
					})));
				}
				return authenticatedEncryptionProvider;
			}
			else
			{
				if (SupportedAlgorithms.IsSupportedEncryptionAlgorithm(algorithm, key))
				{
					return new AuthenticatedEncryptionProvider(key, algorithm);
				}
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10652: The algorithm '{0}' is not supported.", new object[] { LogHelper.MarkAsNonPII(algorithm) }), "algorithm"));
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00039DF2 File Offset: 0x00037FF2
		public virtual KeyWrapProvider CreateKeyWrapProvider(SecurityKey key, string algorithm)
		{
			return this.CreateKeyWrapProvider(key, algorithm, false);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00039DFD File Offset: 0x00037FFD
		public virtual KeyWrapProvider CreateKeyWrapProviderForUnwrap(SecurityKey key, string algorithm)
		{
			return this.CreateKeyWrapProvider(key, algorithm, true);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00039E08 File Offset: 0x00038008
		private KeyWrapProvider CreateKeyWrapProvider(SecurityKey key, string algorithm, bool willUnwrap)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			if (this.CustomCryptoProvider != null && this.CustomCryptoProvider.IsSupportedAlgorithm(algorithm, new object[] { key, willUnwrap }))
			{
				KeyWrapProvider keyWrapProvider = this.CustomCryptoProvider.Create(algorithm, new object[] { key, willUnwrap }) as KeyWrapProvider;
				if (keyWrapProvider == null)
				{
					throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10646: A CustomCryptoProvider was set and returned 'true' for IsSupportedAlgorithm(Algorithm: '{0}', Key: '{1}'), but Create.(algorithm, args) as '{2}' == NULL.", new object[]
					{
						LogHelper.MarkAsNonPII(algorithm),
						key,
						LogHelper.MarkAsNonPII(typeof(SignatureProvider))
					})));
				}
				return keyWrapProvider;
			}
			else
			{
				if (SupportedAlgorithms.IsSupportedRsaKeyWrap(algorithm, key))
				{
					return new RsaKeyWrapProvider(key, algorithm, willUnwrap);
				}
				if (SupportedAlgorithms.IsSupportedSymmetricKeyWrap(algorithm, key))
				{
					return new SymmetricKeyWrapProvider(key, algorithm);
				}
				throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10661: Unable to create the KeyWrapProvider.\nKeyWrapAlgorithm: '{0}', SecurityKey: '{1}'\n is not supported.", new object[]
				{
					LogHelper.MarkAsNonPII(algorithm),
					key
				})));
			}
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00039F10 File Offset: 0x00038110
		public virtual SignatureProvider CreateForSigning(SecurityKey key, string algorithm)
		{
			return this.CreateForSigning(key, algorithm, this.CacheSignatureProviders);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00039F20 File Offset: 0x00038120
		public virtual SignatureProvider CreateForSigning(SecurityKey key, string algorithm, bool cacheProvider)
		{
			return this.CreateSignatureProvider(key, algorithm, true, cacheProvider);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00039F2C File Offset: 0x0003812C
		public virtual SignatureProvider CreateForVerifying(SecurityKey key, string algorithm)
		{
			return this.CreateForVerifying(key, algorithm, this.CacheSignatureProviders);
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00039F3C File Offset: 0x0003813C
		public virtual SignatureProvider CreateForVerifying(SecurityKey key, string algorithm, bool cacheProvider)
		{
			return this.CreateSignatureProvider(key, algorithm, false, cacheProvider);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00039F48 File Offset: 0x00038148
		public virtual HashAlgorithm CreateHashAlgorithm(HashAlgorithmName algorithm)
		{
			if (this.CustomCryptoProvider != null && this.CustomCryptoProvider.IsSupportedAlgorithm(algorithm.Name, Array.Empty<object>()))
			{
				HashAlgorithm hashAlgorithm = this.CustomCryptoProvider.Create(algorithm.Name, Array.Empty<object>()) as HashAlgorithm;
				if (hashAlgorithm == null)
				{
					throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10647: A CustomCryptoProvider was set and returned 'true' for IsSupportedAlgorithm(Algorithm: '{0}'), but Create.(algorithm, args) as '{1}' == NULL.", new object[]
					{
						LogHelper.MarkAsNonPII(algorithm),
						LogHelper.MarkAsNonPII(typeof(HashAlgorithm))
					})));
				}
				CryptoProviderFactory._typeToAlgorithmMap[hashAlgorithm.GetType().ToString()] = algorithm.Name;
				return hashAlgorithm;
			}
			else
			{
				if (algorithm == HashAlgorithmName.SHA256)
				{
					return SHA256.Create();
				}
				if (algorithm == HashAlgorithmName.SHA384)
				{
					return SHA384.Create();
				}
				if (algorithm == HashAlgorithmName.SHA512)
				{
					return SHA512.Create();
				}
				throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10640: Algorithm is not supported: '{0}'.", new object[] { LogHelper.MarkAsNonPII(algorithm) })));
			}
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0003A054 File Offset: 0x00038254
		public virtual HashAlgorithm CreateHashAlgorithm(string algorithm)
		{
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			if (this.CustomCryptoProvider != null && this.CustomCryptoProvider.IsSupportedAlgorithm(algorithm, Array.Empty<object>()))
			{
				HashAlgorithm hashAlgorithm = this.CustomCryptoProvider.Create(algorithm, Array.Empty<object>()) as HashAlgorithm;
				if (hashAlgorithm == null)
				{
					throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10647: A CustomCryptoProvider was set and returned 'true' for IsSupportedAlgorithm(Algorithm: '{0}'), but Create.(algorithm, args) as '{1}' == NULL.", new object[]
					{
						LogHelper.MarkAsNonPII(algorithm),
						LogHelper.MarkAsNonPII(typeof(HashAlgorithm))
					})));
				}
				CryptoProviderFactory._typeToAlgorithmMap[hashAlgorithm.GetType().ToString()] = algorithm;
				return hashAlgorithm;
			}
			else
			{
				if (algorithm == "SHA256" || algorithm == "http://www.w3.org/2001/04/xmlenc#sha256")
				{
					return SHA256.Create();
				}
				if (algorithm == "SHA384" || algorithm == "http://www.w3.org/2001/04/xmldsig-more#sha384")
				{
					return SHA384.Create();
				}
				if (!(algorithm == "SHA512") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#sha512"))
				{
					throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10640: Algorithm is not supported: '{0}'.", new object[] { LogHelper.MarkAsNonPII(algorithm) })));
				}
				return SHA512.Create();
			}
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0003A17C File Offset: 0x0003837C
		public virtual KeyedHashAlgorithm CreateKeyedHashAlgorithm(byte[] keyBytes, string algorithm)
		{
			if (keyBytes == null)
			{
				throw LogHelper.LogArgumentNullException("keyBytes");
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			if (this.CustomCryptoProvider == null || !this.CustomCryptoProvider.IsSupportedAlgorithm(algorithm, new object[] { keyBytes }))
			{
				if (algorithm != null)
				{
					int length = algorithm.Length;
					if (length != 5)
					{
						if (length != 13)
						{
							if (length != 50)
							{
								goto IL_022F;
							}
							switch (algorithm[47])
							{
							case '2':
								if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"))
								{
									goto IL_022F;
								}
								break;
							case '3':
								if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384"))
								{
									goto IL_022F;
								}
								goto IL_020F;
							case '4':
								goto IL_022F;
							case '5':
								if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512"))
								{
									goto IL_022F;
								}
								goto IL_021F;
							default:
								goto IL_022F;
							}
						}
						else
						{
							char c = algorithm[2];
							if (c != '2')
							{
								if (c != '5')
								{
									if (c != '9')
									{
										goto IL_022F;
									}
									if (!(algorithm == "A192CBC-HS384"))
									{
										goto IL_022F;
									}
									CryptoProviderFactory.ValidateKeySize(keyBytes, algorithm, 24);
									return new HMACSHA384(keyBytes);
								}
								else
								{
									if (!(algorithm == "A256CBC-HS512"))
									{
										goto IL_022F;
									}
									CryptoProviderFactory.ValidateKeySize(keyBytes, algorithm, 32);
									return new HMACSHA512(keyBytes);
								}
							}
							else
							{
								if (!(algorithm == "A128CBC-HS256"))
								{
									goto IL_022F;
								}
								CryptoProviderFactory.ValidateKeySize(keyBytes, algorithm, 16);
								return new HMACSHA256(keyBytes);
							}
						}
					}
					else
					{
						switch (algorithm[2])
						{
						case '2':
							if (!(algorithm == "HS256"))
							{
								goto IL_022F;
							}
							break;
						case '3':
							if (!(algorithm == "HS384"))
							{
								goto IL_022F;
							}
							goto IL_020F;
						case '4':
							goto IL_022F;
						case '5':
							if (!(algorithm == "HS512"))
							{
								goto IL_022F;
							}
							goto IL_021F;
						default:
							goto IL_022F;
						}
					}
					CryptoProviderFactory.ValidateKeySize(keyBytes, algorithm, 32);
					return new HMACSHA256(keyBytes);
					IL_020F:
					CryptoProviderFactory.ValidateKeySize(keyBytes, algorithm, 48);
					return new HMACSHA384(keyBytes);
					IL_021F:
					CryptoProviderFactory.ValidateKeySize(keyBytes, algorithm, 64);
					return new HMACSHA512(keyBytes);
				}
				IL_022F:
				throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10666: Unable to create KeyedHashAlgorithm for algorithm '{0}'.", new object[] { LogHelper.MarkAsNonPII(algorithm) })));
			}
			KeyedHashAlgorithm keyedHashAlgorithm = this.CustomCryptoProvider.Create(algorithm, new object[] { keyBytes }) as KeyedHashAlgorithm;
			if (keyedHashAlgorithm == null)
			{
				throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10647: A CustomCryptoProvider was set and returned 'true' for IsSupportedAlgorithm(Algorithm: '{0}'), but Create.(algorithm, args) as '{1}' == NULL.", new object[]
				{
					LogHelper.MarkAsNonPII(algorithm),
					LogHelper.MarkAsNonPII(typeof(KeyedHashAlgorithm))
				})));
			}
			return keyedHashAlgorithm;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003A3DC File Offset: 0x000385DC
		private static void ValidateKeySize(byte[] keyBytes, string algorithm, int expectedNumberOfBytes)
		{
			bool flag;
			if (AppContext.TryGetSwitch("Switch.Microsoft.IdentityModel.UnsafeRelaxHmacKeySizeValidation", out flag) && flag)
			{
				return;
			}
			if (keyBytes.Length < expectedNumberOfBytes)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("keyBytes", LogHelper.FormatInvariant("IDX10720: Unable to create KeyedHashAlgorithm for algorithm '{0}', the key size must be greater than: '{1}' bits, key has '{2}' bits. See https://aka.ms/IdentityModel/UnsafeRelaxHmacKeySizeValidation", new object[]
				{
					LogHelper.MarkAsNonPII(algorithm),
					LogHelper.MarkAsNonPII(expectedNumberOfBytes * 8),
					LogHelper.MarkAsNonPII(keyBytes.Length * 8)
				})));
			}
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0003A44C File Offset: 0x0003864C
		private SignatureProvider CreateSignatureProvider(SecurityKey key, string algorithm, bool willCreateSignatures, bool cacheProvider)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			if (this.CustomCryptoProvider != null && this.CustomCryptoProvider.IsSupportedAlgorithm(algorithm, new object[] { key, willCreateSignatures }))
			{
				SignatureProvider signatureProvider = this.CustomCryptoProvider.Create(algorithm, new object[] { key, willCreateSignatures }) as SignatureProvider;
				if (signatureProvider == null)
				{
					throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10646: A CustomCryptoProvider was set and returned 'true' for IsSupportedAlgorithm(Algorithm: '{0}', Key: '{1}'), but Create.(algorithm, args) as '{2}' == NULL.", new object[]
					{
						LogHelper.MarkAsNonPII(algorithm),
						key,
						LogHelper.MarkAsNonPII(typeof(SignatureProvider))
					})));
				}
				return signatureProvider;
			}
			else
			{
				string text = null;
				bool flag = true;
				if (key is AsymmetricSecurityKey)
				{
					text = typeof(AsymmetricSignatureProvider).ToString();
				}
				else
				{
					JsonWebKey jsonWebKey = key as JsonWebKey;
					if (jsonWebKey != null)
					{
						try
						{
							SecurityKey securityKey;
							if (JsonWebKeyConverter.TryConvertToSecurityKey(jsonWebKey, out securityKey))
							{
								if (securityKey is AsymmetricSecurityKey)
								{
									text = typeof(AsymmetricSignatureProvider).ToString();
								}
								else if (securityKey is SymmetricSecurityKey)
								{
									text = typeof(SymmetricSignatureProvider).ToString();
									flag = false;
								}
							}
							else if (jsonWebKey.Kty == "RSA" || jsonWebKey.Kty == "EC")
							{
								text = typeof(AsymmetricSignatureProvider).ToString();
							}
							else if (jsonWebKey.Kty == "oct")
							{
								text = typeof(SymmetricSignatureProvider).ToString();
								flag = false;
							}
							goto IL_01BF;
						}
						catch (Exception ex)
						{
							throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10694: JsonWebKeyConverter threw attempting to convert JsonWebKey: '{0}'. Exception: '{1}'.", new object[] { key, ex }), ex));
						}
					}
					if (key is SymmetricSecurityKey)
					{
						text = typeof(SymmetricSignatureProvider).ToString();
						flag = false;
					}
				}
				IL_01BF:
				if (text == null)
				{
					throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10621: '{0}' supports: '{1}' of types: '{2}' or '{3}'. SecurityKey received was of type '{4}'.", new object[]
					{
						LogHelper.MarkAsNonPII(typeof(SymmetricSignatureProvider)),
						LogHelper.MarkAsNonPII(typeof(SecurityKey)),
						LogHelper.MarkAsNonPII(typeof(AsymmetricSecurityKey)),
						LogHelper.MarkAsNonPII(typeof(SymmetricSecurityKey)),
						LogHelper.MarkAsNonPII(key.GetType())
					})));
				}
				if (!this.IsSupportedAlgorithm(algorithm, key))
				{
					throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10634: Unable to create the SignatureProvider.\nAlgorithm: '{0}', SecurityKey: '{1}'\n is not supported. The list of supported algorithms is available here: https://aka.ms/IdentityModel/supported-algorithms", new object[]
					{
						LogHelper.MarkAsNonPII(algorithm),
						key
					})));
				}
				SignatureProvider signatureProvider;
				if (this.CacheSignatureProviders && cacheProvider)
				{
					if (this.CryptoProviderCache.TryGetSignatureProvider(key, algorithm, text, willCreateSignatures, out signatureProvider))
					{
						signatureProvider.AddRef();
						return signatureProvider;
					}
					object cacheLock = CryptoProviderFactory._cacheLock;
					lock (cacheLock)
					{
						if (this.CryptoProviderCache.TryGetSignatureProvider(key, algorithm, text, willCreateSignatures, out signatureProvider))
						{
							signatureProvider.AddRef();
							return signatureProvider;
						}
						if (flag)
						{
							signatureProvider = new AsymmetricSignatureProvider(key, algorithm, willCreateSignatures, this);
						}
						else
						{
							signatureProvider = new SymmetricSignatureProvider(key, algorithm, willCreateSignatures);
						}
						if (CryptoProviderFactory.ShouldCacheSignatureProvider(signatureProvider))
						{
							this.CryptoProviderCache.TryAdd(signatureProvider);
						}
						return signatureProvider;
					}
				}
				if (flag)
				{
					signatureProvider = new AsymmetricSignatureProvider(key, algorithm, willCreateSignatures);
				}
				else
				{
					signatureProvider = new SymmetricSignatureProvider(key, algorithm, willCreateSignatures);
				}
				return signatureProvider;
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003A78C File Offset: 0x0003898C
		internal static bool ShouldCacheSignatureProvider(SignatureProvider signatureProvider)
		{
			if (signatureProvider == null)
			{
				throw new ArgumentNullException("signatureProvider");
			}
			return signatureProvider.Key.InternalId.Length != 0;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003A7AF File Offset: 0x000389AF
		public virtual bool IsSupportedAlgorithm(string algorithm)
		{
			return (this.CustomCryptoProvider != null && this.CustomCryptoProvider.IsSupportedAlgorithm(algorithm, Array.Empty<object>())) || SupportedAlgorithms.IsSupportedHashAlgorithm(algorithm);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0003A7D4 File Offset: 0x000389D4
		public virtual bool IsSupportedAlgorithm(string algorithm, SecurityKey key)
		{
			if (this.CustomCryptoProvider != null && this.CustomCryptoProvider.IsSupportedAlgorithm(algorithm, new object[] { key }))
			{
				return true;
			}
			JsonWebKey jsonWebKey = key as JsonWebKey;
			return SupportedAlgorithms.IsSupportedAlgorithm(algorithm, (jsonWebKey != null && jsonWebKey.ConvertedSecurityKey != null) ? jsonWebKey.ConvertedSecurityKey : key);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003A824 File Offset: 0x00038A24
		public virtual void ReleaseHashAlgorithm(HashAlgorithm hashAlgorithm)
		{
			if (hashAlgorithm == null)
			{
				throw LogHelper.LogArgumentNullException("hashAlgorithm");
			}
			string text;
			if (this.CustomCryptoProvider != null && CryptoProviderFactory._typeToAlgorithmMap.TryGetValue(hashAlgorithm.GetType().ToString(), out text) && this.CustomCryptoProvider.IsSupportedAlgorithm(text, Array.Empty<object>()))
			{
				this.CustomCryptoProvider.Release(hashAlgorithm);
				return;
			}
			hashAlgorithm.Dispose();
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0003A888 File Offset: 0x00038A88
		public virtual void ReleaseKeyWrapProvider(KeyWrapProvider provider)
		{
			if (provider == null)
			{
				throw LogHelper.LogArgumentNullException("provider");
			}
			if (this.CustomCryptoProvider != null && this.CustomCryptoProvider.IsSupportedAlgorithm(provider.Algorithm, Array.Empty<object>()))
			{
				this.CustomCryptoProvider.Release(provider);
				return;
			}
			provider.Dispose();
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003A8D8 File Offset: 0x00038AD8
		public virtual void ReleaseRsaKeyWrapProvider(RsaKeyWrapProvider provider)
		{
			if (provider == null)
			{
				throw LogHelper.LogArgumentNullException("provider");
			}
			if (this.CustomCryptoProvider != null && this.CustomCryptoProvider.IsSupportedAlgorithm(provider.Algorithm, Array.Empty<object>()))
			{
				this.CustomCryptoProvider.Release(provider);
				return;
			}
			provider.Dispose();
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003A928 File Offset: 0x00038B28
		public virtual void ReleaseSignatureProvider(SignatureProvider signatureProvider)
		{
			if (signatureProvider == null)
			{
				throw LogHelper.LogArgumentNullException("signatureProvider");
			}
			signatureProvider.Release();
			if (this.CustomCryptoProvider != null && this.CustomCryptoProvider.IsSupportedAlgorithm(signatureProvider.Algorithm, Array.Empty<object>()))
			{
				this.CustomCryptoProvider.Release(signatureProvider);
				return;
			}
			if (signatureProvider.CryptoProviderCache == null && signatureProvider.RefCount == 0)
			{
				signatureProvider.Dispose();
			}
		}

		// Token: 0x040004A8 RID: 1192
		private static CryptoProviderFactory _default;

		// Token: 0x040004A9 RID: 1193
		private static ConcurrentDictionary<string, string> _typeToAlgorithmMap = new ConcurrentDictionary<string, string>();

		// Token: 0x040004AA RID: 1194
		private static object _cacheLock = new object();

		// Token: 0x040004AB RID: 1195
		private static int _defaultSignatureProviderObjectPoolCacheSize = Environment.ProcessorCount * 4;

		// Token: 0x040004AC RID: 1196
		private int _signatureProviderObjectPoolCacheSize = CryptoProviderFactory._defaultSignatureProviderObjectPoolCacheSize;

		// Token: 0x040004AD RID: 1197
		internal const string _skipValidationOfHmacKeySizes = "Switch.Microsoft.IdentityModel.UnsafeRelaxHmacKeySizeValidation";
	}
}
