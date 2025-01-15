using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000046 RID: 70
	public interface IEdmOperation : IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000106 RID: 262
		IEdmTypeReference ReturnType { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000107 RID: 263
		IEnumerable<IEdmOperationParameter> Parameters { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000108 RID: 264
		bool IsBound { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000109 RID: 265
		IEdmPathExpression EntitySetPath { get; }

		// Token: 0x0600010A RID: 266
		IEdmOperationParameter FindParameter(string name);
	}
}
