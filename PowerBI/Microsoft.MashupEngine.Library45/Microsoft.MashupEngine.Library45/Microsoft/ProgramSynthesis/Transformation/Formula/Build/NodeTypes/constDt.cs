using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015DD RID: 5597
	public struct constDt : IProgramNodeBuilder, IEquatable<constDt>
	{
		// Token: 0x17002011 RID: 8209
		// (get) Token: 0x0600B9C6 RID: 47558 RVA: 0x002819F2 File Offset: 0x0027FBF2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B9C7 RID: 47559 RVA: 0x002819FA File Offset: 0x0027FBFA
		private constDt(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B9C8 RID: 47560 RVA: 0x00281A03 File Offset: 0x0027FC03
		public static constDt CreateUnsafe(ProgramNode node)
		{
			return new constDt(node);
		}

		// Token: 0x0600B9C9 RID: 47561 RVA: 0x00281A0C File Offset: 0x0027FC0C
		public static constDt? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.constDt)
			{
				return null;
			}
			return new constDt?(constDt.CreateUnsafe(node));
		}

		// Token: 0x0600B9CA RID: 47562 RVA: 0x00281A46 File Offset: 0x0027FC46
		public static constDt CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new constDt(new Hole(g.Symbol.constDt, holeId));
		}

		// Token: 0x0600B9CB RID: 47563 RVA: 0x00281A5E File Offset: 0x0027FC5E
		public constDt(GrammarBuilders g, DateTime value)
		{
			this = new constDt(new LiteralNode(g.Symbol.constDt, value));
		}

		// Token: 0x17002012 RID: 8210
		// (get) Token: 0x0600B9CC RID: 47564 RVA: 0x00281A7C File Offset: 0x0027FC7C
		public DateTime Value
		{
			get
			{
				return (DateTime)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B9CD RID: 47565 RVA: 0x00281A93 File Offset: 0x0027FC93
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B9CE RID: 47566 RVA: 0x00281AA8 File Offset: 0x0027FCA8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B9CF RID: 47567 RVA: 0x00281AD2 File Offset: 0x0027FCD2
		public bool Equals(constDt other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400468B RID: 18059
		private ProgramNode _node;
	}
}
