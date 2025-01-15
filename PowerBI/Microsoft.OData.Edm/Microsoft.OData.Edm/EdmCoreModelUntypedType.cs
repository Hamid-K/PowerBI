using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200000B RID: 11
	internal sealed class EdmCoreModelUntypedType : EdmType, IEdmUntypedType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmCoreModelElement, IEdmFullNamedElement
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002683 File Offset: 0x00000883
		private EdmCoreModelUntypedType()
		{
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002715 File Offset: 0x00000915
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Untyped;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002718 File Offset: 0x00000918
		public string Name
		{
			get
			{
				return "Untyped";
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002698 File Offset: 0x00000898
		public string Namespace
		{
			get
			{
				return "Edm";
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000271F File Offset: 0x0000091F
		public string FullName
		{
			get
			{
				return "Edm.Untyped";
			}
		}

		// Token: 0x0400000C RID: 12
		public static readonly EdmCoreModelUntypedType Instance = new EdmCoreModelUntypedType();
	}
}
