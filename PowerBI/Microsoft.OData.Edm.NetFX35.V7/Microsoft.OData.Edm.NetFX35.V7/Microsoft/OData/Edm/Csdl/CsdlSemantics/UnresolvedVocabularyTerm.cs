using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000158 RID: 344
	internal class UnresolvedVocabularyTerm : EdmElement, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IUnresolvedElement
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x00019453 File Offset: 0x00017653
		public UnresolvedVocabularyTerm(string qualifiedName)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00019485 File Offset: 0x00017685
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x0001948D File Offset: 0x0001768D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00008F68 File Offset: 0x00007168
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Term;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00019495 File Offset: 0x00017695
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0001949D File Offset: 0x0001769D
		public string AppliesTo
		{
			get
			{
				return this.appliesTo;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x000194A5 File Offset: 0x000176A5
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		// Token: 0x0400056C RID: 1388
		private readonly UnresolvedVocabularyTerm.UnresolvedTermTypeReference type = new UnresolvedVocabularyTerm.UnresolvedTermTypeReference();

		// Token: 0x0400056D RID: 1389
		private readonly string namespaceName;

		// Token: 0x0400056E RID: 1390
		private readonly string name;

		// Token: 0x0400056F RID: 1391
		private readonly string appliesTo;

		// Token: 0x04000570 RID: 1392
		private readonly string defaultValue;

		// Token: 0x020002A0 RID: 672
		private class UnresolvedTermTypeReference : IEdmTypeReference, IEdmElement
		{
			// Token: 0x170004AD RID: 1197
			// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00008EC3 File Offset: 0x000070C3
			public bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170004AE RID: 1198
			// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x0002AE62 File Offset: 0x00029062
			public IEdmType Definition
			{
				get
				{
					return this.definition;
				}
			}

			// Token: 0x04000803 RID: 2051
			private readonly UnresolvedVocabularyTerm.UnresolvedTermTypeReference.UnresolvedTermType definition = new UnresolvedVocabularyTerm.UnresolvedTermTypeReference.UnresolvedTermType();

			// Token: 0x020002EE RID: 750
			private class UnresolvedTermType : IEdmType, IEdmElement
			{
				// Token: 0x170004C0 RID: 1216
				// (get) Token: 0x060010D8 RID: 4312 RVA: 0x00008EC3 File Offset: 0x000070C3
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
