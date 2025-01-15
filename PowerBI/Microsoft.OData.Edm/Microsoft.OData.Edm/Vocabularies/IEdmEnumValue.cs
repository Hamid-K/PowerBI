using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000116 RID: 278
	public interface IEdmEnumValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000782 RID: 1922
		IEdmEnumMemberValue Value { get; }
	}
}
