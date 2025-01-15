using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015B5 RID: 5557
	public struct rowNumberTransform : IProgramNodeBuilder, IEquatable<rowNumberTransform>
	{
		// Token: 0x17001FDB RID: 8155
		// (get) Token: 0x0600B79C RID: 47004 RVA: 0x0027D316 File Offset: 0x0027B516
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B79D RID: 47005 RVA: 0x0027D31E File Offset: 0x0027B51E
		private rowNumberTransform(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B79E RID: 47006 RVA: 0x0027D327 File Offset: 0x0027B527
		public static rowNumberTransform CreateUnsafe(ProgramNode node)
		{
			return new rowNumberTransform(node);
		}

		// Token: 0x0600B79F RID: 47007 RVA: 0x0027D330 File Offset: 0x0027B530
		public static rowNumberTransform? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.rowNumberTransform)
			{
				return null;
			}
			return new rowNumberTransform?(rowNumberTransform.CreateUnsafe(node));
		}

		// Token: 0x0600B7A0 RID: 47008 RVA: 0x0027D36A File Offset: 0x0027B56A
		public static rowNumberTransform CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new rowNumberTransform(new Hole(g.Symbol.rowNumberTransform, holeId));
		}

		// Token: 0x0600B7A1 RID: 47009 RVA: 0x0027D382 File Offset: 0x0027B582
		public bool Is_rowNumberTransform_fromRowNumber(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.rowNumberTransform_fromRowNumber;
		}

		// Token: 0x0600B7A2 RID: 47010 RVA: 0x0027D39C File Offset: 0x0027B59C
		public bool Is_rowNumberTransform_fromRowNumber(GrammarBuilders g, out rowNumberTransform_fromRowNumber value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.rowNumberTransform_fromRowNumber)
			{
				value = rowNumberTransform_fromRowNumber.CreateUnsafe(this.Node);
				return true;
			}
			value = default(rowNumberTransform_fromRowNumber);
			return false;
		}

		// Token: 0x0600B7A3 RID: 47011 RVA: 0x0027D3D4 File Offset: 0x0027B5D4
		public rowNumberTransform_fromRowNumber? As_rowNumberTransform_fromRowNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.rowNumberTransform_fromRowNumber)
			{
				return null;
			}
			return new rowNumberTransform_fromRowNumber?(rowNumberTransform_fromRowNumber.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B7A4 RID: 47012 RVA: 0x0027D414 File Offset: 0x0027B614
		public rowNumberTransform_fromRowNumber Cast_rowNumberTransform_fromRowNumber(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.rowNumberTransform_fromRowNumber)
			{
				return rowNumberTransform_fromRowNumber.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_rowNumberTransform_fromRowNumber is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B7A5 RID: 47013 RVA: 0x0027D469 File Offset: 0x0027B669
		public bool Is_RowNumberLinearTransform(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RowNumberLinearTransform;
		}

		// Token: 0x0600B7A6 RID: 47014 RVA: 0x0027D483 File Offset: 0x0027B683
		public bool Is_RowNumberLinearTransform(GrammarBuilders g, out RowNumberLinearTransform value)
		{
			if (this.Node.GrammarRule == g.Rule.RowNumberLinearTransform)
			{
				value = RowNumberLinearTransform.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RowNumberLinearTransform);
			return false;
		}

		// Token: 0x0600B7A7 RID: 47015 RVA: 0x0027D4B8 File Offset: 0x0027B6B8
		public RowNumberLinearTransform? As_RowNumberLinearTransform(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RowNumberLinearTransform)
			{
				return null;
			}
			return new RowNumberLinearTransform?(RowNumberLinearTransform.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B7A8 RID: 47016 RVA: 0x0027D4F8 File Offset: 0x0027B6F8
		public RowNumberLinearTransform Cast_RowNumberLinearTransform(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RowNumberLinearTransform)
			{
				return RowNumberLinearTransform.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RowNumberLinearTransform is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B7A9 RID: 47017 RVA: 0x0027D550 File Offset: 0x0027B750
		public T Switch<T>(GrammarBuilders g, Func<rowNumberTransform_fromRowNumber, T> func0, Func<RowNumberLinearTransform, T> func1)
		{
			rowNumberTransform_fromRowNumber rowNumberTransform_fromRowNumber;
			if (this.Is_rowNumberTransform_fromRowNumber(g, out rowNumberTransform_fromRowNumber))
			{
				return func0(rowNumberTransform_fromRowNumber);
			}
			RowNumberLinearTransform rowNumberLinearTransform;
			if (this.Is_RowNumberLinearTransform(g, out rowNumberLinearTransform))
			{
				return func1(rowNumberLinearTransform);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol rowNumberTransform");
		}

		// Token: 0x0600B7AA RID: 47018 RVA: 0x0027D5A8 File Offset: 0x0027B7A8
		public void Switch(GrammarBuilders g, Action<rowNumberTransform_fromRowNumber> func0, Action<RowNumberLinearTransform> func1)
		{
			rowNumberTransform_fromRowNumber rowNumberTransform_fromRowNumber;
			if (this.Is_rowNumberTransform_fromRowNumber(g, out rowNumberTransform_fromRowNumber))
			{
				func0(rowNumberTransform_fromRowNumber);
				return;
			}
			RowNumberLinearTransform rowNumberLinearTransform;
			if (this.Is_RowNumberLinearTransform(g, out rowNumberLinearTransform))
			{
				func1(rowNumberLinearTransform);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol rowNumberTransform");
		}

		// Token: 0x0600B7AB RID: 47019 RVA: 0x0027D5FF File Offset: 0x0027B7FF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B7AC RID: 47020 RVA: 0x0027D614 File Offset: 0x0027B814
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B7AD RID: 47021 RVA: 0x0027D63E File Offset: 0x0027B83E
		public bool Equals(rowNumberTransform other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004663 RID: 18019
		private ProgramNode _node;
	}
}
