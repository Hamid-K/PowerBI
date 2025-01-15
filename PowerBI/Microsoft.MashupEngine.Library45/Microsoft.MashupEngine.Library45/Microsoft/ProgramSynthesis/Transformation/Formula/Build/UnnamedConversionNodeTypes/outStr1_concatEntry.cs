using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x0200151E RID: 5406
	public struct outStr1_concatEntry : IProgramNodeBuilder, IEquatable<outStr1_concatEntry>
	{
		// Token: 0x17001E82 RID: 7810
		// (get) Token: 0x0600B023 RID: 45091 RVA: 0x0026EA22 File Offset: 0x0026CC22
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B024 RID: 45092 RVA: 0x0026EA2A File Offset: 0x0026CC2A
		private outStr1_concatEntry(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B025 RID: 45093 RVA: 0x0026EA33 File Offset: 0x0026CC33
		public static outStr1_concatEntry CreateUnsafe(ProgramNode node)
		{
			return new outStr1_concatEntry(node);
		}

		// Token: 0x0600B026 RID: 45094 RVA: 0x0026EA3C File Offset: 0x0026CC3C
		public static outStr1_concatEntry? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.outStr1_concatEntry)
			{
				return null;
			}
			return new outStr1_concatEntry?(outStr1_concatEntry.CreateUnsafe(node));
		}

		// Token: 0x0600B027 RID: 45095 RVA: 0x0026EA71 File Offset: 0x0026CC71
		public outStr1_concatEntry(GrammarBuilders g, concatEntry value0)
		{
			this._node = g.UnnamedConversion.outStr1_concatEntry.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B028 RID: 45096 RVA: 0x0026EA90 File Offset: 0x0026CC90
		public static implicit operator outStr1(outStr1_concatEntry arg)
		{
			return outStr1.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001E83 RID: 7811
		// (get) Token: 0x0600B029 RID: 45097 RVA: 0x0026EA9E File Offset: 0x0026CC9E
		public concatEntry concatEntry
		{
			get
			{
				return concatEntry.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B02A RID: 45098 RVA: 0x0026EAB2 File Offset: 0x0026CCB2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B02B RID: 45099 RVA: 0x0026EAC8 File Offset: 0x0026CCC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B02C RID: 45100 RVA: 0x0026EAF2 File Offset: 0x0026CCF2
		public bool Equals(outStr1_concatEntry other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045CC RID: 17868
		private ProgramNode _node;
	}
}
