using System;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000136 RID: 310
	public class RsaKeyWrapProvider : KeyWrapProvider
	{
		// Token: 0x06000F1D RID: 3869 RVA: 0x0003C36C File Offset: 0x0003A56C
		public RsaKeyWrapProvider(SecurityKey key, string algorithm, bool willUnwrap)
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
			this._willUnwrap = willUnwrap;
			this._asymmetricAdapter = new Lazy<AsymmetricAdapter>(new Func<AsymmetricAdapter>(this.CreateAsymmetricAdapter));
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003C3CC File Offset: 0x0003A5CC
		internal AsymmetricAdapter CreateAsymmetricAdapter()
		{
			if (!this.IsSupportedAlgorithm(this.Key, this.Algorithm))
			{
				throw LogHelper.LogExceptionMessage(new NotSupportedException(LogHelper.FormatInvariant("IDX10661: Unable to create the KeyWrapProvider.\nKeyWrapAlgorithm: '{0}', SecurityKey: '{1}'\n is not supported.", new object[]
				{
					LogHelper.MarkAsNonPII(this.Algorithm),
					this.Key
				})));
			}
			return new AsymmetricAdapter(this.Key, this.Algorithm, this._willUnwrap);
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0003C436 File Offset: 0x0003A636
		public override string Algorithm { get; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0003C43E File Offset: 0x0003A63E
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x0003C446 File Offset: 0x0003A646
		public override string Context { get; set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0003C44F File Offset: 0x0003A64F
		public override SecurityKey Key { get; }

		// Token: 0x06000F23 RID: 3875 RVA: 0x0003C457 File Offset: 0x0003A657
		protected override void Dispose(bool disposing)
		{
			if (!this._disposed && disposing)
			{
				this._disposed = true;
				this._asymmetricAdapter.Value.Dispose();
			}
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0003C47B File Offset: 0x0003A67B
		protected virtual bool IsSupportedAlgorithm(SecurityKey key, string algorithm)
		{
			return SupportedAlgorithms.IsSupportedRsaKeyWrap(algorithm, key);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003C484 File Offset: 0x0003A684
		public override byte[] UnwrapKey(byte[] keyBytes)
		{
			if (keyBytes == null || keyBytes.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("keyBytes");
			}
			if (this._disposed)
			{
				throw LogHelper.LogExceptionMessage(new ObjectDisposedException(base.GetType().ToString()));
			}
			byte[] array;
			try
			{
				array = this._asymmetricAdapter.Value.Decrypt(keyBytes);
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenKeyWrapException(LogHelper.FormatInvariant("IDX10659: UnwrapKey failed, exception from cryptographic operation: '{0}'", new object[] { ex })));
			}
			return array;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0003C508 File Offset: 0x0003A708
		public override byte[] WrapKey(byte[] keyBytes)
		{
			if (keyBytes == null || keyBytes.Length == 0)
			{
				throw LogHelper.LogArgumentNullException("keyBytes");
			}
			if (this._disposed)
			{
				throw LogHelper.LogExceptionMessage(new ObjectDisposedException(base.GetType().ToString()));
			}
			byte[] array;
			try
			{
				array = this._asymmetricAdapter.Value.Encrypt(keyBytes);
			}
			catch (Exception ex)
			{
				throw LogHelper.LogExceptionMessage(new SecurityTokenKeyWrapException(LogHelper.FormatInvariant("IDX10658: WrapKey failed, exception from cryptographic operation: '{0}'", new object[] { ex })));
			}
			return array;
		}

		// Token: 0x040004CE RID: 1230
		private Lazy<AsymmetricAdapter> _asymmetricAdapter;

		// Token: 0x040004CF RID: 1231
		private bool _disposed;

		// Token: 0x040004D0 RID: 1232
		private bool _willUnwrap;
	}
}
