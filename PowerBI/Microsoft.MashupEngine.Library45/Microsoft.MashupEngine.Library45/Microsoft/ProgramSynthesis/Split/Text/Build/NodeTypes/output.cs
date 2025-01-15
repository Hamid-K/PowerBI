using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001370 RID: 4976
	public struct output : IProgramNodeBuilder, IEquatable<output>
	{
		// Token: 0x17001A79 RID: 6777
		// (get) Token: 0x06009A41 RID: 39489 RVA: 0x0020A9C6 File Offset: 0x00208BC6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009A42 RID: 39490 RVA: 0x0020A9CE File Offset: 0x00208BCE
		private output(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009A43 RID: 39491 RVA: 0x0020A9D7 File Offset: 0x00208BD7
		public static output CreateUnsafe(ProgramNode node)
		{
			return new output(node);
		}

		// Token: 0x06009A44 RID: 39492 RVA: 0x0020A9E0 File Offset: 0x00208BE0
		public static output? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.output)
			{
				return null;
			}
			return new output?(output.CreateUnsafe(node));
		}

		// Token: 0x06009A45 RID: 39493 RVA: 0x0020AA1A File Offset: 0x00208C1A
		public static output CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new output(new Hole(g.Symbol.output, holeId));
		}

		// Token: 0x06009A46 RID: 39494 RVA: 0x0020AA32 File Offset: 0x00208C32
		public bool Is_List(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.List;
		}

		// Token: 0x06009A47 RID: 39495 RVA: 0x0020AA4C File Offset: 0x00208C4C
		public bool Is_List(GrammarBuilders g, out List value)
		{
			if (this.Node.GrammarRule == g.Rule.List)
			{
				value = List.CreateUnsafe(this.Node);
				return true;
			}
			value = default(List);
			return false;
		}

		// Token: 0x06009A48 RID: 39496 RVA: 0x0020AA84 File Offset: 0x00208C84
		public List? As_List(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.List)
			{
				return null;
			}
			return new List?(List.CreateUnsafe(this.Node));
		}

		// Token: 0x06009A49 RID: 39497 RVA: 0x0020AAC4 File Offset: 0x00208CC4
		public List Cast_List(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.List)
			{
				return List.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_List is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06009A4A RID: 39498 RVA: 0x0020AB19 File Offset: 0x00208D19
		public bool Is_OuterLetWitness(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.OuterLetWitness;
		}

		// Token: 0x06009A4B RID: 39499 RVA: 0x0020AB33 File Offset: 0x00208D33
		public bool Is_OuterLetWitness(GrammarBuilders g, out OuterLetWitness value)
		{
			if (this.Node.GrammarRule == g.Rule.OuterLetWitness)
			{
				value = OuterLetWitness.CreateUnsafe(this.Node);
				return true;
			}
			value = default(OuterLetWitness);
			return false;
		}

		// Token: 0x06009A4C RID: 39500 RVA: 0x0020AB68 File Offset: 0x00208D68
		public OuterLetWitness? As_OuterLetWitness(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.OuterLetWitness)
			{
				return null;
			}
			return new OuterLetWitness?(OuterLetWitness.CreateUnsafe(this.Node));
		}

		// Token: 0x06009A4D RID: 39501 RVA: 0x0020ABA8 File Offset: 0x00208DA8
		public OuterLetWitness Cast_OuterLetWitness(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.OuterLetWitness)
			{
				return OuterLetWitness.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_OuterLetWitness is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06009A4E RID: 39502 RVA: 0x0020AC00 File Offset: 0x00208E00
		public T Switch<T>(GrammarBuilders g, Func<List, T> func0, Func<OuterLetWitness, T> func1)
		{
			List list;
			if (this.Is_List(g, out list))
			{
				return func0(list);
			}
			OuterLetWitness outerLetWitness;
			if (this.Is_OuterLetWitness(g, out outerLetWitness))
			{
				return func1(outerLetWitness);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol output");
		}

		// Token: 0x06009A4F RID: 39503 RVA: 0x0020AC58 File Offset: 0x00208E58
		public void Switch(GrammarBuilders g, Action<List> func0, Action<OuterLetWitness> func1)
		{
			List list;
			if (this.Is_List(g, out list))
			{
				func0(list);
				return;
			}
			OuterLetWitness outerLetWitness;
			if (this.Is_OuterLetWitness(g, out outerLetWitness))
			{
				func1(outerLetWitness);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol output");
		}

		// Token: 0x06009A50 RID: 39504 RVA: 0x0020ACAF File Offset: 0x00208EAF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009A51 RID: 39505 RVA: 0x0020ACC4 File Offset: 0x00208EC4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009A52 RID: 39506 RVA: 0x0020ACEE File Offset: 0x00208EEE
		public bool Equals(output other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DE7 RID: 15847
		private ProgramNode _node;
	}
}
