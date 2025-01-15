using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C33 RID: 7219
	public struct LetPL2 : IProgramNodeBuilder, IEquatable<LetPL2>
	{
		// Token: 0x170028C1 RID: 10433
		// (get) Token: 0x0600F338 RID: 62264 RVA: 0x003422A6 File Offset: 0x003404A6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F339 RID: 62265 RVA: 0x003422AE File Offset: 0x003404AE
		private LetPL2(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F33A RID: 62266 RVA: 0x003422B7 File Offset: 0x003404B7
		public static LetPL2 CreateUnsafe(ProgramNode node)
		{
			return new LetPL2(node);
		}

		// Token: 0x0600F33B RID: 62267 RVA: 0x003422C0 File Offset: 0x003404C0
		public static LetPL2? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetPL2)
			{
				return null;
			}
			return new LetPL2?(LetPL2.CreateUnsafe(node));
		}

		// Token: 0x0600F33C RID: 62268 RVA: 0x003422F5 File Offset: 0x003404F5
		public LetPL2(GrammarBuilders g, pos value0, _LetB4 value1)
		{
			this._node = new LetNode(g.Rule.LetPL2, value0.Node, value1.Node);
		}

		// Token: 0x0600F33D RID: 62269 RVA: 0x0034231B File Offset: 0x0034051B
		public static implicit operator _LetB6(LetPL2 arg)
		{
			return _LetB6.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028C2 RID: 10434
		// (get) Token: 0x0600F33E RID: 62270 RVA: 0x00342329 File Offset: 0x00340529
		public pos pos
		{
			get
			{
				return pos.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170028C3 RID: 10435
		// (get) Token: 0x0600F33F RID: 62271 RVA: 0x0034233D File Offset: 0x0034053D
		public _LetB4 _LetB4
		{
			get
			{
				return _LetB4.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F340 RID: 62272 RVA: 0x00342351 File Offset: 0x00340551
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F341 RID: 62273 RVA: 0x00342364 File Offset: 0x00340564
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F342 RID: 62274 RVA: 0x0034238E File Offset: 0x0034058E
		public bool Equals(LetPL2 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B22 RID: 23330
		private ProgramNode _node;
	}
}
