using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000120 RID: 288
	internal class AmbiguousTypeBinding : AmbiguousBinding<IEdmSchemaType>, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x0000DFA5 File Offset: 0x0000C1A5
		public AmbiguousTypeBinding(IEdmSchemaType first, IEdmSchemaType second)
			: base(first, second)
		{
			this.namespaceName = first.Namespace ?? string.Empty;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000DFC4 File Offset: 0x0000C1C4
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000DFC7 File Offset: 0x0000C1C7
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000DFCF File Offset: 0x0000C1CF
		public EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.None;
			}
		}

		// Token: 0x04000225 RID: 549
		private readonly string namespaceName;
	}
}
