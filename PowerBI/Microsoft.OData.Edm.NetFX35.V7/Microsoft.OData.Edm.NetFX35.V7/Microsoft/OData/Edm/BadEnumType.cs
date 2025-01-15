using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000034 RID: 52
	internal class BadEnumType : BadType, IEdmEnumType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x06000266 RID: 614 RVA: 0x000092BE File Offset: 0x000074BE
		public BadEnumType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000092E6 File Offset: 0x000074E6
		public IEnumerable<IEdmEnumMember> Members
		{
			get
			{
				return Enumerable.Empty<IEdmEnumMember>();
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000268 RID: 616 RVA: 0x000092ED File Offset: 0x000074ED
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000092F0 File Offset: 0x000074F0
		public IEdmPrimitiveType UnderlyingType
		{
			get
			{
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00008EC3 File Offset: 0x000070C3
		public bool IsFlags
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000092FE File Offset: 0x000074FE
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00009306 File Offset: 0x00007506
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000051 RID: 81
		private readonly string namespaceName;

		// Token: 0x04000052 RID: 82
		private readonly string name;
	}
}
