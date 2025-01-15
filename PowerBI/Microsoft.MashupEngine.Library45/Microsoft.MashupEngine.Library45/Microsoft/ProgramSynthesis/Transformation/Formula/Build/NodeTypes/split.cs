using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015BE RID: 5566
	public struct split : IProgramNodeBuilder, IEquatable<split>
	{
		// Token: 0x17001FE4 RID: 8164
		// (get) Token: 0x0600B848 RID: 47176 RVA: 0x0027F0AA File Offset: 0x0027D2AA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B849 RID: 47177 RVA: 0x0027F0B2 File Offset: 0x0027D2B2
		private split(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B84A RID: 47178 RVA: 0x0027F0BB File Offset: 0x0027D2BB
		public static split CreateUnsafe(ProgramNode node)
		{
			return new split(node);
		}

		// Token: 0x0600B84B RID: 47179 RVA: 0x0027F0C4 File Offset: 0x0027D2C4
		public static split? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.split)
			{
				return null;
			}
			return new split?(split.CreateUnsafe(node));
		}

		// Token: 0x0600B84C RID: 47180 RVA: 0x0027F0FE File Offset: 0x0027D2FE
		public static split CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new split(new Hole(g.Symbol.split, holeId));
		}

		// Token: 0x0600B84D RID: 47181 RVA: 0x0027F116 File Offset: 0x0027D316
		public Split Cast_Split()
		{
			return Split.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B84E RID: 47182 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Split(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B84F RID: 47183 RVA: 0x0027F123 File Offset: 0x0027D323
		public bool Is_Split(GrammarBuilders g, out Split value)
		{
			value = Split.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B850 RID: 47184 RVA: 0x0027F137 File Offset: 0x0027D337
		public Split? As_Split(GrammarBuilders g)
		{
			return new Split?(Split.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B851 RID: 47185 RVA: 0x0027F149 File Offset: 0x0027D349
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B852 RID: 47186 RVA: 0x0027F15C File Offset: 0x0027D35C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B853 RID: 47187 RVA: 0x0027F186 File Offset: 0x0027D386
		public bool Equals(split other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400466C RID: 18028
		private ProgramNode _node;
	}
}
