using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000C09 RID: 3081
	public struct tolerance : IProgramNodeBuilder, IEquatable<tolerance>
	{
		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06004F9D RID: 20381 RVA: 0x000FAFB6 File Offset: 0x000F91B6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x000FAFBE File Offset: 0x000F91BE
		private tolerance(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x000FAFC7 File Offset: 0x000F91C7
		public static tolerance CreateUnsafe(ProgramNode node)
		{
			return new tolerance(node);
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x000FAFD0 File Offset: 0x000F91D0
		public static tolerance? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.tolerance)
			{
				return null;
			}
			return new tolerance?(tolerance.CreateUnsafe(node));
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x000FB00A File Offset: 0x000F920A
		public static tolerance CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new tolerance(new Hole(g.Symbol.tolerance, holeId));
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x000FB022 File Offset: 0x000F9222
		public tolerance(GrammarBuilders g, int value)
		{
			this = new tolerance(new LiteralNode(g.Symbol.tolerance, value));
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06004FA3 RID: 20387 RVA: 0x000FB040 File Offset: 0x000F9240
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x000FB057 File Offset: 0x000F9257
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x000FB06C File Offset: 0x000F926C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x000FB096 File Offset: 0x000F9296
		public bool Equals(tolerance other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002331 RID: 9009
		private ProgramNode _node;
	}
}
