using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000C0C RID: 3084
	public struct before : IProgramNodeBuilder, IEquatable<before>
	{
		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06004FBB RID: 20411 RVA: 0x000FB272 File Offset: 0x000F9472
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x000FB27A File Offset: 0x000F947A
		private before(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x000FB283 File Offset: 0x000F9483
		public static before CreateUnsafe(ProgramNode node)
		{
			return new before(node);
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x000FB28C File Offset: 0x000F948C
		public static before? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.before)
			{
				return null;
			}
			return new before?(before.CreateUnsafe(node));
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x000FB2C6 File Offset: 0x000F94C6
		public static before CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new before(new Hole(g.Symbol.before, holeId));
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x000FB2DE File Offset: 0x000F94DE
		public before(GrammarBuilders g)
		{
			this = new before(new VariableNode(g.Symbol.before));
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06004FC1 RID: 20417 RVA: 0x000FB2F6 File Offset: 0x000F94F6
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x000FB303 File Offset: 0x000F9503
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x000FB318 File Offset: 0x000F9518
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x000FB342 File Offset: 0x000F9542
		public bool Equals(before other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002334 RID: 9012
		private ProgramNode _node;
	}
}
