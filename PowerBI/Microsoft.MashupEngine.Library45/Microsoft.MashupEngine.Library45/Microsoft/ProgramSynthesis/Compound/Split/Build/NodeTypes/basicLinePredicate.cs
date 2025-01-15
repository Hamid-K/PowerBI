using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200096D RID: 2413
	public struct basicLinePredicate : IProgramNodeBuilder, IEquatable<basicLinePredicate>
	{
		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x0600395A RID: 14682 RVA: 0x000B1D86 File Offset: 0x000AFF86
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x000B1D8E File Offset: 0x000AFF8E
		private basicLinePredicate(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000B1D97 File Offset: 0x000AFF97
		public static basicLinePredicate CreateUnsafe(ProgramNode node)
		{
			return new basicLinePredicate(node);
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x000B1DA0 File Offset: 0x000AFFA0
		public static basicLinePredicate? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.basicLinePredicate)
			{
				return null;
			}
			return new basicLinePredicate?(basicLinePredicate.CreateUnsafe(node));
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000B1DDA File Offset: 0x000AFFDA
		public static basicLinePredicate CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new basicLinePredicate(new Hole(g.Symbol.basicLinePredicate, holeId));
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x000B1DF2 File Offset: 0x000AFFF2
		public StartsWith Cast_StartsWith()
		{
			return StartsWith.CreateUnsafe(this.Node);
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_StartsWith(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x000B1DFF File Offset: 0x000AFFFF
		public bool Is_StartsWith(GrammarBuilders g, out StartsWith value)
		{
			value = StartsWith.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x000B1E13 File Offset: 0x000B0013
		public StartsWith? As_StartsWith(GrammarBuilders g)
		{
			return new StartsWith?(StartsWith.CreateUnsafe(this.Node));
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x000B1E25 File Offset: 0x000B0025
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x000B1E38 File Offset: 0x000B0038
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x000B1E62 File Offset: 0x000B0062
		public bool Equals(basicLinePredicate other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A8D RID: 6797
		private ProgramNode _node;
	}
}
