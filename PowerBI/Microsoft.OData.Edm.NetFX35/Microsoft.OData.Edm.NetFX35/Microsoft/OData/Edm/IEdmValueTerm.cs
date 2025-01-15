using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000CB RID: 203
	public interface IEdmValueTerm : IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000373 RID: 883
		IEdmTypeReference Type { get; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000374 RID: 884
		string AppliesTo { get; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000375 RID: 885
		string DefaultValue { get; }
	}
}
