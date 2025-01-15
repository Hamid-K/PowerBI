using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015B9 RID: 5561
	public struct itime : IProgramNodeBuilder, IEquatable<itime>
	{
		// Token: 0x17001FDF RID: 8159
		// (get) Token: 0x0600B7E2 RID: 47074 RVA: 0x0027DEC6 File Offset: 0x0027C0C6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B7E3 RID: 47075 RVA: 0x0027DECE File Offset: 0x0027C0CE
		private itime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B7E4 RID: 47076 RVA: 0x0027DED7 File Offset: 0x0027C0D7
		public static itime CreateUnsafe(ProgramNode node)
		{
			return new itime(node);
		}

		// Token: 0x0600B7E5 RID: 47077 RVA: 0x0027DEE0 File Offset: 0x0027C0E0
		public static itime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.itime)
			{
				return null;
			}
			return new itime?(itime.CreateUnsafe(node));
		}

		// Token: 0x0600B7E6 RID: 47078 RVA: 0x0027DF1A File Offset: 0x0027C11A
		public static itime CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new itime(new Hole(g.Symbol.itime, holeId));
		}

		// Token: 0x0600B7E7 RID: 47079 RVA: 0x0027DF32 File Offset: 0x0027C132
		public itime_fromTime Cast_itime_fromTime()
		{
			return itime_fromTime.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B7E8 RID: 47080 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_itime_fromTime(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B7E9 RID: 47081 RVA: 0x0027DF3F File Offset: 0x0027C13F
		public bool Is_itime_fromTime(GrammarBuilders g, out itime_fromTime value)
		{
			value = itime_fromTime.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B7EA RID: 47082 RVA: 0x0027DF53 File Offset: 0x0027C153
		public itime_fromTime? As_itime_fromTime(GrammarBuilders g)
		{
			return new itime_fromTime?(itime_fromTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B7EB RID: 47083 RVA: 0x0027DF65 File Offset: 0x0027C165
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B7EC RID: 47084 RVA: 0x0027DF78 File Offset: 0x0027C178
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B7ED RID: 47085 RVA: 0x0027DFA2 File Offset: 0x0027C1A2
		public bool Equals(itime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004667 RID: 18023
		private ProgramNode _node;
	}
}
