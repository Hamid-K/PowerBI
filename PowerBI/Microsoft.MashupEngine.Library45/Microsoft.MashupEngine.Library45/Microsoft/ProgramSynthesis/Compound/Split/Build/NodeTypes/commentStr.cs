using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200097C RID: 2428
	public struct commentStr : IProgramNodeBuilder, IEquatable<commentStr>
	{
		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x000B2DFA File Offset: 0x000B0FFA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x000B2E02 File Offset: 0x000B1002
		private commentStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x000B2E0B File Offset: 0x000B100B
		public static commentStr CreateUnsafe(ProgramNode node)
		{
			return new commentStr(node);
		}

		// Token: 0x06003A01 RID: 14849 RVA: 0x000B2E14 File Offset: 0x000B1014
		public static commentStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.commentStr)
			{
				return null;
			}
			return new commentStr?(commentStr.CreateUnsafe(node));
		}

		// Token: 0x06003A02 RID: 14850 RVA: 0x000B2E4E File Offset: 0x000B104E
		public static commentStr CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new commentStr(new Hole(g.Symbol.commentStr, holeId));
		}

		// Token: 0x06003A03 RID: 14851 RVA: 0x000B2E66 File Offset: 0x000B1066
		public commentStr(GrammarBuilders g, Optional<string> value)
		{
			this = new commentStr(new LiteralNode(g.Symbol.commentStr, value));
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06003A04 RID: 14852 RVA: 0x000B2E84 File Offset: 0x000B1084
		public Optional<string> Value
		{
			get
			{
				return (Optional<string>)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06003A05 RID: 14853 RVA: 0x000B2E9B File Offset: 0x000B109B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003A06 RID: 14854 RVA: 0x000B2EB0 File Offset: 0x000B10B0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003A07 RID: 14855 RVA: 0x000B2EDA File Offset: 0x000B10DA
		public bool Equals(commentStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A9C RID: 6812
		private ProgramNode _node;
	}
}
