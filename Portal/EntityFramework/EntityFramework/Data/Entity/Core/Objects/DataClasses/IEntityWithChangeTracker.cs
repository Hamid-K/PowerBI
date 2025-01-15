using System;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x0200047A RID: 1146
	public interface IEntityWithChangeTracker
	{
		// Token: 0x06003830 RID: 14384
		void SetChangeTracker(IEntityChangeTracker changeTracker);
	}
}
