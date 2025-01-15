using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.OData.Client
{
	// Token: 0x020000AD RID: 173
	public sealed class DataServiceQueryProvider : IQueryProvider
	{
		// Token: 0x0600058B RID: 1419 RVA: 0x000183B5 File Offset: 0x000165B5
		internal DataServiceQueryProvider(DataServiceContext context)
		{
			this.Context = context;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000183C4 File Offset: 0x000165C4
		public IQueryable CreateQuery(Expression expression)
		{
			Util.CheckArgumentNull<Expression>(expression, "expression");
			Type elementType = TypeSystem.GetElementType(expression.Type);
			Type type = typeof(DataServiceQuery<>.DataServiceOrderedQuery).MakeGenericType(new Type[] { elementType });
			object[] array = new object[] { expression, this };
			ConstructorInfo instanceConstructor = type.GetInstanceConstructor(false, new Type[]
			{
				typeof(Expression),
				typeof(DataServiceQueryProvider)
			});
			return (IQueryable)Util.ConstructorInvoke(instanceConstructor, array);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00018446 File Offset: 0x00016646
		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			Util.CheckArgumentNull<Expression>(expression, "expression");
			return new DataServiceQuery<TElement>.DataServiceOrderedQuery(expression, this);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0001845C File Offset: 0x0001665C
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public object Execute(Expression expression)
		{
			Util.CheckArgumentNull<Expression>(expression, "expression");
			MethodInfo method = typeof(DataServiceQueryProvider).GetMethod("ReturnSingleton", false, false);
			return method.MakeGenericMethod(new Type[] { expression.Type }).Invoke(this, new object[] { expression });
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000184B1 File Offset: 0x000166B1
		public TResult Execute<TResult>(Expression expression)
		{
			Util.CheckArgumentNull<Expression>(expression, "expression");
			return this.ReturnSingleton<TResult>(expression);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000184C8 File Offset: 0x000166C8
		internal TElement ReturnSingleton<TElement>(Expression expression)
		{
			IQueryable<TElement> queryable = new DataServiceQuery<TElement>.DataServiceOrderedQuery(expression, this);
			MethodCallExpression methodCallExpression = expression as MethodCallExpression;
			SequenceMethod sequenceMethod;
			if (ReflectionUtil.TryIdentifySequenceMethod(methodCallExpression.Method, out sequenceMethod))
			{
				if (sequenceMethod <= SequenceMethod.Single)
				{
					if (sequenceMethod == SequenceMethod.First)
					{
						return queryable.AsEnumerable<TElement>().First<TElement>();
					}
					if (sequenceMethod == SequenceMethod.FirstOrDefault)
					{
						return queryable.AsEnumerable<TElement>().FirstOrDefault<TElement>();
					}
					if (sequenceMethod == SequenceMethod.Single)
					{
						return queryable.AsEnumerable<TElement>().Single<TElement>();
					}
				}
				else
				{
					if (sequenceMethod == SequenceMethod.SingleOrDefault)
					{
						return queryable.AsEnumerable<TElement>().SingleOrDefault<TElement>();
					}
					if (sequenceMethod == SequenceMethod.Count || sequenceMethod == SequenceMethod.LongCount)
					{
						return (TElement)((object)Convert.ChangeType(((DataServiceQuery<TElement>)queryable).GetQuerySetCount(this.Context), typeof(TElement), CultureInfo.InvariantCulture.NumberFormat));
					}
				}
				throw Error.MethodNotSupported(methodCallExpression);
			}
			throw Error.MethodNotSupported(methodCallExpression);
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00018590 File Offset: 0x00016790
		internal QueryComponents Translate(Expression e)
		{
			bool flag = false;
			Dictionary<Expression, Expression> dictionary = null;
			if (!(e is QueryableResourceExpression))
			{
				dictionary = new Dictionary<Expression, Expression>(ReferenceEqualityComparer<Expression>.Instance);
				e = Evaluator.PartialEval(e);
				e = ExpressionNormalizer.Normalize(e, dictionary);
				e = ResourceBinder.Bind(e, this.Context);
				flag = true;
			}
			Uri uri;
			Version version;
			UriWriter.Translate(this.Context, flag, e, out uri, out version);
			ResourceExpression resourceExpression = e as ResourceExpression;
			Type type = ((resourceExpression.Projection == null) ? resourceExpression.ResourceType : resourceExpression.Projection.Selector.Parameters[0].Type);
			LambdaExpression lambdaExpression = ((resourceExpression.Projection == null) ? null : resourceExpression.Projection.Selector);
			return new QueryComponents(uri, version, type, lambdaExpression, dictionary);
		}

		// Token: 0x04000280 RID: 640
		internal readonly DataServiceContext Context;
	}
}
