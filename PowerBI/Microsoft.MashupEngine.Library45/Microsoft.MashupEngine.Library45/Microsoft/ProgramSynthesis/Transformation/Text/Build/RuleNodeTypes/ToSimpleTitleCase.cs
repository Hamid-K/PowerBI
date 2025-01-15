using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C02 RID: 7170
	public struct ToSimpleTitleCase : IProgramNodeBuilder, IEquatable<ToSimpleTitleCase>
	{
		// Token: 0x1700282C RID: 10284
		// (get) Token: 0x0600F11B RID: 61723 RVA: 0x0033F17A File Offset: 0x0033D37A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F11C RID: 61724 RVA: 0x0033F182 File Offset: 0x0033D382
		private ToSimpleTitleCase(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F11D RID: 61725 RVA: 0x0033F18B File Offset: 0x0033D38B
		public static ToSimpleTitleCase CreateUnsafe(ProgramNode node)
		{
			return new ToSimpleTitleCase(node);
		}

		// Token: 0x0600F11E RID: 61726 RVA: 0x0033F194 File Offset: 0x0033D394
		public static ToSimpleTitleCase? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ToSimpleTitleCase)
			{
				return null;
			}
			return new ToSimpleTitleCase?(ToSimpleTitleCase.CreateUnsafe(node));
		}

		// Token: 0x0600F11F RID: 61727 RVA: 0x0033F1C9 File Offset: 0x0033D3C9
		public ToSimpleTitleCase(GrammarBuilders g, SS value0)
		{
			this._node = g.Rule.ToSimpleTitleCase.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F120 RID: 61728 RVA: 0x0033F1E8 File Offset: 0x0033D3E8
		public static implicit operator conv(ToSimpleTitleCase arg)
		{
			return conv.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700282D RID: 10285
		// (get) Token: 0x0600F121 RID: 61729 RVA: 0x0033F1F6 File Offset: 0x0033D3F6
		public SS SS
		{
			get
			{
				return SS.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F122 RID: 61730 RVA: 0x0033F20A File Offset: 0x0033D40A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F123 RID: 61731 RVA: 0x0033F220 File Offset: 0x0033D420
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F124 RID: 61732 RVA: 0x0033F24A File Offset: 0x0033D44A
		public bool Equals(ToSimpleTitleCase other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AF1 RID: 23281
		private ProgramNode _node;
	}
}
