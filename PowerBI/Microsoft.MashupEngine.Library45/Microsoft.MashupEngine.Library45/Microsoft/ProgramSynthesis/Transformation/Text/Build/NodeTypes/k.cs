using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C5E RID: 7262
	public struct k : IProgramNodeBuilder, IEquatable<k>
	{
		// Token: 0x170028F4 RID: 10484
		// (get) Token: 0x0600F5E4 RID: 62948 RVA: 0x003486C2 File Offset: 0x003468C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F5E5 RID: 62949 RVA: 0x003486CA File Offset: 0x003468CA
		private k(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F5E6 RID: 62950 RVA: 0x003486D3 File Offset: 0x003468D3
		public static k CreateUnsafe(ProgramNode node)
		{
			return new k(node);
		}

		// Token: 0x0600F5E7 RID: 62951 RVA: 0x003486DC File Offset: 0x003468DC
		public static k? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.k)
			{
				return null;
			}
			return new k?(k.CreateUnsafe(node));
		}

		// Token: 0x0600F5E8 RID: 62952 RVA: 0x00348716 File Offset: 0x00346916
		public static k CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new k(new Hole(g.Symbol.k, holeId));
		}

		// Token: 0x0600F5E9 RID: 62953 RVA: 0x0034872E File Offset: 0x0034692E
		public k(GrammarBuilders g, int value)
		{
			this = new k(new LiteralNode(g.Symbol.k, value));
		}

		// Token: 0x170028F5 RID: 10485
		// (get) Token: 0x0600F5EA RID: 62954 RVA: 0x0034874C File Offset: 0x0034694C
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F5EB RID: 62955 RVA: 0x00348763 File Offset: 0x00346963
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F5EC RID: 62956 RVA: 0x00348778 File Offset: 0x00346978
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F5ED RID: 62957 RVA: 0x003487A2 File Offset: 0x003469A2
		public bool Equals(k other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B4D RID: 23373
		private ProgramNode _node;
	}
}
