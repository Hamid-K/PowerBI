using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E81 RID: 7809
	public struct sequenceChildren : IProgramNodeBuilder, IEquatable<sequenceChildren>
	{
		// Token: 0x17002BDA RID: 11226
		// (get) Token: 0x060107B0 RID: 67504 RVA: 0x0038C1C2 File Offset: 0x0038A3C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060107B1 RID: 67505 RVA: 0x0038C1CA File Offset: 0x0038A3CA
		private sequenceChildren(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060107B2 RID: 67506 RVA: 0x0038C1D3 File Offset: 0x0038A3D3
		public static sequenceChildren CreateUnsafe(ProgramNode node)
		{
			return new sequenceChildren(node);
		}

		// Token: 0x060107B3 RID: 67507 RVA: 0x0038C1DC File Offset: 0x0038A3DC
		public static sequenceChildren? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sequenceChildren)
			{
				return null;
			}
			return new sequenceChildren?(sequenceChildren.CreateUnsafe(node));
		}

		// Token: 0x060107B4 RID: 67508 RVA: 0x0038C216 File Offset: 0x0038A416
		public static sequenceChildren CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sequenceChildren(new Hole(g.Symbol.sequenceChildren, holeId));
		}

		// Token: 0x060107B5 RID: 67509 RVA: 0x0038C22E File Offset: 0x0038A42E
		public bool Is_sequenceChildren_children(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.sequenceChildren_children;
		}

		// Token: 0x060107B6 RID: 67510 RVA: 0x0038C248 File Offset: 0x0038A448
		public bool Is_sequenceChildren_children(GrammarBuilders g, out sequenceChildren_children value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.sequenceChildren_children)
			{
				value = sequenceChildren_children.CreateUnsafe(this.Node);
				return true;
			}
			value = default(sequenceChildren_children);
			return false;
		}

		// Token: 0x060107B7 RID: 67511 RVA: 0x0038C280 File Offset: 0x0038A480
		public sequenceChildren_children? As_sequenceChildren_children(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.sequenceChildren_children)
			{
				return null;
			}
			return new sequenceChildren_children?(sequenceChildren_children.CreateUnsafe(this.Node));
		}

		// Token: 0x060107B8 RID: 67512 RVA: 0x0038C2C0 File Offset: 0x0038A4C0
		public sequenceChildren_children Cast_sequenceChildren_children(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.sequenceChildren_children)
			{
				return sequenceChildren_children.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_sequenceChildren_children is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060107B9 RID: 67513 RVA: 0x0038C315 File Offset: 0x0038A515
		public bool Is_InsertAtAbs(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.InsertAtAbs;
		}

		// Token: 0x060107BA RID: 67514 RVA: 0x0038C32F File Offset: 0x0038A52F
		public bool Is_InsertAtAbs(GrammarBuilders g, out InsertAtAbs value)
		{
			if (this.Node.GrammarRule == g.Rule.InsertAtAbs)
			{
				value = InsertAtAbs.CreateUnsafe(this.Node);
				return true;
			}
			value = default(InsertAtAbs);
			return false;
		}

		// Token: 0x060107BB RID: 67515 RVA: 0x0038C364 File Offset: 0x0038A564
		public InsertAtAbs? As_InsertAtAbs(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.InsertAtAbs)
			{
				return null;
			}
			return new InsertAtAbs?(InsertAtAbs.CreateUnsafe(this.Node));
		}

		// Token: 0x060107BC RID: 67516 RVA: 0x0038C3A4 File Offset: 0x0038A5A4
		public InsertAtAbs Cast_InsertAtAbs(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.InsertAtAbs)
			{
				return InsertAtAbs.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_InsertAtAbs is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060107BD RID: 67517 RVA: 0x0038C3F9 File Offset: 0x0038A5F9
		public bool Is_InsertAtRel(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.InsertAtRel;
		}

		// Token: 0x060107BE RID: 67518 RVA: 0x0038C413 File Offset: 0x0038A613
		public bool Is_InsertAtRel(GrammarBuilders g, out InsertAtRel value)
		{
			if (this.Node.GrammarRule == g.Rule.InsertAtRel)
			{
				value = InsertAtRel.CreateUnsafe(this.Node);
				return true;
			}
			value = default(InsertAtRel);
			return false;
		}

		// Token: 0x060107BF RID: 67519 RVA: 0x0038C448 File Offset: 0x0038A648
		public InsertAtRel? As_InsertAtRel(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.InsertAtRel)
			{
				return null;
			}
			return new InsertAtRel?(InsertAtRel.CreateUnsafe(this.Node));
		}

		// Token: 0x060107C0 RID: 67520 RVA: 0x0038C488 File Offset: 0x0038A688
		public InsertAtRel Cast_InsertAtRel(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.InsertAtRel)
			{
				return InsertAtRel.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_InsertAtRel is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060107C1 RID: 67521 RVA: 0x0038C4DD File Offset: 0x0038A6DD
		public bool Is_DeleteChild(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.DeleteChild;
		}

		// Token: 0x060107C2 RID: 67522 RVA: 0x0038C4F7 File Offset: 0x0038A6F7
		public bool Is_DeleteChild(GrammarBuilders g, out DeleteChild value)
		{
			if (this.Node.GrammarRule == g.Rule.DeleteChild)
			{
				value = DeleteChild.CreateUnsafe(this.Node);
				return true;
			}
			value = default(DeleteChild);
			return false;
		}

		// Token: 0x060107C3 RID: 67523 RVA: 0x0038C52C File Offset: 0x0038A72C
		public DeleteChild? As_DeleteChild(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.DeleteChild)
			{
				return null;
			}
			return new DeleteChild?(DeleteChild.CreateUnsafe(this.Node));
		}

		// Token: 0x060107C4 RID: 67524 RVA: 0x0038C56C File Offset: 0x0038A76C
		public DeleteChild Cast_DeleteChild(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.DeleteChild)
			{
				return DeleteChild.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_DeleteChild is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060107C5 RID: 67525 RVA: 0x0038C5C1 File Offset: 0x0038A7C1
		public bool Is_ReplaceChildren(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ReplaceChildren;
		}

		// Token: 0x060107C6 RID: 67526 RVA: 0x0038C5DB File Offset: 0x0038A7DB
		public bool Is_ReplaceChildren(GrammarBuilders g, out ReplaceChildren value)
		{
			if (this.Node.GrammarRule == g.Rule.ReplaceChildren)
			{
				value = ReplaceChildren.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ReplaceChildren);
			return false;
		}

		// Token: 0x060107C7 RID: 67527 RVA: 0x0038C610 File Offset: 0x0038A810
		public ReplaceChildren? As_ReplaceChildren(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ReplaceChildren)
			{
				return null;
			}
			return new ReplaceChildren?(ReplaceChildren.CreateUnsafe(this.Node));
		}

		// Token: 0x060107C8 RID: 67528 RVA: 0x0038C650 File Offset: 0x0038A850
		public ReplaceChildren Cast_ReplaceChildren(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ReplaceChildren)
			{
				return ReplaceChildren.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ReplaceChildren is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060107C9 RID: 67529 RVA: 0x0038C6A5 File Offset: 0x0038A8A5
		public bool Is_sequenceChildren_convertSequence(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.sequenceChildren_convertSequence;
		}

		// Token: 0x060107CA RID: 67530 RVA: 0x0038C6BF File Offset: 0x0038A8BF
		public bool Is_sequenceChildren_convertSequence(GrammarBuilders g, out sequenceChildren_convertSequence value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.sequenceChildren_convertSequence)
			{
				value = sequenceChildren_convertSequence.CreateUnsafe(this.Node);
				return true;
			}
			value = default(sequenceChildren_convertSequence);
			return false;
		}

		// Token: 0x060107CB RID: 67531 RVA: 0x0038C6F4 File Offset: 0x0038A8F4
		public sequenceChildren_convertSequence? As_sequenceChildren_convertSequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.sequenceChildren_convertSequence)
			{
				return null;
			}
			return new sequenceChildren_convertSequence?(sequenceChildren_convertSequence.CreateUnsafe(this.Node));
		}

		// Token: 0x060107CC RID: 67532 RVA: 0x0038C734 File Offset: 0x0038A934
		public sequenceChildren_convertSequence Cast_sequenceChildren_convertSequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.sequenceChildren_convertSequence)
			{
				return sequenceChildren_convertSequence.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_sequenceChildren_convertSequence is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060107CD RID: 67533 RVA: 0x0038C78C File Offset: 0x0038A98C
		public T Switch<T>(GrammarBuilders g, Func<sequenceChildren_children, T> func0, Func<InsertAtAbs, T> func1, Func<InsertAtRel, T> func2, Func<DeleteChild, T> func3, Func<ReplaceChildren, T> func4, Func<sequenceChildren_convertSequence, T> func5)
		{
			sequenceChildren_children sequenceChildren_children;
			if (this.Is_sequenceChildren_children(g, out sequenceChildren_children))
			{
				return func0(sequenceChildren_children);
			}
			InsertAtAbs insertAtAbs;
			if (this.Is_InsertAtAbs(g, out insertAtAbs))
			{
				return func1(insertAtAbs);
			}
			InsertAtRel insertAtRel;
			if (this.Is_InsertAtRel(g, out insertAtRel))
			{
				return func2(insertAtRel);
			}
			DeleteChild deleteChild;
			if (this.Is_DeleteChild(g, out deleteChild))
			{
				return func3(deleteChild);
			}
			ReplaceChildren replaceChildren;
			if (this.Is_ReplaceChildren(g, out replaceChildren))
			{
				return func4(replaceChildren);
			}
			sequenceChildren_convertSequence sequenceChildren_convertSequence;
			if (this.Is_sequenceChildren_convertSequence(g, out sequenceChildren_convertSequence))
			{
				return func5(sequenceChildren_convertSequence);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol sequenceChildren");
		}

		// Token: 0x060107CE RID: 67534 RVA: 0x0038C838 File Offset: 0x0038AA38
		public void Switch(GrammarBuilders g, Action<sequenceChildren_children> func0, Action<InsertAtAbs> func1, Action<InsertAtRel> func2, Action<DeleteChild> func3, Action<ReplaceChildren> func4, Action<sequenceChildren_convertSequence> func5)
		{
			sequenceChildren_children sequenceChildren_children;
			if (this.Is_sequenceChildren_children(g, out sequenceChildren_children))
			{
				func0(sequenceChildren_children);
				return;
			}
			InsertAtAbs insertAtAbs;
			if (this.Is_InsertAtAbs(g, out insertAtAbs))
			{
				func1(insertAtAbs);
				return;
			}
			InsertAtRel insertAtRel;
			if (this.Is_InsertAtRel(g, out insertAtRel))
			{
				func2(insertAtRel);
				return;
			}
			DeleteChild deleteChild;
			if (this.Is_DeleteChild(g, out deleteChild))
			{
				func3(deleteChild);
				return;
			}
			ReplaceChildren replaceChildren;
			if (this.Is_ReplaceChildren(g, out replaceChildren))
			{
				func4(replaceChildren);
				return;
			}
			sequenceChildren_convertSequence sequenceChildren_convertSequence;
			if (this.Is_sequenceChildren_convertSequence(g, out sequenceChildren_convertSequence))
			{
				func5(sequenceChildren_convertSequence);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol sequenceChildren");
		}

		// Token: 0x060107CF RID: 67535 RVA: 0x0038C8E1 File Offset: 0x0038AAE1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060107D0 RID: 67536 RVA: 0x0038C8F4 File Offset: 0x0038AAF4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060107D1 RID: 67537 RVA: 0x0038C91E File Offset: 0x0038AB1E
		public bool Equals(sequenceChildren other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062C0 RID: 25280
		private ProgramNode _node;
	}
}
