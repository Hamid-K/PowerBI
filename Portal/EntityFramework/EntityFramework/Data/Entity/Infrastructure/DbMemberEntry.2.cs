using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Validation;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200022A RID: 554
	public abstract class DbMemberEntry<TEntity, TProperty> where TEntity : class
	{
		// Token: 0x06001D24 RID: 7460 RVA: 0x0005315B File Offset: 0x0005135B
		internal static DbMemberEntry<TEntity, TProperty> Create(InternalMemberEntry internalMemberEntry)
		{
			return internalMemberEntry.CreateDbMemberEntry<TEntity, TProperty>();
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001D25 RID: 7461
		public abstract string Name { get; }

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001D26 RID: 7462
		// (set) Token: 0x06001D27 RID: 7463
		public abstract TProperty CurrentValue { get; set; }

		// Token: 0x06001D28 RID: 7464 RVA: 0x00053163 File Offset: 0x00051363
		public static implicit operator DbMemberEntry(DbMemberEntry<TEntity, TProperty> entry)
		{
			return DbMemberEntry.Create(entry.InternalMemberEntry);
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001D29 RID: 7465
		internal abstract InternalMemberEntry InternalMemberEntry { get; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001D2A RID: 7466
		public abstract DbEntityEntry<TEntity> EntityEntry { get; }

		// Token: 0x06001D2B RID: 7467 RVA: 0x00053170 File Offset: 0x00051370
		public ICollection<DbValidationError> GetValidationErrors()
		{
			return this.InternalMemberEntry.GetValidationErrors().ToList<DbValidationError>();
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x00053182 File Offset: 0x00051382
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0005318A File Offset: 0x0005138A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x00053193 File Offset: 0x00051393
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0005319B File Offset: 0x0005139B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
