using System;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200011C RID: 284
	public class HMACSHA256Hashing : OneWayObfuscation
	{
		// Token: 0x0600079C RID: 1948 RVA: 0x0001A631 File Offset: 0x00018831
		public HMACSHA256Hashing(byte[] key, int keyIdentifier)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.hmacProvider = new HMACSHA256Cng(key);
			this.keyIdentifier = keyIdentifier;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001A65A File Offset: 0x0001885A
		public override int GetKeyIdentifier()
		{
			return this.keyIdentifier;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001A662 File Offset: 0x00018862
		public override void Dispose()
		{
			if (this.hmacProvider != null)
			{
				this.hmacProvider.Clear();
				this.hmacProvider = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001A684 File Offset: 0x00018884
		protected override byte[] ComputeHash(byte[] inputData)
		{
			return this.hmacProvider.ComputeHash(inputData);
		}

		// Token: 0x040002BE RID: 702
		private readonly int keyIdentifier;

		// Token: 0x040002BF RID: 703
		private HMACSHA256Cng hmacProvider;
	}
}
