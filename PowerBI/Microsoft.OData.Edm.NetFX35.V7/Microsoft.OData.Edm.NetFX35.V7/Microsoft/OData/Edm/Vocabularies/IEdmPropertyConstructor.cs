using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200010E RID: 270
	public interface IEdmPropertyConstructor : IEdmElement
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600073D RID: 1853
		string Name { get; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600073E RID: 1854
		IEdmExpression Value { get; }
	}
}
