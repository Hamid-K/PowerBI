using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F33 RID: 3891
	public struct LetPrepend : IProgramNodeBuilder, IEquatable<LetPrepend>
	{
		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x06006BD4 RID: 27604 RVA: 0x00161A22 File Offset: 0x0015FC22
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006BD5 RID: 27605 RVA: 0x00161A2A File Offset: 0x0015FC2A
		private LetPrepend(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006BD6 RID: 27606 RVA: 0x00161A33 File Offset: 0x0015FC33
		public static LetPrepend CreateUnsafe(ProgramNode node)
		{
			return new LetPrepend(node);
		}

		// Token: 0x06006BD7 RID: 27607 RVA: 0x00161A3C File Offset: 0x0015FC3C
		public static LetPrepend? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetPrepend)
			{
				return null;
			}
			return new LetPrepend?(LetPrepend.CreateUnsafe(node));
		}

		// Token: 0x06006BD8 RID: 27608 RVA: 0x00161A71 File Offset: 0x0015FC71
		public LetPrepend(GrammarBuilders g, _LetB0 value0, _LetB1 value1)
		{
			this._node = new LetNode(g.Rule.LetPrepend, value0.Node, value1.Node);
		}

		// Token: 0x06006BD9 RID: 27609 RVA: 0x00161A97 File Offset: 0x0015FC97
		public static implicit operator _LetB2(LetPrepend arg)
		{
			return _LetB2.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x06006BDA RID: 27610 RVA: 0x00161AA5 File Offset: 0x0015FCA5
		public _LetB0 _LetB0
		{
			get
			{
				return _LetB0.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x06006BDB RID: 27611 RVA: 0x00161AB9 File Offset: 0x0015FCB9
		public _LetB1 _LetB1
		{
			get
			{
				return _LetB1.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006BDC RID: 27612 RVA: 0x00161ACD File Offset: 0x0015FCCD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006BDD RID: 27613 RVA: 0x00161AE0 File Offset: 0x0015FCE0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006BDE RID: 27614 RVA: 0x00161B0A File Offset: 0x0015FD0A
		public bool Equals(LetPrepend other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F1E RID: 12062
		private ProgramNode _node;
	}
}
