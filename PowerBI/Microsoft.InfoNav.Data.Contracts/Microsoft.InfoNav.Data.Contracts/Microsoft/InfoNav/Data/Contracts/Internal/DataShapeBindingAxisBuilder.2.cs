using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001A0 RID: 416
	public class DataShapeBindingAxisBuilder<TParent> : BaseBindingBuilder<DataShapeBindingAxis, TParent>
	{
		// Token: 0x06000B4C RID: 2892 RVA: 0x00016420 File Offset: 0x00014620
		public DataShapeBindingAxisBuilder(TParent parent)
			: base(parent)
		{
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00016429 File Offset: 0x00014629
		internal IList<DataShapeBindingAxisGroupingBuilder<DataShapeBindingAxisBuilder<TParent>>> Groupings
		{
			get
			{
				return this._groupingBuilders;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00016431 File Offset: 0x00014631
		internal IList<DataShapeBindingAxisSynchronizedGroupingBlock> Synchronization
		{
			get
			{
				return this._synchronization;
			}
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0001643C File Offset: 0x0001463C
		public DataShapeBindingAxisGroupingBuilder<DataShapeBindingAxisBuilder<TParent>> WithGroup()
		{
			DataShapeBindingAxisGroupingBuilder<DataShapeBindingAxisBuilder<TParent>> dataShapeBindingAxisGroupingBuilder = new DataShapeBindingAxisGroupingBuilder<DataShapeBindingAxisBuilder<TParent>>(this);
			BaseBindingBuilder<DataShapeBindingAxis, TParent>.AddToLazyList<DataShapeBindingAxisGroupingBuilder<DataShapeBindingAxisBuilder<TParent>>>(ref this._groupingBuilders, dataShapeBindingAxisGroupingBuilder);
			return dataShapeBindingAxisGroupingBuilder;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0001645D File Offset: 0x0001465D
		public DataShapeBindingAxisExpansionStateBuilder<DataShapeBindingAxisBuilder<TParent>> WithExpansion()
		{
			this._expansionBuilder = new DataShapeBindingAxisExpansionStateBuilder<DataShapeBindingAxisBuilder<TParent>>(this);
			return this._expansionBuilder;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00016474 File Offset: 0x00014674
		public DataShapeBindingAxisBuilder<TParent> WithSynchronization(params int[] groupingsIndices)
		{
			DataShapeBindingAxisSynchronizedGroupingBlock dataShapeBindingAxisSynchronizedGroupingBlock = new DataShapeBindingAxisSynchronizedGroupingBlock
			{
				Groupings = groupingsIndices
			};
			BaseBindingBuilder<DataShapeBindingAxis, TParent>.AddToLazyList<DataShapeBindingAxisSynchronizedGroupingBlock>(ref this._synchronization, dataShapeBindingAxisSynchronizedGroupingBlock);
			return this;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0001649B File Offset: 0x0001469B
		public override DataShapeBindingAxis Build()
		{
			return new DataShapeBindingAxis
			{
				Groupings = BaseBindingBuilder<DataShapeBindingAxis, TParent>.SafeBuild<DataShapeBindingAxisGrouping, DataShapeBindingAxisGroupingBuilder<DataShapeBindingAxisBuilder<TParent>>, DataShapeBindingAxisBuilder<TParent>>(this._groupingBuilders),
				Expansion = BaseBindingBuilder<DataShapeBindingAxis, TParent>.SafeBuild<DataShapeBindingAxisExpansionState, DataShapeBindingAxisBuilder<TParent>>(this._expansionBuilder),
				Synchronization = this._synchronization
			};
		}

		// Token: 0x04000611 RID: 1553
		private List<DataShapeBindingAxisGroupingBuilder<DataShapeBindingAxisBuilder<TParent>>> _groupingBuilders;

		// Token: 0x04000612 RID: 1554
		private DataShapeBindingAxisExpansionStateBuilder<DataShapeBindingAxisBuilder<TParent>> _expansionBuilder;

		// Token: 0x04000613 RID: 1555
		private List<DataShapeBindingAxisSynchronizedGroupingBlock> _synchronization;
	}
}
