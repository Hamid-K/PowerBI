using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001551 RID: 5457
	public struct ProperCase : IProgramNodeBuilder, IEquatable<ProperCase>
	{
		// Token: 0x17001EEC RID: 7916
		// (get) Token: 0x0600B225 RID: 45605 RVA: 0x002717F6 File Offset: 0x0026F9F6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B226 RID: 45606 RVA: 0x002717FE File Offset: 0x0026F9FE
		private ProperCase(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B227 RID: 45607 RVA: 0x00271807 File Offset: 0x0026FA07
		public static ProperCase CreateUnsafe(ProgramNode node)
		{
			return new ProperCase(node);
		}

		// Token: 0x0600B228 RID: 45608 RVA: 0x00271810 File Offset: 0x0026FA10
		public static ProperCase? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ProperCase)
			{
				return null;
			}
			return new ProperCase?(ProperCase.CreateUnsafe(node));
		}

		// Token: 0x0600B229 RID: 45609 RVA: 0x00271845 File Offset: 0x0026FA45
		public ProperCase(GrammarBuilders g, segment value0)
		{
			this._node = g.Rule.ProperCase.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B22A RID: 45610 RVA: 0x00271864 File Offset: 0x0026FA64
		public static implicit operator segmentCase(ProperCase arg)
		{
			return segmentCase.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EED RID: 7917
		// (get) Token: 0x0600B22B RID: 45611 RVA: 0x00271872 File Offset: 0x0026FA72
		public segment segment
		{
			get
			{
				return segment.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B22C RID: 45612 RVA: 0x00271886 File Offset: 0x0026FA86
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B22D RID: 45613 RVA: 0x0027189C File Offset: 0x0026FA9C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B22E RID: 45614 RVA: 0x002718C6 File Offset: 0x0026FAC6
		public bool Equals(ProperCase other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045FF RID: 17919
		private ProgramNode _node;
	}
}
