using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder.Hierarchy
{
	// Token: 0x020006FC RID: 1788
	public static class HierarchyIdEdmFunctions
	{
		// Token: 0x0600546B RID: 21611 RVA: 0x0012FA34 File Offset: 0x0012DC34
		public static DbFunctionExpression HierarchyIdParse(DbExpression input)
		{
			Check.NotNull<DbExpression>(input, "input");
			return EdmFunctions.InvokeCanonicalFunction("HierarchyIdParse", new DbExpression[] { input });
		}

		// Token: 0x0600546C RID: 21612 RVA: 0x0012FA56 File Offset: 0x0012DC56
		public static DbFunctionExpression HierarchyIdGetRoot()
		{
			return EdmFunctions.InvokeCanonicalFunction("HierarchyIdGetRoot", new DbExpression[0]);
		}

		// Token: 0x0600546D RID: 21613 RVA: 0x0012FA68 File Offset: 0x0012DC68
		public static DbFunctionExpression GetAncestor(this DbExpression hierarchyIdValue, DbExpression n)
		{
			Check.NotNull<DbExpression>(hierarchyIdValue, "hierarchyIdValue");
			Check.NotNull<DbExpression>(n, "n");
			return EdmFunctions.InvokeCanonicalFunction("GetAncestor", new DbExpression[] { hierarchyIdValue, n });
		}

		// Token: 0x0600546E RID: 21614 RVA: 0x0012FA9C File Offset: 0x0012DC9C
		public static DbFunctionExpression GetDescendant(this DbExpression hierarchyIdValue, DbExpression child1, DbExpression child2)
		{
			Check.NotNull<DbExpression>(hierarchyIdValue, "hierarchyIdValue");
			Check.NotNull<DbExpression>(child1, "child1");
			Check.NotNull<DbExpression>(child2, "child2");
			return EdmFunctions.InvokeCanonicalFunction("GetDescendant", new DbExpression[] { hierarchyIdValue, child1, child2 });
		}

		// Token: 0x0600546F RID: 21615 RVA: 0x0012FAE9 File Offset: 0x0012DCE9
		public static DbFunctionExpression GetLevel(this DbExpression hierarchyIdValue)
		{
			Check.NotNull<DbExpression>(hierarchyIdValue, "hierarchyIdValue");
			return EdmFunctions.InvokeCanonicalFunction("GetLevel", new DbExpression[] { hierarchyIdValue });
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x0012FB0B File Offset: 0x0012DD0B
		public static DbFunctionExpression IsDescendantOf(this DbExpression hierarchyIdValue, DbExpression parent)
		{
			Check.NotNull<DbExpression>(hierarchyIdValue, "hierarchyIdValue");
			Check.NotNull<DbExpression>(parent, "parent");
			return EdmFunctions.InvokeCanonicalFunction("IsDescendantOf", new DbExpression[] { hierarchyIdValue, parent });
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x0012FB40 File Offset: 0x0012DD40
		public static DbFunctionExpression GetReparentedValue(this DbExpression hierarchyIdValue, DbExpression oldRoot, DbExpression newRoot)
		{
			Check.NotNull<DbExpression>(hierarchyIdValue, "hierarchyIdValue");
			Check.NotNull<DbExpression>(oldRoot, "oldRoot");
			Check.NotNull<DbExpression>(newRoot, "newRoot");
			return EdmFunctions.InvokeCanonicalFunction("GetReparentedValue", new DbExpression[] { hierarchyIdValue, oldRoot, newRoot });
		}
	}
}
