using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200011E RID: 286
	public interface IEdmStructuredValue : IEdmValue, IEdmElement
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000788 RID: 1928
		IEnumerable<IEdmPropertyValue> PropertyValues { get; }

		// Token: 0x06000789 RID: 1929
		IEdmPropertyValue FindPropertyValue(string propertyName);
	}
}
