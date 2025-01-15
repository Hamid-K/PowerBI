using System;

namespace System.Data.Entity.Core.Metadata.Edm.Provider
{
	// Token: 0x0200051B RID: 1307
	internal static class EdmProviderManifestHierarchyIdFunctions
	{
		// Token: 0x0600406B RID: 16491 RVA: 0x000D8D40 File Offset: 0x000D6F40
		internal static void AddFunctions(EdmProviderManifestFunctionBuilder functions)
		{
			functions.AddFunction(PrimitiveTypeKind.HierarchyId, "HierarchyIdGetRoot");
			functions.AddFunction(PrimitiveTypeKind.HierarchyId, "HierarchyIdParse", PrimitiveTypeKind.String, "input");
			functions.AddFunction(PrimitiveTypeKind.HierarchyId, "GetAncestor", PrimitiveTypeKind.HierarchyId, "hierarchyIdValue", PrimitiveTypeKind.Int32, "n");
			functions.AddFunction(PrimitiveTypeKind.HierarchyId, "GetDescendant", PrimitiveTypeKind.HierarchyId, "hierarchyIdValue", PrimitiveTypeKind.HierarchyId, "child1", PrimitiveTypeKind.HierarchyId, "child2");
			functions.AddFunction(PrimitiveTypeKind.Int16, "GetLevel", PrimitiveTypeKind.HierarchyId, "hierarchyIdValue");
			functions.AddFunction(PrimitiveTypeKind.Boolean, "IsDescendantOf", PrimitiveTypeKind.HierarchyId, "hierarchyIdValue", PrimitiveTypeKind.HierarchyId, "parent");
			functions.AddFunction(PrimitiveTypeKind.HierarchyId, "GetReparentedValue", PrimitiveTypeKind.HierarchyId, "hierarchyIdValue", PrimitiveTypeKind.HierarchyId, "oldRoot", PrimitiveTypeKind.HierarchyId, "newRoot");
		}
	}
}
