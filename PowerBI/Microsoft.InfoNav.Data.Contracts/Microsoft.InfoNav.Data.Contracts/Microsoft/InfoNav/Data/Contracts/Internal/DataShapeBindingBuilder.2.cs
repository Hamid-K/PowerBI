using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200019E RID: 414
	public class DataShapeBindingBuilder<TParent> : BaseBindingBuilder<DataShapeBinding, TParent> where TParent : class
	{
		// Token: 0x06000B34 RID: 2868 RVA: 0x000160E8 File Offset: 0x000142E8
		public DataShapeBindingBuilder(int? version = null)
			: this(default(TParent), version)
		{
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00016105 File Offset: 0x00014305
		public DataShapeBindingBuilder(TParent parent, int? version = null)
			: base(parent)
		{
			if (version != null)
			{
				this.WithVersion(version.Value);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00016131 File Offset: 0x00014331
		public bool HasSecondaryAxis
		{
			get
			{
				return this._secondaryAxisBuilder != null;
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0001613C File Offset: 0x0001433C
		public DataShapeBindingBuilder<TParent> WithVersion(int version)
		{
			this._version = new int?(version);
			return this;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0001614B File Offset: 0x0001434B
		public DataShapeBindingAxisGroupingBuilder<DataShapeBindingAxisBuilder<DataShapeBindingBuilder<TParent>>> WithPrimaryGrouping()
		{
			return this.WithPrimaryAxis().WithGroup();
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00016158 File Offset: 0x00014358
		public DataShapeBindingAxisGroupingBuilder<DataShapeBindingAxisBuilder<DataShapeBindingBuilder<TParent>>> WithSecondaryGrouping()
		{
			return this.WithSecondaryAxis().WithGroup();
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00016165 File Offset: 0x00014365
		public DataShapeBindingAxisBuilder<DataShapeBindingBuilder<TParent>> WithPrimaryAxis()
		{
			if (this._primaryAxisBuilder == null)
			{
				this._primaryAxisBuilder = new DataShapeBindingAxisBuilder<DataShapeBindingBuilder<TParent>>(this);
			}
			return this._primaryAxisBuilder;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00016181 File Offset: 0x00014381
		public DataShapeBindingAxisBuilder<DataShapeBindingBuilder<TParent>> WithSecondaryAxis()
		{
			if (this._secondaryAxisBuilder == null)
			{
				this._secondaryAxisBuilder = new DataShapeBindingAxisBuilder<DataShapeBindingBuilder<TParent>>(this);
			}
			return this._secondaryAxisBuilder;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0001619D File Offset: 0x0001439D
		public DataReductionBuilder<DataShapeBindingBuilder<TParent>> WithDataReduction(int? dataVolume)
		{
			if (this._dataReductionBuilder == null)
			{
				this._dataReductionBuilder = new DataReductionBuilder<DataShapeBindingBuilder<TParent>>(this, dataVolume);
			}
			return this._dataReductionBuilder;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000161BC File Offset: 0x000143BC
		public FilterDefinitionBuilder<DataShapeBindingBuilder<TParent>> WithHighlights()
		{
			FilterDefinitionBuilder<DataShapeBindingBuilder<TParent>> filterDefinitionBuilder = new FilterDefinitionBuilder<DataShapeBindingBuilder<TParent>>(this);
			BaseBindingBuilder<DataShapeBinding, TParent>.AddToLazyList<FilterDefinitionBuilder<DataShapeBindingBuilder<TParent>>>(ref this._highlights, filterDefinitionBuilder);
			return filterDefinitionBuilder;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000161DD File Offset: 0x000143DD
		public DataShapeBindingBuilder<TParent> WithNullSuppressedJoinPredicatesByName()
		{
			this._suppressedJoinPredicatesByName = null;
			return this;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000161E7 File Offset: 0x000143E7
		public DataShapeBindingBuilder<TParent> WithEmptySuppressedJoinPredicatesByName()
		{
			this._suppressedJoinPredicatesByName = new List<DataShapeBindingSuppressedJoinPredicate>();
			return this;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x000161F8 File Offset: 0x000143F8
		public DataShapeBindingBuilder<TParent> WithSuppressedJoinPredicate(string queryName, string expressionName)
		{
			DataShapeBindingSuppressedJoinPredicate dataShapeBindingSuppressedJoinPredicate = new DataShapeBindingSuppressedJoinPredicate
			{
				QueryReference = new DataShapeBindingQueryReference
				{
					SourceName = queryName,
					ExpressionName = expressionName
				}
			};
			BaseBindingBuilder<DataShapeBinding, TParent>.AddToLazyList<DataShapeBindingSuppressedJoinPredicate>(ref this._suppressedJoinPredicatesByName, dataShapeBindingSuppressedJoinPredicate);
			return this;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00016231 File Offset: 0x00014431
		public DataShapeBindingBuilder<TParent> WithSuppressedJoinPredicate(int index)
		{
			BaseBindingBuilder<DataShapeBinding, TParent>.AddToLazyList<int>(ref this._suppressedJoinPredicates, index);
			return this;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00016240 File Offset: 0x00014440
		public DataShapeBindingBuilder<TParent> WithNullHiddenProjections()
		{
			this._hiddenProjections = null;
			return this;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001624A File Offset: 0x0001444A
		public DataShapeBindingBuilder<TParent> WithEmptyHiddenProjections()
		{
			this._hiddenProjections = new List<DataShapeBindingHiddenProjections>();
			return this;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00016258 File Offset: 0x00014458
		public DataShapeBindingBuilder<TParent> WithHiddenProjection(string expressionName)
		{
			DataShapeBindingHiddenProjections dataShapeBindingHiddenProjections = new DataShapeBindingHiddenProjections
			{
				QueryReference = new DataShapeBindingQueryReference
				{
					ExpressionName = expressionName
				}
			};
			BaseBindingBuilder<DataShapeBinding, TParent>.AddToLazyList<DataShapeBindingHiddenProjections>(ref this._hiddenProjections, dataShapeBindingHiddenProjections);
			return this;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0001628A File Offset: 0x0001448A
		public DataShapeBindingBuilder<TParent> WithIncludeEmptyGroups()
		{
			this._includeEmptyGroups = true;
			return this;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00016294 File Offset: 0x00014494
		public DataShapeBindingAggregateBuilder<DataShapeBindingBuilder<TParent>> WithAggregate(int selectIndex)
		{
			DataShapeBindingAggregateBuilder<DataShapeBindingBuilder<TParent>> dataShapeBindingAggregateBuilder = new DataShapeBindingAggregateBuilder<DataShapeBindingBuilder<TParent>>(this, selectIndex);
			BaseBindingBuilder<DataShapeBinding, TParent>.AddToLazyList<DataShapeBindingAggregateBuilder<DataShapeBindingBuilder<TParent>>>(ref this._aggregateBuilders, dataShapeBindingAggregateBuilder);
			return dataShapeBindingAggregateBuilder;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000162B6 File Offset: 0x000144B6
		public DataShapeBindingBuilder<TParent> AddProjection(int index)
		{
			if (this._projections == null || !this._projections.Contains(index))
			{
				BaseBindingBuilder<DataShapeBinding, TParent>.AddToLazyList<int>(ref this._projections, index);
			}
			return this;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x000162DB File Offset: 0x000144DB
		public DataShapeBindingBuilder<TParent> WithOrderBy(params QuerySortDirection?[] overrides)
		{
			if (this._dataShapeBindingOrderBy == null)
			{
				this._dataShapeBindingOrderBy = new DataShapeBindingOrderBy();
			}
			this._dataShapeBindingOrderBy.Overrides = overrides.ToList<QuerySortDirection?>();
			return this;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00016308 File Offset: 0x00014508
		public DataShapeBindingBuilder<TParent> WithLimit(DataShapeBindingLimitType type, int count, int? primaryIndex, int? secondaryIndex)
		{
			DataShapeBindingLimit dataShapeBindingLimit = new DataShapeBindingLimit
			{
				Type = type,
				Count = count,
				Target = new DataShapeBindingLimitTarget
				{
					Primary = primaryIndex,
					Secondary = secondaryIndex
				}
			};
			BaseBindingBuilder<DataShapeBinding, TParent>.AddToLazyList<DataShapeBindingLimit>(ref this._limits, dataShapeBindingLimit);
			return this;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00016350 File Offset: 0x00014550
		public override DataShapeBinding Build()
		{
			return new DataShapeBinding
			{
				Version = this._version,
				Primary = BaseBindingBuilder<DataShapeBinding, TParent>.SafeBuild<DataShapeBindingAxis, DataShapeBindingBuilder<TParent>>(this._primaryAxisBuilder),
				Secondary = BaseBindingBuilder<DataShapeBinding, TParent>.SafeBuild<DataShapeBindingAxis, DataShapeBindingBuilder<TParent>>(this._secondaryAxisBuilder),
				OrderBy = this._dataShapeBindingOrderBy,
				DataReduction = BaseBindingBuilder<DataShapeBinding, TParent>.SafeBuild<DataReduction, DataShapeBindingBuilder<TParent>>(this._dataReductionBuilder),
				Highlights = BaseBindingBuilder<DataShapeBinding, TParent>.SafeBuild<FilterDefinition, FilterDefinitionBuilder<DataShapeBindingBuilder<TParent>>, DataShapeBindingBuilder<TParent>>(this._highlights),
				SuppressedJoinPredicates = this._suppressedJoinPredicates,
				SuppressedJoinPredicatesByName = this._suppressedJoinPredicatesByName,
				HiddenProjections = this._hiddenProjections,
				IncludeEmptyGroups = this._includeEmptyGroups,
				Aggregates = BaseBindingBuilder<DataShapeBinding, TParent>.SafeBuild<DataShapeBindingAggregate, DataShapeBindingAggregateBuilder<DataShapeBindingBuilder<TParent>>, DataShapeBindingBuilder<TParent>>(this._aggregateBuilders),
				Projections = this._projections,
				Limits = this._limits
			};
		}

		// Token: 0x04000604 RID: 1540
		private DataShapeBindingAxisBuilder<DataShapeBindingBuilder<TParent>> _primaryAxisBuilder;

		// Token: 0x04000605 RID: 1541
		private DataShapeBindingAxisBuilder<DataShapeBindingBuilder<TParent>> _secondaryAxisBuilder;

		// Token: 0x04000606 RID: 1542
		private DataReductionBuilder<DataShapeBindingBuilder<TParent>> _dataReductionBuilder;

		// Token: 0x04000607 RID: 1543
		private int? _version = new int?(1);

		// Token: 0x04000608 RID: 1544
		private IList<FilterDefinitionBuilder<DataShapeBindingBuilder<TParent>>> _highlights;

		// Token: 0x04000609 RID: 1545
		private List<DataShapeBindingSuppressedJoinPredicate> _suppressedJoinPredicatesByName;

		// Token: 0x0400060A RID: 1546
		private List<DataShapeBindingHiddenProjections> _hiddenProjections;

		// Token: 0x0400060B RID: 1547
		private List<int> _suppressedJoinPredicates;

		// Token: 0x0400060C RID: 1548
		private bool _includeEmptyGroups;

		// Token: 0x0400060D RID: 1549
		private List<DataShapeBindingAggregateBuilder<DataShapeBindingBuilder<TParent>>> _aggregateBuilders;

		// Token: 0x0400060E RID: 1550
		private IList<int> _projections;

		// Token: 0x0400060F RID: 1551
		private DataShapeBindingOrderBy _dataShapeBindingOrderBy;

		// Token: 0x04000610 RID: 1552
		private IList<DataShapeBindingLimit> _limits;
	}
}
