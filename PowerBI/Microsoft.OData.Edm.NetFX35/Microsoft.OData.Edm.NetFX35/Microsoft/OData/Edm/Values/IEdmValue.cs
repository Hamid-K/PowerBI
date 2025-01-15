using System;

namespace Microsoft.OData.Edm.Values
{
	// Token: 0x02000071 RID: 113
	public interface IEdmValue : IEdmElement
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060001C2 RID: 450
		IEdmTypeReference Type { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060001C3 RID: 451
		EdmValueKind ValueKind { get; }
	}
}
