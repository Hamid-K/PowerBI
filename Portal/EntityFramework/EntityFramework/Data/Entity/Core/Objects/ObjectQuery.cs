using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000419 RID: 1049
	public abstract class ObjectQuery : IEnumerable, IOrderedQueryable, IQueryable, IListSource, IDbAsyncEnumerable
	{
		// Token: 0x06003229 RID: 12841 RVA: 0x000A1209 File Offset: 0x0009F409
		internal ObjectQuery(ObjectQueryState queryState)
		{
			this._state = queryState;
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x000A1218 File Offset: 0x0009F418
		internal ObjectQuery()
		{
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x0600322B RID: 12843 RVA: 0x000A1220 File Offset: 0x0009F420
		internal ObjectQueryState QueryState
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600322C RID: 12844 RVA: 0x000A1228 File Offset: 0x0009F428
		internal virtual ObjectQueryProvider ObjectQueryProvider
		{
			get
			{
				if (this._provider == null)
				{
					this._provider = new ObjectQueryProvider(this);
				}
				return this._provider;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x0600322D RID: 12845 RVA: 0x000A1244 File Offset: 0x0009F444
		// (set) Token: 0x0600322E RID: 12846 RVA: 0x000A1251 File Offset: 0x0009F451
		internal IDbExecutionStrategy ExecutionStrategy
		{
			get
			{
				return this.QueryState.ExecutionStrategy;
			}
			set
			{
				this.QueryState.ExecutionStrategy = value;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x000A125F File Offset: 0x0009F45F
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x000A1264 File Offset: 0x0009F464
		public string CommandText
		{
			get
			{
				string text;
				if (!this._state.TryGetCommandText(out text))
				{
					return string.Empty;
				}
				return text;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06003231 RID: 12849 RVA: 0x000A1287 File Offset: 0x0009F487
		public ObjectContext Context
		{
			get
			{
				return this._state.ObjectContext;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x000A1294 File Offset: 0x0009F494
		// (set) Token: 0x06003233 RID: 12851 RVA: 0x000A12A1 File Offset: 0x0009F4A1
		public MergeOption MergeOption
		{
			get
			{
				return this._state.EffectiveMergeOption;
			}
			set
			{
				EntityUtil.CheckArgumentMergeOption(value);
				this._state.UserSpecifiedMergeOption = new MergeOption?(value);
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x000A12BA File Offset: 0x0009F4BA
		// (set) Token: 0x06003235 RID: 12853 RVA: 0x000A12C7 File Offset: 0x0009F4C7
		public bool Streaming
		{
			get
			{
				return this._state.EffectiveStreamingBehavior;
			}
			set
			{
				this._state.UserSpecifiedStreamingBehavior = new bool?(value);
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x000A12DA File Offset: 0x0009F4DA
		public ObjectParameterCollection Parameters
		{
			get
			{
				return this._state.EnsureParameters();
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x000A12E7 File Offset: 0x0009F4E7
		// (set) Token: 0x06003238 RID: 12856 RVA: 0x000A12F4 File Offset: 0x0009F4F4
		public bool EnablePlanCaching
		{
			get
			{
				return this._state.PlanCachingEnabled;
			}
			set
			{
				this._state.PlanCachingEnabled = value;
			}
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x000A1304 File Offset: 0x0009F504
		[Browsable(false)]
		public string ToTraceString()
		{
			return this._state.GetExecutionPlan(null).ToTraceString();
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x000A132C File Offset: 0x0009F52C
		public TypeUsage GetResultType()
		{
			if (this._resultType == null)
			{
				TypeUsage resultType = this._state.ResultType;
				TypeUsage typeUsage;
				if (!TypeHelpers.TryGetCollectionElementType(resultType, out typeUsage))
				{
					typeUsage = resultType;
				}
				typeUsage = this._state.ObjectContext.Perspective.MetadataWorkspace.GetOSpaceTypeUsage(typeUsage);
				if (typeUsage == null)
				{
					throw new InvalidOperationException(Strings.ObjectQuery_UnableToMapResultType);
				}
				this._resultType = typeUsage;
			}
			return this._resultType;
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000A1390 File Offset: 0x0009F590
		public ObjectResult Execute(MergeOption mergeOption)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			return this.ExecuteInternal(mergeOption);
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000A139F File Offset: 0x0009F59F
		public Task<ObjectResult> ExecuteAsync(MergeOption mergeOption)
		{
			return this.ExecuteAsync(mergeOption, CancellationToken.None);
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000A13AD File Offset: 0x0009F5AD
		public Task<ObjectResult> ExecuteAsync(MergeOption mergeOption, CancellationToken cancellationToken)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			cancellationToken.ThrowIfCancellationRequested();
			return this.ExecuteInternalAsync(mergeOption, cancellationToken);
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000A13C4 File Offset: 0x0009F5C4
		IList IListSource.GetList()
		{
			return this.GetIListSourceListInternal();
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x000A13CC File Offset: 0x0009F5CC
		Type IQueryable.ElementType
		{
			get
			{
				return this._state.ElementType;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x000A13D9 File Offset: 0x0009F5D9
		Expression IQueryable.Expression
		{
			get
			{
				return this.GetExpression();
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x000A13E1 File Offset: 0x0009F5E1
		IQueryProvider IQueryable.Provider
		{
			get
			{
				return this.ObjectQueryProvider;
			}
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000A13E9 File Offset: 0x0009F5E9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorInternal();
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000A13F1 File Offset: 0x0009F5F1
		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return this.GetAsyncEnumeratorInternal();
		}

		// Token: 0x06003244 RID: 12868
		internal abstract Expression GetExpression();

		// Token: 0x06003245 RID: 12869
		internal abstract IEnumerator GetEnumeratorInternal();

		// Token: 0x06003246 RID: 12870
		internal abstract IDbAsyncEnumerator GetAsyncEnumeratorInternal();

		// Token: 0x06003247 RID: 12871
		internal abstract Task<ObjectResult> ExecuteInternalAsync(MergeOption mergeOption, CancellationToken cancellationToken);

		// Token: 0x06003248 RID: 12872
		internal abstract IList GetIListSourceListInternal();

		// Token: 0x06003249 RID: 12873
		internal abstract ObjectResult ExecuteInternal(MergeOption mergeOption);

		// Token: 0x04001074 RID: 4212
		private readonly ObjectQueryState _state;

		// Token: 0x04001075 RID: 4213
		private TypeUsage _resultType;

		// Token: 0x04001076 RID: 4214
		private ObjectQueryProvider _provider;
	}
}
