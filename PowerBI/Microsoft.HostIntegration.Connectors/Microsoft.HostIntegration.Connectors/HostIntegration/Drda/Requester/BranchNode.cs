using System;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x020008F2 RID: 2290
	internal class BranchNode : Node
	{
		// Token: 0x0600487B RID: 18555 RVA: 0x00109448 File Offset: 0x00107648
		internal BranchNode(int effectiveIndex)
		{
			this._treeNodes = new Node[28];
			this._effectiveIndex = effectiveIndex;
			this._leafNode = null;
			for (int i = 0; i < 28; i++)
			{
				this._treeNodes[i] = null;
			}
		}

		// Token: 0x0600487C RID: 18556 RVA: 0x0010948C File Offset: 0x0010768C
		internal bool AddToken(Token token)
		{
			char c = char.ToUpperInvariant(token._tokenTag[this._effectiveIndex]);
			int indexFromChar = base.GetIndexFromChar(c);
			if (this._treeNodes[indexFromChar] == null)
			{
				this._treeNodes[indexFromChar] = new LeafNode(token, this);
				return true;
			}
			LeafNode leafNode = (LeafNode)this._treeNodes[indexFromChar];
			if (string.Compare(token._tokenTag, leafNode._token._tokenTag, true) == 0)
			{
				return false;
			}
			BranchNode branchNode = new BranchNode(this._effectiveIndex + 1);
			this._treeNodes[indexFromChar] = branchNode;
			if (!branchNode.AddLeafNode(leafNode))
			{
				branchNode._leafNode = leafNode;
			}
			return branchNode.AddToken(token);
		}

		// Token: 0x0600487D RID: 18557 RVA: 0x0010952C File Offset: 0x0010772C
		private bool AddLeafNode(LeafNode node)
		{
			if (this._effectiveIndex >= node._token._tokenTag.Length)
			{
				return false;
			}
			int indexFromChar = base.GetIndexFromChar(char.ToUpperInvariant(node._token._tokenTag[this._effectiveIndex]));
			node._branch = this;
			this._treeNodes[indexFromChar] = node;
			return true;
		}

		// Token: 0x0600487E RID: 18558 RVA: 0x00109588 File Offset: 0x00107788
		internal Node GetNode(char c)
		{
			int indexFromChar = base.GetIndexFromChar(char.ToUpperInvariant(c));
			if (indexFromChar >= 0)
			{
				return this._treeNodes[indexFromChar];
			}
			return null;
		}

		// Token: 0x04003522 RID: 13602
		private Node[] _treeNodes;

		// Token: 0x04003523 RID: 13603
		internal int _effectiveIndex;

		// Token: 0x04003524 RID: 13604
		internal LeafNode _leafNode;
	}
}
