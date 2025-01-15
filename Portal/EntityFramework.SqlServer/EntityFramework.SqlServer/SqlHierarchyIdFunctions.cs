using System;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.SqlServer.Resources;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x0200000E RID: 14
	public static class SqlHierarchyIdFunctions
	{
		// Token: 0x060000EA RID: 234 RVA: 0x00003B04 File Offset: 0x00001D04
		[DbFunction("SqlServer", "GetAncestor")]
		public static HierarchyId GetAncestor(HierarchyId hierarchyIdValue, int n)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003B10 File Offset: 0x00001D10
		[DbFunction("SqlServer", "GetDescendant")]
		public static HierarchyId GetDescendant(HierarchyId hierarchyIdValue, HierarchyId child1, HierarchyId child2)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003B1C File Offset: 0x00001D1C
		[DbFunction("SqlServer", "GetLevel")]
		public static short GetLevel(HierarchyId hierarchyIdValue)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003B28 File Offset: 0x00001D28
		[DbFunction("SqlServer", "GetRoot")]
		public static HierarchyId GetRoot()
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003B34 File Offset: 0x00001D34
		[DbFunction("SqlServer", "IsDescendantOf")]
		public static bool IsDescendantOf(HierarchyId hierarchyIdValue, HierarchyId parent)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003B40 File Offset: 0x00001D40
		[DbFunction("SqlServer", "GetReparentedValue")]
		public static HierarchyId GetReparentedValue(HierarchyId hierarchyIdValue, HierarchyId oldRoot, HierarchyId newRoot)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003B4C File Offset: 0x00001D4C
		[DbFunction("SqlServer", "Parse")]
		public static HierarchyId Parse(string input)
		{
			throw new NotSupportedException(Strings.ELinq_DbFunctionDirectCall);
		}
	}
}
