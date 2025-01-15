using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200005F RID: 95
	internal abstract class BadNamedStructuredType : BadStructuredType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00005112 File Offset: 0x00003312
		protected BadNamedStructuredType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name, out this.fullName);
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00005140 File Offset: 0x00003340
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00005148 File Offset: 0x00003348
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00005150 File Offset: 0x00003350
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x040000B2 RID: 178
		private readonly string namespaceName;

		// Token: 0x040000B3 RID: 179
		private readonly string name;

		// Token: 0x040000B4 RID: 180
		private readonly string fullName;
	}
}
