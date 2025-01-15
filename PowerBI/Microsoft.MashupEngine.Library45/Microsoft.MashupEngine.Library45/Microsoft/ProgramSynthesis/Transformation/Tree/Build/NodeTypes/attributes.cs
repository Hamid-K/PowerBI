using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Build.NodeTypes
{
	// Token: 0x02001E8D RID: 7821
	public struct attributes : IProgramNodeBuilder, IEquatable<attributes>
	{
		// Token: 0x17002BE7 RID: 11239
		// (get) Token: 0x06010860 RID: 67680 RVA: 0x0038D81A File Offset: 0x0038BA1A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06010861 RID: 67681 RVA: 0x0038D822 File Offset: 0x0038BA22
		private attributes(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06010862 RID: 67682 RVA: 0x0038D82B File Offset: 0x0038BA2B
		public static attributes CreateUnsafe(ProgramNode node)
		{
			return new attributes(node);
		}

		// Token: 0x06010863 RID: 67683 RVA: 0x0038D834 File Offset: 0x0038BA34
		public static attributes? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.attributes)
			{
				return null;
			}
			return new attributes?(attributes.CreateUnsafe(node));
		}

		// Token: 0x06010864 RID: 67684 RVA: 0x0038D86E File Offset: 0x0038BA6E
		public static attributes CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new attributes(new Hole(g.Symbol.attributes, holeId));
		}

		// Token: 0x06010865 RID: 67685 RVA: 0x0038D886 File Offset: 0x0038BA86
		public attributes(GrammarBuilders g, Dictionary<string, string> value)
		{
			this = new attributes(new LiteralNode(g.Symbol.attributes, value));
		}

		// Token: 0x17002BE8 RID: 11240
		// (get) Token: 0x06010866 RID: 67686 RVA: 0x0038D89F File Offset: 0x0038BA9F
		public Dictionary<string, string> Value
		{
			get
			{
				return (Dictionary<string, string>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06010867 RID: 67687 RVA: 0x0038D8B6 File Offset: 0x0038BAB6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06010868 RID: 67688 RVA: 0x0038D8CC File Offset: 0x0038BACC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06010869 RID: 67689 RVA: 0x0038D8F6 File Offset: 0x0038BAF6
		public bool Equals(attributes other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040062CC RID: 25292
		private ProgramNode _node;
	}
}
