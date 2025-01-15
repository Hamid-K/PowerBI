using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001E5C RID: 7772
	public struct sequenceChildren_convertSequence : IProgramNodeBuilder, IEquatable<sequenceChildren_convertSequence>
	{
		// Token: 0x17002B78 RID: 11128
		// (get) Token: 0x060105E5 RID: 67045 RVA: 0x00388E86 File Offset: 0x00387086
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060105E6 RID: 67046 RVA: 0x00388E8E File Offset: 0x0038708E
		private sequenceChildren_convertSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060105E7 RID: 67047 RVA: 0x00388E97 File Offset: 0x00387097
		public static sequenceChildren_convertSequence CreateUnsafe(ProgramNode node)
		{
			return new sequenceChildren_convertSequence(node);
		}

		// Token: 0x060105E8 RID: 67048 RVA: 0x00388EA0 File Offset: 0x003870A0
		public static sequenceChildren_convertSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.sequenceChildren_convertSequence)
			{
				return null;
			}
			return new sequenceChildren_convertSequence?(sequenceChildren_convertSequence.CreateUnsafe(node));
		}

		// Token: 0x060105E9 RID: 67049 RVA: 0x00388ED5 File Offset: 0x003870D5
		public sequenceChildren_convertSequence(GrammarBuilders g, convertSequence value0)
		{
			this._node = g.UnnamedConversion.sequenceChildren_convertSequence.BuildASTNode(value0.Node);
		}

		// Token: 0x060105EA RID: 67050 RVA: 0x00388EF4 File Offset: 0x003870F4
		public static implicit operator sequenceChildren(sequenceChildren_convertSequence arg)
		{
			return sequenceChildren.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002B79 RID: 11129
		// (get) Token: 0x060105EB RID: 67051 RVA: 0x00388F02 File Offset: 0x00387102
		public convertSequence convertSequence
		{
			get
			{
				return convertSequence.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060105EC RID: 67052 RVA: 0x00388F16 File Offset: 0x00387116
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060105ED RID: 67053 RVA: 0x00388F2C File Offset: 0x0038712C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060105EE RID: 67054 RVA: 0x00388F56 File Offset: 0x00387156
		public bool Equals(sequenceChildren_convertSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400629B RID: 25243
		private ProgramNode _node;
	}
}
