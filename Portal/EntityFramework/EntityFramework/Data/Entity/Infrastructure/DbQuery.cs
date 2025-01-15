using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000231 RID: 561
	[DebuggerDisplay("{DebuggerDisplay()}")]
	public abstract class DbQuery : IOrderedQueryable, IQueryable, IEnumerable, IListSource, IInternalQueryAdapter, IDbAsyncEnumerable
	{
		// Token: 0x06001D70 RID: 7536 RVA: 0x0005366F File Offset: 0x0005186F
		internal DbQuery()
		{
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x00053677 File Offset: 0x00051877
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0005367A File Offset: 0x0005187A
		IList IListSource.GetList()
		{
			throw Error.DbQuery_BindingToDbQueryNotSupported();
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00053681 File Offset: 0x00051881
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetInternalQueryWithCheck("IEnumerable.GetEnumerator").GetEnumerator();
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00053693 File Offset: 0x00051893
		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return this.GetInternalQueryWithCheck("IDbAsyncEnumerable.GetAsyncEnumerator").GetAsyncEnumerator();
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x000536A5 File Offset: 0x000518A5
		public virtual Type ElementType
		{
			get
			{
				return this.GetInternalQueryWithCheck("ElementType").ElementType;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x000536B7 File Offset: 0x000518B7
		Expression IQueryable.Expression
		{
			get
			{
				return this.GetInternalQueryWithCheck("IQueryable.Expression").Expression;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001D77 RID: 7543 RVA: 0x000536CC File Offset: 0x000518CC
		IQueryProvider IQueryable.Provider
		{
			get
			{
				IQueryProvider queryProvider;
				if ((queryProvider = this._provider) == null)
				{
					queryProvider = (this._provider = new NonGenericDbQueryProvider(this.GetInternalQueryWithCheck("IQueryable.Provider").InternalContext, this.GetInternalQueryWithCheck("IQueryable.Provider")));
				}
				return queryProvider;
			}
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0005370C File Offset: 0x0005190C
		public virtual DbQuery Include(string path)
		{
			return this;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0005370F File Offset: 0x0005190F
		public virtual DbQuery AsNoTracking()
		{
			return this;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x00053712 File Offset: 0x00051912
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public virtual DbQuery AsStreaming()
		{
			return this;
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x00053715 File Offset: 0x00051915
		internal virtual DbQuery WithExecutionStrategy(IDbExecutionStrategy executionStrategy)
		{
			return this;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x00053718 File Offset: 0x00051918
		public DbQuery<TElement> Cast<TElement>()
		{
			if (this.InternalQuery == null)
			{
				throw new NotSupportedException(Strings.TestDoublesCannotBeConverted);
			}
			if (typeof(TElement) != this.InternalQuery.ElementType)
			{
				throw Error.DbEntity_BadTypeForCast(typeof(DbQuery).Name, typeof(TElement).Name, this.InternalQuery.ElementType.Name);
			}
			return new DbQuery<TElement>((IInternalQuery<TElement>)this.InternalQuery);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x00053798 File Offset: 0x00051998
		public override string ToString()
		{
			if (this.InternalQuery != null)
			{
				return this.InternalQuery.ToTraceString();
			}
			return base.ToString();
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x000537B4 File Offset: 0x000519B4
		private string DebuggerDisplay()
		{
			return base.ToString();
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x000537BC File Offset: 0x000519BC
		public string Sql
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x000537C4 File Offset: 0x000519C4
		internal virtual IInternalQuery InternalQuery
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x000537C7 File Offset: 0x000519C7
		internal virtual IInternalQuery GetInternalQueryWithCheck(string memberName)
		{
			throw new NotImplementedException(Strings.TestDoubleNotImplemented(memberName, this.GetType().Name, typeof(DbSet).Name));
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x000537EE File Offset: 0x000519EE
		IInternalQuery IInternalQueryAdapter.InternalQuery
		{
			get
			{
				return this.InternalQuery;
			}
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x000537F6 File Offset: 0x000519F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x000537FF File Offset: 0x000519FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x00053807 File Offset: 0x00051A07
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B23 RID: 2851
		private IQueryProvider _provider;
	}
}
