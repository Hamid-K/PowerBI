using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000380 RID: 896
	internal abstract class BasicOpVisitorOfNode : BasicOpVisitorOfT<Node>
	{
		// Token: 0x06002B61 RID: 11105 RVA: 0x0008D9B8 File Offset: 0x0008BBB8
		protected override void VisitChildren(Node n)
		{
			for (int i = 0; i < n.Children.Count; i++)
			{
				n.Children[i] = base.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x0008D9FC File Offset: 0x0008BBFC
		protected override void VisitChildrenReverse(Node n)
		{
			for (int i = n.Children.Count - 1; i >= 0; i--)
			{
				n.Children[i] = base.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x0008DA3F File Offset: 0x0008BC3F
		protected override Node VisitDefault(Node n)
		{
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x0008DA49 File Offset: 0x0008BC49
		protected override Node VisitAncillaryOpDefault(AncillaryOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x0008DA52 File Offset: 0x0008BC52
		protected override Node VisitPhysicalOpDefault(PhysicalOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x0008DA5B File Offset: 0x0008BC5B
		protected override Node VisitRelOpDefault(RelOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x0008DA64 File Offset: 0x0008BC64
		protected override Node VisitScalarOpDefault(ScalarOp op, Node n)
		{
			return this.VisitDefault(n);
		}
	}
}
