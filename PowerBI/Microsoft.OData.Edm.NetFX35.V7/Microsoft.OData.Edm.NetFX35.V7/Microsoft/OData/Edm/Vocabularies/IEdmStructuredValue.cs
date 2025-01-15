using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000135 RID: 309
	public interface IEdmStructuredValue : IEdmValue, IEdmElement
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060007A9 RID: 1961
		IEnumerable<IEdmPropertyValue> PropertyValues { get; }

		// Token: 0x060007AA RID: 1962
		IEdmPropertyValue FindPropertyValue(string propertyName);
	}
}
