using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001375 RID: 4981
	public struct _LetB3 : IProgramNodeBuilder, IEquatable<_LetB3>
	{
		// Token: 0x17001A7E RID: 6782
		// (get) Token: 0x06009A83 RID: 39555 RVA: 0x0020B0C2 File Offset: 0x002092C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009A84 RID: 39556 RVA: 0x0020B0CA File Offset: 0x002092CA
		private _LetB3(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009A85 RID: 39557 RVA: 0x0020B0D3 File Offset: 0x002092D3
		public static _LetB3 CreateUnsafe(ProgramNode node)
		{
			return new _LetB3(node);
		}

		// Token: 0x06009A86 RID: 39558 RVA: 0x0020B0DC File Offset: 0x002092DC
		public static _LetB3? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB3)
			{
				return null;
			}
			return new _LetB3?(_LetB3.CreateUnsafe(node));
		}

		// Token: 0x06009A87 RID: 39559 RVA: 0x0020B116 File Offset: 0x00209316
		public static _LetB3 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB3(new Hole(g.Symbol._LetB3, holeId));
		}

		// Token: 0x06009A88 RID: 39560 RVA: 0x0020B12E File Offset: 0x0020932E
		public InnerLetWitness Cast_InnerLetWitness()
		{
			return InnerLetWitness.CreateUnsafe(this.Node);
		}

		// Token: 0x06009A89 RID: 39561 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_InnerLetWitness(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06009A8A RID: 39562 RVA: 0x0020B13B File Offset: 0x0020933B
		public bool Is_InnerLetWitness(GrammarBuilders g, out InnerLetWitness value)
		{
			value = InnerLetWitness.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06009A8B RID: 39563 RVA: 0x0020B14F File Offset: 0x0020934F
		public InnerLetWitness? As_InnerLetWitness(GrammarBuilders g)
		{
			return new InnerLetWitness?(InnerLetWitness.CreateUnsafe(this.Node));
		}

		// Token: 0x06009A8C RID: 39564 RVA: 0x0020B161 File Offset: 0x00209361
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009A8D RID: 39565 RVA: 0x0020B174 File Offset: 0x00209374
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009A8E RID: 39566 RVA: 0x0020B19E File Offset: 0x0020939E
		public bool Equals(_LetB3 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DEC RID: 15852
		private ProgramNode _node;
	}
}
