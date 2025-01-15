using System;
using System.ComponentModel;
using System.Data.Entity.Internal;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000238 RID: 568
	public class DbSqlQuery<TEntity> : DbRawSqlQuery<TEntity> where TEntity : class
	{
		// Token: 0x06001E12 RID: 7698 RVA: 0x000541B0 File Offset: 0x000523B0
		internal DbSqlQuery(InternalSqlQuery internalQuery)
			: base(internalQuery)
		{
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x000541B9 File Offset: 0x000523B9
		protected DbSqlQuery()
			: this(null)
		{
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x000541C2 File Offset: 0x000523C2
		public virtual DbSqlQuery<TEntity> AsNoTracking()
		{
			if (base.InternalQuery != null)
			{
				return new DbSqlQuery<TEntity>(base.InternalQuery.AsNoTracking());
			}
			return this;
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x000541DE File Offset: 0x000523DE
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public new virtual DbSqlQuery<TEntity> AsStreaming()
		{
			if (base.InternalQuery != null)
			{
				return new DbSqlQuery<TEntity>(base.InternalQuery.AsStreaming());
			}
			return this;
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x000541FA File Offset: 0x000523FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x00054202 File Offset: 0x00052402
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x0005420B File Offset: 0x0005240B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00054213 File Offset: 0x00052413
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
