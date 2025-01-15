using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E90 RID: 7824
	public struct value : IProgramNodeBuilder, IEquatable<value>
	{
		// Token: 0x17002BED RID: 11245
		// (get) Token: 0x0601087E RID: 67710 RVA: 0x0038DAEA File Offset: 0x0038BCEA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601087F RID: 67711 RVA: 0x0038DAF2 File Offset: 0x0038BCF2
		private value(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010880 RID: 67712 RVA: 0x0038DAFB File Offset: 0x0038BCFB
		public static value CreateUnsafe(ProgramNode node)
		{
			return new value(node);
		}

		// Token: 0x06010881 RID: 67713 RVA: 0x0038DB04 File Offset: 0x0038BD04
		public static value? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.value)
			{
				return null;
			}
			return new value?(value.CreateUnsafe(node));
		}

		// Token: 0x06010882 RID: 67714 RVA: 0x0038DB3E File Offset: 0x0038BD3E
		public static value CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new value(new Hole(g.Symbol.value, holeId));
		}

		// Token: 0x06010883 RID: 67715 RVA: 0x0038DB56 File Offset: 0x0038BD56
		public value(GrammarBuilders g, string value)
		{
			this = new value(new LiteralNode(g.Symbol.value, value));
		}

		// Token: 0x17002BEE RID: 11246
		// (get) Token: 0x06010884 RID: 67716 RVA: 0x0038DB6F File Offset: 0x0038BD6F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06010885 RID: 67717 RVA: 0x0038DB86 File Offset: 0x0038BD86
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010886 RID: 67718 RVA: 0x0038DB9C File Offset: 0x0038BD9C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010887 RID: 67719 RVA: 0x0038DBC6 File Offset: 0x0038BDC6
		public bool Equals(value other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062CF RID: 25295
		private ProgramNode _node;
	}
}
