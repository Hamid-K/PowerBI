using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C66 RID: 7270
	public struct maxTrailingZeros : IProgramNodeBuilder, IEquatable<maxTrailingZeros>
	{
		// Token: 0x17002904 RID: 10500
		// (get) Token: 0x0600F634 RID: 63028 RVA: 0x00348E4A File Offset: 0x0034704A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F635 RID: 63029 RVA: 0x00348E52 File Offset: 0x00347052
		private maxTrailingZeros(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F636 RID: 63030 RVA: 0x00348E5B File Offset: 0x0034705B
		public static maxTrailingZeros CreateUnsafe(ProgramNode node)
		{
			return new maxTrailingZeros(node);
		}

		// Token: 0x0600F637 RID: 63031 RVA: 0x00348E64 File Offset: 0x00347064
		public static maxTrailingZeros? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.maxTrailingZeros)
			{
				return null;
			}
			return new maxTrailingZeros?(maxTrailingZeros.CreateUnsafe(node));
		}

		// Token: 0x0600F638 RID: 63032 RVA: 0x00348E9E File Offset: 0x0034709E
		public static maxTrailingZeros CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new maxTrailingZeros(new Hole(g.Symbol.maxTrailingZeros, holeId));
		}

		// Token: 0x0600F639 RID: 63033 RVA: 0x00348EB6 File Offset: 0x003470B6
		public maxTrailingZeros(GrammarBuilders g, uint? value)
		{
			this = new maxTrailingZeros(new LiteralNode(g.Symbol.maxTrailingZeros, value));
		}

		// Token: 0x17002905 RID: 10501
		// (get) Token: 0x0600F63A RID: 63034 RVA: 0x00348ED4 File Offset: 0x003470D4
		public uint? Value
		{
			get
			{
				return (uint?)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F63B RID: 63035 RVA: 0x00348EEB File Offset: 0x003470EB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F63C RID: 63036 RVA: 0x00348F00 File Offset: 0x00347100
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F63D RID: 63037 RVA: 0x00348F2A File Offset: 0x0034712A
		public bool Equals(maxTrailingZeros other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B55 RID: 23381
		private ProgramNode _node;
	}
}
