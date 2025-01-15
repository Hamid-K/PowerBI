using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Compound.Split.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Compound.Split.Build.NodeTypes
{
	// Token: 0x0200096F RID: 2415
	public struct _LetB0 : IProgramNodeBuilder, IEquatable<_LetB0>
	{
		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06003978 RID: 14712 RVA: 0x000B21B2 File Offset: 0x000B03B2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000B21BA File Offset: 0x000B03BA
		private _LetB0(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x000B21C3 File Offset: 0x000B03C3
		public static _LetB0 CreateUnsafe(ProgramNode node)
		{
			return new _LetB0(node);
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x000B21CC File Offset: 0x000B03CC
		public static _LetB0? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol._LetB0)
			{
				return null;
			}
			return new _LetB0?(_LetB0.CreateUnsafe(node));
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x000B2206 File Offset: 0x000B0406
		public static _LetB0 CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new _LetB0(new Hole(g.Symbol._LetB0, holeId));
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x000B221E File Offset: 0x000B041E
		public SplitFile Cast_SplitFile()
		{
			return SplitFile.CreateUnsafe(this.Node);
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SplitFile(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x000B222B File Offset: 0x000B042B
		public bool Is_SplitFile(GrammarBuilders g, out SplitFile value)
		{
			value = SplitFile.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x000B223F File Offset: 0x000B043F
		public SplitFile? As_SplitFile(GrammarBuilders g)
		{
			return new SplitFile?(SplitFile.CreateUnsafe(this.Node));
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000B2251 File Offset: 0x000B0451
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000B2264 File Offset: 0x000B0464
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000B228E File Offset: 0x000B048E
		public bool Equals(_LetB0 other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04001A8F RID: 6799
		private ProgramNode _node;
	}
}
