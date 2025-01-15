using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015C0 RID: 5568
	public struct slice : IProgramNodeBuilder, IEquatable<slice>
	{
		// Token: 0x17001FE6 RID: 8166
		// (get) Token: 0x0600B86A RID: 47210 RVA: 0x0027F5E2 File Offset: 0x0027D7E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B86B RID: 47211 RVA: 0x0027F5EA File Offset: 0x0027D7EA
		private slice(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B86C RID: 47212 RVA: 0x0027F5F3 File Offset: 0x0027D7F3
		public static slice CreateUnsafe(ProgramNode node)
		{
			return new slice(node);
		}

		// Token: 0x0600B86D RID: 47213 RVA: 0x0027F5FC File Offset: 0x0027D7FC
		public static slice? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.slice)
			{
				return null;
			}
			return new slice?(slice.CreateUnsafe(node));
		}

		// Token: 0x0600B86E RID: 47214 RVA: 0x0027F636 File Offset: 0x0027D836
		public static slice CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new slice(new Hole(g.Symbol.slice, holeId));
		}

		// Token: 0x0600B86F RID: 47215 RVA: 0x0027F64E File Offset: 0x0027D84E
		public Slice Cast_Slice()
		{
			return Slice.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B870 RID: 47216 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Slice(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B871 RID: 47217 RVA: 0x0027F65B File Offset: 0x0027D85B
		public bool Is_Slice(GrammarBuilders g, out Slice value)
		{
			value = Slice.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B872 RID: 47218 RVA: 0x0027F66F File Offset: 0x0027D86F
		public Slice? As_Slice(GrammarBuilders g)
		{
			return new Slice?(Slice.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B873 RID: 47219 RVA: 0x0027F681 File Offset: 0x0027D881
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B874 RID: 47220 RVA: 0x0027F694 File Offset: 0x0027D894
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B875 RID: 47221 RVA: 0x0027F6BE File Offset: 0x0027D8BE
		public bool Equals(slice other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400466E RID: 18030
		private ProgramNode _node;
	}
}
