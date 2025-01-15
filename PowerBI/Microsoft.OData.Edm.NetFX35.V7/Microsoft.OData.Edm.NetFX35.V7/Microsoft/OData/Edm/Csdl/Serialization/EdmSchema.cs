using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x02000152 RID: 338
	internal class EdmSchema
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x0001921E File Offset: 0x0001741E
		public EdmSchema(string namespaceString)
		{
			this.schemaNamespace = namespaceString;
			this.schemaElements = new List<IEdmSchemaElement>();
			this.entityContainers = new List<IEdmEntityContainer>();
			this.annotations = new Dictionary<string, List<IEdmVocabularyAnnotation>>();
			this.usedNamespaces = new List<string>();
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00019259 File Offset: 0x00017459
		public string Namespace
		{
			get
			{
				return this.schemaNamespace;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00019261 File Offset: 0x00017461
		public List<IEdmSchemaElement> SchemaElements
		{
			get
			{
				return this.schemaElements;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00019269 File Offset: 0x00017469
		public List<IEdmEntityContainer> EntityContainers
		{
			get
			{
				return this.entityContainers;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00019271 File Offset: 0x00017471
		public IEnumerable<KeyValuePair<string, List<IEdmVocabularyAnnotation>>> OutOfLineAnnotations
		{
			get
			{
				return this.annotations;
			}
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00019279 File Offset: 0x00017479
		public void AddSchemaElement(IEdmSchemaElement element)
		{
			this.schemaElements.Add(element);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00019287 File Offset: 0x00017487
		public void AddEntityContainer(IEdmEntityContainer container)
		{
			this.entityContainers.Add(container);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00019295 File Offset: 0x00017495
		public void AddNamespaceUsing(string usedNamespace)
		{
			if (usedNamespace != "Edm" && !this.usedNamespaces.Contains(usedNamespace))
			{
				this.usedNamespaces.Add(usedNamespace);
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000192C0 File Offset: 0x000174C0
		public void AddVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			List<IEdmVocabularyAnnotation> list;
			if (!this.annotations.TryGetValue(annotation.TargetString(), ref list))
			{
				list = new List<IEdmVocabularyAnnotation>();
				this.annotations[annotation.TargetString()] = list;
			}
			list.Add(annotation);
		}

		// Token: 0x04000561 RID: 1377
		private readonly string schemaNamespace;

		// Token: 0x04000562 RID: 1378
		private readonly List<IEdmSchemaElement> schemaElements;

		// Token: 0x04000563 RID: 1379
		private readonly List<IEdmEntityContainer> entityContainers;

		// Token: 0x04000564 RID: 1380
		private readonly Dictionary<string, List<IEdmVocabularyAnnotation>> annotations;

		// Token: 0x04000565 RID: 1381
		private readonly List<string> usedNamespaces;
	}
}
