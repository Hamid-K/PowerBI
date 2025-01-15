using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002D3 RID: 723
	public interface ISerializerHost
	{
		// Token: 0x06001645 RID: 5701
		Type GetSubstituteType(Type type);

		// Token: 0x06001646 RID: 5702
		void OnDeserialization(object value);

		// Token: 0x06001647 RID: 5703
		IEnumerable<ExtensionNamespace> GetExtensionNamespaces();
	}
}
