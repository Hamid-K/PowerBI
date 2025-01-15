using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200096E RID: 2414
	public struct splitSequence : IProgramNodeBuilder, IEquatable<splitSequence>
	{
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06003966 RID: 14694 RVA: 0x000B1E76 File Offset: 0x000B0076
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x000B1E7E File Offset: 0x000B007E
		private splitSequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x000B1E87 File Offset: 0x000B0087
		public static splitSequence CreateUnsafe(ProgramNode node)
		{
			return new splitSequence(node);
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000B1E90 File Offset: 0x000B0090
		public static splitSequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.splitSequence)
			{
				return null;
			}
			return new splitSequence?(splitSequence.CreateUnsafe(node));
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000B1ECA File Offset: 0x000B00CA
		public static splitSequence CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new splitSequence(new Hole(g.Symbol.splitSequence, holeId));
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000B1EE2 File Offset: 0x000B00E2
		public bool Is_SplitSequence(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SplitSequence;
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x000B1EFC File Offset: 0x000B00FC
		public bool Is_SplitSequence(GrammarBuilders g, out SplitSequence value)
		{
			if (this.Node.GrammarRule == g.Rule.SplitSequence)
			{
				value = SplitSequence.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SplitSequence);
			return false;
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x000B1F34 File Offset: 0x000B0134
		public SplitSequence? As_SplitSequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SplitSequence)
			{
				return null;
			}
			return new SplitSequence?(SplitSequence.CreateUnsafe(this.Node));
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x000B1F74 File Offset: 0x000B0174
		public SplitSequence Cast_SplitSequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SplitSequence)
			{
				return SplitSequence.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SplitSequence is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x000B1FC9 File Offset: 0x000B01C9
		public bool Is_Sequence(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Sequence;
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000B1FE3 File Offset: 0x000B01E3
		public bool Is_Sequence(GrammarBuilders g, out Sequence value)
		{
			if (this.Node.GrammarRule == g.Rule.Sequence)
			{
				value = Sequence.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Sequence);
			return false;
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000B2018 File Offset: 0x000B0218
		public Sequence? As_Sequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Sequence)
			{
				return null;
			}
			return new Sequence?(Sequence.CreateUnsafe(this.Node));
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000B2058 File Offset: 0x000B0258
		public Sequence Cast_Sequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Sequence)
			{
				return Sequence.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Sequence is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000B20B0 File Offset: 0x000B02B0
		public T Switch<T>(GrammarBuilders g, Func<SplitSequence, T> func0, Func<Sequence, T> func1)
		{
			SplitSequence splitSequence;
			if (this.Is_SplitSequence(g, out splitSequence))
			{
				return func0(splitSequence);
			}
			Sequence sequence;
			if (this.Is_Sequence(g, out sequence))
			{
				return func1(sequence);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol splitSequence");
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000B2108 File Offset: 0x000B0308
		public void Switch(GrammarBuilders g, Action<SplitSequence> func0, Action<Sequence> func1)
		{
			SplitSequence splitSequence;
			if (this.Is_SplitSequence(g, out splitSequence))
			{
				func0(splitSequence);
				return;
			}
			Sequence sequence;
			if (this.Is_Sequence(g, out sequence))
			{
				func1(sequence);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol splitSequence");
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000B215F File Offset: 0x000B035F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000B2174 File Offset: 0x000B0374
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000B219E File Offset: 0x000B039E
		public bool Equals(splitSequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A8E RID: 6798
		private ProgramNode _node;
	}
}
