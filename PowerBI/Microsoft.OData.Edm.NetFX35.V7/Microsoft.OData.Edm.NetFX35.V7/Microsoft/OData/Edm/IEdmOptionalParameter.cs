using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000AE RID: 174
	public interface IEdmOptionalParameter : IEdmOperationParameter, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004A2 RID: 1186
		string DefaultValueString { get; }
	}
}
