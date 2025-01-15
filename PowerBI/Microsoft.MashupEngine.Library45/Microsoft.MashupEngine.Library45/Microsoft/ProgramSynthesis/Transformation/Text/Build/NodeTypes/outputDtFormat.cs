using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C6D RID: 7277
	public struct outputDtFormat : IProgramNodeBuilder, IEquatable<outputDtFormat>
	{
		// Token: 0x17002912 RID: 10514
		// (get) Token: 0x0600F67A RID: 63098 RVA: 0x003494EE File Offset: 0x003476EE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F67B RID: 63099 RVA: 0x003494F6 File Offset: 0x003476F6
		private outputDtFormat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F67C RID: 63100 RVA: 0x003494FF File Offset: 0x003476FF
		public static outputDtFormat CreateUnsafe(ProgramNode node)
		{
			return new outputDtFormat(node);
		}

		// Token: 0x0600F67D RID: 63101 RVA: 0x00349508 File Offset: 0x00347708
		public static outputDtFormat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.outputDtFormat)
			{
				return null;
			}
			return new outputDtFormat?(outputDtFormat.CreateUnsafe(node));
		}

		// Token: 0x0600F67E RID: 63102 RVA: 0x00349542 File Offset: 0x00347742
		public static outputDtFormat CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new outputDtFormat(new Hole(g.Symbol.outputDtFormat, holeId));
		}

		// Token: 0x0600F67F RID: 63103 RVA: 0x0034955A File Offset: 0x0034775A
		public outputDtFormat(GrammarBuilders g, DateTimeFormat value)
		{
			this = new outputDtFormat(new LiteralNode(g.Symbol.outputDtFormat, value));
		}

		// Token: 0x17002913 RID: 10515
		// (get) Token: 0x0600F680 RID: 63104 RVA: 0x00349573 File Offset: 0x00347773
		public DateTimeFormat Value
		{
			get
			{
				return (DateTimeFormat)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F681 RID: 63105 RVA: 0x0034958A File Offset: 0x0034778A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F682 RID: 63106 RVA: 0x003495A0 File Offset: 0x003477A0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F683 RID: 63107 RVA: 0x003495CA File Offset: 0x003477CA
		public bool Equals(outputDtFormat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B5C RID: 23388
		private ProgramNode _node;
	}
}
