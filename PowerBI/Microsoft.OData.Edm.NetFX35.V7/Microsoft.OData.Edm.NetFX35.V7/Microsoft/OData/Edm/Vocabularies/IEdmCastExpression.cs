using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000FF RID: 255
	public interface IEdmCastExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000731 RID: 1841
		IEdmExpression Operand { get; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000732 RID: 1842
		IEdmTypeReference Type { get; }
	}
}
