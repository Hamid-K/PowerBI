using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004EA RID: 1258
	internal abstract class Perspective
	{
		// Token: 0x06003E94 RID: 16020 RVA: 0x000D06EE File Offset: 0x000CE8EE
		internal Perspective(MetadataWorkspace metadataWorkspace, DataSpace targetDataspace)
		{
			this._metadataWorkspace = metadataWorkspace;
			this._targetDataspace = targetDataspace;
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x000D0704 File Offset: 0x000CE904
		internal virtual bool TryGetMember(StructuralType type, string memberName, bool ignoreCase, out EdmMember outMember)
		{
			Check.NotEmpty(memberName, "memberName");
			outMember = null;
			return type.Members.TryGetValue(memberName, ignoreCase, out outMember);
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x000D0725 File Offset: 0x000CE925
		internal virtual bool TryGetEnumMember(EnumType type, string memberName, bool ignoreCase, out EnumMember outMember)
		{
			Check.NotEmpty(memberName, "memberName");
			outMember = null;
			return type.Members.TryGetValue(memberName, ignoreCase, out outMember);
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x000D0746 File Offset: 0x000CE946
		internal virtual bool TryGetExtent(EntityContainer entityContainer, string extentName, bool ignoreCase, out EntitySetBase outSet)
		{
			return entityContainer.BaseEntitySets.TryGetValue(extentName, ignoreCase, out outSet);
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x000D0758 File Offset: 0x000CE958
		internal virtual bool TryGetFunctionImport(EntityContainer entityContainer, string functionImportName, bool ignoreCase, out EdmFunction functionImport)
		{
			functionImport = null;
			if (ignoreCase)
			{
				functionImport = entityContainer.FunctionImports.Where((EdmFunction fi) => string.Equals(fi.Name, functionImportName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault<EdmFunction>();
			}
			else
			{
				functionImport = entityContainer.FunctionImports.Where((EdmFunction fi) => fi.Name == functionImportName).SingleOrDefault<EdmFunction>();
			}
			return functionImport != null;
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x000D07BF File Offset: 0x000CE9BF
		internal virtual EntityContainer GetDefaultContainer()
		{
			return null;
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x000D07C2 File Offset: 0x000CE9C2
		internal virtual bool TryGetEntityContainer(string name, bool ignoreCase, out EntityContainer entityContainer)
		{
			return this.MetadataWorkspace.TryGetEntityContainer(name, ignoreCase, this.TargetDataspace, out entityContainer);
		}

		// Token: 0x06003E9B RID: 16027
		internal abstract bool TryGetTypeByName(string fullName, bool ignoreCase, out TypeUsage typeUsage);

		// Token: 0x06003E9C RID: 16028 RVA: 0x000D07D8 File Offset: 0x000CE9D8
		internal bool TryGetFunctionByName(string namespaceName, string functionName, bool ignoreCase, out IList<EdmFunction> functionOverloads)
		{
			Check.NotEmpty(namespaceName, "namespaceName");
			Check.NotEmpty(functionName, "functionName");
			string text = namespaceName + "." + functionName;
			ItemCollection itemCollection = this._metadataWorkspace.GetItemCollection(this._targetDataspace);
			IList<EdmFunction> list = ((this._targetDataspace == DataSpace.SSpace) ? ((StoreItemCollection)itemCollection).GetCTypeFunctions(text, ignoreCase) : itemCollection.GetFunctions(text, ignoreCase));
			if (this._targetDataspace == DataSpace.CSpace)
			{
				EntityContainer entityContainer;
				EdmFunction edmFunction;
				if ((list == null || list.Count == 0) && this.TryGetEntityContainer(namespaceName, false, out entityContainer) && this.TryGetFunctionImport(entityContainer, functionName, false, out edmFunction))
				{
					list = new EdmFunction[] { edmFunction };
				}
				ItemCollection itemCollection2;
				if ((list == null || list.Count == 0) && this._metadataWorkspace.TryGetItemCollection(DataSpace.SSpace, out itemCollection2))
				{
					list = ((StoreItemCollection)itemCollection2).GetCTypeFunctions(text, ignoreCase);
				}
			}
			functionOverloads = ((list != null && list.Count > 0) ? list : null);
			return functionOverloads != null;
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06003E9D RID: 16029 RVA: 0x000D08B9 File Offset: 0x000CEAB9
		internal MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this._metadataWorkspace;
			}
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x000D08C1 File Offset: 0x000CEAC1
		internal virtual bool TryGetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind, out PrimitiveType primitiveType)
		{
			primitiveType = this._metadataWorkspace.GetMappedPrimitiveType(primitiveTypeKind, DataSpace.CSpace);
			return primitiveType != null;
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06003E9F RID: 16031 RVA: 0x000D08D7 File Offset: 0x000CEAD7
		internal DataSpace TargetDataspace
		{
			get
			{
				return this._targetDataspace;
			}
		}

		// Token: 0x04001543 RID: 5443
		private readonly MetadataWorkspace _metadataWorkspace;

		// Token: 0x04001544 RID: 5444
		private readonly DataSpace _targetDataspace;
	}
}
