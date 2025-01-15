using System;
using System.Net.Http;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Deserialization
{
	// Token: 0x020001B0 RID: 432
	public abstract class ODataDeserializerProvider
	{
		// Token: 0x06000E5E RID: 3678
		public abstract ODataDeserializer GetODataDeserializer(Type type, HttpRequestMessage request);

		// Token: 0x06000E5F RID: 3679
		public abstract ODataEdmTypeDeserializer GetEdmTypeDeserializer(IEdmTypeReference edmType);
	}
}
