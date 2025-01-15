using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000060 RID: 96
	internal sealed class CompoundFilterCondition<T> : IFilterCondition<T>
	{
		// Token: 0x060001FC RID: 508 RVA: 0x0000B6FB File Offset: 0x000098FB
		internal CompoundFilterCondition(CompoundFilterOperator filterOperator, List<IFilterCondition<T>> conditions)
		{
			this._conditions = conditions;
			this._filterOperator = filterOperator;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000B711 File Offset: 0x00009911
		public CompoundFilterOperator Operator
		{
			get
			{
				return this._filterOperator;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000B719 File Offset: 0x00009919
		public List<IFilterCondition<T>> Conditions
		{
			get
			{
				return this._conditions;
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000B721 File Offset: 0x00009921
		public void Accept(IFilterConditionVisitor<T> visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000164 RID: 356
		private readonly CompoundFilterOperator _filterOperator;

		// Token: 0x04000165 RID: 357
		private readonly List<IFilterCondition<T>> _conditions;
	}
}
