using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200133B RID: 4923
	public struct multipleMatches_d : IProgramNodeBuilder, IEquatable<multipleMatches_d>
	{
		// Token: 0x170019FB RID: 6651
		// (get) Token: 0x06009796 RID: 38806 RVA: 0x0020587E File Offset: 0x00203A7E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009797 RID: 38807 RVA: 0x00205886 File Offset: 0x00203A86
		private multipleMatches_d(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009798 RID: 38808 RVA: 0x0020588F File Offset: 0x00203A8F
		public static multipleMatches_d CreateUnsafe(ProgramNode node)
		{
			return new multipleMatches_d(node);
		}

		// Token: 0x06009799 RID: 38809 RVA: 0x00205898 File Offset: 0x00203A98
		public static multipleMatches_d? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.multipleMatches_d)
			{
				return null;
			}
			return new multipleMatches_d?(multipleMatches_d.CreateUnsafe(node));
		}

		// Token: 0x0600979A RID: 38810 RVA: 0x002058CD File Offset: 0x00203ACD
		public multipleMatches_d(GrammarBuilders g, d value0)
		{
			this._node = g.UnnamedConversion.multipleMatches_d.BuildASTNode(value0.Node);
		}

		// Token: 0x0600979B RID: 38811 RVA: 0x002058EC File Offset: 0x00203AEC
		public static implicit operator multipleMatches(multipleMatches_d arg)
		{
			return multipleMatches.CreateUnsafe(arg.Node);
		}

		// Token: 0x170019FC RID: 6652
		// (get) Token: 0x0600979C RID: 38812 RVA: 0x002058FA File Offset: 0x00203AFA
		public d d
		{
			get
			{
				return d.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600979D RID: 38813 RVA: 0x0020590E File Offset: 0x00203B0E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600979E RID: 38814 RVA: 0x00205924 File Offset: 0x00203B24
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600979F RID: 38815 RVA: 0x0020594E File Offset: 0x00203B4E
		public bool Equals(multipleMatches_d other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DB2 RID: 15794
		private ProgramNode _node;
	}
}
