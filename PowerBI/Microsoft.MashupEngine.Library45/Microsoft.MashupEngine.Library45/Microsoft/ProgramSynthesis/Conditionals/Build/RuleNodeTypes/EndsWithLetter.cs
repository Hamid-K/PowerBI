using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes
{
	// Token: 0x02000A42 RID: 2626
	public struct EndsWithLetter : IProgramNodeBuilder, IEquatable<EndsWithLetter>
	{
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06004080 RID: 16512 RVA: 0x000CAD56 File Offset: 0x000C8F56
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x000CAD5E File Offset: 0x000C8F5E
		private EndsWithLetter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x000CAD67 File Offset: 0x000C8F67
		public static EndsWithLetter CreateUnsafe(ProgramNode node)
		{
			return new EndsWithLetter(node);
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x000CAD70 File Offset: 0x000C8F70
		public static EndsWithLetter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.EndsWithLetter)
			{
				return null;
			}
			return new EndsWithLetter?(EndsWithLetter.CreateUnsafe(node));
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x000CADA5 File Offset: 0x000C8FA5
		public EndsWithLetter(GrammarBuilders g, s value0)
		{
			this._node = g.Rule.EndsWithLetter.BuildASTNode(value0.Node);
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x000CADC4 File Offset: 0x000C8FC4
		public static implicit operator match(EndsWithLetter arg)
		{
			return match.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x000CADD2 File Offset: 0x000C8FD2
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x000CADE6 File Offset: 0x000C8FE6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x000CADFC File Offset: 0x000C8FFC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x000CAE26 File Offset: 0x000C9026
		public bool Equals(EndsWithLetter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001D7D RID: 7549
		private ProgramNode _node;
	}
}
