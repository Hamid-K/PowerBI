using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000967 RID: 2407
	public struct splitFile : IProgramNodeBuilder, IEquatable<splitFile>
	{
		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060038F2 RID: 14578 RVA: 0x000B0C9E File Offset: 0x000AEE9E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x000B0CA6 File Offset: 0x000AEEA6
		private splitFile(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x000B0CAF File Offset: 0x000AEEAF
		public static splitFile CreateUnsafe(ProgramNode node)
		{
			return new splitFile(node);
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000B0CB8 File Offset: 0x000AEEB8
		public static splitFile? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.splitFile)
			{
				return null;
			}
			return new splitFile?(splitFile.CreateUnsafe(node));
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x000B0CF2 File Offset: 0x000AEEF2
		public static splitFile CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new splitFile(new Hole(g.Symbol.splitFile, holeId));
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x000B0D0A File Offset: 0x000AEF0A
		public LetSplitFile Cast_LetSplitFile()
		{
			return LetSplitFile.CreateUnsafe(this.Node);
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetSplitFile(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x000B0D17 File Offset: 0x000AEF17
		public bool Is_LetSplitFile(GrammarBuilders g, out LetSplitFile value)
		{
			value = LetSplitFile.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x000B0D2B File Offset: 0x000AEF2B
		public LetSplitFile? As_LetSplitFile(GrammarBuilders g)
		{
			return new LetSplitFile?(LetSplitFile.CreateUnsafe(this.Node));
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x000B0D3D File Offset: 0x000AEF3D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x000B0D50 File Offset: 0x000AEF50
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x000B0D7A File Offset: 0x000AEF7A
		public bool Equals(splitFile other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A87 RID: 6791
		private ProgramNode _node;
	}
}
