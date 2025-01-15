using System;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000054 RID: 84
	internal abstract class UnresolvedVocabularyTerm : EdmElement, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement, IUnresolvedElement
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00003BB1 File Offset: 0x00001DB1
		protected UnresolvedVocabularyTerm(string qualifiedName)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00003BD8 File Offset: 0x00001DD8
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600012E RID: 302
		public abstract EdmTermKind TermKind { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600012F RID: 303
		public abstract EdmSchemaElementKind SchemaElementKind { get; }

		// Token: 0x04000067 RID: 103
		private readonly string namespaceName;

		// Token: 0x04000068 RID: 104
		private readonly string name;
	}
}
