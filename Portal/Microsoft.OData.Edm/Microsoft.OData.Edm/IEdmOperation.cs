using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000083 RID: 131
	public interface IEdmOperation : IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600038F RID: 911
		IEdmTypeReference ReturnType { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000390 RID: 912
		IEnumerable<IEdmOperationParameter> Parameters { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000391 RID: 913
		bool IsBound { get; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000392 RID: 914
		IEdmPathExpression EntitySetPath { get; }

		// Token: 0x06000393 RID: 915
		IEdmOperationParameter FindParameter(string name);
	}
}
