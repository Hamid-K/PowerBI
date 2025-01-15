using System;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001BF RID: 447
	internal class UnresolvedValueTerm : UnresolvedVocabularyTerm, IEdmValueTerm, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x000195B2 File Offset: 0x000177B2
		public UnresolvedValueTerm(string qualifiedName)
			: base(qualifiedName)
		{
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x000195C6 File Offset: 0x000177C6
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.ValueTerm;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x000195C9 File Offset: 0x000177C9
		public override EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x000195CC File Offset: 0x000177CC
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x000195D4 File Offset: 0x000177D4
		public string AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x000195DC File Offset: 0x000177DC
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x040004A3 RID: 1187
		private readonly UnresolvedValueTerm.UnresolvedValueTermTypeReference type = new UnresolvedValueTerm.UnresolvedValueTermTypeReference();

		// Token: 0x040004A4 RID: 1188
		private readonly string appliesTo;

		// Token: 0x040004A5 RID: 1189
		private readonly string defaultValue;

		// Token: 0x020001C0 RID: 448
		private class UnresolvedValueTermTypeReference : IEdmTypeReference, IEdmElement
		{
			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x06000972 RID: 2418 RVA: 0x000195E4 File Offset: 0x000177E4
			public bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170003D4 RID: 980
			// (get) Token: 0x06000973 RID: 2419 RVA: 0x000195E7 File Offset: 0x000177E7
			public IEdmType Definition
			{
				get
				{
					return this.definition;
				}
			}

			// Token: 0x040004A6 RID: 1190
			private readonly UnresolvedValueTerm.UnresolvedValueTermTypeReference.UnresolvedValueTermType definition = new UnresolvedValueTerm.UnresolvedValueTermTypeReference.UnresolvedValueTermType();

			// Token: 0x020001C1 RID: 449
			private class UnresolvedValueTermType : IEdmType, IEdmElement
			{
				// Token: 0x170003D5 RID: 981
				// (get) Token: 0x06000975 RID: 2421 RVA: 0x00019602 File Offset: 0x00017802
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
