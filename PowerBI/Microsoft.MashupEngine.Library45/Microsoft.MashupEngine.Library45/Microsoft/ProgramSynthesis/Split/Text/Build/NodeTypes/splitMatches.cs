using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x02001360 RID: 4960
	public struct splitMatches : IProgramNodeBuilder, IEquatable<splitMatches>
	{
		// Token: 0x17001A69 RID: 6761
		// (get) Token: 0x06009935 RID: 39221 RVA: 0x00207F9E File Offset: 0x0020619E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009936 RID: 39222 RVA: 0x00207FA6 File Offset: 0x002061A6
		private splitMatches(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009937 RID: 39223 RVA: 0x00207FAF File Offset: 0x002061AF
		public static splitMatches CreateUnsafe(ProgramNode node)
		{
			return new splitMatches(node);
		}

		// Token: 0x06009938 RID: 39224 RVA: 0x00207FB8 File Offset: 0x002061B8
		public static splitMatches? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.splitMatches)
			{
				return null;
			}
			return new splitMatches?(splitMatches.CreateUnsafe(node));
		}

		// Token: 0x06009939 RID: 39225 RVA: 0x00207FF2 File Offset: 0x002061F2
		public static splitMatches CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new splitMatches(new Hole(g.Symbol.splitMatches, holeId));
		}

		// Token: 0x0600993A RID: 39226 RVA: 0x0020800A File Offset: 0x0020620A
		public bool Is_splitMatches_multipleMatches(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.splitMatches_multipleMatches;
		}

		// Token: 0x0600993B RID: 39227 RVA: 0x00208024 File Offset: 0x00206224
		public bool Is_splitMatches_multipleMatches(GrammarBuilders g, out splitMatches_multipleMatches value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.splitMatches_multipleMatches)
			{
				value = splitMatches_multipleMatches.CreateUnsafe(this.Node);
				return true;
			}
			value = default(splitMatches_multipleMatches);
			return false;
		}

		// Token: 0x0600993C RID: 39228 RVA: 0x0020805C File Offset: 0x0020625C
		public splitMatches_multipleMatches? As_splitMatches_multipleMatches(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.splitMatches_multipleMatches)
			{
				return null;
			}
			return new splitMatches_multipleMatches?(splitMatches_multipleMatches.CreateUnsafe(this.Node));
		}

		// Token: 0x0600993D RID: 39229 RVA: 0x0020809C File Offset: 0x0020629C
		public splitMatches_multipleMatches Cast_splitMatches_multipleMatches(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.splitMatches_multipleMatches)
			{
				return splitMatches_multipleMatches.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_splitMatches_multipleMatches is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600993E RID: 39230 RVA: 0x002080F1 File Offset: 0x002062F1
		public bool Is_splitMatches_constantDelimiterMatches(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.splitMatches_constantDelimiterMatches;
		}

		// Token: 0x0600993F RID: 39231 RVA: 0x0020810B File Offset: 0x0020630B
		public bool Is_splitMatches_constantDelimiterMatches(GrammarBuilders g, out splitMatches_constantDelimiterMatches value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.splitMatches_constantDelimiterMatches)
			{
				value = splitMatches_constantDelimiterMatches.CreateUnsafe(this.Node);
				return true;
			}
			value = default(splitMatches_constantDelimiterMatches);
			return false;
		}

		// Token: 0x06009940 RID: 39232 RVA: 0x00208140 File Offset: 0x00206340
		public splitMatches_constantDelimiterMatches? As_splitMatches_constantDelimiterMatches(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.splitMatches_constantDelimiterMatches)
			{
				return null;
			}
			return new splitMatches_constantDelimiterMatches?(splitMatches_constantDelimiterMatches.CreateUnsafe(this.Node));
		}

		// Token: 0x06009941 RID: 39233 RVA: 0x00208180 File Offset: 0x00206380
		public splitMatches_constantDelimiterMatches Cast_splitMatches_constantDelimiterMatches(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.splitMatches_constantDelimiterMatches)
			{
				return splitMatches_constantDelimiterMatches.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_splitMatches_constantDelimiterMatches is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06009942 RID: 39234 RVA: 0x002081D5 File Offset: 0x002063D5
		public bool Is_splitMatches_fixedWidthMatches(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.splitMatches_fixedWidthMatches;
		}

		// Token: 0x06009943 RID: 39235 RVA: 0x002081EF File Offset: 0x002063EF
		public bool Is_splitMatches_fixedWidthMatches(GrammarBuilders g, out splitMatches_fixedWidthMatches value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.splitMatches_fixedWidthMatches)
			{
				value = splitMatches_fixedWidthMatches.CreateUnsafe(this.Node);
				return true;
			}
			value = default(splitMatches_fixedWidthMatches);
			return false;
		}

		// Token: 0x06009944 RID: 39236 RVA: 0x00208224 File Offset: 0x00206424
		public splitMatches_fixedWidthMatches? As_splitMatches_fixedWidthMatches(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.splitMatches_fixedWidthMatches)
			{
				return null;
			}
			return new splitMatches_fixedWidthMatches?(splitMatches_fixedWidthMatches.CreateUnsafe(this.Node));
		}

		// Token: 0x06009945 RID: 39237 RVA: 0x00208264 File Offset: 0x00206464
		public splitMatches_fixedWidthMatches Cast_splitMatches_fixedWidthMatches(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.splitMatches_fixedWidthMatches)
			{
				return splitMatches_fixedWidthMatches.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_splitMatches_fixedWidthMatches is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06009946 RID: 39238 RVA: 0x002082BC File Offset: 0x002064BC
		public T Switch<T>(GrammarBuilders g, Func<splitMatches_multipleMatches, T> func0, Func<splitMatches_constantDelimiterMatches, T> func1, Func<splitMatches_fixedWidthMatches, T> func2)
		{
			splitMatches_multipleMatches splitMatches_multipleMatches;
			if (this.Is_splitMatches_multipleMatches(g, out splitMatches_multipleMatches))
			{
				return func0(splitMatches_multipleMatches);
			}
			splitMatches_constantDelimiterMatches splitMatches_constantDelimiterMatches;
			if (this.Is_splitMatches_constantDelimiterMatches(g, out splitMatches_constantDelimiterMatches))
			{
				return func1(splitMatches_constantDelimiterMatches);
			}
			splitMatches_fixedWidthMatches splitMatches_fixedWidthMatches;
			if (this.Is_splitMatches_fixedWidthMatches(g, out splitMatches_fixedWidthMatches))
			{
				return func2(splitMatches_fixedWidthMatches);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol splitMatches");
		}

		// Token: 0x06009947 RID: 39239 RVA: 0x00208328 File Offset: 0x00206528
		public void Switch(GrammarBuilders g, Action<splitMatches_multipleMatches> func0, Action<splitMatches_constantDelimiterMatches> func1, Action<splitMatches_fixedWidthMatches> func2)
		{
			splitMatches_multipleMatches splitMatches_multipleMatches;
			if (this.Is_splitMatches_multipleMatches(g, out splitMatches_multipleMatches))
			{
				func0(splitMatches_multipleMatches);
				return;
			}
			splitMatches_constantDelimiterMatches splitMatches_constantDelimiterMatches;
			if (this.Is_splitMatches_constantDelimiterMatches(g, out splitMatches_constantDelimiterMatches))
			{
				func1(splitMatches_constantDelimiterMatches);
				return;
			}
			splitMatches_fixedWidthMatches splitMatches_fixedWidthMatches;
			if (this.Is_splitMatches_fixedWidthMatches(g, out splitMatches_fixedWidthMatches))
			{
				func2(splitMatches_fixedWidthMatches);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol splitMatches");
		}

		// Token: 0x06009948 RID: 39240 RVA: 0x00208393 File Offset: 0x00206593
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009949 RID: 39241 RVA: 0x002083A8 File Offset: 0x002065A8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600994A RID: 39242 RVA: 0x002083D2 File Offset: 0x002065D2
		public bool Equals(splitMatches other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DD7 RID: 15831
		private ProgramNode _node;
	}
}
