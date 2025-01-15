using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A42 RID: 6722
	public struct selectOrTransformValue : IProgramNodeBuilder, IEquatable<selectOrTransformValue>
	{
		// Token: 0x17002511 RID: 9489
		// (get) Token: 0x0600DD5C RID: 56668 RVA: 0x002F12B6 File Offset: 0x002EF4B6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DD5D RID: 56669 RVA: 0x002F12BE File Offset: 0x002EF4BE
		private selectOrTransformValue(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DD5E RID: 56670 RVA: 0x002F12C7 File Offset: 0x002EF4C7
		public static selectOrTransformValue CreateUnsafe(ProgramNode node)
		{
			return new selectOrTransformValue(node);
		}

		// Token: 0x0600DD5F RID: 56671 RVA: 0x002F12D0 File Offset: 0x002EF4D0
		public static selectOrTransformValue? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selectOrTransformValue)
			{
				return null;
			}
			return new selectOrTransformValue?(selectOrTransformValue.CreateUnsafe(node));
		}

		// Token: 0x0600DD60 RID: 56672 RVA: 0x002F130A File Offset: 0x002EF50A
		public static selectOrTransformValue CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selectOrTransformValue(new Hole(g.Symbol.selectOrTransformValue, holeId));
		}

		// Token: 0x0600DD61 RID: 56673 RVA: 0x002F1322 File Offset: 0x002EF522
		public bool Is_selectOrTransformValue_selectValue(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.selectOrTransformValue_selectValue;
		}

		// Token: 0x0600DD62 RID: 56674 RVA: 0x002F133C File Offset: 0x002EF53C
		public bool Is_selectOrTransformValue_selectValue(GrammarBuilders g, out selectOrTransformValue_selectValue value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selectOrTransformValue_selectValue)
			{
				value = selectOrTransformValue_selectValue.CreateUnsafe(this.Node);
				return true;
			}
			value = default(selectOrTransformValue_selectValue);
			return false;
		}

		// Token: 0x0600DD63 RID: 56675 RVA: 0x002F1374 File Offset: 0x002EF574
		public selectOrTransformValue_selectValue? As_selectOrTransformValue_selectValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.selectOrTransformValue_selectValue)
			{
				return null;
			}
			return new selectOrTransformValue_selectValue?(selectOrTransformValue_selectValue.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD64 RID: 56676 RVA: 0x002F13B4 File Offset: 0x002EF5B4
		public selectOrTransformValue_selectValue Cast_selectOrTransformValue_selectValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selectOrTransformValue_selectValue)
			{
				return selectOrTransformValue_selectValue.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_selectOrTransformValue_selectValue is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD65 RID: 56677 RVA: 0x002F1409 File Offset: 0x002EF609
		public bool Is_selectOrTransformValue_transformValue(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.selectOrTransformValue_transformValue;
		}

		// Token: 0x0600DD66 RID: 56678 RVA: 0x002F1423 File Offset: 0x002EF623
		public bool Is_selectOrTransformValue_transformValue(GrammarBuilders g, out selectOrTransformValue_transformValue value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selectOrTransformValue_transformValue)
			{
				value = selectOrTransformValue_transformValue.CreateUnsafe(this.Node);
				return true;
			}
			value = default(selectOrTransformValue_transformValue);
			return false;
		}

		// Token: 0x0600DD67 RID: 56679 RVA: 0x002F1458 File Offset: 0x002EF658
		public selectOrTransformValue_transformValue? As_selectOrTransformValue_transformValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.selectOrTransformValue_transformValue)
			{
				return null;
			}
			return new selectOrTransformValue_transformValue?(selectOrTransformValue_transformValue.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD68 RID: 56680 RVA: 0x002F1498 File Offset: 0x002EF698
		public selectOrTransformValue_transformValue Cast_selectOrTransformValue_transformValue(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selectOrTransformValue_transformValue)
			{
				return selectOrTransformValue_transformValue.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_selectOrTransformValue_transformValue is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DD69 RID: 56681 RVA: 0x002F14F0 File Offset: 0x002EF6F0
		public T Switch<T>(GrammarBuilders g, Func<selectOrTransformValue_selectValue, T> func0, Func<selectOrTransformValue_transformValue, T> func1)
		{
			selectOrTransformValue_selectValue selectOrTransformValue_selectValue;
			if (this.Is_selectOrTransformValue_selectValue(g, out selectOrTransformValue_selectValue))
			{
				return func0(selectOrTransformValue_selectValue);
			}
			selectOrTransformValue_transformValue selectOrTransformValue_transformValue;
			if (this.Is_selectOrTransformValue_transformValue(g, out selectOrTransformValue_transformValue))
			{
				return func1(selectOrTransformValue_transformValue);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selectOrTransformValue");
		}

		// Token: 0x0600DD6A RID: 56682 RVA: 0x002F1548 File Offset: 0x002EF748
		public void Switch(GrammarBuilders g, Action<selectOrTransformValue_selectValue> func0, Action<selectOrTransformValue_transformValue> func1)
		{
			selectOrTransformValue_selectValue selectOrTransformValue_selectValue;
			if (this.Is_selectOrTransformValue_selectValue(g, out selectOrTransformValue_selectValue))
			{
				func0(selectOrTransformValue_selectValue);
				return;
			}
			selectOrTransformValue_transformValue selectOrTransformValue_transformValue;
			if (this.Is_selectOrTransformValue_transformValue(g, out selectOrTransformValue_transformValue))
			{
				func1(selectOrTransformValue_transformValue);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selectOrTransformValue");
		}

		// Token: 0x0600DD6B RID: 56683 RVA: 0x002F159F File Offset: 0x002EF79F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DD6C RID: 56684 RVA: 0x002F15B4 File Offset: 0x002EF7B4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DD6D RID: 56685 RVA: 0x002F15DE File Offset: 0x002EF7DE
		public bool Equals(selectOrTransformValue other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005433 RID: 21555
		private ProgramNode _node;
	}
}
