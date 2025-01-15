using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001090 RID: 4240
	public struct idName : IProgramNodeBuilder, IEquatable<idName>
	{
		// Token: 0x1700167D RID: 5757
		// (get) Token: 0x06007FA7 RID: 32679 RVA: 0x001AC4DA File Offset: 0x001AA6DA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007FA8 RID: 32680 RVA: 0x001AC4E2 File Offset: 0x001AA6E2
		private idName(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007FA9 RID: 32681 RVA: 0x001AC4EB File Offset: 0x001AA6EB
		public static idName CreateUnsafe(ProgramNode node)
		{
			return new idName(node);
		}

		// Token: 0x06007FAA RID: 32682 RVA: 0x001AC4F4 File Offset: 0x001AA6F4
		public static idName? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.idName)
			{
				return null;
			}
			return new idName?(idName.CreateUnsafe(node));
		}

		// Token: 0x06007FAB RID: 32683 RVA: 0x001AC52E File Offset: 0x001AA72E
		public static idName CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new idName(new Hole(g.Symbol.idName, holeId));
		}

		// Token: 0x06007FAC RID: 32684 RVA: 0x001AC546 File Offset: 0x001AA746
		public idName(GrammarBuilders g, string value)
		{
			this = new idName(new LiteralNode(g.Symbol.idName, value));
		}

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x06007FAD RID: 32685 RVA: 0x001AC55F File Offset: 0x001AA75F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06007FAE RID: 32686 RVA: 0x001AC576 File Offset: 0x001AA776
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007FAF RID: 32687 RVA: 0x001AC58C File Offset: 0x001AA78C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007FB0 RID: 32688 RVA: 0x001AC5B6 File Offset: 0x001AA7B6
		public bool Equals(idName other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033A9 RID: 13225
		private ProgramNode _node;
	}
}
