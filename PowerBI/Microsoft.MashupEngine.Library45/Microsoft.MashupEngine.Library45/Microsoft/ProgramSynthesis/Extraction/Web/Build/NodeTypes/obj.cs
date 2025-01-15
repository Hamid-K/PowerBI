using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200109C RID: 4252
	public struct obj : IProgramNodeBuilder, IEquatable<obj>
	{
		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x0600801F RID: 32799 RVA: 0x001AD02E File Offset: 0x001AB22E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008020 RID: 32800 RVA: 0x001AD036 File Offset: 0x001AB236
		private obj(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008021 RID: 32801 RVA: 0x001AD03F File Offset: 0x001AB23F
		public static obj CreateUnsafe(ProgramNode node)
		{
			return new obj(node);
		}

		// Token: 0x06008022 RID: 32802 RVA: 0x001AD048 File Offset: 0x001AB248
		public static obj? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.obj)
			{
				return null;
			}
			return new obj?(obj.CreateUnsafe(node));
		}

		// Token: 0x06008023 RID: 32803 RVA: 0x001AD082 File Offset: 0x001AB282
		public static obj CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new obj(new Hole(g.Symbol.obj, holeId));
		}

		// Token: 0x06008024 RID: 32804 RVA: 0x001AD09A File Offset: 0x001AB29A
		public obj(GrammarBuilders g, object value)
		{
			this = new obj(new LiteralNode(g.Symbol.obj, value));
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x06008025 RID: 32805 RVA: 0x001AD0B3 File Offset: 0x001AB2B3
		public object Value
		{
			get
			{
				return ((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008026 RID: 32806 RVA: 0x001AD0C5 File Offset: 0x001AB2C5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008027 RID: 32807 RVA: 0x001AD0D8 File Offset: 0x001AB2D8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008028 RID: 32808 RVA: 0x001AD102 File Offset: 0x001AB302
		public bool Equals(obj other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033B5 RID: 13237
		private ProgramNode _node;
	}
}
