using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001A6 RID: 422
	public class DataReductionWindowExpansionStateBuilder<TParent> : DataShapeBindingAxisExpansionStateBuilderBase<TParent, DataReductionWindowExpansionState, DataReductionWindowExpansionInstanceBuilder<DataReductionWindowExpansionStateBuilder<TParent>>>
	{
		// Token: 0x06000B67 RID: 2919 RVA: 0x000166BD File Offset: 0x000148BD
		public DataReductionWindowExpansionStateBuilder(TParent parent)
			: base(parent)
		{
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x000166C6 File Offset: 0x000148C6
		public override DataReductionWindowExpansionInstanceBuilder<DataReductionWindowExpansionStateBuilder<TParent>> WithInstance()
		{
			this._instance = new DataReductionWindowExpansionInstanceBuilder<DataReductionWindowExpansionStateBuilder<TParent>>(this);
			return this._instance;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000166DA File Offset: 0x000148DA
		public override DataReductionWindowExpansionState Build()
		{
			return new DataReductionWindowExpansionState
			{
				From = this._froms,
				Levels = this._levels,
				WindowInstances = ((this._instance != null) ? this._instance.Build() : null)
			};
		}

		// Token: 0x0400061E RID: 1566
		private DataReductionWindowExpansionInstanceBuilder<DataReductionWindowExpansionStateBuilder<TParent>> _instance;
	}
}
