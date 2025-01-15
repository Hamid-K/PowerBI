using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001CB RID: 459
	public class EdmComplexType : EdmStructuredType, IEdmComplexType, IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x0001FF9C File Offset: 0x0001E19C
		public EdmComplexType(string namespaceName, string name)
			: this(namespaceName, name, null, false)
		{
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0001FFA8 File Offset: 0x0001E1A8
		public EdmComplexType(string namespaceName, string name, IEdmComplexType baseType, bool isAbstract)
			: base(isAbstract, false, baseType)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0001FFDA File Offset: 0x0001E1DA
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0001FFDD File Offset: 0x0001E1DD
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0001FFE5 File Offset: 0x0001E1E5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0001FFED File Offset: 0x0001E1ED
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0001FFF0 File Offset: 0x0001E1F0
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Type;
			}
		}

		// Token: 0x0400051D RID: 1309
		private readonly string namespaceName;

		// Token: 0x0400051E RID: 1310
		private readonly string name;
	}
}
