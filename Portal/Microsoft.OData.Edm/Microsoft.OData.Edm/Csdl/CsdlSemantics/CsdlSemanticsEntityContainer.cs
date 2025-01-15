using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000199 RID: 409
	internal class CsdlSemanticsEntityContainer : CsdlSemanticsElement, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmCheckable, IEdmFullNamedElement
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0001E58C File Offset: 0x0001C78C
		public CsdlSemanticsEntityContainer(CsdlSemanticsSchema context, CsdlEntityContainer entityContainer)
			: base(entityContainer)
		{
			this.context = context;
			this.entityContainer = entityContainer;
			CsdlSemanticsSchema csdlSemanticsSchema = this.context;
			string text = ((csdlSemanticsSchema != null) ? csdlSemanticsSchema.Namespace : null);
			CsdlEntityContainer csdlEntityContainer = this.entityContainer;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(text, (csdlEntityContainer != null) ? csdlEntityContainer.Name : null);
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x000039FB File Offset: 0x00001BFB
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0001E61F File Offset: 0x0001C81F
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0001E62C File Offset: 0x0001C82C
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return this.elementsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeElementsFunc, null);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0001E640 File Offset: 0x0001C840
		public string Namespace
		{
			get
			{
				return this.context.Namespace;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0001E64D File Offset: 0x0001C84D
		public string Name
		{
			get
			{
				return this.entityContainer.Name;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0001E65A File Offset: 0x0001C85A
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0001E662 File Offset: 0x0001C862
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0001E676 File Offset: 0x0001C876
		public override CsdlElement Element
		{
			get
			{
				return this.entityContainer;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0001E67E File Offset: 0x0001C87E
		internal CsdlSemanticsSchema Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0001E686 File Offset: 0x0001C886
		internal IEdmEntityContainer Extends
		{
			get
			{
				return this.extendsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeExtendsFunc, CsdlSemanticsEntityContainer.OnCycleExtendsFunc);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0001E69E File Offset: 0x0001C89E
		private Dictionary<string, IEdmEntitySet> EntitySetDictionary
		{
			get
			{
				return this.entitySetDictionaryCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeEntitySetDictionaryFunc, null);
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0001E6B2 File Offset: 0x0001C8B2
		private Dictionary<string, IEdmSingleton> SingletonDictionary
		{
			get
			{
				return this.singletonDictionaryCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeSingletonDictionaryFunc, null);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0001E6C6 File Offset: 0x0001C8C6
		private Dictionary<string, object> OperationImportsDictionary
		{
			get
			{
				return this.operationImportsDictionaryCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeOperationImportsDictionaryFunc, null);
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0001E6DC File Offset: 0x0001C8DC
		public IEdmEntitySet FindEntitySet(string name)
		{
			IEdmEntitySet edmEntitySet;
			if (!this.EntitySetDictionary.TryGetValue(name, out edmEntitySet))
			{
				return null;
			}
			return edmEntitySet;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0001E6FC File Offset: 0x0001C8FC
		public IEdmSingleton FindSingleton(string name)
		{
			IEdmSingleton edmSingleton;
			if (!this.SingletonDictionary.TryGetValue(name, out edmSingleton))
			{
				return null;
			}
			return edmSingleton;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0001E71C File Offset: 0x0001C91C
		public IEnumerable<IEdmOperationImport> FindOperationImports(string operationName)
		{
			object obj;
			if (!this.OperationImportsDictionary.TryGetValue(operationName, out obj))
			{
				return Enumerable.Empty<IEdmOperationImport>();
			}
			List<IEdmOperationImport> list = obj as List<IEdmOperationImport>;
			if (list != null)
			{
				return list;
			}
			return new IEdmOperationImport[] { (IEdmOperationImport)obj };
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0001E75A File Offset: 0x0001C95A
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0001E770 File Offset: 0x0001C970
		private IEnumerable<IEdmEntityContainerElement> ComputeElements()
		{
			List<IEdmEntityContainerElement> list = new List<IEdmEntityContainerElement>();
			foreach (CsdlEntitySet csdlEntitySet in this.entityContainer.EntitySets)
			{
				CsdlSemanticsEntitySet csdlSemanticsEntitySet = new CsdlSemanticsEntitySet(this, csdlEntitySet);
				list.Add(csdlSemanticsEntitySet);
			}
			foreach (CsdlSingleton csdlSingleton in this.entityContainer.Singletons)
			{
				CsdlSemanticsSingleton csdlSemanticsSingleton = new CsdlSemanticsSingleton(this, csdlSingleton);
				list.Add(csdlSemanticsSingleton);
			}
			foreach (CsdlOperationImport csdlOperationImport in this.entityContainer.OperationImports)
			{
				this.AddOperationImport(csdlOperationImport, list);
			}
			return list;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001E86C File Offset: 0x0001CA6C
		private void AddOperationImport(CsdlOperationImport operationImport, List<IEdmEntityContainerElement> elements)
		{
			CsdlFunctionImport csdlFunctionImport = operationImport as CsdlFunctionImport;
			CsdlActionImport csdlActionImport = operationImport as CsdlActionImport;
			EdmSchemaElementKind filterKind = EdmSchemaElementKind.Action;
			if (csdlFunctionImport != null)
			{
				filterKind = EdmSchemaElementKind.Function;
			}
			IEnumerable<IEdmOperation> enumerable = from o in this.context.FindOperations(operationImport.SchemaOperationQualifiedTypeName)
				where o.SchemaElementKind == filterKind && !o.IsBound
				select o;
			int num = 0;
			foreach (IEdmOperation edmOperation in enumerable)
			{
				CsdlSemanticsOperationImport csdlSemanticsOperationImport;
				if (csdlFunctionImport != null)
				{
					csdlSemanticsOperationImport = new CsdlSemanticsFunctionImport(this, csdlFunctionImport, (IEdmFunction)edmOperation);
				}
				else
				{
					csdlSemanticsOperationImport = new CsdlSemanticsActionImport(this, csdlActionImport, (IEdmAction)edmOperation);
				}
				num++;
				elements.Add(csdlSemanticsOperationImport);
			}
			if (num == 0)
			{
				CsdlSemanticsOperationImport csdlSemanticsOperationImport;
				if (filterKind == EdmSchemaElementKind.Action)
				{
					UnresolvedAction unresolvedAction = new UnresolvedAction(operationImport.SchemaOperationQualifiedTypeName, Strings.Bad_UnresolvedOperation(operationImport.SchemaOperationQualifiedTypeName), operationImport.Location);
					csdlSemanticsOperationImport = new CsdlSemanticsActionImport(this, csdlActionImport, unresolvedAction);
				}
				else
				{
					UnresolvedFunction unresolvedFunction = new UnresolvedFunction(operationImport.SchemaOperationQualifiedTypeName, Strings.Bad_UnresolvedOperation(operationImport.SchemaOperationQualifiedTypeName), operationImport.Location);
					csdlSemanticsOperationImport = new CsdlSemanticsFunctionImport(this, csdlFunctionImport, unresolvedFunction);
				}
				elements.Add(csdlSemanticsOperationImport);
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0001E998 File Offset: 0x0001CB98
		private IEnumerable<EdmError> ComputeErrors()
		{
			List<EdmError> list = new List<EdmError>();
			if (this.Extends != null && this.Extends.IsBad())
			{
				list.AddRange(((IEdmCheckable)this.Extends).Errors);
			}
			return list;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0001E9D8 File Offset: 0x0001CBD8
		private Dictionary<string, IEdmEntitySet> ComputeEntitySetDictionary()
		{
			Dictionary<string, IEdmEntitySet> dictionary = new Dictionary<string, IEdmEntitySet>();
			foreach (IEdmEntitySet edmEntitySet in this.Elements.OfType<IEdmEntitySet>())
			{
				RegistrationHelper.AddElement<IEdmEntitySet>(edmEntitySet, edmEntitySet.Name, dictionary, new Func<IEdmEntitySet, IEdmEntitySet, IEdmEntitySet>(RegistrationHelper.CreateAmbiguousEntitySetBinding));
			}
			return dictionary;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0001EA44 File Offset: 0x0001CC44
		private Dictionary<string, IEdmSingleton> ComputeSingletonDictionary()
		{
			Dictionary<string, IEdmSingleton> dictionary = new Dictionary<string, IEdmSingleton>();
			foreach (IEdmSingleton edmSingleton in this.Elements.OfType<IEdmSingleton>())
			{
				RegistrationHelper.AddElement<IEdmSingleton>(edmSingleton, edmSingleton.Name, dictionary, new Func<IEdmSingleton, IEdmSingleton, IEdmSingleton>(RegistrationHelper.CreateAmbiguousSingletonBinding));
			}
			return dictionary;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0001EAB0 File Offset: 0x0001CCB0
		private Dictionary<string, object> ComputeOperationImportsDictionary()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (IEdmOperationImport edmOperationImport in this.Elements.OfType<IEdmOperationImport>())
			{
				RegistrationHelper.AddOperationImport(edmOperationImport, edmOperationImport.Name, dictionary);
			}
			return dictionary;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0001EB10 File Offset: 0x0001CD10
		private IEdmEntityContainer ComputeExtends()
		{
			string extends = this.entityContainer.Extends;
			if (extends != null)
			{
				IEdmEntityContainer edmEntityContainer = this.Context.FindEntityContainer(extends);
				return edmEntityContainer ?? new UnresolvedEntityContainer(this.entityContainer.Extends, base.Location);
			}
			return null;
		}

		// Token: 0x040006A4 RID: 1700
		private readonly string fullName;

		// Token: 0x040006A5 RID: 1701
		private readonly CsdlEntityContainer entityContainer;

		// Token: 0x040006A6 RID: 1702
		private readonly CsdlSemanticsSchema context;

		// Token: 0x040006A7 RID: 1703
		private readonly Cache<CsdlSemanticsEntityContainer, IEnumerable<IEdmEntityContainerElement>> elementsCache = new Cache<CsdlSemanticsEntityContainer, IEnumerable<IEdmEntityContainerElement>>();

		// Token: 0x040006A8 RID: 1704
		private static readonly Func<CsdlSemanticsEntityContainer, IEnumerable<IEdmEntityContainerElement>> ComputeElementsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeElements();

		// Token: 0x040006A9 RID: 1705
		private readonly Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmEntitySet>> entitySetDictionaryCache = new Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmEntitySet>>();

		// Token: 0x040006AA RID: 1706
		private static readonly Func<CsdlSemanticsEntityContainer, Dictionary<string, IEdmEntitySet>> ComputeEntitySetDictionaryFunc = (CsdlSemanticsEntityContainer me) => me.ComputeEntitySetDictionary();

		// Token: 0x040006AB RID: 1707
		private readonly Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmSingleton>> singletonDictionaryCache = new Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmSingleton>>();

		// Token: 0x040006AC RID: 1708
		private static readonly Func<CsdlSemanticsEntityContainer, Dictionary<string, IEdmSingleton>> ComputeSingletonDictionaryFunc = (CsdlSemanticsEntityContainer me) => me.ComputeSingletonDictionary();

		// Token: 0x040006AD RID: 1709
		private readonly Cache<CsdlSemanticsEntityContainer, Dictionary<string, object>> operationImportsDictionaryCache = new Cache<CsdlSemanticsEntityContainer, Dictionary<string, object>>();

		// Token: 0x040006AE RID: 1710
		private static readonly Func<CsdlSemanticsEntityContainer, Dictionary<string, object>> ComputeOperationImportsDictionaryFunc = (CsdlSemanticsEntityContainer me) => me.ComputeOperationImportsDictionary();

		// Token: 0x040006AF RID: 1711
		private readonly Cache<CsdlSemanticsEntityContainer, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsEntityContainer, IEnumerable<EdmError>>();

		// Token: 0x040006B0 RID: 1712
		private static readonly Func<CsdlSemanticsEntityContainer, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeErrors();

		// Token: 0x040006B1 RID: 1713
		private readonly Cache<CsdlSemanticsEntityContainer, IEdmEntityContainer> extendsCache = new Cache<CsdlSemanticsEntityContainer, IEdmEntityContainer>();

		// Token: 0x040006B2 RID: 1714
		private static readonly Func<CsdlSemanticsEntityContainer, IEdmEntityContainer> ComputeExtendsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeExtends();

		// Token: 0x040006B3 RID: 1715
		private static readonly Func<CsdlSemanticsEntityContainer, IEdmEntityContainer> OnCycleExtendsFunc = (CsdlSemanticsEntityContainer me) => new CyclicEntityContainer(me.entityContainer.Extends, me.Location);
	}
}
