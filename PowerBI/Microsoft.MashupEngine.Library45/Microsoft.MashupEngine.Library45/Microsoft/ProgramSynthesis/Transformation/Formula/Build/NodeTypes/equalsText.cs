using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015CF RID: 5583
	public struct equalsText : IProgramNodeBuilder, IEquatable<equalsText>
	{
		// Token: 0x17001FF5 RID: 8181
		// (get) Token: 0x0600B93A RID: 47418 RVA: 0x00280CBA File Offset: 0x0027EEBA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B93B RID: 47419 RVA: 0x00280CC2 File Offset: 0x0027EEC2
		private equalsText(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B93C RID: 47420 RVA: 0x00280CCB File Offset: 0x0027EECB
		public static equalsText CreateUnsafe(ProgramNode node)
		{
			return new equalsText(node);
		}

		// Token: 0x0600B93D RID: 47421 RVA: 0x00280CD4 File Offset: 0x0027EED4
		public static equalsText? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.equalsText)
			{
				return null;
			}
			return new equalsText?(equalsText.CreateUnsafe(node));
		}

		// Token: 0x0600B93E RID: 47422 RVA: 0x00280D0E File Offset: 0x0027EF0E
		public static equalsText CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new equalsText(new Hole(g.Symbol.equalsText, holeId));
		}

		// Token: 0x0600B93F RID: 47423 RVA: 0x00280D26 File Offset: 0x0027EF26
		public equalsText(GrammarBuilders g, string value)
		{
			this = new equalsText(new LiteralNode(g.Symbol.equalsText, value));
		}

		// Token: 0x17001FF6 RID: 8182
		// (get) Token: 0x0600B940 RID: 47424 RVA: 0x00280D3F File Offset: 0x0027EF3F
		public string Value
		{
			get
			{
				return (string)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B941 RID: 47425 RVA: 0x00280D56 File Offset: 0x0027EF56
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B942 RID: 47426 RVA: 0x00280D6C File Offset: 0x0027EF6C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B943 RID: 47427 RVA: 0x00280D96 File Offset: 0x0027EF96
		public bool Equals(equalsText other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400467D RID: 18045
		private ProgramNode _node;
	}
}
