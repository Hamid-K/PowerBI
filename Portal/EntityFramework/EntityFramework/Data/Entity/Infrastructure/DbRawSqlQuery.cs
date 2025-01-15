using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000233 RID: 563
	public class DbRawSqlQuery : IEnumerable, IListSource, IDbAsyncEnumerable
	{
		// Token: 0x06001D9E RID: 7582 RVA: 0x000539FF File Offset: 0x00051BFF
		internal DbRawSqlQuery(InternalSqlQuery internalQuery)
		{
			this._internalQuery = internalQuery;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00053A0E File Offset: 0x00051C0E
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public virtual DbRawSqlQuery AsStreaming()
		{
			if (this._internalQuery != null)
			{
				return new DbRawSqlQuery(this._internalQuery.AsStreaming());
			}
			return this;
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x00053A2A File Offset: 0x00051C2A
		public virtual IEnumerator GetEnumerator()
		{
			return this.GetInternalQueryWithCheck("GetEnumerator").GetEnumerator();
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x00053A3C File Offset: 0x00051C3C
		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return this.GetInternalQueryWithCheck("IDbAsyncEnumerable.GetAsyncEnumerator").GetAsyncEnumerator();
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x00053A4E File Offset: 0x00051C4E
		public virtual Task ForEachAsync(Action<object> action)
		{
			Check.NotNull<Action<object>>(action, "action");
			return this.ForEachAsync(action, CancellationToken.None);
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00053A68 File Offset: 0x00051C68
		public virtual Task ForEachAsync(Action<object> action, CancellationToken cancellationToken)
		{
			Check.NotNull<Action<object>>(action, "action");
			return this.ForEachAsync(action, cancellationToken);
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x00053A7E File Offset: 0x00051C7E
		public virtual Task<List<object>> ToListAsync()
		{
			return this.ToListAsync<object>();
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x00053A86 File Offset: 0x00051C86
		public virtual Task<List<object>> ToListAsync(CancellationToken cancellationToken)
		{
			return this.ToListAsync(cancellationToken);
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x00053A8F File Offset: 0x00051C8F
		public override string ToString()
		{
			if (this._internalQuery != null)
			{
				return this._internalQuery.ToString();
			}
			return base.ToString();
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x00053AAB File Offset: 0x00051CAB
		internal InternalSqlQuery InternalQuery
		{
			get
			{
				return this._internalQuery;
			}
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x00053AB3 File Offset: 0x00051CB3
		private InternalSqlQuery GetInternalQueryWithCheck(string memberName)
		{
			if (this._internalQuery == null)
			{
				throw new NotImplementedException(Strings.TestDoubleNotImplemented(memberName, this.GetType().Name, typeof(DbSqlQuery).Name));
			}
			return this._internalQuery;
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x00053AE9 File Offset: 0x00051CE9
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x00053AEC File Offset: 0x00051CEC
		IList IListSource.GetList()
		{
			throw Error.DbQuery_BindingToDbQueryNotSupported();
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x00053AF3 File Offset: 0x00051CF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x00053AFC File Offset: 0x00051CFC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x00053B04 File Offset: 0x00051D04
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B26 RID: 2854
		private readonly InternalSqlQuery _internalQuery;
	}
}
