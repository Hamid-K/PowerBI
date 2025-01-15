using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C04 RID: 7172
	public struct FormatNumber : IProgramNodeBuilder, IEquatable<FormatNumber>
	{
		// Token: 0x17002831 RID: 10289
		// (get) Token: 0x0600F130 RID: 61744 RVA: 0x0033F35A File Offset: 0x0033D55A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F131 RID: 61745 RVA: 0x0033F362 File Offset: 0x0033D562
		private FormatNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F132 RID: 61746 RVA: 0x0033F36B File Offset: 0x0033D56B
		public static FormatNumber CreateUnsafe(ProgramNode node)
		{
			return new FormatNumber(node);
		}

		// Token: 0x0600F133 RID: 61747 RVA: 0x0033F374 File Offset: 0x0033D574
		public static FormatNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FormatNumber)
			{
				return null;
			}
			return new FormatNumber?(FormatNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F134 RID: 61748 RVA: 0x0033F3A9 File Offset: 0x0033D5A9
		public FormatNumber(GrammarBuilders g, number value0, numberFormat value1)
		{
			this._node = g.Rule.FormatNumber.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600F135 RID: 61749 RVA: 0x0033F3CF File Offset: 0x0033D5CF
		public static implicit operator conv(FormatNumber arg)
		{
			return conv.CreateUnsafe(arg.Node);
		}

		// Token: 0x17002832 RID: 10290
		// (get) Token: 0x0600F136 RID: 61750 RVA: 0x0033F3DD File Offset: 0x0033D5DD
		public number number
		{
			get
			{
				return number.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17002833 RID: 10291
		// (get) Token: 0x0600F137 RID: 61751 RVA: 0x0033F3F1 File Offset: 0x0033D5F1
		public numberFormat numberFormat
		{
			get
			{
				return numberFormat.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F138 RID: 61752 RVA: 0x0033F405 File Offset: 0x0033D605
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F139 RID: 61753 RVA: 0x0033F418 File Offset: 0x0033D618
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F13A RID: 61754 RVA: 0x0033F442 File Offset: 0x0033D642
		public bool Equals(FormatNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005AF3 RID: 23283
		private ProgramNode _node;
	}
}
