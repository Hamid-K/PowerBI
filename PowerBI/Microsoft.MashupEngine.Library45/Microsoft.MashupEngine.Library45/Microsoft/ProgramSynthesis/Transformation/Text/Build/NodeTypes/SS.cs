using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C4B RID: 7243
	public struct SS : IProgramNodeBuilder, IEquatable<SS>
	{
		// Token: 0x170028E1 RID: 10465
		// (get) Token: 0x0600F4C6 RID: 62662 RVA: 0x003460AE File Offset: 0x003442AE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F4C7 RID: 62663 RVA: 0x003460B6 File Offset: 0x003442B6
		private SS(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F4C8 RID: 62664 RVA: 0x003460BF File Offset: 0x003442BF
		public static SS CreateUnsafe(ProgramNode node)
		{
			return new SS(node);
		}

		// Token: 0x0600F4C9 RID: 62665 RVA: 0x003460C8 File Offset: 0x003442C8
		public static SS? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.SS)
			{
				return null;
			}
			return new SS?(SS.CreateUnsafe(node));
		}

		// Token: 0x0600F4CA RID: 62666 RVA: 0x00346102 File Offset: 0x00344302
		public static SS CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new SS(new Hole(g.Symbol.SS, holeId));
		}

		// Token: 0x0600F4CB RID: 62667 RVA: 0x0034611A File Offset: 0x0034431A
		public bool Is_WholeColumn(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.WholeColumn;
		}

		// Token: 0x0600F4CC RID: 62668 RVA: 0x00346134 File Offset: 0x00344334
		public bool Is_WholeColumn(GrammarBuilders g, out WholeColumn value)
		{
			if (this.Node.GrammarRule == g.Rule.WholeColumn)
			{
				value = WholeColumn.CreateUnsafe(this.Node);
				return true;
			}
			value = default(WholeColumn);
			return false;
		}

		// Token: 0x0600F4CD RID: 62669 RVA: 0x0034616C File Offset: 0x0034436C
		public WholeColumn? As_WholeColumn(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.WholeColumn)
			{
				return null;
			}
			return new WholeColumn?(WholeColumn.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F4CE RID: 62670 RVA: 0x003461AC File Offset: 0x003443AC
		public WholeColumn Cast_WholeColumn(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.WholeColumn)
			{
				return WholeColumn.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_WholeColumn is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F4CF RID: 62671 RVA: 0x00346201 File Offset: 0x00344401
		public bool Is_SubStr(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SubStr;
		}

		// Token: 0x0600F4D0 RID: 62672 RVA: 0x0034621B File Offset: 0x0034441B
		public bool Is_SubStr(GrammarBuilders g, out SubStr value)
		{
			if (this.Node.GrammarRule == g.Rule.SubStr)
			{
				value = SubStr.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SubStr);
			return false;
		}

		// Token: 0x0600F4D1 RID: 62673 RVA: 0x00346250 File Offset: 0x00344450
		public SubStr? As_SubStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SubStr)
			{
				return null;
			}
			return new SubStr?(SubStr.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F4D2 RID: 62674 RVA: 0x00346290 File Offset: 0x00344490
		public SubStr Cast_SubStr(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SubStr)
			{
				return SubStr.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SubStr is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F4D3 RID: 62675 RVA: 0x003462E8 File Offset: 0x003444E8
		public T Switch<T>(GrammarBuilders g, Func<WholeColumn, T> func0, Func<SubStr, T> func1)
		{
			WholeColumn wholeColumn;
			if (this.Is_WholeColumn(g, out wholeColumn))
			{
				return func0(wholeColumn);
			}
			SubStr subStr;
			if (this.Is_SubStr(g, out subStr))
			{
				return func1(subStr);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol SS");
		}

		// Token: 0x0600F4D4 RID: 62676 RVA: 0x00346340 File Offset: 0x00344540
		public void Switch(GrammarBuilders g, Action<WholeColumn> func0, Action<SubStr> func1)
		{
			WholeColumn wholeColumn;
			if (this.Is_WholeColumn(g, out wholeColumn))
			{
				func0(wholeColumn);
				return;
			}
			SubStr subStr;
			if (this.Is_SubStr(g, out subStr))
			{
				func1(subStr);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol SS");
		}

		// Token: 0x0600F4D5 RID: 62677 RVA: 0x00346397 File Offset: 0x00344597
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F4D6 RID: 62678 RVA: 0x003463AC File Offset: 0x003445AC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F4D7 RID: 62679 RVA: 0x003463D6 File Offset: 0x003445D6
		public bool Equals(SS other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B3A RID: 23354
		private ProgramNode _node;
	}
}
