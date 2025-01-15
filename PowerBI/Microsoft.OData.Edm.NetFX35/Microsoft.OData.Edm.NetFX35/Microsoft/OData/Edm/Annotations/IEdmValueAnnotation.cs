using System;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Annotations
{
	// Token: 0x020000C9 RID: 201
	public interface IEdmValueAnnotation : IEdmVocabularyAnnotation, IEdmElement
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600036C RID: 876
		IEdmExpression Value { get; }
	}
}
