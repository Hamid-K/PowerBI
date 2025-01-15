using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200038B RID: 907
	internal abstract class ColumnMapVisitorWithResults<TResultType, TArgType>
	{
		// Token: 0x06002C05 RID: 11269 RVA: 0x0008EF24 File Offset: 0x0008D124
		protected EntityIdentity VisitEntityIdentity(EntityIdentity entityIdentity, TArgType arg)
		{
			DiscriminatedEntityIdentity discriminatedEntityIdentity = entityIdentity as DiscriminatedEntityIdentity;
			if (discriminatedEntityIdentity != null)
			{
				return this.VisitEntityIdentity(discriminatedEntityIdentity, arg);
			}
			return this.VisitEntityIdentity((SimpleEntityIdentity)entityIdentity, arg);
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x0008EF51 File Offset: 0x0008D151
		protected virtual EntityIdentity VisitEntityIdentity(DiscriminatedEntityIdentity entityIdentity, TArgType arg)
		{
			return entityIdentity;
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x0008EF54 File Offset: 0x0008D154
		protected virtual EntityIdentity VisitEntityIdentity(SimpleEntityIdentity entityIdentity, TArgType arg)
		{
			return entityIdentity;
		}

		// Token: 0x06002C08 RID: 11272
		internal abstract TResultType Visit(ComplexTypeColumnMap columnMap, TArgType arg);

		// Token: 0x06002C09 RID: 11273
		internal abstract TResultType Visit(DiscriminatedCollectionColumnMap columnMap, TArgType arg);

		// Token: 0x06002C0A RID: 11274
		internal abstract TResultType Visit(EntityColumnMap columnMap, TArgType arg);

		// Token: 0x06002C0B RID: 11275
		internal abstract TResultType Visit(SimplePolymorphicColumnMap columnMap, TArgType arg);

		// Token: 0x06002C0C RID: 11276
		internal abstract TResultType Visit(RecordColumnMap columnMap, TArgType arg);

		// Token: 0x06002C0D RID: 11277
		internal abstract TResultType Visit(RefColumnMap columnMap, TArgType arg);

		// Token: 0x06002C0E RID: 11278
		internal abstract TResultType Visit(ScalarColumnMap columnMap, TArgType arg);

		// Token: 0x06002C0F RID: 11279
		internal abstract TResultType Visit(SimpleCollectionColumnMap columnMap, TArgType arg);

		// Token: 0x06002C10 RID: 11280
		internal abstract TResultType Visit(VarRefColumnMap columnMap, TArgType arg);

		// Token: 0x06002C11 RID: 11281
		internal abstract TResultType Visit(MultipleDiscriminatorPolymorphicColumnMap columnMap, TArgType arg);
	}
}
