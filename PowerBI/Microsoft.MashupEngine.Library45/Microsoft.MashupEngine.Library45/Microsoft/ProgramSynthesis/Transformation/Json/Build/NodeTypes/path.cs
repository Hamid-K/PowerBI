using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Wrangling.Json;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A49 RID: 6729
	public struct path : IProgramNodeBuilder, IEquatable<path>
	{
		// Token: 0x17002518 RID: 9496
		// (get) Token: 0x0600DDC6 RID: 56774 RVA: 0x002F2136 File Offset: 0x002F0336
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DDC7 RID: 56775 RVA: 0x002F213E File Offset: 0x002F033E
		private path(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DDC8 RID: 56776 RVA: 0x002F2147 File Offset: 0x002F0347
		public static path CreateUnsafe(ProgramNode node)
		{
			return new path(node);
		}

		// Token: 0x0600DDC9 RID: 56777 RVA: 0x002F2150 File Offset: 0x002F0350
		public static path? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.path)
			{
				return null;
			}
			return new path?(path.CreateUnsafe(node));
		}

		// Token: 0x0600DDCA RID: 56778 RVA: 0x002F218A File Offset: 0x002F038A
		public static path CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new path(new Hole(g.Symbol.path, holeId));
		}

		// Token: 0x0600DDCB RID: 56779 RVA: 0x002F21A2 File Offset: 0x002F03A2
		public path(GrammarBuilders g, JPath value)
		{
			this = new path(new LiteralNode(g.Symbol.path, value));
		}

		// Token: 0x17002519 RID: 9497
		// (get) Token: 0x0600DDCC RID: 56780 RVA: 0x002F21BB File Offset: 0x002F03BB
		public JPath Value
		{
			get
			{
				return (JPath)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600DDCD RID: 56781 RVA: 0x002F21D2 File Offset: 0x002F03D2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DDCE RID: 56782 RVA: 0x002F21E8 File Offset: 0x002F03E8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DDCF RID: 56783 RVA: 0x002F2212 File Offset: 0x002F0412
		public bool Equals(path other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400543A RID: 21562
		private ProgramNode _node;
	}
}
