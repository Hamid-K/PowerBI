using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C2B RID: 7211
	public struct LetColumnName : IProgramNodeBuilder, IEquatable<LetColumnName>
	{
		// Token: 0x170028A9 RID: 10409
		// (get) Token: 0x0600F2E0 RID: 62176 RVA: 0x00341AC6 File Offset: 0x0033FCC6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F2E1 RID: 62177 RVA: 0x00341ACE File Offset: 0x0033FCCE
		private LetColumnName(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F2E2 RID: 62178 RVA: 0x00341AD7 File Offset: 0x0033FCD7
		public static LetColumnName CreateUnsafe(ProgramNode node)
		{
			return new LetColumnName(node);
		}

		// Token: 0x0600F2E3 RID: 62179 RVA: 0x00341AE0 File Offset: 0x0033FCE0
		public static LetColumnName? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetColumnName)
			{
				return null;
			}
			return new LetColumnName?(LetColumnName.CreateUnsafe(node));
		}

		// Token: 0x0600F2E4 RID: 62180 RVA: 0x00341B15 File Offset: 0x0033FD15
		public LetColumnName(GrammarBuilders g, idx value0, letOptions value1)
		{
			this._node = new LetNode(g.Rule.LetColumnName, value0.Node, value1.Node);
		}

		// Token: 0x0600F2E5 RID: 62181 RVA: 0x00341B3B File Offset: 0x0033FD3B
		public static implicit operator f(LetColumnName arg)
		{
			return f.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028AA RID: 10410
		// (get) Token: 0x0600F2E6 RID: 62182 RVA: 0x00341B49 File Offset: 0x0033FD49
		public idx idx
		{
			get
			{
				return idx.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170028AB RID: 10411
		// (get) Token: 0x0600F2E7 RID: 62183 RVA: 0x00341B5D File Offset: 0x0033FD5D
		public letOptions letOptions
		{
			get
			{
				return letOptions.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F2E8 RID: 62184 RVA: 0x00341B71 File Offset: 0x0033FD71
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F2E9 RID: 62185 RVA: 0x00341B84 File Offset: 0x0033FD84
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F2EA RID: 62186 RVA: 0x00341BAE File Offset: 0x0033FDAE
		public bool Equals(LetColumnName other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B1A RID: 23322
		private ProgramNode _node;
	}
}
