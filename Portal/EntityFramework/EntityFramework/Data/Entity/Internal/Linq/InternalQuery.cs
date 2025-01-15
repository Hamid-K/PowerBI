using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000146 RID: 326
	internal class InternalQuery<TElement> : IInternalQuery<TElement>, IInternalQuery
	{
		// Token: 0x06001538 RID: 5432 RVA: 0x0003758D File Offset: 0x0003578D
		public InternalQuery(InternalContext internalContext)
		{
			this._internalContext = internalContext;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0003759C File Offset: 0x0003579C
		public InternalQuery(InternalContext internalContext, ObjectQuery objectQuery)
		{
			this._internalContext = internalContext;
			this._objectQuery = (ObjectQuery<TElement>)objectQuery;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x000375B7 File Offset: 0x000357B7
		public virtual void ResetQuery()
		{
			this._objectQuery = null;
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x000375C0 File Offset: 0x000357C0
		public virtual InternalContext InternalContext
		{
			get
			{
				return this._internalContext;
			}
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x000375C8 File Offset: 0x000357C8
		public virtual IInternalQuery<TElement> Include(string path)
		{
			return new InternalQuery<TElement>(this._internalContext, this._objectQuery.Include(path));
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x000375E1 File Offset: 0x000357E1
		public virtual IInternalQuery<TElement> AsNoTracking()
		{
			return new InternalQuery<TElement>(this._internalContext, (ObjectQuery)DbHelpers.CreateNoTrackingQuery(this._objectQuery));
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x000375FE File Offset: 0x000357FE
		public virtual IInternalQuery<TElement> AsStreaming()
		{
			return new InternalQuery<TElement>(this._internalContext, (ObjectQuery)DbHelpers.CreateStreamingQuery(this._objectQuery));
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0003761B File Offset: 0x0003581B
		public virtual IInternalQuery<TElement> WithExecutionStrategy(IDbExecutionStrategy executionStrategy)
		{
			return new InternalQuery<TElement>(this._internalContext, (ObjectQuery)DbHelpers.CreateQueryWithExecutionStrategy(this._objectQuery, executionStrategy));
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x00037639 File Offset: 0x00035839
		public virtual ObjectQuery<TElement> ObjectQuery
		{
			get
			{
				return this._objectQuery;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x00037641 File Offset: 0x00035841
		ObjectQuery IInternalQuery.ObjectQuery
		{
			get
			{
				return this.ObjectQuery;
			}
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00037649 File Offset: 0x00035849
		protected void InitializeQuery(ObjectQuery<TElement> objectQuery)
		{
			this._objectQuery = objectQuery;
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00037652 File Offset: 0x00035852
		public virtual string ToTraceString()
		{
			return this._objectQuery.ToTraceString();
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x0003765F File Offset: 0x0003585F
		public virtual Expression Expression
		{
			get
			{
				return ((IQueryable)this._objectQuery).Expression;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x0003766C File Offset: 0x0003586C
		public virtual ObjectQueryProvider ObjectQueryProvider
		{
			get
			{
				return this._objectQuery.ObjectQueryProvider;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x00037679 File Offset: 0x00035879
		public Type ElementType
		{
			get
			{
				return typeof(TElement);
			}
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00037685 File Offset: 0x00035885
		public virtual IEnumerator<TElement> GetEnumerator()
		{
			this.InternalContext.Initialize();
			return ((IEnumerable<TElement>)this._objectQuery).GetEnumerator();
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0003769D File Offset: 0x0003589D
		IEnumerator IInternalQuery.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x000376A5 File Offset: 0x000358A5
		public virtual IDbAsyncEnumerator<TElement> GetAsyncEnumerator()
		{
			this.InternalContext.Initialize();
			return ((IDbAsyncEnumerable<TElement>)this._objectQuery).GetAsyncEnumerator();
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x000376BD File Offset: 0x000358BD
		IDbAsyncEnumerator IInternalQuery.GetAsyncEnumerator()
		{
			return this.GetAsyncEnumerator();
		}

		// Token: 0x040009CE RID: 2510
		private readonly InternalContext _internalContext;

		// Token: 0x040009CF RID: 2511
		private ObjectQuery<TElement> _objectQuery;
	}
}
