using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000050 RID: 80
	internal class AmbiguousTypeBinding : AmbiguousBinding<IEdmSchemaType>, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmFullNamedElement
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00004CC5 File Offset: 0x00002EC5
		public AmbiguousTypeBinding(IEdmSchemaType first, IEdmSchemaType second)
			: base(first, second)
		{
			this.namespaceName = first.Namespace ?? string.Empty;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(this.namespaceName, base.Name);
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00004CFB File Offset: 0x00002EFB
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00004D03 File Offset: 0x00002F03
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000026A6 File Offset: 0x000008A6
		public EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.None;
			}
		}

		// Token: 0x04000098 RID: 152
		private readonly string namespaceName;

		// Token: 0x04000099 RID: 153
		private readonly string fullName;
	}
}
