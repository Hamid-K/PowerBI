using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E7D RID: 7805
	public struct newDsl : IProgramNodeBuilder, IEquatable<newDsl>
	{
		// Token: 0x17002BD6 RID: 11222
		// (get) Token: 0x0601076C RID: 67436 RVA: 0x0038B752 File Offset: 0x00389952
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601076D RID: 67437 RVA: 0x0038B75A File Offset: 0x0038995A
		private newDsl(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601076E RID: 67438 RVA: 0x0038B763 File Offset: 0x00389963
		public static newDsl CreateUnsafe(ProgramNode node)
		{
			return new newDsl(node);
		}

		// Token: 0x0601076F RID: 67439 RVA: 0x0038B76C File Offset: 0x0038996C
		public static newDsl? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.newDsl)
			{
				return null;
			}
			return new newDsl?(newDsl.CreateUnsafe(node));
		}

		// Token: 0x06010770 RID: 67440 RVA: 0x0038B7A6 File Offset: 0x003899A6
		public static newDsl CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new newDsl(new Hole(g.Symbol.newDsl, holeId));
		}

		// Token: 0x06010771 RID: 67441 RVA: 0x0038B7BE File Offset: 0x003899BE
		public bool Is_newDsl_select(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.newDsl_select;
		}

		// Token: 0x06010772 RID: 67442 RVA: 0x0038B7D8 File Offset: 0x003899D8
		public bool Is_newDsl_select(GrammarBuilders g, out newDsl_select value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.newDsl_select)
			{
				value = newDsl_select.CreateUnsafe(this.Node);
				return true;
			}
			value = default(newDsl_select);
			return false;
		}

		// Token: 0x06010773 RID: 67443 RVA: 0x0038B810 File Offset: 0x00389A10
		public newDsl_select? As_newDsl_select(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.newDsl_select)
			{
				return null;
			}
			return new newDsl_select?(newDsl_select.CreateUnsafe(this.Node));
		}

		// Token: 0x06010774 RID: 67444 RVA: 0x0038B850 File Offset: 0x00389A50
		public newDsl_select Cast_newDsl_select(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.newDsl_select)
			{
				return newDsl_select.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_newDsl_select is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06010775 RID: 67445 RVA: 0x0038B8A5 File Offset: 0x00389AA5
		public bool Is_newDsl_construction(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.newDsl_construction;
		}

		// Token: 0x06010776 RID: 67446 RVA: 0x0038B8BF File Offset: 0x00389ABF
		public bool Is_newDsl_construction(GrammarBuilders g, out newDsl_construction value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.newDsl_construction)
			{
				value = newDsl_construction.CreateUnsafe(this.Node);
				return true;
			}
			value = default(newDsl_construction);
			return false;
		}

		// Token: 0x06010777 RID: 67447 RVA: 0x0038B8F4 File Offset: 0x00389AF4
		public newDsl_construction? As_newDsl_construction(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.newDsl_construction)
			{
				return null;
			}
			return new newDsl_construction?(newDsl_construction.CreateUnsafe(this.Node));
		}

		// Token: 0x06010778 RID: 67448 RVA: 0x0038B934 File Offset: 0x00389B34
		public newDsl_construction Cast_newDsl_construction(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.newDsl_construction)
			{
				return newDsl_construction.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_newDsl_construction is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06010779 RID: 67449 RVA: 0x0038B98C File Offset: 0x00389B8C
		public T Switch<T>(GrammarBuilders g, Func<newDsl_select, T> func0, Func<newDsl_construction, T> func1)
		{
			newDsl_select newDsl_select;
			if (this.Is_newDsl_select(g, out newDsl_select))
			{
				return func0(newDsl_select);
			}
			newDsl_construction newDsl_construction;
			if (this.Is_newDsl_construction(g, out newDsl_construction))
			{
				return func1(newDsl_construction);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol newDsl");
		}

		// Token: 0x0601077A RID: 67450 RVA: 0x0038B9E4 File Offset: 0x00389BE4
		public void Switch(GrammarBuilders g, Action<newDsl_select> func0, Action<newDsl_construction> func1)
		{
			newDsl_select newDsl_select;
			if (this.Is_newDsl_select(g, out newDsl_select))
			{
				func0(newDsl_select);
				return;
			}
			newDsl_construction newDsl_construction;
			if (this.Is_newDsl_construction(g, out newDsl_construction))
			{
				func1(newDsl_construction);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol newDsl");
		}

		// Token: 0x0601077B RID: 67451 RVA: 0x0038BA3B File Offset: 0x00389C3B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601077C RID: 67452 RVA: 0x0038BA50 File Offset: 0x00389C50
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601077D RID: 67453 RVA: 0x0038BA7A File Offset: 0x00389C7A
		public bool Equals(newDsl other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062BC RID: 25276
		private ProgramNode _node;
	}
}
