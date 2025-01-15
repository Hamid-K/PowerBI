using System;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x02000073 RID: 115
	public interface IEdmDateValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060001C4 RID: 452
		Date Value { get; }
	}
}
