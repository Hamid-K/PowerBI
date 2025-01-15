using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Annotations;

namespace Microsoft.OData.Edm
{
	// Token: 0x020001AF RID: 431
	public interface IEdmModel : IEdmElement
	{
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060008E3 RID: 2275
		IEnumerable<IEdmSchemaElement> SchemaElements { get; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060008E4 RID: 2276
		IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations { get; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060008E5 RID: 2277
		IEnumerable<IEdmModel> ReferencedModels { get; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060008E6 RID: 2278
		IEnumerable<string> DeclaredNamespaces { get; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060008E7 RID: 2279
		IEdmDirectValueAnnotationsManager DirectValueAnnotationsManager { get; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060008E8 RID: 2280
		IEdmEntityContainer EntityContainer { get; }

		// Token: 0x060008E9 RID: 2281
		IEdmSchemaType FindDeclaredType(string qualifiedName);

		// Token: 0x060008EA RID: 2282
		IEnumerable<IEdmOperation> FindDeclaredBoundOperations(IEdmType bindingType);

		// Token: 0x060008EB RID: 2283
		IEnumerable<IEdmOperation> FindDeclaredBoundOperations(string qualifiedName, IEdmType bindingType);

		// Token: 0x060008EC RID: 2284
		IEnumerable<IEdmOperation> FindDeclaredOperations(string qualifiedName);

		// Token: 0x060008ED RID: 2285
		IEdmValueTerm FindDeclaredValueTerm(string qualifiedName);

		// Token: 0x060008EE RID: 2286
		IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element);

		// Token: 0x060008EF RID: 2287
		IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType);
	}
}
