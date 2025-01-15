using System;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000137 RID: 311
	public class SymmetricKeyWrapProvider : KeyWrapProvider
	{
		// Token: 0x06000F27 RID: 3879 RVA: 0x0003C58C File Offset: 0x0003A78C
		public SymmetricKeyWrapProvider(SecurityKey key, string algorithm)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw LogHelper.LogArgumentNullException("algorithm");
			}
			this.Algorithm = algorithm;
			this.Key = key;
			this._symmetricAlgorithm = new Lazy<SymmetricAlgorithm>(new Func<SymmetricAlgorithm>(this.CreateSymmetricAlgorithm));
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0003C5E5 File Offset: 0x0003A7E5
		public override string Algorithm { get; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0003C5ED File Offset: 0x0003A7ED
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x0003C5F5 File Offset: 0x0003A7F5
		public override string Context { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0003C5FE File Offset: 0x0003A7FE
		public override SecurityKey Key { get; }

		// Token: 0x06000F2C RID: 3884 RVA: 0x0003C608 File Offset: 0x0003A808
		private SymmetricAlgorithm CreateSymmetricAlgorithm()
		{
			if (!this.IsSupportedAlgorithm(this.Key, this.Algorithm))
			{
				throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10661: Unable to create the KeyWrapProvider.\nKeyWrapAlgorithm: '{0}', SecurityKey: '{1}'\n is not supported.", new object[]
				{
					LogHelper.MarkAsNonPII(this.Algorithm),
					this.Key
				})));
			}
			SymmetricAlgorithm symmetricAlgorithm = this.GetSymmetricAlgorithm(this.Key, this.Algorithm);
			if (symmetricAlgorithm == null)
			{
				throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10669: Failed to create symmetric algorithm.", Array.Empty<object>())));
			}
			return symmetricAlgorithm;
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0003C68C File Offset: 0x0003A88C
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed && disposing)
			{
				if (this._symmetricAlgorithm != null)
				{
					this._symmetricAlgorithm.Value.Dispose();
					this._symmetricAlgorithm = null;
				}
				if (this._symmetricAlgorithmEncryptor != null)
				{
					this._symmetricAlgorithmEncryptor.Dispose();
					this._symmetricAlgorithmEncryptor = null;
				}
				if (this._symmetricAlgorithmDecryptor != null)
				{
					this._symmetricAlgorithmDecryptor.Dispose();
					this._symmetricAlgorithmDecryptor = null;
				}
				this._disposed = true;
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0003C700 File Offset: 0x0003A900
		private static byte[] GetBytes(ulong i)
		{
			byte[] bytes = BitConverter.GetBytes(i);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}
			return bytes;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0003C724 File Offset: 0x0003A924
		protected virtual SymmetricAlgorithm GetSymmetricAlgorithm(SecurityKey key, string algorithm)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			if (!this.IsSupportedAlgorithm(key, algorithm))
			{
				throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10661: Unable to create the KeyWrapProvider.\nKeyWrapAlgorithm: '{0}', SecurityKey: '{1}'\n is not supported.", new object[]
				{
					LogHelper.MarkAsNonPII(algorithm),
					key
				})));
			}
			byte[] array = null;
			SymmetricSecurityKey symmetricSecurityKey = key as SymmetricSecurityKey;
			if (symmetricSecurityKey != null)
			{
				array = symmetricSecurityKey.Key;
			}
			else
			{
				JsonWebKey jsonWebKey = key as JsonWebKey;
				SecurityKey securityKey;
				if (jsonWebKey != null && JsonWebKeyConverter.TryConvertToSymmetricSecurityKey(jsonWebKey, out securityKey))
				{
					array = (securityKey as SymmetricSecurityKey).Key;
				}
			}
			if (array == null)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10657: The SecurityKey provided for the symmetric key wrap algorithm cannot be converted to byte array. Type is: '{0}'.", new object[] { LogHelper.MarkAsNonPII(key.GetType()) })));
			}
			this.ValidateKeySize(array, algorithm);
			SymmetricAlgorithm symmetricAlgorithm;
			try
			{
				Aes aes = Aes.Create();
				aes.Mode = CipherMode.ECB;
				aes.Padding = PaddingMode.None;
				aes.KeySize = array.Length * 8;
				aes.Key = array;
				byte[] array2 = new byte[aes.BlockSize >> 3];
				Utility.Zero(array2);
				aes.IV = array2;
				symmetricAlgorithm = aes;
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new InvalidOperationException(LogHelper.FormatInvariant("IDX10663: Failed to create symmetric algorithm with SecurityKey: '{0}', KeyWrapAlgorithm: '{1}'.", new object[]
				{
					key,
					LogHelper.MarkAsNonPII(algorithm)
				}), ex));
			}
			return symmetricAlgorithm;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0003C860 File Offset: 0x0003AA60
		protected virtual bool IsSupportedAlgorithm(SecurityKey key, string algorithm)
		{
			return SupportedAlgorithms.IsSupportedSymmetricKeyWrap(algorithm, key);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x0003C86C File Offset: 0x0003AA6C
		public override byte[] UnwrapKey(byte[] keyBytes)
		{
			if (keyBytes == null || keyBytes.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("keyBytes");
			}
			if (keyBytes.Length % 8 != 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10664: The length of input must be a multiple of 64 bits. The input size is: '{0}' bits.", new object[] { LogHelper.MarkAsNonPII(keyBytes.Length << 3) }), "keyBytes"));
			}
			if (this._disposed)
			{
				throw LogHelper.LogExceptionMessage(new ObjectDisposedException(base.GetType().ToString()));
			}
			byte[] array;
			try
			{
				array = this.UnwrapKeyPrivate(keyBytes, 0, keyBytes.Length);
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenKeyWrapException(LogHelper.FormatInvariant("IDX10659: UnwrapKey failed, exception from cryptographic operation: '{0}'", new object[] { ex })));
			}
			return array;
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0003C924 File Offset: 0x0003AB24
		private byte[] UnwrapKeyPrivate(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = new byte[8];
			Array.Copy(inputBuffer, inputOffset, array, 0, 8);
			int num = inputCount - 8 >> 3;
			byte[] array2 = new byte[num << 3];
			Array.Copy(inputBuffer, inputOffset + 8, array2, 0, inputCount - 8);
			if (this._symmetricAlgorithmDecryptor == null)
			{
				object decryptorLock = SymmetricKeyWrapProvider._decryptorLock;
				lock (decryptorLock)
				{
					if (this._symmetricAlgorithmDecryptor == null)
					{
						this._symmetricAlgorithmDecryptor = this._symmetricAlgorithm.Value.CreateDecryptor();
					}
				}
			}
			byte[] array3 = new byte[16];
			for (int i = 5; i >= 0; i--)
			{
				for (int j = num; j > 0; j--)
				{
					ulong num2 = (ulong)((long)(num * i + j));
					Utility.Xor(array, SymmetricKeyWrapProvider.GetBytes(num2), 0, true);
					Array.Copy(array, array3, 8);
					Array.Copy(array2, j - 1 << 3, array3, 8, 8);
					byte[] array4 = this._symmetricAlgorithmDecryptor.TransformFinalBlock(array3, 0, 16);
					Array.Copy(array4, array, 8);
					Array.Copy(array4, 8, array2, j - 1 << 3, 8);
				}
			}
			if (Utility.AreEqual(array, SymmetricKeyWrapProvider._defaultIV))
			{
				byte[] array5 = new byte[num << 3];
				for (int k = 0; k < num; k++)
				{
					Array.Copy(array2, k << 3, array5, k << 3, 8);
				}
				return array5;
			}
			throw LogHelper.LogExceptionMessage(new InvalidOperationException("IDX10665: Data is not authentic"));
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0003CA80 File Offset: 0x0003AC80
		private void ValidateKeySize(byte[] key, string algorithm)
		{
			if ("A128KW".Equals(algorithm) || "http://www.w3.org/2001/04/xmlenc#kw-aes128".Equals(algorithm))
			{
				if (key.Length != 16)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("key", LogHelper.FormatInvariant("IDX10662: The KeyWrap algorithm '{0}' requires a key size of '{1}' bits. Key '{2}', is of size:'{3}'.", new object[]
					{
						LogHelper.MarkAsNonPII(algorithm),
						LogHelper.MarkAsNonPII(128),
						this.Key.KeyId,
						LogHelper.MarkAsNonPII(key.Length << 3)
					})));
				}
				return;
			}
			else if ("A192KW".Equals(algorithm) || "http://www.w3.org/2001/04/xmlenc#kw-aes192".Equals(algorithm))
			{
				if (key.Length != 24)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("key", LogHelper.FormatInvariant("IDX10662: The KeyWrap algorithm '{0}' requires a key size of '{1}' bits. Key '{2}', is of size:'{3}'.", new object[]
					{
						LogHelper.MarkAsNonPII(algorithm),
						LogHelper.MarkAsNonPII(128),
						this.Key.KeyId,
						LogHelper.MarkAsNonPII(key.Length << 3)
					})));
				}
				return;
			}
			else
			{
				if (!"A256KW".Equals(algorithm) && !"http://www.w3.org/2001/04/xmlenc#kw-aes256".Equals(algorithm))
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("algorithm", LogHelper.FormatInvariant("IDX10652: The algorithm '{0}' is not supported.", new object[] { LogHelper.MarkAsNonPII(algorithm) })));
				}
				if (key.Length != 32)
				{
					throw LogHelper.LogExceptionMessage(new ArgumentOutOfRangeException("key", LogHelper.FormatInvariant("IDX10662: The KeyWrap algorithm '{0}' requires a key size of '{1}' bits. Key '{2}', is of size:'{3}'.", new object[]
					{
						LogHelper.MarkAsNonPII(algorithm),
						LogHelper.MarkAsNonPII(256),
						this.Key.KeyId,
						LogHelper.MarkAsNonPII(key.Length << 3)
					})));
				}
				return;
			}
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0003CC2C File Offset: 0x0003AE2C
		public override byte[] WrapKey(byte[] keyBytes)
		{
			if (keyBytes == null || keyBytes.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("keyBytes");
			}
			if (keyBytes.Length % 8 != 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10664: The length of input must be a multiple of 64 bits. The input size is: '{0}' bits.", new object[] { LogHelper.MarkAsNonPII(keyBytes.Length << 3) }), "keyBytes"));
			}
			if (this._disposed)
			{
				throw LogHelper.LogExceptionMessage(new ObjectDisposedException(base.GetType().ToString()));
			}
			byte[] array;
			try
			{
				array = this.WrapKeyPrivate(keyBytes, 0, keyBytes.Length);
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenKeyWrapException(LogHelper.FormatInvariant("IDX10658: WrapKey failed, exception from cryptographic operation: '{0}'", new object[] { ex })));
			}
			return array;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0003CCE4 File Offset: 0x0003AEE4
		private byte[] WrapKeyPrivate(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			byte[] array = SymmetricKeyWrapProvider._defaultIV.Clone() as byte[];
			int num = inputCount >> 3;
			byte[] array2 = new byte[num << 3];
			Array.Copy(inputBuffer, inputOffset, array2, 0, inputCount);
			if (this._symmetricAlgorithmEncryptor == null)
			{
				object encryptorLock = SymmetricKeyWrapProvider._encryptorLock;
				lock (encryptorLock)
				{
					if (this._symmetricAlgorithmEncryptor == null)
					{
						this._symmetricAlgorithmEncryptor = this._symmetricAlgorithm.Value.CreateEncryptor();
					}
				}
			}
			byte[] array3 = new byte[16];
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < num; j++)
				{
					ulong num2 = (ulong)((long)(num * i + j + 1));
					Array.Copy(array, array3, array.Length);
					Array.Copy(array2, j << 3, array3, 8, 8);
					byte[] array4 = this._symmetricAlgorithmEncryptor.TransformFinalBlock(array3, 0, 16);
					Array.Copy(array4, array, 8);
					Utility.Xor(array, SymmetricKeyWrapProvider.GetBytes(num2), 0, true);
					Array.Copy(array4, 8, array2, j << 3, 8);
				}
			}
			byte[] array5 = new byte[num + 1 << 3];
			Array.Copy(array, array5, array.Length);
			for (int k = 0; k < num; k++)
			{
				Array.Copy(array2, k << 3, array5, k + 1 << 3, 8);
			}
			return array5;
		}

		// Token: 0x040004D4 RID: 1236
		private static readonly byte[] _defaultIV = new byte[] { 166, 166, 166, 166, 166, 166, 166, 166 };

		// Token: 0x040004D5 RID: 1237
		private const int _blockSizeInBits = 64;

		// Token: 0x040004D6 RID: 1238
		private const int _blockSizeInBytes = 8;

		// Token: 0x040004D7 RID: 1239
		private static object _encryptorLock = new object();

		// Token: 0x040004D8 RID: 1240
		private static object _decryptorLock = new object();

		// Token: 0x040004D9 RID: 1241
		private Lazy<SymmetricAlgorithm> _symmetricAlgorithm;

		// Token: 0x040004DA RID: 1242
		private ICryptoTransform _symmetricAlgorithmEncryptor;

		// Token: 0x040004DB RID: 1243
		private ICryptoTransform _symmetricAlgorithmDecryptor;

		// Token: 0x040004DC RID: 1244
		private bool _disposed;
	}
}
