using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E2 RID: 226
	public class EdmTerm : EdmNamedElement, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600068D RID: 1677 RVA: 0x00011D82 File Offset: 0x0000FF82
		public EdmTerm(string namespaceName, string name, EdmPrimitiveTypeKind type)
			: this(namespaceName, name, type, null)
		{
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00011D8E File Offset: 0x0000FF8E
		public EdmTerm(string namespaceName, string name, EdmPrimitiveTypeKind type, string appliesTo)
			: this(namespaceName, name, EdmCoreModel.Instance.GetPrimitive(type, true), appliesTo)
		{
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00011DA6 File Offset: 0x0000FFA6
		public EdmTerm(string namespaceName, string name, IEdmTypeReference type)
			: this(namespaceName, name, type, null)
		{
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00011DB2 File Offset: 0x0000FFB2
		public EdmTerm(string namespaceName, string name, IEdmTypeReference type, string appliesTo)
			: this(namespaceName, name, type, appliesTo, null)
		{
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		public EdmTerm(string namespaceName, string name, IEdmTypeReference type, string appliesTo, string defaultValue)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.namespaceName = namespaceName;
			this.type = type;
			this.appliesTo = appliesTo;
			this.defaultValue = defaultValue;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00011DFF File Offset: 0x0000FFFF
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00011E07 File Offset: 0x00010007
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00011E0F File Offset: 0x0001000F
		public string AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00011E17 File Offset: 0x00010017
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00008F68 File Offset: 0x00007168
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Term;
			}
		}

		// Token: 0x040003F3 RID: 1011
		private readonly string namespaceName;

		// Token: 0x040003F4 RID: 1012
		private readonly IEdmTypeReference type;

		// Token: 0x040003F5 RID: 1013
		private readonly string appliesTo;

		// Token: 0x040003F6 RID: 1014
		private readonly string defaultValue;
	}
}
