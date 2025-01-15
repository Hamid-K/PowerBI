using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A3D RID: 6717
	public struct array : IProgramNodeBuilder, IEquatable<array>
	{
		// Token: 0x1700250C RID: 9484
		// (get) Token: 0x0600DD08 RID: 56584 RVA: 0x002F04D6 File Offset: 0x002EE6D6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DD09 RID: 56585 RVA: 0x002F04DE File Offset: 0x002EE6DE
		private array(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DD0A RID: 56586 RVA: 0x002F04E7 File Offset: 0x002EE6E7
		public static array CreateUnsafe(ProgramNode node)
		{
			return new array(node);
		}

		// Token: 0x0600DD0B RID: 56587 RVA: 0x002F04F0 File Offset: 0x002EE6F0
		public static array? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.array)
			{
				return null;
			}
			return new array?(array.CreateUnsafe(node));
		}

		// Token: 0x0600DD0C RID: 56588 RVA: 0x002F052A File Offset: 0x002EE72A
		public static array CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new array(new Hole(g.Symbol.array, holeId));
		}

		// Token: 0x0600DD0D RID: 56589 RVA: 0x002F0542 File Offset: 0x002EE742
		public bool Is_Array(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Array;
		}

		// Token: 0x0600DD0E RID: 56590 RVA: 0x002F055C File Offset: 0x002EE75C
		public bool Is_Array(GrammarBuilders g, out Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array value)
		{
			if (this.Node.GrammarRule == g.Rule.Array)
			{
				value = Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array);
			return false;
		}

		// Token: 0x0600DD0F RID: 56591 RVA: 0x002F0594 File Offset: 0x002EE794
		public Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array? As_Array(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Array)
			{
				return null;
			}
			return new Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array?(Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD10 RID: 56592 RVA: 0x002F05D4 File Offset: 0x002EE7D4
		public Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array Cast_Array(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Array)
			{
				return Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Array is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD11 RID: 56593 RVA: 0x002F0629 File Offset: 0x002EE829
		public bool Is_array_selectArray(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.array_selectArray;
		}

		// Token: 0x0600DD12 RID: 56594 RVA: 0x002F0643 File Offset: 0x002EE843
		public bool Is_array_selectArray(GrammarBuilders g, out array_selectArray value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.array_selectArray)
			{
				value = array_selectArray.CreateUnsafe(this.Node);
				return true;
			}
			value = default(array_selectArray);
			return false;
		}

		// Token: 0x0600DD13 RID: 56595 RVA: 0x002F0678 File Offset: 0x002EE878
		public array_selectArray? As_array_selectArray(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.array_selectArray)
			{
				return null;
			}
			return new array_selectArray?(array_selectArray.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD14 RID: 56596 RVA: 0x002F06B8 File Offset: 0x002EE8B8
		public array_selectArray Cast_array_selectArray(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.array_selectArray)
			{
				return array_selectArray.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_array_selectArray is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD15 RID: 56597 RVA: 0x002F0710 File Offset: 0x002EE910
		public T Switch<T>(GrammarBuilders g, Func<Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array, T> func0, Func<array_selectArray, T> func1)
		{
			Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array array;
			if (this.Is_Array(g, out array))
			{
				return func0(array);
			}
			array_selectArray array_selectArray;
			if (this.Is_array_selectArray(g, out array_selectArray))
			{
				return func1(array_selectArray);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol array");
		}

		// Token: 0x0600DD16 RID: 56598 RVA: 0x002F0768 File Offset: 0x002EE968
		public void Switch(GrammarBuilders g, Action<Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array> func0, Action<array_selectArray> func1)
		{
			Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes.Array array;
			if (this.Is_Array(g, out array))
			{
				func0(array);
				return;
			}
			array_selectArray array_selectArray;
			if (this.Is_array_selectArray(g, out array_selectArray))
			{
				func1(array_selectArray);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol array");
		}

		// Token: 0x0600DD17 RID: 56599 RVA: 0x002F07BF File Offset: 0x002EE9BF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DD18 RID: 56600 RVA: 0x002F07D4 File Offset: 0x002EE9D4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DD19 RID: 56601 RVA: 0x002F07FE File Offset: 0x002EE9FE
		public bool Equals(array other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400542E RID: 21550
		private ProgramNode _node;
	}
}
