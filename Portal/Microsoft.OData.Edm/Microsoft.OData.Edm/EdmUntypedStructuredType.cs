using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000010 RID: 16
	public sealed class EdmUntypedStructuredType : EdmStructuredType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmSchemaType, IEdmFullNamedElement
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003118 File Offset: 0x00001318
		public EdmUntypedStructuredType(string namespaceName, string name)
			: base(true, true, null)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(this.namespaceName, this.name);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000316B File Offset: 0x0000136B
		public EdmUntypedStructuredType()
			: this("Edm", "Untyped")
		{
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000317D File Offset: 0x0000137D
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003185 File Offset: 0x00001385
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000318D File Offset: 0x0000138D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002715 File Offset: 0x00000915
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Untyped;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x0400001C RID: 28
		private readonly string namespaceName;

		// Token: 0x0400001D RID: 29
		private readonly string name;

		// Token: 0x0400001E RID: 30
		private readonly string fullName;
	}
}
