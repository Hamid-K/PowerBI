using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000024 RID: 36
	public interface IEdmNavigationPropertyBinding
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000AB RID: 171
		IEdmNavigationProperty NavigationProperty { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000AC RID: 172
		IEdmNavigationSource Target { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000AD RID: 173
		IEdmPathExpression Path { get; }
	}
}
