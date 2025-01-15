using System;
using System.Security;
using System.Security.Cryptography;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02001FF9 RID: 8185
	public sealed class RNGCng : RandomNumberGenerator, ICngAlgorithm, IDisposable
	{
		// Token: 0x0600C786 RID: 51078 RVA: 0x0027B510 File Offset: 0x00279710
		public RNGCng()
			: this(CngProvider2.MicrosoftPrimitiveAlgorithmProvider)
		{
		}

		// Token: 0x0600C787 RID: 51079 RVA: 0x0027B51D File Offset: 0x0027971D
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public RNGCng(CngProvider algorithmProvider)
		{
			if (algorithmProvider == null)
			{
				throw new ArgumentNullException("algorithmProvider");
			}
			this.m_algorithm = BCryptNative.OpenAlgorithm("RNG", algorithmProvider.Provider);
			this.m_implementation = algorithmProvider;
		}

		// Token: 0x17003055 RID: 12373
		// (get) Token: 0x0600C788 RID: 51080 RVA: 0x0027B556 File Offset: 0x00279756
		public CngProvider Provider
		{
			get
			{
				return this.m_implementation;
			}
		}

		// Token: 0x17003056 RID: 12374
		// (get) Token: 0x0600C789 RID: 51081 RVA: 0x0027B55E File Offset: 0x0027975E
		internal static RNGCng StaticRng
		{
			get
			{
				return RNGCng.s_rngCng;
			}
		}

		// Token: 0x0600C78A RID: 51082 RVA: 0x0027B565 File Offset: 0x00279765
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public new void Dispose()
		{
			if (this.m_algorithm != null)
			{
				this.m_algorithm.Dispose();
			}
		}

		// Token: 0x0600C78B RID: 51083 RVA: 0x0027B57C File Offset: 0x0027977C
		internal static byte[] GenerateKey(int size)
		{
			byte[] array = new byte[size];
			RNGCng.StaticRng.GetBytes(array);
			return array;
		}

		// Token: 0x0600C78C RID: 51084 RVA: 0x0027B59C File Offset: 0x0027979C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public override void GetBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			BCryptNative.GenerateRandomBytes(this.m_algorithm, data);
		}

		// Token: 0x0600C78D RID: 51085 RVA: 0x000091AE File Offset: 0x000073AE
		public override void GetNonZeroBytes(byte[] data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040065E3 RID: 26083
		[SecurityCritical]
		private SafeBCryptAlgorithmHandle m_algorithm;

		// Token: 0x040065E4 RID: 26084
		private CngProvider m_implementation;

		// Token: 0x040065E5 RID: 26085
		private static RNGCng s_rngCng = new RNGCng();
	}
}
