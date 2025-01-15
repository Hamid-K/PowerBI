using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003C1 RID: 961
	internal class NodeCounter : BasicOpVisitorOfT<int>
	{
		// Token: 0x06002DFC RID: 11772 RVA: 0x00092745 File Offset: 0x00090945
		internal static int Count(Node subTree)
		{
			return new NodeCounter().VisitNode(subTree);
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x00092754 File Offset: 0x00090954
		protected override int VisitDefault(Node n)
		{
			int num = 1;
			foreach (Node node in n.Children)
			{
				num += base.VisitNode(node);
			}
			return num;
		}
	}
}
