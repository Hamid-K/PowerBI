using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200009C RID: 156
	public interface IEdmModel : IEdmElement
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060003C9 RID: 969
		IEnumerable<IEdmSchemaElement> SchemaElements { get; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003CA RID: 970
		IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations { get; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003CB RID: 971
		IEnumerable<IEdmModel> ReferencedModels { get; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003CC RID: 972
		IEnumerable<string> DeclaredNamespaces { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003CD RID: 973
		IEdmDirectValueAnnotationsManager DirectValueAnnotationsManager { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003CE RID: 974
		IEdmEntityContainer EntityContainer { get; }

		// Token: 0x060003CF RID: 975
		IEdmSchemaType FindDeclaredType(string qualifiedName);

		// Token: 0x060003D0 RID: 976
		IEnumerable<IEdmOperation> FindDeclaredBoundOperations(IEdmType bindingType);

		// Token: 0x060003D1 RID: 977
		IEnumerable<IEdmOperation> FindDeclaredBoundOperations(string qualifiedName, IEdmType bindingType);

		// Token: 0x060003D2 RID: 978
		IEnumerable<IEdmOperation> FindDeclaredOperations(string qualifiedName);

		// Token: 0x060003D3 RID: 979
		IEdmTerm FindDeclaredTerm(string qualifiedName);

		// Token: 0x060003D4 RID: 980
		IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element);

		// Token: 0x060003D5 RID: 981
		IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType);
	}
}
