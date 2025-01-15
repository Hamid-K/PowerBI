using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.OData.Edm.Vocabularies.V1;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000BD RID: 189
	public abstract class EdmModelBase : EdmElement, IEdmModel, IEdmElement
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x0000B7B1 File Offset: 0x000099B1
		protected EdmModelBase(IEnumerable<IEdmModel> referencedModels, IEdmDirectValueAnnotationsManager annotationsManager)
			: this(referencedModels, annotationsManager, true)
		{
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000B7BC File Offset: 0x000099BC
		protected EdmModelBase(IEnumerable<IEdmModel> referencedModels, IEdmDirectValueAnnotationsManager annotationsManager, bool includeDefaultVocabularies)
		{
			EdmUtil.CheckArgumentNull<IEnumerable<IEdmModel>>(referencedModels, "referencedModels");
			EdmUtil.CheckArgumentNull<IEdmDirectValueAnnotationsManager>(annotationsManager, "annotationsManager");
			this.referencedEdmModels = new List<IEdmModel>(referencedModels);
			this.referencedEdmModels.Insert(0, EdmCoreModel.Instance);
			if (includeDefaultVocabularies)
			{
				this.referencedEdmModels.AddRange(VocabularyModelProvider.VocabularyModels);
			}
			this.annotationsManager = annotationsManager;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000481 RID: 1153
		public abstract IEnumerable<IEdmSchemaElement> SchemaElements { get; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000482 RID: 1154
		public abstract IEnumerable<string> DeclaredNamespaces { get; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00002CBB File Offset: 0x00000EBB
		public virtual IEnumerable<IEdmVocabularyAnnotation> VocabularyAnnotations
		{
			get
			{
				return Enumerable.Empty<IEdmVocabularyAnnotation>();
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000B84A File Offset: 0x00009A4A
		public IEnumerable<IEdmModel> ReferencedModels
		{
			get
			{
				return this.referencedEdmModels;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000B852 File Offset: 0x00009A52
		public IEdmDirectValueAnnotationsManager DirectValueAnnotationsManager
		{
			get
			{
				return this.annotationsManager;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000B85A File Offset: 0x00009A5A
		public IEdmEntityContainer EntityContainer
		{
			get
			{
				return this.containersDictionary.Values.FirstOrDefault<IEdmEntityContainer>();
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000B86C File Offset: 0x00009A6C
		public IEdmSchemaType FindDeclaredType(string qualifiedName)
		{
			IEdmSchemaType edmSchemaType;
			this.schemaTypeDictionary.TryGetValue(qualifiedName, out edmSchemaType);
			return edmSchemaType;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000B88C File Offset: 0x00009A8C
		public IEdmTerm FindDeclaredTerm(string qualifiedName)
		{
			IEdmTerm edmTerm;
			this.termDictionary.TryGetValue(qualifiedName, out edmTerm);
			return edmTerm;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000B8AC File Offset: 0x00009AAC
		public IEnumerable<IEdmOperation> FindDeclaredOperations(string qualifiedName)
		{
			IList<IEdmOperation> list;
			if (this.functionDictionary.TryGetValue(qualifiedName, out list))
			{
				return list;
			}
			return Enumerable.Empty<IEdmOperation>();
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000B8D0 File Offset: 0x00009AD0
		public virtual IEnumerable<IEdmOperation> FindDeclaredBoundOperations(IEdmType bindingType)
		{
			Func<IEdmOperation, bool> <>9__0;
			foreach (IEnumerable<IEdmOperation> enumerable in this.functionDictionary.Values.Distinct<IList<IEdmOperation>>())
			{
				IEnumerable<IEdmOperation> enumerable2 = enumerable;
				Func<IEdmOperation, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (IEdmOperation o) => o.HasEquivalentBindingType(bindingType));
				}
				foreach (IEdmOperation edmOperation in enumerable2.Where(func))
				{
					yield return edmOperation;
				}
				IEnumerator<IEdmOperation> enumerator2 = null;
			}
			IEnumerator<IList<IEdmOperation>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		public virtual IEnumerable<IEdmOperation> FindDeclaredBoundOperations(string qualifiedName, IEdmType bindingType)
		{
			IEnumerable<IEdmOperation> enumerable = this.FindDeclaredOperations(qualifiedName);
			IList<IEdmOperation> list = enumerable as IList<IEdmOperation>;
			if (list != null)
			{
				IList<IEdmOperation> list2 = new List<IEdmOperation>();
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i].HasEquivalentBindingType(bindingType))
					{
						list2.Add(list[i]);
					}
				}
				return list2;
			}
			return enumerable.Where((IEdmOperation o) => o.HasEquivalentBindingType(bindingType));
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00002CBB File Offset: 0x00000EBB
		public virtual IEnumerable<IEdmVocabularyAnnotation> FindDeclaredVocabularyAnnotations(IEdmVocabularyAnnotatable element)
		{
			return Enumerable.Empty<IEdmVocabularyAnnotation>();
		}

		// Token: 0x0600048D RID: 1165
		public abstract IEnumerable<IEdmStructuredType> FindDirectlyDerivedTypes(IEdmStructuredType baseType);

		// Token: 0x0600048E RID: 1166 RVA: 0x0000B966 File Offset: 0x00009B66
		protected void RegisterElement(IEdmSchemaElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmSchemaElement>(element, "element");
			RegistrationHelper.RegisterSchemaElement(element, this.schemaTypeDictionary, this.termDictionary, this.functionDictionary, this.containersDictionary);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000B992 File Offset: 0x00009B92
		protected void AddReferencedModel(IEdmModel model)
		{
			EdmUtil.CheckArgumentNull<IEdmModel>(model, "model");
			this.referencedEdmModels.Add(model);
		}

		// Token: 0x04000164 RID: 356
		private readonly List<IEdmModel> referencedEdmModels;

		// Token: 0x04000165 RID: 357
		private readonly IEdmDirectValueAnnotationsManager annotationsManager;

		// Token: 0x04000166 RID: 358
		private readonly Dictionary<string, IEdmEntityContainer> containersDictionary = new Dictionary<string, IEdmEntityContainer>();

		// Token: 0x04000167 RID: 359
		private readonly Dictionary<string, IEdmSchemaType> schemaTypeDictionary = new Dictionary<string, IEdmSchemaType>();

		// Token: 0x04000168 RID: 360
		private readonly Dictionary<string, IEdmTerm> termDictionary = new Dictionary<string, IEdmTerm>();

		// Token: 0x04000169 RID: 361
		private readonly Dictionary<string, IList<IEdmOperation>> functionDictionary = new Dictionary<string, IList<IEdmOperation>>();
	}
}
