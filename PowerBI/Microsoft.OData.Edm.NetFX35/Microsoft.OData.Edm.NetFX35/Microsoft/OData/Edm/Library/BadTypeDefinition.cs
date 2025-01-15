using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020000FB RID: 251
	internal class BadTypeDefinition : BadType, IEdmTypeDefinition, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x0000D185 File Offset: 0x0000B385
		public BadTypeDefinition(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000D1AD File Offset: 0x0000B3AD
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
		public IEdmPrimitiveType UnderlyingType
		{
			get
			{
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000D1BE File Offset: 0x0000B3BE
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0000D1C1 File Offset: 0x0000B3C1
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000D1C9 File Offset: 0x0000B3C9
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040001DC RID: 476
		private readonly string namespaceName;

		// Token: 0x040001DD RID: 477
		private readonly string name;
	}
}
