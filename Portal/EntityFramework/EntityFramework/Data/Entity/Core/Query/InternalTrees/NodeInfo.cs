using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003C2 RID: 962
	internal class NodeInfo
	{
		// Token: 0x06002DFF RID: 11775 RVA: 0x000927B8 File Offset: 0x000909B8
		internal NodeInfo(Command cmd)
		{
			this.m_externalReferences = cmd.CreateVarVec();
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x000927CC File Offset: 0x000909CC
		internal virtual void Clear()
		{
			this.m_externalReferences.Clear();
			this.m_hashValue = 0;
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06002E01 RID: 11777 RVA: 0x000927E0 File Offset: 0x000909E0
		internal VarVec ExternalReferences
		{
			get
			{
				return this.m_externalReferences;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x000927E8 File Offset: 0x000909E8
		internal int HashValue
		{
			get
			{
				return this.m_hashValue;
			}
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x000927F0 File Offset: 0x000909F0
		internal static int GetHashValue(VarVec vec)
		{
			int num = 0;
			foreach (Var var in vec)
			{
				num ^= var.GetHashCode();
			}
			return num;
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x00092840 File Offset: 0x00090A40
		internal virtual void ComputeHashValue(Command cmd, Node n)
		{
			this.m_hashValue = 0;
			foreach (Node node in n.Children)
			{
				NodeInfo nodeInfo = cmd.GetNodeInfo(node);
				this.m_hashValue ^= nodeInfo.HashValue;
			}
			this.m_hashValue = (this.m_hashValue << 4) ^ (int)n.Op.OpType;
			this.m_hashValue = (this.m_hashValue << 4) ^ NodeInfo.GetHashValue(this.m_externalReferences);
		}

		// Token: 0x04000F5C RID: 3932
		private readonly VarVec m_externalReferences;

		// Token: 0x04000F5D RID: 3933
		protected int m_hashValue;
	}
}
