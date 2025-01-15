using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000128 RID: 296
	internal abstract class BadNamedStructuredType : BadStructuredType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x0000E19C File Offset: 0x0000C39C
		protected BadNamedStructuredType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000E1CC File Offset: 0x0000C3CC
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x04000230 RID: 560
		private readonly string namespaceName;

		// Token: 0x04000231 RID: 561
		private readonly string name;
	}
}
