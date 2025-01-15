using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build.RuleNodeTypes
{
	// Token: 0x0200127E RID: 4734
	public struct LetEText : IProgramNodeBuilder, IEquatable<LetEText>
	{
		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x06008F0F RID: 36623 RVA: 0x001E1F66 File Offset: 0x001E0166
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06008F10 RID: 36624 RVA: 0x001E1F6E File Offset: 0x001E016E
		private LetEText(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06008F11 RID: 36625 RVA: 0x001E1F77 File Offset: 0x001E0177
		public static LetEText CreateUnsafe(ProgramNode node)
		{
			return new LetEText(node);
		}

		// Token: 0x06008F12 RID: 36626 RVA: 0x001E1F80 File Offset: 0x001E0180
		public static LetEText? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetEText)
			{
				return null;
			}
			return new LetEText?(LetEText.CreateUnsafe(node));
		}

		// Token: 0x06008F13 RID: 36627 RVA: 0x001E1FB5 File Offset: 0x001E01B5
		public LetEText(GrammarBuilders g, _LetB0 value0, _LetB1 value1)
		{
			this._node = new LetNode(g.Rule.LetEText, value0.Node, value1.Node);
		}

		// Token: 0x06008F14 RID: 36628 RVA: 0x001E1FDB File Offset: 0x001E01DB
		public static implicit operator eText(LetEText arg)
		{
			return eText.CreateUnsafe(arg.Node);
		}

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06008F15 RID: 36629 RVA: 0x001E1FE9 File Offset: 0x001E01E9
		public _LetB0 _LetB0
		{
			get
			{
				return _LetB0.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x06008F16 RID: 36630 RVA: 0x001E1FFD File Offset: 0x001E01FD
		public _LetB1 _LetB1
		{
			get
			{
				return _LetB1.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06008F17 RID: 36631 RVA: 0x001E2011 File Offset: 0x001E0211
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06008F18 RID: 36632 RVA: 0x001E2024 File Offset: 0x001E0224
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06008F19 RID: 36633 RVA: 0x001E204E File Offset: 0x001E024E
		public bool Equals(LetEText other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003A6F RID: 14959
		private ProgramNode _node;
	}
}
