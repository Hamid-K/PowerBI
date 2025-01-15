using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001584 RID: 5508
	public struct TrimFullSlice : IProgramNodeBuilder, IEquatable<TrimFullSlice>
	{
		// Token: 0x17001F81 RID: 8065
		// (get) Token: 0x0600B452 RID: 46162 RVA: 0x00274A3E File Offset: 0x00272C3E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B453 RID: 46163 RVA: 0x00274A46 File Offset: 0x00272C46
		private TrimFullSlice(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B454 RID: 46164 RVA: 0x00274A4F File Offset: 0x00272C4F
		public static TrimFullSlice CreateUnsafe(ProgramNode node)
		{
			return new TrimFullSlice(node);
		}

		// Token: 0x0600B455 RID: 46165 RVA: 0x00274A58 File Offset: 0x00272C58
		public static TrimFullSlice? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimFullSlice)
			{
				return null;
			}
			return new TrimFullSlice?(TrimFullSlice.CreateUnsafe(node));
		}

		// Token: 0x0600B456 RID: 46166 RVA: 0x00274A8D File Offset: 0x00272C8D
		public TrimFullSlice(GrammarBuilders g, slice value0)
		{
			this._node = g.Rule.TrimFullSlice.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B457 RID: 46167 RVA: 0x00274AAC File Offset: 0x00272CAC
		public static implicit operator sliceTrim(TrimFullSlice arg)
		{
			return sliceTrim.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F82 RID: 8066
		// (get) Token: 0x0600B458 RID: 46168 RVA: 0x00274ABA File Offset: 0x00272CBA
		public slice slice
		{
			get
			{
				return slice.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B459 RID: 46169 RVA: 0x00274ACE File Offset: 0x00272CCE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B45A RID: 46170 RVA: 0x00274AE4 File Offset: 0x00272CE4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B45B RID: 46171 RVA: 0x00274B0E File Offset: 0x00272D0E
		public bool Equals(TrimFullSlice other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004632 RID: 17970
		private ProgramNode _node;
	}
}
