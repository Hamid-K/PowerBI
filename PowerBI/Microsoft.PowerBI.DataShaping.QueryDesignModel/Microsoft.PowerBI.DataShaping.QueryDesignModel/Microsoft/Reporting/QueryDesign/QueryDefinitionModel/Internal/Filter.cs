using System;
using System.Collections.Generic;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000D1 RID: 209
	internal sealed class Filter : CompoundFilterCondition
	{
		// Token: 0x06000D7B RID: 3451 RVA: 0x0002284C File Offset: 0x00020A4C
		internal Filter(IEnumerable<FilterCondition> conditions)
			: base(CompoundFilterOperator.All, conditions)
		{
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00022856 File Offset: 0x00020A56
		internal Filter(params FilterCondition[] conditions)
			: base(CompoundFilterOperator.All, conditions)
		{
		}
	}
}
