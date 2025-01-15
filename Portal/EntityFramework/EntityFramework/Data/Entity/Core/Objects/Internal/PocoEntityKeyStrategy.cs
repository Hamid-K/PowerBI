using System;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000453 RID: 1107
	internal sealed class PocoEntityKeyStrategy : IEntityKeyStrategy
	{
		// Token: 0x060035FE RID: 13822 RVA: 0x000ADE4A File Offset: 0x000AC04A
		public EntityKey GetEntityKey()
		{
			return this._key;
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000ADE52 File Offset: 0x000AC052
		public void SetEntityKey(EntityKey key)
		{
			this._key = key;
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000ADE5B File Offset: 0x000AC05B
		public EntityKey GetEntityKeyFromEntity()
		{
			return null;
		}

		// Token: 0x04001173 RID: 4467
		private EntityKey _key;
	}
}
