using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Annotations;

namespace Microsoft.Data.Edm.Csdl.Internal.Serialization
{
	// Token: 0x020000C0 RID: 192
	internal class EdmModelSchemaSeparationSerializationVisitor : EdmModelVisitor
	{
		// Token: 0x060003BD RID: 957 RVA: 0x00009790 File Offset: 0x00007990
		public EdmModelSchemaSeparationSerializationVisitor(IEdmModel visitedModel)
			: base(visitedModel)
		{
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000097A4 File Offset: 0x000079A4
		public IEnumerable<EdmSchema> GetSchemas()
		{
			if (!this.visitCompleted)
			{
				this.Visit();
			}
			return this.modelSchemas.Values;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000097BF File Offset: 0x000079BF
		protected void Visit()
		{
			base.VisitEdmModel();
			this.visitCompleted = true;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000097DF File Offset: 0x000079DF
		protected override void ProcessModel(IEdmModel model)
		{
			this.ProcessElement(model);
			base.VisitSchemaElements(model.SchemaElements);
			base.VisitVocabularyAnnotations(Enumerable.Where<IEdmVocabularyAnnotation>(model.VocabularyAnnotations, (IEdmVocabularyAnnotation a) => !a.IsInline(this.Model)));
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000981F File Offset: 0x00007A1F
		protected override void ProcessVocabularyAnnotatable(IEdmVocabularyAnnotatable element)
		{
			base.VisitAnnotations(this.Model.DirectValueAnnotations(element));
			base.VisitVocabularyAnnotations(Enumerable.Where<IEdmVocabularyAnnotation>(this.Model.FindDeclaredVocabularyAnnotations(element), (IEdmVocabularyAnnotation a) => a.IsInline(this.Model)));
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00009858 File Offset: 0x00007A58
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

		// Token: 0x060003C3 RID: 963 RVA: 0x000098BC File Offset: 0x00007ABC
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

		// Token: 0x060003C4 RID: 964 RVA: 0x00009970 File Offset: 0x00007B70
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

		// Token: 0x060003C5 RID: 965 RVA: 0x000099C2 File Offset: 0x00007BC2
		protected override void ProcessComplexTypeReference(IEdmComplexTypeReference element)
		{
			this.CheckSchemaElementReference(element.ComplexDefinition());
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000099D0 File Offset: 0x00007BD0
		protected override void ProcessEntityTypeReference(IEdmEntityTypeReference element)
		{
			this.CheckSchemaElementReference(element.EntityDefinition());
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000099DE File Offset: 0x00007BDE
		protected override void ProcessEntityReferenceTypeReference(IEdmEntityReferenceTypeReference element)
		{
			this.CheckSchemaElementReference(element.EntityType());
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000099EC File Offset: 0x00007BEC
		protected override void ProcessEnumTypeReference(IEdmEnumTypeReference element)
		{
			this.CheckSchemaElementReference(element.EnumDefinition());
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x000099FA File Offset: 0x00007BFA
		protected override void ProcessEntityType(IEdmEntityType element)
		{
			base.ProcessEntityType(element);
			if (element.BaseEntityType() != null)
			{
				this.CheckSchemaElementReference(element.BaseEntityType());
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00009A17 File Offset: 0x00007C17
		protected override void ProcessComplexType(IEdmComplexType element)
		{
			base.ProcessComplexType(element);
			if (element.BaseComplexType() != null)
			{
				this.CheckSchemaElementReference(element.BaseComplexType());
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00009A34 File Offset: 0x00007C34
		protected override void ProcessEnumType(IEdmEnumType element)
		{
			base.ProcessEnumType(element);
			this.CheckSchemaElementReference(element.UnderlyingType);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00009A4C File Offset: 0x00007C4C
		protected override void ProcessNavigationProperty(IEdmNavigationProperty property)
		{
			string associationNamespace = this.Model.GetAssociationNamespace(property);
			EdmSchema edmSchema;
			if (!this.modelSchemas.TryGetValue(associationNamespace, ref edmSchema))
			{
				edmSchema = new EdmSchema(associationNamespace);
				this.modelSchemas.Add(edmSchema.Namespace, edmSchema);
			}
			edmSchema.AddAssociatedNavigationProperty(property);
			edmSchema.AddNamespaceUsing(property.DeclaringEntityType().Namespace);
			edmSchema.AddNamespaceUsing(property.Partner.DeclaringEntityType().Namespace);
			this.activeSchema.AddNamespaceUsing(associationNamespace);
			base.ProcessNavigationProperty(property);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00009AD0 File Offset: 0x00007CD0
		private void CheckSchemaElementReference(IEdmSchemaElement element)
		{
			this.CheckSchemaElementReference(element.Namespace);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00009ADE File Offset: 0x00007CDE
		private void CheckSchemaElementReference(string namespaceName)
		{
			if (this.activeSchema != null)
			{
				this.activeSchema.AddNamespaceUsing(namespaceName);
			}
		}

		// Token: 0x0400017B RID: 379
		private bool visitCompleted;

		// Token: 0x0400017C RID: 380
		private Dictionary<string, EdmSchema> modelSchemas = new Dictionary<string, EdmSchema>();

		// Token: 0x0400017D RID: 381
		private EdmSchema activeSchema;
	}
}
