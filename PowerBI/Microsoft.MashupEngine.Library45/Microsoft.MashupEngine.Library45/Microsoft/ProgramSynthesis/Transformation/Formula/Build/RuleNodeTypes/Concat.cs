using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001555 RID: 5461
	public struct Concat : IProgramNodeBuilder, IEquatable<Concat>
	{
		// Token: 0x17001EF4 RID: 7924
		// (get) Token: 0x0600B24D RID: 45645 RVA: 0x00271B86 File Offset: 0x0026FD86
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B24E RID: 45646 RVA: 0x00271B8E File Offset: 0x0026FD8E
		private Concat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B24F RID: 45647 RVA: 0x00271B97 File Offset: 0x0026FD97
		public static Concat CreateUnsafe(ProgramNode node)
		{
			return new Concat(node);
		}

		// Token: 0x0600B250 RID: 45648 RVA: 0x00271BA0 File Offset: 0x0026FDA0
		public static Concat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Concat)
			{
				return null;
			}
			return new Concat?(Concat.CreateUnsafe(node));
		}

		// Token: 0x0600B251 RID: 45649 RVA: 0x00271BD5 File Offset: 0x0026FDD5
		public Concat(GrammarBuilders g, concatPrefix value0, concatSuffix value1)
		{
			this._node = g.Rule.Concat.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B252 RID: 45650 RVA: 0x00271BFB File Offset: 0x0026FDFB
		public static implicit operator concat(Concat arg)
		{
			return concat.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EF5 RID: 7925
		// (get) Token: 0x0600B253 RID: 45651 RVA: 0x00271C09 File Offset: 0x0026FE09
		public concatPrefix concatPrefix
		{
			get
			{
				return concatPrefix.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001EF6 RID: 7926
		// (get) Token: 0x0600B254 RID: 45652 RVA: 0x00271C1D File Offset: 0x0026FE1D
		public concatSuffix concatSuffix
		{
			get
			{
				return concatSuffix.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B255 RID: 45653 RVA: 0x00271C31 File Offset: 0x0026FE31
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B256 RID: 45654 RVA: 0x00271C44 File Offset: 0x0026FE44
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B257 RID: 45655 RVA: 0x00271C6E File Offset: 0x0026FE6E
		public bool Equals(Concat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004603 RID: 17923
		private ProgramNode _node;
	}
}
