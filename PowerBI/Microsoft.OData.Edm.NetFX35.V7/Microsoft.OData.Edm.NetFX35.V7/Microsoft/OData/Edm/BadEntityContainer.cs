using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200002F RID: 47
	internal class BadEntityContainer : BadElement, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000249 RID: 585 RVA: 0x000091BE File Offset: 0x000073BE
		public BadEntityContainer(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00008D62 File Offset: 0x00006F62
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return Enumerable.Empty<IEdmEntityContainerElement>();
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000091E6 File Offset: 0x000073E6
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600024C RID: 588 RVA: 0x000091EE File Offset: 0x000073EE
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00008D57 File Offset: 0x00006F57
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmEntitySet FindEntitySet(string setName)
		{
			return null;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEdmSingleton FindSingleton(string singletonName)
		{
			return null;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEnumerable<IEdmOperationImport> FindOperationImports(string operationName)
		{
			return null;
		}

		// Token: 0x0400004B RID: 75
		private readonly string namespaceName;

		// Token: 0x0400004C RID: 76
		private readonly string name;
	}
}
