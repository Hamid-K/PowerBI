using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000027 RID: 39
	internal class AmbiguousTypeBinding : AmbiguousBinding<IEdmSchemaType>, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x06000233 RID: 563 RVA: 0x00008FBC File Offset: 0x000071BC
		public AmbiguousTypeBinding(IEdmSchemaType first, IEdmSchemaType second)
			: base(first, second)
		{
			this.namespaceName = first.Namespace ?? string.Empty;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00008FDB File Offset: 0x000071DB
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00008EC3 File Offset: 0x000070C3
		public EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.None;
			}
		}

		// Token: 0x04000045 RID: 69
		private readonly string namespaceName;
	}
}
