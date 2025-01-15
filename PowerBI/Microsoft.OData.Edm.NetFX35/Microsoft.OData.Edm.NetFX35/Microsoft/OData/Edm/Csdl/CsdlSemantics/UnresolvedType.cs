using System;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000211 RID: 529
	internal class UnresolvedType : BadType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement, IUnresolvedElement
	{
		// Token: 0x06000C5D RID: 3165 RVA: 0x00022E98 File Offset: 0x00021098
		public UnresolvedType(string qualifiedName, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedType, Strings.Bad_UnresolvedType(qualifiedName))
			})
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00022EE6 File Offset: 0x000210E6
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x00022EE9 File Offset: 0x000210E9
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00022EF1 File Offset: 0x000210F1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x0400059A RID: 1434
		private readonly string namespaceName;

		// Token: 0x0400059B RID: 1435
		private readonly string name;
	}
}
