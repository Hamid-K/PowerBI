using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C62 RID: 7266
	public struct name : IProgramNodeBuilder, IEquatable<name>
	{
		// Token: 0x170028FC RID: 10492
		// (get) Token: 0x0600F60C RID: 62988 RVA: 0x00348A86 File Offset: 0x00346C86
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F60D RID: 62989 RVA: 0x00348A8E File Offset: 0x00346C8E
		private name(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F60E RID: 62990 RVA: 0x00348A97 File Offset: 0x00346C97
		public static name CreateUnsafe(ProgramNode node)
		{
			return new name(node);
		}

		// Token: 0x0600F60F RID: 62991 RVA: 0x00348AA0 File Offset: 0x00346CA0
		public static name? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.name)
			{
				return null;
			}
			return new name?(name.CreateUnsafe(node));
		}

		// Token: 0x0600F610 RID: 62992 RVA: 0x00348ADA File Offset: 0x00346CDA
		public static name CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new name(new Hole(g.Symbol.name, holeId));
		}

		// Token: 0x0600F611 RID: 62993 RVA: 0x00348AF2 File Offset: 0x00346CF2
		public name(GrammarBuilders g, string value)
		{
			this = new name(new LiteralNode(g.Symbol.name, value));
		}

		// Token: 0x170028FD RID: 10493
		// (get) Token: 0x0600F612 RID: 62994 RVA: 0x00348B0B File Offset: 0x00346D0B
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F613 RID: 62995 RVA: 0x00348B22 File Offset: 0x00346D22
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F614 RID: 62996 RVA: 0x00348B38 File Offset: 0x00346D38
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F615 RID: 62997 RVA: 0x00348B62 File Offset: 0x00346D62
		public bool Equals(name other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B51 RID: 23377
		private ProgramNode _node;
	}
}
