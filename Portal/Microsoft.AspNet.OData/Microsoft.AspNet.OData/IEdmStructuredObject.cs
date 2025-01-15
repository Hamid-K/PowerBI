using System;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000043 RID: 67
	public interface IEdmStructuredObject : IEdmObject
	{
		// Token: 0x06000197 RID: 407
		bool TryGetPropertyValue(string propertyName, out object value);
	}
}
