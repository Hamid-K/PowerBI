using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200101B RID: 4123
	public struct SingleSelection3 : IProgramNodeBuilder, IEquatable<SingleSelection3>
	{
		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x060079A2 RID: 31138 RVA: 0x001A0AF6 File Offset: 0x0019ECF6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060079A3 RID: 31139 RVA: 0x001A0AFE File Offset: 0x0019ECFE
		private SingleSelection3(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060079A4 RID: 31140 RVA: 0x001A0B07 File Offset: 0x0019ED07
		public static SingleSelection3 CreateUnsafe(ProgramNode node)
		{
			return new SingleSelection3(node);
		}

		// Token: 0x060079A5 RID: 31141 RVA: 0x001A0B10 File Offset: 0x0019ED10
		public static SingleSelection3? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SingleSelection3)
			{
				return null;
			}
			return new SingleSelection3?(SingleSelection3.CreateUnsafe(node));
		}

		// Token: 0x060079A6 RID: 31142 RVA: 0x001A0B45 File Offset: 0x0019ED45
		public SingleSelection3(GrammarBuilders g, filterSelection3 value0)
		{
			this._node = g.Rule.SingleSelection3.BuildASTNode(value0.Node);
		}

		// Token: 0x060079A7 RID: 31143 RVA: 0x001A0B64 File Offset: 0x0019ED64
		public static implicit operator selection5(SingleSelection3 arg)
		{
			return selection5.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x060079A8 RID: 31144 RVA: 0x001A0B72 File Offset: 0x0019ED72
		public filterSelection3 filterSelection3
		{
			get
			{
				return filterSelection3.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060079A9 RID: 31145 RVA: 0x001A0B86 File Offset: 0x0019ED86
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060079AA RID: 31146 RVA: 0x001A0B9C File Offset: 0x0019ED9C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060079AB RID: 31147 RVA: 0x001A0BC6 File Offset: 0x0019EDC6
		public bool Equals(SingleSelection3 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003334 RID: 13108
		private ProgramNode _node;
	}
}
