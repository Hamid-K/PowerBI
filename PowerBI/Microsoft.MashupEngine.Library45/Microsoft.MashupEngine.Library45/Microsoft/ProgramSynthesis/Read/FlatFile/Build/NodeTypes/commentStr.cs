using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes
{
	// Token: 0x02001289 RID: 4745
	public struct commentStr : IProgramNodeBuilder, IEquatable<commentStr>
	{
		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x06008F90 RID: 36752 RVA: 0x001E2D26 File Offset: 0x001E0F26
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F91 RID: 36753 RVA: 0x001E2D2E File Offset: 0x001E0F2E
		private commentStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F92 RID: 36754 RVA: 0x001E2D37 File Offset: 0x001E0F37
		public static commentStr CreateUnsafe(ProgramNode node)
		{
			return new commentStr(node);
		}

		// Token: 0x06008F93 RID: 36755 RVA: 0x001E2D40 File Offset: 0x001E0F40
		public static commentStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.commentStr)
			{
				return null;
			}
			return new commentStr?(commentStr.CreateUnsafe(node));
		}

		// Token: 0x06008F94 RID: 36756 RVA: 0x001E2D7A File Offset: 0x001E0F7A
		public static commentStr CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new commentStr(new Hole(g.Symbol.commentStr, holeId));
		}

		// Token: 0x06008F95 RID: 36757 RVA: 0x001E2D92 File Offset: 0x001E0F92
		public commentStr(GrammarBuilders g, Optional<string> value)
		{
			this = new commentStr(new LiteralNode(g.Symbol.commentStr, value));
		}

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x06008F96 RID: 36758 RVA: 0x001E2DB0 File Offset: 0x001E0FB0
		public Optional<string> Value
		{
			get
			{
				return (Optional<string>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008F97 RID: 36759 RVA: 0x001E2DC7 File Offset: 0x001E0FC7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F98 RID: 36760 RVA: 0x001E2DDC File Offset: 0x001E0FDC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F99 RID: 36761 RVA: 0x001E2E06 File Offset: 0x001E1006
		public bool Equals(commentStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A7A RID: 14970
		private ProgramNode _node;
	}
}
