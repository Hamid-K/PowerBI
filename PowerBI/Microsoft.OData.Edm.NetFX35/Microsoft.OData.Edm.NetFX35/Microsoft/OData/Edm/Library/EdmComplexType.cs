using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001FA RID: 506
	public class EdmComplexType : EdmStructuredType, IEdmComplexType, IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x00021A78 File Offset: 0x0001FC78
		public EdmComplexType(string namespaceName, string name)
			: this(namespaceName, name, null, false)
		{
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00021A84 File Offset: 0x0001FC84
		public EdmComplexType(string namespaceName, string name, IEdmComplexType baseType)
			: this(namespaceName, name, baseType, false, false)
		{
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00021A91 File Offset: 0x0001FC91
		public EdmComplexType(string namespaceName, string name, IEdmComplexType baseType, bool isAbstract)
			: this(namespaceName, name, baseType, isAbstract, false)
		{
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00021A9F File Offset: 0x0001FC9F
		public EdmComplexType(string namespaceName, string name, IEdmComplexType baseType, bool isAbstract, bool isOpen)
			: base(isAbstract, isOpen, baseType)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x00021AD2 File Offset: 0x0001FCD2
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x00021AD5 File Offset: 0x0001FCD5
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00021ADD File Offset: 0x0001FCDD
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00021AE5 File Offset: 0x0001FCE5
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00021AE8 File Offset: 0x0001FCE8
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Type;
			}
		}

		// Token: 0x0400056F RID: 1391
		private readonly string namespaceName;

		// Token: 0x04000570 RID: 1392
		private readonly string name;
	}
}
