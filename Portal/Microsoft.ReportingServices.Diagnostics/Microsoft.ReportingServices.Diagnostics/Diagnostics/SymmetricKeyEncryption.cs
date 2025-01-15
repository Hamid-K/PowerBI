using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using RSManagedCrypto;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000048 RID: 72
	internal sealed class SymmetricKeyEncryption : Encryption
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000B30A File Offset: 0x0000950A
		public static SymmetricKeyEncryption Instance
		{
			get
			{
				return SymmetricKeyEncryption._instance;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000B311 File Offset: 0x00009511
		private SymmetricKeyEncryption()
		{
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000B319 File Offset: 0x00009519
		protected override bool IsEncryptionChecked()
		{
			return this._encryptionChecked;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000B321 File Offset: 0x00009521
		protected override void SetEncryptionChecked()
		{
			this._encryptionChecked = true;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000B32C File Offset: 0x0000952C
		protected override byte[] EncryptInternal(byte[] data)
		{
			if (data == null)
			{
				return null;
			}
			byte[] array;
			try
			{
				SymmetricKeyEncryption.RWLock.AcquireReaderLock(-1);
				SymmetricKeyEncryption.ThrowIfNotInitialized();
				array = SymmetricKeyEncryption._crypto.EncryptData(data);
			}
			catch (Exception ex)
			{
				if (RSTrace.CryptoTrace.TraceError)
				{
					RSTrace.CryptoTrace.Trace(TraceLevel.Error, "ProtectData failed: " + ex.ToString());
				}
				if (RSTrace.CryptoTrace.TraceInfo)
				{
					RSTrace.CryptoTrace.Trace(TraceLevel.Info, "Current user: {0}", new object[] { UserUtil.GetWindowsIdentityName() });
				}
				throw;
			}
			finally
			{
				SymmetricKeyEncryption.RWLock.ReleaseReaderLock();
			}
			return array;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000B3DC File Offset: 0x000095DC
		public override byte[] Decrypt(byte[] data, bool useSalt = true)
		{
			if (data == null)
			{
				return null;
			}
			byte[] array;
			try
			{
				SymmetricKeyEncryption.RWLock.AcquireReaderLock(-1);
				SymmetricKeyEncryption.ThrowIfNotInitialized();
				array = SymmetricKeyEncryption._crypto.DecryptData(data, useSalt);
			}
			catch (COMException ex)
			{
				if (RSTrace.CryptoTrace.TraceError)
				{
					RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Decryption failed, Current user: {0} : {1}", new object[]
					{
						UserUtil.GetWindowsIdentityName(),
						ex
					});
				}
				if (ex.ErrorCode == -2146893819)
				{
					throw new CannotValidateEncryptedDataException();
				}
				throw;
			}
			catch (Exception ex2)
			{
				if (RSTrace.CryptoTrace.TraceError)
				{
					RSTrace.CryptoTrace.Trace(TraceLevel.Error, "Decryption failed, Current user: {0} : {1}", new object[]
					{
						UserUtil.GetWindowsIdentityName(),
						ex2
					});
				}
				throw;
			}
			finally
			{
				SymmetricKeyEncryption.RWLock.ReleaseReaderLock();
			}
			return array;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000B4BC File Offset: 0x000096BC
		public static void ThrowIfNotInitialized()
		{
			if (!SymmetricKeyEncryption.IsInitialized)
			{
				throw new CannotValidateEncryptedDataException();
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000B4CB File Offset: 0x000096CB
		private static ReaderWriterLock RWLock
		{
			get
			{
				return SymmetricKeyEncryption._rwLock;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000B4D2 File Offset: 0x000096D2
		public static byte[] CreateSymmetricKey()
		{
			return SymmetricKeyEncryption._crypto.CreateSymmetricKey();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000B4DE File Offset: 0x000096DE
		public static byte[] PasswordDecryptSymmetricKey(byte[] encryptedKey, string encryptPassword)
		{
			return SymmetricKeyEncryption._crypto.ImportSymmetricKey(encryptedKey, encryptPassword);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000B4EC File Offset: 0x000096EC
		public static byte[] GetPublicKeyEncryptedSymmetricKey(byte[] publicKeyBlob)
		{
			return SymmetricKeyEncryption._crypto.ExportSymmetricKey(publicKeyBlob);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B4F9 File Offset: 0x000096F9
		public static void SetSymmetricKeyFromPublicKeyEncryptedBlob(byte[] encryptedSymmetricKey)
		{
			SymmetricKeyEncryption._crypto.ImportSymmetricKey(encryptedSymmetricKey);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B506 File Offset: 0x00009706
		public static byte[] PasswordEncryptSymmetricKey(string password)
		{
			return SymmetricKeyEncryption._crypto.ExportSymmetricKey(password);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000B513 File Offset: 0x00009713
		public static byte[] ReencryptSymmetricKey(byte[] encryptedSymmetricKey, byte[] publicKey)
		{
			return SymmetricKeyEncryption._crypto.ReencryptSymmetricKey(encryptedSymmetricKey, publicKey);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B4F9 File Offset: 0x000096F9
		public static void SetPublicKeyEncryptedSymmetricKey(byte[] encryptedSymmetricKey)
		{
			SymmetricKeyEncryption._crypto.ImportSymmetricKey(encryptedSymmetricKey);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B524 File Offset: 0x00009724
		public static void ResetKeyManager()
		{
			SymmetricKeyEncryption.AcquireWriterLock();
			try
			{
				SymmetricKeyEncryption._crypto.Dispose();
				SymmetricKeyEncryption._crypto = new RSCrypto();
			}
			finally
			{
				SymmetricKeyEncryption.ReleaseWriterLock();
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B564 File Offset: 0x00009764
		public static void SetKeyManager(RSCrypto keyManager)
		{
			if (keyManager == null)
			{
				throw new ArgumentNullException("KeyManager cannot be set to null");
			}
			SymmetricKeyEncryption.AcquireWriterLock();
			try
			{
				SymmetricKeyEncryption._crypto.Dispose();
				SymmetricKeyEncryption._crypto = keyManager;
			}
			finally
			{
				SymmetricKeyEncryption.ReleaseWriterLock();
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B5AC File Offset: 0x000097AC
		public static void AcquireWriterLock()
		{
			if (SymmetricKeyEncryption.RWLock.IsReaderLockHeld)
			{
				SymmetricKeyEncryption.RWLock.UpgradeToWriterLock(-1);
				return;
			}
			SymmetricKeyEncryption.RWLock.AcquireWriterLock(-1);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B5D2 File Offset: 0x000097D2
		public static void ReleaseWriterLock()
		{
			SymmetricKeyEncryption.RWLock.ReleaseWriterLock();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B5E0 File Offset: 0x000097E0
		public static byte[] GetPublicKey()
		{
			byte[] publicKey_Locked;
			try
			{
				publicKey_Locked = SymmetricKeyEncryption.GetPublicKey_Locked();
			}
			catch (COMException ex)
			{
				RSTrace.CryptoTrace.Trace(TraceLevel.Error, "ExportPublicKey failed: " + ex.ToString());
				if (ex.ErrorCode == -2146893813)
				{
					throw new CannotValidateEncryptedDataException();
				}
				throw;
			}
			return publicKey_Locked;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B638 File Offset: 0x00009838
		public static byte[] GetOrRepairPublicKey()
		{
			byte[] array;
			try
			{
				array = SymmetricKeyEncryption.GetPublicKey_Locked();
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode != -2146893813)
				{
					RSTrace.CryptoTrace.Trace(TraceLevel.Error, "ExportPublicKey failed: " + ex.ToString());
					throw;
				}
				SymmetricKeyEncryption._crypto.CreateKeyContainer();
				array = SymmetricKeyEncryption._crypto.ExportPublicKey();
			}
			return array;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B6A0 File Offset: 0x000098A0
		private static byte[] GetPublicKey_Locked()
		{
			SymmetricKeyEncryption.AcquireWriterLock();
			SymmetricKeyEncryption.ResetKeyManager();
			byte[] array;
			try
			{
				array = SymmetricKeyEncryption._crypto.ExportPublicKey();
			}
			finally
			{
				SymmetricKeyEncryption.ReleaseWriterLock();
			}
			return array;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000B6DC File Offset: 0x000098DC
		public static bool IsInitialized
		{
			get
			{
				SymmetricKeyEncryption.RWLock.AcquireReaderLock(-1);
				bool flag;
				try
				{
					flag = SymmetricKeyEncryption._crypto != null && SymmetricKeyEncryption._crypto.IsInit();
				}
				finally
				{
					SymmetricKeyEncryption.RWLock.ReleaseReaderLock();
				}
				return flag;
			}
		}

		// Token: 0x04000219 RID: 537
		private static readonly ReaderWriterLock _rwLock = new ReaderWriterLock();

		// Token: 0x0400021A RID: 538
		private static RSCrypto _crypto = new RSCrypto();

		// Token: 0x0400021B RID: 539
		private static readonly SymmetricKeyEncryption _instance = new SymmetricKeyEncryption();

		// Token: 0x0400021C RID: 540
		private bool _encryptionChecked;
	}
}
