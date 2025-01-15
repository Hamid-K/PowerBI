using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;

namespace Microsoft.OData.Edm.Csdl.Serialization
{
	// Token: 0x020000D7 RID: 215
	internal class EdmModelSchemaSeparationSerializationVisitor : EdmModelVisitor
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x0000A531 File Offset: 0x00008731
		public EdmModelSchemaSeparationSerializationVisitor(IEdmModel visitedModel)
			: base(visitedModel)
		{
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000A545 File Offset: 0x00008745
		public IEnumerable<EdmSchema> GetSchemas()
		{
			if (!this.visitCompleted)
			{
				this.Visit();
			}
			return this.modelSchemas.Values;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000A560 File Offset: 0x00008760
		protected void Visit()
		{
			base.VisitEdmModel();
			this.visitCompleted = true;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000A580 File Offset: 0x00008780
		protected override void ProcessModel(IEdmModel model)
		{
			this.ProcessElement(model);
			base.VisitSchemaElements(model.SchemaElements);
			base.VisitVocabularyAnnotations(Enumerable.Where<IEdmVocabularyAnnotation>(model.VocabularyAnnotations, (IEdmVocabularyAnnotation a) => !a.IsInline(this.Model)));
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000A5C0 File Offset: 0x000087C0
		protected override void ProcessVocabularyAnnotatable(IEdmVocabularyAnnotatable element)
		{
			base.VisitAnnotations(this.Model.DirectValueAnnotations(element));
			base.VisitVocabularyAnnotations(Enumerable.Where<IEdmVocabularyAnnotation>(this.Model.FindDeclaredVocabularyAnnotations(element), (IEdmVocabularyAnnotation a) => a.IsInline(this.Model)));
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000A5F8 File Offset: 0x000087F8
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

		// Token: 0x06000439 RID: 1081 RVA: 0x0000A65C File Offset: 0x0000885C
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

		// Token: 0x0600043A RID: 1082 RVA: 0x0000A710 File Offset: 0x00008910
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

		// Token: 0x0600043B RID: 1083 RVA: 0x0000A762 File Offset: 0x00008962
		protected override void ProcessComplexTypeReference(IEdmComplexTypeReference element)
		{
			this.CheckSchemaElementReference(element.ComplexDefinition());
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000A770 File Offset: 0x00008970
		protected override void ProcessEntityTypeReference(IEdmEntityTypeReference element)
		{
			this.CheckSchemaElementReference(element.EntityDefinition());
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000A77E File Offset: 0x0000897E
		protected override void ProcessEntityReferenceTypeReference(IEdmEntityReferenceTypeReference element)
		{
			this.CheckSchemaElementReference(element.EntityType());
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000A78C File Offset: 0x0000898C
		protected override void ProcessEnumTypeReference(IEdmEnumTypeReference element)
		{
			this.CheckSchemaElementReference(element.EnumDefinition());
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000A79A File Offset: 0x0000899A
		protected override void ProcessTypeDefinitionReference(IEdmTypeDefinitionReference element)
		{
			this.CheckSchemaElementReference(element.TypeDefinition());
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000A7A8 File Offset: 0x000089A8
		protected override void ProcessEntityType(IEdmEntityType element)
		{
			base.ProcessEntityType(element);
			if (element.BaseEntityType() != null)
			{
				this.CheckSchemaElementReference(element.BaseEntityType());
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000A7C5 File Offset: 0x000089C5
		protected override void ProcessComplexType(IEdmComplexType element)
		{
			base.ProcessComplexType(element);
			if (element.BaseComplexType() != null)
			{
				this.CheckSchemaElementReference(element.BaseComplexType());
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000A7E2 File Offset: 0x000089E2
		protected override void ProcessEnumType(IEdmEnumType element)
		{
			base.ProcessEnumType(element);
			this.CheckSchemaElementReference(element.UnderlyingType);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000A7F7 File Offset: 0x000089F7
		protected override void ProcessTypeDefinition(IEdmTypeDefinition element)
		{
			base.ProcessTypeDefinition(element);
			this.CheckSchemaElementReference(element.UnderlyingType);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000A80C File Offset: 0x00008A0C
		private void CheckSchemaElementReference(IEdmSchemaElement element)
		{
			this.CheckSchemaElementReference(element.Namespace);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000A81A File Offset: 0x00008A1A
		private void CheckSchemaElementReference(string namespaceName)
		{
			if (this.activeSchema != null)
			{
				this.activeSchema.AddNamespaceUsing(namespaceName);
			}
		}

		// Token: 0x040001A2 RID: 418
		private bool visitCompleted;

		// Token: 0x040001A3 RID: 419
		private Dictionary<string, EdmSchema> modelSchemas = new Dictionary<string, EdmSchema>();

		// Token: 0x040001A4 RID: 420
		private EdmSchema activeSchema;
	}
}
