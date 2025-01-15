using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000063 RID: 99
	public class EdmModel : EdmModelBase
	{
		// Token: 0x06000383 RID: 899 RVA: 0x0000AFA1 File Offset: 0x000091A1
		public EdmModel()
			: base(Enumerable.Empty<IEdmModel>(), new EdmDirectValueAnnotationsManager())
		{
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000AFDF File Offset: 0x000091DF
		public override IEnumerable<IEdmSchemaElement> SchemaElements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000AFE7 File Offset: 0x000091E7
		public override IEnumerable<string> DeclaredNamespaces
		{
			get
			{
				return this.declaredNamespaces;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000AFEF File Offset: 0x000091EF
		public override IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations
		{
			get
			{
				return Enumerable.SelectMany<KeyValuePair<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>>, IEdmVocabularyAnnotation>(this.vocabularyAnnotationsDictionary, (KeyValuePair<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>> kvp) => kvp.Value);
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000B01B File Offset: 0x0000921B
		public new void AddReferencedModel(IEdmModel model)
		{
			base.AddReferencedModel(model);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000B024 File Offset: 0x00009224
		public void AddElement(IEdmSchemaElement element)
		{
			if (!this.declaredNamespaces.Contains(element.Namespace))
			{
				this.declaredNamespaces.Add(element.Namespace);
			}
			EdmUtil.CheckArgumentNull<IEdmSchemaElement>(element, "element");
			this.elements.Add(element);
			IEdmStructuredType edmStructuredType = element as IEdmStructuredType;
			if (edmStructuredType != null && edmStructuredType.BaseType != null)
			{
				List<IEdmStructuredType> list;
				if (!this.derivedTypeMappings.TryGetValue(edmStructuredType.BaseType, ref list))
				{
					list = new List<IEdmStructuredType>();
					this.derivedTypeMappings[edmStructuredType.BaseType] = list;
				}
				list.Add(edmStructuredType);
			}
			base.RegisterElement(element);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000B0BC File Offset: 0x000092BC
		public void AddElements(IEnumerable<IEdmSchemaElement> newElements)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmSchemaElement>>(newElements, "newElements");
			foreach (IEdmSchemaElement edmSchemaElement in newElements)
			{
				this.AddElement(edmSchemaElement);
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000B110 File Offset: 0x00009310
		public void AddVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			if (annotation.Target == null)
			{
				throw new InvalidOperationException(Strings.Constructable_VocabularyAnnotationMustHaveTarget);
			}
			List<IEdmVocabularyAnnotation> list;
			if (!this.vocabularyAnnotationsDictionary.TryGetValue(annotation.Target, ref list))
			{
				list = new List<IEdmVocabularyAnnotation>();
				this.vocabularyAnnotationsDictionary.Add(annotation.Target, list);
			}
			list.Add(annotation);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000B170 File Offset: 0x00009370
		public override IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			List<IEdmVocabularyAnnotation> list;
			if (!this.vocabularyAnnotationsDictionary.TryGetValue(element, ref list))
			{
				return Enumerable.Empty<IEdmVocabularyAnnotation>();
			}
			return list;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000B198 File Offset: 0x00009398
		public override IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType)
		{
			List<IEdmStructuredType> list;
			if (this.derivedTypeMappings.TryGetValue(baseType, ref list))
			{
				return list;
			}
			return Enumerable.Empty<IEdmStructuredType>();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000B1BC File Offset: 0x000093BC
		public void SetVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			if (annotation.Target == null)
			{
				throw new InvalidOperationException(Strings.Constructable_VocabularyAnnotationMustHaveTarget);
			}
			List<IEdmVocabularyAnnotation> list;
			if (!this.vocabularyAnnotationsDictionary.TryGetValue(annotation.Target, ref list))
			{
				list = new List<IEdmVocabularyAnnotation>();
				this.vocabularyAnnotationsDictionary.Add(annotation.Target, list);
			}
			list.RemoveAll((IEdmVocabularyAnnotation p) => p.Term.FullName() == annotation.Term.FullName());
			list.Add(annotation);
		}

		// Token: 0x040000CB RID: 203
		private readonly List<IEdmSchemaElement> elements = new List<IEdmSchemaElement>();

		// Token: 0x040000CC RID: 204
		private readonly Dictionary<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>> vocabularyAnnotationsDictionary = new Dictionary<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>>();

		// Token: 0x040000CD RID: 205
		private readonly Dictionary<IEdmStructuredType, List<IEdmStructuredType>> derivedTypeMappings = new Dictionary<IEdmStructuredType, List<IEdmStructuredType>>();

		// Token: 0x040000CE RID: 206
		private readonly HashSet<string> declaredNamespaces = new HashSet<string>();
	}
}
