using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C36 RID: 7222
	public struct LetPredicate : IProgramNodeBuilder, IEquatable<LetPredicate>
	{
		// Token: 0x170028CA RID: 10442
		// (get) Token: 0x0600F359 RID: 62297 RVA: 0x0034259A File Offset: 0x0034079A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F35A RID: 62298 RVA: 0x003425A2 File Offset: 0x003407A2
		private LetPredicate(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F35B RID: 62299 RVA: 0x003425AB File Offset: 0x003407AB
		public static LetPredicate CreateUnsafe(ProgramNode node)
		{
			return new LetPredicate(node);
		}

		// Token: 0x0600F35C RID: 62300 RVA: 0x003425B4 File Offset: 0x003407B4
		public static LetPredicate? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetPredicate)
			{
				return null;
			}
			return new LetPredicate?(LetPredicate.CreateUnsafe(node));
		}

		// Token: 0x0600F35D RID: 62301 RVA: 0x003425E9 File Offset: 0x003407E9
		public LetPredicate(GrammarBuilders g, y value0, pred value1)
		{
			this._node = new LetNode(g.Rule.LetPredicate, value0.Node, value1.Node);
		}

		// Token: 0x0600F35E RID: 62302 RVA: 0x0034260F File Offset: 0x0034080F
		public static implicit operator b(LetPredicate arg)
		{
			return b.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028CB RID: 10443
		// (get) Token: 0x0600F35F RID: 62303 RVA: 0x0034261D File Offset: 0x0034081D
		public y y
		{
			get
			{
				return y.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170028CC RID: 10444
		// (get) Token: 0x0600F360 RID: 62304 RVA: 0x00342631 File Offset: 0x00340831
		public pred pred
		{
			get
			{
				return pred.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F361 RID: 62305 RVA: 0x00342645 File Offset: 0x00340845
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F362 RID: 62306 RVA: 0x00342658 File Offset: 0x00340858
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F363 RID: 62307 RVA: 0x00342682 File Offset: 0x00340882
		public bool Equals(LetPredicate other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B25 RID: 23333
		private ProgramNode _node;
	}
}
