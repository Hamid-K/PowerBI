using System;
using Microsoft.ProgramSynthesis.AST;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes
{
	// Token: 0x02001A4B RID: 6731
	public struct t : IProgramNodeBuilder, IEquatable<t>
	{
		// Token: 0x1700251C RID: 9500
		// (get) Token: 0x0600DDDA RID: 56794 RVA: 0x002F2316 File Offset: 0x002F0516
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600DDDB RID: 56795 RVA: 0x002F231E File Offset: 0x002F051E
		private t(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600DDDC RID: 56796 RVA: 0x002F2327 File Offset: 0x002F0527
		public static t CreateUnsafe(ProgramNode node)
		{
			return new t(node);
		}

		// Token: 0x0600DDDD RID: 56797 RVA: 0x002F2330 File Offset: 0x002F0530
		public static t? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.t)
			{
				return null;
			}
			return new t?(t.CreateUnsafe(node));
		}

		// Token: 0x0600DDDE RID: 56798 RVA: 0x002F236A File Offset: 0x002F056A
		public static t CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new t(new Hole(g.Symbol.t, holeId));
		}

		// Token: 0x0600DDDF RID: 56799 RVA: 0x002F2382 File Offset: 0x002F0582
		public t(GrammarBuilders g, JTokenType value)
		{
			this = new t(new LiteralNode(g.Symbol.t, value));
		}

		// Token: 0x1700251D RID: 9501
		// (get) Token: 0x0600DDE0 RID: 56800 RVA: 0x002F23A0 File Offset: 0x002F05A0
		public JTokenType Value
		{
			get
			{
				return (JTokenType)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600DDE1 RID: 56801 RVA: 0x002F23B7 File Offset: 0x002F05B7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600DDE2 RID: 56802 RVA: 0x002F23CC File Offset: 0x002F05CC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600DDE3 RID: 56803 RVA: 0x002F23F6 File Offset: 0x002F05F6
		public bool Equals(t other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400543C RID: 21564
		private ProgramNode _node;
	}
}
