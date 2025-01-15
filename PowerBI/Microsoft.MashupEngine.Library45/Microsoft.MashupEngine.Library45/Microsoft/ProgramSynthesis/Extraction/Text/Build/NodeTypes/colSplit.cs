using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F38 RID: 3896
	public struct colSplit : IProgramNodeBuilder, IEquatable<colSplit>
	{
		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06006C0D RID: 27661 RVA: 0x00161EF6 File Offset: 0x001600F6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006C0E RID: 27662 RVA: 0x00161EFE File Offset: 0x001600FE
		private colSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006C0F RID: 27663 RVA: 0x00161F07 File Offset: 0x00160107
		public static colSplit CreateUnsafe(ProgramNode node)
		{
			return new colSplit(node);
		}

		// Token: 0x06006C10 RID: 27664 RVA: 0x00161F10 File Offset: 0x00160110
		public static colSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.colSplit)
			{
				return null;
			}
			return new colSplit?(colSplit.CreateUnsafe(node));
		}

		// Token: 0x06006C11 RID: 27665 RVA: 0x00161F4A File Offset: 0x0016014A
		public static colSplit CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new colSplit(new Hole(g.Symbol.colSplit, holeId));
		}

		// Token: 0x06006C12 RID: 27666 RVA: 0x00161F62 File Offset: 0x00160162
		public bool Is_List(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.List;
		}

		// Token: 0x06006C13 RID: 27667 RVA: 0x00161F7C File Offset: 0x0016017C
		public bool Is_List(GrammarBuilders g, out List value)
		{
			if (this.Node.GrammarRule == g.Rule.List)
			{
				value = List.CreateUnsafe(this.Node);
				return true;
			}
			value = default(List);
			return false;
		}

		// Token: 0x06006C14 RID: 27668 RVA: 0x00161FB4 File Offset: 0x001601B4
		public List? As_List(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.List)
			{
				return null;
			}
			return new List?(List.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C15 RID: 27669 RVA: 0x00161FF4 File Offset: 0x001601F4
		public List Cast_List(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.List)
			{
				return List.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_List is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C16 RID: 27670 RVA: 0x00162049 File Offset: 0x00160249
		public bool Is_LetSplit(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LetSplit;
		}

		// Token: 0x06006C17 RID: 27671 RVA: 0x00162063 File Offset: 0x00160263
		public bool Is_LetSplit(GrammarBuilders g, out LetSplit value)
		{
			if (this.Node.GrammarRule == g.Rule.LetSplit)
			{
				value = LetSplit.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LetSplit);
			return false;
		}

		// Token: 0x06006C18 RID: 27672 RVA: 0x00162098 File Offset: 0x00160298
		public LetSplit? As_LetSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LetSplit)
			{
				return null;
			}
			return new LetSplit?(LetSplit.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C19 RID: 27673 RVA: 0x001620D8 File Offset: 0x001602D8
		public LetSplit Cast_LetSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LetSplit)
			{
				return LetSplit.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LetSplit is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C1A RID: 27674 RVA: 0x00162130 File Offset: 0x00160330
		public T Switch<T>(GrammarBuilders g, Func<List, T> func0, Func<LetSplit, T> func1)
		{
			List list;
			if (this.Is_List(g, out list))
			{
				return func0(list);
			}
			LetSplit letSplit;
			if (this.Is_LetSplit(g, out letSplit))
			{
				return func1(letSplit);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol colSplit");
		}

		// Token: 0x06006C1B RID: 27675 RVA: 0x00162188 File Offset: 0x00160388
		public void Switch(GrammarBuilders g, Action<List> func0, Action<LetSplit> func1)
		{
			List list;
			if (this.Is_List(g, out list))
			{
				func0(list);
				return;
			}
			LetSplit letSplit;
			if (this.Is_LetSplit(g, out letSplit))
			{
				func1(letSplit);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol colSplit");
		}

		// Token: 0x06006C1C RID: 27676 RVA: 0x001621DF File Offset: 0x001603DF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006C1D RID: 27677 RVA: 0x001621F4 File Offset: 0x001603F4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006C1E RID: 27678 RVA: 0x0016221E File Offset: 0x0016041E
		public bool Equals(colSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F23 RID: 12067
		private ProgramNode _node;
	}
}
