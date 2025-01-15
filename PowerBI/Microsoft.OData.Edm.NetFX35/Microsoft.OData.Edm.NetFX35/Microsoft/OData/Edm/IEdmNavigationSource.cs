using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000080 RID: 128
	public interface IEdmNavigationSource : IEdmNamedElement, IEdmElement
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000206 RID: 518
		IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings { get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000207 RID: 519
		IEdmPathExpression Path { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000208 RID: 520
		IEdmType Type { get; }

		// Token: 0x06000209 RID: 521
		IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty);
	}
}
