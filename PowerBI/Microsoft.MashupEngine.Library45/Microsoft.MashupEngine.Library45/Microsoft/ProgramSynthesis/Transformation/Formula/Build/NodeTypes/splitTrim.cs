using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015BD RID: 5565
	public struct splitTrim : IProgramNodeBuilder, IEquatable<splitTrim>
	{
		// Token: 0x17001FE3 RID: 8163
		// (get) Token: 0x0600B832 RID: 47154 RVA: 0x0027EC62 File Offset: 0x0027CE62
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B833 RID: 47155 RVA: 0x0027EC6A File Offset: 0x0027CE6A
		private splitTrim(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B834 RID: 47156 RVA: 0x0027EC73 File Offset: 0x0027CE73
		public static splitTrim CreateUnsafe(ProgramNode node)
		{
			return new splitTrim(node);
		}

		// Token: 0x0600B835 RID: 47157 RVA: 0x0027EC7C File Offset: 0x0027CE7C
		public static splitTrim? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.splitTrim)
			{
				return null;
			}
			return new splitTrim?(splitTrim.CreateUnsafe(node));
		}

		// Token: 0x0600B836 RID: 47158 RVA: 0x0027ECB6 File Offset: 0x0027CEB6
		public static splitTrim CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new splitTrim(new Hole(g.Symbol.splitTrim, holeId));
		}

		// Token: 0x0600B837 RID: 47159 RVA: 0x0027ECCE File Offset: 0x0027CECE
		public bool Is_splitTrim_split(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.splitTrim_split;
		}

		// Token: 0x0600B838 RID: 47160 RVA: 0x0027ECE8 File Offset: 0x0027CEE8
		public bool Is_splitTrim_split(GrammarBuilders g, out splitTrim_split value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.splitTrim_split)
			{
				value = splitTrim_split.CreateUnsafe(this.Node);
				return true;
			}
			value = default(splitTrim_split);
			return false;
		}

		// Token: 0x0600B839 RID: 47161 RVA: 0x0027ED20 File Offset: 0x0027CF20
		public splitTrim_split? As_splitTrim_split(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.splitTrim_split)
			{
				return null;
			}
			return new splitTrim_split?(splitTrim_split.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B83A RID: 47162 RVA: 0x0027ED60 File Offset: 0x0027CF60
		public splitTrim_split Cast_splitTrim_split(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.splitTrim_split)
			{
				return splitTrim_split.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_splitTrim_split is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B83B RID: 47163 RVA: 0x0027EDB5 File Offset: 0x0027CFB5
		public bool Is_TrimSplit(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimSplit;
		}

		// Token: 0x0600B83C RID: 47164 RVA: 0x0027EDCF File Offset: 0x0027CFCF
		public bool Is_TrimSplit(GrammarBuilders g, out TrimSplit value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimSplit)
			{
				value = TrimSplit.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimSplit);
			return false;
		}

		// Token: 0x0600B83D RID: 47165 RVA: 0x0027EE04 File Offset: 0x0027D004
		public TrimSplit? As_TrimSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimSplit)
			{
				return null;
			}
			return new TrimSplit?(TrimSplit.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B83E RID: 47166 RVA: 0x0027EE44 File Offset: 0x0027D044
		public TrimSplit Cast_TrimSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimSplit)
			{
				return TrimSplit.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimSplit is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B83F RID: 47167 RVA: 0x0027EE99 File Offset: 0x0027D099
		public bool Is_TrimFullSplit(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimFullSplit;
		}

		// Token: 0x0600B840 RID: 47168 RVA: 0x0027EEB3 File Offset: 0x0027D0B3
		public bool Is_TrimFullSplit(GrammarBuilders g, out TrimFullSplit value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimFullSplit)
			{
				value = TrimFullSplit.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimFullSplit);
			return false;
		}

		// Token: 0x0600B841 RID: 47169 RVA: 0x0027EEE8 File Offset: 0x0027D0E8
		public TrimFullSplit? As_TrimFullSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimFullSplit)
			{
				return null;
			}
			return new TrimFullSplit?(TrimFullSplit.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B842 RID: 47170 RVA: 0x0027EF28 File Offset: 0x0027D128
		public TrimFullSplit Cast_TrimFullSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimFullSplit)
			{
				return TrimFullSplit.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimFullSplit is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B843 RID: 47171 RVA: 0x0027EF80 File Offset: 0x0027D180
		public T Switch<T>(GrammarBuilders g, Func<splitTrim_split, T> func0, Func<TrimSplit, T> func1, Func<TrimFullSplit, T> func2)
		{
			splitTrim_split splitTrim_split;
			if (this.Is_splitTrim_split(g, out splitTrim_split))
			{
				return func0(splitTrim_split);
			}
			TrimSplit trimSplit;
			if (this.Is_TrimSplit(g, out trimSplit))
			{
				return func1(trimSplit);
			}
			TrimFullSplit trimFullSplit;
			if (this.Is_TrimFullSplit(g, out trimFullSplit))
			{
				return func2(trimFullSplit);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol splitTrim");
		}

		// Token: 0x0600B844 RID: 47172 RVA: 0x0027EFEC File Offset: 0x0027D1EC
		public void Switch(GrammarBuilders g, Action<splitTrim_split> func0, Action<TrimSplit> func1, Action<TrimFullSplit> func2)
		{
			splitTrim_split splitTrim_split;
			if (this.Is_splitTrim_split(g, out splitTrim_split))
			{
				func0(splitTrim_split);
				return;
			}
			TrimSplit trimSplit;
			if (this.Is_TrimSplit(g, out trimSplit))
			{
				func1(trimSplit);
				return;
			}
			TrimFullSplit trimFullSplit;
			if (this.Is_TrimFullSplit(g, out trimFullSplit))
			{
				func2(trimFullSplit);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol splitTrim");
		}

		// Token: 0x0600B845 RID: 47173 RVA: 0x0027F057 File Offset: 0x0027D257
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B846 RID: 47174 RVA: 0x0027F06C File Offset: 0x0027D26C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B847 RID: 47175 RVA: 0x0027F096 File Offset: 0x0027D296
		public bool Equals(splitTrim other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400466B RID: 18027
		private ProgramNode _node;
	}
}
