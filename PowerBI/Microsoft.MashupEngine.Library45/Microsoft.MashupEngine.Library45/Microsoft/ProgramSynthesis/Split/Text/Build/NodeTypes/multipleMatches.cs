using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001361 RID: 4961
	public struct multipleMatches : IProgramNodeBuilder, IEquatable<multipleMatches>
	{
		// Token: 0x17001A6A RID: 6762
		// (get) Token: 0x0600994B RID: 39243 RVA: 0x002083E6 File Offset: 0x002065E6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600994C RID: 39244 RVA: 0x002083EE File Offset: 0x002065EE
		private multipleMatches(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600994D RID: 39245 RVA: 0x002083F7 File Offset: 0x002065F7
		public static multipleMatches CreateUnsafe(ProgramNode node)
		{
			return new multipleMatches(node);
		}

		// Token: 0x0600994E RID: 39246 RVA: 0x00208400 File Offset: 0x00206600
		public static multipleMatches? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.multipleMatches)
			{
				return null;
			}
			return new multipleMatches?(multipleMatches.CreateUnsafe(node));
		}

		// Token: 0x0600994F RID: 39247 RVA: 0x0020843A File Offset: 0x0020663A
		public static multipleMatches CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new multipleMatches(new Hole(g.Symbol.multipleMatches, holeId));
		}

		// Token: 0x06009950 RID: 39248 RVA: 0x00208452 File Offset: 0x00206652
		public bool Is_SplitMultiple(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SplitMultiple;
		}

		// Token: 0x06009951 RID: 39249 RVA: 0x0020846C File Offset: 0x0020666C
		public bool Is_SplitMultiple(GrammarBuilders g, out SplitMultiple value)
		{
			if (this.Node.GrammarRule == g.Rule.SplitMultiple)
			{
				value = SplitMultiple.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SplitMultiple);
			return false;
		}

		// Token: 0x06009952 RID: 39250 RVA: 0x002084A4 File Offset: 0x002066A4
		public SplitMultiple? As_SplitMultiple(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SplitMultiple)
			{
				return null;
			}
			return new SplitMultiple?(SplitMultiple.CreateUnsafe(this.Node));
		}

		// Token: 0x06009953 RID: 39251 RVA: 0x002084E4 File Offset: 0x002066E4
		public SplitMultiple Cast_SplitMultiple(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SplitMultiple)
			{
				return SplitMultiple.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SplitMultiple is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06009954 RID: 39252 RVA: 0x00208539 File Offset: 0x00206739
		public bool Is_multipleMatches_d(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.multipleMatches_d;
		}

		// Token: 0x06009955 RID: 39253 RVA: 0x00208553 File Offset: 0x00206753
		public bool Is_multipleMatches_d(GrammarBuilders g, out multipleMatches_d value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.multipleMatches_d)
			{
				value = multipleMatches_d.CreateUnsafe(this.Node);
				return true;
			}
			value = default(multipleMatches_d);
			return false;
		}

		// Token: 0x06009956 RID: 39254 RVA: 0x00208588 File Offset: 0x00206788
		public multipleMatches_d? As_multipleMatches_d(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.multipleMatches_d)
			{
				return null;
			}
			return new multipleMatches_d?(multipleMatches_d.CreateUnsafe(this.Node));
		}

		// Token: 0x06009957 RID: 39255 RVA: 0x002085C8 File Offset: 0x002067C8
		public multipleMatches_d Cast_multipleMatches_d(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.multipleMatches_d)
			{
				return multipleMatches_d.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_multipleMatches_d is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06009958 RID: 39256 RVA: 0x00208620 File Offset: 0x00206820
		public T Switch<T>(GrammarBuilders g, Func<SplitMultiple, T> func0, Func<multipleMatches_d, T> func1)
		{
			SplitMultiple splitMultiple;
			if (this.Is_SplitMultiple(g, out splitMultiple))
			{
				return func0(splitMultiple);
			}
			multipleMatches_d multipleMatches_d;
			if (this.Is_multipleMatches_d(g, out multipleMatches_d))
			{
				return func1(multipleMatches_d);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol multipleMatches");
		}

		// Token: 0x06009959 RID: 39257 RVA: 0x00208678 File Offset: 0x00206878
		public void Switch(GrammarBuilders g, Action<SplitMultiple> func0, Action<multipleMatches_d> func1)
		{
			SplitMultiple splitMultiple;
			if (this.Is_SplitMultiple(g, out splitMultiple))
			{
				func0(splitMultiple);
				return;
			}
			multipleMatches_d multipleMatches_d;
			if (this.Is_multipleMatches_d(g, out multipleMatches_d))
			{
				func1(multipleMatches_d);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol multipleMatches");
		}

		// Token: 0x0600995A RID: 39258 RVA: 0x002086CF File Offset: 0x002068CF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600995B RID: 39259 RVA: 0x002086E4 File Offset: 0x002068E4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600995C RID: 39260 RVA: 0x0020870E File Offset: 0x0020690E
		public bool Equals(multipleMatches other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DD8 RID: 15832
		private ProgramNode _node;
	}
}
