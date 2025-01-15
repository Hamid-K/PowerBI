using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200013D RID: 317
	internal class BadPrimitiveType : BadType, IEdmPrimitiveType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x0000E557 File Offset: 0x0000C757
		public BadPrimitiveType(string qualifiedName, EdmPrimitiveTypeKind primitiveKind, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.primitiveKind = primitiveKind;
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000E586 File Offset: 0x0000C786
		public EdmPrimitiveTypeKind PrimitiveKind
		{
			get
			{
				return this.primitiveKind;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000E58E File Offset: 0x0000C78E
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000E596 File Offset: 0x0000C796
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000E59E File Offset: 0x0000C79E
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Primitive;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000E5A1 File Offset: 0x0000C7A1
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x04000242 RID: 578
		private readonly EdmPrimitiveTypeKind primitiveKind;

		// Token: 0x04000243 RID: 579
		private readonly string name;

		// Token: 0x04000244 RID: 580
		private readonly string namespaceName;
	}
}
