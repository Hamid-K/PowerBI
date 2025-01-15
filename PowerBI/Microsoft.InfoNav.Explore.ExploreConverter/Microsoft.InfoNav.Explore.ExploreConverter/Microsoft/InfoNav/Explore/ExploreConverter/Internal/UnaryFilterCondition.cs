using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x020000B6 RID: 182
	internal sealed class UnaryFilterCondition<T> : IFilterCondition<T>
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x00014347 File Offset: 0x00012547
		internal UnaryFilterCondition(T expression, bool notValue)
		{
			this._expression = expression;
			this._not = notValue;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0001435D File Offset: 0x0001255D
		public T Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00014365 File Offset: 0x00012565
		public bool Not
		{
			get
			{
				return this._not;
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001436D File Offset: 0x0001256D
		public void Accept(IFilterConditionVisitor<T> visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000265 RID: 613
		private readonly T _expression;

		// Token: 0x04000266 RID: 614
		private readonly bool _not;
	}
}
