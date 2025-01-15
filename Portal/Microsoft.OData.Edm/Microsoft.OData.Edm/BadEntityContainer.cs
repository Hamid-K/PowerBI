using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000058 RID: 88
	internal class BadEntityContainer : BadElement, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00004F61 File Offset: 0x00003161
		public BadEntityContainer(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name, out this.fullName);
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00004B0E File Offset: 0x00002D0E
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return Enumerable.Empty<IEdmEntityContainerElement>();
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00004F8F File Offset: 0x0000318F
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00004F97 File Offset: 0x00003197
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00004F9F File Offset: 0x0000319F
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000039FB File Offset: 0x00001BFB
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmEntitySet FindEntitySet(string setName)
		{
			return null;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEdmSingleton FindSingleton(string singletonName)
		{
			return null;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEnumerable<IEdmOperationImport> FindOperationImports(string operationName)
		{
			return null;
		}

		// Token: 0x040000A5 RID: 165
		private readonly string namespaceName;

		// Token: 0x040000A6 RID: 166
		private readonly string name;

		// Token: 0x040000A7 RID: 167
		private readonly string fullName;
	}
}
