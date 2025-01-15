using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A3F RID: 6719
	public struct elements : IProgramNodeBuilder, IEquatable<elements>
	{
		// Token: 0x1700250E RID: 9486
		// (get) Token: 0x0600DD2C RID: 56620 RVA: 0x002F0B4E File Offset: 0x002EED4E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DD2D RID: 56621 RVA: 0x002F0B56 File Offset: 0x002EED56
		private elements(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DD2E RID: 56622 RVA: 0x002F0B5F File Offset: 0x002EED5F
		public static elements CreateUnsafe(ProgramNode node)
		{
			return new elements(node);
		}

		// Token: 0x0600DD2F RID: 56623 RVA: 0x002F0B68 File Offset: 0x002EED68
		public static elements? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.elements)
			{
				return null;
			}
			return new elements?(elements.CreateUnsafe(node));
		}

		// Token: 0x0600DD30 RID: 56624 RVA: 0x002F0BA2 File Offset: 0x002EEDA2
		public static elements CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new elements(new Hole(g.Symbol.elements, holeId));
		}

		// Token: 0x0600DD31 RID: 56625 RVA: 0x002F0BBA File Offset: 0x002EEDBA
		public bool Is_Transform(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Transform;
		}

		// Token: 0x0600DD32 RID: 56626 RVA: 0x002F0BD4 File Offset: 0x002EEDD4
		public bool Is_Transform(GrammarBuilders g, out Transform value)
		{
			if (this.Node.GrammarRule == g.Rule.Transform)
			{
				value = Transform.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Transform);
			return false;
		}

		// Token: 0x0600DD33 RID: 56627 RVA: 0x002F0C0C File Offset: 0x002EEE0C
		public Transform? As_Transform(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Transform)
			{
				return null;
			}
			return new Transform?(Transform.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD34 RID: 56628 RVA: 0x002F0C4C File Offset: 0x002EEE4C
		public Transform Cast_Transform(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Transform)
			{
				return Transform.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Transform is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD35 RID: 56629 RVA: 0x002F0CA1 File Offset: 0x002EEEA1
		public bool Is_TransformFlatten(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TransformFlatten;
		}

		// Token: 0x0600DD36 RID: 56630 RVA: 0x002F0CBB File Offset: 0x002EEEBB
		public bool Is_TransformFlatten(GrammarBuilders g, out TransformFlatten value)
		{
			if (this.Node.GrammarRule == g.Rule.TransformFlatten)
			{
				value = TransformFlatten.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TransformFlatten);
			return false;
		}

		// Token: 0x0600DD37 RID: 56631 RVA: 0x002F0CF0 File Offset: 0x002EEEF0
		public TransformFlatten? As_TransformFlatten(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TransformFlatten)
			{
				return null;
			}
			return new TransformFlatten?(TransformFlatten.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD38 RID: 56632 RVA: 0x002F0D30 File Offset: 0x002EEF30
		public TransformFlatten Cast_TransformFlatten(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TransformFlatten)
			{
				return TransformFlatten.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TransformFlatten is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD39 RID: 56633 RVA: 0x002F0D88 File Offset: 0x002EEF88
		public T Switch<T>(GrammarBuilders g, Func<Transform, T> func0, Func<TransformFlatten, T> func1)
		{
			Transform transform;
			if (this.Is_Transform(g, out transform))
			{
				return func0(transform);
			}
			TransformFlatten transformFlatten;
			if (this.Is_TransformFlatten(g, out transformFlatten))
			{
				return func1(transformFlatten);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol elements");
		}

		// Token: 0x0600DD3A RID: 56634 RVA: 0x002F0DE0 File Offset: 0x002EEFE0
		public void Switch(GrammarBuilders g, Action<Transform> func0, Action<TransformFlatten> func1)
		{
			Transform transform;
			if (this.Is_Transform(g, out transform))
			{
				func0(transform);
				return;
			}
			TransformFlatten transformFlatten;
			if (this.Is_TransformFlatten(g, out transformFlatten))
			{
				func1(transformFlatten);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol elements");
		}

		// Token: 0x0600DD3B RID: 56635 RVA: 0x002F0E37 File Offset: 0x002EF037
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DD3C RID: 56636 RVA: 0x002F0E4C File Offset: 0x002EF04C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DD3D RID: 56637 RVA: 0x002F0E76 File Offset: 0x002EF076
		public bool Equals(elements other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005430 RID: 21552
		private ProgramNode _node;
	}
}
