using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x0200015B RID: 347
	internal class EdmModelSchemaSeparationSerializationVisitor : EdmModelVisitor
	{
		// Token: 0x060008CF RID: 2255 RVA: 0x000182C3 File Offset: 0x000164C3
		public EdmModelSchemaSeparationSerializationVisitor(IEdmModel visitedModel)
			: base(visitedModel)
		{
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000182D7 File Offset: 0x000164D7
		public IEnumerable<EdmSchema> GetSchemas()
		{
			if (!this.visitCompleted)
			{
				this.Visit();
			}
			return this.modelSchemas.Values;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x000182F2 File Offset: 0x000164F2
		protected void Visit()
		{
			base.VisitEdmModel();
			this.visitCompleted = true;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00018301 File Offset: 0x00016501
		protected override void ProcessModel(IEdmModel model)
		{
			this.ProcessElement(model);
			base.VisitSchemaElements(model.SchemaElements);
			base.VisitVocabularyAnnotations(model.VocabularyAnnotations.Where((IEdmVocabularyAnnotation a) => !a.IsInline(this.Model)));
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00018333 File Offset: 0x00016533
		protected override void ProcessVocabularyAnnotatable(IEdmVocabularyAnnotatable element)
		{
			base.VisitAnnotations(this.Model.DirectValueAnnotations(element));
			base.VisitVocabularyAnnotations(from a in this.Model.FindDeclaredVocabularyAnnotations(element)
				where a.IsInline(this.Model)
				select a);
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0001836C File Offset: 0x0001656C
		protected override void ProcessSchemaElement(IEdmSchemaElement element)
		{
			string text = element.Namespace;
			if (EdmUtil.IsNullOrWhiteSpaceInternal(text))
			{
				text = string.Empty;
			}
			EdmSchema edmSchema;
			if (!this.modelSchemas.TryGetValue(text, out edmSchema))
			{
				edmSchema = new EdmSchema(text);
				this.modelSchemas.Add(text, edmSchema);
			}
			edmSchema.AddSchemaElement(element);
			this.activeSchema = edmSchema;
			base.ProcessSchemaElement(element);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x000183C8 File Offset: 0x000165C8
		protected override void ProcessVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			if (!annotation.IsInline(this.Model))
			{
				string text;
				if ((text = annotation.GetSchemaNamespace(this.Model)) == null)
				{
					text = this.modelSchemas.Select((KeyValuePair<string, EdmSchema> s) => s.Key).FirstOrDefault<string>() ?? string.Empty;
				}
				string text2 = text;
				EdmSchema edmSchema;
				if (!this.modelSchemas.TryGetValue(text2, out edmSchema))
				{
					edmSchema = new EdmSchema(text2);
					this.modelSchemas.Add(edmSchema.Namespace, edmSchema);
				}
				edmSchema.AddVocabularyAnnotation(annotation);
				this.activeSchema = edmSchema;
			}
			if (annotation.Term != null)
			{
				this.CheckSchemaElementReference(annotation.Term);
			}
			base.ProcessVocabularyAnnotation(annotation);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00018484 File Offset: 0x00016684
		protected override void ProcessEntityContainer(IEdmEntityContainer element)
		{
			string @namespace = element.Namespace;
			EdmSchema edmSchema;
			if (!this.modelSchemas.TryGetValue(@namespace, out edmSchema))
			{
				edmSchema = new EdmSchema(@namespace);
				this.modelSchemas.Add(edmSchema.Namespace, edmSchema);
			}
			edmSchema.AddEntityContainer(element);
			this.activeSchema = edmSchema;
			base.ProcessEntityContainer(element);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000184D6 File Offset: 0x000166D6
		protected override void ProcessComplexTypeReference(IEdmComplexTypeReference element)
		{
			this.CheckSchemaElementReference(element.ComplexDefinition());
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000184E4 File Offset: 0x000166E4
		protected override void ProcessEntityTypeReference(IEdmEntityTypeReference element)
		{
			this.CheckSchemaElementReference(element.EntityDefinition());
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000184F2 File Offset: 0x000166F2
		protected override void ProcessEntityReferenceTypeReference(IEdmEntityReferenceTypeReference element)
		{
			this.CheckSchemaElementReference(element.EntityType());
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00018500 File Offset: 0x00016700
		protected override void ProcessEnumTypeReference(IEdmEnumTypeReference element)
		{
			this.CheckSchemaElementReference(element.EnumDefinition());
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001850E File Offset: 0x0001670E
		protected override void ProcessTypeDefinitionReference(IEdmTypeDefinitionReference element)
		{
			this.CheckSchemaElementReference(element.TypeDefinition());
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001851C File Offset: 0x0001671C
		protected override void ProcessEntityType(IEdmEntityType element)
		{
			base.ProcessEntityType(element);
			if (element.BaseEntityType() != null)
			{
				this.CheckSchemaElementReference(element.BaseEntityType());
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00018539 File Offset: 0x00016739
		protected override void ProcessComplexType(IEdmComplexType element)
		{
			base.ProcessComplexType(element);
			if (element.BaseComplexType() != null)
			{
				this.CheckSchemaElementReference(element.BaseComplexType());
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00018556 File Offset: 0x00016756
		protected override void ProcessEnumType(IEdmEnumType element)
		{
			base.ProcessEnumType(element);
			this.CheckSchemaElementReference(element.UnderlyingType);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001856B File Offset: 0x0001676B
		protected override void ProcessTypeDefinition(IEdmTypeDefinition element)
		{
			base.ProcessTypeDefinition(element);
			this.CheckSchemaElementReference(element.UnderlyingType);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00018580 File Offset: 0x00016780
		private void CheckSchemaElementReference(IEdmSchemaElement element)
		{
			this.CheckSchemaElementReference(element.Namespace);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001858E File Offset: 0x0001678E
		private void CheckSchemaElementReference(string namespaceName)
		{
			if (this.activeSchema != null)
			{
				this.activeSchema.AddNamespaceUsing(namespaceName);
			}
		}

		// Token: 0x040005BF RID: 1471
		private bool visitCompleted;

		// Token: 0x040005C0 RID: 1472
		private Dictionary<string, EdmSchema> modelSchemas = new Dictionary<string, EdmSchema>();

		// Token: 0x040005C1 RID: 1473
		private EdmSchema activeSchema;
	}
}
