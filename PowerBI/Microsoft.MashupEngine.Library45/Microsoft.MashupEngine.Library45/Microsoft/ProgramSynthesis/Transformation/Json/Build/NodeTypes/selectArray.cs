using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A47 RID: 6727
	public struct selectArray : IProgramNodeBuilder, IEquatable<selectArray>
	{
		// Token: 0x17002516 RID: 9494
		// (get) Token: 0x0600DDA8 RID: 56744 RVA: 0x002F1D0A File Offset: 0x002EFF0A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DDA9 RID: 56745 RVA: 0x002F1D12 File Offset: 0x002EFF12
		private selectArray(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DDAA RID: 56746 RVA: 0x002F1D1B File Offset: 0x002EFF1B
		public static selectArray CreateUnsafe(ProgramNode node)
		{
			return new selectArray(node);
		}

		// Token: 0x0600DDAB RID: 56747 RVA: 0x002F1D24 File Offset: 0x002EFF24
		public static selectArray? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selectArray)
			{
				return null;
			}
			return new selectArray?(selectArray.CreateUnsafe(node));
		}

		// Token: 0x0600DDAC RID: 56748 RVA: 0x002F1D5E File Offset: 0x002EFF5E
		public static selectArray CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selectArray(new Hole(g.Symbol.selectArray, holeId));
		}

		// Token: 0x0600DDAD RID: 56749 RVA: 0x002F1D76 File Offset: 0x002EFF76
		public bool Is_SelectArray(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SelectArray;
		}

		// Token: 0x0600DDAE RID: 56750 RVA: 0x002F1D90 File Offset: 0x002EFF90
		public bool Is_SelectArray(GrammarBuilders g, out SelectArray value)
		{
			if (this.Node.GrammarRule == g.Rule.SelectArray)
			{
				value = SelectArray.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SelectArray);
			return false;
		}

		// Token: 0x0600DDAF RID: 56751 RVA: 0x002F1DC8 File Offset: 0x002EFFC8
		public SelectArray? As_SelectArray(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SelectArray)
			{
				return null;
			}
			return new SelectArray?(SelectArray.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DDB0 RID: 56752 RVA: 0x002F1E08 File Offset: 0x002F0008
		public SelectArray Cast_SelectArray(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SelectArray)
			{
				return SelectArray.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SelectArray is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DDB1 RID: 56753 RVA: 0x002F1E5D File Offset: 0x002F005D
		public bool Is_ToArray(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ToArray;
		}

		// Token: 0x0600DDB2 RID: 56754 RVA: 0x002F1E77 File Offset: 0x002F0077
		public bool Is_ToArray(GrammarBuilders g, out ToArray value)
		{
			if (this.Node.GrammarRule == g.Rule.ToArray)
			{
				value = ToArray.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ToArray);
			return false;
		}

		// Token: 0x0600DDB3 RID: 56755 RVA: 0x002F1EAC File Offset: 0x002F00AC
		public ToArray? As_ToArray(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ToArray)
			{
				return null;
			}
			return new ToArray?(ToArray.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DDB4 RID: 56756 RVA: 0x002F1EEC File Offset: 0x002F00EC
		public ToArray Cast_ToArray(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ToArray)
			{
				return ToArray.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ToArray is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600DDB5 RID: 56757 RVA: 0x002F1F44 File Offset: 0x002F0144
		public T Switch<T>(GrammarBuilders g, Func<SelectArray, T> func0, Func<ToArray, T> func1)
		{
			SelectArray selectArray;
			if (this.Is_SelectArray(g, out selectArray))
			{
				return func0(selectArray);
			}
			ToArray toArray;
			if (this.Is_ToArray(g, out toArray))
			{
				return func1(toArray);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selectArray");
		}

		// Token: 0x0600DDB6 RID: 56758 RVA: 0x002F1F9C File Offset: 0x002F019C
		public void Switch(GrammarBuilders g, Action<SelectArray> func0, Action<ToArray> func1)
		{
			SelectArray selectArray;
			if (this.Is_SelectArray(g, out selectArray))
			{
				func0(selectArray);
				return;
			}
			ToArray toArray;
			if (this.Is_ToArray(g, out toArray))
			{
				func1(toArray);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selectArray");
		}

		// Token: 0x0600DDB7 RID: 56759 RVA: 0x002F1FF3 File Offset: 0x002F01F3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DDB8 RID: 56760 RVA: 0x002F2008 File Offset: 0x002F0208
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DDB9 RID: 56761 RVA: 0x002F2032 File Offset: 0x002F0232
		public bool Equals(selectArray other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005438 RID: 21560
		private ProgramNode _node;
	}
}
