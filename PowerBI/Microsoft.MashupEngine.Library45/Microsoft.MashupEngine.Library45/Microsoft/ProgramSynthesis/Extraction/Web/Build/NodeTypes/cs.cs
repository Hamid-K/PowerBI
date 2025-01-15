using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200109F RID: 4255
	public struct cs : IProgramNodeBuilder, IEquatable<cs>
	{
		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x0600803D RID: 32829 RVA: 0x001AD2DE File Offset: 0x001AB4DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600803E RID: 32830 RVA: 0x001AD2E6 File Offset: 0x001AB4E6
		private cs(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600803F RID: 32831 RVA: 0x001AD2EF File Offset: 0x001AB4EF
		public static cs CreateUnsafe(ProgramNode node)
		{
			return new cs(node);
		}

		// Token: 0x06008040 RID: 32832 RVA: 0x001AD2F8 File Offset: 0x001AB4F8
		public static cs? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.cs)
			{
				return null;
			}
			return new cs?(cs.CreateUnsafe(node));
		}

		// Token: 0x06008041 RID: 32833 RVA: 0x001AD332 File Offset: 0x001AB532
		public static cs CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new cs(new Hole(g.Symbol.cs, holeId));
		}

		// Token: 0x06008042 RID: 32834 RVA: 0x001AD34A File Offset: 0x001AB54A
		public cs(GrammarBuilders g)
		{
			this = new cs(new VariableNode(g.Symbol.cs));
		}

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x06008043 RID: 32835 RVA: 0x001AD362 File Offset: 0x001AB562
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x06008044 RID: 32836 RVA: 0x001AD36F File Offset: 0x001AB56F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008045 RID: 32837 RVA: 0x001AD384 File Offset: 0x001AB584
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008046 RID: 32838 RVA: 0x001AD3AE File Offset: 0x001AB5AE
		public bool Equals(cs other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033B8 RID: 13240
		private ProgramNode _node;
	}
}
