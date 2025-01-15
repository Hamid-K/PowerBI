using System;
using System.Security.Cryptography;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02001FF6 RID: 8182
	public sealed class HMACSHA1Cng : HMAC, ICngAlgorithm
	{
		// Token: 0x0600C765 RID: 51045 RVA: 0x0027B253 File Offset: 0x00279453
		public HMACSHA1Cng()
			: this(RNGCng.GenerateKey(64))
		{
		}

		// Token: 0x0600C766 RID: 51046 RVA: 0x0027B262 File Offset: 0x00279462
		public HMACSHA1Cng(byte[] key)
			: this(key, CngProvider2.MicrosoftPrimitiveAlgorithmProvider)
		{
		}

		// Token: 0x0600C767 RID: 51047 RVA: 0x0027B270 File Offset: 0x00279470
		public HMACSHA1Cng(byte[] key, CngProvider algorithmProvider)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (algorithmProvider == null)
			{
				throw new ArgumentNullException("algorithmProvider");
			}
			this.m_hmac = new BCryptHMAC(CngAlgorithm.Sha1, algorithmProvider, "SHA1", 64, key);
			base.HashName = this.m_hmac.HashName;
		}

		// Token: 0x0600C768 RID: 51048 RVA: 0x0027B2D0 File Offset: 0x002794D0
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.m_hmac != null)
				{
					((IDisposable)this.m_hmac).Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17003044 RID: 12356
		// (get) Token: 0x0600C769 RID: 51049 RVA: 0x0027B310 File Offset: 0x00279510
		public override bool CanReuseTransform
		{
			get
			{
				return this.m_hmac.CanReuseTransform;
			}
		}

		// Token: 0x17003045 RID: 12357
		// (get) Token: 0x0600C76A RID: 51050 RVA: 0x0027B31D File Offset: 0x0027951D
		public override bool CanTransformMultipleBlocks
		{
			get
			{
				return this.m_hmac.CanTransformMultipleBlocks;
			}
		}

		// Token: 0x17003046 RID: 12358
		// (get) Token: 0x0600C76B RID: 51051 RVA: 0x0027B32A File Offset: 0x0027952A
		public override byte[] Hash
		{
			get
			{
				return this.m_hmac.Hash;
			}
		}

		// Token: 0x17003047 RID: 12359
		// (get) Token: 0x0600C76C RID: 51052 RVA: 0x0027B337 File Offset: 0x00279537
		public override int HashSize
		{
			get
			{
				return this.m_hmac.HashSize;
			}
		}

		// Token: 0x17003048 RID: 12360
		// (get) Token: 0x0600C76D RID: 51053 RVA: 0x0027B344 File Offset: 0x00279544
		public override int InputBlockSize
		{
			get
			{
				return this.m_hmac.InputBlockSize;
			}
		}

		// Token: 0x17003049 RID: 12361
		// (get) Token: 0x0600C76E RID: 51054 RVA: 0x0027B351 File Offset: 0x00279551
		// (set) Token: 0x0600C76F RID: 51055 RVA: 0x0027B35E File Offset: 0x0027955E
		public override byte[] Key
		{
			get
			{
				return this.m_hmac.Key;
			}
			set
			{
				this.m_hmac.Key = value;
			}
		}

		// Token: 0x1700304A RID: 12362
		// (get) Token: 0x0600C770 RID: 51056 RVA: 0x0027B36C File Offset: 0x0027956C
		public override int OutputBlockSize
		{
			get
			{
				return this.m_hmac.OutputBlockSize;
			}
		}

		// Token: 0x1700304B RID: 12363
		// (get) Token: 0x0600C771 RID: 51057 RVA: 0x0027B379 File Offset: 0x00279579
		public CngProvider Provider
		{
			get
			{
				return this.m_hmac.Provider;
			}
		}

		// Token: 0x0600C772 RID: 51058 RVA: 0x0027B386 File Offset: 0x00279586
		protected override void HashCore(byte[] rgb, int ib, int cb)
		{
			this.m_hmac.HashCoreImpl(rgb, ib, cb);
		}

		// Token: 0x0600C773 RID: 51059 RVA: 0x0027B396 File Offset: 0x00279596
		protected override byte[] HashFinal()
		{
			return this.m_hmac.HashFinalImpl();
		}

		// Token: 0x0600C774 RID: 51060 RVA: 0x0027B3A3 File Offset: 0x002795A3
		public override void Initialize()
		{
			this.m_hmac.Initialize();
		}

		// Token: 0x040065DF RID: 26079
		private const int BlockSize = 64;

		// Token: 0x040065E0 RID: 26080
		private BCryptHMAC m_hmac;
	}
}
