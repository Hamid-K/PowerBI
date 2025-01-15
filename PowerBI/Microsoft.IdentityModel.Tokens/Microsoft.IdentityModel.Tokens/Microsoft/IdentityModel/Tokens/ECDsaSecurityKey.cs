using System;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200012E RID: 302
	public class ECDsaSecurityKey : AsymmetricSecurityKey
	{
		// Token: 0x06000ECC RID: 3788 RVA: 0x0003B284 File Offset: 0x00039484
		internal ECDsaSecurityKey(JsonWebKey webKey, bool usePrivateKey)
			: base(webKey)
		{
			this.ECDsa = ECDsaAdapter.Instance.CreateECDsa(webKey, usePrivateKey);
			webKey.ConvertedSecurityKey = this;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0003B2A6 File Offset: 0x000394A6
		public ECDsaSecurityKey(ECDsa ecdsa)
		{
			if (ecdsa == null)
			{
				throw LogHelper.LogArgumentNullException("ecdsa");
			}
			this.ECDsa = ecdsa;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0003B2C4 File Offset: 0x000394C4
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x0003B2CC File Offset: 0x000394CC
		public ECDsa ECDsa { get; private set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0003B2D8 File Offset: 0x000394D8
		[Obsolete("HasPrivateKey method is deprecated, please use FoundPrivateKey instead.")]
		public override bool HasPrivateKey
		{
			get
			{
				if (this._hasPrivateKey == null)
				{
					try
					{
						this.ECDsa.SignHash(new byte[20]);
						this._hasPrivateKey = new bool?(true);
					}
					catch (CryptographicException)
					{
						this._hasPrivateKey = new bool?(false);
					}
				}
				return this._hasPrivateKey.Value;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0003B340 File Offset: 0x00039540
		public override PrivateKeyStatus PrivateKeyStatus
		{
			get
			{
				return PrivateKeyStatus.Unknown;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0003B343 File Offset: 0x00039543
		public override int KeySize
		{
			get
			{
				return this.ECDsa.KeySize;
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0003B350 File Offset: 0x00039550
		public override bool CanComputeJwkThumbprint()
		{
			return false;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003B353 File Offset: 0x00039553
		public override byte[] ComputeJwkThumbprint()
		{
			throw LogHelper.LogExceptionMessage(new PlatformNotSupportedException("IDX10695: Unable to create a JsonWebKey from an ECDsa object. Required ECParameters structure is not supported by .NET Framework < 4.7."));
		}

		// Token: 0x040004B6 RID: 1206
		private bool? _hasPrivateKey;
	}
}
