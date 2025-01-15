using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000AA RID: 170
	public interface IEdmTypeReference : IEdmElement
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003E6 RID: 998
		bool IsNullable { get; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003E7 RID: 999
		IEdmType Definition { get; }
	}
}
