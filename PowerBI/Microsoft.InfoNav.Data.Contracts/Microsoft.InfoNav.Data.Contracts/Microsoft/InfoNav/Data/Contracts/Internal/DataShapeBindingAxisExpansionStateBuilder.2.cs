using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001A4 RID: 420
	public class DataShapeBindingAxisExpansionStateBuilder<TParent> : DataShapeBindingAxisExpansionStateBuilderBase<TParent, DataShapeBindingAxisExpansionState, DataShapeBindingAxisExpansionInstanceBuilder<DataShapeBindingAxisExpansionStateBuilder<TParent>>>
	{
		// Token: 0x06000B63 RID: 2915 RVA: 0x0001665C File Offset: 0x0001485C
		public DataShapeBindingAxisExpansionStateBuilder(TParent parent)
			: base(parent)
		{
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00016665 File Offset: 0x00014865
		public override DataShapeBindingAxisExpansionInstanceBuilder<DataShapeBindingAxisExpansionStateBuilder<TParent>> WithInstance()
		{
			this._instance = new DataShapeBindingAxisExpansionInstanceBuilder<DataShapeBindingAxisExpansionStateBuilder<TParent>>(this);
			return this._instance;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00016679 File Offset: 0x00014879
		public override DataShapeBindingAxisExpansionState Build()
		{
			return new DataShapeBindingAxisExpansionState
			{
				From = this._froms,
				Levels = this._levels,
				Instances = ((this._instance != null) ? this._instance.Build() : null)
			};
		}

		// Token: 0x0400061D RID: 1565
		private DataShapeBindingAxisExpansionInstanceBuilder<DataShapeBindingAxisExpansionStateBuilder<TParent>> _instance;
	}
}
