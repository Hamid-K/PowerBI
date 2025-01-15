using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000107 RID: 263
	public interface IEdmPropertyConstructor : IEdmElement
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600076A RID: 1898
		string Name { get; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600076B RID: 1899
		IEdmExpression Value { get; }
	}
}
