using System;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000479 RID: 1145
	public interface IEntityChangeTracker
	{
		// Token: 0x0600382B RID: 14379
		void EntityMemberChanging(string entityMemberName);

		// Token: 0x0600382C RID: 14380
		void EntityMemberChanged(string entityMemberName);

		// Token: 0x0600382D RID: 14381
		void EntityComplexMemberChanging(string entityMemberName, object complexObject, string complexObjectMemberName);

		// Token: 0x0600382E RID: 14382
		void EntityComplexMemberChanged(string entityMemberName, object complexObject, string complexObjectMemberName);

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x0600382F RID: 14383
		EntityState EntityState { get; }
	}
}
