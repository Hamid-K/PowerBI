using System;

namespace Microsoft.OData.Json
{
	// Token: 0x020001E2 RID: 482
	internal interface IODataJsonOperationsDeserializerContext
	{
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060012ED RID: 4845
		IJsonReader JsonReader { get; }

		// Token: 0x060012EE RID: 4846
		Uri ProcessUriFromPayload(string uriFromPayload);

		// Token: 0x060012EF RID: 4847
		void AddActionToResource(ODataAction action);

		// Token: 0x060012F0 RID: 4848
		void AddFunctionToResource(ODataFunction function);
	}
}
