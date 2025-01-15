using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x0200101E RID: 4126
	public struct SingleSelection4 : IProgramNodeBuilder, IEquatable<SingleSelection4>
	{
		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x060079C1 RID: 31169 RVA: 0x001A0DBA File Offset: 0x0019EFBA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060079C2 RID: 31170 RVA: 0x001A0DC2 File Offset: 0x0019EFC2
		private SingleSelection4(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060079C3 RID: 31171 RVA: 0x001A0DCB File Offset: 0x0019EFCB
		public static SingleSelection4 CreateUnsafe(ProgramNode node)
		{
			return new SingleSelection4(node);
		}

		// Token: 0x060079C4 RID: 31172 RVA: 0x001A0DD4 File Offset: 0x0019EFD4
		public static SingleSelection4? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SingleSelection4)
			{
				return null;
			}
			return new SingleSelection4?(SingleSelection4.CreateUnsafe(node));
		}

		// Token: 0x060079C5 RID: 31173 RVA: 0x001A0E09 File Offset: 0x0019F009
		public SingleSelection4(GrammarBuilders g, filterSelection4 value0)
		{
			this._node = g.Rule.SingleSelection4.BuildASTNode(value0.Node);
		}

		// Token: 0x060079C6 RID: 31174 RVA: 0x001A0E28 File Offset: 0x0019F028
		public static implicit operator selection7(SingleSelection4 arg)
		{
			return selection7.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x060079C7 RID: 31175 RVA: 0x001A0E36 File Offset: 0x0019F036
		public filterSelection4 filterSelection4
		{
			get
			{
				return filterSelection4.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060079C8 RID: 31176 RVA: 0x001A0E4A File Offset: 0x0019F04A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060079C9 RID: 31177 RVA: 0x001A0E60 File Offset: 0x0019F060
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060079CA RID: 31178 RVA: 0x001A0E8A File Offset: 0x0019F08A
		public bool Equals(SingleSelection4 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003337 RID: 13111
		private ProgramNode _node;
	}
}
