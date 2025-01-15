using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E5E RID: 3678
	public struct horizontalSheetSplits : IProgramNodeBuilder, IEquatable<horizontalSheetSplits>
	{
		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x0600637F RID: 25471 RVA: 0x001438CA File Offset: 0x00141ACA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006380 RID: 25472 RVA: 0x001438D2 File Offset: 0x00141AD2
		private horizontalSheetSplits(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006381 RID: 25473 RVA: 0x001438DB File Offset: 0x00141ADB
		public static horizontalSheetSplits CreateUnsafe(ProgramNode node)
		{
			return new horizontalSheetSplits(node);
		}

		// Token: 0x06006382 RID: 25474 RVA: 0x001438E4 File Offset: 0x00141AE4
		public static horizontalSheetSplits? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.horizontalSheetSplits)
			{
				return null;
			}
			return new horizontalSheetSplits?(horizontalSheetSplits.CreateUnsafe(node));
		}

		// Token: 0x06006383 RID: 25475 RVA: 0x0014391E File Offset: 0x00141B1E
		public static horizontalSheetSplits CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new horizontalSheetSplits(new Hole(g.Symbol.horizontalSheetSplits, holeId));
		}

		// Token: 0x06006384 RID: 25476 RVA: 0x00143936 File Offset: 0x00141B36
		public bool Is_SplitOnEmptyRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SplitOnEmptyRows;
		}

		// Token: 0x06006385 RID: 25477 RVA: 0x00143950 File Offset: 0x00141B50
		public bool Is_SplitOnEmptyRows(GrammarBuilders g, out SplitOnEmptyRows value)
		{
			if (this.Node.GrammarRule == g.Rule.SplitOnEmptyRows)
			{
				value = SplitOnEmptyRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SplitOnEmptyRows);
			return false;
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x00143988 File Offset: 0x00141B88
		public SplitOnEmptyRows? As_SplitOnEmptyRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SplitOnEmptyRows)
			{
				return null;
			}
			return new SplitOnEmptyRows?(SplitOnEmptyRows.CreateUnsafe(this.Node));
		}

		// Token: 0x06006387 RID: 25479 RVA: 0x001439C8 File Offset: 0x00141BC8
		public SplitOnEmptyRows Cast_SplitOnEmptyRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SplitOnEmptyRows)
			{
				return SplitOnEmptyRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SplitOnEmptyRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006388 RID: 25480 RVA: 0x00143A1D File Offset: 0x00141C1D
		public bool Is_SplitOnMatchingRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SplitOnMatchingRows;
		}

		// Token: 0x06006389 RID: 25481 RVA: 0x00143A37 File Offset: 0x00141C37
		public bool Is_SplitOnMatchingRows(GrammarBuilders g, out SplitOnMatchingRows value)
		{
			if (this.Node.GrammarRule == g.Rule.SplitOnMatchingRows)
			{
				value = SplitOnMatchingRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SplitOnMatchingRows);
			return false;
		}

		// Token: 0x0600638A RID: 25482 RVA: 0x00143A6C File Offset: 0x00141C6C
		public SplitOnMatchingRows? As_SplitOnMatchingRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SplitOnMatchingRows)
			{
				return null;
			}
			return new SplitOnMatchingRows?(SplitOnMatchingRows.CreateUnsafe(this.Node));
		}

		// Token: 0x0600638B RID: 25483 RVA: 0x00143AAC File Offset: 0x00141CAC
		public SplitOnMatchingRows Cast_SplitOnMatchingRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SplitOnMatchingRows)
			{
				return SplitOnMatchingRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SplitOnMatchingRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600638C RID: 25484 RVA: 0x00143B04 File Offset: 0x00141D04
		public T Switch<T>(GrammarBuilders g, Func<SplitOnEmptyRows, T> func0, Func<SplitOnMatchingRows, T> func1)
		{
			SplitOnEmptyRows splitOnEmptyRows;
			if (this.Is_SplitOnEmptyRows(g, out splitOnEmptyRows))
			{
				return func0(splitOnEmptyRows);
			}
			SplitOnMatchingRows splitOnMatchingRows;
			if (this.Is_SplitOnMatchingRows(g, out splitOnMatchingRows))
			{
				return func1(splitOnMatchingRows);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol horizontalSheetSplits");
		}

		// Token: 0x0600638D RID: 25485 RVA: 0x00143B5C File Offset: 0x00141D5C
		public void Switch(GrammarBuilders g, Action<SplitOnEmptyRows> func0, Action<SplitOnMatchingRows> func1)
		{
			SplitOnEmptyRows splitOnEmptyRows;
			if (this.Is_SplitOnEmptyRows(g, out splitOnEmptyRows))
			{
				func0(splitOnEmptyRows);
				return;
			}
			SplitOnMatchingRows splitOnMatchingRows;
			if (this.Is_SplitOnMatchingRows(g, out splitOnMatchingRows))
			{
				func1(splitOnMatchingRows);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol horizontalSheetSplits");
		}

		// Token: 0x0600638E RID: 25486 RVA: 0x00143BB3 File Offset: 0x00141DB3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600638F RID: 25487 RVA: 0x00143BC8 File Offset: 0x00141DC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006390 RID: 25488 RVA: 0x00143BF2 File Offset: 0x00141DF2
		public bool Equals(horizontalSheetSplits other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C08 RID: 11272
		private ProgramNode _node;
	}
}
