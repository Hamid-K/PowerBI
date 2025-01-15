using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x0200136B RID: 4971
	public struct fieldMatch : IProgramNodeBuilder, IEquatable<fieldMatch>
	{
		// Token: 0x17001A74 RID: 6772
		// (get) Token: 0x060099FF RID: 39423 RVA: 0x0020A2CA File Offset: 0x002084CA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009A00 RID: 39424 RVA: 0x0020A2D2 File Offset: 0x002084D2
		private fieldMatch(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009A01 RID: 39425 RVA: 0x0020A2DB File Offset: 0x002084DB
		public static fieldMatch CreateUnsafe(ProgramNode node)
		{
			return new fieldMatch(node);
		}

		// Token: 0x06009A02 RID: 39426 RVA: 0x0020A2E4 File Offset: 0x002084E4
		public static fieldMatch? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fieldMatch)
			{
				return null;
			}
			return new fieldMatch?(fieldMatch.CreateUnsafe(node));
		}

		// Token: 0x06009A03 RID: 39427 RVA: 0x0020A31E File Offset: 0x0020851E
		public static fieldMatch CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fieldMatch(new Hole(g.Symbol.fieldMatch, holeId));
		}

		// Token: 0x06009A04 RID: 39428 RVA: 0x0020A336 File Offset: 0x00208536
		public FieldMatch Cast_FieldMatch()
		{
			return FieldMatch.CreateUnsafe(this.Node);
		}

		// Token: 0x06009A05 RID: 39429 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FieldMatch(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06009A06 RID: 39430 RVA: 0x0020A343 File Offset: 0x00208543
		public bool Is_FieldMatch(GrammarBuilders g, out FieldMatch value)
		{
			value = FieldMatch.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06009A07 RID: 39431 RVA: 0x0020A357 File Offset: 0x00208557
		public FieldMatch? As_FieldMatch(GrammarBuilders g)
		{
			return new FieldMatch?(FieldMatch.CreateUnsafe(this.Node));
		}

		// Token: 0x06009A08 RID: 39432 RVA: 0x0020A369 File Offset: 0x00208569
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009A09 RID: 39433 RVA: 0x0020A37C File Offset: 0x0020857C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009A0A RID: 39434 RVA: 0x0020A3A6 File Offset: 0x002085A6
		public bool Equals(fieldMatch other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DE2 RID: 15842
		private ProgramNode _node;
	}
}
