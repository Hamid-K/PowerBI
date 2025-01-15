using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000BE RID: 190
	public interface IEdmStructuralProperty : IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004B8 RID: 1208
		string DefaultValueString { get; }
	}
}
