using System;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x020008F0 RID: 2288
	internal class TokenTree
	{
		// Token: 0x06004870 RID: 18544 RVA: 0x0010917A File Offset: 0x0010737A
		internal TokenTree()
		{
			this.rootNode = new BranchNode(0);
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x0010918E File Offset: 0x0010738E
		internal bool AddSupportedToken(Token token)
		{
			return this.FindBranchNode(token._tokenTag).AddToken(token);
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x001091A4 File Offset: 0x001073A4
		internal Token ParseToken(string stringInput, int startIndex, ref int shiftOffset)
		{
			LeafNode leafNode = this.FindLeafNode(stringInput, startIndex, ref shiftOffset);
			if (leafNode != null)
			{
				return leafNode._token;
			}
			return null;
		}

		// Token: 0x06004873 RID: 18547 RVA: 0x001091C8 File Offset: 0x001073C8
		private int GetNextChar(string stringInput, int currentIndex)
		{
			for (int i = currentIndex + 1; i < stringInput.Length; i++)
			{
				if (stringInput[i] != ' ' || (i > currentIndex && stringInput[i - 1] != ' '))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06004874 RID: 18548 RVA: 0x00109208 File Offset: 0x00107408
		private BranchNode FindBranchNode(string stringInput)
		{
			int num = 0;
			return (BranchNode)this.FindNode(stringInput, 0, true, ref num);
		}

		// Token: 0x06004875 RID: 18549 RVA: 0x00109227 File Offset: 0x00107427
		private LeafNode FindLeafNode(string stringInput, int startIndex, ref int shiftOffset)
		{
			return (LeafNode)this.FindNode(stringInput, startIndex, false, ref shiftOffset);
		}

		// Token: 0x06004876 RID: 18550 RVA: 0x00109238 File Offset: 0x00107438
		private Node FindNode(string stringInput, int startIndex, bool isBranchNode, ref int shiftOffset)
		{
			BranchNode branchNode = this.rootNode;
			shiftOffset = 0;
			int i = startIndex;
			while (i >= 0)
			{
				Node node = branchNode.GetNode(stringInput[i]);
				if (node == null)
				{
					if (isBranchNode)
					{
						return branchNode;
					}
					if (!Node.IsDelimiter(stringInput[i]))
					{
						return null;
					}
					return branchNode._leafNode;
				}
				else if (node.IsLeaf())
				{
					if (isBranchNode)
					{
						return branchNode;
					}
					LeafNode leafNode = (LeafNode)node;
					string tokenTag = leafNode._token._tokenTag;
					int num = leafNode._branch._effectiveIndex + 1;
					int num2 = this.GetNextChar(stringInput, i);
					char c = ((i < stringInput.Length - 1) ? stringInput[i + 1] : ' ');
					if (num2 > 0)
					{
						shiftOffset = num2 - startIndex;
					}
					else
					{
						shiftOffset = stringInput.Length - startIndex;
					}
					while (num2 >= 0 && num < tokenTag.Length)
					{
						if (char.ToUpperInvariant(stringInput[num2]) != char.ToUpperInvariant(tokenTag[num]))
						{
							return null;
						}
						c = ((num2 < stringInput.Length - 1) ? stringInput[num2 + 1] : ' ');
						num++;
						num2 = this.GetNextChar(stringInput, num2);
						if (num2 == -1)
						{
							shiftOffset = stringInput.Length - startIndex;
						}
						else
						{
							shiftOffset = num2 - startIndex;
						}
					}
					if (num != tokenTag.Length)
					{
						return null;
					}
					if (!char.IsWhiteSpace(tokenTag, num - 1) && !Node.IsDelimiter(c))
					{
						return null;
					}
					return node;
				}
				else
				{
					branchNode = (BranchNode)node;
					i = this.GetNextChar(stringInput, i);
					if (i > -1)
					{
						shiftOffset = i - startIndex;
					}
					else
					{
						shiftOffset = stringInput.Length - startIndex;
					}
				}
			}
			if (!isBranchNode && branchNode != null && branchNode.GetType() == typeof(BranchNode))
			{
				return branchNode._leafNode;
			}
			return branchNode;
		}

		// Token: 0x04003520 RID: 13600
		private BranchNode rootNode;
	}
}
