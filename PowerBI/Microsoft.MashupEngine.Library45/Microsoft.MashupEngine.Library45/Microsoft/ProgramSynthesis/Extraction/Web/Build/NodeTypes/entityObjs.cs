using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200109A RID: 4250
	public struct entityObjs : IProgramNodeBuilder, IEquatable<entityObjs>
	{
		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x0600800B RID: 32779 RVA: 0x001ACE4A File Offset: 0x001AB04A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600800C RID: 32780 RVA: 0x001ACE52 File Offset: 0x001AB052
		private entityObjs(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600800D RID: 32781 RVA: 0x001ACE5B File Offset: 0x001AB05B
		public static entityObjs CreateUnsafe(ProgramNode node)
		{
			return new entityObjs(node);
		}

		// Token: 0x0600800E RID: 32782 RVA: 0x001ACE64 File Offset: 0x001AB064
		public static entityObjs? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.entityObjs)
			{
				return null;
			}
			return new entityObjs?(entityObjs.CreateUnsafe(node));
		}

		// Token: 0x0600800F RID: 32783 RVA: 0x001ACE9E File Offset: 0x001AB09E
		public static entityObjs CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new entityObjs(new Hole(g.Symbol.entityObjs, holeId));
		}

		// Token: 0x06008010 RID: 32784 RVA: 0x001ACEB6 File Offset: 0x001AB0B6
		public entityObjs(GrammarBuilders g, EntityDetector[] value)
		{
			this = new entityObjs(new LiteralNode(g.Symbol.entityObjs, value));
		}

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x06008011 RID: 32785 RVA: 0x001ACECF File Offset: 0x001AB0CF
		public EntityDetector[] Value
		{
			get
			{
				return (EntityDetector[])((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x06008012 RID: 32786 RVA: 0x001ACEE6 File Offset: 0x001AB0E6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008013 RID: 32787 RVA: 0x001ACEFC File Offset: 0x001AB0FC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008014 RID: 32788 RVA: 0x001ACF26 File Offset: 0x001AB126
		public bool Equals(entityObjs other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040033B3 RID: 13235
		private ProgramNode _node;
	}
}
