using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015AA RID: 5546
	public struct inull : IProgramNodeBuilder, IEquatable<inull>
	{
		// Token: 0x17001FD0 RID: 8144
		// (get) Token: 0x0600B6BE RID: 46782 RVA: 0x0027AAA6 File Offset: 0x00278CA6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B6BF RID: 46783 RVA: 0x0027AAAE File Offset: 0x00278CAE
		private inull(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B6C0 RID: 46784 RVA: 0x0027AAB7 File Offset: 0x00278CB7
		public static inull CreateUnsafe(ProgramNode node)
		{
			return new inull(node);
		}

		// Token: 0x0600B6C1 RID: 46785 RVA: 0x0027AAC0 File Offset: 0x00278CC0
		public static inull? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.inull)
			{
				return null;
			}
			return new inull?(inull.CreateUnsafe(node));
		}

		// Token: 0x0600B6C2 RID: 46786 RVA: 0x0027AAFA File Offset: 0x00278CFA
		public static inull CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new inull(new Hole(g.Symbol.inull, holeId));
		}

		// Token: 0x0600B6C3 RID: 46787 RVA: 0x0027AB12 File Offset: 0x00278D12
		public Null Cast_Null()
		{
			return Null.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B6C4 RID: 46788 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Null(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B6C5 RID: 46789 RVA: 0x0027AB1F File Offset: 0x00278D1F
		public bool Is_Null(GrammarBuilders g, out Null value)
		{
			value = Null.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B6C6 RID: 46790 RVA: 0x0027AB33 File Offset: 0x00278D33
		public Null? As_Null(GrammarBuilders g)
		{
			return new Null?(Null.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B6C7 RID: 46791 RVA: 0x0027AB45 File Offset: 0x00278D45
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B6C8 RID: 46792 RVA: 0x0027AB58 File Offset: 0x00278D58
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B6C9 RID: 46793 RVA: 0x0027AB82 File Offset: 0x00278D82
		public bool Equals(inull other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004658 RID: 18008
		private ProgramNode _node;
	}
}
