using System;
using System.Data.Entity.Internal;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000236 RID: 566
	public class DbReferenceEntry<TEntity, TProperty> : DbMemberEntry<TEntity, TProperty> where TEntity : class
	{
		// Token: 0x06001DFC RID: 7676 RVA: 0x0005407B File Offset: 0x0005227B
		internal static DbReferenceEntry<TEntity, TProperty> Create(InternalReferenceEntry internalReferenceEntry)
		{
			return (DbReferenceEntry<TEntity, TProperty>)internalReferenceEntry.CreateDbMemberEntry<TEntity, TProperty>();
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x00054088 File Offset: 0x00052288
		internal DbReferenceEntry(InternalReferenceEntry internalReferenceEntry)
		{
			this._internalReferenceEntry = internalReferenceEntry;
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x00054097 File Offset: 0x00052297
		public override string Name
		{
			get
			{
				return this._internalReferenceEntry.Name;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x000540A4 File Offset: 0x000522A4
		// (set) Token: 0x06001E00 RID: 7680 RVA: 0x000540B6 File Offset: 0x000522B6
		public override TProperty CurrentValue
		{
			get
			{
				return (TProperty)((object)this._internalReferenceEntry.CurrentValue);
			}
			set
			{
				this._internalReferenceEntry.CurrentValue = value;
			}
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x000540C9 File Offset: 0x000522C9
		public void Load()
		{
			this._internalReferenceEntry.Load();
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x000540D6 File Offset: 0x000522D6
		public Task LoadAsync()
		{
			return this.LoadAsync(CancellationToken.None);
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000540E3 File Offset: 0x000522E3
		public Task LoadAsync(CancellationToken cancellationToken)
		{
			return this._internalReferenceEntry.LoadAsync(cancellationToken);
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x000540F1 File Offset: 0x000522F1
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x000540FE File Offset: 0x000522FE
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

		// Token: 0x06001E06 RID: 7686 RVA: 0x0005410C File Offset: 0x0005230C
		public IQueryable<TProperty> Query()
		{
			return (IQueryable<TProperty>)this._internalReferenceEntry.Query();
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x0005411E File Offset: 0x0005231E
		public static implicit operator DbReferenceEntry(DbReferenceEntry<TEntity, TProperty> entry)
		{
			return DbReferenceEntry.Create(entry._internalReferenceEntry);
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x0005412B File Offset: 0x0005232B
		internal override InternalMemberEntry InternalMemberEntry
		{
			get
			{
				return this._internalReferenceEntry;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x00054133 File Offset: 0x00052333
		public override DbEntityEntry<TEntity> EntityEntry
		{
			get
			{
				return new DbEntityEntry<TEntity>(this._internalReferenceEntry.InternalEntityEntry);
			}
		}

		// Token: 0x04000B29 RID: 2857
		private readonly InternalReferenceEntry _internalReferenceEntry;
	}
}
