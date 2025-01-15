using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E5 RID: 229
	public class EdmTerm : EdmNamedElement, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x00010F1C File Offset: 0x0000F11C
		public EdmTerm(string namespaceName, string name, EdmPrimitiveTypeKind type)
			: this(namespaceName, name, type, null)
		{
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00010F28 File Offset: 0x0000F128
		public EdmTerm(string namespaceName, string name, EdmPrimitiveTypeKind type, string appliesTo)
			: this(namespaceName, name, EdmCoreModel.Instance.GetPrimitive(type, true), appliesTo)
		{
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00010F40 File Offset: 0x0000F140
		public EdmTerm(string namespaceName, string name, IEdmTypeReference type)
			: this(namespaceName, name, type, null)
		{
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00010F4C File Offset: 0x0000F14C
		public EdmTerm(string namespaceName, string name, IEdmTypeReference type, string appliesTo)
			: this(namespaceName, name, type, appliesTo, null)
		{
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00010F5C File Offset: 0x0000F15C
		public EdmTerm(string namespaceName, string name, IEdmTypeReference type, string appliesTo, string defaultValue)
			: base(name)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			this.namespaceName = namespaceName;
			this.type = type;
			this.appliesTo = appliesTo;
			this.defaultValue = defaultValue;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(this.namespaceName, base.Name);
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00010FBD File Offset: 0x0000F1BD
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x00010FC5 File Offset: 0x0000F1C5
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x00010FCD File Offset: 0x0000F1CD
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x00010FD5 File Offset: 0x0000F1D5
		public string AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00010FDD File Offset: 0x0000F1DD
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00002732 File Offset: 0x00000932
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Term;
			}
		}

		// Token: 0x040002F0 RID: 752
		private readonly string namespaceName;

		// Token: 0x040002F1 RID: 753
		private readonly string fullName;

		// Token: 0x040002F2 RID: 754
		private readonly IEdmTypeReference type;

		// Token: 0x040002F3 RID: 755
		private readonly string appliesTo;

		// Token: 0x040002F4 RID: 756
		private readonly string defaultValue;
	}
}
