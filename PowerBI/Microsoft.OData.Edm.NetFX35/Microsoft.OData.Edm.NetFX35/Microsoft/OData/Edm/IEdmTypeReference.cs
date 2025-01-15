using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200009E RID: 158
	public interface IEdmTypeReference : IEdmElement
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060002B8 RID: 696
		bool IsNullable { get; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060002B9 RID: 697
		IEdmType Definition { get; }
	}
}
