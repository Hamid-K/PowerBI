using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm
{
	// Token: 0x020000AB RID: 171
	internal interface IReadOnlyExtensionEdmItemCollection<T>
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060007A1 RID: 1953
		int Count { get; }

		// Token: 0x060007A2 RID: 1954
		bool TryGetItem(string entitySetReferenceName, string itemReferenceName, out T item);
	}
}
