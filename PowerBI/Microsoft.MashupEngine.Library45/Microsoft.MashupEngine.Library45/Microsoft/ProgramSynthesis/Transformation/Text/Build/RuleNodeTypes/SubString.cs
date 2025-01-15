using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C28 RID: 7208
	public struct SubString : IProgramNodeBuilder, IEquatable<SubString>
	{
		// Token: 0x170028A3 RID: 10403
		// (get) Token: 0x0600F2C2 RID: 62146 RVA: 0x0034181A File Offset: 0x0033FA1A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F2C3 RID: 62147 RVA: 0x00341822 File Offset: 0x0033FA22
		private SubString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F2C4 RID: 62148 RVA: 0x0034182B File Offset: 0x0033FA2B
		public static SubString CreateUnsafe(ProgramNode node)
		{
			return new SubString(node);
		}

		// Token: 0x0600F2C5 RID: 62149 RVA: 0x00341834 File Offset: 0x0033FA34
		public static SubString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SubString)
			{
				return null;
			}
			return new SubString?(SubString.CreateUnsafe(node));
		}

		// Token: 0x0600F2C6 RID: 62150 RVA: 0x00341869 File Offset: 0x0033FA69
		public SubString(GrammarBuilders g, SS value0)
		{
			this._node = g.Rule.SubString.BuildASTNode(value0.Node);
		}

		// Token: 0x0600F2C7 RID: 62151 RVA: 0x00341888 File Offset: 0x0033FA88
		public static implicit operator conv(SubString arg)
		{
			return conv.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028A4 RID: 10404
		// (get) Token: 0x0600F2C8 RID: 62152 RVA: 0x00341896 File Offset: 0x0033FA96
		public SS SS
		{
			get
			{
				return SS.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600F2C9 RID: 62153 RVA: 0x003418AA File Offset: 0x0033FAAA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F2CA RID: 62154 RVA: 0x003418C0 File Offset: 0x0033FAC0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F2CB RID: 62155 RVA: 0x003418EA File Offset: 0x0033FAEA
		public bool Equals(SubString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B17 RID: 23319
		private ProgramNode _node;
	}
}
