using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015E5 RID: 5605
	public struct dateTimeRoundDesc : IProgramNodeBuilder, IEquatable<dateTimeRoundDesc>
	{
		// Token: 0x17002021 RID: 8225
		// (get) Token: 0x0600BA16 RID: 47638 RVA: 0x00282176 File Offset: 0x00280376
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA17 RID: 47639 RVA: 0x0028217E File Offset: 0x0028037E
		private dateTimeRoundDesc(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA18 RID: 47640 RVA: 0x00282187 File Offset: 0x00280387
		public static dateTimeRoundDesc CreateUnsafe(ProgramNode node)
		{
			return new dateTimeRoundDesc(node);
		}

		// Token: 0x0600BA19 RID: 47641 RVA: 0x00282190 File Offset: 0x00280390
		public static dateTimeRoundDesc? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.dateTimeRoundDesc)
			{
				return null;
			}
			return new dateTimeRoundDesc?(dateTimeRoundDesc.CreateUnsafe(node));
		}

		// Token: 0x0600BA1A RID: 47642 RVA: 0x002821CA File Offset: 0x002803CA
		public static dateTimeRoundDesc CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new dateTimeRoundDesc(new Hole(g.Symbol.dateTimeRoundDesc, holeId));
		}

		// Token: 0x0600BA1B RID: 47643 RVA: 0x002821E2 File Offset: 0x002803E2
		public dateTimeRoundDesc(GrammarBuilders g, RoundDateTimeDescriptor value)
		{
			this = new dateTimeRoundDesc(new LiteralNode(g.Symbol.dateTimeRoundDesc, value));
		}

		// Token: 0x17002022 RID: 8226
		// (get) Token: 0x0600BA1C RID: 47644 RVA: 0x002821FB File Offset: 0x002803FB
		public RoundDateTimeDescriptor Value
		{
			get
			{
				return (RoundDateTimeDescriptor)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA1D RID: 47645 RVA: 0x00282212 File Offset: 0x00280412
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA1E RID: 47646 RVA: 0x00282228 File Offset: 0x00280428
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA1F RID: 47647 RVA: 0x00282252 File Offset: 0x00280452
		public bool Equals(dateTimeRoundDesc other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004693 RID: 18067
		private ProgramNode _node;
	}
}
