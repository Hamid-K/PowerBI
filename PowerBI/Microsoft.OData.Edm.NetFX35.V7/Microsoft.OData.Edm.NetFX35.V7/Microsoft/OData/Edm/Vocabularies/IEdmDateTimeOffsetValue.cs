using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000128 RID: 296
	public interface IEdmDateTimeOffsetValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600079E RID: 1950
		DateTimeOffset Value { get; }
	}
}
