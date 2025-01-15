using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001FC RID: 508
	public class EdmEntityContainer : EdmElement, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000BE1 RID: 3041 RVA: 0x00021B14 File Offset: 0x0001FD14
		public EdmEntityContainer(string namespaceName, string name)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x00021B79 File Offset: 0x0001FD79
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return this.containerElements;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x00021B81 File Offset: 0x0001FD81
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00021B89 File Offset: 0x0001FD89
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00021B91 File Offset: 0x0001FD91
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00021B94 File Offset: 0x0001FD94
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

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00021C5C File Offset: 0x0001FE5C
		public virtual EdmEntitySet AddEntitySet(string name, IEdmEntityType elementType)
		{
			EdmEntitySet edmEntitySet = new EdmEntitySet(this, name, elementType);
			this.AddElement(edmEntitySet);
			return edmEntitySet;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00021C7C File Offset: 0x0001FE7C
		public virtual EdmSingleton AddSingleton(string name, IEdmEntityType entityType)
		{
			EdmSingleton edmSingleton = new EdmSingleton(this, name, entityType);
			this.AddElement(edmSingleton);
			return edmSingleton;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00021C9C File Offset: 0x0001FE9C
		public virtual EdmFunctionImport AddFunctionImport(IEdmFunction function)
		{
			EdmFunctionImport edmFunctionImport = new EdmFunctionImport(this, function.Name, function);
			this.AddElement(edmFunctionImport);
			return edmFunctionImport;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00021CC0 File Offset: 0x0001FEC0
		public virtual EdmFunctionImport AddFunctionImport(string name, IEdmFunction function)
		{
			EdmFunctionImport edmFunctionImport = new EdmFunctionImport(this, name, function);
			this.AddElement(edmFunctionImport);
			return edmFunctionImport;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00021CE0 File Offset: 0x0001FEE0
		public virtual EdmFunctionImport AddFunctionImport(string name, IEdmFunction function, IEdmExpression entitySet)
		{
			EdmFunctionImport edmFunctionImport = new EdmFunctionImport(this, name, function, entitySet, false);
			this.AddElement(edmFunctionImport);
			return edmFunctionImport;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00021D00 File Offset: 0x0001FF00
		public virtual EdmOperationImport AddFunctionImport(string name, IEdmFunction function, IEdmExpression entitySet, bool includeInServiceDocument)
		{
			EdmOperationImport edmOperationImport = new EdmFunctionImport(this, name, function, entitySet, includeInServiceDocument);
			this.AddElement(edmOperationImport);
			return edmOperationImport;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00021D24 File Offset: 0x0001FF24
		public virtual EdmActionImport AddActionImport(string name, IEdmAction action, IEdmExpression entitySet)
		{
			EdmActionImport edmActionImport = new EdmActionImport(this, name, action, entitySet);
			this.AddElement(edmActionImport);
			return edmActionImport;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00021D44 File Offset: 0x0001FF44
		public virtual EdmActionImport AddActionImport(IEdmAction action)
		{
			EdmActionImport edmActionImport = new EdmActionImport(this, action.Name, action, null);
			this.AddElement(edmActionImport);
			return edmActionImport;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00021D68 File Offset: 0x0001FF68
		public virtual EdmActionImport AddActionImport(string name, IEdmAction action)
		{
			EdmActionImport edmActionImport = new EdmActionImport(this, name, action, null);
			this.AddElement(edmActionImport);
			return edmActionImport;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00021D88 File Offset: 0x0001FF88
		public virtual IEdmEntitySet FindEntitySet(string setName)
		{
			if (string.IsNullOrEmpty(setName))
			{
				return null;
			}
			IEdmEntitySet edmEntitySet;
			if (!this.entitySetDictionary.TryGetValue(setName, ref edmEntitySet))
			{
				return null;
			}
			return edmEntitySet;
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00021DB4 File Offset: 0x0001FFB4
		public virtual IEdmSingleton FindSingleton(string singletonName)
		{
			IEdmSingleton edmSingleton;
			if (!this.singletonDictionary.TryGetValue(singletonName, ref edmSingleton))
			{
				return null;
			}
			return edmSingleton;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00021DD4 File Offset: 0x0001FFD4
		public IEnumerable<IEdmOperationImport> FindOperationImports(string operationName)
		{
			object obj;
			if (!this.operationImportDictionary.TryGetValue(operationName, ref obj))
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

		// Token: 0x04000573 RID: 1395
		private readonly string namespaceName;

		// Token: 0x04000574 RID: 1396
		private readonly string name;

		// Token: 0x04000575 RID: 1397
		private readonly List<IEdmEntityContainerElement> containerElements = new List<IEdmEntityContainerElement>();

		// Token: 0x04000576 RID: 1398
		private readonly Dictionary<string, IEdmEntitySet> entitySetDictionary = new Dictionary<string, IEdmEntitySet>();

		// Token: 0x04000577 RID: 1399
		private readonly Dictionary<string, IEdmSingleton> singletonDictionary = new Dictionary<string, IEdmSingleton>();

		// Token: 0x04000578 RID: 1400
		private readonly Dictionary<string, object> operationImportDictionary = new Dictionary<string, object>();
	}
}
