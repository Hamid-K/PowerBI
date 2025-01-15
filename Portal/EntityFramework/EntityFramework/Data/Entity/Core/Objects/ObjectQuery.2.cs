using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200041A RID: 1050
	public class ObjectQuery<T> : ObjectQuery, IOrderedQueryable<T>, IQueryable<T>, IEnumerable<T>, IEnumerable, IQueryable, IOrderedQueryable, IDbAsyncEnumerable<T>, IDbAsyncEnumerable
	{
		// Token: 0x0600324A RID: 12874 RVA: 0x000A13F9 File Offset: 0x0009F5F9
		private static bool IsLinqQuery(ObjectQuery query)
		{
			return query.QueryState is ELinqQueryState;
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000A1409 File Offset: 0x0009F609
		public ObjectQuery(string commandText, ObjectContext context)
			: this(new EntitySqlQueryState(typeof(T), commandText, false, context, null, null))
		{
			context.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(T), Assembly.GetCallingAssembly());
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000A1440 File Offset: 0x0009F640
		public ObjectQuery(string commandText, ObjectContext context, MergeOption mergeOption)
			: this(new EntitySqlQueryState(typeof(T), commandText, false, context, null, null))
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			base.QueryState.UserSpecifiedMergeOption = new MergeOption?(mergeOption);
			context.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(T), Assembly.GetCallingAssembly());
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000A1498 File Offset: 0x0009F698
		internal ObjectQuery(EntitySetBase entitySet, ObjectContext context, MergeOption mergeOption)
			: this(new EntitySqlQueryState(typeof(T), ObjectQuery<T>.BuildScanEntitySetEsql(entitySet), entitySet.Scan(), false, context, null, null, null))
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			base.QueryState.UserSpecifiedMergeOption = new MergeOption?(mergeOption);
			context.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(T), Assembly.GetCallingAssembly());
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000A14FC File Offset: 0x0009F6FC
		private static string BuildScanEntitySetEsql(EntitySetBase entitySet)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				EntityUtil.QuoteIdentifier(entitySet.EntityContainer.Name),
				EntityUtil.QuoteIdentifier(entitySet.Name)
			});
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000A1534 File Offset: 0x0009F734
		internal ObjectQuery(ObjectQueryState queryState)
		{
			this._name = "it";
			base..ctor(queryState);
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000A1548 File Offset: 0x0009F748
		internal ObjectQuery()
		{
			this._name = "it";
			base..ctor();
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06003251 RID: 12881 RVA: 0x000A155B File Offset: 0x0009F75B
		// (set) Token: 0x06003252 RID: 12882 RVA: 0x000A1563 File Offset: 0x0009F763
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				Check.NotNull<string>(value, "value");
				if (!ObjectParameter.ValidateParameterName(value))
				{
					throw new ArgumentException(Strings.ObjectQuery_InvalidQueryName(value), "value");
				}
				this._name = value;
			}
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000A1591 File Offset: 0x0009F791
		public new ObjectResult<T> Execute(MergeOption mergeOption)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			return this.GetResults(new MergeOption?(mergeOption));
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000A15A5 File Offset: 0x0009F7A5
		public new Task<ObjectResult<T>> ExecuteAsync(MergeOption mergeOption)
		{
			return this.ExecuteAsync(mergeOption, CancellationToken.None);
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000A15B3 File Offset: 0x0009F7B3
		public new Task<ObjectResult<T>> ExecuteAsync(MergeOption mergeOption, CancellationToken cancellationToken)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			return this.GetResultsAsync(new MergeOption?(mergeOption), cancellationToken);
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000A15C8 File Offset: 0x0009F7C8
		public ObjectQuery<T> Include(string path)
		{
			Check.NotEmpty(path, "path");
			return new ObjectQuery<T>(base.QueryState.Include<T>(this, path));
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x000A15E8 File Offset: 0x0009F7E8
		public ObjectQuery<T> Distinct()
		{
			if (ObjectQuery<T>.IsLinqQuery(this))
			{
				return (ObjectQuery<T>)this.Distinct<T>();
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Distinct(base.QueryState));
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x000A1610 File Offset: 0x0009F810
		public ObjectQuery<T> Except(ObjectQuery<T> query)
		{
			Check.NotNull<ObjectQuery<T>>(query, "query");
			if (ObjectQuery<T>.IsLinqQuery(this) || ObjectQuery<T>.IsLinqQuery(query))
			{
				return (ObjectQuery<T>)this.Except(query);
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Except(base.QueryState, query.QueryState));
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x000A165C File Offset: 0x0009F85C
		public ObjectQuery<DbDataRecord> GroupBy(string keys, string projection, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(keys, "keys");
			Check.NotEmpty(projection, "projection");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			return new ObjectQuery<DbDataRecord>(EntitySqlQueryBuilder.GroupBy(base.QueryState, this.Name, keys, projection, parameters));
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x000A169C File Offset: 0x0009F89C
		public ObjectQuery<T> Intersect(ObjectQuery<T> query)
		{
			Check.NotNull<ObjectQuery<T>>(query, "query");
			if (ObjectQuery<T>.IsLinqQuery(this) || ObjectQuery<T>.IsLinqQuery(query))
			{
				return (ObjectQuery<T>)this.Intersect(query);
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Intersect(base.QueryState, query.QueryState));
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x000A16E8 File Offset: 0x0009F8E8
		public ObjectQuery<TResultType> OfType<TResultType>()
		{
			if (ObjectQuery<T>.IsLinqQuery(this))
			{
				return (ObjectQuery<TResultType>)this.OfType<TResultType>();
			}
			base.QueryState.ObjectContext.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResultType), Assembly.GetCallingAssembly());
			Type typeFromHandle = typeof(TResultType);
			EdmType edmType;
			if (!base.QueryState.ObjectContext.MetadataWorkspace.GetItemCollection(DataSpace.OSpace).TryGetType(typeFromHandle.Name, typeFromHandle.NestingNamespace() ?? string.Empty, out edmType) || (!Helper.IsEntityType(edmType) && !Helper.IsComplexType(edmType)))
			{
				throw new EntitySqlException(Strings.ObjectQuery_QueryBuilder_InvalidResultType(typeof(TResultType).FullName));
			}
			return new ObjectQuery<TResultType>(EntitySqlQueryBuilder.OfType(base.QueryState, edmType, typeFromHandle));
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x000A17A8 File Offset: 0x0009F9A8
		public ObjectQuery<T> OrderBy(string keys, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(keys, "keys");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			return new ObjectQuery<T>(EntitySqlQueryBuilder.OrderBy(base.QueryState, this.Name, keys, parameters));
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x000A17DA File Offset: 0x0009F9DA
		public ObjectQuery<DbDataRecord> Select(string projection, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(projection, "projection");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			return new ObjectQuery<DbDataRecord>(EntitySqlQueryBuilder.Select(base.QueryState, this.Name, projection, parameters));
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x000A180C File Offset: 0x0009FA0C
		public ObjectQuery<TResultType> SelectValue<TResultType>(string projection, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(projection, "projection");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			base.QueryState.ObjectContext.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResultType), Assembly.GetCallingAssembly());
			return new ObjectQuery<TResultType>(EntitySqlQueryBuilder.SelectValue(base.QueryState, this.Name, projection, parameters, typeof(TResultType)));
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x000A1877 File Offset: 0x0009FA77
		public ObjectQuery<T> Skip(string keys, string count, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(keys, "keys");
			Check.NotEmpty(count, "count");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Skip(base.QueryState, this.Name, keys, count, parameters));
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x000A18B6 File Offset: 0x0009FAB6
		public ObjectQuery<T> Top(string count, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(count, "count");
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Top(base.QueryState, this.Name, count, parameters));
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x000A18DC File Offset: 0x0009FADC
		public ObjectQuery<T> Union(ObjectQuery<T> query)
		{
			Check.NotNull<ObjectQuery<T>>(query, "query");
			if (ObjectQuery<T>.IsLinqQuery(this) || ObjectQuery<T>.IsLinqQuery(query))
			{
				return (ObjectQuery<T>)this.Union(query);
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Union(base.QueryState, query.QueryState));
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x000A1928 File Offset: 0x0009FB28
		public ObjectQuery<T> UnionAll(ObjectQuery<T> query)
		{
			Check.NotNull<ObjectQuery<T>>(query, "query");
			return new ObjectQuery<T>(EntitySqlQueryBuilder.UnionAll(base.QueryState, query.QueryState));
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x000A194C File Offset: 0x0009FB4C
		public ObjectQuery<T> Where(string predicate, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(predicate, "predicate");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Where(base.QueryState, this.Name, predicate, parameters));
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000A197E File Offset: 0x0009FB7E
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			base.QueryState.ObjectContext.AsyncMonitor.EnsureNotEntered();
			return new LazyEnumerator<T>(() => this.GetResults(null));
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000A19A6 File Offset: 0x0009FBA6
		IDbAsyncEnumerator<T> IDbAsyncEnumerable<T>.GetAsyncEnumerator()
		{
			base.QueryState.ObjectContext.AsyncMonitor.EnsureNotEntered();
			return new LazyAsyncEnumerator<T>((CancellationToken cancellationToken) => this.GetResultsAsync(null, cancellationToken));
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x000A19CE File Offset: 0x0009FBCE
		internal override IEnumerator GetEnumeratorInternal()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000A19D6 File Offset: 0x0009FBD6
		internal override IDbAsyncEnumerator GetAsyncEnumeratorInternal()
		{
			return ((IDbAsyncEnumerable<T>)this).GetAsyncEnumerator();
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000A19E0 File Offset: 0x0009FBE0
		internal override IList GetIListSourceListInternal()
		{
			return ((IListSource)this.GetResults(null)).GetList();
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x000A1A01 File Offset: 0x0009FC01
		internal override ObjectResult ExecuteInternal(MergeOption mergeOption)
		{
			return this.GetResults(new MergeOption?(mergeOption));
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000A1A10 File Offset: 0x0009FC10
		internal override async Task<ObjectResult> ExecuteInternalAsync(MergeOption mergeOption, CancellationToken cancellationToken)
		{
			return await this.GetResultsAsync(new MergeOption?(mergeOption), cancellationToken).WithCurrentCulture<ObjectResult<T>>();
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000A1A68 File Offset: 0x0009FC68
		internal override Expression GetExpression()
		{
			Expression expression;
			if (!base.QueryState.TryGetExpression(out expression))
			{
				expression = Expression.Constant(this);
			}
			if (base.QueryState.UserSpecifiedMergeOption != null)
			{
				expression = TypeSystem.EnsureType(expression, typeof(ObjectQuery<T>));
				expression = Expression.Call(expression, ObjectQuery<T>.MergeAsMethod, new Expression[] { Expression.Constant(base.QueryState.UserSpecifiedMergeOption.Value) });
			}
			if (base.QueryState.Span != null)
			{
				expression = TypeSystem.EnsureType(expression, typeof(ObjectQuery<T>));
				expression = Expression.Call(expression, ObjectQuery<T>.IncludeSpanMethod, new Expression[] { Expression.Constant(base.QueryState.Span) });
			}
			return expression;
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000A1B27 File Offset: 0x0009FD27
		internal ObjectQuery<T> MergeAs(MergeOption mergeOption)
		{
			throw new InvalidOperationException(Strings.ELinq_MethodNotDirectlyCallable);
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000A1B33 File Offset: 0x0009FD33
		internal ObjectQuery<T> IncludeSpan(Span span)
		{
			throw new InvalidOperationException(Strings.ELinq_MethodNotDirectlyCallable);
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000A1B40 File Offset: 0x0009FD40
		private ObjectResult<T> GetResults(MergeOption? forMergeOption)
		{
			base.QueryState.ObjectContext.AsyncMonitor.EnsureNotEntered();
			IDbExecutionStrategy executionStrategy = base.ExecutionStrategy ?? DbProviderServices.GetExecutionStrategy(base.QueryState.ObjectContext.Connection, base.QueryState.ObjectContext.MetadataWorkspace);
			if (executionStrategy.RetriesOnFailure && base.QueryState.EffectiveStreamingBehavior)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_StreamingNotSupported(executionStrategy.GetType().Name));
			}
			Func<ObjectResult<T>> <>9__1;
			return executionStrategy.Execute<ObjectResult<T>>(delegate
			{
				ObjectContext objectContext = this.QueryState.ObjectContext;
				Func<ObjectResult<T>> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => this.QueryState.GetExecutionPlan(forMergeOption).Execute<T>(this.QueryState.ObjectContext, this.QueryState.Parameters));
				}
				return objectContext.ExecuteInTransaction<ObjectResult<T>>(func, executionStrategy, false, !this.QueryState.EffectiveStreamingBehavior);
			});
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000A1BF8 File Offset: 0x0009FDF8
		private Task<ObjectResult<T>> GetResultsAsync(MergeOption? forMergeOption, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			base.QueryState.ObjectContext.AsyncMonitor.EnsureNotEntered();
			IDbExecutionStrategy dbExecutionStrategy = base.ExecutionStrategy ?? DbProviderServices.GetExecutionStrategy(base.QueryState.ObjectContext.Connection, base.QueryState.ObjectContext.MetadataWorkspace);
			if (dbExecutionStrategy.RetriesOnFailure && base.QueryState.EffectiveStreamingBehavior)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_StreamingNotSupported(dbExecutionStrategy.GetType().Name));
			}
			return this.GetResultsAsync(forMergeOption, dbExecutionStrategy, cancellationToken);
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000A1C88 File Offset: 0x0009FE88
		private async Task<ObjectResult<T>> GetResultsAsync(MergeOption? forMergeOption, IDbExecutionStrategy executionStrategy, CancellationToken cancellationToken)
		{
			MergeOption mergeOption = ((forMergeOption != null) ? forMergeOption.Value : base.QueryState.EffectiveMergeOption);
			if (mergeOption != MergeOption.NoTracking)
			{
				base.QueryState.ObjectContext.AsyncMonitor.Enter();
			}
			ObjectResult<T> objectResult;
			try
			{
				Func<Task<ObjectResult<T>>> <>9__1;
				objectResult = await executionStrategy.ExecuteAsync<ObjectResult<T>>(delegate
				{
					ObjectContext objectContext = this.QueryState.ObjectContext;
					Func<Task<ObjectResult<T>>> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = () => this.QueryState.GetExecutionPlan(forMergeOption).ExecuteAsync<T>(this.QueryState.ObjectContext, this.QueryState.Parameters, cancellationToken));
					}
					return objectContext.ExecuteInTransactionAsync<ObjectResult<T>>(func, executionStrategy, false, !this.QueryState.EffectiveStreamingBehavior, cancellationToken);
				}, cancellationToken).WithCurrentCulture<ObjectResult<T>>();
			}
			finally
			{
				if (mergeOption != MergeOption.NoTracking)
				{
					base.QueryState.ObjectContext.AsyncMonitor.Exit();
				}
			}
			return objectResult;
		}

		// Token: 0x04001077 RID: 4215
		internal static readonly MethodInfo MergeAsMethod = typeof(ObjectQuery<T>).GetOnlyDeclaredMethod("MergeAs");

		// Token: 0x04001078 RID: 4216
		internal static readonly MethodInfo IncludeSpanMethod = typeof(ObjectQuery<T>).GetOnlyDeclaredMethod("IncludeSpan");

		// Token: 0x04001079 RID: 4217
		private const string DefaultName = "it";

		// Token: 0x0400107A RID: 4218
		private string _name;
	}
}
