using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C54 RID: 7252
	public struct y : IProgramNodeBuilder, IEquatable<y>
	{
		// Token: 0x170028EA RID: 10474
		// (get) Token: 0x0600F560 RID: 62816 RVA: 0x003478CA File Offset: 0x00345ACA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F561 RID: 62817 RVA: 0x003478D2 File Offset: 0x00345AD2
		private y(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F562 RID: 62818 RVA: 0x003478DB File Offset: 0x00345ADB
		public static y CreateUnsafe(ProgramNode node)
		{
			return new y(node);
		}

		// Token: 0x0600F563 RID: 62819 RVA: 0x003478E4 File Offset: 0x00345AE4
		public static y? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.y)
			{
				return null;
			}
			return new y?(y.CreateUnsafe(node));
		}

		// Token: 0x0600F564 RID: 62820 RVA: 0x0034791E File Offset: 0x00345B1E
		public static y CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new y(new Hole(g.Symbol.y, holeId));
		}

		// Token: 0x0600F565 RID: 62821 RVA: 0x00347936 File Offset: 0x00345B36
		public bool Is_SelectInput(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SelectInput;
		}

		// Token: 0x0600F566 RID: 62822 RVA: 0x00347950 File Offset: 0x00345B50
		public bool Is_SelectInput(GrammarBuilders g, out SelectInput value)
		{
			if (this.Node.GrammarRule == g.Rule.SelectInput)
			{
				value = SelectInput.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SelectInput);
			return false;
		}

		// Token: 0x0600F567 RID: 62823 RVA: 0x00347988 File Offset: 0x00345B88
		public SelectInput? As_SelectInput(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SelectInput)
			{
				return null;
			}
			return new SelectInput?(SelectInput.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F568 RID: 62824 RVA: 0x003479C8 File Offset: 0x00345BC8
		public SelectInput Cast_SelectInput(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SelectInput)
			{
				return SelectInput.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SelectInput is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F569 RID: 62825 RVA: 0x00347A1D File Offset: 0x00345C1D
		public bool Is_SelectIndexedInput(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SelectIndexedInput;
		}

		// Token: 0x0600F56A RID: 62826 RVA: 0x00347A37 File Offset: 0x00345C37
		public bool Is_SelectIndexedInput(GrammarBuilders g, out SelectIndexedInput value)
		{
			if (this.Node.GrammarRule == g.Rule.SelectIndexedInput)
			{
				value = SelectIndexedInput.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SelectIndexedInput);
			return false;
		}

		// Token: 0x0600F56B RID: 62827 RVA: 0x00347A6C File Offset: 0x00345C6C
		public SelectIndexedInput? As_SelectIndexedInput(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SelectIndexedInput)
			{
				return null;
			}
			return new SelectIndexedInput?(SelectIndexedInput.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F56C RID: 62828 RVA: 0x00347AAC File Offset: 0x00345CAC
		public SelectIndexedInput Cast_SelectIndexedInput(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SelectIndexedInput)
			{
				return SelectIndexedInput.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SelectIndexedInput is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F56D RID: 62829 RVA: 0x00347B04 File Offset: 0x00345D04
		public T Switch<T>(GrammarBuilders g, Func<SelectInput, T> func0, Func<SelectIndexedInput, T> func1)
		{
			SelectInput selectInput;
			if (this.Is_SelectInput(g, out selectInput))
			{
				return func0(selectInput);
			}
			SelectIndexedInput selectIndexedInput;
			if (this.Is_SelectIndexedInput(g, out selectIndexedInput))
			{
				return func1(selectIndexedInput);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol y");
		}

		// Token: 0x0600F56E RID: 62830 RVA: 0x00347B5C File Offset: 0x00345D5C
		public void Switch(GrammarBuilders g, Action<SelectInput> func0, Action<SelectIndexedInput> func1)
		{
			SelectInput selectInput;
			if (this.Is_SelectInput(g, out selectInput))
			{
				func0(selectInput);
				return;
			}
			SelectIndexedInput selectIndexedInput;
			if (this.Is_SelectIndexedInput(g, out selectIndexedInput))
			{
				func1(selectIndexedInput);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol y");
		}

		// Token: 0x0600F56F RID: 62831 RVA: 0x00347BB3 File Offset: 0x00345DB3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F570 RID: 62832 RVA: 0x00347BC8 File Offset: 0x00345DC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F571 RID: 62833 RVA: 0x00347BF2 File Offset: 0x00345DF2
		public bool Equals(y other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B43 RID: 23363
		private ProgramNode _node;
	}
}
