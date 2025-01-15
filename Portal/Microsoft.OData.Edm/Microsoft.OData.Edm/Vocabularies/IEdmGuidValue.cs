using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000118 RID: 280
	public interface IEdmGuidValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000784 RID: 1924
		Guid Value { get; }
	}
}
