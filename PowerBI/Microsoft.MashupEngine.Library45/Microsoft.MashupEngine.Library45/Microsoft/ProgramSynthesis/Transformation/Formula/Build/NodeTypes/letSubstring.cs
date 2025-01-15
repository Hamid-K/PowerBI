using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015BB RID: 5563
	public struct letSubstring : IProgramNodeBuilder, IEquatable<letSubstring>
	{
		// Token: 0x17001FE1 RID: 8161
		// (get) Token: 0x0600B800 RID: 47104 RVA: 0x0027E2F2 File Offset: 0x0027C4F2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B801 RID: 47105 RVA: 0x0027E2FA File Offset: 0x0027C4FA
		private letSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B802 RID: 47106 RVA: 0x0027E303 File Offset: 0x0027C503
		public static letSubstring CreateUnsafe(ProgramNode node)
		{
			return new letSubstring(node);
		}

		// Token: 0x0600B803 RID: 47107 RVA: 0x0027E30C File Offset: 0x0027C50C
		public static letSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.letSubstring)
			{
				return null;
			}
			return new letSubstring?(letSubstring.CreateUnsafe(node));
		}

		// Token: 0x0600B804 RID: 47108 RVA: 0x0027E346 File Offset: 0x0027C546
		public static letSubstring CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new letSubstring(new Hole(g.Symbol.letSubstring, holeId));
		}

		// Token: 0x0600B805 RID: 47109 RVA: 0x0027E35E File Offset: 0x0027C55E
		public LetX Cast_LetX()
		{
			return LetX.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B806 RID: 47110 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_LetX(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B807 RID: 47111 RVA: 0x0027E36B File Offset: 0x0027C56B
		public bool Is_LetX(GrammarBuilders g, out LetX value)
		{
			value = LetX.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B808 RID: 47112 RVA: 0x0027E37F File Offset: 0x0027C57F
		public LetX? As_LetX(GrammarBuilders g)
		{
			return new LetX?(LetX.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B809 RID: 47113 RVA: 0x0027E391 File Offset: 0x0027C591
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B80A RID: 47114 RVA: 0x0027E3A4 File Offset: 0x0027C5A4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B80B RID: 47115 RVA: 0x0027E3CE File Offset: 0x0027C5CE
		public bool Equals(letSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004669 RID: 18025
		private ProgramNode _node;
	}
}
