using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001067 RID: 4199
	public struct selectionEnd : IProgramNodeBuilder, IEquatable<selectionEnd>
	{
		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06007D07 RID: 32007 RVA: 0x001A6006 File Offset: 0x001A4206
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007D08 RID: 32008 RVA: 0x001A600E File Offset: 0x001A420E
		private selectionEnd(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007D09 RID: 32009 RVA: 0x001A6017 File Offset: 0x001A4217
		public static selectionEnd CreateUnsafe(ProgramNode node)
		{
			return new selectionEnd(node);
		}

		// Token: 0x06007D0A RID: 32010 RVA: 0x001A6020 File Offset: 0x001A4220
		public static selectionEnd? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selectionEnd)
			{
				return null;
			}
			return new selectionEnd?(selectionEnd.CreateUnsafe(node));
		}

		// Token: 0x06007D0B RID: 32011 RVA: 0x001A605A File Offset: 0x001A425A
		public static selectionEnd CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selectionEnd(new Hole(g.Symbol.selectionEnd, holeId));
		}

		// Token: 0x06007D0C RID: 32012 RVA: 0x001A6072 File Offset: 0x001A4272
		public bool Is_FilterNodesEnd(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FilterNodesEnd;
		}

		// Token: 0x06007D0D RID: 32013 RVA: 0x001A608C File Offset: 0x001A428C
		public bool Is_FilterNodesEnd(GrammarBuilders g, out FilterNodesEnd value)
		{
			if (this.Node.GrammarRule == g.Rule.FilterNodesEnd)
			{
				value = FilterNodesEnd.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FilterNodesEnd);
			return false;
		}

		// Token: 0x06007D0E RID: 32014 RVA: 0x001A60C4 File Offset: 0x001A42C4
		public FilterNodesEnd? As_FilterNodesEnd(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FilterNodesEnd)
			{
				return null;
			}
			return new FilterNodesEnd?(FilterNodesEnd.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D0F RID: 32015 RVA: 0x001A6104 File Offset: 0x001A4304
		public FilterNodesEnd Cast_FilterNodesEnd(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FilterNodesEnd)
			{
				return FilterNodesEnd.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FilterNodesEnd is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007D10 RID: 32016 RVA: 0x001A6159 File Offset: 0x001A4359
		public bool Is_TakeWhileNodesEnd(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TakeWhileNodesEnd;
		}

		// Token: 0x06007D11 RID: 32017 RVA: 0x001A6173 File Offset: 0x001A4373
		public bool Is_TakeWhileNodesEnd(GrammarBuilders g, out TakeWhileNodesEnd value)
		{
			if (this.Node.GrammarRule == g.Rule.TakeWhileNodesEnd)
			{
				value = TakeWhileNodesEnd.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TakeWhileNodesEnd);
			return false;
		}

		// Token: 0x06007D12 RID: 32018 RVA: 0x001A61A8 File Offset: 0x001A43A8
		public TakeWhileNodesEnd? As_TakeWhileNodesEnd(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TakeWhileNodesEnd)
			{
				return null;
			}
			return new TakeWhileNodesEnd?(TakeWhileNodesEnd.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D13 RID: 32019 RVA: 0x001A61E8 File Offset: 0x001A43E8
		public TakeWhileNodesEnd Cast_TakeWhileNodesEnd(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TakeWhileNodesEnd)
			{
				return TakeWhileNodesEnd.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TakeWhileNodesEnd is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007D14 RID: 32020 RVA: 0x001A623D File Offset: 0x001A443D
		public bool Is_selectionEnd_regionStartSiblings(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.selectionEnd_regionStartSiblings;
		}

		// Token: 0x06007D15 RID: 32021 RVA: 0x001A6257 File Offset: 0x001A4457
		public bool Is_selectionEnd_regionStartSiblings(GrammarBuilders g, out selectionEnd_regionStartSiblings value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selectionEnd_regionStartSiblings)
			{
				value = selectionEnd_regionStartSiblings.CreateUnsafe(this.Node);
				return true;
			}
			value = default(selectionEnd_regionStartSiblings);
			return false;
		}

		// Token: 0x06007D16 RID: 32022 RVA: 0x001A628C File Offset: 0x001A448C
		public selectionEnd_regionStartSiblings? As_selectionEnd_regionStartSiblings(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.selectionEnd_regionStartSiblings)
			{
				return null;
			}
			return new selectionEnd_regionStartSiblings?(selectionEnd_regionStartSiblings.CreateUnsafe(this.Node));
		}

		// Token: 0x06007D17 RID: 32023 RVA: 0x001A62CC File Offset: 0x001A44CC
		public selectionEnd_regionStartSiblings Cast_selectionEnd_regionStartSiblings(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selectionEnd_regionStartSiblings)
			{
				return selectionEnd_regionStartSiblings.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_selectionEnd_regionStartSiblings is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007D18 RID: 32024 RVA: 0x001A6324 File Offset: 0x001A4524
		public T Switch<T>(GrammarBuilders g, Func<FilterNodesEnd, T> func0, Func<TakeWhileNodesEnd, T> func1, Func<selectionEnd_regionStartSiblings, T> func2)
		{
			FilterNodesEnd filterNodesEnd;
			if (this.Is_FilterNodesEnd(g, out filterNodesEnd))
			{
				return func0(filterNodesEnd);
			}
			TakeWhileNodesEnd takeWhileNodesEnd;
			if (this.Is_TakeWhileNodesEnd(g, out takeWhileNodesEnd))
			{
				return func1(takeWhileNodesEnd);
			}
			selectionEnd_regionStartSiblings selectionEnd_regionStartSiblings;
			if (this.Is_selectionEnd_regionStartSiblings(g, out selectionEnd_regionStartSiblings))
			{
				return func2(selectionEnd_regionStartSiblings);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selectionEnd");
		}

		// Token: 0x06007D19 RID: 32025 RVA: 0x001A6390 File Offset: 0x001A4590
		public void Switch(GrammarBuilders g, Action<FilterNodesEnd> func0, Action<TakeWhileNodesEnd> func1, Action<selectionEnd_regionStartSiblings> func2)
		{
			FilterNodesEnd filterNodesEnd;
			if (this.Is_FilterNodesEnd(g, out filterNodesEnd))
			{
				func0(filterNodesEnd);
				return;
			}
			TakeWhileNodesEnd takeWhileNodesEnd;
			if (this.Is_TakeWhileNodesEnd(g, out takeWhileNodesEnd))
			{
				func1(takeWhileNodesEnd);
				return;
			}
			selectionEnd_regionStartSiblings selectionEnd_regionStartSiblings;
			if (this.Is_selectionEnd_regionStartSiblings(g, out selectionEnd_regionStartSiblings))
			{
				func2(selectionEnd_regionStartSiblings);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selectionEnd");
		}

		// Token: 0x06007D1A RID: 32026 RVA: 0x001A63FB File Offset: 0x001A45FB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007D1B RID: 32027 RVA: 0x001A6410 File Offset: 0x001A4610
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007D1C RID: 32028 RVA: 0x001A643A File Offset: 0x001A463A
		public bool Equals(selectionEnd other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003380 RID: 13184
		private ProgramNode _node;
	}
}
