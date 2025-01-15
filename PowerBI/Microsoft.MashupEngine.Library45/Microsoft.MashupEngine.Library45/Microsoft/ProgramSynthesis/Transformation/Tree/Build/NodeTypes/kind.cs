using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E8E RID: 7822
	public struct kind : IProgramNodeBuilder, IEquatable<kind>
	{
		// Token: 0x17002BE9 RID: 11241
		// (get) Token: 0x0601086A RID: 67690 RVA: 0x0038D90A File Offset: 0x0038BB0A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0601086B RID: 67691 RVA: 0x0038D912 File Offset: 0x0038BB12
		private kind(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0601086C RID: 67692 RVA: 0x0038D91B File Offset: 0x0038BB1B
		public static kind CreateUnsafe(ProgramNode node)
		{
			return new kind(node);
		}

		// Token: 0x0601086D RID: 67693 RVA: 0x0038D924 File Offset: 0x0038BB24
		public static kind? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.kind)
			{
				return null;
			}
			return new kind?(kind.CreateUnsafe(node));
		}

		// Token: 0x0601086E RID: 67694 RVA: 0x0038D95E File Offset: 0x0038BB5E
		public static kind CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new kind(new Hole(g.Symbol.kind, holeId));
		}

		// Token: 0x0601086F RID: 67695 RVA: 0x0038D976 File Offset: 0x0038BB76
		public kind(GrammarBuilders g, string value)
		{
			this = new kind(new LiteralNode(g.Symbol.kind, value));
		}

		// Token: 0x17002BEA RID: 11242
		// (get) Token: 0x06010870 RID: 67696 RVA: 0x0038D98F File Offset: 0x0038BB8F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06010871 RID: 67697 RVA: 0x0038D9A6 File Offset: 0x0038BBA6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010872 RID: 67698 RVA: 0x0038D9BC File Offset: 0x0038BBBC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010873 RID: 67699 RVA: 0x0038D9E6 File Offset: 0x0038BBE6
		public bool Equals(kind other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062CD RID: 25293
		private ProgramNode _node;
	}
}
