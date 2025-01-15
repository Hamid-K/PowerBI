using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000040 RID: 64
	internal class BadTypeDefinition : BadType, IEdmTypeDefinition, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x0000977E File Offset: 0x0000797E
		public BadTypeDefinition(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000092ED File Offset: 0x000074ED
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000092F0 File Offset: 0x000074F0
		public IEdmPrimitiveType UnderlyingType
		{
			get
			{
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000097A6 File Offset: 0x000079A6
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000097AE File Offset: 0x000079AE
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000067 RID: 103
		private readonly string namespaceName;

		// Token: 0x04000068 RID: 104
		private readonly string name;
	}
}
