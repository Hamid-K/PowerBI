using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes
{
	// Token: 0x02000BFE RID: 3070
	public struct LetBetweenAxis : IProgramNodeBuilder, IEquatable<LetBetweenAxis>
	{
		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x000F97EE File Offset: 0x000F79EE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004EF9 RID: 20217 RVA: 0x000F97F6 File Offset: 0x000F79F6
		private LetBetweenAxis(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004EFA RID: 20218 RVA: 0x000F97FF File Offset: 0x000F79FF
		public static LetBetweenAxis CreateUnsafe(ProgramNode node)
		{
			return new LetBetweenAxis(node);
		}

		// Token: 0x06004EFB RID: 20219 RVA: 0x000F9808 File Offset: 0x000F7A08
		public static LetBetweenAxis? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetBetweenAxis)
			{
				return null;
			}
			return new LetBetweenAxis?(LetBetweenAxis.CreateUnsafe(node));
		}

		// Token: 0x06004EFC RID: 20220 RVA: 0x000F983D File Offset: 0x000F7A3D
		public LetBetweenAxis(GrammarBuilders g, axis value0, _LetB1 value1)
		{
			this._node = new LetNode(g.Rule.LetBetweenAxis, value0.Node, value1.Node);
		}

		// Token: 0x06004EFD RID: 20221 RVA: 0x000F9863 File Offset: 0x000F7A63
		public static implicit operator selectedBounds(LetBetweenAxis arg)
		{
			return selectedBounds.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06004EFE RID: 20222 RVA: 0x000F9871 File Offset: 0x000F7A71
		public axis axis
		{
			get
			{
				return axis.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06004EFF RID: 20223 RVA: 0x000F9885 File Offset: 0x000F7A85
		public _LetB1 _LetB1
		{
			get
			{
				return _LetB1.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x000F9899 File Offset: 0x000F7A99
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x000F98AC File Offset: 0x000F7AAC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004F02 RID: 20226 RVA: 0x000F98D6 File Offset: 0x000F7AD6
		public bool Equals(LetBetweenAxis other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002326 RID: 8998
		private ProgramNode _node;
	}
}
