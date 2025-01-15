using System;
using System.Collections.Generic;
using System.Data.Entity.Internal;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200021E RID: 542
	public class DbCollectionEntry<TEntity, TElement> : DbMemberEntry<TEntity, ICollection<TElement>> where TEntity : class
	{
		// Token: 0x06001C75 RID: 7285 RVA: 0x000518A3 File Offset: 0x0004FAA3
		internal static DbCollectionEntry<TEntity, TElement> Create(InternalCollectionEntry internalCollectionEntry)
		{
			return internalCollectionEntry.CreateDbCollectionEntry<TEntity, TElement>();
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x000518AB File Offset: 0x0004FAAB
		internal DbCollectionEntry(InternalCollectionEntry internalCollectionEntry)
		{
			this._internalCollectionEntry = internalCollectionEntry;
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x000518BA File Offset: 0x0004FABA
		public override string Name
		{
			get
			{
				return this._internalCollectionEntry.Name;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x000518C7 File Offset: 0x0004FAC7
		// (set) Token: 0x06001C79 RID: 7289 RVA: 0x000518D9 File Offset: 0x0004FAD9
		public override ICollection<TElement> CurrentValue
		{
			get
			{
				return (ICollection<TElement>)this._internalCollectionEntry.CurrentValue;
			}
			set
			{
				this._internalCollectionEntry.CurrentValue = value;
			}
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x000518E7 File Offset: 0x0004FAE7
		public void Load()
		{
			this._internalCollectionEntry.Load();
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x000518F4 File Offset: 0x0004FAF4
		public Task LoadAsync()
		{
			return this.LoadAsync(CancellationToken.None);
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x00051901 File Offset: 0x0004FB01
		public Task LoadAsync(CancellationToken cancellationToken)
		{
			return this._internalCollectionEntry.LoadAsync(cancellationToken);
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x0005190F File Offset: 0x0004FB0F
		// (set) Token: 0x06001C7E RID: 7294 RVA: 0x0005191C File Offset: 0x0004FB1C
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

		// Token: 0x06001C7F RID: 7295 RVA: 0x0005192A File Offset: 0x0004FB2A
		public IQueryable<TElement> Query()
		{
			return (IQueryable<TElement>)this._internalCollectionEntry.Query();
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x0005193C File Offset: 0x0004FB3C
		public static implicit operator DbCollectionEntry(DbCollectionEntry<TEntity, TElement> entry)
		{
			return DbCollectionEntry.Create(entry._internalCollectionEntry);
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x00051949 File Offset: 0x0004FB49
		internal override InternalMemberEntry InternalMemberEntry
		{
			get
			{
				return this._internalCollectionEntry;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x00051951 File Offset: 0x0004FB51
		public override DbEntityEntry<TEntity> EntityEntry
		{
			get
			{
				return new DbEntityEntry<TEntity>(this._internalCollectionEntry.InternalEntityEntry);
			}
		}

		// Token: 0x04000AF4 RID: 2804
		private readonly InternalCollectionEntry _internalCollectionEntry;
	}
}
