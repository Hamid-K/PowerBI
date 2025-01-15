using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Wrangling.Json;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes
{
	// Token: 0x02000B6D RID: 2925
	public struct path : IProgramNodeBuilder, IEquatable<path>
	{
		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06004A37 RID: 18999 RVA: 0x000E967E File Offset: 0x000E787E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x000E9686 File Offset: 0x000E7886
		private path(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x000E968F File Offset: 0x000E788F
		public static path CreateUnsafe(ProgramNode node)
		{
			return new path(node);
		}

		// Token: 0x06004A3A RID: 19002 RVA: 0x000E9698 File Offset: 0x000E7898
		public static path? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.path)
			{
				return null;
			}
			return new path?(path.CreateUnsafe(node));
		}

		// Token: 0x06004A3B RID: 19003 RVA: 0x000E96D2 File Offset: 0x000E78D2
		public static path CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new path(new Hole(g.Symbol.path, holeId));
		}

		// Token: 0x06004A3C RID: 19004 RVA: 0x000E96EA File Offset: 0x000E78EA
		public path(GrammarBuilders g, JPath value)
		{
			this = new path(new LiteralNode(g.Symbol.path, value));
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06004A3D RID: 19005 RVA: 0x000E9703 File Offset: 0x000E7903
		public JPath Value
		{
			get
			{
				return (JPath)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06004A3E RID: 19006 RVA: 0x000E971A File Offset: 0x000E791A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x000E9730 File Offset: 0x000E7930
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x000E975A File Offset: 0x000E795A
		public bool Equals(path other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002168 RID: 8552
		private ProgramNode _node;
	}
}
