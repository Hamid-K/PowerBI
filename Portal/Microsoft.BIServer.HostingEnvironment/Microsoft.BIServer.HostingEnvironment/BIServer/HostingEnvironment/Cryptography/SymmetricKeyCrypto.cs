using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using Microsoft.BIServer.HostingEnvironment.Cryptography.Exceptions;

namespace Microsoft.BIServer.HostingEnvironment.Cryptography
{
	// Token: 0x02000039 RID: 57
	public sealed class SymmetricKeyCrypto : Crypto
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000054CB File Offset: 0x000036CB
		public static SymmetricKeyCrypto Instance
		{
			get
			{
				return SymmetricKeyCrypto._instance;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000054D4 File Offset: 0x000036D4
		private SymmetricKeyCrypto()
		{
			this._crypto = new TripleDESCryptoServiceProvider();
			this._rsaKeyStore = new CspParameters(24, null, "Microsoft SQL Server Reporting Services Key Container 2010");
			this._rsaKeyStore.Flags = CspProviderFlags.UseExistingKey;
			this._rsaKeyStore.KeyNumber = 1;
			Logger.Info("Provider name {0}", new object[] { this._rsaKeyStore.ProviderName });
			Logger.Info("Container name {0}", new object[] { this._rsaKeyStore.KeyContainerName });
			this._currentSafeKey = null;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000556B File Offset: 0x0000376B
		public override string GetName()
		{
			return this._name;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005574 File Offset: 0x00003774
		protected override byte[] EncryptInternal(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			byte[] array;
			try
			{
				this.ThrowIfNotInitialized();
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (ICryptoTransform cryptoTransform = this._crypto.CreateEncryptor())
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
						{
							cryptoStream.Write(this._crypto.IV, 0, this._crypto.IV.Length);
							cryptoStream.Write(data, 0, data.Length);
							cryptoStream.Close();
							array = memoryStream.ToArray();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "ProtectData failed, Current user: {0}", new object[] { WindowsIdentity.GetCurrent().Name });
				throw;
			}
			return array;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005654 File Offset: 0x00003854
		protected override byte[] DecryptInternal(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			byte[] array2;
			try
			{
				this.ThrowIfNotInitialized();
				int num = this._crypto.IV.Length;
				byte[] array = new byte[num];
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (MemoryStream memoryStream2 = new MemoryStream(data))
					{
						using (ICryptoTransform cryptoTransform = this._crypto.CreateDecryptor())
						{
							using (CryptoStream cryptoStream = new CryptoStream(memoryStream2, cryptoTransform, CryptoStreamMode.Read))
							{
								cryptoStream.Read(array, 0, num);
								cryptoStream.CopyTo(memoryStream);
								array2 = memoryStream.ToArray();
							}
						}
					}
				}
			}
			catch (COMException ex)
			{
				Logger.Error(ex, "Decryption failed, Current user: {0}", new object[] { WindowsIdentity.GetCurrent().Name });
				if (ex.ErrorCode == -2146893819)
				{
					throw new CannotValidateEncryptedDataException();
				}
				throw new CryptoException(ex, "Decryption Failure");
			}
			catch (Exception ex2)
			{
				Logger.Error(ex2, "Decryption failed, Current user: {0}", new object[] { WindowsIdentity.GetCurrent().Name });
				throw new CryptoException(ex2, "Decryption Failure");
			}
			return array2;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000057B4 File Offset: 0x000039B4
		public void SetPublicKeyEncryptedSymmetricKey(byte[] symmetricKeyBlob)
		{
			if (this.IsInitialized)
			{
				if (symmetricKeyBlob.SequenceEqual(this._currentSafeKey))
				{
					Logger.Trace("Ignoring set of symmetric key (encrypted sources are equal)", Array.Empty<object>());
					return;
				}
				Logger.Warning("Changing Symmetric key", Array.Empty<object>());
			}
			else
			{
				Logger.Info("Setting Symmetric Key", Array.Empty<object>());
			}
			try
			{
				this._name = "SymmetricKeyCrypto [unknown]";
				this._currentSafeKey = (byte[])symmetricKeyBlob.Clone();
				this._crypto.Key = this.RsaDecrypt(this.ConvertNativeRsCryptoToManagedRsaFormat(symmetricKeyBlob));
				this._crypto.GenerateIV();
				this._intialized = true;
				this._name = "SymmetricKeyCrypto [From Key Store]";
			}
			catch (CryptographicException ex)
			{
				string text = "Failed: " + ex.Message;
				this._name = string.Format("SymmetricKeyCrypto [{0}]", text);
				this._currentSafeKey = null;
				Logger.Error(ex, text, Array.Empty<object>());
				throw new SymmetricKeyNotInitializedException(ex.Message);
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000058B0 File Offset: 0x00003AB0
		private byte[] ConvertNativeRsCryptoToManagedRsaFormat(byte[] keyBlob)
		{
			return keyBlob.Skip(SymmetricKeyCrypto.BlobHeaderSize).Reverse<byte>().ToArray<byte>();
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000058C7 File Offset: 0x00003AC7
		private byte[] ConvertManagedRsaToNativeRsCryptoFormat(byte[] key)
		{
			return SymmetricKeyCrypto.DummyBlobHeader.Concat(key.Reverse<byte>()).ToArray<byte>();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000058E0 File Offset: 0x00003AE0
		public void CreateSymmetricKey()
		{
			if (this.IsInitialized)
			{
				Logger.Warning("Recreating Symmetric key", Array.Empty<object>());
			}
			else
			{
				Logger.Info("Creating Symmetric Key", Array.Empty<object>());
			}
			this._name = "SymmetricKeyCrypto [creating...]";
			this._crypto.GenerateKey();
			this._crypto.GenerateIV();
			this._intialized = true;
			this._name = "SymmetricKeyCrypto [created]";
			this.SetCurrentSafeKey();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000594E File Offset: 0x00003B4E
		public byte[] GetPublicKeyEncryptedSymmetricKey()
		{
			return this._currentSafeKey;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005958 File Offset: 0x00003B58
		private void SetCurrentSafeKey()
		{
			try
			{
				this._currentSafeKey = this.ConvertManagedRsaToNativeRsCryptoFormat(this.RsaEncryptSymKey());
			}
			catch (CryptographicException ex)
			{
				Logger.Warning(ex, "Encryption error", Array.Empty<object>());
				throw;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000599C File Offset: 0x00003B9C
		private byte[] RsaEncryptSymKey()
		{
			byte[] array;
			using (RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(this._rsaKeyStore))
			{
				array = rsacryptoServiceProvider.Encrypt(this._crypto.Key, false);
			}
			return array;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000059E8 File Offset: 0x00003BE8
		private byte[] RsaDecrypt(byte[] encryptedSymmetricKey)
		{
			byte[] array;
			using (RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(this._rsaKeyStore))
			{
				array = rsacryptoServiceProvider.Decrypt(encryptedSymmetricKey, false);
			}
			return array;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00005A28 File Offset: 0x00003C28
		public bool IsInitialized
		{
			get
			{
				return this._intialized;
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005A30 File Offset: 0x00003C30
		private void ThrowIfNotInitialized()
		{
			if (!this.IsInitialized)
			{
				throw new CannotValidateEncryptedDataException();
			}
		}

		// Token: 0x04000098 RID: 152
		private static readonly SymmetricKeyCrypto _instance = new SymmetricKeyCrypto();

		// Token: 0x04000099 RID: 153
		public const string KeyContainerName = "Microsoft SQL Server Reporting Services Key Container 2010";

		// Token: 0x0400009A RID: 154
		public const string KeyContainerNamePrevious = "Microsoft SQL Server Reporting Services Key Container";

		// Token: 0x0400009B RID: 155
		private const int PROV_RSA_AES = 24;

		// Token: 0x0400009C RID: 156
		private readonly CspParameters _rsaKeyStore;

		// Token: 0x0400009D RID: 157
		private byte[] _currentSafeKey;

		// Token: 0x0400009E RID: 158
		private readonly TripleDESCryptoServiceProvider _crypto;

		// Token: 0x0400009F RID: 159
		private bool _intialized;

		// Token: 0x040000A0 RID: 160
		private const CspProviderFlags ACQUIRE_EXISTING = CspProviderFlags.UseExistingKey;

		// Token: 0x040000A1 RID: 161
		private const int AT_KEYEXCHANGE = 1;

		// Token: 0x040000A2 RID: 162
		private string _name = "SymmetricKeyCrypto [Uninitialized]";

		// Token: 0x040000A3 RID: 163
		private static readonly byte[] DummyBlobHeader = new byte[]
		{
			1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
			11, 12
		};

		// Token: 0x040000A4 RID: 164
		private static readonly int BlobHeaderSize = SymmetricKeyCrypto.DummyBlobHeader.Length;

		// Token: 0x040000A5 RID: 165
		private const bool NoOaepPadding = false;
	}
}
