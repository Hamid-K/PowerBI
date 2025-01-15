using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Tree.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E83 RID: 7811
	public struct sequenceMap : IProgramNodeBuilder, IEquatable<sequenceMap>
	{
		// Token: 0x17002BDC RID: 11228
		// (get) Token: 0x060107DE RID: 67550 RVA: 0x0038CA22 File Offset: 0x0038AC22
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060107DF RID: 67551 RVA: 0x0038CA2A File Offset: 0x0038AC2A
		private sequenceMap(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060107E0 RID: 67552 RVA: 0x0038CA33 File Offset: 0x0038AC33
		public static sequenceMap CreateUnsafe(ProgramNode node)
		{
			return new sequenceMap(node);
		}

		// Token: 0x060107E1 RID: 67553 RVA: 0x0038CA3C File Offset: 0x0038AC3C
		public static sequenceMap? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sequenceMap)
			{
				return null;
			}
			return new sequenceMap?(sequenceMap.CreateUnsafe(node));
		}

		// Token: 0x060107E2 RID: 67554 RVA: 0x0038CA76 File Offset: 0x0038AC76
		public static sequenceMap CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sequenceMap(new Hole(g.Symbol.sequenceMap, holeId));
		}

		// Token: 0x060107E3 RID: 67555 RVA: 0x0038CA8E File Offset: 0x0038AC8E
		public SequenceMap Cast_SequenceMap()
		{
			return SequenceMap.CreateUnsafe(this.Node);
		}

		// Token: 0x060107E4 RID: 67556 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SequenceMap(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x060107E5 RID: 67557 RVA: 0x0038CA9B File Offset: 0x0038AC9B
		public bool Is_SequenceMap(GrammarBuilders g, out SequenceMap value)
		{
			value = SequenceMap.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x060107E6 RID: 67558 RVA: 0x0038CAAF File Offset: 0x0038ACAF
		public SequenceMap? As_SequenceMap(GrammarBuilders g)
		{
			return new SequenceMap?(SequenceMap.CreateUnsafe(this.Node));
		}

		// Token: 0x060107E7 RID: 67559 RVA: 0x0038CAC1 File Offset: 0x0038ACC1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060107E8 RID: 67560 RVA: 0x0038CAD4 File Offset: 0x0038ACD4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060107E9 RID: 67561 RVA: 0x0038CAFE File Offset: 0x0038ACFE
		public bool Equals(sequenceMap other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062C2 RID: 25282
		private ProgramNode _node;
	}
}
