using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000115 RID: 277
	public interface IEdmDurationValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000781 RID: 1921
		TimeSpan Value { get; }
	}
}
