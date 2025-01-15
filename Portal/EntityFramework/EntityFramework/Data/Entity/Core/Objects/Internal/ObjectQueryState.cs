using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000451 RID: 1105
	internal abstract class ObjectQueryState
	{
		// Token: 0x060035C7 RID: 13767 RVA: 0x000AD298 File Offset: 0x000AB498
		protected ObjectQueryState(Type elementType, ObjectContext context, ObjectParameterCollection parameters, Span span)
		{
			this._elementType = elementType;
			this._context = context;
			this._span = span;
			this._parameters = parameters;
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000AD2C4 File Offset: 0x000AB4C4
		protected ObjectQueryState(Type elementType, ObjectQuery query)
			: this(elementType, query.Context, null, null)
		{
			this._cachingEnabled = query.EnablePlanCaching;
			this.UserSpecifiedStreamingBehavior = query.QueryState.UserSpecifiedStreamingBehavior;
			this.ExecutionStrategy = query.QueryState.ExecutionStrategy;
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x060035C9 RID: 13769 RVA: 0x000AD304 File Offset: 0x000AB504
		internal bool EffectiveStreamingBehavior
		{
			get
			{
				bool? userSpecifiedStreamingBehavior = this.UserSpecifiedStreamingBehavior;
				if (userSpecifiedStreamingBehavior == null)
				{
					return this.DefaultStreamingBehavior;
				}
				return userSpecifiedStreamingBehavior.GetValueOrDefault();
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060035CA RID: 13770 RVA: 0x000AD32F File Offset: 0x000AB52F
		// (set) Token: 0x060035CB RID: 13771 RVA: 0x000AD337 File Offset: 0x000AB537
		internal bool? UserSpecifiedStreamingBehavior { get; set; }

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060035CC RID: 13772 RVA: 0x000AD340 File Offset: 0x000AB540
		internal bool DefaultStreamingBehavior
		{
			get
			{
				return !(this.ExecutionStrategy ?? DbProviderServices.GetExecutionStrategy(this.ObjectContext.Connection, this.ObjectContext.MetadataWorkspace)).RetriesOnFailure;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060035CD RID: 13773 RVA: 0x000AD36F File Offset: 0x000AB56F
		// (set) Token: 0x060035CE RID: 13774 RVA: 0x000AD377 File Offset: 0x000AB577
		internal IDbExecutionStrategy ExecutionStrategy { get; set; }

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060035CF RID: 13775 RVA: 0x000AD380 File Offset: 0x000AB580
		internal Type ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060035D0 RID: 13776 RVA: 0x000AD388 File Offset: 0x000AB588
		internal ObjectContext ObjectContext
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x060035D1 RID: 13777 RVA: 0x000AD390 File Offset: 0x000AB590
		internal ObjectParameterCollection Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000AD398 File Offset: 0x000AB598
		internal ObjectParameterCollection EnsureParameters()
		{
			if (this._parameters == null)
			{
				this._parameters = new ObjectParameterCollection(this.ObjectContext.Perspective);
				if (this._cachedPlan != null)
				{
					this._parameters.SetReadOnly(true);
				}
			}
			return this._parameters;
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x060035D3 RID: 13779 RVA: 0x000AD3D2 File Offset: 0x000AB5D2
		internal Span Span
		{
			get
			{
				return this._span;
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060035D4 RID: 13780 RVA: 0x000AD3DC File Offset: 0x000AB5DC
		internal MergeOption EffectiveMergeOption
		{
			get
			{
				if (this._userMergeOption != null)
				{
					return this._userMergeOption.Value;
				}
				ObjectQueryExecutionPlan cachedPlan = this._cachedPlan;
				if (cachedPlan != null)
				{
					return cachedPlan.MergeOption;
				}
				return ObjectQueryState.DefaultMergeOption;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060035D5 RID: 13781 RVA: 0x000AD418 File Offset: 0x000AB618
		// (set) Token: 0x060035D6 RID: 13782 RVA: 0x000AD420 File Offset: 0x000AB620
		internal MergeOption? UserSpecifiedMergeOption
		{
			get
			{
				return this._userMergeOption;
			}
			set
			{
				this._userMergeOption = value;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060035D7 RID: 13783 RVA: 0x000AD429 File Offset: 0x000AB629
		// (set) Token: 0x060035D8 RID: 13784 RVA: 0x000AD431 File Offset: 0x000AB631
		internal bool PlanCachingEnabled
		{
			get
			{
				return this._cachingEnabled;
			}
			set
			{
				this._cachingEnabled = value;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060035D9 RID: 13785 RVA: 0x000AD43C File Offset: 0x000AB63C
		internal TypeUsage ResultType
		{
			get
			{
				ObjectQueryExecutionPlan cachedPlan = this._cachedPlan;
				if (cachedPlan != null)
				{
					return cachedPlan.ResultType;
				}
				return this.GetResultType();
			}
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000AD460 File Offset: 0x000AB660
		internal void ApplySettingsTo(ObjectQueryState other)
		{
			other.PlanCachingEnabled = this.PlanCachingEnabled;
			other.UserSpecifiedMergeOption = this.UserSpecifiedMergeOption;
		}

		// Token: 0x060035DB RID: 13787
		internal abstract bool TryGetCommandText(out string commandText);

		// Token: 0x060035DC RID: 13788
		internal abstract bool TryGetExpression(out Expression expression);

		// Token: 0x060035DD RID: 13789
		internal abstract ObjectQueryExecutionPlan GetExecutionPlan(MergeOption? forMergeOption);

		// Token: 0x060035DE RID: 13790
		internal abstract ObjectQueryState Include<TElementType>(ObjectQuery<TElementType> sourceQuery, string includePath);

		// Token: 0x060035DF RID: 13791
		protected abstract TypeUsage GetResultType();

		// Token: 0x060035E0 RID: 13792 RVA: 0x000AD47C File Offset: 0x000AB67C
		protected static MergeOption EnsureMergeOption(params MergeOption?[] preferredMergeOptions)
		{
			foreach (MergeOption mergeOption in preferredMergeOptions)
			{
				if (mergeOption != null)
				{
					return mergeOption.Value;
				}
			}
			return ObjectQueryState.DefaultMergeOption;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000AD4B8 File Offset: 0x000AB6B8
		protected static MergeOption? GetMergeOption(params MergeOption?[] preferredMergeOptions)
		{
			foreach (MergeOption mergeOption in preferredMergeOptions)
			{
				if (mergeOption != null)
				{
					return new MergeOption?(mergeOption.Value);
				}
			}
			return null;
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000AD4FC File Offset: 0x000AB6FC
		public ObjectQuery CreateQuery()
		{
			return (ObjectQuery)ObjectQueryState.CreateObjectQueryMethod.MakeGenericMethod(new Type[] { this._elementType }).Invoke(this, new object[0]);
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000AD528 File Offset: 0x000AB728
		public ObjectQuery<TResultType> CreateObjectQuery<TResultType>()
		{
			return new ObjectQuery<TResultType>(this);
		}

		// Token: 0x04001161 RID: 4449
		internal static readonly MergeOption DefaultMergeOption = MergeOption.AppendOnly;

		// Token: 0x04001162 RID: 4450
		internal static readonly MethodInfo CreateObjectQueryMethod = typeof(ObjectQueryState).GetOnlyDeclaredMethod("CreateObjectQuery");

		// Token: 0x04001163 RID: 4451
		private readonly ObjectContext _context;

		// Token: 0x04001164 RID: 4452
		private readonly Type _elementType;

		// Token: 0x04001165 RID: 4453
		private ObjectParameterCollection _parameters;

		// Token: 0x04001166 RID: 4454
		private readonly Span _span;

		// Token: 0x04001167 RID: 4455
		private MergeOption? _userMergeOption;

		// Token: 0x04001168 RID: 4456
		private bool _cachingEnabled = true;

		// Token: 0x04001169 RID: 4457
		protected ObjectQueryExecutionPlan _cachedPlan;
	}
}
