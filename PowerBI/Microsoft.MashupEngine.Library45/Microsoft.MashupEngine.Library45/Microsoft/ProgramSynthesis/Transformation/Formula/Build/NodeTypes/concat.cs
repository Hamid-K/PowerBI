using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015A4 RID: 5540
	public struct concat : IProgramNodeBuilder, IEquatable<concat>
	{
		// Token: 0x17001FCA RID: 8138
		// (get) Token: 0x0600B626 RID: 46630 RVA: 0x00278D16 File Offset: 0x00276F16
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B627 RID: 46631 RVA: 0x00278D1E File Offset: 0x00276F1E
		private concat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B628 RID: 46632 RVA: 0x00278D27 File Offset: 0x00276F27
		public static concat CreateUnsafe(ProgramNode node)
		{
			return new concat(node);
		}

		// Token: 0x0600B629 RID: 46633 RVA: 0x00278D30 File Offset: 0x00276F30
		public static concat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.concat)
			{
				return null;
			}
			return new concat?(concat.CreateUnsafe(node));
		}

		// Token: 0x0600B62A RID: 46634 RVA: 0x00278D6A File Offset: 0x00276F6A
		public static concat CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new concat(new Hole(g.Symbol.concat, holeId));
		}

		// Token: 0x0600B62B RID: 46635 RVA: 0x00278D82 File Offset: 0x00276F82
		public Concat Cast_Concat()
		{
			return Concat.CreateUnsafe(this.Node);
		}

		// Token: 0x0600B62C RID: 46636 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_Concat(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600B62D RID: 46637 RVA: 0x00278D8F File Offset: 0x00276F8F
		public bool Is_Concat(GrammarBuilders g, out Concat value)
		{
			value = Concat.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600B62E RID: 46638 RVA: 0x00278DA3 File Offset: 0x00276FA3
		public Concat? As_Concat(GrammarBuilders g)
		{
			return new Concat?(Concat.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B62F RID: 46639 RVA: 0x00278DB5 File Offset: 0x00276FB5
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B630 RID: 46640 RVA: 0x00278DC8 File Offset: 0x00276FC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B631 RID: 46641 RVA: 0x00278DF2 File Offset: 0x00276FF2
		public bool Equals(concat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004652 RID: 18002
		private ProgramNode _node;
	}
}
