using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017CE RID: 6094
	internal interface IFilteredQuery
	{
		// Token: 0x06009A17 RID: 39447
		bool TrySelectRows(FunctionValue selector, out Query query);

		// Token: 0x06009A18 RID: 39448
		ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector);

		// Token: 0x06009A19 RID: 39449
		ActionValue DeleteRows(FunctionValue selector);
	}
}
