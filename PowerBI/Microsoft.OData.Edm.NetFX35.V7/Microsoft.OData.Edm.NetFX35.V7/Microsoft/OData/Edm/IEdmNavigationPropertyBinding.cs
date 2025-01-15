using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000A8 RID: 168
	public interface IEdmNavigationPropertyBinding
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000490 RID: 1168
		IEdmNavigationProperty NavigationProperty { get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000491 RID: 1169
		IEdmNavigationSource Target { get; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000492 RID: 1170
		IEdmPathExpression Path { get; }
	}
}
