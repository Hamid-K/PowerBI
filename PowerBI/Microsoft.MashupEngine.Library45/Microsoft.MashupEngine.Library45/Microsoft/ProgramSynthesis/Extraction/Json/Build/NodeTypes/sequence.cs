using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes
{
	// Token: 0x02000B68 RID: 2920
	public struct sequence : IProgramNodeBuilder, IEquatable<sequence>
	{
		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x060049F5 RID: 18933 RVA: 0x000E8F82 File Offset: 0x000E7182
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060049F6 RID: 18934 RVA: 0x000E8F8A File Offset: 0x000E718A
		private sequence(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060049F7 RID: 18935 RVA: 0x000E8F93 File Offset: 0x000E7193
		public static sequence CreateUnsafe(ProgramNode node)
		{
			return new sequence(node);
		}

		// Token: 0x060049F8 RID: 18936 RVA: 0x000E8F9C File Offset: 0x000E719C
		public static sequence? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sequence)
			{
				return null;
			}
			return new sequence?(sequence.CreateUnsafe(node));
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x000E8FD6 File Offset: 0x000E71D6
		public static sequence CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sequence(new Hole(g.Symbol.sequence, holeId));
		}

		// Token: 0x060049FA RID: 18938 RVA: 0x000E8FEE File Offset: 0x000E71EE
		public bool Is_Sequence(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Sequence;
		}

		// Token: 0x060049FB RID: 18939 RVA: 0x000E9008 File Offset: 0x000E7208
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

		// Token: 0x060049FC RID: 18940 RVA: 0x000E9040 File Offset: 0x000E7240
		public Sequence? As_Sequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Sequence)
			{
				return null;
			}
			return new Sequence?(Sequence.CreateUnsafe(this.Node));
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x000E9080 File Offset: 0x000E7280
		public Sequence Cast_Sequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Sequence)
			{
				return Sequence.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Sequence is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060049FE RID: 18942 RVA: 0x000E90D5 File Offset: 0x000E72D5
		public bool Is_DummySequence(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DummySequence;
		}

		// Token: 0x060049FF RID: 18943 RVA: 0x000E90EF File Offset: 0x000E72EF
		public bool Is_DummySequence(GrammarBuilders g, out DummySequence value)
		{
			if (this.Node.GrammarRule == g.Rule.DummySequence)
			{
				value = DummySequence.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DummySequence);
			return false;
		}

		// Token: 0x06004A00 RID: 18944 RVA: 0x000E9124 File Offset: 0x000E7324
		public DummySequence? As_DummySequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DummySequence)
			{
				return null;
			}
			return new DummySequence?(DummySequence.CreateUnsafe(this.Node));
		}

		// Token: 0x06004A01 RID: 18945 RVA: 0x000E9164 File Offset: 0x000E7364
		public DummySequence Cast_DummySequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DummySequence)
			{
				return DummySequence.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DummySequence is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004A02 RID: 18946 RVA: 0x000E91BC File Offset: 0x000E73BC
		public T Switch<T>(GrammarBuilders g, Func<Sequence, T> func0, Func<DummySequence, T> func1)
		{
			Sequence sequence;
			if (this.Is_Sequence(g, out sequence))
			{
				return func0(sequence);
			}
			DummySequence dummySequence;
			if (this.Is_DummySequence(g, out dummySequence))
			{
				return func1(dummySequence);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol sequence");
		}

		// Token: 0x06004A03 RID: 18947 RVA: 0x000E9214 File Offset: 0x000E7414
		public void Switch(GrammarBuilders g, Action<Sequence> func0, Action<DummySequence> func1)
		{
			Sequence sequence;
			if (this.Is_Sequence(g, out sequence))
			{
				func0(sequence);
				return;
			}
			DummySequence dummySequence;
			if (this.Is_DummySequence(g, out dummySequence))
			{
				func1(dummySequence);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol sequence");
		}

		// Token: 0x06004A04 RID: 18948 RVA: 0x000E926B File Offset: 0x000E746B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004A05 RID: 18949 RVA: 0x000E9280 File Offset: 0x000E7480
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004A06 RID: 18950 RVA: 0x000E92AA File Offset: 0x000E74AA
		public bool Equals(sequence other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002163 RID: 8547
		private ProgramNode _node;
	}
}
