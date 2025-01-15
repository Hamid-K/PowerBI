using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x02001549 RID: 5449
	public struct ToInt : IProgramNodeBuilder, IEquatable<ToInt>
	{
		// Token: 0x17001EDA RID: 7898
		// (get) Token: 0x0600B1D3 RID: 45523 RVA: 0x002710A2 File Offset: 0x0026F2A2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B1D4 RID: 45524 RVA: 0x002710AA File Offset: 0x0026F2AA
		private ToInt(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B1D5 RID: 45525 RVA: 0x002710B3 File Offset: 0x0026F2B3
		public static ToInt CreateUnsafe(ProgramNode node)
		{
			return new ToInt(node);
		}

		// Token: 0x0600B1D6 RID: 45526 RVA: 0x002710BC File Offset: 0x0026F2BC
		public static ToInt? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ToInt)
			{
				return null;
			}
			return new ToInt?(ToInt.CreateUnsafe(node));
		}

		// Token: 0x0600B1D7 RID: 45527 RVA: 0x002710F1 File Offset: 0x0026F2F1
		public ToInt(GrammarBuilders g, outNumber value0)
		{
			this._node = g.Rule.ToInt.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B1D8 RID: 45528 RVA: 0x00271110 File Offset: 0x0026F310
		public static implicit operator output(ToInt arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EDB RID: 7899
		// (get) Token: 0x0600B1D9 RID: 45529 RVA: 0x0027111E File Offset: 0x0026F31E
		public outNumber outNumber
		{
			get
			{
				return outNumber.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B1DA RID: 45530 RVA: 0x00271132 File Offset: 0x0026F332
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B1DB RID: 45531 RVA: 0x00271148 File Offset: 0x0026F348
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B1DC RID: 45532 RVA: 0x00271172 File Offset: 0x0026F372
		public bool Equals(ToInt other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045F7 RID: 17911
		private ProgramNode _node;
	}
}
