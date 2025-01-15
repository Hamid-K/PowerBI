using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000235 RID: 565
	public class DbReferenceEntry : DbMemberEntry
	{
		// Token: 0x06001DEE RID: 7662 RVA: 0x00053F32 File Offset: 0x00052132
		internal static DbReferenceEntry Create(InternalReferenceEntry internalReferenceEntry)
		{
			return (DbReferenceEntry)internalReferenceEntry.CreateDbMemberEntry();
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x00053F3F File Offset: 0x0005213F
		internal DbReferenceEntry(InternalReferenceEntry internalReferenceEntry)
		{
			this._internalReferenceEntry = internalReferenceEntry;
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x00053F4E File Offset: 0x0005214E
		public override string Name
		{
			get
			{
				return this._internalReferenceEntry.Name;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x00053F5B File Offset: 0x0005215B
		// (set) Token: 0x06001DF2 RID: 7666 RVA: 0x00053F68 File Offset: 0x00052168
		public override object CurrentValue
		{
			get
			{
				return this._internalReferenceEntry.CurrentValue;
			}
			set
			{
				this._internalReferenceEntry.CurrentValue = value;
			}
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00053F76 File Offset: 0x00052176
		public void Load()
		{
			this._internalReferenceEntry.Load();
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00053F83 File Offset: 0x00052183
		public Task LoadAsync()
		{
			return this.LoadAsync(CancellationToken.None);
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00053F90 File Offset: 0x00052190
		public Task LoadAsync(CancellationToken cancellationToken)
		{
			return this._internalReferenceEntry.LoadAsync(cancellationToken);
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x00053F9E File Offset: 0x0005219E
		// (set) Token: 0x06001DF7 RID: 7671 RVA: 0x00053FAB File Offset: 0x000521AB
		public bool IsLoaded
		{
			get
			{
				return this._internalReferenceEntry.IsLoaded;
			}
			set
			{
				this._internalReferenceEntry.IsLoaded = value;
			}
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x00053FB9 File Offset: 0x000521B9
		public IQueryable Query()
		{
			return this._internalReferenceEntry.Query();
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x00053FC6 File Offset: 0x000521C6
		public override DbEntityEntry EntityEntry
		{
			get
			{
				return new DbEntityEntry(this._internalReferenceEntry.InternalEntityEntry);
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x00053FD8 File Offset: 0x000521D8
		internal override InternalMemberEntry InternalMemberEntry
		{
			get
			{
				return this._internalReferenceEntry;
			}
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00053FE0 File Offset: 0x000521E0
		public new DbReferenceEntry<TEntity, TProperty> Cast<TEntity, TProperty>() where TEntity : class
		{
			MemberEntryMetadata entryMetadata = this._internalReferenceEntry.EntryMetadata;
			if (!typeof(TEntity).IsAssignableFrom(entryMetadata.DeclaringType) || !typeof(TProperty).IsAssignableFrom(entryMetadata.ElementType))
			{
				throw Error.DbMember_BadTypeForCast(typeof(DbReferenceEntry).Name, typeof(TEntity).Name, typeof(TProperty).Name, entryMetadata.DeclaringType.Name, entryMetadata.MemberType.Name);
			}
			return DbReferenceEntry<TEntity, TProperty>.Create(this._internalReferenceEntry);
		}

		// Token: 0x04000B28 RID: 2856
		private readonly InternalReferenceEntry _internalReferenceEntry;
	}
}
