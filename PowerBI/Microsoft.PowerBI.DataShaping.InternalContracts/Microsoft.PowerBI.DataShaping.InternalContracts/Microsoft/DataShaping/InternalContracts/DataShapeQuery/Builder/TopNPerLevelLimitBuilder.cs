using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x02000100 RID: 256
	internal sealed class TopNPerLevelLimitBuilder<TParent> : BuilderBase<TopNPerLevelLimitOperator, TParent>
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x0000F012 File Offset: 0x0000D212
		internal TopNPerLevelLimitBuilder(TParent parent, TopNPerLevelLimitOperator activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0000F01C File Offset: 0x0000D21C
		public TopNPerLevelLimitBuilder<TParent> WithLevel(List<Expression> level)
		{
			if (base.ActiveObject.Levels == null)
			{
				base.ActiveObject.Levels = new List<List<Expression>>();
			}
			base.ActiveObject.Levels.Add(level);
			return this;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0000F050 File Offset: 0x0000D250
		public LimitWindowExpansionInstanceBuilder<TopNPerLevelLimitBuilder<TParent>> WithWindowExpansionState()
		{
			LimitWindowExpansionInstance limitWindowExpansionInstance = new LimitWindowExpansionInstance();
			base.ActiveObject.WindowExpansionInstance = limitWindowExpansionInstance;
			return new LimitWindowExpansionInstanceBuilder<TopNPerLevelLimitBuilder<TParent>>(this, limitWindowExpansionInstance);
		}
	}
}
