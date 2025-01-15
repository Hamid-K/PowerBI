using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015C6 RID: 5574
	public struct fromNumberCoalesced : IProgramNodeBuilder, IEquatable<fromNumberCoalesced>
	{
		// Token: 0x17001FEC RID: 8172
		// (get) Token: 0x0600B8CE RID: 47310 RVA: 0x0028044A File Offset: 0x0027E64A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B8CF RID: 47311 RVA: 0x00280452 File Offset: 0x0027E652
		private fromNumberCoalesced(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B8D0 RID: 47312 RVA: 0x0028045B File Offset: 0x0027E65B
		public static fromNumberCoalesced CreateUnsafe(ProgramNode node)
		{
			return new fromNumberCoalesced(node);
		}

		// Token: 0x0600B8D1 RID: 47313 RVA: 0x00280464 File Offset: 0x0027E664
		public static fromNumberCoalesced? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fromNumberCoalesced)
			{
				return null;
			}
			return new fromNumberCoalesced?(fromNumberCoalesced.CreateUnsafe(node));
		}

		// Token: 0x0600B8D2 RID: 47314 RVA: 0x0028049E File Offset: 0x0027E69E
		public static fromNumberCoalesced CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fromNumberCoalesced(new Hole(g.Symbol.fromNumberCoalesced, holeId));
		}

		// Token: 0x0600B8D3 RID: 47315 RVA: 0x002804B6 File Offset: 0x0027E6B6
		public FromNumberCoalesced Cast_FromNumberCoalesced()
		{
			return FromNumberCoalesced.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B8D4 RID: 47316 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_FromNumberCoalesced(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B8D5 RID: 47317 RVA: 0x002804C3 File Offset: 0x0027E6C3
		public bool Is_FromNumberCoalesced(GrammarBuilders g, out FromNumberCoalesced value)
		{
			value = FromNumberCoalesced.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B8D6 RID: 47318 RVA: 0x002804D7 File Offset: 0x0027E6D7
		public FromNumberCoalesced? As_FromNumberCoalesced(GrammarBuilders g)
		{
			return new FromNumberCoalesced?(FromNumberCoalesced.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B8D7 RID: 47319 RVA: 0x002804E9 File Offset: 0x0027E6E9
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B8D8 RID: 47320 RVA: 0x002804FC File Offset: 0x0027E6FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B8D9 RID: 47321 RVA: 0x00280526 File Offset: 0x0027E726
		public bool Equals(fromNumberCoalesced other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004674 RID: 18036
		private ProgramNode _node;
	}
}
