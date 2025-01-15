using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200008C RID: 140
	public interface IEdmStructuralProperty : IEdmProperty, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600039B RID: 923
		string DefaultValueString { get; }
	}
}
