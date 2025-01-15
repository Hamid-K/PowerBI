using System;
using System.ComponentModel;
using System.Data.Entity.Internal;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000237 RID: 567
	public class DbSqlQuery : DbRawSqlQuery
	{
		// Token: 0x06001E0A RID: 7690 RVA: 0x00054145 File Offset: 0x00052345
		internal DbSqlQuery(InternalSqlQuery internalQuery)
			: base(internalQuery)
		{
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0005414E File Offset: 0x0005234E
		protected DbSqlQuery()
			: this(null)
		{
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x00054157 File Offset: 0x00052357
		public virtual DbSqlQuery AsNoTracking()
		{
			if (base.InternalQuery != null)
			{
				return new DbSqlQuery(base.InternalQuery.AsNoTracking());
			}
			return this;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00054173 File Offset: 0x00052373
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public new virtual DbSqlQuery AsStreaming()
		{
			if (base.InternalQuery != null)
			{
				return new DbSqlQuery(base.InternalQuery.AsStreaming());
			}
			return this;
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x0005418F File Offset: 0x0005238F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x00054197 File Offset: 0x00052397
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x000541A0 File Offset: 0x000523A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x000541A8 File Offset: 0x000523A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
