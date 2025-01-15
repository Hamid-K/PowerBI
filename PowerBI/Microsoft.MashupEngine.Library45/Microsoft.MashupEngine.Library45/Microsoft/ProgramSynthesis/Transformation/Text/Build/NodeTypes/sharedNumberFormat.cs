using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C76 RID: 7286
	public struct sharedNumberFormat : IProgramNodeBuilder, IEquatable<sharedNumberFormat>
	{
		// Token: 0x17002924 RID: 10532
		// (get) Token: 0x0600F6D4 RID: 63188 RVA: 0x00349D32 File Offset: 0x00347F32
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F6D5 RID: 63189 RVA: 0x00349D3A File Offset: 0x00347F3A
		private sharedNumberFormat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F6D6 RID: 63190 RVA: 0x00349D43 File Offset: 0x00347F43
		public static sharedNumberFormat CreateUnsafe(ProgramNode node)
		{
			return new sharedNumberFormat(node);
		}

		// Token: 0x0600F6D7 RID: 63191 RVA: 0x00349D4C File Offset: 0x00347F4C
		public static sharedNumberFormat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sharedNumberFormat)
			{
				return null;
			}
			return new sharedNumberFormat?(sharedNumberFormat.CreateUnsafe(node));
		}

		// Token: 0x0600F6D8 RID: 63192 RVA: 0x00349D86 File Offset: 0x00347F86
		public static sharedNumberFormat CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sharedNumberFormat(new Hole(g.Symbol.sharedNumberFormat, holeId));
		}

		// Token: 0x0600F6D9 RID: 63193 RVA: 0x00349D9E File Offset: 0x00347F9E
		public sharedNumberFormat(GrammarBuilders g)
		{
			this = new sharedNumberFormat(new VariableNode(g.Symbol.sharedNumberFormat));
		}

		// Token: 0x17002925 RID: 10533
		// (get) Token: 0x0600F6DA RID: 63194 RVA: 0x00349DB6 File Offset: 0x00347FB6
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600F6DB RID: 63195 RVA: 0x00349DC3 File Offset: 0x00347FC3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F6DC RID: 63196 RVA: 0x00349DD8 File Offset: 0x00347FD8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F6DD RID: 63197 RVA: 0x00349E02 File Offset: 0x00348002
		public bool Equals(sharedNumberFormat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B65 RID: 23397
		private ProgramNode _node;
	}
}
