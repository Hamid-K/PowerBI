using System;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x02000176 RID: 374
	internal interface IODataJsonOperationsDeserializerContext
	{
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A5A RID: 2650
		JsonReader JsonReader { get; }

		// Token: 0x06000A5B RID: 2651
		Uri ProcessUriFromPayload(string uriFromPayload);

		// Token: 0x06000A5C RID: 2652
		void AddActionToEntry(ODataAction action);

		// Token: 0x06000A5D RID: 2653
		void AddFunctionToEntry(ODataFunction function);
	}
}
