using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x0200015E RID: 350
	internal class EdmSchema
	{
		// Token: 0x06000980 RID: 2432 RVA: 0x0001AF72 File Offset: 0x00019172
		public EdmSchema(string namespaceString)
		{
			this.schemaNamespace = namespaceString;
			this.schemaElements = new List<IEdmSchemaElement>();
			this.entityContainers = new List<IEdmEntityContainer>();
			this.annotations = new Dictionary<string, List<IEdmVocabularyAnnotation>>();
			this.usedNamespaces = new List<string>();
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0001AFAD File Offset: 0x000191AD
		public string Namespace
		{
			get
			{
				return this.schemaNamespace;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0001AFB5 File Offset: 0x000191B5
		public List<IEdmSchemaElement> SchemaElements
		{
			get
			{
				return this.schemaElements;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0001AFBD File Offset: 0x000191BD
		public List<IEdmEntityContainer> EntityContainers
		{
			get
			{
				return this.entityContainers;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0001AFC5 File Offset: 0x000191C5
		public IEnumerable<KeyValuePair<string, List<IEdmVocabularyAnnotation>>> OutOfLineAnnotations
		{
			get
			{
				return this.annotations;
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001AFCD File Offset: 0x000191CD
		public void AddSchemaElement(IEdmSchemaElement element)
		{
			this.schemaElements.Add(element);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001AFDB File Offset: 0x000191DB
		public void AddEntityContainer(IEdmEntityContainer container)
		{
			this.entityContainers.Add(container);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0001AFE9 File Offset: 0x000191E9
		public void AddNamespaceUsing(string usedNamespace)
		{
			if (usedNamespace != "Edm" && !this.usedNamespaces.Contains(usedNamespace))
			{
				this.usedNamespaces.Add(usedNamespace);
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0001B014 File Offset: 0x00019214
		public void AddVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			List<IEdmVocabularyAnnotation> list;
			if (!this.annotations.TryGetValue(annotation.TargetString(), out list))
			{
				list = new List<IEdmVocabularyAnnotation>();
				this.annotations[annotation.TargetString()] = list;
			}
			list.Add(annotation);
		}

		// Token: 0x040005CB RID: 1483
		private readonly string schemaNamespace;

		// Token: 0x040005CC RID: 1484
		private readonly List<IEdmSchemaElement> schemaElements;

		// Token: 0x040005CD RID: 1485
		private readonly List<IEdmEntityContainer> entityContainers;

		// Token: 0x040005CE RID: 1486
		private readonly Dictionary<string, List<IEdmVocabularyAnnotation>> annotations;

		// Token: 0x040005CF RID: 1487
		private readonly List<string> usedNamespaces;
	}
}
