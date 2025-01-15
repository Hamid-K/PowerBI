using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200011F RID: 287
	public interface IEdmTimeOfDayValue : IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600078A RID: 1930
		TimeOfDay Value { get; }
	}
}
