using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x02000464 RID: 1124
	internal class ObjectQueryProvider : IQueryProvider, IDbAsyncQueryProvider
	{
		// Token: 0x0600374D RID: 14157 RVA: 0x000B3F31 File Offset: 0x000B2131
		internal ObjectQueryProvider(ObjectContext context)
		{
			this._context = context;
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x000B3F40 File Offset: 0x000B2140
		internal ObjectQueryProvider(ObjectQuery query)
			: this(query.Context)
		{
			this._query = query;
		}

		// Token: 0x0600374F RID: 14159 RVA: 0x000B3F55 File Offset: 0x000B2155
		internal virtual ObjectQuery<TElement> CreateQuery<TElement>(Expression expression)
		{
			return this.GetObjectQueryState(this._query, expression, typeof(TElement)).CreateObjectQuery<TElement>();
		}

		// Token: 0x06003750 RID: 14160 RVA: 0x000B3F73 File Offset: 0x000B2173
		internal virtual ObjectQuery CreateQuery(Expression expression, Type ofType)
		{
			return this.GetObjectQueryState(this._query, expression, ofType).CreateQuery();
		}

		// Token: 0x06003751 RID: 14161 RVA: 0x000B3F88 File Offset: 0x000B2188
		private ObjectQueryState GetObjectQueryState(ObjectQuery query, Expression expression, Type ofType)
		{
			if (query != null)
			{
				return new ELinqQueryState(ofType, this._query, expression, null);
			}
			return new ELinqQueryState(ofType, this._context, expression, null);
		}

		// Token: 0x06003752 RID: 14162 RVA: 0x000B3FAA File Offset: 0x000B21AA
		IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			if (!typeof(IQueryable<TElement>).IsAssignableFrom(expression.Type))
			{
				throw new ArgumentException(Strings.ELinq_ExpressionMustBeIQueryable, "expression");
			}
			return this.CreateQuery<TElement>(expression);
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x000B3FE6 File Offset: 0x000B21E6
		TResult IQueryProvider.Execute<TResult>(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			return ObjectQueryProvider.ExecuteSingle<TResult>(this.CreateQuery<TResult>(expression), expression);
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x000B4004 File Offset: 0x000B2204
		IQueryable IQueryProvider.CreateQuery(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			if (!typeof(IQueryable).IsAssignableFrom(expression.Type))
			{
				throw new ArgumentException(Strings.ELinq_ExpressionMustBeIQueryable, "expression");
			}
			Type elementType = TypeSystem.GetElementType(expression.Type);
			return this.CreateQuery(expression, elementType);
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x000B4058 File Offset: 0x000B2258
		object IQueryProvider.Execute(Expression expression)
		{
			Check.NotNull<Expression>(expression, "expression");
			return ObjectQueryProvider.ExecuteSingle<object>(this.CreateQuery(expression, expression.Type).Cast<object>(), expression);
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x000B407E File Offset: 0x000B227E
		Task<TResult> IDbAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
		{
			Check.NotNull<Expression>(expression, "expression");
			cancellationToken.ThrowIfCancellationRequested();
			return ObjectQueryProvider.ExecuteSingleAsync<TResult>(this.CreateQuery<TResult>(expression), expression, cancellationToken);
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x000B40A1 File Offset: 0x000B22A1
		Task<object> IDbAsyncQueryProvider.ExecuteAsync(Expression expression, CancellationToken cancellationToken)
		{
			Check.NotNull<Expression>(expression, "expression");
			cancellationToken.ThrowIfCancellationRequested();
			return ObjectQueryProvider.ExecuteSingleAsync<object>(this.CreateQuery(expression, expression.Type).Cast<object>(), expression, cancellationToken);
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x000B40CF File Offset: 0x000B22CF
		internal static TResult ExecuteSingle<TResult>(IEnumerable<TResult> query, Expression queryRoot)
		{
			return ObjectQueryProvider.GetElementFunction<TResult>(queryRoot)(query);
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x000B40E0 File Offset: 0x000B22E0
		private static Func<IEnumerable<TResult>, TResult> GetElementFunction<TResult>(Expression queryRoot)
		{
			SequenceMethod sequenceMethod;
			if (ReflectionUtil.TryIdentifySequenceMethod(queryRoot, true, out sequenceMethod))
			{
				if (sequenceMethod - SequenceMethod.First <= 1)
				{
					return (IEnumerable<TResult> sequence) => sequence.First<TResult>();
				}
				if (sequenceMethod - SequenceMethod.FirstOrDefault <= 1)
				{
					return (IEnumerable<TResult> sequence) => sequence.FirstOrDefault<TResult>();
				}
				if (sequenceMethod - SequenceMethod.SingleOrDefault <= 1)
				{
					return (IEnumerable<TResult> sequence) => sequence.SingleOrDefault<TResult>();
				}
			}
			return (IEnumerable<TResult> sequence) => sequence.Single<TResult>();
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x000B418E File Offset: 0x000B238E
		internal static Task<TResult> ExecuteSingleAsync<TResult>(IDbAsyncEnumerable<TResult> query, Expression queryRoot, CancellationToken cancellationToken)
		{
			return ObjectQueryProvider.GetAsyncElementFunction<TResult>(queryRoot)(query, cancellationToken);
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x000B41A0 File Offset: 0x000B23A0
		private static Func<IDbAsyncEnumerable<TResult>, CancellationToken, Task<TResult>> GetAsyncElementFunction<TResult>(Expression queryRoot)
		{
			SequenceMethod sequenceMethod;
			if (ReflectionUtil.TryIdentifySequenceMethod(queryRoot, true, out sequenceMethod))
			{
				if (sequenceMethod - SequenceMethod.First <= 1)
				{
					return (IDbAsyncEnumerable<TResult> sequence, CancellationToken cancellationToken) => sequence.FirstAsync(cancellationToken);
				}
				if (sequenceMethod - SequenceMethod.FirstOrDefault <= 1)
				{
					return (IDbAsyncEnumerable<TResult> sequence, CancellationToken cancellationToken) => sequence.FirstOrDefaultAsync(cancellationToken);
				}
				if (sequenceMethod - SequenceMethod.SingleOrDefault <= 1)
				{
					return (IDbAsyncEnumerable<TResult> sequence, CancellationToken cancellationToken) => sequence.SingleOrDefaultAsync(cancellationToken);
				}
			}
			return (IDbAsyncEnumerable<TResult> sequence, CancellationToken cancellationToken) => sequence.SingleAsync(cancellationToken);
		}

		// Token: 0x04001214 RID: 4628
		private readonly ObjectContext _context;

		// Token: 0x04001215 RID: 4629
		private readonly ObjectQuery _query;
	}
}
