using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200012D RID: 301
	public interface IEdmEnumValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060007A3 RID: 1955
		IEdmEnumMemberValue Value { get; }
	}
}
