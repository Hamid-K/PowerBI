using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Annotations
{
	// Token: 0x02000172 RID: 370
	public interface IEdmPropertyValueBinding : IEdmElement
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000706 RID: 1798
		IEdmProperty BoundProperty { get; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000707 RID: 1799
		IEdmExpression Value { get; }
	}
}
