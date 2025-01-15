using System;
using System.Globalization;
using System.Security;
using System.Security.Cryptography;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02001FD9 RID: 8153
	internal sealed class BCryptHMAC : HMAC, ICngAlgorithm
	{
		// Token: 0x0600C71F RID: 50975 RVA: 0x0027A748 File Offset: 0x00278948
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal BCryptHMAC(CngAlgorithm algorithm, CngProvider algorithmProvider, string hashName, int blockSize, byte[] key)
		{
			base.BlockSizeValue = blockSize;
			Type typeFromHandle = typeof(SHA256Cng);
			base.HashName = string.Format(CultureInfo.InvariantCulture, "System.Security.Cryptography.{0}Cng, {1}", new object[]
			{
				hashName,
				typeFromHandle.Assembly.FullName
			});
			this.m_implementation = algorithmProvider;
			this.m_algorithm = BCryptNative.OpenAlgorithm(algorithm.Algorithm, algorithmProvider.Provider, BCryptNative.AlgorithmProviderOptions.HmacAlgorithm);
			this.Key = key;
			this.HashSizeValue = BCryptNative.GetInt32Property<SafeBCryptHashHandle>(this.m_hash, "HashDigestLength") * 8;
		}

		// Token: 0x1700303E RID: 12350
		// (get) Token: 0x0600C720 RID: 50976 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700303F RID: 12351
		// (get) Token: 0x0600C721 RID: 50977 RVA: 0x00002139 File Offset: 0x00000339
		public override bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003040 RID: 12352
		// (set) Token: 0x0600C722 RID: 50978 RVA: 0x0027A7DA File Offset: 0x002789DA
		public override byte[] Key
		{
			set
			{
				base.Key = value;
				this.Initialize();
			}
		}

		// Token: 0x17003041 RID: 12353
		// (get) Token: 0x0600C723 RID: 50979 RVA: 0x0027A7E9 File Offset: 0x002789E9
		public CngProvider Provider
		{
			get
			{
				return this.m_implementation;
			}
		}

		// Token: 0x0600C724 RID: 50980 RVA: 0x0027A7F4 File Offset: 0x002789F4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this.m_hash != null)
					{
						this.m_hash.Dispose();
					}
					if (this.m_algorithm != null)
					{
						this.m_algorithm.Dispose();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600C725 RID: 50981 RVA: 0x0027A844 File Offset: 0x00278A44
		protected override void HashCore(byte[] rgb, int ib, int cb)
		{
			this.HashCoreImpl(rgb, ib, cb);
		}

		// Token: 0x0600C726 RID: 50982 RVA: 0x0027A850 File Offset: 0x00278A50
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal void HashCoreImpl(byte[] rgb, int ib, int cb)
		{
			if (rgb == null)
			{
				throw new ArgumentNullException("rgb");
			}
			if (ib < 0 || ib > rgb.Length - cb)
			{
				throw new ArgumentOutOfRangeException("ib");
			}
			if (cb < 0 || cb > rgb.Length)
			{
				throw new ArgumentOutOfRangeException("cb");
			}
			this.State = 1;
			byte[] array = new byte[cb];
			Buffer.BlockCopy(rgb, ib, array, 0, array.Length);
			BCryptNative.HashData(this.m_hash, array);
		}

		// Token: 0x0600C727 RID: 50983 RVA: 0x0027A8BD File Offset: 0x00278ABD
		protected override byte[] HashFinal()
		{
			return this.HashFinalImpl();
		}

		// Token: 0x0600C728 RID: 50984 RVA: 0x0027A8C5 File Offset: 0x00278AC5
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal byte[] HashFinalImpl()
		{
			return BCryptNative.FinishHash(this.m_hash);
		}

		// Token: 0x0600C729 RID: 50985 RVA: 0x0027A8D2 File Offset: 0x00278AD2
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public override void Initialize()
		{
			base.Initialize();
			if (this.m_hash != null)
			{
				this.m_hash.Dispose();
			}
			this.m_hash = BCryptNative.CreateHash(this.m_algorithm, this.KeyValue);
			this.State = 0;
		}

		// Token: 0x0400658F RID: 25999
		[SecurityCritical]
		private SafeBCryptAlgorithmHandle m_algorithm;

		// Token: 0x04006590 RID: 26000
		[SecurityCritical]
		private SafeBCryptHashHandle m_hash;

		// Token: 0x04006591 RID: 26001
		private CngProvider m_implementation;
	}
}
