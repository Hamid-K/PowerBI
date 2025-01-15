using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E8F RID: 7823
	public struct name : IProgramNodeBuilder, IEquatable<name>
	{
		// Token: 0x17002BEB RID: 11243
		// (get) Token: 0x06010874 RID: 67700 RVA: 0x0038D9FA File Offset: 0x0038BBFA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010875 RID: 67701 RVA: 0x0038DA02 File Offset: 0x0038BC02
		private name(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010876 RID: 67702 RVA: 0x0038DA0B File Offset: 0x0038BC0B
		public static name CreateUnsafe(ProgramNode node)
		{
			return new name(node);
		}

		// Token: 0x06010877 RID: 67703 RVA: 0x0038DA14 File Offset: 0x0038BC14
		public static name? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.name)
			{
				return null;
			}
			return new name?(name.CreateUnsafe(node));
		}

		// Token: 0x06010878 RID: 67704 RVA: 0x0038DA4E File Offset: 0x0038BC4E
		public static name CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new name(new Hole(g.Symbol.name, holeId));
		}

		// Token: 0x06010879 RID: 67705 RVA: 0x0038DA66 File Offset: 0x0038BC66
		public name(GrammarBuilders g, string value)
		{
			this = new name(new LiteralNode(g.Symbol.name, value));
		}

		// Token: 0x17002BEC RID: 11244
		// (get) Token: 0x0601087A RID: 67706 RVA: 0x0038DA7F File Offset: 0x0038BC7F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0601087B RID: 67707 RVA: 0x0038DA96 File Offset: 0x0038BC96
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0601087C RID: 67708 RVA: 0x0038DAAC File Offset: 0x0038BCAC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0601087D RID: 67709 RVA: 0x0038DAD6 File Offset: 0x0038BCD6
		public bool Equals(name other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062CE RID: 25294
		private ProgramNode _node;
	}
}
