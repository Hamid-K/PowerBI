using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E85 RID: 7813
	public struct relChildList : IProgramNodeBuilder, IEquatable<relChildList>
	{
		// Token: 0x17002BDE RID: 11230
		// (get) Token: 0x060107F6 RID: 67574 RVA: 0x0038CC02 File Offset: 0x0038AE02
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060107F7 RID: 67575 RVA: 0x0038CC0A File Offset: 0x0038AE0A
		private relChildList(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060107F8 RID: 67576 RVA: 0x0038CC13 File Offset: 0x0038AE13
		public static relChildList CreateUnsafe(ProgramNode node)
		{
			return new relChildList(node);
		}

		// Token: 0x060107F9 RID: 67577 RVA: 0x0038CC1C File Offset: 0x0038AE1C
		public static relChildList? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.relChildList)
			{
				return null;
			}
			return new relChildList?(relChildList.CreateUnsafe(node));
		}

		// Token: 0x060107FA RID: 67578 RVA: 0x0038CC56 File Offset: 0x0038AE56
		public static relChildList CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new relChildList(new Hole(g.Symbol.relChildList, holeId));
		}

		// Token: 0x060107FB RID: 67579 RVA: 0x0038CC6E File Offset: 0x0038AE6E
		public bool Is_relChildList_singleRelChildList(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.relChildList_singleRelChildList;
		}

		// Token: 0x060107FC RID: 67580 RVA: 0x0038CC88 File Offset: 0x0038AE88
		public bool Is_relChildList_singleRelChildList(GrammarBuilders g, out relChildList_singleRelChildList value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.relChildList_singleRelChildList)
			{
				value = relChildList_singleRelChildList.CreateUnsafe(this.Node);
				return true;
			}
			value = default(relChildList_singleRelChildList);
			return false;
		}

		// Token: 0x060107FD RID: 67581 RVA: 0x0038CCC0 File Offset: 0x0038AEC0
		public relChildList_singleRelChildList? As_relChildList_singleRelChildList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.relChildList_singleRelChildList)
			{
				return null;
			}
			return new relChildList_singleRelChildList?(relChildList_singleRelChildList.CreateUnsafe(this.Node));
		}

		// Token: 0x060107FE RID: 67582 RVA: 0x0038CD00 File Offset: 0x0038AF00
		public relChildList_singleRelChildList Cast_relChildList_singleRelChildList(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.relChildList_singleRelChildList)
			{
				return relChildList_singleRelChildList.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_relChildList_singleRelChildList is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060107FF RID: 67583 RVA: 0x0038CD55 File Offset: 0x0038AF55
		public bool Is_ConcatChild(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ConcatChild;
		}

		// Token: 0x06010800 RID: 67584 RVA: 0x0038CD6F File Offset: 0x0038AF6F
		public bool Is_ConcatChild(GrammarBuilders g, out ConcatChild value)
		{
			if (this.Node.GrammarRule == g.Rule.ConcatChild)
			{
				value = ConcatChild.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ConcatChild);
			return false;
		}

		// Token: 0x06010801 RID: 67585 RVA: 0x0038CDA4 File Offset: 0x0038AFA4
		public ConcatChild? As_ConcatChild(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ConcatChild)
			{
				return null;
			}
			return new ConcatChild?(ConcatChild.CreateUnsafe(this.Node));
		}

		// Token: 0x06010802 RID: 67586 RVA: 0x0038CDE4 File Offset: 0x0038AFE4
		public ConcatChild Cast_ConcatChild(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ConcatChild)
			{
				return ConcatChild.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ConcatChild is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06010803 RID: 67587 RVA: 0x0038CE3C File Offset: 0x0038B03C
		public T Switch<T>(GrammarBuilders g, Func<relChildList_singleRelChildList, T> func0, Func<ConcatChild, T> func1)
		{
			relChildList_singleRelChildList relChildList_singleRelChildList;
			if (this.Is_relChildList_singleRelChildList(g, out relChildList_singleRelChildList))
			{
				return func0(relChildList_singleRelChildList);
			}
			ConcatChild concatChild;
			if (this.Is_ConcatChild(g, out concatChild))
			{
				return func1(concatChild);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol relChildList");
		}

		// Token: 0x06010804 RID: 67588 RVA: 0x0038CE94 File Offset: 0x0038B094
		public void Switch(GrammarBuilders g, Action<relChildList_singleRelChildList> func0, Action<ConcatChild> func1)
		{
			relChildList_singleRelChildList relChildList_singleRelChildList;
			if (this.Is_relChildList_singleRelChildList(g, out relChildList_singleRelChildList))
			{
				func0(relChildList_singleRelChildList);
				return;
			}
			ConcatChild concatChild;
			if (this.Is_ConcatChild(g, out concatChild))
			{
				func1(concatChild);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol relChildList");
		}

		// Token: 0x06010805 RID: 67589 RVA: 0x0038CEEB File Offset: 0x0038B0EB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010806 RID: 67590 RVA: 0x0038CF00 File Offset: 0x0038B100
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010807 RID: 67591 RVA: 0x0038CF2A File Offset: 0x0038B12A
		public bool Equals(relChildList other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062C4 RID: 25284
		private ProgramNode _node;
	}
}
