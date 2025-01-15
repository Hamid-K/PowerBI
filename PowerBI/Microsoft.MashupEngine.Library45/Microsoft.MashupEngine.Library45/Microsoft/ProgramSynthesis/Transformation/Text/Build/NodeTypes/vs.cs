using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C7D RID: 7293
	public struct vs : IProgramNodeBuilder, IEquatable<vs>
	{
		// Token: 0x17002932 RID: 10546
		// (get) Token: 0x0600F71A RID: 63258 RVA: 0x0034A36E File Offset: 0x0034856E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F71B RID: 63259 RVA: 0x0034A376 File Offset: 0x00348576
		private vs(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F71C RID: 63260 RVA: 0x0034A37F File Offset: 0x0034857F
		public static vs CreateUnsafe(ProgramNode node)
		{
			return new vs(node);
		}

		// Token: 0x0600F71D RID: 63261 RVA: 0x0034A388 File Offset: 0x00348588
		public static vs? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.vs)
			{
				return null;
			}
			return new vs?(vs.CreateUnsafe(node));
		}

		// Token: 0x0600F71E RID: 63262 RVA: 0x0034A3C2 File Offset: 0x003485C2
		public static vs CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new vs(new Hole(g.Symbol.vs, holeId));
		}

		// Token: 0x0600F71F RID: 63263 RVA: 0x0034A3DA File Offset: 0x003485DA
		public vs(GrammarBuilders g)
		{
			this = new vs(new VariableNode(g.Symbol.vs));
		}

		// Token: 0x17002933 RID: 10547
		// (get) Token: 0x0600F720 RID: 63264 RVA: 0x0034A3F2 File Offset: 0x003485F2
		public VariableNode Variable
		{
			get
			{
				return (VariableNode)this.Node;
			}
		}

		// Token: 0x0600F721 RID: 63265 RVA: 0x0034A3FF File Offset: 0x003485FF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F722 RID: 63266 RVA: 0x0034A414 File Offset: 0x00348614
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F723 RID: 63267 RVA: 0x0034A43E File Offset: 0x0034863E
		public bool Equals(vs other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B6C RID: 23404
		private ProgramNode _node;
	}
}
