using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B3 RID: 179
	public class EdmEntityContainer : EdmElement, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmFullNamedElement
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0000ABAC File Offset: 0x00008DAC
		public EdmEntityContainer(string namespaceName, string name)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(this.namespaceName, this.Name);
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000AC28 File Offset: 0x00008E28
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return this.containerElements;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000AC30 File Offset: 0x00008E30
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000AC38 File Offset: 0x00008E38
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000AC40 File Offset: 0x00008E40
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x000039FB File Offset: 0x00001BFB
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000AC48 File Offset: 0x00008E48
		public void AddElement(IEdmEntityContainerElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityContainerElement>(element, "element");
			this.containerElements.Add(element);
			switch (element.ContainerElementKind)
			{
			case EdmContainerElementKind.None:
				throw new InvalidOperationException(Strings.EdmEntityContainer_CannotUseElementWithTypeNone);
			case EdmContainerElementKind.EntitySet:
				RegistrationHelper.AddElement<IEdmEntitySet>((IEdmEntitySet)element, element.Name, this.entitySetDictionary, new Func<IEdmEntitySet, IEdmEntitySet, IEdmEntitySet>(RegistrationHelper.CreateAmbiguousEntitySetBinding));
				return;
			case EdmContainerElementKind.ActionImport:
			case EdmContainerElementKind.FunctionImport:
				RegistrationHelper.AddOperationImport((IEdmOperationImport)element, element.Name, this.operationImportDictionary);
				return;
			case EdmContainerElementKind.Singleton:
				RegistrationHelper.AddElement<IEdmSingleton>((IEdmSingleton)element, element.Name, this.singletonDictionary, new Func<IEdmSingleton, IEdmSingleton, IEdmSingleton>(RegistrationHelper.CreateAmbiguousSingletonBinding));
				return;
			default:
				throw new InvalidOperationException(Strings.UnknownEnumVal_ContainerElementKind(element.ContainerElementKind));
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000AD10 File Offset: 0x00008F10
		public virtual EdmEntitySet AddEntitySet(string name, IEdmEntityType elementType)
		{
			EdmEntitySet edmEntitySet = new EdmEntitySet(this, name, elementType);
			this.AddElement(edmEntitySet);
			return edmEntitySet;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000AD30 File Offset: 0x00008F30
		public virtual EdmEntitySet AddEntitySet(string name, IEdmEntityType elementType, bool includeInServiceDocument)
		{
			EdmEntitySet edmEntitySet = new EdmEntitySet(this, name, elementType, includeInServiceDocument);
			this.AddElement(edmEntitySet);
			return edmEntitySet;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000AD50 File Offset: 0x00008F50
		public virtual EdmSingleton AddSingleton(string name, IEdmEntityType entityType)
		{
			EdmSingleton edmSingleton = new EdmSingleton(this, name, entityType);
			this.AddElement(edmSingleton);
			return edmSingleton;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000AD70 File Offset: 0x00008F70
		public virtual EdmFunctionImport AddFunctionImport(IEdmFunction function)
		{
			EdmFunctionImport edmFunctionImport = new EdmFunctionImport(this, function.Name, function);
			this.AddElement(edmFunctionImport);
			return edmFunctionImport;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000AD94 File Offset: 0x00008F94
		public virtual EdmFunctionImport AddFunctionImport(string name, IEdmFunction function)
		{
			EdmFunctionImport edmFunctionImport = new EdmFunctionImport(this, name, function);
			this.AddElement(edmFunctionImport);
			return edmFunctionImport;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		public virtual EdmFunctionImport AddFunctionImport(string name, IEdmFunction function, IEdmExpression entitySet)
		{
			EdmFunctionImport edmFunctionImport = new EdmFunctionImport(this, name, function, entitySet, false);
			this.AddElement(edmFunctionImport);
			return edmFunctionImport;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		public virtual EdmOperationImport AddFunctionImport(string name, IEdmFunction function, IEdmExpression entitySet, bool includeInServiceDocument)
		{
			EdmOperationImport edmOperationImport = new EdmFunctionImport(this, name, function, entitySet, includeInServiceDocument);
			this.AddElement(edmOperationImport);
			return edmOperationImport;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		public virtual EdmActionImport AddActionImport(string name, IEdmAction action, IEdmExpression entitySet)
		{
			EdmActionImport edmActionImport = new EdmActionImport(this, name, action, entitySet);
			this.AddElement(edmActionImport);
			return edmActionImport;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000AE18 File Offset: 0x00009018
		public virtual EdmActionImport AddActionImport(IEdmAction action)
		{
			EdmActionImport edmActionImport = new EdmActionImport(this, action.Name, action, null);
			this.AddElement(edmActionImport);
			return edmActionImport;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000AE3C File Offset: 0x0000903C
		public virtual EdmActionImport AddActionImport(string name, IEdmAction action)
		{
			EdmActionImport edmActionImport = new EdmActionImport(this, name, action, null);
			this.AddElement(edmActionImport);
			return edmActionImport;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000AE5C File Offset: 0x0000905C
		public virtual IEdmEntitySet FindEntitySet(string setName)
		{
			if (string.IsNullOrEmpty(setName))
			{
				return null;
			}
			IEdmEntitySet edmEntitySet;
			if (!this.entitySetDictionary.TryGetValue(setName, out edmEntitySet))
			{
				return null;
			}
			return edmEntitySet;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000AE88 File Offset: 0x00009088
		public virtual IEdmSingleton FindSingleton(string singletonName)
		{
			IEdmSingleton edmSingleton;
			if (!this.singletonDictionary.TryGetValue(singletonName, out edmSingleton))
			{
				return null;
			}
			return edmSingleton;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000AEA8 File Offset: 0x000090A8
		public IEnumerable<IEdmOperationImport> FindOperationImports(string operationName)
		{
			object obj;
			if (!this.operationImportDictionary.TryGetValue(operationName, out obj))
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

		// Token: 0x04000141 RID: 321
		private readonly string namespaceName;

		// Token: 0x04000142 RID: 322
		private readonly string name;

		// Token: 0x04000143 RID: 323
		private readonly string fullName;

		// Token: 0x04000144 RID: 324
		private readonly List<IEdmEntityContainerElement> containerElements = new List<IEdmEntityContainerElement>();

		// Token: 0x04000145 RID: 325
		private readonly Dictionary<string, IEdmEntitySet> entitySetDictionary = new Dictionary<string, IEdmEntitySet>();

		// Token: 0x04000146 RID: 326
		private readonly Dictionary<string, IEdmSingleton> singletonDictionary = new Dictionary<string, IEdmSingleton>();

		// Token: 0x04000147 RID: 327
		private readonly Dictionary<string, object> operationImportDictionary = new Dictionary<string, object>();
	}
}
