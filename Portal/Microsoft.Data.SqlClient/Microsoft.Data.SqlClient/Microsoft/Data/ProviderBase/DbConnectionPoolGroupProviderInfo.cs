using System;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000162 RID: 354
	internal class DbConnectionPoolGroupProviderInfo
	{
		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0006C644 File Offset: 0x0006A844
		// (set) Token: 0x06001A82 RID: 6786 RVA: 0x0006C64C File Offset: 0x0006A84C
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._poolGroup;
			}
			set
			{
				this._poolGroup = value;
			}
		}

		// Token: 0x04000AC8 RID: 2760
		private DbConnectionPoolGroup _poolGroup;
	}
}
