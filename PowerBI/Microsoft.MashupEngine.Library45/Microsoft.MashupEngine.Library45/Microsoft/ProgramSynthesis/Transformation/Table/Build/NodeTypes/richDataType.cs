using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001ABB RID: 6843
	public struct richDataType : IProgramNodeBuilder, IEquatable<richDataType>
	{
		// Token: 0x170025DB RID: 9691
		// (get) Token: 0x0600E255 RID: 57941 RVA: 0x0030183A File Offset: 0x002FFA3A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E256 RID: 57942 RVA: 0x00301842 File Offset: 0x002FFA42
		private richDataType(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E257 RID: 57943 RVA: 0x0030184B File Offset: 0x002FFA4B
		public static richDataType CreateUnsafe(ProgramNode node)
		{
			return new richDataType(node);
		}

		// Token: 0x0600E258 RID: 57944 RVA: 0x00301854 File Offset: 0x002FFA54
		public static richDataType? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.richDataType)
			{
				return null;
			}
			return new richDataType?(richDataType.CreateUnsafe(node));
		}

		// Token: 0x0600E259 RID: 57945 RVA: 0x0030188E File Offset: 0x002FFA8E
		public static richDataType CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new richDataType(new Hole(g.Symbol.richDataType, holeId));
		}

		// Token: 0x0600E25A RID: 57946 RVA: 0x003018A6 File Offset: 0x002FFAA6
		public richDataType(GrammarBuilders g, IRichDataType value)
		{
			this = new richDataType(new LiteralNode(g.Symbol.richDataType, value));
		}

		// Token: 0x170025DC RID: 9692
		// (get) Token: 0x0600E25B RID: 57947 RVA: 0x003018BF File Offset: 0x002FFABF
		public IRichDataType Value
		{
			get
			{
				return (IRichDataType)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600E25C RID: 57948 RVA: 0x003018D6 File Offset: 0x002FFAD6
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E25D RID: 57949 RVA: 0x003018EC File Offset: 0x002FFAEC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E25E RID: 57950 RVA: 0x00301916 File Offset: 0x002FFB16
		public bool Equals(richDataType other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400557A RID: 21882
		private ProgramNode _node;
	}
}
