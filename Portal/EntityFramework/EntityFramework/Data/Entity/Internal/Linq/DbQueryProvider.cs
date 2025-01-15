using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x0200013C RID: 316
	internal class DbQueryProvider : IQueryProvider, IDbAsyncQueryProvider
	{
		// Token: 0x060014EB RID: 5355 RVA: 0x00036E1C File Offset: 0x0003501C
		public DbQueryProvider(InternalContext internalContext, IInternalQuery internalQuery)
		{
			this._internalContext = internalContext;
			this._internalQuery = internalQuery;
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00036E34 File Offset: 0x00035034
		public virtual IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			ObjectQuery objectQuery = this.CreateObjectQuery(expression);
			if (typeof(TElement) != ((IQueryable)objectQuery).ElementType)
			{
				return (IQueryable<TElement>)this.CreateQuery(objectQuery);
			}
			return new DbQuery<TElement>(new InternalQuery<TElement>(this._internalContext, objectQuery));
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00036E8A File Offset: 0x0003508A
		public virtual IQueryable CreateQuery(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			return this.CreateQuery(this.CreateObjectQuery(expression));
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00036EA5 File Offset: 0x000350A5
		public virtual TResult Execute<TResult>(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			this._internalContext.Initialize();
			return ((IQueryProvider)this._internalQuery.ObjectQueryProvider).Execute<TResult>(expression);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00036ECF File Offset: 0x000350CF
		public virtual object Execute(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			this._internalContext.Initialize();
			return ((IQueryProvider)this._internalQuery.ObjectQueryProvider).Execute(expression);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00036EF9 File Offset: 0x000350F9
		Task<TResult> IDbAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
		{
			Check.NotNull<Expression>(expression, "expression");
			cancellationToken.ThrowIfCancellationRequested();
			this._internalContext.Initialize();
			return ((IDbAsyncQueryProvider)this._internalQuery.ObjectQueryProvider).ExecuteAsync<TResult>(expression, cancellationToken);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00036F2B File Offset: 0x0003512B
		Task<object> IDbAsyncQueryProvider.ExecuteAsync(Expression expression, CancellationToken cancellationToken)
		{
			Check.NotNull<Expression>(expression, "expression");
			cancellationToken.ThrowIfCancellationRequested();
			this._internalContext.Initialize();
			return ((IDbAsyncQueryProvider)this._internalQuery.ObjectQueryProvider).ExecuteAsync(expression, cancellationToken);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00036F60 File Offset: 0x00035160
		private IQueryable CreateQuery(ObjectQuery objectQuery)
		{
			IInternalQuery internalQuery = this.CreateInternalQuery(objectQuery);
			return (IQueryable)typeof(DbQuery<>).MakeGenericType(new Type[] { internalQuery.ElementType }).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Single<ConstructorInfo>()
				.Invoke(new object[] { internalQuery });
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00036FB3 File Offset: 0x000351B3
		protected ObjectQuery CreateObjectQuery(Expression expression)
		{
			expression = new DbQueryVisitor().Visit(expression);
			return (ObjectQuery)((IQueryProvider)this._internalQuery.ObjectQueryProvider).CreateQuery(expression);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00036FD8 File Offset: 0x000351D8
		protected IInternalQuery CreateInternalQuery(ObjectQuery objectQuery)
		{
			return (IInternalQuery)typeof(InternalQuery<>).MakeGenericType(new Type[] { ((IQueryable)objectQuery).ElementType }).GetDeclaredConstructor(new Type[]
			{
				typeof(InternalContext),
				typeof(ObjectQuery)
			}).Invoke(new object[] { this._internalContext, objectQuery });
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00037045 File Offset: 0x00035245
		public InternalContext InternalContext
		{
			get
			{
				return this._internalContext;
			}
		}

		// Token: 0x040009C8 RID: 2504
		private readonly InternalContext _internalContext;

		// Token: 0x040009C9 RID: 2505
		private readonly IInternalQuery _internalQuery;
	}
}
