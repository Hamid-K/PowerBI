using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000167 RID: 359
	internal class UnresolvedVocabularyTerm : EdmElement, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IUnresolvedElement, IEdmFullNamedElement
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x0001B50F File Offset: 0x0001970F
		public UnresolvedVocabularyTerm(string qualifiedName)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name, out this.fullName);
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0001B547 File Offset: 0x00019747
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0001B54F File Offset: 0x0001974F
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0001B557 File Offset: 0x00019757
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00002732 File Offset: 0x00000932
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Term;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0001B55F File Offset: 0x0001975F
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0001B567 File Offset: 0x00019767
		public string AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0001B56F File Offset: 0x0001976F
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x040005E5 RID: 1509
		private readonly UnresolvedVocabularyTerm.UnresolvedTermTypeReference type = new UnresolvedVocabularyTerm.UnresolvedTermTypeReference();

		// Token: 0x040005E6 RID: 1510
		private readonly string namespaceName;

		// Token: 0x040005E7 RID: 1511
		private readonly string name;

		// Token: 0x040005E8 RID: 1512
		private readonly string fullName;

		// Token: 0x040005E9 RID: 1513
		private readonly string appliesTo;

		// Token: 0x040005EA RID: 1514
		private readonly string defaultValue;

		// Token: 0x020002BA RID: 698
		private class UnresolvedTermTypeReference : IEdmTypeReference, IEdmElement
		{
			// Token: 0x170004E9 RID: 1257
			// (get) Token: 0x0600108A RID: 4234 RVA: 0x000026A6 File Offset: 0x000008A6
			public bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170004EA RID: 1258
			// (get) Token: 0x0600108B RID: 4235 RVA: 0x0002D822 File Offset: 0x0002BA22
			public IEdmType Definition
			{
				get
				{
					return this.definition;
				}
			}

			// Token: 0x04000897 RID: 2199
			private readonly UnresolvedVocabularyTerm.UnresolvedTermTypeReference.UnresolvedTermType definition = new UnresolvedVocabularyTerm.UnresolvedTermTypeReference.UnresolvedTermType();

			// Token: 0x02000307 RID: 775
			private class UnresolvedTermType : IEdmType, IEdmElement
			{
				// Token: 0x170004FC RID: 1276
				// (get) Token: 0x060011BB RID: 4539 RVA: 0x000026A6 File Offset: 0x000008A6
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
