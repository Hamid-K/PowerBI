using System;

namespace Microsoft.OData.Edm.Annotations
{
	// Token: 0x02000092 RID: 146
	public interface IEdmDirectValueAnnotation : IEdmNamedElement, IEdmElement
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000272 RID: 626
		string NamespaceUri { get; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000273 RID: 627
		object Value { get; }
	}
}
