using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C8 RID: 200
	public interface IEdmTypeReference : IEdmElement
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060004C7 RID: 1223
		bool IsNullable { get; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060004C8 RID: 1224
		IEdmType Definition { get; }
	}
}
