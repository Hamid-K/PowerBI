using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E8C RID: 7820
	public struct label : IProgramNodeBuilder, IEquatable<label>
	{
		// Token: 0x17002BE5 RID: 11237
		// (get) Token: 0x06010856 RID: 67670 RVA: 0x0038D72A File Offset: 0x0038B92A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010857 RID: 67671 RVA: 0x0038D732 File Offset: 0x0038B932
		private label(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010858 RID: 67672 RVA: 0x0038D73B File Offset: 0x0038B93B
		public static label CreateUnsafe(ProgramNode node)
		{
			return new label(node);
		}

		// Token: 0x06010859 RID: 67673 RVA: 0x0038D744 File Offset: 0x0038B944
		public static label? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.label)
			{
				return null;
			}
			return new label?(label.CreateUnsafe(node));
		}

		// Token: 0x0601085A RID: 67674 RVA: 0x0038D77E File Offset: 0x0038B97E
		public static label CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new label(new Hole(g.Symbol.label, holeId));
		}

		// Token: 0x0601085B RID: 67675 RVA: 0x0038D796 File Offset: 0x0038B996
		public label(GrammarBuilders g, string value)
		{
			this = new label(new LiteralNode(g.Symbol.label, value));
		}

		// Token: 0x17002BE6 RID: 11238
		// (get) Token: 0x0601085C RID: 67676 RVA: 0x0038D7AF File Offset: 0x0038B9AF
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0601085D RID: 67677 RVA: 0x0038D7C6 File Offset: 0x0038B9C6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601085E RID: 67678 RVA: 0x0038D7DC File Offset: 0x0038B9DC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601085F RID: 67679 RVA: 0x0038D806 File Offset: 0x0038BA06
		public bool Equals(label other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062CB RID: 25291
		private ProgramNode _node;
	}
}
