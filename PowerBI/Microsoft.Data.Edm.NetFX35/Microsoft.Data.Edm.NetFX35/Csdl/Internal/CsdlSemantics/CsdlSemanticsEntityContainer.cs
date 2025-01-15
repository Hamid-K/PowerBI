using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Library.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x0200016C RID: 364
	internal class CsdlSemanticsEntityContainer : CsdlSemanticsElement, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement, IEdmCheckable
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x00014C70 File Offset: 0x00012E70
		public CsdlSemanticsEntityContainer(CsdlSemanticsSchema context, CsdlEntityContainer entityContainer)
			: base(entityContainer)
		{
			this.context = context;
			this.entityContainer = entityContainer;
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00014CDF File Offset: 0x00012EDF
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00014CE2 File Offset: 0x00012EE2
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.context.Model;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00014CEF File Offset: 0x00012EEF
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return this.elementsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeElementsFunc, null);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00014D03 File Offset: 0x00012F03
		public IEnumerable<CsdlSemanticsAssociationSet> AssociationSets
		{
			get
			{
				return this.associationSetsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeAssociationSetsFunc, null);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00014D17 File Offset: 0x00012F17
		public string Namespace
		{
			get
			{
				return this.context.Namespace;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00014D24 File Offset: 0x00012F24
		public string Name
		{
			get
			{
				return this.entityContainer.Name;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00014D31 File Offset: 0x00012F31
		public IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errorsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeErrorsFunc, null);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00014D45 File Offset: 0x00012F45
		public override CsdlElement Element
		{
			get
			{
				return this.entityContainer;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00014D4D File Offset: 0x00012F4D
		internal CsdlSemanticsSchema Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00014D55 File Offset: 0x00012F55
		private IEdmEntityContainer Extends
		{
			get
			{
				return this.extendsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeExtendsFunc, CsdlSemanticsEntityContainer.OnCycleExtendsFunc);
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00014D6D File Offset: 0x00012F6D
		private Dictionary<string, IEdmEntitySet> EntitySetDictionary
		{
			get
			{
				return this.entitySetDictionaryCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeEntitySetDictionaryFunc, null);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00014D81 File Offset: 0x00012F81
		private Dictionary<string, object> FunctionImportsDictionary
		{
			get
			{
				return this.functionImportsDictionaryCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeFunctionImportsDictionaryFunc, null);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00014D95 File Offset: 0x00012F95
		private Dictionary<IEdmAssociation, IEnumerable<CsdlSemanticsAssociationSet>> AssociationSetMappings
		{
			get
			{
				return this.associationSetMappingsCache.GetValue(this, CsdlSemanticsEntityContainer.ComputeAssociationSetMappingsFunc, null);
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00014DAC File Offset: 0x00012FAC
		public IEdmEntitySet FindEntitySet(string name)
		{
			IEdmEntitySet edmEntitySet;
			if (!this.EntitySetDictionary.TryGetValue(name, ref edmEntitySet))
			{
				return null;
			}
			return edmEntitySet;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00014DCC File Offset: 0x00012FCC
		public IEnumerable<CsdlSemanticsAssociationSet> FindAssociationSets(IEdmAssociation association)
		{
			IEnumerable<CsdlSemanticsAssociationSet> enumerable;
			if (!this.AssociationSetMappings.TryGetValue(association, ref enumerable))
			{
				return null;
			}
			return enumerable;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00014DEC File Offset: 0x00012FEC
		public IEnumerable<IEdmFunctionImport> FindFunctionImports(string name)
		{
			object obj;
			if (!this.FunctionImportsDictionary.TryGetValue(name, ref obj))
			{
				return Enumerable.Empty<IEdmFunctionImport>();
			}
			List<IEdmFunctionImport> list = obj as List<IEdmFunctionImport>;
			if (list != null)
			{
				return list;
			}
			return new IEdmFunctionImport[] { (IEdmFunctionImport)obj };
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00014E2C File Offset: 0x0001302C
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.Context);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00014E40 File Offset: 0x00013040
		private IEnumerable<IEdmEntityContainerElement> ComputeElements()
		{
			List<IEdmEntityContainerElement> list = new List<IEdmEntityContainerElement>();
			CsdlSemanticsEntityContainer csdlSemanticsEntityContainer = this.Extends as CsdlSemanticsEntityContainer;
			if (csdlSemanticsEntityContainer != null)
			{
				foreach (CsdlEntitySet csdlEntitySet in csdlSemanticsEntityContainer.entityContainer.EntitySets)
				{
					CsdlSemanticsEntitySet csdlSemanticsEntitySet = new CsdlSemanticsEntitySet(this, csdlEntitySet);
					list.Add(csdlSemanticsEntitySet);
				}
				foreach (CsdlFunctionImport csdlFunctionImport in csdlSemanticsEntityContainer.entityContainer.FunctionImports)
				{
					CsdlSemanticsFunctionImport csdlSemanticsFunctionImport = new CsdlSemanticsFunctionImport(this, csdlFunctionImport);
					list.Add(csdlSemanticsFunctionImport);
				}
			}
			foreach (CsdlEntitySet csdlEntitySet2 in this.entityContainer.EntitySets)
			{
				CsdlSemanticsEntitySet csdlSemanticsEntitySet2 = new CsdlSemanticsEntitySet(this, csdlEntitySet2);
				list.Add(csdlSemanticsEntitySet2);
			}
			foreach (CsdlFunctionImport csdlFunctionImport2 in this.entityContainer.FunctionImports)
			{
				CsdlSemanticsFunctionImport csdlSemanticsFunctionImport2 = new CsdlSemanticsFunctionImport(this, csdlFunctionImport2);
				list.Add(csdlSemanticsFunctionImport2);
			}
			return list;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00014FB0 File Offset: 0x000131B0
		private IEnumerable<CsdlSemanticsAssociationSet> ComputeAssociationSets()
		{
			List<CsdlSemanticsAssociationSet> list = new List<CsdlSemanticsAssociationSet>();
			if (this.entityContainer.Extends != null)
			{
				CsdlSemanticsEntityContainer csdlSemanticsEntityContainer = this.Extends as CsdlSemanticsEntityContainer;
				if (csdlSemanticsEntityContainer != null)
				{
					foreach (CsdlAssociationSet csdlAssociationSet in csdlSemanticsEntityContainer.entityContainer.AssociationSets)
					{
						CsdlSemanticsAssociationSet csdlSemanticsAssociationSet = new CsdlSemanticsAssociationSet(this, csdlAssociationSet);
						list.Add(csdlSemanticsAssociationSet);
					}
				}
			}
			foreach (CsdlAssociationSet csdlAssociationSet2 in this.entityContainer.AssociationSets)
			{
				CsdlSemanticsAssociationSet csdlSemanticsAssociationSet2 = new CsdlSemanticsAssociationSet(this, csdlAssociationSet2);
				list.Add(csdlSemanticsAssociationSet2);
			}
			return list;
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00015084 File Offset: 0x00013284
		private IEnumerable<EdmError> ComputeErrors()
		{
			List<EdmError> list = new List<EdmError>();
			if (this.Extends != null && this.Extends.IsBad())
			{
				list.AddRange(((IEdmCheckable)this.Extends).Errors);
			}
			foreach (CsdlSemanticsAssociationSet csdlSemanticsAssociationSet in this.AssociationSets)
			{
				int count = list.Count;
				list.AddRange(csdlSemanticsAssociationSet.Errors());
				if (list.Count == count)
				{
					list.AddRange(csdlSemanticsAssociationSet.End1.Errors());
					list.AddRange(csdlSemanticsAssociationSet.End2.Errors());
				}
			}
			return list;
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001513C File Offset: 0x0001333C
		private Dictionary<IEdmAssociation, IEnumerable<CsdlSemanticsAssociationSet>> ComputeAssociationSetMappings()
		{
			Dictionary<IEdmAssociation, IEnumerable<CsdlSemanticsAssociationSet>> dictionary = new Dictionary<IEdmAssociation, IEnumerable<CsdlSemanticsAssociationSet>>();
			if (this.entityContainer.Extends != null)
			{
				CsdlSemanticsEntityContainer csdlSemanticsEntityContainer = this.Extends as CsdlSemanticsEntityContainer;
				if (csdlSemanticsEntityContainer != null)
				{
					foreach (KeyValuePair<IEdmAssociation, IEnumerable<CsdlSemanticsAssociationSet>> keyValuePair in csdlSemanticsEntityContainer.AssociationSetMappings)
					{
						dictionary[keyValuePair.Key] = new List<CsdlSemanticsAssociationSet>(keyValuePair.Value);
					}
				}
			}
			foreach (CsdlSemanticsAssociationSet csdlSemanticsAssociationSet in this.AssociationSets)
			{
				CsdlSemanticsAssociation csdlSemanticsAssociation = csdlSemanticsAssociationSet.Association as CsdlSemanticsAssociation;
				if (csdlSemanticsAssociation != null)
				{
					IEnumerable<CsdlSemanticsAssociationSet> enumerable;
					if (!dictionary.TryGetValue(csdlSemanticsAssociation, ref enumerable))
					{
						enumerable = new List<CsdlSemanticsAssociationSet>();
						dictionary[csdlSemanticsAssociation] = enumerable;
					}
					((List<CsdlSemanticsAssociationSet>)enumerable).Add(csdlSemanticsAssociationSet);
				}
			}
			return dictionary;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001523C File Offset: 0x0001343C
		private Dictionary<string, IEdmEntitySet> ComputeEntitySetDictionary()
		{
			Dictionary<string, IEdmEntitySet> dictionary = new Dictionary<string, IEdmEntitySet>();
			foreach (IEdmEntitySet edmEntitySet in Enumerable.OfType<IEdmEntitySet>(this.Elements))
			{
				RegistrationHelper.AddElement<IEdmEntitySet>(edmEntitySet, edmEntitySet.Name, dictionary, new Func<IEdmEntitySet, IEdmEntitySet, IEdmEntitySet>(RegistrationHelper.CreateAmbiguousEntitySetBinding));
			}
			return dictionary;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x000152A8 File Offset: 0x000134A8
		private Dictionary<string, object> ComputeFunctionImportsDictionary()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (IEdmFunctionImport edmFunctionImport in Enumerable.OfType<IEdmFunctionImport>(this.Elements))
			{
				RegistrationHelper.AddFunction<IEdmFunctionImport>(edmFunctionImport, edmFunctionImport.Name, dictionary);
			}
			return dictionary;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00015308 File Offset: 0x00013508
		private IEdmEntityContainer ComputeExtends()
		{
			if (this.entityContainer.Extends == null)
			{
				return null;
			}
			CsdlSemanticsEntityContainer csdlSemanticsEntityContainer = this.Model.FindDeclaredEntityContainer(this.entityContainer.Extends) as CsdlSemanticsEntityContainer;
			if (csdlSemanticsEntityContainer != null)
			{
				IEdmEntityContainer extends = csdlSemanticsEntityContainer.Extends;
				return csdlSemanticsEntityContainer;
			}
			return new UnresolvedEntityContainer(this.entityContainer.Extends, base.Location);
		}

		// Token: 0x040003D6 RID: 982
		private readonly CsdlEntityContainer entityContainer;

		// Token: 0x040003D7 RID: 983
		private readonly CsdlSemanticsSchema context;

		// Token: 0x040003D8 RID: 984
		private readonly Cache<CsdlSemanticsEntityContainer, IEnumerable<IEdmEntityContainerElement>> elementsCache = new Cache<CsdlSemanticsEntityContainer, IEnumerable<IEdmEntityContainerElement>>();

		// Token: 0x040003D9 RID: 985
		private static readonly Func<CsdlSemanticsEntityContainer, IEnumerable<IEdmEntityContainerElement>> ComputeElementsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeElements();

		// Token: 0x040003DA RID: 986
		private readonly Cache<CsdlSemanticsEntityContainer, IEnumerable<CsdlSemanticsAssociationSet>> associationSetsCache = new Cache<CsdlSemanticsEntityContainer, IEnumerable<CsdlSemanticsAssociationSet>>();

		// Token: 0x040003DB RID: 987
		private static readonly Func<CsdlSemanticsEntityContainer, IEnumerable<CsdlSemanticsAssociationSet>> ComputeAssociationSetsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeAssociationSets();

		// Token: 0x040003DC RID: 988
		private readonly Cache<CsdlSemanticsEntityContainer, Dictionary<IEdmAssociation, IEnumerable<CsdlSemanticsAssociationSet>>> associationSetMappingsCache = new Cache<CsdlSemanticsEntityContainer, Dictionary<IEdmAssociation, IEnumerable<CsdlSemanticsAssociationSet>>>();

		// Token: 0x040003DD RID: 989
		private static readonly Func<CsdlSemanticsEntityContainer, Dictionary<IEdmAssociation, IEnumerable<CsdlSemanticsAssociationSet>>> ComputeAssociationSetMappingsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeAssociationSetMappings();

		// Token: 0x040003DE RID: 990
		private readonly Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmEntitySet>> entitySetDictionaryCache = new Cache<CsdlSemanticsEntityContainer, Dictionary<string, IEdmEntitySet>>();

		// Token: 0x040003DF RID: 991
		private static readonly Func<CsdlSemanticsEntityContainer, Dictionary<string, IEdmEntitySet>> ComputeEntitySetDictionaryFunc = (CsdlSemanticsEntityContainer me) => me.ComputeEntitySetDictionary();

		// Token: 0x040003E0 RID: 992
		private readonly Cache<CsdlSemanticsEntityContainer, Dictionary<string, object>> functionImportsDictionaryCache = new Cache<CsdlSemanticsEntityContainer, Dictionary<string, object>>();

		// Token: 0x040003E1 RID: 993
		private static readonly Func<CsdlSemanticsEntityContainer, Dictionary<string, object>> ComputeFunctionImportsDictionaryFunc = (CsdlSemanticsEntityContainer me) => me.ComputeFunctionImportsDictionary();

		// Token: 0x040003E2 RID: 994
		private readonly Cache<CsdlSemanticsEntityContainer, IEnumerable<EdmError>> errorsCache = new Cache<CsdlSemanticsEntityContainer, IEnumerable<EdmError>>();

		// Token: 0x040003E3 RID: 995
		private static readonly Func<CsdlSemanticsEntityContainer, IEnumerable<EdmError>> ComputeErrorsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeErrors();

		// Token: 0x040003E4 RID: 996
		private readonly Cache<CsdlSemanticsEntityContainer, IEdmEntityContainer> extendsCache = new Cache<CsdlSemanticsEntityContainer, IEdmEntityContainer>();

		// Token: 0x040003E5 RID: 997
		private static readonly Func<CsdlSemanticsEntityContainer, IEdmEntityContainer> ComputeExtendsFunc = (CsdlSemanticsEntityContainer me) => me.ComputeExtends();

		// Token: 0x040003E6 RID: 998
		private static readonly Func<CsdlSemanticsEntityContainer, IEdmEntityContainer> OnCycleExtendsFunc = (CsdlSemanticsEntityContainer me) => new CyclicEntityContainer(me.entityContainer.Extends, me.Location);
	}
}
