using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200007E RID: 126
	public sealed class EdmUntypedStructuredType : EdmStructuredType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmSchemaType
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0000C823 File Offset: 0x0000AA23
		public EdmUntypedStructuredType(string namespaceName, string name)
			: base(true, true, null)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000C854 File Offset: 0x0000AA54
		public EdmUntypedStructuredType()
			: this("Edm", "Untyped")
		{
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000C866 File Offset: 0x0000AA66
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000C86E File Offset: 0x0000AA6E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000C876 File Offset: 0x0000AA76
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Untyped;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x0400011A RID: 282
		private readonly string namespaceName;

		// Token: 0x0400011B RID: 283
		private readonly string name;
	}
}
