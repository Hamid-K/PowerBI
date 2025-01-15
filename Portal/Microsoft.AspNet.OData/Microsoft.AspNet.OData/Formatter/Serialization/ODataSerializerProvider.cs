using System;
using System.Net.Http;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Formatter.Serialization
{
	// Token: 0x02000199 RID: 409
	public abstract class ODataSerializerProvider
	{
		// Token: 0x06000D96 RID: 3478
		public abstract ODataSerializer GetODataPayloadSerializer(Type type, HttpRequestMessage request);

		// Token: 0x06000D97 RID: 3479
		public abstract ODataEdmTypeSerializer GetEdmTypeSerializer(IEdmTypeReference edmType);
	}
}
