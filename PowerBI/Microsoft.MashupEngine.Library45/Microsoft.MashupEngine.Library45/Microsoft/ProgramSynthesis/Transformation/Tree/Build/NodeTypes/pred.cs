using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E7C RID: 7804
	public struct pred : IProgramNodeBuilder, IEquatable<pred>
	{
		// Token: 0x17002BD5 RID: 11221
		// (get) Token: 0x0601074E RID: 67406 RVA: 0x0038B0F2 File Offset: 0x003892F2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601074F RID: 67407 RVA: 0x0038B0FA File Offset: 0x003892FA
		private pred(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010750 RID: 67408 RVA: 0x0038B103 File Offset: 0x00389303
		public static pred CreateUnsafe(ProgramNode node)
		{
			return new pred(node);
		}

		// Token: 0x06010751 RID: 67409 RVA: 0x0038B10C File Offset: 0x0038930C
		public static pred? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.pred)
			{
				return null;
			}
			return new pred?(pred.CreateUnsafe(node));
		}

		// Token: 0x06010752 RID: 67410 RVA: 0x0038B146 File Offset: 0x00389346
		public static pred CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new pred(new Hole(g.Symbol.pred, holeId));
		}

		// Token: 0x06010753 RID: 67411 RVA: 0x0038B15E File Offset: 0x0038935E
		public bool Is_IsKind(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IsKind;
		}

		// Token: 0x06010754 RID: 67412 RVA: 0x0038B178 File Offset: 0x00389378
		public bool Is_IsKind(GrammarBuilders g, out IsKind value)
		{
			if (this.Node.GrammarRule == g.Rule.IsKind)
			{
				value = IsKind.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IsKind);
			return false;
		}

		// Token: 0x06010755 RID: 67413 RVA: 0x0038B1B0 File Offset: 0x003893B0
		public IsKind? As_IsKind(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IsKind)
			{
				return null;
			}
			return new IsKind?(IsKind.CreateUnsafe(this.Node));
		}

		// Token: 0x06010756 RID: 67414 RVA: 0x0038B1F0 File Offset: 0x003893F0
		public IsKind Cast_IsKind(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IsKind)
			{
				return IsKind.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IsKind is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06010757 RID: 67415 RVA: 0x0038B245 File Offset: 0x00389445
		public bool Is_IsAttributePresent(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IsAttributePresent;
		}

		// Token: 0x06010758 RID: 67416 RVA: 0x0038B25F File Offset: 0x0038945F
		public bool Is_IsAttributePresent(GrammarBuilders g, out IsAttributePresent value)
		{
			if (this.Node.GrammarRule == g.Rule.IsAttributePresent)
			{
				value = IsAttributePresent.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IsAttributePresent);
			return false;
		}

		// Token: 0x06010759 RID: 67417 RVA: 0x0038B294 File Offset: 0x00389494
		public IsAttributePresent? As_IsAttributePresent(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IsAttributePresent)
			{
				return null;
			}
			return new IsAttributePresent?(IsAttributePresent.CreateUnsafe(this.Node));
		}

		// Token: 0x0601075A RID: 67418 RVA: 0x0038B2D4 File Offset: 0x003894D4
		public IsAttributePresent Cast_IsAttributePresent(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IsAttributePresent)
			{
				return IsAttributePresent.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IsAttributePresent is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0601075B RID: 67419 RVA: 0x0038B329 File Offset: 0x00389529
		public bool Is_IsNthChild(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.IsNthChild;
		}

		// Token: 0x0601075C RID: 67420 RVA: 0x0038B343 File Offset: 0x00389543
		public bool Is_IsNthChild(GrammarBuilders g, out IsNthChild value)
		{
			if (this.Node.GrammarRule == g.Rule.IsNthChild)
			{
				value = IsNthChild.CreateUnsafe(this.Node);
				return true;
			}
			value = default(IsNthChild);
			return false;
		}

		// Token: 0x0601075D RID: 67421 RVA: 0x0038B378 File Offset: 0x00389578
		public IsNthChild? As_IsNthChild(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.IsNthChild)
			{
				return null;
			}
			return new IsNthChild?(IsNthChild.CreateUnsafe(this.Node));
		}

		// Token: 0x0601075E RID: 67422 RVA: 0x0038B3B8 File Offset: 0x003895B8
		public IsNthChild Cast_IsNthChild(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.IsNthChild)
			{
				return IsNthChild.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_IsNthChild is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0601075F RID: 67423 RVA: 0x0038B40D File Offset: 0x0038960D
		public bool Is_HasNChildren(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.HasNChildren;
		}

		// Token: 0x06010760 RID: 67424 RVA: 0x0038B427 File Offset: 0x00389627
		public bool Is_HasNChildren(GrammarBuilders g, out HasNChildren value)
		{
			if (this.Node.GrammarRule == g.Rule.HasNChildren)
			{
				value = HasNChildren.CreateUnsafe(this.Node);
				return true;
			}
			value = default(HasNChildren);
			return false;
		}

		// Token: 0x06010761 RID: 67425 RVA: 0x0038B45C File Offset: 0x0038965C
		public HasNChildren? As_HasNChildren(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.HasNChildren)
			{
				return null;
			}
			return new HasNChildren?(HasNChildren.CreateUnsafe(this.Node));
		}

		// Token: 0x06010762 RID: 67426 RVA: 0x0038B49C File Offset: 0x0038969C
		public HasNChildren Cast_HasNChildren(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.HasNChildren)
			{
				return HasNChildren.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_HasNChildren is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06010763 RID: 67427 RVA: 0x0038B4F1 File Offset: 0x003896F1
		public bool Is_Not(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Not;
		}

		// Token: 0x06010764 RID: 67428 RVA: 0x0038B50B File Offset: 0x0038970B
		public bool Is_Not(GrammarBuilders g, out Not value)
		{
			if (this.Node.GrammarRule == g.Rule.Not)
			{
				value = Not.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Not);
			return false;
		}

		// Token: 0x06010765 RID: 67429 RVA: 0x0038B540 File Offset: 0x00389740
		public Not? As_Not(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Not)
			{
				return null;
			}
			return new Not?(Not.CreateUnsafe(this.Node));
		}

		// Token: 0x06010766 RID: 67430 RVA: 0x0038B580 File Offset: 0x00389780
		public Not Cast_Not(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Not)
			{
				return Not.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Not is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06010767 RID: 67431 RVA: 0x0038B5D8 File Offset: 0x003897D8
		public T Switch<T>(GrammarBuilders g, Func<IsKind, T> func0, Func<IsAttributePresent, T> func1, Func<IsNthChild, T> func2, Func<HasNChildren, T> func3, Func<Not, T> func4)
		{
			IsKind isKind;
			if (this.Is_IsKind(g, out isKind))
			{
				return func0(isKind);
			}
			IsAttributePresent isAttributePresent;
			if (this.Is_IsAttributePresent(g, out isAttributePresent))
			{
				return func1(isAttributePresent);
			}
			IsNthChild isNthChild;
			if (this.Is_IsNthChild(g, out isNthChild))
			{
				return func2(isNthChild);
			}
			HasNChildren hasNChildren;
			if (this.Is_HasNChildren(g, out hasNChildren))
			{
				return func3(hasNChildren);
			}
			Not not;
			if (this.Is_Not(g, out not))
			{
				return func4(not);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol pred");
		}

		// Token: 0x06010768 RID: 67432 RVA: 0x0038B66C File Offset: 0x0038986C
		public void Switch(GrammarBuilders g, Action<IsKind> func0, Action<IsAttributePresent> func1, Action<IsNthChild> func2, Action<HasNChildren> func3, Action<Not> func4)
		{
			IsKind isKind;
			if (this.Is_IsKind(g, out isKind))
			{
				func0(isKind);
				return;
			}
			IsAttributePresent isAttributePresent;
			if (this.Is_IsAttributePresent(g, out isAttributePresent))
			{
				func1(isAttributePresent);
				return;
			}
			IsNthChild isNthChild;
			if (this.Is_IsNthChild(g, out isNthChild))
			{
				func2(isNthChild);
				return;
			}
			HasNChildren hasNChildren;
			if (this.Is_HasNChildren(g, out hasNChildren))
			{
				func3(hasNChildren);
				return;
			}
			Not not;
			if (this.Is_Not(g, out not))
			{
				func4(not);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol pred");
		}

		// Token: 0x06010769 RID: 67433 RVA: 0x0038B700 File Offset: 0x00389900
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601076A RID: 67434 RVA: 0x0038B714 File Offset: 0x00389914
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601076B RID: 67435 RVA: 0x0038B73E File Offset: 0x0038993E
		public bool Equals(pred other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062BB RID: 25275
		private ProgramNode _node;
	}
}
