using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F26 RID: 3878
	public struct First : IProgramNodeBuilder, IEquatable<First>
	{
		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06006B44 RID: 27460 RVA: 0x00160D1A File Offset: 0x0015EF1A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B45 RID: 27461 RVA: 0x00160D22 File Offset: 0x0015EF22
		private First(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006B46 RID: 27462 RVA: 0x00160D2B File Offset: 0x0015EF2B
		public static First CreateUnsafe(ProgramNode node)
		{
			return new First(node);
		}

		// Token: 0x06006B47 RID: 27463 RVA: 0x00160D34 File Offset: 0x0015EF34
		public static First? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.First)
			{
				return null;
			}
			return new First?(First.CreateUnsafe(node));
		}

		// Token: 0x06006B48 RID: 27464 RVA: 0x00160D69 File Offset: 0x0015EF69
		public First(GrammarBuilders g, tup value0)
		{
			this._node = g.Rule.First.BuildASTNode(value0.Node);
		}

		// Token: 0x06006B49 RID: 27465 RVA: 0x00160D88 File Offset: 0x0015EF88
		public static implicit operator _LetB3(First arg)
		{
			return _LetB3.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06006B4A RID: 27466 RVA: 0x00160D96 File Offset: 0x0015EF96
		public tup tup
		{
			get
			{
				return tup.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06006B4B RID: 27467 RVA: 0x00160DAA File Offset: 0x0015EFAA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006B4C RID: 27468 RVA: 0x00160DC0 File Offset: 0x0015EFC0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006B4D RID: 27469 RVA: 0x00160DEA File Offset: 0x0015EFEA
		public bool Equals(First other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F11 RID: 12049
		private ProgramNode _node;
	}
}
