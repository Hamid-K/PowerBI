using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x02000173 RID: 371
	public interface IEdmStructuredValue : IEdmValue, IEdmElement
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000708 RID: 1800
		IEnumerable<IEdmPropertyValue> PropertyValues { get; }

		// Token: 0x06000709 RID: 1801
		IEdmPropertyValue FindPropertyValue(string propertyName);
	}
}
