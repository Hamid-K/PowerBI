using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Library.Annotations;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000201 RID: 513
	public class EdmModel : EdmModelBase
	{
		// Token: 0x06000C10 RID: 3088 RVA: 0x00022161 File Offset: 0x00020361
		public EdmModel()
			: base(Enumerable.Empty<IEdmModel>(), new EdmDirectValueAnnotationsManager())
		{
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0002219F File Offset: 0x0002039F
		public override IEnumerable<IEdmSchemaElement> SchemaElements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x000221A7 File Offset: 0x000203A7
		public override IEnumerable<string> DeclaredNamespaces
		{
			get
			{
				return this.declaredNamespaces;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x000221B8 File Offset: 0x000203B8
		public override IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations
		{
			get
			{
				return Enumerable.SelectMany<KeyValuePair<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>>, IEdmVocabularyAnnotation>(this.vocabularyAnnotationsDictionary, (KeyValuePair<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>> kvp) => kvp.Value);
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x000221E2 File Offset: 0x000203E2
		public new void AddReferencedModel(IEdmModel model)
		{
			base.AddReferencedModel(model);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x000221EC File Offset: 0x000203EC
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

		// Token: 0x06000C16 RID: 3094 RVA: 0x00022284 File Offset: 0x00020484
		public void AddElements(IEnumerable<IEdmSchemaElement> newElements)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmSchemaElement>>(newElements, "newElements");
			foreach (IEdmSchemaElement edmSchemaElement in newElements)
			{
				this.AddElement(edmSchemaElement);
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x000222D8 File Offset: 0x000204D8
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

		// Token: 0x06000C18 RID: 3096 RVA: 0x00022338 File Offset: 0x00020538
		public override IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			List<IEdmVocabularyAnnotation> list;
			if (!this.vocabularyAnnotationsDictionary.TryGetValue(element, ref list))
			{
				return Enumerable.Empty<IEdmVocabularyAnnotation>();
			}
			return list;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002235C File Offset: 0x0002055C
		public override IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType)
		{
			List<IEdmStructuredType> list;
			if (this.derivedTypeMappings.TryGetValue(baseType, ref list))
			{
				return list;
			}
			return Enumerable.Empty<IEdmStructuredType>();
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x000223AC File Offset: 0x000205AC
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

		// Token: 0x04000583 RID: 1411
		private readonly List<IEdmSchemaElement> elements = new List<IEdmSchemaElement>();

		// Token: 0x04000584 RID: 1412
		private readonly Dictionary<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>> vocabularyAnnotationsDictionary = new Dictionary<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>>();

		// Token: 0x04000585 RID: 1413
		private readonly Dictionary<IEdmStructuredType, List<IEdmStructuredType>> derivedTypeMappings = new Dictionary<IEdmStructuredType, List<IEdmStructuredType>>();

		// Token: 0x04000586 RID: 1414
		private readonly HashSet<string> declaredNamespaces = new HashSet<string>();
	}
}
