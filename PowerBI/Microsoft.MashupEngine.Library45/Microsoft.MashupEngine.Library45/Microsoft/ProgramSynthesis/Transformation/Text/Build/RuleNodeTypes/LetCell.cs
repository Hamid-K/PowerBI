using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C2C RID: 7212
	public struct LetCell : IProgramNodeBuilder, IEquatable<LetCell>
	{
		// Token: 0x170028AC RID: 10412
		// (get) Token: 0x0600F2EB RID: 62187 RVA: 0x00341BC2 File Offset: 0x0033FDC2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F2EC RID: 62188 RVA: 0x00341BCA File Offset: 0x0033FDCA
		private LetCell(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F2ED RID: 62189 RVA: 0x00341BD3 File Offset: 0x0033FDD3
		public static LetCell CreateUnsafe(ProgramNode node)
		{
			return new LetCell(node);
		}

		// Token: 0x0600F2EE RID: 62190 RVA: 0x00341BDC File Offset: 0x0033FDDC
		public static LetCell? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetCell)
			{
				return null;
			}
			return new LetCell?(LetCell.CreateUnsafe(node));
		}

		// Token: 0x0600F2EF RID: 62191 RVA: 0x00341C11 File Offset: 0x0033FE11
		public LetCell(GrammarBuilders g, lookupInput value0, conv value1)
		{
			this._node = new LetNode(g.Rule.LetCell, value0.Node, value1.Node);
		}

		// Token: 0x0600F2F0 RID: 62192 RVA: 0x00341C37 File Offset: 0x0033FE37
		public static implicit operator letOptions(LetCell arg)
		{
			return letOptions.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028AD RID: 10413
		// (get) Token: 0x0600F2F1 RID: 62193 RVA: 0x00341C45 File Offset: 0x0033FE45
		public lookupInput lookupInput
		{
			get
			{
				return lookupInput.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170028AE RID: 10414
		// (get) Token: 0x0600F2F2 RID: 62194 RVA: 0x00341C59 File Offset: 0x0033FE59
		public conv conv
		{
			get
			{
				return conv.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F2F3 RID: 62195 RVA: 0x00341C6D File Offset: 0x0033FE6D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F2F4 RID: 62196 RVA: 0x00341C80 File Offset: 0x0033FE80
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F2F5 RID: 62197 RVA: 0x00341CAA File Offset: 0x0033FEAA
		public bool Equals(LetCell other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B1B RID: 23323
		private ProgramNode _node;
	}
}
