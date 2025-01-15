using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E8B RID: 7819
	public struct inorderAllNodes : IProgramNodeBuilder, IEquatable<inorderAllNodes>
	{
		// Token: 0x17002BE4 RID: 11236
		// (get) Token: 0x0601084A RID: 67658 RVA: 0x0038D63A File Offset: 0x0038B83A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601084B RID: 67659 RVA: 0x0038D642 File Offset: 0x0038B842
		private inorderAllNodes(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601084C RID: 67660 RVA: 0x0038D64B File Offset: 0x0038B84B
		public static inorderAllNodes CreateUnsafe(ProgramNode node)
		{
			return new inorderAllNodes(node);
		}

		// Token: 0x0601084D RID: 67661 RVA: 0x0038D654 File Offset: 0x0038B854
		public static inorderAllNodes? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.inorderAllNodes)
			{
				return null;
			}
			return new inorderAllNodes?(inorderAllNodes.CreateUnsafe(node));
		}

		// Token: 0x0601084E RID: 67662 RVA: 0x0038D68E File Offset: 0x0038B88E
		public static inorderAllNodes CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new inorderAllNodes(new Hole(g.Symbol.inorderAllNodes, holeId));
		}

		// Token: 0x0601084F RID: 67663 RVA: 0x0038D6A6 File Offset: 0x0038B8A6
		public InOrderAllNodes Cast_InOrderAllNodes()
		{
			return InOrderAllNodes.CreateUnsafe(this.Node);
		}

		// Token: 0x06010850 RID: 67664 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_InOrderAllNodes(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06010851 RID: 67665 RVA: 0x0038D6B3 File Offset: 0x0038B8B3
		public bool Is_InOrderAllNodes(GrammarBuilders g, out InOrderAllNodes value)
		{
			value = InOrderAllNodes.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06010852 RID: 67666 RVA: 0x0038D6C7 File Offset: 0x0038B8C7
		public InOrderAllNodes? As_InOrderAllNodes(GrammarBuilders g)
		{
			return new InOrderAllNodes?(InOrderAllNodes.CreateUnsafe(this.Node));
		}

		// Token: 0x06010853 RID: 67667 RVA: 0x0038D6D9 File Offset: 0x0038B8D9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010854 RID: 67668 RVA: 0x0038D6EC File Offset: 0x0038B8EC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010855 RID: 67669 RVA: 0x0038D716 File Offset: 0x0038B916
		public bool Equals(inorderAllNodes other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062CA RID: 25290
		private ProgramNode _node;
	}
}
