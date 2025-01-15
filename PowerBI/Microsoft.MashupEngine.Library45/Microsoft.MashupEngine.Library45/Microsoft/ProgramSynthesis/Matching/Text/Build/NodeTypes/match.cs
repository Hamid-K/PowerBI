using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes
{
	// Token: 0x020011F2 RID: 4594
	public struct match : IProgramNodeBuilder, IEquatable<match>
	{
		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x06008A34 RID: 35380 RVA: 0x001D057A File Offset: 0x001CE77A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008A35 RID: 35381 RVA: 0x001D0582 File Offset: 0x001CE782
		private match(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008A36 RID: 35382 RVA: 0x001D058B File Offset: 0x001CE78B
		public static match CreateUnsafe(ProgramNode node)
		{
			return new match(node);
		}

		// Token: 0x06008A37 RID: 35383 RVA: 0x001D0594 File Offset: 0x001CE794
		public static match? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.match)
			{
				return null;
			}
			return new match?(match.CreateUnsafe(node));
		}

		// Token: 0x06008A38 RID: 35384 RVA: 0x001D05CE File Offset: 0x001CE7CE
		public static match CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new match(new Hole(g.Symbol.match, holeId));
		}

		// Token: 0x06008A39 RID: 35385 RVA: 0x001D05E6 File Offset: 0x001CE7E6
		public bool Is_IsNull(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IsNull;
		}

		// Token: 0x06008A3A RID: 35386 RVA: 0x001D0600 File Offset: 0x001CE800
		public bool Is_IsNull(GrammarBuilders g, out IsNull value)
		{
			if (this.Node.GrammarRule == g.Rule.IsNull)
			{
				value = IsNull.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IsNull);
			return false;
		}

		// Token: 0x06008A3B RID: 35387 RVA: 0x001D0638 File Offset: 0x001CE838
		public IsNull? As_IsNull(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IsNull)
			{
				return null;
			}
			return new IsNull?(IsNull.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A3C RID: 35388 RVA: 0x001D0678 File Offset: 0x001CE878
		public IsNull Cast_IsNull(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IsNull)
			{
				return IsNull.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IsNull is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008A3D RID: 35389 RVA: 0x001D06CD File Offset: 0x001CE8CD
		public bool Is_EndOf(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.EndOf;
		}

		// Token: 0x06008A3E RID: 35390 RVA: 0x001D06E7 File Offset: 0x001CE8E7
		public bool Is_EndOf(GrammarBuilders g, out EndOf value)
		{
			if (this.Node.GrammarRule == g.Rule.EndOf)
			{
				value = EndOf.CreateUnsafe(this.Node);
				return true;
			}
			value = default(EndOf);
			return false;
		}

		// Token: 0x06008A3F RID: 35391 RVA: 0x001D071C File Offset: 0x001CE91C
		public EndOf? As_EndOf(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.EndOf)
			{
				return null;
			}
			return new EndOf?(EndOf.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A40 RID: 35392 RVA: 0x001D075C File Offset: 0x001CE95C
		public EndOf Cast_EndOf(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.EndOf)
			{
				return EndOf.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_EndOf is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008A41 RID: 35393 RVA: 0x001D07B1 File Offset: 0x001CE9B1
		public bool Is_LetSplit(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LetSplit;
		}

		// Token: 0x06008A42 RID: 35394 RVA: 0x001D07CB File Offset: 0x001CE9CB
		public bool Is_LetSplit(GrammarBuilders g, out LetSplit value)
		{
			if (this.Node.GrammarRule == g.Rule.LetSplit)
			{
				value = LetSplit.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LetSplit);
			return false;
		}

		// Token: 0x06008A43 RID: 35395 RVA: 0x001D0800 File Offset: 0x001CEA00
		public LetSplit? As_LetSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LetSplit)
			{
				return null;
			}
			return new LetSplit?(LetSplit.CreateUnsafe(this.Node));
		}

		// Token: 0x06008A44 RID: 35396 RVA: 0x001D0840 File Offset: 0x001CEA40
		public LetSplit Cast_LetSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LetSplit)
			{
				return LetSplit.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LetSplit is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06008A45 RID: 35397 RVA: 0x001D0898 File Offset: 0x001CEA98
		public T Switch<T>(GrammarBuilders g, Func<IsNull, T> func0, Func<EndOf, T> func1, Func<LetSplit, T> func2)
		{
			IsNull isNull;
			if (this.Is_IsNull(g, out isNull))
			{
				return func0(isNull);
			}
			EndOf endOf;
			if (this.Is_EndOf(g, out endOf))
			{
				return func1(endOf);
			}
			LetSplit letSplit;
			if (this.Is_LetSplit(g, out letSplit))
			{
				return func2(letSplit);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol match");
		}

		// Token: 0x06008A46 RID: 35398 RVA: 0x001D0904 File Offset: 0x001CEB04
		public void Switch(GrammarBuilders g, Action<IsNull> func0, Action<EndOf> func1, Action<LetSplit> func2)
		{
			IsNull isNull;
			if (this.Is_IsNull(g, out isNull))
			{
				func0(isNull);
				return;
			}
			EndOf endOf;
			if (this.Is_EndOf(g, out endOf))
			{
				func1(endOf);
				return;
			}
			LetSplit letSplit;
			if (this.Is_LetSplit(g, out letSplit))
			{
				func2(letSplit);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol match");
		}

		// Token: 0x06008A47 RID: 35399 RVA: 0x001D096F File Offset: 0x001CEB6F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008A48 RID: 35400 RVA: 0x001D0984 File Offset: 0x001CEB84
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008A49 RID: 35401 RVA: 0x001D09AE File Offset: 0x001CEBAE
		public bool Equals(match other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040038A6 RID: 14502
		private ProgramNode _node;
	}
}
