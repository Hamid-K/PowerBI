using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015BF RID: 5567
	public struct sliceTrim : IProgramNodeBuilder, IEquatable<sliceTrim>
	{
		// Token: 0x17001FE5 RID: 8165
		// (get) Token: 0x0600B854 RID: 47188 RVA: 0x0027F19A File Offset: 0x0027D39A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B855 RID: 47189 RVA: 0x0027F1A2 File Offset: 0x0027D3A2
		private sliceTrim(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B856 RID: 47190 RVA: 0x0027F1AB File Offset: 0x0027D3AB
		public static sliceTrim CreateUnsafe(ProgramNode node)
		{
			return new sliceTrim(node);
		}

		// Token: 0x0600B857 RID: 47191 RVA: 0x0027F1B4 File Offset: 0x0027D3B4
		public static sliceTrim? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sliceTrim)
			{
				return null;
			}
			return new sliceTrim?(sliceTrim.CreateUnsafe(node));
		}

		// Token: 0x0600B858 RID: 47192 RVA: 0x0027F1EE File Offset: 0x0027D3EE
		public static sliceTrim CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sliceTrim(new Hole(g.Symbol.sliceTrim, holeId));
		}

		// Token: 0x0600B859 RID: 47193 RVA: 0x0027F206 File Offset: 0x0027D406
		public bool Is_sliceTrim_slice(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.sliceTrim_slice;
		}

		// Token: 0x0600B85A RID: 47194 RVA: 0x0027F220 File Offset: 0x0027D420
		public bool Is_sliceTrim_slice(GrammarBuilders g, out sliceTrim_slice value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.sliceTrim_slice)
			{
				value = sliceTrim_slice.CreateUnsafe(this.Node);
				return true;
			}
			value = default(sliceTrim_slice);
			return false;
		}

		// Token: 0x0600B85B RID: 47195 RVA: 0x0027F258 File Offset: 0x0027D458
		public sliceTrim_slice? As_sliceTrim_slice(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.sliceTrim_slice)
			{
				return null;
			}
			return new sliceTrim_slice?(sliceTrim_slice.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B85C RID: 47196 RVA: 0x0027F298 File Offset: 0x0027D498
		public sliceTrim_slice Cast_sliceTrim_slice(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.sliceTrim_slice)
			{
				return sliceTrim_slice.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_sliceTrim_slice is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B85D RID: 47197 RVA: 0x0027F2ED File Offset: 0x0027D4ED
		public bool Is_TrimSlice(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimSlice;
		}

		// Token: 0x0600B85E RID: 47198 RVA: 0x0027F307 File Offset: 0x0027D507
		public bool Is_TrimSlice(GrammarBuilders g, out TrimSlice value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimSlice)
			{
				value = TrimSlice.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimSlice);
			return false;
		}

		// Token: 0x0600B85F RID: 47199 RVA: 0x0027F33C File Offset: 0x0027D53C
		public TrimSlice? As_TrimSlice(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimSlice)
			{
				return null;
			}
			return new TrimSlice?(TrimSlice.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B860 RID: 47200 RVA: 0x0027F37C File Offset: 0x0027D57C
		public TrimSlice Cast_TrimSlice(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimSlice)
			{
				return TrimSlice.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimSlice is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B861 RID: 47201 RVA: 0x0027F3D1 File Offset: 0x0027D5D1
		public bool Is_TrimFullSlice(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimFullSlice;
		}

		// Token: 0x0600B862 RID: 47202 RVA: 0x0027F3EB File Offset: 0x0027D5EB
		public bool Is_TrimFullSlice(GrammarBuilders g, out TrimFullSlice value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimFullSlice)
			{
				value = TrimFullSlice.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimFullSlice);
			return false;
		}

		// Token: 0x0600B863 RID: 47203 RVA: 0x0027F420 File Offset: 0x0027D620
		public TrimFullSlice? As_TrimFullSlice(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimFullSlice)
			{
				return null;
			}
			return new TrimFullSlice?(TrimFullSlice.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B864 RID: 47204 RVA: 0x0027F460 File Offset: 0x0027D660
		public TrimFullSlice Cast_TrimFullSlice(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimFullSlice)
			{
				return TrimFullSlice.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimFullSlice is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B865 RID: 47205 RVA: 0x0027F4B8 File Offset: 0x0027D6B8
		public T Switch<T>(GrammarBuilders g, Func<sliceTrim_slice, T> func0, Func<TrimSlice, T> func1, Func<TrimFullSlice, T> func2)
		{
			sliceTrim_slice sliceTrim_slice;
			if (this.Is_sliceTrim_slice(g, out sliceTrim_slice))
			{
				return func0(sliceTrim_slice);
			}
			TrimSlice trimSlice;
			if (this.Is_TrimSlice(g, out trimSlice))
			{
				return func1(trimSlice);
			}
			TrimFullSlice trimFullSlice;
			if (this.Is_TrimFullSlice(g, out trimFullSlice))
			{
				return func2(trimFullSlice);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol sliceTrim");
		}

		// Token: 0x0600B866 RID: 47206 RVA: 0x0027F524 File Offset: 0x0027D724
		public void Switch(GrammarBuilders g, Action<sliceTrim_slice> func0, Action<TrimSlice> func1, Action<TrimFullSlice> func2)
		{
			sliceTrim_slice sliceTrim_slice;
			if (this.Is_sliceTrim_slice(g, out sliceTrim_slice))
			{
				func0(sliceTrim_slice);
				return;
			}
			TrimSlice trimSlice;
			if (this.Is_TrimSlice(g, out trimSlice))
			{
				func1(trimSlice);
				return;
			}
			TrimFullSlice trimFullSlice;
			if (this.Is_TrimFullSlice(g, out trimFullSlice))
			{
				func2(trimFullSlice);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol sliceTrim");
		}

		// Token: 0x0600B867 RID: 47207 RVA: 0x0027F58F File Offset: 0x0027D78F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B868 RID: 47208 RVA: 0x0027F5A4 File Offset: 0x0027D7A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B869 RID: 47209 RVA: 0x0027F5CE File Offset: 0x0027D7CE
		public bool Equals(sliceTrim other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400466D RID: 18029
		private ProgramNode _node;
	}
}
