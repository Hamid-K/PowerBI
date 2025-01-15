using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000F7 RID: 247
	internal sealed class InFilterConditionValuesBuilder<TParent> : BuilderBase<List<Expression>, TParent>
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x0000E9DA File Offset: 0x0000CBDA
		internal InFilterConditionValuesBuilder(TParent parent, List<Expression> activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		public InFilterConditionValuesBuilder<TParent> WithValue(Expression expression)
		{
			base.ActiveObject.Add(expression);
			return this;
		}
	}
}
