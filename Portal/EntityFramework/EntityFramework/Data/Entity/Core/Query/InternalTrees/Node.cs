using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003C0 RID: 960
	internal class Node
	{
		// Token: 0x06002DE7 RID: 11751 RVA: 0x0009254F File Offset: 0x0009074F
		internal Node(int nodeId, Op op, List<Node> children)
		{
			this.m_id = nodeId;
			this.Op = op;
			this.m_children = children;
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x0009256C File Offset: 0x0009076C
		internal Node(Op op, params Node[] children)
			: this(-1, op, new List<Node>(children))
		{
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x0009257C File Offset: 0x0009077C
		internal List<Node> Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002DEA RID: 11754 RVA: 0x00092584 File Offset: 0x00090784
		// (set) Token: 0x06002DEB RID: 11755 RVA: 0x0009258C File Offset: 0x0009078C
		internal Op Op { get; set; }

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002DEC RID: 11756 RVA: 0x00092595 File Offset: 0x00090795
		// (set) Token: 0x06002DED RID: 11757 RVA: 0x000925A3 File Offset: 0x000907A3
		internal Node Child0
		{
			get
			{
				return this.m_children[0];
			}
			set
			{
				this.m_children[0] = value;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x000925B2 File Offset: 0x000907B2
		internal bool HasChild0
		{
			get
			{
				return this.m_children.Count > 0;
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000925C2 File Offset: 0x000907C2
		// (set) Token: 0x06002DF0 RID: 11760 RVA: 0x000925D0 File Offset: 0x000907D0
		internal Node Child1
		{
			get
			{
				return this.m_children[1];
			}
			set
			{
				this.m_children[1] = value;
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x000925DF File Offset: 0x000907DF
		internal bool HasChild1
		{
			get
			{
				return this.m_children.Count > 1;
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x000925EF File Offset: 0x000907EF
		// (set) Token: 0x06002DF3 RID: 11763 RVA: 0x000925FD File Offset: 0x000907FD
		internal Node Child2
		{
			get
			{
				return this.m_children[2];
			}
			set
			{
				this.m_children[2] = value;
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002DF4 RID: 11764 RVA: 0x0009260C File Offset: 0x0009080C
		internal Node Child3
		{
			get
			{
				return this.m_children[3];
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x0009261A File Offset: 0x0009081A
		internal bool HasChild2
		{
			get
			{
				return this.m_children.Count > 2;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x0009262A File Offset: 0x0009082A
		internal bool HasChild3
		{
			get
			{
				return this.m_children.Count > 3;
			}
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x0009263C File Offset: 0x0009083C
		internal bool IsEquivalent(Node other)
		{
			if (this.Children.Count != other.Children.Count)
			{
				return false;
			}
			bool? flag = new bool?(this.Op.IsEquivalent(other.Op));
			bool flag2 = true;
			if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
			{
				return false;
			}
			for (int i = 0; i < this.Children.Count; i++)
			{
				if (!this.Children[i].IsEquivalent(other.Children[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x000926CA File Offset: 0x000908CA
		internal bool IsNodeInfoInitialized
		{
			get
			{
				return this.m_nodeInfo != null;
			}
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000926D5 File Offset: 0x000908D5
		internal NodeInfo GetNodeInfo(Command command)
		{
			if (this.m_nodeInfo == null)
			{
				this.InitializeNodeInfo(command);
			}
			return this.m_nodeInfo;
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x000926EC File Offset: 0x000908EC
		internal ExtendedNodeInfo GetExtendedNodeInfo(Command command)
		{
			if (this.m_nodeInfo == null)
			{
				this.InitializeNodeInfo(command);
			}
			return this.m_nodeInfo as ExtendedNodeInfo;
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x00092708 File Offset: 0x00090908
		private void InitializeNodeInfo(Command command)
		{
			if (this.Op.IsRelOp || this.Op.IsPhysicalOp)
			{
				this.m_nodeInfo = new ExtendedNodeInfo(command);
			}
			else
			{
				this.m_nodeInfo = new NodeInfo(command);
			}
			command.RecomputeNodeInfo(this);
		}

		// Token: 0x04000F58 RID: 3928
		private readonly int m_id;

		// Token: 0x04000F59 RID: 3929
		private readonly List<Node> m_children;

		// Token: 0x04000F5A RID: 3930
		private NodeInfo m_nodeInfo;
	}
}
