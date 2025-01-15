using System;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000163 RID: 355
	internal sealed class DbConnectionPoolGroupOptions
	{
		// Token: 0x06001A84 RID: 6788 RVA: 0x0006C658 File Offset: 0x0006A858
		public DbConnectionPoolGroupOptions(bool poolByIdentity, int minPoolSize, int maxPoolSize, int creationTimeout, int loadBalanceTimeout, bool hasTransactionAffinity)
		{
			this._poolByIdentity = poolByIdentity;
			this._minPoolSize = minPoolSize;
			this._maxPoolSize = maxPoolSize;
			this._creationTimeout = creationTimeout;
			if (loadBalanceTimeout != 0)
			{
				this._loadBalanceTimeout = new TimeSpan(0, 0, loadBalanceTimeout);
				this._useLoadBalancing = true;
			}
			this._hasTransactionAffinity = hasTransactionAffinity;
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x0006C6AA File Offset: 0x0006A8AA
		public int CreationTimeout
		{
			get
			{
				return this._creationTimeout;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x0006C6B2 File Offset: 0x0006A8B2
		public bool HasTransactionAffinity
		{
			get
			{
				return this._hasTransactionAffinity;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0006C6BA File Offset: 0x0006A8BA
		public TimeSpan LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06001A88 RID: 6792 RVA: 0x0006C6C2 File Offset: 0x0006A8C2
		public int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x0006C6CA File Offset: 0x0006A8CA
		public int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x0006C6D2 File Offset: 0x0006A8D2
		public bool PoolByIdentity
		{
			get
			{
				return this._poolByIdentity;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x0006C6DA File Offset: 0x0006A8DA
		public bool UseLoadBalancing
		{
			get
			{
				return this._useLoadBalancing;
			}
		}

		// Token: 0x04000AC9 RID: 2761
		private readonly bool _poolByIdentity;

		// Token: 0x04000ACA RID: 2762
		private readonly int _minPoolSize;

		// Token: 0x04000ACB RID: 2763
		private readonly int _maxPoolSize;

		// Token: 0x04000ACC RID: 2764
		private readonly int _creationTimeout;

		// Token: 0x04000ACD RID: 2765
		private readonly TimeSpan _loadBalanceTimeout;

		// Token: 0x04000ACE RID: 2766
		private readonly bool _hasTransactionAffinity;

		// Token: 0x04000ACF RID: 2767
		private readonly bool _useLoadBalancing;
	}
}
