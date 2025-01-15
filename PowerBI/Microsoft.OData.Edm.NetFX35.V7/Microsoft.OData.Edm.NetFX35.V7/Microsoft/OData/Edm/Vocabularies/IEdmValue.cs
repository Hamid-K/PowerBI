using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000138 RID: 312
	public interface IEdmValue : IEdmElement
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060007AC RID: 1964
		IEdmTypeReference Type { get; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060007AD RID: 1965
		EdmValueKind ValueKind { get; }
	}
}
