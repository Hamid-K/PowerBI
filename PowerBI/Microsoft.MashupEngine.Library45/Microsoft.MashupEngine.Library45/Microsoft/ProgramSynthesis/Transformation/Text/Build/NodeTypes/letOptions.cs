using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C3D RID: 7229
	public struct letOptions : IProgramNodeBuilder, IEquatable<letOptions>
	{
		// Token: 0x170028D3 RID: 10451
		// (get) Token: 0x0600F3BE RID: 62398 RVA: 0x0034331A File Offset: 0x0034151A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F3BF RID: 62399 RVA: 0x00343322 File Offset: 0x00341522
		private letOptions(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F3C0 RID: 62400 RVA: 0x0034332B File Offset: 0x0034152B
		public static letOptions CreateUnsafe(ProgramNode node)
		{
			return new letOptions(node);
		}

		// Token: 0x0600F3C1 RID: 62401 RVA: 0x00343334 File Offset: 0x00341534
		public static letOptions? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.letOptions)
			{
				return null;
			}
			return new letOptions?(letOptions.CreateUnsafe(node));
		}

		// Token: 0x0600F3C2 RID: 62402 RVA: 0x0034336E File Offset: 0x0034156E
		public static letOptions CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new letOptions(new Hole(g.Symbol.letOptions, holeId));
		}

		// Token: 0x0600F3C3 RID: 62403 RVA: 0x00343386 File Offset: 0x00341586
		public bool Is_LetCell(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LetCell;
		}

		// Token: 0x0600F3C4 RID: 62404 RVA: 0x003433A0 File Offset: 0x003415A0
		public bool Is_LetCell(GrammarBuilders g, out LetCell value)
		{
			if (this.Node.GrammarRule == g.Rule.LetCell)
			{
				value = LetCell.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LetCell);
			return false;
		}

		// Token: 0x0600F3C5 RID: 62405 RVA: 0x003433D8 File Offset: 0x003415D8
		public LetCell? As_LetCell(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LetCell)
			{
				return null;
			}
			return new LetCell?(LetCell.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F3C6 RID: 62406 RVA: 0x00343418 File Offset: 0x00341618
		public LetCell Cast_LetCell(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LetCell)
			{
				return LetCell.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LetCell is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F3C7 RID: 62407 RVA: 0x0034346D File Offset: 0x0034166D
		public bool Is_LetX(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LetX;
		}

		// Token: 0x0600F3C8 RID: 62408 RVA: 0x00343487 File Offset: 0x00341687
		public bool Is_LetX(GrammarBuilders g, out LetX value)
		{
			if (this.Node.GrammarRule == g.Rule.LetX)
			{
				value = LetX.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LetX);
			return false;
		}

		// Token: 0x0600F3C9 RID: 62409 RVA: 0x003434BC File Offset: 0x003416BC
		public LetX? As_LetX(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LetX)
			{
				return null;
			}
			return new LetX?(LetX.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F3CA RID: 62410 RVA: 0x003434FC File Offset: 0x003416FC
		public LetX Cast_LetX(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LetX)
			{
				return LetX.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LetX is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F3CB RID: 62411 RVA: 0x00343554 File Offset: 0x00341754
		public T Switch<T>(GrammarBuilders g, Func<LetCell, T> func0, Func<LetX, T> func1)
		{
			LetCell letCell;
			if (this.Is_LetCell(g, out letCell))
			{
				return func0(letCell);
			}
			LetX letX;
			if (this.Is_LetX(g, out letX))
			{
				return func1(letX);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol letOptions");
		}

		// Token: 0x0600F3CC RID: 62412 RVA: 0x003435AC File Offset: 0x003417AC
		public void Switch(GrammarBuilders g, Action<LetCell> func0, Action<LetX> func1)
		{
			LetCell letCell;
			if (this.Is_LetCell(g, out letCell))
			{
				func0(letCell);
				return;
			}
			LetX letX;
			if (this.Is_LetX(g, out letX))
			{
				func1(letX);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol letOptions");
		}

		// Token: 0x0600F3CD RID: 62413 RVA: 0x00343603 File Offset: 0x00341803
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F3CE RID: 62414 RVA: 0x00343618 File Offset: 0x00341818
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F3CF RID: 62415 RVA: 0x00343642 File Offset: 0x00341842
		public bool Equals(letOptions other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B2C RID: 23340
		private ProgramNode _node;
	}
}
