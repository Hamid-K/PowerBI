using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Validation;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000229 RID: 553
	public abstract class DbMemberEntry
	{
		// Token: 0x06001D17 RID: 7447 RVA: 0x0005307D File Offset: 0x0005127D
		internal static DbMemberEntry Create(InternalMemberEntry internalMemberEntry)
		{
			return internalMemberEntry.CreateDbMemberEntry();
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001D18 RID: 7448
		public abstract string Name { get; }

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001D19 RID: 7449
		// (set) Token: 0x06001D1A RID: 7450
		public abstract object CurrentValue { get; set; }

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001D1B RID: 7451
		public abstract DbEntityEntry EntityEntry { get; }

		// Token: 0x06001D1C RID: 7452 RVA: 0x00053085 File Offset: 0x00051285
		public ICollection<DbValidationError> GetValidationErrors()
		{
			return this.InternalMemberEntry.GetValidationErrors().ToList<DbValidationError>();
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x00053097 File Offset: 0x00051297
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0005309F File Offset: 0x0005129F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x000530A8 File Offset: 0x000512A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x000530B0 File Offset: 0x000512B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001D21 RID: 7457
		internal abstract InternalMemberEntry InternalMemberEntry { get; }

		// Token: 0x06001D22 RID: 7458 RVA: 0x000530B8 File Offset: 0x000512B8
		public DbMemberEntry<TEntity, TProperty> Cast<TEntity, TProperty>() where TEntity : class
		{
			MemberEntryMetadata entryMetadata = this.InternalMemberEntry.EntryMetadata;
			if (!typeof(TEntity).IsAssignableFrom(entryMetadata.DeclaringType) || !typeof(TProperty).IsAssignableFrom(entryMetadata.MemberType))
			{
				throw Error.DbMember_BadTypeForCast(typeof(DbMemberEntry).Name, typeof(TEntity).Name, typeof(TProperty).Name, entryMetadata.DeclaringType.Name, entryMetadata.MemberType.Name);
			}
			return DbMemberEntry<TEntity, TProperty>.Create(this.InternalMemberEntry);
		}
	}
}
