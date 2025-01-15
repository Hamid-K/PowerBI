using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E4E RID: 3662
	public struct Output : IProgramNodeBuilder, IEquatable<Output>
	{
		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x0600625D RID: 25181 RVA: 0x0014076A File Offset: 0x0013E96A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600625E RID: 25182 RVA: 0x00140772 File Offset: 0x0013E972
		private Output(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x0014077B File Offset: 0x0013E97B
		public static Output CreateUnsafe(ProgramNode node)
		{
			return new Output(node);
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x00140784 File Offset: 0x0013E984
		public static Output? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Output)
			{
				return null;
			}
			return new Output?(Output.CreateUnsafe(node));
		}

		// Token: 0x06006261 RID: 25185 RVA: 0x001407B9 File Offset: 0x0013E9B9
		public Output(GrammarBuilders g, trim value0)
		{
			this._node = g.Rule.Output.BuildASTNode(value0.Node);
		}

		// Token: 0x06006262 RID: 25186 RVA: 0x001407D8 File Offset: 0x0013E9D8
		public static implicit operator output(Output arg)
		{
			return output.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06006263 RID: 25187 RVA: 0x001407E6 File Offset: 0x0013E9E6
		public trim trim
		{
			get
			{
				return trim.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x001407FA File Offset: 0x0013E9FA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x00140810 File Offset: 0x0013EA10
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x0014083A File Offset: 0x0013EA3A
		public bool Equals(Output other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BF8 RID: 11256
		private ProgramNode _node;
	}
}
