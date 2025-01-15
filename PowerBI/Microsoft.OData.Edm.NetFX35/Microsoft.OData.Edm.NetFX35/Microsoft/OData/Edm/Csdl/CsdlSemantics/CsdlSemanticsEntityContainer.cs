using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A9 RID: 425
	internal class CsdlSemanticsEntityContainer : CsdlSemanticsElement, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement, IEdmCheckable
	{
		// Token: 0x0600089A RID: 2202 RVA: 0x00016438 File Offset: 0x00014638
		public CsdlSemanticsEntityContainer(CsdlSemanticsSchema context, CsdlEntityContainer entityContainer)
			: base(entityContainer)
		{
			this.context = context;
			this.entityContainer = entityContainer;
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0001649C File Offset: 0x0001469C
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0001649F File Offset: 0x0001469F
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x000164AC File Offset: 0x000146AC
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return this.elementsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeElementsFunc, null);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x000164C0 File Offset: 0x000146C0
		public string Namespace
		{
			get
			{
				return this.context.Namespace;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x000164CD File Offset: 0x000146CD
		public string Name
		{
			get
			{
				return this.entityContainer.Name;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x000164DA File Offset: 0x000146DA
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x000164EE File Offset: 0x000146EE
		public override CsdlElement Element
		{
			get
			{
				return this.entityContainer;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x000164F6 File Offset: 0x000146F6
		internal CsdlSemanticsSchema Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000164FE File Offset: 0x000146FE
		internal IEdmEntityContainer Extends
		{
			get
			{
				return this.extendsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeExtendsFunc, CsdlSemanticsEntityContainer.OnCycleExtendsFunc);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00016516 File Offset: 0x00014716
		private Dictionary<string, IEdmEntitySet> EntitySetDictionary
		{
			get
			{
				return this.entitySetDictionaryCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeEntitySetDictionaryFunc, null);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001652A File Offset: 0x0001472A
		private Dictionary<string, IEdmSingleton> SingletonDictionary
		{
			get
			{
				return this.singletonDictionaryCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeSingletonDictionaryFunc, null);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0001653E File Offset: 0x0001473E
		private Dictionary<string, object> OperationImportsDictionary
		{
			get
			{
				return this.operationImportsDictionaryCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeOperationImportsDictionaryFunc, null);
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00016554 File Offset: 0x00014754
		public IEdmEntitySet FindEntitySet(string name)
		{
			IEdmEntitySet edmEntitySet;
			if (!this.EntitySetDictionary.TryGetValue(name, ref edmEntitySet))
			{
				return null;
			}
			return edmEntitySet;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00016574 File Offset: 0x00014774
		public IEdmSingleton FindSingleton(string name)
		{
			IEdmSingleton edmSingleton;
			if (!this.SingletonDictionary.TryGetValue(name, ref edmSingleton))
			{
				return null;
			}
			return edmSingleton;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00016594 File Offset: 0x00014794
		public IEnumerable<IEdmOperationImport> FindOperationImports(string operationName)
		{
			object obj;
			if (!this.OperationImportsDictionary.TryGetValue(operationName, ref obj))
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

		// Token: 0x060008AA RID: 2218 RVA: 0x000165D4 File Offset: 0x000147D4
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000165E8 File Offset: 0x000147E8
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

		// Token: 0x060008AC RID: 2220 RVA: 0x0001670C File Offset: 0x0001490C
		private void AddOperationImport(CsdlOperationImport operationImport, List<IEdmEntityContainerElement> elements)
		{
			CsdlFunctionImport csdlFunctionImport = operationImport as CsdlFunctionImport;
			CsdlActionImport csdlActionImport = operationImport as CsdlActionImport;
			EdmSchemaElementKind filterKind = EdmSchemaElementKind.Action;
			if (csdlFunctionImport != null)
			{
				filterKind = EdmSchemaElementKind.Function;
			}
			IEnumerable<IEdmOperation> enumerable = Enumerable.Where<IEdmOperation>(this.context.FindOperations(operationImport.SchemaOperationQualifiedTypeName), (IEdmOperation o) => o.SchemaElementKind == filterKind && !o.IsBound);
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

		// Token: 0x060008AD RID: 2221 RVA: 0x0001683C File Offset: 0x00014A3C
		private IEnumerable<EdmError> ComputeErrors()
		{
			List<EdmError> list = new List<EdmError>();
			if (this.Extends != null && this.Extends.IsBad())
			{
				list.AddRange(((IEdmCheckable)this.Extends).Errors);
			}
			return list;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001687C File Offset: 0x00014A7C
		private Dictionary<string, IEdmEntitySet> ComputeEntitySetDictionary()
		{
			Dictionary<string, IEdmEntitySet> dictionary = new Dictionary<string, IEdmEntitySet>();
			foreach (IEdmEntitySet edmEntitySet in Enumerable.OfType<IEdmEntitySet>(this.Elements))
			{
				RegistrationHelper.AddElement<IEdmEntitySet>(edmEntitySet, edmEntitySet.Name, dictionary, new Func<IEdmEntitySet, IEdmEntitySet, IEdmEntitySet>(RegistrationHelper.CreateAmbiguousEntitySetBinding));
			}
			return dictionary;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x000168E8 File Offset: 0x00014AE8
		private Dictionary<string, IEdmSingleton> ComputeSingletonDictionary()
		{
			Dictionary<string, IEdmSingleton> dictionary = new Dictionary<string, IEdmSingleton>();
			foreach (IEdmSingleton edmSingleton in Enumerable.OfType<IEdmSingleton>(this.Elements))
			{
				RegistrationHelper.AddElement<IEdmSingleton>(edmSingleton, edmSingleton.Name, dictionary, new Func<IEdmSingleton, IEdmSingleton, IEdmSingleton>(RegistrationHelper.CreateAmbiguousSingletonBinding));
			}
			return dictionary;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00016954 File Offset: 0x00014B54
		private Dictionary<string, object> ComputeOperationImportsDictionary()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (IEdmOperationImport edmOperationImport in Enumerable.OfType<IEdmOperationImport>(this.Elements))
			{
				RegistrationHelper.AddOperationImport(edmOperationImport, edmOperationImport.Name, dictionary);
			}
			return dictionary;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000169B4 File Offset: 0x00014BB4
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

		// Token: 0x0400043C RID: 1084
		private readonly CsdlEntityContainer entityContainer;

		// Token: 0x0400043D RID: 1085
		private readonly CsdlSemanticsSchema context;

		// Token: 0x0400043E RID: 1086
		private readonly Cache<CsdlSemanticsEntityContainer, IEnumerable<IEdmEntityContainerElement>> elementsCache = new Cache<CsdlSemanticsEntityContainer, IEnumerable<IEdmEntityContainerElement>>();

		// Token: 0x0400043F RID: 1087
		private static readonly Func<CsdlSemanticsEntityContainer, IEnumerable<IEdmEntityContainerElement>> ComputeElementsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeElements();

		// Token: 0x04000440 RID: 1088
		private readonly Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmEntitySet>> entitySetDictionaryCache = new Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmEntitySet>>();

		// Token: 0x04000441 RID: 1089
		private static readonly Func<CsdlSemanticsEntityContainer, Dictionary<string, IEdmEntitySet>> ComputeEntitySetDictionaryFunc = (CsdlSemanticsEntityContainer me) => me.ComputeEntitySetDictionary();

		// Token: 0x04000442 RID: 1090
		private readonly Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmSingleton>> singletonDictionaryCache = new Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmSingleton>>();

		// Token: 0x04000443 RID: 1091
		private static readonly Func<CsdlSemanticsEntityContainer, Dictionary<string, IEdmSingleton>> ComputeSingletonDictionaryFunc = (CsdlSemanticsEntityContainer me) => me.ComputeSingletonDictionary();

		// Token: 0x04000444 RID: 1092
		private readonly Cache<CsdlSemanticsEntityContainer, Dictionary<string, object>> operationImportsDictionaryCache = new Cache<CsdlSemanticsEntityContainer, Dictionary<string, object>>();

		// Token: 0x04000445 RID: 1093
		private static readonly Func<CsdlSemanticsEntityContainer, Dictionary<string, object>> ComputeOperationImportsDictionaryFunc = (CsdlSemanticsEntityContainer me) => me.ComputeOperationImportsDictionary();

		// Token: 0x04000446 RID: 1094
		private readonly Cache<CsdlSemanticsEntityContainer, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsEntityContainer, IEnumerable<EdmError>>();

		// Token: 0x04000447 RID: 1095
		private static readonly Func<CsdlSemanticsEntityContainer, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeErrors();

		// Token: 0x04000448 RID: 1096
		private readonly Cache<CsdlSemanticsEntityContainer, IEdmEntityContainer> extendsCache = new Cache<CsdlSemanticsEntityContainer, IEdmEntityContainer>();

		// Token: 0x04000449 RID: 1097
		private static readonly Func<CsdlSemanticsEntityContainer, IEdmEntityContainer> ComputeExtendsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeExtends();

		// Token: 0x0400044A RID: 1098
		private static readonly Func<CsdlSemanticsEntityContainer, IEdmEntityContainer> OnCycleExtendsFunc = (CsdlSemanticsEntityContainer me) => new CyclicEntityContainer(me.entityContainer.Extends, me.Location);
	}
}
