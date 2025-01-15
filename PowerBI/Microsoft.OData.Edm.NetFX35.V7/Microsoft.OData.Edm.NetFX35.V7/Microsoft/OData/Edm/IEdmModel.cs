using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000A3 RID: 163
	public interface IEdmModel : IEdmElement
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600047E RID: 1150
		IEnumerable<IEdmSchemaElement> SchemaElements { get; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600047F RID: 1151
		IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations { get; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000480 RID: 1152
		IEnumerable<IEdmModel> ReferencedModels { get; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000481 RID: 1153
		IEnumerable<string> DeclaredNamespaces { get; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000482 RID: 1154
		IEdmDirectValueAnnotationsManager DirectValueAnnotationsManager { get; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000483 RID: 1155
		IEdmEntityContainer EntityContainer { get; }

		// Token: 0x06000484 RID: 1156
		IEdmSchemaType FindDeclaredType(string qualifiedName);

		// Token: 0x06000485 RID: 1157
		IEnumerable<IEdmOperation> FindDeclaredBoundOperations(IEdmType bindingType);

		// Token: 0x06000486 RID: 1158
		IEnumerable<IEdmOperation> FindDeclaredBoundOperations(string qualifiedName, IEdmType bindingType);

		// Token: 0x06000487 RID: 1159
		IEnumerable<IEdmOperation> FindDeclaredOperations(string qualifiedName);

		// Token: 0x06000488 RID: 1160
		IEdmTerm FindDeclaredTerm(string qualifiedName);

		// Token: 0x06000489 RID: 1161
		IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element);

		// Token: 0x0600048A RID: 1162
		IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType);
	}
}
