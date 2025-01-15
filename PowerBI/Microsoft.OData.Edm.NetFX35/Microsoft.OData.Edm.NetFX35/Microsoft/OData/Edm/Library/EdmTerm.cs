using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001D7 RID: 471
	public class EdmTerm : EdmNamedElement, IEdmValueTerm, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x00019AF5 File Offset: 0x00017CF5
		public EdmTerm(string namespaceName, string name, EdmPrimitiveTypeKind type)
			: this(namespaceName, name, type, null)
		{
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00019B01 File Offset: 0x00017D01
		public EdmTerm(string namespaceName, string name, EdmPrimitiveTypeKind type, string appliesTo)
			: this(namespaceName, name, EdmCoreModel.Instance.GetPrimitive(type, true), appliesTo)
		{
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x00019B19 File Offset: 0x00017D19
		public EdmTerm(string namespaceName, string name, IEdmTypeReference type)
			: this(namespaceName, name, type, null)
		{
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00019B25 File Offset: 0x00017D25
		public EdmTerm(string namespaceName, string name, IEdmTypeReference type, string appliesTo)
			: this(namespaceName, name, type, appliesTo, null)
		{
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00019B33 File Offset: 0x00017D33
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

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00019B72 File Offset: 0x00017D72
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00019B7A File Offset: 0x00017D7A
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Value;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00019B7D File Offset: 0x00017D7D
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00019B85 File Offset: 0x00017D85
		public string AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00019B8D File Offset: 0x00017D8D
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00019B95 File Offset: 0x00017D95
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.ValueTerm;
			}
		}

		// Token: 0x040004C5 RID: 1221
		private readonly string namespaceName;

		// Token: 0x040004C6 RID: 1222
		private readonly IEdmTypeReference type;

		// Token: 0x040004C7 RID: 1223
		private readonly string appliesTo;

		// Token: 0x040004C8 RID: 1224
		private readonly string defaultValue;
	}
}
