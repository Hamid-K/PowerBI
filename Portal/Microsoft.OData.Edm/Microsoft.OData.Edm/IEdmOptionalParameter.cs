using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200009B RID: 155
	public interface IEdmOptionalParameter : IEdmOperationParameter, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060003C8 RID: 968
		string DefaultValueString { get; }
	}
}
