using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E54 RID: 3668
	public struct trimLeft : IProgramNodeBuilder, IEquatable<trimLeft>
	{
		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x060062AB RID: 25259 RVA: 0x0014117E File Offset: 0x0013F37E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060062AC RID: 25260 RVA: 0x00141186 File Offset: 0x0013F386
		private trimLeft(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060062AD RID: 25261 RVA: 0x0014118F File Offset: 0x0013F38F
		public static trimLeft CreateUnsafe(ProgramNode node)
		{
			return new trimLeft(node);
		}

		// Token: 0x060062AE RID: 25262 RVA: 0x00141198 File Offset: 0x0013F398
		public static trimLeft? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.trimLeft)
			{
				return null;
			}
			return new trimLeft?(trimLeft.CreateUnsafe(node));
		}

		// Token: 0x060062AF RID: 25263 RVA: 0x001411D2 File Offset: 0x0013F3D2
		public static trimLeft CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new trimLeft(new Hole(g.Symbol.trimLeft, holeId));
		}

		// Token: 0x060062B0 RID: 25264 RVA: 0x001411EA File Offset: 0x0013F3EA
		public bool Is_trimLeft_trimBottom(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.trimLeft_trimBottom;
		}

		// Token: 0x060062B1 RID: 25265 RVA: 0x00141204 File Offset: 0x0013F404
		public bool Is_trimLeft_trimBottom(GrammarBuilders g, out trimLeft_trimBottom value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.trimLeft_trimBottom)
			{
				value = trimLeft_trimBottom.CreateUnsafe(this.Node);
				return true;
			}
			value = default(trimLeft_trimBottom);
			return false;
		}

		// Token: 0x060062B2 RID: 25266 RVA: 0x0014123C File Offset: 0x0013F43C
		public trimLeft_trimBottom? As_trimLeft_trimBottom(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.trimLeft_trimBottom)
			{
				return null;
			}
			return new trimLeft_trimBottom?(trimLeft_trimBottom.CreateUnsafe(this.Node));
		}

		// Token: 0x060062B3 RID: 25267 RVA: 0x0014127C File Offset: 0x0013F47C
		public trimLeft_trimBottom Cast_trimLeft_trimBottom(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.trimLeft_trimBottom)
			{
				return trimLeft_trimBottom.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_trimLeft_trimBottom is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062B4 RID: 25268 RVA: 0x001412D1 File Offset: 0x0013F4D1
		public bool Is_TrimLeftSingleCellColumns(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimLeftSingleCellColumns;
		}

		// Token: 0x060062B5 RID: 25269 RVA: 0x001412EB File Offset: 0x0013F4EB
		public bool Is_TrimLeftSingleCellColumns(GrammarBuilders g, out TrimLeftSingleCellColumns value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimLeftSingleCellColumns)
			{
				value = TrimLeftSingleCellColumns.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimLeftSingleCellColumns);
			return false;
		}

		// Token: 0x060062B6 RID: 25270 RVA: 0x00141320 File Offset: 0x0013F520
		public TrimLeftSingleCellColumns? As_TrimLeftSingleCellColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimLeftSingleCellColumns)
			{
				return null;
			}
			return new TrimLeftSingleCellColumns?(TrimLeftSingleCellColumns.CreateUnsafe(this.Node));
		}

		// Token: 0x060062B7 RID: 25271 RVA: 0x00141360 File Offset: 0x0013F560
		public TrimLeftSingleCellColumns Cast_TrimLeftSingleCellColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimLeftSingleCellColumns)
			{
				return TrimLeftSingleCellColumns.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimLeftSingleCellColumns is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062B8 RID: 25272 RVA: 0x001413B8 File Offset: 0x0013F5B8
		public T Switch<T>(GrammarBuilders g, Func<trimLeft_trimBottom, T> func0, Func<TrimLeftSingleCellColumns, T> func1)
		{
			trimLeft_trimBottom trimLeft_trimBottom;
			if (this.Is_trimLeft_trimBottom(g, out trimLeft_trimBottom))
			{
				return func0(trimLeft_trimBottom);
			}
			TrimLeftSingleCellColumns trimLeftSingleCellColumns;
			if (this.Is_TrimLeftSingleCellColumns(g, out trimLeftSingleCellColumns))
			{
				return func1(trimLeftSingleCellColumns);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol trimLeft");
		}

		// Token: 0x060062B9 RID: 25273 RVA: 0x00141410 File Offset: 0x0013F610
		public void Switch(GrammarBuilders g, Action<trimLeft_trimBottom> func0, Action<TrimLeftSingleCellColumns> func1)
		{
			trimLeft_trimBottom trimLeft_trimBottom;
			if (this.Is_trimLeft_trimBottom(g, out trimLeft_trimBottom))
			{
				func0(trimLeft_trimBottom);
				return;
			}
			TrimLeftSingleCellColumns trimLeftSingleCellColumns;
			if (this.Is_TrimLeftSingleCellColumns(g, out trimLeftSingleCellColumns))
			{
				func1(trimLeftSingleCellColumns);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol trimLeft");
		}

		// Token: 0x060062BA RID: 25274 RVA: 0x00141467 File Offset: 0x0013F667
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060062BB RID: 25275 RVA: 0x0014147C File Offset: 0x0013F67C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060062BC RID: 25276 RVA: 0x001414A6 File Offset: 0x0013F6A6
		public bool Equals(trimLeft other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BFE RID: 11262
		private ProgramNode _node;
	}
}
