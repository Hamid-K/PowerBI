using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001552 RID: 5458
	public struct LowerCaseConcat : IProgramNodeBuilder, IEquatable<LowerCaseConcat>
	{
		// Token: 0x17001EEE RID: 7918
		// (get) Token: 0x0600B22F RID: 45615 RVA: 0x002718DA File Offset: 0x0026FADA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B230 RID: 45616 RVA: 0x002718E2 File Offset: 0x0026FAE2
		private LowerCaseConcat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B231 RID: 45617 RVA: 0x002718EB File Offset: 0x0026FAEB
		public static LowerCaseConcat CreateUnsafe(ProgramNode node)
		{
			return new LowerCaseConcat(node);
		}

		// Token: 0x0600B232 RID: 45618 RVA: 0x002718F4 File Offset: 0x0026FAF4
		public static LowerCaseConcat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LowerCaseConcat)
			{
				return null;
			}
			return new LowerCaseConcat?(LowerCaseConcat.CreateUnsafe(node));
		}

		// Token: 0x0600B233 RID: 45619 RVA: 0x00271929 File Offset: 0x0026FB29
		public LowerCaseConcat(GrammarBuilders g, concat value0)
		{
			this._node = g.Rule.LowerCaseConcat.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B234 RID: 45620 RVA: 0x00271948 File Offset: 0x0026FB48
		public static implicit operator concatCase(LowerCaseConcat arg)
		{
			return concatCase.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EEF RID: 7919
		// (get) Token: 0x0600B235 RID: 45621 RVA: 0x00271956 File Offset: 0x0026FB56
		public concat concat
		{
			get
			{
				return concat.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B236 RID: 45622 RVA: 0x0027196A File Offset: 0x0026FB6A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B237 RID: 45623 RVA: 0x00271980 File Offset: 0x0026FB80
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B238 RID: 45624 RVA: 0x002719AA File Offset: 0x0026FBAA
		public bool Equals(LowerCaseConcat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004600 RID: 17920
		private ProgramNode _node;
	}
}
