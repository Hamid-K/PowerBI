using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000211 RID: 529
	[Serializable]
	internal struct CacheItemFlag
	{
		// Token: 0x06001117 RID: 4375 RVA: 0x00038274 File Offset: 0x00036474
		private CacheItemFlag(int flag)
		{
			this._flag = flag;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0003827D File Offset: 0x0003647D
		private void SetBit(int mask)
		{
			this._flag |= mask;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0003828D File Offset: 0x0003648D
		private void UnsetBit(int mask)
		{
			this._flag &= ~mask;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0003829E File Offset: 0x0003649E
		private bool IsBitOn(int mask)
		{
			return (this._flag & mask) != 0;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x000382AE File Offset: 0x000364AE
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x000382B7 File Offset: 0x000364B7
		internal bool Committed
		{
			get
			{
				return this.IsBitOn(1);
			}
			set
			{
				if (value)
				{
					this.SetBit(1);
					return;
				}
				this.UnsetBit(1);
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x000382CB File Offset: 0x000364CB
		// (set) Token: 0x0600111E RID: 4382 RVA: 0x000382D5 File Offset: 0x000364D5
		internal bool IsCommitLocked
		{
			get
			{
				return this.IsBitOn(16);
			}
			set
			{
				if (value)
				{
					this.SetBit(16);
					return;
				}
				this.UnsetBit(16);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x000382EB File Offset: 0x000364EB
		// (set) Token: 0x06001120 RID: 4384 RVA: 0x000382F4 File Offset: 0x000364F4
		internal bool IsItemGettingDeleted
		{
			get
			{
				return this.IsBitOn(4);
			}
			set
			{
				if (value)
				{
					this.SetBit(4);
					return;
				}
				this.UnsetBit(4);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x00038308 File Offset: 0x00036508
		// (set) Token: 0x06001122 RID: 4386 RVA: 0x00038311 File Offset: 0x00036511
		internal bool IgnoreforEviction
		{
			get
			{
				return this.IsBitOn(2);
			}
			set
			{
				if (value)
				{
					this.SetBit(2);
					return;
				}
				this.UnsetBit(2);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x00038325 File Offset: 0x00036525
		// (set) Token: 0x06001124 RID: 4388 RVA: 0x0003832E File Offset: 0x0003652E
		internal bool IsLockPlaceHolderObject
		{
			get
			{
				return this.IsBitOn(8);
			}
			set
			{
				if (value)
				{
					this.SetBit(8);
					return;
				}
				this.UnsetBit(8);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x00038342 File Offset: 0x00036542
		// (set) Token: 0x06001126 RID: 4390 RVA: 0x0003834C File Offset: 0x0003654C
		internal bool IsRtLockPlaceHolderObject
		{
			get
			{
				return this.IsBitOn(32);
			}
			set
			{
				if (value)
				{
					this.SetBit(32);
					return;
				}
				this.UnsetBit(32);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x00038362 File Offset: 0x00036562
		// (set) Token: 0x06001128 RID: 4392 RVA: 0x0003836C File Offset: 0x0003656C
		internal bool IsTagsPresent
		{
			get
			{
				return this.IsBitOn(64);
			}
			set
			{
				if (value)
				{
					this.SetBit(64);
					return;
				}
				this.UnsetBit(64);
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00038382 File Offset: 0x00036582
		internal CacheItemFlag GetPrunedFlags()
		{
			return new CacheItemFlag(this._flag & 73);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00038394 File Offset: 0x00036594
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "0x{0:x}", new object[] { this._flag });
		}

		// Token: 0x04000AE3 RID: 2787
		private const int CommittedMask = 1;

		// Token: 0x04000AE4 RID: 2788
		private const int IgnoreforEvictionMask = 2;

		// Token: 0x04000AE5 RID: 2789
		private const int ItemGettingDeletedMask = 4;

		// Token: 0x04000AE6 RID: 2790
		private const int LockPlaceHolderMask = 8;

		// Token: 0x04000AE7 RID: 2791
		private const int LockMask = 16;

		// Token: 0x04000AE8 RID: 2792
		private const int RtLockPlaceHolderMask = 32;

		// Token: 0x04000AE9 RID: 2793
		private const int TagsPresentMask = 64;

		// Token: 0x04000AEA RID: 2794
		private const int CopyOverMask = 73;

		// Token: 0x04000AEB RID: 2795
		private int _flag;
	}
}
