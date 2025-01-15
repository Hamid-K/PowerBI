using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C72 RID: 7282
	public struct columnName : IProgramNodeBuilder, IEquatable<columnName>
	{
		// Token: 0x1700291C RID: 10524
		// (get) Token: 0x0600F6AC RID: 63148 RVA: 0x003499A2 File Offset: 0x00347BA2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F6AD RID: 63149 RVA: 0x003499AA File Offset: 0x00347BAA
		private columnName(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F6AE RID: 63150 RVA: 0x003499B3 File Offset: 0x00347BB3
		public static columnName CreateUnsafe(ProgramNode node)
		{
			return new columnName(node);
		}

		// Token: 0x0600F6AF RID: 63151 RVA: 0x003499BC File Offset: 0x00347BBC
		public static columnName? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.columnName)
			{
				return null;
			}
			return new columnName?(columnName.CreateUnsafe(node));
		}

		// Token: 0x0600F6B0 RID: 63152 RVA: 0x003499F6 File Offset: 0x00347BF6
		public static columnName CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new columnName(new Hole(g.Symbol.columnName, holeId));
		}

		// Token: 0x0600F6B1 RID: 63153 RVA: 0x00349A0E File Offset: 0x00347C0E
		public columnName(GrammarBuilders g)
		{
			this = new columnName(new VariableNode(g.Symbol.columnName));
		}

		// Token: 0x1700291D RID: 10525
		// (get) Token: 0x0600F6B2 RID: 63154 RVA: 0x00349A26 File Offset: 0x00347C26
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600F6B3 RID: 63155 RVA: 0x00349A33 File Offset: 0x00347C33
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F6B4 RID: 63156 RVA: 0x00349A48 File Offset: 0x00347C48
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F6B5 RID: 63157 RVA: 0x00349A72 File Offset: 0x00347C72
		public bool Equals(columnName other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B61 RID: 23393
		private ProgramNode _node;
	}
}
