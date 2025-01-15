using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001013 RID: 4115
	public struct SingleSelection1 : IProgramNodeBuilder, IEquatable<SingleSelection1>
	{
		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x0600794F RID: 31055 RVA: 0x001A038E File Offset: 0x0019E58E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007950 RID: 31056 RVA: 0x001A0396 File Offset: 0x0019E596
		private SingleSelection1(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007951 RID: 31057 RVA: 0x001A039F File Offset: 0x0019E59F
		public static SingleSelection1 CreateUnsafe(ProgramNode node)
		{
			return new SingleSelection1(node);
		}

		// Token: 0x06007952 RID: 31058 RVA: 0x001A03A8 File Offset: 0x0019E5A8
		public static SingleSelection1? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SingleSelection1)
			{
				return null;
			}
			return new SingleSelection1?(SingleSelection1.CreateUnsafe(node));
		}

		// Token: 0x06007953 RID: 31059 RVA: 0x001A03DD File Offset: 0x0019E5DD
		public SingleSelection1(GrammarBuilders g, filterSelection value0)
		{
			this._node = g.Rule.SingleSelection1.BuildASTNode(value0.Node);
		}

		// Token: 0x06007954 RID: 31060 RVA: 0x001A03FC File Offset: 0x0019E5FC
		public static implicit operator selection(SingleSelection1 arg)
		{
			return selection.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x06007955 RID: 31061 RVA: 0x001A040A File Offset: 0x0019E60A
		public filterSelection filterSelection
		{
			get
			{
				return filterSelection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007956 RID: 31062 RVA: 0x001A041E File Offset: 0x0019E61E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007957 RID: 31063 RVA: 0x001A0434 File Offset: 0x0019E634
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007958 RID: 31064 RVA: 0x001A045E File Offset: 0x0019E65E
		public bool Equals(SingleSelection1 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400332C RID: 13100
		private ProgramNode _node;
	}
}
