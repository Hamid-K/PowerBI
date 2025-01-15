using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.Edm.Vocabularies.Community.V1;
using Microsoft.OData.Edm.Vocabularies.V1;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000064 RID: 100
	public abstract class EdmModelBase : EdmElement, IEdmModel, IEdmElement
	{
		// Token: 0x0600038E RID: 910 RVA: 0x0000B258 File Offset: 0x00009458
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

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600038F RID: 911
		public abstract IEnumerable<IEdmSchemaElement> SchemaElements { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000390 RID: 912
		public abstract IEnumerable<string> DeclaredNamespaces { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000A305 File Offset: 0x00008505
		public virtual IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations
		{
			get
			{
				return Enumerable.Empty<IEdmVocabularyAnnotation>();
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000B31E File Offset: 0x0000951E
		public IEnumerable<IEdmModel> ReferencedModels
		{
			get
			{
				return this.referencedEdmModels;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000B326 File Offset: 0x00009526
		public IEdmDirectValueAnnotationsManager DirectValueAnnotationsManager
		{
			get
			{
				return this.annotationsManager;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000394 RID: 916 RVA: 0x0000B32E File Offset: 0x0000952E
		public IEdmEntityContainer EntityContainer
		{
			get
			{
				return Enumerable.FirstOrDefault<IEdmEntityContainer>(this.containersDictionary.Values);
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000B340 File Offset: 0x00009540
		public IEdmSchemaType FindDeclaredType(string qualifiedName)
		{
			IEdmSchemaType edmSchemaType;
			this.schemaTypeDictionary.TryGetValue(qualifiedName, ref edmSchemaType);
			return edmSchemaType;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000B360 File Offset: 0x00009560
		public IEdmTerm FindDeclaredTerm(string qualifiedName)
		{
			IEdmTerm edmTerm;
			this.termDictionary.TryGetValue(qualifiedName, ref edmTerm);
			return edmTerm;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000B380 File Offset: 0x00009580
		public IEnumerable<IEdmOperation> FindDeclaredOperations(string qualifiedName)
		{
			IList<IEdmOperation> list;
			if (this.functionDictionary.TryGetValue(qualifiedName, ref list))
			{
				return list;
			}
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000B3A4 File Offset: 0x000095A4
		public virtual IEnumerable<IEdmOperation> FindDeclaredBoundOperations(IEdmType bindingType)
		{
			Func<IEdmOperation, bool> <>9__0;
			foreach (IEnumerable<IEdmOperation> enumerable in Enumerable.Distinct<IList<IEdmOperation>>(this.functionDictionary.Values))
			{
				IEnumerable<IEdmOperation> enumerable2 = enumerable;
				Func<IEdmOperation, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (IEdmOperation o) => o.IsBound && Enumerable.Any<IEdmOperationParameter>(o.Parameters) && o.HasEquivalentBindingType(bindingType));
				}
				foreach (IEdmOperation edmOperation in Enumerable.Where<IEdmOperation>(enumerable2, func))
				{
					yield return edmOperation;
				}
				IEnumerator<IEdmOperation> enumerator2 = null;
			}
			IEnumerator<IList<IEdmOperation>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000B3BC File Offset: 0x000095BC
		public virtual IEnumerable<IEdmOperation> FindDeclaredBoundOperations(string qualifiedName, IEdmType bindingType)
		{
			return Enumerable.Where<IEdmOperation>(this.FindDeclaredOperations(qualifiedName), (IEdmOperation o) => o.IsBound && Enumerable.Any<IEdmOperationParameter>(o.Parameters) && o.HasEquivalentBindingType(bindingType));
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000A305 File Offset: 0x00008505
		public virtual IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			return Enumerable.Empty<IEdmVocabularyAnnotation>();
		}

		// Token: 0x0600039B RID: 923
		public abstract IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType);

		// Token: 0x0600039C RID: 924 RVA: 0x0000B3EE File Offset: 0x000095EE
		protected void RegisterElement(IEdmSchemaElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmSchemaElement>(element, "element");
			RegistrationHelper.RegisterSchemaElement(element, this.schemaTypeDictionary, this.termDictionary, this.functionDictionary, this.containersDictionary);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000B41A File Offset: 0x0000961A
		protected void AddReferencedModel(IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			this.referencedEdmModels.Add(model);
		}

		// Token: 0x040000CF RID: 207
		private readonly List<IEdmModel> referencedEdmModels;

		// Token: 0x040000D0 RID: 208
		private readonly IEdmDirectValueAnnotationsManager annotationsManager;

		// Token: 0x040000D1 RID: 209
		private readonly Dictionary<string, IEdmEntityContainer> containersDictionary = new Dictionary<string, IEdmEntityContainer>();

		// Token: 0x040000D2 RID: 210
		private readonly Dictionary<string, IEdmSchemaType> schemaTypeDictionary = new Dictionary<string, IEdmSchemaType>();

		// Token: 0x040000D3 RID: 211
		private readonly Dictionary<string, IEdmTerm> termDictionary = new Dictionary<string, IEdmTerm>();

		// Token: 0x040000D4 RID: 212
		private readonly Dictionary<string, IList<IEdmOperation>> functionDictionary = new Dictionary<string, IList<IEdmOperation>>();
	}
}
