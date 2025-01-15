using System;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x020008F3 RID: 2291
	internal class LeafNode : Node
	{
		// Token: 0x0600487F RID: 18559 RVA: 0x001095B0 File Offset: 0x001077B0
		internal LeafNode(Token token, BranchNode branch)
		{
			this._token = token;
			this._branch = branch;
		}

		// Token: 0x06004880 RID: 18560 RVA: 0x00002B16 File Offset: 0x00000D16
		internal override bool IsLeaf()
		{
			return true;
		}

		// Token: 0x04003525 RID: 13605
		internal Token _token;

		// Token: 0x04003526 RID: 13606
		internal BranchNode _branch;
	}
}
