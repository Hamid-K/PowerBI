using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200018A RID: 394
	internal class CsdlSemanticsEntityContainer : CsdlSemanticsElement, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmCheckable
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x0001C284 File Offset: 0x0001A484
		public CsdlSemanticsEntityContainer(CsdlSemanticsSchema context, CsdlEntityContainer entityContainer)
			: base(entityContainer)
		{
			this.context = context;
			this.entityContainer = entityContainer;
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00008D57 File Offset: 0x00006F57
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0001C2E8 File Offset: 0x0001A4E8
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x0001C2F5 File Offset: 0x0001A4F5
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return this.elementsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeElementsFunc, null);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x0001C309 File Offset: 0x0001A509
		public string Namespace
		{
			get
			{
				return this.context.Namespace;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0001C316 File Offset: 0x0001A516
		public string Name
		{
			get
			{
				return this.entityContainer.Name;
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0001C323 File Offset: 0x0001A523
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0001C337 File Offset: 0x0001A537
		public override CsdlElement Element
		{
			get
			{
				return this.entityContainer;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0001C33F File Offset: 0x0001A53F
		internal CsdlSemanticsSchema Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0001C347 File Offset: 0x0001A547
		internal IEdmEntityContainer Extends
		{
			get
			{
				return this.extendsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeExtendsFunc, CsdlSemanticsEntityContainer.OnCycleExtendsFunc);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0001C35F File Offset: 0x0001A55F
		private Dictionary<string, IEdmEntitySet> EntitySetDictionary
		{
			get
			{
				return this.entitySetDictionaryCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeEntitySetDictionaryFunc, null);
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0001C373 File Offset: 0x0001A573
		private Dictionary<string, IEdmSingleton> SingletonDictionary
		{
			get
			{
				return this.singletonDictionaryCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeSingletonDictionaryFunc, null);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0001C387 File Offset: 0x0001A587
		private Dictionary<string, object> OperationImportsDictionary
		{
			get
			{
				return this.operationImportsDictionaryCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeOperationImportsDictionaryFunc, null);
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001C39C File Offset: 0x0001A59C
		public IEdmEntitySet FindEntitySet(string name)
		{
			IEdmEntitySet edmEntitySet;
			if (!this.EntitySetDictionary.TryGetValue(name, ref edmEntitySet))
			{
				return null;
			}
			return edmEntitySet;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001C3BC File Offset: 0x0001A5BC
		public IEdmSingleton FindSingleton(string name)
		{
			IEdmSingleton edmSingleton;
			if (!this.SingletonDictionary.TryGetValue(name, ref edmSingleton))
			{
				return null;
			}
			return edmSingleton;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0001C3DC File Offset: 0x0001A5DC
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

		// Token: 0x06000A84 RID: 2692 RVA: 0x0001C41A File Offset: 0x0001A61A
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001C430 File Offset: 0x0001A630
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

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001C52C File Offset: 0x0001A72C
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

		// Token: 0x06000A87 RID: 2695 RVA: 0x0001C658 File Offset: 0x0001A858
		private IEnumerable<EdmError> ComputeErrors()
		{
			List<EdmError> list = new List<EdmError>();
			if (this.Extends != null && this.Extends.IsBad())
			{
				list.AddRange(((IEdmCheckable)this.Extends).Errors);
			}
			return list;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0001C698 File Offset: 0x0001A898
		private Dictionary<string, IEdmEntitySet> ComputeEntitySetDictionary()
		{
			Dictionary<string, IEdmEntitySet> dictionary = new Dictionary<string, IEdmEntitySet>();
			foreach (IEdmEntitySet edmEntitySet in Enumerable.OfType<IEdmEntitySet>(this.Elements))
			{
				RegistrationHelper.AddElement<IEdmEntitySet>(edmEntitySet, edmEntitySet.Name, dictionary, new Func<IEdmEntitySet, IEdmEntitySet, IEdmEntitySet>(RegistrationHelper.CreateAmbiguousEntitySetBinding));
			}
			return dictionary;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001C704 File Offset: 0x0001A904
		private Dictionary<string, IEdmSingleton> ComputeSingletonDictionary()
		{
			Dictionary<string, IEdmSingleton> dictionary = new Dictionary<string, IEdmSingleton>();
			foreach (IEdmSingleton edmSingleton in Enumerable.OfType<IEdmSingleton>(this.Elements))
			{
				RegistrationHelper.AddElement<IEdmSingleton>(edmSingleton, edmSingleton.Name, dictionary, new Func<IEdmSingleton, IEdmSingleton, IEdmSingleton>(RegistrationHelper.CreateAmbiguousSingletonBinding));
			}
			return dictionary;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0001C770 File Offset: 0x0001A970
		private Dictionary<string, object> ComputeOperationImportsDictionary()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (IEdmOperationImport edmOperationImport in Enumerable.OfType<IEdmOperationImport>(this.Elements))
			{
				RegistrationHelper.AddOperationImport(edmOperationImport, edmOperationImport.Name, dictionary);
			}
			return dictionary;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001C7D0 File Offset: 0x0001A9D0
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

		// Token: 0x04000624 RID: 1572
		private readonly CsdlEntityContainer entityContainer;

		// Token: 0x04000625 RID: 1573
		private readonly CsdlSemanticsSchema context;

		// Token: 0x04000626 RID: 1574
		private readonly Cache<CsdlSemanticsEntityContainer, IEnumerable<IEdmEntityContainerElement>> elementsCache = new Cache<CsdlSemanticsEntityContainer, IEnumerable<IEdmEntityContainerElement>>();

		// Token: 0x04000627 RID: 1575
		private static readonly Func<CsdlSemanticsEntityContainer, IEnumerable<IEdmEntityContainerElement>> ComputeElementsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeElements();

		// Token: 0x04000628 RID: 1576
		private readonly Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmEntitySet>> entitySetDictionaryCache = new Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmEntitySet>>();

		// Token: 0x04000629 RID: 1577
		private static readonly Func<CsdlSemanticsEntityContainer, Dictionary<string, IEdmEntitySet>> ComputeEntitySetDictionaryFunc = (CsdlSemanticsEntityContainer me) => me.ComputeEntitySetDictionary();

		// Token: 0x0400062A RID: 1578
		private readonly Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmSingleton>> singletonDictionaryCache = new Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmSingleton>>();

		// Token: 0x0400062B RID: 1579
		private static readonly Func<CsdlSemanticsEntityContainer, Dictionary<string, IEdmSingleton>> ComputeSingletonDictionaryFunc = (CsdlSemanticsEntityContainer me) => me.ComputeSingletonDictionary();

		// Token: 0x0400062C RID: 1580
		private readonly Cache<CsdlSemanticsEntityContainer, Dictionary<string, object>> operationImportsDictionaryCache = new Cache<CsdlSemanticsEntityContainer, Dictionary<string, object>>();

		// Token: 0x0400062D RID: 1581
		private static readonly Func<CsdlSemanticsEntityContainer, Dictionary<string, object>> ComputeOperationImportsDictionaryFunc = (CsdlSemanticsEntityContainer me) => me.ComputeOperationImportsDictionary();

		// Token: 0x0400062E RID: 1582
		private readonly Cache<CsdlSemanticsEntityContainer, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsEntityContainer, IEnumerable<EdmError>>();

		// Token: 0x0400062F RID: 1583
		private static readonly Func<CsdlSemanticsEntityContainer, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeErrors();

		// Token: 0x04000630 RID: 1584
		private readonly Cache<CsdlSemanticsEntityContainer, IEdmEntityContainer> extendsCache = new Cache<CsdlSemanticsEntityContainer, IEdmEntityContainer>();

		// Token: 0x04000631 RID: 1585
		private static readonly Func<CsdlSemanticsEntityContainer, IEdmEntityContainer> ComputeExtendsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeExtends();

		// Token: 0x04000632 RID: 1586
		private static readonly Func<CsdlSemanticsEntityContainer, IEdmEntityContainer> OnCycleExtendsFunc = (CsdlSemanticsEntityContainer me) => new CyclicEntityContainer(me.entityContainer.Extends, me.Location);
	}
}
