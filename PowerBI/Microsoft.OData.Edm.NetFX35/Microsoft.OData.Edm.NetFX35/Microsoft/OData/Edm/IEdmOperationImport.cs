using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000063 RID: 99
	public interface IEdmOperationImport : IEdmEntityContainerElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600017E RID: 382
		IEdmOperation Operation { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600017F RID: 383
		IEdmExpression EntitySet { get; }
	}
}
