using System;

namespace Microsoft.OData.Edm.Annotations
{
	// Token: 0x020000F6 RID: 246
	public interface IEdmDirectValueAnnotationBinding
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060004D4 RID: 1236
		IEdmElement Element { get; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060004D5 RID: 1237
		string NamespaceUri { get; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060004D6 RID: 1238
		string Name { get; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060004D7 RID: 1239
		object Value { get; }
	}
}
