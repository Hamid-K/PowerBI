using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x0200014F RID: 335
	internal class EdmModelSchemaSeparationSerializationVisitor : EdmModelVisitor
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x000165AB File Offset: 0x000147AB
		public EdmModelSchemaSeparationSerializationVisitor(IEdmModel visitedModel)
			: base(visitedModel)
		{
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x000165BF File Offset: 0x000147BF
		public IEnumerable<EdmSchema> GetSchemas()
		{
			if (!this.visitCompleted)
			{
				this.Visit();
			}
			return this.modelSchemas.Values;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x000165DA File Offset: 0x000147DA
		protected void Visit()
		{
			base.VisitEdmModel();
			this.visitCompleted = true;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000165E9 File Offset: 0x000147E9
		protected override void ProcessModel(IEdmModel model)
		{
			this.ProcessElement(model);
			base.VisitSchemaElements(model.SchemaElements);
			base.VisitVocabularyAnnotations(Enumerable.Where<IEdmVocabularyAnnotation>(model.VocabularyAnnotations, (IEdmVocabularyAnnotation a) => !a.IsInline(this.Model)));
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001661B File Offset: 0x0001481B
		protected override void ProcessVocabularyAnnotatable(IEdmVocabularyAnnotatable element)
		{
			base.VisitAnnotations(this.Model.DirectValueAnnotations(element));
			base.VisitVocabularyAnnotations(Enumerable.Where<IEdmVocabularyAnnotation>(this.Model.FindDeclaredVocabularyAnnotations(element), (IEdmVocabularyAnnotation a) => a.IsInline(this.Model)));
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00016654 File Offset: 0x00014854
		protected override void ProcessSchemaElement(IEdmSchemaElement element)
		{
			string text = element.Namespace;
			if (EdmUtil.IsNullOrWhiteSpaceInternal(text))
			{
				text = string.Empty;
			}
			EdmSchema edmSchema;
			if (!this.modelSchemas.TryGetValue(text, ref edmSchema))
			{
				edmSchema = new EdmSchema(text);
				this.modelSchemas.Add(text, edmSchema);
			}
			edmSchema.AddSchemaElement(element);
			this.activeSchema = edmSchema;
			base.ProcessSchemaElement(element);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000166B0 File Offset: 0x000148B0
		protected override void ProcessVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			if (!annotation.IsInline(this.Model))
			{
				string text;
				if ((text = annotation.GetSchemaNamespace(this.Model)) == null)
				{
					text = Enumerable.FirstOrDefault<string>(Enumerable.Select<KeyValuePair<string, EdmSchema>, string>(this.modelSchemas, (KeyValuePair<string, EdmSchema> s) => s.Key)) ?? string.Empty;
				}
				string text2 = text;
				EdmSchema edmSchema;
				if (!this.modelSchemas.TryGetValue(text2, ref edmSchema))
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

		// Token: 0x06000835 RID: 2101 RVA: 0x0001676C File Offset: 0x0001496C
		protected override void ProcessEntityContainer(IEdmEntityContainer element)
		{
			string @namespace = element.Namespace;
			EdmSchema edmSchema;
			if (!this.modelSchemas.TryGetValue(@namespace, ref edmSchema))
			{
				edmSchema = new EdmSchema(@namespace);
				this.modelSchemas.Add(edmSchema.Namespace, edmSchema);
			}
			edmSchema.AddEntityContainer(element);
			this.activeSchema = edmSchema;
			base.ProcessEntityContainer(element);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x000167BE File Offset: 0x000149BE
		protected override void ProcessComplexTypeReference(IEdmComplexTypeReference element)
		{
			this.CheckSchemaElementReference(element.ComplexDefinition());
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000167CC File Offset: 0x000149CC
		protected override void ProcessEntityTypeReference(IEdmEntityTypeReference element)
		{
			this.CheckSchemaElementReference(element.EntityDefinition());
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000167DA File Offset: 0x000149DA
		protected override void ProcessEntityReferenceTypeReference(IEdmEntityReferenceTypeReference element)
		{
			this.CheckSchemaElementReference(element.EntityType());
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x000167E8 File Offset: 0x000149E8
		protected override void ProcessEnumTypeReference(IEdmEnumTypeReference element)
		{
			this.CheckSchemaElementReference(element.EnumDefinition());
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000167F6 File Offset: 0x000149F6
		protected override void ProcessTypeDefinitionReference(IEdmTypeDefinitionReference element)
		{
			this.CheckSchemaElementReference(element.TypeDefinition());
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00016804 File Offset: 0x00014A04
		protected override void ProcessEntityType(IEdmEntityType element)
		{
			base.ProcessEntityType(element);
			if (element.BaseEntityType() != null)
			{
				this.CheckSchemaElementReference(element.BaseEntityType());
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00016821 File Offset: 0x00014A21
		protected override void ProcessComplexType(IEdmComplexType element)
		{
			base.ProcessComplexType(element);
			if (element.BaseComplexType() != null)
			{
				this.CheckSchemaElementReference(element.BaseComplexType());
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001683E File Offset: 0x00014A3E
		protected override void ProcessEnumType(IEdmEnumType element)
		{
			base.ProcessEnumType(element);
			this.CheckSchemaElementReference(element.UnderlyingType);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00016853 File Offset: 0x00014A53
		protected override void ProcessTypeDefinition(IEdmTypeDefinition element)
		{
			base.ProcessTypeDefinition(element);
			this.CheckSchemaElementReference(element.UnderlyingType);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00016868 File Offset: 0x00014A68
		private void CheckSchemaElementReference(IEdmSchemaElement element)
		{
			this.CheckSchemaElementReference(element.Namespace);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00016876 File Offset: 0x00014A76
		private void CheckSchemaElementReference(string namespaceName)
		{
			if (this.activeSchema != null)
			{
				this.activeSchema.AddNamespaceUsing(namespaceName);
			}
		}

		// Token: 0x04000555 RID: 1365
		private bool visitCompleted;

		// Token: 0x04000556 RID: 1366
		private Dictionary<string, EdmSchema> modelSchemas = new Dictionary<string, EdmSchema>();

		// Token: 0x04000557 RID: 1367
		private EdmSchema activeSchema;
	}
}
