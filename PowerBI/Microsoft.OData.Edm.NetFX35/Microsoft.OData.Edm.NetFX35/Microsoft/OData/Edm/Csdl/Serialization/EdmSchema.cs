using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Annotations;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x020001E1 RID: 481
	internal class EdmSchema
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001D66A File Offset: 0x0001B86A
		public EdmSchema(string namespaceString)
		{
			this.schemaNamespace = namespaceString;
			this.schemaElements = new List<IEdmSchemaElement>();
			this.entityContainers = new List<IEdmEntityContainer>();
			this.annotations = new Dictionary<string, List<IEdmVocabularyAnnotation>>();
			this.usedNamespaces = new List<string>();
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0001D6A5 File Offset: 0x0001B8A5
		public string Namespace
		{
			get
			{
				return this.schemaNamespace;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x0001D6AD File Offset: 0x0001B8AD
		public List<IEdmSchemaElement> SchemaElements
		{
			get
			{
				return this.schemaElements;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0001D6B5 File Offset: 0x0001B8B5
		public List<IEdmEntityContainer> EntityContainers
		{
			get
			{
				return this.entityContainers;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0001D6BD File Offset: 0x0001B8BD
		public IEnumerable<KeyValuePair<string, List<IEdmVocabularyAnnotation>>> OutOfLineAnnotations
		{
			get
			{
				return this.annotations;
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001D6C5 File Offset: 0x0001B8C5
		public void AddSchemaElement(IEdmSchemaElement element)
		{
			this.schemaElements.Add(element);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001D6D3 File Offset: 0x0001B8D3
		public void AddEntityContainer(IEdmEntityContainer container)
		{
			this.entityContainers.Add(container);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001D6E1 File Offset: 0x0001B8E1
		public void AddNamespaceUsing(string usedNamespace)
		{
			if (usedNamespace != "Edm" && !this.usedNamespaces.Contains(usedNamespace))
			{
				this.usedNamespaces.Add(usedNamespace);
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0001D70C File Offset: 0x0001B90C
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

		// Token: 0x040004EC RID: 1260
		private readonly string schemaNamespace;

		// Token: 0x040004ED RID: 1261
		private readonly List<IEdmSchemaElement> schemaElements;

		// Token: 0x040004EE RID: 1262
		private readonly List<IEdmEntityContainer> entityContainers;

		// Token: 0x040004EF RID: 1263
		private readonly Dictionary<string, List<IEdmVocabularyAnnotation>> annotations;

		// Token: 0x040004F0 RID: 1264
		private readonly List<string> usedNamespaces;
	}
}
