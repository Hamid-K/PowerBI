using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree
{
	// Token: 0x020000EE RID: 238
	public static class NodeExtensions
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x000124A8 File Offset: 0x000106A8
		public static bool IsTrueHole(this Node node)
		{
			HoleNode holeNode = node as HoleNode;
			return holeNode != null && holeNode.Type == HoleNodeType.TrueHole;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000124CC File Offset: 0x000106CC
		public static bool IsSameNode(this Node node)
		{
			HoleNode holeNode = node as HoleNode;
			return holeNode != null && holeNode.Type == HoleNodeType.SameNode;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000124F0 File Offset: 0x000106F0
		public static bool IsTrueHole(this Node node, out HoleNode holeNode)
		{
			HoleNode holeNode2 = node as HoleNode;
			if (holeNode2 != null && holeNode2.Type == HoleNodeType.TrueHole)
			{
				holeNode = holeNode2;
				return true;
			}
			holeNode = null;
			return false;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00012518 File Offset: 0x00010718
		public static bool IsSameNode(this Node node, out HoleNode holeNode)
		{
			HoleNode holeNode2 = node as HoleNode;
			if (holeNode2 != null && holeNode2.Type == HoleNodeType.SameNode)
			{
				holeNode = holeNode2;
				return true;
			}
			holeNode = null;
			return false;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00012544 File Offset: 0x00010744
		public static bool RootEquals(this Node node1, Node node2)
		{
			if (node1.GetType() != node2.GetType())
			{
				return false;
			}
			if (node1.Label != node2.Label || !node1.Attributes.Equals(node2.Attributes))
			{
				return false;
			}
			SequenceNode sequenceNode = node1 as SequenceNode;
			if (sequenceNode != null)
			{
				SequenceNode sequenceNode2 = node2 as SequenceNode;
				if (sequenceNode2 != null && !sequenceNode.Separator.Equals(sequenceNode2.Separator))
				{
					return false;
				}
			}
			return true;
		}
	}
}
