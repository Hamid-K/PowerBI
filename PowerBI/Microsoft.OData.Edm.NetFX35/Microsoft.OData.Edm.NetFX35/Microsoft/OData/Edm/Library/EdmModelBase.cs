using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Vocabularies.Community.V1;
using Microsoft.OData.Edm.Vocabularies.V1;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001B0 RID: 432
	public abstract class EdmModelBase : EdmElement, IEdmModel, IEdmElement
	{
		// Token: 0x060008F0 RID: 2288 RVA: 0x0001705C File Offset: 0x0001525C
		protected EdmModelBase(IEnumerable<IEdmModel> referencedModels, IEdmDirectValueAnnotationsManager annotationsManager)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmModel>>(referencedModels, "referencedModels");
			EdmUtil.CheckArgumentNull<IEdmDirectValueAnnotationsManager>(annotationsManager, "annotationsManager");
			this.referencedEdmModels = new List<IEdmModel>(referencedModels);
			this.referencedEdmModels.Add(EdmCoreModel.Instance);
			if (CoreVocabularyModel.Instance != null)
			{
				this.referencedEdmModels.Add(CoreVocabularyModel.Instance);
			}
			if (!CoreVocabularyModel.IsInitializing && CapabilitiesVocabularyModel.Instance != null)
			{
				this.referencedEdmModels.Add(CapabilitiesVocabularyModel.Instance);
			}
			if (AlternateKeysVocabularyModel.Instance != null)
			{
				this.referencedEdmModels.Add(AlternateKeysVocabularyModel.Instance);
			}
			this.annotationsManager = annotationsManager;
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060008F1 RID: 2289
		public abstract IEnumerable<IEdmSchemaElement> SchemaElements { get; }

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060008F2 RID: 2290
		public abstract IEnumerable<string> DeclaredNamespaces { get; }

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00017122 File Offset: 0x00015322
		public virtual IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations
		{
			get
			{
				return Enumerable.Empty<IEdmVocabularyAnnotation>();
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x00017129 File Offset: 0x00015329
		public IEnumerable<IEdmModel> ReferencedModels
		{
			get
			{
				return this.referencedEdmModels;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00017131 File Offset: 0x00015331
		public IEdmDirectValueAnnotationsManager DirectValueAnnotationsManager
		{
			get
			{
				return this.annotationsManager;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00017139 File Offset: 0x00015339
		public IEdmEntityContainer EntityContainer
		{
			get
			{
				return Enumerable.FirstOrDefault<IEdmEntityContainer>(this.containersDictionary.Values);
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001714C File Offset: 0x0001534C
		public IEdmSchemaType FindDeclaredType(string qualifiedName)
		{
			IEdmSchemaType edmSchemaType;
			this.schemaTypeDictionary.TryGetValue(qualifiedName, ref edmSchemaType);
			return edmSchemaType;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x0001716C File Offset: 0x0001536C
		public IEdmValueTerm FindDeclaredValueTerm(string qualifiedName)
		{
			IEdmValueTerm edmValueTerm;
			this.valueTermDictionary.TryGetValue(qualifiedName, ref edmValueTerm);
			return edmValueTerm;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0001718C File Offset: 0x0001538C
		public IEnumerable<IEdmOperation> FindDeclaredOperations(string qualifiedName)
		{
			IList<IEdmOperation> list;
			if (this.functionDictionary.TryGetValue(qualifiedName, ref list))
			{
				return list;
			}
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001746C File Offset: 0x0001566C
		public virtual IEnumerable<IEdmOperation> FindDeclaredBoundOperations(IEdmType bindingType)
		{
			foreach (IEnumerable<IEdmOperation> operations in Enumerable.Distinct<IList<IEdmOperation>>(this.functionDictionary.Values))
			{
				foreach (IEdmOperation operation in Enumerable.Where<IEdmOperation>(operations, (IEdmOperation o) => o.IsBound && Enumerable.Any<IEdmOperationParameter>(o.Parameters) && o.HasEquivalentBindingType(bindingType)))
				{
					yield return operation;
				}
			}
			yield break;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x000174C0 File Offset: 0x000156C0
		public virtual IEnumerable<IEdmOperation> FindDeclaredBoundOperations(string qualifiedName, IEdmType bindingType)
		{
			return Enumerable.Where<IEdmOperation>(this.FindDeclaredOperations(qualifiedName), (IEdmOperation o) => o.IsBound && Enumerable.Any<IEdmOperationParameter>(o.Parameters) && o.HasEquivalentBindingType(bindingType));
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x000174F2 File Offset: 0x000156F2
		public virtual IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			return Enumerable.Empty<IEdmVocabularyAnnotation>();
		}

		// Token: 0x060008FD RID: 2301
		public abstract IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType);

		// Token: 0x060008FE RID: 2302 RVA: 0x000174F9 File Offset: 0x000156F9
		protected void RegisterElement(IEdmSchemaElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmSchemaElement>(element, "element");
			RegistrationHelper.RegisterSchemaElement(element, this.schemaTypeDictionary, this.valueTermDictionary, this.functionDictionary, this.containersDictionary);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00017525 File Offset: 0x00015725
		protected void AddReferencedModel(IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			this.referencedEdmModels.Add(model);
		}

		// Token: 0x04000465 RID: 1125
		private readonly List<IEdmModel> referencedEdmModels;

		// Token: 0x04000466 RID: 1126
		private readonly IEdmDirectValueAnnotationsManager annotationsManager;

		// Token: 0x04000467 RID: 1127
		private readonly Dictionary<string, IEdmEntityContainer> containersDictionary = new Dictionary<string, IEdmEntityContainer>();

		// Token: 0x04000468 RID: 1128
		private readonly Dictionary<string, IEdmSchemaType> schemaTypeDictionary = new Dictionary<string, IEdmSchemaType>();

		// Token: 0x04000469 RID: 1129
		private readonly Dictionary<string, IEdmValueTerm> valueTermDictionary = new Dictionary<string, IEdmValueTerm>();

		// Token: 0x0400046A RID: 1130
		private readonly Dictionary<string, IList<IEdmOperation>> functionDictionary = new Dictionary<string, IList<IEdmOperation>>();
	}
}
