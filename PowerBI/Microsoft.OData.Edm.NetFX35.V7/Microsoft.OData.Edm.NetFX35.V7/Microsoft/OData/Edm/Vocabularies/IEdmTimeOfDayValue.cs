using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000136 RID: 310
	public interface IEdmTimeOfDayValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060007AB RID: 1963
		TimeOfDay Value { get; }
	}
}
