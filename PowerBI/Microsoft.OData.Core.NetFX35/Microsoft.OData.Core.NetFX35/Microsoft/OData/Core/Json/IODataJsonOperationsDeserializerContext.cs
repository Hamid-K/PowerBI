using System;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x020000DE RID: 222
	internal interface IODataJsonOperationsDeserializerContext
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600084C RID: 2124
		JsonReader JsonReader { get; }

		// Token: 0x0600084D RID: 2125
		Uri ProcessUriFromPayload(string uriFromPayload);

		// Token: 0x0600084E RID: 2126
		void AddActionToEntry(ODataAction action);

		// Token: 0x0600084F RID: 2127
		void AddFunctionToEntry(ODataFunction function);
	}
}
