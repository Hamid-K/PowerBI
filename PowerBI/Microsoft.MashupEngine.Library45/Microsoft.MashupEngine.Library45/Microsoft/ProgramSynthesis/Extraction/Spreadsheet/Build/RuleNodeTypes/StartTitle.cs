using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E4F RID: 3663
	public struct StartTitle : IProgramNodeBuilder, IEquatable<StartTitle>
	{
		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06006267 RID: 25191 RVA: 0x0014084E File Offset: 0x0013EA4E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x00140856 File Offset: 0x0013EA56
		private StartTitle(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x0014085F File Offset: 0x0013EA5F
		public static StartTitle CreateUnsafe(ProgramNode node)
		{
			return new StartTitle(node);
		}

		// Token: 0x0600626A RID: 25194 RVA: 0x00140868 File Offset: 0x0013EA68
		public static StartTitle? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.StartTitle)
			{
				return null;
			}
			return new StartTitle?(StartTitle.CreateUnsafe(node));
		}

		// Token: 0x0600626B RID: 25195 RVA: 0x0014089D File Offset: 0x0013EA9D
		public StartTitle(GrammarBuilders g, title value0)
		{
			this._node = g.Rule.StartTitle.BuildASTNode(value0.Node);
		}

		// Token: 0x0600626C RID: 25196 RVA: 0x001408BC File Offset: 0x0013EABC
		public static implicit operator startTitle(StartTitle arg)
		{
			return startTitle.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x0600626D RID: 25197 RVA: 0x001408CA File Offset: 0x0013EACA
		public title title
		{
			get
			{
				return title.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600626E RID: 25198 RVA: 0x001408DE File Offset: 0x0013EADE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600626F RID: 25199 RVA: 0x001408F4 File Offset: 0x0013EAF4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006270 RID: 25200 RVA: 0x0014091E File Offset: 0x0013EB1E
		public bool Equals(StartTitle other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BF9 RID: 11257
		private ProgramNode _node;
	}
}
