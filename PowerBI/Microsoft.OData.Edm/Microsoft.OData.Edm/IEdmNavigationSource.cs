using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000026 RID: 38
	public interface IEdmNavigationSource : IEdmNamedElement, IEdmElement
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000AE RID: 174
		IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000AF RID: 175
		IEdmPathExpression Path { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000B0 RID: 176
		IEdmType Type { get; }

		// Token: 0x060000B1 RID: 177
		IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty);

		// Token: 0x060000B2 RID: 178
		IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath);

		// Token: 0x060000B3 RID: 179
		IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty);
	}
}
