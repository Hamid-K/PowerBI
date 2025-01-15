using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001367 RID: 4967
	public struct c : IProgramNodeBuilder, IEquatable<c>
	{
		// Token: 0x17001A70 RID: 6768
		// (get) Token: 0x060099B5 RID: 39349 RVA: 0x0020960E File Offset: 0x0020780E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060099B6 RID: 39350 RVA: 0x00209616 File Offset: 0x00207816
		private c(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060099B7 RID: 39351 RVA: 0x0020961F File Offset: 0x0020781F
		public static c CreateUnsafe(ProgramNode node)
		{
			return new c(node);
		}

		// Token: 0x060099B8 RID: 39352 RVA: 0x00209628 File Offset: 0x00207828
		public static c? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.c)
			{
				return null;
			}
			return new c?(c.CreateUnsafe(node));
		}

		// Token: 0x060099B9 RID: 39353 RVA: 0x00209662 File Offset: 0x00207862
		public static c CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new c(new Hole(g.Symbol.c, holeId));
		}

		// Token: 0x060099BA RID: 39354 RVA: 0x0020967A File Offset: 0x0020787A
		public bool Is_ConstStr(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConstStr;
		}

		// Token: 0x060099BB RID: 39355 RVA: 0x00209694 File Offset: 0x00207894
		public bool Is_ConstStr(GrammarBuilders g, out ConstStr value)
		{
			if (this.Node.GrammarRule == g.Rule.ConstStr)
			{
				value = ConstStr.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConstStr);
			return false;
		}

		// Token: 0x060099BC RID: 39356 RVA: 0x002096CC File Offset: 0x002078CC
		public ConstStr? As_ConstStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConstStr)
			{
				return null;
			}
			return new ConstStr?(ConstStr.CreateUnsafe(this.Node));
		}

		// Token: 0x060099BD RID: 39357 RVA: 0x0020970C File Offset: 0x0020790C
		public ConstStr Cast_ConstStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConstStr)
			{
				return ConstStr.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConstStr is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060099BE RID: 39358 RVA: 0x00209761 File Offset: 0x00207961
		public bool Is_ConstStrWithWhitespace(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConstStrWithWhitespace;
		}

		// Token: 0x060099BF RID: 39359 RVA: 0x0020977B File Offset: 0x0020797B
		public bool Is_ConstStrWithWhitespace(GrammarBuilders g, out ConstStrWithWhitespace value)
		{
			if (this.Node.GrammarRule == g.Rule.ConstStrWithWhitespace)
			{
				value = ConstStrWithWhitespace.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConstStrWithWhitespace);
			return false;
		}

		// Token: 0x060099C0 RID: 39360 RVA: 0x002097B0 File Offset: 0x002079B0
		public ConstStrWithWhitespace? As_ConstStrWithWhitespace(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConstStrWithWhitespace)
			{
				return null;
			}
			return new ConstStrWithWhitespace?(ConstStrWithWhitespace.CreateUnsafe(this.Node));
		}

		// Token: 0x060099C1 RID: 39361 RVA: 0x002097F0 File Offset: 0x002079F0
		public ConstStrWithWhitespace Cast_ConstStrWithWhitespace(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConstStrWithWhitespace)
			{
				return ConstStrWithWhitespace.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConstStrWithWhitespace is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060099C2 RID: 39362 RVA: 0x00209845 File Offset: 0x00207A45
		public bool Is_ConstAlphStr(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConstAlphStr;
		}

		// Token: 0x060099C3 RID: 39363 RVA: 0x0020985F File Offset: 0x00207A5F
		public bool Is_ConstAlphStr(GrammarBuilders g, out ConstAlphStr value)
		{
			if (this.Node.GrammarRule == g.Rule.ConstAlphStr)
			{
				value = ConstAlphStr.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConstAlphStr);
			return false;
		}

		// Token: 0x060099C4 RID: 39364 RVA: 0x00209894 File Offset: 0x00207A94
		public ConstAlphStr? As_ConstAlphStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConstAlphStr)
			{
				return null;
			}
			return new ConstAlphStr?(ConstAlphStr.CreateUnsafe(this.Node));
		}

		// Token: 0x060099C5 RID: 39365 RVA: 0x002098D4 File Offset: 0x00207AD4
		public ConstAlphStr Cast_ConstAlphStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConstAlphStr)
			{
				return ConstAlphStr.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConstAlphStr is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060099C6 RID: 39366 RVA: 0x0020992C File Offset: 0x00207B2C
		public T Switch<T>(GrammarBuilders g, Func<ConstStr, T> func0, Func<ConstStrWithWhitespace, T> func1, Func<ConstAlphStr, T> func2)
		{
			ConstStr constStr;
			if (this.Is_ConstStr(g, out constStr))
			{
				return func0(constStr);
			}
			ConstStrWithWhitespace constStrWithWhitespace;
			if (this.Is_ConstStrWithWhitespace(g, out constStrWithWhitespace))
			{
				return func1(constStrWithWhitespace);
			}
			ConstAlphStr constAlphStr;
			if (this.Is_ConstAlphStr(g, out constAlphStr))
			{
				return func2(constAlphStr);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol c");
		}

		// Token: 0x060099C7 RID: 39367 RVA: 0x00209998 File Offset: 0x00207B98
		public void Switch(GrammarBuilders g, Action<ConstStr> func0, Action<ConstStrWithWhitespace> func1, Action<ConstAlphStr> func2)
		{
			ConstStr constStr;
			if (this.Is_ConstStr(g, out constStr))
			{
				func0(constStr);
				return;
			}
			ConstStrWithWhitespace constStrWithWhitespace;
			if (this.Is_ConstStrWithWhitespace(g, out constStrWithWhitespace))
			{
				func1(constStrWithWhitespace);
				return;
			}
			ConstAlphStr constAlphStr;
			if (this.Is_ConstAlphStr(g, out constAlphStr))
			{
				func2(constAlphStr);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol c");
		}

		// Token: 0x060099C8 RID: 39368 RVA: 0x00209A03 File Offset: 0x00207C03
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060099C9 RID: 39369 RVA: 0x00209A18 File Offset: 0x00207C18
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060099CA RID: 39370 RVA: 0x00209A42 File Offset: 0x00207C42
		public bool Equals(c other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DDE RID: 15838
		private ProgramNode _node;
	}
}
