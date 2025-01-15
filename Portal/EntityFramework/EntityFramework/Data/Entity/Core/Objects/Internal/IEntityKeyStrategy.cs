using System;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000446 RID: 1094
	internal interface IEntityKeyStrategy
	{
		// Token: 0x06003544 RID: 13636
		EntityKey GetEntityKey();

		// Token: 0x06003545 RID: 13637
		void SetEntityKey(EntityKey key);

		// Token: 0x06003546 RID: 13638
		EntityKey GetEntityKeyFromEntity();
	}
}
