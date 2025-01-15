using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001024 RID: 4132
	public struct ContainsNum : IProgramNodeBuilder, IEquatable<ContainsNum>
	{
		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x060079FF RID: 31231 RVA: 0x001A1342 File Offset: 0x0019F542
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007A00 RID: 31232 RVA: 0x001A134A File Offset: 0x0019F54A
		private ContainsNum(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007A01 RID: 31233 RVA: 0x001A1353 File Offset: 0x0019F553
		public static ContainsNum CreateUnsafe(ProgramNode node)
		{
			return new ContainsNum(node);
		}

		// Token: 0x06007A02 RID: 31234 RVA: 0x001A135C File Offset: 0x0019F55C
		public static ContainsNum? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ContainsNum)
			{
				return null;
			}
			return new ContainsNum?(ContainsNum.CreateUnsafe(node));
		}

		// Token: 0x06007A03 RID: 31235 RVA: 0x001A1391 File Offset: 0x0019F591
		public ContainsNum(GrammarBuilders g, node value0)
		{
			this._node = g.Rule.ContainsNum.BuildASTNode(value0.Node);
		}

		// Token: 0x06007A04 RID: 31236 RVA: 0x001A13B0 File Offset: 0x0019F5B0
		public static implicit operator atomExpr(ContainsNum arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x06007A05 RID: 31237 RVA: 0x001A13BE File Offset: 0x0019F5BE
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007A06 RID: 31238 RVA: 0x001A13D2 File Offset: 0x0019F5D2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007A07 RID: 31239 RVA: 0x001A13E8 File Offset: 0x0019F5E8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007A08 RID: 31240 RVA: 0x001A1412 File Offset: 0x0019F612
		public bool Equals(ContainsNum other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400333D RID: 13117
		private ProgramNode _node;
	}
}
