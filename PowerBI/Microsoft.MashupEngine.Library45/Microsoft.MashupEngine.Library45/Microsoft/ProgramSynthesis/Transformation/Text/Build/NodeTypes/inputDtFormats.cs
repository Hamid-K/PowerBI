using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C6E RID: 7278
	public struct inputDtFormats : IProgramNodeBuilder, IEquatable<inputDtFormats>
	{
		// Token: 0x17002914 RID: 10516
		// (get) Token: 0x0600F684 RID: 63108 RVA: 0x003495DE File Offset: 0x003477DE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F685 RID: 63109 RVA: 0x003495E6 File Offset: 0x003477E6
		private inputDtFormats(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F686 RID: 63110 RVA: 0x003495EF File Offset: 0x003477EF
		public static inputDtFormats CreateUnsafe(ProgramNode node)
		{
			return new inputDtFormats(node);
		}

		// Token: 0x0600F687 RID: 63111 RVA: 0x003495F8 File Offset: 0x003477F8
		public static inputDtFormats? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.inputDtFormats)
			{
				return null;
			}
			return new inputDtFormats?(inputDtFormats.CreateUnsafe(node));
		}

		// Token: 0x0600F688 RID: 63112 RVA: 0x00349632 File Offset: 0x00347832
		public static inputDtFormats CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new inputDtFormats(new Hole(g.Symbol.inputDtFormats, holeId));
		}

		// Token: 0x0600F689 RID: 63113 RVA: 0x0034964A File Offset: 0x0034784A
		public inputDtFormats(GrammarBuilders g, DateTimeFormat[] value)
		{
			this = new inputDtFormats(new LiteralNode(g.Symbol.inputDtFormats, value));
		}

		// Token: 0x17002915 RID: 10517
		// (get) Token: 0x0600F68A RID: 63114 RVA: 0x00349663 File Offset: 0x00347863
		public DateTimeFormat[] Value
		{
			get
			{
				return (DateTimeFormat[])((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F68B RID: 63115 RVA: 0x0034967A File Offset: 0x0034787A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F68C RID: 63116 RVA: 0x00349690 File Offset: 0x00347890
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F68D RID: 63117 RVA: 0x003496BA File Offset: 0x003478BA
		public bool Equals(inputDtFormats other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B5D RID: 23389
		private ProgramNode _node;
	}
}
