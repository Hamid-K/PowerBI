using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C61 RID: 7265
	public struct s : IProgramNodeBuilder, IEquatable<s>
	{
		// Token: 0x170028FA RID: 10490
		// (get) Token: 0x0600F602 RID: 62978 RVA: 0x00348996 File Offset: 0x00346B96
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F603 RID: 62979 RVA: 0x0034899E File Offset: 0x00346B9E
		private s(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F604 RID: 62980 RVA: 0x003489A7 File Offset: 0x00346BA7
		public static s CreateUnsafe(ProgramNode node)
		{
			return new s(node);
		}

		// Token: 0x0600F605 RID: 62981 RVA: 0x003489B0 File Offset: 0x00346BB0
		public static s? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.s)
			{
				return null;
			}
			return new s?(s.CreateUnsafe(node));
		}

		// Token: 0x0600F606 RID: 62982 RVA: 0x003489EA File Offset: 0x00346BEA
		public static s CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new s(new Hole(g.Symbol.s, holeId));
		}

		// Token: 0x0600F607 RID: 62983 RVA: 0x00348A02 File Offset: 0x00346C02
		public s(GrammarBuilders g, string value)
		{
			this = new s(new LiteralNode(g.Symbol.s, value));
		}

		// Token: 0x170028FB RID: 10491
		// (get) Token: 0x0600F608 RID: 62984 RVA: 0x00348A1B File Offset: 0x00346C1B
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F609 RID: 62985 RVA: 0x00348A32 File Offset: 0x00346C32
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F60A RID: 62986 RVA: 0x00348A48 File Offset: 0x00346C48
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F60B RID: 62987 RVA: 0x00348A72 File Offset: 0x00346C72
		public bool Equals(s other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B50 RID: 23376
		private ProgramNode _node;
	}
}
