using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000054 RID: 84
	public class EdmEntityContainer : EdmElement, IEdmEntityContainer, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600032B RID: 811 RVA: 0x0000A720 File Offset: 0x00008920
		public EdmEntityContainer(string namespaceName, string name)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000A785 File Offset: 0x00008985
		public IEnumerable<IEdmEntityContainerElement> Elements
		{
			get
			{
				return this.containerElements;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000A78D File Offset: 0x0000898D
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000A795 File Offset: 0x00008995
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00008D57 File Offset: 0x00006F57
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.EntityContainer;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000A7A0 File Offset: 0x000089A0
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

		// Token: 0x06000331 RID: 817 RVA: 0x0000A868 File Offset: 0x00008A68
		public virtual EdmEntitySet AddEntitySet(string name, IEdmEntityType elementType)
		{
			EdmEntitySet edmEntitySet = new EdmEntitySet(this, name, elementType);
			this.AddElement(edmEntitySet);
			return edmEntitySet;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000A888 File Offset: 0x00008A88
		public virtual EdmEntitySet AddEntitySet(string name, IEdmEntityType elementType, bool includeInServiceDocument)
		{
			EdmEntitySet edmEntitySet = new EdmEntitySet(this, name, elementType, includeInServiceDocument);
			this.AddElement(edmEntitySet);
			return edmEntitySet;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000A8A8 File Offset: 0x00008AA8
		public virtual EdmSingleton AddSingleton(string name, IEdmEntityType entityType)
		{
			EdmSingleton edmSingleton = new EdmSingleton(this, name, entityType);
			this.AddElement(edmSingleton);
			return edmSingleton;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		public virtual EdmFunctionImport AddFunctionImport(IEdmFunction function)
		{
			EdmFunctionImport edmFunctionImport = new EdmFunctionImport(this, function.Name, function);
			this.AddElement(edmFunctionImport);
			return edmFunctionImport;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000A8EC File Offset: 0x00008AEC
		public virtual EdmFunctionImport AddFunctionImport(string name, IEdmFunction function)
		{
			EdmFunctionImport edmFunctionImport = new EdmFunctionImport(this, name, function);
			this.AddElement(edmFunctionImport);
			return edmFunctionImport;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000A90C File Offset: 0x00008B0C
		public virtual EdmFunctionImport AddFunctionImport(string name, IEdmFunction function, IEdmExpression entitySet)
		{
			EdmFunctionImport edmFunctionImport = new EdmFunctionImport(this, name, function, entitySet, false);
			this.AddElement(edmFunctionImport);
			return edmFunctionImport;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000A92C File Offset: 0x00008B2C
		public virtual EdmOperationImport AddFunctionImport(string name, IEdmFunction function, IEdmExpression entitySet, bool includeInServiceDocument)
		{
			EdmOperationImport edmOperationImport = new EdmFunctionImport(this, name, function, entitySet, includeInServiceDocument);
			this.AddElement(edmOperationImport);
			return edmOperationImport;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000A950 File Offset: 0x00008B50
		public virtual EdmActionImport AddActionImport(string name, IEdmAction action, IEdmExpression entitySet)
		{
			EdmActionImport edmActionImport = new EdmActionImport(this, name, action, entitySet);
			this.AddElement(edmActionImport);
			return edmActionImport;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000A970 File Offset: 0x00008B70
		public virtual EdmActionImport AddActionImport(IEdmAction action)
		{
			EdmActionImport edmActionImport = new EdmActionImport(this, action.Name, action, null);
			this.AddElement(edmActionImport);
			return edmActionImport;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000A994 File Offset: 0x00008B94
		public virtual EdmActionImport AddActionImport(string name, IEdmAction action)
		{
			EdmActionImport edmActionImport = new EdmActionImport(this, name, action, null);
			this.AddElement(edmActionImport);
			return edmActionImport;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000A9B4 File Offset: 0x00008BB4
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

		// Token: 0x0600033C RID: 828 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		public virtual IEdmSingleton FindSingleton(string singletonName)
		{
			IEdmSingleton edmSingleton;
			if (!this.singletonDictionary.TryGetValue(singletonName, ref edmSingleton))
			{
				return null;
			}
			return edmSingleton;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000AA00 File Offset: 0x00008C00
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

		// Token: 0x040000AA RID: 170
		private readonly string namespaceName;

		// Token: 0x040000AB RID: 171
		private readonly string name;

		// Token: 0x040000AC RID: 172
		private readonly List<IEdmEntityContainerElement> containerElements = new List<IEdmEntityContainerElement>();

		// Token: 0x040000AD RID: 173
		private readonly Dictionary<string, IEdmEntitySet> entitySetDictionary = new Dictionary<string, IEdmEntitySet>();

		// Token: 0x040000AE RID: 174
		private readonly Dictionary<string, IEdmSingleton> singletonDictionary = new Dictionary<string, IEdmSingleton>();

		// Token: 0x040000AF RID: 175
		private readonly Dictionary<string, object> operationImportDictionary = new Dictionary<string, object>();
	}
}
