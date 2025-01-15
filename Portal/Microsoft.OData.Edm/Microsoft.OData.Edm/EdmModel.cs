using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000BC RID: 188
	public class EdmModel : EdmModelBase
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x0000B4F2 File Offset: 0x000096F2
		public EdmModel()
			: this(true)
		{
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000B4FB File Offset: 0x000096FB
		public EdmModel(bool includeDefaultVocabularies)
			: base(Enumerable.Empty<IEdmModel>(), new EdmDirectValueAnnotationsManager(), includeDefaultVocabularies)
		{
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000B53A File Offset: 0x0000973A
		public override IEnumerable<IEdmSchemaElement> SchemaElements
		{
			get
			{
				return this.elements;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000B542 File Offset: 0x00009742
		public override IEnumerable<string> DeclaredNamespaces
		{
			get
			{
				return this.declaredNamespaces;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000B54A File Offset: 0x0000974A
		public override IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations
		{
			get
			{
				return this.vocabularyAnnotationsDictionary.SelectMany((KeyValuePair<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>> kvp) => kvp.Value);
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000B576 File Offset: 0x00009776
		public new void AddReferencedModel(IEdmModel model)
		{
			base.AddReferencedModel(model);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000B580 File Offset: 0x00009780
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
				if (!this.derivedTypeMappings.TryGetValue(edmStructuredType.BaseType, out list))
				{
					list = new List<IEdmStructuredType>();
					this.derivedTypeMappings[edmStructuredType.BaseType] = list;
				}
				list.Add(edmStructuredType);
			}
			base.RegisterElement(element);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000B618 File Offset: 0x00009818
		public void AddElements(IEnumerable<IEdmSchemaElement> newElements)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmSchemaElement>>(newElements, "newElements");
			foreach (IEdmSchemaElement edmSchemaElement in newElements)
			{
				this.AddElement(edmSchemaElement);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000B66C File Offset: 0x0000986C
		public void AddVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			if (annotation.Target == null)
			{
				throw new InvalidOperationException(Strings.Constructable_VocabularyAnnotationMustHaveTarget);
			}
			List<IEdmVocabularyAnnotation> list;
			if (!this.vocabularyAnnotationsDictionary.TryGetValue(annotation.Target, out list))
			{
				list = new List<IEdmVocabularyAnnotation>();
				this.vocabularyAnnotationsDictionary.Add(annotation.Target, list);
			}
			list.Add(annotation);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000B6CC File Offset: 0x000098CC
		public override IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			List<IEdmVocabularyAnnotation> list;
			if (!this.vocabularyAnnotationsDictionary.TryGetValue(element, out list))
			{
				return Enumerable.Empty<IEdmVocabularyAnnotation>();
			}
			return list;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000B6F4 File Offset: 0x000098F4
		public override IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType)
		{
			List<IEdmStructuredType> list;
			if (this.derivedTypeMappings.TryGetValue(baseType, out list))
			{
				return list;
			}
			return Enumerable.Empty<IEdmStructuredType>();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000B718 File Offset: 0x00009918
		public void SetVocabularyAnnotation(IEdmVocabularyAnnotation annotation)
		{
			EdmUtil.CheckArgumentNull<IEdmVocabularyAnnotation>(annotation, "annotation");
			if (annotation.Target == null)
			{
				throw new InvalidOperationException(Strings.Constructable_VocabularyAnnotationMustHaveTarget);
			}
			List<IEdmVocabularyAnnotation> list;
			if (!this.vocabularyAnnotationsDictionary.TryGetValue(annotation.Target, out list))
			{
				list = new List<IEdmVocabularyAnnotation>();
				this.vocabularyAnnotationsDictionary.Add(annotation.Target, list);
			}
			list.RemoveAll((IEdmVocabularyAnnotation p) => p.Term.FullName() == annotation.Term.FullName());
			list.Add(annotation);
		}

		// Token: 0x04000160 RID: 352
		private readonly List<IEdmSchemaElement> elements = new List<IEdmSchemaElement>();

		// Token: 0x04000161 RID: 353
		private readonly Dictionary<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>> vocabularyAnnotationsDictionary = new Dictionary<IEdmVocabularyAnnotatable, List<IEdmVocabularyAnnotation>>();

		// Token: 0x04000162 RID: 354
		private readonly Dictionary<IEdmStructuredType, List<IEdmStructuredType>> derivedTypeMappings = new Dictionary<IEdmStructuredType, List<IEdmStructuredType>>();

		// Token: 0x04000163 RID: 355
		private readonly HashSet<string> declaredNamespaces = new HashSet<string>();
	}
}
