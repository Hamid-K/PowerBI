using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000130 RID: 304
	public interface IEdmIntegerValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060007A6 RID: 1958
		long Value { get; }
	}
}
