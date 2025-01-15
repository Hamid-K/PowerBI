using System;
using System.Security.Cryptography;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02001FF7 RID: 8183
	public sealed class HMACSHA256Cng : HMAC, ICngAlgorithm
	{
		// Token: 0x0600C775 RID: 51061 RVA: 0x0027B3B0 File Offset: 0x002795B0
		public HMACSHA256Cng()
			: this(RNGCng.GenerateKey(64))
		{
		}

		// Token: 0x0600C776 RID: 51062 RVA: 0x0027B3BF File Offset: 0x002795BF
		public HMACSHA256Cng(byte[] key)
			: this(key, CngProvider2.MicrosoftPrimitiveAlgorithmProvider)
		{
		}

		// Token: 0x0600C777 RID: 51063 RVA: 0x0027B3D0 File Offset: 0x002795D0
		public HMACSHA256Cng(byte[] key, CngProvider algorithmProvider)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (algorithmProvider == null)
			{
				throw new ArgumentNullException("algorithmProvider");
			}
			this.m_hmac = new BCryptHMAC(CngAlgorithm.Sha256, algorithmProvider, "SHA256", 64, key);
			base.HashName = this.m_hmac.HashName;
		}

		// Token: 0x0600C778 RID: 51064 RVA: 0x0027B430 File Offset: 0x00279630
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

		// Token: 0x1700304C RID: 12364
		// (get) Token: 0x0600C779 RID: 51065 RVA: 0x0027B470 File Offset: 0x00279670
		public override bool CanReuseTransform
		{
			get
			{
				return this.m_hmac.CanReuseTransform;
			}
		}

		// Token: 0x1700304D RID: 12365
		// (get) Token: 0x0600C77A RID: 51066 RVA: 0x0027B47D File Offset: 0x0027967D
		public override bool CanTransformMultipleBlocks
		{
			get
			{
				return this.m_hmac.CanTransformMultipleBlocks;
			}
		}

		// Token: 0x1700304E RID: 12366
		// (get) Token: 0x0600C77B RID: 51067 RVA: 0x0027B48A File Offset: 0x0027968A
		public override byte[] Hash
		{
			get
			{
				return this.m_hmac.Hash;
			}
		}

		// Token: 0x1700304F RID: 12367
		// (get) Token: 0x0600C77C RID: 51068 RVA: 0x0027B497 File Offset: 0x00279697
		public override int HashSize
		{
			get
			{
				return this.m_hmac.HashSize;
			}
		}

		// Token: 0x17003050 RID: 12368
		// (get) Token: 0x0600C77D RID: 51069 RVA: 0x0027B4A4 File Offset: 0x002796A4
		public override int InputBlockSize
		{
			get
			{
				return this.m_hmac.InputBlockSize;
			}
		}

		// Token: 0x17003051 RID: 12369
		// (get) Token: 0x0600C77E RID: 51070 RVA: 0x0027B4B1 File Offset: 0x002796B1
		// (set) Token: 0x0600C77F RID: 51071 RVA: 0x0027B4BE File Offset: 0x002796BE
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

		// Token: 0x17003052 RID: 12370
		// (get) Token: 0x0600C780 RID: 51072 RVA: 0x0027B4CC File Offset: 0x002796CC
		public override int OutputBlockSize
		{
			get
			{
				return this.m_hmac.OutputBlockSize;
			}
		}

		// Token: 0x17003053 RID: 12371
		// (get) Token: 0x0600C781 RID: 51073 RVA: 0x0027B4D9 File Offset: 0x002796D9
		public CngProvider Provider
		{
			get
			{
				return this.m_hmac.Provider;
			}
		}

		// Token: 0x0600C782 RID: 51074 RVA: 0x0027B4E6 File Offset: 0x002796E6
		protected override void HashCore(byte[] rgb, int ib, int cb)
		{
			this.m_hmac.HashCoreImpl(rgb, ib, cb);
		}

		// Token: 0x0600C783 RID: 51075 RVA: 0x0027B4F6 File Offset: 0x002796F6
		protected override byte[] HashFinal()
		{
			return this.m_hmac.HashFinalImpl();
		}

		// Token: 0x0600C784 RID: 51076 RVA: 0x0027B503 File Offset: 0x00279703
		public override void Initialize()
		{
			this.m_hmac.Initialize();
		}

		// Token: 0x040065E1 RID: 26081
		private const int BlockSize = 64;

		// Token: 0x040065E2 RID: 26082
		private BCryptHMAC m_hmac;
	}
}
