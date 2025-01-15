using System;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000005 RID: 5
	internal class ConnectionPoolConfig
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		public ConnectionPoolConfig()
		{
			this.ConnectionExpirationTime = TimeSpan.FromMinutes(10.0);
			this.PoolSize = 500;
			this.TrimSizeInPercent = 20;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002096 File Offset: 0x00000296
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000209E File Offset: 0x0000029E
		public int PoolSize
		{
			get
			{
				return this._poolSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("PoolSize should be >= 0.");
				}
				this._poolSize = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020B6 File Offset: 0x000002B6
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020BE File Offset: 0x000002BE
		public TimeSpan ConnectionExpirationTime { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020C7 File Offset: 0x000002C7
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020CF File Offset: 0x000002CF
		public int TrimSizeInPercent
		{
			get
			{
				return this._trimSizeInPercent;
			}
			set
			{
				if (value < 0 || value > 100)
				{
					throw new ArgumentException("TrimSizeInPercent should be between 0 and 100.");
				}
				this._trimSizeInPercent = value;
			}
		}

		// Token: 0x04000029 RID: 41
		private int _trimSizeInPercent;

		// Token: 0x0400002A RID: 42
		private int _poolSize;
	}
}
