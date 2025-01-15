using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001A8 RID: 424
	public sealed class DataReductionWindowExpansionInstanceBuilder<TParent> : BaseBindingBuilder<DataReductionWindowExpansionInstance, TParent>
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x00016773 File Offset: 0x00014973
		public DataReductionWindowExpansionInstanceBuilder(TParent parent)
			: base(parent)
		{
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0001677C File Offset: 0x0001497C
		public DataReductionWindowExpansionInstanceBuilder<TParent> WithValues(params QueryExpressionContainer[] values)
		{
			this._values = values;
			return this;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00016788 File Offset: 0x00014988
		public DataReductionWindowExpansionInstanceBuilder<DataReductionWindowExpansionInstanceBuilder<TParent>> WithChild(params QueryExpressionContainer[] values)
		{
			DataReductionWindowExpansionInstanceBuilder<DataReductionWindowExpansionInstanceBuilder<TParent>> dataReductionWindowExpansionInstanceBuilder = new DataReductionWindowExpansionInstanceBuilder<DataReductionWindowExpansionInstanceBuilder<TParent>>(this);
			BaseBindingBuilder<DataReductionWindowExpansionInstance, TParent>.AddToLazyList<DataReductionWindowExpansionInstanceBuilder<DataReductionWindowExpansionInstanceBuilder<TParent>>>(ref this._children, dataReductionWindowExpansionInstanceBuilder);
			return dataReductionWindowExpansionInstanceBuilder.WithValues(values);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x000167B0 File Offset: 0x000149B0
		public DataReductionWindowExpansionInstanceBuilder<TParent> WithWindowValue(WindowKind windowKind, params QueryExpressionContainer[] values)
		{
			DataReductionWindowExpansionInstanceValue dataReductionWindowExpansionInstanceValue = new DataReductionWindowExpansionInstanceValue
			{
				Values = values,
				WindowStartKind = windowKind
			};
			BaseBindingBuilder<DataReductionWindowExpansionInstance, TParent>.AddToLazyList<DataReductionWindowExpansionInstanceValue>(ref this._windowValues, dataReductionWindowExpansionInstanceValue);
			return this;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x000167E0 File Offset: 0x000149E0
		public override DataReductionWindowExpansionInstance Build()
		{
			IList<DataReductionWindowExpansionInstance> list = null;
			if (!this._children.IsNullOrEmptyCollection<DataReductionWindowExpansionInstanceBuilder<DataReductionWindowExpansionInstanceBuilder<TParent>>>())
			{
				list = new List<DataReductionWindowExpansionInstance>(this._children.Count);
				foreach (DataReductionWindowExpansionInstanceBuilder<DataReductionWindowExpansionInstanceBuilder<TParent>> dataReductionWindowExpansionInstanceBuilder in this._children)
				{
					list.Add(dataReductionWindowExpansionInstanceBuilder.Build());
				}
			}
			return new DataReductionWindowExpansionInstance
			{
				Values = this._values,
				Children = list,
				WindowExpansionInstanceWindowValue = this._windowValues
			};
		}

		// Token: 0x04000621 RID: 1569
		private IList<QueryExpressionContainer> _values;

		// Token: 0x04000622 RID: 1570
		private IList<DataReductionWindowExpansionInstanceBuilder<DataReductionWindowExpansionInstanceBuilder<TParent>>> _children;

		// Token: 0x04000623 RID: 1571
		private IList<DataReductionWindowExpansionInstanceValue> _windowValues;
	}
}
