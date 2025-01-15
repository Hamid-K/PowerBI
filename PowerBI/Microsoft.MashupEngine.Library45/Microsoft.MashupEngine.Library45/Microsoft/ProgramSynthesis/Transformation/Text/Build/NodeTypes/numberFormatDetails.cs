using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C6B RID: 7275
	public struct numberFormatDetails : IProgramNodeBuilder, IEquatable<numberFormatDetails>
	{
		// Token: 0x1700290E RID: 10510
		// (get) Token: 0x0600F666 RID: 63078 RVA: 0x0034930E File Offset: 0x0034750E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F667 RID: 63079 RVA: 0x00349316 File Offset: 0x00347516
		private numberFormatDetails(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F668 RID: 63080 RVA: 0x0034931F File Offset: 0x0034751F
		public static numberFormatDetails CreateUnsafe(ProgramNode node)
		{
			return new numberFormatDetails(node);
		}

		// Token: 0x0600F669 RID: 63081 RVA: 0x00349328 File Offset: 0x00347528
		public static numberFormatDetails? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.numberFormatDetails)
			{
				return null;
			}
			return new numberFormatDetails?(numberFormatDetails.CreateUnsafe(node));
		}

		// Token: 0x0600F66A RID: 63082 RVA: 0x00349362 File Offset: 0x00347562
		public static numberFormatDetails CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new numberFormatDetails(new Hole(g.Symbol.numberFormatDetails, holeId));
		}

		// Token: 0x0600F66B RID: 63083 RVA: 0x0034937A File Offset: 0x0034757A
		public numberFormatDetails(GrammarBuilders g, NumberFormatDetails value)
		{
			this = new numberFormatDetails(new LiteralNode(g.Symbol.numberFormatDetails, value));
		}

		// Token: 0x1700290F RID: 10511
		// (get) Token: 0x0600F66C RID: 63084 RVA: 0x00349393 File Offset: 0x00347593
		public NumberFormatDetails Value
		{
			get
			{
				return (NumberFormatDetails)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600F66D RID: 63085 RVA: 0x003493AA File Offset: 0x003475AA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F66E RID: 63086 RVA: 0x003493C0 File Offset: 0x003475C0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F66F RID: 63087 RVA: 0x003493EA File Offset: 0x003475EA
		public bool Equals(numberFormatDetails other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B5A RID: 23386
		private ProgramNode _node;
	}
}
