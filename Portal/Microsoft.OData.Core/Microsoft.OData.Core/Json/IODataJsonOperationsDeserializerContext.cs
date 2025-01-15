using System;

namespace Microsoft.OData.Json
{
	// Token: 0x02000214 RID: 532
	internal interface IODataJsonOperationsDeserializerContext
	{
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001754 RID: 5972
		IJsonReader JsonReader { get; }

		// Token: 0x06001755 RID: 5973
		Uri ProcessUriFromPayload(string uriFromPayload);

		// Token: 0x06001756 RID: 5974
		void AddActionToResource(ODataAction action);

		// Token: 0x06001757 RID: 5975
		void AddFunctionToResource(ODataFunction function);
	}
}
