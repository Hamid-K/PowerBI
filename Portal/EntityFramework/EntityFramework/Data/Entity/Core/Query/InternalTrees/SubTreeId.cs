using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003F1 RID: 1009
	internal class SubTreeId
	{
		// Token: 0x06002F3B RID: 12091 RVA: 0x000957FB File Offset: 0x000939FB
		internal SubTreeId(RuleProcessingContext context, Node node, Node parent, int childIndex)
		{
			this.m_subTreeRoot = node;
			this.m_parent = parent;
			this.m_childIndex = childIndex;
			this.m_hashCode = context.GetHashCode(node);
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x00095826 File Offset: 0x00093A26
		public override int GetHashCode()
		{
			return this.m_hashCode;
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x00095830 File Offset: 0x00093A30
		public override bool Equals(object obj)
		{
			SubTreeId subTreeId = obj as SubTreeId;
			return subTreeId != null && this.m_hashCode == subTreeId.m_hashCode && (subTreeId.m_subTreeRoot == this.m_subTreeRoot || (subTreeId.m_parent == this.m_parent && subTreeId.m_childIndex == this.m_childIndex));
		}

		// Token: 0x04000FE9 RID: 4073
		public Node m_subTreeRoot;

		// Token: 0x04000FEA RID: 4074
		private readonly int m_hashCode;

		// Token: 0x04000FEB RID: 4075
		private readonly Node m_parent;

		// Token: 0x04000FEC RID: 4076
		private readonly int m_childIndex;
	}
}
