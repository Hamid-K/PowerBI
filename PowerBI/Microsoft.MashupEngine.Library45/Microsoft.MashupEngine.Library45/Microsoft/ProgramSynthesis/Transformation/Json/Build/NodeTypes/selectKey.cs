using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A41 RID: 6721
	public struct selectKey : IProgramNodeBuilder, IEquatable<selectKey>
	{
		// Token: 0x17002510 RID: 9488
		// (get) Token: 0x0600DD50 RID: 56656 RVA: 0x002F11C6 File Offset: 0x002EF3C6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DD51 RID: 56657 RVA: 0x002F11CE File Offset: 0x002EF3CE
		private selectKey(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DD52 RID: 56658 RVA: 0x002F11D7 File Offset: 0x002EF3D7
		public static selectKey CreateUnsafe(ProgramNode node)
		{
			return new selectKey(node);
		}

		// Token: 0x0600DD53 RID: 56659 RVA: 0x002F11E0 File Offset: 0x002EF3E0
		public static selectKey? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selectKey)
			{
				return null;
			}
			return new selectKey?(selectKey.CreateUnsafe(node));
		}

		// Token: 0x0600DD54 RID: 56660 RVA: 0x002F121A File Offset: 0x002EF41A
		public static selectKey CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selectKey(new Hole(g.Symbol.selectKey, holeId));
		}

		// Token: 0x0600DD55 RID: 56661 RVA: 0x002F1232 File Offset: 0x002EF432
		public SelectKey Cast_SelectKey()
		{
			return SelectKey.CreateUnsafe(this.Node);
		}

		// Token: 0x0600DD56 RID: 56662 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SelectKey(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600DD57 RID: 56663 RVA: 0x002F123F File Offset: 0x002EF43F
		public bool Is_SelectKey(GrammarBuilders g, out SelectKey value)
		{
			value = SelectKey.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600DD58 RID: 56664 RVA: 0x002F1253 File Offset: 0x002EF453
		public SelectKey? As_SelectKey(GrammarBuilders g)
		{
			return new SelectKey?(SelectKey.CreateUnsafe(this.Node));
		}

		// Token: 0x0600DD59 RID: 56665 RVA: 0x002F1265 File Offset: 0x002EF465
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DD5A RID: 56666 RVA: 0x002F1278 File Offset: 0x002EF478
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DD5B RID: 56667 RVA: 0x002F12A2 File Offset: 0x002EF4A2
		public bool Equals(selectKey other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005432 RID: 21554
		private ProgramNode _node;
	}
}
