using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000B6 RID: 182
	public abstract class PoolObjectSpecification
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x00006DD4 File Offset: 0x00004FD4
		protected PoolObjectSpecification(int minPoolSize, int maxPoolSize)
		{
			Contract.Check(minPoolSize >= 0, "poolSize cannot be negative.");
			Contract.Check(maxPoolSize >= minPoolSize, "maxPoolSize should be greater than or equal to poolSize.");
			this._minPoolSize = minPoolSize;
			this._maxPoolSize = maxPoolSize;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00006E0C File Offset: 0x0000500C
		public int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00006E14 File Offset: 0x00005014
		public int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
		}

		// Token: 0x040003DD RID: 989
		private readonly int _minPoolSize;

		// Token: 0x040003DE RID: 990
		private readonly int _maxPoolSize;
	}
}
