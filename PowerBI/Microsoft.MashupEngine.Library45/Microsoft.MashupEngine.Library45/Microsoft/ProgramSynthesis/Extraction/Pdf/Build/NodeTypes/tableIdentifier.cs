using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000C0A RID: 3082
	public struct tableIdentifier : IProgramNodeBuilder, IEquatable<tableIdentifier>
	{
		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06004FA7 RID: 20391 RVA: 0x000FB0AA File Offset: 0x000F92AA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x000FB0B2 File Offset: 0x000F92B2
		private tableIdentifier(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x000FB0BB File Offset: 0x000F92BB
		public static tableIdentifier CreateUnsafe(ProgramNode node)
		{
			return new tableIdentifier(node);
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x000FB0C4 File Offset: 0x000F92C4
		public static tableIdentifier? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.tableIdentifier)
			{
				return null;
			}
			return new tableIdentifier?(tableIdentifier.CreateUnsafe(node));
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x000FB0FE File Offset: 0x000F92FE
		public static tableIdentifier CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new tableIdentifier(new Hole(g.Symbol.tableIdentifier, holeId));
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x000FB116 File Offset: 0x000F9316
		public tableIdentifier(GrammarBuilders g)
		{
			this = new tableIdentifier(new VariableNode(g.Symbol.tableIdentifier));
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06004FAD RID: 20397 RVA: 0x000FB12E File Offset: 0x000F932E
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x000FB13B File Offset: 0x000F933B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x000FB150 File Offset: 0x000F9350
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x000FB17A File Offset: 0x000F937A
		public bool Equals(tableIdentifier other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002332 RID: 9010
		private ProgramNode _node;
	}
}
