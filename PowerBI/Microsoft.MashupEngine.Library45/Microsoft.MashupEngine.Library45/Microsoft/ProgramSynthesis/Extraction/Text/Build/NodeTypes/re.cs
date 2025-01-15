using System;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F47 RID: 3911
	public struct re : IProgramNodeBuilder, IEquatable<re>
	{
		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06006CEF RID: 27887 RVA: 0x00163F06 File Offset: 0x00162106
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006CF0 RID: 27888 RVA: 0x00163F0E File Offset: 0x0016210E
		private re(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006CF1 RID: 27889 RVA: 0x00163F17 File Offset: 0x00162117
		public static re CreateUnsafe(ProgramNode node)
		{
			return new re(node);
		}

		// Token: 0x06006CF2 RID: 27890 RVA: 0x00163F20 File Offset: 0x00162120
		public static re? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.re)
			{
				return null;
			}
			return new re?(re.CreateUnsafe(node));
		}

		// Token: 0x06006CF3 RID: 27891 RVA: 0x00163F5A File Offset: 0x0016215A
		public static re CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new re(new Hole(g.Symbol.re, holeId));
		}

		// Token: 0x06006CF4 RID: 27892 RVA: 0x00163F72 File Offset: 0x00162172
		public re(GrammarBuilders g, Regex value)
		{
			this = new re(new LiteralNode(g.Symbol.re, value));
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06006CF5 RID: 27893 RVA: 0x00163F8B File Offset: 0x0016218B
		public Regex Value
		{
			get
			{
				return (Regex)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06006CF6 RID: 27894 RVA: 0x00163FA2 File Offset: 0x001621A2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006CF7 RID: 27895 RVA: 0x00163FB8 File Offset: 0x001621B8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006CF8 RID: 27896 RVA: 0x00163FE2 File Offset: 0x001621E2
		public bool Equals(re other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F32 RID: 12082
		private ProgramNode _node;
	}
}
