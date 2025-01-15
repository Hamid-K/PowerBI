using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001084 RID: 4228
	public struct nodeCollection : IProgramNodeBuilder, IEquatable<nodeCollection>
	{
		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x06007EFD RID: 32509 RVA: 0x001AAFF2 File Offset: 0x001A91F2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007EFE RID: 32510 RVA: 0x001AAFFA File Offset: 0x001A91FA
		private nodeCollection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007EFF RID: 32511 RVA: 0x001AB003 File Offset: 0x001A9203
		public static nodeCollection CreateUnsafe(ProgramNode node)
		{
			return new nodeCollection(node);
		}

		// Token: 0x06007F00 RID: 32512 RVA: 0x001AB00C File Offset: 0x001A920C
		public static nodeCollection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.nodeCollection)
			{
				return null;
			}
			return new nodeCollection?(nodeCollection.CreateUnsafe(node));
		}

		// Token: 0x06007F01 RID: 32513 RVA: 0x001AB046 File Offset: 0x001A9246
		public static nodeCollection CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new nodeCollection(new Hole(g.Symbol.nodeCollection, holeId));
		}

		// Token: 0x06007F02 RID: 32514 RVA: 0x001AB05E File Offset: 0x001A925E
		public bool Is_AsCollection(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.AsCollection;
		}

		// Token: 0x06007F03 RID: 32515 RVA: 0x001AB078 File Offset: 0x001A9278
		public bool Is_AsCollection(GrammarBuilders g, out AsCollection value)
		{
			if (this.Node.GrammarRule == g.Rule.AsCollection)
			{
				value = AsCollection.CreateUnsafe(this.Node);
				return true;
			}
			value = default(AsCollection);
			return false;
		}

		// Token: 0x06007F04 RID: 32516 RVA: 0x001AB0B0 File Offset: 0x001A92B0
		public AsCollection? As_AsCollection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.AsCollection)
			{
				return null;
			}
			return new AsCollection?(AsCollection.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F05 RID: 32517 RVA: 0x001AB0F0 File Offset: 0x001A92F0
		public AsCollection Cast_AsCollection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.AsCollection)
			{
				return AsCollection.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_AsCollection is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007F06 RID: 32518 RVA: 0x001AB145 File Offset: 0x001A9345
		public bool Is_DescendantsOf(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DescendantsOf;
		}

		// Token: 0x06007F07 RID: 32519 RVA: 0x001AB15F File Offset: 0x001A935F
		public bool Is_DescendantsOf(GrammarBuilders g, out DescendantsOf value)
		{
			if (this.Node.GrammarRule == g.Rule.DescendantsOf)
			{
				value = DescendantsOf.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DescendantsOf);
			return false;
		}

		// Token: 0x06007F08 RID: 32520 RVA: 0x001AB194 File Offset: 0x001A9394
		public DescendantsOf? As_DescendantsOf(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DescendantsOf)
			{
				return null;
			}
			return new DescendantsOf?(DescendantsOf.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F09 RID: 32521 RVA: 0x001AB1D4 File Offset: 0x001A93D4
		public DescendantsOf Cast_DescendantsOf(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DescendantsOf)
			{
				return DescendantsOf.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DescendantsOf is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007F0A RID: 32522 RVA: 0x001AB229 File Offset: 0x001A9429
		public bool Is_RightSiblingOf(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RightSiblingOf;
		}

		// Token: 0x06007F0B RID: 32523 RVA: 0x001AB243 File Offset: 0x001A9443
		public bool Is_RightSiblingOf(GrammarBuilders g, out RightSiblingOf value)
		{
			if (this.Node.GrammarRule == g.Rule.RightSiblingOf)
			{
				value = RightSiblingOf.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RightSiblingOf);
			return false;
		}

		// Token: 0x06007F0C RID: 32524 RVA: 0x001AB278 File Offset: 0x001A9478
		public RightSiblingOf? As_RightSiblingOf(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RightSiblingOf)
			{
				return null;
			}
			return new RightSiblingOf?(RightSiblingOf.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F0D RID: 32525 RVA: 0x001AB2B8 File Offset: 0x001A94B8
		public RightSiblingOf Cast_RightSiblingOf(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RightSiblingOf)
			{
				return RightSiblingOf.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RightSiblingOf is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007F0E RID: 32526 RVA: 0x001AB30D File Offset: 0x001A950D
		public bool Is_ClassFilter(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ClassFilter;
		}

		// Token: 0x06007F0F RID: 32527 RVA: 0x001AB327 File Offset: 0x001A9527
		public bool Is_ClassFilter(GrammarBuilders g, out ClassFilter value)
		{
			if (this.Node.GrammarRule == g.Rule.ClassFilter)
			{
				value = ClassFilter.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ClassFilter);
			return false;
		}

		// Token: 0x06007F10 RID: 32528 RVA: 0x001AB35C File Offset: 0x001A955C
		public ClassFilter? As_ClassFilter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ClassFilter)
			{
				return null;
			}
			return new ClassFilter?(ClassFilter.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F11 RID: 32529 RVA: 0x001AB39C File Offset: 0x001A959C
		public ClassFilter Cast_ClassFilter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ClassFilter)
			{
				return ClassFilter.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ClassFilter is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007F12 RID: 32530 RVA: 0x001AB3F1 File Offset: 0x001A95F1
		public bool Is_IDFilter(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IDFilter;
		}

		// Token: 0x06007F13 RID: 32531 RVA: 0x001AB40B File Offset: 0x001A960B
		public bool Is_IDFilter(GrammarBuilders g, out IDFilter value)
		{
			if (this.Node.GrammarRule == g.Rule.IDFilter)
			{
				value = IDFilter.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IDFilter);
			return false;
		}

		// Token: 0x06007F14 RID: 32532 RVA: 0x001AB440 File Offset: 0x001A9640
		public IDFilter? As_IDFilter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IDFilter)
			{
				return null;
			}
			return new IDFilter?(IDFilter.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F15 RID: 32533 RVA: 0x001AB480 File Offset: 0x001A9680
		public IDFilter Cast_IDFilter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IDFilter)
			{
				return IDFilter.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IDFilter is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007F16 RID: 32534 RVA: 0x001AB4D5 File Offset: 0x001A96D5
		public bool Is_NodeNameFilter(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NodeNameFilter;
		}

		// Token: 0x06007F17 RID: 32535 RVA: 0x001AB4EF File Offset: 0x001A96EF
		public bool Is_NodeNameFilter(GrammarBuilders g, out NodeNameFilter value)
		{
			if (this.Node.GrammarRule == g.Rule.NodeNameFilter)
			{
				value = NodeNameFilter.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NodeNameFilter);
			return false;
		}

		// Token: 0x06007F18 RID: 32536 RVA: 0x001AB524 File Offset: 0x001A9724
		public NodeNameFilter? As_NodeNameFilter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NodeNameFilter)
			{
				return null;
			}
			return new NodeNameFilter?(NodeNameFilter.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F19 RID: 32537 RVA: 0x001AB564 File Offset: 0x001A9764
		public NodeNameFilter Cast_NodeNameFilter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NodeNameFilter)
			{
				return NodeNameFilter.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NodeNameFilter is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007F1A RID: 32538 RVA: 0x001AB5B9 File Offset: 0x001A97B9
		public bool Is_ItemPropFilter(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ItemPropFilter;
		}

		// Token: 0x06007F1B RID: 32539 RVA: 0x001AB5D3 File Offset: 0x001A97D3
		public bool Is_ItemPropFilter(GrammarBuilders g, out ItemPropFilter value)
		{
			if (this.Node.GrammarRule == g.Rule.ItemPropFilter)
			{
				value = ItemPropFilter.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ItemPropFilter);
			return false;
		}

		// Token: 0x06007F1C RID: 32540 RVA: 0x001AB608 File Offset: 0x001A9808
		public ItemPropFilter? As_ItemPropFilter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ItemPropFilter)
			{
				return null;
			}
			return new ItemPropFilter?(ItemPropFilter.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F1D RID: 32541 RVA: 0x001AB648 File Offset: 0x001A9848
		public ItemPropFilter Cast_ItemPropFilter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ItemPropFilter)
			{
				return ItemPropFilter.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ItemPropFilter is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007F1E RID: 32542 RVA: 0x001AB69D File Offset: 0x001A989D
		public bool Is_NthChildFilter(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NthChildFilter;
		}

		// Token: 0x06007F1F RID: 32543 RVA: 0x001AB6B7 File Offset: 0x001A98B7
		public bool Is_NthChildFilter(GrammarBuilders g, out NthChildFilter value)
		{
			if (this.Node.GrammarRule == g.Rule.NthChildFilter)
			{
				value = NthChildFilter.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NthChildFilter);
			return false;
		}

		// Token: 0x06007F20 RID: 32544 RVA: 0x001AB6EC File Offset: 0x001A98EC
		public NthChildFilter? As_NthChildFilter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NthChildFilter)
			{
				return null;
			}
			return new NthChildFilter?(NthChildFilter.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F21 RID: 32545 RVA: 0x001AB72C File Offset: 0x001A992C
		public NthChildFilter Cast_NthChildFilter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NthChildFilter)
			{
				return NthChildFilter.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NthChildFilter is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007F22 RID: 32546 RVA: 0x001AB781 File Offset: 0x001A9981
		public bool Is_NthLastChildFilter(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NthLastChildFilter;
		}

		// Token: 0x06007F23 RID: 32547 RVA: 0x001AB79B File Offset: 0x001A999B
		public bool Is_NthLastChildFilter(GrammarBuilders g, out NthLastChildFilter value)
		{
			if (this.Node.GrammarRule == g.Rule.NthLastChildFilter)
			{
				value = NthLastChildFilter.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NthLastChildFilter);
			return false;
		}

		// Token: 0x06007F24 RID: 32548 RVA: 0x001AB7D0 File Offset: 0x001A99D0
		public NthLastChildFilter? As_NthLastChildFilter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NthLastChildFilter)
			{
				return null;
			}
			return new NthLastChildFilter?(NthLastChildFilter.CreateUnsafe(this.Node));
		}

		// Token: 0x06007F25 RID: 32549 RVA: 0x001AB810 File Offset: 0x001A9A10
		public NthLastChildFilter Cast_NthLastChildFilter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NthLastChildFilter)
			{
				return NthLastChildFilter.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NthLastChildFilter is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007F26 RID: 32550 RVA: 0x001AB868 File Offset: 0x001A9A68
		public T Switch<T>(GrammarBuilders g, Func<AsCollection, T> func0, Func<DescendantsOf, T> func1, Func<RightSiblingOf, T> func2, Func<ClassFilter, T> func3, Func<IDFilter, T> func4, Func<NodeNameFilter, T> func5, Func<ItemPropFilter, T> func6, Func<NthChildFilter, T> func7, Func<NthLastChildFilter, T> func8)
		{
			AsCollection asCollection;
			if (this.Is_AsCollection(g, out asCollection))
			{
				return func0(asCollection);
			}
			DescendantsOf descendantsOf;
			if (this.Is_DescendantsOf(g, out descendantsOf))
			{
				return func1(descendantsOf);
			}
			RightSiblingOf rightSiblingOf;
			if (this.Is_RightSiblingOf(g, out rightSiblingOf))
			{
				return func2(rightSiblingOf);
			}
			ClassFilter classFilter;
			if (this.Is_ClassFilter(g, out classFilter))
			{
				return func3(classFilter);
			}
			IDFilter idfilter;
			if (this.Is_IDFilter(g, out idfilter))
			{
				return func4(idfilter);
			}
			NodeNameFilter nodeNameFilter;
			if (this.Is_NodeNameFilter(g, out nodeNameFilter))
			{
				return func5(nodeNameFilter);
			}
			ItemPropFilter itemPropFilter;
			if (this.Is_ItemPropFilter(g, out itemPropFilter))
			{
				return func6(itemPropFilter);
			}
			NthChildFilter nthChildFilter;
			if (this.Is_NthChildFilter(g, out nthChildFilter))
			{
				return func7(nthChildFilter);
			}
			NthLastChildFilter nthLastChildFilter;
			if (this.Is_NthLastChildFilter(g, out nthLastChildFilter))
			{
				return func8(nthLastChildFilter);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol nodeCollection");
		}

		// Token: 0x06007F27 RID: 32551 RVA: 0x001AB950 File Offset: 0x001A9B50
		public void Switch(GrammarBuilders g, Action<AsCollection> func0, Action<DescendantsOf> func1, Action<RightSiblingOf> func2, Action<ClassFilter> func3, Action<IDFilter> func4, Action<NodeNameFilter> func5, Action<ItemPropFilter> func6, Action<NthChildFilter> func7, Action<NthLastChildFilter> func8)
		{
			AsCollection asCollection;
			if (this.Is_AsCollection(g, out asCollection))
			{
				func0(asCollection);
				return;
			}
			DescendantsOf descendantsOf;
			if (this.Is_DescendantsOf(g, out descendantsOf))
			{
				func1(descendantsOf);
				return;
			}
			RightSiblingOf rightSiblingOf;
			if (this.Is_RightSiblingOf(g, out rightSiblingOf))
			{
				func2(rightSiblingOf);
				return;
			}
			ClassFilter classFilter;
			if (this.Is_ClassFilter(g, out classFilter))
			{
				func3(classFilter);
				return;
			}
			IDFilter idfilter;
			if (this.Is_IDFilter(g, out idfilter))
			{
				func4(idfilter);
				return;
			}
			NodeNameFilter nodeNameFilter;
			if (this.Is_NodeNameFilter(g, out nodeNameFilter))
			{
				func5(nodeNameFilter);
				return;
			}
			ItemPropFilter itemPropFilter;
			if (this.Is_ItemPropFilter(g, out itemPropFilter))
			{
				func6(itemPropFilter);
				return;
			}
			NthChildFilter nthChildFilter;
			if (this.Is_NthChildFilter(g, out nthChildFilter))
			{
				func7(nthChildFilter);
				return;
			}
			NthLastChildFilter nthLastChildFilter;
			if (this.Is_NthLastChildFilter(g, out nthLastChildFilter))
			{
				func8(nthLastChildFilter);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol nodeCollection");
		}

		// Token: 0x06007F28 RID: 32552 RVA: 0x001ABA38 File Offset: 0x001A9C38
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007F29 RID: 32553 RVA: 0x001ABA4C File Offset: 0x001A9C4C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007F2A RID: 32554 RVA: 0x001ABA76 File Offset: 0x001A9C76
		public bool Equals(nodeCollection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400339D RID: 13213
		private ProgramNode _node;
	}
}
