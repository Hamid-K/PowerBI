using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001ABC RID: 6844
	public struct fillMethod : IProgramNodeBuilder, IEquatable<fillMethod>
	{
		// Token: 0x170025DD RID: 9693
		// (get) Token: 0x0600E25F RID: 57951 RVA: 0x0030192A File Offset: 0x002FFB2A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E260 RID: 57952 RVA: 0x00301932 File Offset: 0x002FFB32
		private fillMethod(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E261 RID: 57953 RVA: 0x0030193B File Offset: 0x002FFB3B
		public static fillMethod CreateUnsafe(ProgramNode node)
		{
			return new fillMethod(node);
		}

		// Token: 0x0600E262 RID: 57954 RVA: 0x00301944 File Offset: 0x002FFB44
		public static fillMethod? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fillMethod)
			{
				return null;
			}
			return new fillMethod?(fillMethod.CreateUnsafe(node));
		}

		// Token: 0x0600E263 RID: 57955 RVA: 0x0030197E File Offset: 0x002FFB7E
		public static fillMethod CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fillMethod(new Hole(g.Symbol.fillMethod, holeId));
		}

		// Token: 0x0600E264 RID: 57956 RVA: 0x00301996 File Offset: 0x002FFB96
		public fillMethod(GrammarBuilders g, FillMethod value)
		{
			this = new fillMethod(new LiteralNode(g.Symbol.fillMethod, value));
		}

		// Token: 0x170025DE RID: 9694
		// (get) Token: 0x0600E265 RID: 57957 RVA: 0x003019B4 File Offset: 0x002FFBB4
		public FillMethod Value
		{
			get
			{
				return (FillMethod)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600E266 RID: 57958 RVA: 0x003019CB File Offset: 0x002FFBCB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E267 RID: 57959 RVA: 0x003019E0 File Offset: 0x002FFBE0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E268 RID: 57960 RVA: 0x00301A0A File Offset: 0x002FFC0A
		public bool Equals(fillMethod other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400557B RID: 21883
		private ProgramNode _node;
	}
}
