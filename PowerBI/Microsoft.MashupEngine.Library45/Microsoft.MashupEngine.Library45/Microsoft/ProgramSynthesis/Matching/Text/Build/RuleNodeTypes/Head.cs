using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Matching.Text.Build.RuleNodeTypes
{
	// Token: 0x020011E7 RID: 4583
	public struct Head : IProgramNodeBuilder, IEquatable<Head>
	{
		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x060089B4 RID: 35252 RVA: 0x001CF886 File Offset: 0x001CDA86
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060089B5 RID: 35253 RVA: 0x001CF88E File Offset: 0x001CDA8E
		private Head(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060089B6 RID: 35254 RVA: 0x001CF897 File Offset: 0x001CDA97
		public static Head CreateUnsafe(ProgramNode node)
		{
			return new Head(node);
		}

		// Token: 0x060089B7 RID: 35255 RVA: 0x001CF8A0 File Offset: 0x001CDAA0
		public static Head? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Head)
			{
				return null;
			}
			return new Head?(Head.CreateUnsafe(node));
		}

		// Token: 0x060089B8 RID: 35256 RVA: 0x001CF8D5 File Offset: 0x001CDAD5
		public Head(GrammarBuilders g, sRegions value0)
		{
			this._node = g.Rule.Head.BuildASTNode(value0.Node);
		}

		// Token: 0x060089B9 RID: 35257 RVA: 0x001CF8F4 File Offset: 0x001CDAF4
		public static implicit operator _LetB3(Head arg)
		{
			return _LetB3.CreateUnsafe(arg.Node);
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x060089BA RID: 35258 RVA: 0x001CF902 File Offset: 0x001CDB02
		public sRegions sRegions
		{
			get
			{
				return sRegions.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060089BB RID: 35259 RVA: 0x001CF916 File Offset: 0x001CDB16
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060089BC RID: 35260 RVA: 0x001CF92C File Offset: 0x001CDB2C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060089BD RID: 35261 RVA: 0x001CF956 File Offset: 0x001CDB56
		public bool Equals(Head other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400389B RID: 14491
		private ProgramNode _node;
	}
}
