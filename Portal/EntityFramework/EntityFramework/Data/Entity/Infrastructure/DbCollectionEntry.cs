using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200021D RID: 541
	public class DbCollectionEntry : DbMemberEntry
	{
		// Token: 0x06001C67 RID: 7271 RVA: 0x00051757 File Offset: 0x0004F957
		internal static DbCollectionEntry Create(InternalCollectionEntry internalCollectionEntry)
		{
			return (DbCollectionEntry)internalCollectionEntry.CreateDbMemberEntry();
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00051764 File Offset: 0x0004F964
		internal DbCollectionEntry(InternalCollectionEntry internalCollectionEntry)
		{
			this._internalCollectionEntry = internalCollectionEntry;
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x00051773 File Offset: 0x0004F973
		public override string Name
		{
			get
			{
				return this._internalCollectionEntry.Name;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x00051780 File Offset: 0x0004F980
		// (set) Token: 0x06001C6B RID: 7275 RVA: 0x0005178D File Offset: 0x0004F98D
		public override object CurrentValue
		{
			get
			{
				return this._internalCollectionEntry.CurrentValue;
			}
			set
			{
				this._internalCollectionEntry.CurrentValue = value;
			}
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0005179B File Offset: 0x0004F99B
		public void Load()
		{
			this._internalCollectionEntry.Load();
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x000517A8 File Offset: 0x0004F9A8
		public Task LoadAsync()
		{
			return this.LoadAsync(CancellationToken.None);
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x000517B5 File Offset: 0x0004F9B5
		public Task LoadAsync(CancellationToken cancellationToken)
		{
			return this._internalCollectionEntry.LoadAsync(cancellationToken);
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x000517C3 File Offset: 0x0004F9C3
		// (set) Token: 0x06001C70 RID: 7280 RVA: 0x000517D0 File Offset: 0x0004F9D0
		public bool IsLoaded
		{
			get
			{
				return this._internalCollectionEntry.IsLoaded;
			}
			set
			{
				this._internalCollectionEntry.IsLoaded = value;
			}
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x000517DE File Offset: 0x0004F9DE
		public IQueryable Query()
		{
			return this._internalCollectionEntry.Query();
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x000517EB File Offset: 0x0004F9EB
		public override DbEntityEntry EntityEntry
		{
			get
			{
				return new DbEntityEntry(this._internalCollectionEntry.InternalEntityEntry);
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x000517FD File Offset: 0x0004F9FD
		internal override InternalMemberEntry InternalMemberEntry
		{
			get
			{
				return this._internalCollectionEntry;
			}
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x00051808 File Offset: 0x0004FA08
		public new DbCollectionEntry<TEntity, TElement> Cast<TEntity, TElement>() where TEntity : class
		{
			MemberEntryMetadata entryMetadata = this._internalCollectionEntry.EntryMetadata;
			if (!typeof(TEntity).IsAssignableFrom(entryMetadata.DeclaringType) || !typeof(TElement).IsAssignableFrom(entryMetadata.ElementType))
			{
				throw Error.DbMember_BadTypeForCast(typeof(DbCollectionEntry).Name, typeof(TEntity).Name, typeof(TElement).Name, entryMetadata.DeclaringType.Name, entryMetadata.ElementType.Name);
			}
			return DbCollectionEntry<TEntity, TElement>.Create(this._internalCollectionEntry);
		}

		// Token: 0x04000AF3 RID: 2803
		private readonly InternalCollectionEntry _internalCollectionEntry;
	}
}
