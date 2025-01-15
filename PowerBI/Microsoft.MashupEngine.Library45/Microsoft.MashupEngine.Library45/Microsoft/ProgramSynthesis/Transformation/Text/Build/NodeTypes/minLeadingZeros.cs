using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C68 RID: 7272
	public struct minLeadingZeros : IProgramNodeBuilder, IEquatable<minLeadingZeros>
	{
		// Token: 0x17002908 RID: 10504
		// (get) Token: 0x0600F648 RID: 63048 RVA: 0x00349032 File Offset: 0x00347232
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F649 RID: 63049 RVA: 0x0034903A File Offset: 0x0034723A
		private minLeadingZeros(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F64A RID: 63050 RVA: 0x00349043 File Offset: 0x00347243
		public static minLeadingZeros CreateUnsafe(ProgramNode node)
		{
			return new minLeadingZeros(node);
		}

		// Token: 0x0600F64B RID: 63051 RVA: 0x0034904C File Offset: 0x0034724C
		public static minLeadingZeros? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.minLeadingZeros)
			{
				return null;
			}
			return new minLeadingZeros?(minLeadingZeros.CreateUnsafe(node));
		}

		// Token: 0x0600F64C RID: 63052 RVA: 0x00349086 File Offset: 0x00347286
		public static minLeadingZeros CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new minLeadingZeros(new Hole(g.Symbol.minLeadingZeros, holeId));
		}

		// Token: 0x0600F64D RID: 63053 RVA: 0x0034909E File Offset: 0x0034729E
		public minLeadingZeros(GrammarBuilders g, uint? value)
		{
			this = new minLeadingZeros(new LiteralNode(g.Symbol.minLeadingZeros, value));
		}

		// Token: 0x17002909 RID: 10505
		// (get) Token: 0x0600F64E RID: 63054 RVA: 0x003490BC File Offset: 0x003472BC
		public uint? Value
		{
			get
			{
				return (uint?)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F64F RID: 63055 RVA: 0x003490D3 File Offset: 0x003472D3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F650 RID: 63056 RVA: 0x003490E8 File Offset: 0x003472E8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F651 RID: 63057 RVA: 0x00349112 File Offset: 0x00347312
		public bool Equals(minLeadingZeros other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B57 RID: 23383
		private ProgramNode _node;
	}
}
