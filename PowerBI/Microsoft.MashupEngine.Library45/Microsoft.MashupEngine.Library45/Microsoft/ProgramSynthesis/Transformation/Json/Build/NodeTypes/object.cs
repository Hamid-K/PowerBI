using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A3C RID: 6716
	public struct @object : IProgramNodeBuilder, IEquatable<@object>
	{
		// Token: 0x1700250B RID: 9483
		// (get) Token: 0x0600DCEE RID: 56558 RVA: 0x002EFF82 File Offset: 0x002EE182
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DCEF RID: 56559 RVA: 0x002EFF8A File Offset: 0x002EE18A
		private @object(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DCF0 RID: 56560 RVA: 0x002EFF93 File Offset: 0x002EE193
		public static @object CreateUnsafe(ProgramNode node)
		{
			return new @object(node);
		}

		// Token: 0x0600DCF1 RID: 56561 RVA: 0x002EFF9C File Offset: 0x002EE19C
		public static @object? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.@object)
			{
				return null;
			}
			return new @object?(@object.CreateUnsafe(node));
		}

		// Token: 0x0600DCF2 RID: 56562 RVA: 0x002EFFD6 File Offset: 0x002EE1D6
		public static @object CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new @object(new Hole(g.Symbol.@object, holeId));
		}

		// Token: 0x0600DCF3 RID: 56563 RVA: 0x002EFFEE File Offset: 0x002EE1EE
		public bool Is_Object(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Object;
		}

		// Token: 0x0600DCF4 RID: 56564 RVA: 0x002F0008 File Offset: 0x002EE208
		public bool Is_Object(GrammarBuilders g, out Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object value)
		{
			if (this.Node.GrammarRule == g.Rule.Object)
			{
				value = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object);
			return false;
		}

		// Token: 0x0600DCF5 RID: 56565 RVA: 0x002F0040 File Offset: 0x002EE240
		public Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object? As_Object(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Object)
			{
				return null;
			}
			return new Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object?(Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DCF6 RID: 56566 RVA: 0x002F0080 File Offset: 0x002EE280
		public Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object Cast_Object(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Object)
			{
				return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Object is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DCF7 RID: 56567 RVA: 0x002F00D5 File Offset: 0x002EE2D5
		public bool Is_Append(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Append;
		}

		// Token: 0x0600DCF8 RID: 56568 RVA: 0x002F00EF File Offset: 0x002EE2EF
		public bool Is_Append(GrammarBuilders g, out Append value)
		{
			if (this.Node.GrammarRule == g.Rule.Append)
			{
				value = Append.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Append);
			return false;
		}

		// Token: 0x0600DCF9 RID: 56569 RVA: 0x002F0124 File Offset: 0x002EE324
		public Append? As_Append(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Append)
			{
				return null;
			}
			return new Append?(Append.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DCFA RID: 56570 RVA: 0x002F0164 File Offset: 0x002EE364
		public Append Cast_Append(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Append)
			{
				return Append.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Append is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DCFB RID: 56571 RVA: 0x002F01B9 File Offset: 0x002EE3B9
		public bool Is_SelectObject(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SelectObject;
		}

		// Token: 0x0600DCFC RID: 56572 RVA: 0x002F01D3 File Offset: 0x002EE3D3
		public bool Is_SelectObject(GrammarBuilders g, out SelectObject value)
		{
			if (this.Node.GrammarRule == g.Rule.SelectObject)
			{
				value = SelectObject.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SelectObject);
			return false;
		}

		// Token: 0x0600DCFD RID: 56573 RVA: 0x002F0208 File Offset: 0x002EE408
		public SelectObject? As_SelectObject(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SelectObject)
			{
				return null;
			}
			return new SelectObject?(SelectObject.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DCFE RID: 56574 RVA: 0x002F0248 File Offset: 0x002EE448
		public SelectObject Cast_SelectObject(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SelectObject)
			{
				return SelectObject.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SelectObject is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DCFF RID: 56575 RVA: 0x002F029D File Offset: 0x002EE49D
		public bool Is_FlattenObject(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FlattenObject;
		}

		// Token: 0x0600DD00 RID: 56576 RVA: 0x002F02B7 File Offset: 0x002EE4B7
		public bool Is_FlattenObject(GrammarBuilders g, out FlattenObject value)
		{
			if (this.Node.GrammarRule == g.Rule.FlattenObject)
			{
				value = FlattenObject.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FlattenObject);
			return false;
		}

		// Token: 0x0600DD01 RID: 56577 RVA: 0x002F02EC File Offset: 0x002EE4EC
		public FlattenObject? As_FlattenObject(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FlattenObject)
			{
				return null;
			}
			return new FlattenObject?(FlattenObject.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD02 RID: 56578 RVA: 0x002F032C File Offset: 0x002EE52C
		public FlattenObject Cast_FlattenObject(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FlattenObject)
			{
				return FlattenObject.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FlattenObject is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD03 RID: 56579 RVA: 0x002F0384 File Offset: 0x002EE584
		public T Switch<T>(GrammarBuilders g, Func<Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object, T> func0, Func<Append, T> func1, Func<SelectObject, T> func2, Func<FlattenObject, T> func3)
		{
			Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object @object;
			if (this.Is_Object(g, out @object))
			{
				return func0(@object);
			}
			Append append;
			if (this.Is_Append(g, out append))
			{
				return func1(append);
			}
			SelectObject selectObject;
			if (this.Is_SelectObject(g, out selectObject))
			{
				return func2(selectObject);
			}
			FlattenObject flattenObject;
			if (this.Is_FlattenObject(g, out flattenObject))
			{
				return func3(flattenObject);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol object");
		}

		// Token: 0x0600DD04 RID: 56580 RVA: 0x002F0404 File Offset: 0x002EE604
		public void Switch(GrammarBuilders g, Action<Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object> func0, Action<Append> func1, Action<SelectObject> func2, Action<FlattenObject> func3)
		{
			Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Object @object;
			if (this.Is_Object(g, out @object))
			{
				func0(@object);
				return;
			}
			Append append;
			if (this.Is_Append(g, out append))
			{
				func1(append);
				return;
			}
			SelectObject selectObject;
			if (this.Is_SelectObject(g, out selectObject))
			{
				func2(selectObject);
				return;
			}
			FlattenObject flattenObject;
			if (this.Is_FlattenObject(g, out flattenObject))
			{
				func3(flattenObject);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol object");
		}

		// Token: 0x0600DD05 RID: 56581 RVA: 0x002F0483 File Offset: 0x002EE683
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DD06 RID: 56582 RVA: 0x002F0498 File Offset: 0x002EE698
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DD07 RID: 56583 RVA: 0x002F04C2 File Offset: 0x002EE6C2
		public bool Equals(@object other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400542D RID: 21549
		private ProgramNode _node;
	}
}
