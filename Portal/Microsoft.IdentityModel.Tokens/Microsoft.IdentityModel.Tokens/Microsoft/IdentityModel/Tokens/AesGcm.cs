using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000164 RID: 356
	internal class AesGcm : IDisposable
	{
		// Token: 0x06001069 RID: 4201 RVA: 0x0003FE57 File Offset: 0x0003E057
		public AesGcm(byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.ImportKey(key);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0003FE74 File Offset: 0x0003E074
		private void ImportKey(byte[] key)
		{
			this._keyHandle = Interop.BCrypt.BCryptImportKey(AesGcm.s_aesGcm, key);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0003FE87 File Offset: 0x0003E087
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0003FE96 File Offset: 0x0003E096
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed && disposing)
			{
				this._disposed = true;
				this._keyHandle.Dispose();
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0003FEB7 File Offset: 0x0003E0B7
		public void Decrypt(byte[] nonce, byte[] ciphertext, byte[] tag, byte[] plaintext, byte[] associatedData = null)
		{
			AesAead.CheckArgumentsForNull(nonce, plaintext, ciphertext, tag);
			AesAead.Decrypt(this._keyHandle, nonce, associatedData, ciphertext, tag, plaintext, true);
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0003FED6 File Offset: 0x0003E0D6
		internal void Encrypt(byte[] nonce, byte[] plaintext, byte[] ciphertext, byte[] tag, byte[] associatedData = null)
		{
			AesAead.CheckArgumentsForNull(nonce, plaintext, ciphertext, tag);
			AesAead.Encrypt(this._keyHandle, nonce, associatedData, plaintext, ciphertext, tag);
		}

		// Token: 0x0400060F RID: 1551
		public const int NonceSize = 12;

		// Token: 0x04000610 RID: 1552
		public const int TagSize = 16;

		// Token: 0x04000611 RID: 1553
		private static readonly SafeAlgorithmHandle s_aesGcm = AesBCryptModes.OpenAesAlgorithm("ChainingModeGCM").Value;

		// Token: 0x04000612 RID: 1554
		private SafeKeyHandle _keyHandle;

		// Token: 0x04000613 RID: 1555
		private bool _disposed;
	}
}
