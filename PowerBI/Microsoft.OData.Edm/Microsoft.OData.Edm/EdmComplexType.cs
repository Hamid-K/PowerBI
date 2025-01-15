using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B2 RID: 178
	public class EdmComplexType : EdmStructuredType, IEdmComplexType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0000AB13 File Offset: 0x00008D13
		public EdmComplexType(string namespaceName, string name)
			: this(namespaceName, name, null, false)
		{
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000AB1F File Offset: 0x00008D1F
		public EdmComplexType(string namespaceName, string name, IEdmComplexType baseType)
			: this(namespaceName, name, baseType, false, false)
		{
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000AB2C File Offset: 0x00008D2C
		public EdmComplexType(string namespaceName, string name, IEdmComplexType baseType, bool isAbstract)
			: this(namespaceName, name, baseType, isAbstract, false)
		{
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000AB3C File Offset: 0x00008D3C
		public EdmComplexType(string namespaceName, string name, IEdmComplexType baseType, bool isAbstract, bool isOpen)
			: base(isAbstract, isOpen, baseType)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(this.namespaceName, this.Name);
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000AB91 File Offset: 0x00008D91
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000AB99 File Offset: 0x00008D99
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000ABA1 File Offset: 0x00008DA1
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000268B File Offset: 0x0000088B
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}

		// Token: 0x0400013E RID: 318
		private readonly string namespaceName;

		// Token: 0x0400013F RID: 319
		private readonly string name;

		// Token: 0x04000140 RID: 320
		private readonly string fullName;
	}
}
