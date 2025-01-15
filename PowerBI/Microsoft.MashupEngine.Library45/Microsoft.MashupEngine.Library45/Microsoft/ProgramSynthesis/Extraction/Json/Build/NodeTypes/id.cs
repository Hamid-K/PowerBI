using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes
{
	// Token: 0x02000B6E RID: 2926
	public struct id : IProgramNodeBuilder, IEquatable<id>
	{
		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06004A41 RID: 19009 RVA: 0x000E976E File Offset: 0x000E796E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x000E9776 File Offset: 0x000E7976
		private id(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x000E977F File Offset: 0x000E797F
		public static id CreateUnsafe(ProgramNode node)
		{
			return new id(node);
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x000E9788 File Offset: 0x000E7988
		public static id? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.id)
			{
				return null;
			}
			return new id?(id.CreateUnsafe(node));
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x000E97C2 File Offset: 0x000E79C2
		public static id CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new id(new Hole(g.Symbol.id, holeId));
		}

		// Token: 0x06004A46 RID: 19014 RVA: 0x000E97DA File Offset: 0x000E79DA
		public id(GrammarBuilders g, string value)
		{
			this = new id(new LiteralNode(g.Symbol.id, value));
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06004A47 RID: 19015 RVA: 0x000E97F3 File Offset: 0x000E79F3
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x000E980A File Offset: 0x000E7A0A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x000E9820 File Offset: 0x000E7A20
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004A4A RID: 19018 RVA: 0x000E984A File Offset: 0x000E7A4A
		public bool Equals(id other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002169 RID: 8553
		private ProgramNode _node;
	}
}
