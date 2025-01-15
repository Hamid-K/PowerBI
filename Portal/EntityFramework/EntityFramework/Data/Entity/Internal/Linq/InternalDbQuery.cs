using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000144 RID: 324
	internal class InternalDbQuery<TElement> : DbQuery, IOrderedQueryable<TElement>, IQueryable<TElement>, IEnumerable<TElement>, IEnumerable, IQueryable, IOrderedQueryable, IDbAsyncEnumerable<TElement>, IDbAsyncEnumerable
	{
		// Token: 0x0600151E RID: 5406 RVA: 0x000373A0 File Offset: 0x000355A0
		public InternalDbQuery(IInternalQuery<TElement> internalQuery)
		{
			this._internalQuery = internalQuery;
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x000373AF File Offset: 0x000355AF
		internal override IInternalQuery InternalQuery
		{
			get
			{
				return this._internalQuery;
			}
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x000373B7 File Offset: 0x000355B7
		public override DbQuery Include(string path)
		{
			Check.NotEmpty(path, "path");
			return new InternalDbQuery<TElement>(this._internalQuery.Include(path));
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x000373D6 File Offset: 0x000355D6
		public override DbQuery AsNoTracking()
		{
			return new InternalDbQuery<TElement>(this._internalQuery.AsNoTracking());
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x000373E8 File Offset: 0x000355E8
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public override DbQuery AsStreaming()
		{
			return new InternalDbQuery<TElement>(this._internalQuery.AsStreaming());
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x000373FA File Offset: 0x000355FA
		internal override DbQuery WithExecutionStrategy(IDbExecutionStrategy executionStrategy)
		{
			return new InternalDbQuery<TElement>(this._internalQuery.WithExecutionStrategy(executionStrategy));
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0003740D File Offset: 0x0003560D
		internal override IInternalQuery GetInternalQueryWithCheck(string memberName)
		{
			return this._internalQuery;
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x00037415 File Offset: 0x00035615
		public IEnumerator<TElement> GetEnumerator()
		{
			return this._internalQuery.GetEnumerator();
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00037422 File Offset: 0x00035622
		public IDbAsyncEnumerator<TElement> GetAsyncEnumerator()
		{
			return this._internalQuery.GetAsyncEnumerator();
		}

		// Token: 0x040009CC RID: 2508
		private readonly IInternalQuery<TElement> _internalQuery;
	}
}
