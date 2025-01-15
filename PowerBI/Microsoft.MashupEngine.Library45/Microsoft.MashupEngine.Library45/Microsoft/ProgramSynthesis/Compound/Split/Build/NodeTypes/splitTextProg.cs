using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x02000966 RID: 2406
	public struct splitTextProg : IProgramNodeBuilder, IEquatable<splitTextProg>
	{
		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060038E6 RID: 14566 RVA: 0x000B0BAE File Offset: 0x000AEDAE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x000B0BB6 File Offset: 0x000AEDB6
		private splitTextProg(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x000B0BBF File Offset: 0x000AEDBF
		public static splitTextProg CreateUnsafe(ProgramNode node)
		{
			return new splitTextProg(node);
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x000B0BC8 File Offset: 0x000AEDC8
		public static splitTextProg? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.splitTextProg)
			{
				return null;
			}
			return new splitTextProg?(splitTextProg.CreateUnsafe(node));
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x000B0C02 File Offset: 0x000AEE02
		public static splitTextProg CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new splitTextProg(new Hole(g.Symbol.splitTextProg, holeId));
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x000B0C1A File Offset: 0x000AEE1A
		public SplitTextProg Cast_SplitTextProg()
		{
			return SplitTextProg.CreateUnsafe(this.Node);
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SplitTextProg(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x000B0C27 File Offset: 0x000AEE27
		public bool Is_SplitTextProg(GrammarBuilders g, out SplitTextProg value)
		{
			value = SplitTextProg.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x000B0C3B File Offset: 0x000AEE3B
		public SplitTextProg? As_SplitTextProg(GrammarBuilders g)
		{
			return new SplitTextProg?(SplitTextProg.CreateUnsafe(this.Node));
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x000B0C4D File Offset: 0x000AEE4D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x000B0C60 File Offset: 0x000AEE60
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x000B0C8A File Offset: 0x000AEE8A
		public bool Equals(splitTextProg other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A86 RID: 6790
		private ProgramNode _node;
	}
}
