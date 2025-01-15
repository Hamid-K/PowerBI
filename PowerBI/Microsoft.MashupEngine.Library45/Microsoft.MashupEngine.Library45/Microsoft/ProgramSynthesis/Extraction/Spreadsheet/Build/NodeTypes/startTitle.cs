using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E65 RID: 3685
	public struct startTitle : IProgramNodeBuilder, IEquatable<startTitle>
	{
		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x0600640F RID: 25615 RVA: 0x00145202 File Offset: 0x00143402
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006410 RID: 25616 RVA: 0x0014520A File Offset: 0x0014340A
		private startTitle(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006411 RID: 25617 RVA: 0x00145213 File Offset: 0x00143413
		public static startTitle CreateUnsafe(ProgramNode node)
		{
			return new startTitle(node);
		}

		// Token: 0x06006412 RID: 25618 RVA: 0x0014521C File Offset: 0x0014341C
		public static startTitle? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.startTitle)
			{
				return null;
			}
			return new startTitle?(startTitle.CreateUnsafe(node));
		}

		// Token: 0x06006413 RID: 25619 RVA: 0x00145256 File Offset: 0x00143456
		public static startTitle CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new startTitle(new Hole(g.Symbol.startTitle, holeId));
		}

		// Token: 0x06006414 RID: 25620 RVA: 0x0014526E File Offset: 0x0014346E
		public StartTitle Cast_StartTitle()
		{
			return StartTitle.CreateUnsafe(this.Node);
		}

		// Token: 0x06006415 RID: 25621 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_StartTitle(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06006416 RID: 25622 RVA: 0x0014527B File Offset: 0x0014347B
		public bool Is_StartTitle(GrammarBuilders g, out StartTitle value)
		{
			value = StartTitle.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06006417 RID: 25623 RVA: 0x0014528F File Offset: 0x0014348F
		public StartTitle? As_StartTitle(GrammarBuilders g)
		{
			return new StartTitle?(StartTitle.CreateUnsafe(this.Node));
		}

		// Token: 0x06006418 RID: 25624 RVA: 0x001452A1 File Offset: 0x001434A1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006419 RID: 25625 RVA: 0x001452B4 File Offset: 0x001434B4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600641A RID: 25626 RVA: 0x001452DE File Offset: 0x001434DE
		public bool Equals(startTitle other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C0F RID: 11279
		private ProgramNode _node;
	}
}
