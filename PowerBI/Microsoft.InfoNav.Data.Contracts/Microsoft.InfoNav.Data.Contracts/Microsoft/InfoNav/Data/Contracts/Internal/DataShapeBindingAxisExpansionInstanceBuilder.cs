using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001A7 RID: 423
	public sealed class DataShapeBindingAxisExpansionInstanceBuilder<TParent> : BaseBindingBuilder<DataShapeBindingAxisExpansionInstance, TParent>
	{
		// Token: 0x06000B6A RID: 2922 RVA: 0x00016715 File Offset: 0x00014915
		public DataShapeBindingAxisExpansionInstanceBuilder(TParent parent)
			: base(parent)
		{
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0001671E File Offset: 0x0001491E
		public DataShapeBindingAxisExpansionInstanceBuilder<TParent> WithValues(params QueryExpressionContainer[] values)
		{
			this._values = values;
			return this;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00016728 File Offset: 0x00014928
		public DataShapeBindingAxisExpansionInstanceBuilder<DataShapeBindingAxisExpansionInstanceBuilder<TParent>> WithChild(params QueryExpressionContainer[] values)
		{
			DataShapeBindingAxisExpansionInstanceBuilder<DataShapeBindingAxisExpansionInstanceBuilder<TParent>> dataShapeBindingAxisExpansionInstanceBuilder = new DataShapeBindingAxisExpansionInstanceBuilder<DataShapeBindingAxisExpansionInstanceBuilder<TParent>>(this);
			BaseBindingBuilder<DataShapeBindingAxisExpansionInstance, TParent>.AddToLazyList<DataShapeBindingAxisExpansionInstanceBuilder<DataShapeBindingAxisExpansionInstanceBuilder<TParent>>>(ref this._children, dataShapeBindingAxisExpansionInstanceBuilder);
			return dataShapeBindingAxisExpansionInstanceBuilder.WithValues(values);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0001674F File Offset: 0x0001494F
		public override DataShapeBindingAxisExpansionInstance Build()
		{
			return new DataShapeBindingAxisExpansionInstance
			{
				Values = this._values,
				Children = BaseBindingBuilder<DataShapeBindingAxisExpansionInstance, TParent>.SafeBuild<DataShapeBindingAxisExpansionInstance, DataShapeBindingAxisExpansionInstanceBuilder<DataShapeBindingAxisExpansionInstanceBuilder<TParent>>, DataShapeBindingAxisExpansionInstanceBuilder<TParent>>(this._children)
			};
		}

		// Token: 0x0400061F RID: 1567
		private IList<QueryExpressionContainer> _values;

		// Token: 0x04000620 RID: 1568
		private IList<DataShapeBindingAxisExpansionInstanceBuilder<DataShapeBindingAxisExpansionInstanceBuilder<TParent>>> _children;
	}
}
