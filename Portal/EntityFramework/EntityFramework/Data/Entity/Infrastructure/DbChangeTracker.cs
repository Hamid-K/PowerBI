using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200021C RID: 540
	public class DbChangeTracker
	{
		// Token: 0x06001C5C RID: 7260 RVA: 0x00051694 File Offset: 0x0004F894
		internal DbChangeTracker(InternalContext internalContext)
		{
			this._internalContext = internalContext;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x000516A3 File Offset: 0x0004F8A3
		public IEnumerable<DbEntityEntry> Entries()
		{
			return from e in this._internalContext.GetStateEntries()
				select new DbEntityEntry(new InternalEntityEntry(this._internalContext, e));
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x000516C1 File Offset: 0x0004F8C1
		public IEnumerable<DbEntityEntry<TEntity>> Entries<TEntity>() where TEntity : class
		{
			return from e in this._internalContext.GetStateEntries<TEntity>()
				select new DbEntityEntry<TEntity>(new InternalEntityEntry(this._internalContext, e));
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x000516DF File Offset: 0x0004F8DF
		public bool HasChanges()
		{
			this._internalContext.DetectChanges(false);
			return this._internalContext.ObjectContext.ObjectStateManager.HasChanges();
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x00051702 File Offset: 0x0004F902
		public void DetectChanges()
		{
			this._internalContext.DetectChanges(true);
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x00051710 File Offset: 0x0004F910
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x00051718 File Offset: 0x0004F918
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x00051721 File Offset: 0x0004F921
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x00051729 File Offset: 0x0004F929
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000AF2 RID: 2802
		private readonly InternalContext _internalContext;
	}
}
