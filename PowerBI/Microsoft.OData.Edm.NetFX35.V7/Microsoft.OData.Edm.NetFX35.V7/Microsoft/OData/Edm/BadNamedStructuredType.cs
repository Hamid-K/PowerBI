using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000036 RID: 54
	internal abstract class BadNamedStructuredType : BadStructuredType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000274 RID: 628 RVA: 0x00009365 File Offset: 0x00007565
		protected BadNamedStructuredType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000938D File Offset: 0x0000758D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00009395 File Offset: 0x00007595
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x04000056 RID: 86
		private readonly string namespaceName;

		// Token: 0x04000057 RID: 87
		private readonly string name;
	}
}
