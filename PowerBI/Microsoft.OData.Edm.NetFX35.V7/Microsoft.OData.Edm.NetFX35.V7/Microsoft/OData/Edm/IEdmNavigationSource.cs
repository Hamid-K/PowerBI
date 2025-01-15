using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000AA RID: 170
	public interface IEdmNavigationSource : IEdmNamedElement, IEdmElement
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000493 RID: 1171
		IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings { get; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000494 RID: 1172
		IEdmPathExpression Path { get; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000495 RID: 1173
		IEdmType Type { get; }

		// Token: 0x06000496 RID: 1174
		IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty);

		// Token: 0x06000497 RID: 1175
		IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath);

		// Token: 0x06000498 RID: 1176
		IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty);
	}
}
