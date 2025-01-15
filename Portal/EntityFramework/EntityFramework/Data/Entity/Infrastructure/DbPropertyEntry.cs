using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200022D RID: 557
	public class DbPropertyEntry : DbMemberEntry
	{
		// Token: 0x06001D41 RID: 7489 RVA: 0x00053295 File Offset: 0x00051495
		internal static DbPropertyEntry Create(InternalPropertyEntry internalPropertyEntry)
		{
			return (DbPropertyEntry)internalPropertyEntry.CreateDbMemberEntry();
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x000532A2 File Offset: 0x000514A2
		internal DbPropertyEntry(InternalPropertyEntry internalPropertyEntry)
		{
			this._internalPropertyEntry = internalPropertyEntry;
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x000532B1 File Offset: 0x000514B1
		public override string Name
		{
			get
			{
				return this._internalPropertyEntry.Name;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x000532BE File Offset: 0x000514BE
		// (set) Token: 0x06001D45 RID: 7493 RVA: 0x000532CB File Offset: 0x000514CB
		public object OriginalValue
		{
			get
			{
				return this._internalPropertyEntry.OriginalValue;
			}
			set
			{
				this._internalPropertyEntry.OriginalValue = value;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x000532D9 File Offset: 0x000514D9
		// (set) Token: 0x06001D47 RID: 7495 RVA: 0x000532E6 File Offset: 0x000514E6
		public override object CurrentValue
		{
			get
			{
				return this._internalPropertyEntry.CurrentValue;
			}
			set
			{
				this._internalPropertyEntry.CurrentValue = value;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x000532F4 File Offset: 0x000514F4
		// (set) Token: 0x06001D49 RID: 7497 RVA: 0x00053301 File Offset: 0x00051501
		public bool IsModified
		{
			get
			{
				return this._internalPropertyEntry.IsModified;
			}
			set
			{
				this._internalPropertyEntry.IsModified = value;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x0005330F File Offset: 0x0005150F
		public override DbEntityEntry EntityEntry
		{
			get
			{
				return new DbEntityEntry(this._internalPropertyEntry.InternalEntityEntry);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x00053324 File Offset: 0x00051524
		public DbComplexPropertyEntry ParentProperty
		{
			get
			{
				InternalPropertyEntry parentPropertyEntry = this._internalPropertyEntry.ParentPropertyEntry;
				if (parentPropertyEntry == null)
				{
					return null;
				}
				return DbComplexPropertyEntry.Create(parentPropertyEntry);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x00053348 File Offset: 0x00051548
		internal override InternalMemberEntry InternalMemberEntry
		{
			get
			{
				return this._internalPropertyEntry;
			}
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x00053350 File Offset: 0x00051550
		public new DbPropertyEntry<TEntity, TProperty> Cast<TEntity, TProperty>() where TEntity : class
		{
			PropertyEntryMetadata entryMetadata = this._internalPropertyEntry.EntryMetadata;
			if (!typeof(TEntity).IsAssignableFrom(entryMetadata.DeclaringType) || !typeof(TProperty).IsAssignableFrom(entryMetadata.ElementType))
			{
				throw Error.DbMember_BadTypeForCast(typeof(DbPropertyEntry).Name, typeof(TEntity).Name, typeof(TProperty).Name, entryMetadata.DeclaringType.Name, entryMetadata.MemberType.Name);
			}
			return DbPropertyEntry<TEntity, TProperty>.Create(this._internalPropertyEntry);
		}

		// Token: 0x04000B1E RID: 2846
		private readonly InternalPropertyEntry _internalPropertyEntry;
	}
}
