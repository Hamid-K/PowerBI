using System;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000179 RID: 377
	public class SymmetricSecurityKey : SecurityKey
	{
		// Token: 0x0600110F RID: 4367 RVA: 0x000419B4 File Offset: 0x0003FBB4
		internal SymmetricSecurityKey(JsonWebKey webKey)
			: base(webKey)
		{
			if (string.IsNullOrEmpty(webKey.K))
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10703: Cannot create a '{0}', key length is zero.", new object[] { LogHelper.MarkAsNonPII(typeof(SymmetricSecurityKey)) })));
			}
			this._key = Base64UrlEncoder.DecodeBytes(webKey.K);
			this._keySize = this._key.Length * 8;
			webKey.ConvertedSecurityKey = this;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00041A2C File Offset: 0x0003FC2C
		public SymmetricSecurityKey(byte[] key)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			if (key.Length == 0)
			{
				throw LogHelper.LogExceptionMessage(new ArgumentException(LogHelper.FormatInvariant("IDX10703: Cannot create a '{0}', key length is zero.", new object[] { LogHelper.MarkAsNonPII(typeof(SymmetricSecurityKey)) })));
			}
			this._key = key.CloneByteArray();
			this._keySize = this._key.Length * 8;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x00041A9A File Offset: 0x0003FC9A
		public override int KeySize
		{
			get
			{
				return this._keySize;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00041AA2 File Offset: 0x0003FCA2
		public virtual byte[] Key
		{
			get
			{
				return this._key.CloneByteArray();
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00041AAF File Offset: 0x0003FCAF
		public override bool CanComputeJwkThumbprint()
		{
			return true;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00041AB2 File Offset: 0x0003FCB2
		public override byte[] ComputeJwkThumbprint()
		{
			return Utility.GenerateSha256Hash("{\"k\":\"" + Base64UrlEncoder.Encode(this.Key) + "\",\"kty\":\"oct\"}");
		}

		// Token: 0x04000684 RID: 1668
		private int _keySize;

		// Token: 0x04000685 RID: 1669
		private byte[] _key;
	}
}
