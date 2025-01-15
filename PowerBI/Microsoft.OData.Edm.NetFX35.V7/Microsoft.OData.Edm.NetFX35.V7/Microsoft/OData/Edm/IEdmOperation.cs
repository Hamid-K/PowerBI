using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000AB RID: 171
	public interface IEdmOperation : IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000499 RID: 1177
		IEdmTypeReference ReturnType { get; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600049A RID: 1178
		IEnumerable<IEdmOperationParameter> Parameters { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600049B RID: 1179
		bool IsBound { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600049C RID: 1180
		IEdmPathExpression EntitySetPath { get; }

		// Token: 0x0600049D RID: 1181
		IEdmOperationParameter FindParameter(string name);
	}
}
