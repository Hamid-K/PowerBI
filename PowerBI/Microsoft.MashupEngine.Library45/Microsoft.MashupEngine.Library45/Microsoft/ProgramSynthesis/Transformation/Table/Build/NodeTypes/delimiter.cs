using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001AC1 RID: 6849
	public struct delimiter : IProgramNodeBuilder, IEquatable<delimiter>
	{
		// Token: 0x170025E7 RID: 9703
		// (get) Token: 0x0600E291 RID: 58001 RVA: 0x00301DDA File Offset: 0x002FFFDA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E292 RID: 58002 RVA: 0x00301DE2 File Offset: 0x002FFFE2
		private delimiter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E293 RID: 58003 RVA: 0x00301DEB File Offset: 0x002FFFEB
		public static delimiter CreateUnsafe(ProgramNode node)
		{
			return new delimiter(node);
		}

		// Token: 0x0600E294 RID: 58004 RVA: 0x00301DF4 File Offset: 0x002FFFF4
		public static delimiter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.delimiter)
			{
				return null;
			}
			return new delimiter?(delimiter.CreateUnsafe(node));
		}

		// Token: 0x0600E295 RID: 58005 RVA: 0x00301E2E File Offset: 0x0030002E
		public static delimiter CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new delimiter(new Hole(g.Symbol.delimiter, holeId));
		}

		// Token: 0x0600E296 RID: 58006 RVA: 0x00301E46 File Offset: 0x00300046
		public delimiter(GrammarBuilders g, string value)
		{
			this = new delimiter(new LiteralNode(g.Symbol.delimiter, value));
		}

		// Token: 0x170025E8 RID: 9704
		// (get) Token: 0x0600E297 RID: 58007 RVA: 0x00301E5F File Offset: 0x0030005F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600E298 RID: 58008 RVA: 0x00301E76 File Offset: 0x00300076
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E299 RID: 58009 RVA: 0x00301E8C File Offset: 0x0030008C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E29A RID: 58010 RVA: 0x00301EB6 File Offset: 0x003000B6
		public bool Equals(delimiter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005580 RID: 21888
		private ProgramNode _node;
	}
}
