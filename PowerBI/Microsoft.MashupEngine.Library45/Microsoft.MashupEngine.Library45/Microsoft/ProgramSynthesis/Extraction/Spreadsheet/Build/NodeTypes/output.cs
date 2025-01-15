using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E51 RID: 3665
	public struct output : IProgramNodeBuilder, IEquatable<output>
	{
		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x0600627B RID: 25211 RVA: 0x00140A16 File Offset: 0x0013EC16
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600627C RID: 25212 RVA: 0x00140A1E File Offset: 0x0013EC1E
		private output(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600627D RID: 25213 RVA: 0x00140A27 File Offset: 0x0013EC27
		public static output CreateUnsafe(ProgramNode node)
		{
			return new output(node);
		}

		// Token: 0x0600627E RID: 25214 RVA: 0x00140A30 File Offset: 0x0013EC30
		public static output? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.output)
			{
				return null;
			}
			return new output?(output.CreateUnsafe(node));
		}

		// Token: 0x0600627F RID: 25215 RVA: 0x00140A6A File Offset: 0x0013EC6A
		public static output CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new output(new Hole(g.Symbol.output, holeId));
		}

		// Token: 0x06006280 RID: 25216 RVA: 0x00140A82 File Offset: 0x0013EC82
		public Output Cast_Output()
		{
			return Output.CreateUnsafe(this.Node);
		}

		// Token: 0x06006281 RID: 25217 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Output(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x00140A8F File Offset: 0x0013EC8F
		public bool Is_Output(GrammarBuilders g, out Output value)
		{
			value = Output.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06006283 RID: 25219 RVA: 0x00140AA3 File Offset: 0x0013ECA3
		public Output? As_Output(GrammarBuilders g)
		{
			return new Output?(Output.CreateUnsafe(this.Node));
		}

		// Token: 0x06006284 RID: 25220 RVA: 0x00140AB5 File Offset: 0x0013ECB5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006285 RID: 25221 RVA: 0x00140AC8 File Offset: 0x0013ECC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006286 RID: 25222 RVA: 0x00140AF2 File Offset: 0x0013ECF2
		public bool Equals(output other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BFB RID: 11259
		private ProgramNode _node;
	}
}
