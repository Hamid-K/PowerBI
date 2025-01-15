using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001F15 RID: 7957
	internal interface INestedOperationQuery
	{
		// Token: 0x17002C68 RID: 11368
		// (get) Token: 0x06010C0E RID: 68622
		Query AsQuery { get; }

		// Token: 0x06010C0F RID: 68623
		bool TrySelectColumns(NestedColumnSelection columnSelection, out INestedOperationQuery query);
	}
}
