using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015DA RID: 5594
	public struct columnNames : IProgramNodeBuilder, IEquatable<columnNames>
	{
		// Token: 0x1700200B RID: 8203
		// (get) Token: 0x0600B9A8 RID: 47528 RVA: 0x0028171E File Offset: 0x0027F91E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B9A9 RID: 47529 RVA: 0x00281726 File Offset: 0x0027F926
		private columnNames(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B9AA RID: 47530 RVA: 0x0028172F File Offset: 0x0027F92F
		public static columnNames CreateUnsafe(ProgramNode node)
		{
			return new columnNames(node);
		}

		// Token: 0x0600B9AB RID: 47531 RVA: 0x00281738 File Offset: 0x0027F938
		public static columnNames? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.columnNames)
			{
				return null;
			}
			return new columnNames?(columnNames.CreateUnsafe(node));
		}

		// Token: 0x0600B9AC RID: 47532 RVA: 0x00281772 File Offset: 0x0027F972
		public static columnNames CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new columnNames(new Hole(g.Symbol.columnNames, holeId));
		}

		// Token: 0x0600B9AD RID: 47533 RVA: 0x0028178A File Offset: 0x0027F98A
		public columnNames(GrammarBuilders g, string[] value)
		{
			this = new columnNames(new LiteralNode(g.Symbol.columnNames, value));
		}

		// Token: 0x1700200C RID: 8204
		// (get) Token: 0x0600B9AE RID: 47534 RVA: 0x002817A3 File Offset: 0x0027F9A3
		public string[] Value
		{
			get
			{
				return (string[])((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B9AF RID: 47535 RVA: 0x002817BA File Offset: 0x0027F9BA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B9B0 RID: 47536 RVA: 0x002817D0 File Offset: 0x0027F9D0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B9B1 RID: 47537 RVA: 0x002817FA File Offset: 0x0027F9FA
		public bool Equals(columnNames other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004688 RID: 18056
		private ProgramNode _node;
	}
}
