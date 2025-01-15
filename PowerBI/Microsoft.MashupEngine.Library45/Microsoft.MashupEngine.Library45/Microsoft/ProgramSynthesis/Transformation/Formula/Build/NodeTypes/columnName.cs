using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015D9 RID: 5593
	public struct columnName : IProgramNodeBuilder, IEquatable<columnName>
	{
		// Token: 0x17002009 RID: 8201
		// (get) Token: 0x0600B99E RID: 47518 RVA: 0x0028162E File Offset: 0x0027F82E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B99F RID: 47519 RVA: 0x00281636 File Offset: 0x0027F836
		private columnName(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B9A0 RID: 47520 RVA: 0x0028163F File Offset: 0x0027F83F
		public static columnName CreateUnsafe(ProgramNode node)
		{
			return new columnName(node);
		}

		// Token: 0x0600B9A1 RID: 47521 RVA: 0x00281648 File Offset: 0x0027F848
		public static columnName? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.columnName)
			{
				return null;
			}
			return new columnName?(columnName.CreateUnsafe(node));
		}

		// Token: 0x0600B9A2 RID: 47522 RVA: 0x00281682 File Offset: 0x0027F882
		public static columnName CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new columnName(new Hole(g.Symbol.columnName, holeId));
		}

		// Token: 0x0600B9A3 RID: 47523 RVA: 0x0028169A File Offset: 0x0027F89A
		public columnName(GrammarBuilders g, string value)
		{
			this = new columnName(new LiteralNode(g.Symbol.columnName, value));
		}

		// Token: 0x1700200A RID: 8202
		// (get) Token: 0x0600B9A4 RID: 47524 RVA: 0x002816B3 File Offset: 0x0027F8B3
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B9A5 RID: 47525 RVA: 0x002816CA File Offset: 0x0027F8CA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B9A6 RID: 47526 RVA: 0x002816E0 File Offset: 0x0027F8E0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B9A7 RID: 47527 RVA: 0x0028170A File Offset: 0x0027F90A
		public bool Equals(columnName other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004687 RID: 18055
		private ProgramNode _node;
	}
}
