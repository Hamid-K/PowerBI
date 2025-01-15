using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001554 RID: 5460
	public struct ProperCaseConcat : IProgramNodeBuilder, IEquatable<ProperCaseConcat>
	{
		// Token: 0x17001EF2 RID: 7922
		// (get) Token: 0x0600B243 RID: 45635 RVA: 0x00271AA2 File Offset: 0x0026FCA2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B244 RID: 45636 RVA: 0x00271AAA File Offset: 0x0026FCAA
		private ProperCaseConcat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B245 RID: 45637 RVA: 0x00271AB3 File Offset: 0x0026FCB3
		public static ProperCaseConcat CreateUnsafe(ProgramNode node)
		{
			return new ProperCaseConcat(node);
		}

		// Token: 0x0600B246 RID: 45638 RVA: 0x00271ABC File Offset: 0x0026FCBC
		public static ProperCaseConcat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ProperCaseConcat)
			{
				return null;
			}
			return new ProperCaseConcat?(ProperCaseConcat.CreateUnsafe(node));
		}

		// Token: 0x0600B247 RID: 45639 RVA: 0x00271AF1 File Offset: 0x0026FCF1
		public ProperCaseConcat(GrammarBuilders g, concat value0)
		{
			this._node = g.Rule.ProperCaseConcat.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B248 RID: 45640 RVA: 0x00271B10 File Offset: 0x0026FD10
		public static implicit operator concatCase(ProperCaseConcat arg)
		{
			return concatCase.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EF3 RID: 7923
		// (get) Token: 0x0600B249 RID: 45641 RVA: 0x00271B1E File Offset: 0x0026FD1E
		public concat concat
		{
			get
			{
				return concat.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B24A RID: 45642 RVA: 0x00271B32 File Offset: 0x0026FD32
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B24B RID: 45643 RVA: 0x00271B48 File Offset: 0x0026FD48
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B24C RID: 45644 RVA: 0x00271B72 File Offset: 0x0026FD72
		public bool Equals(ProperCaseConcat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004602 RID: 17922
		private ProgramNode _node;
	}
}
