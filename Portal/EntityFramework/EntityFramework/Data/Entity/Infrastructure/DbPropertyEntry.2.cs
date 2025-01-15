using System;
using System.Data.Entity.Internal;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200022E RID: 558
	public class DbPropertyEntry<TEntity, TProperty> : DbMemberEntry<TEntity, TProperty> where TEntity : class
	{
		// Token: 0x06001D4E RID: 7502 RVA: 0x000533EB File Offset: 0x000515EB
		internal static DbPropertyEntry<TEntity, TProperty> Create(InternalPropertyEntry internalPropertyEntry)
		{
			return (DbPropertyEntry<TEntity, TProperty>)internalPropertyEntry.CreateDbMemberEntry<TEntity, TProperty>();
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x000533F8 File Offset: 0x000515F8
		internal DbPropertyEntry(InternalPropertyEntry internalPropertyEntry)
		{
			this._internalPropertyEntry = internalPropertyEntry;
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x00053407 File Offset: 0x00051607
		public override string Name
		{
			get
			{
				return this._internalPropertyEntry.Name;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x00053414 File Offset: 0x00051614
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x00053426 File Offset: 0x00051626
		public TProperty OriginalValue
		{
			get
			{
				return (TProperty)((object)this._internalPropertyEntry.OriginalValue);
			}
			set
			{
				this._internalPropertyEntry.OriginalValue = value;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x00053439 File Offset: 0x00051639
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x0005344B File Offset: 0x0005164B
		public override TProperty CurrentValue
		{
			get
			{
				return (TProperty)((object)this._internalPropertyEntry.CurrentValue);
			}
			set
			{
				this._internalPropertyEntry.CurrentValue = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x0005345E File Offset: 0x0005165E
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x0005346B File Offset: 0x0005166B
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

		// Token: 0x06001D57 RID: 7511 RVA: 0x00053479 File Offset: 0x00051679
		public static implicit operator DbPropertyEntry(DbPropertyEntry<TEntity, TProperty> entry)
		{
			return DbPropertyEntry.Create(entry._internalPropertyEntry);
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x00053486 File Offset: 0x00051686
		public override DbEntityEntry<TEntity> EntityEntry
		{
			get
			{
				return new DbEntityEntry<TEntity>(this._internalPropertyEntry.InternalEntityEntry);
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00053498 File Offset: 0x00051698
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

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x000534BC File Offset: 0x000516BC
		internal InternalPropertyEntry InternalPropertyEntry
		{
			get
			{
				return this._internalPropertyEntry;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x000534C4 File Offset: 0x000516C4
		internal override InternalMemberEntry InternalMemberEntry
		{
			get
			{
				return this.InternalPropertyEntry;
			}
		}

		// Token: 0x04000B1F RID: 2847
		private readonly InternalPropertyEntry _internalPropertyEntry;
	}
}
