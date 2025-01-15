using System;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x0200018C RID: 396
	internal class UnresolvedValueTerm : UnresolvedVocabularyTerm, IEdmValueTerm, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060008BA RID: 2234 RVA: 0x0001833B File Offset: 0x0001653B
		public UnresolvedValueTerm(string qualifiedName)
			: base(qualifiedName)
		{
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0001834F File Offset: 0x0001654F
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.ValueTerm;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00018352 File Offset: 0x00016552
		public override EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Value;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00018355 File Offset: 0x00016555
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000453 RID: 1107
		private readonly UnresolvedValueTerm.UnresolvedValueTermTypeReference type = new UnresolvedValueTerm.UnresolvedValueTermTypeReference();

		// Token: 0x0200018D RID: 397
		private class UnresolvedValueTermTypeReference : IEdmTypeReference, IEdmElement
		{
			// Token: 0x170003A7 RID: 935
			// (get) Token: 0x060008BE RID: 2238 RVA: 0x0001835D File Offset: 0x0001655D
			public bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003A8 RID: 936
			// (get) Token: 0x060008BF RID: 2239 RVA: 0x00018360 File Offset: 0x00016560
			public IEdmType Definition
			{
				get
				{
					return this.definition;
				}
			}

			// Token: 0x04000454 RID: 1108
			private readonly UnresolvedValueTerm.UnresolvedValueTermTypeReference.UnresolvedValueTermType definition = new UnresolvedValueTerm.UnresolvedValueTermTypeReference.UnresolvedValueTermType();

			// Token: 0x0200018E RID: 398
			private class UnresolvedValueTermType : IEdmType, IEdmElement
			{
				// Token: 0x170003A9 RID: 937
				// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001837B File Offset: 0x0001657B
				public EdmTypeKind TypeKind
				{
					get
					{
						return EdmTypeKind.None;
					}
				}
			}
		}
	}
}
