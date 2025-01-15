using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes
{
	// Token: 0x02000F2E RID: 3886
	public struct Group : IProgramNodeBuilder, IEquatable<Group>
	{
		// Token: 0x17001340 RID: 4928
		// (get) Token: 0x06006B9E RID: 27550 RVA: 0x0016153A File Offset: 0x0015F73A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006B9F RID: 27551 RVA: 0x00161542 File Offset: 0x0015F742
		private Group(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006BA0 RID: 27552 RVA: 0x0016154B File Offset: 0x0015F74B
		public static Group CreateUnsafe(ProgramNode node)
		{
			return new Group(node);
		}

		// Token: 0x06006BA1 RID: 27553 RVA: 0x00161554 File Offset: 0x0015F754
		public static Group? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Group)
			{
				return null;
			}
			return new Group?(Group.CreateUnsafe(node));
		}

		// Token: 0x06006BA2 RID: 27554 RVA: 0x00161589 File Offset: 0x0015F789
		public Group(GrammarBuilders g, re value0, skip value1)
		{
			this._node = g.Rule.Group.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06006BA3 RID: 27555 RVA: 0x001615AF File Offset: 0x0015F7AF
		public static implicit operator records(Group arg)
		{
			return records.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001341 RID: 4929
		// (get) Token: 0x06006BA4 RID: 27556 RVA: 0x001615BD File Offset: 0x0015F7BD
		public re re
		{
			get
			{
				return re.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x06006BA5 RID: 27557 RVA: 0x001615D1 File Offset: 0x0015F7D1
		public skip skip
		{
			get
			{
				return skip.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006BA6 RID: 27558 RVA: 0x001615E5 File Offset: 0x0015F7E5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006BA7 RID: 27559 RVA: 0x001615F8 File Offset: 0x0015F7F8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006BA8 RID: 27560 RVA: 0x00161622 File Offset: 0x0015F822
		public bool Equals(Group other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F19 RID: 12057
		private ProgramNode _node;
	}
}
