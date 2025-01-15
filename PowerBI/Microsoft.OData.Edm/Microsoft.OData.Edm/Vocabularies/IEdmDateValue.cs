using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000112 RID: 274
	public interface IEdmDateValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600077E RID: 1918
		Date Value { get; }
	}
}
