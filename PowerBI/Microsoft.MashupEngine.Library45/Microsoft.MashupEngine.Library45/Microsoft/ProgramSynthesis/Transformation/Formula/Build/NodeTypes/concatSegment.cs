using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015A6 RID: 5542
	public struct concatSegment : IProgramNodeBuilder, IEquatable<concatSegment>
	{
		// Token: 0x17001FCC RID: 8140
		// (get) Token: 0x0600B648 RID: 46664 RVA: 0x0027924E File Offset: 0x0027744E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B649 RID: 46665 RVA: 0x00279256 File Offset: 0x00277456
		private concatSegment(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B64A RID: 46666 RVA: 0x0027925F File Offset: 0x0027745F
		public static concatSegment CreateUnsafe(ProgramNode node)
		{
			return new concatSegment(node);
		}

		// Token: 0x0600B64B RID: 46667 RVA: 0x00279268 File Offset: 0x00277468
		public static concatSegment? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.concatSegment)
			{
				return null;
			}
			return new concatSegment?(concatSegment.CreateUnsafe(node));
		}

		// Token: 0x0600B64C RID: 46668 RVA: 0x002792A2 File Offset: 0x002774A2
		public static concatSegment CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new concatSegment(new Hole(g.Symbol.concatSegment, holeId));
		}

		// Token: 0x0600B64D RID: 46669 RVA: 0x002792BA File Offset: 0x002774BA
		public bool Is_concatSegment_segment(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.concatSegment_segment;
		}

		// Token: 0x0600B64E RID: 46670 RVA: 0x002792D4 File Offset: 0x002774D4
		public bool Is_concatSegment_segment(GrammarBuilders g, out concatSegment_segment value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatSegment_segment)
			{
				value = concatSegment_segment.CreateUnsafe(this.Node);
				return true;
			}
			value = default(concatSegment_segment);
			return false;
		}

		// Token: 0x0600B64F RID: 46671 RVA: 0x0027930C File Offset: 0x0027750C
		public concatSegment_segment? As_concatSegment_segment(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.concatSegment_segment)
			{
				return null;
			}
			return new concatSegment_segment?(concatSegment_segment.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B650 RID: 46672 RVA: 0x0027934C File Offset: 0x0027754C
		public concatSegment_segment Cast_concatSegment_segment(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatSegment_segment)
			{
				return concatSegment_segment.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_concatSegment_segment is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B651 RID: 46673 RVA: 0x002793A1 File Offset: 0x002775A1
		public bool Is_concatSegment_segmentCase(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.concatSegment_segmentCase;
		}

		// Token: 0x0600B652 RID: 46674 RVA: 0x002793BB File Offset: 0x002775BB
		public bool Is_concatSegment_segmentCase(GrammarBuilders g, out concatSegment_segmentCase value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatSegment_segmentCase)
			{
				value = concatSegment_segmentCase.CreateUnsafe(this.Node);
				return true;
			}
			value = default(concatSegment_segmentCase);
			return false;
		}

		// Token: 0x0600B653 RID: 46675 RVA: 0x002793F0 File Offset: 0x002775F0
		public concatSegment_segmentCase? As_concatSegment_segmentCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.concatSegment_segmentCase)
			{
				return null;
			}
			return new concatSegment_segmentCase?(concatSegment_segmentCase.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B654 RID: 46676 RVA: 0x00279430 File Offset: 0x00277630
		public concatSegment_segmentCase Cast_concatSegment_segmentCase(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.concatSegment_segmentCase)
			{
				return concatSegment_segmentCase.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_concatSegment_segmentCase is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B655 RID: 46677 RVA: 0x00279488 File Offset: 0x00277688
		public T Switch<T>(GrammarBuilders g, Func<concatSegment_segment, T> func0, Func<concatSegment_segmentCase, T> func1)
		{
			concatSegment_segment concatSegment_segment;
			if (this.Is_concatSegment_segment(g, out concatSegment_segment))
			{
				return func0(concatSegment_segment);
			}
			concatSegment_segmentCase concatSegment_segmentCase;
			if (this.Is_concatSegment_segmentCase(g, out concatSegment_segmentCase))
			{
				return func1(concatSegment_segmentCase);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol concatSegment");
		}

		// Token: 0x0600B656 RID: 46678 RVA: 0x002794E0 File Offset: 0x002776E0
		public void Switch(GrammarBuilders g, Action<concatSegment_segment> func0, Action<concatSegment_segmentCase> func1)
		{
			concatSegment_segment concatSegment_segment;
			if (this.Is_concatSegment_segment(g, out concatSegment_segment))
			{
				func0(concatSegment_segment);
				return;
			}
			concatSegment_segmentCase concatSegment_segmentCase;
			if (this.Is_concatSegment_segmentCase(g, out concatSegment_segmentCase))
			{
				func1(concatSegment_segmentCase);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol concatSegment");
		}

		// Token: 0x0600B657 RID: 46679 RVA: 0x00279537 File Offset: 0x00277737
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B658 RID: 46680 RVA: 0x0027954C File Offset: 0x0027774C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B659 RID: 46681 RVA: 0x00279576 File Offset: 0x00277776
		public bool Equals(concatSegment other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004654 RID: 18004
		private ProgramNode _node;
	}
}
