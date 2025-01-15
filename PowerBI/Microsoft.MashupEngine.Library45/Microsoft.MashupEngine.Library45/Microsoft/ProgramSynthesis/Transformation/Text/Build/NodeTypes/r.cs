using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C60 RID: 7264
	public struct r : IProgramNodeBuilder, IEquatable<r>
	{
		// Token: 0x170028F8 RID: 10488
		// (get) Token: 0x0600F5F8 RID: 62968 RVA: 0x003488A6 File Offset: 0x00346AA6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F5F9 RID: 62969 RVA: 0x003488AE File Offset: 0x00346AAE
		private r(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F5FA RID: 62970 RVA: 0x003488B7 File Offset: 0x00346AB7
		public static r CreateUnsafe(ProgramNode node)
		{
			return new r(node);
		}

		// Token: 0x0600F5FB RID: 62971 RVA: 0x003488C0 File Offset: 0x00346AC0
		public static r? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.r)
			{
				return null;
			}
			return new r?(r.CreateUnsafe(node));
		}

		// Token: 0x0600F5FC RID: 62972 RVA: 0x003488FA File Offset: 0x00346AFA
		public static r CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new r(new Hole(g.Symbol.r, holeId));
		}

		// Token: 0x0600F5FD RID: 62973 RVA: 0x00348912 File Offset: 0x00346B12
		public r(GrammarBuilders g, RegularExpression value)
		{
			this = new r(new LiteralNode(g.Symbol.r, value));
		}

		// Token: 0x170028F9 RID: 10489
		// (get) Token: 0x0600F5FE RID: 62974 RVA: 0x0034892B File Offset: 0x00346B2B
		public RegularExpression Value
		{
			get
			{
				return (RegularExpression)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F5FF RID: 62975 RVA: 0x00348942 File Offset: 0x00346B42
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F600 RID: 62976 RVA: 0x00348958 File Offset: 0x00346B58
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F601 RID: 62977 RVA: 0x00348982 File Offset: 0x00346B82
		public bool Equals(r other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B4F RID: 23375
		private ProgramNode _node;
	}
}
