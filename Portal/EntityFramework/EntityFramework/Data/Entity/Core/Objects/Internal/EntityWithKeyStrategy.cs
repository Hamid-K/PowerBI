using System;
using System.Data.Entity.Core.Objects.DataClasses;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200043F RID: 1087
	internal sealed class EntityWithKeyStrategy : IEntityKeyStrategy
	{
		// Token: 0x06003508 RID: 13576 RVA: 0x000AAB96 File Offset: 0x000A8D96
		public EntityWithKeyStrategy(IEntityWithKey entity)
		{
			this._entity = entity;
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000AABA5 File Offset: 0x000A8DA5
		public EntityKey GetEntityKey()
		{
			return this._entity.EntityKey;
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x000AABB2 File Offset: 0x000A8DB2
		public void SetEntityKey(EntityKey key)
		{
			this._entity.EntityKey = key;
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x000AABC0 File Offset: 0x000A8DC0
		public EntityKey GetEntityKeyFromEntity()
		{
			return this._entity.EntityKey;
		}

		// Token: 0x0400112F RID: 4399
		private readonly IEntityWithKey _entity;
	}
}
