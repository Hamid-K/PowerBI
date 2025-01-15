using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200004B RID: 75
	public class EdmComplexType : EdmStructuredType, IEdmComplexType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x00009CC4 File Offset: 0x00007EC4
		public EdmComplexType(string namespaceName, string name)
			: this(namespaceName, name, null, false)
		{
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00009CD0 File Offset: 0x00007ED0
		public EdmComplexType(string namespaceName, string name, IEdmComplexType baseType)
			: this(namespaceName, name, baseType, false, false)
		{
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00009CDD File Offset: 0x00007EDD
		public EdmComplexType(string namespaceName, string name, IEdmComplexType baseType, bool isAbstract)
			: this(namespaceName, name, baseType, isAbstract, false)
		{
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00009CEB File Offset: 0x00007EEB
		public EdmComplexType(string namespaceName, string name, IEdmComplexType baseType, bool isAbstract, bool isOpen)
			: base(isAbstract, isOpen, baseType)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00009D1E File Offset: 0x00007F1E
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00009D26 File Offset: 0x00007F26
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00009097 File Offset: 0x00007297
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}

		// Token: 0x04000072 RID: 114
		private readonly string namespaceName;

		// Token: 0x04000073 RID: 115
		private readonly string name;
	}
}
