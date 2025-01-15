using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x02001356 RID: 4950
	public struct GEN_LookAround : IProgramNodeBuilder, IEquatable<GEN_LookAround>
	{
		// Token: 0x17001A4E RID: 6734
		// (get) Token: 0x060098C1 RID: 39105 RVA: 0x00207396 File Offset: 0x00205596
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060098C2 RID: 39106 RVA: 0x0020739E File Offset: 0x0020559E
		private GEN_LookAround(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060098C3 RID: 39107 RVA: 0x002073A7 File Offset: 0x002055A7
		public static GEN_LookAround CreateUnsafe(ProgramNode node)
		{
			return new GEN_LookAround(node);
		}

		// Token: 0x060098C4 RID: 39108 RVA: 0x002073B0 File Offset: 0x002055B0
		public static GEN_LookAround? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.GEN_LookAround)
			{
				return null;
			}
			return new GEN_LookAround?(GEN_LookAround.CreateUnsafe(node));
		}

		// Token: 0x060098C5 RID: 39109 RVA: 0x002073E5 File Offset: 0x002055E5
		public GEN_LookAround(GrammarBuilders g, obj value0, obj value1, obj value2)
		{
			this._node = g.Rule.GEN_LookAround.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x060098C6 RID: 39110 RVA: 0x00207412 File Offset: 0x00205612
		public static implicit operator gen_LookAround(GEN_LookAround arg)
		{
			return gen_LookAround.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A4F RID: 6735
		// (get) Token: 0x060098C7 RID: 39111 RVA: 0x00207420 File Offset: 0x00205620
		public obj obj1
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A50 RID: 6736
		// (get) Token: 0x060098C8 RID: 39112 RVA: 0x00207434 File Offset: 0x00205634
		public obj obj2
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17001A51 RID: 6737
		// (get) Token: 0x060098C9 RID: 39113 RVA: 0x00207448 File Offset: 0x00205648
		public obj obj3
		{
			get
			{
				return obj.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x060098CA RID: 39114 RVA: 0x0020745C File Offset: 0x0020565C
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060098CB RID: 39115 RVA: 0x00207470 File Offset: 0x00205670
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060098CC RID: 39116 RVA: 0x0020749A File Offset: 0x0020569A
		public bool Equals(GEN_LookAround other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DCD RID: 15821
		private ProgramNode _node;
	}
}
