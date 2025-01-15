using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001023 RID: 4131
	public struct ContainsDate : IProgramNodeBuilder, IEquatable<ContainsDate>
	{
		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x060079F5 RID: 31221 RVA: 0x001A125E File Offset: 0x0019F45E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060079F6 RID: 31222 RVA: 0x001A1266 File Offset: 0x0019F466
		private ContainsDate(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060079F7 RID: 31223 RVA: 0x001A126F File Offset: 0x0019F46F
		public static ContainsDate CreateUnsafe(ProgramNode node)
		{
			return new ContainsDate(node);
		}

		// Token: 0x060079F8 RID: 31224 RVA: 0x001A1278 File Offset: 0x0019F478
		public static ContainsDate? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ContainsDate)
			{
				return null;
			}
			return new ContainsDate?(ContainsDate.CreateUnsafe(node));
		}

		// Token: 0x060079F9 RID: 31225 RVA: 0x001A12AD File Offset: 0x0019F4AD
		public ContainsDate(GrammarBuilders g, node value0)
		{
			this._node = g.Rule.ContainsDate.BuildASTNode(value0.Node);
		}

		// Token: 0x060079FA RID: 31226 RVA: 0x001A12CC File Offset: 0x0019F4CC
		public static implicit operator atomExpr(ContainsDate arg)
		{
			return atomExpr.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x060079FB RID: 31227 RVA: 0x001A12DA File Offset: 0x0019F4DA
		public node node
		{
			get
			{
				return node.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060079FC RID: 31228 RVA: 0x001A12EE File Offset: 0x0019F4EE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060079FD RID: 31229 RVA: 0x001A1304 File Offset: 0x0019F504
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060079FE RID: 31230 RVA: 0x001A132E File Offset: 0x0019F52E
		public bool Equals(ContainsDate other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400333C RID: 13116
		private ProgramNode _node;
	}
}
