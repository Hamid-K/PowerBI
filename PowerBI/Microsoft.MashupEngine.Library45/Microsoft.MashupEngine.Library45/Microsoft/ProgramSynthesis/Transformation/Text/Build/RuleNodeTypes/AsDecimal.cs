using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C1D RID: 7197
	public struct AsDecimal : IProgramNodeBuilder, IEquatable<AsDecimal>
	{
		// Token: 0x17002883 RID: 10371
		// (get) Token: 0x0600F24A RID: 62026 RVA: 0x00340D06 File Offset: 0x0033EF06
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F24B RID: 62027 RVA: 0x00340D0E File Offset: 0x0033EF0E
		private AsDecimal(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F24C RID: 62028 RVA: 0x00340D17 File Offset: 0x0033EF17
		public static AsDecimal CreateUnsafe(ProgramNode node)
		{
			return new AsDecimal(node);
		}

		// Token: 0x0600F24D RID: 62029 RVA: 0x00340D20 File Offset: 0x0033EF20
		public static AsDecimal? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.AsDecimal)
			{
				return null;
			}
			return new AsDecimal?(AsDecimal.CreateUnsafe(node));
		}

		// Token: 0x0600F24E RID: 62030 RVA: 0x00340D55 File Offset: 0x0033EF55
		public AsDecimal(GrammarBuilders g, cell value0)
		{
			this._node = g.Rule.AsDecimal.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F24F RID: 62031 RVA: 0x00340D74 File Offset: 0x0033EF74
		public static implicit operator castToNumber(AsDecimal arg)
		{
			return castToNumber.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002884 RID: 10372
		// (get) Token: 0x0600F250 RID: 62032 RVA: 0x00340D82 File Offset: 0x0033EF82
		public cell cell
		{
			get
			{
				return cell.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F251 RID: 62033 RVA: 0x00340D96 File Offset: 0x0033EF96
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F252 RID: 62034 RVA: 0x00340DAC File Offset: 0x0033EFAC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F253 RID: 62035 RVA: 0x00340DD6 File Offset: 0x0033EFD6
		public bool Equals(AsDecimal other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B0C RID: 23308
		private ProgramNode _node;
	}
}
