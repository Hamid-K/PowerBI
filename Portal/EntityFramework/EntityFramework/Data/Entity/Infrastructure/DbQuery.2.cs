using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000232 RID: 562
	[DebuggerDisplay("{DebuggerDisplay()}")]
	public class DbQuery<TResult> : IOrderedQueryable<TResult>, IQueryable<TResult>, IEnumerable<TResult>, IEnumerable, IQueryable, IOrderedQueryable, IListSource, IInternalQueryAdapter, IDbAsyncEnumerable<TResult>, IDbAsyncEnumerable
	{
		// Token: 0x06001D86 RID: 7558 RVA: 0x0005380F File Offset: 0x00051A0F
		internal DbQuery(IInternalQuery<TResult> internalQuery)
		{
			this._internalQuery = internalQuery;
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0005381E File Offset: 0x00051A1E
		public virtual DbQuery<TResult> Include(string path)
		{
			Check.NotEmpty(path, "path");
			if (this._internalQuery != null)
			{
				return new DbQuery<TResult>(this._internalQuery.Include(path));
			}
			return this;
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x00053847 File Offset: 0x00051A47
		public virtual DbQuery<TResult> AsNoTracking()
		{
			if (this._internalQuery != null)
			{
				return new DbQuery<TResult>(this._internalQuery.AsNoTracking());
			}
			return this;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x00053863 File Offset: 0x00051A63
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public virtual DbQuery<TResult> AsStreaming()
		{
			if (this._internalQuery != null)
			{
				return new DbQuery<TResult>(this._internalQuery.AsStreaming());
			}
			return this;
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0005387F File Offset: 0x00051A7F
		internal virtual DbQuery<TResult> WithExecutionStrategy(IDbExecutionStrategy executionStrategy)
		{
			if (this._internalQuery != null)
			{
				return new DbQuery<TResult>(this._internalQuery.WithExecutionStrategy(executionStrategy));
			}
			return this;
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x0005389C File Offset: 0x00051A9C
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0005389F File Offset: 0x00051A9F
		IList IListSource.GetList()
		{
			throw Error.DbQuery_BindingToDbQueryNotSupported();
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x000538A6 File Offset: 0x00051AA6
		IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator()
		{
			return this.GetInternalQueryWithCheck("IEnumerable<TResult>.GetEnumerator").GetEnumerator();
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x000538B8 File Offset: 0x00051AB8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetInternalQueryWithCheck("IEnumerable.GetEnumerator").GetEnumerator();
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x000538CA File Offset: 0x00051ACA
		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return this.GetInternalQueryWithCheck("IDbAsyncEnumerable.GetAsyncEnumerator").GetAsyncEnumerator();
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x000538DC File Offset: 0x00051ADC
		IDbAsyncEnumerator<TResult> IDbAsyncEnumerable<TResult>.GetAsyncEnumerator()
		{
			return this.GetInternalQueryWithCheck("IDbAsyncEnumerable<TResult>.GetAsyncEnumerator").GetAsyncEnumerator();
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x000538EE File Offset: 0x00051AEE
		Type IQueryable.ElementType
		{
			get
			{
				return this.GetInternalQueryWithCheck("IQueryable.ElementType").ElementType;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001D92 RID: 7570 RVA: 0x00053900 File Offset: 0x00051B00
		Expression IQueryable.Expression
		{
			get
			{
				return this.GetInternalQueryWithCheck("IQueryable.Expression").Expression;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x00053914 File Offset: 0x00051B14
		IQueryProvider IQueryable.Provider
		{
			get
			{
				IQueryProvider queryProvider;
				if ((queryProvider = this._provider) == null)
				{
					queryProvider = (this._provider = new DbQueryProvider(this.GetInternalQueryWithCheck("IQueryable.Provider").InternalContext, this.GetInternalQueryWithCheck("IQueryable.Provider")));
				}
				return queryProvider;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001D94 RID: 7572 RVA: 0x00053954 File Offset: 0x00051B54
		IInternalQuery IInternalQueryAdapter.InternalQuery
		{
			get
			{
				return this._internalQuery;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0005395C File Offset: 0x00051B5C
		internal IInternalQuery<TResult> InternalQuery
		{
			get
			{
				return this._internalQuery;
			}
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x00053964 File Offset: 0x00051B64
		private IInternalQuery<TResult> GetInternalQueryWithCheck(string memberName)
		{
			if (this._internalQuery == null)
			{
				throw new NotImplementedException(Strings.TestDoubleNotImplemented(memberName, this.GetType().Name, typeof(DbSet<>).Name));
			}
			return this._internalQuery;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0005399A File Offset: 0x00051B9A
		public override string ToString()
		{
			if (this._internalQuery != null)
			{
				return this._internalQuery.ToTraceString();
			}
			return base.ToString();
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x000539B6 File Offset: 0x00051BB6
		private string DebuggerDisplay()
		{
			return base.ToString();
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x000539BE File Offset: 0x00051BBE
		public string Sql
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x000539C6 File Offset: 0x00051BC6
		public static implicit operator DbQuery(DbQuery<TResult> entry)
		{
			if (entry._internalQuery == null)
			{
				throw new NotSupportedException(Strings.TestDoublesCannotBeConverted);
			}
			return new InternalDbQuery<TResult>(entry._internalQuery);
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000539E6 File Offset: 0x00051BE6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000539EF File Offset: 0x00051BEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x000539F7 File Offset: 0x00051BF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B24 RID: 2852
		private readonly IInternalQuery<TResult> _internalQuery;

		// Token: 0x04000B25 RID: 2853
		private IQueryProvider _provider;
	}
}
