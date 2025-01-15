using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001095 RID: 4245
	public struct names : IProgramNodeBuilder, IEquatable<names>
	{
		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06007FD9 RID: 32729 RVA: 0x001AC992 File Offset: 0x001AAB92
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007FDA RID: 32730 RVA: 0x001AC99A File Offset: 0x001AAB9A
		private names(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007FDB RID: 32731 RVA: 0x001AC9A3 File Offset: 0x001AABA3
		public static names CreateUnsafe(ProgramNode node)
		{
			return new names(node);
		}

		// Token: 0x06007FDC RID: 32732 RVA: 0x001AC9AC File Offset: 0x001AABAC
		public static names? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.names)
			{
				return null;
			}
			return new names?(names.CreateUnsafe(node));
		}

		// Token: 0x06007FDD RID: 32733 RVA: 0x001AC9E6 File Offset: 0x001AABE6
		public static names CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new names(new Hole(g.Symbol.names, holeId));
		}

		// Token: 0x06007FDE RID: 32734 RVA: 0x001AC9FE File Offset: 0x001AABFE
		public names(GrammarBuilders g, string[] value)
		{
			this = new names(new LiteralNode(g.Symbol.names, value));
		}

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x06007FDF RID: 32735 RVA: 0x001ACA17 File Offset: 0x001AAC17
		public string[] Value
		{
			get
			{
				return (string[])((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06007FE0 RID: 32736 RVA: 0x001ACA2E File Offset: 0x001AAC2E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007FE1 RID: 32737 RVA: 0x001ACA44 File Offset: 0x001AAC44
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007FE2 RID: 32738 RVA: 0x001ACA6E File Offset: 0x001AAC6E
		public bool Equals(names other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033AE RID: 13230
		private ProgramNode _node;
	}
}
