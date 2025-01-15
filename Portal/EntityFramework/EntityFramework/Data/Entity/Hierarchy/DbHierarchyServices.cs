using System;

namespace System.Data.Entity.Hierarchy
{
	// Token: 0x020002C8 RID: 712
	[Serializable]
	public abstract class DbHierarchyServices
	{
		// Token: 0x0600223B RID: 8763
		public abstract HierarchyId GetAncestor(int n);

		// Token: 0x0600223C RID: 8764
		public abstract HierarchyId GetDescendant(HierarchyId child1, HierarchyId child2);

		// Token: 0x0600223D RID: 8765
		public abstract short GetLevel();

		// Token: 0x0600223E RID: 8766 RVA: 0x00060A79 File Offset: 0x0005EC79
		public static HierarchyId GetRoot()
		{
			return new HierarchyId("/");
		}

		// Token: 0x0600223F RID: 8767
		public abstract bool IsDescendantOf(HierarchyId parent);

		// Token: 0x06002240 RID: 8768
		public abstract HierarchyId GetReparentedValue(HierarchyId oldRoot, HierarchyId newRoot);

		// Token: 0x06002241 RID: 8769 RVA: 0x00060A85 File Offset: 0x0005EC85
		public static HierarchyId Parse(string input)
		{
			return new HierarchyId(input);
		}
	}
}
